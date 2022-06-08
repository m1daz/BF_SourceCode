using System;
using UnityEngine;

// Token: 0x0200056A RID: 1386
[AddComponentMenu("NGUI/Interaction/Drag and Drop Container")]
public class UIDragDropContainer : MonoBehaviour
{
	// Token: 0x060026B1 RID: 9905 RVA: 0x0011EB65 File Offset: 0x0011CF65
	protected virtual void Start()
	{
		if (this.reparentTarget == null)
		{
			this.reparentTarget = base.transform;
		}
	}

	// Token: 0x04002762 RID: 10082
	public Transform reparentTarget;
}
