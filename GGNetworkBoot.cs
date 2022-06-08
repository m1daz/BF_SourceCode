using System;
using ProtoBuf;

// Token: 0x0200051E RID: 1310
[ProtoContract]
public class GGNetworkBoot
{
	// Token: 0x04002586 RID: 9606
	[ProtoMember(1, IsRequired = true)]
	public bool needTargetBoot;

	// Token: 0x04002587 RID: 9607
	[ProtoMember(2, IsRequired = true)]
	public int ownerID;

	// Token: 0x04002588 RID: 9608
	[ProtoMember(3, IsRequired = true)]
	public byte[] data;
}
