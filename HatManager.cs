using System;
using System.Collections.Generic;
using System.Text;
using HatEditor;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class HatManager : MonoBehaviour
{
	// Token: 0x06001AA6 RID: 6822 RVA: 0x000D6328 File Offset: 0x000D4728
	private void Awake()
	{
		HatManager.mInstance = this;
		this.myCharacterHatEntity = new HatEntity();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		if (this.useInScene == HatManager.CurrentScene.MenuScene)
		{
			GHatItemInfo curSettedHatInfo = GrowthManagerKit.GetCurSettedHatInfo();
			if (HatManager.IsNullHat(curSettedHatInfo.mName))
			{
				this.myCharacterHatEntity = null;
			}
			else
			{
				string mName = curSettedHatInfo.mName;
				if (mName.ToUpper().Contains("Custom".ToUpper()))
				{
					Texture2D texture2D = this.DeserializeHatData(this.GetHatTextureData(mName, HatBaseType.Custom));
					this.myCharacterHatEntity = new HatEntity(mName, texture2D, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterHatMat",
						mainTexture = texture2D,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedHatInfo, HatBaseType.Custom, HatManager.DeserializeHatType(mName));
				}
				else
				{
					Texture2D texture2D2 = this.DeserializeHatData(this.GetHatTextureData(mName, HatBaseType.System));
					this.myCharacterHatEntity = new HatEntity(mName, texture2D2, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterHatMat",
						mainTexture = texture2D2,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedHatInfo, HatBaseType.System, HatManager.DeserializeHatType(mName));
				}
			}
			string[] allHatNameList = GrowthManagerKit.GetAllHatNameList();
			for (int i = 0; i < allHatNameList.Length; i++)
			{
				list.Add(allHatNameList[i]);
			}
			list2 = HatIOController.GetHatNameList(HatBaseType.Custom);
			foreach (string text in list)
			{
				GHatItemInfo hatItemInfoByName = GrowthManagerKit.GetHatItemInfoByName(text);
				Texture2D texture2D3 = this.DeserializeHatData(this.GetHatTextureData(text, HatBaseType.System));
				HatEntity hatEntity = new HatEntity(text, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, hatItemInfoByName, HatBaseType.System, HatManager.DeserializeHatType(text));
				if (this.myCharacterHatEntity != null && hatEntity.info.mName == curSettedHatInfo.mName)
				{
					hatEntity.isSetted = true;
					this.curSettedIndex = this.myAllHatEntityList.Count;
				}
				this.mySystemHatEntityList.Add(hatEntity);
				this.myAllHatEntityList.Add(hatEntity);
			}
			foreach (string text2 in list2)
			{
				GHatItemInfo hatItemInfoByName2 = GrowthManagerKit.GetHatItemInfoByName(text2);
				Texture2D texture2D3 = this.DeserializeHatData(this.GetHatTextureData(text2, HatBaseType.Custom));
				HatEntity hatEntity2 = new HatEntity(text2, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, hatItemInfoByName2, HatBaseType.Custom, HatManager.DeserializeHatType(text2));
				if (this.myCharacterHatEntity != null && hatEntity2.info.mName == curSettedHatInfo.mName)
				{
					hatEntity2.isSetted = true;
					this.curSettedIndex = this.myAllHatEntityList.Count;
				}
				this.myCustomHatEntityList.Add(hatEntity2);
				this.myAllHatEntityList.Add(hatEntity2);
			}
		}
		else if (this.useInScene == HatManager.CurrentScene.GameScene)
		{
			GHatItemInfo curSettedHatInfo2 = GrowthManagerKit.GetCurSettedHatInfo();
			if (HatManager.IsNullHat(curSettedHatInfo2.mName))
			{
				this.myCharacterHatEntity = null;
			}
			else
			{
				string mName2 = curSettedHatInfo2.mName;
				if (mName2.ToUpper().Contains("Custom".ToUpper()))
				{
					Texture2D texture2D4 = this.DeserializeHatData(this.GetHatTextureData(mName2, HatBaseType.Custom));
					this.myCharacterHatEntity = new HatEntity(mName2, texture2D4, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterHatMat",
						mainTexture = texture2D4,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedHatInfo2, HatBaseType.Custom, HatManager.DeserializeHatType(mName2));
				}
				else
				{
					Texture2D texture2D5 = this.DeserializeHatData(this.GetHatTextureData(mName2, HatBaseType.System));
					this.myCharacterHatEntity = new HatEntity(mName2, texture2D5, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterHatMat",
						mainTexture = texture2D5,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedHatInfo2, HatBaseType.System, HatManager.DeserializeHatType(mName2));
				}
			}
		}
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000D680C File Offset: 0x000D4C0C
	private void OnDestroy()
	{
		HatManager.mInstance = null;
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000D6814 File Offset: 0x000D4C14
	private byte[] GetHatTextureData(string hatTexName, HatBaseType hType)
	{
		return HatIOController.ReadHatTextureData(hatTexName, hType);
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x000D681D File Offset: 0x000D4C1D
	private byte[] SerializeHatData(Texture2D tex)
	{
		return tex.EncodeToPNG();
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x000D6828 File Offset: 0x000D4C28
	public static HatTypeDetail DeserializeHatType(string name)
	{
		HatTypeDetail hatTypeDetail = new HatTypeDetail();
		string[] array = name.Split(new char[]
		{
			'_'
		});
		if (array.Length <= 2)
		{
			hatTypeDetail.isCustomHat = false;
			hatTypeDetail.modelIndex = int.Parse(array[1]);
		}
		else
		{
			hatTypeDetail.isCustomHat = true;
			hatTypeDetail.modelIndex = int.Parse(array[1]);
		}
		return hatTypeDetail;
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x000D6888 File Offset: 0x000D4C88
	private Texture2D DeserializeHatData(byte[] data)
	{
		Texture2D texture2D = new Texture2D(HatDefine.TexWidth, HatDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return texture2D;
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x000D68B8 File Offset: 0x000D4CB8
	private Material DeserializeHatDataToMat(byte[] data)
	{
		Texture2D texture2D = new Texture2D(HatDefine.TexWidth, HatDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0.2f)
		};
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000D6920 File Offset: 0x000D4D20
	public static bool IsNullHat(string name)
	{
		List<string> list = new List<string>();
		string[] allHatNameList = GrowthManagerKit.GetAllHatNameList();
		for (int i = 0; i < allHatNameList.Length; i++)
		{
			if (name.Equals(allHatNameList[i]))
			{
				return false;
			}
		}
		list = HatIOController.GetHatNameList(HatBaseType.Custom);
		foreach (string value in list)
		{
			if (name.Equals(value))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x000D69C0 File Offset: 0x000D4DC0
	public static HatNetWorkPackObj DeserializeHatDataToPackObj(byte[] data)
	{
		if (data.Length == 1)
		{
			return new HatNetWorkPackObj(true);
		}
		int num = (int)data[0];
		byte[] array = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = data[i + 1];
		}
		string @string = Encoding.Default.GetString(array);
		HatTypeDetail hatTypeDetail = HatManager.DeserializeHatType(@string);
		byte[] array2 = new byte[data.Length - num - 1];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = data[num + 1 + j];
		}
		Texture2D texture2D = new Texture2D(HatDefine.TexWidth, HatDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(array2);
		texture2D.filterMode = FilterMode.Point;
		Material material = new Material(Shader.Find("Diffuse"));
		material.mainTexture = texture2D;
		if (hatTypeDetail.modelIndex == 1)
		{
			material.color = new Color(1f, 1f, 1f, 0.2f);
		}
		else
		{
			material.color = new Color(1f, 1f, 1f, 0f);
		}
		return new HatNetWorkPackObj(hatTypeDetail, texture2D, material, false);
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x000D6AE8 File Offset: 0x000D4EE8
	public static byte[] GetMySettedHatPackData()
	{
		GHatItemInfo curSettedHatInfo = GrowthManagerKit.GetCurSettedHatInfo();
		string mName = curSettedHatInfo.mName;
		if (HatManager.IsNullHat(mName))
		{
			return new byte[]
			{
				byte.MaxValue
			};
		}
		byte[] array = HatIOController.ReadHatTextureData(mName, HatBaseType.System);
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

	// Token: 0x06001AB0 RID: 6832 RVA: 0x000D6B88 File Offset: 0x000D4F88
	public void SetCurSettedHat(int index)
	{
		HatEntity hatEntity = this.myAllHatEntityList[index];
		GrowthManagerKit.SetCurSettedHat(this.myAllHatEntityList[index].info.mName);
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllHatEntityList.Count)
		{
			this.myAllHatEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myAllHatEntityList[index].isSetted = true;
		this.curSettedIndex = index;
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x000D6C10 File Offset: 0x000D5010
	public void UnSetCurSettedHat(int index)
	{
		GrowthManagerKit.SetCurSettedHat("Hat_Null");
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllHatEntityList.Count)
		{
			this.myAllHatEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myCharacterHatEntity = null;
		this.curSettedIndex = -1;
	}

	// Token: 0x04001CCF RID: 7375
	public static HatManager mInstance;

	// Token: 0x04001CD0 RID: 7376
	public HatManager.CurrentScene useInScene;

	// Token: 0x04001CD1 RID: 7377
	public HatEntity myCharacterHatEntity;

	// Token: 0x04001CD2 RID: 7378
	public List<HatEntity> mySystemHatEntityList;

	// Token: 0x04001CD3 RID: 7379
	public List<HatEntity> myCustomHatEntityList;

	// Token: 0x04001CD4 RID: 7380
	public List<HatEntity> myAllHatEntityList;

	// Token: 0x04001CD5 RID: 7381
	public List<HatEntity> remoteCharacterHatEntityList;

	// Token: 0x04001CD6 RID: 7382
	private int curSettedIndex = -1;

	// Token: 0x0200034D RID: 845
	public enum CurrentScene
	{
		// Token: 0x04001CD8 RID: 7384
		Nil,
		// Token: 0x04001CD9 RID: 7385
		GameScene,
		// Token: 0x04001CDA RID: 7386
		MenuScene
	}
}
