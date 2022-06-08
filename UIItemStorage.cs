using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200053A RID: 1338
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x060025DF RID: 9695 RVA: 0x00119608 File Offset: 0x00117A08
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x00119637 File Offset: 0x00117A37
	public InvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x0011965C File Offset: 0x00117A5C
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x00119694 File Offset: 0x00117A94
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = base.gameObject.AddChild(this.template);
					Transform transform = gameObject.transform;
					transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x04002678 RID: 9848
	public int maxItemCount = 8;

	// Token: 0x04002679 RID: 9849
	public int maxRows = 4;

	// Token: 0x0400267A RID: 9850
	public int maxColumns = 4;

	// Token: 0x0400267B RID: 9851
	public GameObject template;

	// Token: 0x0400267C RID: 9852
	public UIWidget background;

	// Token: 0x0400267D RID: 9853
	public int spacing = 128;

	// Token: 0x0400267E RID: 9854
	public int padding = 10;

	// Token: 0x0400267F RID: 9855
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
