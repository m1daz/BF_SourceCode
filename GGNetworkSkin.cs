using System;
using ProtoBuf;

// Token: 0x0200051B RID: 1307
[ProtoContract]
public class GGNetworkSkin
{
	// Token: 0x0400257D RID: 9597
	[ProtoMember(1, IsRequired = true)]
	public bool needTargetSkin;

	// Token: 0x0400257E RID: 9598
	[ProtoMember(2, IsRequired = true)]
	public int ownerID;

	// Token: 0x0400257F RID: 9599
	[ProtoMember(3, IsRequired = true)]
	public byte[] data;
}
