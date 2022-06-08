using System;
using Photon;
using UnityEngine;

// Token: 0x020004E2 RID: 1250
public class GGNetworkCharacterTest : Photon.MonoBehaviour
{
	// Token: 0x060022CC RID: 8908 RVA: 0x00103AD1 File Offset: 0x00101ED1
	private void Start()
	{
		this.InitEvent();
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x00103AD9 File Offset: 0x00101ED9
	private void InitEvent()
	{
		GGNetworkKit.mInstance.ReceiveDamage += this.Event_Damage;
		GGNetworkKit.mInstance.ModeResult += this.Event_ModeResult;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x00103B07 File Offset: 0x00101F07
	private void OnDisable()
	{
		GGNetworkKit.mInstance.ReceiveDamage -= this.Event_Damage;
		GGNetworkKit.mInstance.ModeResult -= this.Event_ModeResult;
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x00103B35 File Offset: 0x00101F35
	private void Event_Damage(GGDamageEventArgs damageEventArgs)
	{
		Debug.Log(damageEventArgs.damage);
		base.GetComponent<GGNetworkCharacter>().mBlood -= (int)damageEventArgs.damage;
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x00103B5F File Offset: 0x00101F5F
	private void Event_ModeResult(GGModeEventArgs modeEventArgs)
	{
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x00103B64 File Offset: 0x00101F64
	public void OnGUI()
	{
		if (base.photonView.isMine && GUI.Button(new Rect(50f, 150f, 100f, 20f), "damage"))
		{
			GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
			ggdamageEventArgs.damage = 30;
			GGNetworkKit.mInstance.DamageToPlayer(ggdamageEventArgs, PhotonView.Find(2001));
		}
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x00103BCC File Offset: 0x00101FCC
	private void Update()
	{
	}
}
