using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000026 RID: 38
[AddComponentMenu("Pathfinding/GraphUpdateScene")]
public class GraphUpdateScene : GraphModifier
{
	// Token: 0x06000133 RID: 307 RVA: 0x0000A508 File Offset: 0x00008908
	public void Start()
	{
		if (!this.firstApplied && this.applyOnStart)
		{
			this.Apply();
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000A526 File Offset: 0x00008926
	public override void OnPostScan()
	{
		if (this.applyOnScan)
		{
			this.Apply();
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000A53C File Offset: 0x0000893C
	public virtual void InvertSettings()
	{
		this.setWalkability = !this.setWalkability;
		this.penaltyDelta = -this.penaltyDelta;
		if (this.setTagInvert == 0)
		{
			this.setTagInvert = this.setTag;
			this.setTag = 0;
		}
		else
		{
			this.setTag = this.setTagInvert;
			this.setTagInvert = 0;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000A59B File Offset: 0x0000899B
	public void RecalcConvex()
	{
		if (this.convex)
		{
			this.convexPoints = Polygon.ConvexHull(this.points);
		}
		else
		{
			this.convexPoints = null;
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000A5C8 File Offset: 0x000089C8
	public void ToggleUseWorldSpace()
	{
		this.useWorldSpace = !this.useWorldSpace;
		if (this.points == null)
		{
			return;
		}
		Matrix4x4 matrix4x = (!this.useWorldSpace) ? base.transform.worldToLocalMatrix : base.transform.localToWorldMatrix;
		for (int i = 0; i < this.points.Length; i++)
		{
			this.points[i] = matrix4x.MultiplyPoint3x4(this.points[i]);
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000A65C File Offset: 0x00008A5C
	public void LockToY()
	{
		if (this.points == null)
		{
			return;
		}
		for (int i = 0; i < this.points.Length; i++)
		{
			this.points[i].y = this.lockToYValue;
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000A6A5 File Offset: 0x00008AA5
	public void Apply(AstarPath active)
	{
		if (this.applyOnScan)
		{
			this.Apply();
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000A6B8 File Offset: 0x00008AB8
	public void Apply()
	{
		if (AstarPath.active == null)
		{
			Debug.LogError("There is no AstarPath object in the scene");
			return;
		}
		this.firstApplied = true;
		GraphUpdateShape graphUpdateShape = new GraphUpdateShape();
		graphUpdateShape.convex = this.convex;
		Vector3[] array = this.points;
		if (!this.useWorldSpace)
		{
			array = new Vector3[this.points.Length];
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = localToWorldMatrix.MultiplyPoint3x4(this.points[i]);
			}
		}
		graphUpdateShape.points = array;
		Bounds bounds = graphUpdateShape.GetBounds();
		if (bounds.size.y < this.minBoundsHeight)
		{
			bounds.size = new Vector3(bounds.size.x, this.minBoundsHeight, bounds.size.z);
		}
		GraphUpdateObject graphUpdateObject = new GraphUpdateObject(bounds);
		graphUpdateObject.shape = graphUpdateShape;
		graphUpdateObject.modifyWalkability = this.modifyWalkability;
		graphUpdateObject.setWalkability = this.setWalkability;
		graphUpdateObject.addPenalty = this.penaltyDelta;
		graphUpdateObject.updatePhysics = this.updatePhysics;
		graphUpdateObject.updateErosion = this.updateErosion;
		graphUpdateObject.modifyTag = this.modifyTag;
		graphUpdateObject.setTag = this.setTag;
		AstarPath.active.UpdateGraphs(graphUpdateObject);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000A831 File Offset: 0x00008C31
	public void OnDrawGizmos()
	{
		this.OnDrawGizmos(false);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000A83A File Offset: 0x00008C3A
	public void OnDrawGizmosSelected()
	{
		this.OnDrawGizmos(true);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000A844 File Offset: 0x00008C44
	public void OnDrawGizmos(bool selected)
	{
		if (this.points == null)
		{
			return;
		}
		Gizmos.color = ((!selected) ? new Color(0f, 0.9f, 0f, 0.5f) : new Color(0f, 0.9f, 0f, 1f));
		Matrix4x4 matrix4x = (!this.useWorldSpace) ? base.transform.localToWorldMatrix : Matrix4x4.identity;
		for (int i = 0; i < this.points.Length; i++)
		{
			Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.points[i]), matrix4x.MultiplyPoint3x4(this.points[(i + 1) % this.points.Length]));
		}
		if (this.convex)
		{
			if (this.convexPoints == null)
			{
				this.RecalcConvex();
			}
			Gizmos.color = ((!selected) ? new Color(0.9f, 0f, 0f, 0.5f) : new Color(0.9f, 0f, 0f, 1f));
			for (int j = 0; j < this.convexPoints.Length; j++)
			{
				Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.convexPoints[j]), matrix4x.MultiplyPoint3x4(this.convexPoints[(j + 1) % this.convexPoints.Length]));
			}
		}
	}

	// Token: 0x0400010F RID: 271
	public Vector3[] points;

	// Token: 0x04000110 RID: 272
	private Vector3[] convexPoints;

	// Token: 0x04000111 RID: 273
	[HideInInspector]
	public bool convex = true;

	// Token: 0x04000112 RID: 274
	[HideInInspector]
	public float minBoundsHeight = 1f;

	// Token: 0x04000113 RID: 275
	[HideInInspector]
	public int penaltyDelta;

	// Token: 0x04000114 RID: 276
	[HideInInspector]
	public bool modifyWalkability;

	// Token: 0x04000115 RID: 277
	[HideInInspector]
	public bool setWalkability;

	// Token: 0x04000116 RID: 278
	[HideInInspector]
	public bool applyOnStart = true;

	// Token: 0x04000117 RID: 279
	[HideInInspector]
	public bool applyOnScan = true;

	// Token: 0x04000118 RID: 280
	[HideInInspector]
	public bool useWorldSpace = true;

	// Token: 0x04000119 RID: 281
	public bool updatePhysics;

	// Token: 0x0400011A RID: 282
	public bool updateErosion = true;

	// Token: 0x0400011B RID: 283
	[HideInInspector]
	public bool lockToY;

	// Token: 0x0400011C RID: 284
	[HideInInspector]
	public float lockToYValue;

	// Token: 0x0400011D RID: 285
	[HideInInspector]
	public bool modifyTag;

	// Token: 0x0400011E RID: 286
	[HideInInspector]
	public int setTag;

	// Token: 0x0400011F RID: 287
	private int setTagInvert;

	// Token: 0x04000120 RID: 288
	private bool firstApplied;
}
