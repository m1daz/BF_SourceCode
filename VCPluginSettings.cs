using System;
using UnityEngine;

// Token: 0x020006C0 RID: 1728
public class VCPluginSettings
{
	// Token: 0x060032E4 RID: 13028 RVA: 0x00166355 File Offset: 0x00164755
	public static bool EzguiEnabled(GameObject go)
	{
		if (typeof(SpriteRoot).GetMember("fakeMember").Length == 0)
		{
			return true;
		}
		VCUtils.DestroyWithError(go, "An EZGUI Virtual Control is being used, but EZGUI is not properly enabled!\nIn order to use EZGUI, open VCPluginSettings.cs and edit line 63 to #if false.\nSee that file for further instruction.  Destroying this control.");
		return false;
	}

	// Token: 0x060032E5 RID: 13029 RVA: 0x00166380 File Offset: 0x00164780
	public static bool NguiEnabled(GameObject go)
	{
		if (typeof(UISprite).GetMember("fakeMember").Length == 0)
		{
			return true;
		}
		VCUtils.DestroyWithError(go, "An NGUI Virtual Control is being used, but NGUI is not properly enabled!\nIn order to use NGUI, open VCPluginSettings.cs and edit line 82 to #if false.\nSee that file for further instruction.  Destroying this control.");
		return false;
	}

	// Token: 0x04002F46 RID: 12102
	public const string kFakeMemberName = "fakeMember";
}
