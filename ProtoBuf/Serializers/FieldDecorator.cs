using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000689 RID: 1673
	internal sealed class FieldDecorator : ProtoDecoratorBase
	{
		// Token: 0x06003149 RID: 12617 RVA: 0x00160D1C File Offset: 0x0015F11C
		public FieldDecorator(Type forType, FieldInfo field, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.field = field;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x00160D33 File Offset: 0x0015F133
		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x00160D3B File Offset: 0x0015F13B
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x00160D3E File Offset: 0x0015F13E
		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00160D41 File Offset: 0x0015F141
		public override void Write(object value, ProtoWriter dest)
		{
			value = this.field.GetValue(value);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00160D64 File Offset: 0x0015F164
		public override object Read(object value, ProtoReader source)
		{
			object obj = this.Tail.Read((!this.Tail.RequiresOldValue) ? null : this.field.GetValue(value), source);
			if (obj != null)
			{
				this.field.SetValue(value, obj);
			}
			return null;
		}

		// Token: 0x04002E6D RID: 11885
		private readonly FieldInfo field;

		// Token: 0x04002E6E RID: 11886
		private readonly Type forType;
	}
}
