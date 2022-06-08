using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class TwoLongTapMe : MonoBehaviour
{
	// Token: 0x06001BC8 RID: 7112 RVA: 0x000DD1F0 File Offset: 0x000DB5F0
	private void OnEnable()
	{
		EasyTouch.On_LongTapStart2Fingers += this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers += this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers += this.On_LongTapEnd2Fingers;
		EasyTouch.On_Cancel2Fingers += this.On_Cancel2Fingers;
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x000DD241 File Offset: 0x000DB641
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000DD249 File Offset: 0x000DB649
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x000DD254 File Offset: 0x000DB654
	private void UnsubscribeEvent()
	{
		EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000DD2A5 File Offset: 0x000DB6A5
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000DD2CE File Offset: 0x000DB6CE
	private void On_LongTapStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x000DD2EC File Offset: 0x000DB6EC
	private void On_LongTap2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = gesture.actionTime.ToString("f2");
		}
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x000DD320 File Offset: 0x000DB720
	private void On_LongTapEnd2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Long tap me";
		}
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000DD36E File Offset: 0x000DB76E
	private void On_Cancel2Fingers(Gesture gesture)
	{
		this.On_LongTapEnd2Fingers(gesture);
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x000DD378 File Offset: 0x000DB778
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001D96 RID: 7574
	private TextMesh textMesh;

	// Token: 0x04001D97 RID: 7575
	private Color startColor;
}
