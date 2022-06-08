using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class GGAttackBufferZone : MonoBehaviour
{
	// Token: 0x0600102E RID: 4142 RVA: 0x0008A7D2 File Offset: 0x00088BD2
	private void Start()
	{
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x0008A7D4 File Offset: 0x00088BD4
	private void Update()
	{
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x0008A7D6 File Offset: 0x00088BD6
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player")
		{
			other.gameObject.transform.SendMessageUpwards("HuntingModePlayerEnterAttackZone", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x0008A812 File Offset: 0x00088C12
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player")
		{
			other.gameObject.transform.SendMessageUpwards("HuntingModePlayerLeaveAttackZone", SendMessageOptions.DontRequireReceiver);
		}
	}
}
