using System;
using CapeEditor;
using UnityEngine;

// Token: 0x02000341 RID: 833
[Serializable]
public class CapeEntity
{
	// Token: 0x06001A81 RID: 6785 RVA: 0x000D51B4 File Offset: 0x000D35B4
	public CapeEntity(string tName, Texture2D tTex, Material tMat, GCapeItemInfo tInfo, CapeBaseType tType, CapeTypeDetail tDetail)
	{
		this.name = tName;
		this.tex = tTex;
		this.mat = tMat;
		this.info = tInfo;
		this.type = tType;
		this.detail = tDetail;
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x000D51E9 File Offset: 0x000D35E9
	public CapeEntity()
	{
		this.detail = new CapeTypeDetail();
		this.info = new GCapeItemInfo();
	}

	// Token: 0x04001CA4 RID: 7332
	public string name;

	// Token: 0x04001CA5 RID: 7333
	public Texture2D tex;

	// Token: 0x04001CA6 RID: 7334
	public Material mat;

	// Token: 0x04001CA7 RID: 7335
	public GCapeItemInfo info;

	// Token: 0x04001CA8 RID: 7336
	public CapeBaseType type;

	// Token: 0x04001CA9 RID: 7337
	public CapeTypeDetail detail;

	// Token: 0x04001CAA RID: 7338
	public bool isSetted;
}
