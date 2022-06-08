using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using GOG.Utility;
using RioLog;
using SimpleJSON;
using SkinEditor;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class SkinManager : MonoBehaviour
{
	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06001B05 RID: 6917 RVA: 0x000DA120 File Offset: 0x000D8520
	// (remove) Token: 0x06001B06 RID: 6918 RVA: 0x000DA154 File Offset: 0x000D8554
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event SkinManager.SharedSkinsDownloadEventHandler OnSharedSkinsDownloaded;

	// Token: 0x06001B07 RID: 6919 RVA: 0x000DA188 File Offset: 0x000D8588
	public void GenSharedSkinsDownloadedEvent()
	{
		if (SkinManager.OnSharedSkinsDownloaded != null)
		{
			SkinManager.OnSharedSkinsDownloaded();
		}
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000DA1A0 File Offset: 0x000D85A0
	private void Awake()
	{
		SkinManager.mInstance = this;
		this.myCharacterSkinEntity = new SkinEntity();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		if (this.useInScene == SkinManager.CurrentScene.MenuScene)
		{
			GSkinItemInfo curSettedSkinInfo = GrowthManagerKit.GetCurSettedSkinInfo();
			string mName = curSettedSkinInfo.mName;
			if (mName.ToUpper().Contains("Custom".ToUpper()))
			{
				Texture2D texture2D = this.DeserializeSkinData(this.GetSkinTextureData(mName, SkinBaseType.Custom));
				this.myCharacterSkinEntity = new SkinEntity(mName, texture2D, new Material(Shader.Find("Diffuse"))
				{
					name = "myCharacterSkinMat",
					mainTexture = texture2D,
					color = new Color(1f, 1f, 1f, 0f)
				}, curSettedSkinInfo, SkinBaseType.Custom);
			}
			else
			{
				Texture2D texture2D2 = this.DeserializeSkinData(this.GetSkinTextureData(mName, SkinBaseType.System));
				this.myCharacterSkinEntity = new SkinEntity(mName, texture2D2, new Material(Shader.Find("Diffuse"))
				{
					name = "myCharacterSkinMat",
					mainTexture = texture2D2,
					color = new Color(1f, 1f, 1f, 0f)
				}, curSettedSkinInfo, SkinBaseType.System);
			}
			string[] allSkinNameList = GrowthManagerKit.GetAllSkinNameList();
			for (int i = 0; i < allSkinNameList.Length; i++)
			{
				list.Add(allSkinNameList[i]);
			}
			list2 = SkinIOController.GetSkinNameList(SkinBaseType.Custom);
			list2 = this.OrderCustomSkinNameList(list2);
			foreach (string text in list)
			{
				GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName(text);
				Texture2D texture2D3 = this.DeserializeSkinData(this.GetSkinTextureData(text, SkinBaseType.System));
				SkinEntity skinEntity = new SkinEntity(text, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, skinItemInfoByName, SkinBaseType.System);
				if (skinEntity.info.mName == mName)
				{
					skinEntity.isSetted = true;
					this.curSettedIndex = this.myAllSkinEntityList.Count;
				}
				this.mySystemSkinEntityList.Add(skinEntity);
				this.myAllSkinEntityList.Add(skinEntity);
			}
			foreach (string text2 in list2)
			{
				GSkinItemInfo skinItemInfoByName2 = GrowthManagerKit.GetSkinItemInfoByName(text2);
				Texture2D texture2D3 = this.DeserializeSkinData(this.GetSkinTextureData(text2, SkinBaseType.Custom));
				SkinEntity skinEntity2 = new SkinEntity(text2, texture2D3, new Material(Shader.Find("Diffuse"))
				{
					mainTexture = texture2D3,
					color = new Color(1f, 1f, 1f, 0f)
				}, skinItemInfoByName2, SkinBaseType.Custom);
				if (skinEntity2.info.mName == mName)
				{
					skinEntity2.isSetted = true;
					this.curSettedIndex = this.myAllSkinEntityList.Count;
				}
				this.myCustomSkinEntityList.Add(skinEntity2);
				this.myAllSkinEntityList.Add(skinEntity2);
			}
		}
		else if (this.useInScene == SkinManager.CurrentScene.GameScene)
		{
			GSkinItemInfo curSettedSkinInfo2 = GrowthManagerKit.GetCurSettedSkinInfo();
			string mName2 = curSettedSkinInfo2.mName;
			if (mName2.ToUpper().Contains("Custom".ToUpper()))
			{
				Texture2D texture2D4 = this.DeserializeSkinData(this.GetSkinTextureData(mName2, SkinBaseType.Custom));
				this.myCharacterSkinEntity = new SkinEntity(mName2, texture2D4, new Material(Shader.Find("Diffuse"))
				{
					name = "myCharacterSkinMat",
					mainTexture = texture2D4,
					color = new Color(1f, 1f, 1f, 0f)
				}, curSettedSkinInfo2, SkinBaseType.Custom);
			}
			else
			{
				Texture2D texture2D5 = this.DeserializeSkinData(this.GetSkinTextureData(mName2, SkinBaseType.System));
				this.myCharacterSkinEntity = new SkinEntity(mName2, texture2D5, new Material(Shader.Find("Diffuse"))
				{
					name = "myCharacterSkinMat",
					mainTexture = texture2D5,
					color = new Color(1f, 1f, 1f, 0f)
				}, curSettedSkinInfo2, SkinBaseType.System);
			}
		}
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x000DA60C File Offset: 0x000D8A0C
	private void OnDestroy()
	{
		SkinManager.mInstance = null;
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x000DA614 File Offset: 0x000D8A14
	private List<string> OrderCustomSkinNameList(List<string> nList)
	{
		List<int> list = new List<int>();
		int item = 1;
		foreach (string text in nList)
		{
			if (text.Split(new char[]
			{
				'_'
			}).Length == 2)
			{
				bool flag = int.TryParse(text.Split(new char[]
				{
					'_'
				})[1], out item);
				if (flag)
				{
					list.Add(item);
				}
			}
		}
		if (list.Count != nList.Count)
		{
			return nList;
		}
		nList.Sort(delegate(string left, string right)
		{
			if (int.Parse(left.Split(new char[]
			{
				'_'
			})[1]) > int.Parse(right.Split(new char[]
			{
				'_'
			})[1]))
			{
				return 1;
			}
			if (int.Parse(left.Split(new char[]
			{
				'_'
			})[1]) == int.Parse(right.Split(new char[]
			{
				'_'
			})[1]))
			{
				return 0;
			}
			return -1;
		});
		return nList;
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x000DA6EC File Offset: 0x000D8AEC
	private List<string> OrderSharedSkinNameList(List<string> nList)
	{
		List<int> list = new List<int>();
		int item = 1;
		foreach (string text in nList)
		{
			if (text.Split(new char[]
			{
				'-'
			}).Length == 2)
			{
				bool flag = int.TryParse(text.Split(new char[]
				{
					'-'
				})[0], out item);
				if (flag)
				{
					list.Add(item);
				}
			}
		}
		if (list.Count != nList.Count)
		{
			return nList;
		}
		nList.Sort(delegate(string left, string right)
		{
			if (int.Parse(left.Split(new char[]
			{
				'-'
			})[0]) > int.Parse(right.Split(new char[]
			{
				'-'
			})[0]))
			{
				return 1;
			}
			if (int.Parse(left.Split(new char[]
			{
				'-'
			})[0]) == int.Parse(right.Split(new char[]
			{
				'-'
			})[0]))
			{
				return 0;
			}
			return -1;
		});
		return nList;
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x000DA7C4 File Offset: 0x000D8BC4
	private byte[] GetSkinTextureData(string skinTexName, SkinBaseType sType)
	{
		return SkinIOController.ReadSkinTextureData(skinTexName, sType);
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x000DA7CD File Offset: 0x000D8BCD
	private byte[] SerializeSkinData(Texture2D tex)
	{
		return tex.EncodeToPNG();
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x000DA7D8 File Offset: 0x000D8BD8
	public Texture2D DeserializeSkinData(byte[] data)
	{
		Texture2D texture2D = new Texture2D(SkinDefine.TexWidth, SkinDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return texture2D;
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x000DA808 File Offset: 0x000D8C08
	public static Material DeserializeSkinDataToMat(byte[] data)
	{
		Texture2D texture2D = new Texture2D(SkinDefine.TexWidth, SkinDefine.TexHeight, TextureFormat.ARGB4444, false);
		texture2D.LoadImage(data);
		texture2D.filterMode = FilterMode.Point;
		return new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0f)
		};
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000DA870 File Offset: 0x000D8C70
	public static byte[] GetMySettedSkinTexData()
	{
		GSkinItemInfo curSettedSkinInfo = GrowthManagerKit.GetCurSettedSkinInfo();
		string mName = curSettedSkinInfo.mName;
		if (mName.ToUpper().Contains("Custom".ToUpper()))
		{
			return SkinIOController.ReadSkinTextureData(mName, SkinBaseType.Custom);
		}
		return SkinIOController.ReadSkinTextureData(mName, SkinBaseType.System);
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000DA8B4 File Offset: 0x000D8CB4
	public void SetCurSettedSkin(int index)
	{
		SkinEntity skinEntity = this.myAllSkinEntityList[index];
		GrowthManagerKit.SetCurSettedSkin(this.myAllSkinEntityList[index].info.mName);
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllSkinEntityList.Count)
		{
			this.myAllSkinEntityList[this.curSettedIndex].isSetted = false;
		}
		this.myAllSkinEntityList[index].isSetted = true;
		this.curSettedIndex = index;
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000DA93C File Offset: 0x000D8D3C
	public void LoadEditorForNewSkin()
	{
		this.editorCfg = new SkinManager.EditorLogicCfg(true, string.Empty);
		UnityEngine.Object.DontDestroyOnLoad(base.transform.parent.gameObject);
		Application.LoadLevel("SkinEditorScene");
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x000DA96E File Offset: 0x000D8D6E
	public void LoadEditorForExistSkin(string skinName)
	{
		this.editorCfg = new SkinManager.EditorLogicCfg(false, skinName);
		UnityEngine.Object.DontDestroyOnLoad(base.transform.parent.gameObject);
		Application.LoadLevel("SkinEditorScene");
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x000DA99C File Offset: 0x000D8D9C
	public void DeleteSkin(int index)
	{
		if (this.curSettedIndex >= 0 && this.curSettedIndex < this.myAllSkinEntityList.Count)
		{
			string text = this.myAllSkinEntityList[index].info.mName + ".png";
			this.myAllSkinEntityList[index].info.Disable();
			SkinIOController.DeleteSkin(this.myAllSkinEntityList[index].info.mName + ".png");
			this.myAllSkinEntityList.RemoveAt(index);
			int num = index - this.mySystemSkinEntityList.Count;
			if (num < this.myCustomSkinEntityList.Count && num >= 0)
			{
				this.myCustomSkinEntityList.RemoveAt(index - this.mySystemSkinEntityList.Count);
			}
			GGCloudServiceAdapter.mInstance.mUploadService.RemoveFileByUser(UIUserDataController.GetDefaultRoleName() + "@Private@" + text, UIUserDataController.GetDefaultUserName(), new SkinRemoveCallBack(text, false));
		}
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x000DAA9D File Offset: 0x000D8E9D
	public void StartDownloadAllSharedSkins(string[] skinUrlList, string[] skinNameList, string[] skinDescriptionList)
	{
		SkinIOController.DeleteAllSharedSkin();
		base.StartCoroutine(this.DownloadAllSharedSkins(skinUrlList, skinNameList, skinDescriptionList));
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x000DAAB8 File Offset: 0x000D8EB8
	private IEnumerator DownloadAllSharedSkins(string[] skinUrlList, string[] skinNameList, string[] skinDescriptionList)
	{
		for (int i = 0; i < skinUrlList.Length; i++)
		{
			RioRyanDebug.Log("Download Skin - " + skinNameList[i]);
			WWW www = new WWW(skinUrlList[i]);
			yield return www;
			SkinIOController.SaveSharedSkin(skinNameList[i], www.bytes);
			this.progressController.Update((i + 1) * 100 / skinUrlList.Length);
			RioRyanDebug.Log("progressController.Progress - " + this.progressController.Progress);
		}
		yield return new WaitForSeconds(0.2f);
		this.RefreshSharedSkinsList();
		this.GenSharedSkinsDownloadedEvent();
		RioRyanDebug.Log("Download OK!");
		yield break;
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x000DAAE1 File Offset: 0x000D8EE1
	public void InitSharedSkinsPageInfo(int totalSkinsCount)
	{
		this.m_SharedSkinsCountInServer = totalSkinsCount;
		this.m_SharedSkinsTotalPage = totalSkinsCount / 20;
		this.m_SharedSkinsTotalPage = Math.Max(1, this.m_SharedSkinsTotalPage);
		this.m_SharedSkinsCurPage = UnityEngine.Random.Range(1, this.m_SharedSkinsTotalPage + 1);
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x000DAB1C File Offset: 0x000D8F1C
	public void RequestSharedSkins()
	{
		RioRyanDebug.Log("RequestSharedSkins");
		if (this.m_SharedSkinsCountInServer == 0)
		{
			GGCloudServiceAdapter.mInstance.mUploadService.GetFilesCountByType(UploadFileType.AUDIO, new SkinManager.GetAllSharedSkinsCountCallBack());
		}
		else
		{
			this.progressController = new ProgressController("Download Skins", "Download shared skins.");
			if (this.m_SharedSkinsTotalPage == 1)
			{
				this.m_SharedSkinsCurPage = 1;
			}
			else
			{
				this.m_SharedSkinsCurPage++;
				if (this.m_SharedSkinsCurPage > this.m_SharedSkinsTotalPage)
				{
					this.m_SharedSkinsCurPage = 1;
				}
			}
			GGCloudServiceAdapter.mInstance.GetFilesByType(UploadFileType.AUDIO, 20, (this.m_SharedSkinsCurPage - 1) * 20, new SkinManager.DownloadAllSharedSkinsCallBack());
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x000DABD0 File Offset: 0x000D8FD0
	public void DownloadSkinFromSharedList(SkinEntity sEntity)
	{
		string text = SkinIOController.CreateSkin();
		SkinIOController.SaveSkin(text, sEntity.tex.EncodeToPNG());
		string text2 = text.Remove(text.Length - 4);
		GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName(text2);
		Texture2D texture2D = this.DeserializeSkinData(this.GetSkinTextureData(text2, SkinBaseType.Custom));
		SkinEntity item = new SkinEntity(text2, texture2D, new Material(Shader.Find("Diffuse"))
		{
			mainTexture = texture2D,
			color = new Color(1f, 1f, 1f, 0f)
		}, skinItemInfoByName, SkinBaseType.Custom);
		this.myCustomSkinEntityList.Add(item);
		this.myAllSkinEntityList.Add(item);
		sEntity.hasBeenDownloaded = true;
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x000DAC80 File Offset: 0x000D9080
	public void RefreshSharedSkinsList()
	{
		List<string> list = new List<string>();
		list = SkinIOController.GetSkinNameList(SkinBaseType.Shared);
		list = this.OrderSharedSkinNameList(list);
		this.sharedSkinEntityList.Clear();
		foreach (string text in list)
		{
			GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName(text);
			Texture2D texture2D = this.DeserializeSkinData(this.GetSkinTextureData(text, SkinBaseType.Shared));
			SkinEntity skinEntity = new SkinEntity(text, texture2D, new Material(Shader.Find("Diffuse"))
			{
				mainTexture = texture2D,
				color = new Color(1f, 1f, 1f, 0f)
			}, skinItemInfoByName, SkinBaseType.Custom);
			skinEntity.info.mDescription = "Pick your favorite skin, download to your own skin list.";
			skinEntity.info.mDescription = skinEntity.info.mDescription.ToUpper();
			this.sharedSkinEntityList.Add(skinEntity);
		}
		this.progressController.EndProgress("Download Successful!");
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x000DAD98 File Offset: 0x000D9198
	public bool CanShare(SkinEntity sEntity)
	{
		return sEntity.type == SkinBaseType.Custom && !sEntity.hasBeenShared && !GrowthManagerKit.HasSkinShared(sEntity.name);
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x000DADC4 File Offset: 0x000D91C4
	public bool CanDownload(SkinEntity sEntity)
	{
		return !sEntity.hasBeenDownloaded;
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x000DADD4 File Offset: 0x000D91D4
	public void UploadCustomSkin(SkinEntity sEntity)
	{
		if (sEntity.type != SkinBaseType.Custom)
		{
			return;
		}
		this.progressController = new ProgressController("Upload Skin", "Sharing skins.");
		string text = sEntity.name + ".png";
		SkinIOController.DeleteAllCachedSkin();
		SkinIOController.SaveCachedSkin(text, sEntity.tex.EncodeToPNG());
		sEntity.descriptionForSharing = "Maker [" + UIUserDataController.GetDefaultRoleName() + "]";
		sEntity.nameForSharing = sEntity.name;
		GGCloudServiceAdapter.mInstance.mUploadService.RemoveFileByUser(UIUserDataController.GetDefaultRoleName() + "@Public@" + text, UIUserDataController.GetDefaultUserName(), new SkinManager.SMSkinRemoveCallBack(sEntity.name, text, true, sEntity.nameForSharing, sEntity.descriptionForSharing));
		sEntity.hasBeenShared = true;
	}

	// Token: 0x04001D53 RID: 7507
	public static SkinManager mInstance;

	// Token: 0x04001D54 RID: 7508
	public SkinManager.CurrentScene useInScene;

	// Token: 0x04001D55 RID: 7509
	public SkinEntity myCharacterSkinEntity;

	// Token: 0x04001D56 RID: 7510
	public List<SkinEntity> mySystemSkinEntityList;

	// Token: 0x04001D57 RID: 7511
	public List<SkinEntity> myCustomSkinEntityList;

	// Token: 0x04001D58 RID: 7512
	public List<SkinEntity> sharedSkinEntityList;

	// Token: 0x04001D59 RID: 7513
	public List<SkinEntity> myAllSkinEntityList;

	// Token: 0x04001D5A RID: 7514
	public List<SkinEntity> remoteCharacterSkinEntityList;

	// Token: 0x04001D5B RID: 7515
	public SkinManager.EditorLogicCfg editorCfg;

	// Token: 0x04001D5C RID: 7516
	private int curSettedIndex = -1;

	// Token: 0x04001D5D RID: 7517
	public ProgressController progressController;

	// Token: 0x04001D5E RID: 7518
	public int m_SharedSkinsCountInServer;

	// Token: 0x04001D5F RID: 7519
	public int m_SharedSkinsCurPage = 1;

	// Token: 0x04001D60 RID: 7520
	public int m_SharedSkinsTotalPage = 1;

	// Token: 0x04001D61 RID: 7521
	private const int m_MaxNumInPage = 20;

	// Token: 0x0200035F RID: 863
	public enum CurrentScene
	{
		// Token: 0x04001D65 RID: 7525
		Nil,
		// Token: 0x04001D66 RID: 7526
		GameScene,
		// Token: 0x04001D67 RID: 7527
		MenuScene
	}

	// Token: 0x02000360 RID: 864
	// (Invoke) Token: 0x06001B21 RID: 6945
	public delegate void SharedSkinsDownloadEventHandler();

	// Token: 0x02000361 RID: 865
	public struct EditorLogicCfg
	{
		// Token: 0x06001B24 RID: 6948 RVA: 0x000DAF90 File Offset: 0x000D9390
		public EditorLogicCfg(bool tIsNewSkin, string tSkinNameToEdit)
		{
			this.isNewSkin = tIsNewSkin;
			this.skinNameToEdit = tSkinNameToEdit;
		}

		// Token: 0x04001D68 RID: 7528
		public bool isNewSkin;

		// Token: 0x04001D69 RID: 7529
		public string skinNameToEdit;
	}

	// Token: 0x02000362 RID: 866
	public class DownloadAllSharedSkinsCallBack : App42CallBack
	{
		// Token: 0x06001B26 RID: 6950 RVA: 0x000DAFA8 File Offset: 0x000D93A8
		public void OnSuccess(object response)
		{
			JObject jobject = response as JObject;
			string text = jobject["UploadStrResponse"];
			RioRyanDebug.Log(text);
			Upload upload = new UploadResponseBuilder().BuildResponse(text);
			IList<Upload.File> fileList = upload.GetFileList();
			string[] array = new string[fileList.Count];
			string[] array2 = new string[fileList.Count];
			string[] array3 = new string[fileList.Count];
			for (int i = 0; i < fileList.Count; i++)
			{
				array[i] = fileList[i].GetUrl();
				array2[i] = (i + 1).ToString() + "-MAKER [" + fileList[i].GetName().Split(new char[]
				{
					'@'
				})[0] + "].png";
				RioRyanDebug.Log("fileName is " + fileList[i].GetName());
				RioRyanDebug.Log("fileType is " + fileList[i].GetType());
				RioRyanDebug.Log("fileUrl is " + fileList[i].GetUrl());
				RioRyanDebug.Log("TinyUrl Is  : " + fileList[i].GetTinyUrl());
				RioRyanDebug.Log("fileDescription is " + fileList[i].GetDescription());
				array3[i] = fileList[i].GetDescription();
			}
			if (SkinManager.mInstance == null)
			{
				return;
			}
			if (fileList.Count == 0)
			{
				SkinManager.mInstance.progressController.EndProgress("Download Failed: no skins shared!");
			}
			else
			{
				SkinManager.mInstance.StartDownloadAllSharedSkins(array, array2, array3);
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000DB164 File Offset: 0x000D9564
		public void OnException(Exception e)
		{
			RioRyanDebug.Log("Exception : " + e);
			App42Exception ex = (App42Exception)e;
			int appErrorCode = ex.GetAppErrorCode();
			int httpErrorCode = ex.GetHttpErrorCode();
			if (SkinManager.mInstance == null)
			{
				return;
			}
			SkinManager.mInstance.GenSharedSkinsDownloadedEvent();
			if (appErrorCode == 2102)
			{
				SkinManager.mInstance.progressController.EndProgress("Download Failed: no skins shared!");
			}
			else if (appErrorCode == 1401)
			{
				SkinManager.mInstance.progressController.EndProgress("Download Failed: authorize failed!");
			}
			else if (appErrorCode == 1500)
			{
				SkinManager.mInstance.progressController.EndProgress("Download Failed: internal server error!");
			}
			else
			{
				SkinManager.mInstance.progressController.EndProgress("Download Failed: data processing error!");
			}
		}
	}

	// Token: 0x02000363 RID: 867
	public class GetAllSharedSkinsCountCallBack : App42CallBack
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x000DB23C File Offset: 0x000D963C
		public void OnSuccess(object response)
		{
			App42Response app42Response = (App42Response)response;
			SkinManager.mInstance.InitSharedSkinsPageInfo(app42Response.GetTotalRecords());
			SkinManager.mInstance.RequestSharedSkins();
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000DB26A File Offset: 0x000D966A
		public void OnException(Exception e)
		{
			RioRyanDebug.Log("Exception : " + e);
		}
	}

	// Token: 0x02000364 RID: 868
	public class SMSkinUploadCallBack : App42CallBack
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x000DB27C File Offset: 0x000D967C
		public SMSkinUploadCallBack(string skinName)
		{
			this._skinName = skinName;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000DB28C File Offset: 0x000D968C
		public void OnSuccess(object response)
		{
			Upload upload = (Upload)response;
			IList<Upload.File> fileList = upload.GetFileList();
			for (int i = 0; i < fileList.Count; i++)
			{
			}
			if (SkinManager.mInstance != null)
			{
				SkinManager.mInstance.progressController.Update(100);
				SkinManager.mInstance.progressController.EndProgress("Upload Successful!");
			}
			GrowthManagerKit.SetSkinSharedMark(this._skinName, true);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000DB300 File Offset: 0x000D9700
		public void OnException(Exception e)
		{
			RioRyanDebug.Log("Exception : " + e);
			if (SkinManager.mInstance != null)
			{
				SkinManager.mInstance.progressController.Update(100);
				SkinManager.mInstance.progressController.EndProgress("Upload Failed!");
			}
		}

		// Token: 0x04001D6A RID: 7530
		private string _skinName;
	}

	// Token: 0x02000365 RID: 869
	public class SMSkinRemoveCallBack : App42CallBack
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x000DB354 File Offset: 0x000D9754
		public SMSkinRemoveCallBack(string curSkinName, string curTexFileName, bool needUploadNewFile, string nameForSharing, string descriptionForSharing)
		{
			this._curSkinName = curSkinName;
			this._curTexFileName = curTexFileName;
			this._needUploadNewFile = needUploadNewFile;
			this._nameForSharing = nameForSharing;
			this._descriptionForSharing = descriptionForSharing;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000DB3A4 File Offset: 0x000D97A4
		public void OnSuccess(object response)
		{
			App42Response app42Response = (App42Response)response;
			if (this._needUploadNewFile && SkinManager.mInstance != null)
			{
				SkinManager.mInstance.progressController.Update(50);
				GGCloudServiceAdapter.mInstance.mUploadService.UploadFileForUser(UIUserDataController.GetDefaultRoleName() + "@Public@" + this._curTexFileName, UIUserDataController.GetDefaultUserName(), SkinIOController.GetCachedSkinFolderPath() + this._curTexFileName, UploadFileType.AUDIO, this._descriptionForSharing, new SkinManager.SMSkinUploadCallBack(this._curSkinName));
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000DB434 File Offset: 0x000D9834
		public void OnException(Exception e)
		{
			RioRyanDebug.Log("Exception : " + e);
			if (this._needUploadNewFile && SkinManager.mInstance != null)
			{
				SkinManager.mInstance.progressController.Update(50);
				GGCloudServiceAdapter.mInstance.mUploadService.UploadFileForUser(UIUserDataController.GetDefaultRoleName() + "@Public@" + this._curTexFileName, UIUserDataController.GetDefaultUserName(), SkinIOController.GetCachedSkinFolderPath() + this._curTexFileName, UploadFileType.AUDIO, this._descriptionForSharing, new SkinManager.SMSkinUploadCallBack(this._curSkinName));
			}
		}

		// Token: 0x04001D6B RID: 7531
		public string _curSkinName = string.Empty;

		// Token: 0x04001D6C RID: 7532
		public string _curTexFileName = string.Empty;

		// Token: 0x04001D6D RID: 7533
		public bool _needUploadNewFile;

		// Token: 0x04001D6E RID: 7534
		public string _nameForSharing;

		// Token: 0x04001D6F RID: 7535
		public string _descriptionForSharing;
	}
}
