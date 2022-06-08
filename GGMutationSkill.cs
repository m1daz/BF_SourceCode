using System;
using ProtoBuf;

// Token: 0x02000514 RID: 1300
[ProtoContract]
public class GGMutationSkill
{
	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x0600244D RID: 9293 RVA: 0x00112F87 File Offset: 0x00111387
	// (set) Token: 0x0600244E RID: 9294 RVA: 0x00112F8F File Offset: 0x0011138F
	[ProtoMember(1, IsRequired = true)]
	public byte SelfExplosion { get; set; }

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x0600244F RID: 9295 RVA: 0x00112F98 File Offset: 0x00111398
	// (set) Token: 0x06002450 RID: 9296 RVA: 0x00112FA0 File Offset: 0x001113A0
	[ProtoMember(2, IsRequired = true)]
	public byte PassiveHorror { get; set; }

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06002451 RID: 9297 RVA: 0x00112FA9 File Offset: 0x001113A9
	// (set) Token: 0x06002452 RID: 9298 RVA: 0x00112FB1 File Offset: 0x001113B1
	[ProtoMember(3, IsRequired = true)]
	public byte ActiveHorror { get; set; }

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06002453 RID: 9299 RVA: 0x00112FBA File Offset: 0x001113BA
	// (set) Token: 0x06002454 RID: 9300 RVA: 0x00112FC2 File Offset: 0x001113C2
	[ProtoMember(4, IsRequired = true)]
	public byte Blind { get; set; }
}
