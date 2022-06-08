using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class ObscuredStringTest : MonoBehaviour
{
	// Token: 0x06001898 RID: 6296 RVA: 0x000CD2C4 File Offset: 0x000CB6C4
	private void Start()
	{
		Debug.Log("===== ObscuredStringTest =====\n");
		ObscuredString.SetNewCryptoKey("I LOVE MY GIRL");
		this.cleanString = "Try Goscurry! Or better buy it!";
		Debug.Log("Original string:\n" + this.cleanString);
		this.obscuredString = this.cleanString;
		Debug.Log("How your string is stored in memory when obscured:\n" + this.obscuredString.GetEncrypted());
		this.obscuredString = (this.cleanString = string.Empty);
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x000CD349 File Offset: 0x000CB749
	public void UseRegular()
	{
		this.useRegular = true;
		this.cleanString = "Hey, you can easily change me in memory!";
		this.obscuredString = string.Empty;
		Debug.Log("Try to change this string in memory:\n" + this.cleanString);
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000CD382 File Offset: 0x000CB782
	public void UseObscured()
	{
		this.useRegular = false;
		this.obscuredString = "Hey, you can't change me in memory!";
		this.cleanString = string.Empty;
		Debug.Log("Try to change this string in memory:\n" + this.obscuredString);
	}

	// Token: 0x04001BB5 RID: 7093
	internal string cleanString;

	// Token: 0x04001BB6 RID: 7094
	internal ObscuredString obscuredString;

	// Token: 0x04001BB7 RID: 7095
	internal bool useRegular;
}
