using System;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AE RID: 174
	public class MultiTargetPath : ABPath
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00033549 File Offset: 0x00031949
		public MultiTargetPath()
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0003356D File Offset: 0x0003196D
		[Obsolete("Please use the Construct method instead")]
		public MultiTargetPath(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callbackDelegate = null) : this(target, startPoints, callbackDelegates, callbackDelegate)
		{
			this.inverted = true;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00033581 File Offset: 0x00031981
		[Obsolete("Please use the Construct method instead")]
		public MultiTargetPath(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callbackDelegate = null)
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000335A8 File Offset: 0x000319A8
		public static MultiTargetPath Construct(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(target, startPoints, callbackDelegates, callback);
			multiTargetPath.inverted = true;
			return multiTargetPath;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000335C8 File Offset: 0x000319C8
		public static MultiTargetPath Construct(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath path = PathPool<MultiTargetPath>.GetPath();
			path.Setup(start, targets, callbackDelegates, callback);
			return path;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000335E8 File Offset: 0x000319E8
		protected void Setup(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback)
		{
			this.inverted = false;
			this.callback = callback;
			this.callbacks = callbackDelegates;
			this.targetPoints = targets;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.startIntPoint = (Int3)start;
			if (targets.Length == 0)
			{
				base.Error();
				base.LogError("No targets were assigned to the MultiTargetPath");
				return;
			}
			this.endPoint = targets[0];
			this.originalTargetPoints = new Vector3[this.targetPoints.Length];
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				this.originalTargetPoints[i] = this.targetPoints[i];
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000336A6 File Offset: 0x00031AA6
		protected override void Recycle()
		{
			PathPool<MultiTargetPath>.Recycle(this);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000336B0 File Offset: 0x00031AB0
		public override void OnEnterPool()
		{
			if (this.vectorPaths != null)
			{
				for (int i = 0; i < this.vectorPaths.Length; i++)
				{
					if (this.vectorPaths[i] != null)
					{
						ListPool<Vector3>.Release(this.vectorPaths[i]);
					}
				}
			}
			this.vectorPaths = null;
			this.vectorPath = null;
			if (this.nodePaths != null)
			{
				for (int j = 0; j < this.nodePaths.Length; j++)
				{
					if (this.nodePaths[j] != null)
					{
						ListPool<Node>.Release(this.nodePaths[j]);
					}
				}
			}
			this.nodePaths = null;
			this.path = null;
			base.OnEnterPool();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0003375C File Offset: 0x00031B5C
		public override void ReturnPath()
		{
			if (base.error)
			{
				if (this.callbacks != null)
				{
					for (int i = 0; i < this.callbacks.Length; i++)
					{
						if (this.callbacks[i] != null)
						{
							this.callbacks[i](this);
						}
					}
				}
				if (this.callback != null)
				{
					this.callback(this);
				}
				return;
			}
			bool flag = false;
			Vector3 originalStartPoint = this.originalStartPoint;
			Vector3 startPoint = this.startPoint;
			Node startNode = this.startNode;
			for (int j = 0; j < this.nodePaths.Length; j++)
			{
				this.path = this.nodePaths[j];
				if (this.path != null)
				{
					base.CompleteState = PathCompleteState.Complete;
					flag = true;
				}
				else
				{
					base.CompleteState = PathCompleteState.Error;
				}
				if (this.callbacks != null && this.callbacks[j] != null)
				{
					this.vectorPath = this.vectorPaths[j];
					if (this.inverted)
					{
						this.endPoint = startPoint;
						this.endNode = startNode;
						this.startNode = this.targetNodes[j];
						this.startPoint = this.targetPoints[j];
						this.originalEndPoint = originalStartPoint;
						this.originalStartPoint = this.originalTargetPoints[j];
					}
					else
					{
						this.endPoint = this.targetPoints[j];
						this.originalEndPoint = this.originalTargetPoints[j];
						this.endNode = this.targetNodes[j];
					}
					this.callbacks[j](this);
					this.vectorPaths[j] = this.vectorPath;
				}
			}
			if (flag)
			{
				base.CompleteState = PathCompleteState.Complete;
				if (!this.pathsForAll)
				{
					this.path = this.nodePaths[this.chosenTarget];
					this.vectorPath = this.vectorPaths[this.chosenTarget];
					if (this.inverted)
					{
						this.endPoint = startPoint;
						this.endNode = startNode;
						this.startNode = this.targetNodes[this.chosenTarget];
						this.startPoint = this.targetPoints[this.chosenTarget];
						this.originalEndPoint = originalStartPoint;
						this.originalStartPoint = this.originalTargetPoints[this.chosenTarget];
					}
					else
					{
						this.endPoint = this.targetPoints[this.chosenTarget];
						this.originalEndPoint = this.originalTargetPoints[this.chosenTarget];
						this.endNode = this.targetNodes[this.chosenTarget];
					}
				}
			}
			else
			{
				base.CompleteState = PathCompleteState.Error;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00033A38 File Offset: 0x00031E38
		public void FoundTarget(NodeRun nodeR, int i)
		{
			Node node = nodeR.node;
			node.Bit8 = false;
			this.Trace(nodeR);
			this.vectorPaths[i] = this.vectorPath;
			this.nodePaths[i] = this.path;
			this.vectorPath = ListPool<Vector3>.Claim();
			this.path = ListPool<Node>.Claim();
			this.targetsFound[i] = true;
			this.targetNodeCount--;
			if (!this.pathsForAll)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.chosenTarget = i;
				this.targetNodeCount = 0;
				return;
			}
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			if (this.heuristicMode == MultiTargetPath.HeuristicMode.MovingAverage)
			{
				Vector3 vector = Vector3.zero;
				int num = 0;
				for (int j = 0; j < this.targetPoints.Length; j++)
				{
					if (!this.targetsFound[j])
					{
						vector += (Vector3)this.targetNodes[j].position;
						num++;
					}
				}
				if (num > 0)
				{
					vector /= (float)num;
				}
				this.hTarget = (Int3)vector;
				this.RebuildOpenList();
			}
			else if (this.heuristicMode == MultiTargetPath.HeuristicMode.MovingMidpoint)
			{
				Vector3 vector2 = Vector3.zero;
				Vector3 vector3 = Vector3.zero;
				bool flag = false;
				for (int k = 0; k < this.targetPoints.Length; k++)
				{
					if (!this.targetsFound[k])
					{
						if (!flag)
						{
							vector2 = (Vector3)this.targetNodes[k].position;
							vector3 = (Vector3)this.targetNodes[k].position;
							flag = true;
						}
						else
						{
							vector2 = Vector3.Min((Vector3)this.targetNodes[k].position, vector2);
							vector3 = Vector3.Max((Vector3)this.targetNodes[k].position, vector3);
						}
					}
				}
				Int3 hTarget = (Int3)((vector2 + vector3) * 0.5f);
				this.hTarget = hTarget;
				this.RebuildOpenList();
			}
			else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Sequential && this.sequentialTarget == i)
			{
				float num2 = 0f;
				for (int l = 0; l < this.targetPoints.Length; l++)
				{
					if (!this.targetsFound[l])
					{
						float sqrMagnitude = (this.targetNodes[l].position - this.startNode.position).sqrMagnitude;
						if (sqrMagnitude > num2)
						{
							num2 = sqrMagnitude;
							this.hTarget = (Int3)this.targetPoints[l];
							this.sequentialTarget = l;
						}
					}
				}
				this.RebuildOpenList();
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00033CE8 File Offset: 0x000320E8
		public void RebuildOpenList()
		{
			for (int i = 1; i < this.runData.open.numberOfItems; i++)
			{
				NodeRun node = this.runData.open.GetNode(i);
				node.node.UpdateH(this.hTarget, this.heuristic, this.heuristicScale, node);
			}
			this.runData.open.Rebuild();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00033D58 File Offset: 0x00032158
		public override void Prepare()
		{
			if (AstarPath.NumParallelThreads > 1)
			{
				base.LogError("MultiTargetPath can only be used with at most 1 concurrent pathfinder. Please use no multithreading or only 1 thread.");
				base.Error();
				return;
			}
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint, this.startHint);
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.LogError("Could not find start node for multi target path");
				base.Error();
				return;
			}
			if (!this.startNode.walkable)
			{
				base.LogError("Nearest node to the start point is not walkable");
				base.Error();
				return;
			}
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.vectorPaths = new List<Vector3>[this.targetPoints.Length];
			this.nodePaths = new List<Node>[this.targetPoints.Length];
			this.targetNodes = new Node[this.targetPoints.Length];
			this.targetsFound = new bool[this.targetPoints.Length];
			this.targetNodeCount = this.targetPoints.Length;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.targetPoints[i], this.nnConstraint);
				this.targetNodes[i] = nearest2.node;
				this.targetPoints[i] = nearest2.clampedPosition;
				if (this.targetNodes[i] != null)
				{
					flag3 = true;
					this.endNode = this.targetNodes[i];
				}
				bool flag4 = false;
				if (nearest2.node.walkable)
				{
					flag = true;
				}
				else
				{
					flag4 = true;
				}
				if (nearest2.node.area == this.startNode.area)
				{
					flag2 = true;
				}
				else
				{
					flag4 = true;
				}
				if (flag4)
				{
					this.targetsFound[i] = true;
					this.targetNodeCount--;
				}
			}
			this.startPoint = nearest.clampedPosition;
			this.startIntPoint = (Int3)this.startPoint;
			if (this.startNode == null || !flag3)
			{
				base.LogError(string.Concat(new string[]
				{
					"Couldn't find close nodes to either the start or the end (start = ",
					(this.startNode == null) ? "not found" : "found",
					" end = ",
					(!flag3) ? "none found" : "at least one found",
					")"
				}));
				base.Error();
				return;
			}
			if (!this.startNode.walkable)
			{
				base.LogError("The node closest to the start point is not walkable");
				base.Error();
				return;
			}
			if (!flag)
			{
				base.LogError("No target nodes were walkable");
				base.Error();
				return;
			}
			if (!flag2)
			{
				base.LogError("There are no valid paths to the targets");
				base.Error();
				return;
			}
			if (this.pathsForAll)
			{
				if (this.heuristicMode == MultiTargetPath.HeuristicMode.None)
				{
					this.heuristic = Heuristic.None;
					this.heuristicScale = 0f;
				}
				else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Average || this.heuristicMode == MultiTargetPath.HeuristicMode.MovingAverage)
				{
					Vector3 vector = Vector3.zero;
					for (int j = 0; j < this.targetNodes.Length; j++)
					{
						vector += (Vector3)this.targetNodes[j].position;
					}
					vector /= (float)this.targetNodes.Length;
					this.hTarget = (Int3)vector;
				}
				else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Midpoint || this.heuristicMode == MultiTargetPath.HeuristicMode.MovingMidpoint)
				{
					Vector3 vector2 = Vector3.zero;
					Vector3 vector3 = Vector3.zero;
					bool flag5 = false;
					for (int k = 0; k < this.targetPoints.Length; k++)
					{
						if (!this.targetsFound[k])
						{
							if (!flag5)
							{
								vector2 = (Vector3)this.targetNodes[k].position;
								vector3 = (Vector3)this.targetNodes[k].position;
								flag5 = true;
							}
							else
							{
								vector2 = Vector3.Min((Vector3)this.targetNodes[k].position, vector2);
								vector3 = Vector3.Max((Vector3)this.targetNodes[k].position, vector3);
							}
						}
					}
					Vector3 ob = (vector2 + vector3) * 0.5f;
					this.hTarget = (Int3)ob;
				}
				else if (this.heuristicMode == MultiTargetPath.HeuristicMode.Sequential)
				{
					float num = 0f;
					for (int l = 0; l < this.targetNodes.Length; l++)
					{
						if (!this.targetsFound[l])
						{
							float sqrMagnitude = (this.targetNodes[l].position - this.startNode.position).sqrMagnitude;
							if (sqrMagnitude > num)
							{
								num = sqrMagnitude;
								this.hTarget = (Int3)this.targetPoints[l];
								this.sequentialTarget = l;
							}
						}
					}
				}
			}
			else
			{
				this.heuristic = Heuristic.None;
				this.heuristicScale = 0f;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00034298 File Offset: 0x00032698
		public override void Initialize()
		{
			for (int i = 0; i < this.targetNodes.Length; i++)
			{
				if (this.startNode == this.targetNodes[i])
				{
					this.FoundTarget(this.startNode.GetNodeRun(this.runData), i);
				}
				else
				{
					this.targetNodes[i].Bit8 = true;
				}
			}
			AstarPath.OnPathPostSearch = (OnPathDelegate)Delegate.Combine(AstarPath.OnPathPostSearch, new OnPathDelegate(this.ResetBit8));
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			NodeRun nodeRun = this.startNode.GetNodeRun(this.runData);
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

		// Token: 0x0600056D RID: 1389 RVA: 0x000343EC File Offset: 0x000327EC
		public void ResetBit8(Path p)
		{
			AstarPath.OnPathPostSearch = (OnPathDelegate)Delegate.Remove(AstarPath.OnPathPostSearch, new OnPathDelegate(this.ResetBit8));
			if (p != this)
			{
				Debug.LogError("This should have been cleared after it was called on 'this' path. Was it not called? Or did the delegate reset not work?");
			}
			for (int i = 0; i < this.targetNodes.Length; i++)
			{
				this.targetNodes[i].Bit8 = false;
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00034454 File Offset: 0x00032854
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (!base.IsDone())
			{
				this.searchedNodes++;
				if (this.currentR.node.Bit8)
				{
					for (int i = 0; i < this.targetNodes.Length; i++)
					{
						if (!this.targetsFound[i] && this.currentR.node == this.targetNodes[i])
						{
							this.FoundTarget(this.currentR, i);
							if (base.CompleteState != PathCompleteState.NotCalculated)
							{
								break;
							}
						}
					}
					if (this.targetNodeCount <= 0)
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
				}
				this.currentR.node.Open(this.runData, this.currentR, this.hTarget, this);
				if (this.runData.open.numberOfItems <= 1)
				{
					base.LogError("No open points, whole area searched");
					base.Error();
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

		// Token: 0x0600056F RID: 1391 RVA: 0x00034590 File Offset: 0x00032990
		protected override void Trace(NodeRun node)
		{
			base.Trace(node);
			if (this.inverted)
			{
				int num = this.path.Count / 2;
				for (int i = 0; i < num; i++)
				{
					Node value = this.path[i];
					this.path[i] = this.path[this.path.Count - i - 1];
					this.path[this.path.Count - i - 1] = value;
				}
				for (int j = 0; j < num; j++)
				{
					Vector3 value2 = this.vectorPath[j];
					this.vectorPath[j] = this.vectorPath[this.vectorPath.Count - j - 1];
					this.vectorPath[this.vectorPath.Count - j - 1] = value2;
				}
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00034680 File Offset: 0x00032A80
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
				stringBuilder.Append("\nLast Found Path Length ");
				stringBuilder.Append((this.path != null) ? this.path.Count.ToString() : "Null");
				if (logMode == PathLog.Heavy)
				{
					stringBuilder.Append("\nSearch Iterations " + this.searchIterations);
					stringBuilder.Append("\nPaths (").Append(this.targetsFound.Length).Append("):");
					for (int i = 0; i < this.targetsFound.Length; i++)
					{
						stringBuilder.Append("\n\n\tPath " + i).Append(" Found: ").Append(this.targetsFound[i]);
						Node node = (this.nodePaths[i] != null) ? this.nodePaths[i][this.nodePaths[i].Count - 1] : null;
						if (node != null)
						{
							NodeRun nodeRun = this.endNode.GetNodeRun(this.runData);
							stringBuilder.Append("\n\t\tLength: ");
							stringBuilder.Append(this.nodePaths[i].Count);
							stringBuilder.Append("\n\t\tEnd Node");
							stringBuilder.Append("\n\t\t\tG: ");
							stringBuilder.Append(nodeRun.g);
							stringBuilder.Append("\n\t\t\tH: ");
							stringBuilder.Append(nodeRun.h);
							stringBuilder.Append("\n\t\t\tF: ");
							stringBuilder.Append(nodeRun.f);
							stringBuilder.Append("\n\t\t\tPoint: ");
							StringBuilder stringBuilder2 = stringBuilder;
							Vector3 endPoint = this.endPoint;
							stringBuilder2.Append(endPoint.ToString());
							stringBuilder.Append("\n\t\t\tGraph: ");
							stringBuilder.Append(this.endNode.graphIndex);
						}
					}
					stringBuilder.Append("\nStart Node");
					stringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder3 = stringBuilder;
					Vector3 endPoint2 = this.endPoint;
					stringBuilder3.Append(endPoint2.ToString());
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

		// Token: 0x04000463 RID: 1123
		public OnPathDelegate[] callbacks;

		// Token: 0x04000464 RID: 1124
		public Node[] targetNodes;

		// Token: 0x04000465 RID: 1125
		protected int targetNodeCount;

		// Token: 0x04000466 RID: 1126
		public bool[] targetsFound;

		// Token: 0x04000467 RID: 1127
		public Vector3[] targetPoints;

		// Token: 0x04000468 RID: 1128
		public Vector3[] originalTargetPoints;

		// Token: 0x04000469 RID: 1129
		public List<Vector3>[] vectorPaths;

		// Token: 0x0400046A RID: 1130
		public List<Node>[] nodePaths;

		// Token: 0x0400046B RID: 1131
		public int endsFound;

		// Token: 0x0400046C RID: 1132
		public bool pathsForAll = true;

		// Token: 0x0400046D RID: 1133
		public int chosenTarget = -1;

		// Token: 0x0400046E RID: 1134
		public int sequentialTarget;

		// Token: 0x0400046F RID: 1135
		public MultiTargetPath.HeuristicMode heuristicMode = MultiTargetPath.HeuristicMode.Sequential;

		// Token: 0x04000470 RID: 1136
		public bool inverted = true;

		// Token: 0x020000AF RID: 175
		public enum HeuristicMode
		{
			// Token: 0x04000472 RID: 1138
			None,
			// Token: 0x04000473 RID: 1139
			Average,
			// Token: 0x04000474 RID: 1140
			MovingAverage,
			// Token: 0x04000475 RID: 1141
			Midpoint,
			// Token: 0x04000476 RID: 1142
			MovingMidpoint,
			// Token: 0x04000477 RID: 1143
			Sequential
		}
	}
}
