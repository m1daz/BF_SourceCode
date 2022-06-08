using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class ISDSettings : ScriptableObject
{
	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00039008 File Offset: 0x00037408
	public static ISDSettings Instance
	{
		get
		{
			if (ISDSettings.instance == null)
			{
				ISDSettings.instance = (Resources.Load("ISDSettingsResource") as ISDSettings);
				if (ISDSettings.instance == null)
				{
					ISDSettings.instance = ScriptableObject.CreateInstance<ISDSettings>();
				}
			}
			return ISDSettings.instance;
		}
	}

	// Token: 0x0400059A RID: 1434
	public const string VERSION_NUMBER = "1.6";

	// Token: 0x0400059B RID: 1435
	public bool IsfwSettingOpen;

	// Token: 0x0400059C RID: 1436
	public bool IsLibSettingOpen;

	// Token: 0x0400059D RID: 1437
	public bool IslinkerSettingOpne;

	// Token: 0x0400059E RID: 1438
	public bool IscompilerSettingsOpen;

	// Token: 0x0400059F RID: 1439
	public bool IsPlistSettingsOpen = true;

	// Token: 0x040005A0 RID: 1440
	public List<string> frameworks = new List<string>();

	// Token: 0x040005A1 RID: 1441
	public List<string> libraries = new List<string>();

	// Token: 0x040005A2 RID: 1442
	public List<string> compileFlags = new List<string>();

	// Token: 0x040005A3 RID: 1443
	public List<string> linkFlags = new List<string>();

	// Token: 0x040005A4 RID: 1444
	public List<string> plistkeys = new List<string>();

	// Token: 0x040005A5 RID: 1445
	public List<string> plisttags = new List<string>();

	// Token: 0x040005A6 RID: 1446
	public List<string> plistvalues = new List<string>();

	// Token: 0x040005A7 RID: 1447
	private const string ISDAssetPath = "Extensions/IOSDeploy/Resources";

	// Token: 0x040005A8 RID: 1448
	private const string ISDAssetName = "ISDSettingsResource";

	// Token: 0x040005A9 RID: 1449
	private const string ISDAssetExtension = ".asset";

	// Token: 0x040005AA RID: 1450
	private static ISDSettings instance;
}
