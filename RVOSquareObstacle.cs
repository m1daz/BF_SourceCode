using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class RVOSquareObstacle : RVOObstacle
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x00035AFB File Offset: 0x00033EFB
	protected override bool StaticObstacle
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060005AE RID: 1454 RVA: 0x00035AFE File Offset: 0x00033EFE
	protected override bool ExecuteInEditor
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060005AF RID: 1455 RVA: 0x00035B01 File Offset: 0x00033F01
	protected override bool LocalCoordinates
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00035B04 File Offset: 0x00033F04
	protected override bool AreGizmosDirty()
	{
		return false;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00035B08 File Offset: 0x00033F08
	protected override void CreateObstacles()
	{
		this.size.x = Mathf.Abs(this.size.x);
		this.size.y = Mathf.Abs(this.size.y);
		this.height = Mathf.Abs(this.height);
		Vector3[] array = new Vector3[]
		{
			new Vector3(1f, 0f, -1f),
			new Vector3(1f, 0f, 1f),
			new Vector3(-1f, 0f, 1f),
			new Vector3(-1f, 0f, -1f)
		};
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Scale(new Vector3(this.size.x, 0f, this.size.y));
		}
		base.AddObstacle(array, this.height);
	}

	// Token: 0x040004AA RID: 1194
	public float height = 1f;

	// Token: 0x040004AB RID: 1195
	public Vector2 size = Vector3.one;
}
