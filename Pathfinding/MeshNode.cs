using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000075 RID: 117
	public class MeshNode : Node
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00023ABE File Offset: 0x00021EBE
		public int GetVertexIndex(int i)
		{
			if (i == 0)
			{
				return this.v1;
			}
			if (i == 1)
			{
				return this.v2;
			}
			if (i == 2)
			{
				return this.v3;
			}
			throw new ArgumentOutOfRangeException("A MeshNode only contains 3 vertices");
		}

		// Token: 0x1700005C RID: 92
		public int this[int i]
		{
			get
			{
				return this.GetVertexIndex(i);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00023AFC File Offset: 0x00021EFC
		public Vector3 ClosestPoint(Vector3 p, Int3[] vertices)
		{
			return Polygon.ClosestPointOnTriangle((Vector3)vertices[this.v1], (Vector3)vertices[this.v2], (Vector3)vertices[this.v3], p);
		}

		// Token: 0x0400031B RID: 795
		public int v1;

		// Token: 0x0400031C RID: 796
		public int v2;

		// Token: 0x0400031D RID: 797
		public int v3;
	}
}
