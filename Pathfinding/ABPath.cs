using System;
using System.Text;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A6 RID: 166
	public class ABPath : Path
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00031D9B File Offset: 0x0003019B
		[Obsolete("Use PathPool<T>.GetPath instead")]
		public ABPath(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			this.Reset();
			this.Setup(start, end, callbackDelegate);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00031DC0 File Offset: 0x000301C0
		public ABPath()
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00031DD8 File Offset: 0x000301D8
		public static ABPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			ABPath path = PathPool<ABPath>.GetPath();
			path.Setup(start, end, callback);
			return path;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00031DF5 File Offset: 0x000301F5
		protected void Setup(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			this.callback = callbackDelegate;
			this.UpdateStartEnd(start, end);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00031E06 File Offset: 0x00030206
		protected void UpdateStartEnd(Vector3 start, Vector3 end)
		{
			this.originalStartPoint = start;
			this.originalEndPoint = end;
			this.startPoint = start;
			this.endPoint = end;
			this.startIntPoint = (Int3)start;
			this.hTarget = (Int3)end;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00031E3C File Offset: 0x0003023C
		public override void Reset()
		{
			base.Reset();
			this.startNode = null;
			this.endNode = null;
			this.startHint = null;
			this.endHint = null;
			this.originalStartPoint = Vector3.zero;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.endPoint = Vector3.zero;
			this.calculatePartial = false;
			this.partialBestTarget = null;
			this.hasEndPoint = true;
			this.startIntPoint = default(Int3);
			this.hTarget = default(Int3);
			this.endNodeCosts = null;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00031ED4 File Offset: 0x000302D4
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint, this.startHint);
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.startPoint = nearest.clampedPosition;
			this.startIntPoint = (Int3)this.startPoint;
			this.startNode = nearest.node;
			if (this.hasEndPoint)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.endPoint, this.nnConstraint, this.endHint);
				this.endPoint = nearest2.clampedPosition;
				this.hTarget = (Int3)this.endPoint;
				this.endNode = nearest2.node;
			}
			if (this.startNode == null && this.hasEndPoint && this.endNode == null)
			{
				base.Error();
				base.LogError("Couldn't find close nodes to the start point or the end point");
				return;
			}
			if (this.startNode == null)
			{
				base.Error();
				base.LogError("Couldn't find a close node to the start point");
				return;
			}
			if (this.endNode == null && this.hasEndPoint)
			{
				base.Error();
				base.LogError("Couldn't find a close node to the end point");
				return;
			}
			if (!this.startNode.walkable)
			{
				base.Error();
				base.LogError("The node closest to the start point is not walkable");
				return;
			}
			if (this.hasEndPoint && !this.endNode.walkable)
			{
				base.Error();
				base.LogError("The node closest to the end point is not walkable");
				return;
			}
			if (this.hasEndPoint && this.startNode.area != this.endNode.area)
			{
				base.Error();
				base.LogError(string.Concat(new object[]
				{
					"There is no valid path to the target (start area: ",
					this.startNode.area,
					", target area: ",
					this.endNode.area,
					")"
				}));
				return;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000320F0 File Offset: 0x000304F0
		public override void Initialize()
		{
			if (this.hasEndPoint && this.startNode == this.endNode)
			{
				NodeRun nodeRun = this.endNode.GetNodeRun(this.runData);
				nodeRun.parent = null;
				nodeRun.h = 0U;
				nodeRun.g = 0U;
				this.Trace(nodeRun);
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			NodeRun nodeRun2 = this.startNode.GetNodeRun(this.runData);
			nodeRun2.pathID = this.pathID;
			nodeRun2.parent = null;
			nodeRun2.cost = 0U;
			nodeRun2.g = this.startNode.penalty;
			this.startNode.UpdateH(this.hTarget, this.heuristic, this.heuristicScale, nodeRun2);
			this.startNode.Open(this.runData, nodeRun2, this.hTarget, this);
			this.searchedNodes++;
			this.partialBestTarget = nodeRun2;
			if (this.runData.open.numberOfItems <= 1)
			{
				if (!this.calculatePartial)
				{
					base.Error();
					base.LogError("No open points, the start node didn't open any nodes");
					return;
				}
				base.CompleteState = PathCompleteState.Partial;
				this.Trace(this.partialBestTarget);
			}
			this.currentR = this.runData.open.Remove();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0003223C File Offset: 0x0003063C
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.currentR.node == this.endNode)
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				if (this.currentR.h < this.partialBestTarget.h)
				{
					this.partialBestTarget = this.currentR;
				}
				this.currentR.node.Open(this.runData, this.currentR, this.hTarget, this);
				if (this.runData.open.numberOfItems <= 1)
				{
					base.Error();
					base.LogError("No open points, whole area searched");
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
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.currentR);
			}
			else if (this.calculatePartial && this.partialBestTarget != null)
			{
				base.CompleteState = PathCompleteState.Partial;
				this.Trace(this.partialBestTarget);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0003237E File Offset: 0x0003077E
		public void ResetCosts(Path p)
		{
			if (!this.hasEndPoint)
			{
				return;
			}
			this.endNode.ResetCosts(this.endNodeCosts);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000323A0 File Offset: 0x000307A0
		public override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((!base.error) ? "Path Completed : " : "Path Failed : ");
			stringBuilder.Append("Computation Time ");
			stringBuilder.Append(this.duration.ToString((logMode != PathLog.Heavy) ? "0.00" : "0.000"));
			stringBuilder.Append(" ms Searched Nodes ");
			stringBuilder.Append(this.searchedNodes);
			if (!base.error)
			{
				stringBuilder.Append(" Path Length ");
				stringBuilder.Append((this.path != null) ? this.path.Count.ToString() : "Null");
				if (logMode == PathLog.Heavy)
				{
					stringBuilder.Append("\nSearch Iterations " + this.searchIterations);
					if (this.hasEndPoint && this.endNode != null)
					{
						NodeRun nodeRun = this.endNode.GetNodeRun(this.runData);
						stringBuilder.Append("\nEnd Node\n\tG: ");
						stringBuilder.Append(nodeRun.g);
						stringBuilder.Append("\n\tH: ");
						stringBuilder.Append(nodeRun.h);
						stringBuilder.Append("\n\tF: ");
						stringBuilder.Append(nodeRun.f);
						stringBuilder.Append("\n\tPoint: ");
						StringBuilder stringBuilder2 = stringBuilder;
						Vector3 vector = this.endPoint;
						stringBuilder2.Append(vector.ToString());
						stringBuilder.Append("\n\tGraph: ");
						stringBuilder.Append(this.endNode.graphIndex);
					}
					stringBuilder.Append("\nStart Node");
					stringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder3 = stringBuilder;
					Vector3 vector2 = this.startPoint;
					stringBuilder3.Append(vector2.ToString());
					stringBuilder.Append("\n\tGraph: ");
					stringBuilder.Append(this.startNode.graphIndex);
					stringBuilder.Append("\nBinary Heap size at completion: ");
					stringBuilder.Append((this.runData.open != null) ? (this.runData.open.numberOfItems - 2).ToString() : "Null");
				}
			}
			if (base.error)
			{
				stringBuilder.Append("\nError: ");
				stringBuilder.Append(base.errorLog);
			}
			stringBuilder.Append("\nPath Number ");
			stringBuilder.Append(this.pathID);
			return stringBuilder.ToString();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00032649 File Offset: 0x00030A49
		protected override void Recycle()
		{
			PathPool<ABPath>.Recycle(this);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00032654 File Offset: 0x00030A54
		public Vector3 GetMovementVector(Vector3 point)
		{
			if (this.vectorPath == null || this.vectorPath.Count == 0)
			{
				return Vector3.zero;
			}
			if (this.vectorPath.Count == 1)
			{
				return this.vectorPath[0] - point;
			}
			float num = float.PositiveInfinity;
			int num2 = 0;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				Vector3 a = Mathfx.NearestPointStrict(this.vectorPath[i], this.vectorPath[i + 1], point);
				float sqrMagnitude = (a - point).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					num2 = i;
				}
			}
			return this.vectorPath[num2 + 1] - point;
		}

		// Token: 0x04000447 RID: 1095
		public bool recalcStartEndCosts = true;

		// Token: 0x04000448 RID: 1096
		public Node startNode;

		// Token: 0x04000449 RID: 1097
		public Node endNode;

		// Token: 0x0400044A RID: 1098
		public Node startHint;

		// Token: 0x0400044B RID: 1099
		public Node endHint;

		// Token: 0x0400044C RID: 1100
		public Vector3 originalStartPoint;

		// Token: 0x0400044D RID: 1101
		public Vector3 originalEndPoint;

		// Token: 0x0400044E RID: 1102
		public Vector3 startPoint;

		// Token: 0x0400044F RID: 1103
		public Vector3 endPoint;

		// Token: 0x04000450 RID: 1104
		protected bool hasEndPoint = true;

		// Token: 0x04000451 RID: 1105
		public Int3 startIntPoint;

		// Token: 0x04000452 RID: 1106
		public Int3 hTarget;

		// Token: 0x04000453 RID: 1107
		public bool calculatePartial;

		// Token: 0x04000454 RID: 1108
		protected NodeRun partialBestTarget;

		// Token: 0x04000455 RID: 1109
		protected int[] endNodeCosts;
	}
}
