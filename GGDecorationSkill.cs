using System;
using ProtoBuf;

// Token: 0x02000515 RID: 1301
[ProtoContract]
public class GGDecorationSkill
{
	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06002456 RID: 9302 RVA: 0x00112FD3 File Offset: 0x001113D3
	// (set) Token: 0x06002457 RID: 9303 RVA: 0x00112FDB File Offset: 0x001113DB
	[ProtoMember(1, IsRequired = true)]
	public byte SelfInvisible { get; set; }
}
