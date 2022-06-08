using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000035 RID: 53
[AddComponentMenu("Pathfinding/Link")]
public class NodeLink : MonoBehaviour
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000D101 File Offset: 0x0000B501
	public Transform Start
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000D109 File Offset: 0x0000B509
	public Transform End
	{
		get
		{
			return this.end;
		}
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000D114 File Offset: 0x0000B514
	public virtual void Apply()
	{
		if (this.Start == null || this.End == null || AstarPath.active == null)
		{
			return;
		}
		Node node = AstarPath.active.GetNearest(this.Start.position).node;
		Node node2 = AstarPath.active.GetNearest(this.End.position).node;
		if (node == null || node2 == null)
		{
			return;
		}
		int cost = (int)Math.Round((double)((float)(node.position - node2.position).costMagnitude * this.costFactor));
		if (this.deleteConnection)
		{
			node.RemoveConnection(node2);
			if (!this.oneWay)
			{
				node2.RemoveConnection(node);
			}
		}
		else
		{
			node.AddConnection(node2, cost);
			if (!this.oneWay)
			{
				node2.AddConnection(node, cost);
			}
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000D210 File Offset: 0x0000B610
	public void OnDrawGizmos()
	{
		if (this.Start == null || this.End == null)
		{
			return;
		}
		Vector3 position = this.Start.position;
		Vector3 position2 = this.End.position;
		Gizmos.color = ((!this.deleteConnection) ? Color.green : Color.red);
		this.DrawGizmoBezier(position, position2);
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000D280 File Offset: 0x0000B680
	private void DrawGizmoBezier(Vector3 p1, Vector3 p2)
	{
		Vector3 vector = p2 - p1;
		if (vector == Vector3.zero)
		{
			return;
		}
		Vector3 rhs = Vector3.Cross(Vector3.up, vector);
		Vector3 vector2 = Vector3.Cross(vector, rhs).normalized;
		vector2 *= vector.magnitude * 0.1f;
		Vector3 p3 = p1 + vector2;
		Vector3 p4 = p2 + vector2;
		Vector3 from = p1;
		for (int i = 1; i <= 20; i++)
		{
			float t = (float)i / 20f;
			Vector3 vector3 = Mathfx.CubicBezier(p1, p3, p4, p2, t);
			Gizmos.DrawLine(from, vector3);
			from = vector3;
		}
	}

	// Token: 0x04000185 RID: 389
	public Transform end;

	// Token: 0x04000186 RID: 390
	public float costFactor = 1f;

	// Token: 0x04000187 RID: 391
	public bool oneWay;

	// Token: 0x04000188 RID: 392
	public bool deleteConnection;
}
