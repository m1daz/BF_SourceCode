using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class NavmeshClamp : MonoBehaviour
{
	// Token: 0x06000330 RID: 816 RVA: 0x00018C40 File Offset: 0x00017040
	private void LateUpdate()
	{
		if (this.prevNode == null)
		{
			this.prevNode = AstarPath.active.GetNearest(base.transform.position).node;
			this.prevPos = base.transform.position;
		}
		if (this.prevNode == null)
		{
			return;
		}
		if (this.prevNode != null)
		{
			IRaycastableGraph raycastableGraph = AstarData.GetGraph(this.prevNode) as IRaycastableGraph;
			if (raycastableGraph != null)
			{
				GraphHitInfo graphHitInfo;
				if (raycastableGraph.Linecast(this.prevPos, base.transform.position, this.prevNode, out graphHitInfo))
				{
					graphHitInfo.point.y = base.transform.position.y;
					Vector3 vector = Mathfx.NearestPoint(graphHitInfo.tangentOrigin, graphHitInfo.tangentOrigin + graphHitInfo.tangent, base.transform.position);
					if (raycastableGraph.Linecast(graphHitInfo.point, vector, graphHitInfo.node, out graphHitInfo))
					{
						graphHitInfo.point.y = base.transform.position.y;
						base.transform.position = graphHitInfo.point;
					}
					else
					{
						vector.y = base.transform.position.y;
						base.transform.position = vector;
					}
				}
				this.prevNode = graphHitInfo.node;
			}
		}
		this.prevPos = base.transform.position;
	}

	// Token: 0x0400029F RID: 671
	private Node prevNode;

	// Token: 0x040002A0 RID: 672
	private Vector3 prevPos;
}
