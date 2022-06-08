using System;
using UnityEngine;

// Token: 0x02000579 RID: 1401
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x0600271D RID: 10013 RVA: 0x00120BB4 File Offset: 0x0011EFB4
	// (set) Token: 0x0600271E RID: 10014 RVA: 0x00120BE4 File Offset: 0x0011EFE4
	public bool isEnabled
	{
		get
		{
			Collider component = base.gameObject.GetComponent<Collider>();
			return component && component.enabled;
		}
		set
		{
			Collider component = base.gameObject.GetComponent<Collider>();
			if (!component)
			{
				return;
			}
			if (component.enabled != value)
			{
				component.enabled = value;
				this.UpdateImage();
			}
		}
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x00120C22 File Offset: 0x0011F022
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x00120C48 File Offset: 0x0011F048
	private void OnValidate()
	{
		if (this.target != null)
		{
			if (string.IsNullOrEmpty(this.normalSprite))
			{
				this.normalSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.hoverSprite))
			{
				this.hoverSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.pressedSprite))
			{
				this.pressedSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.disabledSprite))
			{
				this.disabledSprite = this.target.spriteName;
			}
		}
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x00120CEC File Offset: 0x0011F0EC
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.SetSprite((!UICamera.IsHighlighted(base.gameObject)) ? this.normalSprite : this.hoverSprite);
			}
			else
			{
				this.SetSprite(this.disabledSprite);
			}
		}
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x00120D4D File Offset: 0x0011F14D
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.SetSprite((!isOver) ? this.normalSprite : this.hoverSprite);
		}
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x00120D88 File Offset: 0x0011F188
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.SetSprite(this.pressedSprite);
		}
		else
		{
			this.UpdateImage();
		}
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x00120DA8 File Offset: 0x0011F1A8
	private void SetSprite(string sprite)
	{
		if (this.target.atlas == null || this.target.atlas.GetSprite(sprite) == null)
		{
			return;
		}
		this.target.spriteName = sprite;
		if (this.pixelSnap)
		{
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040027E7 RID: 10215
	public UISprite target;

	// Token: 0x040027E8 RID: 10216
	public string normalSprite;

	// Token: 0x040027E9 RID: 10217
	public string hoverSprite;

	// Token: 0x040027EA RID: 10218
	public string pressedSprite;

	// Token: 0x040027EB RID: 10219
	public string disabledSprite;

	// Token: 0x040027EC RID: 10220
	public bool pixelSnap = true;
}
