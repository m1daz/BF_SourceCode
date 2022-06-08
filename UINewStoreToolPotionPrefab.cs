using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class UINewStoreToolPotionPrefab : MonoBehaviour
{
	// Token: 0x060017AC RID: 6060 RVA: 0x000C7B00 File Offset: 0x000C5F00
	private void Start()
	{
		this.potionModel = base.transform.Find("potionModel").gameObject;
		this.potionPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreToolPotionPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x000C7B60 File Offset: 0x000C5F60
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.potionModel.activeSelf)
			{
				this.potionModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.potionModel.activeSelf)
			{
				this.potionModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.potionPrefabInstiate.curSelectedPotionIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.potionPrefabInstiate.curSelectedPotionIndex = this.index;
		}
	}

	// Token: 0x04001AC3 RID: 6851
	private UIScrollView mUIScrollView;

	// Token: 0x04001AC4 RID: 6852
	private GameObject potionModel;

	// Token: 0x04001AC5 RID: 6853
	public int index;

	// Token: 0x04001AC6 RID: 6854
	private UINewStoreToolPotionPrefabInstiate potionPrefabInstiate;

	// Token: 0x04001AC7 RID: 6855
	private float selecttime;
}
