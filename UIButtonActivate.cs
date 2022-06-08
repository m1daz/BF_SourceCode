using System;
using UnityEngine;

// Token: 0x0200055D RID: 1373
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x06002664 RID: 9828 RVA: 0x0011D139 File Offset: 0x0011B539
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04002726 RID: 10022
	public GameObject target;

	// Token: 0x04002727 RID: 10023
	public bool state = true;
}
