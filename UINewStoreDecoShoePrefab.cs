using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class UINewStoreDecoShoePrefab : MonoBehaviour
{
	// Token: 0x06001721 RID: 5921 RVA: 0x000C4190 File Offset: 0x000C2590
	private void Start()
	{
		this.shoeModel = base.transform.Find("shoeModel").gameObject;
		this.shoePrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreDecoShoePrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000C41F0 File Offset: 0x000C25F0
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.shoeModel.activeSelf)
			{
				this.shoeModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.shoeModel.activeSelf)
			{
				this.shoeModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.shoePrefabInstiate.curSelectedBootIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.shoePrefabInstiate.curSelectedBootIndex = this.index;
		}
	}

	// Token: 0x04001A05 RID: 6661
	private UIScrollView mUIScrollView;

	// Token: 0x04001A06 RID: 6662
	private GameObject shoeModel;

	// Token: 0x04001A07 RID: 6663
	public int index;

	// Token: 0x04001A08 RID: 6664
	private UINewStoreDecoShoePrefabInstiate shoePrefabInstiate;

	// Token: 0x04001A09 RID: 6665
	private float selecttime;
}
