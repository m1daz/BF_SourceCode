using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000658 RID: 1624
	public class MetaType : ISerializerProxy
	{
		// Token: 0x06002EF4 RID: 12020 RVA: 0x00155808 File Offset: 0x00153C08
		internal MetaType(RuntimeTypeModel model, Type type, MethodInfo factory)
		{
			this.factory = factory;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IProtoSerializer protoSerializer = model.TryGetBasicTypeSerializer(type);
			if (protoSerializer != null)
			{
				throw MetaType.InbuiltType(type);
			}
			this.type = type;
			this.model = model;
			if (Helpers.IsEnum(type))
			{
				this.EnumPassthru = type.IsDefined(model.MapType(typeof(FlagsAttribute)), false);
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0015589A File Offset: 0x00153C9A
		public override string ToString()
		{
			return this.type.ToString();
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x001558A7 File Offset: 0x00153CA7
		IProtoSerializer ISerializerProxy.Serializer
		{
			get
			{
				return this.Serializer;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x001558AF File Offset: 0x00153CAF
		public MetaType BaseType
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x001558B7 File Offset: 0x00153CB7
		internal TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x001558BF File Offset: 0x00153CBF
		// (set) Token: 0x06002EFA RID: 12026 RVA: 0x001558CB File Offset: 0x00153CCB
		public bool IncludeSerializerMethod
		{
			get
			{
				return !this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, !value, true);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x001558D9 File Offset: 0x00153CD9
		// (set) Token: 0x06002EFC RID: 12028 RVA: 0x001558E3 File Offset: 0x00153CE3
		public bool AsReferenceDefault
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value, true);
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x001558EF File Offset: 0x00153CEF
		private bool IsValidSubType(Type subType)
		{
			return this.type.IsAssignableFrom(subType);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x001558FD File Offset: 0x00153CFD
		public MetaType AddSubType(int fieldNumber, Type derivedType)
		{
			return this.AddSubType(fieldNumber, derivedType, DataFormat.Default);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x00155908 File Offset: 0x00153D08
		public MetaType AddSubType(int fieldNumber, Type derivedType, DataFormat dataFormat)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber < 1)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if ((!this.type.IsClass && !this.type.IsInterface) || this.type.IsSealed)
			{
				throw new InvalidOperationException("Sub-types can only be added to non-sealed classes");
			}
			if (!this.IsValidSubType(derivedType))
			{
				throw new ArgumentException(derivedType.Name + " is not a valid sub-type of " + this.type.Name, "derivedType");
			}
			MetaType metaType = this.model[derivedType];
			this.ThrowIfFrozen();
			metaType.ThrowIfFrozen();
			SubType value = new SubType(fieldNumber, metaType, dataFormat);
			this.ThrowIfFrozen();
			metaType.SetBaseType(this);
			if (this.subTypes == null)
			{
				this.subTypes = new BasicList();
			}
			this.subTypes.Add(value);
			return this;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x001559F8 File Offset: 0x00153DF8
		private void SetBaseType(MetaType baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (this.baseType == baseType)
			{
				return;
			}
			if (this.baseType != null)
			{
				throw new InvalidOperationException("A type can only participate in one inheritance hierarchy");
			}
			for (MetaType metaType = baseType; metaType != null; metaType = metaType.baseType)
			{
				if (object.ReferenceEquals(metaType, this))
				{
					throw new InvalidOperationException("Cyclic inheritance is not allowed");
				}
			}
			this.baseType = baseType;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x00155A6B File Offset: 0x00153E6B
		public bool HasCallbacks
		{
			get
			{
				return this.callbacks != null && this.callbacks.NonTrivial;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x00155A86 File Offset: 0x00153E86
		public bool HasSubtypes
		{
			get
			{
				return this.subTypes != null && this.subTypes.Count != 0;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x00155AA7 File Offset: 0x00153EA7
		public CallbackSet Callbacks
		{
			get
			{
				if (this.callbacks == null)
				{
					this.callbacks = new CallbackSet(this);
				}
				return this.callbacks;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x00155AC6 File Offset: 0x00153EC6
		private bool IsValueType
		{
			get
			{
				return this.type.IsValueType;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x00155AD4 File Offset: 0x00153ED4
		public MetaType SetCallbacks(MethodInfo beforeSerialize, MethodInfo afterSerialize, MethodInfo beforeDeserialize, MethodInfo afterDeserialize)
		{
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = beforeSerialize;
			callbackSet.AfterSerialize = afterSerialize;
			callbackSet.BeforeDeserialize = beforeDeserialize;
			callbackSet.AfterDeserialize = afterDeserialize;
			return this;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00155B08 File Offset: 0x00153F08
		public MetaType SetCallbacks(string beforeSerialize, string afterSerialize, string beforeDeserialize, string afterDeserialize)
		{
			if (this.IsValueType)
			{
				throw new InvalidOperationException();
			}
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = this.ResolveMethod(beforeSerialize, true);
			callbackSet.AfterSerialize = this.ResolveMethod(afterSerialize, true);
			callbackSet.BeforeDeserialize = this.ResolveMethod(beforeDeserialize, true);
			callbackSet.AfterDeserialize = this.ResolveMethod(afterDeserialize, true);
			return this;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00155B68 File Offset: 0x00153F68
		internal string GetSchemaTypeName()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate].GetSchemaTypeName();
			}
			if (!Helpers.IsNullOrEmpty(this.name))
			{
				return this.name;
			}
			string text = this.type.Name;
			if (this.type.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				int num = text.IndexOf('`');
				if (num >= 0)
				{
					stringBuilder.Length = num;
				}
				foreach (Type type in this.type.GetGenericArguments())
				{
					stringBuilder.Append('_');
					Type type2 = type;
					int key = this.model.GetKey(ref type2);
					MetaType metaType;
					if (key >= 0 && (metaType = this.model[type2]) != null && metaType.surrogate == null)
					{
						stringBuilder.Append(metaType.GetSchemaTypeName());
					}
					else
					{
						stringBuilder.Append(type2.Name);
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x00155C81 File Offset: 0x00154081
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x00155C89 File Offset: 0x00154089
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.ThrowIfFrozen();
				this.name = value;
			}
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00155C98 File Offset: 0x00154098
		public MetaType SetFactory(MethodInfo factory)
		{
			this.model.VerifyFactory(factory, this.type);
			this.ThrowIfFrozen();
			this.factory = factory;
			return this;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00155CBA File Offset: 0x001540BA
		public MetaType SetFactory(string factory)
		{
			return this.SetFactory(this.ResolveMethod(factory, false));
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x00155CCA File Offset: 0x001540CA
		private MethodInfo ResolveMethod(string name, bool instance)
		{
			if (Helpers.IsNullOrEmpty(name))
			{
				return null;
			}
			return (!instance) ? Helpers.GetStaticMethod(this.type, name) : Helpers.GetInstanceMethod(this.type, name);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x00155CFC File Offset: 0x001540FC
		internal static Exception InbuiltType(Type type)
		{
			return new ArgumentException("Data of this type has inbuilt behaviour, and cannot be added to a model in this way: " + type.FullName);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x00155D13 File Offset: 0x00154113
		protected internal void ThrowIfFrozen()
		{
			if ((this.flags & 4) != 0)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated for " + this.type.FullName);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x00155D3F File Offset: 0x0015413F
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x00155D48 File Offset: 0x00154148
		internal IProtoTypeSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					int opaqueToken = 0;
					try
					{
						this.model.TakeLock(ref opaqueToken);
						if (this.serializer == null)
						{
							this.SetFlag(4, true, false);
							this.serializer = this.BuildSerializer();
						}
					}
					finally
					{
						this.model.ReleaseLock(opaqueToken);
					}
				}
				return this.serializer;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x00155DB8 File Offset: 0x001541B8
		internal bool IsList
		{
			get
			{
				Type type = (!this.IgnoreListHandling) ? TypeModel.GetListItemType(this.model, this.type) : null;
				return type != null;
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00155DF0 File Offset: 0x001541F0
		private IProtoTypeSerializer BuildSerializer()
		{
			if (Helpers.IsEnum(this.type))
			{
				return new TagDecorator(1, WireType.Variant, false, new EnumSerializer(this.type, this.GetEnumMap()));
			}
			Type type = (!this.IgnoreListHandling) ? TypeModel.GetListItemType(this.model, this.type) : null;
			if (type != null)
			{
				if (this.surrogate != null)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot use a surrogate");
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be subclassed");
				}
				Type defaultType = null;
				MetaType.ResolveListTypes(this.model, this.type, ref type, ref defaultType);
				ValueMember valueMember = new ValueMember(this.model, 1, this.type, type, defaultType, DataFormat.Default);
				return new TypeSerializer(this.model, this.type, new int[]
				{
					1
				}, new IProtoSerializer[]
				{
					valueMember.Serializer
				}, null, true, true, null, this.constructType, this.factory);
			}
			else
			{
				if (this.surrogate != null)
				{
					MetaType metaType = this.model[this.surrogate];
					MetaType metaType2;
					while ((metaType2 = metaType.baseType) != null)
					{
						metaType = metaType2;
					}
					return new SurrogateSerializer(this.model, this.type, this.surrogate, metaType.Serializer);
				}
				if (!this.IsAutoTuple)
				{
					this.fields.Trim();
					int count = this.fields.Count;
					int num = (this.subTypes != null) ? this.subTypes.Count : 0;
					int[] array = new int[count + num];
					IProtoSerializer[] array2 = new IProtoSerializer[count + num];
					int num2 = 0;
					if (num != 0)
					{
						foreach (object obj in this.subTypes)
						{
							SubType subType = (SubType)obj;
							if (!subType.DerivedType.IgnoreListHandling && this.model.MapType(MetaType.ienumerable).IsAssignableFrom(subType.DerivedType.Type))
							{
								throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a subclass");
							}
							array[num2] = subType.FieldNumber;
							array2[num2++] = subType.Serializer;
						}
					}
					if (count != 0)
					{
						foreach (object obj2 in this.fields)
						{
							ValueMember valueMember2 = (ValueMember)obj2;
							array[num2] = valueMember2.FieldNumber;
							array2[num2++] = valueMember2.Serializer;
						}
					}
					BasicList basicList = null;
					for (MetaType metaType3 = this.BaseType; metaType3 != null; metaType3 = metaType3.BaseType)
					{
						MethodInfo methodInfo = (!metaType3.HasCallbacks) ? null : metaType3.Callbacks.BeforeDeserialize;
						if (methodInfo != null)
						{
							if (basicList == null)
							{
								basicList = new BasicList();
							}
							basicList.Add(methodInfo);
						}
					}
					MethodInfo[] array3 = null;
					if (basicList != null)
					{
						array3 = new MethodInfo[basicList.Count];
						basicList.CopyTo(array3, 0);
						Array.Reverse(array3);
					}
					return new TypeSerializer(this.model, this.type, array, array2, array3, this.baseType == null, this.UseConstructor, this.callbacks, this.constructType, this.factory);
				}
				MemberInfo[] members;
				ConstructorInfo constructorInfo = MetaType.ResolveTupleConstructor(this.type, out members);
				if (constructorInfo == null)
				{
					throw new InvalidOperationException();
				}
				return new TupleSerializer(this.model, constructorInfo, members);
			}
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0015616F File Offset: 0x0015456F
		private static Type GetBaseType(MetaType type)
		{
			return type.type.BaseType;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0015617C File Offset: 0x0015457C
		internal static bool GetAsReferenceDefault(RuntimeTypeModel model, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (Helpers.IsEnum(type))
			{
				return false;
			}
			AttributeMap[] array = AttributeMap.Create(model, type, false);
			for (int i = 0; i < array.Length; i++)
			{
				object obj;
				if (array[i].AttributeType.FullName == "ProtoBuf.ProtoContractAttribute" && array[i].TryGet("AsReferenceDefault", out obj))
				{
					return (bool)obj;
				}
			}
			return false;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x001561FC File Offset: 0x001545FC
		internal void ApplyDefaultBehaviour()
		{
			Type type = MetaType.GetBaseType(this);
			if (type != null && this.model.FindWithoutAdd(type) == null && MetaType.GetContractFamily(this.model, type, null) != MetaType.AttributeFamily.None)
			{
				this.model.FindOrAddAuto(type, true, false, false);
			}
			AttributeMap[] array = AttributeMap.Create(this.model, this.type, false);
			MetaType.AttributeFamily attributeFamily = MetaType.GetContractFamily(this.model, this.type, array);
			if (attributeFamily == MetaType.AttributeFamily.AutoTuple)
			{
				this.SetFlag(64, true, true);
			}
			bool flag = !this.EnumPassthru && Helpers.IsEnum(this.type);
			if (attributeFamily == MetaType.AttributeFamily.None && !flag)
			{
				return;
			}
			BasicList basicList = null;
			BasicList basicList2 = null;
			int dataMemberOffset = 0;
			int num = 1;
			bool flag2 = this.model.InferTagFromNameDefault;
			ImplicitFields implicitFields = ImplicitFields.None;
			string text = null;
			foreach (AttributeMap attributeMap in array)
			{
				string fullName = attributeMap.AttributeType.FullName;
				object obj;
				if (!flag && fullName == "ProtoBuf.ProtoIncludeAttribute")
				{
					int fieldNumber = 0;
					if (attributeMap.TryGet("tag", out obj))
					{
						fieldNumber = (int)obj;
					}
					DataFormat dataFormat = DataFormat.Default;
					if (attributeMap.TryGet("DataFormat", out obj))
					{
						dataFormat = (DataFormat)((int)obj);
					}
					Type type2 = null;
					try
					{
						if (attributeMap.TryGet("knownTypeName", out obj))
						{
							type2 = this.model.GetType((string)obj, this.type.Assembly);
						}
						else if (attributeMap.TryGet("knownType", out obj))
						{
							type2 = (Type)obj;
						}
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName, innerException);
					}
					if (type2 == null)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName);
					}
					if (this.IsValidSubType(type2))
					{
						this.AddSubType(fieldNumber, type2, dataFormat);
					}
				}
				if (fullName == "ProtoBuf.ProtoPartialIgnoreAttribute" && attributeMap.TryGet("MemberName", out obj) && obj != null)
				{
					if (basicList == null)
					{
						basicList = new BasicList();
					}
					basicList.Add((string)obj);
				}
				if (!flag && fullName == "ProtoBuf.ProtoPartialMemberAttribute")
				{
					if (basicList2 == null)
					{
						basicList2 = new BasicList();
					}
					basicList2.Add(attributeMap);
				}
				if (fullName == "ProtoBuf.ProtoContractAttribute")
				{
					if (attributeMap.TryGet("Name", out obj))
					{
						text = (string)obj;
					}
					if (Helpers.IsEnum(this.type))
					{
						if (attributeMap.TryGet("EnumPassthruHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("EnumPassthru", out obj))
						{
							this.EnumPassthru = (bool)obj;
							if (this.EnumPassthru)
							{
								flag = false;
							}
						}
					}
					else
					{
						if (attributeMap.TryGet("DataMemberOffset", out obj))
						{
							dataMemberOffset = (int)obj;
						}
						if (attributeMap.TryGet("InferTagFromNameHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("InferTagFromName", out obj))
						{
							flag2 = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFields", out obj) && obj != null)
						{
							implicitFields = (ImplicitFields)((int)obj);
						}
						if (attributeMap.TryGet("SkipConstructor", out obj))
						{
							this.UseConstructor = !(bool)obj;
						}
						if (attributeMap.TryGet("IgnoreListHandling", out obj))
						{
							this.IgnoreListHandling = (bool)obj;
						}
						if (attributeMap.TryGet("AsReferenceDefault", out obj))
						{
							this.AsReferenceDefault = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFirstTag", out obj) && (int)obj > 0)
						{
							num = (int)obj;
						}
					}
				}
				if (fullName == "System.Runtime.Serialization.DataContractAttribute" && text == null && attributeMap.TryGet("Name", out obj))
				{
					text = (string)obj;
				}
				if (fullName == "System.Xml.Serialization.XmlTypeAttribute" && text == null && attributeMap.TryGet("TypeName", out obj))
				{
					text = (string)obj;
				}
			}
			if (!Helpers.IsNullOrEmpty(text))
			{
				this.Name = text;
			}
			if (implicitFields != ImplicitFields.None)
			{
				attributeFamily &= MetaType.AttributeFamily.ProtoBuf;
			}
			MethodInfo[] array2 = null;
			BasicList basicList3 = new BasicList();
			MemberInfo[] members = this.type.GetMembers((!flag) ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Static | BindingFlags.Public));
			foreach (MemberInfo memberInfo in members)
			{
				if (memberInfo.DeclaringType == this.type)
				{
					if (!memberInfo.IsDefined(this.model.MapType(typeof(ProtoIgnoreAttribute)), true))
					{
						if (basicList == null || !basicList.Contains(memberInfo.Name))
						{
							bool flag3 = false;
							PropertyInfo propertyInfo;
							FieldInfo fieldInfo;
							MethodInfo methodInfo;
							if ((propertyInfo = (memberInfo as PropertyInfo)) != null)
							{
								if (!flag)
								{
									Type type3 = propertyInfo.PropertyType;
									bool isPublic = Helpers.GetGetMethod(propertyInfo, false, false) != null;
									bool isField = false;
									MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag2, implicitFields, basicList3, memberInfo, ref flag3, isPublic, isField, ref type3);
								}
							}
							else if ((fieldInfo = (memberInfo as FieldInfo)) != null)
							{
								Type type3 = fieldInfo.FieldType;
								bool isPublic = fieldInfo.IsPublic;
								bool isField = true;
								if (!flag || fieldInfo.IsStatic)
								{
									MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag2, implicitFields, basicList3, memberInfo, ref flag3, isPublic, isField, ref type3);
								}
							}
							else if ((methodInfo = (memberInfo as MethodInfo)) != null)
							{
								if (!flag)
								{
									AttributeMap[] array4 = AttributeMap.Create(this.model, methodInfo, false);
									if (array4 != null && array4.Length > 0)
									{
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoBeforeSerializationAttribute", ref array2, 0);
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoAfterSerializationAttribute", ref array2, 1);
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoBeforeDeserializationAttribute", ref array2, 2);
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoAfterDeserializationAttribute", ref array2, 3);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnSerializingAttribute", ref array2, 4);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnSerializedAttribute", ref array2, 5);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnDeserializingAttribute", ref array2, 6);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnDeserializedAttribute", ref array2, 7);
									}
								}
							}
						}
					}
				}
			}
			ProtoMemberAttribute[] array5 = new ProtoMemberAttribute[basicList3.Count];
			basicList3.CopyTo(array5, 0);
			if (flag2 || implicitFields != ImplicitFields.None)
			{
				Array.Sort<ProtoMemberAttribute>(array5);
				int num2 = num;
				foreach (ProtoMemberAttribute protoMemberAttribute in array5)
				{
					if (!protoMemberAttribute.TagIsPinned)
					{
						protoMemberAttribute.Rebase(num2++);
					}
				}
			}
			foreach (ProtoMemberAttribute normalizedAttribute in array5)
			{
				ValueMember valueMember = this.ApplyDefaultBehaviour(flag, normalizedAttribute);
				if (valueMember != null)
				{
					this.Add(valueMember);
				}
			}
			if (array2 != null)
			{
				this.SetCallbacks(MetaType.Coalesce(array2, 0, 4), MetaType.Coalesce(array2, 1, 5), MetaType.Coalesce(array2, 2, 6), MetaType.Coalesce(array2, 3, 7));
			}
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x001569AC File Offset: 0x00154DAC
		private static void ApplyDefaultBehaviour_AddMembers(TypeModel model, MetaType.AttributeFamily family, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferTagByName, ImplicitFields implicitMode, BasicList members, MemberInfo member, ref bool forced, bool isPublic, bool isField, ref Type effectiveType)
		{
			if (implicitMode != ImplicitFields.AllFields)
			{
				if (implicitMode == ImplicitFields.AllPublic)
				{
					if (isPublic)
					{
						forced = true;
					}
				}
			}
			else if (isField)
			{
				forced = true;
			}
			if (effectiveType.IsSubclassOf(model.MapType(typeof(Delegate))))
			{
				effectiveType = null;
			}
			if (effectiveType != null)
			{
				ProtoMemberAttribute protoMemberAttribute = MetaType.NormalizeProtoMember(model, member, family, forced, isEnum, partialMembers, dataMemberOffset, inferTagByName);
				if (protoMemberAttribute != null)
				{
					members.Add(protoMemberAttribute);
				}
			}
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x00156A3C File Offset: 0x00154E3C
		private static MethodInfo Coalesce(MethodInfo[] arr, int x, int y)
		{
			MethodInfo methodInfo = arr[x];
			if (methodInfo == null)
			{
				methodInfo = arr[y];
			}
			return methodInfo;
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x00156A58 File Offset: 0x00154E58
		internal static MetaType.AttributeFamily GetContractFamily(RuntimeTypeModel model, Type type, AttributeMap[] attributes)
		{
			MetaType.AttributeFamily attributeFamily = MetaType.AttributeFamily.None;
			if (attributes == null)
			{
				attributes = AttributeMap.Create(model, type, false);
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				string fullName = attributes[i].AttributeType.FullName;
				if (fullName != null)
				{
					if (!(fullName == "ProtoBuf.ProtoContractAttribute"))
					{
						if (!(fullName == "System.Xml.Serialization.XmlTypeAttribute"))
						{
							if (fullName == "System.Runtime.Serialization.DataContractAttribute")
							{
								if (!model.AutoAddProtoContractTypesOnly)
								{
									attributeFamily |= MetaType.AttributeFamily.DataContractSerialier;
								}
							}
						}
						else if (!model.AutoAddProtoContractTypesOnly)
						{
							attributeFamily |= MetaType.AttributeFamily.XmlSerializer;
						}
					}
					else
					{
						bool flag = false;
						MetaType.GetFieldBoolean(ref flag, attributes[i], "UseProtoMembersOnly");
						if (flag)
						{
							return MetaType.AttributeFamily.ProtoBuf;
						}
						attributeFamily |= MetaType.AttributeFamily.ProtoBuf;
					}
				}
			}
			MemberInfo[] array;
			if (attributeFamily == MetaType.AttributeFamily.None && MetaType.ResolveTupleConstructor(type, out array) != null)
			{
				attributeFamily |= MetaType.AttributeFamily.AutoTuple;
			}
			return attributeFamily;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x00156B38 File Offset: 0x00154F38
		internal static ConstructorInfo ResolveTupleConstructor(Type type, out MemberInfo[] mappedMembers)
		{
			mappedMembers = null;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsAbstract)
			{
				return null;
			}
			ConstructorInfo[] constructors = Helpers.GetConstructors(type, false);
			if (constructors.Length == 0 || (constructors.Length == 1 && constructors[0].GetParameters().Length == 0))
			{
				return null;
			}
			MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(type, true);
			BasicList basicList = new BasicList();
			for (int i = 0; i < instanceFieldsAndProperties.Length; i++)
			{
				PropertyInfo propertyInfo = instanceFieldsAndProperties[i] as PropertyInfo;
				if (propertyInfo != null)
				{
					if (!propertyInfo.CanRead)
					{
						return null;
					}
					if (propertyInfo.CanWrite && Helpers.GetSetMethod(propertyInfo, false, false) != null)
					{
						return null;
					}
					basicList.Add(propertyInfo);
				}
				else
				{
					FieldInfo fieldInfo = instanceFieldsAndProperties[i] as FieldInfo;
					if (fieldInfo != null)
					{
						if (!fieldInfo.IsInitOnly)
						{
							return null;
						}
						basicList.Add(fieldInfo);
					}
				}
			}
			if (basicList.Count == 0)
			{
				return null;
			}
			MemberInfo[] array = new MemberInfo[basicList.Count];
			basicList.CopyTo(array, 0);
			int[] array2 = new int[array.Length];
			int num = 0;
			ConstructorInfo constructorInfo = null;
			mappedMembers = new MemberInfo[array2.Length];
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				if (parameters.Length == array.Length)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k] = -1;
					}
					for (int l = 0; l < parameters.Length; l++)
					{
						string b = parameters[l].Name.ToLower();
						for (int m = 0; m < array.Length; m++)
						{
							if (!(array[m].Name.ToLower() != b))
							{
								Type memberType = Helpers.GetMemberType(array[m]);
								if (memberType == parameters[l].ParameterType)
								{
									array2[l] = m;
								}
							}
						}
					}
					bool flag = false;
					for (int n = 0; n < array2.Length; n++)
					{
						if (array2[n] < 0)
						{
							flag = true;
							break;
						}
						mappedMembers[n] = array[array2[n]];
					}
					if (!flag)
					{
						num++;
						constructorInfo = constructors[j];
					}
				}
			}
			return (num != 1) ? null : constructorInfo;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x00156DA8 File Offset: 0x001551A8
		private static void CheckForCallback(MethodInfo method, AttributeMap[] attributes, string callbackTypeName, ref MethodInfo[] callbacks, int index)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].AttributeType.FullName == callbackTypeName)
				{
					if (callbacks == null)
					{
						callbacks = new MethodInfo[8];
					}
					else if (callbacks[index] != null)
					{
						Type reflectedType = method.ReflectedType;
						throw new ProtoException("Duplicate " + callbackTypeName + " callbacks on " + reflectedType.FullName);
					}
					callbacks[index] = method;
				}
			}
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x00156E28 File Offset: 0x00155228
		private static bool HasFamily(MetaType.AttributeFamily value, MetaType.AttributeFamily required)
		{
			return (value & required) == required;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x00156E30 File Offset: 0x00155230
		private static ProtoMemberAttribute NormalizeProtoMember(TypeModel model, MemberInfo member, MetaType.AttributeFamily family, bool forced, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferByTagName)
		{
			if (member == null || (family == MetaType.AttributeFamily.None && !isEnum))
			{
				return null;
			}
			int num = int.MinValue;
			int num2 = (!inferByTagName) ? 1 : -1;
			string text = null;
			bool isPacked = false;
			bool flag = false;
			bool flag2 = false;
			bool isRequired = false;
			bool asReference = false;
			bool flag3 = false;
			bool dynamicType = false;
			bool tagIsPinned = false;
			bool overwriteList = false;
			DataFormat dataFormat = DataFormat.Default;
			if (isEnum)
			{
				forced = true;
			}
			AttributeMap[] attribs = AttributeMap.Create(model, member, true);
			if (isEnum)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (attribute != null)
				{
					flag = true;
				}
				else
				{
					attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoEnumAttribute");
					num = Convert.ToInt32(((FieldInfo)member).GetRawConstantValue());
					if (attribute != null)
					{
						MetaType.GetFieldName(ref text, attribute, "Name");
						object obj;
						if ((bool)Helpers.GetInstanceMethod(attribute.AttributeType, "HasValue").Invoke(attribute.Target, null) && attribute.TryGet("Value", out obj))
						{
							num = (int)obj;
						}
					}
				}
				flag2 = true;
			}
			if (!flag && !flag2)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoMemberAttribute");
				MetaType.GetIgnore(ref flag, attribute, attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (!flag && attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Tag");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					MetaType.GetFieldBoolean(ref isPacked, attribute, "IsPacked");
					MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
					MetaType.GetDataFormat(ref dataFormat, attribute, "DataFormat");
					MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
					if (flag3)
					{
						flag3 = MetaType.GetFieldBoolean(ref asReference, attribute, "AsReference", true);
					}
					MetaType.GetFieldBoolean(ref dynamicType, attribute, "DynamicType");
					tagIsPinned = (flag2 = (num > 0));
				}
				if (!flag2 && partialMembers != null)
				{
					foreach (object obj2 in partialMembers)
					{
						AttributeMap attributeMap = (AttributeMap)obj2;
						object obj3;
						if (attributeMap.TryGet("MemberName", out obj3) && (string)obj3 == member.Name)
						{
							MetaType.GetFieldNumber(ref num, attributeMap, "Tag");
							MetaType.GetFieldName(ref text, attributeMap, "Name");
							MetaType.GetFieldBoolean(ref isRequired, attributeMap, "IsRequired");
							MetaType.GetFieldBoolean(ref isPacked, attributeMap, "IsPacked");
							MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
							MetaType.GetDataFormat(ref dataFormat, attributeMap, "DataFormat");
							MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
							if (flag3)
							{
								flag3 = MetaType.GetFieldBoolean(ref asReference, attributeMap, "AsReference", true);
							}
							MetaType.GetFieldBoolean(ref dynamicType, attributeMap, "DynamicType");
							if (flag2 = (tagIsPinned = (num > 0)))
							{
								break;
							}
						}
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.DataContractSerialier))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Runtime.Serialization.DataMemberAttribute");
				if (attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					flag2 = (num >= num2);
					if (flag2)
					{
						num += dataMemberOffset;
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.XmlSerializer))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlElementAttribute");
				if (attribute == null)
				{
					attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlArrayAttribute");
				}
				MetaType.GetIgnore(ref flag, attribute, attribs, "System.Xml.Serialization.XmlIgnoreAttribute");
				if (attribute != null && !flag)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "ElementName");
					flag2 = (num >= num2);
				}
			}
			if (!flag && !flag2 && MetaType.GetAttribute(attribs, "System.NonSerializedAttribute") != null)
			{
				flag = true;
			}
			if (flag || (num < num2 && !forced))
			{
				return null;
			}
			return new ProtoMemberAttribute(num, forced || inferByTagName)
			{
				AsReference = asReference,
				AsReferenceHasValue = flag3,
				DataFormat = dataFormat,
				DynamicType = dynamicType,
				IsPacked = isPacked,
				OverwriteList = overwriteList,
				IsRequired = isRequired,
				Name = ((!Helpers.IsNullOrEmpty(text)) ? text : member.Name),
				Member = member,
				TagIsPinned = tagIsPinned
			};
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x001572AC File Offset: 0x001556AC
		private ValueMember ApplyDefaultBehaviour(bool isEnum, ProtoMemberAttribute normalizedAttribute)
		{
			MemberInfo member;
			if (normalizedAttribute == null || (member = normalizedAttribute.Member) == null)
			{
				return null;
			}
			Type memberType = Helpers.GetMemberType(member);
			Type type = null;
			Type defaultType = null;
			MetaType.ResolveListTypes(this.model, memberType, ref type, ref defaultType);
			if (type != null)
			{
				int num = this.model.FindOrAddAuto(memberType, false, true, false);
				if (num >= 0 && this.model[memberType].IgnoreListHandling)
				{
					type = null;
					defaultType = null;
				}
			}
			AttributeMap[] attribs = AttributeMap.Create(this.model, member, true);
			object defaultValue = null;
			if (this.model.UseImplicitZeroDefaults)
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(memberType);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultValue = false;
					break;
				case ProtoTypeCode.Char:
					defaultValue = '\0';
					break;
				case ProtoTypeCode.SByte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Byte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int32:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt32:
					defaultValue = 0U;
					break;
				case ProtoTypeCode.Int64:
					defaultValue = 0L;
					break;
				case ProtoTypeCode.UInt64:
					defaultValue = 0UL;
					break;
				case ProtoTypeCode.Single:
					defaultValue = 0f;
					break;
				case ProtoTypeCode.Double:
					defaultValue = 0.0;
					break;
				case ProtoTypeCode.Decimal:
					defaultValue = 0m;
					break;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						defaultValue = TimeSpan.Zero;
						break;
					case ProtoTypeCode.Guid:
						defaultValue = Guid.Empty;
						break;
					}
					break;
				}
			}
			AttributeMap attribute;
			object obj;
			if ((attribute = MetaType.GetAttribute(attribs, "System.ComponentModel.DefaultValueAttribute")) != null && attribute.TryGet("Value", out obj))
			{
				defaultValue = obj;
			}
			ValueMember valueMember = (!isEnum && normalizedAttribute.Tag <= 0) ? null : new ValueMember(this.model, this.type, normalizedAttribute.Tag, member, memberType, type, defaultType, normalizedAttribute.DataFormat, defaultValue);
			if (valueMember != null)
			{
				Type declaringType = this.type;
				PropertyInfo propertyInfo = Helpers.GetProperty(declaringType, member.Name + "Specified", true);
				MethodInfo getMethod = Helpers.GetGetMethod(propertyInfo, true, true);
				if (getMethod == null || getMethod.IsStatic)
				{
					propertyInfo = null;
				}
				if (propertyInfo != null)
				{
					valueMember.SetSpecified(getMethod, Helpers.GetSetMethod(propertyInfo, true, true));
				}
				else
				{
					MethodInfo instanceMethod = Helpers.GetInstanceMethod(declaringType, "ShouldSerialize" + member.Name, Helpers.EmptyTypes);
					if (instanceMethod != null && instanceMethod.ReturnType == this.model.MapType(typeof(bool)))
					{
						valueMember.SetSpecified(instanceMethod, null);
					}
				}
				if (!Helpers.IsNullOrEmpty(normalizedAttribute.Name))
				{
					valueMember.SetName(normalizedAttribute.Name);
				}
				valueMember.IsPacked = normalizedAttribute.IsPacked;
				valueMember.IsRequired = normalizedAttribute.IsRequired;
				valueMember.OverwriteList = normalizedAttribute.OverwriteList;
				if (normalizedAttribute.AsReferenceHasValue)
				{
					valueMember.AsReference = normalizedAttribute.AsReference;
				}
				valueMember.DynamicType = normalizedAttribute.DynamicType;
			}
			return valueMember;
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x00157610 File Offset: 0x00155A10
		private static void GetDataFormat(ref DataFormat value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value != DataFormat.Default)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (DataFormat)obj;
			}
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x00157647 File Offset: 0x00155A47
		private static void GetIgnore(ref bool ignore, AttributeMap attrib, AttributeMap[] attribs, string fullName)
		{
			if (ignore || attrib == null)
			{
				return;
			}
			ignore = (MetaType.GetAttribute(attribs, fullName) != null);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x00157666 File Offset: 0x00155A66
		private static void GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName)
		{
			MetaType.GetFieldBoolean(ref value, attrib, memberName, true);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x00157674 File Offset: 0x00155A74
		private static bool GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName, bool publicOnly)
		{
			if (attrib == null)
			{
				return false;
			}
			if (value)
			{
				return true;
			}
			object obj;
			if (attrib.TryGet(memberName, publicOnly, out obj) && obj != null)
			{
				value = (bool)obj;
				return true;
			}
			return false;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x001576B4 File Offset: 0x00155AB4
		private static void GetFieldNumber(ref int value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value > 0)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (int)obj;
			}
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x001576EC File Offset: 0x00155AEC
		private static void GetFieldName(ref string name, AttributeMap attrib, string memberName)
		{
			if (attrib == null || !Helpers.IsNullOrEmpty(name))
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				name = (string)obj;
			}
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x00157728 File Offset: 0x00155B28
		private static AttributeMap GetAttribute(AttributeMap[] attribs, string fullName)
		{
			foreach (AttributeMap attributeMap in attribs)
			{
				if (attributeMap != null && attributeMap.AttributeType.FullName == fullName)
				{
					return attributeMap;
				}
			}
			return null;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0015776C File Offset: 0x00155B6C
		public MetaType Add(int fieldNumber, string memberName)
		{
			this.AddField(fieldNumber, memberName, null, null, null);
			return this;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0015777B File Offset: 0x00155B7B
		public ValueMember AddField(int fieldNumber, string memberName)
		{
			return this.AddField(fieldNumber, memberName, null, null, null);
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x00157788 File Offset: 0x00155B88
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x00157795 File Offset: 0x00155B95
		public bool UseConstructor
		{
			get
			{
				return !this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, !value, true);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x001577A4 File Offset: 0x00155BA4
		// (set) Token: 0x06002F2A RID: 12074 RVA: 0x001577AC File Offset: 0x00155BAC
		public Type ConstructType
		{
			get
			{
				return this.constructType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.constructType = value;
			}
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x001577BB File Offset: 0x00155BBB
		public MetaType Add(string memberName)
		{
			this.Add(this.GetNextFieldNumber(), memberName);
			return this;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x001577CC File Offset: 0x00155BCC
		public void SetSurrogate(Type surrogateType)
		{
			if (surrogateType == this.type)
			{
				surrogateType = null;
			}
			if (surrogateType != null && surrogateType != null && Helpers.IsAssignableFrom(this.model.MapType(typeof(IEnumerable)), surrogateType))
			{
				throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a surrogate");
			}
			this.ThrowIfFrozen();
			this.surrogate = surrogateType;
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x0015782C File Offset: 0x00155C2C
		internal MetaType GetSurrogateOrSelf()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			return this;
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x0015784C File Offset: 0x00155C4C
		internal MetaType GetSurrogateOrBaseOrSelf(bool deep)
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			MetaType metaType = this.baseType;
			if (metaType == null)
			{
				return this;
			}
			if (deep)
			{
				MetaType result;
				do
				{
					result = metaType;
					metaType = metaType.baseType;
				}
				while (metaType != null);
				return result;
			}
			return metaType;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x001578A0 File Offset: 0x00155CA0
		private int GetNextFieldNumber()
		{
			int num = 0;
			foreach (object obj in this.fields)
			{
				ValueMember valueMember = (ValueMember)obj;
				if (valueMember.FieldNumber > num)
				{
					num = valueMember.FieldNumber;
				}
			}
			if (this.subTypes != null)
			{
				foreach (object obj2 in this.subTypes)
				{
					SubType subType = (SubType)obj2;
					if (subType.FieldNumber > num)
					{
						num = subType.FieldNumber;
					}
				}
			}
			return num + 1;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x00157938 File Offset: 0x00155D38
		public MetaType Add(params string[] memberNames)
		{
			if (memberNames == null)
			{
				throw new ArgumentNullException("memberNames");
			}
			int nextFieldNumber = this.GetNextFieldNumber();
			for (int i = 0; i < memberNames.Length; i++)
			{
				this.Add(nextFieldNumber++, memberNames[i]);
			}
			return this;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x00157981 File Offset: 0x00155D81
		public MetaType Add(int fieldNumber, string memberName, object defaultValue)
		{
			this.AddField(fieldNumber, memberName, null, null, defaultValue);
			return this;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x00157990 File Offset: 0x00155D90
		public MetaType Add(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			this.AddField(fieldNumber, memberName, itemType, defaultType, null);
			return this;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x001579A0 File Offset: 0x00155DA0
		public ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			return this.AddField(fieldNumber, memberName, itemType, defaultType, null);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x001579B0 File Offset: 0x00155DB0
		private ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType, object defaultValue)
		{
			MemberInfo memberInfo = null;
			MemberInfo[] member = this.type.GetMember(memberName, (!Helpers.IsEnum(this.type)) ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Static | BindingFlags.Public));
			if (member != null && member.Length == 1)
			{
				memberInfo = member[0];
			}
			if (memberInfo == null)
			{
				throw new ArgumentException("Unable to determine member: " + memberName, "memberName");
			}
			MemberTypes memberType = memberInfo.MemberType;
			Type memberType2;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new NotSupportedException(memberInfo.MemberType.ToString());
				}
				memberType2 = ((PropertyInfo)memberInfo).PropertyType;
			}
			else
			{
				memberType2 = ((FieldInfo)memberInfo).FieldType;
			}
			MetaType.ResolveListTypes(this.model, memberType2, ref itemType, ref defaultType);
			ValueMember valueMember = new ValueMember(this.model, this.type, fieldNumber, memberInfo, memberType2, itemType, defaultType, DataFormat.Default, defaultValue);
			this.Add(valueMember);
			return valueMember;
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00157AA4 File Offset: 0x00155EA4
		internal static void ResolveListTypes(TypeModel model, Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (type.IsArray)
			{
				if (type.GetArrayRank() != 1)
				{
					throw new NotSupportedException("Multi-dimension arrays are supported");
				}
				itemType = type.GetElementType();
				if (itemType == model.MapType(typeof(byte)))
				{
					Type type2;
					itemType = (type2 = null);
					defaultType = type2;
				}
				else
				{
					defaultType = type;
				}
			}
			if (itemType == null)
			{
				itemType = TypeModel.GetListItemType(model, type);
			}
			if (itemType != null)
			{
				Type type3 = null;
				Type type4 = null;
				MetaType.ResolveListTypes(model, itemType, ref type3, ref type4);
				if (type3 != null)
				{
					throw TypeModel.CreateNestedListsNotSupported();
				}
			}
			if (itemType != null && defaultType == null)
			{
				if (type.IsClass && !type.IsAbstract && Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != null)
				{
					defaultType = type;
				}
				if (defaultType == null && type.IsInterface)
				{
					Type[] genericArguments;
					if (type.IsGenericType && type.GetGenericTypeDefinition() == model.MapType(typeof(IDictionary<, >)) && itemType == model.MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = model.MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = model.MapType(typeof(List<>)).MakeGenericType(new Type[]
						{
							itemType
						});
					}
				}
				if (defaultType != null && !Helpers.IsAssignableFrom(type, defaultType))
				{
					defaultType = null;
				}
			}
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00157C28 File Offset: 0x00156028
		private void Add(ValueMember member)
		{
			int opaqueToken = 0;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				this.ThrowIfFrozen();
				this.fields.Add(member);
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x1700039B RID: 923
		public ValueMember this[int fieldNumber]
		{
			get
			{
				foreach (object obj in this.fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					if (valueMember.FieldNumber == fieldNumber)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		// Token: 0x1700039C RID: 924
		public ValueMember this[MemberInfo member]
		{
			get
			{
				if (member == null)
				{
					return null;
				}
				foreach (object obj in this.fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					if (valueMember.Member == member)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00157D14 File Offset: 0x00156114
		public ValueMember[] GetFields()
		{
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			return array;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00157D4C File Offset: 0x0015614C
		public SubType[] GetSubtypes()
		{
			if (this.subTypes == null || this.subTypes.Count == 0)
			{
				return new SubType[0];
			}
			SubType[] array = new SubType[this.subTypes.Count];
			this.subTypes.CopyTo(array, 0);
			Array.Sort<SubType>(array, SubType.Comparer.Default);
			return array;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x00157DA8 File Offset: 0x001561A8
		internal bool IsDefined(int fieldNumber)
		{
			foreach (object obj in this.fields)
			{
				ValueMember valueMember = (ValueMember)obj;
				if (valueMember.FieldNumber == fieldNumber)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x00157DEE File Offset: 0x001561EE
		internal int GetKey(bool demand, bool getBaseKey)
		{
			return this.model.GetKey(this.type, demand, getBaseKey);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x00157E04 File Offset: 0x00156204
		internal EnumSerializer.EnumPair[] GetEnumMap()
		{
			if (this.HasFlag(2))
			{
				return null;
			}
			EnumSerializer.EnumPair[] array = new EnumSerializer.EnumPair[this.fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ValueMember valueMember = (ValueMember)this.fields[i];
				int fieldNumber = valueMember.FieldNumber;
				object rawEnumValue = valueMember.GetRawEnumValue();
				array[i] = new EnumSerializer.EnumPair(fieldNumber, rawEnumValue, valueMember.MemberType);
			}
			return array;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x00157E80 File Offset: 0x00156280
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x00157E89 File Offset: 0x00156289
		public bool EnumPassthru
		{
			get
			{
				return this.HasFlag(2);
			}
			set
			{
				this.SetFlag(2, value, true);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x00157E94 File Offset: 0x00156294
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x00157EA1 File Offset: 0x001562A1
		public bool IgnoreListHandling
		{
			get
			{
				return this.HasFlag(128);
			}
			set
			{
				this.SetFlag(128, value, true);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x00157EB0 File Offset: 0x001562B0
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x00157EB9 File Offset: 0x001562B9
		internal bool Pending
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, false);
			}
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x00157EC4 File Offset: 0x001562C4
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x00157ED4 File Offset: 0x001562D4
		private void SetFlag(byte flag, bool value, bool throwIfFrozen)
		{
			if (throwIfFrozen && this.HasFlag(flag) != value)
			{
				this.ThrowIfFrozen();
			}
			if (value)
			{
				this.flags |= flag;
			}
			else
			{
				this.flags &= ~flag;
			}
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x00157F2C File Offset: 0x0015632C
		internal static MetaType GetRootType(MetaType source)
		{
			while (source.serializer != null)
			{
				MetaType metaType = source.baseType;
				if (metaType == null)
				{
					return source;
				}
				source = metaType;
			}
			RuntimeTypeModel runtimeTypeModel = source.model;
			int opaqueToken = 0;
			MetaType result;
			try
			{
				runtimeTypeModel.TakeLock(ref opaqueToken);
				MetaType metaType2;
				while ((metaType2 = source.baseType) != null)
				{
					source = metaType2;
				}
				result = source;
			}
			finally
			{
				runtimeTypeModel.ReleaseLock(opaqueToken);
			}
			return result;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x00157FA4 File Offset: 0x001563A4
		internal bool IsPrepared()
		{
			return false;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x00157FA7 File Offset: 0x001563A7
		internal IEnumerable Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00157FAF File Offset: 0x001563AF
		internal static StringBuilder NewLine(StringBuilder builder, int indent)
		{
			return Helpers.AppendLine(builder).Append(' ', indent * 3);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x00157FC1 File Offset: 0x001563C1
		internal bool IsAutoTuple
		{
			get
			{
				return this.HasFlag(64);
			}
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x00157FCC File Offset: 0x001563CC
		internal void WriteSchema(StringBuilder builder, int indent, ref bool requiresBclImport)
		{
			if (this.surrogate != null)
			{
				return;
			}
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			if (this.IsList)
			{
				string schemaTypeName = this.model.GetSchemaTypeName(TypeModel.GetListItemType(this.model, this.type), DataFormat.Default, false, false, ref requiresBclImport);
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				MetaType.NewLine(builder, indent + 1).Append("repeated ").Append(schemaTypeName).Append(" items = 1;");
				MetaType.NewLine(builder, indent).Append('}');
			}
			else if (this.IsAutoTuple)
			{
				MemberInfo[] array2;
				if (MetaType.ResolveTupleConstructor(this.type, out array2) != null)
				{
					MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
					for (int i = 0; i < array2.Length; i++)
					{
						Type effectiveType;
						if (array2[i] is PropertyInfo)
						{
							effectiveType = ((PropertyInfo)array2[i]).PropertyType;
						}
						else
						{
							if (!(array2[i] is FieldInfo))
							{
								throw new NotSupportedException("Unknown member type: " + array2[i].GetType().Name);
							}
							effectiveType = ((FieldInfo)array2[i]).FieldType;
						}
						MetaType.NewLine(builder, indent + 1).Append("optional ").Append(this.model.GetSchemaTypeName(effectiveType, DataFormat.Default, false, false, ref requiresBclImport).Replace('.', '_')).Append(' ').Append(array2[i].Name).Append(" = ").Append(i + 1).Append(';');
					}
					MetaType.NewLine(builder, indent).Append('}');
				}
			}
			else if (Helpers.IsEnum(this.type))
			{
				MetaType.NewLine(builder, indent).Append("enum ").Append(this.GetSchemaTypeName()).Append(" {");
				if (array.Length == 0 && this.EnumPassthru)
				{
					if (this.type.IsDefined(this.model.MapType(typeof(FlagsAttribute)), false))
					{
						MetaType.NewLine(builder, indent + 1).Append("// this is a composite/flags enumeration");
					}
					else
					{
						MetaType.NewLine(builder, indent + 1).Append("// this enumeration will be passed as a raw value");
					}
					foreach (FieldInfo fieldInfo in this.type.GetFields())
					{
						if (fieldInfo.IsStatic && fieldInfo.IsLiteral)
						{
							object rawConstantValue = fieldInfo.GetRawConstantValue();
							MetaType.NewLine(builder, indent + 1).Append(fieldInfo.Name).Append(" = ").Append(rawConstantValue).Append(";");
						}
					}
				}
				else
				{
					foreach (ValueMember valueMember in array)
					{
						MetaType.NewLine(builder, indent + 1).Append(valueMember.Name).Append(" = ").Append(valueMember.FieldNumber).Append(';');
					}
				}
				MetaType.NewLine(builder, indent).Append('}');
			}
			else
			{
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				foreach (ValueMember valueMember2 in array)
				{
					string value = (valueMember2.ItemType == null) ? ((!valueMember2.IsRequired) ? "optional" : "required") : "repeated";
					MetaType.NewLine(builder, indent + 1).Append(value).Append(' ');
					if (valueMember2.DataFormat == DataFormat.Group)
					{
						builder.Append("group ");
					}
					string schemaTypeName2 = valueMember2.GetSchemaTypeName(true, ref requiresBclImport);
					builder.Append(schemaTypeName2).Append(" ").Append(valueMember2.Name).Append(" = ").Append(valueMember2.FieldNumber);
					if (valueMember2.DefaultValue != null)
					{
						if (valueMember2.DefaultValue is string)
						{
							builder.Append(" [default = \"").Append(valueMember2.DefaultValue).Append("\"]");
						}
						else if (valueMember2.DefaultValue is bool)
						{
							builder.Append((!(bool)valueMember2.DefaultValue) ? " [default = false]" : " [default = true]");
						}
						else
						{
							builder.Append(" [default = ").Append(valueMember2.DefaultValue).Append(']');
						}
					}
					if (valueMember2.ItemType != null && valueMember2.IsPacked)
					{
						builder.Append(" [packed=true]");
					}
					builder.Append(';');
					if (schemaTypeName2 == "bcl.NetObjectProxy" && valueMember2.AsReference && !valueMember2.DynamicType)
					{
						builder.Append(" // reference-tracked ").Append(valueMember2.GetSchemaTypeName(false, ref requiresBclImport));
					}
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					MetaType.NewLine(builder, indent + 1).Append("// the following represent sub-types; at most 1 should have a value");
					SubType[] array6 = new SubType[this.subTypes.Count];
					this.subTypes.CopyTo(array6, 0);
					Array.Sort<SubType>(array6, SubType.Comparer.Default);
					foreach (SubType subType in array6)
					{
						string schemaTypeName3 = subType.DerivedType.GetSchemaTypeName();
						MetaType.NewLine(builder, indent + 1).Append("optional ").Append(schemaTypeName3).Append(" ").Append(schemaTypeName3).Append(" = ").Append(subType.FieldNumber).Append(';');
					}
				}
				MetaType.NewLine(builder, indent).Append('}');
			}
		}

		// Token: 0x04002D9F RID: 11679
		private MetaType baseType;

		// Token: 0x04002DA0 RID: 11680
		private BasicList subTypes;

		// Token: 0x04002DA1 RID: 11681
		internal static readonly Type ienumerable = typeof(IEnumerable);

		// Token: 0x04002DA2 RID: 11682
		private CallbackSet callbacks;

		// Token: 0x04002DA3 RID: 11683
		private string name;

		// Token: 0x04002DA4 RID: 11684
		private MethodInfo factory;

		// Token: 0x04002DA5 RID: 11685
		private readonly RuntimeTypeModel model;

		// Token: 0x04002DA6 RID: 11686
		private readonly Type type;

		// Token: 0x04002DA7 RID: 11687
		private IProtoTypeSerializer serializer;

		// Token: 0x04002DA8 RID: 11688
		private Type constructType;

		// Token: 0x04002DA9 RID: 11689
		private Type surrogate;

		// Token: 0x04002DAA RID: 11690
		private readonly BasicList fields = new BasicList();

		// Token: 0x04002DAB RID: 11691
		private const byte OPTIONS_Pending = 1;

		// Token: 0x04002DAC RID: 11692
		private const byte OPTIONS_EnumPassThru = 2;

		// Token: 0x04002DAD RID: 11693
		private const byte OPTIONS_Frozen = 4;

		// Token: 0x04002DAE RID: 11694
		private const byte OPTIONS_PrivateOnApi = 8;

		// Token: 0x04002DAF RID: 11695
		private const byte OPTIONS_SkipConstructor = 16;

		// Token: 0x04002DB0 RID: 11696
		private const byte OPTIONS_AsReferenceDefault = 32;

		// Token: 0x04002DB1 RID: 11697
		private const byte OPTIONS_AutoTuple = 64;

		// Token: 0x04002DB2 RID: 11698
		private const byte OPTIONS_IgnoreListHandling = 128;

		// Token: 0x04002DB3 RID: 11699
		private volatile byte flags;

		// Token: 0x02000659 RID: 1625
		internal sealed class Comparer : IComparer, IComparer<MetaType>
		{
			// Token: 0x06002F4E RID: 12110 RVA: 0x0015864B File Offset: 0x00156A4B
			public int Compare(object x, object y)
			{
				return this.Compare(x as MetaType, y as MetaType);
			}

			// Token: 0x06002F4F RID: 12111 RVA: 0x0015865F File Offset: 0x00156A5F
			public int Compare(MetaType x, MetaType y)
			{
				if (object.ReferenceEquals(x, y))
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return string.Compare(x.GetSchemaTypeName(), y.GetSchemaTypeName(), StringComparison.Ordinal);
			}

			// Token: 0x04002DB4 RID: 11700
			public static readonly MetaType.Comparer Default = new MetaType.Comparer();
		}

		// Token: 0x0200065A RID: 1626
		[Flags]
		internal enum AttributeFamily
		{
			// Token: 0x04002DB6 RID: 11702
			None = 0,
			// Token: 0x04002DB7 RID: 11703
			ProtoBuf = 1,
			// Token: 0x04002DB8 RID: 11704
			DataContractSerialier = 2,
			// Token: 0x04002DB9 RID: 11705
			XmlSerializer = 4,
			// Token: 0x04002DBA RID: 11706
			AutoTuple = 8
		}
	}
}
