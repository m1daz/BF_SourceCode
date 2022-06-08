using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003A5 RID: 933
	[AddComponentMenu("EasyTouch/Quick Tap")]
	public class QuickTap : QuickBase
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x000E09F4 File Offset: 0x000DEDF4
		public QuickTap()
		{
			this.quickActionName = "QuickTap" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000E0A34 File Offset: 0x000DEE34
		private void Update()
		{
			this.currentGesture = EasyTouch.current;
			if (!this.is2Finger)
			{
				if (this.currentGesture.type == EasyTouch.EvtType.On_DoubleTap && this.actionTriggering == QuickTap.ActionTriggering.Double_Tap)
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_SimpleTap && this.actionTriggering == QuickTap.ActionTriggering.Simple_Tap)
				{
					this.DoAction(this.currentGesture);
				}
			}
			else
			{
				if (this.currentGesture.type == EasyTouch.EvtType.On_DoubleTap2Fingers && this.actionTriggering == QuickTap.ActionTriggering.Double_Tap)
				{
					this.DoAction(this.currentGesture);
				}
				if (this.currentGesture.type == EasyTouch.EvtType.On_SimpleTap2Fingers && this.actionTriggering == QuickTap.ActionTriggering.Simple_Tap)
				{
					this.DoAction(this.currentGesture);
				}
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000E0B00 File Offset: 0x000DEF00
		private void DoAction(Gesture gesture)
		{
			if (this.realType == QuickBase.GameObjectType.UI)
			{
				if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
				{
					this.onTap.Invoke(gesture);
				}
			}
			else if (((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI) && EasyTouch.GetGameObjectAt(gesture.position, this.is2Finger) == base.gameObject)
			{
				this.onTap.Invoke(gesture);
			}
		}

		// Token: 0x04001E1D RID: 7709
		[SerializeField]
		public QuickTap.OnTap onTap;

		// Token: 0x04001E1E RID: 7710
		public QuickTap.ActionTriggering actionTriggering;

		// Token: 0x04001E1F RID: 7711
		private Gesture currentGesture;

		// Token: 0x020003A6 RID: 934
		[Serializable]
		public class OnTap : UnityEvent<Gesture>
		{
		}

		// Token: 0x020003A7 RID: 935
		public enum ActionTriggering
		{
			// Token: 0x04001E21 RID: 7713
			Simple_Tap,
			// Token: 0x04001E22 RID: 7714
			Double_Tap
		}
	}
}
