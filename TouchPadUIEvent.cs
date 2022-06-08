using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F1 RID: 1009
public class TouchPadUIEvent : MonoBehaviour
{
	// Token: 0x06001E53 RID: 7763 RVA: 0x000E7F09 File Offset: 0x000E6309
	public void TouchDown()
	{
		this.touchDownText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchDownText));
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x000E7F2E File Offset: 0x000E632E
	public void TouchEvt(Vector2 value)
	{
		this.touchText.text = value.ToString();
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x000E7F48 File Offset: 0x000E6348
	public void TouchUp()
	{
		this.touchUpText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchUpText));
		base.StartCoroutine(this.ClearText(this.touchText));
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x000E7F80 File Offset: 0x000E6380
	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	// Token: 0x04001F5D RID: 8029
	public Text touchDownText;

	// Token: 0x04001F5E RID: 8030
	public Text touchText;

	// Token: 0x04001F5F RID: 8031
	public Text touchUpText;
}
