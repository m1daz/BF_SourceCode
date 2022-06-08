using System;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class UINewStoreDecoCapePrefab : MonoBehaviour
{
	// Token: 0x06001703 RID: 5891 RVA: 0x000C3138 File Offset: 0x000C1538
	private void Start()
	{
		this.capeModel = base.transform.Find("capeModel").gameObject;
		this.CapePrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreDecoCapePrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x000C3198 File Offset: 0x000C1598
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.capeModel.activeSelf)
			{
				this.capeModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.capeModel.activeSelf)
			{
				this.capeModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.CapePrefabInstiate.curSelectedCapeIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.CapePrefabInstiate.curSelectedCapeIndex = this.index;
		}
	}

	// Token: 0x040019CF RID: 6607
	private UIScrollView mUIScrollView;

	// Token: 0x040019D0 RID: 6608
	private GameObject capeModel;

	// Token: 0x040019D1 RID: 6609
	public int index;

	// Token: 0x040019D2 RID: 6610
	private UINewStoreDecoCapePrefabInstiate CapePrefabInstiate;

	// Token: 0x040019D3 RID: 6611
	private float selecttime;
}
