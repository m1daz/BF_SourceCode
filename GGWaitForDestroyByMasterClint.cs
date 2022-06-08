using System;
using UnityEngine;

// Token: 0x02000221 RID: 545
public class GGWaitForDestroyByMasterClint : MonoBehaviour
{
	// Token: 0x06000EA1 RID: 3745 RVA: 0x0007A50F File Offset: 0x0007890F
	private void Awake()
	{
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0007A511 File Offset: 0x00078911
	private void Start()
	{
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0007A514 File Offset: 0x00078914
	private void Update()
	{
		this.checkTime += Time.deltaTime;
		if (this.checkTime > this.lifeTime)
		{
			this.checkTime = 0f;
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
			}
		}
	}

	// Token: 0x04000FF9 RID: 4089
	public float lifeTime = 2f;

	// Token: 0x04000FFA RID: 4090
	private float checkTime;
}
