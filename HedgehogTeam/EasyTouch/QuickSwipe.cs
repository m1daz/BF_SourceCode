using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003A1 RID: 929
	[AddComponentMenu("EasyTouch/Quick Swipe")]
	public class QuickSwipe : QuickBase
	{
		// Token: 0x06001C83 RID: 7299 RVA: 0x000E0490 File Offset: 0x000DE890
		public QuickSwipe()
		{
			this.quickActionName = "QuickSwipe" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000E04DC File Offset: 0x000DE8DC
		public override void OnEnable()
		{
			base.OnEnable();
			EasyTouch.On_Drag += this.On_Drag;
			EasyTouch.On_Swipe += this.On_Swipe;
			EasyTouch.On_DragEnd += this.On_DragEnd;
			EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000E0533 File Offset: 0x000DE933
		public override void OnDisable()
		{
			base.OnDisable();
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000E0541 File Offset: 0x000DE941
		private void OnDestroy()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000E054C File Offset: 0x000DE94C
		private void UnsubscribeEvent()
		{
			EasyTouch.On_Drag -= this.On_Drag;
			EasyTouch.On_Swipe -= this.On_Swipe;
			EasyTouch.On_DragEnd -= this.On_DragEnd;
			EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000E05A0 File Offset: 0x000DE9A0
		private void On_Swipe(Gesture gesture)
		{
			if (gesture.touchCount == 1 && ((gesture.pickedObject != base.gameObject && !this.allowSwipeStartOverMe) || this.allowSwipeStartOverMe))
			{
				this.fingerIndex = gesture.fingerIndex;
				if (this.actionTriggering == QuickSwipe.ActionTriggering.InProgress && this.isRightDirection(gesture))
				{
					this.onSwipeAction.Invoke(gesture);
					if (this.enableSimpleAction)
					{
						this.DoAction(gesture);
					}
				}
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000E0628 File Offset: 0x000DEA28
		private void On_SwipeEnd(Gesture gesture)
		{
			if (this.actionTriggering == QuickSwipe.ActionTriggering.End && this.isRightDirection(gesture))
			{
				this.onSwipeAction.Invoke(gesture);
				if (this.enableSimpleAction)
				{
					this.DoAction(gesture);
				}
			}
			if (this.fingerIndex == gesture.fingerIndex)
			{
				this.fingerIndex = -1;
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000E0683 File Offset: 0x000DEA83
		private void On_DragEnd(Gesture gesture)
		{
			if (gesture.pickedObject == base.gameObject && this.allowSwipeStartOverMe)
			{
				this.On_SwipeEnd(gesture);
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000E06AD File Offset: 0x000DEAAD
		private void On_Drag(Gesture gesture)
		{
			if (gesture.pickedObject == base.gameObject && this.allowSwipeStartOverMe)
			{
				this.On_Swipe(gesture);
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000E06D8 File Offset: 0x000DEAD8
		private bool isRightDirection(Gesture gesture)
		{
			float num = -1f;
			if (this.inverseAxisValue)
			{
				num = 1f;
			}
			this.axisActionValue = 0f;
			switch (this.swipeDirection)
			{
			case QuickSwipe.SwipeDirection.Vertical:
				if (gesture.swipe == EasyTouch.SwipeDirection.Up || gesture.swipe == EasyTouch.SwipeDirection.Down)
				{
					this.axisActionValue = gesture.deltaPosition.y * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.Horizontal:
				if (gesture.swipe == EasyTouch.SwipeDirection.Left || gesture.swipe == EasyTouch.SwipeDirection.Right)
				{
					this.axisActionValue = gesture.deltaPosition.x * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.DiagonalRight:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpRight || gesture.swipe == EasyTouch.SwipeDirection.DownLeft)
				{
					this.axisActionValue = gesture.deltaPosition.magnitude * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.DiagonalLeft:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpLeft || gesture.swipe == EasyTouch.SwipeDirection.DownRight)
				{
					this.axisActionValue = gesture.deltaPosition.magnitude * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.Up:
				if (gesture.swipe == EasyTouch.SwipeDirection.Up)
				{
					this.axisActionValue = gesture.deltaPosition.y * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.UpRight:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpRight)
				{
					this.axisActionValue = gesture.deltaPosition.magnitude * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.Right:
				if (gesture.swipe == EasyTouch.SwipeDirection.Right)
				{
					this.axisActionValue = gesture.deltaPosition.x * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.DownRight:
				if (gesture.swipe == EasyTouch.SwipeDirection.DownRight)
				{
					this.axisActionValue = gesture.deltaPosition.magnitude * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.Down:
				if (gesture.swipe == EasyTouch.SwipeDirection.Down)
				{
					this.axisActionValue = gesture.deltaPosition.y * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.DownLeft:
				if (gesture.swipe == EasyTouch.SwipeDirection.DownLeft)
				{
					this.axisActionValue = gesture.deltaPosition.magnitude * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.Left:
				if (gesture.swipe == EasyTouch.SwipeDirection.Left)
				{
					this.axisActionValue = gesture.deltaPosition.x * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.UpLeft:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpLeft)
				{
					this.axisActionValue = gesture.deltaPosition.magnitude * num;
					return true;
				}
				break;
			case QuickSwipe.SwipeDirection.All:
				this.axisActionValue = gesture.deltaPosition.magnitude * num;
				return true;
			}
			this.axisActionValue = 0f;
			return false;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x000E0968 File Offset: 0x000DED68
		private void DoAction(Gesture gesture)
		{
			switch (this.directAction)
			{
			case QuickBase.DirectAction.Rotate:
			case QuickBase.DirectAction.RotateLocal:
				this.axisActionValue *= this.sensibility;
				break;
			case QuickBase.DirectAction.Translate:
			case QuickBase.DirectAction.TranslateLocal:
			case QuickBase.DirectAction.Scale:
				this.axisActionValue /= Screen.dpi;
				this.axisActionValue *= this.sensibility;
				break;
			}
			base.DoDirectAction(this.axisActionValue);
		}

		// Token: 0x04001E06 RID: 7686
		[SerializeField]
		public QuickSwipe.OnSwipeAction onSwipeAction;

		// Token: 0x04001E07 RID: 7687
		public bool allowSwipeStartOverMe = true;

		// Token: 0x04001E08 RID: 7688
		public QuickSwipe.ActionTriggering actionTriggering;

		// Token: 0x04001E09 RID: 7689
		public QuickSwipe.SwipeDirection swipeDirection = QuickSwipe.SwipeDirection.All;

		// Token: 0x04001E0A RID: 7690
		private float axisActionValue;

		// Token: 0x04001E0B RID: 7691
		public bool enableSimpleAction;

		// Token: 0x020003A2 RID: 930
		[Serializable]
		public class OnSwipeAction : UnityEvent<Gesture>
		{
		}

		// Token: 0x020003A3 RID: 931
		public enum ActionTriggering
		{
			// Token: 0x04001E0D RID: 7693
			InProgress,
			// Token: 0x04001E0E RID: 7694
			End
		}

		// Token: 0x020003A4 RID: 932
		public enum SwipeDirection
		{
			// Token: 0x04001E10 RID: 7696
			Vertical,
			// Token: 0x04001E11 RID: 7697
			Horizontal,
			// Token: 0x04001E12 RID: 7698
			DiagonalRight,
			// Token: 0x04001E13 RID: 7699
			DiagonalLeft,
			// Token: 0x04001E14 RID: 7700
			Up,
			// Token: 0x04001E15 RID: 7701
			UpRight,
			// Token: 0x04001E16 RID: 7702
			Right,
			// Token: 0x04001E17 RID: 7703
			DownRight,
			// Token: 0x04001E18 RID: 7704
			Down,
			// Token: 0x04001E19 RID: 7705
			DownLeft,
			// Token: 0x04001E1A RID: 7706
			Left,
			// Token: 0x04001E1B RID: 7707
			UpLeft,
			// Token: 0x04001E1C RID: 7708
			All
		}
	}
}
