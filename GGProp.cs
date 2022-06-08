using System;
using UnityEngine;

// Token: 0x020004EC RID: 1260
public class GGProp : MonoBehaviour
{
	// Token: 0x0600237E RID: 9086 RVA: 0x0010FA6A File Offset: 0x0010DE6A
	private void Start()
	{
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x0010FA6C File Offset: 0x0010DE6C
	private void Update()
	{
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x0010FA6E File Offset: 0x0010DE6E
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			GGNetworkKit.mInstance.DestroySceneObjectRPC(base.gameObject);
		}
	}
}
