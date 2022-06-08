using System;

// Token: 0x020004B8 RID: 1208
[Serializable]
public class CSSeasonScoreBoardInfo
{
	// Token: 0x0600223E RID: 8766 RVA: 0x000FD3AC File Offset: 0x000FB7AC
	public CSSeasonScoreBoardInfo()
	{
		this.Level = "0";
		this.RoleName = string.Empty;
		this.ScoreRank = "0";
		this.Score = "0";
	}

	// Token: 0x040022AB RID: 8875
	public string Level;

	// Token: 0x040022AC RID: 8876
	public string RoleName;

	// Token: 0x040022AD RID: 8877
	public string ScoreRank;

	// Token: 0x040022AE RID: 8878
	public string Score;
}
