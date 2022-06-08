using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.session;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004D5 RID: 1237
	public class SessionResponse : App42CallBack
	{
		// Token: 0x06002299 RID: 8857 RVA: 0x00101460 File Offset: 0x000FF860
		public void OnSuccess(object session)
		{
			if (session is Session)
			{
				Session session2 = (Session)session;
				this.result = session2.ToString();
				Debug.Log("UserName is : " + session2.GetUserName());
				Debug.Log("SesionId is : " + session2.GetSessionId());
				Debug.Log("TotalRecords is : " + session2.GetTotalRecords());
				Debug.Log("InvalidateOn is : " + session2.GetInvalidatedOn());
				Debug.Log("Response is : " + session2.GetStrResponse());
				Debug.Log("AttributeList is : " + session2.GetAttributeList());
				if (session2.GetAttributeList() != null)
				{
					IList<Session.Attribute> attributeList = session2.GetAttributeList();
					for (int i = 0; i < attributeList.Count; i++)
					{
						Debug.Log("Name is : " + attributeList[i].GetName());
						Debug.Log("Type is : " + attributeList[i].GetType());
						Debug.Log("Value is : " + attributeList[i].GetValue());
					}
				}
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0010158D File Offset: 0x000FF98D
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x001015AB File Offset: 0x000FF9AB
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x04002355 RID: 9045
		private string result = string.Empty;
	}
}
