using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003E4 RID: 996
	public class Gesture : BaseFinger, ICloneable
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x000E6C91 File Offset: 0x000E5091
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000E6C99 File Offset: 0x000E5099
		public Vector3 GetTouchToWorldPoint(float z)
		{
			return Camera.main.ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z));
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000E6CC4 File Offset: 0x000E50C4
		public Vector3 GetTouchToWorldPoint(Vector3 position3D)
		{
			return Camera.main.ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, Camera.main.transform.InverseTransformPoint(position3D).z));
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000E6D10 File Offset: 0x000E5110
		public float GetSwipeOrDragAngle()
		{
			return Mathf.Atan2(this.swipeVector.normalized.y, this.swipeVector.normalized.x) * 57.29578f;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000E6D50 File Offset: 0x000E5150
		public Vector2 NormalizedPosition()
		{
			return new Vector2(100f / (float)Screen.width * this.position.x / 100f, 100f / (float)Screen.height * this.position.y / 100f);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x000E6D9E File Offset: 0x000E519E
		public bool IsOverUIElement()
		{
			return EasyTouch.IsFingerOverUIElement(this.fingerIndex);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000E6DAB File Offset: 0x000E51AB
		public bool IsOverRectTransform(RectTransform tr, Camera camera = null)
		{
			if (camera == null)
			{
				return RectTransformUtility.RectangleContainsScreenPoint(tr, this.position, null);
			}
			return RectTransformUtility.RectangleContainsScreenPoint(tr, this.position, camera);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000E6DD4 File Offset: 0x000E51D4
		public GameObject GetCurrentFirstPickedUIElement(bool isTwoFinger = false)
		{
			return EasyTouch.GetCurrentPickedUIElement(this.fingerIndex, isTwoFinger);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000E6DE2 File Offset: 0x000E51E2
		public GameObject GetCurrentPickedObject(bool isTwoFinger = false)
		{
			return EasyTouch.GetCurrentPickedObject(this.fingerIndex, isTwoFinger);
		}

		// Token: 0x04001F06 RID: 7942
		public EasyTouch.SwipeDirection swipe;

		// Token: 0x04001F07 RID: 7943
		public float swipeLength;

		// Token: 0x04001F08 RID: 7944
		public Vector2 swipeVector;

		// Token: 0x04001F09 RID: 7945
		public float deltaPinch;

		// Token: 0x04001F0A RID: 7946
		public float twistAngle;

		// Token: 0x04001F0B RID: 7947
		public float twoFingerDistance;

		// Token: 0x04001F0C RID: 7948
		public EasyTouch.EvtType type;
	}
}
