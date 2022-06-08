using System;

// Token: 0x020004AF RID: 1199
[Serializable]
public class CSUserNamePassword
{
	// Token: 0x06002235 RID: 8757 RVA: 0x000FD2B1 File Offset: 0x000FB6B1
	public CSUserNamePassword()
	{
		this.UserName = string.Empty;
		this.Password = string.Empty;
	}

	// Token: 0x0400228B RID: 8843
	public string UserName;

	// Token: 0x0400228C RID: 8844
	public string Password;
}
