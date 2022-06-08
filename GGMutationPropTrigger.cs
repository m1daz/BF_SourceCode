using System;
using ProtoBuf;

// Token: 0x02000513 RID: 1299
[ProtoContract]
public class GGMutationPropTrigger
{
	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06002442 RID: 9282 RVA: 0x00112F2A File Offset: 0x0011132A
	// (set) Token: 0x06002443 RID: 9283 RVA: 0x00112F32 File Offset: 0x00111332
	[ProtoMember(1, IsRequired = true)]
	public byte BurstBullet { get; set; }

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06002444 RID: 9284 RVA: 0x00112F3B File Offset: 0x0011133B
	// (set) Token: 0x06002445 RID: 9285 RVA: 0x00112F43 File Offset: 0x00111343
	[ProtoMember(2, IsRequired = true)]
	public byte DamageImmune { get; set; }

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06002446 RID: 9286 RVA: 0x00112F4C File Offset: 0x0011134C
	// (set) Token: 0x06002447 RID: 9287 RVA: 0x00112F54 File Offset: 0x00111354
	[ProtoMember(3, IsRequired = true)]
	public byte Antivenom { get; set; }

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06002448 RID: 9288 RVA: 0x00112F5D File Offset: 0x0011135D
	// (set) Token: 0x06002449 RID: 9289 RVA: 0x00112F65 File Offset: 0x00111365
	[ProtoMember(4, IsRequired = true)]
	public byte SpeedTrap { get; set; }

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x0600244A RID: 9290 RVA: 0x00112F6E File Offset: 0x0011136E
	// (set) Token: 0x0600244B RID: 9291 RVA: 0x00112F76 File Offset: 0x00111376
	[ProtoMember(5, IsRequired = true)]
	public byte InvisiblePotion { get; set; }
}
