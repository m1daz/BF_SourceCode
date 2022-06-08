using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000668 RID: 1640
	public class ValueMember
	{
		// Token: 0x06002FE1 RID: 12257 RVA: 0x0015B800 File Offset: 0x00159C00
		public ValueMember(RuntimeTypeModel model, Type parentType, int fieldNumber, MemberInfo member, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat, object defaultValue) : this(model, fieldNumber, memberType, itemType, defaultType, dataFormat)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (parentType == null)
			{
				throw new ArgumentNullException("parentType");
			}
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			this.member = member;
			this.parentType = parentType;
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (defaultValue != null && model.MapType(defaultValue.GetType()) != memberType)
			{
				defaultValue = ValueMember.ParseDefaultValue(memberType, defaultValue);
			}
			this.defaultValue = defaultValue;
			MetaType metaType = model.FindWithoutAdd(memberType);
			if (metaType != null)
			{
				this.asReference = metaType.AsReferenceDefault;
			}
			else
			{
				this.asReference = MetaType.GetAsReferenceDefault(model, memberType);
			}
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0015B8E8 File Offset: 0x00159CE8
		internal ValueMember(RuntimeTypeModel model, int fieldNumber, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat)
		{
			if (memberType == null)
			{
				throw new ArgumentNullException("memberType");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.fieldNumber = fieldNumber;
			this.memberType = memberType;
			this.itemType = itemType;
			this.defaultType = defaultType;
			this.model = model;
			this.dataFormat = dataFormat;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x0015B94A File Offset: 0x00159D4A
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x0015B952 File Offset: 0x00159D52
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x0015B95A File Offset: 0x00159D5A
		public Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x0015B962 File Offset: 0x00159D62
		public Type MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x0015B96A File Offset: 0x00159D6A
		public Type DefaultType
		{
			get
			{
				return this.defaultType;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x0015B972 File Offset: 0x00159D72
		public Type ParentType
		{
			get
			{
				return this.parentType;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x0015B97A File Offset: 0x00159D7A
		// (set) Token: 0x06002FEA RID: 12266 RVA: 0x0015B982 File Offset: 0x00159D82
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.ThrowIfFrozen();
				this.defaultValue = value;
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0015B991 File Offset: 0x00159D91
		internal object GetRawEnumValue()
		{
			return ((FieldInfo)this.member).GetRawConstantValue();
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x0015B9A4 File Offset: 0x00159DA4
		private static object ParseDefaultValue(Type type, object value)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (value is string)
			{
				string text = (string)value;
				if (Helpers.IsEnum(type))
				{
					return Helpers.ParseEnum(type, text);
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return bool.Parse(text);
				case ProtoTypeCode.Char:
					if (text.Length == 1)
					{
						return text[0];
					}
					throw new FormatException("Single character expected: \"" + text + "\"");
				case ProtoTypeCode.SByte:
					return sbyte.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Byte:
					return byte.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int16:
					return short.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt16:
					return ushort.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int32:
					return int.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt32:
					return uint.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int64:
					return long.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt64:
					return ulong.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Single:
					return float.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Double:
					return double.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Decimal:
					return decimal.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.DateTime:
					return DateTime.Parse(text, CultureInfo.InvariantCulture);
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						return TimeSpan.Parse(text);
					case ProtoTypeCode.Guid:
						return new Guid(text);
					case ProtoTypeCode.Uri:
						return text;
					}
					break;
				case ProtoTypeCode.String:
					return text;
				}
			}
			if (Helpers.IsEnum(type))
			{
				return Enum.ToObject(type, value);
			}
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x0015BBC4 File Offset: 0x00159FC4
		internal IProtoSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = this.BuildSerializer();
				}
				return this.serializer;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x0015BBE3 File Offset: 0x00159FE3
		// (set) Token: 0x06002FEF RID: 12271 RVA: 0x0015BBEB File Offset: 0x00159FEB
		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.ThrowIfFrozen();
				this.dataFormat = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x0015BBFA File Offset: 0x00159FFA
		// (set) Token: 0x06002FF1 RID: 12273 RVA: 0x0015BC03 File Offset: 0x0015A003
		public bool IsStrict
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, true);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x0015BC0E File Offset: 0x0015A00E
		// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x0015BC17 File Offset: 0x0015A017
		public bool IsPacked
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x0015BC22 File Offset: 0x0015A022
		// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x0015BC2B File Offset: 0x0015A02B
		public bool OverwriteList
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value, true);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x0015BC36 File Offset: 0x0015A036
		// (set) Token: 0x06002FF7 RID: 12279 RVA: 0x0015BC3F File Offset: 0x0015A03F
		public bool IsRequired
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value, true);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x0015BC4A File Offset: 0x0015A04A
		// (set) Token: 0x06002FF9 RID: 12281 RVA: 0x0015BC52 File Offset: 0x0015A052
		public bool AsReference
		{
			get
			{
				return this.asReference;
			}
			set
			{
				this.ThrowIfFrozen();
				this.asReference = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x0015BC61 File Offset: 0x0015A061
		// (set) Token: 0x06002FFB RID: 12283 RVA: 0x0015BC69 File Offset: 0x0015A069
		public bool DynamicType
		{
			get
			{
				return this.dynamicType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.dynamicType = value;
			}
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0015BC78 File Offset: 0x0015A078
		public void SetSpecified(MethodInfo getSpecified, MethodInfo setSpecified)
		{
			if (getSpecified != null && (getSpecified.ReturnType != this.model.MapType(typeof(bool)) || getSpecified.IsStatic || getSpecified.GetParameters().Length != 0))
			{
				throw new ArgumentException("Invalid pattern for checking member-specified", "getSpecified");
			}
			ParameterInfo[] parameters;
			if (setSpecified != null && (setSpecified.ReturnType != this.model.MapType(typeof(void)) || setSpecified.IsStatic || (parameters = setSpecified.GetParameters()).Length != 1 || parameters[0].ParameterType != this.model.MapType(typeof(bool))))
			{
				throw new ArgumentException("Invalid pattern for setting member-specified", "setSpecified");
			}
			this.ThrowIfFrozen();
			this.getSpecified = getSpecified;
			this.setSpecified = setSpecified;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0015BD5A File Offset: 0x0015A15A
		private void ThrowIfFrozen()
		{
			if (this.serializer != null)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated");
			}
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0015BD74 File Offset: 0x0015A174
		private IProtoSerializer BuildSerializer()
		{
			int opaqueToken = 0;
			IProtoSerializer result;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				Type type = (this.itemType != null) ? this.itemType : this.memberType;
				WireType wireType;
				IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this.model, this.dataFormat, type, out wireType, this.asReference, this.dynamicType, this.OverwriteList, true);
				if (protoSerializer == null)
				{
					throw new InvalidOperationException("No serializer defined for type: " + type.FullName);
				}
				if (this.itemType != null && this.SupportNull)
				{
					if (this.IsPacked)
					{
						throw new NotSupportedException("Packed encodings cannot support null values");
					}
					protoSerializer = new TagDecorator(1, wireType, this.IsStrict, protoSerializer);
					protoSerializer = new NullDecorator(this.model, protoSerializer);
					protoSerializer = new TagDecorator(this.fieldNumber, WireType.StartGroup, false, protoSerializer);
				}
				else
				{
					protoSerializer = new TagDecorator(this.fieldNumber, wireType, this.IsStrict, protoSerializer);
				}
				if (this.itemType != null)
				{
					Type type2 = (!this.SupportNull) ? (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType) : this.itemType;
					if (this.memberType.IsArray)
					{
						protoSerializer = new ArrayDecorator(this.model, protoSerializer, this.fieldNumber, this.IsPacked, wireType, this.memberType, this.OverwriteList, this.SupportNull);
					}
					else
					{
						protoSerializer = ListDecorator.Create(this.model, this.memberType, this.defaultType, protoSerializer, this.fieldNumber, this.IsPacked, wireType, this.member != null && PropertyDecorator.CanWrite(this.model, this.member), this.OverwriteList, this.SupportNull);
					}
				}
				else if (this.defaultValue != null && !this.IsRequired && this.getSpecified == null)
				{
					protoSerializer = new DefaultValueDecorator(this.model, this.defaultValue, protoSerializer);
				}
				if (this.memberType == this.model.MapType(typeof(Uri)))
				{
					protoSerializer = new UriDecorator(this.model, protoSerializer);
				}
				if (this.member != null)
				{
					PropertyInfo propertyInfo = this.member as PropertyInfo;
					if (propertyInfo != null)
					{
						protoSerializer = new PropertyDecorator(this.model, this.parentType, (PropertyInfo)this.member, protoSerializer);
					}
					else
					{
						FieldInfo fieldInfo = this.member as FieldInfo;
						if (fieldInfo == null)
						{
							throw new InvalidOperationException();
						}
						protoSerializer = new FieldDecorator(this.parentType, (FieldInfo)this.member, protoSerializer);
					}
					if (this.getSpecified != null || this.setSpecified != null)
					{
						protoSerializer = new MemberSpecifiedDecorator(this.getSpecified, this.setSpecified, protoSerializer);
					}
				}
				result = protoSerializer;
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
			return result;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x0015C06C File Offset: 0x0015A46C
		private static WireType GetIntWireType(DataFormat format, int width)
		{
			switch (format)
			{
			case DataFormat.Default:
			case DataFormat.TwosComplement:
				return WireType.Variant;
			case DataFormat.ZigZag:
				return WireType.SignedVariant;
			case DataFormat.FixedSize:
				return (width != 32) ? WireType.Fixed64 : WireType.Fixed32;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0015C0A2 File Offset: 0x0015A4A2
		private static WireType GetDateTimeWireType(DataFormat format)
		{
			switch (format)
			{
			case DataFormat.Default:
				return WireType.String;
			case DataFormat.FixedSize:
				return WireType.Fixed64;
			case DataFormat.Group:
				return WireType.StartGroup;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0015C0D0 File Offset: 0x0015A4D0
		internal static IProtoSerializer TryGetCoreSerializer(RuntimeTypeModel model, DataFormat dataFormat, Type type, out WireType defaultWireType, bool asReference, bool dynamicType, bool overwriteList, bool allowComplexTypes)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (Helpers.IsEnum(type))
			{
				if (allowComplexTypes && model != null)
				{
					defaultWireType = WireType.Variant;
					return new EnumSerializer(type, model.GetEnumMap(type));
				}
				defaultWireType = WireType.None;
				return null;
			}
			else
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultWireType = WireType.Variant;
					return new BooleanSerializer(model);
				case ProtoTypeCode.Char:
					defaultWireType = WireType.Variant;
					return new CharSerializer(model);
				case ProtoTypeCode.SByte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new SByteSerializer(model);
				case ProtoTypeCode.Byte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new ByteSerializer(model);
				case ProtoTypeCode.Int16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int16Serializer(model);
				case ProtoTypeCode.UInt16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt16Serializer(model);
				case ProtoTypeCode.Int32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int32Serializer(model);
				case ProtoTypeCode.UInt32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt32Serializer(model);
				case ProtoTypeCode.Int64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new Int64Serializer(model);
				case ProtoTypeCode.UInt64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new UInt64Serializer(model);
				case ProtoTypeCode.Single:
					defaultWireType = WireType.Fixed32;
					return new SingleSerializer(model);
				case ProtoTypeCode.Double:
					defaultWireType = WireType.Fixed64;
					return new DoubleSerializer(model);
				case ProtoTypeCode.Decimal:
					defaultWireType = WireType.String;
					return new DecimalSerializer(model);
				case ProtoTypeCode.DateTime:
					defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
					return new DateTimeSerializer(model);
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
						return new TimeSpanSerializer(model);
					case ProtoTypeCode.ByteArray:
						defaultWireType = WireType.String;
						return new BlobSerializer(model, overwriteList);
					case ProtoTypeCode.Guid:
						defaultWireType = WireType.String;
						return new GuidSerializer(model);
					case ProtoTypeCode.Uri:
						defaultWireType = WireType.String;
						return new StringSerializer(model);
					case ProtoTypeCode.Type:
						defaultWireType = WireType.String;
						return new SystemTypeSerializer(model);
					default:
					{
						IProtoSerializer protoSerializer = (!model.AllowParseableTypes) ? null : ParseableSerializer.TryCreate(type, model);
						if (protoSerializer != null)
						{
							defaultWireType = WireType.String;
							return protoSerializer;
						}
						if (allowComplexTypes && model != null)
						{
							int key = model.GetKey(type, false, true);
							if (asReference || dynamicType)
							{
								defaultWireType = ((dataFormat != DataFormat.Group) ? WireType.String : WireType.StartGroup);
								BclHelpers.NetObjectOptions netObjectOptions = BclHelpers.NetObjectOptions.None;
								if (asReference)
								{
									netObjectOptions |= BclHelpers.NetObjectOptions.AsReference;
								}
								if (dynamicType)
								{
									netObjectOptions |= BclHelpers.NetObjectOptions.DynamicType;
								}
								if (key >= 0)
								{
									if (asReference && Helpers.IsValueType(type))
									{
										string text = "AsReference cannot be used with value-types";
										if (type.Name == "KeyValuePair`2")
										{
											text += "; please see http://stackoverflow.com/q/14436606/";
										}
										else
										{
											text = text + ": " + type.FullName;
										}
										throw new InvalidOperationException(text);
									}
									MetaType metaType = model[type];
									if (asReference && metaType.IsAutoTuple)
									{
										netObjectOptions |= BclHelpers.NetObjectOptions.LateSet;
									}
									if (metaType.UseConstructor)
									{
										netObjectOptions |= BclHelpers.NetObjectOptions.UseConstructor;
									}
								}
								return new NetObjectSerializer(model, type, key, netObjectOptions);
							}
							if (key >= 0)
							{
								defaultWireType = ((dataFormat != DataFormat.Group) ? WireType.String : WireType.StartGroup);
								return new SubItemSerializer(type, key, model[type], true);
							}
						}
						defaultWireType = WireType.None;
						return null;
					}
					}
					break;
				case ProtoTypeCode.String:
					defaultWireType = WireType.String;
					if (asReference)
					{
						return new NetObjectSerializer(model, model.MapType(typeof(string)), 0, BclHelpers.NetObjectOptions.AsReference);
					}
					return new StringSerializer(model);
				}
			}
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x0015C400 File Offset: 0x0015A800
		internal void SetName(string name)
		{
			this.ThrowIfFrozen();
			this.name = name;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x0015C40F File Offset: 0x0015A80F
		public string Name
		{
			get
			{
				return (!Helpers.IsNullOrEmpty(this.name)) ? this.name : this.member.Name;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x0015C437 File Offset: 0x0015A837
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x0015C444 File Offset: 0x0015A844
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x0015C494 File Offset: 0x0015A894
		// (set) Token: 0x06003007 RID: 12295 RVA: 0x0015C49E File Offset: 0x0015A89E
		public bool SupportNull
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value, true);
			}
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x0015C4AC File Offset: 0x0015A8AC
		internal string GetSchemaTypeName(bool applyNetObjectProxy, ref bool requiresBclImport)
		{
			Type type = this.ItemType;
			if (type == null)
			{
				type = this.MemberType;
			}
			return this.model.GetSchemaTypeName(type, this.DataFormat, applyNetObjectProxy && this.asReference, applyNetObjectProxy && this.dynamicType, ref requiresBclImport);
		}

		// Token: 0x04002DE7 RID: 11751
		private readonly int fieldNumber;

		// Token: 0x04002DE8 RID: 11752
		private readonly MemberInfo member;

		// Token: 0x04002DE9 RID: 11753
		private readonly Type parentType;

		// Token: 0x04002DEA RID: 11754
		private readonly Type itemType;

		// Token: 0x04002DEB RID: 11755
		private readonly Type defaultType;

		// Token: 0x04002DEC RID: 11756
		private readonly Type memberType;

		// Token: 0x04002DED RID: 11757
		private object defaultValue;

		// Token: 0x04002DEE RID: 11758
		private readonly RuntimeTypeModel model;

		// Token: 0x04002DEF RID: 11759
		private IProtoSerializer serializer;

		// Token: 0x04002DF0 RID: 11760
		private DataFormat dataFormat;

		// Token: 0x04002DF1 RID: 11761
		private bool asReference;

		// Token: 0x04002DF2 RID: 11762
		private bool dynamicType;

		// Token: 0x04002DF3 RID: 11763
		private MethodInfo getSpecified;

		// Token: 0x04002DF4 RID: 11764
		private MethodInfo setSpecified;

		// Token: 0x04002DF5 RID: 11765
		private string name;

		// Token: 0x04002DF6 RID: 11766
		private const byte OPTIONS_IsStrict = 1;

		// Token: 0x04002DF7 RID: 11767
		private const byte OPTIONS_IsPacked = 2;

		// Token: 0x04002DF8 RID: 11768
		private const byte OPTIONS_IsRequired = 4;

		// Token: 0x04002DF9 RID: 11769
		private const byte OPTIONS_OverwriteList = 8;

		// Token: 0x04002DFA RID: 11770
		private const byte OPTIONS_SupportNull = 16;

		// Token: 0x04002DFB RID: 11771
		private byte flags;

		// Token: 0x02000669 RID: 1641
		internal sealed class Comparer : IComparer, IComparer<ValueMember>
		{
			// Token: 0x0600300A RID: 12298 RVA: 0x0015C506 File Offset: 0x0015A906
			public int Compare(object x, object y)
			{
				return this.Compare(x as ValueMember, y as ValueMember);
			}

			// Token: 0x0600300B RID: 12299 RVA: 0x0015C51C File Offset: 0x0015A91C
			public int Compare(ValueMember x, ValueMember y)
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
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}

			// Token: 0x04002DFC RID: 11772
			public static readonly ValueMember.Comparer Default = new ValueMember.Comparer();
		}
	}
}
