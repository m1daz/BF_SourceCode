using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000685 RID: 1669
	internal sealed class DefaultValueDecorator : ProtoDecoratorBase
	{
		// Token: 0x06003132 RID: 12594 RVA: 0x00160880 File Offset: 0x0015EC80
		public DefaultValueDecorator(TypeModel model, object defaultValue, IProtoSerializer tail) : base(tail)
		{
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type type = model.MapType(defaultValue.GetType());
			if (type != tail.ExpectedType)
			{
				throw new ArgumentException("Default value is of incorrect type", "defaultValue");
			}
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x001608D5 File Offset: 0x0015ECD5
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x001608E2 File Offset: 0x0015ECE2
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x001608EF File Offset: 0x0015ECEF
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x001608FC File Offset: 0x0015ECFC
		public override void Write(object value, ProtoWriter dest)
		{
			if (!object.Equals(value, this.defaultValue))
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0016091C File Offset: 0x0015ED1C
		public override object Read(object value, ProtoReader source)
		{
			return this.Tail.Read(value, source);
		}

		// Token: 0x04002E66 RID: 11878
		private readonly object defaultValue;
	}
}
