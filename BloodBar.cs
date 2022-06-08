using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class BloodBar : MonoBehaviour
{
	// Token: 0x06000D4C RID: 3404 RVA: 0x0006E4D5 File Offset: 0x0006C8D5
	private void Awake()
	{
		this.m_totalNum = 100;
		this.m_blood = 100;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0006E4E8 File Offset: 0x0006C8E8
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
		this.mBloodTransform = base.transform.Find("blood");
		this.mNameTransform = base.transform.Find("Name");
		this.mNameTextMesh = this.mNameTransform.GetComponent<TextMesh>();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0006E5B7 File Offset: 0x0006C9B7
	private void OnDestroy()
	{
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0006E5BC File Offset: 0x0006C9BC
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
					base.gameObject.SetActive(false);
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

	// Token: 0x06000D50 RID: 3408 RVA: 0x0006E6BD File Offset: 0x0006CABD
	public void SetTotalBloodNum(int totalbloodnum)
	{
		this.m_totalNum = totalbloodnum;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0006E6C6 File Offset: 0x0006CAC6
	public void SetBloodNum(int bloodnum)
	{
		this.m_blood = bloodnum;
	}

	// Token: 0x04000D3E RID: 3390
	private int m_totalNum;

	// Token: 0x04000D3F RID: 3391
	private int m_blood;

	// Token: 0x04000D40 RID: 3392
	private Transform looktTarget;

	// Token: 0x04000D41 RID: 3393
	private GameObject mainPlayer;

	// Token: 0x04000D42 RID: 3394
	private GGNetworkCharacter mNetWorkCharacter;

	// Token: 0x04000D43 RID: 3395
	private GGNetWorkPlayerlogic mainPlayerNetWorkPlayerlogic;

	// Token: 0x04000D44 RID: 3396
	private GGNetworkCharacter mainPlayerNetWorkCharacter;

	// Token: 0x04000D45 RID: 3397
	private Transform mainCamera;

	// Token: 0x04000D46 RID: 3398
	private Transform mBloodTransform;

	// Token: 0x04000D47 RID: 3399
	private Transform mNameTransform;

	// Token: 0x04000D48 RID: 3400
	private TextMesh mNameTextMesh;
}
