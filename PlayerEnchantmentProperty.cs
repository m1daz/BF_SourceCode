using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class PlayerEnchantmentProperty
{
	// Token: 0x06000A9E RID: 2718 RVA: 0x0004CB88 File Offset: 0x0004AF88
	public PlayerEnchantmentProperty()
	{
		this.allDic = new Dictionary<EnchantmentType, EnchantmentDetails>();
		this.equipAdditionDic = new Dictionary<EnchantmentType, EnchantmentDetails>();
		this.potionAdditionDic = new Dictionary<EnchantmentType, EnchantmentDetails>();
		this.scenePropsAdditionDic = new Dictionary<EnchantmentType, EnchantmentDetails>();
		this.tickedAdditionList = new List<EnchantmentDetails>();
		int length = Enum.GetNames(Type.GetType("EnchantmentType")).GetLength(0);
		for (int i = 0; i < length; i++)
		{
			string text = Enum.GetNames(Type.GetType("EnchantmentType"))[i];
			EnchantmentType enchantmentType = (EnchantmentType)Enum.GetValues(Type.GetType("EnchantmentType")).GetValue(i);
			this.allDic.Add(enchantmentType, EnchantmentDetails.NewEmptyEnchantmentDetails(enchantmentType));
			this.scenePropsAdditionDic.Add(enchantmentType, EnchantmentDetails.NewEmptyEnchantmentDetails(enchantmentType));
			this.equipAdditionDic.Add(enchantmentType, EnchantmentDetails.NewEmptyEnchantmentDetails(enchantmentType));
			this.potionAdditionDic.Add(enchantmentType, EnchantmentDetails.NewEmptyEnchantmentDetails(enchantmentType));
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0004CC70 File Offset: 0x0004B070
	public void Init()
	{
		GCapeItemInfo curSettedCapeInfo = GrowthManagerKit.GetCurSettedCapeInfo();
		if (curSettedCapeInfo.mEnchantmentDetails.Count > 0)
		{
			foreach (EnchantmentDetails details in curSettedCapeInfo.mEnchantmentDetails)
			{
				this.AddProperty(EnchantmentOriginType.EquipAddition, details);
			}
		}
		GHatItemInfo curSettedHatInfo = GrowthManagerKit.GetCurSettedHatInfo();
		if (curSettedHatInfo.mEnchantmentDetails.Count > 0)
		{
			foreach (EnchantmentDetails details2 in curSettedHatInfo.mEnchantmentDetails)
			{
				this.AddProperty(EnchantmentOriginType.EquipAddition, details2);
			}
		}
		GBootItemInfo curSettedBootInfo = GrowthManagerKit.GetCurSettedBootInfo();
		if (curSettedBootInfo.mEnchantmentDetails.Count > 0)
		{
			foreach (EnchantmentDetails details3 in curSettedBootInfo.mEnchantmentDetails)
			{
				this.AddProperty(EnchantmentOriginType.EquipAddition, details3);
			}
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0004CDB4 File Offset: 0x0004B1B4
	public void ClearPotionAddition()
	{
		foreach (KeyValuePair<EnchantmentType, EnchantmentDetails> keyValuePair in this.potionAdditionDic)
		{
			keyValuePair.Value.Reset();
		}
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0004CE18 File Offset: 0x0004B218
	public void ClearScenePropsAddition()
	{
		foreach (KeyValuePair<EnchantmentType, EnchantmentDetails> keyValuePair in this.scenePropsAdditionDic)
		{
			keyValuePair.Value.Reset();
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0004CE7C File Offset: 0x0004B27C
	public void ClearEquipAddition()
	{
		foreach (KeyValuePair<EnchantmentType, EnchantmentDetails> keyValuePair in this.equipAdditionDic)
		{
			keyValuePair.Value.Reset();
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0004CEE0 File Offset: 0x0004B2E0
	public void ClearAllAddition()
	{
		this.ClearEquipAddition();
		this.ClearScenePropsAddition();
		this.ClearPotionAddition();
		foreach (KeyValuePair<EnchantmentType, EnchantmentDetails> keyValuePair in this.allDic)
		{
			keyValuePair.Value.Reset();
		}
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0004CF54 File Offset: 0x0004B354
	public void AddCustomScenePropsProperty(SceneEnchantmentProps propType, float additionValue)
	{
		if (propType == SceneEnchantmentProps.HCustomDamagePlus)
		{
			EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.DamagePlus, true, additionValue, 600f);
			enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.HCustomDamagePlus;
			this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
		}
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0004CF98 File Offset: 0x0004B398
	public void AddCustomScenePropsProperty(SceneEnchantmentProps propType, float additionValue, float validTimeRest)
	{
		if (propType != SceneEnchantmentProps.HCustomSpeedDown)
		{
			if (propType == SceneEnchantmentProps.HCustomDamagePlus)
			{
				EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.DamagePlus, true, additionValue, 600f);
				enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.HCustomDamagePlus;
				this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
			}
		}
		else
		{
			EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.SpeedPlus, true, additionValue, validTimeRest);
			enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.HCustomSpeedDown;
			this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
		}
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0004D004 File Offset: 0x0004B404
	public void AddScenePropsProperty(SceneEnchantmentProps propType)
	{
		switch (propType)
		{
		case SceneEnchantmentProps.HpAdd100:
			break;
		case SceneEnchantmentProps.TopSpeed:
		{
			EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.SpeedPlus, true, 1f, 15f);
			enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.TopSpeed;
			this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
			break;
		}
		case SceneEnchantmentProps.JumpPlus50:
		{
			EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.JumpPlus, true, 0.5f, 20f);
			enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.JumpPlus50;
			this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
			break;
		}
		case SceneEnchantmentProps.DamagePlus50:
		{
			EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.5f, 25f);
			enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.DamagePlus50;
			this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
			break;
		}
		case SceneEnchantmentProps.DamageReducation50:
		{
			EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.5f, 25f);
			enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.DamageReducation50;
			this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
			break;
		}
		default:
			switch (propType)
			{
			case SceneEnchantmentProps.MBurstBullet30S:
			{
				EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.MBurstBullet, true, 0.05f, 30f);
				enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.MBurstBullet30S;
				this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
				break;
			}
			case SceneEnchantmentProps.MIgnoreDamage10S:
			{
				EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.MIgnoreDamage, true, 1f, 15f);
				enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.MIgnoreDamage10S;
				this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
				break;
			}
			case SceneEnchantmentProps.MAntiVirusUntilDead:
			{
				EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.MAntiVirus, true, 1f, 1800f);
				enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.MAntiVirusUntilDead;
				this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
				break;
			}
			case SceneEnchantmentProps.MInvisibleBuff20S:
			{
				EnchantmentDetails enchantmentDetails = EnchantmentDetails.NewScenePropsEnchantmentDetails(EnchantmentType.MInvisibleBuff, true, 1f, 25f);
				enchantmentDetails.sceneBasicPropsType = SceneEnchantmentProps.MInvisibleBuff20S;
				this.AddProperty(EnchantmentOriginType.ScenePropsAddition, enchantmentDetails);
				break;
			}
			}
			break;
		}
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0004D19C File Offset: 0x0004B59C
	public void RemoveScenePropsProperty(SceneEnchantmentProps propType)
	{
		if (propType != SceneEnchantmentProps.MInvisibleBuff20S)
		{
			if (propType == SceneEnchantmentProps.HCustomDamagePlus)
			{
				this.scenePropsAdditionDic[EnchantmentType.DamagePlus].Reset();
			}
		}
		else
		{
			this.scenePropsAdditionDic[EnchantmentType.MInvisibleBuff].Reset();
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0004D1F4 File Offset: 0x0004B5F4
	public void AddProperty(EnchantmentOriginType originType, EnchantmentDetails details)
	{
		switch (originType)
		{
		case EnchantmentOriginType.ScenePropsAddition:
			if (!this.scenePropsAdditionDic[details.type].isEnabled)
			{
				this.scenePropsAdditionDic[details.type] = details;
				this.tickedAdditionList.Add(this.scenePropsAdditionDic[details.type]);
			}
			else
			{
				this.scenePropsAdditionDic[details.type].Update(details.type, details.isEnabled, details.additionValue, details.validTimeRest + this.scenePropsAdditionDic[details.type].validTimeRest);
			}
			break;
		case EnchantmentOriginType.EquipAddition:
			if (!this.equipAdditionDic[details.type].isEnabled)
			{
				this.equipAdditionDic[details.type] = details;
			}
			else
			{
				this.equipAdditionDic[details.type].Update(details.type, details.isEnabled, details.additionValue + this.equipAdditionDic[details.type].additionValue, details.triggerRate, details.triggerInterval, details.validTimeAfterTrigger);
			}
			break;
		case EnchantmentOriginType.PotionAddition:
			this.potionAdditionDic[details.type] = details;
			this.tickedAdditionList.Add(this.potionAdditionDic[details.type]);
			break;
		}
		EnchantmentDetails[] array = new EnchantmentDetails[]
		{
			this.equipAdditionDic[details.type],
			this.potionAdditionDic[details.type],
			this.scenePropsAdditionDic[details.type]
		};
		this.allDic[details.type].Update(details.type, array[0].isEnabled || array[1].isEnabled || array[2].isEnabled, array[0].additionValue + array[1].additionValue + array[2].additionValue, Mathf.Max(new float[]
		{
			array[0].triggerRate,
			array[1].triggerRate,
			array[2].triggerRate
		}), Mathf.Max(new float[]
		{
			array[0].triggerInterval,
			array[1].triggerInterval,
			array[2].triggerInterval
		}), Mathf.Max(new float[]
		{
			array[0].validTimeAfterTrigger,
			array[1].validTimeAfterTrigger,
			array[2].validTimeAfterTrigger
		}));
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0004D494 File Offset: 0x0004B894
	private void UpdateDetailsInAllDic(EnchantmentType type)
	{
		EnchantmentDetails[] array = new EnchantmentDetails[]
		{
			this.equipAdditionDic[type],
			this.potionAdditionDic[type],
			this.scenePropsAdditionDic[type]
		};
		this.allDic[type].Update(type, array[0].isEnabled || array[1].isEnabled || array[2].isEnabled, array[0].additionValue + array[1].additionValue + array[2].additionValue, Mathf.Max(new float[]
		{
			array[0].triggerRate,
			array[1].triggerRate,
			array[2].triggerRate
		}), Mathf.Max(new float[]
		{
			array[0].triggerInterval,
			array[1].triggerInterval,
			array[2].triggerInterval
		}), Mathf.Max(new float[]
		{
			array[0].validTimeAfterTrigger,
			array[1].validTimeAfterTrigger,
			array[2].validTimeAfterTrigger
		}));
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0004D5AC File Offset: 0x0004B9AC
	public void Update(float deltaTime)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.tickedAdditionList.Count; i++)
		{
			this.tickedAdditionList[i].validTimeRest = Mathf.Max(0f, this.tickedAdditionList[i].validTimeRest - deltaTime);
			if (this.tickedAdditionList[i].validTimeRest <= 0f)
			{
				this.tickedAdditionList[i].Reset();
				list.Add(i);
				this.UpdateDetailsInAllDic(this.tickedAdditionList[i].type);
			}
		}
		int num = 0;
		for (int j = 0; j < list.Count; j++)
		{
			if (this.tickedAdditionList[list[j] - num].originType == EnchantmentOriginType.ScenePropsAddition)
			{
				GrowthManagerKit.GenScenePropsInvalidEvent(this.tickedAdditionList[list[j] - num].sceneBasicPropsType);
			}
			this.tickedAdditionList.RemoveAt(list[j] - num);
			num++;
		}
	}

	// Token: 0x040009D0 RID: 2512
	public Dictionary<EnchantmentType, EnchantmentDetails> allDic;

	// Token: 0x040009D1 RID: 2513
	public Dictionary<EnchantmentType, EnchantmentDetails> equipAdditionDic;

	// Token: 0x040009D2 RID: 2514
	public Dictionary<EnchantmentType, EnchantmentDetails> potionAdditionDic;

	// Token: 0x040009D3 RID: 2515
	public Dictionary<EnchantmentType, EnchantmentDetails> scenePropsAdditionDic;

	// Token: 0x040009D4 RID: 2516
	public List<EnchantmentDetails> tickedAdditionList;
}
