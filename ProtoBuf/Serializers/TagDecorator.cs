using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200069F RID: 1695
	internal sealed class TagDecorator : ProtoDecoratorBase, IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x060031E6 RID: 12774 RVA: 0x001624C0 File Offset: 0x001608C0
		public TagDecorator(int fieldNumber, WireType wireType, bool strict, IProtoSerializer tail) : base(tail)
		{
			this.fieldNumber = fieldNumber;
			this.wireType = wireType;
			this.strict = strict;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x001624E0 File Offset: 0x001608E0
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.HasCallbacks(callbackType);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x0016250C File Offset: 0x0016090C
		public bool CanCreateInstance()
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.CanCreateInstance();
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00162534 File Offset: 0x00160934
		public object CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.Tail).CreateInstance(source);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x00162548 File Offset: 0x00160948
		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x00162570 File Offset: 0x00160970
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x0016257D File Offset: 0x0016097D
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x0016258A File Offset: 0x0016098A
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x00162597 File Offset: 0x00160997
		private bool NeedsHint
		{
			get
			{
				return (this.wireType & (WireType)(-8)) != WireType.Variant;
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x001625A8 File Offset: 0x001609A8
		public override object Read(object value, ProtoReader source)
		{
			if (this.strict)
			{
				source.Assert(this.wireType);
			}
			else if (this.NeedsHint)
			{
				source.Hint(this.wireType);
			}
			return this.Tail.Read(value, source);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x001625F5 File Offset: 0x001609F5
		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, dest);
			this.Tail.Write(value, dest);
		}

		// Token: 0x04002E9F RID: 11935
		private readonly bool strict;

		// Token: 0x04002EA0 RID: 11936
		private readonly int fieldNumber;

		// Token: 0x04002EA1 RID: 11937
		private readonly WireType wireType;
	}
}
