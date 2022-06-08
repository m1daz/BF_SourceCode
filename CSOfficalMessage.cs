using System;
using SimpleJSON;

// Token: 0x020004B1 RID: 1201
public class CSOfficalMessage
{
	// Token: 0x06002236 RID: 8758 RVA: 0x000FD2CF File Offset: 0x000FB6CF
	public CSOfficalMessage()
	{
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x000FD2D8 File Offset: 0x000FB6D8
	public CSOfficalMessage(string json)
	{
		JObject jobject = JObject.Parse(json);
		this.content = jobject["content"];
	}

	// Token: 0x04002291 RID: 8849
	public string content;
}
