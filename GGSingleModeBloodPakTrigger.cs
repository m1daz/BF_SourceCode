using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class GGSingleModeBloodPakTrigger : MonoBehaviour
{
	// Token: 0x060011AA RID: 4522 RVA: 0x000A177E File Offset: 0x0009FB7E
	private void Start()
	{
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x000A1780 File Offset: 0x0009FB80
	private void Update()
	{
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x000A1782 File Offset: 0x0009FB82
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GGSingleModeSupplyManager.mInstance.BloodPakGetInScene();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
