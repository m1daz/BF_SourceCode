using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000099 RID: 153
[AddComponentMenu("Pathfinding/Modifiers/Alternative Path")]
[Serializable]
public class AlternativePath : MonoModifier
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000301AD File Offset: 0x0002E5AD
	public override ModifierData input
	{
		get
		{
			return ModifierData.Original;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000301B1 File Offset: 0x0002E5B1
	public override ModifierData output
	{
		get
		{
			return ModifierData.All;
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x000301B4 File Offset: 0x0002E5B4
	public override void Apply(Path p, ModifierData source)
	{
		object obj = this.lockObject;
		lock (obj)
		{
			this.toBeApplied = p.path.ToArray();
			if (!this.waitingForApply)
			{
				this.waitingForApply = true;
				AstarPath.OnPathPreSearch = (OnPathDelegate)Delegate.Combine(AstarPath.OnPathPreSearch, new OnPathDelegate(this.ApplyNow));
			}
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00030230 File Offset: 0x0002E630
	private void ApplyNow(Path somePath)
	{
		object obj = this.lockObject;
		lock (obj)
		{
			this.waitingForApply = false;
			AstarPath.OnPathPreSearch = (OnPathDelegate)Delegate.Remove(AstarPath.OnPathPreSearch, new OnPathDelegate(this.ApplyNow));
			int seed = this.prevSeed;
			this.rnd = new System.Random(seed);
			if (this.prevNodes != null)
			{
				int num = this.rnd.Next(this.randomStep);
				for (int i = num; i < this.prevNodes.Length; i += this.rnd.Next(1, this.randomStep))
				{
					this.prevNodes[i].penalty = (uint)((ulong)this.prevNodes[i].penalty - (ulong)((long)this.prevPenalty));
				}
			}
			seed = this.seedGenerator.Next();
			this.rnd = new System.Random(seed);
			if (this.toBeApplied != null)
			{
				int num2 = this.rnd.Next(this.randomStep);
				for (int j = num2; j < this.toBeApplied.Length; j += this.rnd.Next(1, this.randomStep))
				{
					this.toBeApplied[j].penalty = (uint)((ulong)this.toBeApplied[j].penalty + (ulong)((long)this.penalty));
				}
			}
			this.prevPenalty = this.penalty;
			this.prevSeed = seed;
			this.prevNodes = this.toBeApplied;
		}
	}

	// Token: 0x0400040A RID: 1034
	public int penalty = 1000;

	// Token: 0x0400040B RID: 1035
	public int randomStep = 10;

	// Token: 0x0400040C RID: 1036
	private Node[] prevNodes;

	// Token: 0x0400040D RID: 1037
	private int prevSeed;

	// Token: 0x0400040E RID: 1038
	private int prevPenalty;

	// Token: 0x0400040F RID: 1039
	private bool waitingForApply;

	// Token: 0x04000410 RID: 1040
	private object lockObject = new object();

	// Token: 0x04000411 RID: 1041
	private System.Random rnd = new System.Random();

	// Token: 0x04000412 RID: 1042
	private System.Random seedGenerator = new System.Random();

	// Token: 0x04000413 RID: 1043
	private Node[] toBeApplied;
}
