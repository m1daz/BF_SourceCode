using System;
using System.IO;
using System.Text;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000678 RID: 1656
	public sealed class ProtoWriter : IDisposable
	{
		// Token: 0x060030A9 RID: 12457 RVA: 0x0015ECA4 File Offset: 0x0015D0A4
		public ProtoWriter(Stream dest, TypeModel model, SerializationContext context)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (!dest.CanWrite)
			{
				throw new ArgumentException("Cannot write to stream", "dest");
			}
			this.dest = dest;
			this.ioBuffer = BufferPool.GetBuffer();
			this.model = model;
			this.wireType = WireType.None;
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			this.context = context;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0015ED30 File Offset: 0x0015D130
		public static void WriteObject(object value, int key, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			SubItemToken token = ProtoWriter.StartSubItem(value, writer);
			if (key >= 0)
			{
				writer.model.Serialize(key, value, writer);
			}
			else if (writer.model == null || !writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false))
			{
				TypeModel.ThrowUnexpectedType(value.GetType());
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0015EDC4 File Offset: 0x0015D1C4
		public static void WriteRecursionSafeObject(object value, int key, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			writer.model.Serialize(key, value, writer);
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0015EE18 File Offset: 0x0015D218
		internal static void WriteObject(object value, int key, ProtoWriter writer, PrefixStyle style, int fieldNumber)
		{
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			switch (style)
			{
			case PrefixStyle.Base128:
				writer.wireType = WireType.String;
				writer.fieldNumber = fieldNumber;
				if (fieldNumber > 0)
				{
					ProtoWriter.WriteHeaderCore(fieldNumber, WireType.String, writer);
				}
				break;
			case PrefixStyle.Fixed32:
			case PrefixStyle.Fixed32BigEndian:
				writer.fieldNumber = 0;
				writer.wireType = WireType.Fixed32;
				break;
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			SubItemToken token = ProtoWriter.StartSubItem(value, writer, true);
			if (key < 0)
			{
				if (!writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false))
				{
					TypeModel.ThrowUnexpectedType(value.GetType());
				}
			}
			else
			{
				writer.model.Serialize(key, value, writer);
			}
			ProtoWriter.EndSubItem(token, writer, style);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0015EEFB File Offset: 0x0015D2FB
		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0015EF09 File Offset: 0x0015D309
		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x0015EF11 File Offset: 0x0015D311
		internal WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x0015EF1C File Offset: 0x0015D31C
		public static void WriteFieldHeader(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot write a ",
					wireType.ToString(),
					" header until the ",
					writer.wireType.ToString(),
					" data has been written"
				}));
			}
			if (fieldNumber < 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer.packedFieldNumber == 0)
			{
				writer.fieldNumber = fieldNumber;
				writer.wireType = wireType;
				ProtoWriter.WriteHeaderCore(fieldNumber, wireType, writer);
			}
			else
			{
				if (writer.packedFieldNumber != fieldNumber)
				{
					throw new InvalidOperationException("Field mismatch during packed encoding; expected " + writer.packedFieldNumber.ToString() + " but received " + fieldNumber.ToString());
				}
				switch (wireType)
				{
				case WireType.Variant:
				case WireType.Fixed64:
				case WireType.Fixed32:
					break;
				default:
					if (wireType != WireType.SignedVariant)
					{
						throw new InvalidOperationException("Wire-type cannot be encoded as packed: " + wireType.ToString());
					}
					break;
				}
				writer.fieldNumber = fieldNumber;
				writer.wireType = wireType;
			}
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0015F06C File Offset: 0x0015D46C
		internal static void WriteHeaderCore(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			uint value = (uint)(fieldNumber << 3 | (int)(wireType & (WireType)7));
			ProtoWriter.WriteUInt32Variant(value, writer);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0015F088 File Offset: 0x0015D488
		public static void WriteBytes(byte[] data, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			ProtoWriter.WriteBytes(data, 0, data.Length, writer);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0015F0A8 File Offset: 0x0015D4A8
		public static void WriteBytes(byte[] data, int offset, int length, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType != WireType.Fixed32)
			{
				if (wireType != WireType.Fixed64)
				{
					if (wireType != WireType.String)
					{
						throw ProtoWriter.CreateException(writer);
					}
					ProtoWriter.WriteUInt32Variant((uint)length, writer);
					writer.wireType = WireType.None;
					if (length == 0)
					{
						return;
					}
					if (writer.flushLock == 0 && length > writer.ioBuffer.Length)
					{
						ProtoWriter.Flush(writer);
						writer.dest.Write(data, offset, length);
						writer.position += length;
						return;
					}
				}
				else if (length != 8)
				{
					throw new ArgumentException("length");
				}
			}
			else if (length != 4)
			{
				throw new ArgumentException("length");
			}
			ProtoWriter.DemandSpace(length, writer);
			Helpers.BlockCopy(data, offset, writer.ioBuffer, writer.ioIndex, length);
			ProtoWriter.IncrementedAndReset(length, writer);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0015F1A8 File Offset: 0x0015D5A8
		private static void CopyRawFromStream(Stream source, ProtoWriter writer)
		{
			byte[] array = writer.ioBuffer;
			int num = array.Length - writer.ioIndex;
			int num2 = 1;
			while (num > 0 && (num2 = source.Read(array, writer.ioIndex, num)) > 0)
			{
				writer.ioIndex += num2;
				writer.position += num2;
				num -= num2;
			}
			if (num2 <= 0)
			{
				return;
			}
			if (writer.flushLock == 0)
			{
				ProtoWriter.Flush(writer);
				while ((num2 = source.Read(array, 0, array.Length)) > 0)
				{
					writer.dest.Write(array, 0, num2);
					writer.position += num2;
				}
			}
			else
			{
				for (;;)
				{
					ProtoWriter.DemandSpace(128, writer);
					if ((num2 = source.Read(writer.ioBuffer, writer.ioIndex, writer.ioBuffer.Length - writer.ioIndex)) <= 0)
					{
						break;
					}
					writer.position += num2;
					writer.ioIndex += num2;
				}
			}
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0015F2B7 File Offset: 0x0015D6B7
		private static void IncrementedAndReset(int length, ProtoWriter writer)
		{
			writer.ioIndex += length;
			writer.position += length;
			writer.wireType = WireType.None;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0015F2DC File Offset: 0x0015D6DC
		public static SubItemToken StartSubItem(object instance, ProtoWriter writer)
		{
			return ProtoWriter.StartSubItem(instance, writer, false);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0015F2E8 File Offset: 0x0015D6E8
		private void CheckRecursionStackAndPush(object instance)
		{
			int num;
			if (this.recursionStack == null)
			{
				this.recursionStack = new MutableList();
			}
			else if (instance != null && (num = this.recursionStack.IndexOfReference(instance)) >= 0)
			{
				throw new ProtoException("Possible recursion detected (offset: " + (this.recursionStack.Count - num).ToString() + " level(s)): " + instance.ToString());
			}
			this.recursionStack.Add(instance);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0015F36D File Offset: 0x0015D76D
		private void PopRecursionStack()
		{
			this.recursionStack.RemoveLast();
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0015F37C File Offset: 0x0015D77C
		private static SubItemToken StartSubItem(object instance, ProtoWriter writer, bool allowFixed)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (++writer.depth > 25)
			{
				writer.CheckRecursionStackAndPush(instance);
			}
			if (writer.packedFieldNumber != 0)
			{
				throw new InvalidOperationException("Cannot begin a sub-item while performing packed encoding");
			}
			switch (writer.wireType)
			{
			case WireType.String:
				writer.wireType = WireType.None;
				ProtoWriter.DemandSpace(32, writer);
				writer.flushLock++;
				writer.position++;
				return new SubItemToken(writer.ioIndex++);
			case WireType.StartGroup:
				writer.wireType = WireType.None;
				return new SubItemToken(-writer.fieldNumber);
			case WireType.Fixed32:
			{
				if (!allowFixed)
				{
					throw ProtoWriter.CreateException(writer);
				}
				ProtoWriter.DemandSpace(32, writer);
				writer.flushLock++;
				SubItemToken result = new SubItemToken(writer.ioIndex);
				ProtoWriter.IncrementedAndReset(4, writer);
				return result;
			}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0015F488 File Offset: 0x0015D888
		public static void EndSubItem(SubItemToken token, ProtoWriter writer)
		{
			ProtoWriter.EndSubItem(token, writer, PrefixStyle.Base128);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x0015F494 File Offset: 0x0015D894
		private static void EndSubItem(SubItemToken token, ProtoWriter writer, PrefixStyle style)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			int value = token.value;
			if (writer.depth <= 0)
			{
				throw ProtoWriter.CreateException(writer);
			}
			if (writer.depth-- > 25)
			{
				writer.PopRecursionStack();
			}
			writer.packedFieldNumber = 0;
			if (value < 0)
			{
				ProtoWriter.WriteHeaderCore(-value, WireType.EndGroup, writer);
				writer.wireType = WireType.None;
				return;
			}
			switch (style)
			{
			case PrefixStyle.Base128:
			{
				int num = writer.ioIndex - value - 1;
				int num2 = 0;
				uint num3 = (uint)num;
				while ((num3 >>= 7) != 0U)
				{
					num2++;
				}
				if (num2 == 0)
				{
					writer.ioBuffer[value] = (byte)(num & 127);
				}
				else
				{
					ProtoWriter.DemandSpace(num2, writer);
					byte[] array = writer.ioBuffer;
					Helpers.BlockCopy(array, value + 1, array, value + 1 + num2, num);
					num3 = (uint)num;
					do
					{
						array[value++] = (byte)((num3 & 127U) | 128U);
					}
					while ((num3 >>= 7) != 0U);
					array[value - 1] = (byte)((int)array[value - 1] & -129);
					writer.position += num2;
					writer.ioIndex += num2;
				}
				break;
			}
			case PrefixStyle.Fixed32:
			{
				int num = writer.ioIndex - value - 4;
				ProtoWriter.WriteInt32ToBuffer(num, writer.ioBuffer, value);
				break;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num = writer.ioIndex - value - 4;
				byte[] array2 = writer.ioBuffer;
				ProtoWriter.WriteInt32ToBuffer(num, array2, value);
				byte b = array2[value];
				array2[value] = array2[value + 3];
				array2[value + 3] = b;
				b = array2[value + 1];
				array2[value + 1] = array2[value + 2];
				array2[value + 2] = b;
				break;
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			if (--writer.flushLock == 0 && writer.ioIndex >= 1024)
			{
				ProtoWriter.Flush(writer);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0015F692 File Offset: 0x0015DA92
		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x0015F69A File Offset: 0x0015DA9A
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x0015F6A2 File Offset: 0x0015DAA2
		private void Dispose()
		{
			if (this.dest != null)
			{
				ProtoWriter.Flush(this);
				this.dest = null;
			}
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0015F6CE File Offset: 0x0015DACE
		internal static int GetPosition(ProtoWriter writer)
		{
			return writer.position;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0015F6D8 File Offset: 0x0015DAD8
		private static void DemandSpace(int required, ProtoWriter writer)
		{
			if (writer.ioBuffer.Length - writer.ioIndex < required)
			{
				if (writer.flushLock == 0)
				{
					ProtoWriter.Flush(writer);
					if (writer.ioBuffer.Length - writer.ioIndex >= required)
					{
						return;
					}
				}
				BufferPool.ResizeAndFlushLeft(ref writer.ioBuffer, required + writer.ioIndex, 0, writer.ioIndex);
			}
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0015F73B File Offset: 0x0015DB3B
		public void Close()
		{
			if (this.depth != 0 || this.flushLock != 0)
			{
				throw new InvalidOperationException("Unable to close stream in an incomplete state");
			}
			this.Dispose();
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0015F764 File Offset: 0x0015DB64
		internal void CheckDepthFlushlock()
		{
			if (this.depth != 0 || this.flushLock != 0)
			{
				throw new InvalidOperationException("The writer is in an incomplete state");
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0015F787 File Offset: 0x0015DB87
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0015F78F File Offset: 0x0015DB8F
		internal static void Flush(ProtoWriter writer)
		{
			if (writer.flushLock == 0 && writer.ioIndex != 0)
			{
				writer.dest.Write(writer.ioBuffer, 0, writer.ioIndex);
				writer.ioIndex = 0;
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0015F7C8 File Offset: 0x0015DBC8
		private static void WriteUInt32Variant(uint value, ProtoWriter writer)
		{
			ProtoWriter.DemandSpace(5, writer);
			int num = 0;
			do
			{
				writer.ioBuffer[writer.ioIndex++] = (byte)((value & 127U) | 128U);
				num++;
			}
			while ((value >>= 7) != 0U);
			byte[] array = writer.ioBuffer;
			int num2 = writer.ioIndex - 1;
			array[num2] &= 127;
			writer.position += num;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0015F838 File Offset: 0x0015DC38
		internal static uint Zig(int value)
		{
			return (uint)(value << 1 ^ value >> 31);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0015F842 File Offset: 0x0015DC42
		internal static ulong Zig(long value)
		{
			return (ulong)(value << 1 ^ value >> 63);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0015F84C File Offset: 0x0015DC4C
		private static void WriteUInt64Variant(ulong value, ProtoWriter writer)
		{
			ProtoWriter.DemandSpace(10, writer);
			int num = 0;
			do
			{
				writer.ioBuffer[writer.ioIndex++] = (byte)((value & 127UL) | 128UL);
				num++;
			}
			while ((value >>= 7) != 0UL);
			byte[] array = writer.ioBuffer;
			int num2 = writer.ioIndex - 1;
			array[num2] &= 127;
			writer.position += num;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0015F8C4 File Offset: 0x0015DCC4
		public static void WriteString(string value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.String)
			{
				throw ProtoWriter.CreateException(writer);
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				ProtoWriter.WriteUInt32Variant(0U, writer);
				writer.wireType = WireType.None;
				return;
			}
			int byteCount = ProtoWriter.encoding.GetByteCount(value);
			ProtoWriter.WriteUInt32Variant((uint)byteCount, writer);
			ProtoWriter.DemandSpace(byteCount, writer);
			int bytes = ProtoWriter.encoding.GetBytes(value, 0, value.Length, writer.ioBuffer, writer.ioIndex);
			ProtoWriter.IncrementedAndReset(bytes, writer);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0015F964 File Offset: 0x0015DD64
		public static void WriteUInt64(ulong value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Variant:
				ProtoWriter.WriteUInt64Variant(value, writer);
				writer.wireType = WireType.None;
				return;
			case WireType.Fixed64:
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			case WireType.Fixed32:
				ProtoWriter.WriteUInt32(checked((uint)value), writer);
				return;
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0015F9D4 File Offset: 0x0015DDD4
		public static void WriteInt64(long value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			switch (wireType)
			{
			case WireType.Variant:
				if (value >= 0L)
				{
					ProtoWriter.WriteUInt64Variant((ulong)value, writer);
					writer.wireType = WireType.None;
				}
				else
				{
					ProtoWriter.DemandSpace(10, writer);
					byte[] array = writer.ioBuffer;
					int num = writer.ioIndex;
					array[num] = (byte)(value | 128L);
					array[num + 1] = (byte)((int)(value >> 7) | 128);
					array[num + 2] = (byte)((int)(value >> 14) | 128);
					array[num + 3] = (byte)((int)(value >> 21) | 128);
					array[num + 4] = (byte)((int)(value >> 28) | 128);
					array[num + 5] = (byte)((int)(value >> 35) | 128);
					array[num + 6] = (byte)((int)(value >> 42) | 128);
					array[num + 7] = (byte)((int)(value >> 49) | 128);
					array[num + 8] = (byte)((int)(value >> 56) | 128);
					array[num + 9] = 1;
					ProtoWriter.IncrementedAndReset(10, writer);
				}
				return;
			case WireType.Fixed64:
			{
				ProtoWriter.DemandSpace(8, writer);
				byte[] array = writer.ioBuffer;
				int num = writer.ioIndex;
				array[num] = (byte)value;
				array[num + 1] = (byte)(value >> 8);
				array[num + 2] = (byte)(value >> 16);
				array[num + 3] = (byte)(value >> 24);
				array[num + 4] = (byte)(value >> 32);
				array[num + 5] = (byte)(value >> 40);
				array[num + 6] = (byte)(value >> 48);
				array[num + 7] = (byte)(value >> 56);
				ProtoWriter.IncrementedAndReset(8, writer);
				return;
			}
			default:
				if (wireType != WireType.SignedVariant)
				{
					throw ProtoWriter.CreateException(writer);
				}
				ProtoWriter.WriteUInt64Variant(ProtoWriter.Zig(value), writer);
				writer.wireType = WireType.None;
				return;
			case WireType.Fixed32:
				ProtoWriter.WriteInt32(checked((int)value), writer);
				return;
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0015FB84 File Offset: 0x0015DF84
		public static void WriteUInt32(uint value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Variant:
				ProtoWriter.WriteUInt32Variant(value, writer);
				writer.wireType = WireType.None;
				return;
			case WireType.Fixed64:
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			case WireType.Fixed32:
				ProtoWriter.WriteInt32((int)value, writer);
				return;
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0015FBF2 File Offset: 0x0015DFF2
		public static void WriteInt16(short value, ProtoWriter writer)
		{
			ProtoWriter.WriteInt32((int)value, writer);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0015FBFB File Offset: 0x0015DFFB
		public static void WriteUInt16(ushort value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((uint)value, writer);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0015FC04 File Offset: 0x0015E004
		public static void WriteByte(byte value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((uint)value, writer);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0015FC0D File Offset: 0x0015E00D
		public static void WriteSByte(sbyte value, ProtoWriter writer)
		{
			ProtoWriter.WriteInt32((int)value, writer);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0015FC17 File Offset: 0x0015E017
		private static void WriteInt32ToBuffer(int value, byte[] buffer, int index)
		{
			buffer[index] = (byte)value;
			buffer[index + 1] = (byte)(value >> 8);
			buffer[index + 2] = (byte)(value >> 16);
			buffer[index + 3] = (byte)(value >> 24);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0015FC3C File Offset: 0x0015E03C
		public static void WriteInt32(int value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			switch (wireType)
			{
			case WireType.Variant:
				if (value >= 0)
				{
					ProtoWriter.WriteUInt32Variant((uint)value, writer);
					writer.wireType = WireType.None;
				}
				else
				{
					ProtoWriter.DemandSpace(10, writer);
					byte[] array = writer.ioBuffer;
					int num = writer.ioIndex;
					array[num] = (byte)(value | 128);
					array[num + 1] = (byte)(value >> 7 | 128);
					array[num + 2] = (byte)(value >> 14 | 128);
					array[num + 3] = (byte)(value >> 21 | 128);
					array[num + 4] = (byte)(value >> 28 | 128);
					array[num + 5] = (array[num + 6] = (array[num + 7] = (array[num + 8] = byte.MaxValue)));
					array[num + 9] = 1;
					ProtoWriter.IncrementedAndReset(10, writer);
				}
				return;
			case WireType.Fixed64:
			{
				ProtoWriter.DemandSpace(8, writer);
				byte[] array = writer.ioBuffer;
				int num = writer.ioIndex;
				array[num] = (byte)value;
				array[num + 1] = (byte)(value >> 8);
				array[num + 2] = (byte)(value >> 16);
				array[num + 3] = (byte)(value >> 24);
				array[num + 4] = (array[num + 5] = (array[num + 6] = (array[num + 7] = 0)));
				ProtoWriter.IncrementedAndReset(8, writer);
				return;
			}
			default:
				if (wireType != WireType.SignedVariant)
				{
					throw ProtoWriter.CreateException(writer);
				}
				ProtoWriter.WriteUInt32Variant(ProtoWriter.Zig(value), writer);
				writer.wireType = WireType.None;
				return;
			case WireType.Fixed32:
				ProtoWriter.DemandSpace(4, writer);
				ProtoWriter.WriteInt32ToBuffer(value, writer.ioBuffer, writer.ioIndex);
				ProtoWriter.IncrementedAndReset(4, writer);
				return;
			}
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0015FDD0 File Offset: 0x0015E1D0
		public static void WriteDouble(double value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType != WireType.Fixed32)
			{
				if (wireType != WireType.Fixed64)
				{
					throw ProtoWriter.CreateException(writer);
				}
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			}
			else
			{
				float value2 = (float)value;
				if (Helpers.IsInfinity(value2) && !Helpers.IsInfinity(value))
				{
					throw new OverflowException();
				}
				ProtoWriter.WriteSingle(value2, writer);
				return;
			}
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0015FE40 File Offset: 0x0015E240
		public static void WriteSingle(float value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Fixed32)
			{
				ProtoWriter.WriteInt32((int)value, writer);
				return;
			}
			if (wireType != WireType.Fixed64)
			{
				throw ProtoWriter.CreateException(writer);
			}
			ProtoWriter.WriteDouble((double)value, writer);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0015FE94 File Offset: 0x0015E294
		public static void ThrowEnumException(ProtoWriter writer, object enumValue)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string str = (enumValue != null) ? (enumValue.GetType().FullName + "." + enumValue.ToString()) : "<null>";
			throw new ProtoException("No wire-value is mapped to the enum " + str + " at position " + writer.position.ToString());
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0015FF04 File Offset: 0x0015E304
		internal static Exception CreateException(ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			return new ProtoException("Invalid serialization operation with wire-type " + writer.wireType.ToString() + " at position " + writer.position.ToString());
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0015FF58 File Offset: 0x0015E358
		public static void WriteBoolean(bool value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((!value) ? 0U : 1U, writer);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0015FF70 File Offset: 0x0015E370
		public static void AppendExtensionData(IExtensible instance, ProtoWriter writer)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			IExtension extensionObject = instance.GetExtensionObject(false);
			if (extensionObject != null)
			{
				Stream stream = extensionObject.BeginQuery();
				try
				{
					ProtoWriter.CopyRawFromStream(stream, writer);
				}
				finally
				{
					extensionObject.EndQuery(stream);
				}
			}
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0015FFEC File Offset: 0x0015E3EC
		public static void SetPackedField(int fieldNumber, ProtoWriter writer)
		{
			if (fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.packedFieldNumber = fieldNumber;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00160018 File Offset: 0x0015E418
		internal string SerializeType(Type type)
		{
			return TypeModel.SerializeType(this.model, type);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x00160026 File Offset: 0x0015E426
		public void SetRootObject(object value)
		{
			this.NetCache.SetKeyedObject(0, value);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x00160035 File Offset: 0x0015E435
		public static void WriteType(Type value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ProtoWriter.WriteString(writer.SerializeType(value), writer);
		}

		// Token: 0x04002E43 RID: 11843
		private Stream dest;

		// Token: 0x04002E44 RID: 11844
		private TypeModel model;

		// Token: 0x04002E45 RID: 11845
		private readonly NetObjectCache netCache = new NetObjectCache();

		// Token: 0x04002E46 RID: 11846
		private int fieldNumber;

		// Token: 0x04002E47 RID: 11847
		private int flushLock;

		// Token: 0x04002E48 RID: 11848
		private WireType wireType;

		// Token: 0x04002E49 RID: 11849
		private int depth;

		// Token: 0x04002E4A RID: 11850
		private const int RecursionCheckDepth = 25;

		// Token: 0x04002E4B RID: 11851
		private MutableList recursionStack;

		// Token: 0x04002E4C RID: 11852
		private readonly SerializationContext context;

		// Token: 0x04002E4D RID: 11853
		private byte[] ioBuffer;

		// Token: 0x04002E4E RID: 11854
		private int ioIndex;

		// Token: 0x04002E4F RID: 11855
		private int position;

		// Token: 0x04002E50 RID: 11856
		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04002E51 RID: 11857
		private int packedFieldNumber;
	}
}
