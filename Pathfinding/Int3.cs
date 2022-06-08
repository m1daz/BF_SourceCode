using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200002E RID: 46
	public struct Int3
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0000B9B8 File Offset: 0x00009DB8
		public Int3(Vector3 position)
		{
			this.x = (int)Math.Round((double)(position.x * 1000f));
			this.y = (int)Math.Round((double)(position.y * 1000f));
			this.z = (int)Math.Round((double)(position.z * 1000f));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000BA13 File Offset: 0x00009E13
		public Int3(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000BA2A File Offset: 0x00009E2A
		public static Int3 zero
		{
			get
			{
				return Int3._zero;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000BA31 File Offset: 0x00009E31
		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000BA6C File Offset: 0x00009E6C
		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000BAAC File Offset: 0x00009EAC
		public static explicit operator Int3(Vector3 ob)
		{
			return new Int3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000BAFA File Offset: 0x00009EFA
		public static explicit operator Vector3(Int3 ob)
		{
			return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000BB2C File Offset: 0x00009F2C
		public static Int3 operator -(Int3 lhs, Int3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000BB7C File Offset: 0x00009F7C
		public static Int3 operator +(Int3 lhs, Int3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000BBC9 File Offset: 0x00009FC9
		public static Int3 operator *(Int3 lhs, int rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BBFC File Offset: 0x00009FFC
		public static Int3 operator *(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
			return lhs;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BC54 File Offset: 0x0000A054
		public static Int3 operator *(Int3 lhs, Vector3 rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs.x));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs.y));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs.z));
			return lhs;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000BCBC File Offset: 0x0000A0BC
		public static Int3 operator /(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
			return lhs;
		}

		// Token: 0x17000014 RID: 20
		public int this[int i]
		{
			get
			{
				return (i != 0) ? ((i != 1) ? this.z : this.y) : this.x;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000BD3D File Offset: 0x0000A13D
		public static int Dot(Int3 lhs, Int3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000BD70 File Offset: 0x0000A170
		public Int3 NormalizeTo(int newMagn)
		{
			float magnitude = this.magnitude;
			if (magnitude == 0f)
			{
				return this;
			}
			this.x *= newMagn;
			this.y *= newMagn;
			this.z *= newMagn;
			this.x = (int)Math.Round((double)((float)this.x / magnitude));
			this.y = (int)Math.Round((double)((float)this.y / magnitude));
			this.z = (int)Math.Round((double)((float)this.z / magnitude));
			return this;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000BE08 File Offset: 0x0000A208
		public float magnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000BE3E File Offset: 0x0000A23E
		public int costMagnitude
		{
			get
			{
				return (int)Math.Round((double)this.magnitude);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000BE50 File Offset: 0x0000A250
		public float worldMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3) * 0.001f;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000BE8C File Offset: 0x0000A28C
		public float sqrMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000BEBD File Offset: 0x0000A2BD
		public int unsafeSqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000BEE8 File Offset: 0x0000A2E8
		[Obsolete("Same implementation as .magnitude")]
		public float safeMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000BF20 File Offset: 0x0000A320
		[Obsolete(".sqrMagnitude is now per default safe (.unsafeSqrMagnitude can be used for unsafe operations)")]
		public float safeSqrMagnitude
		{
			get
			{
				float num = (float)this.x * 0.001f;
				float num2 = (float)this.y * 0.001f;
				float num3 = (float)this.z * 0.001f;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000BF62 File Offset: 0x0000A362
		public static implicit operator string(Int3 ob)
		{
			return ob.ToString();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000BF74 File Offset: 0x0000A374
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.x,
				", ",
				this.y,
				", ",
				this.z,
				")"
			});
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000BFD8 File Offset: 0x0000A3D8
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Int3 @int = (Int3)o;
			return this.x == @int.x && this.y == @int.y && this.z == @int.z;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C02A File Offset: 0x0000A42A
		public override int GetHashCode()
		{
			return this.x * 9 + this.y * 10 + this.z * 11;
		}

		// Token: 0x04000161 RID: 353
		public int x;

		// Token: 0x04000162 RID: 354
		public int y;

		// Token: 0x04000163 RID: 355
		public int z;

		// Token: 0x04000164 RID: 356
		public const int Precision = 1000;

		// Token: 0x04000165 RID: 357
		public const float FloatPrecision = 1000f;

		// Token: 0x04000166 RID: 358
		public const float PrecisionFactor = 0.001f;

		// Token: 0x04000167 RID: 359
		private static Int3 _zero = new Int3(0, 0, 0);
	}
}
