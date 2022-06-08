using System;

// Token: 0x0200008A RID: 138
public struct CompactVoxelSpan
{
	// Token: 0x0600049C RID: 1180 RVA: 0x0002D3A8 File Offset: 0x0002B7A8
	public CompactVoxelSpan(ushort bottom, uint height)
	{
		this.con = 24U;
		this.y = bottom;
		this.h = height;
		this.reg = 0;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0002D3C8 File Offset: 0x0002B7C8
	public void SetConnection(int dir, uint value)
	{
		int num = dir * 6;
		this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63U) << num));
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0002D3FC File Offset: 0x0002B7FC
	public int GetConnection(int dir)
	{
		return (int)this.con >> dir * 6 & 63;
	}

	// Token: 0x040003A4 RID: 932
	public ushort y;

	// Token: 0x040003A5 RID: 933
	public uint con;

	// Token: 0x040003A6 RID: 934
	public uint h;

	// Token: 0x040003A7 RID: 935
	public int reg;
}
