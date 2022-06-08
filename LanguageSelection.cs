using System;
using UnityEngine;

// Token: 0x02000559 RID: 1369
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06002647 RID: 9799 RVA: 0x0011BDF9 File Offset: 0x0011A1F9
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x06002648 RID: 9800 RVA: 0x0011BE07 File Offset: 0x0011A207
	private void Start()
	{
		this.mStarted = true;
		this.Refresh();
		EventDelegate.Add(this.mList.onChange, delegate()
		{
			Localization.language = UIPopupList.current.value;
		});
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x0011BE44 File Offset: 0x0011A244
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Refresh();
		}
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x0011BE58 File Offset: 0x0011A258
	public void Refresh()
	{
		if (this.mList != null && Localization.knownLanguages != null)
		{
			this.mList.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x0011BECC File Offset: 0x0011A2CC
	private void OnLocalize()
	{
		this.Refresh();
	}

	// Token: 0x04002703 RID: 9987
	private UIPopupList mList;

	// Token: 0x04002704 RID: 9988
	private bool mStarted;
}
