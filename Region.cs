using System;

// Token: 0x02000122 RID: 290
public class Region
{
	// Token: 0x06000920 RID: 2336 RVA: 0x0004623C File Offset: 0x0004463C
	public static CloudRegionCode Parse(string codeAsString)
	{
		codeAsString = codeAsString.ToLower();
		CloudRegionCode result = CloudRegionCode.none;
		if (Enum.IsDefined(typeof(CloudRegionCode), codeAsString))
		{
			result = (CloudRegionCode)Enum.Parse(typeof(CloudRegionCode), codeAsString);
		}
		return result;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00046280 File Offset: 0x00044680
	internal static CloudRegionFlag ParseFlag(string codeAsString)
	{
		codeAsString = codeAsString.ToLower();
		CloudRegionFlag result = (CloudRegionFlag)0;
		if (Enum.IsDefined(typeof(CloudRegionFlag), codeAsString))
		{
			result = (CloudRegionFlag)Enum.Parse(typeof(CloudRegionFlag), codeAsString);
		}
		return result;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x000462C3 File Offset: 0x000446C3
	public override string ToString()
	{
		return string.Format("'{0}' \t{1}ms \t{2}", this.Code, this.Ping, this.HostAndPort);
	}

	// Token: 0x040007F6 RID: 2038
	public CloudRegionCode Code;

	// Token: 0x040007F7 RID: 2039
	public string HostAndPort;

	// Token: 0x040007F8 RID: 2040
	public int Ping;
}
