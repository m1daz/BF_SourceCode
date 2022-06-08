using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AB RID: 171
	public class PathIDConstraint : NNConstraint
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00033342 File Offset: 0x00031742
		public void SetPath(Path path)
		{
			if (path == null)
			{
				Debug.LogWarning("PathIDConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0003335B File Offset: 0x0003175B
		public override bool Suitable(Node node)
		{
			return node.GetNodeRun(this.path.runData).pathID == this.path.pathID && base.Suitable(node);
		}

		// Token: 0x04000460 RID: 1120
		private Path path;
	}
}
