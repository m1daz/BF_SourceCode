using System;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class WordFilterInLogin : MonoBehaviour
{
	// Token: 0x06000D3B RID: 3387 RVA: 0x0006D3E8 File Offset: 0x0006B7E8
	private void Awake()
	{
		WordFilterInLogin.mInstance = this;
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0006D3F0 File Offset: 0x0006B7F0
	private void OnDestroy()
	{
		WordFilterInLogin.mInstance = null;
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0006D3F8 File Offset: 0x0006B7F8
	private void Start()
	{
		string[] wf_DirtyWordList = this.WF_DirtyWordStr.Split(new char[]
		{
			'@'
		});
		this.WF_DirtyWordList = wf_DirtyWordList;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0006D424 File Offset: 0x0006B824
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

	// Token: 0x06000D3F RID: 3391 RVA: 0x0006D478 File Offset: 0x0006B878
	public string FilterString(string orStr)
	{
		string text = orStr;
		for (int i = 0; i < this.WF_DirtyWordList.Length; i++)
		{
			text = this.MyReplace(text, this.WF_DirtyWordList[i], "***");
		}
		return text;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0006D4B6 File Offset: 0x0006B8B6
	private void Update()
	{
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0006D4B8 File Offset: 0x0006B8B8
	public static bool CheckString(string inputString)
	{
		Regex regex = new Regex("^[A-Za-z0-9]+$");
		Match match = regex.Match(inputString);
		return match.Success;
	}

	// Token: 0x04000D39 RID: 3385
	public static WordFilterInLogin mInstance;

	// Token: 0x04000D3A RID: 3386
	public string WF_DirtyWordStr;

	// Token: 0x04000D3B RID: 3387
	public string[] WF_DirtyWordList;
}
