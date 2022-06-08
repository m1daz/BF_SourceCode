using System;
using Photon;
using UnityEngine;

// Token: 0x0200024A RID: 586
public class GGNetWorkPlayerControl : Photon.MonoBehaviour
{
	// Token: 0x060010A7 RID: 4263 RVA: 0x0008F434 File Offset: 0x0008D834
	private void Start()
	{
		if (base.photonView.isMine)
		{
			base.transform.Find("Player_1_sinkmesh").gameObject.SetActiveRecursively(false);
			base.transform.GetComponent<GGNetWorkMillorPlayerLogic>().enabled = false;
			base.gameObject.AddComponent<GGSettingControl>();
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.transform.Find("LookObject").gameObject);
			base.transform.GetComponent<CharacterMotorCS>().enabled = false;
			base.transform.GetComponent<MobileFPSInputController>().enabled = false;
			base.transform.GetComponent<GGSliderotate>().enabled = false;
			base.transform.GetComponent<GGNetWorkPlayerlogic>().enabled = false;
			base.transform.GetComponent<GGNetworkModeControl>().enabled = false;
			base.transform.Find("AudioListenerControl").gameObject.SetActive(false);
			base.transform.GetComponent<CharacterController>().enabled = false;
			base.transform.GetComponent<FireworkShooter>().enabled = false;
		}
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0008F53A File Offset: 0x0008D93A
	private void Update()
	{
	}
}
