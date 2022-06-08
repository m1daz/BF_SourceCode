using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E7 RID: 999
public class SliderText : MonoBehaviour
{
	// Token: 0x06001E06 RID: 7686 RVA: 0x000E6E9B File Offset: 0x000E529B
	public void SetText(float value)
	{
		base.GetComponent<Text>().text = value.ToString("f2");
	}
}
