using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class TouchMe : MonoBehaviour
{
	// Token: 0x06001B96 RID: 7062 RVA: 0x000DC8D6 File Offset: 0x000DACD6
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x000DC90B File Offset: 0x000DAD0B
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x000DC913 File Offset: 0x000DAD13
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x000DC91B File Offset: 0x000DAD1B
	private void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x000DC950 File Offset: 0x000DAD50
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x000DC979 File Offset: 0x000DAD79
	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x000DC997 File Offset: 0x000DAD97
	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
		}
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x000DC9D4 File Offset: 0x000DADD4
	private void On_TouchUp(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Touch me";
		}
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x000DCA24 File Offset: 0x000DAE24
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001D8F RID: 7567
	private TextMesh textMesh;

	// Token: 0x04001D90 RID: 7568
	private Color startColor;
}
