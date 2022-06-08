using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200061C RID: 1564
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x00148D11 File Offset: 0x00147111
	// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x00148D2A File Offset: 0x0014712A
	public string defaultText
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x00148D4A File Offset: 0x0014714A
	// (set) Token: 0x06002CCA RID: 11466 RVA: 0x00148D63 File Offset: 0x00147163
	public Color defaultColor
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultColor;
		}
		set
		{
			this.mDefaultColor = value;
			if (!this.isSelected)
			{
				this.label.color = value;
			}
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00148D83 File Offset: 0x00147183
	public bool inputShouldBeHidden
	{
		get
		{
			return this.hideInput && this.label != null && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06002CCC RID: 11468 RVA: 0x00148DC0 File Offset: 0x001471C0
	// (set) Token: 0x06002CCD RID: 11469 RVA: 0x00148DC8 File Offset: 0x001471C8
	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00148DD1 File Offset: 0x001471D1
	// (set) Token: 0x06002CCF RID: 11471 RVA: 0x00148DEA File Offset: 0x001471EA
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x00148DF4 File Offset: 0x001471F4
	public void Set(string value, bool notify = true)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (value == this.value)
		{
			return;
		}
		UIInput.mDrawStart = 0;
		value = this.Validate(value);
		if (this.isSelected && UIInput.mKeyboard != null && this.mCached != value)
		{
			UIInput.mKeyboard.text = value;
			this.mCached = value;
		}
		if (this.mValue != value)
		{
			this.mValue = value;
			this.mLoadSavedValue = false;
			if (this.isSelected)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.mSelectionStart = 0;
					this.mSelectionEnd = 0;
				}
				else
				{
					this.mSelectionStart = value.Length;
					this.mSelectionEnd = this.mSelectionStart;
				}
			}
			else if (this.mStarted)
			{
				this.SaveToPlayerPrefs(value);
			}
			this.UpdateLabel();
			if (notify)
			{
				this.ExecuteOnChange();
			}
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00148EF4 File Offset: 0x001472F4
	// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00148EFC File Offset: 0x001472FC
	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x00148F05 File Offset: 0x00147305
	// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x00148F12 File Offset: 0x00147312
	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x00148F3C File Offset: 0x0014733C
	// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x00148F8B File Offset: 0x0014738B
	public int cursorPosition
	{
		get
		{
			if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
			{
				return this.value.Length;
			}
			return (!this.isSelected) ? this.value.Length : this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
				{
					return;
				}
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x00148FBB File Offset: 0x001473BB
	// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x00148FF5 File Offset: 0x001473F5
	public int selectionStart
	{
		get
		{
			if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
			{
				return 0;
			}
			return (!this.isSelected) ? this.value.Length : this.mSelectionStart;
		}
		set
		{
			if (this.isSelected)
			{
				if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
				{
					return;
				}
				this.mSelectionStart = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x00149028 File Offset: 0x00147428
	// (set) Token: 0x06002CDA RID: 11482 RVA: 0x00149077 File Offset: 0x00147477
	public int selectionEnd
	{
		get
		{
			if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
			{
				return this.value.Length;
			}
			return (!this.isSelected) ? this.value.Length : this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
				{
					return;
				}
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06002CDB RID: 11483 RVA: 0x001490A7 File Offset: 0x001474A7
	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x001490B0 File Offset: 0x001474B0
	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		foreach (char c in val)
		{
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x00149180 File Offset: 0x00147580
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.selectOnTab != null)
		{
			UIKeyNavigation uikeyNavigation = base.GetComponent<UIKeyNavigation>();
			if (uikeyNavigation == null)
			{
				uikeyNavigation = base.gameObject.AddComponent<UIKeyNavigation>();
				uikeyNavigation.onDown = this.selectOnTab;
			}
			this.selectOnTab = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
		}
		else
		{
			this.value = this.mValue.Replace("\\n", "\n");
		}
		this.mStarted = true;
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x00149230 File Offset: 0x00147630
	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.mEllipsis = this.label.overflowEllipsis;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mAlignment = this.label.alignment;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	// Token: 0x06002CDF RID: 11487 RVA: 0x001492F0 File Offset: 0x001476F0
	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
			}
			else
			{
				PlayerPrefs.SetString(this.savedAs, val);
			}
		}
	}

	// Token: 0x06002CE0 RID: 11488 RVA: 0x00149329 File Offset: 0x00147729
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			if (this.label != null)
			{
				this.label.supportEncoding = false;
			}
			this.OnSelectEvent();
		}
		else
		{
			this.OnDeselectEvent();
		}
	}

	// Token: 0x06002CE1 RID: 11489 RVA: 0x00149360 File Offset: 0x00147760
	protected void OnSelectEvent()
	{
		this.mSelectTime = Time.frameCount;
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.mEllipsis = this.label.overflowEllipsis;
			this.label.overflowEllipsis = false;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mSelectMe = Time.frameCount;
		}
	}

	// Token: 0x06002CE2 RID: 11490 RVA: 0x001493E4 File Offset: 0x001477E4
	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.label.overflowEllipsis = this.mEllipsis;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.mKeyboard != null)
			{
				UIInput.mWaitForKeyboard = false;
				UIInput.mKeyboard.active = false;
				UIInput.mKeyboard = null;
			}
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = IMECompositionMode.Auto;
			this.label.alignment = this.mAlignment;
		}
		UIInput.selection = null;
		this.UpdateLabel();
		if (this.submitOnUnselect)
		{
			this.Submit();
		}
	}

	// Token: 0x06002CE3 RID: 11491 RVA: 0x001494EC File Offset: 0x001478EC
	protected virtual void Update()
	{
		if (!this.isSelected || this.mSelectTime == Time.frameCount)
		{
			return;
		}
		if (this.mDoInit)
		{
			this.Init();
		}
		if (UIInput.mWaitForKeyboard)
		{
			if (UIInput.mKeyboard != null && !UIInput.mKeyboard.active)
			{
				return;
			}
			UIInput.mWaitForKeyboard = false;
		}
		if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
		{
			this.mSelectMe = -1;
			this.mSelectionEnd = ((!string.IsNullOrEmpty(this.mValue)) ? this.mValue.Length : 0);
			UIInput.mDrawStart = 0;
			this.mSelectionStart = ((!this.selectAllTextOnFocus) ? this.mSelectionEnd : 0);
			this.label.color = this.activeTextColor;
			RuntimePlatform platform = Application.platform;
			if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.WP8Player || platform == RuntimePlatform.BlackBerryPlayer || platform == RuntimePlatform.MetroPlayerARM || platform == RuntimePlatform.MetroPlayerX64 || platform == RuntimePlatform.MetroPlayerX86)
			{
				TouchScreenKeyboardType touchScreenKeyboardType;
				string text;
				if (this.inputShouldBeHidden)
				{
					TouchScreenKeyboard.hideInput = true;
					touchScreenKeyboardType = (TouchScreenKeyboardType)this.keyboardType;
					text = "|";
				}
				else if (this.inputType == UIInput.InputType.Password)
				{
					TouchScreenKeyboard.hideInput = false;
					touchScreenKeyboardType = (TouchScreenKeyboardType)this.keyboardType;
					text = this.mValue;
					this.mSelectionStart = this.mSelectionEnd;
				}
				else
				{
					TouchScreenKeyboard.hideInput = false;
					touchScreenKeyboardType = (TouchScreenKeyboardType)this.keyboardType;
					text = this.mValue;
					this.mSelectionStart = this.mSelectionEnd;
				}
				UIInput.mWaitForKeyboard = true;
				UIInput.mKeyboard = ((this.inputType != UIInput.InputType.Password) ? TouchScreenKeyboard.Open(text, touchScreenKeyboardType, !this.inputShouldBeHidden && this.inputType == UIInput.InputType.AutoCorrect, this.label.multiLine && !this.hideInput, false, false, this.defaultText) : TouchScreenKeyboard.Open(text, touchScreenKeyboardType, false, false, true));
			}
			else
			{
				Vector2 compositionCursorPos = (!(UICamera.current != null) || !(UICamera.current.cachedCamera != null)) ? this.label.worldCorners[0] : UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]);
				compositionCursorPos.y = (float)Screen.height - compositionCursorPos.y;
				Input.imeCompositionMode = IMECompositionMode.On;
				Input.compositionCursorPos = compositionCursorPos;
			}
			this.UpdateLabel();
			if (string.IsNullOrEmpty(Input.inputString))
			{
				return;
			}
		}
		if (UIInput.mKeyboard != null)
		{
			string text2 = (!UIInput.mKeyboard.done && UIInput.mKeyboard.active) ? UIInput.mKeyboard.text : this.mCached;
			if (this.inputShouldBeHidden)
			{
				if (text2 != "|")
				{
					if (!string.IsNullOrEmpty(text2))
					{
						this.Insert(text2.Substring(1));
					}
					else if (!UIInput.mKeyboard.done && UIInput.mKeyboard.active)
					{
						this.DoBackspace();
					}
					UIInput.mKeyboard.text = "|";
				}
			}
			else if (this.mCached != text2)
			{
				this.mCached = text2;
				if (!UIInput.mKeyboard.done && UIInput.mKeyboard.active)
				{
					this.value = text2;
				}
			}
			if (UIInput.mKeyboard.done || !UIInput.mKeyboard.active)
			{
				if (!UIInput.mKeyboard.wasCanceled)
				{
					this.Submit();
				}
				UIInput.mKeyboard = null;
				this.isSelected = false;
				this.mCached = string.Empty;
			}
		}
		else
		{
			string compositionString = Input.compositionString;
			if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
			{
				foreach (char c in Input.inputString)
				{
					if (c >= ' ')
					{
						if (c != '')
						{
							if (c != '')
							{
								if (c != '')
								{
									if (c != '')
									{
										if (c != '')
										{
											this.Insert(c.ToString());
										}
									}
								}
							}
						}
					}
				}
			}
			if (UIInput.mLastIME != compositionString)
			{
				this.mSelectionEnd = ((!string.IsNullOrEmpty(compositionString)) ? (this.mValue.Length + compositionString.Length) : this.mSelectionStart);
				UIInput.mLastIME = compositionString;
				this.UpdateLabel();
				this.ExecuteOnChange();
			}
		}
		if (this.mCaret != null && this.mNextBlink < RealTime.time)
		{
			this.mNextBlink = RealTime.time + 0.5f;
			this.mCaret.enabled = !this.mCaret.enabled;
		}
		if (this.isSelected && this.mLastAlpha != this.label.finalAlpha)
		{
			this.UpdateLabel();
		}
		if (this.mCam == null)
		{
			this.mCam = UICamera.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mCam != null)
		{
			bool flag = false;
			if (this.label.multiLine)
			{
				bool flag2 = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
				if (this.onReturnKey == UIInput.OnReturnKey.Submit)
				{
					flag = flag2;
				}
				else
				{
					flag = !flag2;
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey0) || (this.mCam.submitKey0 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey0;
					this.Submit();
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey1) || (this.mCam.submitKey1 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey1;
					this.Submit();
				}
			}
			if (!this.mCam.useKeyboard && UICamera.GetKeyUp(KeyCode.Tab))
			{
				this.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x06002CE4 RID: 11492 RVA: 0x00149C24 File Offset: 0x00148024
	private void OnKey(KeyCode key)
	{
		int frameCount = Time.frameCount;
		if (UIInput.mIgnoreKey == frameCount)
		{
			return;
		}
		if (this.mCam != null && (key == this.mCam.cancelKey0 || key == this.mCam.cancelKey1))
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
		}
		else if (key == KeyCode.Tab)
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x06002CE5 RID: 11493 RVA: 0x00149CB8 File Offset: 0x001480B8
	protected void DoBackspace()
	{
		if (!string.IsNullOrEmpty(this.mValue))
		{
			if (this.mSelectionStart == this.mSelectionEnd)
			{
				if (this.mSelectionStart < 1)
				{
					return;
				}
				this.mSelectionEnd--;
			}
			this.Insert(string.Empty);
		}
	}

	// Token: 0x06002CE6 RID: 11494 RVA: 0x00149D0C File Offset: 0x0014810C
	protected virtual void Insert(string text)
	{
		string leftText = this.GetLeftText();
		string rightText = this.GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			char c = text[i];
			if (c == '\b')
			{
				this.DoBackspace();
			}
			else
			{
				if (this.characterLimit > 0 && stringBuilder.Length + length >= this.characterLimit)
				{
					break;
				}
				if (this.onValidate != null)
				{
					c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				else if (this.validation != UIInput.Validation.None)
				{
					c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
			}
			i++;
		}
		this.mSelectionStart = stringBuilder.Length;
		this.mSelectionEnd = this.mSelectionStart;
		int j = 0;
		int length3 = rightText.Length;
		while (j < length3)
		{
			char c2 = rightText[j];
			if (this.onValidate != null)
			{
				c2 = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c2 = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			j++;
		}
		this.mValue = stringBuilder.ToString();
		this.UpdateLabel();
		this.ExecuteOnChange();
	}

	// Token: 0x06002CE7 RID: 11495 RVA: 0x00149EC4 File Offset: 0x001482C4
	protected string GetLeftText()
	{
		int num = Mathf.Min(new int[]
		{
			this.mSelectionStart,
			this.mSelectionEnd,
			this.mValue.Length
		});
		return (!string.IsNullOrEmpty(this.mValue) && num >= 0) ? this.mValue.Substring(0, num) : string.Empty;
	}

	// Token: 0x06002CE8 RID: 11496 RVA: 0x00149F2C File Offset: 0x0014832C
	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length) ? this.mValue.Substring(num) : string.Empty;
	}

	// Token: 0x06002CE9 RID: 11497 RVA: 0x00149F84 File Offset: 0x00148384
	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
		{
			return string.Empty;
		}
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		int num2 = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return this.mValue.Substring(num, num2 - num);
	}

	// Token: 0x06002CEA RID: 11498 RVA: 0x00149FEC File Offset: 0x001483EC
	protected int GetCharUnderMouse()
	{
		Vector3[] worldCorners = this.label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane plane = new Plane(worldCorners[0], worldCorners[1], worldCorners[2]);
		float distance;
		return (!plane.Raycast(currentRay, out distance)) ? 0 : (UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(currentRay.GetPoint(distance), false));
	}

	// Token: 0x06002CEB RID: 11499 RVA: 0x0014A068 File Offset: 0x00148468
	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && this.isSelected && this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				this.selectionStart = this.mSelectionEnd;
			}
		}
	}

	// Token: 0x06002CEC RID: 11500 RVA: 0x0014A0E2 File Offset: 0x001484E2
	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	// Token: 0x06002CED RID: 11501 RVA: 0x0014A116 File Offset: 0x00148516
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x06002CEE RID: 11502 RVA: 0x0014A120 File Offset: 0x00148520
	protected virtual void Cleanup()
	{
		if (this.mHighlight)
		{
			this.mHighlight.enabled = false;
		}
		if (this.mCaret)
		{
			this.mCaret.enabled = false;
		}
		if (this.mBlankTex)
		{
			NGUITools.Destroy(this.mBlankTex);
			this.mBlankTex = null;
		}
	}

	// Token: 0x06002CEF RID: 11503 RVA: 0x0014A188 File Offset: 0x00148588
	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	// Token: 0x06002CF0 RID: 11504 RVA: 0x0014A1E0 File Offset: 0x001485E0
	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((!flag || isSelected) ? this.activeTextColor : this.mDefaultColor);
			string text;
			if (flag)
			{
				text = ((!isSelected) ? this.mDefaultText : string.Empty);
				this.label.alignment = this.mAlignment;
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = string.Empty;
					string str = "*";
					if (this.label.bitmapFont != null && this.label.bitmapFont.bmFont != null && this.label.bitmapFont.bmFont.GetGlyph(42) == null)
					{
						str = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += str;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = (!isSelected) ? 0 : Mathf.Min(text.Length, this.cursorPosition);
				string str2 = text.Substring(0, num);
				if (isSelected)
				{
					str2 += Input.compositionString;
				}
				text = str2 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.label.alignment = this.mAlignment;
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.label.alignment = NGUIText.Alignment.Right;
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.label.alignment = this.mAlignment;
				}
			}
			this.label.text = text;
			if (isSelected && (UIInput.mKeyboard == null || this.inputShouldBeHidden))
			{
				int num3 = this.mSelectionStart - UIInput.mDrawStart;
				int num4 = this.mSelectionEnd - UIInput.mDrawStart;
				if (this.mBlankTex == null)
				{
					this.mBlankTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							this.mBlankTex.SetPixel(k, j, Color.white);
						}
					}
					this.mBlankTex.Apply();
				}
				if (num3 != num4)
				{
					if (this.mHighlight == null)
					{
						this.mHighlight = this.label.cachedGameObject.AddWidget(int.MaxValue);
						this.mHighlight.name = "Input Highlight";
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.fillGeometry = false;
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.SetAnchor(this.label.cachedTransform);
					}
					else
					{
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.MarkAsChanged();
						this.mHighlight.enabled = true;
					}
				}
				if (this.mCaret == null)
				{
					this.mCaret = this.label.cachedGameObject.AddWidget(int.MaxValue);
					this.mCaret.name = "Input Caret";
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.fillGeometry = false;
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.SetAnchor(this.label.cachedTransform);
				}
				else
				{
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.MarkAsChanged();
					this.mCaret.enabled = true;
				}
				if (num3 != num4)
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
					this.mHighlight.enabled = this.mHighlight.geometry.hasVertices;
				}
				else
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, null, this.caretColor, this.selectionColor);
					if (this.mHighlight != null)
					{
						this.mHighlight.enabled = false;
					}
				}
				this.mNextBlink = RealTime.time + 0.5f;
				this.mLastAlpha = this.label.finalAlpha;
			}
			else
			{
				this.Cleanup();
			}
		}
	}

	// Token: 0x06002CF1 RID: 11505 RVA: 0x0014A7A4 File Offset: 0x00148BA4
	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Filename)
		{
			if (ch == ':')
			{
				return '\0';
			}
			if (ch == '/')
			{
				return '\0';
			}
			if (ch == '\\')
			{
				return '\0';
			}
			if (ch == '<')
			{
				return '\0';
			}
			if (ch == '>')
			{
				return '\0';
			}
			if (ch == '|')
			{
				return '\0';
			}
			if (ch == '^')
			{
				return '\0';
			}
			if (ch == '*')
			{
				return '\0';
			}
			if (ch == ';')
			{
				return '\0';
			}
			if (ch == '"')
			{
				return '\0';
			}
			if (ch == '`')
			{
				return '\0';
			}
			if (ch == '\t')
			{
				return '\0';
			}
			if (ch == '\n')
			{
				return '\0';
			}
			return ch;
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)];
			char c2 = (text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)];
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x0014AA9E File Offset: 0x00148E9E
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x06002CF3 RID: 11507 RVA: 0x0014AAD7 File Offset: 0x00148ED7
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x06002CF4 RID: 11508 RVA: 0x0014AAE0 File Offset: 0x00148EE0
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x06002CF5 RID: 11509 RVA: 0x0014AAF0 File Offset: 0x00148EF0
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			string text = this.mValue.Replace("\\n", "\n");
			this.mValue = string.Empty;
			this.value = ((!PlayerPrefs.HasKey(this.savedAs)) ? text : PlayerPrefs.GetString(this.savedAs));
		}
	}

	// Token: 0x04002C1A RID: 11290
	public static UIInput current;

	// Token: 0x04002C1B RID: 11291
	public static UIInput selection;

	// Token: 0x04002C1C RID: 11292
	public UILabel label;

	// Token: 0x04002C1D RID: 11293
	public UIInput.InputType inputType;

	// Token: 0x04002C1E RID: 11294
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x04002C1F RID: 11295
	public UIInput.KeyboardType keyboardType;

	// Token: 0x04002C20 RID: 11296
	public bool hideInput;

	// Token: 0x04002C21 RID: 11297
	[NonSerialized]
	public bool selectAllTextOnFocus = true;

	// Token: 0x04002C22 RID: 11298
	public bool submitOnUnselect;

	// Token: 0x04002C23 RID: 11299
	public UIInput.Validation validation;

	// Token: 0x04002C24 RID: 11300
	public int characterLimit;

	// Token: 0x04002C25 RID: 11301
	public string savedAs;

	// Token: 0x04002C26 RID: 11302
	[HideInInspector]
	[SerializeField]
	private GameObject selectOnTab;

	// Token: 0x04002C27 RID: 11303
	public Color activeTextColor = Color.white;

	// Token: 0x04002C28 RID: 11304
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x04002C29 RID: 11305
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x04002C2A RID: 11306
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x04002C2B RID: 11307
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04002C2C RID: 11308
	public UIInput.OnValidate onValidate;

	// Token: 0x04002C2D RID: 11309
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x04002C2E RID: 11310
	[NonSerialized]
	protected string mDefaultText = string.Empty;

	// Token: 0x04002C2F RID: 11311
	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	// Token: 0x04002C30 RID: 11312
	[NonSerialized]
	protected float mPosition;

	// Token: 0x04002C31 RID: 11313
	[NonSerialized]
	protected bool mDoInit = true;

	// Token: 0x04002C32 RID: 11314
	[NonSerialized]
	protected NGUIText.Alignment mAlignment = NGUIText.Alignment.Left;

	// Token: 0x04002C33 RID: 11315
	[NonSerialized]
	protected bool mLoadSavedValue = true;

	// Token: 0x04002C34 RID: 11316
	protected static int mDrawStart;

	// Token: 0x04002C35 RID: 11317
	protected static string mLastIME = string.Empty;

	// Token: 0x04002C36 RID: 11318
	protected static TouchScreenKeyboard mKeyboard;

	// Token: 0x04002C37 RID: 11319
	private static bool mWaitForKeyboard;

	// Token: 0x04002C38 RID: 11320
	[NonSerialized]
	protected int mSelectionStart;

	// Token: 0x04002C39 RID: 11321
	[NonSerialized]
	protected int mSelectionEnd;

	// Token: 0x04002C3A RID: 11322
	[NonSerialized]
	protected UITexture mHighlight;

	// Token: 0x04002C3B RID: 11323
	[NonSerialized]
	protected UITexture mCaret;

	// Token: 0x04002C3C RID: 11324
	[NonSerialized]
	protected Texture2D mBlankTex;

	// Token: 0x04002C3D RID: 11325
	[NonSerialized]
	protected float mNextBlink;

	// Token: 0x04002C3E RID: 11326
	[NonSerialized]
	protected float mLastAlpha;

	// Token: 0x04002C3F RID: 11327
	[NonSerialized]
	protected string mCached = string.Empty;

	// Token: 0x04002C40 RID: 11328
	[NonSerialized]
	protected int mSelectMe = -1;

	// Token: 0x04002C41 RID: 11329
	[NonSerialized]
	protected int mSelectTime = -1;

	// Token: 0x04002C42 RID: 11330
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x04002C43 RID: 11331
	[NonSerialized]
	private UICamera mCam;

	// Token: 0x04002C44 RID: 11332
	[NonSerialized]
	private bool mEllipsis;

	// Token: 0x04002C45 RID: 11333
	private static int mIgnoreKey;

	// Token: 0x04002C46 RID: 11334
	[NonSerialized]
	public Action onUpArrow;

	// Token: 0x04002C47 RID: 11335
	[NonSerialized]
	public Action onDownArrow;

	// Token: 0x0200061D RID: 1565
	[DoNotObfuscateNGUI]
	public enum InputType
	{
		// Token: 0x04002C49 RID: 11337
		Standard,
		// Token: 0x04002C4A RID: 11338
		AutoCorrect,
		// Token: 0x04002C4B RID: 11339
		Password
	}

	// Token: 0x0200061E RID: 1566
	[DoNotObfuscateNGUI]
	public enum Validation
	{
		// Token: 0x04002C4D RID: 11341
		None,
		// Token: 0x04002C4E RID: 11342
		Integer,
		// Token: 0x04002C4F RID: 11343
		Float,
		// Token: 0x04002C50 RID: 11344
		Alphanumeric,
		// Token: 0x04002C51 RID: 11345
		Username,
		// Token: 0x04002C52 RID: 11346
		Name,
		// Token: 0x04002C53 RID: 11347
		Filename
	}

	// Token: 0x0200061F RID: 1567
	[DoNotObfuscateNGUI]
	public enum KeyboardType
	{
		// Token: 0x04002C55 RID: 11349
		Default,
		// Token: 0x04002C56 RID: 11350
		ASCIICapable,
		// Token: 0x04002C57 RID: 11351
		NumbersAndPunctuation,
		// Token: 0x04002C58 RID: 11352
		URL,
		// Token: 0x04002C59 RID: 11353
		NumberPad,
		// Token: 0x04002C5A RID: 11354
		PhonePad,
		// Token: 0x04002C5B RID: 11355
		NamePhonePad,
		// Token: 0x04002C5C RID: 11356
		EmailAddress
	}

	// Token: 0x02000620 RID: 1568
	[DoNotObfuscateNGUI]
	public enum OnReturnKey
	{
		// Token: 0x04002C5E RID: 11358
		Default,
		// Token: 0x04002C5F RID: 11359
		Submit,
		// Token: 0x04002C60 RID: 11360
		NewLine
	}

	// Token: 0x02000621 RID: 1569
	// (Invoke) Token: 0x06002CF8 RID: 11512
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
