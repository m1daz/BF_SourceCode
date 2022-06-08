using System;

// Token: 0x02000089 RID: 137
public struct CompactVoxelCell
{
	// Token: 0x0600049B RID: 1179 RVA: 0x0002D398 File Offset: 0x0002B798
	public CompactVoxelCell(uint i, uint c)
	{
		this.index = i;
		this.count = c;
	}

	// Token: 0x040003A2 RID: 930
	public uint index;

	// Token: 0x040003A3 RID: 931
	public uint count;
}
