using System;
using UnityEngine;

// Token: 0x02000548 RID: 1352
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Interaction/Envelop Content")]
public class EnvelopContent : MonoBehaviour
{
	// Token: 0x06002610 RID: 9744 RVA: 0x0011A69A File Offset: 0x00118A9A
	private void Start()
	{
		this.mStarted = true;
		this.Execute();
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x0011A6A9 File Offset: 0x00118AA9
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Execute();
		}
	}

	// Token: 0x06002612 RID: 9746 RVA: 0x0011A6BC File Offset: 0x00118ABC
	[ContextMenu("Execute")]
	public void Execute()
	{
		if (this.targetRoot == base.transform)
		{
			Debug.LogError("Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", this);
		}
		else if (NGUITools.IsChild(this.targetRoot, base.transform))
		{
			Debug.LogError("Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", this);
		}
		else
		{
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.transform.parent, this.targetRoot, !this.ignoreDisabled, true);
			float num = bounds.min.x + (float)this.padLeft;
			float num2 = bounds.min.y + (float)this.padBottom;
			float num3 = bounds.max.x + (float)this.padRight;
			float num4 = bounds.max.y + (float)this.padTop;
			UIWidget component = base.GetComponent<UIWidget>();
			component.SetRect(num, num2, num3 - num, num4 - num2);
			base.BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
			NGUITools.UpdateWidgetCollider(base.gameObject);
		}
	}

	// Token: 0x040026CB RID: 9931
	public Transform targetRoot;

	// Token: 0x040026CC RID: 9932
	public int padLeft;

	// Token: 0x040026CD RID: 9933
	public int padRight;

	// Token: 0x040026CE RID: 9934
	public int padBottom;

	// Token: 0x040026CF RID: 9935
	public int padTop;

	// Token: 0x040026D0 RID: 9936
	public bool ignoreDisabled = true;

	// Token: 0x040026D1 RID: 9937
	private bool mStarted;
}
