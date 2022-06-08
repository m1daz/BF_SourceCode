using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004C4 RID: 1220
	public class BuddyResponse : App42CallBack
	{
		// Token: 0x06002258 RID: 8792 RVA: 0x000FDAE4 File Offset: 0x000FBEE4
		public void OnSuccess(object buddy)
		{
			Debug.Log("BuddyName : " + buddy);
			this.result = buddy.ToString();
			try
			{
				if (buddy is Buddy)
				{
					Buddy buddy2 = (Buddy)buddy;
					this.result = buddy2.ToString();
					Debug.Log("BuddyName : " + buddy2.GetBuddyName());
					Debug.Log("OwnerName : " + buddy2.GetOwnerName());
					Debug.Log("GetMessage : " + buddy2.GetMessage());
					Debug.Log("GetAcceptedOn : " + buddy2.GetAcceptedOn());
					Debug.Log("GetMessageId : " + buddy2.GetMessageId());
					Debug.Log("GetSendedOn : " + buddy2.GetSendedOn());
				}
				else
				{
					IList<Buddy> list = (IList<Buddy>)buddy;
					this.result = list[0].ToString();
					for (int i = 0; i < list.Count; i++)
					{
						Debug.Log("BuddyName : " + list[i].GetBuddyName());
						Debug.Log("OwnerName : " + list[i].GetOwnerName());
						Debug.Log("GetMessage : " + list[i].GetMessage());
						Debug.Log("GetAcceptedOn : " + list[i].GetAcceptedOn());
						Debug.Log("GetMessageId : " + list[i].GetMessageId());
						Debug.Log("GetSendedOn : " + list[i].GetSendedOn());
					}
				}
			}
			catch (App42Exception ex)
			{
				this.result = ex.ToString();
				Debug.Log("App42Exception : " + ex);
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000FDCD8 File Offset: 0x000FC0D8
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000FDCF6 File Offset: 0x000FC0F6
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x040022DA RID: 8922
		private string result = string.Empty;
	}
}
