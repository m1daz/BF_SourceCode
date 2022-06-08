using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000D0 RID: 208
public abstract class AN_BaseTemplate
{
	// Token: 0x06000618 RID: 1560 RVA: 0x000379F4 File Offset: 0x00035DF4
	public AN_BaseTemplate()
	{
		this._values = new Dictionary<string, string>();
		this._properties = new Dictionary<string, List<AN_PropertyTemplate>>();
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x00037A14 File Offset: 0x00035E14
	public AN_PropertyTemplate GetOrCreateIntentFilterWithName(string name)
	{
		AN_PropertyTemplate an_PropertyTemplate = this.GetIntentFilterWithName(name);
		if (an_PropertyTemplate == null)
		{
			an_PropertyTemplate = new AN_PropertyTemplate("intent-filter");
			AN_PropertyTemplate an_PropertyTemplate2 = new AN_PropertyTemplate("action");
			an_PropertyTemplate2.SetValue("android:name", name);
			an_PropertyTemplate.AddProperty(an_PropertyTemplate2);
			this.AddProperty(an_PropertyTemplate);
		}
		return an_PropertyTemplate;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00037A60 File Offset: 0x00035E60
	public AN_PropertyTemplate GetIntentFilterWithName(string name)
	{
		string tag = "intent-filter";
		List<AN_PropertyTemplate> propertiesWithTag = this.GetPropertiesWithTag(tag);
		foreach (AN_PropertyTemplate an_PropertyTemplate in propertiesWithTag)
		{
			string intentFilterName = this.GetIntentFilterName(an_PropertyTemplate);
			if (intentFilterName.Equals(name))
			{
				return an_PropertyTemplate;
			}
		}
		return null;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00037AE0 File Offset: 0x00035EE0
	public string GetIntentFilterName(AN_PropertyTemplate intent)
	{
		List<AN_PropertyTemplate> propertiesWithTag = intent.GetPropertiesWithTag("action");
		if (propertiesWithTag.Count > 0)
		{
			return propertiesWithTag[0].GetValue("android:name");
		}
		return string.Empty;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00037B1C File Offset: 0x00035F1C
	public AN_PropertyTemplate GetOrCreatePropertyWithName(string tag, string name)
	{
		AN_PropertyTemplate an_PropertyTemplate = this.GetPropertyWithName(tag, name);
		if (an_PropertyTemplate == null)
		{
			an_PropertyTemplate = new AN_PropertyTemplate(tag);
			an_PropertyTemplate.SetValue("android:name", name);
			this.AddProperty(an_PropertyTemplate);
		}
		return an_PropertyTemplate;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00037B54 File Offset: 0x00035F54
	public AN_PropertyTemplate GetPropertyWithName(string tag, string name)
	{
		List<AN_PropertyTemplate> propertiesWithTag = this.GetPropertiesWithTag(tag);
		foreach (AN_PropertyTemplate an_PropertyTemplate in propertiesWithTag)
		{
			if (an_PropertyTemplate.Values.ContainsKey("android:name") && an_PropertyTemplate.Values["android:name"] == name)
			{
				return an_PropertyTemplate;
			}
		}
		return null;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x00037BE8 File Offset: 0x00035FE8
	public AN_PropertyTemplate GetOrCreatePropertyWithTag(string tag)
	{
		AN_PropertyTemplate an_PropertyTemplate = this.GetPropertyWithTag(tag);
		if (an_PropertyTemplate == null)
		{
			an_PropertyTemplate = new AN_PropertyTemplate(tag);
			this.AddProperty(an_PropertyTemplate);
		}
		return an_PropertyTemplate;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00037C14 File Offset: 0x00036014
	public AN_PropertyTemplate GetPropertyWithTag(string tag)
	{
		List<AN_PropertyTemplate> propertiesWithTag = this.GetPropertiesWithTag(tag);
		if (propertiesWithTag.Count > 0)
		{
			return propertiesWithTag[0];
		}
		return null;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00037C3E File Offset: 0x0003603E
	public List<AN_PropertyTemplate> GetPropertiesWithTag(string tag)
	{
		if (this.Properties.ContainsKey(tag))
		{
			return this.Properties[tag];
		}
		return new List<AN_PropertyTemplate>();
	}

	// Token: 0x06000621 RID: 1569
	public abstract void ToXmlElement(XmlDocument doc, XmlElement parent);

	// Token: 0x06000622 RID: 1570 RVA: 0x00037C63 File Offset: 0x00036063
	public void AddProperty(AN_PropertyTemplate property)
	{
		this.AddProperty(property.Tag, property);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00037C74 File Offset: 0x00036074
	public void AddProperty(string tag, AN_PropertyTemplate property)
	{
		if (!this._properties.ContainsKey(tag))
		{
			List<AN_PropertyTemplate> value = new List<AN_PropertyTemplate>();
			this._properties.Add(tag, value);
		}
		this._properties[tag].Add(property);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00037CB7 File Offset: 0x000360B7
	public void SetValue(string key, string value)
	{
		if (this._values.ContainsKey(key))
		{
			this._values[key] = value;
		}
		else
		{
			this._values.Add(key, value);
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00037CE9 File Offset: 0x000360E9
	public string GetValue(string key)
	{
		if (this._values.ContainsKey(key))
		{
			return this._values[key];
		}
		return string.Empty;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00037D0E File Offset: 0x0003610E
	public void RemoveProperty(AN_PropertyTemplate property)
	{
		this._properties[property.Tag].Remove(property);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00037D28 File Offset: 0x00036128
	public void RemoveValue(string key)
	{
		this._values.Remove(key);
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00037D38 File Offset: 0x00036138
	public void AddPropertiesToXml(XmlDocument doc, XmlElement parent, AN_BaseTemplate template)
	{
		foreach (string text in template.Properties.Keys)
		{
			foreach (AN_PropertyTemplate template2 in template.Properties[text])
			{
				XmlElement xmlElement = doc.CreateElement(text);
				this.AddAttributesToXml(doc, xmlElement, template2);
				this.AddPropertiesToXml(doc, xmlElement, template2);
				parent.AppendChild(xmlElement);
			}
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00037E04 File Offset: 0x00036204
	public void AddAttributesToXml(XmlDocument doc, XmlElement parent, AN_BaseTemplate template)
	{
		foreach (string text in template.Values.Keys)
		{
			string name = text;
			if (text.Contains("android:"))
			{
				name = text.Replace("android:", "android___");
			}
			XmlAttribute xmlAttribute = doc.CreateAttribute(name);
			xmlAttribute.Value = template.Values[text];
			parent.Attributes.Append(xmlAttribute);
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x0600062A RID: 1578 RVA: 0x00037EA8 File Offset: 0x000362A8
	public Dictionary<string, string> Values
	{
		get
		{
			return this._values;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x00037EB0 File Offset: 0x000362B0
	public Dictionary<string, List<AN_PropertyTemplate>> Properties
	{
		get
		{
			return this._properties;
		}
	}

	// Token: 0x040004DD RID: 1245
	protected Dictionary<string, List<AN_PropertyTemplate>> _properties;

	// Token: 0x040004DE RID: 1246
	protected Dictionary<string, string> _values;
}
