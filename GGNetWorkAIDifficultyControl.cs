using System;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class GGNetWorkAIDifficultyControl : MonoBehaviour
{
	// Token: 0x06000F24 RID: 3876 RVA: 0x000818BD File Offset: 0x0007FCBD
	private void Awake()
	{
		GGNetWorkAIDifficultyControl.mInstance = this;
		this.difficultySet = GGNetworkKit.mInstance.GetHuntingDifficulty() + 1;
		this.maxPlayerSet = GGNetworkKit.mInstance.GetMaxPlayers();
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000818E7 File Offset: 0x0007FCE7
	private void Start()
	{
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x000818E9 File Offset: 0x0007FCE9
	private void Update()
	{
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x000818EB File Offset: 0x0007FCEB
	private void OnDestroy()
	{
		GGNetWorkAIDifficultyControl.mInstance = null;
	}

	// Token: 0x040010E8 RID: 4328
	public static GGNetWorkAIDifficultyControl mInstance;

	// Token: 0x040010E9 RID: 4329
	public int difficultySet = 2;

	// Token: 0x040010EA RID: 4330
	public int maxPlayerSet = 4;
}
