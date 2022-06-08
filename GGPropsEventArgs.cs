using System;
using ProtoBuf;

// Token: 0x02000511 RID: 1297
[ProtoContract]
public class GGPropsEventArgs
{
	// Token: 0x17000195 RID: 405
	// (get) Token: 0x0600242E RID: 9262 RVA: 0x00112E81 File Offset: 0x00111281
	// (set) Token: 0x0600242F RID: 9263 RVA: 0x00112E89 File Offset: 0x00111289
	[ProtoMember(1, IsRequired = true)]
	public string propsTag { get; set; }

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06002430 RID: 9264 RVA: 0x00112E92 File Offset: 0x00111292
	// (set) Token: 0x06002431 RID: 9265 RVA: 0x00112E9A File Offset: 0x0011129A
	[ProtoMember(2, IsRequired = true)]
	public int playerViewID { get; set; }

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06002432 RID: 9266 RVA: 0x00112EA3 File Offset: 0x001112A3
	// (set) Token: 0x06002433 RID: 9267 RVA: 0x00112EAB File Offset: 0x001112AB
	[ProtoMember(3, IsRequired = true)]
	public int propsViewID { get; set; }

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06002434 RID: 9268 RVA: 0x00112EB4 File Offset: 0x001112B4
	// (set) Token: 0x06002435 RID: 9269 RVA: 0x00112EBC File Offset: 0x001112BC
	[ProtoMember(4, IsRequired = true)]
	public byte positionIndex { get; set; }
}
