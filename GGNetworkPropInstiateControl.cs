using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class GGNetworkPropInstiateControl : MonoBehaviour
{
	// Token: 0x06001113 RID: 4371 RVA: 0x000979F0 File Offset: 0x00095DF0
	private void Start()
	{
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			this.isSportMode = true;
		}
		else if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Entertainment)
		{
			this.propPositions = GameObject.Find("PropPositions").transform;
			this.propCount = this.propPositions.childCount;
			this.propTransforms = new Transform[this.propCount];
			for (int i = 0; i < this.propCount; i++)
			{
				this.propTransforms[i] = this.propPositions.GetChild(i);
			}
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.isMutationMode = true;
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			int maxPlayerSet = GGNetWorkAIDifficultyControl.mInstance.maxPlayerSet;
			if (maxPlayerSet == 1)
			{
				this.propGenerateTimeCount = 20f;
			}
			else if (maxPlayerSet == 4)
			{
				this.propGenerateTimeCount = 6f;
			}
		}
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x00097AE8 File Offset: 0x00095EE8
	private void Update()
	{
		if (this.isSportMode)
		{
			return;
		}
		this.propGenerateTime += Time.deltaTime;
		if (this.propGenerateTime > this.propGenerateTimeCount)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
				{
					if (GGNetworkKit.mInstance.GetPropNum() < this.maxHuntingPropCount)
					{
						int propRandomPositionIndex = GGNetworkKit.mInstance.GetPropRandomPositionIndex(this.propCount);
						object[] data = new object[]
						{
							propRandomPositionIndex
						};
						GGNetworkKit.mInstance.CreateSeceneObject(this.huntingmodeProps[UnityEngine.Random.Range(0, this.huntingModePropCount)], this.propTransforms[propRandomPositionIndex].position, data);
					}
				}
				else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
				{
					if (GGNetworkKit.mInstance.GetPropNum() < this.maxMutationPropCount)
					{
						if (UnityEngine.Random.Range(0, 2) == 0)
						{
							int propRandomPositionIndex2 = GGNetworkKit.mInstance.GetPropRandomPositionIndex(this.propCount);
							object[] data2 = new object[]
							{
								propRandomPositionIndex2
							};
							GGNetworkKit.mInstance.CreateSeceneObject(this.commonProps[UnityEngine.Random.Range(0, this.commonPropCount)], this.propTransforms[propRandomPositionIndex2].position, data2);
						}
						else
						{
							int propRandomPositionIndex3 = GGNetworkKit.mInstance.GetPropRandomPositionIndex(this.propCount);
							object[] data3 = new object[]
							{
								propRandomPositionIndex3
							};
							GGNetworkKit.mInstance.CreateSeceneObject(this.mutationmodeProps[UnityEngine.Random.Range(0, this.mutationModePropCount)], this.propTransforms[propRandomPositionIndex3].position, data3);
						}
					}
				}
				else if (GGNetworkKit.mInstance.GetPropNum() < this.maxPropCount)
				{
					int propRandomPositionIndex4 = GGNetworkKit.mInstance.GetPropRandomPositionIndex(this.propCount);
					object[] data4 = new object[]
					{
						propRandomPositionIndex4
					};
					GGNetworkKit.mInstance.CreateSeceneObject(this.commonProps[UnityEngine.Random.Range(0, this.commonPropCount)], this.propTransforms[propRandomPositionIndex4].position, data4);
				}
			}
			this.propGenerateTime = 0f;
		}
	}

	// Token: 0x0400137A RID: 4986
	private bool isMutationMode;

	// Token: 0x0400137B RID: 4987
	private int commonPropCount = 5;

	// Token: 0x0400137C RID: 4988
	private int mutationModePropCount = 5;

	// Token: 0x0400137D RID: 4989
	private int huntingModePropCount = 2;

	// Token: 0x0400137E RID: 4990
	private string[] commonProps = new string[]
	{
		"GGPropHpRecover",
		"GGPropAttackEnhance",
		"GGPropArmorEnhance",
		"GGPropSpeedEnhance",
		"GGPropJumpEnhance"
	};

	// Token: 0x0400137F RID: 4991
	private string[] mutationmodeProps = new string[]
	{
		"GGPropBurstBullet",
		"GGPropDamageImmune",
		"GGPropAntivenom",
		"GGPropSpeedTrap",
		"GGPropInvisiblePotion"
	};

	// Token: 0x04001380 RID: 4992
	private string[] huntingmodeProps = new string[]
	{
		"GGPropHpRecover_HuntingMode",
		"GGPropBullet_HuntingMode"
	};

	// Token: 0x04001381 RID: 4993
	private bool isSportMode;

	// Token: 0x04001382 RID: 4994
	public Transform propPositions;

	// Token: 0x04001383 RID: 4995
	private Transform[] propTransforms;

	// Token: 0x04001384 RID: 4996
	private int propCount = 30;

	// Token: 0x04001385 RID: 4997
	private float propGenerateTimeCount = 10f;

	// Token: 0x04001386 RID: 4998
	private int maxPropCount = 6;

	// Token: 0x04001387 RID: 4999
	private int maxMutationPropCount = 12;

	// Token: 0x04001388 RID: 5000
	private int maxHuntingPropCount = 4;

	// Token: 0x04001389 RID: 5001
	private float propGenerateTime;
}
