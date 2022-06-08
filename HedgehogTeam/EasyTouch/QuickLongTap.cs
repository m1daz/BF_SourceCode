using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x0200039A RID: 922
	[AddComponentMenu("EasyTouch/Quick LongTap")]
	public class QuickLongTap : QuickBase
	{
		// Token: 0x06001C73 RID: 7283 RVA: 0x000DFEF0 File Offset: 0x000DE2F0
		public QuickLongTap()
		{
			this.quickActionName = "QuickLongTap" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000DFF30 File Offset: 0x000DE330
		private void Update()
		{
			this.currentGesture = EasyTouch.current;
			if (!this.is2Finger)
			{
				if (this.currentGesture.type == EasyTouch.EvtType.On_TouchStart && this.fingerIndex == -1 && this.IsOverMe(this.currentGesture))
				{
					this.fingerIndex = this.currentGesture.fingerIndex;
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_LongTapStart && this.actionTriggering == QuickLongTap.ActionTriggering.Start && (this.currentGesture.fingerIndex == this.fingerIndex || this.isMultiTouch))
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_LongTap && this.actionTriggering == QuickLongTap.ActionTriggering.InProgress && (this.currentGesture.fingerIndex == this.fingerIndex || this.isMultiTouch))
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_LongTapEnd && this.actionTriggering == QuickLongTap.ActionTriggering.End && (this.currentGesture.fingerIndex == this.fingerIndex || this.isMultiTouch))
				{
					this.DoAction(this.currentGesture);
					this.fingerIndex = -1;
				}
			}
			else
			{
				if (this.currentGesture.type == EasyTouch.EvtType.On_LongTapStart2Fingers && this.actionTriggering == QuickLongTap.ActionTriggering.Start)
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_LongTap2Fingers && this.actionTriggering == QuickLongTap.ActionTriggering.InProgress)
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_LongTapEnd2Fingers && this.actionTriggering == QuickLongTap.ActionTriggering.End)
				{
					this.DoAction(this.currentGesture);
				}
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000E00F8 File Offset: 0x000DE4F8
		private void DoAction(Gesture gesture)
		{
			if (this.IsOverMe(gesture))
			{
				this.onLongTap.Invoke(gesture);
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000E0114 File Offset: 0x000DE514
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

		// Token: 0x04001DF2 RID: 7666
		[SerializeField]
		public QuickLongTap.OnLongTap onLongTap;

		// Token: 0x04001DF3 RID: 7667
		public QuickLongTap.ActionTriggering actionTriggering;

		// Token: 0x04001DF4 RID: 7668
		private Gesture currentGesture;

		// Token: 0x0200039B RID: 923
		[Serializable]
		public class OnLongTap : UnityEvent<Gesture>
		{
		}

		// Token: 0x0200039C RID: 924
		public enum ActionTriggering
		{
			// Token: 0x04001DF6 RID: 7670
			Start,
			// Token: 0x04001DF7 RID: 7671
			InProgress,
			// Token: 0x04001DF8 RID: 7672
			End
		}
	}
}
