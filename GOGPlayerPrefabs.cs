using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class GOGPlayerPrefabs
{
	// Token: 0x06000C65 RID: 3173 RVA: 0x0005D70C File Offset: 0x0005BB0C
	public static void SetSecretKey(string key)
	{
		ObscuredPrefs.SetNewCryptoKey(key);
		ObscuredPrefs.lockToDevice = ObscuredPrefs.DeviceLockLevel.Strict;
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0005D71A File Offset: 0x0005BB1A
	public static void Save()
	{
		ObscuredPrefs.Save();
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0005D721 File Offset: 0x0005BB21
	public static void DeleteAll()
	{
		ObscuredPrefs.DeleteAll();
		PlayerPrefs.DeleteAll();
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0005D730 File Offset: 0x0005BB30
	public static int GetInt(string key, int defaultValue)
	{
		int @int = ObscuredPrefs.GetInt(key, defaultValue);
		int num = @int;
		if (ACTUserDataManager.mInstance == null)
		{
			return @int;
		}
		if (@int != defaultValue)
		{
			key = GOGPlayerPrefabs.DATA_TAG_INT + "_" + key;
			if (!ACTUserDataManager.mInstance.dataCache.ContainsKey(key))
			{
				ACTUserDataManager.mInstance.dataCache.Add(key, num.ToString());
			}
			else
			{
				ACTUserDataManager.mInstance.dataCache[key] = num.ToString();
			}
		}
		return @int;
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0005D7C8 File Offset: 0x0005BBC8
	public static void SetInt(string key, int newValue)
	{
		ObscuredPrefs.SetInt(key, newValue);
		if (ACTUserDataManager.mInstance == null)
		{
			return;
		}
		key = GOGPlayerPrefabs.DATA_TAG_INT + "_" + key;
		if (!ACTUserDataManager.mInstance.dataCache.ContainsKey(key))
		{
			ACTUserDataManager.mInstance.dataCache.Add(key, newValue.ToString());
		}
		else
		{
			ACTUserDataManager.mInstance.dataCache[key] = newValue.ToString();
		}
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0005D854 File Offset: 0x0005BC54
	public static string GetString(string key, string defaultValue)
	{
		string @string = ObscuredPrefs.GetString(key, defaultValue);
		string text = @string;
		if (ACTUserDataManager.mInstance == null || GOGPlayerPrefabs.NeedIgnoreWhenUpload(key))
		{
			return @string;
		}
		if (@string != defaultValue)
		{
			key = GOGPlayerPrefabs.DATA_TAG_STRING + "_" + key;
			if (text == string.Empty)
			{
				text = GOGPlayerPrefabs.DATA_TAG_STRING_NULL;
			}
			if (!ACTUserDataManager.mInstance.dataCache.ContainsKey(key))
			{
				ACTUserDataManager.mInstance.dataCache.Add(key, text.ToString());
			}
			else
			{
				ACTUserDataManager.mInstance.dataCache[key] = text.ToString();
			}
		}
		return @string;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0005D904 File Offset: 0x0005BD04
	public static void SetString(string key, string newValue)
	{
		ObscuredPrefs.SetString(key, newValue);
		if (ACTUserDataManager.mInstance == null || GOGPlayerPrefabs.NeedIgnoreWhenUpload(key))
		{
			return;
		}
		key = GOGPlayerPrefabs.DATA_TAG_STRING + "_" + key;
		if (newValue == string.Empty)
		{
			newValue = GOGPlayerPrefabs.DATA_TAG_STRING_NULL;
		}
		if (!ACTUserDataManager.mInstance.dataCache.ContainsKey(key))
		{
			ACTUserDataManager.mInstance.dataCache.Add(key, newValue.ToString());
		}
		else
		{
			ACTUserDataManager.mInstance.dataCache[key] = newValue.ToString();
		}
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0005D9A3 File Offset: 0x0005BDA3
	public static bool NeedIgnoreWhenUpload(string key)
	{
		return key.Equals("UIDefaultUsername") || key.Equals("UIDefaultPwd") || key.Equals("UIDefaultRoleName");
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0005D9DC File Offset: 0x0005BDDC
	public static float GetFloat(string key, float defaultValue)
	{
		float @float = ObscuredPrefs.GetFloat(key, defaultValue);
		float num = @float;
		if (ACTUserDataManager.mInstance == null)
		{
			return @float;
		}
		if (@float != defaultValue)
		{
			key = GOGPlayerPrefabs.DATA_TAG_FLOAT + "_" + key;
			if (!ACTUserDataManager.mInstance.dataCache.ContainsKey(key))
			{
				ACTUserDataManager.mInstance.dataCache.Add(key, num.ToString());
			}
			else
			{
				ACTUserDataManager.mInstance.dataCache[key] = num.ToString();
			}
		}
		return @float;
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0005DA74 File Offset: 0x0005BE74
	public static void SetFloat(string key, float newValue)
	{
		ObscuredPrefs.SetFloat(key, newValue);
		if (ACTUserDataManager.mInstance == null)
		{
			return;
		}
		key = GOGPlayerPrefabs.DATA_TAG_FLOAT + "_" + key;
		if (!ACTUserDataManager.mInstance.dataCache.ContainsKey(key))
		{
			ACTUserDataManager.mInstance.dataCache.Add(key, newValue.ToString());
		}
		else
		{
			ACTUserDataManager.mInstance.dataCache[key] = newValue.ToString();
		}
	}

	// Token: 0x04000D0F RID: 3343
	public static readonly string DATA_TAG_INT = "#1#";

	// Token: 0x04000D10 RID: 3344
	public static readonly string DATA_TAG_STRING = "#2#";

	// Token: 0x04000D11 RID: 3345
	public static readonly string DATA_TAG_FLOAT = "#3#";

	// Token: 0x04000D12 RID: 3346
	public static readonly string DATA_TAG_STRING_NULL = "#NULL#";
}
