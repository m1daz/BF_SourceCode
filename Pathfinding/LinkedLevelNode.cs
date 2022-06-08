using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200006F RID: 111
	public class LinkedLevelNode
	{
		// Token: 0x04000301 RID: 769
		public Vector3 position;

		// Token: 0x04000302 RID: 770
		public bool walkable;

		// Token: 0x04000303 RID: 771
		public RaycastHit hit;

		// Token: 0x04000304 RID: 772
		public float height;

		// Token: 0x04000305 RID: 773
		public LinkedLevelNode next;
	}
}
