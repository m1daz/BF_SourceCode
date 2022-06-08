using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000539 RID: 1337
public abstract class UIItemSlot : MonoBehaviour
{
	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x060025D6 RID: 9686
	protected abstract InvGameItem observedItem { get; }

	// Token: 0x060025D7 RID: 9687
	protected abstract InvGameItem Replace(InvGameItem item);

	// Token: 0x060025D8 RID: 9688 RVA: 0x00119140 File Offset: 0x00117540
	private void OnTooltip(bool show)
	{
		InvGameItem invGameItem = (!show) ? null : this.mItem;
		if (invGameItem != null)
		{
			InvBaseItem baseItem = invGameItem.baseItem;
			if (baseItem != null)
			{
				string text = string.Concat(new string[]
				{
					"[",
					NGUIText.EncodeColor(invGameItem.color),
					"]",
					invGameItem.name,
					"[-]\n"
				});
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"[AFAFAF]Level ",
					invGameItem.itemLevel,
					" ",
					baseItem.slot
				});
				List<InvStat> list = invGameItem.CalculateStats();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					InvStat invStat = list[i];
					if (invStat.amount != 0)
					{
						if (invStat.amount < 0)
						{
							text = text + "\n[FF0000]" + invStat.amount;
						}
						else
						{
							text = text + "\n[00FF00]+" + invStat.amount;
						}
						if (invStat.modifier == InvStat.Modifier.Percent)
						{
							text += "%";
						}
						text = text + " " + invStat.id;
						text += "[-]";
					}
					i++;
				}
				if (!string.IsNullOrEmpty(baseItem.description))
				{
					text = text + "\n[FF9900]" + baseItem.description;
				}
				UITooltip.Show(text);
				return;
			}
		}
		UITooltip.Hide();
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x001192E0 File Offset: 0x001176E0
	private void OnClick()
	{
		if (UIItemSlot.mDraggedItem != null)
		{
			this.OnDrop(null);
		}
		else if (this.mItem != null)
		{
			UIItemSlot.mDraggedItem = this.Replace(null);
			if (UIItemSlot.mDraggedItem != null)
			{
				NGUITools.PlaySound(this.grabSound);
			}
			this.UpdateCursor();
		}
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x00119336 File Offset: 0x00117736
	private void OnDrag(Vector2 delta)
	{
		if (UIItemSlot.mDraggedItem == null && this.mItem != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UIItemSlot.mDraggedItem = this.Replace(null);
			NGUITools.PlaySound(this.grabSound);
			this.UpdateCursor();
		}
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x00119378 File Offset: 0x00117778
	private void OnDrop(GameObject go)
	{
		InvGameItem invGameItem = this.Replace(UIItemSlot.mDraggedItem);
		if (UIItemSlot.mDraggedItem == invGameItem)
		{
			NGUITools.PlaySound(this.errorSound);
		}
		else if (invGameItem != null)
		{
			NGUITools.PlaySound(this.grabSound);
		}
		else
		{
			NGUITools.PlaySound(this.placeSound);
		}
		UIItemSlot.mDraggedItem = invGameItem;
		this.UpdateCursor();
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x001193DC File Offset: 0x001177DC
	private void UpdateCursor()
	{
		if (UIItemSlot.mDraggedItem != null && UIItemSlot.mDraggedItem.baseItem != null)
		{
			UICursor.Set(UIItemSlot.mDraggedItem.baseItem.iconAtlas, UIItemSlot.mDraggedItem.baseItem.iconName);
		}
		else
		{
			UICursor.Clear();
		}
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x00119430 File Offset: 0x00117830
	private void Update()
	{
		InvGameItem observedItem = this.observedItem;
		if (this.mItem != observedItem)
		{
			this.mItem = observedItem;
			InvBaseItem invBaseItem = (observedItem == null) ? null : observedItem.baseItem;
			if (this.label != null)
			{
				string text = (observedItem == null) ? null : observedItem.name;
				if (string.IsNullOrEmpty(this.mText))
				{
					this.mText = this.label.text;
				}
				this.label.text = ((text == null) ? this.mText : text);
			}
			if (this.icon != null)
			{
				if (invBaseItem == null || invBaseItem.iconAtlas == null)
				{
					this.icon.enabled = false;
				}
				else
				{
					this.icon.atlas = invBaseItem.iconAtlas;
					this.icon.spriteName = invBaseItem.iconName;
					this.icon.enabled = true;
					this.icon.MakePixelPerfect();
				}
			}
			if (this.background != null)
			{
				this.background.color = ((observedItem == null) ? Color.white : observedItem.color);
			}
		}
	}

	// Token: 0x0400266F RID: 9839
	public UISprite icon;

	// Token: 0x04002670 RID: 9840
	public UIWidget background;

	// Token: 0x04002671 RID: 9841
	public UILabel label;

	// Token: 0x04002672 RID: 9842
	public AudioClip grabSound;

	// Token: 0x04002673 RID: 9843
	public AudioClip placeSound;

	// Token: 0x04002674 RID: 9844
	public AudioClip errorSound;

	// Token: 0x04002675 RID: 9845
	private InvGameItem mItem;

	// Token: 0x04002676 RID: 9846
	private string mText = string.Empty;

	// Token: 0x04002677 RID: 9847
	private static InvGameItem mDraggedItem;
}
