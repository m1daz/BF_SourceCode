using System;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class OnJoinedInstantiate : MonoBehaviour
{
	// Token: 0x0600099E RID: 2462 RVA: 0x00048C84 File Offset: 0x00047084
	public void OnJoinedRoom()
	{
		if (this.PrefabsToInstantiate != null)
		{
			foreach (GameObject gameObject in this.PrefabsToInstantiate)
			{
				Debug.Log("Instantiating: " + gameObject.name);
				Vector3 a = Vector3.up;
				if (this.SpawnPosition != null)
				{
					a = this.SpawnPosition.position;
				}
				Vector3 a2 = UnityEngine.Random.insideUnitSphere;
				a2.y = 0f;
				a2 = a2.normalized;
				Vector3 position = a + this.PositionOffset * a2;
				PhotonNetwork.Instantiate(gameObject.name, position, Quaternion.identity, 0);
			}
		}
	}

	// Token: 0x04000896 RID: 2198
	public Transform SpawnPosition;

	// Token: 0x04000897 RID: 2199
	public float PositionOffset = 2f;

	// Token: 0x04000898 RID: 2200
	public GameObject[] PrefabsToInstantiate;
}
