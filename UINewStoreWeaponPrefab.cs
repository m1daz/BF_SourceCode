using System;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class UINewStoreWeaponPrefab : MonoBehaviour
{
	// Token: 0x060017E3 RID: 6115 RVA: 0x000C907C File Offset: 0x000C747C
	private void Start()
	{
		this.weaponModel = base.transform.Find("Texture(Weapon)").gameObject;
		this.WeaponPrefabInstiate = base.transform.parent.parent.GetComponent<UINewStoreWeaponPrefabInstiate>();
		this.mUIScrollView = base.transform.parent.parent.GetComponent<UIScrollView>();
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x000C90DC File Offset: 0x000C74DC
	private void Update()
	{
		if (base.transform.position.x < -0.9f || base.transform.position.x > 0.5f)
		{
			if (this.weaponModel.activeSelf)
			{
				this.weaponModel.SetActive(false);
			}
		}
		else
		{
			base.transform.localScale = new Vector3(1.5f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.8f, 1.5f - Mathf.Abs(base.transform.position.x + 0.2f) * 1.8f, 1f);
			if (!this.weaponModel.activeSelf)
			{
				this.weaponModel.SetActive(true);
			}
		}
		this.selecttime += Time.deltaTime;
		if (this.mUIScrollView.isDragging)
		{
			if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
			{
				this.WeaponPrefabInstiate.curSelectedWeaponIndex = this.index;
			}
		}
		else if (base.transform.position.x > -0.3f && base.transform.position.x < 0f)
		{
			this.WeaponPrefabInstiate.curSelectedWeaponIndex = this.index;
		}
	}

	// Token: 0x04001B29 RID: 6953
	private UIScrollView mUIScrollView;

	// Token: 0x04001B2A RID: 6954
	private GameObject weaponModel;

	// Token: 0x04001B2B RID: 6955
	private float selecttime;

	// Token: 0x04001B2C RID: 6956
	public int index;

	// Token: 0x04001B2D RID: 6957
	private UINewStoreWeaponPrefabInstiate WeaponPrefabInstiate;
}
