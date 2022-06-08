using System;
using UnityEngine;

// Token: 0x0200056D RID: 1389
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x060026C8 RID: 9928 RVA: 0x0011EB8C File Offset: 0x0011CF8C
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x0011EB99 File Offset: 0x0011CF99
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x0400277A RID: 10106
	public static Transform root;
}
