using System;
using System.Collections.Generic;
using ProtoBuf;

// Token: 0x02000517 RID: 1303
[ProtoContract]
public class GGMessageContent
{
	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06002480 RID: 9344 RVA: 0x00113137 File Offset: 0x00111537
	// (set) Token: 0x06002481 RID: 9345 RVA: 0x0011313F File Offset: 0x0011153F
	[ProtoMember(1, IsRequired = true)]
	public int ID { get; set; }

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06002482 RID: 9346 RVA: 0x00113148 File Offset: 0x00111548
	// (set) Token: 0x06002483 RID: 9347 RVA: 0x00113150 File Offset: 0x00111550
	[ProtoMember(2, IsRequired = true)]
	public int Damage { get; set; }

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06002484 RID: 9348 RVA: 0x00113159 File Offset: 0x00111559
	// (set) Token: 0x06002485 RID: 9349 RVA: 0x00113161 File Offset: 0x00111561
	[ProtoMember(3, IsRequired = true)]
	public GGTeamType Team { get; set; }

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06002486 RID: 9350 RVA: 0x0011316A File Offset: 0x0011156A
	// (set) Token: 0x06002487 RID: 9351 RVA: 0x00113172 File Offset: 0x00111572
	[ProtoMember(4, IsRequired = true)]
	public int strongholdID { get; set; }

	// Token: 0x0400256B RID: 9579
	[ProtoMember(5, IsRequired = true)]
	public List<GGNetworkPlayerProperties> blueRankInfoList;

	// Token: 0x0400256C RID: 9580
	[ProtoMember(6, IsRequired = true)]
	public List<GGNetworkPlayerProperties> redRankInfoList;

	// Token: 0x0400256D RID: 9581
	[ProtoMember(7, IsRequired = true)]
	public byte[] data;

	// Token: 0x0400256E RID: 9582
	[ProtoMember(8, IsRequired = true)]
	public int ZombieLv;

	// Token: 0x0400256F RID: 9583
	[ProtoMember(9, IsRequired = true)]
	public int ID2;

	// Token: 0x04002570 RID: 9584
	[ProtoMember(10, IsRequired = true)]
	public float X;

	// Token: 0x04002571 RID: 9585
	[ProtoMember(11, IsRequired = true)]
	public float Y;

	// Token: 0x04002572 RID: 9586
	[ProtoMember(12, IsRequired = true)]
	public float Z;

	// Token: 0x04002573 RID: 9587
	[ProtoMember(13, IsRequired = true)]
	public int TimerBombPositionID;

	// Token: 0x04002574 RID: 9588
	[ProtoMember(14, IsRequired = true)]
	public bool ActiveTimerBomb;
}
