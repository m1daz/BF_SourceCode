using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000CE RID: 206
public class AN_ActivityTemplate : AN_BaseTemplate
{
	// Token: 0x06000607 RID: 1543 RVA: 0x00037EB8 File Offset: 0x000362B8
	public AN_ActivityTemplate(bool isLauncher, string name)
	{
		this._isLauncher = isLauncher;
		this._name = name;
		this._id = this.GetHashCode();
		this._values = new Dictionary<string, string>();
		this._properties = new Dictionary<string, List<AN_PropertyTemplate>>();
		base.SetValue("android:name", name);
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00037F12 File Offset: 0x00036312
	public void SetName(string name)
	{
		this._name = name;
		base.SetValue("android:name", name);
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00037F27 File Offset: 0x00036327
	public void SetAsLauncher(bool isLauncher)
	{
		this._isLauncher = isLauncher;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00037F30 File Offset: 0x00036330
	public static AN_PropertyTemplate GetLauncherPropertyTemplate()
	{
		AN_PropertyTemplate an_PropertyTemplate = new AN_PropertyTemplate("intent-filter");
		AN_PropertyTemplate an_PropertyTemplate2 = new AN_PropertyTemplate("action");
		an_PropertyTemplate2.SetValue("android:name", "android.intent.action.MAIN");
		an_PropertyTemplate.AddProperty("action", an_PropertyTemplate2);
		an_PropertyTemplate2 = new AN_PropertyTemplate("category");
		an_PropertyTemplate2.SetValue("android:name", "android.intent.category.LAUNCHER");
		an_PropertyTemplate.AddProperty("category", an_PropertyTemplate2);
		return an_PropertyTemplate;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00037F98 File Offset: 0x00036398
	public bool IsLauncherProperty(AN_PropertyTemplate property)
	{
		if (property.Tag.Equals("intent-filter"))
		{
			foreach (AN_PropertyTemplate an_PropertyTemplate in property.Properties["category"])
			{
				if (an_PropertyTemplate.Values.ContainsKey("android:name") && an_PropertyTemplate.Values["android:name"].Equals("android.intent.category.LAUNCHER"))
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0003804C File Offset: 0x0003644C
	public override void ToXmlElement(XmlDocument doc, XmlElement parent)
	{
		base.AddAttributesToXml(doc, parent, this);
		AN_PropertyTemplate an_PropertyTemplate = null;
		if (this._isLauncher)
		{
			an_PropertyTemplate = AN_ActivityTemplate.GetLauncherPropertyTemplate();
			base.AddProperty(an_PropertyTemplate.Tag, an_PropertyTemplate);
		}
		base.AddPropertiesToXml(doc, parent, this);
		if (this._isLauncher)
		{
			this._properties["intent-filter"].Remove(an_PropertyTemplate);
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600060D RID: 1549 RVA: 0x000380AD File Offset: 0x000364AD
	public bool IsLauncher
	{
		get
		{
			return this._isLauncher;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600060E RID: 1550 RVA: 0x000380B5 File Offset: 0x000364B5
	public string Name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600060F RID: 1551 RVA: 0x000380BD File Offset: 0x000364BD
	public int Id
	{
		get
		{
			return this._id;
		}
	}

	// Token: 0x040004D8 RID: 1240
	public bool IsOpen;

	// Token: 0x040004D9 RID: 1241
	private int _id;

	// Token: 0x040004DA RID: 1242
	private bool _isLauncher;

	// Token: 0x040004DB RID: 1243
	private string _name = string.Empty;
}
