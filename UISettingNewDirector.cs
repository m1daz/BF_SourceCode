using System;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class UISettingNewDirector : MonoBehaviour
{
	// Token: 0x06001675 RID: 5749 RVA: 0x000C043F File Offset: 0x000BE83F
	private void Awake()
	{
		if (UISettingNewDirector.mInstance == null)
		{
			UISettingNewDirector.mInstance = this;
		}
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x000C0457 File Offset: 0x000BE857
	private void OnDestroy()
	{
		UIUserDataController.SetSensitivity((int)((double)(this.sensitivitySlider.value * 100f) + 1E-06));
		if (UISettingNewDirector.mInstance != null)
		{
			UISettingNewDirector.mInstance = null;
		}
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x000C0491 File Offset: 0x000BE891
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000C0499 File Offset: 0x000BE899
	private void Update()
	{
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000C049C File Offset: 0x000BE89C
	private void Init()
	{
		for (int i = 0; i < 7; i++)
		{
			this.allThrowWeapon[i] = UIUserDataController.allThrowWeapon[i];
		}
		if (UIUserDataController.GetSoundFX() == 0)
		{
			this.soundFxToggle.value = false;
		}
		else
		{
			this.soundFxToggle.value = true;
		}
		if (UIUserDataController.GetMusic() == 0)
		{
			this.musicToggle.value = false;
		}
		else
		{
			this.musicToggle.value = true;
		}
		if (UIUserDataController.GetBulletHole() == 0)
		{
			this.bulletHoleToggle.value = false;
		}
		else
		{
			this.bulletHoleToggle.value = true;
		}
		if (UIUserDataController.GetSniperMode() == 0)
		{
			this.sniperModeLabel.text = "MODE 2";
		}
		else
		{
			this.sniperModeLabel.text = "MODE 1";
		}
		this.sensitivitySlider.value = (float)UIUserDataController.GetSensitivity() / 100f;
		this.sensitivityValueLabel.text = UIUserDataController.GetSensitivity().ToString();
		this.RefreshThrowWeaponData();
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000C05A9 File Offset: 0x000BE9A9
	public void SoundFxToggleValueChanged()
	{
		if (this.soundFxToggle.value)
		{
			UIUserDataController.SetSoundFX(1);
		}
		else
		{
			UIUserDataController.SetSoundFX(0);
		}
		if (GGSettingControl.mInstance != null)
		{
			GGSettingControl.mInstance.SetSound();
		}
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x000C05E6 File Offset: 0x000BE9E6
	public void MusicToggleValueChanged()
	{
		if (this.musicToggle.value)
		{
			UIUserDataController.SetMusic(1);
		}
		else
		{
			UIUserDataController.SetMusic(0);
		}
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000C0609 File Offset: 0x000BEA09
	public void BulletHoleToggleValueChanged()
	{
		if (this.bulletHoleToggle.value)
		{
			UIUserDataController.SetBulletHole(1);
		}
		else
		{
			UIUserDataController.SetBulletHole(0);
		}
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x000C062C File Offset: 0x000BEA2C
	public void SniperModeBtnPressed()
	{
		if (UIUserDataController.GetSniperMode() == 1)
		{
			UIUserDataController.SetSniperMode(0);
			this.sniperModeLabel.text = "MODE 2";
		}
		else
		{
			UIUserDataController.SetSniperMode(1);
			this.sniperModeLabel.text = "MODE 1";
		}
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000C066C File Offset: 0x000BEA6C
	public void BackBtnPressed()
	{
		UIUserDataController.SetSensitivity((int)((double)(this.sensitivitySlider.value * 100f) + 1E-06));
		if (GGSettingControl.mInstance != null)
		{
			GGSettingControl.mInstance.SetSensitivity();
		}
		if (UIPlayDirector.mInstance != null)
		{
			UIPlayDirector.mInstance.RefreshThrowWeapon();
		}
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x000C06D0 File Offset: 0x000BEAD0
	public void SensitivityValueChanged()
	{
		UIUserDataController.SetSensitivity((int)((double)(this.sensitivitySlider.value * 100f) + 1E-06));
		this.sensitivityValueLabel.text = UIUserDataController.GetSensitivity().ToString();
		GGSettingControl.mInstance.SetSensitivity();
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000C0727 File Offset: 0x000BEB27
	public void RefreshThrowWeaponData()
	{
		this.ThrowWeaponCurIndex = UIUserDataController.GetQuickBarItemIndex();
		this.ThrowWeaponPreIndex = this.ThrowWeaponCurIndex;
		this.ThrowWeaponSprite.spriteName = this.allThrowWeapon[this.ThrowWeaponCurIndex];
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x000C0758 File Offset: 0x000BEB58
	public void ThrowWeaponLastBtnPressed()
	{
		this.ThrowWeaponCurIndex--;
		if (this.ThrowWeaponCurIndex < 0)
		{
			this.ThrowWeaponCurIndex = 6;
		}
		this.ThrowWeaponPreIndex = this.ThrowWeaponCurIndex;
		this.ThrowWeaponSprite.spriteName = this.allThrowWeapon[this.ThrowWeaponCurIndex];
		UIUserDataController.SetQuickBarItemIndex(this.ThrowWeaponCurIndex);
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x000C07B8 File Offset: 0x000BEBB8
	public void ThrowWeaponNextBtnPressed()
	{
		this.ThrowWeaponCurIndex++;
		if (this.ThrowWeaponCurIndex > 6)
		{
			this.ThrowWeaponCurIndex = 0;
		}
		this.ThrowWeaponPreIndex = this.ThrowWeaponCurIndex;
		this.ThrowWeaponSprite.spriteName = this.allThrowWeapon[this.ThrowWeaponCurIndex];
		UIUserDataController.SetQuickBarItemIndex(this.ThrowWeaponCurIndex);
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000C0818 File Offset: 0x000BEC18
	public void RestoreBtnPressed()
	{
		this.soundFxToggle.value = true;
		this.musicToggle.value = true;
		this.bulletHoleToggle.value = true;
		UIUserDataController.SetMusic(1);
		UIUserDataController.SetSoundFX(1);
		UIUserDataController.SetBulletHole(1);
		UIUserDataController.SetSniperMode(1);
		this.sniperModeLabel.text = "MODE 1";
		UIUserDataController.SetSensitivity(40);
		this.sensitivitySlider.value = 0.4f;
		this.sensitivityValueLabel.text = UIUserDataController.GetSensitivity().ToString();
		this.ThrowWeaponCurIndex = 0;
		this.ThrowWeaponPreIndex = this.ThrowWeaponCurIndex;
		this.ThrowWeaponSprite.spriteName = this.allThrowWeapon[this.ThrowWeaponCurIndex];
		UIUserDataController.SetQuickBarItemIndex(this.ThrowWeaponCurIndex);
	}

	// Token: 0x0400194D RID: 6477
	public static UISettingNewDirector mInstance;

	// Token: 0x0400194E RID: 6478
	public UIToggle soundFxToggle;

	// Token: 0x0400194F RID: 6479
	public UIToggle musicToggle;

	// Token: 0x04001950 RID: 6480
	public UIToggle bulletHoleToggle;

	// Token: 0x04001951 RID: 6481
	public UILabel sensitivityValueLabel;

	// Token: 0x04001952 RID: 6482
	public UISlider sensitivitySlider;

	// Token: 0x04001953 RID: 6483
	public UILabel sniperModeLabel;

	// Token: 0x04001954 RID: 6484
	public UIToggle RecordToggle;

	// Token: 0x04001955 RID: 6485
	public UISprite ThrowWeaponSprite;

	// Token: 0x04001956 RID: 6486
	private string[] allThrowWeapon = new string[]
	{
		"M67",
		"MilkBomb",
		"GingerbreadBomb",
		"SmokeBomb",
		"FlashBomb",
		"SnowmanBomb",
		"Null"
	};

	// Token: 0x04001957 RID: 6487
	private int ThrowWeaponPreIndex;

	// Token: 0x04001958 RID: 6488
	private int ThrowWeaponCurIndex;
}
