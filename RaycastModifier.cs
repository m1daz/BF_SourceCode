using System;
using System.Collections.Generic;
using System.Diagnostics;
using Pathfinding;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[AddComponentMenu("Pathfinding/Modifiers/Raycast Simplifier")]
[Serializable]
public class RaycastModifier : MonoModifier
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000518 RID: 1304 RVA: 0x00030AF6 File Offset: 0x0002EEF6
	public override ModifierData input
	{
		get
		{
			return ModifierData.Vector;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000519 RID: 1305 RVA: 0x00030AFA File Offset: 0x0002EEFA
	public override ModifierData output
	{
		get
		{
			return ModifierData.VectorPath;
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00030B00 File Offset: 0x0002EF00
	public override void Apply(Path p, ModifierData source)
	{
		if (this.iterations <= 0)
		{
			return;
		}
		if (RaycastModifier.nodes == null)
		{
			RaycastModifier.nodes = new List<Vector3>(p.vectorPath.Count);
		}
		else
		{
			RaycastModifier.nodes.Clear();
		}
		RaycastModifier.nodes.AddRange(p.vectorPath);
		for (int i = 0; i < this.iterations; i++)
		{
			if (this.subdivideEveryIter && i != 0)
			{
				if (RaycastModifier.nodes.Capacity < RaycastModifier.nodes.Count * 3)
				{
					RaycastModifier.nodes.Capacity = RaycastModifier.nodes.Count * 3;
				}
				int count = RaycastModifier.nodes.Count;
				for (int j = 0; j < count - 1; j++)
				{
					RaycastModifier.nodes.Add(Vector3.zero);
					RaycastModifier.nodes.Add(Vector3.zero);
				}
				for (int k = count - 1; k > 0; k--)
				{
					Vector3 a = RaycastModifier.nodes[k];
					Vector3 b = RaycastModifier.nodes[k + 1];
					RaycastModifier.nodes[k * 3] = RaycastModifier.nodes[k];
					if (k != count - 1)
					{
						RaycastModifier.nodes[k * 3 + 1] = Vector3.Lerp(a, b, 0.33f);
						RaycastModifier.nodes[k * 3 + 2] = Vector3.Lerp(a, b, 0.66f);
					}
				}
			}
			int l = 0;
			while (l < RaycastModifier.nodes.Count - 2)
			{
				Vector3 v = RaycastModifier.nodes[l];
				Vector3 v2 = RaycastModifier.nodes[l + 2];
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				if (this.ValidateLine(null, null, v, v2))
				{
					RaycastModifier.nodes.RemoveAt(l + 1);
				}
				else
				{
					l++;
				}
				stopwatch.Stop();
			}
		}
		p.vectorPath.Clear();
		p.vectorPath.AddRange(RaycastModifier.nodes);
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00030D0C File Offset: 0x0002F10C
	public bool ValidateLine(Node n1, Node n2, Vector3 v1, Vector3 v2)
	{
		if (this.useRaycasting)
		{
			RaycastHit raycastHit2;
			if (this.thickRaycast && this.thickRaycastRadius > 0f)
			{
				RaycastHit raycastHit;
				if (Physics.SphereCast(v1 + this.raycastOffset, this.thickRaycastRadius, v2 - v1, out raycastHit, (v2 - v1).magnitude, this.mask))
				{
					return false;
				}
			}
			else if (Physics.Linecast(v1 + this.raycastOffset, v2 + this.raycastOffset, out raycastHit2, this.mask))
			{
				return false;
			}
		}
		if (this.useGraphRaycasting && n1 == null)
		{
			n1 = AstarPath.active.GetNearest(v1).node;
			n2 = AstarPath.active.GetNearest(v2).node;
		}
		if (this.useGraphRaycasting && n1 != null && n2 != null)
		{
			NavGraph graph = AstarData.GetGraph(n1);
			NavGraph graph2 = AstarData.GetGraph(n2);
			if (graph != graph2)
			{
				return false;
			}
			if (graph != null)
			{
				IRaycastableGraph raycastableGraph = graph as IRaycastableGraph;
				if (raycastableGraph != null && raycastableGraph.Linecast(v1, v2, n1))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x04000425 RID: 1061
	public bool useRaycasting = true;

	// Token: 0x04000426 RID: 1062
	public LayerMask mask = -1;

	// Token: 0x04000427 RID: 1063
	public bool thickRaycast;

	// Token: 0x04000428 RID: 1064
	public float thickRaycastRadius;

	// Token: 0x04000429 RID: 1065
	public Vector3 raycastOffset = Vector3.zero;

	// Token: 0x0400042A RID: 1066
	public bool subdivideEveryIter;

	// Token: 0x0400042B RID: 1067
	public int iterations = 2;

	// Token: 0x0400042C RID: 1068
	public bool useGraphRaycasting;

	// Token: 0x0400042D RID: 1069
	private static List<Vector3> nodes;
}
