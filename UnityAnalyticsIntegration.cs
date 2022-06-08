using System;
using UnityEngine;

// Token: 0x020006AC RID: 1708
public class UnityAnalyticsIntegration : MonoBehaviour
{
	// Token: 0x06003243 RID: 12867 RVA: 0x0016387D File Offset: 0x00161C7D
	private void Awake()
	{
		UnityAnalyticsIntegration.mInstance = this;
		if (UnityAnalyticsIntegration.UnityAnalyticsIntegrationRef == null)
		{
			UnityAnalyticsIntegration.UnityAnalyticsIntegrationRef = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		Application.runInBackground = true;
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x001638BC File Offset: 0x00161CBC
	private void Start()
	{
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			this.NewUserLoadedGame();
		}
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x001638CE File Offset: 0x00161CCE
	public void NewUserLoadedGame()
	{
	}

	// Token: 0x06003246 RID: 12870 RVA: 0x001638D0 File Offset: 0x00161CD0
	public void NewUserPressQuickPlayOrRegister()
	{
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x001638D2 File Offset: 0x00161CD2
	public void NewUserPressManualRegister()
	{
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x001638D4 File Offset: 0x00161CD4
	public void NewUserLoadedCreateRoleName()
	{
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x001638D6 File Offset: 0x00161CD6
	public void NewUserPressCreateRoleName()
	{
	}

	// Token: 0x0600324A RID: 12874 RVA: 0x001638D8 File Offset: 0x00161CD8
	public void NewUserLoadedHelp()
	{
	}

	// Token: 0x0600324B RID: 12875 RVA: 0x001638DA File Offset: 0x00161CDA
	public void NewUserLoadedHome()
	{
	}

	// Token: 0x04002ECB RID: 11979
	public static UnityAnalyticsIntegration mInstance;

	// Token: 0x04002ECC RID: 11980
	private static UnityAnalyticsIntegration UnityAnalyticsIntegrationRef;

	// Token: 0x04002ECD RID: 11981
	public bool mbNewUser;
}
