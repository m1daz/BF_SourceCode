using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class TwoTouchMe : MonoBehaviour
{
	// Token: 0x06001BE2 RID: 7138 RVA: 0x000DD5BC File Offset: 0x000DB9BC
	private void OnEnable()
	{
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
		EasyTouch.On_Cancel2Fingers += this.On_Cancel2Fingers;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000DD60D File Offset: 0x000DBA0D
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000DD615 File Offset: 0x000DBA15
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000DD620 File Offset: 0x000DBA20
	private void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000DD671 File Offset: 0x000DBA71
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000DD69A File Offset: 0x000DBA9A
	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000DD6B8 File Offset: 0x000DBAB8
	private void On_TouchDown2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
		}
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x000DD6F8 File Offset: 0x000DBAF8
	private void On_TouchUp2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Touch me";
		}
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x000DD746 File Offset: 0x000DBB46
	private void On_Cancel2Fingers(Gesture gesture)
	{
		this.On_TouchUp2Fingers(gesture);
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x000DD750 File Offset: 0x000DBB50
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001D9A RID: 7578
	private TextMesh textMesh;

	// Token: 0x04001D9B RID: 7579
	private Color startColor;
}
