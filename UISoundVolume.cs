using System;
using UnityEngine;

// Token: 0x02000596 RID: 1430
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x06002813 RID: 10259 RVA: 0x00127874 File Offset: 0x00125C74
	private void Awake()
	{
		UISlider component = base.GetComponent<UISlider>();
		component.value = NGUITools.soundVolume;
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06002814 RID: 10260 RVA: 0x001278AB File Offset: 0x00125CAB
	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}
}
