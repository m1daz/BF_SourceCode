using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class MutliFingersScreenTouch : MonoBehaviour
{
	// Token: 0x06001B6A RID: 7018 RVA: 0x000DC1DE File Offset: 0x000DA5DE
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x000DC1F1 File Offset: 0x000DA5F1
	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x000DC204 File Offset: 0x000DA604
	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.pickedObject == null)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(5f);
			UnityEngine.Object.Instantiate<GameObject>(this.touchGameObject, touchToWorldPoint, Quaternion.identity).GetComponent<FingerTouch>().InitTouch(gesture.fingerIndex);
		}
	}

	// Token: 0x04001D86 RID: 7558
	public GameObject touchGameObject;
}
