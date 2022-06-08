using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class ObscuredIntTest : MonoBehaviour
{
	// Token: 0x0600188E RID: 6286 RVA: 0x000CCCA8 File Offset: 0x000CB0A8
	private void Start()
	{
		Debug.Log("===== ObscuredIntTest =====\n");
		this.cleanLivesCount = 5;
		Debug.Log("Original lives count:\n" + this.cleanLivesCount);
		this.obscuredLivesCount = this.cleanLivesCount;
		Debug.Log("How your lives count is stored in memory when obscured:\n" + this.obscuredLivesCount.GetEncrypted());
		ObscuredInt.SetNewCryptoKey(666);
		ObscuredInt obscuredInt = 100;
		obscuredInt -= 10;
		obscuredInt += 100;
		obscuredInt /= 10;
		ObscuredInt.SetNewCryptoKey(888);
		obscuredInt = ++obscuredInt;
		ObscuredInt.SetNewCryptoKey(999);
		obscuredInt = ++obscuredInt;
		obscuredInt = --obscuredInt;
		Debug.Log(string.Concat(new object[]
		{
			"Lives count: ",
			obscuredInt,
			" (",
			obscuredInt.ToString("X"),
			"h)"
		}));
		ObscuredInt.onCheatingDetected = new Action(this.OnCheatingDetected);
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x000CCDC9 File Offset: 0x000CB1C9
	private void OnCheatingDetected()
	{
		Debug.Log("Cheating detected!");
		this.cheatingDetected = true;
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x000CCDDC File Offset: 0x000CB1DC
	public void UseRegular()
	{
		this.useRegular = true;
		this.cleanLivesCount += UnityEngine.Random.Range(-10, 50);
		this.obscuredLivesCount = 11;
		Debug.Log("Try to change this int in memory:\n" + this.cleanLivesCount);
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x000CCE30 File Offset: 0x000CB230
	public void UseObscured()
	{
		this.useRegular = false;
		this.obscuredLivesCount += UnityEngine.Random.Range(-10, 50);
		this.cleanLivesCount = 11;
		Debug.Log("Try to change this int in memory:\n" + this.obscuredLivesCount);
	}

	// Token: 0x04001BAF RID: 7087
	internal int cleanLivesCount = 11;

	// Token: 0x04001BB0 RID: 7088
	internal ObscuredInt obscuredLivesCount = 11;

	// Token: 0x04001BB1 RID: 7089
	internal bool useRegular;

	// Token: 0x04001BB2 RID: 7090
	internal bool cheatingDetected;
}
