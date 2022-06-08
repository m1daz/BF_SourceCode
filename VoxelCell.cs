using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000088 RID: 136
public struct VoxelCell
{
	// Token: 0x06000499 RID: 1177 RVA: 0x0002D17C File Offset: 0x0002B57C
	public void AddSpan(uint bottom, uint top, int area, int voxelWalkableClimb)
	{
		VoxelSpan voxelSpan = new VoxelSpan(bottom, top, area);
		if (this.firstSpan == null)
		{
			this.firstSpan = voxelSpan;
			return;
		}
		VoxelSpan voxelSpan2 = null;
		VoxelSpan voxelSpan3 = this.firstSpan;
		while (voxelSpan3 != null)
		{
			if (voxelSpan3.bottom > voxelSpan.top)
			{
				break;
			}
			if (voxelSpan3.top < voxelSpan.bottom)
			{
				voxelSpan2 = voxelSpan3;
				voxelSpan3 = voxelSpan3.next;
			}
			else
			{
				if (voxelSpan3.bottom < bottom)
				{
					voxelSpan.bottom = voxelSpan3.bottom;
				}
				if (voxelSpan3.top > top)
				{
					voxelSpan.top = voxelSpan3.top;
				}
				if (Mathfx.Abs((int)(voxelSpan.top - voxelSpan3.top)) <= voxelWalkableClimb)
				{
					voxelSpan.area = Mathfx.Max(voxelSpan.area, voxelSpan3.area);
				}
				VoxelSpan next = voxelSpan3.next;
				if (voxelSpan2 != null)
				{
					voxelSpan2.next = next;
				}
				else
				{
					this.firstSpan = next;
				}
				voxelSpan3 = next;
			}
		}
		if (voxelSpan2 != null)
		{
			voxelSpan.next = voxelSpan2.next;
			voxelSpan2.next = voxelSpan;
		}
		else
		{
			voxelSpan.next = this.firstSpan;
			this.firstSpan = voxelSpan;
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002D2A4 File Offset: 0x0002B6A4
	public void FilterWalkable(uint walkableHeight)
	{
		VoxelSpan voxelSpan = null;
		for (VoxelSpan next = this.firstSpan; next != null; next = next.next)
		{
			if (next.area == 1)
			{
				if (voxelSpan != null)
				{
					voxelSpan.next = next.next;
				}
				else
				{
					this.firstSpan = next.next;
				}
			}
			else
			{
				if (next.next != null)
				{
					next.top = next.next.bottom;
				}
				else
				{
					next.top = VoxelArea.MaxHeight;
				}
				uint num = next.top - next.bottom;
				if (next.top < next.bottom)
				{
					Debug.Log(next.top - next.bottom);
				}
				if (num < walkableHeight)
				{
					if (voxelSpan != null)
					{
						voxelSpan.next = next.next;
					}
					else
					{
						this.firstSpan = next.next;
					}
				}
				else
				{
					voxelSpan = next;
				}
			}
		}
	}

	// Token: 0x040003A1 RID: 929
	public VoxelSpan firstSpan;
}
