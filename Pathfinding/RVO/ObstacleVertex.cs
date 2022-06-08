using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000039 RID: 57
	public class ObstacleVertex
	{
		// Token: 0x040001CE RID: 462
		public bool convex;

		// Token: 0x040001CF RID: 463
		public Vector3 position;

		// Token: 0x040001D0 RID: 464
		public Vector2 dir;

		// Token: 0x040001D1 RID: 465
		public float height;

		// Token: 0x040001D2 RID: 466
		public bool split;

		// Token: 0x040001D3 RID: 467
		public bool thin;

		// Token: 0x040001D4 RID: 468
		public ObstacleVertex next;

		// Token: 0x040001D5 RID: 469
		public ObstacleVertex prev;
	}
}
