using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000082 RID: 130
	public class Utility
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00027E20 File Offset: 0x00026220
		public static Color GetColor(int i)
		{
			while (i >= Utility.colors.Length)
			{
				i -= Utility.colors.Length;
			}
			while (i < 0)
			{
				i += Utility.colors.Length;
			}
			return Utility.colors[i];
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00027E71 File Offset: 0x00026271
		public static int Bit(int a, int b)
		{
			return (a & 1 << b) >> b;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00027E80 File Offset: 0x00026280
		public static Color IntToColor(int i, float a)
		{
			int num = Utility.Bit(i, 1) + Utility.Bit(i, 3) * 2 + 1;
			int num2 = Utility.Bit(i, 2) + Utility.Bit(i, 4) * 2 + 1;
			int num3 = Utility.Bit(i, 0) + Utility.Bit(i, 5) * 2 + 1;
			return new Color((float)num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00027EE8 File Offset: 0x000262E8
		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return Mathf.Abs(a.x * b.z + b.x * c.z + c.x * a.z - a.x * c.z - c.x * b.z - b.x * a.z);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00027F5C File Offset: 0x0002635C
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00027FA8 File Offset: 0x000263A8
		public static float Min(float a, float b, float c)
		{
			a = ((a >= b) ? b : a);
			return (a >= c) ? c : a;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00027FC8 File Offset: 0x000263C8
		public static float Max(float a, float b, float c)
		{
			a = ((a <= b) ? b : a);
			return (a <= c) ? c : a;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00027FE8 File Offset: 0x000263E8
		public static int Max(int a, int b, int c, int d)
		{
			a = ((a <= b) ? b : a);
			a = ((a <= c) ? c : a);
			return (a <= d) ? d : a;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00028018 File Offset: 0x00026418
		public static int Min(int a, int b, int c, int d)
		{
			a = ((a >= b) ? b : a);
			a = ((a >= c) ? c : a);
			return (a >= d) ? d : a;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00028048 File Offset: 0x00026448
		public static float Max(float a, float b, float c, float d)
		{
			a = ((a <= b) ? b : a);
			a = ((a <= c) ? c : a);
			return (a <= d) ? d : a;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00028078 File Offset: 0x00026478
		public static float Min(float a, float b, float c, float d)
		{
			a = ((a >= b) ? b : a);
			a = ((a >= c) ? c : a);
			return (a >= d) ? d : a;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000280A8 File Offset: 0x000264A8
		public static string ToMillis(float v)
		{
			return (v * 1000f).ToString("0");
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000280C9 File Offset: 0x000264C9
		public static void StartTimer()
		{
			Utility.lastStartTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000280D5 File Offset: 0x000264D5
		public static void EndTimer(string label)
		{
			Debug.Log(label + ", process took " + Utility.ToMillis(Time.realtimeSinceStartup - Utility.lastStartTime) + "ms to complete");
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000280FC File Offset: 0x000264FC
		public static void StartTimerAdditive(bool reset)
		{
			if (reset)
			{
				Utility.additiveTimer = 0f;
			}
			Utility.lastAdditiveTimerStart = Time.realtimeSinceStartup;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00028118 File Offset: 0x00026518
		public static void EndTimerAdditive(string label, bool log)
		{
			Utility.additiveTimer += Time.realtimeSinceStartup - Utility.lastAdditiveTimerStart;
			if (log)
			{
				Debug.Log(label + ", process took " + Utility.ToMillis(Utility.additiveTimer) + "ms to complete");
			}
			Utility.lastAdditiveTimerStart = Time.realtimeSinceStartup;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0002816A File Offset: 0x0002656A
		public static void CopyVector(float[] a, int i, Vector3 v)
		{
			a[i] = v.x;
			a[i + 1] = v.y;
			a[i + 2] = v.z;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00028190 File Offset: 0x00026590
		public static int ClipPoly(float[] vIn, int n, float[] vOut, float pnx, float pnz, float pd)
		{
			float[] array = new float[12];
			for (int i = 0; i < n; i++)
			{
				array[i] = pnx * vIn[i * 3] + pnz * vIn[i * 3 + 2] + pd;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					float num3 = array[num2] / (array[num2] - array[j]);
					vOut[num * 3] = vIn[num2 * 3] + (vIn[j * 3] - vIn[num2 * 3]) * num3;
					vOut[num * 3 + 1] = vIn[num2 * 3 + 1] + (vIn[j * 3 + 1] - vIn[num2 * 3 + 1]) * num3;
					vOut[num * 3 + 2] = vIn[num2 * 3 + 2] + (vIn[j * 3 + 2] - vIn[num2 * 3 + 2]) * num3;
					num++;
				}
				if (flag2)
				{
					vOut[num * 3] = vIn[j * 3];
					vOut[num * 3 + 1] = vIn[j * 3 + 1];
					vOut[num * 3 + 2] = vIn[j * 3 + 2];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000282BC File Offset: 0x000266BC
		public static int ClipPolygon(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			float[] array = new float[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					int num3 = num * 3;
					int num4 = j * 3;
					int num5 = num2 * 3;
					float num6 = array[num2] / (array[num2] - array[j]);
					vOut[num3] = vIn[num5] + (vIn[num4] - vIn[num5]) * num6;
					vOut[num3 + 1] = vIn[num5 + 1] + (vIn[num4 + 1] - vIn[num5 + 1]) * num6;
					vOut[num3 + 2] = vIn[num5 + 2] + (vIn[num4 + 2] - vIn[num5 + 2]) * num6;
					num++;
				}
				if (flag2)
				{
					int num7 = num * 3;
					int num8 = j * 3;
					vOut[num7] = vIn[num8];
					vOut[num7 + 1] = vIn[num8 + 1];
					vOut[num7 + 2] = vIn[num8 + 2];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000283E0 File Offset: 0x000267E0
		public static int ClipPolygonY(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			float[] array = new float[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					vOut[num * 3 + 1] = vIn[num2 * 3 + 1] + (vIn[j * 3 + 1] - vIn[num2 * 3 + 1]) * (array[num2] / (array[num2] - array[j]));
					num++;
				}
				if (flag2)
				{
					vOut[num * 3 + 1] = vIn[j * 3 + 1];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000284A8 File Offset: 0x000268A8
		public static int ClipPolygon(Vector3[] vIn, int n, Vector3[] vOut, float multi, float offset, int axis)
		{
			float[] array = new float[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i][axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					float d = array[num2] / (array[num2] - array[j]);
					vOut[num] = vIn[num2] + (vIn[j] - vIn[num2]) * d;
					num++;
				}
				if (flag2)
				{
					vOut[num] = vIn[j];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000285A4 File Offset: 0x000269A4
		public static bool IntersectXAxis(out Vector3 intersection, Vector3 start1, Vector3 dir1, float x)
		{
			float x2 = dir1.x;
			if (x2 == 0f)
			{
				intersection = Vector3.zero;
				return false;
			}
			float num = x - start1.x;
			float num2 = num / x2;
			num2 = Mathf.Clamp01(num2);
			intersection = start1 + dir1 * num2;
			return true;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000285FC File Offset: 0x000269FC
		public static bool IntersectZAxis(out Vector3 intersection, Vector3 start1, Vector3 dir1, float z)
		{
			float num = -dir1.z;
			if (num == 0f)
			{
				intersection = Vector3.zero;
				return false;
			}
			float num2 = start1.z - z;
			float num3 = num2 / num;
			num3 = Mathf.Clamp01(num3);
			intersection = start1 + dir1 * num3;
			return true;
		}

		// Token: 0x04000369 RID: 873
		public static Color[] colors = new Color[]
		{
			Color.green,
			Color.blue,
			Color.red,
			Color.yellow,
			Color.cyan,
			Color.white,
			Color.black
		};

		// Token: 0x0400036A RID: 874
		public static float lastStartTime;

		// Token: 0x0400036B RID: 875
		public static float lastAdditiveTimerStart;

		// Token: 0x0400036C RID: 876
		public static float additiveTimer;
	}
}
