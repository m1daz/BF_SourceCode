using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003EB RID: 1003
public class ControlUIInput : MonoBehaviour
{
	// Token: 0x06001E25 RID: 7717 RVA: 0x000E7598 File Offset: 0x000E5998
	private void Update()
	{
		this.getAxisText.text = ETCInput.GetAxis("Horizontal").ToString("f2");
		this.getAxisSpeedText.text = ETCInput.GetAxisSpeed("Horizontal").ToString("f2");
		this.getAxisYText.text = ETCInput.GetAxis("Vertical").ToString("f2");
		this.getAxisYSpeedText.text = ETCInput.GetAxisSpeed("Vertical").ToString("f2");
		if (ETCInput.GetAxisDownRight("Horizontal"))
		{
			this.downRightText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downRightText));
		}
		if (ETCInput.GetAxisDownDown("Vertical"))
		{
			this.downDownText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downDownText));
		}
		if (ETCInput.GetAxisDownLeft("Horizontal"))
		{
			this.downLeftText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downLeftText));
		}
		if (ETCInput.GetAxisDownUp("Vertical"))
		{
			this.downUpText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downUpText));
		}
		if (ETCInput.GetAxisPressedRight("Horizontal"))
		{
			this.rightText.text = "YES";
		}
		else
		{
			this.rightText.text = string.Empty;
		}
		if (ETCInput.GetAxisPressedDown("Vertical"))
		{
			this.downText.text = "YES";
		}
		else
		{
			this.downText.text = string.Empty;
		}
		if (ETCInput.GetAxisPressedLeft("Horizontal"))
		{
			this.leftText.text = "Yes";
		}
		else
		{
			this.leftText.text = string.Empty;
		}
		if (ETCInput.GetAxisPressedUp("Vertical"))
		{
			this.upText.text = "YES";
		}
		else
		{
			this.upText.text = string.Empty;
		}
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x000E77C8 File Offset: 0x000E5BC8
	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	// Token: 0x04001F44 RID: 8004
	public Text getAxisText;

	// Token: 0x04001F45 RID: 8005
	public Text getAxisSpeedText;

	// Token: 0x04001F46 RID: 8006
	public Text getAxisYText;

	// Token: 0x04001F47 RID: 8007
	public Text getAxisYSpeedText;

	// Token: 0x04001F48 RID: 8008
	public Text downRightText;

	// Token: 0x04001F49 RID: 8009
	public Text downDownText;

	// Token: 0x04001F4A RID: 8010
	public Text downLeftText;

	// Token: 0x04001F4B RID: 8011
	public Text downUpText;

	// Token: 0x04001F4C RID: 8012
	public Text rightText;

	// Token: 0x04001F4D RID: 8013
	public Text downText;

	// Token: 0x04001F4E RID: 8014
	public Text leftText;

	// Token: 0x04001F4F RID: 8015
	public Text upText;
}
