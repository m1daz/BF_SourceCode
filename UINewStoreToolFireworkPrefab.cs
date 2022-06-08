using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class UINewStoreToolFireworkPrefab : MonoBehaviour
{
	// Token: 0x06001796 RID: 6038 RVA: 0x000C7018 File Offset: 0x000C5418
	private void Start()
	{
		this.fireworkModel = base.transform.Find("fireworkModel").gameObject;
		this.fireworkPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreToolFireworkPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x000C7078 File Offset: 0x000C5478
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.fireworkModel.activeSelf)
			{
				this.fireworkModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.fireworkModel.activeSelf)
			{
				this.fireworkModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.fireworkPrefabInstiate.curSelectedFireworkIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.fireworkPrefabInstiate.curSelectedFireworkIndex = this.index;
		}
	}

	// Token: 0x04001A94 RID: 6804
	private UIScrollView mUIScrollView;

	// Token: 0x04001A95 RID: 6805
	private GameObject fireworkModel;

	// Token: 0x04001A96 RID: 6806
	public int index;

	// Token: 0x04001A97 RID: 6807
	private UINewStoreToolFireworkPrefabInstiate fireworkPrefabInstiate;

	// Token: 0x04001A98 RID: 6808
	private float selecttime;
}
