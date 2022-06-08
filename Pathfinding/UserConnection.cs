using System;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000006 RID: 6
	public class UserConnection
	{
		// Token: 0x0400003C RID: 60
		public Vector3 p1;

		// Token: 0x0400003D RID: 61
		public Vector3 p2;

		// Token: 0x0400003E RID: 62
		public ConnectionType type;

		// Token: 0x0400003F RID: 63
		[JsonName("doOverCost")]
		public bool doOverrideCost;

		// Token: 0x04000040 RID: 64
		[JsonName("overCost")]
		public int overrideCost;

		// Token: 0x04000041 RID: 65
		public bool oneWay;

		// Token: 0x04000042 RID: 66
		public bool enable = true;

		// Token: 0x04000043 RID: 67
		public float width;

		// Token: 0x04000044 RID: 68
		[JsonName("doOverWalkable")]
		public bool doOverrideWalkability = true;

		// Token: 0x04000045 RID: 69
		[JsonName("doOverCost")]
		public bool doOverridePenalty;

		// Token: 0x04000046 RID: 70
		[JsonName("overPenalty")]
		public uint overridePenalty;
	}
}
