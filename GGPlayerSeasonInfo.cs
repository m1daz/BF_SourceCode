using System;
using ProtoBuf;

// Token: 0x02000516 RID: 1302
[ProtoContract]
public class GGPlayerSeasonInfo
{
	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06002459 RID: 9305 RVA: 0x00112FEC File Offset: 0x001113EC
	// (set) Token: 0x0600245A RID: 9306 RVA: 0x00112FF4 File Offset: 0x001113F4
	[ProtoMember(1, IsRequired = true)]
	public int Score { get; set; }

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x0600245B RID: 9307 RVA: 0x00112FFD File Offset: 0x001113FD
	// (set) Token: 0x0600245C RID: 9308 RVA: 0x00113005 File Offset: 0x00111405
	[ProtoMember(2, IsRequired = true)]
	public int Rank { get; set; }

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x0600245D RID: 9309 RVA: 0x0011300E File Offset: 0x0011140E
	// (set) Token: 0x0600245E RID: 9310 RVA: 0x00113016 File Offset: 0x00111416
	[ProtoMember(3, IsRequired = true)]
	public int Killing { get; set; }

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x0600245F RID: 9311 RVA: 0x0011301F File Offset: 0x0011141F
	// (set) Token: 0x06002460 RID: 9312 RVA: 0x00113027 File Offset: 0x00111427
	[ProtoMember(4, IsRequired = true)]
	public int HeadShot { get; set; }

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06002461 RID: 9313 RVA: 0x00113030 File Offset: 0x00111430
	// (set) Token: 0x06002462 RID: 9314 RVA: 0x00113038 File Offset: 0x00111438
	[ProtoMember(5, IsRequired = true)]
	public int GodLike { get; set; }

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06002463 RID: 9315 RVA: 0x00113041 File Offset: 0x00111441
	// (set) Token: 0x06002464 RID: 9316 RVA: 0x00113049 File Offset: 0x00111449
	[ProtoMember(6, IsRequired = true)]
	public int KillingJoin { get; set; }

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06002465 RID: 9317 RVA: 0x00113052 File Offset: 0x00111452
	// (set) Token: 0x06002466 RID: 9318 RVA: 0x0011305A File Offset: 0x0011145A
	[ProtoMember(7, IsRequired = true)]
	public int KillingVictory { get; set; }

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06002467 RID: 9319 RVA: 0x00113063 File Offset: 0x00111463
	// (set) Token: 0x06002468 RID: 9320 RVA: 0x0011306B File Offset: 0x0011146B
	[ProtoMember(8, IsRequired = true)]
	public int StrongholdJoin { get; set; }

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06002469 RID: 9321 RVA: 0x00113074 File Offset: 0x00111474
	// (set) Token: 0x0600246A RID: 9322 RVA: 0x0011307C File Offset: 0x0011147C
	[ProtoMember(9, IsRequired = true)]
	public int StrongholdVictory { get; set; }

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x0600246B RID: 9323 RVA: 0x00113085 File Offset: 0x00111485
	// (set) Token: 0x0600246C RID: 9324 RVA: 0x0011308D File Offset: 0x0011148D
	[ProtoMember(10, IsRequired = true)]
	public int ExplositionJoin { get; set; }

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x0600246D RID: 9325 RVA: 0x00113096 File Offset: 0x00111496
	// (set) Token: 0x0600246E RID: 9326 RVA: 0x0011309E File Offset: 0x0011149E
	[ProtoMember(11, IsRequired = true)]
	public int ExplositionVictory { get; set; }

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x0600246F RID: 9327 RVA: 0x001130A7 File Offset: 0x001114A7
	// (set) Token: 0x06002470 RID: 9328 RVA: 0x001130AF File Offset: 0x001114AF
	[ProtoMember(12, IsRequired = true)]
	public int KillingVictoryRate { get; set; }

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06002471 RID: 9329 RVA: 0x001130B8 File Offset: 0x001114B8
	// (set) Token: 0x06002472 RID: 9330 RVA: 0x001130C0 File Offset: 0x001114C0
	[ProtoMember(13, IsRequired = true)]
	public int StrongholdVictoryRate { get; set; }

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06002473 RID: 9331 RVA: 0x001130C9 File Offset: 0x001114C9
	// (set) Token: 0x06002474 RID: 9332 RVA: 0x001130D1 File Offset: 0x001114D1
	[ProtoMember(14, IsRequired = true)]
	public int ExplositionVictoryRate { get; set; }

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06002475 RID: 9333 RVA: 0x001130DA File Offset: 0x001114DA
	// (set) Token: 0x06002476 RID: 9334 RVA: 0x001130E2 File Offset: 0x001114E2
	[ProtoMember(15, IsRequired = true)]
	public int KillingMVP { get; set; }

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06002477 RID: 9335 RVA: 0x001130EB File Offset: 0x001114EB
	// (set) Token: 0x06002478 RID: 9336 RVA: 0x001130F3 File Offset: 0x001114F3
	[ProtoMember(16, IsRequired = true)]
	public int StrongholdMVP { get; set; }

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06002479 RID: 9337 RVA: 0x001130FC File Offset: 0x001114FC
	// (set) Token: 0x0600247A RID: 9338 RVA: 0x00113104 File Offset: 0x00111504
	[ProtoMember(17, IsRequired = true)]
	public int ExplositionMVP { get; set; }

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x0600247B RID: 9339 RVA: 0x0011310D File Offset: 0x0011150D
	// (set) Token: 0x0600247C RID: 9340 RVA: 0x00113115 File Offset: 0x00111515
	[ProtoMember(18, IsRequired = true)]
	public string RoleName { get; set; }

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x0600247D RID: 9341 RVA: 0x0011311E File Offset: 0x0011151E
	// (set) Token: 0x0600247E RID: 9342 RVA: 0x00113126 File Offset: 0x00111526
	[ProtoMember(19, IsRequired = true)]
	public int CharacterLevel { get; set; }
}
