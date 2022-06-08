using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E9 RID: 1001
public class ButtonUIEvent : MonoBehaviour
{
	// Token: 0x06001E0E RID: 7694 RVA: 0x000E707B File Offset: 0x000E547B
	public void Down()
	{
		this.downText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downText));
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x000E70A0 File Offset: 0x000E54A0
	public void Up()
	{
		this.upText.text = "YES";
		base.StartCoroutine(this.ClearText(this.upText));
		base.StartCoroutine(this.ClearText(this.pressText));
		base.StartCoroutine(this.ClearText(this.pressValueText));
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x000E70F6 File Offset: 0x000E54F6
	public void Press()
	{
		this.pressText.text = "YES";
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x000E7108 File Offset: 0x000E5508
	public void PressValue(float value)
	{
		this.pressValueText.text = value.ToString();
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x000E7124 File Offset: 0x000E5524
	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	// Token: 0x04001F2E RID: 7982
	public Text downText;

	// Token: 0x04001F2F RID: 7983
	public Text pressText;

	// Token: 0x04001F30 RID: 7984
	public Text pressValueText;

	// Token: 0x04001F31 RID: 7985
	public Text upText;
}
