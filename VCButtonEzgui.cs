using System;
using UnityEngine;

// Token: 0x020006B5 RID: 1717
public class VCButtonEzgui : VCButtonWithBehaviours
{
	// Token: 0x060032A8 RID: 12968 RVA: 0x001653C0 File Offset: 0x001637C0
	protected override bool Init()
	{
		if (!VCPluginSettings.EzguiEnabled(base.gameObject))
		{
			return false;
		}
		if (!base.Init())
		{
			return false;
		}
		if (this.colliderObject == this.upStateObject || this.colliderObject == this.pressedStateObject)
		{
			Debug.LogWarning("VCButtonEZGUI may not behave properly when the hitTestGameObject is the same as the up or pressedStateGameObject!  You should add a Collider to a gameObject independent from the EZGUI UI components.");
		}
		return true;
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x00165423 File Offset: 0x00163823
	protected override void InitBehaviours()
	{
		this._upBehaviour = this.GetEzguiBehavior(this.upStateObject);
		this._pressedBehavior = this.GetEzguiBehavior(this.pressedStateObject);
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x0016544C File Offset: 0x0016384C
	protected override void ShowPressedState(bool pressed)
	{
		if (pressed)
		{
			if (this._upBehaviour != null)
			{
				(this._upBehaviour as SpriteRoot).Hide(true);
			}
			if (this._pressedBehavior != null)
			{
				(this._pressedBehavior as SpriteRoot).Hide(false);
			}
		}
		else
		{
			if (this._upBehaviour != null)
			{
				(this._upBehaviour as SpriteRoot).Hide(false);
			}
			if (this._pressedBehavior != null)
			{
				(this._pressedBehavior as SpriteRoot).Hide(true);
			}
		}
	}

	// Token: 0x060032AB RID: 12971 RVA: 0x001654EC File Offset: 0x001638EC
	protected Behaviour GetEzguiBehavior(GameObject go)
	{
		if (go == null)
		{
			return null;
		}
		Behaviour component = go.GetComponent<SimpleSprite>();
		if (component == null)
		{
			component = go.GetComponent<UIButton>();
		}
		return component;
	}
}
