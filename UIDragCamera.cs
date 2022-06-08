using System;
using UnityEngine;

// Token: 0x02000569 RID: 1385
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	// Token: 0x060026AC RID: 9900 RVA: 0x0011EA31 File Offset: 0x0011CE31
	private void Awake()
	{
		if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x0011EA58 File Offset: 0x0011CE58
	private void OnPress(bool isPressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null && this.draggableCamera.enabled)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x0011EAB0 File Offset: 0x0011CEB0
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null && this.draggableCamera.enabled)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x0011EB08 File Offset: 0x0011CF08
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null && this.draggableCamera.enabled)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x04002761 RID: 10081
	public UIDraggableCamera draggableCamera;
}
