using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000022 RID: 34
	public class Mathfx
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005738 File Offset: 0x00003B38
		public static int ComputeVertexHash(int x, int y, int z)
		{
			uint num = 2376512323U;
			uint num2 = 3625334849U;
			uint num3 = 3407524639U;
			uint num4 = (uint)((ulong)num * (ulong)((long)x) + (ulong)num2 * (ulong)((long)y) + (ulong)num3 * (ulong)((long)z));
			return (int)(num4 & 1073741823U);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005774 File Offset: 0x00003B74
		public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = Vector3.Normalize(lineEnd - lineStart);
			float d = Vector3.Dot(point - lineStart, vector);
			return lineStart + d * vector;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000057AC File Offset: 0x00003BAC
		public static float NearestPointFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = lineEnd - lineStart;
			float magnitude = vector.magnitude;
			vector /= magnitude;
			float num = Vector3.Dot(point - lineStart, vector);
			return num / magnitude;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000057E4 File Offset: 0x00003BE4
		public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 value = lineEnd - lineStart;
			Vector3 vector = Vector3.Normalize(value);
			float value2 = Vector3.Dot(point - lineStart, vector);
			return lineStart + Mathf.Clamp(value2, 0f, value.magnitude) * vector;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000582C File Offset: 0x00003C2C
		public static Vector3 NearestPointStrictXZ(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			lineStart.y = point.y;
			lineEnd.y = point.y;
			Vector3 vector = lineEnd - lineStart;
			Vector3 value = vector;
			value.y = 0f;
			Vector3 vector2 = Vector3.Normalize(value);
			float value2 = Vector3.Dot(point - lineStart, vector2);
			return lineStart + Mathf.Clamp(value2, 0f, value.magnitude) * vector2;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000058A0 File Offset: 0x00003CA0
		public static float DistancePointSegment(int x, int z, int px, int pz, int qx, int qz)
		{
			float num = (float)(qx - px);
			float num2 = (float)(qz - pz);
			float num3 = (float)(x - px);
			float num4 = (float)(z - pz);
			float num5 = num * num + num2 * num2;
			float num6 = num * num3 + num2 * num4;
			if (num5 > 0f)
			{
				num6 /= num5;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			num3 = (float)px + num6 * num - (float)x;
			num4 = (float)pz + num6 * num2 - (float)z;
			return num3 * num3 + num4 * num4;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005930 File Offset: 0x00003D30
		public static float DistancePointSegment2(int x, int z, int px, int pz, int qx, int qz)
		{
			Vector3 p = new Vector3((float)x, 0f, (float)z);
			Vector3 a = new Vector3((float)px, 0f, (float)pz);
			Vector3 b = new Vector3((float)qx, 0f, (float)qz);
			return Mathfx.DistancePointSegment2(a, b, p);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005978 File Offset: 0x00003D78
		public static float DistancePointSegment2(Vector3 a, Vector3 b, Vector3 p)
		{
			float num = b.x - a.x;
			float num2 = b.z - a.z;
			float num3 = Mathf.Abs(num * (p.z - a.z) - (p.x - a.x) * num2);
			float num4 = num * num + num2 * num2;
			if (num4 > 0f)
			{
				return num3 / Mathf.Sqrt(num4);
			}
			return (a - p).magnitude;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000059FC File Offset: 0x00003DFC
		public static float DistancePointSegmentStrict(Vector3 a, Vector3 b, Vector3 p)
		{
			Vector3 a2 = Mathfx.NearestPointStrict(a, b, p);
			return (a2 - p).sqrMagnitude;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005A21 File Offset: 0x00003E21
		public static float Hermite(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value * value * (3f - 2f * value));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005A3C File Offset: 0x00003E3C
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return Mathf.Pow(num, 3f) * p0 + 3f * Mathf.Pow(num, 2f) * t * p1 + 3f * num * Mathf.Pow(t, 2f) * p2 + Mathf.Pow(t, 3f) * p3;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005AC1 File Offset: 0x00003EC1
		public static float MapTo(float startMin, float startMax, float value)
		{
			value -= startMin;
			value /= startMax - startMin;
			value = Mathf.Clamp01(value);
			return value;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005AD8 File Offset: 0x00003ED8
		public static float MapToRange(float targetMin, float targetMax, float value)
		{
			value *= targetMax - targetMin;
			value += targetMin;
			return value;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005AE7 File Offset: 0x00003EE7
		public static float MapTo(float startMin, float startMax, float targetMin, float targetMax, float value)
		{
			value -= startMin;
			value /= startMax - startMin;
			value = Mathf.Clamp01(value);
			value *= targetMax - targetMin;
			value += targetMin;
			return value;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005B10 File Offset: 0x00003F10
		public static string FormatBytes(int bytes)
		{
			double num = (bytes < 0) ? -1.0 : 1.0;
			bytes = ((bytes < 0) ? (-bytes) : bytes);
			if (bytes < 1000)
			{
				return ((double)bytes * num).ToString() + " bytes";
			}
			if (bytes < 1000000)
			{
				return ((double)bytes / 1000.0 * num).ToString("0.0") + " kb";
			}
			if (bytes < 1000000000)
			{
				return ((double)bytes / 1000000.0 * num).ToString("0.0") + " mb";
			}
			return ((double)bytes / 1000000000.0 * num).ToString("0.0") + " gb";
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005C00 File Offset: 0x00004000
		public static string FormatBytesBinary(int bytes)
		{
			double num = (bytes < 0) ? -1.0 : 1.0;
			bytes = ((bytes < 0) ? (-bytes) : bytes);
			if (bytes < 1024)
			{
				return ((double)bytes * num).ToString() + " bytes";
			}
			if (bytes < 1024)
			{
				return ((double)bytes / 1024.0 * num).ToString("0.0") + " kb";
			}
			if (bytes < 1000000000)
			{
				return ((double)bytes / 1048576.0 * num).ToString("0.0") + " mb";
			}
			return ((double)bytes / 1073741824.0 * num).ToString("0.0") + " gb";
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005CED File Offset: 0x000040ED
		public static int Bit(int a, int b)
		{
			return a >> b & 1;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005CF8 File Offset: 0x000040F8
		public static Color IntToColor(int i, float a)
		{
			int num = Mathfx.Bit(i, 1) + Mathfx.Bit(i, 3) * 2 + 1;
			int num2 = Mathfx.Bit(i, 2) + Mathfx.Bit(i, 4) * 2 + 1;
			int num3 = Mathfx.Bit(i, 0) + Mathfx.Bit(i, 5) * 2 + 1;
			return new Color((float)num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005D60 File Offset: 0x00004160
		public static float MagnitudeXZ(Vector3 a, Vector3 b)
		{
			Vector3 vector = a - b;
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.z * vector.z));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005D9C File Offset: 0x0000419C
		public static float SqrMagnitudeXZ(Vector3 a, Vector3 b)
		{
			Vector3 vector = a - b;
			return vector.x * vector.x + vector.z * vector.z;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005DD0 File Offset: 0x000041D0
		public static int Repeat(int i, int n)
		{
			while (i >= n)
			{
				i -= n;
			}
			return i;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005DE4 File Offset: 0x000041E4
		public static float Abs(float a)
		{
			if (a < 0f)
			{
				return -a;
			}
			return a;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005DF5 File Offset: 0x000041F5
		public static int Abs(int a)
		{
			if (a < 0)
			{
				return -a;
			}
			return a;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005E02 File Offset: 0x00004202
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005E12 File Offset: 0x00004212
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005E22 File Offset: 0x00004222
		public static uint Min(uint a, uint b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005E32 File Offset: 0x00004232
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005E42 File Offset: 0x00004242
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005E52 File Offset: 0x00004252
		public static uint Max(uint a, uint b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005E62 File Offset: 0x00004262
		public static ushort Max(ushort a, ushort b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005E72 File Offset: 0x00004272
		public static float Sign(float a)
		{
			return (a >= 0f) ? 1f : -1f;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005E8E File Offset: 0x0000428E
		public static int Sign(int a)
		{
			return (a >= 0) ? 1 : -1;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005E9E File Offset: 0x0000429E
		public static float Clamp(float a, float b, float c)
		{
			return (a <= c) ? ((a >= b) ? a : b) : c;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005EBB File Offset: 0x000042BB
		public static int Clamp(int a, int b, int c)
		{
			return (a <= c) ? ((a >= b) ? a : b) : c;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005ED8 File Offset: 0x000042D8
		public static float Clamp01(float a)
		{
			return (a <= 1f) ? ((a >= 0f) ? a : 0f) : 1f;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005F05 File Offset: 0x00004305
		public static int Clamp01(int a)
		{
			return (a <= 1) ? ((a >= 0) ? a : 0) : 1;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005F22 File Offset: 0x00004322
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * ((t <= 1f) ? ((t >= 0f) ? t : 0f) : 1f);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005F55 File Offset: 0x00004355
		public static int RoundToInt(float v)
		{
			return (int)(v + 0.5f);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005F5F File Offset: 0x0000435F
		public static int RoundToInt(double v)
		{
			return (int)(v + 0.5);
		}
	}
}
