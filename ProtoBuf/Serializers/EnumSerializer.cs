using System;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000687 RID: 1671
	internal sealed class EnumSerializer : IProtoSerializer
	{
		// Token: 0x0600313F RID: 12607 RVA: 0x0016096C File Offset: 0x0015ED6C
		public EnumSerializer(Type enumType, EnumSerializer.EnumPair[] map)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			this.enumType = enumType;
			this.map = map;
			if (map != null)
			{
				for (int i = 1; i < map.Length; i++)
				{
					for (int j = 0; j < i; j++)
					{
						if (map[i].WireValue == map[j].WireValue && !object.Equals(map[i].RawValue, map[j].RawValue))
						{
							throw new ProtoException("Multiple enums with wire-value " + map[i].WireValue.ToString());
						}
						if (object.Equals(map[i].RawValue, map[j].RawValue) && map[i].WireValue != map[j].WireValue)
						{
							throw new ProtoException("Multiple enums with deserialized-value " + map[i].RawValue);
						}
					}
				}
			}
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x00160A90 File Offset: 0x0015EE90
		private ProtoTypeCode GetTypeCode()
		{
			Type underlyingType = Helpers.GetUnderlyingType(this.enumType);
			if (underlyingType == null)
			{
				underlyingType = this.enumType;
			}
			return Helpers.GetTypeCode(underlyingType);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x00160ABC File Offset: 0x0015EEBC
		public Type ExpectedType
		{
			get
			{
				return this.enumType;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x00160AC4 File Offset: 0x0015EEC4
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x00160AC7 File Offset: 0x0015EEC7
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x00160ACC File Offset: 0x0015EECC
		private int EnumToWire(object value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return (int)((sbyte)value);
			case ProtoTypeCode.Byte:
				return (int)((byte)value);
			case ProtoTypeCode.Int16:
				return (int)((short)value);
			case ProtoTypeCode.UInt16:
				return (int)((ushort)value);
			case ProtoTypeCode.Int32:
				return (int)value;
			case ProtoTypeCode.UInt32:
				return (int)((uint)value);
			case ProtoTypeCode.Int64:
				return (int)((long)value);
			case ProtoTypeCode.UInt64:
				return (int)((ulong)value);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x00160B50 File Offset: 0x0015EF50
		private object WireToEnum(int value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return Enum.ToObject(this.enumType, (sbyte)value);
			case ProtoTypeCode.Byte:
				return Enum.ToObject(this.enumType, (byte)value);
			case ProtoTypeCode.Int16:
				return Enum.ToObject(this.enumType, (short)value);
			case ProtoTypeCode.UInt16:
				return Enum.ToObject(this.enumType, (ushort)value);
			case ProtoTypeCode.Int32:
				return Enum.ToObject(this.enumType, value);
			case ProtoTypeCode.UInt32:
				return Enum.ToObject(this.enumType, (uint)value);
			case ProtoTypeCode.Int64:
				return Enum.ToObject(this.enumType, (long)value);
			case ProtoTypeCode.UInt64:
				return Enum.ToObject(this.enumType, (ulong)((long)value));
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x00160C04 File Offset: 0x0015F004
		public object Read(object value, ProtoReader source)
		{
			int num = source.ReadInt32();
			if (this.map == null)
			{
				return this.WireToEnum(num);
			}
			for (int i = 0; i < this.map.Length; i++)
			{
				if (this.map[i].WireValue == num)
				{
					return this.map[i].TypedValue;
				}
			}
			source.ThrowEnumException(this.ExpectedType, num);
			return null;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00160C7C File Offset: 0x0015F07C
		public void Write(object value, ProtoWriter dest)
		{
			if (this.map == null)
			{
				ProtoWriter.WriteInt32(this.EnumToWire(value), dest);
			}
			else
			{
				for (int i = 0; i < this.map.Length; i++)
				{
					if (object.Equals(this.map[i].TypedValue, value))
					{
						ProtoWriter.WriteInt32(this.map[i].WireValue, dest);
						return;
					}
				}
				ProtoWriter.ThrowEnumException(dest, value);
			}
		}

		// Token: 0x04002E68 RID: 11880
		private readonly Type enumType;

		// Token: 0x04002E69 RID: 11881
		private readonly EnumSerializer.EnumPair[] map;

		// Token: 0x02000688 RID: 1672
		public struct EnumPair
		{
			// Token: 0x06003148 RID: 12616 RVA: 0x00160CFA File Offset: 0x0015F0FA
			public EnumPair(int wireValue, object raw, Type type)
			{
				this.WireValue = wireValue;
				this.RawValue = raw;
				this.TypedValue = (Enum)Enum.ToObject(type, raw);
			}

			// Token: 0x04002E6A RID: 11882
			public readonly object RawValue;

			// Token: 0x04002E6B RID: 11883
			public readonly Enum TypedValue;

			// Token: 0x04002E6C RID: 11884
			public readonly int WireValue;
		}
	}
}
