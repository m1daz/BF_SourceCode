using System;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class WordFilter : MonoBehaviour
{
	// Token: 0x06000D34 RID: 3380 RVA: 0x0006D315 File Offset: 0x0006B715
	private void Awake()
	{
		WordFilter.mInstance = this;
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0006D31D File Offset: 0x0006B71D
	private void OnDestroy()
	{
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0006D320 File Offset: 0x0006B720
	private void Start()
	{
		string[] wf_DirtyWordList = this.WF_DirtyWordStr.Split(new char[]
		{
			'@'
		});
		this.WF_DirtyWordList = wf_DirtyWordList;
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0006D34C File Offset: 0x0006B74C
	private string MyReplace(string strSource, string strRe, string strTo)
	{
		string text = strSource.ToLower();
		string value = strRe.ToLower();
		int num = text.IndexOf(value);
		if (num != -1)
		{
			strSource = strSource.Substring(0, num) + strTo + this.MyReplace(strSource.Substring(num + strRe.Length), strRe, strTo);
		}
		return strSource;
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0006D3A0 File Offset: 0x0006B7A0
	public string FilterString(string orStr)
	{
		string text = orStr;
		for (int i = 0; i < this.WF_DirtyWordList.Length; i++)
		{
			text = this.MyReplace(text, this.WF_DirtyWordList[i], "***");
		}
		return text;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0006D3DE File Offset: 0x0006B7DE
	private void Update()
	{
	}

	// Token: 0x04000D36 RID: 3382
	public static WordFilter mInstance;

	// Token: 0x04000D37 RID: 3383
	public string WF_DirtyWordStr;

	// Token: 0x04000D38 RID: 3384
	public string[] WF_DirtyWordList;
}
