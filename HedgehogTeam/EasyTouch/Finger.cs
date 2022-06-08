using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003E3 RID: 995
	public class Finger : BaseFinger
	{
		// Token: 0x04001F00 RID: 7936
		public float startTimeAction;

		// Token: 0x04001F01 RID: 7937
		public Vector2 oldPosition;

		// Token: 0x04001F02 RID: 7938
		public int tapCount;

		// Token: 0x04001F03 RID: 7939
		public TouchPhase phase;

		// Token: 0x04001F04 RID: 7940
		public EasyTouch.GestureType gesture;

		// Token: 0x04001F05 RID: 7941
		public EasyTouch.SwipeDirection oldSwipeType;
	}
}
