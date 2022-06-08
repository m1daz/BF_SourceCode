using System;
using HatEditor;
using UnityEngine;

// Token: 0x02000349 RID: 841
[Serializable]
public class HatEntity
{
	// Token: 0x06001AA0 RID: 6816 RVA: 0x000D6288 File Offset: 0x000D4688
	public HatEntity(string tName, Texture2D tTex, Material tMat, GHatItemInfo tInfo, HatBaseType tType, HatTypeDetail tDetail)
	{
		this.name = tName;
		this.tex = tTex;
		this.mat = tMat;
		this.info = tInfo;
		this.type = tType;
		this.detail = tDetail;
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000D62BD File Offset: 0x000D46BD
	public HatEntity()
	{
		this.detail = new HatTypeDetail();
		this.info = new GHatItemInfo();
	}

	// Token: 0x04001CC2 RID: 7362
	public string name;

	// Token: 0x04001CC3 RID: 7363
	public Texture2D tex;

	// Token: 0x04001CC4 RID: 7364
	public Material mat;

	// Token: 0x04001CC5 RID: 7365
	public GHatItemInfo info;

	// Token: 0x04001CC6 RID: 7366
	public HatBaseType type;

	// Token: 0x04001CC7 RID: 7367
	public HatTypeDetail detail;

	// Token: 0x04001CC8 RID: 7368
	public bool isSetted;
}
