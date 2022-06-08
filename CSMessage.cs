using System;
using ProtoBuf;

// Token: 0x020004A9 RID: 1193
[ProtoContract]
public class CSMessage
{
	// Token: 0x0400227A RID: 8826
	[ProtoMember(1, IsRequired = true)]
	public string type;

	// Token: 0x0400227B RID: 8827
	[ProtoMember(2, IsRequired = true)]
	public string sender;

	// Token: 0x0400227C RID: 8828
	[ProtoMember(3, IsRequired = true)]
	public string receiver;

	// Token: 0x0400227D RID: 8829
	[ProtoMember(4, IsRequired = true)]
	public string content;
}
