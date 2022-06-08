using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class ObscuredFloatTest : MonoBehaviour
{
	// Token: 0x06001889 RID: 6281 RVA: 0x000CCAD0 File Offset: 0x000CAED0
	private void Start()
	{
		Debug.Log("===== ObscuredFloatTest =====\n");
		ObscuredFloat.SetNewCryptoKey(404);
		this.healthBar = 99.9f;
		Debug.Log("Original health bar:\n" + this.healthBar);
		this.obscuredHealthBar = this.healthBar;
		Debug.Log("How your health bar is stored in memory when obscured:\n" + this.obscuredHealthBar.GetEncrypted());
		float num = 100f;
		ObscuredFloat obscuredFloat = 60.3f;
		ObscuredFloat.SetNewCryptoKey(666);
		obscuredFloat = ++obscuredFloat;
		obscuredFloat -= 2f;
		obscuredFloat = --obscuredFloat;
		obscuredFloat = num - obscuredFloat;
		this.obscuredHealthBar = (this.healthBar = 0f);
		ObscuredFloat.onCheatingDetected = new Action(this.OnCheatingDetected);
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x000CCBC5 File Offset: 0x000CAFC5
	private void OnCheatingDetected()
	{
		this.cheatingDetected = true;
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000CCBD0 File Offset: 0x000CAFD0
	public void UseRegular()
	{
		this.useRegular = true;
		this.healthBar += UnityEngine.Random.Range(-10f, 50f);
		this.obscuredHealthBar = 11f;
		Debug.Log("Try to change this float in memory:\n" + this.healthBar);
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x000CCC2C File Offset: 0x000CB02C
	public void UseObscured()
	{
		this.useRegular = false;
		this.obscuredHealthBar += UnityEngine.Random.Range(-10f, 50f);
		this.healthBar = 11f;
		Debug.Log("Try to change this float in memory:\n" + this.obscuredHealthBar);
	}

	// Token: 0x04001BAB RID: 7083
	internal float healthBar = 11.4f;

	// Token: 0x04001BAC RID: 7084
	internal ObscuredFloat obscuredHealthBar = 11.4f;

	// Token: 0x04001BAD RID: 7085
	internal bool useRegular = true;

	// Token: 0x04001BAE RID: 7086
	internal bool cheatingDetected;
}
