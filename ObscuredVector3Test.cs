using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class ObscuredVector3Test : MonoBehaviour
{
	// Token: 0x0600189C RID: 6300 RVA: 0x000CD414 File Offset: 0x000CB814
	private void Start()
	{
		Debug.Log("===== ObscuredVector3Test =====\n");
		ObscuredVector3.SetNewCryptoKey(404);
		this.playerPosition = new Vector3(54.1f, 64.3f, 63.2f);
		Debug.Log("Original position:\n" + this.playerPosition);
		this.obscuredPlayerPosition = this.playerPosition;
		Vector3 encrypted = this.obscuredPlayerPosition.GetEncrypted();
		Debug.Log(string.Concat(new string[]
		{
			"How your position is stored in memory when obscured:\n(",
			encrypted.x.ToString("0.000"),
			", ",
			encrypted.y.ToString("0.000"),
			", ",
			encrypted.z.ToString("0.000"),
			")"
		}));
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000CD4F4 File Offset: 0x000CB8F4
	public void UseRegular()
	{
		this.useRegular = true;
		this.playerPosition += new Vector3(UnityEngine.Random.Range(-10f, 50f), UnityEngine.Random.Range(-10f, 50f), UnityEngine.Random.Range(-10f, 50f));
		this.obscuredPlayerPosition = new Vector3(10.5f, 11.5f, 12.5f);
		Debug.Log("Try to change this Vector3 in memory:\n" + this.playerPosition);
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000CD584 File Offset: 0x000CB984
	public void UseObscured()
	{
		this.useRegular = false;
		this.obscuredPlayerPosition += new Vector3(UnityEngine.Random.Range(-10f, 50f), UnityEngine.Random.Range(-10f, 50f), UnityEngine.Random.Range(-10f, 50f));
		this.playerPosition = new Vector3(10.5f, 11.5f, 12.5f);
		Debug.Log("Try to change this Vector3 in memory:\n" + this.obscuredPlayerPosition);
	}

	// Token: 0x04001BB8 RID: 7096
	internal Vector3 playerPosition = new Vector3(10.5f, 11.5f, 12.5f);

	// Token: 0x04001BB9 RID: 7097
	internal ObscuredVector3 obscuredPlayerPosition = new Vector3(10.5f, 11.5f, 12.5f);

	// Token: 0x04001BBA RID: 7098
	internal bool useRegular = true;
}
