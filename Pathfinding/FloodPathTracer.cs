using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AD RID: 173
	public class FloodPathTracer : ABPath
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x000333C9 File Offset: 0x000317C9
		[Obsolete("Use the Construct method instead")]
		public FloodPathTracer(Vector3 start, FloodPath flood, OnPathDelegate callbackDelegate)
		{
			throw new Exception("This constructor is obsolete");
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000333DB File Offset: 0x000317DB
		public FloodPathTracer()
		{
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000333E4 File Offset: 0x000317E4
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool<FloodPathTracer>.GetPath();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00033404 File Offset: 0x00031804
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.GetState() < PathState.Returned)
			{
				throw new ArgumentNullException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
			this.hasEndPoint = false;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00033456 File Offset: 0x00031856
		public override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00033468 File Offset: 0x00031868
		public override void Initialize()
		{
			if (this.startNode != null && this.flood.HasPathTo(this.startNode))
			{
				this.Trace(this.startNode);
				base.CompleteState = PathCompleteState.Complete;
			}
			else
			{
				base.Error();
				base.LogError("Could not find valid start node");
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000334BF File Offset: 0x000318BF
		public override void CalculateStep(long targetTick)
		{
			if (!base.IsDone())
			{
				base.Error();
				base.LogError("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000334E0 File Offset: 0x000318E0
		public void Trace(Node from)
		{
			Node node = from;
			int num = 0;
			while (node != null)
			{
				this.path.Add(node);
				this.vectorPath.Add((Vector3)node.position);
				node = this.flood.GetParent(node);
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (FloodPathTracer.cs, Trace function)");
					break;
				}
			}
		}

		// Token: 0x04000462 RID: 1122
		protected FloodPath flood;
	}
}
