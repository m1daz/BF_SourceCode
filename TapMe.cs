using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class TapMe : MonoBehaviour
{
	// Token: 0x06001B90 RID: 7056 RVA: 0x000DC82C File Offset: 0x000DAC2C
	private void OnEnable()
	{
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x000DC83F File Offset: 0x000DAC3F
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B92 RID: 7058 RVA: 0x000DC847 File Offset: 0x000DAC47
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B93 RID: 7059 RVA: 0x000DC84F File Offset: 0x000DAC4F
	private void UnsubscribeEvent()
	{
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
	}

	// Token: 0x06001B94 RID: 7060 RVA: 0x000DC864 File Offset: 0x000DAC64
	private void On_SimpleTap(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
	}
}
