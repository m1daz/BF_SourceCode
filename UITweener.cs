using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020005F6 RID: 1526
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06002B9A RID: 11162 RVA: 0x0013EE68 File Offset: 0x0013D268
	public float amountPerDelta
	{
		get
		{
			if (this.duration == 0f)
			{
				return 1000f;
			}
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs(1f / this.duration) * Mathf.Sign(this.mAmountPerDelta);
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06002B9B RID: 11163 RVA: 0x0013EED1 File Offset: 0x0013D2D1
	// (set) Token: 0x06002B9C RID: 11164 RVA: 0x0013EED9 File Offset: 0x0013D2D9
	public float tweenFactor
	{
		get
		{
			return this.mFactor;
		}
		set
		{
			this.mFactor = Mathf.Clamp01(value);
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06002B9D RID: 11165 RVA: 0x0013EEE7 File Offset: 0x0013D2E7
	public Direction direction
	{
		get
		{
			return (this.amountPerDelta >= 0f) ? Direction.Forward : Direction.Reverse;
		}
	}

	// Token: 0x06002B9E RID: 11166 RVA: 0x0013EF00 File Offset: 0x0013D300
	private void Reset()
	{
		if (!this.mStarted)
		{
			this.SetStartToCurrentValue();
			this.SetEndToCurrentValue();
		}
	}

	// Token: 0x06002B9F RID: 11167 RVA: 0x0013EF19 File Offset: 0x0013D319
	protected virtual void Start()
	{
		this.DoUpdate();
	}

	// Token: 0x06002BA0 RID: 11168 RVA: 0x0013EF21 File Offset: 0x0013D321
	protected void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.DoUpdate();
		}
	}

	// Token: 0x06002BA1 RID: 11169 RVA: 0x0013EF34 File Offset: 0x0013D334
	protected void FixedUpdate()
	{
		if (this.useFixedUpdate)
		{
			this.DoUpdate();
		}
	}

	// Token: 0x06002BA2 RID: 11170 RVA: 0x0013EF48 File Offset: 0x0013D348
	protected void DoUpdate()
	{
		float num = (!this.ignoreTimeScale || this.useFixedUpdate) ? Time.deltaTime : Time.unscaledDeltaTime;
		float num2 = (!this.ignoreTimeScale || this.useFixedUpdate) ? Time.time : Time.unscaledTime;
		if (!this.mStarted)
		{
			num = 0f;
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += ((this.duration != 0f) ? (this.amountPerDelta * num * this.timeScale) : 1f);
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		if (this.style == UITweener.Style.Once && (this.duration == 0f || this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			base.enabled = false;
			if (UITweener.current != this)
			{
				UITweener uitweener = UITweener.current;
				UITweener.current = this;
				if (this.onFinished != null)
				{
					this.mTemp = this.onFinished;
					this.onFinished = new List<EventDelegate>();
					EventDelegate.Execute(this.mTemp);
					for (int i = 0; i < this.mTemp.Count; i++)
					{
						EventDelegate eventDelegate = this.mTemp[i];
						if (eventDelegate != null && !eventDelegate.oneShot)
						{
							EventDelegate.Add(this.onFinished, eventDelegate, eventDelegate.oneShot);
						}
					}
					this.mTemp = null;
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				UITweener.current = uitweener;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x06002BA3 RID: 11171 RVA: 0x0013F21E File Offset: 0x0013D61E
	public void SetOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x06002BA4 RID: 11172 RVA: 0x0013F22D File Offset: 0x0013D62D
	public void SetOnFinished(EventDelegate del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x06002BA5 RID: 11173 RVA: 0x0013F23B File Offset: 0x0013D63B
	public void AddOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x06002BA6 RID: 11174 RVA: 0x0013F24A File Offset: 0x0013D64A
	public void AddOnFinished(EventDelegate del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x06002BA7 RID: 11175 RVA: 0x0013F258 File Offset: 0x0013D658
	public void RemoveOnFinished(EventDelegate del)
	{
		if (this.onFinished != null)
		{
			this.onFinished.Remove(del);
		}
		if (this.mTemp != null)
		{
			this.mTemp.Remove(del);
		}
	}

	// Token: 0x06002BA8 RID: 11176 RVA: 0x0013F28A File Offset: 0x0013D68A
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x06002BA9 RID: 11177 RVA: 0x0013F294 File Offset: 0x0013D694
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.5707964f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.5707964f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.2831855f) / 6.2831855f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve == null) ? num : this.animationCurve.Evaluate(num), isFinished);
	}

	// Token: 0x06002BAA RID: 11178 RVA: 0x0013F3E8 File Offset: 0x0013D7E8
	private float BounceLogic(float val)
	{
		if (val < 0.363636f)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
		}
		else if (val < 0.90909f)
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
		}
		return val;
	}

	// Token: 0x06002BAB RID: 11179 RVA: 0x0013F47F File Offset: 0x0013D87F
	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x06002BAC RID: 11180 RVA: 0x0013F488 File Offset: 0x0013D888
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x0013F491 File Offset: 0x0013D891
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x0013F49C File Offset: 0x0013D89C
	public virtual void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		if (!base.enabled)
		{
			base.enabled = true;
			this.mStarted = false;
		}
		this.DoUpdate();
	}

	// Token: 0x06002BAF RID: 11183 RVA: 0x0013F4EC File Offset: 0x0013D8EC
	public void ResetToBeginning()
	{
		this.mStarted = false;
		this.mFactor = ((this.amountPerDelta >= 0f) ? 0f : 1f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x06002BB0 RID: 11184 RVA: 0x0013F527 File Offset: 0x0013D927
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x06002BB1 RID: 11185
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x06002BB2 RID: 11186 RVA: 0x0013F564 File Offset: 0x0013D964
	public static T Begin<T>(GameObject go, float duration, float delay = 0f) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t != null && t.tweenGroup != 0)
		{
			t = (T)((object)null);
			T[] components = go.GetComponents<T>();
			int i = 0;
			int num = components.Length;
			while (i < num)
			{
				t = components[i];
				if (t != null && t.tweenGroup == 0)
				{
					break;
				}
				t = (T)((object)null);
				i++;
			}
		}
		if (t == null)
		{
			t = go.AddComponent<T>();
			if (t == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Unable to add ",
					typeof(T),
					" to ",
					NGUITools.GetHierarchy(go)
				}), go);
				return (T)((object)null);
			}
		}
		t.mStarted = false;
		t.mFactor = 0f;
		t.duration = duration;
		t.mDuration = duration;
		t.delay = delay;
		t.mAmountPerDelta = ((duration <= 0f) ? 1000f : Mathf.Abs(1f / duration));
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.onFinished.Clear();
		if (t.mTemp != null)
		{
			t.mTemp.Clear();
		}
		t.enabled = true;
		return t;
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x0013F788 File Offset: 0x0013DB88
	public virtual void SetStartToCurrentValue()
	{
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x0013F78A File Offset: 0x0013DB8A
	public virtual void SetEndToCurrentValue()
	{
	}

	// Token: 0x04002B12 RID: 11026
	public static UITweener current;

	// Token: 0x04002B13 RID: 11027
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x04002B14 RID: 11028
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x04002B15 RID: 11029
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x04002B16 RID: 11030
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x04002B17 RID: 11031
	[HideInInspector]
	public float delay;

	// Token: 0x04002B18 RID: 11032
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x04002B19 RID: 11033
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x04002B1A RID: 11034
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x04002B1B RID: 11035
	[Tooltip("By default, Update() will be used for tweening. Setting this to 'true' will make the tween happen in FixedUpdate() insted.")]
	public bool useFixedUpdate;

	// Token: 0x04002B1C RID: 11036
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04002B1D RID: 11037
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04002B1E RID: 11038
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04002B1F RID: 11039
	[NonSerialized]
	public float timeScale = 1f;

	// Token: 0x04002B20 RID: 11040
	private bool mStarted;

	// Token: 0x04002B21 RID: 11041
	private float mStartTime;

	// Token: 0x04002B22 RID: 11042
	private float mDuration;

	// Token: 0x04002B23 RID: 11043
	private float mAmountPerDelta = 1000f;

	// Token: 0x04002B24 RID: 11044
	private float mFactor;

	// Token: 0x04002B25 RID: 11045
	private List<EventDelegate> mTemp;

	// Token: 0x020005F7 RID: 1527
	[DoNotObfuscateNGUI]
	public enum Method
	{
		// Token: 0x04002B27 RID: 11047
		Linear,
		// Token: 0x04002B28 RID: 11048
		EaseIn,
		// Token: 0x04002B29 RID: 11049
		EaseOut,
		// Token: 0x04002B2A RID: 11050
		EaseInOut,
		// Token: 0x04002B2B RID: 11051
		BounceIn,
		// Token: 0x04002B2C RID: 11052
		BounceOut
	}

	// Token: 0x020005F8 RID: 1528
	[DoNotObfuscateNGUI]
	public enum Style
	{
		// Token: 0x04002B2E RID: 11054
		Once,
		// Token: 0x04002B2F RID: 11055
		Loop,
		// Token: 0x04002B30 RID: 11056
		PingPong
	}
}
