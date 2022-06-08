using System;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class IphoneXUIAdapt : MonoBehaviour
{
	// Token: 0x0600124C RID: 4684 RVA: 0x000A46E0 File Offset: 0x000A2AE0
	private void Awake()
	{
		if (Screen.width == 2436)
		{
			this.isNeedAdapt = true;
		}
		this.a = base.GetComponentsInChildren<UIAnchor>();
		this.w = base.GetComponent<UIWidget>();
		if (this.isNeedAdapt)
		{
			this.w.leftAnchor.Set(0f, 66f);
			this.w.rightAnchor.Set(1f, -66f);
			this.w.topAnchor.Set(1f, 0f);
			this.w.bottomAnchor.Set(0f, 32f);
		}
		this.w.ResetAnchors();
		this.w.UpdateAnchors();
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x000A47A4 File Offset: 0x000A2BA4
	private void Start()
	{
		foreach (UIAnchor uianchor in this.a)
		{
			if (uianchor.container != null && uianchor.container == base.gameObject)
			{
				uianchor.Update();
			}
		}
	}

	// Token: 0x04001513 RID: 5395
	private UIAnchor[] a;

	// Token: 0x04001514 RID: 5396
	private UIWidget w;

	// Token: 0x04001515 RID: 5397
	private bool isNeedAdapt;
}
