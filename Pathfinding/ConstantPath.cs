using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A7 RID: 167
	public class ConstantPath : Path
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00032720 File Offset: 0x00030B20
		public ConstantPath()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00032728 File Offset: 0x00030B28
		[Obsolete("Please use the Construct method instead")]
		public ConstantPath(Vector3 start, OnPathDelegate callbackDelegate)
		{
			throw new Exception("This constructor is obsolete, please use the Construct method instead");
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0003273A File Offset: 0x00030B3A
		[Obsolete("Please use the Construct method instead")]
		public ConstantPath(Vector3 start, int maxGScore, OnPathDelegate callbackDelegate)
		{
			throw new Exception("This constructor is obsolete, please use the Construct method instead");
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0003274C File Offset: 0x00030B4C
		public static ConstantPath Construct(Vector3 start, int maxGScore, OnPathDelegate callback = null)
		{
			ConstantPath path = PathPool<ConstantPath>.GetPath();
			path.Setup(start, maxGScore, callback);
			return path;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00032769 File Offset: 0x00030B69
		protected void Setup(Vector3 start, int maxGScore, OnPathDelegate callback)
		{
			this.callback = callback;
			this.startPoint = start;
			this.originalStartPoint = this.startPoint;
			this.endingCondition = new EndingConditionDistance(this, maxGScore);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00032792 File Offset: 0x00030B92
		public override void OnEnterPool()
		{
			base.OnEnterPool();
			if (this.allNodes != null)
			{
				ListPool<Node>.Release(this.allNodes);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000327B0 File Offset: 0x00030BB0
		protected override void Recycle()
		{
			PathPool<ConstantPath>.Recycle(this);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000327B8 File Offset: 0x00030BB8
		public override void Reset()
		{
			base.Reset();
			this.allNodes = ListPool<Node>.Claim();
			this.endingCondition = null;
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000327F8 File Offset: 0x00030BF8
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			this.startNode = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint).node;
			if (this.startNode == null)
			{
				base.Error();
				base.LogError("Could not find close node to the start point");
				return;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00032858 File Offset: 0x00030C58
		public override void Initialize()
		{
			NodeRun nodeRun = this.startNode.GetNodeRun(this.runData);
			nodeRun.pathID = this.pathID;
			nodeRun.parent = null;
			nodeRun.cost = 0U;
			nodeRun.g = this.startNode.penalty;
			this.startNode.UpdateH(Int3.zero, this.heuristic, this.heuristicScale, nodeRun);
			this.startNode.Open(this.runData, nodeRun, Int3.zero, this);
			this.searchedNodes++;
			this.allNodes.Add(this.startNode);
			if (this.runData.open.numberOfItems <= 1)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.runData.open.Remove();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0003292C File Offset: 0x00030D2C
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (!base.IsDone())
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				this.allNodes.Add(this.currentR.node);
				this.currentR.node.Open(this.runData, this.currentR, Int3.zero, this);
				if (this.runData.open.numberOfItems <= 1)
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
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
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.currentR);
			}
		}

		// Token: 0x04000456 RID: 1110
		public Node startNode;

		// Token: 0x04000457 RID: 1111
		public Vector3 startPoint;

		// Token: 0x04000458 RID: 1112
		public Vector3 originalStartPoint;

		// Token: 0x04000459 RID: 1113
		public List<Node> allNodes;

		// Token: 0x0400045A RID: 1114
		public PathEndingCondition endingCondition;
	}
}
