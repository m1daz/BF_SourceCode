using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000008 RID: 8
	public struct GraphHitInfo
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00003A13 File Offset: 0x00001E13
		public GraphHitInfo(Vector3 point)
		{
			this.success = false;
			this.tangentOrigin = Vector3.zero;
			this.origin = Vector3.zero;
			this.point = point;
			this.node = null;
			this.tangent = Vector3.zero;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003A4C File Offset: 0x00001E4C
		public float distance
		{
			get
			{
				return (this.point - this.origin).magnitude;
			}
		}

		// Token: 0x04000057 RID: 87
		public Vector3 origin;

		// Token: 0x04000058 RID: 88
		public Vector3 point;

		// Token: 0x04000059 RID: 89
		public Node node;

		// Token: 0x0400005A RID: 90
		public Vector3 tangentOrigin;

		// Token: 0x0400005B RID: 91
		public Vector3 tangent;

		// Token: 0x0400005C RID: 92
		public bool success;
	}
}
