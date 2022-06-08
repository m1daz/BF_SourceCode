using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000583 RID: 1411
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06002777 RID: 10103 RVA: 0x001224E9 File Offset: 0x001208E9
	// (set) Token: 0x06002778 RID: 10104 RVA: 0x00122524 File Offset: 0x00120924
	public UnityEngine.Object ambigiousFont
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.trueTypeFont;
			}
			if (this.bitmapFont != null)
			{
				return this.bitmapFont;
			}
			return this.font;
		}
		set
		{
			if (value is Font)
			{
				this.trueTypeFont = (value as Font);
				this.bitmapFont = null;
				this.font = null;
			}
			else if (value is UIFont)
			{
				this.bitmapFont = (value as UIFont);
				this.trueTypeFont = null;
				this.font = null;
			}
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06002779 RID: 10105 RVA: 0x00122580 File Offset: 0x00120980
	// (set) Token: 0x0600277A RID: 10106 RVA: 0x00122588 File Offset: 0x00120988
	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public UIPopupList.LegacyEvent onSelectionChange
	{
		get
		{
			return this.mLegacyEvent;
		}
		set
		{
			this.mLegacyEvent = value;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x0600277B RID: 10107 RVA: 0x00122591 File Offset: 0x00120991
	public static bool isOpen
	{
		get
		{
			return UIPopupList.current != null && (UIPopupList.mChild != null || UIPopupList.mFadeOutComplete > Time.unscaledTime);
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x0600277C RID: 10108 RVA: 0x001225C5 File Offset: 0x001209C5
	// (set) Token: 0x0600277D RID: 10109 RVA: 0x001225CD File Offset: 0x001209CD
	public virtual string value
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x0600277E RID: 10110 RVA: 0x001225D8 File Offset: 0x001209D8
	public virtual object data
	{
		get
		{
			int num = this.items.IndexOf(this.mSelectedItem);
			return (num <= -1 || num >= this.itemData.Count) ? null : this.itemData[num];
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x0600277F RID: 10111 RVA: 0x00122624 File Offset: 0x00120A24
	public Action callback
	{
		get
		{
			int num = this.items.IndexOf(this.mSelectedItem);
			return (num <= -1 || num >= this.itemCallbacks.Count) ? null : this.itemCallbacks[num];
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06002780 RID: 10112 RVA: 0x00122670 File Offset: 0x00120A70
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06002781 RID: 10113 RVA: 0x001226B3 File Offset: 0x00120AB3
	protected bool isValid
	{
		get
		{
			return this.bitmapFont != null || this.trueTypeFont != null;
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06002782 RID: 10114 RVA: 0x001226D5 File Offset: 0x00120AD5
	protected int activeFontSize
	{
		get
		{
			return (!(this.trueTypeFont != null) && !(this.bitmapFont == null)) ? this.bitmapFont.defaultSize : this.fontSize;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06002783 RID: 10115 RVA: 0x00122710 File Offset: 0x00120B10
	protected float activeFontScale
	{
		get
		{
			return (!(this.trueTypeFont != null) && !(this.bitmapFont == null)) ? ((float)this.fontSize / (float)this.bitmapFont.defaultSize) : 1f;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06002784 RID: 10116 RVA: 0x00122760 File Offset: 0x00120B60
	protected float fitScale
	{
		get
		{
			if (this.separatePanel)
			{
				float num = (float)this.items.Count * ((float)this.fontSize + this.padding.y) + this.padding.y;
				float y = NGUITools.screenSize.y;
				if (num > y)
				{
					return y / num;
				}
			}
			else if (this.mPanel != null && this.mPanel.anchorCamera != null && this.mPanel.anchorCamera.orthographic)
			{
				float num2 = (float)this.items.Count * ((float)this.fontSize + this.padding.y) + this.padding.y;
				float height = this.mPanel.height;
				if (num2 > height)
				{
					return height / num2;
				}
			}
			return 1f;
		}
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x0012284C File Offset: 0x00120C4C
	public void Set(string value, bool notify = true)
	{
		if (this.mSelectedItem != value)
		{
			this.mSelectedItem = value;
			if (this.mSelectedItem == null)
			{
				return;
			}
			if (notify && this.mSelectedItem != null)
			{
				this.TriggerCallbacks();
			}
			if (!this.keepValue)
			{
				this.mSelectedItem = null;
			}
		}
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x001228A6 File Offset: 0x00120CA6
	public virtual void Clear()
	{
		this.items.Clear();
		this.itemData.Clear();
		this.itemCallbacks.Clear();
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x001228C9 File Offset: 0x00120CC9
	public virtual void AddItem(string text)
	{
		this.items.Add(text);
		this.itemData.Add(text);
		this.itemCallbacks.Add(null);
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x001228EF File Offset: 0x00120CEF
	public virtual void AddItem(string text, Action del)
	{
		this.items.Add(text);
		this.itemCallbacks.Add(del);
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x00122909 File Offset: 0x00120D09
	public virtual void AddItem(string text, object data, Action del = null)
	{
		this.items.Add(text);
		this.itemData.Add(data);
		this.itemCallbacks.Add(del);
	}

	// Token: 0x0600278A RID: 10122 RVA: 0x00122930 File Offset: 0x00120D30
	public virtual void RemoveItem(string text)
	{
		int num = this.items.IndexOf(text);
		if (num != -1)
		{
			this.items.RemoveAt(num);
			this.itemData.RemoveAt(num);
			if (num < this.itemCallbacks.Count)
			{
				this.itemCallbacks.RemoveAt(num);
			}
		}
	}

	// Token: 0x0600278B RID: 10123 RVA: 0x00122988 File Offset: 0x00120D88
	public virtual void RemoveItemByData(object data)
	{
		int num = this.itemData.IndexOf(data);
		if (num != -1)
		{
			this.items.RemoveAt(num);
			this.itemData.RemoveAt(num);
			if (num < this.itemCallbacks.Count)
			{
				this.itemCallbacks.RemoveAt(num);
			}
		}
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x001229E0 File Offset: 0x00120DE0
	protected void TriggerCallbacks()
	{
		if (!this.mExecuting)
		{
			this.mExecuting = true;
			UIPopupList uipopupList = UIPopupList.current;
			UIPopupList.current = this;
			if (this.mLegacyEvent != null)
			{
				this.mLegacyEvent(this.mSelectedItem);
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				EventDelegate.Execute(this.onChange);
			}
			else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, SendMessageOptions.DontRequireReceiver);
			}
			Action callback = this.callback;
			if (callback != null)
			{
				callback();
			}
			UIPopupList.current = uipopupList;
			this.mExecuting = false;
		}
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x00122AA0 File Offset: 0x00120EA0
	protected virtual void OnEnable()
	{
		if (EventDelegate.IsValid(this.onChange))
		{
			this.eventReceiver = null;
			this.functionName = null;
		}
		if (this.font != null)
		{
			if (this.font.isDynamic)
			{
				this.trueTypeFont = this.font.dynamicFont;
				this.fontStyle = this.font.dynamicFontStyle;
				this.mUseDynamicFont = true;
			}
			else if (this.bitmapFont == null)
			{
				this.bitmapFont = this.font;
				this.mUseDynamicFont = false;
			}
			this.font = null;
		}
		if (this.textScale != 0f)
		{
			this.fontSize = ((!(this.bitmapFont != null)) ? 16 : Mathf.RoundToInt((float)this.bitmapFont.defaultSize * this.textScale));
			this.textScale = 0f;
		}
		if (this.trueTypeFont == null && this.bitmapFont != null && this.bitmapFont.isDynamic && this.bitmapFont.replacement == null)
		{
			this.trueTypeFont = this.bitmapFont.dynamicFont;
			this.bitmapFont = null;
		}
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x00122BF8 File Offset: 0x00120FF8
	protected virtual void OnValidate()
	{
		Font x = this.trueTypeFont;
		UIFont uifont = this.bitmapFont;
		this.bitmapFont = null;
		this.trueTypeFont = null;
		if (x != null && (uifont == null || !this.mUseDynamicFont))
		{
			this.bitmapFont = null;
			this.trueTypeFont = x;
			this.mUseDynamicFont = true;
		}
		else if (uifont != null)
		{
			if (uifont.replacement == null)
			{
				if (uifont.isDynamic)
				{
					this.trueTypeFont = uifont.dynamicFont;
					this.fontStyle = uifont.dynamicFontStyle;
					this.fontSize = uifont.defaultSize;
					this.mUseDynamicFont = true;
				}
				else
				{
					this.bitmapFont = uifont;
					this.mUseDynamicFont = false;
				}
			}
		}
		else
		{
			this.trueTypeFont = x;
			this.mUseDynamicFont = true;
		}
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x00122CD8 File Offset: 0x001210D8
	public virtual void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		this.mStarted = true;
		if (this.keepValue)
		{
			string value = this.mSelectedItem;
			this.mSelectedItem = null;
			this.value = value;
		}
		else
		{
			this.mSelectedItem = null;
		}
		if (this.textLabel != null)
		{
			EventDelegate.Add(this.onChange, new EventDelegate.Callback(this.textLabel.SetCurrentSelection));
			this.textLabel = null;
		}
	}

	// Token: 0x06002790 RID: 10128 RVA: 0x00122D59 File Offset: 0x00121159
	protected virtual void OnLocalize()
	{
		if (this.isLocalized)
		{
			this.TriggerCallbacks();
		}
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x00122D6C File Offset: 0x0012116C
	protected virtual void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			this.mHighlightedLabel = lbl;
			Vector3 highlightPosition = this.GetHighlightPosition();
			if (!instant && this.isAnimated)
			{
				TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
				if (!this.mTweening)
				{
					this.mTweening = true;
					base.StartCoroutine("UpdateTweenPosition");
				}
			}
			else
			{
				this.mHighlight.cachedTransform.localPosition = highlightPosition;
			}
		}
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x00122DFC File Offset: 0x001211FC
	protected virtual Vector3 GetHighlightPosition()
	{
		if (this.mHighlightedLabel == null || this.mHighlight == null)
		{
			return Vector3.zero;
		}
		Vector4 border = this.mHighlight.border;
		float num = (!(this.atlas != null)) ? 1f : this.atlas.pixelSize;
		float num2 = border.x * num;
		float y = border.w * num;
		return this.mHighlightedLabel.cachedTransform.localPosition + new Vector3(-num2, y, 1f);
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x00122E9C File Offset: 0x0012129C
	protected virtual IEnumerator UpdateTweenPosition()
	{
		if (this.mHighlight != null && this.mHighlightedLabel != null)
		{
			TweenPosition tp = this.mHighlight.GetComponent<TweenPosition>();
			while (tp != null && tp.enabled)
			{
				tp.to = this.GetHighlightPosition();
				yield return null;
			}
		}
		this.mTweening = false;
		yield break;
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x00122EB8 File Offset: 0x001212B8
	protected virtual void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x00122EDA File Offset: 0x001212DA
	protected virtual void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed && this.selection == UIPopupList.Selection.OnPress)
		{
			this.OnItemClick(go);
		}
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x00122EF4 File Offset: 0x001212F4
	protected virtual void OnItemClick(GameObject go)
	{
		this.Select(go.GetComponent<UILabel>(), true);
		UIEventListener component = go.GetComponent<UIEventListener>();
		this.value = (component.parameter as string);
		UIPlaySound[] components = base.GetComponents<UIPlaySound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIPlaySound uiplaySound = components[i];
			if (uiplaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uiplaySound.audioClip, uiplaySound.volume, 1f);
			}
			i++;
		}
		this.CloseSelf();
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x00122F73 File Offset: 0x00121373
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x00122F80 File Offset: 0x00121380
	protected virtual void OnNavigate(KeyCode key)
	{
		if (base.enabled && UIPopupList.current == this)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (num == -1)
			{
				num = 0;
			}
			if (key == KeyCode.UpArrow)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
				}
			}
			else if (key == KeyCode.DownArrow && num + 1 < this.mLabelList.Count)
			{
				this.Select(this.mLabelList[num + 1], false);
			}
		}
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x00123028 File Offset: 0x00121428
	protected virtual void OnKey(KeyCode key)
	{
		if (base.enabled && UIPopupList.current == this && (key == UICamera.current.cancelKey0 || key == UICamera.current.cancelKey1))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x00123077 File Offset: 0x00121477
	protected virtual void OnDisable()
	{
		this.CloseSelf();
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x00123080 File Offset: 0x00121480
	protected virtual void OnSelect(bool isSelected)
	{
		if (!isSelected)
		{
			GameObject selectedObject = UICamera.selectedObject;
			if (selectedObject == null || (!(selectedObject == UIPopupList.mChild) && (!(UIPopupList.mChild != null) || !(selectedObject != null) || !NGUITools.IsChild(UIPopupList.mChild.transform, selectedObject.transform))))
			{
				this.CloseSelf();
			}
		}
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x001230F1 File Offset: 0x001214F1
	public static void Close()
	{
		if (UIPopupList.current != null)
		{
			UIPopupList.current.CloseSelf();
			UIPopupList.current = null;
		}
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x00123114 File Offset: 0x00121514
	public virtual void CloseSelf()
	{
		if (UIPopupList.mChild != null && UIPopupList.current == this)
		{
			base.StopCoroutine("CloseIfUnselected");
			this.mSelection = null;
			this.mLabelList.Clear();
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = UIPopupList.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				Collider[] componentsInChildren2 = UIPopupList.mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				int num2 = componentsInChildren2.Length;
				while (j < num2)
				{
					componentsInChildren2[j].enabled = false;
					j++;
				}
				UnityEngine.Object.Destroy(UIPopupList.mChild, 0.15f);
				UIPopupList.mFadeOutComplete = Time.unscaledTime + Mathf.Max(0.1f, 0.15f);
			}
			else
			{
				UnityEngine.Object.Destroy(UIPopupList.mChild);
				UIPopupList.mFadeOutComplete = Time.unscaledTime + 0.1f;
			}
			this.mBackground = null;
			this.mHighlight = null;
			UIPopupList.mChild = null;
			UIPopupList.current = null;
		}
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x00123250 File Offset: 0x00121650
	protected virtual void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x001232A0 File Offset: 0x001216A0
	protected virtual void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x00123318 File Offset: 0x00121718
	protected virtual void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float fitScale = this.fitScale;
		float num = (float)this.activeFontSize * this.activeFontScale + this.mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(fitScale, fitScale * num / (float)widget.height, fitScale);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - fitScale * (float)widget.height + fitScale * num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x001233D3 File Offset: 0x001217D3
	protected void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x001233E8 File Offset: 0x001217E8
	protected virtual void OnClick()
	{
		if (this.mOpenFrame == Time.frameCount)
		{
			return;
		}
		if (UIPopupList.mChild == null)
		{
			if (this.openOn == UIPopupList.OpenOn.DoubleClick || this.openOn == UIPopupList.OpenOn.Manual)
			{
				return;
			}
			if (this.openOn == UIPopupList.OpenOn.RightClick && UICamera.currentTouchID != -2)
			{
				return;
			}
			this.Show();
		}
		else if (this.mHighlightedLabel != null)
		{
			this.OnItemPress(this.mHighlightedLabel.gameObject, true);
		}
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x00123476 File Offset: 0x00121876
	protected virtual void OnDoubleClick()
	{
		if (this.openOn == UIPopupList.OpenOn.DoubleClick)
		{
			this.Show();
		}
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x0012348C File Offset: 0x0012188C
	private IEnumerator CloseIfUnselected()
	{
		GameObject sel;
		do
		{
			yield return null;
			sel = UICamera.selectedObject;
		}
		while (!(sel != this.mSelection) || (!(sel == null) && (sel == UIPopupList.mChild || NGUITools.IsChild(UIPopupList.mChild.transform, sel.transform))));
		this.CloseSelf();
		yield break;
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x001234A8 File Offset: 0x001218A8
	public virtual void Show()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && UIPopupList.mChild == null && this.isValid && this.items.Count > 0)
		{
			this.mLabelList.Clear();
			base.StopCoroutine("CloseIfUnselected");
			UICamera.selectedObject = (UICamera.hoveredObject ?? base.gameObject);
			this.mSelection = UICamera.selectedObject;
			this.source = this.mSelection;
			if (this.source == null)
			{
				Debug.LogError("Popup list needs a source object...");
				return;
			}
			this.mOpenFrame = Time.frameCount;
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform);
				if (this.mPanel == null)
				{
					return;
				}
			}
			UIPopupList.mChild = new GameObject("Drop-down List");
			UIPopupList.mChild.layer = base.gameObject.layer;
			if (this.separatePanel)
			{
				if (base.GetComponent<Collider>() != null)
				{
					Rigidbody rigidbody = UIPopupList.mChild.AddComponent<Rigidbody>();
					rigidbody.isKinematic = true;
				}
				else if (base.GetComponent<Collider2D>() != null)
				{
					Rigidbody2D rigidbody2D = UIPopupList.mChild.AddComponent<Rigidbody2D>();
					rigidbody2D.isKinematic = true;
				}
				UIPanel uipanel = UIPopupList.mChild.AddComponent<UIPanel>();
				uipanel.depth = 1000000;
				uipanel.sortingOrder = this.mPanel.sortingOrder;
			}
			UIPopupList.current = this;
			Transform cachedTransform = this.mPanel.cachedTransform;
			Transform transform = UIPopupList.mChild.transform;
			transform.parent = cachedTransform;
			Transform parent = cachedTransform;
			if (this.separatePanel)
			{
				UIRoot uiroot = this.mPanel.GetComponentInParent<UIRoot>();
				if (uiroot == null && UIRoot.list.Count != 0)
				{
					uiroot = UIRoot.list[0];
				}
				if (uiroot != null)
				{
					parent = uiroot.transform;
				}
			}
			Vector3 vector;
			Vector3 vector2;
			if (this.openOn == UIPopupList.OpenOn.Manual && this.mSelection != base.gameObject)
			{
				this.startingPosition = UICamera.lastEventPosition;
				vector = cachedTransform.InverseTransformPoint(this.mPanel.anchorCamera.ScreenToWorldPoint(this.startingPosition));
				vector2 = vector;
				transform.localPosition = vector;
				this.startingPosition = transform.position;
			}
			else
			{
				Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(cachedTransform, base.transform, false, false);
				vector = bounds.min;
				vector2 = bounds.max;
				transform.localPosition = vector;
				this.startingPosition = transform.position;
			}
			base.StartCoroutine("CloseIfUnselected");
			float fitScale = this.fitScale;
			transform.localRotation = Quaternion.identity;
			transform.localScale = new Vector3(fitScale, fitScale, fitScale);
			int num = (!this.separatePanel) ? NGUITools.CalculateNextDepth(this.mPanel.gameObject) : 0;
			if (this.background2DSprite != null)
			{
				UI2DSprite ui2DSprite = UIPopupList.mChild.AddWidget(num);
				ui2DSprite.sprite2D = this.background2DSprite;
				this.mBackground = ui2DSprite;
			}
			else
			{
				if (!(this.atlas != null))
				{
					return;
				}
				this.mBackground = UIPopupList.mChild.AddSprite(this.atlas, this.backgroundSprite, num);
			}
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(this.mSelection.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(this.startingPosition).y < 0.5f);
				}
			}
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, (!flag) ? ((float)this.overlap) : (border.y * 2f - (float)this.overlap), 0f);
			if (this.highlight2DSprite != null)
			{
				UI2DSprite ui2DSprite2 = UIPopupList.mChild.AddWidget(num + 1);
				ui2DSprite2.sprite2D = this.highlight2DSprite;
				this.mHighlight = ui2DSprite2;
			}
			else
			{
				if (!(this.atlas != null))
				{
					return;
				}
				this.mHighlight = UIPopupList.mChild.AddSprite(this.atlas, this.highlightSprite, num + 1);
			}
			float num2 = 0f;
			float num3 = 0f;
			if (this.mHighlight.hasBorder)
			{
				num2 = this.mHighlight.border.w;
				num3 = this.mHighlight.border.x;
			}
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			float num4 = (float)this.activeFontSize * this.activeFontScale;
			float num5 = num4 + this.padding.y;
			float num6 = 0f;
			float num7 = (!flag) ? (-this.padding.y - border.y + (float)this.overlap) : (border.y - this.padding.y - (float)this.overlap);
			float num8 = border.y * 2f + this.padding.y;
			List<UILabel> list = new List<UILabel>();
			if (!this.items.Contains(this.mSelectedItem))
			{
				this.mSelectedItem = null;
			}
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = UIPopupList.mChild.AddWidget(this.mBackground.depth + 2);
				uilabel.name = i.ToString();
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.bitmapFont = this.bitmapFont;
				uilabel.trueTypeFont = this.trueTypeFont;
				uilabel.fontSize = this.fontSize;
				uilabel.fontStyle = this.fontStyle;
				uilabel.text = ((!this.isLocalized) ? text : Localization.Get(text, true));
				uilabel.modifier = this.textModifier;
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x + this.padding.x - uilabel.pivotOffset.x, num7, -1f);
				uilabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uilabel.alignment = this.alignment;
				uilabel.symbolStyle = NGUIText.SymbolStyle.Colored;
				list.Add(uilabel);
				num8 += num5;
				num7 -= num5;
				num6 = Mathf.Max(num6, uilabel.printedSize.x);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.onClick = new UIEventListener.VoidDelegate(this.OnItemClick);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(this.mSelectedItem)))
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num6 = Mathf.Max(num6, vector2.x - vector.x - (border.x + this.padding.x) * 2f);
			float num9 = num6;
			Vector3 vector3 = new Vector3(num9 * 0.5f, -num4 * 0.5f, 0f);
			Vector3 vector4 = new Vector3(num9, num4 + this.padding.y, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				UILabel uilabel2 = list[j];
				NGUITools.AddWidgetCollider(uilabel2.gameObject);
				uilabel2.autoResizeBoxCollider = false;
				BoxCollider component = uilabel2.GetComponent<BoxCollider>();
				if (component != null)
				{
					vector3.z = component.center.z;
					component.center = vector3;
					component.size = vector4;
				}
				else
				{
					BoxCollider2D component2 = uilabel2.GetComponent<BoxCollider2D>();
					component2.offset = vector3;
					component2.size = vector4;
				}
				j++;
			}
			int width = Mathf.RoundToInt(num6);
			num6 += (border.x + this.padding.x) * 2f;
			num7 -= border.y;
			this.mBackground.width = Mathf.RoundToInt(num6);
			this.mBackground.height = Mathf.RoundToInt(num8);
			int k = 0;
			int count3 = list.Count;
			while (k < count3)
			{
				UILabel uilabel3 = list[k];
				uilabel3.overflowMethod = UILabel.Overflow.ShrinkContent;
				uilabel3.width = width;
				k++;
			}
			float num10 = (!(this.atlas != null)) ? 2f : (2f * this.atlas.pixelSize);
			float f = num6 - (border.x + this.padding.x) * 2f + num3 * num10;
			float f2 = num4 + num2 * num10;
			this.mHighlight.width = Mathf.RoundToInt(f);
			this.mHighlight.height = Mathf.RoundToInt(f2);
			if (this.isAnimated)
			{
				this.AnimateColor(this.mBackground);
				if (Time.timeScale == 0f || Time.timeScale >= 0.1f)
				{
					float bottom = num7 + num4;
					this.Animate(this.mHighlight, flag, bottom);
					int l = 0;
					int count4 = list.Count;
					while (l < count4)
					{
						this.Animate(list[l], flag, bottom);
						l++;
					}
					this.AnimateScale(this.mBackground, flag, bottom);
				}
			}
			if (flag)
			{
				float num11 = border.y * fitScale;
				vector.y = vector2.y - border.y * fitScale;
				vector2.y = vector.y + ((float)this.mBackground.height - border.y * 2f) * fitScale;
				vector2.x = vector.x + (float)this.mBackground.width * fitScale;
				transform.localPosition = new Vector3(vector.x, vector2.y - num11, vector.z);
			}
			else
			{
				vector2.y = vector.y + border.y * fitScale;
				vector.y = vector2.y - (float)this.mBackground.height * fitScale;
				vector2.x = vector.x + (float)this.mBackground.width * fitScale;
			}
			UIPanel uipanel2 = this.mPanel;
			for (;;)
			{
				UIRect parent2 = uipanel2.parent;
				if (parent2 == null)
				{
					break;
				}
				UIPanel componentInParent = parent2.GetComponentInParent<UIPanel>();
				if (componentInParent == null)
				{
					break;
				}
				uipanel2 = componentInParent;
			}
			if (cachedTransform != null)
			{
				vector = cachedTransform.TransformPoint(vector);
				vector2 = cachedTransform.TransformPoint(vector2);
				vector = uipanel2.cachedTransform.InverseTransformPoint(vector);
				vector2 = uipanel2.cachedTransform.InverseTransformPoint(vector2);
				float pixelSizeAdjustment = UIRoot.GetPixelSizeAdjustment(base.gameObject);
				vector /= pixelSizeAdjustment;
				vector2 /= pixelSizeAdjustment;
			}
			Vector3 b = uipanel2.CalculateConstrainOffset(vector, vector2);
			Vector3 localPosition = transform.localPosition + b;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			transform.localPosition = localPosition;
			transform.parent = parent;
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x0400283D RID: 10301
	public static UIPopupList current;

	// Token: 0x0400283E RID: 10302
	protected static GameObject mChild;

	// Token: 0x0400283F RID: 10303
	protected static float mFadeOutComplete;

	// Token: 0x04002840 RID: 10304
	private const float animSpeed = 0.15f;

	// Token: 0x04002841 RID: 10305
	public UIAtlas atlas;

	// Token: 0x04002842 RID: 10306
	public UIFont bitmapFont;

	// Token: 0x04002843 RID: 10307
	public Font trueTypeFont;

	// Token: 0x04002844 RID: 10308
	public int fontSize = 16;

	// Token: 0x04002845 RID: 10309
	public FontStyle fontStyle;

	// Token: 0x04002846 RID: 10310
	public string backgroundSprite;

	// Token: 0x04002847 RID: 10311
	public string highlightSprite;

	// Token: 0x04002848 RID: 10312
	public Sprite background2DSprite;

	// Token: 0x04002849 RID: 10313
	public Sprite highlight2DSprite;

	// Token: 0x0400284A RID: 10314
	public UIPopupList.Position position;

	// Token: 0x0400284B RID: 10315
	public UIPopupList.Selection selection;

	// Token: 0x0400284C RID: 10316
	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	// Token: 0x0400284D RID: 10317
	public List<string> items = new List<string>();

	// Token: 0x0400284E RID: 10318
	public List<object> itemData = new List<object>();

	// Token: 0x0400284F RID: 10319
	public List<Action> itemCallbacks = new List<Action>();

	// Token: 0x04002850 RID: 10320
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x04002851 RID: 10321
	public Color textColor = Color.white;

	// Token: 0x04002852 RID: 10322
	public Color backgroundColor = Color.white;

	// Token: 0x04002853 RID: 10323
	public Color highlightColor = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x04002854 RID: 10324
	public bool isAnimated = true;

	// Token: 0x04002855 RID: 10325
	public bool isLocalized;

	// Token: 0x04002856 RID: 10326
	public UILabel.Modifier textModifier;

	// Token: 0x04002857 RID: 10327
	public bool separatePanel = true;

	// Token: 0x04002858 RID: 10328
	public int overlap;

	// Token: 0x04002859 RID: 10329
	public UIPopupList.OpenOn openOn;

	// Token: 0x0400285A RID: 10330
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x0400285B RID: 10331
	[HideInInspector]
	[SerializeField]
	protected string mSelectedItem;

	// Token: 0x0400285C RID: 10332
	[HideInInspector]
	[SerializeField]
	protected UIPanel mPanel;

	// Token: 0x0400285D RID: 10333
	[HideInInspector]
	[SerializeField]
	protected UIBasicSprite mBackground;

	// Token: 0x0400285E RID: 10334
	[HideInInspector]
	[SerializeField]
	protected UIBasicSprite mHighlight;

	// Token: 0x0400285F RID: 10335
	[HideInInspector]
	[SerializeField]
	protected UILabel mHighlightedLabel;

	// Token: 0x04002860 RID: 10336
	[HideInInspector]
	[SerializeField]
	protected List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x04002861 RID: 10337
	[HideInInspector]
	[SerializeField]
	protected float mBgBorder;

	// Token: 0x04002862 RID: 10338
	[Tooltip("Whether the selection will be persistent even after the popup list is closed. By default the selection is cleared when the popup is closed so that the same selection can be chosen again the next time the popup list is opened. If enabled, the selection will persist, but selecting the same choice in succession will not result in the onChange notification being triggered more than once.")]
	public bool keepValue;

	// Token: 0x04002863 RID: 10339
	[NonSerialized]
	protected GameObject mSelection;

	// Token: 0x04002864 RID: 10340
	[NonSerialized]
	protected int mOpenFrame;

	// Token: 0x04002865 RID: 10341
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04002866 RID: 10342
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	// Token: 0x04002867 RID: 10343
	[HideInInspector]
	[SerializeField]
	private float textScale;

	// Token: 0x04002868 RID: 10344
	[HideInInspector]
	[SerializeField]
	private UIFont font;

	// Token: 0x04002869 RID: 10345
	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	// Token: 0x0400286A RID: 10346
	[NonSerialized]
	public Vector3 startingPosition;

	// Token: 0x0400286B RID: 10347
	private UIPopupList.LegacyEvent mLegacyEvent;

	// Token: 0x0400286C RID: 10348
	[NonSerialized]
	protected bool mExecuting;

	// Token: 0x0400286D RID: 10349
	protected bool mUseDynamicFont;

	// Token: 0x0400286E RID: 10350
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x0400286F RID: 10351
	protected bool mTweening;

	// Token: 0x04002870 RID: 10352
	public GameObject source;

	// Token: 0x02000584 RID: 1412
	[DoNotObfuscateNGUI]
	public enum Position
	{
		// Token: 0x04002872 RID: 10354
		Auto,
		// Token: 0x04002873 RID: 10355
		Above,
		// Token: 0x04002874 RID: 10356
		Below
	}

	// Token: 0x02000585 RID: 1413
	[DoNotObfuscateNGUI]
	public enum Selection
	{
		// Token: 0x04002876 RID: 10358
		OnPress,
		// Token: 0x04002877 RID: 10359
		OnClick
	}

	// Token: 0x02000586 RID: 1414
	[DoNotObfuscateNGUI]
	public enum OpenOn
	{
		// Token: 0x04002879 RID: 10361
		ClickOrTap,
		// Token: 0x0400287A RID: 10362
		RightClick,
		// Token: 0x0400287B RID: 10363
		DoubleClick,
		// Token: 0x0400287C RID: 10364
		Manual
	}

	// Token: 0x02000587 RID: 1415
	// (Invoke) Token: 0x060027A8 RID: 10152
	public delegate void LegacyEvent(string val);
}
