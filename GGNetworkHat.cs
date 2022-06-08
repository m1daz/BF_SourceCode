using System;
using ProtoBuf;

// Token: 0x0200051C RID: 1308
[ProtoContract]
public class GGNetworkHat
{
	// Token: 0x04002580 RID: 9600
	[ProtoMember(1, IsRequired = true)]
	public bool needTargetHat;

	// Token: 0x04002581 RID: 9601
	[ProtoMember(2, IsRequired = true)]
	public int ownerID;

	// Token: 0x04002582 RID: 9602
	[ProtoMember(3, IsRequired = true)]
	public byte[] data;
}
