using System;
using UnityEngine;

// Token: 0x020006B6 RID: 1718
public class VCButtonWithBehaviours : VCButtonBase
{
	// Token: 0x060032AD RID: 12973 RVA: 0x00165245 File Offset: 0x00163645
	protected override bool Init()
	{
		if (!base.Init())
		{
			return false;
		}
		this.InitGameObjects();
		this.InitBehaviours();
		return true;
	}

	// Token: 0x060032AE RID: 12974 RVA: 0x00165264 File Offset: 0x00163664
	protected void InitGameObjects()
	{
		if (this.upStateObject == null && this.pressedStateObject == null)
		{
			Debug.LogWarning("No up or pressed state GameObjects specified! Setting upStateObject to this.gameObject.");
			this.upStateObject = base.gameObject;
		}
		if (this.colliderObject == null)
		{
			GameObject gameObject;
			if ((gameObject = this.upStateObject) == null)
			{
				gameObject = (this.pressedStateObject ?? base.gameObject);
			}
			this.colliderObject = gameObject;
		}
		base.InitCollider(this.colliderObject);
		if (this._requireCollider && this._collider == null)
		{
			VCUtils.DestroyWithError(base.gameObject, "colliderObject must have a Collider component!  Destroying this control.");
			return;
		}
	}

	// Token: 0x060032AF RID: 12975 RVA: 0x00165319 File Offset: 0x00163719
	protected virtual void InitBehaviours()
	{
	}

	// Token: 0x060032B0 RID: 12976 RVA: 0x0016531B File Offset: 0x0016371B
	protected override bool Colliding(VCTouchWrapper tw)
	{
		return base.AABBContains(tw.position);
	}

	// Token: 0x060032B1 RID: 12977 RVA: 0x0016532C File Offset: 0x0016372C
	protected override void ShowPressedState(bool pressed)
	{
		if (pressed)
		{
			if (this._upBehaviour != null)
			{
				this._upBehaviour.enabled = false;
			}
			if (this._pressedBehavior != null)
			{
				this._pressedBehavior.enabled = true;
			}
		}
		else
		{
			if (this._upBehaviour != null)
			{
				this._upBehaviour.enabled = true;
			}
			if (this._pressedBehavior != null)
			{
				this._pressedBehavior.enabled = false;
			}
		}
	}

	// Token: 0x04002F23 RID: 12067
	public GameObject upStateObject;

	// Token: 0x04002F24 RID: 12068
	public GameObject pressedStateObject;

	// Token: 0x04002F25 RID: 12069
	public GameObject colliderObject;

	// Token: 0x04002F26 RID: 12070
	protected Behaviour _upBehaviour;

	// Token: 0x04002F27 RID: 12071
	protected Behaviour _pressedBehavior;

	// Token: 0x04002F28 RID: 12072
	protected bool _requireCollider = true;
}
