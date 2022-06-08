using System;
using System.Collections.Generic;
using System.Text;
using CapeEditor;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class CapeManager : MonoBehaviour
{
	// Token: 0x06001A87 RID: 6791 RVA: 0x000D5254 File Offset: 0x000D3654
	private void Awake()
	{
		CapeManager.mInstance = this;
		this.myCharacterCapeEntity = new CapeEntity();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		if (this.useInScene == CapeManager.CurrentScene.MenuScene)
		{
			GCapeItemInfo curSettedCapeInfo = GrowthManagerKit.GetCurSettedCapeInfo();
			if (CapeManager.IsNullCape(curSettedCapeInfo.mName))
			{
				this.myCharacterCapeEntity = null;
			}
			else
			{
				string mName = curSettedCapeInfo.mName;
				if (mName.ToUpper().Contains("Custom".ToUpper()))
				{
					Texture2D texture2D = this.DeserializeCapeData(this.GetCapeTextureData(mName, CapeBaseType.Custom));
					this.myCharacterCapeEntity = new CapeEntity(mName, texture2D, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterCapeMat",
						mainTexture = texture2D,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedCapeInfo, CapeBaseType.Custom, CapeManager.DeserializeCapeType(mName));
				}
				else
				{
					Texture2D texture2D2 = this.DeserializeCapeData(this.GetCapeTextureData(mName, CapeBaseType.System));
					this.myCharacterCapeEntity = new CapeEntity(mName, texture2D2, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterCapeMat",
						mainTexture = texture2D2,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedCapeInfo, CapeBaseType.System, CapeManager.DeserializeCapeType(mName));
				}
			}
			string[] allCapeNameList = GrowthManagerKit.GetAllCapeNameList();
			for (int i = 0; i < allCapeNameList.Length; i++)
			{
				list.Add(allCapeNameList[i]);
			}
			list2 = CapeIOController.GetCapeNameList(CapeBaseType.Custom);
			foreach (string text in list)
			{
				GCapeItemInfo capeItemInfoByName = GrowthManagerKit.GetCapeItemInfoByName(text);
				Texture2D texture2D3 = this.DeserializeCapeData(this.GetCapeTextureData(text, CapeBaseType.System));
				CapeEntity capeEntity = new CapeEntity(text, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, capeItemInfoByName, CapeBaseType.System, CapeManager.DeserializeCapeType(text));
				if (this.myCharacterCapeEntity != null && capeEntity.info.mName == curSettedCapeInfo.mName)
				{
					capeEntity.isSetted = true;
					this.curSettedIndex = this.myAllCapeEntityList.Count;
				}
				this.mySystemCapeEntityList.Add(capeEntity);
				this.myAllCapeEntityList.Add(capeEntity);
			}
			foreach (string text2 in list2)
			{
				GCapeItemInfo capeItemInfoByName2 = GrowthManagerKit.GetCapeItemInfoByName(text2);
				Texture2D texture2D3 = this.DeserializeCapeData(this.GetCapeTextureData(text2, CapeBaseType.Custom));
				CapeEntity capeEntity2 = new CapeEntity(text2, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, capeItemInfoByName2, CapeBaseType.Custom, CapeManager.DeserializeCapeType(text2));
				if (this.myCharacterCapeEntity != null && capeEntity2.info.mName == curSettedCapeInfo.mName)
				{
					capeEntity2.isSetted = true;
					this.curSettedIndex = this.myAllCapeEntityList.Count;
				}
				this.myCustomCapeEntityList.Add(capeEntity2);
				this.myAllCapeEntityList.Add(capeEntity2);
			}
		}
		else if (this.useInScene == CapeManager.CurrentScene.GameScene)
		{
			GCapeItemInfo curSettedCapeInfo2 = GrowthManagerKit.GetCurSettedCapeInfo();
			if (CapeManager.IsNullCape(curSettedCapeInfo2.mName))
			{
				this.myCharacterCapeEntity = null;
			}
			else
			{
				string mName2 = curSettedCapeInfo2.mName;
				if (mName2.ToUpper().Contains("Custom".ToUpper()))
				{
					Texture2D texture2D4 = this.DeserializeCapeData(this.GetCapeTextureData(mName2, CapeBaseType.Custom));
					this.myCharacterCapeEntity = new CapeEntity(mName2, texture2D4, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterCapeMat",
						mainTexture = texture2D4,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedCapeInfo2, CapeBaseType.Custom, CapeManager.DeserializeCapeType(mName2));
				}
				else
				{
					Texture2D texture2D5 = this.DeserializeCapeData(this.GetCapeTextureData(mName2, CapeBaseType.System));
					this.myCharacterCapeEntity = new CapeEntity(mName2, texture2D5, new Material(Shader.Find("Diffuse"))
					{
						name = "myCharacterCapeMat",
						mainTexture = texture2D5,
						color = new Color(1f, 1f, 1f, 0f)
					}, curSettedCapeInfo2, CapeBaseType.System, CapeManager.DeserializeCapeType(mName2));
				}
			}
		}
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x000D5738 File Offset: 0x000D3B38
	private void OnDestroy()
	{
		CapeManager.mInstance = null;
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000D5740 File Offset: 0x000D3B40
	private byte[] GetCapeTextureData(string capeTexName, CapeBaseType hType)
	{
		return CapeIOController.ReadCapeTextureData(capeTexName, hType);
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x000D5749 File Offset: 0x000D3B49
	private byte[] SerializeCapeData(Texture2D tex)
	{
		return tex.EncodeToPNG();
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x000D5754 File Offset: 0x000D3B54
	public static CapeTypeDetail DeserializeCapeType(string name)
	{
		CapeTypeDetail capeTypeDetail = new CapeTypeDetail();
		string[] array = name.Split(new char[]
		{
			'_'
		});
		if (array.Length <= 2)
		{
			capeTypeDetail.isCustomCape = false;
			capeTypeDetail.modelIndex = int.Parse(array[1]);
		}
		else
		{
			capeTypeDetail.isCustomCape = true;
			capeTypeDetail.modelIndex = int.Parse(array[1]);
		}
		return capeTypeDetail;
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x000D57B4 File Offset: 0x000D3BB4
	private Texture2D DeserializeCapeData(byte[] data)
	{
		Texture2D texture2D = new Texture2D(CapeDefine.TexWidth, CapeDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return texture2D;
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x000D57E4 File Offset: 0x000D3BE4
	private Material DeserializeCapeDataToMat(byte[] data)
	{
		Texture2D texture2D = new Texture2D(CapeDefine.TexWidth, CapeDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0f)
		};
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x000D584C File Offset: 0x000D3C4C
	public static bool IsNullCape(string name)
	{
		List<string> list = new List<string>();
		string[] allCapeNameList = GrowthManagerKit.GetAllCapeNameList();
		for (int i = 0; i < allCapeNameList.Length; i++)
		{
			if (name.Equals(allCapeNameList[i]))
			{
				return false;
			}
		}
		list = CapeIOController.GetCapeNameList(CapeBaseType.Custom);
		foreach (string value in list)
		{
			if (name.Equals(value))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x000D58EC File Offset: 0x000D3CEC
	public static CapeNetWorkPackObj DeserializeCapeDataToPackObj(byte[] data)
	{
		if (data.Length == 1)
		{
			return new CapeNetWorkPackObj(true);
		}
		int num = (int)data[0];
		byte[] array = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = data[i + 1];
		}
		string @string = Encoding.Default.GetString(array);
		CapeTypeDetail tDetail = CapeManager.DeserializeCapeType(@string);
		byte[] array2 = new byte[data.Length - num - 1];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = data[num + 1 + j];
		}
		Texture2D texture2D = new Texture2D(CapeDefine.TexWidth, CapeDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(array2);
		texture2D.filterMode = FilterMode.Point;
		return new CapeNetWorkPackObj(tDetail, texture2D, new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0f)
		}, false);
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x000D59E4 File Offset: 0x000D3DE4
	public static byte[] GetMySettedCapePackData()
	{
		GCapeItemInfo curSettedCapeInfo = GrowthManagerKit.GetCurSettedCapeInfo();
		string mName = curSettedCapeInfo.mName;
		if (CapeManager.IsNullCape(mName))
		{
			return new byte[]
			{
				byte.MaxValue
			};
		}
		byte[] array = CapeIOController.ReadCapeTextureData(mName, CapeBaseType.System);
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

	// Token: 0x06001A91 RID: 6801 RVA: 0x000D5A84 File Offset: 0x000D3E84
	public void SetCurSettedCape(int index)
	{
		CapeEntity capeEntity = this.myAllCapeEntityList[index];
		GrowthManagerKit.SetCurSettedCape(this.myAllCapeEntityList[index].info.mName);
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllCapeEntityList.Count)
		{
			this.myAllCapeEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myAllCapeEntityList[index].isSetted = true;
		this.curSettedIndex = index;
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x000D5B0C File Offset: 0x000D3F0C
	public void UnSetCurSettedCape(int index)
	{
		GrowthManagerKit.SetCurSettedCape("Cape_Null");
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllCapeEntityList.Count)
		{
			this.myAllCapeEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myCharacterCapeEntity = null;
		this.curSettedIndex = -1;
	}

	// Token: 0x04001CB1 RID: 7345
	public static CapeManager mInstance;

	// Token: 0x04001CB2 RID: 7346
	public CapeManager.CurrentScene useInScene;

	// Token: 0x04001CB3 RID: 7347
	public CapeEntity myCharacterCapeEntity;

	// Token: 0x04001CB4 RID: 7348
	public List<CapeEntity> mySystemCapeEntityList;

	// Token: 0x04001CB5 RID: 7349
	public List<CapeEntity> myCustomCapeEntityList;

	// Token: 0x04001CB6 RID: 7350
	public List<CapeEntity> myAllCapeEntityList;

	// Token: 0x04001CB7 RID: 7351
	public List<CapeEntity> remoteCharacterCapeEntityList;

	// Token: 0x04001CB8 RID: 7352
	private int curSettedIndex = -1;

	// Token: 0x02000345 RID: 837
	public enum CurrentScene
	{
		// Token: 0x04001CBA RID: 7354
		Nil,
		// Token: 0x04001CBB RID: 7355
		GameScene,
		// Token: 0x04001CBC RID: 7356
		MenuScene
	}
}
