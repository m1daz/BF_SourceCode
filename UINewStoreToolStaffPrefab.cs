using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class UINewStoreToolStaffPrefab : MonoBehaviour
{
	// Token: 0x060017B6 RID: 6070 RVA: 0x000C80D0 File Offset: 0x000C64D0
	private void Start()
	{
		this.StaffModel = base.transform.Find("StaffModel").gameObject;
		this.StaffPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreToolStaffPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x000C8130 File Offset: 0x000C6530
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.StaffModel.activeSelf)
			{
				this.StaffModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1.4f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.4f, 1f);
			if (!this.StaffModel.activeSelf)
			{
				this.StaffModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.StaffPrefabInstiate.curSelectedStaffIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.StaffPrefabInstiate.curSelectedStaffIndex = this.index;
		}
	}

	// Token: 0x04001ADD RID: 6877
	private UIScrollView mUIScrollView;

	// Token: 0x04001ADE RID: 6878
	private GameObject StaffModel;

	// Token: 0x04001ADF RID: 6879
	public int index;

	// Token: 0x04001AE0 RID: 6880
	private UINewStoreToolStaffPrefabInstiate StaffPrefabInstiate;

	// Token: 0x04001AE1 RID: 6881
	private float selecttime;
}
