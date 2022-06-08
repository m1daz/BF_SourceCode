using System;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class GGNetworkModeControl : MonoBehaviour
{
	// Token: 0x0600107D RID: 4221 RVA: 0x0008DF68 File Offset: 0x0008C368
	private void Start()
	{
		this.mModeType = GGNetworkKit.mInstance.GetGameMode();
		if (this.mModeType == GGModeType.TeamDeathMatch)
		{
			if (GameObject.Find("PhotonGame").GetComponent<GGNetworkMode1>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGNetworkMode1>();
				this.SetDibiaoDisable();
			}
		}
		else if (this.mModeType == GGModeType.KillingCompetition)
		{
			if (GameObject.Find("PhotonGame").GetComponent<GGNetworkMode2>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGNetworkMode2>();
				this.SetDibiaoDisable();
			}
		}
		else if (this.mModeType == GGModeType.StrongHold)
		{
			if (GameObject.Find("PhotonGame").GetComponent<GGNetworkMode3>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGNetworkMode3>();
				this.SetDibiaoDisable();
			}
		}
		else if (this.mModeType == GGModeType.Explosion)
		{
			if (GameObject.Find("PhotonGame").GetComponent<GGNetworkMode4>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGNetworkMode4>();
			}
		}
		else if (this.mModeType == GGModeType.Mutation)
		{
			if (GameObject.Find("PhotonGame").GetComponent<GGNetworkMode5>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGNetworkMode5>();
			}
			if (GameObject.Find("PhotonGame").GetComponent<GGMutationModeControl>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGMutationModeControl>();
			}
			this.SetDibiaoDisable();
		}
		else if (this.mModeType == GGModeType.KnifeCompetition)
		{
			if (GameObject.Find("PhotonGame").GetComponent<GGNetworkMode6>() == null)
			{
				GameObject.Find("PhotonGame").AddComponent<GGNetworkMode6>();
				this.SetDibiaoDisable();
			}
		}
		else if (this.mModeType == GGModeType.Hunting && GameObject.Find("PhotonGame").GetComponent<GGNetworkMode7>() == null)
		{
			GameObject.Find("PhotonGame").AddComponent<GGNetworkMode7>();
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0008E164 File Offset: 0x0008C564
	private void Update()
	{
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0008E166 File Offset: 0x0008C566
	private void SetDibiaoDisable()
	{
		if (GameObject.FindWithTag("TimerBombPosition") != null)
		{
			GameObject.FindWithTag("TimerBombPosition").SetActiveRecursively(false);
		}
	}

	// Token: 0x040012DA RID: 4826
	private GGModeType mModeType;
}
