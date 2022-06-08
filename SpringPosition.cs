using System;
using UnityEngine;

// Token: 0x020005E4 RID: 1508
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x06002B03 RID: 11011 RVA: 0x0013EB2E File Offset: 0x0013CF2E
	private void Start()
	{
		this.mTrans = base.transform;
		if (this.updateScrollView)
		{
			this.mSv = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x0013EB58 File Offset: 0x0013CF58
	private void OnEnable()
	{
		this.mThreshold = 0f;
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x0013EB68 File Offset: 0x0013CF68
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).sqrMagnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).sqrMagnitude)
			{
				this.mTrans.position = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).sqrMagnitude * 1E-05f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).sqrMagnitude)
			{
				this.mTrans.localPosition = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		if (this.mSv != null)
		{
			this.mSv.UpdateScrollbars(true);
		}
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x0013ED10 File Offset: 0x0013D110
	private void NotifyListeners()
	{
		SpringPosition.current = this;
		if (this.onFinished != null)
		{
			this.onFinished();
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
		{
			this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
		}
		SpringPosition.current = null;
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x0013ED74 File Offset: 0x0013D174
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x04002AB4 RID: 10932
	public static SpringPosition current;

	// Token: 0x04002AB5 RID: 10933
	public Vector3 target = Vector3.zero;

	// Token: 0x04002AB6 RID: 10934
	public float strength = 10f;

	// Token: 0x04002AB7 RID: 10935
	public bool worldSpace;

	// Token: 0x04002AB8 RID: 10936
	public bool ignoreTimeScale;

	// Token: 0x04002AB9 RID: 10937
	public bool updateScrollView;

	// Token: 0x04002ABA RID: 10938
	public SpringPosition.OnFinished onFinished;

	// Token: 0x04002ABB RID: 10939
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x04002ABC RID: 10940
	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04002ABD RID: 10941
	private Transform mTrans;

	// Token: 0x04002ABE RID: 10942
	private float mThreshold;

	// Token: 0x04002ABF RID: 10943
	private UIScrollView mSv;

	// Token: 0x020005E5 RID: 1509
	// (Invoke) Token: 0x06002B09 RID: 11017
	public delegate void OnFinished();
}
