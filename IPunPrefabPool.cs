using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
public interface IPunPrefabPool
{
	// Token: 0x060007BA RID: 1978
	GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation);

	// Token: 0x060007BB RID: 1979
	void Destroy(GameObject gameObject);
}
