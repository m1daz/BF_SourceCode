using System;

namespace ProtoBuf
{
	// Token: 0x02000676 RID: 1654
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ProtoPartialMemberAttribute : ProtoMemberAttribute
	{
		// Token: 0x0600305D RID: 12381 RVA: 0x0015CD48 File Offset: 0x0015B148
		public ProtoPartialMemberAttribute(int tag, string memberName) : base(tag)
		{
			if (Helpers.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			this.memberName = memberName;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x0015CD6E File Offset: 0x0015B16E
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x04002E2B RID: 11819
		private readonly string memberName;
	}
}
