using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class TwoTapMe : MonoBehaviour
{
	// Token: 0x06001BDB RID: 7131 RVA: 0x000DD50C File Offset: 0x000DB90C
	private void OnEnable()
	{
		EasyTouch.On_SimpleTap2Fingers += this.On_SimpleTap2Fingers;
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x000DD51F File Offset: 0x000DB91F
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x000DD527 File Offset: 0x000DB927
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000DD52F File Offset: 0x000DB92F
	private void UnsubscribeEvent()
	{
		EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000DD542 File Offset: 0x000DB942
	private void On_SimpleTap2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000DD560 File Offset: 0x000DB960
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}
}
