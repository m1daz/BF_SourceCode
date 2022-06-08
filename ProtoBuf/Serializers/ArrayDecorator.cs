using System;
using System.Collections;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200067E RID: 1662
	internal sealed class ArrayDecorator : ProtoDecoratorBase
	{
		// Token: 0x06003102 RID: 12546 RVA: 0x001603E0 File Offset: 0x0015E7E0
		public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull) : base(tail)
		{
			this.itemType = arrayType.GetElementType();
			Type type = (!supportNull) ? (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType) : this.itemType;
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			this.packedWireType = packedWireType;
			if (writePacked)
			{
				this.options |= 1;
			}
			if (overwriteList)
			{
				this.options |= 2;
			}
			if (supportNull)
			{
				this.options |= 4;
			}
			this.arrayType = arrayType;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x001604C7 File Offset: 0x0015E8C7
		public override Type ExpectedType
		{
			get
			{
				return this.arrayType;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x001604CF File Offset: 0x0015E8CF
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x001604D7 File Offset: 0x0015E8D7
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x001604DA File Offset: 0x0015E8DA
		private bool AppendToCollection
		{
			get
			{
				return (this.options & 2) == 0;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x001604E7 File Offset: 0x0015E8E7
		private bool SupportNull
		{
			get
			{
				return (this.options & 4) != 0;
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x001604F8 File Offset: 0x0015E8F8
		public override void Write(object value, ProtoWriter dest)
		{
			IList list = (IList)value;
			int count = list.Count;
			bool flag = (this.options & 1) != 0;
			SubItemToken token;
			if (flag)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag2 = !this.SupportNull;
			for (int i = 0; i < count; i++)
			{
				object obj = list[i];
				if (flag2 && obj == null)
				{
					throw new NullReferenceException();
				}
				this.Tail.Write(obj, dest);
			}
			if (flag)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x001605B0 File Offset: 0x0015E9B0
		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			BasicList basicList = new BasicList();
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					basicList.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			int num = (!this.AppendToCollection) ? 0 : ((value != null) ? ((Array)value).Length : 0);
			Array array = Array.CreateInstance(this.itemType, num + basicList.Count);
			if (num != 0)
			{
				((Array)value).CopyTo(array, 0);
			}
			basicList.CopyTo(array, num);
			return array;
		}

		// Token: 0x04002E57 RID: 11863
		private readonly int fieldNumber;

		// Token: 0x04002E58 RID: 11864
		private const byte OPTIONS_WritePacked = 1;

		// Token: 0x04002E59 RID: 11865
		private const byte OPTIONS_OverwriteList = 2;

		// Token: 0x04002E5A RID: 11866
		private const byte OPTIONS_SupportNull = 4;

		// Token: 0x04002E5B RID: 11867
		private readonly byte options;

		// Token: 0x04002E5C RID: 11868
		private readonly WireType packedWireType;

		// Token: 0x04002E5D RID: 11869
		private readonly Type arrayType;

		// Token: 0x04002E5E RID: 11870
		private readonly Type itemType;
	}
}
