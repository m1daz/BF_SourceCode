using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class UINewStoreDecoHatPrefab : MonoBehaviour
{
	// Token: 0x06001712 RID: 5906 RVA: 0x000C3964 File Offset: 0x000C1D64
	private void Start()
	{
		this.hatModel = base.transform.Find("hatModel").gameObject;
		this.HatPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreDecoHatPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x000C39C4 File Offset: 0x000C1DC4
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.hatModel.activeSelf)
			{
				this.hatModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.hatModel.activeSelf)
			{
				this.hatModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.HatPrefabInstiate.curSelectedHatIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.HatPrefabInstiate.curSelectedHatIndex = this.index;
		}
	}

	// Token: 0x040019EA RID: 6634
	private UIScrollView mUIScrollView;

	// Token: 0x040019EB RID: 6635
	private GameObject hatModel;

	// Token: 0x040019EC RID: 6636
	public int index;

	// Token: 0x040019ED RID: 6637
	private UINewStoreDecoHatPrefabInstiate HatPrefabInstiate;

	// Token: 0x040019EE RID: 6638
	private float selecttime;
}
