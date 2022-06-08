using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class UIDisconnectionDirector : MonoBehaviour
{
	// Token: 0x06001263 RID: 4707 RVA: 0x000A4CAB File Offset: 0x000A30AB
	private void Awake()
	{
		if (UIDisconnectionDirector.mInstance == null)
		{
			UIDisconnectionDirector.mInstance = this;
		}
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x000A4CC3 File Offset: 0x000A30C3
	private void OnDestroy()
	{
		if (UIDisconnectionDirector.mInstance != null)
		{
			UIDisconnectionDirector.mInstance = null;
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000A4CDC File Offset: 0x000A30DC
	private void Start()
	{
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Network Connecting...", Color.white, null, null, null, null, null);
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000A4D03 File Offset: 0x000A3103
	private void Update()
	{
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000A4D05 File Offset: 0x000A3105
	public void ReconnectBtnPressed()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.goSwitchSceneInfo, new Vector3(0f, 0f, 0f), Quaternion.identity);
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000A4D2C File Offset: 0x000A312C
	public void QuitBtnPressed()
	{
		Application.LoadLevel("UILobby");
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000A4D38 File Offset: 0x000A3138
	public void PopDisconnectFailPanel()
	{
		EventDelegate btnEventName = new EventDelegate(this, "ReconnectBtnPressed");
		EventDelegate btnEventName2 = new EventDelegate(this, "QuitBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "Reconnect Failed!", Color.white, "Reconnect", "Quit", btnEventName, btnEventName2, null);
	}

	// Token: 0x0400151E RID: 5406
	public static UIDisconnectionDirector mInstance;

	// Token: 0x0400151F RID: 5407
	public GameObject goSwitchSceneInfo;
}
