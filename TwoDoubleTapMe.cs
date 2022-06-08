using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class TwoDoubleTapMe : MonoBehaviour
{
	// Token: 0x06001BB7 RID: 7095 RVA: 0x000DCECB File Offset: 0x000DB2CB
	private void OnEnable()
	{
		EasyTouch.On_DoubleTap2Fingers += this.On_DoubleTap2Fingers;
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000DCEDE File Offset: 0x000DB2DE
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x000DCEE6 File Offset: 0x000DB2E6
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x000DCEEE File Offset: 0x000DB2EE
	private void UnsubscribeEvent()
	{
		EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x000DCF04 File Offset: 0x000DB304
	private void On_DoubleTap2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
	}
}
