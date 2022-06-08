using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class UINewSotreWeaponUpgrade : MonoBehaviour
{
	// Token: 0x060016B2 RID: 5810 RVA: 0x000C164C File Offset: 0x000BFA4C
	private void Start()
	{
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000C1650 File Offset: 0x000BFA50
	private void Update()
	{
		if (UINewStoreWeaponPrefabInstiate.mInstance.isUpgradeProcess_COIN)
		{
			if (UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount == 0f)
			{
				this.mFireAnimation.T.enabled = true;
				this.mFireAnimation.enabled = true;
				this.mFireAnimation.open = true;
			}
			UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount += 0.5f * Time.deltaTime;
			if (UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount >= 1f)
			{
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount = 0f;
				UINewStoreWeaponPrefabInstiate.mInstance.isUpgradeProcess_COIN = false;
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.gameObject.SetActive(false);
				this.StopFireAnimation();
				base.StartCoroutine(this.WeaponUpgradeWaitResult(0));
			}
		}
		if (UINewStoreWeaponPrefabInstiate.mInstance.isUpgradeProcess_GEM)
		{
			if (UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount == 0f)
			{
				this.mFireAnimation.T.enabled = true;
				this.mFireAnimation.enabled = true;
				this.mFireAnimation.open = true;
			}
			UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount += 0.5f * Time.deltaTime;
			if (UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount >= 1f)
			{
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.fillAmount = 0f;
				UINewStoreWeaponPrefabInstiate.mInstance.isUpgradeProcess_GEM = false;
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultProcess.gameObject.SetActive(false);
				this.StopFireAnimation();
				base.StartCoroutine(this.WeaponUpgradeWaitResult(1));
			}
		}
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000C1804 File Offset: 0x000BFC04
	private IEnumerator WeaponUpgradeWaitResult(int type)
	{
		if (type == 0)
		{
			bool upgradeResult = UINewStoreWeaponPrefabInstiate.mInstance.JudgeUpgradeUseCoinResult();
			if (upgradeResult)
			{
				this.mSuccessAnimation.T.enabled = true;
				this.mSuccessAnimation.enabled = true;
				this.mSuccessAnimation.open = true;
			}
			yield return new WaitForSeconds(1.5f);
			if (upgradeResult)
			{
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultSuccessful.gameObject.SetActive(false);
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultMask.SetActive(false);
				UINewStoreWeaponPrefabInstiate.mInstance.WeaponPlusOnePropertyCancel();
				this.StopSuccessAnimation();
			}
			else
			{
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultMask.SetActive(false);
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultFailed.gameObject.SetActive(false);
			}
		}
		else
		{
			bool upgradeResult2 = UINewStoreWeaponPrefabInstiate.mInstance.JudgeUpgradeUseGemResult();
			if (upgradeResult2)
			{
				this.mSuccessAnimation.T.enabled = true;
				this.mSuccessAnimation.enabled = true;
				this.mSuccessAnimation.open = true;
			}
			yield return new WaitForSeconds(1.5f);
			if (upgradeResult2)
			{
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultSuccessful.gameObject.SetActive(false);
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultMask.SetActive(false);
				UINewStoreWeaponPrefabInstiate.mInstance.WeaponPlusOnePropertyCancel();
				this.StopSuccessAnimation();
			}
			else
			{
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultMask.SetActive(false);
				UINewStoreWeaponPrefabInstiate.mInstance.plusResultFailed.gameObject.SetActive(false);
			}
		}
		yield break;
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x000C1826 File Offset: 0x000BFC26
	private void StopSuccessAnimation()
	{
		this.mSuccessAnimation.T.mainTexture = null;
		this.mSuccessAnimation.open = false;
		this.mSuccessAnimation.enabled = false;
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x000C1851 File Offset: 0x000BFC51
	private void StopFireAnimation()
	{
		this.mFireAnimation.T.mainTexture = null;
		this.mFireAnimation.open = false;
		this.mFireAnimation.enabled = false;
	}

	// Token: 0x04001979 RID: 6521
	public GGSequenceFrameAnimation mSuccessAnimation;

	// Token: 0x0400197A RID: 6522
	public GGSequenceFrameAnimation mFireAnimation;
}
