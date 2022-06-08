using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class UINewStoreDecoSkinSharePrefab : MonoBehaviour
{
	// Token: 0x06001744 RID: 5956 RVA: 0x000C5480 File Offset: 0x000C3880
	private void Start()
	{
		this.skinShareModel = base.transform.Find("skinShareModel").gameObject;
		this.SkinSharePrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreDecoSkinSharePrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000C54E0 File Offset: 0x000C38E0
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.skinShareModel.activeSelf)
			{
				this.skinShareModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.skinShareModel.activeSelf)
			{
				this.skinShareModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (this.selecttime > 0.5f)
			{
				if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
				{
					this.SkinSharePrefabInstiate.curSelectedSkinShareIndex = this.index;
				}
				this.selecttime = 0f;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.SkinSharePrefabInstiate.curSelectedSkinShareIndex = this.index;
		}
	}

	// Token: 0x04001A3D RID: 6717
	private UIScrollView mUIScrollView;

	// Token: 0x04001A3E RID: 6718
	private GameObject skinShareModel;

	// Token: 0x04001A3F RID: 6719
	public int index;

	// Token: 0x04001A40 RID: 6720
	private UINewStoreDecoSkinSharePrefabInstiate SkinSharePrefabInstiate;

	// Token: 0x04001A41 RID: 6721
	private float selecttime;
}
