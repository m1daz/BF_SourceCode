using System;
using UnityEngine;

// Token: 0x02000555 RID: 1365
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x0600263B RID: 9787 RVA: 0x0011BA70 File Offset: 0x00119E70
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		foreach (UITweener uitweener in componentsInChildren)
		{
			uitweener.duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
