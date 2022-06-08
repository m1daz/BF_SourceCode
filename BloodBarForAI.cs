using System;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class BloodBarForAI : MonoBehaviour
{
	// Token: 0x06000D53 RID: 3411 RVA: 0x0006E6D7 File Offset: 0x0006CAD7
	private void Awake()
	{
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0006E6DC File Offset: 0x0006CADC
	private void Start()
	{
		this.mNetWorkAIProperty = base.transform.root.GetComponent<GGNetWorkAIProperty>();
		this.mainPlayer = GameObject.FindWithTag("Player");
		if (this.mainPlayer != null)
		{
			this.mainCamera = this.mainPlayer.transform.Find("LookObject/Main Camera").transform;
		}
		this.mBloodTransform = base.transform.Find("blood");
		this.mNameTransform = base.transform.Find("Name");
		this.mNameTextMesh = this.mNameTransform.GetComponent<TextMesh>();
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0006E77D File Offset: 0x0006CB7D
	private void OnDestroy()
	{
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0006E780 File Offset: 0x0006CB80
	private void Update()
	{
		if (!this.isStart)
		{
			this.m_totalNum = this.mNetWorkAIProperty.mBlood;
			this.isStart = true;
		}
		this.m_blood = this.mNetWorkAIProperty.mBlood;
		float num = (float)this.m_blood / (float)this.m_totalNum;
		float x = -(1f - num);
		this.mBloodTransform.localPosition = new Vector3(x, this.mBloodTransform.localPosition.y, this.mBloodTransform.localPosition.z);
		this.mBloodTransform.localScale = new Vector3(num * 2f, this.mBloodTransform.localScale.y, this.mBloodTransform.localScale.z);
		if (this.mainPlayer == null)
		{
			if (Time.frameCount % 32 == 0)
			{
				this.mainPlayer = GameObject.FindWithTag("Player");
				if (this.mainPlayer != null)
				{
					this.mainCamera = this.mainPlayer.transform.Find("LookObject/Main Camera").transform;
				}
			}
			return;
		}
		base.transform.LookAt(this.mainCamera, Vector3.up);
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0006E8C6 File Offset: 0x0006CCC6
	public void SetTotalBloodNum(int totalbloodnum)
	{
		this.m_totalNum = totalbloodnum;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0006E8CF File Offset: 0x0006CCCF
	public void SetBloodNum(int bloodnum)
	{
		this.m_blood = bloodnum;
	}

	// Token: 0x04000D49 RID: 3401
	private int m_totalNum;

	// Token: 0x04000D4A RID: 3402
	private int m_blood;

	// Token: 0x04000D4B RID: 3403
	private Transform looktTarget;

	// Token: 0x04000D4C RID: 3404
	private GameObject mainPlayer;

	// Token: 0x04000D4D RID: 3405
	private GGNetWorkAIProperty mNetWorkAIProperty;

	// Token: 0x04000D4E RID: 3406
	private Transform mainCamera;

	// Token: 0x04000D4F RID: 3407
	private Transform mBloodTransform;

	// Token: 0x04000D50 RID: 3408
	private Transform mNameTransform;

	// Token: 0x04000D51 RID: 3409
	private TextMesh mNameTextMesh;

	// Token: 0x04000D52 RID: 3410
	private bool isStart;
}
