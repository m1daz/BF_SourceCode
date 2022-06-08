using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000693 RID: 1683
	internal sealed class MemberSpecifiedDecorator : ProtoDecoratorBase
	{
		// Token: 0x0600318B RID: 12683 RVA: 0x00161A67 File Offset: 0x0015FE67
		public MemberSpecifiedDecorator(MethodInfo getSpecified, MethodInfo setSpecified, IProtoSerializer tail) : base(tail)
		{
			if (getSpecified == null && setSpecified == null)
			{
				throw new InvalidOperationException();
			}
			this.getSpecified = getSpecified;
			this.setSpecified = setSpecified;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x00161A90 File Offset: 0x0015FE90
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x00161A9D File Offset: 0x0015FE9D
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x00161AAA File Offset: 0x0015FEAA
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x00161AB7 File Offset: 0x0015FEB7
		public override void Write(object value, ProtoWriter dest)
		{
			if (this.getSpecified == null || (bool)this.getSpecified.Invoke(value, null))
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x00161AE8 File Offset: 0x0015FEE8
		public override object Read(object value, ProtoReader source)
		{
			object result = this.Tail.Read(value, source);
			if (this.setSpecified != null)
			{
				this.setSpecified.Invoke(value, new object[]
				{
					true
				});
			}
			return result;
		}

		// Token: 0x04002E85 RID: 11909
		private readonly MethodInfo getSpecified;

		// Token: 0x04002E86 RID: 11910
		private readonly MethodInfo setSpecified;
	}
}
