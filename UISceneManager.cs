using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class UISceneManager : MonoBehaviour
{
	// Token: 0x0600166F RID: 5743 RVA: 0x000C0268 File Offset: 0x000BE668
	private void Awake()
	{
		UISceneManager.mInstance = this;
		if (GameObject.Find(base.gameObject.name + "_old") != null)
		{
			UnityEngine.Object.Destroy(GameObject.Find(base.gameObject.name + "_old"));
		}
		else
		{
			UserDataController.StatisticsChkPerLogin();
		}
		base.gameObject.name = base.gameObject.name + "_old";
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000C02F4 File Offset: 0x000BE6F4
	private IEnumerator LoadingNewScene()
	{
		while (Application.loadedLevelName != "LoadingScene")
		{
			yield return new WaitForSeconds(0.01f);
		}
		Application.LoadLevel(this.toScene);
		yield break;
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000C030F File Offset: 0x000BE70F
	public void LoadLevel(string name)
	{
		this.fromScene = Application.loadedLevelName;
		this.toScene = name;
		Application.LoadLevel("LoadingScene");
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x000C032D File Offset: 0x000BE72D
	private void Start()
	{
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x000C032F File Offset: 0x000BE72F
	private void Update()
	{
	}

	// Token: 0x0400194A RID: 6474
	public string fromScene = string.Empty;

	// Token: 0x0400194B RID: 6475
	public string toScene = string.Empty;

	// Token: 0x0400194C RID: 6476
	public static UISceneManager mInstance;
}
