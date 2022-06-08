using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003A8 RID: 936
	[AddComponentMenu("EasyTouch/Quick Touch")]
	public class QuickTouch : QuickBase
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x000E0BC4 File Offset: 0x000DEFC4
		public QuickTouch()
		{
			this.quickActionName = "QuickTouch" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000E0C04 File Offset: 0x000DF004
		private void Update()
		{
			this.currentGesture = EasyTouch.current;
			if (!this.is2Finger)
			{
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchStart && this.fingerIndex == -1 && this.IsOverMe(this.currentGesture))
				{
					this.fingerIndex = this.currentGesture.fingerIndex;
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchStart && this.actionTriggering == QuickTouch.ActionTriggering.Start && (this.currentGesture.fingerIndex == this.fingerIndex || this.isMultiTouch))
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchDown && this.actionTriggering == QuickTouch.ActionTriggering.Down && (this.currentGesture.fingerIndex == this.fingerIndex || this.isMultiTouch))
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchUp)
				{
					if (this.actionTriggering == QuickTouch.ActionTriggering.Up && (this.currentGesture.fingerIndex == this.fingerIndex || this.isMultiTouch))
					{
						if (this.IsOverMe(this.currentGesture))
						{
							this.onTouch.Invoke(this.currentGesture);
						}
						else
						{
							this.onTouchNotOverMe.Invoke(this.currentGesture);
						}
					}
					if (this.currentGesture.fingerIndex == this.fingerIndex)
					{
						this.fingerIndex = -1;
					}
				}
			}
			else
			{
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchStart2Fingers && this.actionTriggering == QuickTouch.ActionTriggering.Start)
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchDown2Fingers && this.actionTriggering == QuickTouch.ActionTriggering.Down)
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchUp2Fingers && this.actionTriggering == QuickTouch.ActionTriggering.Up)
				{
					this.DoAction(this.currentGesture);
				}
			}
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000E0E0E File Offset: 0x000DF20E
		private void DoAction(Gesture gesture)
		{
			if (this.IsOverMe(gesture))
			{
				this.onTouch.Invoke(gesture);
			}
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000E0E28 File Offset: 0x000DF228
		private bool IsOverMe(Gesture gesture)
		{
			bool result = false;
			if (this.realType == QuickBase.GameObjectType.UI)
			{
				if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
				{
					result = true;
				}
			}
			else if (((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI) && EasyTouch.GetGameObjectAt(gesture.position, this.is2Finger) == base.gameObject)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x04001E23 RID: 7715
		[SerializeField]
		public QuickTouch.OnTouch onTouch;

		// Token: 0x04001E24 RID: 7716
		public QuickTouch.OnTouchNotOverMe onTouchNotOverMe;

		// Token: 0x04001E25 RID: 7717
		public QuickTouch.ActionTriggering actionTriggering;

		// Token: 0x04001E26 RID: 7718
		private Gesture currentGesture;

		// Token: 0x020003A9 RID: 937
		[Serializable]
		public class OnTouch : UnityEvent<Gesture>
		{
		}

		// Token: 0x020003AA RID: 938
		[Serializable]
		public class OnTouchNotOverMe : UnityEvent<Gesture>
		{
		}

		// Token: 0x020003AB RID: 939
		public enum ActionTriggering
		{
			// Token: 0x04001E28 RID: 7720
			Start,
			// Token: 0x04001E29 RID: 7721
			Down,
			// Token: 0x04001E2A RID: 7722
			Up
		}
	}
}
