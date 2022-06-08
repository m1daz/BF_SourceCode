using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[AddComponentMenu("Pathfinding/Modifiers/Simple Smooth")]
[Serializable]
public class SimpleSmoothModifier : MonoModifier
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x0600051D RID: 1309 RVA: 0x00030EB3 File Offset: 0x0002F2B3
	public override ModifierData input
	{
		get
		{
			return ModifierData.All;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x0600051E RID: 1310 RVA: 0x00030EB8 File Offset: 0x0002F2B8
	public override ModifierData output
	{
		get
		{
			ModifierData modifierData = ModifierData.VectorPath;
			if (this.iterations == 0 && this.smoothType == SimpleSmoothModifier.SmoothType.Simple && !this.uniformLength)
			{
				modifierData |= ModifierData.StrictVectorPath;
			}
			return modifierData;
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00030EF0 File Offset: 0x0002F2F0
	public override void Apply(Path p, ModifierData source)
	{
		if (p.vectorPath == null)
		{
			Debug.LogWarning("Can't process NULL path (has another modifier logged an error?)");
			return;
		}
		List<Vector3> list = null;
		switch (this.smoothType)
		{
		case SimpleSmoothModifier.SmoothType.Simple:
			list = this.SmoothSimple(p.vectorPath);
			break;
		case SimpleSmoothModifier.SmoothType.Bezier:
			list = this.SmoothBezier(p.vectorPath);
			break;
		case SimpleSmoothModifier.SmoothType.OffsetSimple:
			list = this.SmoothOffsetSimple(p.vectorPath);
			break;
		case SimpleSmoothModifier.SmoothType.CurvedNonuniform:
			list = this.CurvedNonuniform(p.vectorPath);
			break;
		}
		if (list != p.vectorPath)
		{
			ListPool<Vector3>.Release(p.vectorPath);
			p.vectorPath = list;
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00030FA0 File Offset: 0x0002F3A0
	public List<Vector3> CurvedNonuniform(List<Vector3> path)
	{
		if (this.maxSegmentLength <= 0f)
		{
			Debug.LogWarning("Max Segment Length is <= 0 which would cause DivByZero-exception or other nasty errors (avoid this)");
			return path;
		}
		int num = 0;
		for (int i = 0; i < path.Count - 1; i++)
		{
			float magnitude = (path[i] - path[i + 1]).magnitude;
			for (float num2 = 0f; num2 <= magnitude; num2 += this.maxSegmentLength)
			{
				num++;
			}
		}
		List<Vector3> list = ListPool<Vector3>.Claim(num);
		Vector3 vector = (path[1] - path[0]).normalized;
		for (int j = 0; j < path.Count - 1; j++)
		{
			float magnitude2 = (path[j] - path[j + 1]).magnitude;
			Vector3 a = vector;
			Vector3 vector2 = (j >= path.Count - 2) ? (path[j + 1] - path[j]).normalized : ((path[j + 2] - path[j + 1]).normalized - (path[j] - path[j + 1]).normalized).normalized;
			Vector3 tan = a * magnitude2 * this.factor;
			Vector3 tan2 = vector2 * magnitude2 * this.factor;
			Vector3 a2 = path[j];
			Vector3 b = path[j + 1];
			float num3 = 1f / magnitude2;
			for (float num4 = 0f; num4 <= magnitude2; num4 += this.maxSegmentLength)
			{
				float t = num4 * num3;
				list.Add(SimpleSmoothModifier.GetPointOnCubic(a2, b, tan, tan2, t));
			}
			vector = vector2;
		}
		list[list.Count - 1] = path[path.Count - 1];
		return list;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x000311C4 File Offset: 0x0002F5C4
	public static Vector3 GetPointOnCubic(Vector3 a, Vector3 b, Vector3 tan1, Vector3 tan2, float t)
	{
		float num = t * t;
		float num2 = num * t;
		float d = 2f * num2 - 3f * num + 1f;
		float d2 = -2f * num2 + 3f * num;
		float d3 = num2 - 2f * num + t;
		float d4 = num2 - num;
		return d * a + d2 * b + d3 * tan1 + d4 * tan2;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00031244 File Offset: 0x0002F644
	public List<Vector3> SmoothOffsetSimple(List<Vector3> path)
	{
		if (path.Count <= 2 || this.iterations <= 0)
		{
			return path;
		}
		if (this.iterations > 12)
		{
			Debug.LogWarning("A very high iteration count was passed, won't let this one through");
			return path;
		}
		int num = (path.Count - 2) * (int)Mathf.Pow(2f, (float)this.iterations) + 2;
		List<Vector3> list = ListPool<Vector3>.Claim(num);
		List<Vector3> list2 = ListPool<Vector3>.Claim(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(Vector3.zero);
			list2.Add(Vector3.zero);
		}
		for (int j = 0; j < path.Count; j++)
		{
			list[j] = path[j];
		}
		for (int k = 0; k < this.iterations; k++)
		{
			int num2 = (path.Count - 2) * (int)Mathf.Pow(2f, (float)k) + 2;
			List<Vector3> list3 = list;
			list = list2;
			list2 = list3;
			float d = 1f;
			for (int l = 0; l < num2 - 1; l++)
			{
				Vector3 vector = list2[l];
				Vector3 vector2 = list2[l + 1];
				Vector3 normalized = Vector3.Cross(vector2 - vector, Vector3.up).normalized;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				if (l != 0 && !Polygon.IsColinear(vector, vector2, list2[l - 1]))
				{
					flag3 = true;
					flag = Polygon.Left(vector, vector2, list2[l - 1]);
				}
				if (l < num2 - 1 && !Polygon.IsColinear(vector, vector2, list2[l + 2]))
				{
					flag4 = true;
					flag2 = Polygon.Left(vector, vector2, list2[l + 2]);
				}
				if (flag3)
				{
					list[l * 2] = vector + ((!flag) ? (-normalized * this.offset * d) : (normalized * this.offset * d));
				}
				else
				{
					list[l * 2] = vector;
				}
				if (flag4)
				{
					list[l * 2 + 1] = vector2 + ((!flag2) ? (-normalized * this.offset * d) : (normalized * this.offset * d));
				}
				else
				{
					list[l * 2 + 1] = vector2;
				}
			}
			list[(path.Count - 2) * (int)Mathf.Pow(2f, (float)(k + 1)) + 2 - 1] = list2[num2 - 1];
		}
		ListPool<Vector3>.Release(list2);
		return list;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00031510 File Offset: 0x0002F910
	public List<Vector3> SmoothSimple(List<Vector3> path)
	{
		if (path.Count <= 2)
		{
			return path;
		}
		if (this.uniformLength)
		{
			int num = 0;
			this.maxSegmentLength = ((this.maxSegmentLength >= 0.005f) ? this.maxSegmentLength : 0.005f);
			for (int i = 0; i < path.Count - 1; i++)
			{
				float num2 = Vector3.Distance(path[i], path[i + 1]);
				num += Mathf.FloorToInt(num2 / this.maxSegmentLength);
			}
			List<Vector3> list = ListPool<Vector3>.Claim(num + 1);
			int num3 = 0;
			float num4 = 0f;
			for (int j = 0; j < path.Count - 1; j++)
			{
				float num5 = Vector3.Distance(path[j], path[j + 1]);
				int num6 = Mathf.FloorToInt((num5 + num4) / this.maxSegmentLength);
				float num7 = num4 / num5;
				Vector3 a = path[j + 1] - path[j];
				for (int k = 0; k < num6; k++)
				{
					list.Add(a * ((float)k / (float)num6 - num7) + path[j]);
					num3++;
				}
				num4 = (num5 + num4) % this.maxSegmentLength;
			}
			list.Add(path[path.Count - 1]);
			for (int l = 0; l < this.iterations; l++)
			{
				Vector3 a2 = list[0];
				for (int m = 1; m < list.Count - 1; m++)
				{
					Vector3 vector = list[m];
					list[m] = Vector3.Lerp(vector, (a2 + list[m + 1]) / 2f, this.strength);
					a2 = vector;
				}
			}
			return list;
		}
		List<Vector3> list2 = ListPool<Vector3>.Claim();
		if (this.subdivisions < 0)
		{
			this.subdivisions = 0;
		}
		int num8 = 1 << this.subdivisions;
		for (int n = 0; n < path.Count - 1; n++)
		{
			for (int num9 = 0; num9 < num8; num9++)
			{
				list2.Add(Vector3.Lerp(path[n], path[n + 1], (float)num9 / (float)num8));
			}
		}
		for (int num10 = 0; num10 < this.iterations; num10++)
		{
			Vector3 a3 = list2[0];
			for (int num11 = 1; num11 < list2.Count - 1; num11++)
			{
				Vector3 vector2 = list2[num11];
				list2[num11] = Vector3.Lerp(vector2, (a3 + list2[num11 + 1]) / 2f, this.strength);
				a3 = vector2;
			}
		}
		return list2;
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00031804 File Offset: 0x0002FC04
	public List<Vector3> SmoothBezier(List<Vector3> path)
	{
		if (this.subdivisions < 0)
		{
			this.subdivisions = 0;
		}
		int num = 1 << this.subdivisions;
		List<Vector3> list = ListPool<Vector3>.Claim();
		for (int i = 0; i < path.Count - 1; i++)
		{
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			if (i == 0)
			{
				vector = path[i + 1] - path[i];
			}
			else
			{
				vector = path[i + 1] - path[i - 1];
			}
			if (i == path.Count - 2)
			{
				vector2 = path[i] - path[i + 1];
			}
			else
			{
				vector2 = path[i] - path[i + 2];
			}
			vector *= this.bezierTangentLength;
			vector2 *= this.bezierTangentLength;
			Vector3 vector3 = path[i];
			Vector3 p = vector3 + vector;
			Vector3 vector4 = path[i + 1];
			Vector3 p2 = vector4 + vector2;
			for (int j = 0; j < num; j++)
			{
				list.Add(Mathfx.CubicBezier(vector3, p, p2, vector4, (float)j / (float)num));
			}
		}
		list.Add(path[path.Count - 1]);
		return list;
	}

	// Token: 0x0400042E RID: 1070
	public SimpleSmoothModifier.SmoothType smoothType;

	// Token: 0x0400042F RID: 1071
	public int subdivisions = 2;

	// Token: 0x04000430 RID: 1072
	public int iterations = 2;

	// Token: 0x04000431 RID: 1073
	public float strength = 0.5f;

	// Token: 0x04000432 RID: 1074
	public bool uniformLength = true;

	// Token: 0x04000433 RID: 1075
	public float maxSegmentLength = 2f;

	// Token: 0x04000434 RID: 1076
	public float bezierTangentLength = 0.4f;

	// Token: 0x04000435 RID: 1077
	public float offset = 0.2f;

	// Token: 0x04000436 RID: 1078
	public float factor = 0.1f;

	// Token: 0x020000A3 RID: 163
	public enum SmoothType
	{
		// Token: 0x04000438 RID: 1080
		Simple,
		// Token: 0x04000439 RID: 1081
		Bezier,
		// Token: 0x0400043A RID: 1082
		OffsetSimple,
		// Token: 0x0400043B RID: 1083
		CurvedNonuniform
	}
}
