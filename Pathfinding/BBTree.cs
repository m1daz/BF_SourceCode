using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007C RID: 124
	public class BBTree
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x00025F40 File Offset: 0x00024340
		public BBTree(INavmesh graph)
		{
			this.graph = graph;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00025F50 File Offset: 0x00024350
		public NNInfo Query(Vector3 p, NNConstraint constraint)
		{
			BBTreeBox bbtreeBox = this.root;
			if (bbtreeBox == null)
			{
				return default(NNInfo);
			}
			NNInfo result = default(NNInfo);
			this.SearchBox(bbtreeBox, p, constraint, ref result);
			result.UpdateInfo();
			return result;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00025F90 File Offset: 0x00024390
		public NNInfo QueryCircle(Vector3 p, float radius, NNConstraint constraint)
		{
			BBTreeBox bbtreeBox = this.root;
			if (bbtreeBox == null)
			{
				return default(NNInfo);
			}
			NNInfo result = new NNInfo(null);
			this.SearchBoxCircle(bbtreeBox, p, radius, constraint, ref result);
			result.UpdateInfo();
			return result;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00025FD0 File Offset: 0x000243D0
		public void SearchBoxCircle(BBTreeBox box, Vector3 p, float radius, NNConstraint constraint, ref NNInfo nnInfo)
		{
			if (box.node != null)
			{
				if (this.NodeIntersectsCircle(box.node, p, radius))
				{
					Vector3 vector = NavMeshGraph.ClosestPointOnNode(box.node, this.graph.vertices, p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (nnInfo.node == null)
					{
						nnInfo.node = box.node;
						nnInfo.clampedPosition = vector;
					}
					else if (sqrMagnitude < (nnInfo.clampedPosition - p).sqrMagnitude)
					{
						nnInfo.node = box.node;
						nnInfo.clampedPosition = vector;
					}
					if (constraint.Suitable(box.node))
					{
						if (nnInfo.constrainedNode == null)
						{
							nnInfo.constrainedNode = box.node;
							nnInfo.constClampedPosition = vector;
						}
						else if (sqrMagnitude < (nnInfo.constClampedPosition - p).sqrMagnitude)
						{
							nnInfo.constrainedNode = box.node;
							nnInfo.constClampedPosition = vector;
						}
					}
				}
				return;
			}
			if (this.RectIntersectsCircle(box.c1.rect, p, radius))
			{
				this.SearchBoxCircle(box.c1, p, radius, constraint, ref nnInfo);
			}
			if (this.RectIntersectsCircle(box.c2.rect, p, radius))
			{
				this.SearchBoxCircle(box.c2, p, radius, constraint, ref nnInfo);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0002613C File Offset: 0x0002453C
		public void SearchBox(BBTreeBox box, Vector3 p, NNConstraint constraint, ref NNInfo nnInfo)
		{
			if (box.node != null)
			{
				if (NavMeshGraph.ContainsPoint(box.node, p, this.graph.vertices))
				{
					if (nnInfo.node == null)
					{
						nnInfo.node = box.node;
					}
					else if (Mathf.Abs(((Vector3)box.node.position).y - p.y) < Mathf.Abs(((Vector3)nnInfo.node.position).y - p.y))
					{
						nnInfo.node = box.node;
					}
					if (constraint.Suitable(box.node))
					{
						if (nnInfo.constrainedNode == null)
						{
							nnInfo.constrainedNode = box.node;
						}
						else if (Mathf.Abs((float)box.node.position.y - p.y) < Mathf.Abs((float)nnInfo.constrainedNode.position.y - p.y))
						{
							nnInfo.constrainedNode = box.node;
						}
					}
				}
				return;
			}
			if (this.RectContains(box.c1.rect, p))
			{
				this.SearchBox(box.c1, p, constraint, ref nnInfo);
			}
			if (this.RectContains(box.c2.rect, p))
			{
				this.SearchBox(box.c2, p, constraint, ref nnInfo);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000262B8 File Offset: 0x000246B8
		public void Insert(MeshNode node)
		{
			BBTreeBox bbtreeBox = new BBTreeBox(this, node);
			if (this.root == null)
			{
				this.root = bbtreeBox;
				return;
			}
			BBTreeBox bbtreeBox2 = this.root;
			for (;;)
			{
				bbtreeBox2.rect = this.ExpandToContain(bbtreeBox2.rect, bbtreeBox.rect);
				if (bbtreeBox2.node != null)
				{
					break;
				}
				float num = this.ExpansionRequired(bbtreeBox2.c1.rect, bbtreeBox.rect);
				float num2 = this.ExpansionRequired(bbtreeBox2.c2.rect, bbtreeBox.rect);
				if (num < num2)
				{
					bbtreeBox2 = bbtreeBox2.c1;
				}
				else if (num2 < num)
				{
					bbtreeBox2 = bbtreeBox2.c2;
				}
				else
				{
					bbtreeBox2 = ((this.RectArea(bbtreeBox2.c1.rect) >= this.RectArea(bbtreeBox2.c2.rect)) ? bbtreeBox2.c2 : bbtreeBox2.c1);
				}
			}
			bbtreeBox2.c1 = bbtreeBox;
			BBTreeBox c = new BBTreeBox(this, bbtreeBox2.node);
			bbtreeBox2.c2 = c;
			bbtreeBox2.node = null;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000263C3 File Offset: 0x000247C3
		public void OnDrawGizmos()
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000263C8 File Offset: 0x000247C8
		public void OnDrawGizmos(BBTreeBox box)
		{
			if (box == null)
			{
				return;
			}
			Vector3 a = new Vector3(box.rect.xMin, 0f, box.rect.yMin);
			Vector3 vector = new Vector3(box.rect.xMax, 0f, box.rect.yMax);
			Vector3 vector2 = (a + vector) * 0.5f;
			Vector3 size = (vector - vector2) * 2f;
			Gizmos.DrawCube(vector2, size);
			this.OnDrawGizmos(box.c1);
			this.OnDrawGizmos(box.c2);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00026464 File Offset: 0x00024864
		public void TestIntersections(Vector3 p, float radius)
		{
			BBTreeBox box = this.root;
			this.TestIntersections(box, p, radius);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00026481 File Offset: 0x00024881
		public void TestIntersections(BBTreeBox box, Vector3 p, float radius)
		{
			if (box == null)
			{
				return;
			}
			this.RectIntersectsCircle(box.rect, p, radius);
			this.TestIntersections(box.c1, p, radius);
			this.TestIntersections(box.c2, p, radius);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000264B8 File Offset: 0x000248B8
		public bool NodeIntersectsCircle(MeshNode node, Vector3 p, float radius)
		{
			if (NavMeshGraph.ContainsPoint(node, p, this.graph.vertices))
			{
				return true;
			}
			Int3[] vertices = this.graph.vertices;
			Vector3 vector = (Vector3)vertices[node[0]];
			Vector3 vector2 = (Vector3)vertices[node[1]];
			Vector3 vector3 = (Vector3)vertices[node[2]];
			float num = radius * radius;
			vector.y = p.y;
			vector2.y = p.y;
			vector3.y = p.y;
			return Mathfx.DistancePointSegmentStrict(vector, vector2, p) < num || Mathfx.DistancePointSegmentStrict(vector2, vector3, p) < num || Mathfx.DistancePointSegmentStrict(vector3, vector, p) < num;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00026590 File Offset: 0x00024990
		public bool RectIntersectsCircle(Rect r, Vector3 p, float radius)
		{
			return this.RectContains(r, p) || this.XIntersectsCircle(r.xMin, r.xMax, r.yMin, p, radius) || this.XIntersectsCircle(r.xMin, r.xMax, r.yMax, p, radius) || this.ZIntersectsCircle(r.yMin, r.yMax, r.xMin, p, radius) || this.ZIntersectsCircle(r.yMin, r.yMax, r.xMax, p, radius);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00026634 File Offset: 0x00024A34
		public bool RectContains(Rect r, Vector3 p)
		{
			return p.x >= r.xMin && p.x <= r.xMax && p.z >= r.yMin && p.z <= r.yMax;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00026690 File Offset: 0x00024A90
		public bool ZIntersectsCircle(float z1, float z2, float xpos, Vector3 circle, float radius)
		{
			double num = (double)(Math.Abs(xpos - circle.x) / radius);
			if (num > 1.0 || num < -1.0)
			{
				return false;
			}
			double a = Math.Acos(num);
			float num2 = (float)Math.Sin(a) * radius;
			float val = circle.z - num2;
			num2 += circle.z;
			float num3 = Math.Min(num2, val);
			float num4 = Math.Max(num2, val);
			num3 = Mathf.Max(z1, num3);
			num4 = Mathf.Min(z2, num4);
			return num4 > num3;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00026724 File Offset: 0x00024B24
		public bool XIntersectsCircle(float x1, float x2, float zpos, Vector3 circle, float radius)
		{
			double num = (double)(Math.Abs(zpos - circle.z) / radius);
			if (num > 1.0 || num < -1.0)
			{
				return false;
			}
			double d = Math.Asin(num);
			float num2 = (float)Math.Cos(d) * radius;
			float val = circle.x - num2;
			num2 += circle.x;
			float num3 = Math.Min(num2, val);
			float num4 = Math.Max(num2, val);
			num3 = Mathf.Max(x1, num3);
			num4 = Mathf.Min(x2, num4);
			return num4 > num3;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000267B8 File Offset: 0x00024BB8
		public float ExpansionRequired(Rect r, Rect r2)
		{
			float num = Mathf.Min(r.xMin, r2.xMin);
			float num2 = Mathf.Max(r.xMax, r2.xMax);
			float num3 = Mathf.Min(r.yMin, r2.yMin);
			float num4 = Mathf.Max(r.yMax, r2.yMax);
			return (num2 - num) * (num4 - num3) - this.RectArea(r);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00026824 File Offset: 0x00024C24
		public Rect ExpandToContain(Rect r, Rect r2)
		{
			float xmin = Mathf.Min(r.xMin, r2.xMin);
			float xmax = Mathf.Max(r.xMax, r2.xMax);
			float ymin = Mathf.Min(r.yMin, r2.yMin);
			float ymax = Mathf.Max(r.yMax, r2.yMax);
			return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0002688A File Offset: 0x00024C8A
		public float RectArea(Rect r)
		{
			return r.width * r.height;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0002689C File Offset: 0x00024C9C
		public new void ToString()
		{
			Console.WriteLine("Root " + ((this.root.node == null) ? string.Empty : this.root.node.ToString()));
			BBTreeBox bbtreeBox = this.root;
			Stack<BBTreeBox> stack = new Stack<BBTreeBox>();
			stack.Push(bbtreeBox);
			bbtreeBox.WriteChildren(0);
		}

		// Token: 0x04000353 RID: 851
		public BBTreeBox root;

		// Token: 0x04000354 RID: 852
		public INavmesh graph;
	}
}
