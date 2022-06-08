using System;

namespace Pathfinding
{
	// Token: 0x0200000A RID: 10
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003B84 File Offset: 0x00001F84
		public new static PathNNConstraint Default
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003B9F File Offset: 0x00001F9F
		public virtual void SetStart(Node node)
		{
			if (node != null)
			{
				this.area = node.area;
			}
			else
			{
				this.constrainArea = false;
			}
		}
	}
}
