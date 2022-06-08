using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public abstract class RVOObstacle : MonoBehaviour
{
	// Token: 0x06000597 RID: 1431
	protected abstract void CreateObstacles();

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000598 RID: 1432
	protected abstract bool ExecuteInEditor { get; }

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000599 RID: 1433
	protected abstract bool LocalCoordinates { get; }

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x0600059A RID: 1434
	protected abstract bool StaticObstacle { get; }

	// Token: 0x0600059B RID: 1435
	protected abstract bool AreGizmosDirty();

	// Token: 0x0600059C RID: 1436 RVA: 0x000353B3 File Offset: 0x000337B3
	public void OnDrawGizmos()
	{
		this.OnDrawGizmos(false);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x000353BC File Offset: 0x000337BC
	public void OnDrawGizmosSelected()
	{
		this.OnDrawGizmos(true);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x000353C8 File Offset: 0x000337C8
	public void OnDrawGizmos(bool selected)
	{
		this.gizmoDrawing = true;
		Gizmos.color = new Color(0.615f, 1f, 0.06f, (!selected) ? 0.7f : 1f);
		if (this.gizmoVerts == null || this.AreGizmosDirty() || this._obstacleMode != this.obstacleMode)
		{
			this._obstacleMode = this.obstacleMode;
			if (this.gizmoVerts == null)
			{
				this.gizmoVerts = new List<Vector3[]>();
			}
			else
			{
				this.gizmoVerts.Clear();
			}
			this.CreateObstacles();
		}
		Matrix4x4 matrix = this.GetMatrix();
		for (int i = 0; i < this.gizmoVerts.Count; i++)
		{
			Vector3[] array = this.gizmoVerts[i];
			int j = 0;
			int num = array.Length - 1;
			while (j < array.Length)
			{
				Gizmos.DrawLine(matrix.MultiplyPoint3x4(array[j]), matrix.MultiplyPoint3x4(array[num]));
				num = j++;
			}
			if (selected && this.obstacleMode != RVOObstacle.ObstacleVertexWinding.Both)
			{
				int k = 0;
				int num2 = array.Length - 1;
				while (k < array.Length)
				{
					Vector3 vector = matrix.MultiplyPoint3x4(array[num2]);
					Vector3 vector2 = matrix.MultiplyPoint3x4(array[k]);
					Vector3 vector3 = (vector + vector2) * 0.5f;
					Vector3 normalized = (vector2 - vector).normalized;
					if (!(normalized == Vector3.zero))
					{
						Vector3 vector4 = Vector3.Cross(Vector3.up, normalized);
						Gizmos.DrawLine(vector3, vector3 + vector4);
						Gizmos.DrawLine(vector3 + vector4, vector3 + vector4 * 0.5f + normalized * 0.5f);
						Gizmos.DrawLine(vector3 + vector4, vector3 + vector4 * 0.5f - normalized * 0.5f);
					}
					num2 = k++;
				}
			}
		}
		this.gizmoDrawing = false;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0003560B File Offset: 0x00033A0B
	protected virtual Matrix4x4 GetMatrix()
	{
		if (this.LocalCoordinates)
		{
			return base.transform.localToWorldMatrix;
		}
		return Matrix4x4.identity;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0003562C File Offset: 0x00033A2C
	public void OnDisable()
	{
		if (this.addedObstacles != null)
		{
			if (this.sim == null)
			{
				throw new Exception("This should not happen! Make sure you are not overriding the OnEnable function");
			}
			for (int i = 0; i < this.addedObstacles.Count; i++)
			{
				this.sim.RemoveObstacle(this.addedObstacles[i]);
			}
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00035690 File Offset: 0x00033A90
	public void OnEnable()
	{
		if (this.addedObstacles != null)
		{
			if (this.sim == null)
			{
				throw new Exception("This should not happen! Make sure you are not overriding the OnDisable function");
			}
			for (int i = 0; i < this.addedObstacles.Count; i++)
			{
				this.sim.AddObstacle(this.addedObstacles[i]);
			}
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000356F2 File Offset: 0x00033AF2
	public void Start()
	{
		this.addedObstacles = new List<ObstacleVertex>();
		this.sourceObstacles = new List<Vector3[]>();
		this.prevUpdateMatrix = this.GetMatrix();
		this.CreateObstacles();
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0003571C File Offset: 0x00033B1C
	public void Update()
	{
		Matrix4x4 matrix = this.GetMatrix();
		if (matrix != this.prevUpdateMatrix)
		{
			for (int i = 0; i < this.addedObstacles.Count; i++)
			{
				this.sim.UpdateObstacle(this.addedObstacles[i], this.sourceObstacles[i], matrix);
			}
			this.prevUpdateMatrix = matrix;
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00035788 File Offset: 0x00033B88
	protected void FindSimulator()
	{
		RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
		if (rvosimulator == null)
		{
			throw new InvalidOperationException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
		}
		this.sim = rvosimulator.GetSimulator();
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x000357D0 File Offset: 0x00033BD0
	protected void AddObstacle(Vector3[] vertices, float height)
	{
		if (vertices == null)
		{
			throw new ArgumentNullException("Vertices Must Not Be Null");
		}
		if (height < 0f)
		{
			throw new ArgumentOutOfRangeException("Height must be non-negative");
		}
		if (vertices.Length < 2)
		{
			throw new ArgumentException("An obstacle must have at least two vertices");
		}
		if (this.gizmoDrawing)
		{
			Vector3[] array = new Vector3[vertices.Length];
			this.WindCorrectly(vertices);
			Array.Copy(vertices, array, vertices.Length);
			this.gizmoVerts.Add(array);
			return;
		}
		if (this.sim == null)
		{
			this.FindSimulator();
		}
		if (vertices.Length == 2)
		{
			this.AddObstacleInternal(vertices, height);
			return;
		}
		if (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.Both)
		{
			int i = 0;
			int num = vertices.Length - 1;
			while (i < vertices.Length)
			{
				this.AddObstacleInternal(new Vector3[]
				{
					vertices[num],
					vertices[i]
				}, height);
				num = i++;
			}
		}
		else
		{
			this.WindCorrectly(vertices);
			this.AddObstacleInternal(vertices, height);
		}
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x000358E6 File Offset: 0x00033CE6
	private void AddObstacleInternal(Vector3[] vertices, float height)
	{
		this.addedObstacles.Add(this.sim.AddObstacle(vertices, height, this.GetMatrix()));
		this.sourceObstacles.Add(vertices);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00035914 File Offset: 0x00033D14
	private void WindCorrectly(Vector3[] vertices)
	{
		if (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.Both)
		{
			return;
		}
		int num = 0;
		float num2 = float.PositiveInfinity;
		for (int i = 0; i < vertices.Length; i++)
		{
			if (vertices[i].x < num2)
			{
				num = i;
				num2 = vertices[i].x;
			}
		}
		if (Polygon.IsClockwise(vertices[(num - 1 + vertices.Length) % vertices.Length], vertices[num], vertices[(num + 1) % vertices.Length]))
		{
			if (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepOut)
			{
				Array.Reverse(vertices);
			}
		}
		else if (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepIn)
		{
			Array.Reverse(vertices);
		}
	}

	// Token: 0x04000499 RID: 1177
	public RVOObstacle.ObstacleVertexWinding obstacleMode;

	// Token: 0x0400049A RID: 1178
	protected Simulator sim;

	// Token: 0x0400049B RID: 1179
	private List<ObstacleVertex> addedObstacles;

	// Token: 0x0400049C RID: 1180
	private List<Vector3[]> sourceObstacles;

	// Token: 0x0400049D RID: 1181
	private bool gizmoDrawing;

	// Token: 0x0400049E RID: 1182
	private List<Vector3[]> gizmoVerts;

	// Token: 0x0400049F RID: 1183
	private RVOObstacle.ObstacleVertexWinding _obstacleMode;

	// Token: 0x040004A0 RID: 1184
	private Matrix4x4 prevUpdateMatrix;

	// Token: 0x020000B7 RID: 183
	public enum ObstacleVertexWinding
	{
		// Token: 0x040004A2 RID: 1186
		KeepOut,
		// Token: 0x040004A3 RID: 1187
		KeepIn,
		// Token: 0x040004A4 RID: 1188
		Both
	}
}
