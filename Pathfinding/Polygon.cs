using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000023 RID: 35
	public class Polygon
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00005F78 File Offset: 0x00004378
		public static long TriangleArea2(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005FC8 File Offset: 0x000043C8
		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006014 File Offset: 0x00004414
		public static long TriangleArea(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006064 File Offset: 0x00004464
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000060B0 File Offset: 0x000044B0
		public static bool ContainsPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return Polygon.IsClockwiseMargin(a, b, p) && Polygon.IsClockwiseMargin(b, c, p) && Polygon.IsClockwiseMargin(c, a, p);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000060D8 File Offset: 0x000044D8
		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[i].y)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[num].y - polyPoints[i].y) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000061CC File Offset: 0x000045CC
		public static bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].z <= p.z && p.z < polyPoints[num].z) || (polyPoints[num].z <= p.z && p.z < polyPoints[i].z)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[num].z - polyPoints[i].z) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000062C0 File Offset: 0x000046C0
		public static bool Left(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) <= 0f;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006318 File Offset: 0x00004718
		public static bool Left(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) <= 0L;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006370 File Offset: 0x00004770
		public static bool IsClockwiseMargin(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) <= float.Epsilon;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000063C8 File Offset: 0x000047C8
		public static bool IsClockwise(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) < 0f;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000641C File Offset: 0x0000481C
		public static bool IsClockwise(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) < 0L;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006470 File Offset: 0x00004870
		public static bool IsColinear(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) == 0L;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000064C4 File Offset: 0x000048C4
		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			return Mathf.Approximately((b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z), 0f);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000651A File Offset: 0x0000491A
		public static bool IntersectsUnclamped(Vector3 a, Vector3 b, Vector3 a2, Vector3 b2)
		{
			return Polygon.Left(a, b, a2) != Polygon.Left(a, b, b2);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006534 File Offset: 0x00004934
		public static bool Intersects(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				return false;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			return num4 >= 0f && num4 <= 1f && num5 >= 0f && num5 <= 1f;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006620 File Offset: 0x00004A20
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
		{
			float num = dir2.z * dir1.x - dir2.x * dir1.z;
			if (num == 0f)
			{
				return start1;
			}
			float num2 = dir2.x * (start1.z - start2.z) - dir2.z * (start1.x - start2.x);
			float d = num2 / num;
			return start1 + dir1 * d;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000669C File Offset: 0x00004A9C
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2, out bool intersects)
		{
			float num = dir2.z * dir1.x - dir2.x * dir1.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = dir2.x * (start1.z - start2.z) - dir2.z * (start1.x - start2.x);
			float d = num2 / num;
			intersects = true;
			return start1 + dir1 * d;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006720 File Offset: 0x00004B20
		public static bool IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out float factor1, out float factor2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				factor1 = 0f;
				factor2 = 0f;
				return false;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			factor1 = num4;
			factor2 = num5;
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000067F4 File Offset: 0x00004BF4
		public static float IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				return -1f;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			return num2 / num;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000687C File Offset: 0x00004C7C
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			bool flag;
			return Polygon.IntersectionPoint(start1, end1, start2, end2, out flag);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006894 File Offset: 0x00004C94
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			Vector3 a = end1 - start1;
			Vector3 vector = end2 - start2;
			float num = vector.z * a.x - vector.x * a.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float d = num2 / num;
			intersects = true;
			return start1 + a * d;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000692C File Offset: 0x00004D2C
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			bool flag;
			return Polygon.IntersectionPoint(start1, end1, start2, end2, out flag);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006944 File Offset: 0x00004D44
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out bool intersects)
		{
			Vector2 a = end1 - start1;
			Vector2 vector = end2 - start2;
			float num = vector.y * a.x - vector.x * a.y;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector.x * (start1.y - start2.y) - vector.y * (start1.x - start2.x);
			float d = num2 / num;
			intersects = true;
			return start1 + a * d;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000069DC File Offset: 0x00004DDC
		public static Vector3 SegmentIntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			Vector3 a = end1 - start1;
			Vector3 vector = end2 - start2;
			float num = vector.z * a.x - vector.x * a.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num3 = a.x * (start1.z - start2.z) - a.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			if (num4 < 0f || num4 > 1f || num5 < 0f || num5 > 1f)
			{
				intersects = false;
				return start1;
			}
			intersects = true;
			return start1 + a * num4;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006AE0 File Offset: 0x00004EE0
		public static Vector3[] ConvexHull(Vector3[] points)
		{
			if (points.Length == 0)
			{
				return new Vector3[0];
			}
			object obj = Polygon.hullCache;
			Vector3[] result;
			lock (obj)
			{
				List<Vector3> list = Polygon.hullCache;
				list.Clear();
				int num = 0;
				for (int i = 1; i < points.Length; i++)
				{
					if (points[i].x < points[num].x)
					{
						num = i;
					}
				}
				int num2 = num;
				int num3 = 0;
				for (;;)
				{
					list.Add(points[num]);
					int num4 = 0;
					for (int j = 0; j < points.Length; j++)
					{
						if (num4 == num || !Polygon.Left(points[num], points[num4], points[j]))
						{
							num4 = j;
						}
					}
					num = num4;
					num3++;
					if (num3 > 10000)
					{
						break;
					}
					if (num == num2)
					{
						goto IL_F7;
					}
				}
				Debug.LogWarning("Infinite Loop in Convex Hull Calculation");
				IL_F7:
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006C0C File Offset: 0x0000500C
		public static bool LineIntersectsBounds(Bounds bounds, Vector3 a, Vector3 b)
		{
			a -= bounds.center;
			b -= bounds.center;
			Vector3 b2 = (a + b) * 0.5f;
			Vector3 vector = a - b2;
			Vector3 vector2 = new Vector3(Math.Abs(vector.x), Math.Abs(vector.y), Math.Abs(vector.z));
			Vector3 extents = bounds.extents;
			return Math.Abs(b2.x) <= extents.x + vector2.x && Math.Abs(b2.y) <= extents.y + vector2.y && Math.Abs(b2.z) <= extents.z + vector2.z && Math.Abs(b2.y * vector.z - b2.z * vector.y) <= extents.y * vector2.z + extents.z * vector2.y && Math.Abs(b2.x * vector.z - b2.z * vector.x) <= extents.x * vector2.z + extents.z * vector2.x && Math.Abs(b2.x * vector.y - b2.y * vector.x) <= extents.x * vector2.y + extents.y * vector2.x;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006DC9 File Offset: 0x000051C9
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006DFC File Offset: 0x000051FC
		public static Vector3[] Subdivide(Vector3[] path, int subdivisions)
		{
			subdivisions = ((subdivisions >= 0) ? subdivisions : 0);
			if (subdivisions == 0)
			{
				return path;
			}
			Vector3[] array = new Vector3[(path.Length - 1) * (int)Mathf.Pow(2f, (float)subdivisions) + 1];
			int num = 0;
			for (int i = 0; i < path.Length - 1; i++)
			{
				float num2 = 1f / Mathf.Pow(2f, (float)subdivisions);
				for (float num3 = 0f; num3 < 1f; num3 += num2)
				{
					array[num] = Vector3.Lerp(path[i], path[i + 1], Mathf.SmoothStep(0f, 1f, num3));
					num++;
				}
			}
			array[num] = path[path.Length - 1];
			return array;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006EE2 File Offset: 0x000052E2
		public static Vector3 ClosestPointOnTriangle(Vector3[] triangle, Vector3 point)
		{
			return Polygon.ClosestPointOnTriangle(triangle[0], triangle[1], triangle[2], point);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006F10 File Offset: 0x00005310
		public static Vector3 ClosestPointOnTriangle(Vector3 tr0, Vector3 tr1, Vector3 tr2, Vector3 point)
		{
			Vector3 vector = tr1 - tr0;
			Vector3 vector2 = tr2 - tr0;
			Vector3 rhs = tr0 - point;
			float num = Vector3.Dot(vector, vector);
			float num2 = Vector3.Dot(vector, vector2);
			float num3 = Vector3.Dot(vector2, vector2);
			float num4 = Vector3.Dot(vector, rhs);
			float num5 = Vector3.Dot(vector2, rhs);
			float num6 = num * num3 - num2 * num2;
			float num7 = num2 * num5 - num3 * num4;
			float num8 = num2 * num4 - num * num5;
			if (num7 + num8 < num6)
			{
				if (num7 < 0f)
				{
					if (num8 < 0f)
					{
						if (num4 < 0f)
						{
							num7 = Mathfx.Clamp01(-num4 / num);
							num8 = 0f;
						}
						else
						{
							num7 = 0f;
							num8 = Mathfx.Clamp01(-num5 / num3);
						}
					}
					else
					{
						num7 = 0f;
						num8 = Mathfx.Clamp01(-num5 / num3);
					}
				}
				else if (num8 < 0f)
				{
					num7 = Mathfx.Clamp01(-num4 / num);
					num8 = 0f;
				}
				else
				{
					float num9 = 1f / num6;
					num7 *= num9;
					num8 *= num9;
				}
			}
			else if (num7 < 0f)
			{
				float num10 = num2 + num4;
				float num11 = num3 + num5;
				if (num11 > num10)
				{
					float num12 = num11 - num10;
					float num13 = num - 2f * num2 + num3;
					num7 = Mathfx.Clamp01(num12 / num13);
					num8 = 1f - num7;
				}
				else
				{
					num8 = Mathfx.Clamp01(-num5 / num3);
					num7 = 0f;
				}
			}
			else if (num8 < 0f)
			{
				if (num + num4 > num2 + num5)
				{
					float num14 = num3 + num5 - num2 - num4;
					float num15 = num - 2f * num2 + num3;
					num7 = Mathfx.Clamp01(num14 / num15);
					num8 = 1f - num7;
				}
				else
				{
					num7 = Mathfx.Clamp01(-num5 / num3);
					num8 = 0f;
				}
			}
			else
			{
				float num16 = num3 + num5 - num2 - num4;
				float num17 = num - 2f * num2 + num3;
				num7 = Mathfx.Clamp01(num16 / num17);
				num8 = 1f - num7;
			}
			return tr0 + num7 * vector + num8 * vector2;
		}

		// Token: 0x040000BF RID: 191
		public static List<Vector3> hullCache = new List<Vector3>();
	}
}
