using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000403 RID: 1027
[Serializable]
public class ETCButton : ETCBase, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06001E8C RID: 7820 RVA: 0x000EA290 File Offset: 0x000E8690
	public ETCButton()
	{
		this.axis = new ETCAxis("Button");
		this._visible = true;
		this._activated = true;
		this.isOnTouch = false;
		this.enableKeySimulation = true;
		this.axis.unityAxis = "Jump";
		this.showPSInspector = true;
		this.showSpriteInspector = false;
		this.showBehaviourInspector = false;
		this.showEventInspector = false;
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x000EA2FB File Offset: 0x000E86FB
	protected override void Awake()
	{
		base.Awake();
		this.cachedImage = base.GetComponent<Image>();
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x000EA310 File Offset: 0x000E8710
	public override void Start()
	{
		this.axis.InitAxis();
		base.Start();
		this.isOnPress = false;
		if (this.allowSimulationStandalone && this.enableKeySimulation && !Application.isEditor)
		{
			this.SetVisible(this.visibleOnStandalone);
		}
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x000EA361 File Offset: 0x000E8761
	protected override void UpdateControlState()
	{
		this.UpdateButton();
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x000EA369 File Offset: 0x000E8769
	protected override void DoActionBeforeEndOfFrame()
	{
		this.axis.DoGravity();
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x000EA378 File Offset: 0x000E8778
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.isSwipeIn && !this.isOnTouch)
		{
			if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ETCBase>() && eventData.pointerDrag != base.gameObject)
			{
				this.previousDargObject = eventData.pointerDrag;
			}
			eventData.pointerDrag = base.gameObject;
			eventData.pointerPress = base.gameObject;
			this.OnPointerDown(eventData);
		}
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x000EA404 File Offset: 0x000E8804
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this._activated && !this.isOnTouch)
		{
			this.pointId = eventData.pointerId;
			this.axis.ResetAxis();
			this.axis.axisState = ETCAxis.AxisState.Down;
			this.isOnPress = false;
			this.isOnTouch = true;
			this.onDown.Invoke();
			this.ApllyState();
			this.axis.UpdateButton();
		}
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x000EA474 File Offset: 0x000E8874
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId)
		{
			this.isOnPress = false;
			this.isOnTouch = false;
			this.axis.axisState = ETCAxis.AxisState.Up;
			this.axis.axisValue = 0f;
			this.onUp.Invoke();
			this.ApllyState();
			if (this.previousDargObject)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(this.previousDargObject, eventData, ExecuteEvents.pointerUpHandler);
				this.previousDargObject = null;
			}
		}
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x000EA4F6 File Offset: 0x000E88F6
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId && this.axis.axisState == ETCAxis.AxisState.Press && !this.isSwipeOut)
		{
			this.OnPointerUp(eventData);
		}
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x000EA52C File Offset: 0x000E892C
	private void UpdateButton()
	{
		if (this.axis.axisState == ETCAxis.AxisState.Down)
		{
			this.isOnPress = true;
			this.axis.axisState = ETCAxis.AxisState.Press;
		}
		if (this.isOnPress)
		{
			this.axis.UpdateButton();
			this.onPressed.Invoke();
			this.onPressedValue.Invoke(this.axis.axisValue);
		}
		if (this.axis.axisState == ETCAxis.AxisState.Up)
		{
			this.isOnPress = false;
			this.axis.axisState = ETCAxis.AxisState.None;
		}
		if (this.enableKeySimulation && this._activated && this._visible && !this.isOnTouch)
		{
			if (Input.GetButton(this.axis.unityAxis) && this.axis.axisState == ETCAxis.AxisState.None)
			{
				this.axis.ResetAxis();
				this.onDown.Invoke();
				this.axis.axisState = ETCAxis.AxisState.Down;
			}
			if (!Input.GetButton(this.axis.unityAxis) && this.axis.axisState == ETCAxis.AxisState.Press)
			{
				this.axis.axisState = ETCAxis.AxisState.Up;
				this.axis.axisValue = 0f;
				this.onUp.Invoke();
			}
			this.axis.UpdateButton();
			this.ApllyState();
		}
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x000EA68C File Offset: 0x000E8A8C
	protected override void SetVisible(bool forceUnvisible = false)
	{
		bool visible = this._visible;
		if (!base.visible)
		{
			visible = base.visible;
		}
		base.GetComponent<Image>().enabled = visible;
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x000EA6C0 File Offset: 0x000E8AC0
	private void ApllyState()
	{
		if (this.cachedImage)
		{
			ETCAxis.AxisState axisState = this.axis.axisState;
			if (axisState != ETCAxis.AxisState.Down && axisState != ETCAxis.AxisState.Press)
			{
				this.cachedImage.sprite = this.normalSprite;
				this.cachedImage.color = this.normalColor;
			}
			else
			{
				this.cachedImage.sprite = this.pressedSprite;
				this.cachedImage.color = this.pressedColor;
			}
		}
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x000EA74A File Offset: 0x000E8B4A
	protected override void SetActivated()
	{
		if (!this._activated)
		{
			this.isOnPress = false;
			this.isOnTouch = false;
			this.axis.axisState = ETCAxis.AxisState.None;
			this.axis.axisValue = 0f;
			this.ApllyState();
		}
	}

	// Token: 0x04002001 RID: 8193
	[SerializeField]
	public ETCButton.OnDownHandler onDown;

	// Token: 0x04002002 RID: 8194
	[SerializeField]
	public ETCButton.OnPressedHandler onPressed;

	// Token: 0x04002003 RID: 8195
	[SerializeField]
	public ETCButton.OnPressedValueandler onPressedValue;

	// Token: 0x04002004 RID: 8196
	[SerializeField]
	public ETCButton.OnUPHandler onUp;

	// Token: 0x04002005 RID: 8197
	public ETCAxis axis;

	// Token: 0x04002006 RID: 8198
	public Sprite normalSprite;

	// Token: 0x04002007 RID: 8199
	public Color normalColor;

	// Token: 0x04002008 RID: 8200
	public Sprite pressedSprite;

	// Token: 0x04002009 RID: 8201
	public Color pressedColor;

	// Token: 0x0400200A RID: 8202
	private Image cachedImage;

	// Token: 0x0400200B RID: 8203
	private bool isOnPress;

	// Token: 0x0400200C RID: 8204
	private GameObject previousDargObject;

	// Token: 0x0400200D RID: 8205
	private bool isOnTouch;

	// Token: 0x02000404 RID: 1028
	[Serializable]
	public class OnDownHandler : UnityEvent
	{
	}

	// Token: 0x02000405 RID: 1029
	[Serializable]
	public class OnPressedHandler : UnityEvent
	{
	}

	// Token: 0x02000406 RID: 1030
	[Serializable]
	public class OnPressedValueandler : UnityEvent<float>
	{
	}

	// Token: 0x02000407 RID: 1031
	[Serializable]
	public class OnUPHandler : UnityEvent
	{
	}
}
