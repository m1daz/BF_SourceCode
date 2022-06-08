using System;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using SimpleJSON;
using UnityEngine;

// Token: 0x020004BF RID: 1215
public class GGDeviceConsoleLog : MonoBehaviour
{
	// Token: 0x06002242 RID: 8770 RVA: 0x000FD3F8 File Offset: 0x000FB7F8
	public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x000FD3FC File Offset: 0x000FB7FC
	private void Awake()
	{
		if (GGDeviceConsoleLog.<>f__mg$cache0 == null)
		{
			GGDeviceConsoleLog.<>f__mg$cache0 = new RemoteCertificateValidationCallback(GGDeviceConsoleLog.Validator);
		}
		ServicePointManager.ServerCertificateValidationCallback = GGDeviceConsoleLog.<>f__mg$cache0;
		this.mSp = new ServiceAPI("43ca4df34bf448d8d116e1c1ce6ae5b4e0c4305cf4f88c28b7e7f3758600729a", "f3bb8e196743b7de6f69298a90ec1dc570d2c70be033588955a6a5b381a5f0bd");
		App42API.SetBaseURL("http://api.app42cloud.com/cloud/");
		this.mStorageService = this.mSp.BuildStorageService();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x000FD478 File Offset: 0x000FB878
	private void HandleLog(string logstring, string stackTrace, LogType type)
	{
		if (GGCloudServiceKit.mInstance != null)
		{
			GGCloudServiceKit mInstance = GGCloudServiceKit.mInstance;
			mInstance.mLoginException += logstring;
			GGCloudServiceKit mInstance2 = GGCloudServiceKit.mInstance;
			mInstance2.mLoginException = mInstance2.mLoginException + "~~~~" + stackTrace;
		}
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x000FD4C6 File Offset: 0x000FB8C6
	private void Update()
	{
		this.mOpTimeForDeviceLog += Time.deltaTime;
		if (this.mOpTimeForDeviceLog > GGDeviceConsoleLog.OPIntervalForDeviceLog)
		{
			this.mOpTimeForDeviceLog = 0f;
			this.SaveConsoleLog();
		}
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x000FD4FC File Offset: 0x000FB8FC
	private void SaveConsoleLog()
	{
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("Log", GGCloudServiceKit.mInstance.mLoginException);
		this.mStorageService.InsertJSONDocument("PLAYER", "NewCollection", jsonclass, new GGDeviceConsoleResponse());
	}

	// Token: 0x06002247 RID: 8775 RVA: 0x000FD544 File Offset: 0x000FB944
	private void OnDestroy()
	{
	}

	// Token: 0x040022CE RID: 8910
	private ServiceAPI mSp;

	// Token: 0x040022CF RID: 8911
	public StorageService mStorageService;

	// Token: 0x040022D0 RID: 8912
	private float mOpTimeForDeviceLog;

	// Token: 0x040022D1 RID: 8913
	private static float OPIntervalForDeviceLog = 15f;

	// Token: 0x040022D2 RID: 8914
	[CompilerGenerated]
	private static RemoteCertificateValidationCallback <>f__mg$cache0;
}
