using System;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x02000162 RID: 354
	public class AuthenticationValues
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0004BB99 File Offset: 0x00049F99
		public AuthenticationValues()
		{
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0004BBAC File Offset: 0x00049FAC
		public AuthenticationValues(string userId)
		{
			this.UserId = userId;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0004BBC6 File Offset: 0x00049FC6
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0004BBCE File Offset: 0x00049FCE
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0004BBD7 File Offset: 0x00049FD7
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0004BBDF File Offset: 0x00049FDF
		public string AuthGetParameters { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0004BBE8 File Offset: 0x00049FE8
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0004BBF0 File Offset: 0x00049FF0
		public object AuthPostData { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0004BBF9 File Offset: 0x00049FF9
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0004BC01 File Offset: 0x0004A001
		public string Token { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0004BC0A File Offset: 0x0004A00A
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0004BC12 File Offset: 0x0004A012
		public string UserId { get; set; }

		// Token: 0x06000A62 RID: 2658 RVA: 0x0004BC1B File Offset: 0x0004A01B
		public virtual void SetAuthPostData(string stringData)
		{
			this.AuthPostData = ((!string.IsNullOrEmpty(stringData)) ? stringData : null);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0004BC35 File Offset: 0x0004A035
		public virtual void SetAuthPostData(byte[] byteData)
		{
			this.AuthPostData = byteData;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0004BC40 File Offset: 0x0004A040
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

		// Token: 0x06000A65 RID: 2661 RVA: 0x0004BCA2 File Offset: 0x0004A0A2
		public override string ToString()
		{
			return string.Format("AuthenticationValues UserId: {0}, GetParameters: {1} Token available: {2}", this.UserId, this.AuthGetParameters, this.Token != null);
		}

		// Token: 0x04000915 RID: 2325
		private CustomAuthenticationType authType = CustomAuthenticationType.None;
	}
}
