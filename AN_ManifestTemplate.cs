using System;
using System.Collections.Generic;
using System.Xml;

// Token: 0x020000D1 RID: 209
public class AN_ManifestTemplate : AN_BaseTemplate
{
	// Token: 0x0600062C RID: 1580 RVA: 0x000382BC File Offset: 0x000366BC
	public AN_ManifestTemplate()
	{
		this._applicationTemplate = new AN_ApplicationTemplate();
		this._permissions = new List<AN_PropertyTemplate>();
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x000382DC File Offset: 0x000366DC
	public bool HasPermission(string name)
	{
		foreach (AN_PropertyTemplate an_PropertyTemplate in this.Permissions)
		{
			if (an_PropertyTemplate.Name.Equals(name))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0003834C File Offset: 0x0003674C
	public void RemovePermission(string name)
	{
		while (this.HasPermission(name))
		{
			foreach (AN_PropertyTemplate an_PropertyTemplate in this.Permissions)
			{
				if (an_PropertyTemplate.Name.Equals(name))
				{
					this.RemovePermission(an_PropertyTemplate);
					break;
				}
			}
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x000383D0 File Offset: 0x000367D0
	public void RemovePermission(AN_PropertyTemplate permission)
	{
		this._permissions.Remove(permission);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x000383E0 File Offset: 0x000367E0
	public void AddPermission(string name)
	{
		if (!this.HasPermission(name))
		{
			this.AddPermission(new AN_PropertyTemplate("uses-permission")
			{
				Name = name
			});
		}
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00038412 File Offset: 0x00036812
	public void AddPermission(AN_PropertyTemplate permission)
	{
		this._permissions.Add(permission);
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00038420 File Offset: 0x00036820
	public override void ToXmlElement(XmlDocument doc, XmlElement parent)
	{
		base.AddAttributesToXml(doc, parent, this);
		base.AddPropertiesToXml(doc, parent, this);
		XmlElement xmlElement = doc.CreateElement("application");
		this._applicationTemplate.ToXmlElement(doc, xmlElement);
		parent.AppendChild(xmlElement);
		foreach (AN_PropertyTemplate an_PropertyTemplate in this.Permissions)
		{
			XmlElement xmlElement2 = doc.CreateElement("uses-permission");
			an_PropertyTemplate.ToXmlElement(doc, xmlElement2);
			parent.AppendChild(xmlElement2);
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000633 RID: 1587 RVA: 0x000384C4 File Offset: 0x000368C4
	public AN_ApplicationTemplate ApplicationTemplate
	{
		get
		{
			return this._applicationTemplate;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000634 RID: 1588 RVA: 0x000384CC File Offset: 0x000368CC
	public List<AN_PropertyTemplate> Permissions
	{
		get
		{
			return this._permissions;
		}
	}

	// Token: 0x040004DF RID: 1247
	private AN_ApplicationTemplate _applicationTemplate;

	// Token: 0x040004E0 RID: 1248
	private List<AN_PropertyTemplate> _permissions;
}
