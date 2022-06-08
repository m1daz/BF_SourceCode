using System;

namespace ProtoBuf
{
	// Token: 0x0200063F RID: 1599
	public static class BclHelpers
	{
		// Token: 0x06002E5D RID: 11869 RVA: 0x001536A0 File Offset: 0x00151AA0
		public static object GetUninitializedObject(Type type)
		{
			throw new NotSupportedException("Constructor-skipping is not supported on this platform");
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x001536AC File Offset: 0x00151AAC
		public static void WriteTimeSpan(TimeSpan timeSpan, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			switch (dest.WireType)
			{
			case WireType.Fixed64:
				ProtoWriter.WriteInt64(timeSpan.Ticks, dest);
				break;
			case WireType.String:
			case WireType.StartGroup:
			{
				long num = timeSpan.Ticks;
				TimeSpanScale timeSpanScale;
				if (timeSpan == TimeSpan.MaxValue)
				{
					num = 1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (timeSpan == TimeSpan.MinValue)
				{
					num = -1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (num % 864000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Days;
					num /= 864000000000L;
				}
				else if (num % 36000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Hours;
					num /= 36000000000L;
				}
				else if (num % 600000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Minutes;
					num /= 600000000L;
				}
				else if (num % 10000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Seconds;
					num /= 10000000L;
				}
				else if (num % 10000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Milliseconds;
					num /= 10000L;
				}
				else
				{
					timeSpanScale = TimeSpanScale.Ticks;
				}
				SubItemToken token = ProtoWriter.StartSubItem(null, dest);
				if (num != 0L)
				{
					ProtoWriter.WriteFieldHeader(1, WireType.SignedVariant, dest);
					ProtoWriter.WriteInt64(num, dest);
				}
				if (timeSpanScale != TimeSpanScale.Days)
				{
					ProtoWriter.WriteFieldHeader(2, WireType.Variant, dest);
					ProtoWriter.WriteInt32((int)timeSpanScale, dest);
				}
				ProtoWriter.EndSubItem(token, dest);
				break;
			}
			default:
				throw new ProtoException("Unexpected wire-type: " + dest.WireType.ToString());
			}
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x00153848 File Offset: 0x00151C48
		public static TimeSpan ReadTimeSpan(ProtoReader source)
		{
			long num = BclHelpers.ReadTimeSpanTicks(source);
			if (num == -9223372036854775808L)
			{
				return TimeSpan.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromTicks(num);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0015388C File Offset: 0x00151C8C
		public static DateTime ReadDateTime(ProtoReader source)
		{
			long num = BclHelpers.ReadTimeSpanTicks(source);
			if (num == -9223372036854775808L)
			{
				return DateTime.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return DateTime.MaxValue;
			}
			return BclHelpers.EpochOrigin.AddTicks(num);
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x001538D8 File Offset: 0x00151CD8
		public static void WriteDateTime(DateTime value, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			TimeSpan timeSpan;
			if (wireType != WireType.StartGroup && wireType != WireType.String)
			{
				timeSpan = value - BclHelpers.EpochOrigin;
			}
			else if (value == DateTime.MaxValue)
			{
				timeSpan = TimeSpan.MaxValue;
			}
			else if (value == DateTime.MinValue)
			{
				timeSpan = TimeSpan.MinValue;
			}
			else
			{
				timeSpan = value - BclHelpers.EpochOrigin;
			}
			BclHelpers.WriteTimeSpan(timeSpan, dest);
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x00153970 File Offset: 0x00151D70
		private static long ReadTimeSpanTicks(ProtoReader source)
		{
			switch (source.WireType)
			{
			case WireType.Fixed64:
				return source.ReadInt64();
			case WireType.String:
			case WireType.StartGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				TimeSpanScale timeSpanScale = TimeSpanScale.Days;
				long num = 0L;
				int num2;
				while ((num2 = source.ReadFieldHeader()) > 0)
				{
					if (num2 != 2)
					{
						if (num2 != 1)
						{
							source.SkipField();
						}
						else
						{
							source.Assert(WireType.SignedVariant);
							num = source.ReadInt64();
						}
					}
					else
					{
						timeSpanScale = (TimeSpanScale)source.ReadInt32();
					}
				}
				ProtoReader.EndSubItem(token, source);
				switch (timeSpanScale)
				{
				case TimeSpanScale.Days:
					return num * 864000000000L;
				case TimeSpanScale.Hours:
					return num * 36000000000L;
				case TimeSpanScale.Minutes:
					return num * 600000000L;
				case TimeSpanScale.Seconds:
					return num * 10000000L;
				case TimeSpanScale.Milliseconds:
					return num * 10000L;
				case TimeSpanScale.Ticks:
					return num;
				default:
					if (timeSpanScale != TimeSpanScale.MinMax)
					{
						throw new ProtoException("Unknown timescale: " + timeSpanScale.ToString());
					}
					if (num == 1L)
					{
						return long.MaxValue;
					}
					if (num != -1L)
					{
						throw new ProtoException("Unknown min/max value: " + num.ToString());
					}
					return long.MinValue;
				}
				break;
			}
			default:
				throw new ProtoException("Unexpected wire-type: " + source.WireType.ToString());
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x00153AF8 File Offset: 0x00151EF8
		public static decimal ReadDecimal(ProtoReader reader)
		{
			ulong num = 0UL;
			uint num2 = 0U;
			uint num3 = 0U;
			SubItemToken token = ProtoReader.StartSubItem(reader);
			int num4;
			while ((num4 = reader.ReadFieldHeader()) > 0)
			{
				switch (num4)
				{
				case 1:
					num = reader.ReadUInt64();
					break;
				case 2:
					num2 = reader.ReadUInt32();
					break;
				case 3:
					num3 = reader.ReadUInt32();
					break;
				default:
					reader.SkipField();
					break;
				}
			}
			ProtoReader.EndSubItem(token, reader);
			if (num == 0UL && num2 == 0U)
			{
				return 0m;
			}
			int lo = (int)(num & (ulong)-1);
			int mid = (int)(num >> 32 & (ulong)-1);
			int hi = (int)num2;
			bool isNegative = (num3 & 1U) == 1U;
			byte scale = (byte)((num3 & 510U) >> 1);
			return new decimal(lo, mid, hi, isNegative, scale);
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x00153BC4 File Offset: 0x00151FC4
		public static void WriteDecimal(decimal value, ProtoWriter writer)
		{
			int[] bits = decimal.GetBits(value);
			ulong num = (ulong)((ulong)((long)bits[1]) << 32);
			ulong num2 = (ulong)((long)bits[0] & (long)((ulong)-1));
			ulong num3 = num | num2;
			uint num4 = (uint)bits[2];
			uint num5 = (uint)((bits[3] >> 15 & 510) | (bits[3] >> 31 & 1));
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			if (num3 != 0UL)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Variant, writer);
				ProtoWriter.WriteUInt64(num3, writer);
			}
			if (num4 != 0U)
			{
				ProtoWriter.WriteFieldHeader(2, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num4, writer);
			}
			if (num5 != 0U)
			{
				ProtoWriter.WriteFieldHeader(3, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num5, writer);
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x00153C60 File Offset: 0x00152060
		public static void WriteGuid(Guid value, ProtoWriter dest)
		{
			byte[] data = value.ToByteArray();
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != Guid.Empty)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 0, 8, dest);
				ProtoWriter.WriteFieldHeader(2, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 8, 8, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x00153CB8 File Offset: 0x001520B8
		public static Guid ReadGuid(ProtoReader source)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				if (num3 != 1)
				{
					if (num3 != 2)
					{
						source.SkipField();
					}
					else
					{
						num2 = source.ReadUInt64();
					}
				}
				else
				{
					num = source.ReadUInt64();
				}
			}
			ProtoReader.EndSubItem(token, source);
			if (num == 0UL && num2 == 0UL)
			{
				return Guid.Empty;
			}
			uint num4 = (uint)(num >> 32);
			uint a = (uint)num;
			uint num5 = (uint)(num2 >> 32);
			uint num6 = (uint)num2;
			return new Guid((int)a, (short)num4, (short)(num4 >> 16), (byte)num6, (byte)(num6 >> 8), (byte)(num6 >> 16), (byte)(num6 >> 24), (byte)num5, (byte)(num5 >> 8), (byte)(num5 >> 16), (byte)(num5 >> 24));
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x00153D88 File Offset: 0x00152188
		public static object ReadNetObject(object value, ProtoReader source, int key, Type type, BclHelpers.NetObjectOptions options)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num = -1;
			int num2 = -1;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				switch (num3)
				{
				case 1:
				{
					int key2 = source.ReadInt32();
					value = source.NetCache.GetKeyedObject(key2);
					continue;
				}
				case 2:
					num = source.ReadInt32();
					continue;
				case 3:
				{
					int key2 = source.ReadInt32();
					type = (Type)source.NetCache.GetKeyedObject(key2);
					key = source.GetTypeKey(ref type);
					continue;
				}
				case 4:
					num2 = source.ReadInt32();
					continue;
				case 8:
				{
					string text = source.ReadString();
					type = source.DeserializeType(text);
					if (type == null)
					{
						throw new ProtoException("Unable to resolve type: " + text + " (you can use the TypeModel.DynamicTypeFormatting event to provide a custom mapping)");
					}
					if (type == typeof(string))
					{
						key = -1;
					}
					else
					{
						key = source.GetTypeKey(ref type);
						if (key < 0)
						{
							throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
						}
					}
					continue;
				}
				case 10:
				{
					bool flag = type == typeof(string);
					bool flag2 = value == null;
					bool flag3 = flag2 && (flag || (byte)(options & BclHelpers.NetObjectOptions.LateSet) != 0);
					if (num >= 0 && !flag3)
					{
						if (value == null)
						{
							source.TrapNextObject(num);
						}
						else
						{
							source.NetCache.SetKeyedObject(num, value);
						}
						if (num2 >= 0)
						{
							source.NetCache.SetKeyedObject(num2, type);
						}
					}
					object obj = value;
					if (flag)
					{
						value = source.ReadString();
					}
					else
					{
						value = ProtoReader.ReadTypedObject(obj, key, source, type);
					}
					if (num >= 0)
					{
						if (flag2 && !flag3)
						{
							obj = source.NetCache.GetKeyedObject(num);
						}
						if (flag3)
						{
							source.NetCache.SetKeyedObject(num, value);
							if (num2 >= 0)
							{
								source.NetCache.SetKeyedObject(num2, type);
							}
						}
					}
					if (num >= 0 && !flag3 && !object.ReferenceEquals(obj, value))
					{
						throw new ProtoException("A reference-tracked object changed reference during deserialization");
					}
					if (num < 0 && num2 >= 0)
					{
						source.NetCache.SetKeyedObject(num2, type);
					}
					continue;
				}
				}
				source.SkipField();
			}
			if (num >= 0 && (byte)(options & BclHelpers.NetObjectOptions.AsReference) == 0)
			{
				throw new ProtoException("Object key in input stream, but reference-tracking was not expected");
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x00154014 File Offset: 0x00152414
		public static void WriteNetObject(object value, ProtoWriter dest, int key, BclHelpers.NetObjectOptions options)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			bool flag = (byte)(options & BclHelpers.NetObjectOptions.DynamicType) != 0;
			bool flag2 = (byte)(options & BclHelpers.NetObjectOptions.AsReference) != 0;
			WireType wireType = dest.WireType;
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			bool flag3 = true;
			if (flag2)
			{
				bool flag4;
				int value2 = dest.NetCache.AddObjectKey(value, out flag4);
				ProtoWriter.WriteFieldHeader((!flag4) ? 2 : 1, WireType.Variant, dest);
				ProtoWriter.WriteInt32(value2, dest);
				if (flag4)
				{
					flag3 = false;
				}
			}
			if (flag3)
			{
				if (flag)
				{
					Type type = value.GetType();
					if (!(value is string))
					{
						key = dest.GetTypeKey(ref type);
						if (key < 0)
						{
							throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
						}
					}
					bool flag5;
					int value3 = dest.NetCache.AddObjectKey(type, out flag5);
					ProtoWriter.WriteFieldHeader((!flag5) ? 4 : 3, WireType.Variant, dest);
					ProtoWriter.WriteInt32(value3, dest);
					if (!flag5)
					{
						ProtoWriter.WriteFieldHeader(8, WireType.String, dest);
						ProtoWriter.WriteString(dest.SerializeType(type), dest);
					}
				}
				ProtoWriter.WriteFieldHeader(10, wireType, dest);
				if (value is string)
				{
					ProtoWriter.WriteString((string)value, dest);
				}
				else
				{
					ProtoWriter.WriteObject(value, key, dest);
				}
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x04002D56 RID: 11606
		private const int FieldTimeSpanValue = 1;

		// Token: 0x04002D57 RID: 11607
		private const int FieldTimeSpanScale = 2;

		// Token: 0x04002D58 RID: 11608
		internal static readonly DateTime EpochOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x04002D59 RID: 11609
		private const int FieldDecimalLow = 1;

		// Token: 0x04002D5A RID: 11610
		private const int FieldDecimalHigh = 2;

		// Token: 0x04002D5B RID: 11611
		private const int FieldDecimalSignScale = 3;

		// Token: 0x04002D5C RID: 11612
		private const int FieldGuidLow = 1;

		// Token: 0x04002D5D RID: 11613
		private const int FieldGuidHigh = 2;

		// Token: 0x04002D5E RID: 11614
		private const int FieldExistingObjectKey = 1;

		// Token: 0x04002D5F RID: 11615
		private const int FieldNewObjectKey = 2;

		// Token: 0x04002D60 RID: 11616
		private const int FieldExistingTypeKey = 3;

		// Token: 0x04002D61 RID: 11617
		private const int FieldNewTypeKey = 4;

		// Token: 0x04002D62 RID: 11618
		private const int FieldTypeName = 8;

		// Token: 0x04002D63 RID: 11619
		private const int FieldObject = 10;

		// Token: 0x02000640 RID: 1600
		[Flags]
		public enum NetObjectOptions : byte
		{
			// Token: 0x04002D65 RID: 11621
			None = 0,
			// Token: 0x04002D66 RID: 11622
			AsReference = 1,
			// Token: 0x04002D67 RID: 11623
			DynamicType = 2,
			// Token: 0x04002D68 RID: 11624
			UseConstructor = 4,
			// Token: 0x04002D69 RID: 11625
			LateSet = 8
		}
	}
}
