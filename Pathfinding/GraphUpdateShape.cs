using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000027 RID: 39
	public class GraphUpdateShape
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000A9D8 File Offset: 0x00008DD8
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000A9E0 File Offset: 0x00008DE0
		public Vector3[] points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				if (this.convex)
				{
					this.CalculateConvexHull();
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000A9FA File Offset: 0x00008DFA
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000AA02 File Offset: 0x00008E02
		public bool convex
		{
			get
			{
				return this._convex;
			}
			set
			{
				if (this._convex != value && value)
				{
					this._convex = value;
					this.CalculateConvexHull();
				}
				else
				{
					this._convex = value;
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AA30 File Offset: 0x00008E30
		private void CalculateConvexHull()
		{
			if (this.points == null)
			{
				this._convexPoints = null;
				return;
			}
			this._convexPoints = Polygon.ConvexHull(this.points);
			for (int i = 0; i < this._convexPoints.Length; i++)
			{
				Debug.DrawLine(this._convexPoints[i], this._convexPoints[(i + 1) % this._convexPoints.Length], Color.green);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AAB4 File Offset: 0x00008EB4
		public Bounds GetBounds()
		{
			if (this.points == null || this.points.Length == 0)
			{
				return default(Bounds);
			}
			Vector3 vector = this.points[0];
			Vector3 vector2 = this.points[0];
			for (int i = 0; i < this.points.Length; i++)
			{
				vector = Vector3.Min(vector, this.points[i]);
				vector2 = Vector3.Max(vector2, this.points[i]);
			}
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AB70 File Offset: 0x00008F70
		public bool Contains(Node node)
		{
			Vector3 p = (Vector3)node.position;
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPoint(this._points, p);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (Polygon.Left(this._convexPoints[i], this._convexPoints[num], p))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AC18 File Offset: 0x00009018
		public bool Contains(Vector3 point)
		{
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPoint(this._points, point);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (Polygon.Left(this._convexPoints[i], this._convexPoints[num], point))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x04000121 RID: 289
		private Vector3[] _points;

		// Token: 0x04000122 RID: 290
		private Vector3[] _convexPoints;

		// Token: 0x04000123 RID: 291
		private bool _convex;
	}
}
