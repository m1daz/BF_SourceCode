using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class GameProtecter : MonoBehaviour
{
	// Token: 0x060020D7 RID: 8407 RVA: 0x000F58A8 File Offset: 0x000F3CA8
	private void Awake()
	{
		GameProtecter.mInstance = this;
		if (GameProtecter.GameProtecterRef == null)
		{
			GameProtecter.GameProtecterRef = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		Application.runInBackground = true;
		Application.targetFrameRate = 60;
	}

	// Token: 0x060020D8 RID: 8408 RVA: 0x000F58F9 File Offset: 0x000F3CF9
	private void Start()
	{
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x000F58FB File Offset: 0x000F3CFB
	private void Update()
	{
		this.mOpTimeForCheckData += Time.deltaTime;
		if (this.mOpTimeForCheckData > GameProtecter.OPIntervalForCheckData)
		{
			this.mOpTimeForCheckData = 0f;
			this.CheckInvalidUserData();
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x000F5930 File Offset: 0x000F3D30
	public string EncryptInt1(int value)
	{
		if (this.mUserName == string.Empty)
		{
			this.mUserName = UIUserDataController.GetDefaultUserName();
		}
		string text = this.mUserName;
		string text2 = string.Empty;
		char[] array = value.ToString().ToCharArray();
		char[] array2 = text.ToCharArray();
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			text2 += (array[i] ^ array2[num]).ToString();
		}
		return text2;
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x000F59C0 File Offset: 0x000F3DC0
	public void WarningPlayer()
	{
		GGCloudServiceKit.mInstance.AddBlackList();
		UITipController.mInstance.SetTipData(UITipController.TipType.NoButtonTitleTip, "Data exception, Please don't use the third-party software, we will seal number if you go on using it!", Color.white, string.Empty, string.Empty, null, null, null);
		base.StartCoroutine(this.KickOut());
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x000F5A08 File Offset: 0x000F3E08
	private IEnumerator KickOut()
	{
		yield return new WaitForSeconds(4f);
		UISceneManager.mInstance.LoadLevel("MainMenu");
		yield break;
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x000F5A1C File Offset: 0x000F3E1C
	public void SetEncryptVariable(ref int EncryptVariable, ref string ResultEncryptVariable, int Value)
	{
		if (this.EncryptInt1(EncryptVariable).Equals(ResultEncryptVariable) || ResultEncryptVariable.Equals(string.Empty))
		{
			EncryptVariable = Value;
			ResultEncryptVariable = this.EncryptInt1(EncryptVariable);
		}
		else
		{
			this.WarningPlayer();
		}
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x000F5A5C File Offset: 0x000F3E5C
	public void SetEncryptVariable2(ref int EncryptVariable, ref string ResultEncryptVariable, int Value)
	{
		Debug.Log(Value);
		if (this.EncryptInt1(EncryptVariable).Equals(ResultEncryptVariable) || ResultEncryptVariable.Equals(string.Empty))
		{
			EncryptVariable = Value;
			ResultEncryptVariable = this.EncryptInt1(EncryptVariable);
		}
		else
		{
			this.WarningPlayer();
		}
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x000F5AB1 File Offset: 0x000F3EB1
	public void CheckInvalidUserData()
	{
		if (UserDataController.GetGems() > 80000 && Application.loadedLevelName != "UILogin")
		{
			this.LPlayer("Get gems by invalid ways");
		}
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x000F5AE4 File Offset: 0x000F3EE4
	private void LPlayer(string reason)
	{
		if (!this.mHaveDeleteUser)
		{
			this.mHaveDeleteUser = true;
			GGCloudServiceKit.mInstance.AddBlackListToDelete(reason);
			string defaultUserName = UIUserDataController.GetDefaultUserName();
			GGCloudServiceKit.mInstance.LUser(defaultUserName);
		}
		EventDelegate btnEventName = new EventDelegate(this, "DisplayWarningDeletePlayer");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "You get gems by invalid ways,which endanger our development,we will seal your number!", Color.white, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x000F5B4D File Offset: 0x000F3F4D
	private void DisplayWarningDeletePlayer()
	{
		Application.Quit();
	}

	// Token: 0x040021B0 RID: 8624
	public static GameProtecter mInstance;

	// Token: 0x040021B1 RID: 8625
	private static GameProtecter GameProtecterRef;

	// Token: 0x040021B2 RID: 8626
	private string mUserName = string.Empty;

	// Token: 0x040021B3 RID: 8627
	private bool mHaveDeleteUser;

	// Token: 0x040021B4 RID: 8628
	private float mOpTimeForCheckData;

	// Token: 0x040021B5 RID: 8629
	private static float OPIntervalForCheckData = 4f;
}
