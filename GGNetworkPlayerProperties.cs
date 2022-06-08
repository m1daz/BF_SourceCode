using System;
using ProtoBuf;

// Token: 0x02000527 RID: 1319
[ProtoContract]
public class GGNetworkPlayerProperties
{
	// Token: 0x06002531 RID: 9521 RVA: 0x00115CD8 File Offset: 0x001140D8
	public GGNetworkPlayerProperties()
	{
		this.killNum = 0;
		this.deadNum = 0;
		this.headshotNum = 0;
		this.maxKillNum = 0;
		this.damageNum = 0;
		this.bedamageNum = 0;
		this.strongholdGetNum = 0;
		this.installbombNum = 0;
		this.unInstallbombNum = 0;
		this.rating = GrowthGameRatingTag.RatingTag_Nil;
		this.CommonPropTrigger = new GGCommonPropTrigger();
		this.MutationPropTrigger = new GGMutationPropTrigger();
		this.MutationSkill = new GGMutationSkill();
		this.DecorationSkill = new GGDecorationSkill();
		this.participation = 0;
		this.StrongholdScore = 0;
		this.KillingCompetitionScore = 0;
		this.ExplosionScore = 0;
		this.KnifeCompetitionScore = 0;
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06002532 RID: 9522 RVA: 0x00115D80 File Offset: 0x00114180
	// (set) Token: 0x06002533 RID: 9523 RVA: 0x00115D88 File Offset: 0x00114188
	[ProtoMember(1, IsRequired = true)]
	public GGTeamType team { get; set; }

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06002534 RID: 9524 RVA: 0x00115D91 File Offset: 0x00114191
	// (set) Token: 0x06002535 RID: 9525 RVA: 0x00115D99 File Offset: 0x00114199
	[ProtoMember(2, IsRequired = true)]
	public string name { get; set; }

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06002536 RID: 9526 RVA: 0x00115DA2 File Offset: 0x001141A2
	// (set) Token: 0x06002537 RID: 9527 RVA: 0x00115DAA File Offset: 0x001141AA
	[ProtoMember(3, IsRequired = true)]
	public short killNum { get; set; }

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06002538 RID: 9528 RVA: 0x00115DB3 File Offset: 0x001141B3
	// (set) Token: 0x06002539 RID: 9529 RVA: 0x00115DBB File Offset: 0x001141BB
	[ProtoMember(4, IsRequired = true)]
	public short deadNum { get; set; }

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x0600253A RID: 9530 RVA: 0x00115DC4 File Offset: 0x001141C4
	// (set) Token: 0x0600253B RID: 9531 RVA: 0x00115DCC File Offset: 0x001141CC
	[ProtoMember(5, IsRequired = true)]
	public short headshotNum { get; set; }

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x0600253C RID: 9532 RVA: 0x00115DD5 File Offset: 0x001141D5
	// (set) Token: 0x0600253D RID: 9533 RVA: 0x00115DDD File Offset: 0x001141DD
	[ProtoMember(6, IsRequired = true)]
	public short maxKillNum { get; set; }

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x0600253E RID: 9534 RVA: 0x00115DE6 File Offset: 0x001141E6
	// (set) Token: 0x0600253F RID: 9535 RVA: 0x00115DEE File Offset: 0x001141EE
	[ProtoMember(7, IsRequired = true)]
	public short damageNum { get; set; }

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06002540 RID: 9536 RVA: 0x00115DF7 File Offset: 0x001141F7
	// (set) Token: 0x06002541 RID: 9537 RVA: 0x00115DFF File Offset: 0x001141FF
	[ProtoMember(8, IsRequired = true)]
	public short bedamageNum { get; set; }

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06002542 RID: 9538 RVA: 0x00115E08 File Offset: 0x00114208
	// (set) Token: 0x06002543 RID: 9539 RVA: 0x00115E10 File Offset: 0x00114210
	[ProtoMember(9, IsRequired = true)]
	public short strongholdGetNum { get; set; }

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06002544 RID: 9540 RVA: 0x00115E19 File Offset: 0x00114219
	// (set) Token: 0x06002545 RID: 9541 RVA: 0x00115E21 File Offset: 0x00114221
	[ProtoMember(10, IsRequired = true)]
	public short installbombNum { get; set; }

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06002546 RID: 9542 RVA: 0x00115E2A File Offset: 0x0011422A
	// (set) Token: 0x06002547 RID: 9543 RVA: 0x00115E32 File Offset: 0x00114232
	[ProtoMember(11, IsRequired = true)]
	public short unInstallbombNum { get; set; }

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06002548 RID: 9544 RVA: 0x00115E3B File Offset: 0x0011423B
	// (set) Token: 0x06002549 RID: 9545 RVA: 0x00115E43 File Offset: 0x00114243
	[ProtoMember(12, IsRequired = true)]
	public short rank { get; set; }

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x0600254A RID: 9546 RVA: 0x00115E4C File Offset: 0x0011424C
	// (set) Token: 0x0600254B RID: 9547 RVA: 0x00115E54 File Offset: 0x00114254
	[ProtoMember(13, IsRequired = true)]
	public short ping { get; set; }

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x0600254C RID: 9548 RVA: 0x00115E5D File Offset: 0x0011425D
	// (set) Token: 0x0600254D RID: 9549 RVA: 0x00115E65 File Offset: 0x00114265
	[ProtoMember(14, IsRequired = true)]
	public GrowthGameRatingTag rating { get; set; }

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x0600254E RID: 9550 RVA: 0x00115E6E File Offset: 0x0011426E
	// (set) Token: 0x0600254F RID: 9551 RVA: 0x00115E76 File Offset: 0x00114276
	[ProtoMember(15, IsRequired = true)]
	public int id { get; set; }

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06002550 RID: 9552 RVA: 0x00115E7F File Offset: 0x0011427F
	// (set) Token: 0x06002551 RID: 9553 RVA: 0x00115E87 File Offset: 0x00114287
	[ProtoMember(16, IsRequired = true)]
	public bool isWinner { get; set; }

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06002552 RID: 9554 RVA: 0x00115E90 File Offset: 0x00114290
	// (set) Token: 0x06002553 RID: 9555 RVA: 0x00115E98 File Offset: 0x00114298
	[ProtoMember(17, IsRequired = true)]
	public short coinAdd { get; set; }

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06002554 RID: 9556 RVA: 0x00115EA1 File Offset: 0x001142A1
	// (set) Token: 0x06002555 RID: 9557 RVA: 0x00115EA9 File Offset: 0x001142A9
	[ProtoMember(18, IsRequired = true)]
	public short expAdd { get; set; }

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06002556 RID: 9558 RVA: 0x00115EB2 File Offset: 0x001142B2
	// (set) Token: 0x06002557 RID: 9559 RVA: 0x00115EBA File Offset: 0x001142BA
	[ProtoMember(19, IsRequired = true)]
	public bool isDataValid { get; set; }

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06002558 RID: 9560 RVA: 0x00115EC3 File Offset: 0x001142C3
	// (set) Token: 0x06002559 RID: 9561 RVA: 0x00115ECB File Offset: 0x001142CB
	[ProtoMember(20, IsRequired = true)]
	public short MutationModeScore { get; set; }

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x0600255A RID: 9562 RVA: 0x00115ED4 File Offset: 0x001142D4
	// (set) Token: 0x0600255B RID: 9563 RVA: 0x00115EDC File Offset: 0x001142DC
	[ProtoMember(21, IsRequired = true)]
	public GGCommonPropTrigger CommonPropTrigger { get; set; }

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x0600255C RID: 9564 RVA: 0x00115EE5 File Offset: 0x001142E5
	// (set) Token: 0x0600255D RID: 9565 RVA: 0x00115EED File Offset: 0x001142ED
	[ProtoMember(22, IsRequired = true)]
	public GGMutationPropTrigger MutationPropTrigger { get; set; }

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x0600255E RID: 9566 RVA: 0x00115EF6 File Offset: 0x001142F6
	// (set) Token: 0x0600255F RID: 9567 RVA: 0x00115EFE File Offset: 0x001142FE
	[ProtoMember(23, IsRequired = true)]
	public GGMutationSkill MutationSkill { get; set; }

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06002560 RID: 9568 RVA: 0x00115F07 File Offset: 0x00114307
	// (set) Token: 0x06002561 RID: 9569 RVA: 0x00115F0F File Offset: 0x0011430F
	[ProtoMember(24, IsRequired = true)]
	public GGDecorationSkill DecorationSkill { get; set; }

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06002562 RID: 9570 RVA: 0x00115F18 File Offset: 0x00114318
	// (set) Token: 0x06002563 RID: 9571 RVA: 0x00115F20 File Offset: 0x00114320
	[ProtoMember(25, IsRequired = true)]
	public short participation { get; set; }

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06002564 RID: 9572 RVA: 0x00115F29 File Offset: 0x00114329
	// (set) Token: 0x06002565 RID: 9573 RVA: 0x00115F31 File Offset: 0x00114331
	[ProtoMember(26, IsRequired = true)]
	public short StrongholdScore { get; set; }

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06002566 RID: 9574 RVA: 0x00115F3A File Offset: 0x0011433A
	// (set) Token: 0x06002567 RID: 9575 RVA: 0x00115F42 File Offset: 0x00114342
	[ProtoMember(27, IsRequired = true)]
	public short KillingCompetitionScore { get; set; }

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06002568 RID: 9576 RVA: 0x00115F4B File Offset: 0x0011434B
	// (set) Token: 0x06002569 RID: 9577 RVA: 0x00115F53 File Offset: 0x00114353
	[ProtoMember(28, IsRequired = true)]
	public short ExplosionScore { get; set; }

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x0600256A RID: 9578 RVA: 0x00115F5C File Offset: 0x0011435C
	// (set) Token: 0x0600256B RID: 9579 RVA: 0x00115F64 File Offset: 0x00114364
	[ProtoMember(29, IsRequired = true)]
	public bool isObserver { get; set; }

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x0600256C RID: 9580 RVA: 0x00115F6D File Offset: 0x0011436D
	// (set) Token: 0x0600256D RID: 9581 RVA: 0x00115F75 File Offset: 0x00114375
	[ProtoMember(30, IsRequired = true)]
	public bool isTakeTimerBomb { get; set; }

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x0600256E RID: 9582 RVA: 0x00115F7E File Offset: 0x0011437E
	// (set) Token: 0x0600256F RID: 9583 RVA: 0x00115F86 File Offset: 0x00114386
	[ProtoMember(31, IsRequired = true)]
	public short honorPointAdd { get; set; }

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06002570 RID: 9584 RVA: 0x00115F8F File Offset: 0x0011438F
	// (set) Token: 0x06002571 RID: 9585 RVA: 0x00115F97 File Offset: 0x00114397
	[ProtoMember(32, IsRequired = true)]
	public short seasonScoreAdd { get; set; }

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06002572 RID: 9586 RVA: 0x00115FA0 File Offset: 0x001143A0
	// (set) Token: 0x06002573 RID: 9587 RVA: 0x00115FA8 File Offset: 0x001143A8
	[ProtoMember(33, IsRequired = true)]
	public bool DamageImmuneWhenRespawn { get; set; }

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06002574 RID: 9588 RVA: 0x00115FB1 File Offset: 0x001143B1
	// (set) Token: 0x06002575 RID: 9589 RVA: 0x00115FB9 File Offset: 0x001143B9
	[ProtoMember(34, IsRequired = true)]
	public short KnifeCompetitionScore { get; set; }
}
