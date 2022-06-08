using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003B0 RID: 944
	public class BaseFinger
	{
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000E11C4 File Offset: 0x000DF5C4
		public Gesture GetGesture()
		{
			return new Gesture
			{
				fingerIndex = this.fingerIndex,
				touchCount = this.touchCount,
				startPosition = this.startPosition,
				position = this.position,
				deltaPosition = this.deltaPosition,
				actionTime = this.actionTime,
				deltaTime = this.deltaTime,
				isOverGui = this.isOverGui,
				pickedCamera = this.pickedCamera,
				pickedObject = this.pickedObject,
				isGuiCamera = this.isGuiCamera,
				pickedUIElement = this.pickedUIElement,
				altitudeAngle = this.altitudeAngle,
				azimuthAngle = this.azimuthAngle,
				maximumPossiblePressure = this.maximumPossiblePressure,
				pressure = this.pressure,
				radius = this.radius,
				radiusVariance = this.radiusVariance,
				touchType = this.touchType
			};
		}

		// Token: 0x04001E38 RID: 7736
		public int fingerIndex;

		// Token: 0x04001E39 RID: 7737
		public int touchCount;

		// Token: 0x04001E3A RID: 7738
		public Vector2 startPosition;

		// Token: 0x04001E3B RID: 7739
		public Vector2 position;

		// Token: 0x04001E3C RID: 7740
		public Vector2 deltaPosition;

		// Token: 0x04001E3D RID: 7741
		public float actionTime;

		// Token: 0x04001E3E RID: 7742
		public float deltaTime;

		// Token: 0x04001E3F RID: 7743
		public Camera pickedCamera;

		// Token: 0x04001E40 RID: 7744
		public GameObject pickedObject;

		// Token: 0x04001E41 RID: 7745
		public bool isGuiCamera;

		// Token: 0x04001E42 RID: 7746
		public bool isOverGui;

		// Token: 0x04001E43 RID: 7747
		public GameObject pickedUIElement;

		// Token: 0x04001E44 RID: 7748
		public float altitudeAngle;

		// Token: 0x04001E45 RID: 7749
		public float azimuthAngle;

		// Token: 0x04001E46 RID: 7750
		public float maximumPossiblePressure;

		// Token: 0x04001E47 RID: 7751
		public float pressure;

		// Token: 0x04001E48 RID: 7752
		public float radius;

		// Token: 0x04001E49 RID: 7753
		public float radiusVariance;

		// Token: 0x04001E4A RID: 7754
		public TouchType touchType;
	}
}
