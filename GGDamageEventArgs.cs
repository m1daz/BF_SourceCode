using System;
using ProtoBuf;

// Token: 0x0200050F RID: 1295
[ProtoContract]
public class GGDamageEventArgs
{
	// Token: 0x1700018D RID: 397
	// (get) Token: 0x0600241C RID: 9244 RVA: 0x00112DE9 File Offset: 0x001111E9
	// (set) Token: 0x0600241D RID: 9245 RVA: 0x00112DF1 File Offset: 0x001111F1
	[ProtoMember(1, IsRequired = true)]
	public short damage { get; set; }

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x0600241E RID: 9246 RVA: 0x00112DFA File Offset: 0x001111FA
	// (set) Token: 0x0600241F RID: 9247 RVA: 0x00112E02 File Offset: 0x00111202
	[ProtoMember(2, IsRequired = true)]
	public int id { get; set; }

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06002420 RID: 9248 RVA: 0x00112E0B File Offset: 0x0011120B
	// (set) Token: 0x06002421 RID: 9249 RVA: 0x00112E13 File Offset: 0x00111213
	[ProtoMember(3, IsRequired = true)]
	public string name { get; set; }

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06002422 RID: 9250 RVA: 0x00112E1C File Offset: 0x0011121C
	// (set) Token: 0x06002423 RID: 9251 RVA: 0x00112E24 File Offset: 0x00111224
	[ProtoMember(4, IsRequired = true)]
	public short weaponType { get; set; }

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06002424 RID: 9252 RVA: 0x00112E2D File Offset: 0x0011122D
	// (set) Token: 0x06002425 RID: 9253 RVA: 0x00112E35 File Offset: 0x00111235
	[ProtoMember(5, IsRequired = true)]
	public GGTeamType team { get; set; }

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06002426 RID: 9254 RVA: 0x00112E3E File Offset: 0x0011123E
	// (set) Token: 0x06002427 RID: 9255 RVA: 0x00112E46 File Offset: 0x00111246
	[ProtoMember(6, IsRequired = true)]
	public float shooterPositionX { get; set; }

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06002428 RID: 9256 RVA: 0x00112E4F File Offset: 0x0011124F
	// (set) Token: 0x06002429 RID: 9257 RVA: 0x00112E57 File Offset: 0x00111257
	[ProtoMember(7, IsRequired = true)]
	public float shooterPositionY { get; set; }

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x0600242A RID: 9258 RVA: 0x00112E60 File Offset: 0x00111260
	// (set) Token: 0x0600242B RID: 9259 RVA: 0x00112E68 File Offset: 0x00111268
	[ProtoMember(8, IsRequired = true)]
	public float shooterPositionZ { get; set; }
}
