using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x02000396 RID: 918
	[AddComponentMenu("EasyTouch/Quick Enter-Over-Exit")]
	public class QuickEnterOverExist : QuickBase
	{
		// Token: 0x06001C68 RID: 7272 RVA: 0x000DFB5C File Offset: 0x000DDF5C
		public QuickEnterOverExist()
		{
			this.quickActionName = "QuickEnterOverExit" + Guid.NewGuid().ToString().Substring(0, 7);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000DFBA8 File Offset: 0x000DDFA8
		private void Awake()
		{
			for (int i = 0; i < 100; i++)
			{
				this.fingerOver[i] = false;
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000DFBD1 File Offset: 0x000DDFD1
		public override void OnEnable()
		{
			base.OnEnable();
			EasyTouch.On_TouchDown += this.On_TouchDown;
			EasyTouch.On_TouchUp += this.On_TouchUp;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000DFBFB File Offset: 0x000DDFFB
		public override void OnDisable()
		{
			base.OnDisable();
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000DFC09 File Offset: 0x000DE009
		private void OnDestroy()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000DFC11 File Offset: 0x000DE011
		private void UnsubscribeEvent()
		{
			EasyTouch.On_TouchDown -= this.On_TouchDown;
			EasyTouch.On_TouchUp -= this.On_TouchUp;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000DFC38 File Offset: 0x000DE038
		private void On_TouchDown(Gesture gesture)
		{
			if (this.realType != QuickBase.GameObjectType.UI)
			{
				if ((!this.enablePickOverUI && gesture.GetCurrentFirstPickedUIElement(false) == null) || this.enablePickOverUI)
				{
					if (gesture.GetCurrentPickedObject(false) == base.gameObject)
					{
						if (!this.fingerOver[gesture.fingerIndex] && ((!this.isOnTouch && !this.isMultiTouch) || this.isMultiTouch))
						{
							this.fingerOver[gesture.fingerIndex] = true;
							this.onTouchEnter.Invoke(gesture);
							this.isOnTouch = true;
						}
						else if (this.fingerOver[gesture.fingerIndex])
						{
							this.onTouchOver.Invoke(gesture);
						}
					}
					else if (this.fingerOver[gesture.fingerIndex])
					{
						this.fingerOver[gesture.fingerIndex] = false;
						this.onTouchExit.Invoke(gesture);
						if (!this.isMultiTouch)
						{
							this.isOnTouch = false;
						}
					}
				}
				else if (gesture.GetCurrentPickedObject(false) == base.gameObject && !this.enablePickOverUI && gesture.GetCurrentFirstPickedUIElement(false) != null && this.fingerOver[gesture.fingerIndex])
				{
					this.fingerOver[gesture.fingerIndex] = false;
					this.onTouchExit.Invoke(gesture);
					if (!this.isMultiTouch)
					{
						this.isOnTouch = false;
					}
				}
			}
			else if (gesture.GetCurrentFirstPickedUIElement(false) == base.gameObject)
			{
				if (!this.fingerOver[gesture.fingerIndex] && ((!this.isOnTouch && !this.isMultiTouch) || this.isMultiTouch))
				{
					this.fingerOver[gesture.fingerIndex] = true;
					this.onTouchEnter.Invoke(gesture);
					this.isOnTouch = true;
				}
				else if (this.fingerOver[gesture.fingerIndex])
				{
					this.onTouchOver.Invoke(gesture);
				}
			}
			else if (this.fingerOver[gesture.fingerIndex])
			{
				this.fingerOver[gesture.fingerIndex] = false;
				this.onTouchExit.Invoke(gesture);
				if (!this.isMultiTouch)
				{
					this.isOnTouch = false;
				}
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000DFE97 File Offset: 0x000DE297
		private void On_TouchUp(Gesture gesture)
		{
			if (this.fingerOver[gesture.fingerIndex])
			{
				this.fingerOver[gesture.fingerIndex] = false;
				this.onTouchExit.Invoke(gesture);
				if (!this.isMultiTouch)
				{
					this.isOnTouch = false;
				}
			}
		}

		// Token: 0x04001DEE RID: 7662
		[SerializeField]
		public QuickEnterOverExist.OnTouchEnter onTouchEnter;

		// Token: 0x04001DEF RID: 7663
		[SerializeField]
		public QuickEnterOverExist.OnTouchOver onTouchOver;

		// Token: 0x04001DF0 RID: 7664
		[SerializeField]
		public QuickEnterOverExist.OnTouchExit onTouchExit;

		// Token: 0x04001DF1 RID: 7665
		private bool[] fingerOver = new bool[100];

		// Token: 0x02000397 RID: 919
		[Serializable]
		public class OnTouchEnter : UnityEvent<Gesture>
		{
		}

		// Token: 0x02000398 RID: 920
		[Serializable]
		public class OnTouchOver : UnityEvent<Gesture>
		{
		}

		// Token: 0x02000399 RID: 921
		[Serializable]
		public class OnTouchExit : UnityEvent<Gesture>
		{
		}
	}
}
