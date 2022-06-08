using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class GGJoyStickController : MonoBehaviour
{
	// Token: 0x06000D8B RID: 3467 RVA: 0x000703E7 File Offset: 0x0006E7E7
	private void StartFireStatusDelayHolding()
	{
		this.fireStatusHoldTimeCount = 0.235f;
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x000703F4 File Offset: 0x0006E7F4
	private void CutDownFireStatusDelayTimeCount(float timeTicket)
	{
		this.fireStatusHoldTimeCount -= timeTicket;
		if (this.fireStatusHoldTimeCount <= 0f)
		{
			this.fireStatusHoldTimeCount = 0f;
		}
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x00070420 File Offset: 0x0006E820
	public void Awake()
	{
		PlayerPrefs.SetInt("moveStatus", 0);
		this.audioFootStep = GameObject.Find("PlayerFoot").GetComponent<AudioSource>();
		if (Application.loadedLevelName == "FreeRun3" || Application.loadedLevelName == "FreeRun4" || Application.loadedLevelName == "FreeRun5" || Application.loadedLevelName == "FreeRun6" || Application.loadedLevelName == "FreeRun7" || Application.loadedLevelName == "FreeRun8" || Application.loadedLevelName == "FreeRun9" || Application.loadedLevelName == "FreeRun10" || Application.loadedLevelName == "FreeRun11" || Application.loadedLevelName == "FreeRun12" || Application.loadedLevelName == "FreeRun13" || Application.loadedLevelName == "FreeRun14" || Application.loadedLevelName == "FreeRun15" || Application.loadedLevelName == "FreeRun16" || Application.loadedLevelName == "FreeRun17" || Application.loadedLevelName == "FreeRun18" || Application.loadedLevelName == "FreeRun19" || Application.loadedLevelName == "FreeRun20")
		{
			this.CalFallDamage = false;
			this.cam = GameObject.Find("InGameMenu-Local/Camera").GetComponent<Camera>();
		}
		if (Application.loadedLevelName == "FreeRun3_1" || Application.loadedLevelName == "FreeRun4_1" || Application.loadedLevelName == "FreeRun5_1" || Application.loadedLevelName == "FreeRun6_1" || Application.loadedLevelName == "FreeRun7_1" || Application.loadedLevelName == "FreeRun8_1" || Application.loadedLevelName == "FreeRun9_1" || Application.loadedLevelName == "FreeRun10_1" || Application.loadedLevelName == "FreeRun11_1" || Application.loadedLevelName == "FreeRun12_1" || Application.loadedLevelName == "FreeRun13_1" || Application.loadedLevelName == "FreeRun14_1" || Application.loadedLevelName == "FreeRun15_1" || Application.loadedLevelName == "FreeRun16_1" || Application.loadedLevelName == "FreeRun17_1" || Application.loadedLevelName == "FreeRun18_1" || Application.loadedLevelName == "FreeRun19_1" || Application.loadedLevelName == "FreeRun20_1")
		{
			this.CalFallDamage = false;
			this.cam = GameObject.Find("InGameMenu-Online/Camera").GetComponent<Camera>();
		}
		if (Application.loadedLevelName == "FreeRun" || Application.loadedLevelName == "FreeRun2" || Application.loadedLevelName == "SingleMode_1" || Application.loadedLevelName == "SingleMode_2" || Application.loadedLevelName == "SingleMode_3" || Application.loadedLevelName == "SingleMode_4")
		{
			this.CalFallDamage = true;
			this.cam = GameObject.Find("InGameMenu/Camera").GetComponent<Camera>();
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000707EC File Offset: 0x0006EBEC
	private void Start()
	{
		this.isJumping = false;
		this.charactercontroller = base.GetComponent<CharacterController>();
		this.ladderPlayer = base.GetComponent<GGLadderPlayer>();
		this.lastHeightPosition = base.transform.position.y;
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x00070834 File Offset: 0x0006EC34
	private void Update()
	{
		this.CutDownFireStatusDelayTimeCount(Time.deltaTime);
		if (GGSingleModePauseControl.mInstance != null && GGSingleModePauseControl.mInstance.PauseState)
		{
			if (this.isJumping)
			{
				this.isJumping = false;
			}
			this.lastFallTime = Time.time;
			return;
		}
		if (this.cannotMove)
		{
			if (this.isJumping)
			{
				this.isJumping = false;
			}
			return;
		}
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000708A7 File Offset: 0x0006ECA7
	public void playFootStepAudio()
	{
		this.audioFootStep.Play();
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000708B4 File Offset: 0x0006ECB4
	private void UpdateFallDamage()
	{
		if (this.charactercontroller.velocity.y >= -9f || this.lastHeightPosition == base.transform.position.y)
		{
			if (this.lastFallTime < 0f)
			{
				this.lastFallTime = Time.time;
			}
			else if (this.CalFallDamage)
			{
				float num = Time.time - this.lastFallTime;
				if (num > 0.5f)
				{
				}
				this.lastFallTime = Time.time;
			}
			else
			{
				this.lastFallTime = Time.time;
			}
			this.lastHeightPosition = base.transform.position.y;
		}
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x00070978 File Offset: 0x0006ED78
	private void PlayerStopMoveAndRotate()
	{
		this.cannotMove = true;
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x00070981 File Offset: 0x0006ED81
	private void PlayerCouldMoveAndRotate()
	{
		this.cannotMove = false;
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0007098C File Offset: 0x0006ED8C
	private IEnumerator EnableFallDamage()
	{
		this.CalFallDamage = false;
		yield return new WaitForSeconds(2f);
		this.CalFallDamage = true;
		yield break;
	}

	// Token: 0x04000DAF RID: 3503
	public float moveSpeed = 7f;

	// Token: 0x04000DB0 RID: 3504
	public float fireRate;

	// Token: 0x04000DB1 RID: 3505
	private CharacterController charactercontroller;

	// Token: 0x04000DB2 RID: 3506
	private int ctrlBulletNum;

	// Token: 0x04000DB3 RID: 3507
	private bool isWalk;

	// Token: 0x04000DB4 RID: 3508
	private AudioSource audioFootStep;

	// Token: 0x04000DB5 RID: 3509
	private float stepTime;

	// Token: 0x04000DB6 RID: 3510
	private Camera cam;

	// Token: 0x04000DB7 RID: 3511
	private float jumpTime = -10f;

	// Token: 0x04000DB8 RID: 3512
	private bool isJumping;

	// Token: 0x04000DB9 RID: 3513
	private float fireStatusHoldTimeCount;

	// Token: 0x04000DBA RID: 3514
	private float lastFallTime = -10f;

	// Token: 0x04000DBB RID: 3515
	private float lastHeightPosition;

	// Token: 0x04000DBC RID: 3516
	private bool cannotMove;

	// Token: 0x04000DBD RID: 3517
	public bool CalFallDamage = true;

	// Token: 0x04000DBE RID: 3518
	private GGLadderPlayer ladderPlayer;

	// Token: 0x04000DBF RID: 3519
	private Vector3 lateralMove = Vector3.zero;

	// Token: 0x04000DC0 RID: 3520
	private Vector3 forwardMove = Vector3.zero;

	// Token: 0x04000DC1 RID: 3521
	private Vector3 ladderMovement = Vector3.zero;

	// Token: 0x04000DC2 RID: 3522
	public float climbSpeed = 1f;

	// Token: 0x04000DC3 RID: 3523
	private bool isonladder;
}
