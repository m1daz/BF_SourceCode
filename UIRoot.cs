using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000630 RID: 1584
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06002DDE RID: 11742 RVA: 0x001501B2 File Offset: 0x0014E5B2
	public UIRoot.Constraint constraint
	{
		get
		{
			if (this.fitWidth)
			{
				if (this.fitHeight)
				{
					return UIRoot.Constraint.Fit;
				}
				return UIRoot.Constraint.FitWidth;
			}
			else
			{
				if (this.fitHeight)
				{
					return UIRoot.Constraint.FitHeight;
				}
				return UIRoot.Constraint.Fill;
			}
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06002DDF RID: 11743 RVA: 0x001501DC File Offset: 0x0014E5DC
	public UIRoot.Scaling activeScaling
	{
		get
		{
			UIRoot.Scaling scaling = this.scalingStyle;
			if (scaling == UIRoot.Scaling.ConstrainedOnMobiles)
			{
				return UIRoot.Scaling.Constrained;
			}
			return scaling;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x001501FC File Offset: 0x0014E5FC
	public int activeHeight
	{
		get
		{
			if (this.activeScaling == UIRoot.Scaling.Flexible)
			{
				Vector2 screenSize = NGUITools.screenSize;
				float num = screenSize.x / screenSize.y;
				if (screenSize.y < (float)this.minimumHeight)
				{
					screenSize.y = (float)this.minimumHeight;
					screenSize.x = screenSize.y * num;
				}
				else if (screenSize.y > (float)this.maximumHeight)
				{
					screenSize.y = (float)this.maximumHeight;
					screenSize.x = screenSize.y * num;
				}
				int num2 = Mathf.RoundToInt((!this.shrinkPortraitUI || screenSize.y <= screenSize.x) ? screenSize.y : (screenSize.y / num));
				return (!this.adjustByDPI) ? num2 : NGUIMath.AdjustByDPI((float)num2);
			}
			UIRoot.Constraint constraint = this.constraint;
			if (constraint == UIRoot.Constraint.FitHeight)
			{
				return this.manualHeight;
			}
			Vector2 screenSize2 = NGUITools.screenSize;
			float num3 = screenSize2.x / screenSize2.y;
			float num4 = (float)this.manualWidth / (float)this.manualHeight;
			if (constraint == UIRoot.Constraint.FitWidth)
			{
				return Mathf.RoundToInt((float)this.manualWidth / num3);
			}
			if (constraint == UIRoot.Constraint.Fit)
			{
				return (num4 <= num3) ? this.manualHeight : Mathf.RoundToInt((float)this.manualWidth / num3);
			}
			if (constraint != UIRoot.Constraint.Fill)
			{
				return this.manualHeight;
			}
			return (num4 >= num3) ? this.manualHeight : Mathf.RoundToInt((float)this.manualWidth / num3);
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x001503A0 File Offset: 0x0014E7A0
	public float pixelSizeAdjustment
	{
		get
		{
			int num = Mathf.RoundToInt(NGUITools.screenSize.y);
			return (num != -1) ? this.GetPixelSizeAdjustment(num) : 1f;
		}
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x001503D8 File Offset: 0x0014E7D8
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uiroot != null)) ? 1f : uiroot.pixelSizeAdjustment;
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x00150408 File Offset: 0x0014E808
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.activeScaling == UIRoot.Scaling.Constrained)
		{
			return (float)this.activeHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x00150468 File Offset: 0x0014E868
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x00150476 File Offset: 0x0014E876
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x00150483 File Offset: 0x0014E883
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x00150494 File Offset: 0x0014E894
	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			this.UpdateScale(false);
		}
	}

	// Token: 0x06002DE8 RID: 11752 RVA: 0x001504F5 File Offset: 0x0014E8F5
	private void Update()
	{
		this.UpdateScale(true);
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x00150500 File Offset: 0x0014E900
	public void UpdateScale(bool updateAnchors = true)
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1E-45f || Mathf.Abs(localScale.y - num2) > 1E-45f || Mathf.Abs(localScale.z - num2) > 1E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
					if (updateAnchors)
					{
						base.BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x001505B4 File Offset: 0x0014E9B4
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x00150600 File Offset: 0x0014EA00
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = UIRoot.list.Count;
			while (i < count)
			{
				UIRoot uiroot = UIRoot.list[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
				}
				i++;
			}
		}
	}

	// Token: 0x04002CDE RID: 11486
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x04002CDF RID: 11487
	public UIRoot.Scaling scalingStyle;

	// Token: 0x04002CE0 RID: 11488
	public int manualWidth = 1280;

	// Token: 0x04002CE1 RID: 11489
	public int manualHeight = 720;

	// Token: 0x04002CE2 RID: 11490
	public int minimumHeight = 320;

	// Token: 0x04002CE3 RID: 11491
	public int maximumHeight = 1536;

	// Token: 0x04002CE4 RID: 11492
	public bool fitWidth;

	// Token: 0x04002CE5 RID: 11493
	public bool fitHeight = true;

	// Token: 0x04002CE6 RID: 11494
	public bool adjustByDPI;

	// Token: 0x04002CE7 RID: 11495
	public bool shrinkPortraitUI;

	// Token: 0x04002CE8 RID: 11496
	private Transform mTrans;

	// Token: 0x02000631 RID: 1585
	[DoNotObfuscateNGUI]
	public enum Scaling
	{
		// Token: 0x04002CEA RID: 11498
		Flexible,
		// Token: 0x04002CEB RID: 11499
		Constrained,
		// Token: 0x04002CEC RID: 11500
		ConstrainedOnMobiles
	}

	// Token: 0x02000632 RID: 1586
	[DoNotObfuscateNGUI]
	public enum Constraint
	{
		// Token: 0x04002CEE RID: 11502
		Fit,
		// Token: 0x04002CEF RID: 11503
		Fill,
		// Token: 0x04002CF0 RID: 11504
		FitWidth,
		// Token: 0x04002CF1 RID: 11505
		FitHeight
	}
}
