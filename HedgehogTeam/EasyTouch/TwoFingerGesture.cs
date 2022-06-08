using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003E5 RID: 997
	public class TwoFingerGesture
	{
		// Token: 0x06001E01 RID: 7681 RVA: 0x000E6E0D File Offset: 0x000E520D
		public void ClearPickedObjectData()
		{
			this.pickedObject = null;
			this.oldPickedObject = null;
			this.pickedCamera = null;
			this.isGuiCamera = false;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000E6E2B File Offset: 0x000E522B
		public void ClearPickedUIData()
		{
			this.isOverGui = false;
			this.pickedUIElement = null;
		}

		// Token: 0x04001F0D RID: 7949
		public EasyTouch.GestureType currentGesture = EasyTouch.GestureType.None;

		// Token: 0x04001F0E RID: 7950
		public EasyTouch.GestureType oldGesture = EasyTouch.GestureType.None;

		// Token: 0x04001F0F RID: 7951
		public int finger0;

		// Token: 0x04001F10 RID: 7952
		public int finger1;

		// Token: 0x04001F11 RID: 7953
		public float startTimeAction;

		// Token: 0x04001F12 RID: 7954
		public float timeSinceStartAction;

		// Token: 0x04001F13 RID: 7955
		public Vector2 startPosition;

		// Token: 0x04001F14 RID: 7956
		public Vector2 position;

		// Token: 0x04001F15 RID: 7957
		public Vector2 deltaPosition;

		// Token: 0x04001F16 RID: 7958
		public Vector2 oldStartPosition;

		// Token: 0x04001F17 RID: 7959
		public float startDistance;

		// Token: 0x04001F18 RID: 7960
		public float fingerDistance;

		// Token: 0x04001F19 RID: 7961
		public float oldFingerDistance;

		// Token: 0x04001F1A RID: 7962
		public bool lockPinch;

		// Token: 0x04001F1B RID: 7963
		public bool lockTwist = true;

		// Token: 0x04001F1C RID: 7964
		public float lastPinch;

		// Token: 0x04001F1D RID: 7965
		public float lastTwistAngle;

		// Token: 0x04001F1E RID: 7966
		public GameObject pickedObject;

		// Token: 0x04001F1F RID: 7967
		public GameObject oldPickedObject;

		// Token: 0x04001F20 RID: 7968
		public Camera pickedCamera;

		// Token: 0x04001F21 RID: 7969
		public bool isGuiCamera;

		// Token: 0x04001F22 RID: 7970
		public bool isOverGui;

		// Token: 0x04001F23 RID: 7971
		public GameObject pickedUIElement;

		// Token: 0x04001F24 RID: 7972
		public bool dragStart;

		// Token: 0x04001F25 RID: 7973
		public bool swipeStart;

		// Token: 0x04001F26 RID: 7974
		public bool inSingleDoubleTaps;

		// Token: 0x04001F27 RID: 7975
		public float tapCurentTime;
	}
}
