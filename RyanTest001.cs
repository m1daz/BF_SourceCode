using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class RyanTest001 : MonoBehaviour
{
	// Token: 0x06000604 RID: 1540 RVA: 0x000379D8 File Offset: 0x00035DD8
	private void Start()
	{
		this.tex = Resources.Load<Texture2D>("UI/Images/UINewStoreLogo/Range_CardLogo_3");
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x000379EA File Offset: 0x00035DEA
	private void Update()
	{
	}

	// Token: 0x040004D6 RID: 1238
	public Texture2D tex;
}
