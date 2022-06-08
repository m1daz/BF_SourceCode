using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059D RID: 1437
[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x06002835 RID: 10293 RVA: 0x001287E0 File Offset: 0x00126BE0
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

	// Token: 0x06002836 RID: 10294 RVA: 0x0012887C File Offset: 0x00126C7C
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				MonoBehaviour monoBehaviour = this.activate[i];
				monoBehaviour.enabled = UIToggle.current.value;
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				MonoBehaviour monoBehaviour2 = this.deactivate[j];
				monoBehaviour2.enabled = !UIToggle.current.value;
			}
		}
	}

	// Token: 0x04002908 RID: 10504
	public List<MonoBehaviour> activate;

	// Token: 0x04002909 RID: 10505
	public List<MonoBehaviour> deactivate;

	// Token: 0x0400290A RID: 10506
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	// Token: 0x0400290B RID: 10507
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
