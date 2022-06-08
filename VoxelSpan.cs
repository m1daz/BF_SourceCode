using System;

// Token: 0x0200008B RID: 139
public class VoxelSpan
{
	// Token: 0x0600049F RID: 1183 RVA: 0x0002D40E File Offset: 0x0002B80E
	public VoxelSpan(uint b, uint t, int area)
	{
		this.bottom = b;
		this.top = t;
		this.area = area;
	}

	// Token: 0x040003A8 RID: 936
	public uint bottom;

	// Token: 0x040003A9 RID: 937
	public uint top;

	// Token: 0x040003AA RID: 938
	public VoxelSpan next;

	// Token: 0x040003AB RID: 939
	public int area;
}
