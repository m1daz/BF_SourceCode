using System;
using UnityEngine;

// Token: 0x0200053B RID: 1339
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x060025E4 RID: 9700 RVA: 0x0011981B File Offset: 0x00117C1B
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.storage != null)) ? null : this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x00119845 File Offset: 0x00117C45
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.storage != null)) ? item : this.storage.Replace(this.slot, item);
	}

	// Token: 0x04002680 RID: 9856
	public UIItemStorage storage;

	// Token: 0x04002681 RID: 9857
	public int slot;
}
