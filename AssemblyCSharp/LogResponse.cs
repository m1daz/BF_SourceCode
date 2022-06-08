using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.log;

namespace AssemblyCSharp
{
	// Token: 0x020004CB RID: 1227
	public class LogResponse : App42CallBack
	{
		// Token: 0x06002271 RID: 8817 RVA: 0x000FEC4C File Offset: 0x000FD04C
		public void OnSuccess(object log)
		{
			if (log is Log)
			{
				Log log2 = (Log)log;
				this.result = log2.ToString();
				App42Log.Console("Log Obj is : " + log2);
				IList<Log.Message> messageList = log2.GetMessageList();
				for (int i = 0; i < messageList.Count; i++)
				{
					App42Log.Console("Module is   : " + messageList[i].GetModule());
					App42Log.Console("LogTime is  : " + messageList[i].GetLogTime());
					App42Log.Console("Type is  :  " + messageList[i].GetType());
					App42Log.Console("Message is :  " + messageList[i].GetMessage());
				}
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000FED16 File Offset: 0x000FD116
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			App42Log.Console("Exception : " + e);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000FED34 File Offset: 0x000FD134
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x0400231E RID: 8990
		private string result = string.Empty;
	}
}
