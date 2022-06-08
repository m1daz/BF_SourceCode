using System;
using ProtoBuf;

// Token: 0x0200051F RID: 1311
[ProtoContract]
public class GGNetworkWeaponProperties
{
	// Token: 0x04002589 RID: 9609
	[ProtoMember(1, IsRequired = true)]
	public int weaponType;

	// Token: 0x0400258A RID: 9610
	[ProtoMember(2, IsRequired = true)]
	public bool isPlused;

	// Token: 0x0400258B RID: 9611
	[ProtoMember(3, IsRequired = true)]
	public bool dontNeedDelete;

	// Token: 0x0400258C RID: 9612
	[ProtoMember(4, IsRequired = true)]
	public int upgradeLv;
}
