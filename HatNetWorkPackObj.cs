using System;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class HatNetWorkPackObj
{
	// Token: 0x06001AA3 RID: 6819 RVA: 0x000D62E3 File Offset: 0x000D46E3
	public HatNetWorkPackObj(HatTypeDetail tDetail, Texture2D tTex, Material tMat, bool tIsNull)
	{
		this.isNull = tIsNull;
		this.detail = tDetail;
		this.tex = tTex;
		this.mat = tMat;
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000D6308 File Offset: 0x000D4708
	public HatNetWorkPackObj(bool tIsNull)
	{
		this.isNull = tIsNull;
	}

	// Token: 0x04001CCB RID: 7371
	public HatTypeDetail detail;

	// Token: 0x04001CCC RID: 7372
	public Texture2D tex;

	// Token: 0x04001CCD RID: 7373
	public Material mat;

	// Token: 0x04001CCE RID: 7374
	public bool isNull;
}
