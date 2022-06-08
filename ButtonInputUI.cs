using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E8 RID: 1000
public class ButtonInputUI : MonoBehaviour
{
	// Token: 0x06001E08 RID: 7688 RVA: 0x000E6EBC File Offset: 0x000E52BC
	private void Update()
	{
		if (ETCInput.GetButton("Button"))
		{
			this.getButtonText.text = "YES";
			this.getButtonTimeText.text = ETCInput.GetButtonValue("Button").ToString();
		}
		else
		{
			this.getButtonText.text = string.Empty;
			this.getButtonTimeText.text = string.Empty;
		}
		if (ETCInput.GetButtonDown("Button"))
		{
			this.getButtonDownText.text = "YES";
			base.StartCoroutine(this.ClearText(this.getButtonDownText));
		}
		if (ETCInput.GetButtonUp("Button"))
		{
			this.getButtonUpText.text = "YES";
			base.StartCoroutine(this.ClearText(this.getButtonUpText));
		}
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x000E6F94 File Offset: 0x000E5394
	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x000E6FAF File Offset: 0x000E53AF
	public void SetSwipeIn(bool value)
	{
		ETCInput.SetControlSwipeIn("Button", value);
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x000E6FBC File Offset: 0x000E53BC
	public void SetSwipeOut(bool value)
	{
		ETCInput.SetControlSwipeOut("Button", value);
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x000E6FC9 File Offset: 0x000E53C9
	public void setTimePush(bool value)
	{
		ETCInput.SetAxisOverTime("Button", value);
	}

	// Token: 0x04001F2A RID: 7978
	public Text getButtonDownText;

	// Token: 0x04001F2B RID: 7979
	public Text getButtonText;

	// Token: 0x04001F2C RID: 7980
	public Text getButtonTimeText;

	// Token: 0x04001F2D RID: 7981
	public Text getButtonUpText;
}
