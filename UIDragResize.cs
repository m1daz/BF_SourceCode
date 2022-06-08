using System;
using UnityEngine;

// Token: 0x02000571 RID: 1393
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
	// Token: 0x060026E3 RID: 9955 RVA: 0x0011FAEC File Offset: 0x0011DEEC
	private void OnDragStart()
	{
		if (this.target != null)
		{
			Vector3[] worldCorners = this.target.worldCorners;
			this.mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
			Ray currentRay = UICamera.currentRay;
			float distance;
			if (this.mPlane.Raycast(currentRay, out distance))
			{
				this.mRayPos = currentRay.GetPoint(distance);
				this.mLocalPos = this.target.cachedTransform.localPosition;
				this.mWidth = this.target.width;
				this.mHeight = this.target.height;
				this.mDragging = true;
			}
		}
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x0011FBAC File Offset: 0x0011DFAC
	private void OnDrag(Vector2 delta)
	{
		if (this.mDragging && this.target != null)
		{
			Ray currentRay = UICamera.currentRay;
			float distance;
			if (this.mPlane.Raycast(currentRay, out distance))
			{
				Transform cachedTransform = this.target.cachedTransform;
				cachedTransform.localPosition = this.mLocalPos;
				this.target.width = this.mWidth;
				this.target.height = this.mHeight;
				Vector3 b = currentRay.GetPoint(distance) - this.mRayPos;
				cachedTransform.position += b;
				Vector3 vector = Quaternion.Inverse(cachedTransform.localRotation) * (cachedTransform.localPosition - this.mLocalPos);
				cachedTransform.localPosition = this.mLocalPos;
				NGUIMath.ResizeWidget(this.target, this.pivot, vector.x, vector.y, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight);
				if (this.updateAnchors)
				{
					this.target.BroadcastMessage("UpdateAnchors");
				}
			}
		}
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x0011FCCF File Offset: 0x0011E0CF
	private void OnDragEnd()
	{
		this.mDragging = false;
	}

	// Token: 0x0400279F RID: 10143
	public UIWidget target;

	// Token: 0x040027A0 RID: 10144
	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	// Token: 0x040027A1 RID: 10145
	public int minWidth = 100;

	// Token: 0x040027A2 RID: 10146
	public int minHeight = 100;

	// Token: 0x040027A3 RID: 10147
	public int maxWidth = 100000;

	// Token: 0x040027A4 RID: 10148
	public int maxHeight = 100000;

	// Token: 0x040027A5 RID: 10149
	public bool updateAnchors;

	// Token: 0x040027A6 RID: 10150
	private Plane mPlane;

	// Token: 0x040027A7 RID: 10151
	private Vector3 mRayPos;

	// Token: 0x040027A8 RID: 10152
	private Vector3 mLocalPos;

	// Token: 0x040027A9 RID: 10153
	private int mWidth;

	// Token: 0x040027AA RID: 10154
	private int mHeight;

	// Token: 0x040027AB RID: 10155
	private bool mDragging;
}
