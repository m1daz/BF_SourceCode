using System;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class UINewStoreDecoSkinPrefab : MonoBehaviour
{
	// Token: 0x06001730 RID: 5936 RVA: 0x000C49BC File Offset: 0x000C2DBC
	private void Start()
	{
		this.skinModel = base.transform.Find("skinModel").gameObject;
		this.SkinPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreDecoSkinPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000C4A1C File Offset: 0x000C2E1C
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.skinModel.activeSelf)
			{
				this.skinModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.skinModel.activeSelf)
			{
				this.skinModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.SkinPrefabInstiate.curSelectedSkinIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.SkinPrefabInstiate.curSelectedSkinIndex = this.index;
		}
	}

	// Token: 0x04001A1E RID: 6686
	private UIScrollView mUIScrollView;

	// Token: 0x04001A1F RID: 6687
	private GameObject skinModel;

	// Token: 0x04001A20 RID: 6688
	public int index;

	// Token: 0x04001A21 RID: 6689
	private UINewStoreDecoSkinPrefabInstiate SkinPrefabInstiate;

	// Token: 0x04001A22 RID: 6690
	private float selecttime;
}
