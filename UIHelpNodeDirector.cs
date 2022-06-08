using System;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class UIHelpNodeDirector : MonoBehaviour
{
	// Token: 0x060014FC RID: 5372 RVA: 0x000B51B5 File Offset: 0x000B35B5
	private void Awake()
	{
		UIHelpNodeDirector.mInstance = this;
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x000B51BD File Offset: 0x000B35BD
	private void OnDestroy()
	{
		if (UIHelpNodeDirector.mInstance == this)
		{
			UIHelpNodeDirector.mInstance = null;
		}
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000B51D5 File Offset: 0x000B35D5
	private void Start()
	{
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x000B51D7 File Offset: 0x000B35D7
	private void Update()
	{
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x000B51DC File Offset: 0x000B35DC
	public void PreBtnPressed()
	{
		this.curIndex--;
		if (this.curIndex < 0)
		{
			this.curIndex = UIHelpNodeDirector.totalNum - 1;
		}
		this.tex = (Resources.Load("UI/Images/Help/UIHelp" + (this.curIndex + 1).ToString()) as Texture);
		this.contentTexture.mainTexture = this.tex;
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000B5254 File Offset: 0x000B3654
	public void NextBtnPressed()
	{
		this.curIndex++;
		if (this.curIndex > UIHelpNodeDirector.totalNum - 1)
		{
			this.curIndex = 0;
		}
		this.tex = (Resources.Load("UI/Images/Help/UIHelp" + (this.curIndex + 1).ToString()) as Texture);
		this.contentTexture.mainTexture = this.tex;
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000B52C9 File Offset: 0x000B36C9
	public void BackBtnPressed()
	{
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.helpNode);
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000B52DF File Offset: 0x000B36DF
	public void HelpBtnPressed()
	{
		this.tex = (Resources.Load("UI/Images/Help/UIHelp1") as Texture);
		this.contentTexture.mainTexture = this.tex;
	}

	// Token: 0x040017B1 RID: 6065
	public static UIHelpNodeDirector mInstance;

	// Token: 0x040017B2 RID: 6066
	private static int totalNum = 4;

	// Token: 0x040017B3 RID: 6067
	public UITexture contentTexture;

	// Token: 0x040017B4 RID: 6068
	private int curIndex;

	// Token: 0x040017B5 RID: 6069
	private Texture tex;
}
