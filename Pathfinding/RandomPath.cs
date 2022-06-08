using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B0 RID: 176
	public class RandomPath : ABPath
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x00032A80 File Offset: 0x00030E80
		public RandomPath()
		{
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00032A93 File Offset: 0x00030E93
		public RandomPath(Vector3 start, int length, OnPathDelegate callback = null)
		{
			throw new Exception("This constructor is obsolete. Please use the pooling API and the Setup methods");
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00032AB0 File Offset: 0x00030EB0
		public override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.uniform = true;
			this.replaceChance = 0.1f;
			this.aimStrength = 0f;
			this.chosenNodeR = null;
			this.maxGScoreNodeR = null;
			this.maxGScore = 0;
			this.aim = Vector3.zero;
			this.hasEndPoint = false;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00032B1D File Offset: 0x00030F1D
		protected override void Recycle()
		{
			PathPool<RandomPath>.Recycle(this);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00032B28 File Offset: 0x00030F28
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool<RandomPath>.GetPath();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00032B48 File Offset: 0x00030F48
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			this.startIntPoint = (Int3)start;
			this.hasEndPoint = false;
			return this;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00032B9C File Offset: 0x00030F9C
		public override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.endNode = this.path[this.path.Count - 1];
				this.endPoint = (Vector3)this.endNode.position;
				this.originalEndPoint = this.endPoint;
				this.hTarget = this.endNode.position;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00032C30 File Offset: 0x00031030
		public override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint, this.startHint);
			this.startPoint = nearest.clampedPosition;
			this.endPoint = this.startPoint;
			this.startIntPoint = (Int3)this.startPoint;
			this.hTarget = (Int3)this.aim;
			this.startNode = nearest.node;
			this.endNode = this.startNode;
			if (this.startNode == null || this.endNode == null)
			{
				base.LogError("Couldn't find close nodes to the start point");
				base.Error();
				return;
			}
			if (!this.startNode.walkable)
			{
				base.LogError("The node closest to the start point is not walkable");
				base.Error();
				return;
			}
			this.heuristicScale = this.aimStrength;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00032D18 File Offset: 0x00031118
		public override void Initialize()
		{
			NodeRun nodeRun = this.startNode.GetNodeRun(this.runData);
			if (this.searchLength <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.Trace(nodeRun);
				return;
			}
			nodeRun.pathID = this.pathID;
			nodeRun.parent = null;
			nodeRun.cost = 0U;
			nodeRun.g = this.startNode.penalty;
			this.startNode.UpdateH(this.hTarget, this.heuristic, this.heuristicScale, nodeRun);
			this.startNode.Open(this.runData, nodeRun, this.hTarget, this);
			this.searchedNodes++;
			if (this.runData.open.numberOfItems <= 1)
			{
				base.LogError("No open points, the start node didn't open any nodes");
				return;
			}
			this.currentR = this.runData.open.Remove();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00032DFC File Offset: 0x000311FC
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (!base.IsDone())
			{
				this.searchedNodes++;
				if ((ulong)this.currentR.g >= (ulong)((long)this.searchLength))
				{
					if (this.chosenNodeR == null)
					{
						this.chosenNodeR = this.currentR;
					}
					else if (this.rnd.NextDouble() < (double)this.replaceChance)
					{
						this.chosenNodeR = this.currentR;
					}
					if ((ulong)this.currentR.g >= (ulong)((long)(this.searchLength + this.spread)))
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
				}
				else if ((ulong)this.currentR.g > (ulong)((long)this.maxGScore))
				{
					this.maxGScore = (int)this.currentR.g;
					this.maxGScoreNodeR = this.currentR;
				}
				this.currentR.node.Open(this.runData, this.currentR, this.hTarget, this);
				if (this.runData.open.numberOfItems <= 1)
				{
					if (this.chosenNodeR != null)
					{
						base.CompleteState = PathCompleteState.Complete;
					}
					else if (this.maxGScoreNodeR != null)
					{
						this.chosenNodeR = this.maxGScoreNodeR;
						base.CompleteState = PathCompleteState.Complete;
					}
					else
					{
						base.LogError("Not a single node found to search");
						base.Error();
					}
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
				this.Trace(this.chosenNodeR);
			}
		}

		// Token: 0x04000478 RID: 1144
		public int searchLength;

		// Token: 0x04000479 RID: 1145
		public int spread;

		// Token: 0x0400047A RID: 1146
		public bool uniform;

		// Token: 0x0400047B RID: 1147
		public float replaceChance;

		// Token: 0x0400047C RID: 1148
		public float aimStrength;

		// Token: 0x0400047D RID: 1149
		private NodeRun chosenNodeR;

		// Token: 0x0400047E RID: 1150
		private NodeRun maxGScoreNodeR;

		// Token: 0x0400047F RID: 1151
		private int maxGScore;

		// Token: 0x04000480 RID: 1152
		public Vector3 aim;

		// Token: 0x04000481 RID: 1153
		private System.Random rnd = new System.Random();
	}
}
