using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003EA RID: 1002
public class ControlUIEvent : MonoBehaviour
{
	// Token: 0x06001E14 RID: 7700 RVA: 0x000E71E4 File Offset: 0x000E55E4
	private void Update()
	{
		if (this.isDown)
		{
			this.downText.text = "YES";
			this.isDown = false;
		}
		else
		{
			this.downText.text = string.Empty;
		}
		if (this.isLeft)
		{
			this.leftText.text = "YES";
			this.isLeft = false;
		}
		else
		{
			this.leftText.text = string.Empty;
		}
		if (this.isUp)
		{
			this.upText.text = "YES";
			this.isUp = false;
		}
		else
		{
			this.upText.text = string.Empty;
		}
		if (this.isRight)
		{
			this.rightText.text = "YES";
			this.isRight = false;
		}
		else
		{
			this.rightText.text = string.Empty;
		}
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x000E72CD File Offset: 0x000E56CD
	public void MoveStart()
	{
		this.moveStartText.text = "YES";
		base.StartCoroutine(this.ClearText(this.moveStartText));
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x000E72F2 File Offset: 0x000E56F2
	public void Move(Vector2 move)
	{
		this.moveText.text = move.ToString();
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x000E730C File Offset: 0x000E570C
	public void MoveSpeed(Vector2 move)
	{
		this.moveSpeedText.text = move.ToString();
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x000E7328 File Offset: 0x000E5728
	public void MoveEnd()
	{
		if (this.moveEndText.enabled)
		{
			this.moveEndText.text = "YES";
			base.StartCoroutine(this.ClearText(this.moveEndText));
			base.StartCoroutine(this.ClearText(this.touchUpText));
			base.StartCoroutine(this.ClearText(this.moveText));
			base.StartCoroutine(this.ClearText(this.moveSpeedText));
		}
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x000E73A1 File Offset: 0x000E57A1
	public void TouchStart()
	{
		this.touchStartText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchStartText));
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x000E73C8 File Offset: 0x000E57C8
	public void TouchUp()
	{
		this.touchUpText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchUpText));
		base.StartCoroutine(this.ClearText(this.moveText));
		base.StartCoroutine(this.ClearText(this.moveSpeedText));
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x000E741E File Offset: 0x000E581E
	public void DownRight()
	{
		this.downRightText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downRightText));
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x000E7443 File Offset: 0x000E5843
	public void DownDown()
	{
		this.downDownText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downDownText));
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x000E7468 File Offset: 0x000E5868
	public void DownLeft()
	{
		this.downLeftText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downLeftText));
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x000E748D File Offset: 0x000E588D
	public void DownUp()
	{
		this.downUpText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downUpText));
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x000E74B2 File Offset: 0x000E58B2
	public void Right()
	{
		this.isRight = true;
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x000E74BB File Offset: 0x000E58BB
	public void Down()
	{
		this.isDown = true;
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x000E74C4 File Offset: 0x000E58C4
	public void Left()
	{
		this.isLeft = true;
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x000E74CD File Offset: 0x000E58CD
	public void Up()
	{
		this.isUp = true;
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x000E74D8 File Offset: 0x000E58D8
	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	// Token: 0x04001F32 RID: 7986
	public Text moveStartText;

	// Token: 0x04001F33 RID: 7987
	public Text moveText;

	// Token: 0x04001F34 RID: 7988
	public Text moveSpeedText;

	// Token: 0x04001F35 RID: 7989
	public Text moveEndText;

	// Token: 0x04001F36 RID: 7990
	public Text touchStartText;

	// Token: 0x04001F37 RID: 7991
	public Text touchUpText;

	// Token: 0x04001F38 RID: 7992
	public Text downRightText;

	// Token: 0x04001F39 RID: 7993
	public Text downDownText;

	// Token: 0x04001F3A RID: 7994
	public Text downLeftText;

	// Token: 0x04001F3B RID: 7995
	public Text downUpText;

	// Token: 0x04001F3C RID: 7996
	public Text rightText;

	// Token: 0x04001F3D RID: 7997
	public Text downText;

	// Token: 0x04001F3E RID: 7998
	public Text leftText;

	// Token: 0x04001F3F RID: 7999
	public Text upText;

	// Token: 0x04001F40 RID: 8000
	private bool isDown;

	// Token: 0x04001F41 RID: 8001
	private bool isLeft;

	// Token: 0x04001F42 RID: 8002
	private bool isUp;

	// Token: 0x04001F43 RID: 8003
	private bool isRight;
}
