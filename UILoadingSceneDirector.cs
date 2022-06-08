using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class UILoadingSceneDirector : MonoBehaviour
{
	// Token: 0x06001668 RID: 5736 RVA: 0x000C000C File Offset: 0x000BE40C
	private void Awake()
	{
		if (UILoadingSceneDirector.mInstance == null)
		{
			UILoadingSceneDirector.mInstance = this;
		}
		this.toScene = UISceneManager.mInstance.toScene;
		if (UISceneManager.mInstance.fromScene == "UILogin" && this.toScene == "MainMenu")
		{
			UIUserDataController.SetLoginSuccessCount(UIUserDataController.GetLoginSuccessCount() + 1);
			UIUserDataController.SetFromLoginToHome(1);
		}
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000C007F File Offset: 0x000BE47F
	private void OnDestroy()
	{
		if (UILoadingSceneDirector.mInstance != null)
		{
			UILoadingSceneDirector.mInstance = null;
		}
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000C0098 File Offset: 0x000BE498
	private void Start()
	{
		base.StartCoroutine(this.StartLoading());
		this.tex[0] = (Resources.Load("UI/Images/General/LoadingTexture1") as Texture);
		this.tex[1] = (Resources.Load("UI/Images/General/LoadingTexture2") as Texture);
		this.loadingTexture.mainTexture = this.tex[0];
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000C00F4 File Offset: 0x000BE4F4
	private IEnumerator StartLoading()
	{
		if (this.toScene != string.Empty)
		{
			yield return this.async = Application.LoadLevelAsync(this.toScene);
		}
		yield break;
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000C010F File Offset: 0x000BE50F
	private void Update()
	{
		this.TextureGif();
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000C0118 File Offset: 0x000BE518
	private void TextureGif()
	{
		this.gifDeltaTime += Time.deltaTime;
		if (this.gifDeltaTime > 0.2f)
		{
			this.gifDeltaTime = 0f;
			this.textureIndex++;
			if (this.textureIndex > 2)
			{
				this.textureIndex = 1;
			}
			this.loadingTexture.mainTexture = this.tex[this.textureIndex - 1];
		}
	}

	// Token: 0x04001940 RID: 6464
	public static UILoadingSceneDirector mInstance;

	// Token: 0x04001941 RID: 6465
	private AsyncOperation async;

	// Token: 0x04001942 RID: 6466
	private int iProgress;

	// Token: 0x04001943 RID: 6467
	private bool isInWhile;

	// Token: 0x04001944 RID: 6468
	private string toScene = string.Empty;

	// Token: 0x04001945 RID: 6469
	public UITexture loadingTexture;

	// Token: 0x04001946 RID: 6470
	private float gifDeltaTime;

	// Token: 0x04001947 RID: 6471
	private int textureIndex = 1;

	// Token: 0x04001948 RID: 6472
	private const int maxNum = 2;

	// Token: 0x04001949 RID: 6473
	private Texture[] tex = new Texture[2];
}
