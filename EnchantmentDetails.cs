using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class EnchantmentDetails
{
	// Token: 0x06000A91 RID: 2705 RVA: 0x0004C81C File Offset: 0x0004AC1C
	public EnchantmentDetails(EnchantmentType type, bool isEnabled, float additionValue, float triggerRate, float triggerInterval, float validTimeAfterTrigger)
	{
		this.type = type;
		this.isEnabled = isEnabled;
		this.additionValue = additionValue;
		this.triggerRate = triggerRate;
		this.triggerInterval = triggerInterval;
		this.validTimeAfterTrigger = validTimeAfterTrigger;
		this.logoSpriteName = "EP_" + type.ToString() + "_Logo";
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0004C88C File Offset: 0x0004AC8C
	public EnchantmentDetails(EnchantmentType type, bool isEnabled, float additionValue, float validTimeRest)
	{
		this.type = type;
		this.isEnabled = isEnabled;
		this.additionValue = additionValue;
		this.validTimeRest = validTimeRest;
		this.logoSpriteName = "EP_" + type.ToString() + "_Logo";
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0004C8EC File Offset: 0x0004ACEC
	public EnchantmentDetails(EnchantmentType type, bool isEnabled, float additionValue, float triggerRate, float triggerInterval, float validTimeAfterTrigger, float validTimeRest)
	{
		this.type = type;
		this.isEnabled = isEnabled;
		this.additionValue = additionValue;
		this.triggerRate = triggerRate;
		this.triggerInterval = triggerInterval;
		this.validTimeAfterTrigger = validTimeAfterTrigger;
		this.validTimeRest = validTimeRest;
		this.logoSpriteName = "EP_" + type.ToString() + "_Logo";
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0004C961 File Offset: 0x0004AD61
	public EnchantmentDetails(EnchantmentType type)
	{
		this.type = type;
		this.logoSpriteName = "EP_" + type.ToString() + "_Logo";
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0004C9A0 File Offset: 0x0004ADA0
	public static EnchantmentDetails NewEquipPropsEnchantmentDetails(EnchantmentType type, bool isEnabled, float additionValue, float triggerRate, float triggerInterval, float validTimeAfterTrigger)
	{
		return new EnchantmentDetails(type, isEnabled, additionValue, triggerRate, triggerInterval, validTimeAfterTrigger)
		{
			originType = EnchantmentOriginType.EquipAddition
		};
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0004C9C4 File Offset: 0x0004ADC4
	public static EnchantmentDetails NewScenePropsEnchantmentDetails(EnchantmentType type, bool isEnabled, float additionValue, float validTimeRest)
	{
		return new EnchantmentDetails(type, isEnabled, additionValue, validTimeRest)
		{
			originType = EnchantmentOriginType.ScenePropsAddition
		};
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0004C9E4 File Offset: 0x0004ADE4
	public static EnchantmentDetails NewPotionEnchantmentDetails(EnchantmentType type, bool isEnabled, float additionValue, float triggerRate, float triggerInterval, float validTimeAfterTrigger, float validTimeRest)
	{
		return new EnchantmentDetails(type, isEnabled, additionValue, triggerRate, triggerInterval, validTimeAfterTrigger, validTimeRest)
		{
			originType = EnchantmentOriginType.PotionAddition
		};
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0004CA0C File Offset: 0x0004AE0C
	public static EnchantmentDetails NewEmptyEnchantmentDetails(EnchantmentType type)
	{
		return new EnchantmentDetails(type);
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0004CA21 File Offset: 0x0004AE21
	public bool isTrigger()
	{
		return this.isEnabled && UnityEngine.Random.Range(0f, 1f) <= this.triggerRate;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0004CA4D File Offset: 0x0004AE4D
	public void Reset()
	{
		this.isEnabled = false;
		this.additionValue = 0f;
		this.triggerRate = 0f;
		this.triggerInterval = 0f;
		this.validTimeAfterTrigger = 0f;
		this.validTimeRest = 0f;
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0004CA8D File Offset: 0x0004AE8D
	public void Update(EnchantmentType type, bool isEnabled, float additionValue, float triggerRate, float triggerInterval, float validTimeAfterTrigger)
	{
		this.type = type;
		this.isEnabled = isEnabled;
		this.additionValue = additionValue;
		this.triggerRate = triggerRate;
		this.triggerInterval = triggerInterval;
		this.validTimeAfterTrigger = validTimeAfterTrigger;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0004CABC File Offset: 0x0004AEBC
	public void Update(EnchantmentType type, bool isEnabled, float additionValue, float validTimeRest)
	{
		this.type = type;
		this.isEnabled = isEnabled;
		this.additionValue = additionValue;
		this.validTimeRest = validTimeRest;
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0004CADC File Offset: 0x0004AEDC
	public new string ToString()
	{
		return string.Format("Type:{0}, isEnabled:{1}, additionValue:{2}, triggerRate:{3}, triggerInterval:{4}, validTimeAfterTrigger:{5}, validTimeRest:{6}", new object[]
		{
			this.type.ToString(),
			this.isEnabled.ToString(),
			this.additionValue.ToString(),
			this.triggerRate.ToString(),
			this.triggerInterval.ToString(),
			this.validTimeAfterTrigger.ToString(),
			this.validTimeRest.ToString()
		});
	}

	// Token: 0x040009C5 RID: 2501
	public EnchantmentType type;

	// Token: 0x040009C6 RID: 2502
	public string description;

	// Token: 0x040009C7 RID: 2503
	public string logoSpriteName;

	// Token: 0x040009C8 RID: 2504
	public bool isEnabled;

	// Token: 0x040009C9 RID: 2505
	public float additionValue;

	// Token: 0x040009CA RID: 2506
	public float triggerRate;

	// Token: 0x040009CB RID: 2507
	public float triggerInterval;

	// Token: 0x040009CC RID: 2508
	public float validTimeAfterTrigger;

	// Token: 0x040009CD RID: 2509
	public float validTimeRest;

	// Token: 0x040009CE RID: 2510
	public EnchantmentOriginType originType;

	// Token: 0x040009CF RID: 2511
	public SceneEnchantmentProps sceneBasicPropsType = SceneEnchantmentProps.Nil;
}
