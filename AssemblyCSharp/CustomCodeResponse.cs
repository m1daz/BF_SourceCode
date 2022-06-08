using System;
using com.shephertz.app42.paas.sdk.csharp;
using SimpleJSON;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004C7 RID: 1223
	public class CustomCodeResponse : App42CallBack
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x000FE730 File Offset: 0x000FCB30
		public void OnSuccess(object response)
		{
			if (response is JObject)
			{
				JObject jobject = (JObject)response;
				App42Log.Console("Success : " + response);
				Debug.Log(jobject["SystemTime"]);
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000FE76F File Offset: 0x000FCB6F
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			App42Log.Console("Exception : " + e.ToString());
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000FE792 File Offset: 0x000FCB92
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x04002311 RID: 8977
		private string result = string.Empty;
	}
}
