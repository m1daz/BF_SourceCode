using System;
using UnityEngine;

// Token: 0x0200053C RID: 1340
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x060025E7 RID: 9703 RVA: 0x00119878 File Offset: 0x00117C78
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = UnityEngine.Object.Instantiate<GameObject>(this.mPrefab, transform.position, transform.rotation);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04002682 RID: 9858
	public InvBaseItem.Slot slot;

	// Token: 0x04002683 RID: 9859
	private GameObject mPrefab;

	// Token: 0x04002684 RID: 9860
	private GameObject mChild;
}
