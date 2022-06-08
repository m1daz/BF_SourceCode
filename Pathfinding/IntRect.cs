using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000012 RID: 18
	public struct IntRect
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003F4F File Offset: 0x0000234F
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003F6E File Offset: 0x0000236E
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003FA3 File Offset: 0x000023A3
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003FCC File Offset: 0x000023CC
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			IntRect result = new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004030 File Offset: 0x00002430
		public static IntRect Union(IntRect a, IntRect b)
		{
			IntRect result = new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004091 File Offset: 0x00002491
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000040B8 File Offset: 0x000024B8
		public void DebugDraw(Matrix4x4 matrix, Color col)
		{
			Vector3 vector = matrix.MultiplyPoint3x4(new Vector3((float)this.xmin, 0f, (float)this.ymin));
			Vector3 vector2 = matrix.MultiplyPoint3x4(new Vector3((float)this.xmin, 0f, (float)this.ymax));
			Vector3 vector3 = matrix.MultiplyPoint3x4(new Vector3((float)this.xmax, 0f, (float)this.ymax));
			Vector3 vector4 = matrix.MultiplyPoint3x4(new Vector3((float)this.xmax, 0f, (float)this.ymin));
			Debug.DrawLine(vector, vector2, col);
			Debug.DrawLine(vector2, vector3, col);
			Debug.DrawLine(vector3, vector4, col);
			Debug.DrawLine(vector4, vector, col);
		}

		// Token: 0x04000080 RID: 128
		public int xmin;

		// Token: 0x04000081 RID: 129
		public int ymin;

		// Token: 0x04000082 RID: 130
		public int xmax;

		// Token: 0x04000083 RID: 131
		public int ymax;
	}
}
