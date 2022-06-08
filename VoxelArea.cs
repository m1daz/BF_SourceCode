using System;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class VoxelArea
{
	// Token: 0x06000494 RID: 1172 RVA: 0x0002CF94 File Offset: 0x0002B394
	public VoxelArea(Bounds b)
	{
		this.width = Mathf.RoundToInt(b.size.x);
		this.height = Mathf.RoundToInt(b.size.y);
		this.depth = Mathf.RoundToInt(b.size.z);
		int num = this.width * this.depth;
		this.cells = new VoxelCell[num];
		this.compactCells = new CompactVoxelCell[num];
		int[] array = new int[4];
		array[0] = -1;
		array[2] = 1;
		this.DirectionX = array;
		this.DirectionZ = new int[]
		{
			0,
			this.width,
			0,
			-this.width
		};
		this.VectorDirection = new Vector3[]
		{
			Vector3.left,
			Vector3.forward,
			Vector3.right,
			Vector3.back
		};
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0002D0A0 File Offset: 0x0002B4A0
	public int GetSpanCountAll()
	{
		int num = 0;
		int num2 = this.width * this.depth;
		for (int i = 0; i < num2; i++)
		{
			for (VoxelSpan voxelSpan = this.cells[i].firstSpan; voxelSpan != null; voxelSpan = voxelSpan.next)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0002D0F8 File Offset: 0x0002B4F8
	public int GetSpanCount()
	{
		int num = 0;
		int num2 = this.width * this.depth;
		for (int i = 0; i < num2; i++)
		{
			for (VoxelSpan voxelSpan = this.cells[i].firstSpan; voxelSpan != null; voxelSpan = voxelSpan.next)
			{
				if (voxelSpan.area != 0)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x04000389 RID: 905
	public static uint MaxHeight = 65536U;

	// Token: 0x0400038A RID: 906
	public static int MaxHeightInt = 65536;

	// Token: 0x0400038B RID: 907
	public int width;

	// Token: 0x0400038C RID: 908
	public int height;

	// Token: 0x0400038D RID: 909
	public int depth;

	// Token: 0x0400038E RID: 910
	public VoxelCell[] cells;

	// Token: 0x0400038F RID: 911
	public CompactVoxelSpan[] compactSpans;

	// Token: 0x04000390 RID: 912
	public CompactVoxelCell[] compactCells;

	// Token: 0x04000391 RID: 913
	public int[] areaTypes;

	// Token: 0x04000392 RID: 914
	public ushort[] dist;

	// Token: 0x04000393 RID: 915
	public ushort maxDistance;

	// Token: 0x04000394 RID: 916
	public int maxRegions;

	// Token: 0x04000395 RID: 917
	public int[] DirectionX;

	// Token: 0x04000396 RID: 918
	public int[] DirectionZ;

	// Token: 0x04000397 RID: 919
	public Vector3[] VectorDirection;
}
