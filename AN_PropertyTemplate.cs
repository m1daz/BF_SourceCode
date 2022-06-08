using System;
using System.Xml;

// Token: 0x020000D3 RID: 211
public class AN_PropertyTemplate : AN_BaseTemplate
{
	// Token: 0x06000635 RID: 1589 RVA: 0x000384D4 File Offset: 0x000368D4
	public AN_PropertyTemplate(string tag)
	{
		this._tag = tag;
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x000384EE File Offset: 0x000368EE
	public override void ToXmlElement(XmlDocument doc, XmlElement parent)
	{
		base.AddAttributesToXml(doc, parent, this);
		base.AddPropertiesToXml(doc, parent, this);
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000637 RID: 1591 RVA: 0x00038502 File Offset: 0x00036902
	public string Tag
	{
		get
		{
			return this._tag;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000638 RID: 1592 RVA: 0x0003850A File Offset: 0x0003690A
	// (set) Token: 0x06000639 RID: 1593 RVA: 0x00038517 File Offset: 0x00036917
	public string Name
	{
		get
		{
			return base.GetValue("android:name");
		}
		set
		{
			base.SetValue("android:name", value);
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x0600063A RID: 1594 RVA: 0x00038525 File Offset: 0x00036925
	// (set) Token: 0x0600063B RID: 1595 RVA: 0x00038532 File Offset: 0x00036932
	public string Value
	{
		get
		{
			return base.GetValue("android:value");
		}
		set
		{
			base.SetValue("android:value", value);
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x00038540 File Offset: 0x00036940
	// (set) Token: 0x0600063D RID: 1597 RVA: 0x0003854D File Offset: 0x0003694D
	public string Label
	{
		get
		{
			return base.GetValue("android:label");
		}
		set
		{
			base.SetValue("android:label", value);
		}
	}

	// Token: 0x04000579 RID: 1401
	public bool IsOpen;

	// Token: 0x0400057A RID: 1402
	private string _tag = string.Empty;
}
