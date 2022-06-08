using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CapeEditor
{
	// Token: 0x02000340 RID: 832
	public class CapeIOController
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x000D4AB7 File Offset: 0x000D2EB7
		public static string GetCustomCapeFolderPath()
		{
			return Application.persistentDataPath + "/CapeEditor/CapeTextures/";
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000D4AC8 File Offset: 0x000D2EC8
		public static string GetCustomCapeTemplateFolderPath()
		{
			return Application.persistentDataPath + "/assets/CapeEditor/TemplateCapes/";
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000D4AD9 File Offset: 0x000D2ED9
		public static string GetSystemCapeFolderPath()
		{
			return Application.persistentDataPath + "/assets/CapeEditor/SystemCapes/";
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000D4AEC File Offset: 0x000D2EEC
		public static bool CreateCape(string srcCapeTexName, string desCapeTexName)
		{
			bool result;
			try
			{
				string customCapeTemplateFolderPath = CapeIOController.GetCustomCapeTemplateFolderPath();
				string customCapeFolderPath = CapeIOController.GetCustomCapeFolderPath();
				FileStream fileStream = new FileStream(customCapeTemplateFolderPath + srcCapeTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customCapeFolderPath))
				{
					Directory.CreateDirectory(customCapeFolderPath);
				}
				string fileName = Path.GetFileName(customCapeTemplateFolderPath + srcCapeTexName);
				string path = customCapeFolderPath + fileName;
				FileStream fileStream2 = new FileStream(path, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty cape exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000D4BB4 File Offset: 0x000D2FB4
		public static bool CreateCape(string srcCapeTexName)
		{
			bool result;
			try
			{
				string text = "CustomCape_1.png";
				string customCapeTemplateFolderPath = CapeIOController.GetCustomCapeTemplateFolderPath();
				string customCapeFolderPath = CapeIOController.GetCustomCapeFolderPath();
				FileStream fileStream = new FileStream(customCapeTemplateFolderPath + srcCapeTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customCapeFolderPath))
				{
					Directory.CreateDirectory(customCapeFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customCapeFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomCape_" + num.ToString() + ".png";
				}
				string path2 = customCapeFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty cape exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000D4CF0 File Offset: 0x000D30F0
		public static string CreateCape()
		{
			string result;
			try
			{
				string str = "EmptyCape.png";
				string text = "CustomCape_1.png";
				string customCapeTemplateFolderPath = CapeIOController.GetCustomCapeTemplateFolderPath();
				string customCapeFolderPath = CapeIOController.GetCustomCapeFolderPath();
				FileStream fileStream = new FileStream(customCapeTemplateFolderPath + str, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customCapeFolderPath))
				{
					Directory.CreateDirectory(customCapeFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customCapeFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomCape_" + num.ToString() + ".png";
				}
				string path2 = customCapeFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = text;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty cape exception: " + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000D4E44 File Offset: 0x000D3244
		public static bool SaveCape(string SavedCapeTexName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customCapeFolderPath = CapeIOController.GetCustomCapeFolderPath();
				if (!Directory.Exists(customCapeFolderPath))
				{
					Directory.CreateDirectory(customCapeFolderPath);
				}
				string path = customCapeFolderPath + SavedCapeTexName;
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
				fileStream.Write(fullFile, 0, fullFile.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save cape exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000D4EC4 File Offset: 0x000D32C4
		public static bool CreateTempCape(string SavedCapeTexName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customCapeFolderPath = CapeIOController.GetCustomCapeFolderPath();
				if (!Directory.Exists(customCapeFolderPath))
				{
					Directory.CreateDirectory(customCapeFolderPath);
				}
				string path = customCapeFolderPath + SavedCapeTexName;
				FileStream fileStream = new FileStream(path, FileMode.Create);
				Debug.Log("CreateTempCape-" + fileStream.Length);
				fileStream.Write(fullFile, 0, fullFile.Length);
				Debug.Log("CreateTempCape-" + fullFile.Length);
				Debug.Log("CreateTempCape-" + fileStream.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save cape exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000D4F8C File Offset: 0x000D338C
		public static byte[] ReadCapeTextureData(string capeTexName, CapeBaseType cType)
		{
			byte[] result;
			try
			{
				if (!capeTexName.Contains(".png"))
				{
					capeTexName += ".png";
				}
				string path = string.Empty;
				if (cType == CapeBaseType.Custom)
				{
					path = CapeIOController.GetCustomCapeFolderPath() + capeTexName;
				}
				else if (cType == CapeBaseType.System)
				{
					path = CapeIOController.GetSystemCapeFolderPath() + capeTexName;
				}
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				result = array;
			}
			catch (Exception ex)
			{
				Debug.Log("Load cape data exception: " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000D504C File Offset: 0x000D344C
		public static List<string> GetCapeNameList(CapeBaseType cType)
		{
			List<string> result;
			try
			{
				string path = string.Empty;
				if (cType == CapeBaseType.Custom)
				{
					path = CapeIOController.GetCustomCapeFolderPath();
				}
				else if (cType == CapeBaseType.System)
				{
					path = CapeIOController.GetSystemCapeFolderPath();
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
								if (cType == CapeBaseType.Custom)
								{
									if (!text.Contains("Custom") || text.Split(new char[]
									{
										'_'
									}).Length != 3)
									{
										goto IL_107;
									}
								}
								else if (cType == CapeBaseType.Custom && text.Split(new char[]
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
				Debug.Log("Get custom cape list exception: " + ex.Message);
				result = null;
			}
			return result;
		}
	}
}
