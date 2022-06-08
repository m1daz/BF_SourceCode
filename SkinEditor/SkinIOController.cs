using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x0200035B RID: 859
	public class SkinIOController
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x000D93C4 File Offset: 0x000D77C4
		public static string GetCustomSkinFolderPath()
		{
			return Application.persistentDataPath + "/SkinEditor/SkinTextures/";
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000D93D5 File Offset: 0x000D77D5
		public static string GetCustomSkinTemplateFolderPath()
		{
			return Application.persistentDataPath + "/assets/SkinEditor/TemplateSkins/";
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x000D93E6 File Offset: 0x000D77E6
		public static string GetSystemSkinFolderPath()
		{
			return Application.persistentDataPath + "/assets/SkinEditor/SystemSkins/";
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000D93F7 File Offset: 0x000D77F7
		public static string GetSharedSkinFolderPath()
		{
			return Application.persistentDataPath + "/SkinEditor/SharedSkins/";
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000D9408 File Offset: 0x000D7808
		public static string GetCachedSkinFolderPath()
		{
			return Application.persistentDataPath + "/SkinEditor/CachedSkins/";
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000D941C File Offset: 0x000D781C
		public static bool CreateSkin(string srcSkinTexName, string desSkinTexName)
		{
			bool result;
			try
			{
				string customSkinTemplateFolderPath = SkinIOController.GetCustomSkinTemplateFolderPath();
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				FileStream fileStream = new FileStream(customSkinTemplateFolderPath + srcSkinTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customSkinFolderPath))
				{
					Directory.CreateDirectory(customSkinFolderPath);
				}
				string fileName = Path.GetFileName(customSkinTemplateFolderPath + srcSkinTexName);
				string path = customSkinFolderPath + fileName;
				FileStream fileStream2 = new FileStream(path, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000D94E4 File Offset: 0x000D78E4
		public static bool CreateSkin(string srcSkinTexName)
		{
			bool result;
			try
			{
				string text = "CustomSkin_1.png";
				string customSkinTemplateFolderPath = SkinIOController.GetCustomSkinTemplateFolderPath();
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				FileStream fileStream = new FileStream(customSkinTemplateFolderPath + srcSkinTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customSkinFolderPath))
				{
					Directory.CreateDirectory(customSkinFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customSkinFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomSkin_" + num.ToString() + ".png";
				}
				string path2 = customSkinFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000D9620 File Offset: 0x000D7A20
		public static byte[] EncryptData(byte[] data, string skinTexFileName)
		{
			byte[] array = new byte[16];
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)UnityEngine.Random.Range(1, 10);
				string str = text;
				int num = (int)array[i];
				text = str + num.ToString();
			}
			byte[] array2 = new byte[data.Length + array.Length];
			for (int j = 0; j < data.Length; j++)
			{
				array2[j] = data[j];
			}
			for (int k = data.Length; k < data.Length + array.Length; k++)
			{
				array2[k] = array[k - data.Length];
			}
			GOGPlayerPrefabs.SetString("SIC_" + skinTexFileName.Replace('.', '_'), text);
			return array2;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000D96EC File Offset: 0x000D7AEC
		public static byte[] DecryptData(byte[] data, string skinTexFileName)
		{
			byte[] array = new byte[16];
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = data[i + data.Length - 16];
				string str = text;
				int num = (int)array[i];
				text = str + num.ToString();
			}
			if (text != GOGPlayerPrefabs.GetString("SIC_" + skinTexFileName.Replace('.', '_'), "NoKey"))
			{
				return new byte[data.Length - array.Length];
			}
			byte[] array2 = new byte[data.Length - array.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = data[j];
			}
			return array2;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000D97A8 File Offset: 0x000D7BA8
		public static void RemoveEncryptDataRecord(string skinTexFileName)
		{
			GOGPlayerPrefabs.SetString("SIC_" + skinTexFileName.Replace('.', '_'), "NoKey");
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000D97C8 File Offset: 0x000D7BC8
		public static string CreateSkin()
		{
			string result;
			try
			{
				string str = "EmptySkin.png";
				string text = "CustomSkin_1.png";
				string customSkinTemplateFolderPath = SkinIOController.GetCustomSkinTemplateFolderPath();
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				FileStream fileStream = new FileStream(customSkinTemplateFolderPath + str, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customSkinFolderPath))
				{
					Directory.CreateDirectory(customSkinFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customSkinFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomSkin_" + num.ToString() + ".png";
				}
				array = SkinIOController.EncryptData(array, text);
				string path2 = customSkinFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = text;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty skin exception: " + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000D9928 File Offset: 0x000D7D28
		public static bool SaveSkin(string SavedSkinTexFileName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				if (!Directory.Exists(customSkinFolderPath))
				{
					Directory.CreateDirectory(customSkinFolderPath);
				}
				fullFile = SkinIOController.EncryptData(fullFile, SavedSkinTexFileName);
				string path = customSkinFolderPath + SavedSkinTexFileName;
				FileStream fileStream;
				if (File.Exists(path))
				{
					fileStream = new FileStream(path, FileMode.Truncate);
				}
				else
				{
					fileStream = new FileStream(path, FileMode.OpenOrCreate);
				}
				fileStream.Write(fullFile, 0, fullFile.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000D99C8 File Offset: 0x000D7DC8
		public static bool SaveSharedSkin(string SavedSkinTexFileName, byte[] fullFile)
		{
			bool result;
			try
			{
				string sharedSkinFolderPath = SkinIOController.GetSharedSkinFolderPath();
				if (!Directory.Exists(sharedSkinFolderPath))
				{
					Directory.CreateDirectory(sharedSkinFolderPath);
				}
				string path = sharedSkinFolderPath + SavedSkinTexFileName;
				FileStream fileStream;
				if (File.Exists(path))
				{
					fileStream = new FileStream(path, FileMode.Truncate);
				}
				else
				{
					fileStream = new FileStream(path, FileMode.OpenOrCreate);
				}
				fileStream.Write(fullFile, 0, fullFile.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000D9A60 File Offset: 0x000D7E60
		public static bool SaveCachedSkin(string SavedSkinTexFileName, byte[] fullFile)
		{
			bool result;
			try
			{
				string cachedSkinFolderPath = SkinIOController.GetCachedSkinFolderPath();
				if (!Directory.Exists(cachedSkinFolderPath))
				{
					Directory.CreateDirectory(cachedSkinFolderPath);
				}
				string path = cachedSkinFolderPath + SavedSkinTexFileName;
				FileStream fileStream;
				if (File.Exists(path))
				{
					fileStream = new FileStream(path, FileMode.Truncate);
				}
				else
				{
					fileStream = new FileStream(path, FileMode.OpenOrCreate);
				}
				fileStream.Write(fullFile, 0, fullFile.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000D9AF8 File Offset: 0x000D7EF8
		public static bool DeleteSkin(string SkinTexFileName)
		{
			bool result;
			try
			{
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				if (!Directory.Exists(customSkinFolderPath))
				{
					result = false;
				}
				else
				{
					string path = customSkinFolderPath + SkinTexFileName;
					File.Delete(path);
					result = true;
				}
			}
			catch (Exception ex)
			{
				Debug.Log("Delete skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000D9B60 File Offset: 0x000D7F60
		public static bool DeleteAllCustomSkin()
		{
			List<string> skinNameList = SkinIOController.GetSkinNameList(SkinBaseType.Custom);
			bool result;
			try
			{
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				if (!Directory.Exists(customSkinFolderPath))
				{
					result = false;
				}
				else
				{
					foreach (string str in skinNameList)
					{
						string path = customSkinFolderPath + str + ".png";
						File.Delete(path);
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Debug.Log("Delete skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000D9C18 File Offset: 0x000D8018
		public static bool DeleteAllSharedSkin()
		{
			List<string> skinNameList = SkinIOController.GetSkinNameList(SkinBaseType.Shared);
			bool result;
			try
			{
				string sharedSkinFolderPath = SkinIOController.GetSharedSkinFolderPath();
				if (!Directory.Exists(sharedSkinFolderPath))
				{
					result = false;
				}
				else
				{
					foreach (string str in skinNameList)
					{
						string path = sharedSkinFolderPath + str + ".png";
						File.Delete(path);
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Debug.Log("Delete skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000D9CD0 File Offset: 0x000D80D0
		public static bool DeleteAllCachedSkin()
		{
			List<string> skinNameList = SkinIOController.GetSkinNameList(SkinBaseType.Cached);
			bool result;
			try
			{
				string sharedSkinFolderPath = SkinIOController.GetSharedSkinFolderPath();
				if (!Directory.Exists(sharedSkinFolderPath))
				{
					result = false;
				}
				else
				{
					foreach (string str in skinNameList)
					{
						string path = sharedSkinFolderPath + str + ".png";
						File.Delete(path);
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				Debug.Log("Delete skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000D9D88 File Offset: 0x000D8188
		public static bool CreateTempSkin(string SavedSkinTexFileName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customSkinFolderPath = SkinIOController.GetCustomSkinFolderPath();
				if (!Directory.Exists(customSkinFolderPath))
				{
					Directory.CreateDirectory(customSkinFolderPath);
				}
				string path = customSkinFolderPath + SavedSkinTexFileName;
				FileStream fileStream = new FileStream(path, FileMode.Create);
				Debug.Log("CreateTempSkin-" + fileStream.Length);
				fileStream.Write(fullFile, 0, fullFile.Length);
				Debug.Log("CreateTempSkin-" + fullFile.Length);
				Debug.Log("CreateTempSkin-" + fileStream.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save skin exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000D9E50 File Offset: 0x000D8250
		public static byte[] ReadSkinTextureData(string skinTexName, SkinBaseType sType)
		{
			byte[] result;
			try
			{
				if (!skinTexName.Contains(".png"))
				{
					skinTexName += ".png";
				}
				string path = string.Empty;
				if (sType == SkinBaseType.Custom)
				{
					path = SkinIOController.GetCustomSkinFolderPath() + skinTexName;
				}
				else if (sType == SkinBaseType.System)
				{
					path = SkinIOController.GetSystemSkinFolderPath() + skinTexName;
				}
				else if (sType == SkinBaseType.Shared)
				{
					path = SkinIOController.GetSharedSkinFolderPath() + skinTexName;
				}
				else if (sType == SkinBaseType.Cached)
				{
					path = SkinIOController.GetCachedSkinFolderPath() + skinTexName;
				}
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (sType == SkinBaseType.Custom)
				{
					array = SkinIOController.DecryptData(array, skinTexName);
				}
				result = array;
			}
			catch (Exception ex)
			{
				Debug.Log("Load skin data exception: " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000D9F50 File Offset: 0x000D8350
		public static List<string> GetSkinNameList(SkinBaseType sType)
		{
			List<string> result;
			try
			{
				string path = string.Empty;
				if (sType == SkinBaseType.Custom)
				{
					path = SkinIOController.GetCustomSkinFolderPath();
				}
				else if (sType == SkinBaseType.System)
				{
					path = SkinIOController.GetSystemSkinFolderPath();
				}
				else if (sType == SkinBaseType.Shared)
				{
					path = SkinIOController.GetSharedSkinFolderPath();
				}
				if (!Directory.Exists(path))
				{
					result = new List<string>();
				}
				else
				{
					List<string> list = new List<string>();
					foreach (string path2 in Directory.GetFiles(path))
					{
						string text = Path.GetFileName(path2);
						if (sType != SkinBaseType.Custom || text.Contains("Custom"))
						{
							if (text.Contains(".png"))
							{
								text = text.Remove(text.Length - 4);
								if (!text.Contains(".png"))
								{
									list.Add(text);
								}
							}
						}
					}
					result = list;
				}
			}
			catch (Exception ex)
			{
				Debug.Log("Get custom skin list exception: " + ex.Message);
				result = null;
			}
			return result;
		}
	}
}
