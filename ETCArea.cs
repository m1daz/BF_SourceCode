using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F5 RID: 1013
[Serializable]
public class ETCArea : MonoBehaviour
{
	// Token: 0x06001E5F RID: 7775 RVA: 0x000E8292 File Offset: 0x000E6692
	public ETCArea()
	{
		this.show = true;
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x000E82A1 File Offset: 0x000E66A1
	public void Awake()
	{
		base.GetComponent<Image>().enabled = this.show;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x000E82B4 File Offset: 0x000E66B4
	public void ApplyPreset(ETCArea.AreaPreset preset)
	{
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		switch (preset)
		{
		case ETCArea.AreaPreset.TopLeft:
			this.rectTransform().anchoredPosition = new Vector2(-component.rect.width / 4f, component.rect.height / 4f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, component.rect.width / 2f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.rect.height / 2f);
			this.rectTransform().anchorMin = new Vector2(0f, 1f);
			this.rectTransform().anchorMax = new Vector2(0f, 1f);
			this.rectTransform().anchoredPosition = new Vector2(this.rectTransform().sizeDelta.x / 2f, -this.rectTransform().sizeDelta.y / 2f);
			break;
		case ETCArea.AreaPreset.TopRight:
			this.rectTransform().anchoredPosition = new Vector2(component.rect.width / 4f, component.rect.height / 4f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, component.rect.width / 2f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.rect.height / 2f);
			this.rectTransform().anchorMin = new Vector2(1f, 1f);
			this.rectTransform().anchorMax = new Vector2(1f, 1f);
			this.rectTransform().anchoredPosition = new Vector2(-this.rectTransform().sizeDelta.x / 2f, -this.rectTransform().sizeDelta.y / 2f);
			break;
		case ETCArea.AreaPreset.BottomLeft:
			this.rectTransform().anchoredPosition = new Vector2(-component.rect.width / 4f, -component.rect.height / 4f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, component.rect.width / 2f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.rect.height / 2f);
			this.rectTransform().anchorMin = new Vector2(0f, 0f);
			this.rectTransform().anchorMax = new Vector2(0f, 0f);
			this.rectTransform().anchoredPosition = new Vector2(this.rectTransform().sizeDelta.x / 2f, this.rectTransform().sizeDelta.y / 2f);
			break;
		case ETCArea.AreaPreset.BottomRight:
			this.rectTransform().anchoredPosition = new Vector2(component.rect.width / 4f, -component.rect.height / 4f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, component.rect.width / 2f);
			this.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.rect.height / 2f);
			this.rectTransform().anchorMin = new Vector2(1f, 0f);
			this.rectTransform().anchorMax = new Vector2(1f, 0f);
			this.rectTransform().anchoredPosition = new Vector2(-this.rectTransform().sizeDelta.x / 2f, this.rectTransform().sizeDelta.y / 2f);
			break;
		}
	}

	// Token: 0x04001F64 RID: 8036
	public bool show;

	// Token: 0x020003F6 RID: 1014
	public enum AreaPreset
	{
		// Token: 0x04001F66 RID: 8038
		Choose,
		// Token: 0x04001F67 RID: 8039
		TopLeft,
		// Token: 0x04001F68 RID: 8040
		TopRight,
		// Token: 0x04001F69 RID: 8041
		BottomLeft,
		// Token: 0x04001F6A RID: 8042
		BottomRight
	}
}
