using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class UIToolFunctionController : MonoBehaviour
{
	// Token: 0x06001818 RID: 6168 RVA: 0x000CB838 File Offset: 0x000C9C38
	private void Start()
	{
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x000CB83A File Offset: 0x000C9C3A
	private void Update()
	{
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000CB83C File Offset: 0x000C9C3C
	public static string GetColorCode(GGColor color)
	{
		string result = "[FFFFFF]";
		switch (color)
		{
		case GGColor.Blue:
			result = "[0066ff]";
			break;
		case GGColor.Red:
			result = "[FF0000]";
			break;
		case GGColor.White:
			result = "[ffffff]";
			break;
		case GGColor.Yellow:
			result = "[FFCC00]";
			break;
		case GGColor.Black:
			result = "[000000]";
			break;
		case GGColor.Green:
			result = "[00EE00]";
			break;
		case GGColor.Purple:
			result = "[9933FF]";
			break;
		default:
			if (color == GGColor.Other)
			{
				result = "[ffffff]";
			}
			break;
		}
		return result;
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000CB8DC File Offset: 0x000C9CDC
	public static string ParseTimeSeconds(int t, int type)
	{
		string text = string.Empty;
		int num = (int)Convert.ToInt16(t / 3600);
		int num2 = (int)Convert.ToInt16(t % 3600 / 60);
		int num3 = (int)Convert.ToInt16(t % 3600 % 60);
		if (num < 10)
		{
			text = "0" + num.ToString();
		}
		else
		{
			text = num.ToString();
		}
		if (num2 < 10)
		{
			text = text + ":0" + num2.ToString();
		}
		else
		{
			text = text + ":" + num2.ToString();
		}
		if (num3 < 10)
		{
			text = text + ":0" + num3.ToString();
		}
		else
		{
			text = text + ":" + num3.ToString();
		}
		return text;
	}
}
