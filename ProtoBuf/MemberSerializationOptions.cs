using System;

namespace ProtoBuf
{
	// Token: 0x02000675 RID: 1653
	[Flags]
	public enum MemberSerializationOptions
	{
		// Token: 0x04002E24 RID: 11812
		None = 0,
		// Token: 0x04002E25 RID: 11813
		Packed = 1,
		// Token: 0x04002E26 RID: 11814
		Required = 2,
		// Token: 0x04002E27 RID: 11815
		AsReference = 4,
		// Token: 0x04002E28 RID: 11816
		DynamicType = 8,
		// Token: 0x04002E29 RID: 11817
		OverwriteList = 16,
		// Token: 0x04002E2A RID: 11818
		AsReferenceHasValue = 32
	}
}
