using System;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class TeamIcon : MonoBehaviour
{
	// Token: 0x06000DD0 RID: 3536 RVA: 0x00072B7A File Offset: 0x00070F7A
	private void Awake()
	{
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00072B7C File Offset: 0x00070F7C
	private void Start()
	{
		this.mNetWorkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
		this.mainPlayer = GameObject.FindWithTag("Player");
		if (this.mainPlayer != null)
		{
			this.mainPlayerNetWorkPlayerlogic = this.mainPlayer.GetComponent<GGNetWorkPlayerlogic>();
			this.mainCamera = this.mainPlayer.transform.Find("LookObject/Main Camera").transform;
			this.mainPlayerNetWorkCharacter = this.mainPlayer.GetComponent<GGNetworkCharacter>();
		}
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x00072C02 File Offset: 0x00071002
	private void OnDestroy()
	{
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00072C04 File Offset: 0x00071004
	private void Update()
	{
		if (this.mainPlayer == null)
		{
			if (Time.frameCount % 32 == 0)
			{
				this.mainPlayer = GameObject.FindWithTag("Player");
				if (this.mainPlayer != null)
				{
					this.mainPlayerNetWorkPlayerlogic = this.mainPlayer.GetComponent<GGNetWorkPlayerlogic>();
					this.mainCamera = this.mainPlayer.transform.Find("LookObject/Main Camera").transform;
					this.mainPlayerNetWorkCharacter = this.mainPlayer.GetComponent<GGNetworkCharacter>();
				}
			}
			return;
		}
		if (!this.mainPlayerNetWorkCharacter.mPlayerProperties.isObserver)
		{
			base.transform.LookAt(this.mainCamera, Vector3.up);
		}
		else if (this.mainPlayerNetWorkPlayerlogic.ObserverCamera != null)
		{
			base.transform.LookAt(this.mainPlayerNetWorkPlayerlogic.ObserverCamera.transform, Vector3.up);
		}
	}

	// Token: 0x04000E55 RID: 3669
	private Transform looktTarget;

	// Token: 0x04000E56 RID: 3670
	private GameObject mainPlayer;

	// Token: 0x04000E57 RID: 3671
	private GGNetworkCharacter mNetWorkCharacter;

	// Token: 0x04000E58 RID: 3672
	private GGNetWorkPlayerlogic mainPlayerNetWorkPlayerlogic;

	// Token: 0x04000E59 RID: 3673
	private GGNetworkCharacter mainPlayerNetWorkCharacter;

	// Token: 0x04000E5A RID: 3674
	private Transform mainCamera;
}
