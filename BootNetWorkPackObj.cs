using System;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class BootNetWorkPackObj
{
	// Token: 0x06001A65 RID: 6757 RVA: 0x000D413B File Offset: 0x000D253B
	public BootNetWorkPackObj(BootTypeDetail tDetail, Texture2D tTex, Material tMat, bool tIsNull)
	{
		this.isNull = tIsNull;
		this.detail = tDetail;
		this.tex = tTex;
		this.mat = tMat;
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x000D4160 File Offset: 0x000D2560
	public BootNetWorkPackObj(bool tIsNull)
	{
		this.isNull = tIsNull;
	}

	// Token: 0x04001C8F RID: 7311
	public BootTypeDetail detail;

	// Token: 0x04001C90 RID: 7312
	public Texture2D tex;

	// Token: 0x04001C91 RID: 7313
	public Material mat;

	// Token: 0x04001C92 RID: 7314
	public bool isNull;
}
