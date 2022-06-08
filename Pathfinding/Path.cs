using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000036 RID: 54
	public abstract class Path
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000D38C File Offset: 0x0000B78C
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000D394 File Offset: 0x0000B794
		public PathCompleteState CompleteState
		{
			get
			{
				return this.pathCompleteState;
			}
			protected set
			{
				this.pathCompleteState = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000D39D File Offset: 0x0000B79D
		public bool error
		{
			get
			{
				return this.CompleteState == PathCompleteState.Error;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000D3A8 File Offset: 0x0000B7A8
		public string errorLog
		{
			get
			{
				return this._errorLog;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000D3B0 File Offset: 0x0000B7B0
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000D3B8 File Offset: 0x0000B7B8
		public int[] tagPenalties
		{
			get
			{
				return this._tagPenalties;
			}
			set
			{
				if (value == null || value.Length != 32)
				{
					this._tagPenalties = new int[0];
				}
				else
				{
					this._tagPenalties = value;
				}
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D3E4 File Offset: 0x0000B7E4
		public float GetTotalLength()
		{
			if (this.vectorPath == null)
			{
				return float.PositiveInfinity;
			}
			float num = 0f;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				num += Vector3.Distance(this.vectorPath[i], this.vectorPath[i + 1]);
			}
			return num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D44C File Offset: 0x0000B84C
		public IEnumerator WaitForPath()
		{
			if (this.GetState() == PathState.Created)
			{
				throw new InvalidOperationException("This path has not been started yet");
			}
			while (this.GetState() != PathState.Returned)
			{
				yield return 0;
			}
			yield break;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D467 File Offset: 0x0000B867
		public uint GetTagPenalty(int tag)
		{
			return (uint)((tag >= this._tagPenalties.Length) ? 0 : this._tagPenalties[tag]);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D485 File Offset: 0x0000B885
		public bool CanTraverse(Node node)
		{
			return node.walkable && (this.enabledTags >> node.tags & 1) != 0;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D4AD File Offset: 0x0000B8AD
		public bool IsDone()
		{
			return this.CompleteState != PathCompleteState.NotCalculated;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D4BC File Offset: 0x0000B8BC
		public void AdvanceState(PathState s)
		{
			object obj = this.stateLock;
			lock (obj)
			{
				this.state = (PathState)Math.Max((int)this.state, (int)s);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D504 File Offset: 0x0000B904
		public PathState GetState()
		{
			return this.state;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D50C File Offset: 0x0000B90C
		public void LogError(string msg)
		{
			this._errorLog += msg;
			if (AstarPath.active.logPathResults != PathLog.None && AstarPath.active.logPathResults != PathLog.InGame)
			{
				Debug.LogWarning(msg);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000D545 File Offset: 0x0000B945
		public void ForceLogError(string msg)
		{
			this.Error();
			this._errorLog += msg;
			Debug.LogError(msg);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D565 File Offset: 0x0000B965
		public void Log(string msg)
		{
			this._errorLog += msg;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D579 File Offset: 0x0000B979
		public void Error()
		{
			this.CompleteState = PathCompleteState.Error;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D584 File Offset: 0x0000B984
		private void ErrorCheck()
		{
			if (!this.hasBeenReset)
			{
				throw new Exception("The path has never been reset. Use pooling API or call Reset() after creating the path with the default constructor.");
			}
			if (this.recycled)
			{
				throw new Exception("The path is currently in a path pool. Are you sending the path for calculation twice?");
			}
			if (this.runData == null)
			{
				throw new Exception("Field runData is not set. Please report this bug.");
			}
			if (this.GetState() > PathState.Processing)
			{
				throw new Exception("This path has already been processed. Do not request a path with the same path object twice.");
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D5EA File Offset: 0x0000B9EA
		public virtual void OnEnterPool()
		{
			if (this.vectorPath != null)
			{
				ListPool<Vector3>.Release(this.vectorPath);
			}
			if (this.path != null)
			{
				ListPool<Node>.Release(this.path);
			}
			this.vectorPath = null;
			this.path = null;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D628 File Offset: 0x0000BA28
		public virtual void Reset()
		{
			if (AstarPath.active == null)
			{
				throw new NullReferenceException("No AstarPath object found in the scene. Make sure there is one or do not create paths in Awake");
			}
			this.hasBeenReset = true;
			this.state = PathState.Created;
			this.releasedNotSilent = false;
			this.runData = null;
			this.callback = null;
			this._errorLog = string.Empty;
			this.pathCompleteState = PathCompleteState.NotCalculated;
			this.path = ListPool<Node>.Claim();
			this.vectorPath = ListPool<Vector3>.Claim();
			this.currentR = null;
			this.duration = 0f;
			this.searchIterations = 0;
			this.searchedNodes = 0;
			this.nnConstraint = PathNNConstraint.Default;
			this.next = null;
			this.radius = 0;
			this.walkabilityMask = -1;
			this.height = 0;
			this.turnRadius = 0;
			this.speed = 0;
			this.heuristic = AstarPath.active.heuristic;
			this.heuristicScale = AstarPath.active.heuristicScale;
			this.pathID = 0;
			this.enabledTags = -1;
			this.tagPenalties = null;
			this.callTime = DateTime.UtcNow;
			this.pathID = AstarPath.active.GetNextPathID();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D740 File Offset: 0x0000BB40
		protected bool HasExceededTime(int searchedNodes, long targetTime)
		{
			return DateTime.UtcNow.Ticks >= targetTime;
		}

		// Token: 0x060001DE RID: 478
		protected abstract void Recycle();

		// Token: 0x060001DF RID: 479 RVA: 0x0000D760 File Offset: 0x0000BB60
		public void Claim(object o)
		{
			if (this.claimed.Contains(o))
			{
				throw new ArgumentException("You have already claimed the path with that object (" + o.ToString() + "). Are you claiming the path with the same object twice?");
			}
			this.claimed.Add(o);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D79C File Offset: 0x0000BB9C
		public void ReleaseSilent(object o)
		{
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					this.claimed.RemoveAt(i);
					if (this.releasedNotSilent && this.claimed.Count == 0)
					{
						this.Recycle();
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o.ToString() + ") twice?");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o.ToString() + "). Are you releasing the path with the same object twice?");
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D84C File Offset: 0x0000BC4C
		public void Release(object o)
		{
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					this.claimed.RemoveAt(i);
					this.releasedNotSilent = true;
					if (this.claimed.Count == 0)
					{
						this.Recycle();
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o.ToString() + ") twice?");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o.ToString() + "). Are you releasing the path with the same object twice?");
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D8F8 File Offset: 0x0000BCF8
		protected virtual void Trace(NodeRun from)
		{
			int num = 0;
			NodeRun nodeRun = from;
			while (nodeRun != null)
			{
				nodeRun = nodeRun.parent;
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (Path.cs, Trace function)");
					break;
				}
			}
			if (this.path.Capacity < num)
			{
				this.path.Capacity = num;
			}
			if (this.vectorPath.Capacity < num)
			{
				this.vectorPath.Capacity = num;
			}
			nodeRun = from;
			for (int i = 0; i < num; i++)
			{
				this.path.Add(nodeRun.node);
				nodeRun = nodeRun.parent;
			}
			int num2 = num / 2;
			for (int j = 0; j < num2; j++)
			{
				Node value = this.path[j];
				this.path[j] = this.path[num - j - 1];
				this.path[num - j - 1] = value;
			}
			for (int k = 0; k < num; k++)
			{
				this.vectorPath.Add((Vector3)this.path[k].position);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000DA34 File Offset: 0x0000BE34
		public virtual string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!this.error && logMode == PathLog.OnlyErrors))
			{
				return string.Empty;
			}
			StringBuilder debugStringBuilder = this.runData.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			debugStringBuilder.Append((!this.error) ? "Path Completed : " : "Path Failed : ");
			debugStringBuilder.Append("Computation Time ");
			debugStringBuilder.Append(this.duration.ToString((logMode != PathLog.Heavy) ? "0.00 ms " : "0.000 ms "));
			debugStringBuilder.Append("Searched Nodes ");
			debugStringBuilder.Append(this.searchedNodes);
			if (!this.error)
			{
				debugStringBuilder.Append(" Path Length ");
				debugStringBuilder.Append((this.path != null) ? this.path.Count.ToString() : "Null");
				if (logMode == PathLog.Heavy)
				{
					debugStringBuilder.Append("\nSearch Iterations " + this.searchIterations);
					debugStringBuilder.Append("\nBinary Heap size at complete: ");
					debugStringBuilder.Append((this.runData.open != null) ? (this.runData.open.numberOfItems - 2).ToString() : "null");
				}
			}
			if (this.error)
			{
				debugStringBuilder.Append("\nError: ");
				debugStringBuilder.Append(this.errorLog);
			}
			debugStringBuilder.Append("\nPath Number ");
			debugStringBuilder.Append(this.pathID);
			return debugStringBuilder.ToString();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000DBDF File Offset: 0x0000BFDF
		public virtual void ReturnPath()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000DBF8 File Offset: 0x0000BFF8
		public void PrepareBase(NodeRunData runData)
		{
			if (runData.pathID > this.pathID)
			{
				runData.ClearPathIDs();
			}
			this.runData = runData;
			runData.Initialize(this);
			try
			{
				this.ErrorCheck();
			}
			catch (Exception ex)
			{
				this.ForceLogError(string.Concat(new object[]
				{
					"Exception in path ",
					this.pathID,
					"\n",
					ex.ToString()
				}));
			}
		}

		// Token: 0x060001E6 RID: 486
		public abstract void Prepare();

		// Token: 0x060001E7 RID: 487
		public abstract void Initialize();

		// Token: 0x060001E8 RID: 488
		public abstract void CalculateStep(long targetTick);

		// Token: 0x04000189 RID: 393
		public NodeRunData runData;

		// Token: 0x0400018A RID: 394
		public OnPathDelegate callback;

		// Token: 0x0400018B RID: 395
		private PathState state;

		// Token: 0x0400018C RID: 396
		private object stateLock = new object();

		// Token: 0x0400018D RID: 397
		private PathCompleteState pathCompleteState;

		// Token: 0x0400018E RID: 398
		private string _errorLog = string.Empty;

		// Token: 0x0400018F RID: 399
		private Node[] _path;

		// Token: 0x04000190 RID: 400
		private Vector3[] _vectorPath;

		// Token: 0x04000191 RID: 401
		public List<Node> path;

		// Token: 0x04000192 RID: 402
		public List<Vector3> vectorPath;

		// Token: 0x04000193 RID: 403
		protected float maxFrameTime;

		// Token: 0x04000194 RID: 404
		protected NodeRun currentR;

		// Token: 0x04000195 RID: 405
		public float duration;

		// Token: 0x04000196 RID: 406
		public int searchIterations;

		// Token: 0x04000197 RID: 407
		public int searchedNodes;

		// Token: 0x04000198 RID: 408
		public DateTime callTime;

		// Token: 0x04000199 RID: 409
		public bool recycled;

		// Token: 0x0400019A RID: 410
		protected bool hasBeenReset;

		// Token: 0x0400019B RID: 411
		public NNConstraint nnConstraint = PathNNConstraint.Default;

		// Token: 0x0400019C RID: 412
		public Path next;

		// Token: 0x0400019D RID: 413
		public int radius;

		// Token: 0x0400019E RID: 414
		public int walkabilityMask = -1;

		// Token: 0x0400019F RID: 415
		public int height;

		// Token: 0x040001A0 RID: 416
		public int turnRadius;

		// Token: 0x040001A1 RID: 417
		public int speed;

		// Token: 0x040001A2 RID: 418
		public Heuristic heuristic;

		// Token: 0x040001A3 RID: 419
		public float heuristicScale = 1f;

		// Token: 0x040001A4 RID: 420
		public ushort pathID;

		// Token: 0x040001A5 RID: 421
		public int enabledTags = -1;

		// Token: 0x040001A6 RID: 422
		protected int[] _tagPenalties = new int[0];

		// Token: 0x040001A7 RID: 423
		private List<object> claimed = new List<object>();

		// Token: 0x040001A8 RID: 424
		private bool releasedNotSilent;
	}
}
