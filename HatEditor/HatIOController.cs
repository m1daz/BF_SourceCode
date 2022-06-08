using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HatEditor
{
	// Token: 0x02000348 RID: 840
	public class HatIOController
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x000D5B8B File Offset: 0x000D3F8B
		public static string GetCustomHatFolderPath()
		{
			return Application.persistentDataPath + "/HatEditor/HatTextures/";
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000D5B9C File Offset: 0x000D3F9C
		public static string GetCustomHatTemplateFolderPath()
		{
			return Application.persistentDataPath + "/assets/HatEditor/TemplateHats/";
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000D5BAD File Offset: 0x000D3FAD
		public static string GetSystemHatFolderPath()
		{
			return Application.persistentDataPath + "/assets/HatEditor/SystemHats/";
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000D5BC0 File Offset: 0x000D3FC0
		public static bool CreateHat(string srcHatTexName, string desHatTexName)
		{
			bool result;
			try
			{
				string customHatTemplateFolderPath = HatIOController.GetCustomHatTemplateFolderPath();
				string customHatFolderPath = HatIOController.GetCustomHatFolderPath();
				FileStream fileStream = new FileStream(customHatTemplateFolderPath + srcHatTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customHatFolderPath))
				{
					Directory.CreateDirectory(customHatFolderPath);
				}
				string fileName = Path.GetFileName(customHatTemplateFolderPath + srcHatTexName);
				string path = customHatFolderPath + fileName;
				FileStream fileStream2 = new FileStream(path, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty hat exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000D5C88 File Offset: 0x000D4088
		public static bool CreateHat(string srcHatTexName)
		{
			bool result;
			try
			{
				string text = "CustomHat_1.png";
				string customHatTemplateFolderPath = HatIOController.GetCustomHatTemplateFolderPath();
				string customHatFolderPath = HatIOController.GetCustomHatFolderPath();
				FileStream fileStream = new FileStream(customHatTemplateFolderPath + srcHatTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customHatFolderPath))
				{
					Directory.CreateDirectory(customHatFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customHatFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomHat_" + num.ToString() + ".png";
				}
				string path2 = customHatFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty hat exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000D5DC4 File Offset: 0x000D41C4
		public static string CreateHat()
		{
			string result;
			try
			{
				string str = "EmptyHat.png";
				string text = "CustomHat_1.png";
				string customHatTemplateFolderPath = HatIOController.GetCustomHatTemplateFolderPath();
				string customHatFolderPath = HatIOController.GetCustomHatFolderPath();
				FileStream fileStream = new FileStream(customHatTemplateFolderPath + str, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customHatFolderPath))
				{
					Directory.CreateDirectory(customHatFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customHatFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomHat_" + num.ToString() + ".png";
				}
				string path2 = customHatFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = text;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty hat exception: " + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000D5F18 File Offset: 0x000D4318
		public static bool SaveHat(string SavedHatTexName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customHatFolderPath = HatIOController.GetCustomHatFolderPath();
				if (!Directory.Exists(customHatFolderPath))
				{
					Directory.CreateDirectory(customHatFolderPath);
				}
				string path = customHatFolderPath + SavedHatTexName;
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
				fileStream.Write(fullFile, 0, fullFile.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save hat exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000D5F98 File Offset: 0x000D4398
		public static bool CreateTempHat(string SavedHatTexName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customHatFolderPath = HatIOController.GetCustomHatFolderPath();
				if (!Directory.Exists(customHatFolderPath))
				{
					Directory.CreateDirectory(customHatFolderPath);
				}
				string path = customHatFolderPath + SavedHatTexName;
				FileStream fileStream = new FileStream(path, FileMode.Create);
				Debug.Log("CreateTempHat-" + fileStream.Length);
				fileStream.Write(fullFile, 0, fullFile.Length);
				Debug.Log("CreateTempHat-" + fullFile.Length);
				Debug.Log("CreateTempHat-" + fileStream.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save hat exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000D6060 File Offset: 0x000D4460
		public static byte[] ReadHatTextureData(string hatTexName, HatBaseType hType)
		{
			byte[] result;
			try
			{
				if (!hatTexName.Contains(".png"))
				{
					hatTexName += ".png";
				}
				string path = string.Empty;
				if (hType == HatBaseType.Custom)
				{
					path = HatIOController.GetCustomHatFolderPath() + hatTexName;
				}
				else if (hType == HatBaseType.System)
				{
					path = HatIOController.GetSystemHatFolderPath() + hatTexName;
				}
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				result = array;
			}
			catch (Exception ex)
			{
				Debug.Log("Load hat data exception: " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000D6120 File Offset: 0x000D4520
		public static List<string> GetHatNameList(HatBaseType hType)
		{
			List<string> result;
			try
			{
				string path = string.Empty;
				if (hType == HatBaseType.Custom)
				{
					path = HatIOController.GetCustomHatFolderPath();
				}
				else if (hType == HatBaseType.System)
				{
					path = HatIOController.GetSystemHatFolderPath();
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
						if (text.Contains(".png"))
						{
							text = text.Remove(text.Length - 4);
							if (!text.Contains(".png"))
							{
								if (hType == HatBaseType.Custom)
								{
									if (!text.Contains("Custom") || text.Split(new char[]
									{
										'_'
									}).Length != 3)
									{
										goto IL_107;
									}
								}
								else if (hType == HatBaseType.Custom && text.Split(new char[]
								{
									'_'
								}).Length != 2)
								{
									goto IL_107;
								}
								list.Add(text);
							}
						}
						IL_107:;
					}
					result = list;
				}
			}
			catch (Exception ex)
			{
				Debug.Log("Get custom hat list exception: " + ex.Message);
				result = null;
			}
			return result;
		}
	}
}
