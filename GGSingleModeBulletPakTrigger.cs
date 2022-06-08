using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class GGSingleModeBulletPakTrigger : MonoBehaviour
{
	// Token: 0x060011B1 RID: 4529 RVA: 0x000A1A4B File Offset: 0x0009FE4B
	private void Start()
	{
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000A1A4D File Offset: 0x0009FE4D
	private void Update()
	{
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000A1A4F File Offset: 0x0009FE4F
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GGSingleModeSupplyManager.mInstance.BulletPakGetInScene();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
