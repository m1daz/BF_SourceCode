using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.customcode;
using SimpleJSON;
using UnityEngine;

// Token: 0x020004C8 RID: 1224
public class CustomCodeTest : MonoBehaviour
{
	// Token: 0x06002265 RID: 8805 RVA: 0x000FE7B8 File Offset: 0x000FCBB8
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x000FE7DB File Offset: 0x000FCBDB
	private void Update()
	{
	}

	// Token: 0x06002267 RID: 8807 RVA: 0x000FE7E0 File Offset: 0x000FCBE0
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Run Java Code"))
		{
			App42Log.SetDebug(true);
			this.customCodeService = this.sp.BuildCustomCodeService();
			JSONClass jsonclass = new JSONClass();
			jsonclass.Add("name", "John");
			jsonclass.Add("age", 30);
			this.customCodeService.RunJavaCode(this.cons.customServiceName, jsonclass, new CustomCodeResponse());
		}
	}

	// Token: 0x04002312 RID: 8978
	private Constant cons = new Constant();

	// Token: 0x04002313 RID: 8979
	private ServiceAPI sp;

	// Token: 0x04002314 RID: 8980
	private CustomCodeService customCodeService;

	// Token: 0x04002315 RID: 8981
	public string success;

	// Token: 0x04002316 RID: 8982
	public string box;

	// Token: 0x04002317 RID: 8983
	private CustomCodeResponse callBack = new CustomCodeResponse();
}
