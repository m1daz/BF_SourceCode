using System;
using ProtoBuf;

// Token: 0x02000512 RID: 1298
[ProtoContract]
public class GGCommonPropTrigger
{
	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06002437 RID: 9271 RVA: 0x00112ECD File Offset: 0x001112CD
	// (set) Token: 0x06002438 RID: 9272 RVA: 0x00112ED5 File Offset: 0x001112D5
	[ProtoMember(1, IsRequired = true)]
	public byte HpRecover { get; set; }

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06002439 RID: 9273 RVA: 0x00112EDE File Offset: 0x001112DE
	// (set) Token: 0x0600243A RID: 9274 RVA: 0x00112EE6 File Offset: 0x001112E6
	[ProtoMember(2, IsRequired = true)]
	public byte AttackEnhance { get; set; }

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x0600243B RID: 9275 RVA: 0x00112EEF File Offset: 0x001112EF
	// (set) Token: 0x0600243C RID: 9276 RVA: 0x00112EF7 File Offset: 0x001112F7
	[ProtoMember(3, IsRequired = true)]
	public byte ArmorEnhance { get; set; }

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x0600243D RID: 9277 RVA: 0x00112F00 File Offset: 0x00111300
	// (set) Token: 0x0600243E RID: 9278 RVA: 0x00112F08 File Offset: 0x00111308
	[ProtoMember(4, IsRequired = true)]
	public byte SpeedEnhance { get; set; }

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x0600243F RID: 9279 RVA: 0x00112F11 File Offset: 0x00111311
	// (set) Token: 0x06002440 RID: 9280 RVA: 0x00112F19 File Offset: 0x00111319
	[ProtoMember(5, IsRequired = true)]
	public byte JumpEnhance { get; set; }
}
