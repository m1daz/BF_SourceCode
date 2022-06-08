using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003AC RID: 940
	[AddComponentMenu("EasyTouch/Quick Twist")]
	public class QuickTwist : QuickBase
	{
		// Token: 0x06001C99 RID: 7321 RVA: 0x000E0EE4 File Offset: 0x000DF2E4
		public QuickTwist()
		{
			this.quickActionName = "QuickTwist" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000E0F21 File Offset: 0x000DF321
		public override void OnEnable()
		{
			EasyTouch.On_Twist += this.On_Twist;
			EasyTouch.On_TwistEnd += this.On_TwistEnd;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000E0F45 File Offset: 0x000DF345
		public override void OnDisable()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000E0F4D File Offset: 0x000DF34D
		private void OnDestroy()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000E0F55 File Offset: 0x000DF355
		private void UnsubscribeEvent()
		{
			EasyTouch.On_Twist -= this.On_Twist;
			EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000E0F79 File Offset: 0x000DF379
		private void On_Twist(Gesture gesture)
		{
			if (this.actionTriggering == QuickTwist.ActionTiggering.InProgress && this.IsRightRotation(gesture))
			{
				this.DoAction(gesture);
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000E0F99 File Offset: 0x000DF399
		private void On_TwistEnd(Gesture gesture)
		{
			if (this.actionTriggering == QuickTwist.ActionTiggering.End && this.IsRightRotation(gesture))
			{
				this.DoAction(gesture);
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000E0FBC File Offset: 0x000DF3BC
		private bool IsRightRotation(Gesture gesture)
		{
			this.axisActionValue = 0f;
			float num = 1f;
			if (this.inverseAxisValue)
			{
				num = -1f;
			}
			QuickTwist.ActionRotationDirection actionRotationDirection = this.rotationDirection;
			if (actionRotationDirection != QuickTwist.ActionRotationDirection.All)
			{
				if (actionRotationDirection != QuickTwist.ActionRotationDirection.Clockwise)
				{
					if (actionRotationDirection == QuickTwist.ActionRotationDirection.Counterclockwise)
					{
						if (gesture.twistAngle > 0f)
						{
							this.axisActionValue = gesture.twistAngle * this.sensibility * num;
							return true;
						}
					}
				}
				else if (gesture.twistAngle < 0f)
				{
					this.axisActionValue = gesture.twistAngle * this.sensibility * num;
					return true;
				}
				return false;
			}
			this.axisActionValue = gesture.twistAngle * this.sensibility * num;
			return true;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000E107C File Offset: 0x000DF47C
		private void DoAction(Gesture gesture)
		{
			if (this.isGestureOnMe)
			{
				if (this.realType == QuickBase.GameObjectType.UI)
				{
					if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
					{
						this.onTwistAction.Invoke(gesture);
						if (this.enableSimpleAction)
						{
							base.DoDirectAction(this.axisActionValue);
						}
					}
				}
				else if (((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI) && gesture.GetCurrentPickedObject(true) == base.gameObject)
				{
					this.onTwistAction.Invoke(gesture);
					if (this.enableSimpleAction)
					{
						base.DoDirectAction(this.axisActionValue);
					}
				}
			}
			else if ((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI)
			{
				this.onTwistAction.Invoke(gesture);
				if (this.enableSimpleAction)
				{
					base.DoDirectAction(this.axisActionValue);
				}
			}
		}

		// Token: 0x04001E2B RID: 7723
		[SerializeField]
		public QuickTwist.OnTwistAction onTwistAction;

		// Token: 0x04001E2C RID: 7724
		public bool isGestureOnMe;

		// Token: 0x04001E2D RID: 7725
		public QuickTwist.ActionTiggering actionTriggering;

		// Token: 0x04001E2E RID: 7726
		public QuickTwist.ActionRotationDirection rotationDirection;

		// Token: 0x04001E2F RID: 7727
		private float axisActionValue;

		// Token: 0x04001E30 RID: 7728
		public bool enableSimpleAction;

		// Token: 0x020003AD RID: 941
		[Serializable]
		public class OnTwistAction : UnityEvent<Gesture>
		{
		}

		// Token: 0x020003AE RID: 942
		public enum ActionTiggering
		{
			// Token: 0x04001E32 RID: 7730
			InProgress,
			// Token: 0x04001E33 RID: 7731
			End
		}

		// Token: 0x020003AF RID: 943
		public enum ActionRotationDirection
		{
			// Token: 0x04001E35 RID: 7733
			All,
			// Token: 0x04001E36 RID: 7734
			Clockwise,
			// Token: 0x04001E37 RID: 7735
			Counterclockwise
		}
	}
}
