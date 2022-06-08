using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class UINewStoreToolKeyPrefab : MonoBehaviour
{
	// Token: 0x060017A0 RID: 6048 RVA: 0x000C755C File Offset: 0x000C595C
	private void Start()
	{
		this.keyModel = base.transform.Find("keyModel").gameObject;
		this.keyPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreToolKeyPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x000C75BC File Offset: 0x000C59BC
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.keyModel.activeSelf)
			{
				this.keyModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.keyModel.activeSelf)
			{
				this.keyModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.keyPrefabInstiate.curSelectedKeyIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.keyPrefabInstiate.curSelectedKeyIndex = this.index;
		}
	}

	// Token: 0x04001AA9 RID: 6825
	private UIScrollView mUIScrollView;

	// Token: 0x04001AAA RID: 6826
	private GameObject keyModel;

	// Token: 0x04001AAB RID: 6827
	public int index;

	// Token: 0x04001AAC RID: 6828
	private UINewStoreToolKeyPrefabInstiate keyPrefabInstiate;

	// Token: 0x04001AAD RID: 6829
	private float selecttime;
}
