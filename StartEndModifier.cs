using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020000A4 RID: 164
[Serializable]
public class StartEndModifier : Modifier
{
	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000526 RID: 1318 RVA: 0x00031981 File Offset: 0x0002FD81
	public override ModifierData input
	{
		get
		{
			return ModifierData.Vector;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000527 RID: 1319 RVA: 0x00031985 File Offset: 0x0002FD85
	public override ModifierData output
	{
		get
		{
			return ((!this.addPoints) ? ModifierData.StrictVectorPath : ModifierData.None) | ModifierData.VectorPath;
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0003199C File Offset: 0x0002FD9C
	public override void Apply(Path _p, ModifierData source)
	{
		ABPath abpath = _p as ABPath;
		if (abpath == null)
		{
			return;
		}
		if (abpath.vectorPath.Count == 0)
		{
			return;
		}
		if (abpath.vectorPath.Count < 2 && !this.addPoints)
		{
			abpath.vectorPath.Add(abpath.vectorPath[0]);
		}
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (this.exactStartPoint == StartEndModifier.Exactness.Original)
		{
			vector = this.GetClampedPoint((Vector3)abpath.path[0].position, abpath.originalStartPoint, abpath.path[0]);
		}
		else if (this.exactStartPoint == StartEndModifier.Exactness.ClosestOnNode)
		{
			vector = this.GetClampedPoint((Vector3)abpath.path[0].position, abpath.startPoint, abpath.path[0]);
		}
		else if (this.exactStartPoint == StartEndModifier.Exactness.Interpolate)
		{
			vector = this.GetClampedPoint((Vector3)abpath.path[0].position, abpath.originalStartPoint, abpath.path[0]);
			vector = Mathfx.NearestPointStrict((Vector3)abpath.path[0].position, (Vector3)abpath.path[(1 < abpath.path.Count) ? 1 : 0].position, vector);
		}
		else
		{
			vector = (Vector3)abpath.path[0].position;
		}
		if (this.exactEndPoint == StartEndModifier.Exactness.Original)
		{
			vector2 = this.GetClampedPoint((Vector3)abpath.path[abpath.path.Count - 1].position, abpath.originalEndPoint, abpath.path[abpath.path.Count - 1]);
		}
		else if (this.exactEndPoint == StartEndModifier.Exactness.ClosestOnNode)
		{
			vector2 = this.GetClampedPoint((Vector3)abpath.path[abpath.path.Count - 1].position, abpath.endPoint, abpath.path[abpath.path.Count - 1]);
		}
		else if (this.exactEndPoint == StartEndModifier.Exactness.Interpolate)
		{
			vector2 = this.GetClampedPoint((Vector3)abpath.path[abpath.path.Count - 1].position, abpath.originalEndPoint, abpath.path[abpath.path.Count - 1]);
			vector2 = Mathfx.NearestPointStrict((Vector3)abpath.path[abpath.path.Count - 1].position, (Vector3)abpath.path[(abpath.path.Count - 2 >= 0) ? (abpath.path.Count - 2) : 0].position, vector2);
		}
		else
		{
			vector2 = (Vector3)abpath.path[abpath.path.Count - 1].position;
		}
		if (!this.addPoints)
		{
			abpath.vectorPath[0] = vector;
			abpath.vectorPath[abpath.vectorPath.Count - 1] = vector2;
		}
		else
		{
			if (this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Insert(0, vector);
			}
			if (this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Add(vector2);
			}
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00031D1C File Offset: 0x0003011C
	public Vector3 GetClampedPoint(Vector3 from, Vector3 to, Node hint)
	{
		Vector3 vector = to;
		RaycastHit raycastHit;
		if (this.useRaycasting && Physics.Linecast(from, to, out raycastHit, this.mask))
		{
			vector = raycastHit.point;
		}
		if (this.useGraphRaycasting && hint != null)
		{
			NavGraph graph = AstarData.GetGraph(hint);
			if (graph != null)
			{
				IRaycastableGraph raycastableGraph = graph as IRaycastableGraph;
				GraphHitInfo graphHitInfo;
				if (raycastableGraph != null && raycastableGraph.Linecast(from, vector, hint, out graphHitInfo))
				{
					vector = graphHitInfo.point;
				}
			}
		}
		return vector;
	}

	// Token: 0x0400043C RID: 1084
	public bool addPoints;

	// Token: 0x0400043D RID: 1085
	public StartEndModifier.Exactness exactStartPoint = StartEndModifier.Exactness.Original;

	// Token: 0x0400043E RID: 1086
	public StartEndModifier.Exactness exactEndPoint = StartEndModifier.Exactness.Original;

	// Token: 0x0400043F RID: 1087
	public bool useRaycasting;

	// Token: 0x04000440 RID: 1088
	public LayerMask mask = -1;

	// Token: 0x04000441 RID: 1089
	public bool useGraphRaycasting;

	// Token: 0x020000A5 RID: 165
	public enum Exactness
	{
		// Token: 0x04000443 RID: 1091
		SnapToNode,
		// Token: 0x04000444 RID: 1092
		Original,
		// Token: 0x04000445 RID: 1093
		Interpolate,
		// Token: 0x04000446 RID: 1094
		ClosestOnNode
	}
}
