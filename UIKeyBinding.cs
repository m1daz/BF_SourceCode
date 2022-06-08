using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200057A RID: 1402
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06002726 RID: 10022 RVA: 0x00120E0C File Offset: 0x0011F20C
	public string captionText
	{
		get
		{
			string text = NGUITools.KeyToCaption(this.keyCode);
			if (this.modifier == UIKeyBinding.Modifier.Alt)
			{
				return "Alt+" + text;
			}
			if (this.modifier == UIKeyBinding.Modifier.Control)
			{
				return "Control+" + text;
			}
			if (this.modifier == UIKeyBinding.Modifier.Shift)
			{
				return "Shift+" + text;
			}
			return text;
		}
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x00120E70 File Offset: 0x0011F270
	public static bool IsBound(KeyCode key)
	{
		int i = 0;
		int count = UIKeyBinding.mList.Count;
		while (i < count)
		{
			UIKeyBinding uikeyBinding = UIKeyBinding.mList[i];
			if (uikeyBinding != null && uikeyBinding.keyCode == key)
			{
				return true;
			}
			i++;
		}
		return false;
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x00120EC1 File Offset: 0x0011F2C1
	protected virtual void OnEnable()
	{
		UIKeyBinding.mList.Add(this);
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x00120ECE File Offset: 0x0011F2CE
	protected virtual void OnDisable()
	{
		UIKeyBinding.mList.Remove(this);
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x00120EDC File Offset: 0x0011F2DC
	protected virtual void Start()
	{
		UIInput component = base.GetComponent<UIInput>();
		this.mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
		}
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x00120F22 File Offset: 0x0011F322
	protected virtual void OnSubmit()
	{
		if (UICamera.currentKey == this.keyCode && this.IsModifierActive())
		{
			this.mIgnoreUp = true;
		}
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x00120F46 File Offset: 0x0011F346
	protected virtual bool IsModifierActive()
	{
		return UIKeyBinding.IsModifierActive(this.modifier);
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x00120F54 File Offset: 0x0011F354
	public static bool IsModifierActive(UIKeyBinding.Modifier modifier)
	{
		if (modifier == UIKeyBinding.Modifier.Any)
		{
			return true;
		}
		if (modifier == UIKeyBinding.Modifier.Alt)
		{
			if (UICamera.GetKey(KeyCode.LeftAlt) || UICamera.GetKey(KeyCode.RightAlt))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.Control)
		{
			if (UICamera.GetKey(KeyCode.LeftControl) || UICamera.GetKey(KeyCode.RightControl))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.Shift)
		{
			if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.None)
		{
			return !UICamera.GetKey(KeyCode.LeftAlt) && !UICamera.GetKey(KeyCode.RightAlt) && !UICamera.GetKey(KeyCode.LeftControl) && !UICamera.GetKey(KeyCode.RightControl) && !UICamera.GetKey(KeyCode.LeftShift) && !UICamera.GetKey(KeyCode.RightShift);
		}
		return false;
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x00121090 File Offset: 0x0011F490
	protected virtual void Update()
	{
		if (this.keyCode != KeyCode.Numlock && UICamera.inputHasFocus)
		{
			return;
		}
		if (this.keyCode == KeyCode.None || !this.IsModifierActive())
		{
			return;
		}
		bool flag = UICamera.GetKeyDown(this.keyCode);
		bool flag2 = UICamera.GetKeyUp(this.keyCode);
		if (flag)
		{
			this.mPress = true;
		}
		if (this.action == UIKeyBinding.Action.PressAndClick || this.action == UIKeyBinding.Action.All)
		{
			if (flag)
			{
				UICamera.currentTouchID = -1;
				UICamera.currentKey = this.keyCode;
				this.OnBindingPress(true);
			}
			if (this.mPress && flag2)
			{
				UICamera.currentTouchID = -1;
				UICamera.currentKey = this.keyCode;
				this.OnBindingPress(false);
				this.OnBindingClick();
			}
		}
		if ((this.action == UIKeyBinding.Action.Select || this.action == UIKeyBinding.Action.All) && flag2)
		{
			if (this.mIsInput)
			{
				if (!this.mIgnoreUp && (this.keyCode == KeyCode.Numlock || !UICamera.inputHasFocus) && this.mPress)
				{
					UICamera.selectedObject = base.gameObject;
				}
				this.mIgnoreUp = false;
			}
			else if (this.mPress)
			{
				UICamera.hoveredObject = base.gameObject;
			}
		}
		if (flag2)
		{
			this.mPress = false;
		}
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x001211F5 File Offset: 0x0011F5F5
	protected virtual void OnBindingPress(bool pressed)
	{
		UICamera.Notify(base.gameObject, "OnPress", pressed);
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x0012120D File Offset: 0x0011F60D
	protected virtual void OnBindingClick()
	{
		UICamera.Notify(base.gameObject, "OnClick", null);
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x00121220 File Offset: 0x0011F620
	public override string ToString()
	{
		return UIKeyBinding.GetString(this.keyCode, this.modifier);
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x00121233 File Offset: 0x0011F633
	public static string GetString(KeyCode keyCode, UIKeyBinding.Modifier modifier)
	{
		return (modifier == UIKeyBinding.Modifier.None) ? keyCode.ToString() : (modifier + "+" + keyCode);
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x00121264 File Offset: 0x0011F664
	public static bool GetKeyCode(string text, out KeyCode key, out UIKeyBinding.Modifier modifier)
	{
		key = KeyCode.None;
		modifier = UIKeyBinding.Modifier.None;
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		if (text.Contains("+"))
		{
			string[] array = text.Split(new char[]
			{
				'+'
			});
			try
			{
				modifier = (UIKeyBinding.Modifier)Enum.Parse(typeof(UIKeyBinding.Modifier), array[0]);
				key = (KeyCode)Enum.Parse(typeof(KeyCode), array[1]);
			}
			catch (Exception)
			{
				return false;
			}
		}
		else
		{
			modifier = UIKeyBinding.Modifier.None;
			try
			{
				key = (KeyCode)Enum.Parse(typeof(KeyCode), text);
			}
			catch (Exception)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x00121330 File Offset: 0x0011F730
	public static UIKeyBinding.Modifier GetActiveModifier()
	{
		UIKeyBinding.Modifier result = UIKeyBinding.Modifier.None;
		if (UICamera.GetKey(KeyCode.LeftAlt) || UICamera.GetKey(KeyCode.RightAlt))
		{
			result = UIKeyBinding.Modifier.Alt;
		}
		else if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
		{
			result = UIKeyBinding.Modifier.Shift;
		}
		else if (UICamera.GetKey(KeyCode.LeftControl) || UICamera.GetKey(KeyCode.RightControl))
		{
			result = UIKeyBinding.Modifier.Control;
		}
		return result;
	}

	// Token: 0x040027ED RID: 10221
	private static List<UIKeyBinding> mList = new List<UIKeyBinding>();

	// Token: 0x040027EE RID: 10222
	public KeyCode keyCode;

	// Token: 0x040027EF RID: 10223
	public UIKeyBinding.Modifier modifier;

	// Token: 0x040027F0 RID: 10224
	public UIKeyBinding.Action action;

	// Token: 0x040027F1 RID: 10225
	[NonSerialized]
	private bool mIgnoreUp;

	// Token: 0x040027F2 RID: 10226
	[NonSerialized]
	private bool mIsInput;

	// Token: 0x040027F3 RID: 10227
	[NonSerialized]
	private bool mPress;

	// Token: 0x0200057B RID: 1403
	[DoNotObfuscateNGUI]
	public enum Action
	{
		// Token: 0x040027F5 RID: 10229
		PressAndClick,
		// Token: 0x040027F6 RID: 10230
		Select,
		// Token: 0x040027F7 RID: 10231
		All
	}

	// Token: 0x0200057C RID: 1404
	[DoNotObfuscateNGUI]
	public enum Modifier
	{
		// Token: 0x040027F9 RID: 10233
		Any,
		// Token: 0x040027FA RID: 10234
		Shift,
		// Token: 0x040027FB RID: 10235
		Control,
		// Token: 0x040027FC RID: 10236
		Alt,
		// Token: 0x040027FD RID: 10237
		None
	}
}
