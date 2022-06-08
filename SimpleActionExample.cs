using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class SimpleActionExample : MonoBehaviour
{
	// Token: 0x06001C13 RID: 7187 RVA: 0x000DE153 File Offset: 0x000DC553
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startScale = base.transform.localScale;
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x000DE172 File Offset: 0x000DC572
	public void ChangeColor(Gesture gesture)
	{
		this.RandomColor();
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x000DE17A File Offset: 0x000DC57A
	public void TimePressed(Gesture gesture)
	{
		this.textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x000DE1A4 File Offset: 0x000DC5A4
	public void DisplaySwipeAngle(Gesture gesture)
	{
		float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
		this.textMesh.text = swipeOrDragAngle.ToString("f2") + " / " + gesture.swipe.ToString();
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000DE1EA File Offset: 0x000DC5EA
	public void ChangeText(string text)
	{
		this.textMesh.text = text;
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x000DE1F8 File Offset: 0x000DC5F8
	public void ResetScale()
	{
		base.transform.localScale = this.startScale;
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x000DE20C File Offset: 0x000DC60C
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001DA1 RID: 7585
	private TextMesh textMesh;

	// Token: 0x04001DA2 RID: 7586
	private Vector3 startScale;
}
