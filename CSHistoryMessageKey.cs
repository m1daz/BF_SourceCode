using System;
using ProtoBuf;

// Token: 0x020004AB RID: 1195
[ProtoContract]
public struct CSHistoryMessageKey
{
	// Token: 0x04002282 RID: 8834
	[ProtoMember(1, IsRequired = true)]
	public string name1;

	// Token: 0x04002283 RID: 8835
	[ProtoMember(2, IsRequired = true)]
	public string name2;
}
