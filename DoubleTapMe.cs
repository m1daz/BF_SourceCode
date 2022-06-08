using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class DoubleTapMe : MonoBehaviour
{
	// Token: 0x06001B6E RID: 7022 RVA: 0x000DC257 File Offset: 0x000DA657
	private void OnEnable()
	{
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x000DC26A File Offset: 0x000DA66A
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x000DC272 File Offset: 0x000DA672
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x000DC27A File Offset: 0x000DA67A
	private void UnsubscribeEvent()
	{
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x000DC290 File Offset: 0x000DA690
	private void On_DoubleTap(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
	}
}
