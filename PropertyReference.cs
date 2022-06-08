using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x020005C0 RID: 1472
[Serializable]
public class PropertyReference
{
	// Token: 0x060029BF RID: 10687 RVA: 0x00136517 File Offset: 0x00134917
	public PropertyReference()
	{
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x0013651F File Offset: 0x0013491F
	public PropertyReference(Component target, string fieldName)
	{
		this.mTarget = target;
		this.mName = fieldName;
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x060029C1 RID: 10689 RVA: 0x00136535 File Offset: 0x00134935
	// (set) Token: 0x060029C2 RID: 10690 RVA: 0x0013653D File Offset: 0x0013493D
	public Component target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x060029C3 RID: 10691 RVA: 0x00136554 File Offset: 0x00134954
	// (set) Token: 0x060029C4 RID: 10692 RVA: 0x0013655C File Offset: 0x0013495C
	public string name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x060029C5 RID: 10693 RVA: 0x00136573 File Offset: 0x00134973
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mName);
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x060029C6 RID: 10694 RVA: 0x00136598 File Offset: 0x00134998
	public bool isEnabled
	{
		get
		{
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget as MonoBehaviour;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x060029C7 RID: 10695 RVA: 0x001365DC File Offset: 0x001349DC
	public Type GetPropertyType()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			return this.mProperty.PropertyType;
		}
		if (this.mField != null)
		{
			return this.mField.FieldType;
		}
		return typeof(void);
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x0013664C File Offset: 0x00134A4C
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is PropertyReference)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return this.mTarget == propertyReference.mTarget && string.Equals(this.mName, propertyReference.mName);
		}
		return false;
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x001366A7 File Offset: 0x00134AA7
	public override int GetHashCode()
	{
		return PropertyReference.s_Hash;
	}

	// Token: 0x060029CA RID: 10698 RVA: 0x001366AE File Offset: 0x00134AAE
	public void Set(Component target, string methodName)
	{
		this.mTarget = target;
		this.mName = methodName;
	}

	// Token: 0x060029CB RID: 10699 RVA: 0x001366BE File Offset: 0x00134ABE
	public void Clear()
	{
		this.mTarget = null;
		this.mName = null;
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x001366CE File Offset: 0x00134ACE
	public void Reset()
	{
		this.mField = null;
		this.mProperty = null;
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x001366DE File Offset: 0x00134ADE
	public override string ToString()
	{
		return PropertyReference.ToString(this.mTarget, this.name);
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x001366F4 File Offset: 0x00134AF4
	public static string ToString(Component comp, string property)
	{
		if (!(comp != null))
		{
			return null;
		}
		string text = comp.GetType().ToString();
		int num = text.LastIndexOf('.');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(property))
		{
			return text + "." + property;
		}
		return text + ".[property]";
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x00136758 File Offset: 0x00134B58
	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			if (this.mProperty.CanRead)
			{
				return this.mProperty.GetValue(this.mTarget, null);
			}
		}
		else if (this.mField != null)
		{
			return this.mField.GetValue(this.mTarget);
		}
		return null;
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x001367E0 File Offset: 0x00134BE0
	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty == null && this.mField == null)
		{
			return false;
		}
		if (value == null)
		{
			try
			{
				if (this.mProperty == null)
				{
					this.mField.SetValue(this.mTarget, null);
					return true;
				}
				if (this.mProperty.CanWrite)
				{
					this.mProperty.SetValue(this.mTarget, null, null);
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		if (!this.Convert(ref value))
		{
			if (Application.isPlaying)
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"Unable to convert ",
					value.GetType(),
					" to ",
					this.GetPropertyType()
				}));
			}
		}
		else
		{
			if (this.mField != null)
			{
				this.mField.SetValue(this.mTarget, value);
				return true;
			}
			if (this.mProperty.CanWrite)
			{
				this.mProperty.SetValue(this.mTarget, value, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x00136938 File Offset: 0x00134D38
	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (this.mTarget != null && !string.IsNullOrEmpty(this.mName))
		{
			Type type = this.mTarget.GetType();
			this.mField = type.GetField(this.mName);
			this.mProperty = type.GetProperty(this.mName);
		}
		else
		{
			this.mField = null;
			this.mProperty = null;
		}
		return this.mField != null || this.mProperty != null;
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x001369C4 File Offset: 0x00134DC4
	private bool Convert(ref object value)
	{
		if (this.mTarget == null)
		{
			return false;
		}
		Type propertyType = this.GetPropertyType();
		Type from;
		if (value == null)
		{
			if (!propertyType.IsClass)
			{
				return false;
			}
			from = propertyType;
		}
		else
		{
			from = value.GetType();
		}
		return PropertyReference.Convert(ref value, from, propertyType);
	}

	// Token: 0x060029D3 RID: 10707 RVA: 0x00136A18 File Offset: 0x00134E18
	public static bool Convert(Type from, Type to)
	{
		object obj = null;
		return PropertyReference.Convert(ref obj, from, to);
	}

	// Token: 0x060029D4 RID: 10708 RVA: 0x00136A30 File Offset: 0x00134E30
	public static bool Convert(object value, Type to)
	{
		if (value == null)
		{
			value = null;
			return PropertyReference.Convert(ref value, to, to);
		}
		return PropertyReference.Convert(ref value, value.GetType(), to);
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x00136A54 File Offset: 0x00134E54
	public static bool Convert(ref object value, Type from, Type to)
	{
		if (to.IsAssignableFrom(from))
		{
			return true;
		}
		if (to == typeof(string))
		{
			value = ((value == null) ? "null" : value.ToString());
			return true;
		}
		if (value == null)
		{
			return false;
		}
		if (to == typeof(int))
		{
			if (from == typeof(string))
			{
				int num;
				if (int.TryParse((string)value, out num))
				{
					value = num;
					return true;
				}
			}
			else
			{
				if (from == typeof(float))
				{
					value = Mathf.RoundToInt((float)value);
					return true;
				}
				if (from == typeof(double))
				{
					value = (int)Math.Round((double)value);
				}
			}
		}
		else if (to == typeof(float))
		{
			if (from == typeof(string))
			{
				float num2;
				if (float.TryParse((string)value, out num2))
				{
					value = num2;
					return true;
				}
			}
			else if (from == typeof(double))
			{
				value = (float)((double)value);
			}
			else if (from == typeof(int))
			{
				value = (float)((int)value);
			}
		}
		else if (to == typeof(double))
		{
			if (from == typeof(string))
			{
				double num3;
				if (double.TryParse((string)value, out num3))
				{
					value = num3;
					return true;
				}
			}
			else if (from == typeof(float))
			{
				value = (double)((float)value);
			}
			else if (from == typeof(int))
			{
				value = (double)((int)value);
			}
		}
		return false;
	}

	// Token: 0x040029DD RID: 10717
	[SerializeField]
	private Component mTarget;

	// Token: 0x040029DE RID: 10718
	[SerializeField]
	private string mName;

	// Token: 0x040029DF RID: 10719
	private FieldInfo mField;

	// Token: 0x040029E0 RID: 10720
	private PropertyInfo mProperty;

	// Token: 0x040029E1 RID: 10721
	private static int s_Hash = "PropertyBinding".GetHashCode();
}
