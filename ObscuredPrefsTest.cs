using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class ObscuredPrefsTest : MonoBehaviour
{
	// Token: 0x06001893 RID: 6291 RVA: 0x000CCEA4 File Offset: 0x000CB2A4
	private void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("money");
		PlayerPrefs.DeleteKey("lifeBar");
		PlayerPrefs.DeleteKey("playerName");
		ObscuredPrefs.DeleteKey("money");
		ObscuredPrefs.DeleteKey("lifeBar");
		ObscuredPrefs.DeleteKey("playerName");
		ObscuredPrefs.DeleteKey("gameComplete");
		ObscuredPrefs.DeleteKey("demoLong");
		ObscuredPrefs.DeleteKey("demoDouble");
		ObscuredPrefs.DeleteKey("demoByteArray");
		ObscuredPrefs.DeleteKey("demoVector3");
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x000CCF1F File Offset: 0x000CB31F
	private void Awake()
	{
		ObscuredPrefs.SetNewCryptoKey(this.encryptionKey);
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x000CCF2C File Offset: 0x000CB32C
	public void SaveGame(bool obscured)
	{
		if (obscured)
		{
			ObscuredPrefs.SetInt("money", 1500);
			ObscuredPrefs.SetFloat("lifeBar", 25.9f);
			ObscuredPrefs.SetString("playerName", "focus xD");
			ObscuredPrefs.SetBool("gameComplete", true);
			ObscuredPrefs.SetLong("demoLong", 3457657543456775432L);
			ObscuredPrefs.SetDouble("demoDouble", 345765.1312315678);
			ObscuredPrefs.SetByteArray("demoByteArray", new byte[]
			{
				44,
				104,
				43,
				32
			});
			ObscuredPrefs.SetVector3("demoVector3", new Vector3(123.312f, 453.12344f, 1223f));
			Debug.Log("Game saved using ObscuredPrefs. Try to find and change saved data now! ;)");
		}
		else
		{
			PlayerPrefs.SetInt("money", 2100);
			PlayerPrefs.SetFloat("lifeBar", 88.4f);
			PlayerPrefs.SetString("playerName", "focus :D");
			Debug.Log("Game saved with regular PlayerPrefs. Try to find and change saved data now (it's easy)!");
		}
		ObscuredPrefs.Save();
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x000CD024 File Offset: 0x000CB424
	public void ReadSavedGame(bool obscured)
	{
		if (obscured)
		{
			this.gameData = "Money: " + ObscuredPrefs.GetInt("money") + "\n";
			string text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"Life bar: ",
				ObscuredPrefs.GetFloat("lifeBar"),
				"\n"
			});
			this.gameData = this.gameData + "Player name: " + ObscuredPrefs.GetString("playerName") + "\n";
			text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"bool: ",
				ObscuredPrefs.GetBool("gameComplete"),
				"\n"
			});
			text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"long: ",
				ObscuredPrefs.GetLong("demoLong"),
				"\n"
			});
			text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"double: ",
				ObscuredPrefs.GetDouble("demoDouble"),
				"\n"
			});
			byte[] byteArray = ObscuredPrefs.GetByteArray("demoByteArray", 0, 4);
			text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"Vector3: ",
				ObscuredPrefs.GetVector3("demoVector3"),
				"\n"
			});
			text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"byte[]: {",
				byteArray[0],
				",",
				byteArray[1],
				",",
				byteArray[2],
				",",
				byteArray[3],
				"}"
			});
		}
		else
		{
			this.gameData = "Money: " + PlayerPrefs.GetInt("money") + "\n";
			string text = this.gameData;
			this.gameData = string.Concat(new object[]
			{
				text,
				"Life bar: ",
				PlayerPrefs.GetFloat("lifeBar"),
				"\n"
			});
			this.gameData = this.gameData + "Player name: " + PlayerPrefs.GetString("playerName");
		}
	}

	// Token: 0x04001BB3 RID: 7091
	public string encryptionKey = "change me!";

	// Token: 0x04001BB4 RID: 7092
	internal string gameData = string.Empty;
}
