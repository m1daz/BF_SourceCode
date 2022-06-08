using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000677 RID: 1655
	public sealed class ProtoReader : IDisposable
	{
		// Token: 0x0600305F RID: 12383 RVA: 0x0015CD76 File Offset: 0x0015B176
		public ProtoReader(Stream source, TypeModel model, SerializationContext context)
		{
			ProtoReader.Init(this, source, model, context, -1);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x0015CD88 File Offset: 0x0015B188
		public ProtoReader(Stream source, TypeModel model, SerializationContext context, int length)
		{
			ProtoReader.Init(this, source, model, context, length);
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x0015CD9B File Offset: 0x0015B19B
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x0015CDA3 File Offset: 0x0015B1A3
		public WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x0015CDAB File Offset: 0x0015B1AB
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x0015CDB3 File Offset: 0x0015B1B3
		public bool InternStrings
		{
			get
			{
				return this.internStrings;
			}
			set
			{
				this.internStrings = value;
			}
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x0015CDBC File Offset: 0x0015B1BC
		private static void Init(ProtoReader reader, Stream source, TypeModel model, SerializationContext context, int length)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!source.CanRead)
			{
				throw new ArgumentException("Cannot read from stream", "source");
			}
			reader.source = source;
			reader.ioBuffer = BufferPool.GetBuffer();
			reader.model = model;
			bool flag = length >= 0;
			reader.isFixedLength = flag;
			reader.dataRemaining = ((!flag) ? 0 : length);
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			reader.context = context;
			reader.position = (reader.available = (reader.depth = (reader.fieldNumber = (reader.ioIndex = 0))));
			reader.blockEnd = int.MaxValue;
			reader.internStrings = true;
			reader.wireType = WireType.None;
			reader.trapCount = 1U;
			if (reader.netCache == null)
			{
				reader.netCache = new NetObjectCache();
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x0015CEB2 File Offset: 0x0015B2B2
		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0015CEBC File Offset: 0x0015B2BC
		public void Dispose()
		{
			this.source = null;
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
			if (this.stringInterner != null)
			{
				this.stringInterner.Clear();
			}
			if (this.netCache != null)
			{
				this.netCache.Clear();
			}
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x0015CF10 File Offset: 0x0015B310
		internal int TryReadUInt32VariantWithoutMoving(bool trimNegative, out uint value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0U;
				return 0;
			}
			int num = this.ioIndex;
			value = (uint)this.ioBuffer[num++];
			if ((value & 128U) == 0U)
			{
				return 1;
			}
			value &= 127U;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			uint num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127U) << 7;
			if ((num2 & 128U) == 0U)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127U) << 14;
			if ((num2 & 128U) == 0U)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127U) << 21;
			if ((num2 & 128U) == 0U)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num];
			value |= num2 << 28;
			if ((num2 & 240U) == 0U)
			{
				return 5;
			}
			if (trimNegative && (num2 & 240U) == 240U && this.available >= 10 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[num + 1] == 1)
			{
				return 10;
			}
			throw ProtoReader.AddErrorData(new OverflowException(), this);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x0015D0EC File Offset: 0x0015B4EC
		private uint ReadUInt32Variant(bool trimNegative)
		{
			uint result;
			int num = this.TryReadUInt32VariantWithoutMoving(trimNegative, out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0015D13C File Offset: 0x0015B53C
		private bool TryReadUInt32Variant(out uint value)
		{
			int num = this.TryReadUInt32VariantWithoutMoving(false, out value);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return true;
			}
			return false;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0015D188 File Offset: 0x0015B588
		public uint ReadUInt32()
		{
			switch (this.wireType)
			{
			case WireType.Variant:
				return this.ReadUInt32Variant(false);
			case WireType.Fixed64:
			{
				ulong num = this.ReadUInt64();
				return checked((uint)num);
			}
			case WireType.Fixed32:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.position += 4;
				this.available -= 4;
				return (uint)((int)this.ioBuffer[this.ioIndex++] | (int)this.ioBuffer[this.ioIndex++] << 8 | (int)this.ioBuffer[this.ioIndex++] << 16 | (int)this.ioBuffer[this.ioIndex++] << 24);
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x0015D273 File Offset: 0x0015B673
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0015D27C File Offset: 0x0015B67C
		internal void Ensure(int count, bool strict)
		{
			if (count > this.ioBuffer.Length)
			{
				BufferPool.ResizeAndFlushLeft(ref this.ioBuffer, count, this.ioIndex, this.available);
				this.ioIndex = 0;
			}
			else if (this.ioIndex + count >= this.ioBuffer.Length)
			{
				Helpers.BlockCopy(this.ioBuffer, this.ioIndex, this.ioBuffer, 0, this.available);
				this.ioIndex = 0;
			}
			count -= this.available;
			int num = this.ioIndex + this.available;
			int num2 = this.ioBuffer.Length - num;
			if (this.isFixedLength && this.dataRemaining < num2)
			{
				num2 = this.dataRemaining;
			}
			int num3;
			while (count > 0 && num2 > 0 && (num3 = this.source.Read(this.ioBuffer, num, num2)) > 0)
			{
				this.available += num3;
				count -= num3;
				num2 -= num3;
				num += num3;
				if (this.isFixedLength)
				{
					this.dataRemaining -= num3;
				}
			}
			if (strict && count > 0)
			{
				throw ProtoReader.EoF(this);
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0015D3AC File Offset: 0x0015B7AC
		public short ReadInt16()
		{
			return checked((short)this.ReadInt32());
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0015D3B5 File Offset: 0x0015B7B5
		public ushort ReadUInt16()
		{
			return checked((ushort)this.ReadUInt32());
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0015D3BE File Offset: 0x0015B7BE
		public byte ReadByte()
		{
			return checked((byte)this.ReadUInt32());
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0015D3C7 File Offset: 0x0015B7C7
		public sbyte ReadSByte()
		{
			return checked((sbyte)this.ReadInt32());
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0015D3D0 File Offset: 0x0015B7D0
		public int ReadInt32()
		{
			WireType wireType = this.wireType;
			switch (wireType)
			{
			case WireType.Variant:
				return (int)this.ReadUInt32Variant(true);
			case WireType.Fixed64:
			{
				long num = this.ReadInt64();
				return checked((int)num);
			}
			default:
				if (wireType != WireType.SignedVariant)
				{
					throw this.CreateWireTypeException();
				}
				return ProtoReader.Zag(this.ReadUInt32Variant(true));
			case WireType.Fixed32:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.position += 4;
				this.available -= 4;
				return (int)this.ioBuffer[this.ioIndex++] | (int)this.ioBuffer[this.ioIndex++] << 8 | (int)this.ioBuffer[this.ioIndex++] << 16 | (int)this.ioBuffer[this.ioIndex++] << 24;
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0015D4D0 File Offset: 0x0015B8D0
		private static int Zag(uint ziggedValue)
		{
			return (int)(-(ziggedValue & 1U) ^ (uint)((int)ziggedValue >> 1 & int.MaxValue));
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0015D4F0 File Offset: 0x0015B8F0
		private static long Zag(ulong ziggedValue)
		{
			return (long)(-(long)(ziggedValue & 1UL) ^ (ziggedValue >> 1 & 9223372036854775807UL));
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x0015D514 File Offset: 0x0015B914
		public long ReadInt64()
		{
			WireType wireType = this.wireType;
			switch (wireType)
			{
			case WireType.Variant:
				return (long)this.ReadUInt64Variant();
			case WireType.Fixed64:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.position += 8;
				this.available -= 8;
				return (long)this.ioBuffer[this.ioIndex++] | (long)this.ioBuffer[this.ioIndex++] << 8 | (long)this.ioBuffer[this.ioIndex++] << 16 | (long)this.ioBuffer[this.ioIndex++] << 24 | (long)this.ioBuffer[this.ioIndex++] << 32 | (long)this.ioBuffer[this.ioIndex++] << 40 | (long)this.ioBuffer[this.ioIndex++] << 48 | (long)this.ioBuffer[this.ioIndex++] << 56;
			default:
				if (wireType != WireType.SignedVariant)
				{
					throw this.CreateWireTypeException();
				}
				return ProtoReader.Zag(this.ReadUInt64Variant());
			case WireType.Fixed32:
				return (long)this.ReadInt32();
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0015D688 File Offset: 0x0015BA88
		private int TryReadUInt64VariantWithoutMoving(out ulong value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0UL;
				return 0;
			}
			int num = this.ioIndex;
			value = (ulong)this.ioBuffer[num++];
			if ((value & 128UL) == 0UL)
			{
				return 1;
			}
			value &= 127UL;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			ulong num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 7;
			if ((num2 & 128UL) == 0UL)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 14;
			if ((num2 & 128UL) == 0UL)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 21;
			if ((num2 & 128UL) == 0UL)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 28;
			if ((num2 & 128UL) == 0UL)
			{
				return 5;
			}
			if (this.available == 5)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 35;
			if ((num2 & 128UL) == 0UL)
			{
				return 6;
			}
			if (this.available == 6)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 42;
			if ((num2 & 128UL) == 0UL)
			{
				return 7;
			}
			if (this.available == 7)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 49;
			if ((num2 & 128UL) == 0UL)
			{
				return 8;
			}
			if (this.available == 8)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 56;
			if ((num2 & 128UL) == 0UL)
			{
				return 9;
			}
			if (this.available == 9)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num];
			value |= num2 << 63;
			if ((num2 & 18446744073709551614UL) != 0UL)
			{
				throw ProtoReader.AddErrorData(new OverflowException(), this);
			}
			return 10;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0015D928 File Offset: 0x0015BD28
		private ulong ReadUInt64Variant()
		{
			ulong result;
			int num = this.TryReadUInt64VariantWithoutMoving(out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0015D978 File Offset: 0x0015BD78
		private string Intern(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return string.Empty;
			}
			string text;
			if (this.stringInterner == null)
			{
				this.stringInterner = new Dictionary<string, string>();
				this.stringInterner.Add(value, value);
			}
			else if (this.stringInterner.TryGetValue(value, out text))
			{
				value = text;
			}
			else
			{
				this.stringInterner.Add(value, value);
			}
			return value;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0015D9F0 File Offset: 0x0015BDF0
		public string ReadString()
		{
			if (this.wireType != WireType.String)
			{
				throw this.CreateWireTypeException();
			}
			int num = (int)this.ReadUInt32Variant(false);
			if (num == 0)
			{
				return string.Empty;
			}
			if (this.available < num)
			{
				this.Ensure(num, true);
			}
			string text = ProtoReader.encoding.GetString(this.ioBuffer, this.ioIndex, num);
			if (this.internStrings)
			{
				text = this.Intern(text);
			}
			this.available -= num;
			this.position += num;
			this.ioIndex += num;
			return text;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0015DA90 File Offset: 0x0015BE90
		public void ThrowEnumException(Type type, int value)
		{
			string str = (type != null) ? type.FullName : "<null>";
			throw ProtoReader.AddErrorData(new ProtoException("No " + str + " enum is mapped to the wire-value " + value.ToString()), this);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x0015DADC File Offset: 0x0015BEDC
		private Exception CreateWireTypeException()
		{
			return this.CreateException("Invalid wire-type; this usually means you have over-written a file without truncating or setting the length; see http://stackoverflow.com/q/2152978/23354");
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0015DAE9 File Offset: 0x0015BEE9
		private Exception CreateException(string message)
		{
			return ProtoReader.AddErrorData(new ProtoException(message), this);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x0015DAF8 File Offset: 0x0015BEF8
		public double ReadDouble()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Fixed32)
			{
				return (double)this.ReadSingle();
			}
			if (wireType != WireType.Fixed64)
			{
				throw this.CreateWireTypeException();
			}
			return (double)this.ReadInt64();
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0015DB38 File Offset: 0x0015BF38
		public static object ReadObject(object value, int key, ProtoReader reader)
		{
			return ProtoReader.ReadTypedObject(value, key, reader, null);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x0015DB44 File Offset: 0x0015BF44
		internal static object ReadTypedObject(object value, int key, ProtoReader reader, Type type)
		{
			if (reader.model == null)
			{
				throw ProtoReader.AddErrorData(new InvalidOperationException("Cannot deserialize sub-objects unless a model is provided"), reader);
			}
			SubItemToken token = ProtoReader.StartSubItem(reader);
			if (key >= 0)
			{
				value = reader.model.Deserialize(key, value, reader);
			}
			else if (type == null || !reader.model.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
			ProtoReader.EndSubItem(token, reader);
			return value;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x0015DBC4 File Offset: 0x0015BFC4
		public static void EndSubItem(SubItemToken token, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int value = token.value;
			WireType wireType = reader.wireType;
			if (wireType != WireType.EndGroup)
			{
				if (value < reader.position)
				{
					throw reader.CreateException("Sub-message not read entirely");
				}
				if (reader.blockEnd != reader.position && reader.blockEnd != 2147483647)
				{
					throw reader.CreateException("Sub-message not read correctly");
				}
				reader.blockEnd = value;
				reader.depth--;
			}
			else
			{
				if (value >= 0)
				{
					throw ProtoReader.AddErrorData(new ArgumentException("token"), reader);
				}
				if (-value != reader.fieldNumber)
				{
					throw reader.CreateException("Wrong group was ended");
				}
				reader.wireType = WireType.None;
				reader.depth--;
			}
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x0015DCA8 File Offset: 0x0015C0A8
		public static SubItemToken StartSubItem(ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType == WireType.StartGroup)
			{
				reader.wireType = WireType.None;
				reader.depth++;
				return new SubItemToken(-reader.fieldNumber);
			}
			if (wireType != WireType.String)
			{
				throw reader.CreateWireTypeException();
			}
			int num = (int)reader.ReadUInt32Variant(false);
			if (num < 0)
			{
				throw ProtoReader.AddErrorData(new InvalidOperationException(), reader);
			}
			int value = reader.blockEnd;
			reader.blockEnd = reader.position + num;
			reader.depth++;
			return new SubItemToken(value);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x0015DD50 File Offset: 0x0015C150
		public int ReadFieldHeader()
		{
			if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
			{
				return 0;
			}
			uint num;
			if (this.TryReadUInt32Variant(out num))
			{
				this.wireType = (WireType)(num & 7U);
				this.fieldNumber = (int)(num >> 3);
				if (this.fieldNumber < 1)
				{
					throw new ProtoException("Invalid field in source data: " + this.fieldNumber.ToString());
				}
			}
			else
			{
				this.wireType = WireType.None;
				this.fieldNumber = 0;
			}
			if (this.wireType != WireType.EndGroup)
			{
				return this.fieldNumber;
			}
			if (this.depth > 0)
			{
				return 0;
			}
			throw new ProtoException("Unexpected end-group in source data; this usually means the source data is corrupt");
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0015DE08 File Offset: 0x0015C208
		public bool TryReadFieldHeader(int field)
		{
			if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
			{
				return false;
			}
			uint num2;
			int num = this.TryReadUInt32VariantWithoutMoving(false, out num2);
			WireType wireType;
			if (num > 0 && (int)num2 >> 3 == field && (wireType = (WireType)(num2 & 7U)) != WireType.EndGroup)
			{
				this.wireType = wireType;
				this.fieldNumber = field;
				this.position += num;
				this.ioIndex += num;
				this.available -= num;
				return true;
			}
			return false;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x0015DE94 File Offset: 0x0015C294
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x0015DE9C File Offset: 0x0015C29C
		public void Hint(WireType wireType)
		{
			if (this.wireType != wireType)
			{
				if ((wireType & (WireType)7) == this.wireType)
				{
					this.wireType = wireType;
				}
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0015DEC4 File Offset: 0x0015C2C4
		public void Assert(WireType wireType)
		{
			if (this.wireType != wireType)
			{
				if ((wireType & (WireType)7) != this.wireType)
				{
					throw this.CreateWireTypeException();
				}
				this.wireType = wireType;
			}
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x0015DEF8 File Offset: 0x0015C2F8
		public void SkipField()
		{
			WireType wireType = this.wireType;
			switch (wireType + 1)
			{
			case WireType.Fixed64:
			case (WireType)9:
				this.ReadUInt64Variant();
				return;
			case WireType.String:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.available -= 8;
				this.ioIndex += 8;
				this.position += 8;
				return;
			case WireType.StartGroup:
			{
				int num = (int)this.ReadUInt32Variant(false);
				if (num <= this.available)
				{
					this.available -= num;
					this.ioIndex += num;
					this.position += num;
					return;
				}
				this.position += num;
				num -= this.available;
				this.ioIndex = (this.available = 0);
				if (this.isFixedLength)
				{
					if (num > this.dataRemaining)
					{
						throw ProtoReader.EoF(this);
					}
					this.dataRemaining -= num;
				}
				ProtoReader.Seek(this.source, num, this.ioBuffer);
				return;
			}
			case WireType.EndGroup:
			{
				int num2 = this.fieldNumber;
				this.depth++;
				while (this.ReadFieldHeader() > 0)
				{
					this.SkipField();
				}
				this.depth--;
				if (this.wireType == WireType.EndGroup && this.fieldNumber == num2)
				{
					this.wireType = WireType.None;
					return;
				}
				throw this.CreateWireTypeException();
			}
			case (WireType)6:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.available -= 4;
				this.ioIndex += 4;
				this.position += 4;
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x0015E0D4 File Offset: 0x0015C4D4
		public ulong ReadUInt64()
		{
			switch (this.wireType)
			{
			case WireType.Variant:
				return this.ReadUInt64Variant();
			case WireType.Fixed64:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.position += 8;
				this.available -= 8;
				return (ulong)this.ioBuffer[this.ioIndex++] | (ulong)this.ioBuffer[this.ioIndex++] << 8 | (ulong)this.ioBuffer[this.ioIndex++] << 16 | (ulong)this.ioBuffer[this.ioIndex++] << 24 | (ulong)this.ioBuffer[this.ioIndex++] << 32 | (ulong)this.ioBuffer[this.ioIndex++] << 40 | (ulong)this.ioBuffer[this.ioIndex++] << 48 | (ulong)this.ioBuffer[this.ioIndex++] << 56;
			case WireType.Fixed32:
				return (ulong)this.ReadUInt32();
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0015E234 File Offset: 0x0015C634
		public float ReadSingle()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Fixed32)
			{
				return (float)this.ReadInt32();
			}
			if (wireType != WireType.Fixed64)
			{
				throw this.CreateWireTypeException();
			}
			double num = this.ReadDouble();
			float num2 = (float)num;
			if (Helpers.IsInfinity(num2) && !Helpers.IsInfinity(num))
			{
				throw ProtoReader.AddErrorData(new OverflowException(), this);
			}
			return num2;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0015E29C File Offset: 0x0015C69C
		public bool ReadBoolean()
		{
			uint num = this.ReadUInt32();
			if (num == 0U)
			{
				return false;
			}
			if (num != 1U)
			{
				throw this.CreateException("Unexpected boolean value");
			}
			return true;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0015E2D4 File Offset: 0x0015C6D4
		public static byte[] AppendBytes(byte[] value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType != WireType.String)
			{
				throw reader.CreateWireTypeException();
			}
			int i = (int)reader.ReadUInt32Variant(false);
			reader.wireType = WireType.None;
			if (i == 0)
			{
				return (value != null) ? value : ProtoReader.EmptyBlob;
			}
			int num;
			if (value == null || value.Length == 0)
			{
				num = 0;
				value = new byte[i];
			}
			else
			{
				num = value.Length;
				byte[] array = new byte[value.Length + i];
				Helpers.BlockCopy(value, 0, array, 0, value.Length);
				value = array;
			}
			reader.position += i;
			while (i > reader.available)
			{
				if (reader.available > 0)
				{
					Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, reader.available);
					i -= reader.available;
					num += reader.available;
					reader.ioIndex = (reader.available = 0);
				}
				int num2 = (i <= reader.ioBuffer.Length) ? i : reader.ioBuffer.Length;
				if (num2 > 0)
				{
					reader.Ensure(num2, true);
				}
			}
			if (i > 0)
			{
				Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, i);
				reader.ioIndex += i;
				reader.available -= i;
			}
			return value;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0015E440 File Offset: 0x0015C840
		private static int ReadByteOrThrow(Stream source)
		{
			int num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			return num;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0015E464 File Offset: 0x0015C864
		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber)
		{
			int num;
			return ProtoReader.ReadLengthPrefix(source, expectHeader, style, out fieldNumber, out num);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0015E47C File Offset: 0x0015C87C
		public static int DirectReadLittleEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0015E4A1 File Offset: 0x0015C8A1
		public static int DirectReadBigEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0015E4C8 File Offset: 0x0015C8C8
		public static int DirectReadVarintInt32(Stream source)
		{
			uint result;
			int num = ProtoReader.TryReadUInt32Variant(source, out result);
			if (num <= 0)
			{
				throw ProtoReader.EoF(null);
			}
			return (int)result;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0015E4F0 File Offset: 0x0015C8F0
		public static void DirectReadBytes(Stream source, byte[] buffer, int offset, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			while (count > 0 && (num = source.Read(buffer, offset, count)) > 0)
			{
				count -= num;
				offset += num;
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0015E544 File Offset: 0x0015C944
		public static byte[] DirectReadBytes(Stream source, int count)
		{
			byte[] array = new byte[count];
			ProtoReader.DirectReadBytes(source, array, 0, count);
			return array;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0015E564 File Offset: 0x0015C964
		public static string DirectReadString(Stream source, int length)
		{
			byte[] array = new byte[length];
			ProtoReader.DirectReadBytes(source, array, 0, length);
			return Encoding.UTF8.GetString(array, 0, length);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0015E590 File Offset: 0x0015C990
		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber, out int bytesRead)
		{
			fieldNumber = 0;
			switch (style)
			{
			case PrefixStyle.None:
				bytesRead = 0;
				return int.MaxValue;
			case PrefixStyle.Base128:
			{
				bytesRead = 0;
				uint num2;
				int num;
				if (!expectHeader)
				{
					num = ProtoReader.TryReadUInt32Variant(source, out num2);
					bytesRead += num;
					return (int)((bytesRead >= 0) ? num2 : uint.MaxValue);
				}
				num = ProtoReader.TryReadUInt32Variant(source, out num2);
				bytesRead += num;
				if (num <= 0)
				{
					bytesRead = 0;
					return -1;
				}
				if ((num2 & 7U) != 2U)
				{
					throw new InvalidOperationException();
				}
				fieldNumber = (int)(num2 >> 3);
				num = ProtoReader.TryReadUInt32Variant(source, out num2);
				bytesRead += num;
				if (bytesRead == 0)
				{
					throw ProtoReader.EoF(null);
				}
				return (int)num2;
			}
			case PrefixStyle.Fixed32:
			{
				int num3 = source.ReadByte();
				if (num3 < 0)
				{
					bytesRead = 0;
					return -1;
				}
				bytesRead = 4;
				return num3 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num4 = source.ReadByte();
				if (num4 < 0)
				{
					bytesRead = 0;
					return -1;
				}
				bytesRead = 4;
				return num4 << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0015E6C0 File Offset: 0x0015CAC0
		private static int TryReadUInt32Variant(Stream source, out uint value)
		{
			value = 0U;
			int num = source.ReadByte();
			if (num < 0)
			{
				return 0;
			}
			value = (uint)num;
			if ((value & 128U) == 0U)
			{
				return 1;
			}
			value &= 127U;
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 7);
			if ((num & 128) == 0)
			{
				return 2;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 14);
			if ((num & 128) == 0)
			{
				return 3;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 21);
			if ((num & 128) == 0)
			{
				return 4;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)num << 28);
			if ((num & 240) == 0)
			{
				return 5;
			}
			throw new OverflowException();
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0015E7B8 File Offset: 0x0015CBB8
		internal static void Seek(Stream source, int count, byte[] buffer)
		{
			if (source.CanSeek)
			{
				source.Seek((long)count, SeekOrigin.Current);
				count = 0;
			}
			else if (buffer != null)
			{
				int num;
				while (count > buffer.Length && (num = source.Read(buffer, 0, buffer.Length)) > 0)
				{
					count -= num;
				}
				while (count > 0 && (num = source.Read(buffer, 0, count)) > 0)
				{
					count -= num;
				}
			}
			else
			{
				buffer = BufferPool.GetBuffer();
				try
				{
					int num2;
					while (count > buffer.Length && (num2 = source.Read(buffer, 0, buffer.Length)) > 0)
					{
						count -= num2;
					}
					while (count > 0 && (num2 = source.Read(buffer, 0, count)) > 0)
					{
						count -= num2;
					}
				}
				finally
				{
					BufferPool.ReleaseBufferToPool(ref buffer);
				}
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0015E8B0 File Offset: 0x0015CCB0
		internal static Exception AddErrorData(Exception exception, ProtoReader source)
		{
			if (exception != null && source != null && !exception.Data.Contains("protoSource"))
			{
				exception.Data.Add("protoSource", string.Format("tag={0}; wire-type={1}; offset={2}; depth={3}", new object[]
				{
					source.fieldNumber,
					source.wireType,
					source.position,
					source.depth
				}));
			}
			return exception;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0015E937 File Offset: 0x0015CD37
		private static Exception EoF(ProtoReader source)
		{
			return ProtoReader.AddErrorData(new EndOfStreamException(), source);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0015E944 File Offset: 0x0015CD44
		public void AppendExtensionData(IExtensible instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IExtension extensionObject = instance.GetExtensionObject(true);
			bool commit = false;
			Stream stream = extensionObject.BeginAppend();
			try
			{
				using (ProtoWriter protoWriter = new ProtoWriter(stream, this.model, null))
				{
					this.AppendExtensionField(protoWriter);
					protoWriter.Close();
				}
				commit = true;
			}
			finally
			{
				extensionObject.EndAppend(stream, commit);
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0015E9CC File Offset: 0x0015CDCC
		private void AppendExtensionField(ProtoWriter writer)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, writer);
			WireType wireType = this.wireType;
			switch (wireType + 1)
			{
			case WireType.Fixed64:
			case WireType.String:
			case (WireType)9:
				ProtoWriter.WriteInt64(this.ReadInt64(), writer);
				return;
			case WireType.StartGroup:
				ProtoWriter.WriteBytes(ProtoReader.AppendBytes(null, this), writer);
				return;
			case WireType.EndGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(this);
				SubItemToken token2 = ProtoWriter.StartSubItem(null, writer);
				while (this.ReadFieldHeader() > 0)
				{
					this.AppendExtensionField(writer);
				}
				ProtoReader.EndSubItem(token, this);
				ProtoWriter.EndSubItem(token2, writer);
				return;
			}
			case (WireType)6:
				ProtoWriter.WriteInt32(this.ReadInt32(), writer);
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0015EA8B File Offset: 0x0015CE8B
		public static bool HasSubValue(WireType wireType, ProtoReader source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.blockEnd <= source.position || wireType == WireType.EndGroup)
			{
				return false;
			}
			source.wireType = wireType;
			return true;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0015EAC0 File Offset: 0x0015CEC0
		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x0015EACE File Offset: 0x0015CECE
		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0015EAD6 File Offset: 0x0015CED6
		internal Type DeserializeType(string value)
		{
			return TypeModel.DeserializeType(this.model, value);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0015EAE4 File Offset: 0x0015CEE4
		internal void SetRootObject(object value)
		{
			this.netCache.SetKeyedObject(0, value);
			this.trapCount -= 1U;
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0015EB01 File Offset: 0x0015CF01
		public static void NoteObject(object value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.trapCount != 0U)
			{
				reader.netCache.RegisterTrappedObject(value);
				reader.trapCount -= 1U;
			}
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0015EB39 File Offset: 0x0015CF39
		public Type ReadType()
		{
			return TypeModel.DeserializeType(this.model, this.ReadString());
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0015EB4C File Offset: 0x0015CF4C
		internal void TrapNextObject(int newObjectKey)
		{
			this.trapCount += 1U;
			this.netCache.SetKeyedObject(newObjectKey, null);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0015EB69 File Offset: 0x0015CF69
		internal void CheckFullyConsumed()
		{
			if (this.isFixedLength)
			{
				if (this.dataRemaining != 0)
				{
					throw new ProtoException("Incorrect number of bytes consumed");
				}
			}
			else if (this.available != 0)
			{
				throw new ProtoException("Unconsumed data left in the buffer; this suggests corrupt input");
			}
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0015EBA8 File Offset: 0x0015CFA8
		public static object Merge(ProtoReader parent, object from, object to)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			TypeModel typeModel = parent.Model;
			SerializationContext serializationContext = parent.Context;
			if (typeModel == null)
			{
				throw new InvalidOperationException("Types cannot be merged unless a type-model has been specified");
			}
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				typeModel.Serialize(memoryStream, from, serializationContext);
				memoryStream.Position = 0L;
				result = typeModel.Deserialize(memoryStream, to, null);
			}
			return result;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0015EC2C File Offset: 0x0015D02C
		internal static ProtoReader Create(Stream source, TypeModel model, SerializationContext context, int len)
		{
			ProtoReader recycled = ProtoReader.GetRecycled();
			if (recycled == null)
			{
				return new ProtoReader(source, model, context, len);
			}
			ProtoReader.Init(recycled, source, model, context, len);
			return recycled;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0015EC5C File Offset: 0x0015D05C
		private static ProtoReader GetRecycled()
		{
			ProtoReader result = ProtoReader.lastReader;
			ProtoReader.lastReader = null;
			return result;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0015EC76 File Offset: 0x0015D076
		internal static void Recycle(ProtoReader reader)
		{
			if (reader != null)
			{
				reader.Dispose();
				ProtoReader.lastReader = reader;
			}
		}

		// Token: 0x04002E2C RID: 11820
		private Stream source;

		// Token: 0x04002E2D RID: 11821
		private byte[] ioBuffer;

		// Token: 0x04002E2E RID: 11822
		private TypeModel model;

		// Token: 0x04002E2F RID: 11823
		private int fieldNumber;

		// Token: 0x04002E30 RID: 11824
		private int depth;

		// Token: 0x04002E31 RID: 11825
		private int dataRemaining;

		// Token: 0x04002E32 RID: 11826
		private int ioIndex;

		// Token: 0x04002E33 RID: 11827
		private int position;

		// Token: 0x04002E34 RID: 11828
		private int available;

		// Token: 0x04002E35 RID: 11829
		private int blockEnd;

		// Token: 0x04002E36 RID: 11830
		private WireType wireType;

		// Token: 0x04002E37 RID: 11831
		private bool isFixedLength;

		// Token: 0x04002E38 RID: 11832
		private bool internStrings;

		// Token: 0x04002E39 RID: 11833
		private NetObjectCache netCache;

		// Token: 0x04002E3A RID: 11834
		private uint trapCount;

		// Token: 0x04002E3B RID: 11835
		internal const int TO_EOF = -1;

		// Token: 0x04002E3C RID: 11836
		private SerializationContext context;

		// Token: 0x04002E3D RID: 11837
		private const long Int64Msb = -9223372036854775808L;

		// Token: 0x04002E3E RID: 11838
		private const int Int32Msb = -2147483648;

		// Token: 0x04002E3F RID: 11839
		private Dictionary<string, string> stringInterner;

		// Token: 0x04002E40 RID: 11840
		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04002E41 RID: 11841
		private static readonly byte[] EmptyBlob = new byte[0];

		// Token: 0x04002E42 RID: 11842
		[ThreadStatic]
		private static ProtoReader lastReader;
	}
}
