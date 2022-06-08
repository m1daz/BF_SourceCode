using System;
using UnityEngine;

// Token: 0x0200063D RID: 1597
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06002E5B RID: 11867 RVA: 0x00153548 File Offset: 0x00151948
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06002E5C RID: 11868 RVA: 0x00153574 File Offset: 0x00151974
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			if (this.topLeft.gameObject.activeInHierarchy)
			{
				Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
				Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
				Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
				float num = this.fullSize * rect.height;
				if (rect != this.mCam.rect)
				{
					this.mCam.rect = rect;
				}
				if (this.mCam.orthographicSize != num)
				{
					this.mCam.orthographicSize = num;
				}
				this.mCam.enabled = true;
			}
			else
			{
				this.mCam.enabled = false;
			}
		}
	}

	// Token: 0x04002D49 RID: 11593
	public Camera sourceCamera;

	// Token: 0x04002D4A RID: 11594
	public Transform topLeft;

	// Token: 0x04002D4B RID: 11595
	public Transform bottomRight;

	// Token: 0x04002D4C RID: 11596
	public float fullSize = 1f;

	// Token: 0x04002D4D RID: 11597
	private Camera mCam;
}
