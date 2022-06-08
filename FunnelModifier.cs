using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x0200009A RID: 154
[AddComponentMenu("Pathfinding/Modifiers/Funnel")]
[Serializable]
public class FunnelModifier : MonoModifier
{
	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060004EB RID: 1259 RVA: 0x000303CC File Offset: 0x0002E7CC
	public override ModifierData input
	{
		get
		{
			return ModifierData.StrictVectorPath;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060004EC RID: 1260 RVA: 0x000303CF File Offset: 0x0002E7CF
	public override ModifierData output
	{
		get
		{
			return ModifierData.VectorPath;
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000303D4 File Offset: 0x0002E7D4
	public override void Apply(Path p, ModifierData source)
	{
		List<Node> path = p.path;
		List<Vector3> vectorPath = p.vectorPath;
		if (path == null || path.Count == 0 || vectorPath == null || vectorPath.Count != path.Count)
		{
			return;
		}
		int graphIndex = path[0].graphIndex;
		int num = 0;
		List<Vector3> list = ListPool<Vector3>.Claim();
		List<Vector3> list2 = ListPool<Vector3>.Claim();
		List<Vector3> list3 = ListPool<Vector3>.Claim();
		for (int i = 0; i < path.Count; i++)
		{
			if (path[i].graphIndex != graphIndex)
			{
				IFunnelGraph funnelGraph = AstarData.GetGraph(path[num]) as IFunnelGraph;
				if (funnelGraph == null)
				{
					for (int j = num; j <= i; j++)
					{
						list.Add((Vector3)path[j].position);
					}
				}
				else
				{
					this.ConstructFunnel(funnelGraph, vectorPath, path, num, i - 1, list, list2, list3);
				}
				graphIndex = path[i].graphIndex;
				num = i;
			}
		}
		IFunnelGraph funnelGraph2 = AstarData.GetGraph(path[num]) as IFunnelGraph;
		if (funnelGraph2 == null)
		{
			for (int k = num; k < path.Count - 1; k++)
			{
				list.Add((Vector3)path[k].position);
			}
		}
		else
		{
			this.ConstructFunnel(funnelGraph2, vectorPath, path, num, path.Count - 1, list, list2, list3);
		}
		ListPool<Vector3>.Release(p.vectorPath);
		p.vectorPath = list;
		ListPool<Vector3>.Release(list2);
		ListPool<Vector3>.Release(list3);
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00030570 File Offset: 0x0002E970
	public void ConstructFunnel(IFunnelGraph funnelGraph, List<Vector3> vectorPath, List<Node> path, int sIndex, int eIndex, List<Vector3> funnelPath, List<Vector3> left, List<Vector3> right)
	{
		left.Clear();
		right.Clear();
		left.Add(vectorPath[sIndex]);
		right.Add(vectorPath[sIndex]);
		funnelGraph.BuildFunnelCorridor(path, sIndex, eIndex, left, right);
		left.Add(vectorPath[eIndex]);
		right.Add(vectorPath[eIndex]);
		if (!this.RunFunnel(left, right, funnelPath))
		{
			funnelPath.Add(vectorPath[sIndex]);
			funnelPath.Add(vectorPath[eIndex]);
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00030608 File Offset: 0x0002EA08
	public bool RunFunnel(List<Vector3> left, List<Vector3> right, List<Vector3> funnelPath)
	{
		if (left.Count <= 3)
		{
			return false;
		}
		while (left[1] == left[2] && right[1] == right[2])
		{
			left.RemoveAt(1);
			right.RemoveAt(1);
			if (left.Count <= 3)
			{
				return false;
			}
		}
		Vector3 vector = left[2];
		if (vector == left[1])
		{
			vector = right[2];
		}
		while (Polygon.IsColinear(left[0], left[1], right[1]) || Polygon.Left(left[1], right[1], vector) == Polygon.Left(left[1], right[1], left[0]))
		{
			left.RemoveAt(1);
			right.RemoveAt(1);
			if (left.Count <= 3)
			{
				return false;
			}
			vector = left[2];
			if (vector == left[1])
			{
				vector = right[2];
			}
		}
		if (!Polygon.IsClockwise(left[0], left[1], right[1]) && !Polygon.IsColinear(left[0], left[1], right[1]))
		{
			List<Vector3> list = left;
			left = right;
			right = list;
		}
		funnelPath.Add(left[0]);
		Vector3 vector2 = left[0];
		Vector3 vector3 = left[1];
		Vector3 vector4 = right[1];
		int num = 1;
		int num2 = 1;
		int i = 2;
		while (i < left.Count)
		{
			if (funnelPath.Count > 200)
			{
				Debug.LogWarning("Avoiding infinite loop");
				break;
			}
			Vector3 vector5 = left[i];
			Vector3 vector6 = right[i];
			if (Polygon.TriangleArea2(vector2, vector4, vector6) < 0f)
			{
				goto IL_22A;
			}
			if (vector2 == vector4 || Polygon.TriangleArea2(vector2, vector3, vector6) <= 0f)
			{
				vector4 = vector6;
				num = i;
				goto IL_22A;
			}
			funnelPath.Add(vector3);
			vector2 = vector3;
			int num3 = num2;
			vector3 = vector2;
			vector4 = vector2;
			num2 = num3;
			num = num3;
			i = num3;
			IL_28E:
			i++;
			continue;
			IL_22A:
			if (Polygon.TriangleArea2(vector2, vector3, vector5) > 0f)
			{
				goto IL_28E;
			}
			if (vector2 == vector3 || Polygon.TriangleArea2(vector2, vector4, vector5) >= 0f)
			{
				vector3 = vector5;
				num2 = i;
				goto IL_28E;
			}
			funnelPath.Add(vector4);
			vector2 = vector4;
			num3 = num;
			vector3 = vector2;
			vector4 = vector2;
			num2 = num3;
			num = num3;
			i = num3;
			goto IL_28E;
		}
		funnelPath.Add(left[left.Count - 1]);
		return true;
	}

	// Token: 0x04000414 RID: 1044
	private static List<Vector3> tmpList;

	// Token: 0x04000415 RID: 1045
	private static Int3[] leftFunnel;

	// Token: 0x04000416 RID: 1046
	private static Int3[] rightFunnel;
}
