using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000547 RID: 1351
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x0600260D RID: 9741 RVA: 0x0011A548 File Offset: 0x00118948
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture component = base.GetComponent<UITexture>();
			component.mainTexture = this.mTex;
			if (this.pixelPerfect)
			{
				component.MakePixelPerfect();
			}
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x0011A563 File Offset: 0x00118963
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			UnityEngine.Object.Destroy(this.mTex);
		}
	}

	// Token: 0x040026C8 RID: 9928
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x040026C9 RID: 9929
	public bool pixelPerfect = true;

	// Token: 0x040026CA RID: 9930
	private Texture2D mTex;
}
