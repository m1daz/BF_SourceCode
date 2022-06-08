using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B1 RID: 177
	public class XPath : ABPath
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000349E4 File Offset: 0x00032DE4
		public XPath()
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000349EC File Offset: 0x00032DEC
		public XPath(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			base.Setup(start, end, callbackDelegate);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00034A00 File Offset: 0x00032E00
		public new static XPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			XPath path = PathPool<XPath>.GetPath();
			path.Setup(start, end, callback);
			path.endingCondition = new ABPathEndingCondition(path);
			return path;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00034A29 File Offset: 0x00032E29
		protected override void Recycle()
		{
			PathPool<XPath>.Recycle(this);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00034A31 File Offset: 0x00032E31
		public override void Reset()
		{
			base.Reset();
			this.endingCondition = null;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00034A40 File Offset: 0x00032E40
		public override void Initialize()
		{
			base.Initialize();
			if (this.currentR != null && this.endingCondition.TargetFound(this.currentR))
			{
				base.CompleteState = PathCompleteState.Complete;
				this.endNode = this.currentR.node;
				this.Trace(this.currentR);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00034A98 File Offset: 0x00032E98
		public override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (!base.IsDone())
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					this.endNode = this.currentR.node;
					break;
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
		}

		// Token: 0x04000482 RID: 1154
		public PathEndingCondition endingCondition;
	}
}
