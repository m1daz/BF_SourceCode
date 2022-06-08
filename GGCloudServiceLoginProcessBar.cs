using System;
using System.Collections;
using GOG.Utility;
using UnityEngine;

// Token: 0x02000480 RID: 1152
public class GGCloudServiceLoginProcessBar : MonoBehaviour
{
	// Token: 0x060021AB RID: 8619 RVA: 0x000FA028 File Offset: 0x000F8428
	private void Awake()
	{
		GGCloudServiceLoginProcessBar.mInstance = this;
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x000FA030 File Offset: 0x000F8430
	private void Start()
	{
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x000FA034 File Offset: 0x000F8434
	private void Update()
	{
		this.mProcessCheckTime += Time.deltaTime;
		if (this.mProcessCheckTime >= 0.5f)
		{
			this.result = this.mLoginProcessBarInfo.Result;
			this.status = this.mLoginProcessBarInfo.ResultStatus;
			this.title = this.mLoginProcessBarInfo.Title;
			this.mProcessCheckTime = 0f;
			int progress = (this.mLoginProcessBarInfo.Progress + 2 <= 100) ? (this.mLoginProcessBarInfo.Progress + 2) : 100;
			this.mLoginProcessBarInfo.Update(progress);
			if (this.mLoginProcessBarInfo.ResultStatus != ProgressStatus.Progressing && !this.mRemove)
			{
				this.mRemove = true;
				base.StartCoroutine(this.RemoveLoginProcessBarInfo());
			}
		}
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x000FA108 File Offset: 0x000F8508
	public IEnumerator RemoveLoginProcessBarInfo()
	{
		yield return new WaitForSeconds(2f);
		GGCloudServiceKit.mInstance.RemoveLoginProcessBar();
		yield break;
	}

	// Token: 0x04002234 RID: 8756
	public static GGCloudServiceLoginProcessBar mInstance;

	// Token: 0x04002235 RID: 8757
	public ProgressController mLoginProcessBarInfo = new ProgressController(string.Empty, string.Empty);

	// Token: 0x04002236 RID: 8758
	private float mProcessCheckTime;

	// Token: 0x04002237 RID: 8759
	private bool mRemove;

	// Token: 0x04002238 RID: 8760
	public string result;

	// Token: 0x04002239 RID: 8761
	public ProgressStatus status;

	// Token: 0x0400223A RID: 8762
	public string title;
}
