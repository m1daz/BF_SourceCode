using System;
using System.Collections.Generic;
using ProtoBuf;

// Token: 0x02000520 RID: 1312
[ProtoContract]
public class GGNetworkWeaponPropertiesList
{
	// Token: 0x0400258D RID: 9613
	[ProtoMember(1, IsRequired = true)]
	public bool needTargetWeaponProperties;

	// Token: 0x0400258E RID: 9614
	[ProtoMember(2, IsRequired = true)]
	public int ownerID;

	// Token: 0x0400258F RID: 9615
	[ProtoMember(3, IsRequired = true)]
	public List<GGNetworkWeaponProperties> weaponPropertiesList;
}
