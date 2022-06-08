using System;
using System.Text;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200032B RID: 811
	public static class ObscuredPrefs
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000D1066 File Offset: 0x000CF466
		private static string DeviceHash
		{
			get
			{
				if (string.IsNullOrEmpty(ObscuredPrefs.deviceHash))
				{
					ObscuredPrefs.deviceHash = ObscuredPrefs.GetDeviceID();
				}
				return ObscuredPrefs.deviceHash;
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000D1086 File Offset: 0x000CF486
		public static void ForceLockToDeviceInit()
		{
			if (string.IsNullOrEmpty(ObscuredPrefs.deviceHash))
			{
				ObscuredPrefs.deviceHash = ObscuredPrefs.GetDeviceID();
			}
			else
			{
				Debug.LogWarning("[ACT] ObscuredPrefs.ForceLockToDeviceInit() is called, but LockToDevice feature is already inited!");
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000D10B0 File Offset: 0x000CF4B0
		public static void SetNewCryptoKey(string newKey)
		{
			ObscuredPrefs.encryptionKey = newKey;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000D10B8 File Offset: 0x000CF4B8
		public static void SetInt(string key, int value)
		{
			ObscuredPrefs.SetStringValue(key, value.ToString());
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000D10CD File Offset: 0x000CF4CD
		public static int GetInt(string key)
		{
			return ObscuredPrefs.GetInt(key, 0);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000D10D8 File Offset: 0x000CF4D8
		public static int GetInt(string key, int defaultValue)
		{
			string key2 = ObscuredPrefs.EncryptKey(key);
			if (!PlayerPrefs.HasKey(key2) && PlayerPrefs.HasKey(key))
			{
				int @int = PlayerPrefs.GetInt(key, defaultValue);
				if (!ObscuredPrefs.preservePlayerPrefs)
				{
					ObscuredPrefs.SetInt(key, @int);
					PlayerPrefs.DeleteKey(key);
				}
				return @int;
			}
			string data = ObscuredPrefs.GetData(key2, defaultValue.ToString());
			int result;
			int.TryParse(data, out result);
			return result;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000D1141 File Offset: 0x000CF541
		public static void SetString(string key, string value)
		{
			ObscuredPrefs.SetStringValue(key, value);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000D114A File Offset: 0x000CF54A
		public static string GetString(string key)
		{
			return ObscuredPrefs.GetString(key, string.Empty);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000D1158 File Offset: 0x000CF558
		public static string GetString(string key, string defaultValue)
		{
			string key2 = ObscuredPrefs.EncryptKey(key);
			if (!PlayerPrefs.HasKey(key2) && PlayerPrefs.HasKey(key))
			{
				string @string = PlayerPrefs.GetString(key, defaultValue);
				if (!ObscuredPrefs.preservePlayerPrefs)
				{
					ObscuredPrefs.SetString(key, @string);
					PlayerPrefs.DeleteKey(key);
				}
				return @string;
			}
			return ObscuredPrefs.GetData(key2, defaultValue);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000D11AC File Offset: 0x000CF5AC
		public static void SetFloat(string key, float value)
		{
			ObscuredPrefs.SetStringValue(key, value.ToString());
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000D11C1 File Offset: 0x000CF5C1
		public static float GetFloat(string key)
		{
			return ObscuredPrefs.GetFloat(key, 0f);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000D11D0 File Offset: 0x000CF5D0
		public static float GetFloat(string key, float defaultValue)
		{
			string key2 = ObscuredPrefs.EncryptKey(key);
			if (!PlayerPrefs.HasKey(key2) && PlayerPrefs.HasKey(key))
			{
				float @float = PlayerPrefs.GetFloat(key, defaultValue);
				if (!ObscuredPrefs.preservePlayerPrefs)
				{
					ObscuredPrefs.SetFloat(key, @float);
					PlayerPrefs.DeleteKey(key);
				}
				return @float;
			}
			string data = ObscuredPrefs.GetData(key2, defaultValue.ToString());
			float result;
			float.TryParse(data, out result);
			return result;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000D1239 File Offset: 0x000CF639
		public static void SetDouble(string key, double value)
		{
			ObscuredPrefs.SetStringValue(key, value.ToString());
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000D124E File Offset: 0x000CF64E
		public static double GetDouble(string key)
		{
			return ObscuredPrefs.GetDouble(key, 0.0);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000D1260 File Offset: 0x000CF660
		public static double GetDouble(string key, double defaultValue)
		{
			string data = ObscuredPrefs.GetData(ObscuredPrefs.EncryptKey(key), defaultValue.ToString());
			double result;
			double.TryParse(data, out result);
			return result;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000D1290 File Offset: 0x000CF690
		public static void SetLong(string key, long value)
		{
			ObscuredPrefs.SetStringValue(key, value.ToString());
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000D12A5 File Offset: 0x000CF6A5
		public static long GetLong(string key)
		{
			return ObscuredPrefs.GetLong(key, 0L);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000D12B0 File Offset: 0x000CF6B0
		public static long GetLong(string key, long defaultValue)
		{
			string data = ObscuredPrefs.GetData(ObscuredPrefs.EncryptKey(key), defaultValue.ToString());
			long result;
			long.TryParse(data, out result);
			return result;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000D12E0 File Offset: 0x000CF6E0
		public static void SetBool(string key, bool value)
		{
			ObscuredPrefs.SetInt(key, (!value) ? 0 : 1);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000D12F5 File Offset: 0x000CF6F5
		public static bool GetBool(string key)
		{
			return ObscuredPrefs.GetBool(key, false);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000D1300 File Offset: 0x000CF700
		public static bool GetBool(string key, bool defaultValue)
		{
			int num = (!defaultValue) ? 0 : 1;
			string data = ObscuredPrefs.GetData(ObscuredPrefs.EncryptKey(key), num.ToString());
			int num2;
			int.TryParse(data, out num2);
			return num2 == 1;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000D1344 File Offset: 0x000CF744
		public static void SetVector3(string key, Vector3 value)
		{
			string value2 = string.Concat(new object[]
			{
				value.x,
				"|",
				value.y,
				"|",
				value.z
			});
			ObscuredPrefs.SetStringValue(key, value2);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000D13A1 File Offset: 0x000CF7A1
		public static Vector3 GetVector3(string key)
		{
			return ObscuredPrefs.GetVector3(key, Vector3.zero);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000D13B0 File Offset: 0x000CF7B0
		public static Vector3 GetVector3(string key, Vector3 defaultValue)
		{
			string data = ObscuredPrefs.GetData(ObscuredPrefs.EncryptKey(key), "{not_found}");
			Vector3 result;
			if (data == "{not_found}")
			{
				result = defaultValue;
			}
			else
			{
				string[] array = data.Split(new char[]
				{
					'|'
				});
				float x;
				float.TryParse(array[0], out x);
				float y;
				float.TryParse(array[1], out y);
				float z;
				float.TryParse(array[2], out z);
				result = new Vector3(x, y, z);
			}
			return result;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000D1425 File Offset: 0x000CF825
		public static void SetByteArray(string key, byte[] value)
		{
			ObscuredPrefs.SetStringValue(key, Encoding.UTF8.GetString(value, 0, value.Length));
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000D143C File Offset: 0x000CF83C
		public static byte[] GetByteArray(string key)
		{
			return ObscuredPrefs.GetByteArray(key, 0, 0);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000D1448 File Offset: 0x000CF848
		public static byte[] GetByteArray(string key, byte defaultValue, int defaultLength)
		{
			string data = ObscuredPrefs.GetData(ObscuredPrefs.EncryptKey(key), "{not_found}");
			byte[] array;
			if (data == "{not_found}")
			{
				array = new byte[defaultLength];
				for (int i = 0; i < defaultLength; i++)
				{
					array[i] = defaultValue;
				}
			}
			else
			{
				array = Encoding.UTF8.GetBytes(data);
			}
			return array;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000D14A5 File Offset: 0x000CF8A5
		public static bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key) || PlayerPrefs.HasKey(ObscuredPrefs.EncryptKey(key));
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000D14BF File Offset: 0x000CF8BF
		public static void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(ObscuredPrefs.EncryptKey(key));
			PlayerPrefs.DeleteKey(key);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000D14D2 File Offset: 0x000CF8D2
		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000D14D9 File Offset: 0x000CF8D9
		public static void Save()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000D14E0 File Offset: 0x000CF8E0
		private static void SetStringValue(string key, string value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptValue(value));
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000D14F4 File Offset: 0x000CF8F4
		private static string GetData(string key, string defaultValueRaw)
		{
			string text = PlayerPrefs.GetString(key, defaultValueRaw);
			if (text != defaultValueRaw)
			{
				text = ObscuredPrefs.DecryptValue(text);
				if (text == string.Empty)
				{
					text = defaultValueRaw;
				}
			}
			else
			{
				string text2 = ObscuredPrefs.DecryptKey(key);
				string key2 = ObscuredPrefs.EncryptKeyDeprecated(text2);
				text = PlayerPrefs.GetString(key2, defaultValueRaw);
				if (text != defaultValueRaw)
				{
					text = ObscuredPrefs.DecryptValueDeprecated(text);
					PlayerPrefs.DeleteKey(key2);
					ObscuredPrefs.SetStringValue(text2, text);
				}
				else if (PlayerPrefs.HasKey(text2))
				{
					Debug.LogWarning("[ACT] Are you trying to read data saved with regular PlayerPrefs using ObscuredPrefs (key = " + text2 + ")?");
				}
			}
			return text;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000D158F File Offset: 0x000CF98F
		private static string EncryptKey(string key)
		{
			key = ObscuredString.EncryptDecrypt(key, ObscuredPrefs.encryptionKey);
			key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
			return key;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000D15B4 File Offset: 0x000CF9B4
		private static string DecryptKey(string key)
		{
			byte[] array = Convert.FromBase64String(key);
			key = Encoding.UTF8.GetString(array, 0, array.Length);
			key = ObscuredString.EncryptDecrypt(key, ObscuredPrefs.encryptionKey);
			return key;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000D15E8 File Offset: 0x000CF9E8
		private static string EncryptValue(string value)
		{
			string text = ObscuredString.EncryptDecrypt(value, ObscuredPrefs.encryptionKey);
			text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
			if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					':',
					ObscuredPrefs.CalculateChecksum(text + ObscuredPrefs.DeviceHash),
					":",
					ObscuredPrefs.DeviceHash
				});
			}
			else
			{
				text = text + ':' + ObscuredPrefs.CalculateChecksum(text);
			}
			return text;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000D1678 File Offset: 0x000CFA78
		private static string DecryptValue(string value)
		{
			string[] array = value.Split(new char[]
			{
				':'
			});
			if (array.Length < 2)
			{
				ObscuredPrefs.SavesTampered();
				return string.Empty;
			}
			string text = array[0];
			string a = array[1];
			byte[] array2;
			try
			{
				array2 = Convert.FromBase64String(text);
			}
			catch
			{
				ObscuredPrefs.SavesTampered();
				return string.Empty;
			}
			string @string = Encoding.UTF8.GetString(array2, 0, array2.Length);
			string result = ObscuredString.EncryptDecrypt(@string, ObscuredPrefs.encryptionKey);
			if (array.Length == 3)
			{
				if (a != ObscuredPrefs.CalculateChecksum(text + ObscuredPrefs.DeviceHash))
				{
					ObscuredPrefs.SavesTampered();
				}
			}
			else if (array.Length == 2)
			{
				if (a != ObscuredPrefs.CalculateChecksum(text))
				{
					ObscuredPrefs.SavesTampered();
				}
			}
			else
			{
				ObscuredPrefs.SavesTampered();
			}
			if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None && !ObscuredPrefs.emergencyMode)
			{
				if (array.Length >= 3)
				{
					string a2 = array[2];
					if (a2 != ObscuredPrefs.DeviceHash)
					{
						if (!ObscuredPrefs.readForeignSaves)
						{
							result = string.Empty;
						}
						ObscuredPrefs.PossibleForeignSavesDetected();
					}
				}
				else if (ObscuredPrefs.lockToDevice == ObscuredPrefs.DeviceLockLevel.Strict)
				{
					if (!ObscuredPrefs.readForeignSaves)
					{
						result = string.Empty;
					}
					ObscuredPrefs.PossibleForeignSavesDetected();
				}
				else if (a != ObscuredPrefs.CalculateChecksum(text))
				{
					if (!ObscuredPrefs.readForeignSaves)
					{
						result = string.Empty;
					}
					ObscuredPrefs.PossibleForeignSavesDetected();
				}
			}
			return result;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000D17F8 File Offset: 0x000CFBF8
		private static string CalculateChecksum(string input)
		{
			int num = 0;
			byte[] bytes = Encoding.UTF8.GetBytes(input + ObscuredPrefs.encryptionKey);
			int num2 = bytes.Length;
			int num3 = ObscuredPrefs.encryptionKey.Length ^ 64;
			for (int i = 0; i < num2; i++)
			{
				byte b = bytes[i];
				num += (int)b + (int)b * (i + num3) % 3;
			}
			return num.ToString("X2");
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000D1866 File Offset: 0x000CFC66
		private static void SavesTampered()
		{
			if (ObscuredPrefs.onAlterationDetected != null && !ObscuredPrefs.savesAlterationReported)
			{
				ObscuredPrefs.savesAlterationReported = true;
				ObscuredPrefs.onAlterationDetected();
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000D188C File Offset: 0x000CFC8C
		private static void PossibleForeignSavesDetected()
		{
			if (ObscuredPrefs.onPossibleForeignSavesDetected != null && !ObscuredPrefs.foreignSavesReported)
			{
				ObscuredPrefs.foreignSavesReported = true;
				ObscuredPrefs.onPossibleForeignSavesDetected();
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000D18B4 File Offset: 0x000CFCB4
		private static string GetDeviceID()
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				text = SystemInfo.deviceUniqueIdentifier;
			}
			return ObscuredPrefs.CalculateChecksum(text);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000D18DE File Offset: 0x000CFCDE
		private static string EncryptKeyDeprecated(string key)
		{
			key = ObscuredString.EncryptDecrypt(key);
			if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
			{
				key = ObscuredString.EncryptDecrypt(key, ObscuredPrefs.GetDeviceIDDeprecated());
			}
			key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
			return key;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000D1914 File Offset: 0x000CFD14
		private static string DecryptValueDeprecated(string value)
		{
			byte[] array = Convert.FromBase64String(value);
			value = Encoding.UTF8.GetString(array, 0, array.Length);
			if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
			{
				value = ObscuredString.EncryptDecrypt(value, ObscuredPrefs.GetDeviceIDDeprecated());
			}
			value = ObscuredString.EncryptDecrypt(value, ObscuredPrefs.encryptionKey);
			return value;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000D195E File Offset: 0x000CFD5E
		private static string GetDeviceIDDeprecated()
		{
			return SystemInfo.deviceUniqueIdentifier;
		}

		// Token: 0x04001C3D RID: 7229
		private static string encryptionKey = "e806f6";

		// Token: 0x04001C3E RID: 7230
		private static bool savesAlterationReported;

		// Token: 0x04001C3F RID: 7231
		private static bool foreignSavesReported;

		// Token: 0x04001C40 RID: 7232
		private static string deviceHash;

		// Token: 0x04001C41 RID: 7233
		public static Action onAlterationDetected;

		// Token: 0x04001C42 RID: 7234
		public static bool preservePlayerPrefs;

		// Token: 0x04001C43 RID: 7235
		public static Action onPossibleForeignSavesDetected;

		// Token: 0x04001C44 RID: 7236
		public static ObscuredPrefs.DeviceLockLevel lockToDevice;

		// Token: 0x04001C45 RID: 7237
		public static bool readForeignSaves;

		// Token: 0x04001C46 RID: 7238
		public static bool emergencyMode;

		// Token: 0x0200032C RID: 812
		public enum DeviceLockLevel : byte
		{
			// Token: 0x04001C48 RID: 7240
			None,
			// Token: 0x04001C49 RID: 7241
			Soft,
			// Token: 0x04001C4A RID: 7242
			Strict
		}
	}
}
