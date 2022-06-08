using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AC RID: 172
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0003338D File Offset: 0x0003178D
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000333AC File Offset: 0x000317AC
		public override bool Suitable(Node node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x04000461 RID: 1121
		private FloodPath path;
	}
}
