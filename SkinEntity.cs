using System;
using SkinEditor;
using UnityEngine;

// Token: 0x0200035D RID: 861
[Serializable]
public class SkinEntity
{
	// Token: 0x06001B01 RID: 6913 RVA: 0x000DA0B2 File Offset: 0x000D84B2
	public SkinEntity(string tName, Texture2D tTex, Material tMat, GSkinItemInfo tInfo, SkinBaseType tType)
	{
		this.name = tName;
		this.tex = tTex;
		this.mat = tMat;
		this.info = tInfo;
		this.type = tType;
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x000DA0DF File Offset: 0x000D84DF
	public SkinEntity()
	{
		this.info = new GSkinItemInfo();
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x000DA0F2 File Offset: 0x000D84F2
	public void SetAppendInfoForSharing(string name, string description)
	{
		this.nameForSharing = name;
		this.descriptionForSharing = description;
	}

	// Token: 0x04001D48 RID: 7496
	public string name;

	// Token: 0x04001D49 RID: 7497
	public Texture2D tex;

	// Token: 0x04001D4A RID: 7498
	public Material mat;

	// Token: 0x04001D4B RID: 7499
	public GSkinItemInfo info;

	// Token: 0x04001D4C RID: 7500
	public SkinBaseType type;

	// Token: 0x04001D4D RID: 7501
	public bool isSetted;

	// Token: 0x04001D4E RID: 7502
	public string nameForSharing;

	// Token: 0x04001D4F RID: 7503
	public string descriptionForSharing;

	// Token: 0x04001D50 RID: 7504
	public bool hasBeenShared;

	// Token: 0x04001D51 RID: 7505
	public bool hasBeenDownloaded;
}
