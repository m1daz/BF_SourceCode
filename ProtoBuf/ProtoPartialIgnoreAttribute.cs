using System;

namespace ProtoBuf
{
	// Token: 0x02000672 RID: 1650
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ProtoPartialIgnoreAttribute : ProtoIgnoreAttribute
	{
		// Token: 0x0600303C RID: 12348 RVA: 0x0015CA4C File Offset: 0x0015AE4C
		public ProtoPartialIgnoreAttribute(string memberName)
		{
			if (Helpers.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			this.memberName = memberName;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x0015CA71 File Offset: 0x0015AE71
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x04002E19 RID: 11801
		private readonly string memberName;
	}
}
