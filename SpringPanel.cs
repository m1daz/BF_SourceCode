using System;
using UnityEngine;

// Token: 0x020005C2 RID: 1474
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x060029DB RID: 10715 RVA: 0x00136C89 File Offset: 0x00135089
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIScrollView>();
		this.mTrans = base.transform;
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x00136CAF File Offset: 0x001350AF
	private void Update()
	{
		this.AdvanceTowardsPosition();
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x00136CB8 File Offset: 0x001350B8
	protected virtual void AdvanceTowardsPosition()
	{
		float deltaTime = RealTime.deltaTime;
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		if ((vector - this.target).sqrMagnitude < 0.01f)
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= vector2.x;
		clipOffset.y -= vector2.y;
		this.mPanel.clipOffset = clipOffset;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			SpringPanel.current = this;
			this.onFinished();
			SpringPanel.current = null;
		}
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x00136DC4 File Offset: 0x001351C4
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		springPanel.enabled = true;
		return springPanel;
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x00136E08 File Offset: 0x00135208
	public static SpringPanel Stop(GameObject go)
	{
		SpringPanel component = go.GetComponent<SpringPanel>();
		if (component != null && component.enabled)
		{
			if (component.onFinished != null)
			{
				component.onFinished();
			}
			component.enabled = false;
		}
		return component;
	}

	// Token: 0x040029E2 RID: 10722
	public static SpringPanel current;

	// Token: 0x040029E3 RID: 10723
	public Vector3 target = Vector3.zero;

	// Token: 0x040029E4 RID: 10724
	public float strength = 10f;

	// Token: 0x040029E5 RID: 10725
	public SpringPanel.OnFinished onFinished;

	// Token: 0x040029E6 RID: 10726
	private UIPanel mPanel;

	// Token: 0x040029E7 RID: 10727
	private Transform mTrans;

	// Token: 0x040029E8 RID: 10728
	private UIScrollView mDrag;

	// Token: 0x020005C3 RID: 1475
	// (Invoke) Token: 0x060029E1 RID: 10721
	public delegate void OnFinished();
}
