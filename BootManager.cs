using System;
using System.Collections.Generic;
using System.Text;
using BootEditor;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class BootManager : MonoBehaviour
{
	// Token: 0x06001A68 RID: 6760 RVA: 0x000D4180 File Offset: 0x000D2580
	private void Awake()
	{
		BootManager.mInstance = this;
		this.myCharacterBootEntity = new BootEntity();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		if (this.useInScene == BootManager.CurrentScene.MenuScene)
		{
			GBootItemInfo curSettedBootInfo = GrowthManagerKit.GetCurSettedBootInfo();
			if (BootManager.IsNullBoot(curSettedBootInfo.mName))
			{
				this.myCharacterBootEntity = null;
			}
			else
			{
				string mName = curSettedBootInfo.mName;
				if (mName.ToUpper().Contains("Custom".ToUpper()))
				{
					Texture2D texture2D = this.DeserializeBootData(this.GetBootTextureData(mName, BootBaseType.Custom));
					this.myCharacterBootEntity = new BootEntity(mName, texture2D, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterBootMat",
						mainTexture = texture2D,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedBootInfo, BootBaseType.Custom, BootManager.DeserializeBootType(mName));
				}
				else
				{
					Texture2D texture2D2 = this.DeserializeBootData(this.GetBootTextureData(mName, BootBaseType.System));
					this.myCharacterBootEntity = new BootEntity(mName, texture2D2, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterBootMat",
						mainTexture = texture2D2,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedBootInfo, BootBaseType.System, BootManager.DeserializeBootType(mName));
				}
			}
			string[] allBootNameList = GrowthManagerKit.GetAllBootNameList();
			for (int i = 0; i < allBootNameList.Length; i++)
			{
				list.Add(allBootNameList[i]);
			}
			list2 = BootIOController.GetBootNameList(BootBaseType.Custom);
			foreach (string text in list)
			{
				GBootItemInfo bootItemInfoByName = GrowthManagerKit.GetBootItemInfoByName(text);
				Texture2D texture2D3 = this.DeserializeBootData(this.GetBootTextureData(text, BootBaseType.System));
				BootEntity bootEntity = new BootEntity(text, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, bootItemInfoByName, BootBaseType.System, BootManager.DeserializeBootType(text));
				if (this.myCharacterBootEntity != null && bootEntity.info.mName == curSettedBootInfo.mName)
				{
					bootEntity.isSetted = true;
					this.curSettedIndex = this.myAllBootEntityList.Count;
				}
				this.mySystemBootEntityList.Add(bootEntity);
				this.myAllBootEntityList.Add(bootEntity);
			}
			foreach (string text2 in list2)
			{
				GBootItemInfo bootItemInfoByName2 = GrowthManagerKit.GetBootItemInfoByName(text2);
				Texture2D texture2D3 = this.DeserializeBootData(this.GetBootTextureData(text2, BootBaseType.Custom));
				BootEntity bootEntity2 = new BootEntity(text2, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, bootItemInfoByName2, BootBaseType.Custom, BootManager.DeserializeBootType(text2));
				if (this.myCharacterBootEntity != null && bootEntity2.info.mName == curSettedBootInfo.mName)
				{
					bootEntity2.isSetted = true;
					this.curSettedIndex = this.myAllBootEntityList.Count;
				}
				this.myCustomBootEntityList.Add(bootEntity2);
				this.myAllBootEntityList.Add(bootEntity2);
			}
		}
		else if (this.useInScene == BootManager.CurrentScene.GameScene)
		{
			GBootItemInfo curSettedBootInfo2 = GrowthManagerKit.GetCurSettedBootInfo();
			if (BootManager.IsNullBoot(curSettedBootInfo2.mName))
			{
				this.myCharacterBootEntity = null;
			}
			else
			{
				string mName2 = curSettedBootInfo2.mName;
				if (mName2.ToUpper().Contains("Custom".ToUpper()))
				{
					Texture2D texture2D4 = this.DeserializeBootData(this.GetBootTextureData(mName2, BootBaseType.Custom));
					this.myCharacterBootEntity = new BootEntity(mName2, texture2D4, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterBootMat",
						mainTexture = texture2D4,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedBootInfo2, BootBaseType.Custom, BootManager.DeserializeBootType(mName2));
				}
				else
				{
					Texture2D texture2D5 = this.DeserializeBootData(this.GetBootTextureData(mName2, BootBaseType.System));
					this.myCharacterBootEntity = new BootEntity(mName2, texture2D5, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterBootMat",
						mainTexture = texture2D5,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedBootInfo2, BootBaseType.System, BootManager.DeserializeBootType(mName2));
				}
			}
		}
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x000D4664 File Offset: 0x000D2A64
	private void OnDestroy()
	{
		BootManager.mInstance = null;
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x000D466C File Offset: 0x000D2A6C
	private byte[] GetBootTextureData(string bootTexName, BootBaseType hType)
	{
		return BootIOController.ReadBootTextureData(bootTexName, hType);
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x000D4675 File Offset: 0x000D2A75
	private byte[] SerializeBootData(Texture2D tex)
	{
		return tex.EncodeToPNG();
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x000D4680 File Offset: 0x000D2A80
	public static BootTypeDetail DeserializeBootType(string name)
	{
		BootTypeDetail bootTypeDetail = new BootTypeDetail();
		string[] array = name.Split(new char[]
		{
			'_'
		});
		if (array.Length <= 2)
		{
			bootTypeDetail.isCustomBoot = false;
			bootTypeDetail.modelIndex = int.Parse(array[1]);
		}
		else
		{
			bootTypeDetail.isCustomBoot = true;
			bootTypeDetail.modelIndex = int.Parse(array[1]);
		}
		return bootTypeDetail;
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x000D46E0 File Offset: 0x000D2AE0
	private Texture2D DeserializeBootData(byte[] data)
	{
		Texture2D texture2D = new Texture2D(BootDefine.TexWidth, BootDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return texture2D;
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x000D4710 File Offset: 0x000D2B10
	private Material DeserializeBootDataToMat(byte[] data)
	{
		Texture2D texture2D = new Texture2D(BootDefine.TexWidth, BootDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0f)
		};
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x000D4778 File Offset: 0x000D2B78
	public static bool IsNullBoot(string name)
	{
		List<string> list = new List<string>();
		string[] allBootNameList = GrowthManagerKit.GetAllBootNameList();
		for (int i = 0; i < allBootNameList.Length; i++)
		{
			if (name.Equals(allBootNameList[i]))
			{
				return false;
			}
		}
		list = BootIOController.GetBootNameList(BootBaseType.Custom);
		foreach (string value in list)
		{
			if (name.Equals(value))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x000D4818 File Offset: 0x000D2C18
	public static BootNetWorkPackObj DeserializeBootDataToPackObj(byte[] data)
	{
		if (data.Length == 1)
		{
			return new BootNetWorkPackObj(true);
		}
		int num = (int)data[0];
		byte[] array = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = data[i + 1];
		}
		string @string = Encoding.Default.GetString(array);
		BootTypeDetail tDetail = BootManager.DeserializeBootType(@string);
		byte[] array2 = new byte[data.Length - num - 1];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = data[num + 1 + j];
		}
		Texture2D texture2D = new Texture2D(BootDefine.TexWidth, BootDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(array2);
		texture2D.filterMode = FilterMode.Point;
		return new BootNetWorkPackObj(tDetail, texture2D, new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0f)
		}, false);
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x000D4910 File Offset: 0x000D2D10
	public static byte[] GetMySettedBootPackData()
	{
		GBootItemInfo curSettedBootInfo = GrowthManagerKit.GetCurSettedBootInfo();
		string mName = curSettedBootInfo.mName;
		if (BootManager.IsNullBoot(mName))
		{
			return new byte[]
			{
				byte.MaxValue
			};
		}
		byte[] array = BootIOController.ReadBootTextureData(mName, BootBaseType.System);
		byte[] bytes = Encoding.Default.GetBytes(mName.Replace(mName.Split(new char[]
		{
			'_'
		})[0], "h").ToCharArray());
		byte b = (byte)bytes.Length;
		byte[] array2 = new byte[array.Length + bytes.Length + 1];
		array2[0] = b;
		bytes.CopyTo(array2, 1);
		array.CopyTo(array2, (int)(b + 1));
		return array2;
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x000D49B0 File Offset: 0x000D2DB0
	public void SetCurSettedBoot(int index)
	{
		BootEntity bootEntity = this.myAllBootEntityList[index];
		GrowthManagerKit.SetCurSettedBoot(this.myAllBootEntityList[index].info.mName);
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllBootEntityList.Count)
		{
			this.myAllBootEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myAllBootEntityList[index].isSetted = true;
		this.curSettedIndex = index;
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x000D4A38 File Offset: 0x000D2E38
	public void UnSetCurSettedBoot(int index)
	{
		GrowthManagerKit.SetCurSettedBoot("Boot_Null");
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllBootEntityList.Count)
		{
			this.myAllBootEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myCharacterBootEntity = null;
		this.curSettedIndex = -1;
	}

	// Token: 0x04001C93 RID: 7315
	public static BootManager mInstance;

	// Token: 0x04001C94 RID: 7316
	public BootManager.CurrentScene useInScene;

	// Token: 0x04001C95 RID: 7317
	public BootEntity myCharacterBootEntity;

	// Token: 0x04001C96 RID: 7318
	public List<BootEntity> mySystemBootEntityList;

	// Token: 0x04001C97 RID: 7319
	public List<BootEntity> myCustomBootEntityList;

	// Token: 0x04001C98 RID: 7320
	public List<BootEntity> myAllBootEntityList;

	// Token: 0x04001C99 RID: 7321
	public List<BootEntity> remoteCharacterBootEntityList;

	// Token: 0x04001C9A RID: 7322
	private int curSettedIndex = -1;

	// Token: 0x0200033D RID: 829
	public enum CurrentScene
	{
		// Token: 0x04001C9C RID: 7324
		Nil,
		// Token: 0x04001C9D RID: 7325
		GameScene,
		// Token: 0x04001C9E RID: 7326
		MenuScene
	}
}
