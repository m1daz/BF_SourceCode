using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class GGNetWorkPropInHuntingMode : MonoBehaviour
{
	// Token: 0x0600103B RID: 4155 RVA: 0x0008AA60 File Offset: 0x00088E60
	private void Start()
	{
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			this.isSportMode = true;
		}
		else if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Entertainment)
		{
			this.propPositionsCount = base.transform.childCount;
			this.propTransforms = new Transform[this.propPositionsCount];
			for (int i = 0; i < this.propPositionsCount; i++)
			{
				this.propTransforms[i] = this.propPositions.GetChild(i);
				this.propExistIndex.Add(i);
			}
		}
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0008AAF4 File Offset: 0x00088EF4
	private void Update()
	{
		if (this.isSportMode)
		{
			return;
		}
		this.propGenerateTime += Time.deltaTime;
		if (this.propGenerateTime > this.propGenerateTimeCount)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.huntingPropPrefab[UnityEngine.Random.Range(0, this.huntingPropCount)], this.propTransforms[UnityEngine.Random.Range(0, this.propExistIndex.Count)].position, Quaternion.identity);
			this.propGenerateTime = 0f;
		}
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0008AB76 File Offset: 0x00088F76
	public void PropDestroyInHuntingMode(int propIndex)
	{
		this.maxPropCount--;
		this.propExistIndex.Add(propIndex);
	}

	// Token: 0x04001228 RID: 4648
	private bool isHuntingMode;

	// Token: 0x04001229 RID: 4649
	private int huntingPropCount = 2;

	// Token: 0x0400122A RID: 4650
	private string[] huntingProps = new string[]
	{
		"GGPropHpRecover",
		"GGPropBullet",
		"GGPropAttackEnhance",
		"GGPropArmorEnhance",
		"GGPropSpeedEnhance",
		"GGPropJumpEnhance"
	};

	// Token: 0x0400122B RID: 4651
	public GameObject[] huntingPropPrefab;

	// Token: 0x0400122C RID: 4652
	private bool isSportMode;

	// Token: 0x0400122D RID: 4653
	public Transform propPositions;

	// Token: 0x0400122E RID: 4654
	private Transform[] propTransforms;

	// Token: 0x0400122F RID: 4655
	private int propPositionsCount = 30;

	// Token: 0x04001230 RID: 4656
	private float propGenerateTimeCount = 30f;

	// Token: 0x04001231 RID: 4657
	public int maxPropCount;

	// Token: 0x04001232 RID: 4658
	private int maxMutationPropCount = 12;

	// Token: 0x04001233 RID: 4659
	private int PropCount;

	// Token: 0x04001234 RID: 4660
	private float propGenerateTime;

	// Token: 0x04001235 RID: 4661
	public List<int> propExistIndex = new List<int>();
}
