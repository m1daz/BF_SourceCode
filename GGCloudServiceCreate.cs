using System;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class GGCloudServiceCreate : MonoBehaviour
{
	// Token: 0x06002125 RID: 8485 RVA: 0x000F6D67 File Offset: 0x000F5167
	private void Awake()
	{
		GGCloudServiceCreate.mInstance = this;
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x000F6D70 File Offset: 0x000F5170
	public void BeforeCloudServiceCreate()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("GGCloudService");
		if (gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		this.goCloudService = UnityEngine.Object.Instantiate<GameObject>(this.goCloudServicePrefab, new Vector3(-1000f, -1000f, -1000f), Quaternion.identity);
		this.goCloudService.name = "GGCloudService";
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x000F6DD4 File Offset: 0x000F51D4
	private void Start()
	{
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x000F6DD8 File Offset: 0x000F51D8
	public void AutoQuickEnterGame(string username, string password)
	{
		if (base.gameObject.GetComponent<GGCloudServiceLoginProcessBar>() == null)
		{
			base.gameObject.AddComponent<GGCloudServiceLoginProcessBar>();
		}
		GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init...", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Init...");
		UILoginNewDirector.mInstance.AutoEnterGameProcessBar();
		GGCloudServiceKit.mInstance.LogIn(username, password);
		if (ACTUserDataManager.mInstance != null)
		{
			ACTUserDataManager.mInstance.Reset();
		}
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x000F6E64 File Offset: 0x000F5264
	public void InitCloudServiceCreate()
	{
		if (UIUserDataController.GetCurLoginUserName() != string.Empty && UIUserDataController.GetCurLoginPassword() != string.Empty)
		{
			bool flag = true;
			if (ACTUserDataManager.mInstance != null && ACTUserDataManager.mInstance.mClickLogOut)
			{
				flag = false;
			}
			string @string = GOGPlayerPrefabs.GetString("sessionid", string.Empty);
			if (UIUserDataController.GetCurLoginUserName() != string.Empty && UIUserDataController.GetCurLoginPassword() != string.Empty)
			{
				GGCloudServiceConstant.mInstance.mSessionId = @string;
				if (UILoginNewDirector.mInstance != null && flag)
				{
					if (this.goCloudService.GetComponent<GGCloudServiceLoginProcessBar>() == null)
					{
						this.goCloudService.AddComponent<GGCloudServiceLoginProcessBar>();
					}
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init...", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Init...");
					UILoginNewDirector.mInstance.AutoEnterGameProcessBar();
					UILoginNewDirector.mInstance.EnterGameDontNeedLogin(@string);
				}
			}
			if (ACTUserDataManager.mInstance != null)
			{
				ACTUserDataManager.mInstance.Reset();
			}
		}
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x000F6F93 File Offset: 0x000F5393
	private void Update()
	{
	}

	// Token: 0x040021E3 RID: 8675
	public static GGCloudServiceCreate mInstance;

	// Token: 0x040021E4 RID: 8676
	public GameObject goCloudServicePrefab;

	// Token: 0x040021E5 RID: 8677
	public GameObject goCloudService;
}
