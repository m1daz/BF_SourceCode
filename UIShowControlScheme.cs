using System;
using UnityEngine;

// Token: 0x02000593 RID: 1427
public class UIShowControlScheme : MonoBehaviour
{
	// Token: 0x06002802 RID: 10242 RVA: 0x001277A9 File Offset: 0x00125BA9
	private void OnEnable()
	{
		UICamera.onSchemeChange = (UICamera.OnSchemeChange)Delegate.Combine(UICamera.onSchemeChange, new UICamera.OnSchemeChange(this.OnScheme));
		this.OnScheme();
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x001277D1 File Offset: 0x00125BD1
	private void OnDisable()
	{
		UICamera.onSchemeChange = (UICamera.OnSchemeChange)Delegate.Remove(UICamera.onSchemeChange, new UICamera.OnSchemeChange(this.OnScheme));
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x001277F4 File Offset: 0x00125BF4
	private void OnScheme()
	{
		if (this.target != null)
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			if (currentScheme == UICamera.ControlScheme.Mouse)
			{
				this.target.SetActive(this.mouse);
			}
			else if (currentScheme == UICamera.ControlScheme.Touch)
			{
				this.target.SetActive(this.touch);
			}
			else if (currentScheme == UICamera.ControlScheme.Controller)
			{
				this.target.SetActive(this.controller);
			}
		}
	}

	// Token: 0x040028CE RID: 10446
	public GameObject target;

	// Token: 0x040028CF RID: 10447
	public bool mouse;

	// Token: 0x040028D0 RID: 10448
	public bool touch;

	// Token: 0x040028D1 RID: 10449
	public bool controller = true;
}
