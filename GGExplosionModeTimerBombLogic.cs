using System;
using UnityEngine;

// Token: 0x02000241 RID: 577
public class GGExplosionModeTimerBombLogic : MonoBehaviour
{
	// Token: 0x0600104D RID: 4173 RVA: 0x0008B5E8 File Offset: 0x000899E8
	private void Start()
	{
		GGExplosionModeTimerBombLogic.mInstance = this;
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
		{
			this.isExplosionMode = true;
			this.StartCountDownExplosion = UIPlayDirector.mInstance.startCountDownMutationObj;
			this.ExplosionModeRoundNum = UIModeDirector.mInstance.explosionCurRoundLabel;
		}
		else if (this.ExplosionModeTimerBombPositions != null)
		{
			this.ExplosionModeTimerBombPositions.SetActive(false);
		}
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0008B664 File Offset: 0x00089A64
	private void OnDestroy()
	{
		GGExplosionModeTimerBombLogic.mInstance = null;
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0008B66C File Offset: 0x00089A6C
	private void Update()
	{
		if (this.isExplosionMode)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (this.mGlobalInfo == null)
			{
				return;
			}
			if (this.isShow5sExplosionTimer)
			{
				this.Show5sExplosionTimer();
			}
			if (this.mGlobalInfo.modeInfo.isStartExplosionTimer)
			{
				this.isShow5sExplosionTimer = true;
			}
			else if (this.isShow5sExplosionTimer)
			{
				this.isShow5sExplosionTimer = false;
			}
			if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 5)
			{
				if (this.ExplosionDoor != null && !this.ExplosionDoor.activeSelf)
				{
					this.ExplosionDoor.SetActive(true);
				}
			}
			else if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 0 && this.ExplosionDoor != null && this.ExplosionDoor.activeSelf)
			{
				this.ExplosionDoor.SetActive(false);
			}
			if (this.mGlobalInfo.modeInfo.explosionTimer < 50 && this.mGlobalInfo.modeInfo.explosionTimer > 10)
			{
				this.bombSingleDididiTimeCount += Time.deltaTime;
				if (this.bombSingleDididiTimeCount >= 3f)
				{
					base.GetComponent<AudioSource>().clip = this.bombSingleDididiAudioClip;
					base.GetComponent<AudioSource>().Play();
					this.bombSingleDididiTimeCount = 0f;
				}
			}
			if (this.mGlobalInfo.modeInfo.explosionTimer == 10)
			{
				base.GetComponent<AudioSource>().clip = this.bombDididiAudioClip;
				base.GetComponent<AudioSource>().Play();
			}
			if ((this.mGlobalInfo.modeInfo.totalTimer == 0 || this.mGlobalInfo.modeInfo.IsTimerBombUninstall) && base.GetComponent<AudioSource>().isPlaying)
			{
				base.GetComponent<AudioSource>().Stop();
			}
		}
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0008B864 File Offset: 0x00089C64
	private void Show5sExplosionTimer()
	{
		if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 5)
		{
			UIModeDirector.mInstance.EndWaitTipNode();
			this.StartCountDownExplosion.SetActive(true);
			this.StartCountDownExplosion.GetComponent<UILabel>().text = "5";
			UIModeDirector.mInstance.countdownBgObj.SetActive(true);
			string roundContent = "ROUND " + this.mGlobalInfo.modeInfo.RoundNum.ToString();
			UIModeDirector.mInstance.ShowExplosionCurRoundLabel(roundContent);
			base.GetComponent<AudioSource>().volume = 1f;
			base.GetComponent<AudioSource>().clip = this.ReadyAudioClip;
			base.GetComponent<AudioSource>().Play();
		}
		if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 4)
		{
			this.StartCountDownExplosion.GetComponent<UILabel>().text = "4";
		}
		if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 3)
		{
			UIModeDirector.mInstance.HideExplosionCurRoundLabel();
			this.StartCountDownExplosion.GetComponent<UILabel>().text = "3";
		}
		if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 2)
		{
			this.StartCountDownExplosion.GetComponent<UILabel>().text = "2";
		}
		if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 1)
		{
			this.StartCountDownExplosion.GetComponent<UILabel>().text = "1";
		}
		if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 0)
		{
			UIModeDirector.mInstance.countdownBgObj.SetActive(false);
			this.StartCountDownExplosion.GetComponent<UILabel>().text = " ";
			this.StartCountDownExplosion.SetActive(false);
			this.isShow5sExplosionTimer = false;
			UIModeDirector.mInstance.ShowJoystickNode(true);
			this.cannotControlJoystick = false;
		}
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0008BA34 File Offset: 0x00089E34
	public void Reset()
	{
		UIModeDirector.mInstance.HideExplosionCurRoundLabel();
		this.StartCountDownExplosion.GetComponent<UILabel>().text = " ";
		this.cannotControlJoystick = false;
	}

	// Token: 0x0400125E RID: 4702
	public static GGExplosionModeTimerBombLogic mInstance;

	// Token: 0x0400125F RID: 4703
	private bool isExplosionMode;

	// Token: 0x04001260 RID: 4704
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x04001261 RID: 4705
	private GameObject StartCountDownExplosion;

	// Token: 0x04001262 RID: 4706
	private bool isShow5sExplosionTimer;

	// Token: 0x04001263 RID: 4707
	public GameObject ExplosionDoor;

	// Token: 0x04001264 RID: 4708
	public GameObject ExplosionModeTimerBombPositions;

	// Token: 0x04001265 RID: 4709
	private UILabel ExplosionModeRoundNum;

	// Token: 0x04001266 RID: 4710
	private AudioSource bombExplosionAudio;

	// Token: 0x04001267 RID: 4711
	public AudioClip bombDididiAudioClip;

	// Token: 0x04001268 RID: 4712
	public AudioClip bombSingleDididiAudioClip;

	// Token: 0x04001269 RID: 4713
	public AudioClip ReadyAudioClip;

	// Token: 0x0400126A RID: 4714
	private int preExplosionTimer = 50;

	// Token: 0x0400126B RID: 4715
	public bool cannotControlJoystick;

	// Token: 0x0400126C RID: 4716
	private float bombSingleDididiTimeCount;
}
