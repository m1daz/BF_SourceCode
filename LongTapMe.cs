using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class LongTapMe : MonoBehaviour
{
	// Token: 0x06001B7E RID: 7038 RVA: 0x000DC554 File Offset: 0x000DA954
	private void OnEnable()
	{
		EasyTouch.On_LongTapStart += this.On_LongTapStart;
		EasyTouch.On_LongTap += this.On_LongTap;
		EasyTouch.On_LongTapEnd += this.On_LongTapEnd;
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x000DC589 File Offset: 0x000DA989
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x000DC591 File Offset: 0x000DA991
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x000DC599 File Offset: 0x000DA999
	private void UnsubscribeEvent()
	{
		EasyTouch.On_LongTapStart -= this.On_LongTapStart;
		EasyTouch.On_LongTap -= this.On_LongTap;
		EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x000DC5CE File Offset: 0x000DA9CE
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x000DC5F7 File Offset: 0x000DA9F7
	private void On_LongTapStart(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x000DC615 File Offset: 0x000DAA15
	private void On_LongTap(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = gesture.actionTime.ToString("f2");
		}
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x000DC648 File Offset: 0x000DAA48
	private void On_LongTapEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Long tap me";
		}
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x000DC698 File Offset: 0x000DAA98
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001D8B RID: 7563
	private TextMesh textMesh;

	// Token: 0x04001D8C RID: 7564
	private Color startColor;
}
