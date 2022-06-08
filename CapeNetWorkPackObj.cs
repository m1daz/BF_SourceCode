using System;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class CapeNetWorkPackObj
{
	// Token: 0x06001A84 RID: 6788 RVA: 0x000D520F File Offset: 0x000D360F
	public CapeNetWorkPackObj(CapeTypeDetail tDetail, Texture2D tTex, Material tMat, bool tIsNull)
	{
		this.isNull = tIsNull;
		this.detail = tDetail;
		this.tex = tTex;
		this.mat = tMat;
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000D5234 File Offset: 0x000D3634
	public CapeNetWorkPackObj(bool tIsNull)
	{
		this.isNull = tIsNull;
	}

	// Token: 0x04001CAD RID: 7341
	public CapeTypeDetail detail;

	// Token: 0x04001CAE RID: 7342
	public Texture2D tex;

	// Token: 0x04001CAF RID: 7343
	public Material mat;

	// Token: 0x04001CB0 RID: 7344
	public bool isNull;
}
