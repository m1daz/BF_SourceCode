using System;
using UnityEngine;

// Token: 0x02000538 RID: 1336
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00119578 File Offset: 0x00117978
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.equipment != null)) ? null : this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x001195A2 File Offset: 0x001179A2
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.equipment != null)) ? item : this.equipment.Replace(this.slot, item);
	}

	// Token: 0x0400266D RID: 9837
	public InvEquipment equipment;

	// Token: 0x0400266E RID: 9838
	public InvBaseItem.Slot slot;
}
