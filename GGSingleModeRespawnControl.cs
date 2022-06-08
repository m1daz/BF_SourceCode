using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class GGSingleModeRespawnControl : MonoBehaviour
{
	// Token: 0x060011DE RID: 4574 RVA: 0x000A286E File Offset: 0x000A0C6E
	private void Awake()
	{
		GGSingleModeRespawnControl.mInstance = this;
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000A2876 File Offset: 0x000A0C76
	private void OnDestroy()
	{
		GGSingleModeRespawnControl.mInstance = null;
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000A287E File Offset: 0x000A0C7E
	private void OnDisable()
	{
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000A2880 File Offset: 0x000A0C80
	private void Start()
	{
		GGSingleModeRespawnControl.mInstance = this;
		int[] respawnPrice = new int[]
		{
			10,
			15,
			20,
			25,
			30
		};
		this.RespawnPrice = respawnPrice;
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000A28AC File Offset: 0x000A0CAC
	private void Update()
	{
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x000A28B0 File Offset: 0x000A0CB0
	private void OnYesToRespawn()
	{
		if (PlayerPrefs.GetInt("GameGems", 0) > this.RespawnPrice[5 - PlayerPrefs.GetInt("SingleModeRespawnLimit", 5)])
		{
			PlayerPrefs.SetInt("GameGems", PlayerPrefs.GetInt("GameGems", 0) - this.RespawnPrice[5 - PlayerPrefs.GetInt("SingleModeRespawnLimit", 5)]);
			PlayerPrefs.SetInt("SingleModeRespawnLimit", PlayerPrefs.GetInt("SingleModeRespawnLimit", 5) - 1);
			base.gameObject.BroadcastMessage("EnableFallDamage", SendMessageOptions.DontRequireReceiver);
			base.StartCoroutine(this.GeneratePlayerInSingleMode(0.2f));
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x000A2948 File Offset: 0x000A0D48
	private void OnNoToRespawn()
	{
		this.singleModePointGet = PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0);
		this.singleModeCoinGet = (int)((float)this.singleModePointGet * 0.5f);
		this.singleModeExpGet = (int)((float)this.singleModePointGet * 0.25f);
		this.singleModeScoreGet = (int)((float)this.singleModePointGet * 5f);
		PlayerPrefs.SetInt("SingleModeChapterOneHighScore", Mathf.Max(PlayerPrefs.GetInt("SingleModeChapterOneHighScore", 0), this.singleModeScoreGet));
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x000A29C4 File Offset: 0x000A0DC4
	private IEnumerator GeneratePlayerInSingleMode(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.transform.position = GameObject.Find("SpawnPosition").transform.position;
		GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled = true;
		yield break;
	}

	// Token: 0x040014C5 RID: 5317
	public static GGSingleModeRespawnControl mInstance;

	// Token: 0x040014C6 RID: 5318
	public int[] RespawnPrice = new int[5];

	// Token: 0x040014C7 RID: 5319
	public int singleModePointGet;

	// Token: 0x040014C8 RID: 5320
	public int singleModeCoinGet;

	// Token: 0x040014C9 RID: 5321
	public int singleModeExpGet;

	// Token: 0x040014CA RID: 5322
	public int singleModeScoreGet;

	// Token: 0x040014CB RID: 5323
	private GameObject goDied;
}
