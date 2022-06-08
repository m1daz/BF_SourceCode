using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class ActTesterGUI : MonoBehaviour
{
	// Token: 0x0600187F RID: 6271 RVA: 0x000CC40B File Offset: 0x000CA80B
	private void Awake()
	{
		ObscuredPrefs.onAlterationDetected = new Action(this.SavesAlterationDetected);
		ObscuredPrefs.onPossibleForeignSavesDetected = new Action(this.ForeignSavesDetected);
		this.detectorsUsageExample = (DetectorsUsageExample)UnityEngine.Object.FindObjectOfType(typeof(DetectorsUsageExample));
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000CC449 File Offset: 0x000CA849
	private void SavesAlterationDetected()
	{
		this.savesAlterationDetected = true;
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x000CC452 File Offset: 0x000CA852
	private void ForeignSavesDetected()
	{
		this.foreignSavesDetected = true;
	}

	// Token: 0x06001882 RID: 6274 RVA: 0x000CC45C File Offset: 0x000CA85C
	private void OnGUI()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		this.CenteredLabel("Memory cheating protection");
		GUILayout.Space(10f);
		if (this.obscuredStringTest && this.obscuredStringTest.enabled)
		{
			if (GUILayout.Button("Use regular string", new GUILayoutOption[0]))
			{
				this.obscuredStringTest.UseRegular();
			}
			if (GUILayout.Button("Use obscured string", new GUILayoutOption[0]))
			{
				this.obscuredStringTest.UseObscured();
			}
			string str;
			if (this.obscuredStringTest.useRegular)
			{
				str = this.obscuredStringTest.cleanString;
			}
			else
			{
				str = this.obscuredStringTest.obscuredString;
			}
			GUILayout.Label("Current string (try to change it!):\n" + str, new GUILayoutOption[0]);
		}
		if (this.obscuredIntTest && this.obscuredIntTest.enabled)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button("Use regular int (click to generate new number)", new GUILayoutOption[0]))
			{
				this.obscuredIntTest.UseRegular();
			}
			if (GUILayout.Button("Use ObscuredInt (click to generate new number)", new GUILayoutOption[0]))
			{
				this.obscuredIntTest.UseObscured();
			}
			int num;
			if (this.obscuredIntTest.useRegular)
			{
				num = this.obscuredIntTest.cleanLivesCount;
			}
			else
			{
				num = this.obscuredIntTest.obscuredLivesCount;
			}
			GUILayout.Label("Current lives count (try to change them!):\n" + num, new GUILayoutOption[0]);
			if (this.obscuredIntTest.cheatingDetected)
			{
				GUILayout.Label("ObscuredInt cheating try detected!", new GUILayoutOption[0]);
			}
		}
		if (this.obscuredFloatTest && this.obscuredFloatTest.enabled)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button("Use regular float (click to generate new number)", new GUILayoutOption[0]))
			{
				this.obscuredFloatTest.UseRegular();
			}
			if (GUILayout.Button("Use ObscuredFloat (click to generate new number)", new GUILayoutOption[0]))
			{
				this.obscuredFloatTest.UseObscured();
			}
			float num2;
			if (this.obscuredFloatTest.useRegular)
			{
				num2 = this.obscuredFloatTest.healthBar;
			}
			else
			{
				num2 = this.obscuredFloatTest.obscuredHealthBar;
			}
			GUILayout.Label("Current health bar (try to change it!):\n" + string.Format("{0:0.000}", num2), new GUILayoutOption[0]);
			if (this.obscuredFloatTest.cheatingDetected)
			{
				GUILayout.Label("ObscuredFloat cheating try detected!", new GUILayoutOption[0]);
			}
		}
		if (this.obscuredVector3Test && this.obscuredVector3Test.enabled)
		{
			GUILayout.Space(10f);
			if (GUILayout.Button("Use regular Vector3 (click to generate new one)", new GUILayoutOption[0]))
			{
				this.obscuredVector3Test.UseRegular();
			}
			if (GUILayout.Button("Use ObscuredVector3 (click to generate new one)", new GUILayoutOption[0]))
			{
				this.obscuredVector3Test.UseObscured();
			}
			Vector3 vector;
			if (this.obscuredVector3Test.useRegular)
			{
				vector = this.obscuredVector3Test.playerPosition;
			}
			else
			{
				vector = this.obscuredVector3Test.obscuredPlayerPosition;
			}
			GUILayout.Label("Current player position (try to change it!):\n" + vector, new GUILayoutOption[0]);
		}
		GUILayout.Space(10f);
		GUILayout.EndVertical();
		GUILayout.Space(10f);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		this.CenteredLabel("Saves cheating protection");
		GUILayout.Space(10f);
		if (this.obscuredPrefsTest && this.obscuredPrefsTest.enabled)
		{
			if (GUILayout.Button("Save game with regular PlayerPrefs!", new GUILayoutOption[0]))
			{
				this.obscuredPrefsTest.SaveGame(false);
			}
			if (GUILayout.Button("Read data saved with regular PlayerPrefs", new GUILayoutOption[0]))
			{
				this.obscuredPrefsTest.ReadSavedGame(false);
			}
			GUILayout.Space(10f);
			if (GUILayout.Button("Save game with ObscuredPrefs!", new GUILayoutOption[0]))
			{
				this.obscuredPrefsTest.SaveGame(true);
			}
			if (GUILayout.Button("Read data saved with ObscuredPrefs", new GUILayoutOption[0]))
			{
				this.obscuredPrefsTest.ReadSavedGame(true);
			}
			ObscuredPrefs.preservePlayerPrefs = GUILayout.Toggle(ObscuredPrefs.preservePlayerPrefs, "preservePlayerPrefs", new GUILayoutOption[0]);
			ObscuredPrefs.emergencyMode = GUILayout.Toggle(ObscuredPrefs.emergencyMode, "emergencyMode", new GUILayoutOption[0]);
			GUILayout.Label("LockToDevice level:", new GUILayoutOption[0]);
			this.savesLock = GUILayout.SelectionGrid(this.savesLock, new string[]
			{
				ObscuredPrefs.DeviceLockLevel.None.ToString(),
				ObscuredPrefs.DeviceLockLevel.Soft.ToString(),
				ObscuredPrefs.DeviceLockLevel.Strict.ToString()
			}, 3, new GUILayoutOption[0]);
			ObscuredPrefs.lockToDevice = (ObscuredPrefs.DeviceLockLevel)this.savesLock;
			ObscuredPrefs.readForeignSaves = GUILayout.Toggle(ObscuredPrefs.readForeignSaves, "readForeignSaves", new GUILayoutOption[0]);
			GUILayout.Label("PlayerPrefs: \n" + this.obscuredPrefsTest.gameData, new GUILayoutOption[0]);
			if (this.savesAlterationDetected)
			{
				GUILayout.Label("Saves were altered! }:>", new GUILayoutOption[0]);
			}
			if (this.foreignSavesDetected)
			{
				GUILayout.Label("Saves more likely from another device! }:>", new GUILayoutOption[0]);
			}
		}
		if (this.detectorsUsageExample != null)
		{
			GUILayout.Label("Speed hack detected: " + this.detectorsUsageExample.speedHackDetected, new GUILayoutOption[0]);
			GUILayout.Label("Injection detected: " + this.detectorsUsageExample.injectionDetected, new GUILayoutOption[0]);
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	// Token: 0x06001883 RID: 6275 RVA: 0x000CCA0E File Offset: 0x000CAE0E
	private void CenteredLabel(string caption)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.Label(caption, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}

	// Token: 0x04001BA0 RID: 7072
	private bool savesAlterationDetected;

	// Token: 0x04001BA1 RID: 7073
	private int savesLock;

	// Token: 0x04001BA2 RID: 7074
	private bool foreignSavesDetected;

	// Token: 0x04001BA3 RID: 7075
	public ObscuredVector3Test obscuredVector3Test;

	// Token: 0x04001BA4 RID: 7076
	public ObscuredFloatTest obscuredFloatTest;

	// Token: 0x04001BA5 RID: 7077
	public ObscuredIntTest obscuredIntTest;

	// Token: 0x04001BA6 RID: 7078
	public ObscuredStringTest obscuredStringTest;

	// Token: 0x04001BA7 RID: 7079
	public ObscuredPrefsTest obscuredPrefsTest;

	// Token: 0x04001BA8 RID: 7080
	private DetectorsUsageExample detectorsUsageExample;
}
