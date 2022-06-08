using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BootEditor
{
	// Token: 0x02000338 RID: 824
	public class BootIOController
	{
		// Token: 0x06001A58 RID: 6744 RVA: 0x000D39E4 File Offset: 0x000D1DE4
		public static string GetCustomBootFolderPath()
		{
			return Application.persistentDataPath + "/BootEditor/BootTextures/";
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000D39F5 File Offset: 0x000D1DF5
		public static string GetCustomBootTemplateFolderPath()
		{
			return Application.persistentDataPath + "/assets/BootEditor/TemplateBoots/";
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000D3A06 File Offset: 0x000D1E06
		public static string GetSystemBootFolderPath()
		{
			return Application.persistentDataPath + "/assets/BootEditor/SystemBoots/";
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000D3A18 File Offset: 0x000D1E18
		public static bool CreateBoot(string srcBootTexName, string desBootTexName)
		{
			bool result;
			try
			{
				string customBootTemplateFolderPath = BootIOController.GetCustomBootTemplateFolderPath();
				string customBootFolderPath = BootIOController.GetCustomBootFolderPath();
				FileStream fileStream = new FileStream(customBootTemplateFolderPath + srcBootTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customBootFolderPath))
				{
					Directory.CreateDirectory(customBootFolderPath);
				}
				string fileName = Path.GetFileName(customBootTemplateFolderPath + srcBootTexName);
				string path = customBootFolderPath + fileName;
				FileStream fileStream2 = new FileStream(path, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty boot exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000D3AE0 File Offset: 0x000D1EE0
		public static bool CreateBoot(string srcBootTexName)
		{
			bool result;
			try
			{
				string text = "CustomBoot_1.png";
				string customBootTemplateFolderPath = BootIOController.GetCustomBootTemplateFolderPath();
				string customBootFolderPath = BootIOController.GetCustomBootFolderPath();
				FileStream fileStream = new FileStream(customBootTemplateFolderPath + srcBootTexName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customBootFolderPath))
				{
					Directory.CreateDirectory(customBootFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customBootFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomBoot_" + num.ToString() + ".png";
				}
				string path2 = customBootFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty boot exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000D3C1C File Offset: 0x000D201C
		public static string CreateBoot()
		{
			string result;
			try
			{
				string str = "EmptyBoot.png";
				string text = "CustomBoot_1.png";
				string customBootTemplateFolderPath = BootIOController.GetCustomBootTemplateFolderPath();
				string customBootFolderPath = BootIOController.GetCustomBootFolderPath();
				FileStream fileStream = new FileStream(customBootTemplateFolderPath + str, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (!Directory.Exists(customBootFolderPath))
				{
					Directory.CreateDirectory(customBootFolderPath);
				}
				List<string> list = new List<string>();
				foreach (string path in Directory.GetFiles(customBootFolderPath))
				{
					list.Add(Path.GetFileName(path));
				}
				int num = 1;
				while (list.Contains(text))
				{
					num++;
					text = "CustomBoot_" + num.ToString() + ".png";
				}
				string path2 = customBootFolderPath + text;
				FileStream fileStream2 = new FileStream(path2, FileMode.OpenOrCreate);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = text;
			}
			catch (Exception ex)
			{
				Debug.Log("Create empty boot exception: " + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000D3D70 File Offset: 0x000D2170
		public static bool SaveBoot(string SavedBootTexName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customBootFolderPath = BootIOController.GetCustomBootFolderPath();
				if (!Directory.Exists(customBootFolderPath))
				{
					Directory.CreateDirectory(customBootFolderPath);
				}
				string path = customBootFolderPath + SavedBootTexName;
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
				fileStream.Write(fullFile, 0, fullFile.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save boot exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000D3DF0 File Offset: 0x000D21F0
		public static bool CreateTempBoot(string SavedBootTexName, byte[] fullFile)
		{
			bool result;
			try
			{
				string customBootFolderPath = BootIOController.GetCustomBootFolderPath();
				if (!Directory.Exists(customBootFolderPath))
				{
					Directory.CreateDirectory(customBootFolderPath);
				}
				string path = customBootFolderPath + SavedBootTexName;
				FileStream fileStream = new FileStream(path, FileMode.Create);
				Debug.Log("CreateTempBoot-" + fileStream.Length);
				fileStream.Write(fullFile, 0, fullFile.Length);
				Debug.Log("CreateTempBoot-" + fullFile.Length);
				Debug.Log("CreateTempBoot-" + fileStream.Length);
				fileStream.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Log("Save boot exception: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000D3EB8 File Offset: 0x000D22B8
		public static byte[] ReadBootTextureData(string bootTexName, BootBaseType cType)
		{
			byte[] result;
			try
			{
				if (!bootTexName.Contains(".png"))
				{
					bootTexName += ".png";
				}
				string path = string.Empty;
				if (cType == BootBaseType.Custom)
				{
					path = BootIOController.GetCustomBootFolderPath() + bootTexName;
				}
				else if (cType == BootBaseType.System)
				{
					path = BootIOController.GetSystemBootFolderPath() + bootTexName;
				}
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				result = array;
			}
			catch (Exception ex)
			{
				Debug.Log("Load boot data exception: " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000D3F78 File Offset: 0x000D2378
		public static List<string> GetBootNameList(BootBaseType cType)
		{
			List<string> result;
			try
			{
				string path = string.Empty;
				if (cType == BootBaseType.Custom)
				{
					path = BootIOController.GetCustomBootFolderPath();
				}
				else if (cType == BootBaseType.System)
				{
					path = BootIOController.GetSystemBootFolderPath();
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
								if (cType == BootBaseType.Custom)
								{
									if (!text.Contains("Custom") || text.Split(new char[]
									{
										'_'
									}).Length != 3)
									{
										goto IL_107;
									}
								}
								else if (cType == BootBaseType.Custom && text.Split(new char[]
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
				Debug.Log("Get custom boot list exception: " + ex.Message);
				result = null;
			}
			return result;
		}
	}
}
