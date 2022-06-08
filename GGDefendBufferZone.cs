using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class GGDefendBufferZone : MonoBehaviour
{
	// Token: 0x06001033 RID: 4147 RVA: 0x0008A856 File Offset: 0x00088C56
	private void Start()
	{
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0008A858 File Offset: 0x00088C58
	private void Update()
	{
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0008A85A File Offset: 0x00088C5A
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player")
		{
			other.gameObject.transform.SendMessageUpwards("HuntingModePlayerEnterDefendZone", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0008A896 File Offset: 0x00088C96
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player")
		{
			other.gameObject.transform.SendMessageUpwards("HuntingModePlayerLeaveDefendZone", SendMessageOptions.DontRequireReceiver);
		}
	}
}
