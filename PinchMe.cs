using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000378 RID: 888
public class PinchMe : MonoBehaviour
{
	// Token: 0x06001BA0 RID: 7072 RVA: 0x000DCA80 File Offset: 0x000DAE80
	private void OnEnable()
	{
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_PinchIn += this.On_PinchIn;
		EasyTouch.On_PinchOut += this.On_PinchOut;
		EasyTouch.On_PinchEnd += this.On_PinchEnd;
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x000DCAD1 File Offset: 0x000DAED1
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x000DCAD9 File Offset: 0x000DAED9
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000DCAE4 File Offset: 0x000DAEE4
	private void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_PinchIn -= this.On_PinchIn;
		EasyTouch.On_PinchOut -= this.On_PinchOut;
		EasyTouch.On_PinchEnd -= this.On_PinchEnd;
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x000DCB35 File Offset: 0x000DAF35
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x000DCB43 File Offset: 0x000DAF43
	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			EasyTouch.SetEnableTwist(false);
			EasyTouch.SetEnablePinch(true);
		}
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x000DCB68 File Offset: 0x000DAF68
	private void On_PinchIn(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			float num = Time.deltaTime * gesture.deltaPinch;
			Vector3 localScale = base.transform.localScale;
			base.transform.localScale = new Vector3(localScale.x - num, localScale.y - num, localScale.z - num);
			this.textMesh.text = "Delta pinch : " + gesture.deltaPinch.ToString();
		}
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x000DCBF8 File Offset: 0x000DAFF8
	private void On_PinchOut(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			float num = Time.deltaTime * gesture.deltaPinch;
			Vector3 localScale = base.transform.localScale;
			base.transform.localScale = new Vector3(localScale.x + num, localScale.y + num, localScale.z + num);
			this.textMesh.text = "Delta pinch : " + gesture.deltaPinch.ToString();
		}
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x000DCC88 File Offset: 0x000DB088
	private void On_PinchEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
			EasyTouch.SetEnableTwist(true);
			this.textMesh.text = "Pinch me";
		}
	}

	// Token: 0x04001D91 RID: 7569
	private TextMesh textMesh;
}
