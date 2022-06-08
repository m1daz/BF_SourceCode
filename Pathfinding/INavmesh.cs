using System;

namespace Pathfinding
{
	// Token: 0x02000072 RID: 114
	public interface INavmesh
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003B1 RID: 945
		// (set) Token: 0x060003B2 RID: 946
		Int3[] vertices { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060003B3 RID: 947
		// (set) Token: 0x060003B4 RID: 948
		BBTree bbTree { get; set; }
	}
}
