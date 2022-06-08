using System;
using UnityEngine;

// Token: 0x0200054F RID: 1359
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x06002629 RID: 9769 RVA: 0x0011B46C File Offset: 0x0011986C
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.lastWorldPosition);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
