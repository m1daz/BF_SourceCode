using System;
using ProtoBuf;

// Token: 0x020004F4 RID: 1268
[ProtoContract]
public class GGNetworkKillMessage
{
	// Token: 0x04002484 RID: 9348
	[ProtoMember(1, IsRequired = true)]
	public bool displayed;

	// Token: 0x04002485 RID: 9349
	[ProtoMember(2, IsRequired = true)]
	public GGTeamType killerTeam;

	// Token: 0x04002486 RID: 9350
	[ProtoMember(3, IsRequired = true)]
	public string killer;

	// Token: 0x04002487 RID: 9351
	[ProtoMember(4, IsRequired = true)]
	public int gun;

	// Token: 0x04002488 RID: 9352
	[ProtoMember(5, IsRequired = true)]
	public GGTeamType theDeadTeam;

	// Token: 0x04002489 RID: 9353
	[ProtoMember(6, IsRequired = true)]
	public string theDead;

	// Token: 0x0400248A RID: 9354
	[ProtoMember(7, IsRequired = true)]
	public bool headShot;
}
