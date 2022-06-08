using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059E RID: 1438
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x06002838 RID: 10296 RVA: 0x00128914 File Offset: 0x00126D14
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.activate.Count == 0 && this.deactivate.Count == 0)
			{
				if (this.inverse)
				{
					this.deactivate.Add(this.target);
				}
				else
				{
					this.activate.Add(this.target);
				}
			}
			else
			{
				this.target = null;
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.Toggle));
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x001289B0 File Offset: 0x00126DB0
	public void Toggle()
	{
		bool value = UIToggle.current.value;
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.Set(this.activate[i], value);
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.Set(this.deactivate[j], !value);
			}
		}
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x00128A34 File Offset: 0x00126E34
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x0400290C RID: 10508
	public List<GameObject> activate;

	// Token: 0x0400290D RID: 10509
	public List<GameObject> deactivate;

	// Token: 0x0400290E RID: 10510
	[HideInInspector]
	[SerializeField]
	private GameObject target;

	// Token: 0x0400290F RID: 10511
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
