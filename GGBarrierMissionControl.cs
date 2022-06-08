using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class GGBarrierMissionControl : MonoBehaviour
{
	// Token: 0x0600115B RID: 4443 RVA: 0x0009ABF8 File Offset: 0x00098FF8
	private void Start()
	{
		this.shippilotsRescued = false;
		this.airplanepilotRescued = false;
		this.foodNum = 0;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0009AC0F File Offset: 0x0009900F
	private void Update()
	{
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0009AC14 File Offset: 0x00099014
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("SingleModeOneExit"))
		{
			this.PlayerCannotMoveAndRotate();
			switch (GGSingleEnemyManager.mInstance.Difficulty)
			{
			case 1:
				PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 20);
				PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 20);
				break;
			case 2:
				PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 40);
				PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 40);
				break;
			case 3:
				PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 60);
				PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 60);
				break;
			}
			base.StartCoroutine(this.LoadLevelTwo(2f));
		}
		if (other.gameObject.name.Equals("SingleModeOneLanShen"))
		{
			this.PlayerCannotMoveAndRotate();
			base.StartCoroutine(this.ChangePlayerPositionLevelOne(1.5f));
		}
		if (other.gameObject.name.Equals("FireDamageTrigger"))
		{
		}
		if (other.gameObject.name.Equals("TiedingDamageTrigger"))
		{
		}
		if (other.gameObject.name.Equals("SingleModeTwoShipController"))
		{
			if (this.shippilotsRescued)
			{
				switch (GGSingleEnemyManager.mInstance.Difficulty)
				{
				case 1:
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 30);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 30);
					break;
				case 2:
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 60);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 60);
					break;
				case 3:
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 90);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 90);
					break;
				}
				base.StartCoroutine(this.LoadLevelThree(2f));
			}
			else
			{
				base.StartCoroutine(this.ClearMessage(3f));
			}
		}
		if (other.gameObject.name.Equals("SingleModeTwoGotoShip"))
		{
			this.ChangePlayerPositionToShip();
			base.StartCoroutine(this.ClearMessage(3f));
		}
		if (other.gameObject.name.Contains("SingleModeThreeFood"))
		{
			this.foodNum++;
			UnityEngine.Object.Destroy(other.gameObject);
			if (this.foodNum < 4)
			{
				base.StartCoroutine(this.ClearMessage(3f));
			}
			if (this.foodNum == 4)
			{
				GameObject.Find("guangzhuone").GetComponent<Renderer>().enabled = true;
				GameObject.Find("guangzhutwo").GetComponent<Renderer>().enabled = true;
				base.StartCoroutine(this.ClearMessage(3f));
			}
		}
		if (other.gameObject.name.Contains("SingleModeThreeChuanSong") && this.foodNum >= 4)
		{
			switch (GGSingleEnemyManager.mInstance.Difficulty)
			{
			case 1:
				PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 50);
				PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 50);
				break;
			case 2:
				PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 80);
				PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 80);
				break;
			case 3:
				PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 100);
				PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 100);
				break;
			}
			base.StartCoroutine(this.LoadLevelFour(2f));
		}
		if (other.gameObject.name.Contains("SingleModeAirplanePilot"))
		{
			other.gameObject.transform.position = new Vector3(520.6f, 7.5f, -5f);
			this.ChangeAirplanePilotsPosition();
		}
		if (other.gameObject.name.Equals("SingleModeFourAirplane"))
		{
			if (this.airplanepilotRescued)
			{
				switch (GGSingleEnemyManager.mInstance.Difficulty)
				{
				case 1:
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 80);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 80);
					PlayerPrefs.SetInt("SingleModePassEasy", 1);
					PlayerPrefs.SetInt("SingleModeChapterOneHighScore", Mathf.Max(PlayerPrefs.GetInt("SingleModeChapterOneHighScore", 0), PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) * 5));
					base.StartCoroutine(this.PassAllLv(3f));
					break;
				case 2:
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 100);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 100);
					PlayerPrefs.SetInt("SingleModePassNormal", 1);
					PlayerPrefs.SetInt("SingleModeChapterOneHighScore", Mathf.Max(PlayerPrefs.GetInt("SingleModeChapterOneHighScore", 0), PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) * 5));
					base.StartCoroutine(this.PassAllLv(3f));
					break;
				case 3:
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + 130);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + 130);
					PlayerPrefs.SetInt("SingleModePassHard", 1);
					PlayerPrefs.SetInt("SingleModeChapterOneHighScore", Mathf.Max(PlayerPrefs.GetInt("SingleModeChapterOneHighScore", 0), PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) * 5));
					base.StartCoroutine(this.PassAllLv(3f));
					break;
				}
			}
			else
			{
				base.StartCoroutine(this.ClearMessage(3f));
			}
		}
		if (other.gameObject.name.Equals("SingleModeWaterTrigger"))
		{
		}
		if (other.gameObject.name.Contains("coinPak"))
		{
			switch (GGSingleEnemyManager.mInstance.Difficulty)
			{
			case 1:
			{
				int num = UnityEngine.Random.Range(20, 51);
				break;
			}
			case 2:
			{
				int num2 = UnityEngine.Random.Range(40, 101);
				break;
			}
			case 3:
			{
				int num3 = UnityEngine.Random.Range(60, 151);
				break;
			}
			}
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.gameObject.name.Contains("jewelPak"))
		{
			switch (GGSingleEnemyManager.mInstance.Difficulty)
			{
			case 1:
			{
				int num4 = UnityEngine.Random.Range(1, 2);
				break;
			}
			case 2:
			{
				int num5 = UnityEngine.Random.Range(1, 3);
				break;
			}
			case 3:
			{
				int num6 = UnityEngine.Random.Range(1, 4);
				break;
			}
			}
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x0009B388 File Offset: 0x00099788
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name.Equals("TiedingDamageTrigger"))
		{
		}
		if (other.gameObject.name.Equals("FireDamageTrigger"))
		{
		}
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x0009B3C0 File Offset: 0x000997C0
	private IEnumerator LoadLevelTwo(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.PlayerCanMoveAndRotate();
		Application.LoadLevel("SingleMode_2");
		yield break;
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x0009B3E4 File Offset: 0x000997E4
	private IEnumerator ChangePlayerPositionLevelOne(float delay)
	{
		base.gameObject.BroadcastMessage("EnableFallDamage", SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(delay);
		base.transform.position = new Vector3(-6f, 3f, 67f);
		this.PlayerCanMoveAndRotate();
		yield break;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0009B406 File Offset: 0x00099806
	private void PlayerCannotMoveAndRotate()
	{
		base.gameObject.BroadcastMessage("PlayerStopMoveAndRotate", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0009B419 File Offset: 0x00099819
	private void PlayerCanMoveAndRotate()
	{
		base.gameObject.BroadcastMessage("PlayerCouldMoveAndRotate", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x0009B42C File Offset: 0x0009982C
	private IEnumerator LoadLevelThree(float delay)
	{
		yield return new WaitForSeconds(delay);
		Application.LoadLevel("SingleMode_3");
		yield break;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0009B447 File Offset: 0x00099847
	private void ChangePlayerPositionToShip()
	{
		base.gameObject.BroadcastMessage("EnableFallDamage", SendMessageOptions.DontRequireReceiver);
		base.transform.position = new Vector3(11f, 8f, 51f);
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0009B47C File Offset: 0x0009987C
	private void RescueCrew()
	{
		this.shippilotsRescued = true;
		GameObject.Find("SingleModeCrew").transform.position = new Vector3(-30.2f, 13.5f, 68.8f);
		base.StartCoroutine(this.ClearMessage(3f));
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x0009B4CC File Offset: 0x000998CC
	private void BombDestroy()
	{
		GameObject.Find("SingleModeTwoBombs").transform.position -= new Vector3(0f, 10f, 0f);
		base.StartCoroutine(this.ClearMessage(3f));
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x0009B520 File Offset: 0x00099920
	private IEnumerator LoadLevelFour(float delay)
	{
		yield return new WaitForSeconds(delay);
		Application.LoadLevel("SingleMode_4");
		yield break;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0009B53B File Offset: 0x0009993B
	private void ChangeAirplanePilotsPosition()
	{
		this.airplanepilotRescued = true;
		base.StartCoroutine(this.ClearMessage(3f));
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0009B558 File Offset: 0x00099958
	private IEnumerator PassAllLv(float delay)
	{
		yield return new WaitForSeconds(delay);
		yield break;
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0009B574 File Offset: 0x00099974
	private IEnumerator ClearMessage(float delay)
	{
		yield return new WaitForSeconds(delay);
		yield break;
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0009B590 File Offset: 0x00099990
	private void EnemyGenerate()
	{
		string empty = string.Empty;
		string loadedLevelName = Application.loadedLevelName;
		if (loadedLevelName != null)
		{
			if (!(loadedLevelName == "SingleMode_1"))
			{
				if (!(loadedLevelName == "SingleMode_2"))
				{
					if (!(loadedLevelName == "SingleMode_3"))
					{
						if (!(loadedLevelName == "SingleMode_4"))
						{
						}
					}
				}
			}
		}
		base.StartCoroutine(this.ClearMessage(3f));
	}

	// Token: 0x04001407 RID: 5127
	public bool shippilotsRescued;

	// Token: 0x04001408 RID: 5128
	public bool airplanepilotRescued;

	// Token: 0x04001409 RID: 5129
	public int foodNum;
}
