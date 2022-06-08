using System;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000698 RID: 1688
	internal abstract class ProtoDecoratorBase : IProtoSerializer
	{
		// Token: 0x060031AE RID: 12718 RVA: 0x001603D0 File Offset: 0x0015E7D0
		protected ProtoDecoratorBase(IProtoSerializer tail)
		{
			this.Tail = tail;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060031AF RID: 12719
		public abstract Type ExpectedType { get; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060031B0 RID: 12720
		public abstract bool ReturnsValue { get; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060031B1 RID: 12721
		public abstract bool RequiresOldValue { get; }

		// Token: 0x060031B2 RID: 12722
		public abstract void Write(object value, ProtoWriter dest);

		// Token: 0x060031B3 RID: 12723
		public abstract object Read(object value, ProtoReader source);

		// Token: 0x04002E91 RID: 11921
		protected readonly IProtoSerializer Tail;
	}
}
