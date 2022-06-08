using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AA RID: 170
	public class FloodPath : Path
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x0003304D File Offset: 0x0003144D
		[Obsolete("Please use the Construct method instead")]
		public FloodPath(Vector3 start, OnPathDelegate callbackDelegate)
		{
			this.Setup(start, callbackDelegate);
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00033064 File Offset: 0x00031464
		public FloodPath()
		{
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0003306C File Offset: 0x0003146C
		public bool HasPathTo(Node node)
		{
			return this.parents != null && this.parents.ContainsKey(node);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00033088 File Offset: 0x00031488
		public Node GetParent(Node node)
		{
			return this.parents[node];
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00033098 File Offset: 0x00031498
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool<FloodPath>.GetPath();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000330B4 File Offset: 0x000314B4
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000330D2 File Offset: 0x000314D2
		public override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<Node, Node>();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00033102 File Offset: 0x00031502
		protected override void Recycle()
		{
			PathPool<FloodPath>.Recycle(this);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0003310C File Offset: 0x0003150C
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.originalStartPoint, this.nnConstraint);
			this.startPoint = nearest.clampedPosition;
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.Error();
				base.LogError("Couldn't find a close node to the start point");
				return;
			}
			if (!this.startNode.walkable)
			{
				base.Error();
				base.LogError("The node closest to the start point is not walkable");
				return;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0003319C File Offset: 0x0003159C
		public override void Initialize()
		{
			NodeRun nodeRun = this.startNode.GetNodeRun(this.runData);
			nodeRun.pathID = this.pathID;
			nodeRun.parent = null;
			nodeRun.cost = 0U;
			nodeRun.g = this.startNode.penalty;
			this.startNode.UpdateH(Int3.zero, Heuristic.None, 0f, nodeRun);
			this.startNode.Open(this.runData, nodeRun, Int3.zero, this);
			this.parents[this.startNode] = null;
			this.searchedNodes++;
			if (this.runData.open.numberOfItems <= 1)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.runData.open.Remove();
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0003326C File Offset: 0x0003166C
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (!base.IsDone())
			{
				this.searchedNodes++;
				this.currentR.node.Open(this.runData, this.currentR, Int3.zero, this);
				this.parents[this.currentR.node] = this.currentR.parent.node;
				if (this.runData.open.numberOfItems <= 1)
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
				this.currentR = this.runData.open.Remove();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
				}
				num++;
			}
		}

		// Token: 0x0400045C RID: 1116
		public Vector3 originalStartPoint;

		// Token: 0x0400045D RID: 1117
		public Vector3 startPoint;

		// Token: 0x0400045E RID: 1118
		public Node startNode;

		// Token: 0x0400045F RID: 1119
		protected Dictionary<Node, Node> parents;
	}
}
