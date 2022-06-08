using System;
using ExitGames.Client.Photon;

// Token: 0x0200014B RID: 331
public static class ScoreExtensions
{
	// Token: 0x060009BD RID: 2493 RVA: 0x000496A4 File Offset: 0x00047AA4
	public static void SetScore(this PhotonPlayer player, int newScore)
	{
		Hashtable hashtable = new Hashtable();
		hashtable["score"] = newScore;
		player.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x000496D4 File Offset: 0x00047AD4
	public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
	{
		int num = player.GetScore();
		num += scoreToAddToCurrent;
		Hashtable hashtable = new Hashtable();
		hashtable["score"] = num;
		player.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0004970C File Offset: 0x00047B0C
	public static int GetScore(this PhotonPlayer player)
	{
		object obj;
		if (player.customProperties.TryGetValue("score", out obj))
		{
			return (int)obj;
		}
		return 0;
	}
}
