using System;
using System.Collections.Generic;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x0200038A RID: 906
	[AddComponentMenu("EasyTouch/Trigger")]
	[Serializable]
	public class EasyTouchTrigger : MonoBehaviour
	{
		// Token: 0x06001C1B RID: 7195 RVA: 0x000DE273 File Offset: 0x000DC673
		private void Start()
		{
			EasyTouch.SetEnableAutoSelect(true);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000DE27B File Offset: 0x000DC67B
		private void OnEnable()
		{
			this.SubscribeEasyTouchEvent();
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000DE283 File Offset: 0x000DC683
		private void OnDisable()
		{
			this.UnsubscribeEasyTouchEvent();
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000DE28B File Offset: 0x000DC68B
		private void OnDestroy()
		{
			this.UnsubscribeEasyTouchEvent();
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000DE294 File Offset: 0x000DC694
		private void SubscribeEasyTouchEvent()
		{
			if (this.IsRecevier4(EasyTouch.EvtType.On_Cancel))
			{
				EasyTouch.On_Cancel += this.On_Cancel;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TouchStart))
			{
				EasyTouch.On_TouchStart += this.On_TouchStart;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TouchDown))
			{
				EasyTouch.On_TouchDown += this.On_TouchDown;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TouchUp))
			{
				EasyTouch.On_TouchUp += this.On_TouchUp;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_SimpleTap))
			{
				EasyTouch.On_SimpleTap += this.On_SimpleTap;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_LongTapStart))
			{
				EasyTouch.On_LongTapStart += this.On_LongTapStart;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_LongTap))
			{
				EasyTouch.On_LongTap += this.On_LongTap;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_LongTapEnd))
			{
				EasyTouch.On_LongTapEnd += this.On_LongTapEnd;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_DoubleTap))
			{
				EasyTouch.On_DoubleTap += this.On_DoubleTap;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_DragStart))
			{
				EasyTouch.On_DragStart += this.On_DragStart;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_Drag))
			{
				EasyTouch.On_Drag += this.On_Drag;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_DragEnd))
			{
				EasyTouch.On_DragEnd += this.On_DragEnd;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_SwipeStart))
			{
				EasyTouch.On_SwipeStart += this.On_SwipeStart;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_Swipe))
			{
				EasyTouch.On_Swipe += this.On_Swipe;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_SwipeEnd))
			{
				EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TouchStart2Fingers))
			{
				EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TouchDown2Fingers))
			{
				EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TouchUp2Fingers))
			{
				EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_SimpleTap2Fingers))
			{
				EasyTouch.On_SimpleTap2Fingers += this.On_SimpleTap2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_LongTapStart2Fingers))
			{
				EasyTouch.On_LongTapStart2Fingers += this.On_LongTapStart2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_LongTap2Fingers))
			{
				EasyTouch.On_LongTap2Fingers += this.On_LongTap2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_LongTapEnd2Fingers))
			{
				EasyTouch.On_LongTapEnd2Fingers += this.On_LongTapEnd2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_DoubleTap2Fingers))
			{
				EasyTouch.On_DoubleTap2Fingers += this.On_DoubleTap2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_SwipeStart2Fingers))
			{
				EasyTouch.On_SwipeStart2Fingers += this.On_SwipeStart2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_Swipe2Fingers))
			{
				EasyTouch.On_Swipe2Fingers += this.On_Swipe2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_SwipeEnd2Fingers))
			{
				EasyTouch.On_SwipeEnd2Fingers += this.On_SwipeEnd2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_DragStart2Fingers))
			{
				EasyTouch.On_DragStart2Fingers += this.On_DragStart2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_Drag2Fingers))
			{
				EasyTouch.On_Drag2Fingers += this.On_Drag2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_DragEnd2Fingers))
			{
				EasyTouch.On_DragEnd2Fingers += this.On_DragEnd2Fingers;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_Pinch))
			{
				EasyTouch.On_Pinch += this.On_Pinch;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_PinchIn))
			{
				EasyTouch.On_PinchIn += this.On_PinchIn;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_PinchOut))
			{
				EasyTouch.On_PinchOut += this.On_PinchOut;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_PinchEnd))
			{
				EasyTouch.On_PinchEnd += this.On_PinchEnd;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_Twist))
			{
				EasyTouch.On_Twist += this.On_Twist;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_TwistEnd))
			{
				EasyTouch.On_TwistEnd += this.On_TwistEnd;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_OverUIElement))
			{
				EasyTouch.On_OverUIElement += this.On_OverUIElement;
			}
			if (this.IsRecevier4(EasyTouch.EvtType.On_UIElementTouchUp))
			{
				EasyTouch.On_UIElementTouchUp += this.On_UIElementTouchUp;
			}
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000DE6F0 File Offset: 0x000DCAF0
		private void UnsubscribeEasyTouchEvent()
		{
			EasyTouch.On_Cancel -= this.On_Cancel;
			EasyTouch.On_TouchStart -= this.On_TouchStart;
			EasyTouch.On_TouchDown -= this.On_TouchDown;
			EasyTouch.On_TouchUp -= this.On_TouchUp;
			EasyTouch.On_SimpleTap -= this.On_SimpleTap;
			EasyTouch.On_LongTapStart -= this.On_LongTapStart;
			EasyTouch.On_LongTap -= this.On_LongTap;
			EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
			EasyTouch.On_DoubleTap -= this.On_DoubleTap;
			EasyTouch.On_DragStart -= this.On_DragStart;
			EasyTouch.On_Drag -= this.On_Drag;
			EasyTouch.On_DragEnd -= this.On_DragEnd;
			EasyTouch.On_SwipeStart -= this.On_SwipeStart;
			EasyTouch.On_Swipe -= this.On_Swipe;
			EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
			EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
			EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
			EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
			EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
			EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
			EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
			EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
			EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
			EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
			EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
			EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
			EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
			EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
			EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
			EasyTouch.On_Pinch -= this.On_Pinch;
			EasyTouch.On_PinchIn -= this.On_PinchIn;
			EasyTouch.On_PinchOut -= this.On_PinchOut;
			EasyTouch.On_PinchEnd -= this.On_PinchEnd;
			EasyTouch.On_Twist -= this.On_Twist;
			EasyTouch.On_TwistEnd -= this.On_TwistEnd;
			EasyTouch.On_OverUIElement += this.On_OverUIElement;
			EasyTouch.On_UIElementTouchUp += this.On_UIElementTouchUp;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000DE972 File Offset: 0x000DCD72
		private void On_TouchStart(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TouchStart, gesture);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000DE97C File Offset: 0x000DCD7C
		private void On_TouchDown(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TouchDown, gesture);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000DE986 File Offset: 0x000DCD86
		private void On_TouchUp(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TouchUp, gesture);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000DE990 File Offset: 0x000DCD90
		private void On_SimpleTap(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_SimpleTap, gesture);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000DE99A File Offset: 0x000DCD9A
		private void On_DoubleTap(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_DoubleTap, gesture);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000DE9A4 File Offset: 0x000DCDA4
		private void On_LongTapStart(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_LongTapStart, gesture);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000DE9AE File Offset: 0x000DCDAE
		private void On_LongTap(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_LongTap, gesture);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000DE9B8 File Offset: 0x000DCDB8
		private void On_LongTapEnd(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_LongTapEnd, gesture);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000DE9C2 File Offset: 0x000DCDC2
		private void On_SwipeStart(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_SwipeStart, gesture);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000DE9CD File Offset: 0x000DCDCD
		private void On_Swipe(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Swipe, gesture);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000DE9D8 File Offset: 0x000DCDD8
		private void On_SwipeEnd(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_SwipeEnd, gesture);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000DE9E3 File Offset: 0x000DCDE3
		private void On_DragStart(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_DragStart, gesture);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000DE9EE File Offset: 0x000DCDEE
		private void On_Drag(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Drag, gesture);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000DE9F9 File Offset: 0x000DCDF9
		private void On_DragEnd(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_DragEnd, gesture);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000DEA04 File Offset: 0x000DCE04
		private void On_Cancel(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Cancel, gesture);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000DEA0F File Offset: 0x000DCE0F
		private void On_TouchStart2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TouchStart2Fingers, gesture);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000DEA1A File Offset: 0x000DCE1A
		private void On_TouchDown2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TouchDown2Fingers, gesture);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000DEA25 File Offset: 0x000DCE25
		private void On_TouchUp2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TouchUp2Fingers, gesture);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000DEA30 File Offset: 0x000DCE30
		private void On_LongTapStart2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_LongTapStart2Fingers, gesture);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000DEA3B File Offset: 0x000DCE3B
		private void On_LongTap2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_LongTap2Fingers, gesture);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000DEA46 File Offset: 0x000DCE46
		private void On_LongTapEnd2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_LongTapEnd2Fingers, gesture);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000DEA51 File Offset: 0x000DCE51
		private void On_DragStart2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_DragStart2Fingers, gesture);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000DEA5C File Offset: 0x000DCE5C
		private void On_Drag2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Drag2Fingers, gesture);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000DEA67 File Offset: 0x000DCE67
		private void On_DragEnd2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_DragEnd2Fingers, gesture);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000DEA72 File Offset: 0x000DCE72
		private void On_SwipeStart2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_SwipeStart2Fingers, gesture);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000DEA7D File Offset: 0x000DCE7D
		private void On_Swipe2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Swipe2Fingers, gesture);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000DEA88 File Offset: 0x000DCE88
		private void On_SwipeEnd2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_SwipeEnd2Fingers, gesture);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000DEA93 File Offset: 0x000DCE93
		private void On_Twist(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Twist, gesture);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000DEA9E File Offset: 0x000DCE9E
		private void On_TwistEnd(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_TwistEnd, gesture);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000DEAA9 File Offset: 0x000DCEA9
		private void On_Pinch(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_Pinch, gesture);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000DEAB4 File Offset: 0x000DCEB4
		private void On_PinchOut(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_PinchOut, gesture);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000DEABF File Offset: 0x000DCEBF
		private void On_PinchIn(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_PinchIn, gesture);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000DEACA File Offset: 0x000DCECA
		private void On_PinchEnd(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_PinchEnd, gesture);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000DEAD5 File Offset: 0x000DCED5
		private void On_SimpleTap2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_SimpleTap2Fingers, gesture);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000DEAE0 File Offset: 0x000DCEE0
		private void On_DoubleTap2Fingers(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_DoubleTap2Fingers, gesture);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000DEAEB File Offset: 0x000DCEEB
		private void On_UIElementTouchUp(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_UIElementTouchUp, gesture);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000DEAF6 File Offset: 0x000DCEF6
		private void On_OverUIElement(Gesture gesture)
		{
			this.TriggerScheduler(EasyTouch.EvtType.On_OverUIElement, gesture);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000DEB04 File Offset: 0x000DCF04
		public void AddTrigger(EasyTouch.EvtType ev)
		{
			EasyTouchTrigger.EasyTouchReceiver easyTouchReceiver = new EasyTouchTrigger.EasyTouchReceiver();
			easyTouchReceiver.enable = true;
			easyTouchReceiver.restricted = true;
			easyTouchReceiver.eventName = ev;
			easyTouchReceiver.gameObject = null;
			easyTouchReceiver.otherReceiver = false;
			easyTouchReceiver.name = "New trigger";
			this.receivers.Add(easyTouchReceiver);
			if (Application.isPlaying)
			{
				this.UnsubscribeEasyTouchEvent();
				this.SubscribeEasyTouchEvent();
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000DEB68 File Offset: 0x000DCF68
		public bool SetTriggerEnable(string triggerName, bool value)
		{
			EasyTouchTrigger.EasyTouchReceiver trigger = this.GetTrigger(triggerName);
			if (trigger != null)
			{
				trigger.enable = value;
				return true;
			}
			return false;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000DEB90 File Offset: 0x000DCF90
		public bool GetTriggerEnable(string triggerName)
		{
			EasyTouchTrigger.EasyTouchReceiver trigger = this.GetTrigger(triggerName);
			return trigger != null && trigger.enable;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000DEBB4 File Offset: 0x000DCFB4
		private void TriggerScheduler(EasyTouch.EvtType evnt, Gesture gesture)
		{
			foreach (EasyTouchTrigger.EasyTouchReceiver easyTouchReceiver in this.receivers)
			{
				if (easyTouchReceiver.enable && easyTouchReceiver.eventName == evnt && ((easyTouchReceiver.restricted && ((gesture.pickedObject == base.gameObject && easyTouchReceiver.triggerType == EasyTouchTrigger.ETTType.Object3D) || (gesture.pickedUIElement == base.gameObject && easyTouchReceiver.triggerType == EasyTouchTrigger.ETTType.UI))) || (!easyTouchReceiver.restricted && (easyTouchReceiver.gameObject == null || (easyTouchReceiver.gameObject == gesture.pickedObject && easyTouchReceiver.triggerType == EasyTouchTrigger.ETTType.Object3D) || (gesture.pickedUIElement == easyTouchReceiver.gameObject && easyTouchReceiver.triggerType == EasyTouchTrigger.ETTType.UI)))))
				{
					GameObject gameObject = base.gameObject;
					if (easyTouchReceiver.otherReceiver && easyTouchReceiver.gameObjectReceiver != null)
					{
						gameObject = easyTouchReceiver.gameObjectReceiver;
					}
					switch (easyTouchReceiver.parameter)
					{
					case EasyTouchTrigger.ETTParameter.None:
						gameObject.SendMessage(easyTouchReceiver.methodName, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Gesture:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Finger_Id:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.fingerIndex, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Touch_Count:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.touchCount, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Start_Position:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.startPosition, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Position:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.position, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Delta_Position:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.deltaPosition, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Swipe_Type:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.swipe, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Swipe_Length:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.swipeLength, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Swipe_Vector:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.swipeVector, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Delta_Pinch:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.deltaPinch, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.Twist_Anlge:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.twistAngle, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.ActionTime:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.actionTime, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.DeltaTime:
						gameObject.SendMessage(easyTouchReceiver.methodName, gesture.deltaTime, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyTouchTrigger.ETTParameter.PickedObject:
						if (gesture.pickedObject != null)
						{
							gameObject.SendMessage(easyTouchReceiver.methodName, gesture.pickedObject, SendMessageOptions.DontRequireReceiver);
						}
						break;
					case EasyTouchTrigger.ETTParameter.PickedUIElement:
						if (gesture.pickedUIElement != null)
						{
							gameObject.SendMessage(easyTouchReceiver.methodName, gesture.pickedObject, SendMessageOptions.DontRequireReceiver);
						}
						break;
					}
				}
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000DEF28 File Offset: 0x000DD328
		private bool IsRecevier4(EasyTouch.EvtType evnt)
		{
			int num = this.receivers.FindIndex((EasyTouchTrigger.EasyTouchReceiver e) => e.eventName == evnt);
			return num > -1;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000DEF64 File Offset: 0x000DD364
		private EasyTouchTrigger.EasyTouchReceiver GetTrigger(string triggerName)
		{
			return this.receivers.Find((EasyTouchTrigger.EasyTouchReceiver n) => n.name == triggerName);
		}

		// Token: 0x04001DA3 RID: 7587
		[SerializeField]
		public List<EasyTouchTrigger.EasyTouchReceiver> receivers = new List<EasyTouchTrigger.EasyTouchReceiver>();

		// Token: 0x0200038B RID: 907
		public enum ETTParameter
		{
			// Token: 0x04001DA5 RID: 7589
			None,
			// Token: 0x04001DA6 RID: 7590
			Gesture,
			// Token: 0x04001DA7 RID: 7591
			Finger_Id,
			// Token: 0x04001DA8 RID: 7592
			Touch_Count,
			// Token: 0x04001DA9 RID: 7593
			Start_Position,
			// Token: 0x04001DAA RID: 7594
			Position,
			// Token: 0x04001DAB RID: 7595
			Delta_Position,
			// Token: 0x04001DAC RID: 7596
			Swipe_Type,
			// Token: 0x04001DAD RID: 7597
			Swipe_Length,
			// Token: 0x04001DAE RID: 7598
			Swipe_Vector,
			// Token: 0x04001DAF RID: 7599
			Delta_Pinch,
			// Token: 0x04001DB0 RID: 7600
			Twist_Anlge,
			// Token: 0x04001DB1 RID: 7601
			ActionTime,
			// Token: 0x04001DB2 RID: 7602
			DeltaTime,
			// Token: 0x04001DB3 RID: 7603
			PickedObject,
			// Token: 0x04001DB4 RID: 7604
			PickedUIElement
		}

		// Token: 0x0200038C RID: 908
		public enum ETTType
		{
			// Token: 0x04001DB6 RID: 7606
			Object3D,
			// Token: 0x04001DB7 RID: 7607
			UI
		}

		// Token: 0x0200038D RID: 909
		[Serializable]
		public class EasyTouchReceiver
		{
			// Token: 0x04001DB8 RID: 7608
			public bool enable;

			// Token: 0x04001DB9 RID: 7609
			public EasyTouchTrigger.ETTType triggerType;

			// Token: 0x04001DBA RID: 7610
			public string name;

			// Token: 0x04001DBB RID: 7611
			public bool restricted;

			// Token: 0x04001DBC RID: 7612
			public GameObject gameObject;

			// Token: 0x04001DBD RID: 7613
			public bool otherReceiver;

			// Token: 0x04001DBE RID: 7614
			public GameObject gameObjectReceiver;

			// Token: 0x04001DBF RID: 7615
			public EasyTouch.EvtType eventName;

			// Token: 0x04001DC0 RID: 7616
			public string methodName;

			// Token: 0x04001DC1 RID: 7617
			public EasyTouchTrigger.ETTParameter parameter;
		}
	}
}
