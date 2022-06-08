using System;

// Token: 0x02000101 RID: 257
public class AuthenticationValues
{
	// Token: 0x0600070A RID: 1802 RVA: 0x0003AB30 File Offset: 0x00038F30
	public AuthenticationValues()
	{
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0003AB43 File Offset: 0x00038F43
	public AuthenticationValues(string userId)
	{
		this.UserId = userId;
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x0600070C RID: 1804 RVA: 0x0003AB5D File Offset: 0x00038F5D
	// (set) Token: 0x0600070D RID: 1805 RVA: 0x0003AB65 File Offset: 0x00038F65
	public CustomAuthenticationType AuthType
	{
		get
		{
			return this.authType;
		}
		set
		{
			this.authType = value;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x0600070E RID: 1806 RVA: 0x0003AB6E File Offset: 0x00038F6E
	// (set) Token: 0x0600070F RID: 1807 RVA: 0x0003AB76 File Offset: 0x00038F76
	public string AuthGetParameters { get; set; }

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000710 RID: 1808 RVA: 0x0003AB7F File Offset: 0x00038F7F
	// (set) Token: 0x06000711 RID: 1809 RVA: 0x0003AB87 File Offset: 0x00038F87
	public object AuthPostData { get; private set; }

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000712 RID: 1810 RVA: 0x0003AB90 File Offset: 0x00038F90
	// (set) Token: 0x06000713 RID: 1811 RVA: 0x0003AB98 File Offset: 0x00038F98
	public string Token { get; set; }

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x0003ABA1 File Offset: 0x00038FA1
	// (set) Token: 0x06000715 RID: 1813 RVA: 0x0003ABA9 File Offset: 0x00038FA9
	public string UserId { get; set; }

	// Token: 0x06000716 RID: 1814 RVA: 0x0003ABB2 File Offset: 0x00038FB2
	public virtual void SetAuthPostData(string stringData)
	{
		this.AuthPostData = ((!string.IsNullOrEmpty(stringData)) ? stringData : null);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0003ABCC File Offset: 0x00038FCC
	public virtual void SetAuthPostData(byte[] byteData)
	{
		this.AuthPostData = byteData;
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0003ABD8 File Offset: 0x00038FD8
	public virtual void AddAuthParameter(string key, string value)
	{
		string text = (!string.IsNullOrEmpty(this.AuthGetParameters)) ? "&" : string.Empty;
		this.AuthGetParameters = string.Format("{0}{1}{2}={3}", new object[]
		{
			this.AuthGetParameters,
			text,
			Uri.EscapeDataString(key),
			Uri.EscapeDataString(value)
		});
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0003AC3A File Offset: 0x0003903A
	public override string ToString()
	{
		return string.Format("AuthenticationValues UserId: {0}, GetParameters: {1} Token available: {2}", this.UserId, this.AuthGetParameters, this.Token != null);
	}

	// Token: 0x040006ED RID: 1773
	private CustomAuthenticationType authType = CustomAuthenticationType.None;
}
