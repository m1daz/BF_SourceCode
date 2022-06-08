using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class GGExplosionModeTimerBomb : MonoBehaviour
{
	// Token: 0x06001047 RID: 4167 RVA: 0x0008AE90 File Offset: 0x00089290
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		this.C4 = base.transform.Find("TimerBomb").gameObject;
		this.MainPlayer = GGNetworkKit.mInstance.GetMainPlayer();
		if (this.MainPlayer != null)
		{
			this.mNetWorkPlayerlogic = this.MainPlayer.GetComponent<GGNetWorkPlayerlogic>();
			this.mGGNetworkCharacter = this.MainPlayer.GetComponent<GGNetworkCharacter>();
		}
		this.TimerBomb3DIcon = base.transform.Find("TimerBomb3DIcon");
		if (base.gameObject.name.Contains("ExplosionModeTimerBombDroped"))
		{
			this.isDroped = true;
			base.GetComponent<Rigidbody>().velocity = new Vector3(3f, 0f, 0f);
		}
		else
		{
			this.isInstalled = true;
		}
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0008AF70 File Offset: 0x00089370
	private void Update()
	{
		if (this.mGlobalInfo == null)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
			return;
		}
		if (this.MainPlayer == null)
		{
			if (Time.frameCount % 32 == 0)
			{
				this.MainPlayer = GameObject.FindWithTag("Player");
				if (this.MainPlayer != null)
				{
					this.mNetWorkPlayerlogic = this.MainPlayer.GetComponent<GGNetWorkPlayerlogic>();
					this.mGGNetworkCharacter = this.MainPlayer.GetComponent<GGNetworkCharacter>();
				}
			}
			return;
		}
		if (this.isInstalled && !this.IsC4Explosioned && this.mGlobalInfo.modeInfo.explosionTimer == 0)
		{
			if (this.MainPlayer != null)
			{
				this.MainPlayer.transform.Find("LookObject/Main Camera").GetComponent<Animation>().Play();
			}
			this.C4.GetComponent<Renderer>().enabled = false;
			UnityEngine.Object.Instantiate<GameObject>(this.TimerBombExplosion, base.transform.position, base.transform.rotation);
			this.IsC4Explosioned = true;
		}
		if (this.isDroped)
		{
			if (!this.mGGNetworkCharacter.mPlayerProperties.isObserver)
			{
				this.TimerBomb3DIcon.LookAt(this.MainPlayer.transform, Vector3.up);
				this.TimerBomb3DIcon.localEulerAngles = new Vector3(90f, this.TimerBomb3DIcon.localEulerAngles.y, this.TimerBomb3DIcon.localEulerAngles.z);
			}
			else if (this.mNetWorkPlayerlogic.ObserverCamera != null)
			{
				this.TimerBomb3DIcon.LookAt(this.mNetWorkPlayerlogic.ObserverCamera.transform, Vector3.up);
				this.TimerBomb3DIcon.localEulerAngles = new Vector3(90f, this.TimerBomb3DIcon.localEulerAngles.y, this.TimerBomb3DIcon.localEulerAngles.z);
			}
			this.floatingTimeCount += Time.deltaTime;
			if (this.floatingTimeCount < 2f)
			{
				if (this.floatingTimeCount < 1f)
				{
					this.TimerBomb3DIcon.localPosition += new Vector3(0f, 0.3f * Time.deltaTime, 0f);
					this.TimerBomb3DIcon.localPosition = Vector3.Min(this.TimerBomb3DIcon.localPosition, new Vector3(0f, 2.3f, 0f));
				}
				else if (this.floatingTimeCount > 1f)
				{
					this.TimerBomb3DIcon.localPosition -= new Vector3(0f, 0.3f * Time.deltaTime, 0f);
					this.TimerBomb3DIcon.localPosition = Vector3.Max(this.TimerBomb3DIcon.localPosition, new Vector3(0f, 1.7f, 0f));
				}
			}
			else
			{
				this.floatingTimeCount = 0f;
			}
		}
	}

	// Token: 0x0400124E RID: 4686
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x0400124F RID: 4687
	private bool IsC4Explosioned;

	// Token: 0x04001250 RID: 4688
	public GameObject TimerBombExplosion;

	// Token: 0x04001251 RID: 4689
	private GameObject C4;

	// Token: 0x04001252 RID: 4690
	private GameObject MainPlayer;

	// Token: 0x04001253 RID: 4691
	private Transform TimerBomb3DIcon;

	// Token: 0x04001254 RID: 4692
	private GGNetWorkPlayerlogic mNetWorkPlayerlogic;

	// Token: 0x04001255 RID: 4693
	private GGNetworkCharacter mGGNetworkCharacter;

	// Token: 0x04001256 RID: 4694
	private float floatingTimeCount;

	// Token: 0x04001257 RID: 4695
	private bool isDroped;

	// Token: 0x04001258 RID: 4696
	private bool isInstalled;
}
