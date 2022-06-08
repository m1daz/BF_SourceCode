using System;
using System.Collections.Generic;
using ProtoBuf;

// Token: 0x02000519 RID: 1305
[ProtoContract]
public class GGVector3List
{
	// Token: 0x0400257A RID: 9594
	[ProtoMember(1, IsRequired = true)]
	public List<GGVector3> vector3List;
}
