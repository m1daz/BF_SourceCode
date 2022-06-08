using System;

namespace Pathfinding
{
	// Token: 0x0200009C RID: 156
	[Flags]
	public enum ModifierData
	{
		// Token: 0x04000418 RID: 1048
		All = -1,
		// Token: 0x04000419 RID: 1049
		StrictNodePath = 1,
		// Token: 0x0400041A RID: 1050
		NodePath = 2,
		// Token: 0x0400041B RID: 1051
		StrictVectorPath = 4,
		// Token: 0x0400041C RID: 1052
		VectorPath = 8,
		// Token: 0x0400041D RID: 1053
		Original = 16,
		// Token: 0x0400041E RID: 1054
		None = 0,
		// Token: 0x0400041F RID: 1055
		Nodes = 3,
		// Token: 0x04000420 RID: 1056
		Vector = 12
	}
}
