using System;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class UINewStoreArmorPrefab : MonoBehaviour
{
	// Token: 0x060016D6 RID: 5846 RVA: 0x000C200E File Offset: 0x000C040E
	private void Start()
	{
		this.mArmorWindowDirector = base.transform.root.Find("ArmorWindow").GetComponent<UINewStoreArmorWindowDirector>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000C204C File Offset: 0x000C044C
	private void Update()
	{
		if (base.transform.position.x >= -0.9f && base.transform.position.x <= 0.5f)
		{
			base.transform.localScale = new Vector3(1.2f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.2f, 1.2f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.2f, 1f);
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.mArmorWindowDirector.curSelectedArmorIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.mArmorWindowDirector.curSelectedArmorIndex = this.index;
		}
	}

	// Token: 0x0400199A RID: 6554
	private UIScrollView mUIScrollView;

	// Token: 0x0400199B RID: 6555
	private float selecttime;

	// Token: 0x0400199C RID: 6556
	private UINewStoreArmorWindowDirector mArmorWindowDirector;

	// Token: 0x0400199D RID: 6557
	public int index = -1;
}
