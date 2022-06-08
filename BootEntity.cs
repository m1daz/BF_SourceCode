using System;
using BootEditor;
using UnityEngine;

// Token: 0x02000339 RID: 825
[Serializable]
public class BootEntity
{
	// Token: 0x06001A62 RID: 6754 RVA: 0x000D40E0 File Offset: 0x000D24E0
	public BootEntity(string tName, Texture2D tTex, Material tMat, GBootItemInfo tInfo, BootBaseType tType, BootTypeDetail tDetail)
	{
		this.name = tName;
		this.tex = tTex;
		this.mat = tMat;
		this.info = tInfo;
		this.type = tType;
		this.detail = tDetail;
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x000D4115 File Offset: 0x000D2515
	public BootEntity()
	{
		this.detail = new BootTypeDetail();
		this.info = new GBootItemInfo();
	}

	// Token: 0x04001C86 RID: 7302
	public string name;

	// Token: 0x04001C87 RID: 7303
	public Texture2D tex;

	// Token: 0x04001C88 RID: 7304
	public Material mat;

	// Token: 0x04001C89 RID: 7305
	public GBootItemInfo info;

	// Token: 0x04001C8A RID: 7306
	public BootBaseType type;

	// Token: 0x04001C8B RID: 7307
	public BootTypeDetail detail;

	// Token: 0x04001C8C RID: 7308
	public bool isSetted;
}
