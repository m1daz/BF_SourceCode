using System;
using System.Collections.Generic;
using ProtoBuf;

// Token: 0x02000510 RID: 1296
[ProtoContract]
public class GGModeEventArgs
{
	// Token: 0x0400253F RID: 9535
	[ProtoMember(1, IsRequired = true)]
	public List<GGNetworkPlayerProperties> blueRankInfoList;

	// Token: 0x04002540 RID: 9536
	[ProtoMember(2, IsRequired = true)]
	public List<GGNetworkPlayerProperties> redRankInfoList;
}
