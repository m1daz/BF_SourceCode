using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x0200039D RID: 925
	[AddComponentMenu("EasyTouch/Quick Pinch")]
	public class QuickPinch : QuickBase
	{
		// Token: 0x06001C78 RID: 7288 RVA: 0x000E01C8 File Offset: 0x000DE5C8
		public QuickPinch()
		{
			this.quickActionName = "QuickPinch" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000E0208 File Offset: 0x000DE608
		public override void OnEnable()
		{
			EasyTouch.On_Pinch += this.On_Pinch;
			EasyTouch.On_PinchIn += this.On_PinchIn;
			EasyTouch.On_PinchOut += this.On_PinchOut;
			EasyTouch.On_PinchEnd += this.On_PichEnd;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000E0259 File Offset: 0x000DE659
		public override void OnDisable()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000E0261 File Offset: 0x000DE661
		private void OnDestroy()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000E026C File Offset: 0x000DE66C
		private void UnsubscribeEvent()
		{
			EasyTouch.On_Pinch -= this.On_Pinch;
			EasyTouch.On_PinchIn -= this.On_PinchIn;
			EasyTouch.On_PinchOut -= this.On_PinchOut;
			EasyTouch.On_PinchEnd -= this.On_PichEnd;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000E02BD File Offset: 0x000DE6BD
		private void On_Pinch(Gesture gesture)
		{
			if (this.actionTriggering == QuickPinch.ActionTiggering.InProgress && this.pinchDirection == QuickPinch.ActionPinchDirection.All)
			{
				this.DoAction(gesture);
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000E02DC File Offset: 0x000DE6DC
		private void On_PinchIn(Gesture gesture)
		{
			if (this.actionTriggering == QuickPinch.ActionTiggering.InProgress & this.pinchDirection == QuickPinch.ActionPinchDirection.PinchIn)
			{
				this.DoAction(gesture);
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000E02FD File Offset: 0x000DE6FD
		private void On_PinchOut(Gesture gesture)
		{
			if (this.actionTriggering == QuickPinch.ActionTiggering.InProgress & this.pinchDirection == QuickPinch.ActionPinchDirection.PinchOut)
			{
				this.DoAction(gesture);
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000E031E File Offset: 0x000DE71E
		private void On_PichEnd(Gesture gesture)
		{
			if (this.actionTriggering == QuickPinch.ActionTiggering.End)
			{
				this.DoAction(gesture);
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000E0334 File Offset: 0x000DE734
		private void DoAction(Gesture gesture)
		{
			this.axisActionValue = gesture.deltaPinch * this.sensibility * Time.deltaTime;
			if (this.isGestureOnMe)
			{
				if (this.realType == QuickBase.GameObjectType.UI)
				{
					if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
					{
						this.onPinchAction.Invoke(gesture);
						if (this.enableSimpleAction)
						{
							base.DoDirectAction(this.axisActionValue);
						}
					}
				}
				else if (((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI) && gesture.GetCurrentPickedObject(true) == base.gameObject)
				{
					this.onPinchAction.Invoke(gesture);
					if (this.enableSimpleAction)
					{
						base.DoDirectAction(this.axisActionValue);
					}
				}
			}
			else if ((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI)
			{
				this.onPinchAction.Invoke(gesture);
				if (this.enableSimpleAction)
				{
					base.DoDirectAction(this.axisActionValue);
				}
			}
		}

		// Token: 0x04001DF9 RID: 7673
		[SerializeField]
		public QuickPinch.OnPinchAction onPinchAction;

		// Token: 0x04001DFA RID: 7674
		public bool isGestureOnMe;

		// Token: 0x04001DFB RID: 7675
		public QuickPinch.ActionTiggering actionTriggering;

		// Token: 0x04001DFC RID: 7676
		public QuickPinch.ActionPinchDirection pinchDirection;

		// Token: 0x04001DFD RID: 7677
		private float axisActionValue;

		// Token: 0x04001DFE RID: 7678
		public bool enableSimpleAction;

		// Token: 0x0200039E RID: 926
		[Serializable]
		public class OnPinchAction : UnityEvent<Gesture>
		{
		}

		// Token: 0x0200039F RID: 927
		public enum ActionTiggering
		{
			// Token: 0x04001E00 RID: 7680
			InProgress,
			// Token: 0x04001E01 RID: 7681
			End
		}

		// Token: 0x020003A0 RID: 928
		public enum ActionPinchDirection
		{
			// Token: 0x04001E03 RID: 7683
			All,
			// Token: 0x04001E04 RID: 7684
			PinchIn,
			// Token: 0x04001E05 RID: 7685
			PinchOut
		}
	}
}
