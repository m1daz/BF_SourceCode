using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x0200065B RID: 1627
	public sealed class RuntimeTypeModel : TypeModel
	{
		// Token: 0x06002F51 RID: 12113 RVA: 0x0015A22C File Offset: 0x0015862C
		internal RuntimeTypeModel(bool isDefault)
		{
			this.AutoAddMissingTypes = true;
			this.UseImplicitZeroDefaults = true;
			this.SetOption(2, isDefault);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x0015A27D File Offset: 0x0015867D
		private bool GetOption(byte option)
		{
			return (this.options & option) == option;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x0015A28A File Offset: 0x0015868A
		private void SetOption(byte option, bool value)
		{
			if (value)
			{
				this.options |= option;
			}
			else
			{
				this.options &= ~option;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x0015A2B7 File Offset: 0x001586B7
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x0015A2C0 File Offset: 0x001586C0
		public bool InferTagFromNameDefault
		{
			get
			{
				return this.GetOption(1);
			}
			set
			{
				this.SetOption(1, value);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x0015A2CA File Offset: 0x001586CA
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x0015A2D7 File Offset: 0x001586D7
		public bool AutoAddProtoContractTypesOnly
		{
			get
			{
				return this.GetOption(128);
			}
			set
			{
				this.SetOption(128, value);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x0015A2E5 File Offset: 0x001586E5
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x0015A2EF File Offset: 0x001586EF
		public bool UseImplicitZeroDefaults
		{
			get
			{
				return this.GetOption(32);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
				}
				this.SetOption(32, value);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x0015A317 File Offset: 0x00158717
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x0015A321 File Offset: 0x00158721
		public bool AllowParseableTypes
		{
			get
			{
				return this.GetOption(64);
			}
			set
			{
				this.SetOption(64, value);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x0015A32C File Offset: 0x0015872C
		public static RuntimeTypeModel Default
		{
			get
			{
				return RuntimeTypeModel.Singleton.Value;
			}
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0015A333 File Offset: 0x00158733
		public IEnumerable GetTypes()
		{
			return this.types;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x0015A33C File Offset: 0x0015873C
		public override string GetSchema(Type type)
		{
			BasicList basicList = new BasicList();
			MetaType metaType = null;
			bool flag = false;
			if (type == null)
			{
				foreach (object obj in this.types)
				{
					MetaType metaType2 = (MetaType)obj;
					MetaType surrogateOrBaseOrSelf = metaType2.GetSurrogateOrBaseOrSelf(false);
					if (!basicList.Contains(surrogateOrBaseOrSelf))
					{
						basicList.Add(surrogateOrBaseOrSelf);
						this.CascadeDependents(basicList, surrogateOrBaseOrSelf);
					}
				}
			}
			else
			{
				Type underlyingType = Helpers.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					type = underlyingType;
				}
				WireType wireType;
				flag = (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false) != null);
				if (!flag)
				{
					int num = this.FindOrAddAuto(type, false, false, false);
					if (num < 0)
					{
						throw new ArgumentException("The type specified is not a contract-type", "type");
					}
					metaType = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
					basicList.Add(metaType);
					this.CascadeDependents(basicList, metaType);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (!flag)
			{
				IEnumerable enumerable = (metaType != null) ? basicList : this.types;
				IEnumerator enumerator2 = enumerable.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						MetaType metaType3 = (MetaType)obj2;
						if (!metaType3.IsList)
						{
							string @namespace = metaType3.Type.Namespace;
							if (!Helpers.IsNullOrEmpty(@namespace))
							{
								if (!@namespace.StartsWith("System."))
								{
									if (text == null)
									{
										text = @namespace;
									}
									else if (!(text == @namespace))
									{
										text = null;
										break;
									}
								}
							}
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (!Helpers.IsNullOrEmpty(text))
			{
				stringBuilder.Append("package ").Append(text).Append(';');
				Helpers.AppendLine(stringBuilder);
			}
			bool flag2 = false;
			StringBuilder stringBuilder2 = new StringBuilder();
			MetaType[] array = new MetaType[basicList.Count];
			basicList.CopyTo(array, 0);
			Array.Sort<MetaType>(array, MetaType.Comparer.Default);
			if (flag)
			{
				Helpers.AppendLine(stringBuilder2).Append("message ").Append(type.Name).Append(" {");
				MetaType.NewLine(stringBuilder2, 1).Append("optional ").Append(this.GetSchemaTypeName(type, DataFormat.Default, false, false, ref flag2)).Append(" value = 1;");
				Helpers.AppendLine(stringBuilder2).Append('}');
			}
			else
			{
				foreach (MetaType metaType4 in array)
				{
					if (!metaType4.IsList || metaType4 == metaType)
					{
						metaType4.WriteSchema(stringBuilder2, 0, ref flag2);
					}
				}
			}
			if (flag2)
			{
				stringBuilder.Append("import \"bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
				Helpers.AppendLine(stringBuilder);
			}
			return Helpers.AppendLine(stringBuilder.Append(stringBuilder2)).ToString();
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x0015A650 File Offset: 0x00158A50
		private void CascadeDependents(BasicList list, MetaType metaType)
		{
			if (metaType.IsList)
			{
				Type listItemType = TypeModel.GetListItemType(this, metaType.Type);
				WireType wireType;
				if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, listItemType, out wireType, false, false, false, false) == null)
				{
					int num = this.FindOrAddAuto(listItemType, false, false, false);
					if (num >= 0)
					{
						MetaType metaType2 = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
						if (!list.Contains(metaType2))
						{
							list.Add(metaType2);
							this.CascadeDependents(list, metaType2);
						}
					}
				}
			}
			else
			{
				MetaType metaType2;
				if (metaType.IsAutoTuple)
				{
					MemberInfo[] array;
					if (MetaType.ResolveTupleConstructor(metaType.Type, out array) != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							Type type = null;
							if (array[i] is PropertyInfo)
							{
								type = ((PropertyInfo)array[i]).PropertyType;
							}
							else if (array[i] is FieldInfo)
							{
								type = ((FieldInfo)array[i]).FieldType;
							}
							WireType wireType2;
							if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType2, false, false, false, false) == null)
							{
								int num2 = this.FindOrAddAuto(type, false, false, false);
								if (num2 >= 0)
								{
									metaType2 = ((MetaType)this.types[num2]).GetSurrogateOrBaseOrSelf(false);
									if (!list.Contains(metaType2))
									{
										list.Add(metaType2);
										this.CascadeDependents(list, metaType2);
									}
								}
							}
						}
					}
				}
				else
				{
					IEnumerator enumerator = metaType.Fields.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							ValueMember valueMember = (ValueMember)obj;
							Type type2 = valueMember.ItemType;
							if (type2 == null)
							{
								type2 = valueMember.MemberType;
							}
							WireType wireType3;
							if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type2, out wireType3, false, false, false, false) == null)
							{
								int num3 = this.FindOrAddAuto(type2, false, false, false);
								if (num3 >= 0)
								{
									metaType2 = ((MetaType)this.types[num3]).GetSurrogateOrBaseOrSelf(false);
									if (!list.Contains(metaType2))
									{
										list.Add(metaType2);
										this.CascadeDependents(list, metaType2);
									}
								}
							}
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
				if (metaType.HasSubtypes)
				{
					foreach (SubType subType in metaType.GetSubtypes())
					{
						metaType2 = subType.DerivedType.GetSurrogateOrSelf();
						if (!list.Contains(metaType2))
						{
							list.Add(metaType2);
							this.CascadeDependents(list, metaType2);
						}
					}
				}
				metaType2 = metaType.BaseType;
				if (metaType2 != null)
				{
					metaType2 = metaType2.GetSurrogateOrSelf();
				}
				if (metaType2 != null && !list.Contains(metaType2))
				{
					list.Add(metaType2);
					this.CascadeDependents(list, metaType2);
				}
			}
		}

		// Token: 0x170003A7 RID: 935
		public MetaType this[Type type]
		{
			get
			{
				return (MetaType)this.types[this.FindOrAddAuto(type, true, false, false)];
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x0015A940 File Offset: 0x00158D40
		internal MetaType FindWithoutAdd(Type type)
		{
			foreach (object obj in this.types)
			{
				MetaType metaType = (MetaType)obj;
				if (metaType.Type == type)
				{
					if (metaType.Pending)
					{
						this.WaitOnLock(metaType);
					}
					return metaType;
				}
			}
			Type type2 = TypeModel.ResolveProxies(type);
			return (type2 != null) ? this.FindWithoutAdd(type2) : null;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0015A9B1 File Offset: 0x00158DB1
		private static bool MetaTypeFinderImpl(object value, object ctx)
		{
			return ((MetaType)value).Type == (Type)ctx;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0015A9C6 File Offset: 0x00158DC6
		private static bool BasicTypeFinderImpl(object value, object ctx)
		{
			return ((RuntimeTypeModel.BasicType)value).Type == (Type)ctx;
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x0015A9DC File Offset: 0x00158DDC
		private void WaitOnLock(MetaType type)
		{
			int opaqueToken = 0;
			try
			{
				this.TakeLock(ref opaqueToken);
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x0015AA10 File Offset: 0x00158E10
		internal IProtoSerializer TryGetBasicTypeSerializer(Type type)
		{
			int num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
			if (num >= 0)
			{
				return ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
			}
			object obj = this.basicTypes;
			IProtoSerializer result;
			lock (obj)
			{
				num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
				if (num >= 0)
				{
					result = ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
				}
				else
				{
					WireType wireType;
					IProtoSerializer protoSerializer = (MetaType.GetContractFamily(this, type, null) != MetaType.AttributeFamily.None) ? null : ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false);
					if (protoSerializer != null)
					{
						this.basicTypes.Add(new RuntimeTypeModel.BasicType(type, protoSerializer));
					}
					result = protoSerializer;
				}
			}
			return result;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x0015AAF4 File Offset: 0x00158EF4
		internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
		{
			int num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
			if (num >= 0)
			{
				MetaType metaType = (MetaType)this.types[num];
				if (metaType.Pending)
				{
					this.WaitOnLock(metaType);
				}
				return num;
			}
			bool flag = this.AutoAddMissingTypes || addEvenIfAutoDisabled;
			if (Helpers.IsEnum(type) || this.TryGetBasicTypeSerializer(type) == null)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null)
				{
					num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type2);
					type = type2;
				}
				if (num < 0)
				{
					int opaqueToken = 0;
					try
					{
						this.TakeLock(ref opaqueToken);
						MetaType metaType;
						if ((metaType = this.RecogniseCommonTypes(type)) == null)
						{
							MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, null);
							if (contractFamily == MetaType.AttributeFamily.AutoTuple)
							{
								addEvenIfAutoDisabled = (flag = true);
							}
							if (!flag || (!Helpers.IsEnum(type) && addWithContractOnly && contractFamily == MetaType.AttributeFamily.None))
							{
								if (demand)
								{
									TypeModel.ThrowUnexpectedType(type);
								}
								return num;
							}
							metaType = this.Create(type);
						}
						metaType.Pending = true;
						bool flag2 = false;
						int num2 = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
						if (num2 < 0)
						{
							this.ThrowIfFrozen();
							num = this.types.Add(metaType);
							flag2 = true;
						}
						else
						{
							num = num2;
						}
						if (flag2)
						{
							metaType.ApplyDefaultBehaviour();
							metaType.Pending = false;
						}
					}
					finally
					{
						this.ReleaseLock(opaqueToken);
					}
					return num;
				}
				return num;
			}
			if (flag && !addWithContractOnly)
			{
				throw MetaType.InbuiltType(type);
			}
			return -1;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x0015AC8C File Offset: 0x0015908C
		private MetaType RecogniseCommonTypes(Type type)
		{
			return null;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0015AC8F File Offset: 0x0015908F
		private MetaType Create(Type type)
		{
			this.ThrowIfFrozen();
			return new MetaType(this, type, this.defaultFactory);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0015ACA4 File Offset: 0x001590A4
		public MetaType Add(Type type, bool applyDefaultBehaviour)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MetaType metaType = this.FindWithoutAdd(type);
			if (metaType != null)
			{
				return metaType;
			}
			int opaqueToken = 0;
			if (type.IsInterface && base.MapType(MetaType.ienumerable).IsAssignableFrom(type) && TypeModel.GetListItemType(this, type) == null)
			{
				throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
			}
			try
			{
				metaType = this.RecogniseCommonTypes(type);
				if (metaType != null)
				{
					if (!applyDefaultBehaviour)
					{
						throw new ArgumentException("Default behaviour must be observed for certain types with special handling; " + type.FullName, "applyDefaultBehaviour");
					}
					applyDefaultBehaviour = false;
				}
				if (metaType == null)
				{
					metaType = this.Create(type);
				}
				metaType.Pending = true;
				this.TakeLock(ref opaqueToken);
				if (this.FindWithoutAdd(type) != null)
				{
					throw new ArgumentException("Duplicate type", "type");
				}
				this.ThrowIfFrozen();
				this.types.Add(metaType);
				if (applyDefaultBehaviour)
				{
					metaType.ApplyDefaultBehaviour();
				}
				metaType.Pending = false;
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
			return metaType;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x0015ADBC File Offset: 0x001591BC
		// (set) Token: 0x06002F6B RID: 12139 RVA: 0x0015ADC5 File Offset: 0x001591C5
		public bool AutoAddMissingTypes
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("The default model must allow missing types");
				}
				this.ThrowIfFrozen();
				this.SetOption(8, value);
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0015ADF2 File Offset: 0x001591F2
		private void ThrowIfFrozen()
		{
			if (this.GetOption(4))
			{
				throw new InvalidOperationException("The model cannot be changed once frozen");
			}
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0015AE0B File Offset: 0x0015920B
		public void Freeze()
		{
			if (this.GetOption(2))
			{
				throw new InvalidOperationException("The default model cannot be frozen");
			}
			this.SetOption(4, true);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0015AE2C File Offset: 0x0015922C
		protected override int GetKeyImpl(Type type)
		{
			return this.GetKey(type, false, true);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0015AE38 File Offset: 0x00159238
		internal int GetKey(Type type, bool demand, bool getBaseKey)
		{
			int result;
			try
			{
				int num = this.FindOrAddAuto(type, demand, true, false);
				if (num >= 0)
				{
					MetaType metaType = (MetaType)this.types[num];
					if (getBaseKey)
					{
						metaType = MetaType.GetRootType(metaType);
						num = this.FindOrAddAuto(metaType.Type, true, true, false);
					}
				}
				result = num;
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.Message.IndexOf(type.FullName) >= 0)
				{
					throw;
				}
				throw new ProtoException(ex.Message + " (" + type.FullName + ")", ex);
			}
			return result;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0015AEEC File Offset: 0x001592EC
		protected internal override void Serialize(int key, object value, ProtoWriter dest)
		{
			((MetaType)this.types[key]).Serializer.Write(value, dest);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x0015AF0C File Offset: 0x0015930C
		protected internal override object Deserialize(int key, object value, ProtoReader source)
		{
			IProtoSerializer serializer = ((MetaType)this.types[key]).Serializer;
			if (value == null && Helpers.IsValueType(serializer.ExpectedType))
			{
				if (serializer.RequiresOldValue)
				{
					value = Activator.CreateInstance(serializer.ExpectedType);
				}
				return serializer.Read(value, source);
			}
			return serializer.Read(value, source);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0015AF70 File Offset: 0x00159370
		internal bool IsPrepared(Type type)
		{
			MetaType metaType = this.FindWithoutAdd(type);
			return metaType != null && metaType.IsPrepared();
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0015AF94 File Offset: 0x00159394
		internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
		{
			int num = this.FindOrAddAuto(type, false, false, false);
			return (num >= 0) ? ((MetaType)this.types[num]).GetEnumMap() : null;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x0015AFCF File Offset: 0x001593CF
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x0015AFD7 File Offset: 0x001593D7
		public int MetadataTimeoutMilliseconds
		{
			get
			{
				return this.metadataTimeoutMilliseconds;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
				}
				this.metadataTimeoutMilliseconds = value;
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x0015AFF2 File Offset: 0x001593F2
		internal void TakeLock(ref int opaqueToken)
		{
			opaqueToken = 0;
			if (Monitor.TryEnter(this.types, this.metadataTimeoutMilliseconds))
			{
				opaqueToken = this.GetContention();
				return;
			}
			this.AddContention();
			throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0015B02B File Offset: 0x0015942B
		private int GetContention()
		{
			return Interlocked.CompareExchange(ref this.contentionCounter, 0, 0);
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0015B03A File Offset: 0x0015943A
		private void AddContention()
		{
			Interlocked.Increment(ref this.contentionCounter);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0015B048 File Offset: 0x00159448
		internal void ReleaseLock(int opaqueToken)
		{
			if (opaqueToken != 0)
			{
				Monitor.Exit(this.types);
				if (opaqueToken != this.GetContention())
				{
					LockContentedEventHandler lockContended = this.LockContended;
					if (lockContended != null)
					{
						string stackTrace;
						try
						{
							throw new ProtoException();
						}
						catch (Exception ex)
						{
							stackTrace = ex.StackTrace;
						}
						lockContended(this, new LockContentedEventArgs(stackTrace));
					}
				}
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06002F7A RID: 12154 RVA: 0x0015B0B0 File Offset: 0x001594B0
		// (remove) Token: 0x06002F7B RID: 12155 RVA: 0x0015B0E8 File Offset: 0x001594E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LockContentedEventHandler LockContended;

		// Token: 0x06002F7C RID: 12156 RVA: 0x0015B120 File Offset: 0x00159520
		internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (Helpers.GetTypeCode(type) != ProtoTypeCode.Unknown)
			{
				return;
			}
			if (this[type].IgnoreListHandling)
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
				if (itemType == base.MapType(typeof(byte)))
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
				itemType = TypeModel.GetListItemType(this, type);
			}
			if (itemType != null)
			{
				Type type3 = null;
				Type type4 = null;
				this.ResolveListTypes(itemType, ref type3, ref type4);
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
					if (type.IsGenericType && type.GetGenericTypeDefinition() == base.MapType(typeof(IDictionary<, >)) && itemType == base.MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = base.MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = base.MapType(typeof(List<>)).MakeGenericType(new Type[]
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

		// Token: 0x06002F7D RID: 12157 RVA: 0x0015B2C4 File Offset: 0x001596C4
		internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref bool requiresBclImport)
		{
			Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
			if (underlyingType != null)
			{
				effectiveType = underlyingType;
			}
			if (effectiveType == base.MapType(typeof(byte[])))
			{
				return "bytes";
			}
			WireType wireType;
			IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out wireType, false, false, false, false);
			if (protoSerializer == null)
			{
				if (asReference || dynamicType)
				{
					requiresBclImport = true;
					return "bcl.NetObjectProxy";
				}
				return this[effectiveType].GetSurrogateOrBaseOrSelf(true).GetSchemaTypeName();
			}
			else
			{
				if (protoSerializer is ParseableSerializer)
				{
					if (asReference)
					{
						requiresBclImport = true;
					}
					return (!asReference) ? "string" : "bcl.NetObjectProxy";
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(effectiveType);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return "bool";
				case ProtoTypeCode.Char:
				case ProtoTypeCode.Byte:
				case ProtoTypeCode.UInt16:
				case ProtoTypeCode.UInt32:
					if (dataFormat != DataFormat.FixedSize)
					{
						return "uint32";
					}
					return "fixed32";
				case ProtoTypeCode.SByte:
				case ProtoTypeCode.Int16:
				case ProtoTypeCode.Int32:
					if (dataFormat == DataFormat.ZigZag)
					{
						return "sint32";
					}
					if (dataFormat != DataFormat.FixedSize)
					{
						return "int32";
					}
					return "sfixed32";
				case ProtoTypeCode.Int64:
					if (dataFormat == DataFormat.ZigZag)
					{
						return "sint64";
					}
					if (dataFormat != DataFormat.FixedSize)
					{
						return "int64";
					}
					return "sfixed64";
				case ProtoTypeCode.UInt64:
					if (dataFormat != DataFormat.FixedSize)
					{
						return "uint64";
					}
					return "fixed64";
				case ProtoTypeCode.Single:
					return "float";
				case ProtoTypeCode.Double:
					return "double";
				case ProtoTypeCode.Decimal:
					requiresBclImport = true;
					return "bcl.Decimal";
				case ProtoTypeCode.DateTime:
					requiresBclImport = true;
					return "bcl.DateTime";
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						requiresBclImport = true;
						return "bcl.TimeSpan";
					case ProtoTypeCode.Guid:
						requiresBclImport = true;
						return "bcl.Guid";
					}
					throw new NotSupportedException("No .proto map found for: " + effectiveType.FullName);
				case ProtoTypeCode.String:
					if (asReference)
					{
						requiresBclImport = true;
					}
					return (!asReference) ? "string" : "bcl.NetObjectProxy";
				}
			}
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0015B4B9 File Offset: 0x001598B9
		public void SetDefaultFactory(MethodInfo methodInfo)
		{
			this.VerifyFactory(methodInfo, null);
			this.defaultFactory = methodInfo;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0015B4CC File Offset: 0x001598CC
		internal void VerifyFactory(MethodInfo factory, Type type)
		{
			if (factory != null)
			{
				if (type != null && Helpers.IsValueType(type))
				{
					throw new InvalidOperationException();
				}
				if (!factory.IsStatic)
				{
					throw new ArgumentException("A factory-method must be static", "factory");
				}
				if (type != null && factory.ReturnType != type && factory.ReturnType != base.MapType(typeof(object)))
				{
					throw new ArgumentException("The factory-method must return object" + ((type != null) ? (" or " + type.FullName) : string.Empty), "factory");
				}
				if (!CallbackSet.CheckCallbackParameters(this, factory))
				{
					throw new ArgumentException("Invalid factory signature in " + factory.DeclaringType.FullName + "." + factory.Name, "factory");
				}
			}
		}

		// Token: 0x04002DBB RID: 11707
		private byte options;

		// Token: 0x04002DBC RID: 11708
		private const byte OPTIONS_InferTagFromNameDefault = 1;

		// Token: 0x04002DBD RID: 11709
		private const byte OPTIONS_IsDefaultModel = 2;

		// Token: 0x04002DBE RID: 11710
		private const byte OPTIONS_Frozen = 4;

		// Token: 0x04002DBF RID: 11711
		private const byte OPTIONS_AutoAddMissingTypes = 8;

		// Token: 0x04002DC0 RID: 11712
		private const byte OPTIONS_UseImplicitZeroDefaults = 32;

		// Token: 0x04002DC1 RID: 11713
		private const byte OPTIONS_AllowParseableTypes = 64;

		// Token: 0x04002DC2 RID: 11714
		private const byte OPTIONS_AutoAddProtoContractTypesOnly = 128;

		// Token: 0x04002DC3 RID: 11715
		private static readonly BasicList.MatchPredicate MetaTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.MetaTypeFinderImpl);

		// Token: 0x04002DC4 RID: 11716
		private static readonly BasicList.MatchPredicate BasicTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.BasicTypeFinderImpl);

		// Token: 0x04002DC5 RID: 11717
		private BasicList basicTypes = new BasicList();

		// Token: 0x04002DC6 RID: 11718
		private readonly BasicList types = new BasicList();

		// Token: 0x04002DC7 RID: 11719
		private int metadataTimeoutMilliseconds = 5000;

		// Token: 0x04002DC8 RID: 11720
		private int contentionCounter = 1;

		// Token: 0x04002DCA RID: 11722
		private MethodInfo defaultFactory;

		// Token: 0x0200065C RID: 1628
		private sealed class Singleton
		{
			// Token: 0x06002F81 RID: 12161 RVA: 0x0015B5CE File Offset: 0x001599CE
			private Singleton()
			{
			}

			// Token: 0x04002DCB RID: 11723
			internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(true);
		}

		// Token: 0x0200065D RID: 1629
		private sealed class BasicType
		{
			// Token: 0x06002F83 RID: 12163 RVA: 0x0015B5E3 File Offset: 0x001599E3
			public BasicType(Type type, IProtoSerializer serializer)
			{
				this.type = type;
				this.serializer = serializer;
			}

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06002F84 RID: 12164 RVA: 0x0015B5F9 File Offset: 0x001599F9
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06002F85 RID: 12165 RVA: 0x0015B601 File Offset: 0x00159A01
			public IProtoSerializer Serializer
			{
				get
				{
					return this.serializer;
				}
			}

			// Token: 0x04002DCC RID: 11724
			private readonly Type type;

			// Token: 0x04002DCD RID: 11725
			private readonly IProtoSerializer serializer;
		}
	}
}
