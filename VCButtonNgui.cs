using System;

// Token: 0x020006BB RID: 1723
public class VCButtonNgui : VCButtonWithBehaviours
{
	// Token: 0x060032D3 RID: 13011 RVA: 0x00166036 File Offset: 0x00164436
	protected override bool Init()
	{
		return VCPluginSettings.NguiEnabled(base.gameObject) && base.Init();
	}

	// Token: 0x060032D4 RID: 13012 RVA: 0x00166050 File Offset: 0x00164450
	protected override void InitBehaviours()
	{
		if (this.upStateObject != null)
		{
			this._upBehaviour = this.upStateObject.GetComponent<UISprite>();
		}
		if (this.pressedStateObject != null)
		{
			this._pressedBehavior = this.pressedStateObject.GetComponent<UISprite>();
		}
	}

	// Token: 0x060032D5 RID: 13013 RVA: 0x001660A4 File Offset: 0x001644A4
	protected override void ShowPressedState(bool pressed)
	{
		base.ShowPressedState(pressed);
		if (base.Pressed)
		{
			UISprite uisprite = this._pressedBehavior as UISprite;
			if (uisprite != null && uisprite.panel.widgetsAreStatic)
			{
				uisprite.panel.Refresh();
			}
		}
		else
		{
			UISprite uisprite2 = this._upBehaviour as UISprite;
			if (uisprite2 != null && uisprite2.panel.widgetsAreStatic)
			{
				uisprite2.panel.Refresh();
			}
		}
	}

	// Token: 0x060032D6 RID: 13014 RVA: 0x0016612E File Offset: 0x0016452E
	protected override bool Colliding(VCTouchWrapper tw)
	{
		return base.AABBContains(tw.position);
	}
}
