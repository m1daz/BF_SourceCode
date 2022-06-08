using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000CF RID: 207
public class AN_ApplicationTemplate : AN_BaseTemplate
{
	// Token: 0x06000610 RID: 1552 RVA: 0x000380C5 File Offset: 0x000364C5
	public AN_ApplicationTemplate()
	{
		this._activities = new Dictionary<int, AN_ActivityTemplate>();
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x000380D8 File Offset: 0x000364D8
	public void AddActivity(AN_ActivityTemplate activity)
	{
		this._activities.Add(activity.Id, activity);
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x000380EC File Offset: 0x000364EC
	public void RemoveActivity(AN_ActivityTemplate activity)
	{
		this._activities.Remove(activity.Id);
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00038100 File Offset: 0x00036500
	public AN_ActivityTemplate GetOrCreateActivityWithName(string name)
	{
		AN_ActivityTemplate an_ActivityTemplate = this.GetActivityWithName(name);
		if (an_ActivityTemplate == null)
		{
			an_ActivityTemplate = new AN_ActivityTemplate(false, name);
			this.AddActivity(an_ActivityTemplate);
		}
		return an_ActivityTemplate;
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0003812C File Offset: 0x0003652C
	public AN_ActivityTemplate GetActivityWithName(string name)
	{
		foreach (KeyValuePair<int, AN_ActivityTemplate> keyValuePair in this.Activities)
		{
			if (keyValuePair.Value.Name.Equals(name))
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000381A8 File Offset: 0x000365A8
	public AN_ActivityTemplate GetLauncherActivity()
	{
		foreach (KeyValuePair<int, AN_ActivityTemplate> keyValuePair in this.Activities)
		{
			if (keyValuePair.Value.IsLauncher)
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00038220 File Offset: 0x00036620
	public override void ToXmlElement(XmlDocument doc, XmlElement parent)
	{
		base.AddAttributesToXml(doc, parent, this);
		base.AddPropertiesToXml(doc, parent, this);
		foreach (int key in this._activities.Keys)
		{
			XmlElement xmlElement = doc.CreateElement("activity");
			this._activities[key].ToXmlElement(doc, xmlElement);
			parent.AppendChild(xmlElement);
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000617 RID: 1559 RVA: 0x000382B4 File Offset: 0x000366B4
	public Dictionary<int, AN_ActivityTemplate> Activities
	{
		get
		{
			return this._activities;
		}
	}

	// Token: 0x040004DC RID: 1244
	private Dictionary<int, AN_ActivityTemplate> _activities;
}
