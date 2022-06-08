using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class WeaponManagerForMultiplayer : MonoBehaviour
{
	// Token: 0x0600113B RID: 4411 RVA: 0x000996C3 File Offset: 0x00097AC3
	private void Awake()
	{
		this.mAudioSource = base.GetComponent<AudioSource>();
		this.weaponDic = GrowthManagerKit.GetWeaponNameDic();
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000996DC File Offset: 0x00097ADC
	private void Update()
	{
		if ((double)Time.timeScale < 0.01)
		{
			return;
		}
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x000996F4 File Offset: 0x00097AF4
	public void SwitchWeaponOnline(int tmpIndex)
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.SetActive(false);
					if (transform.gameObject.name == "DualPistol(Clone)")
					{
						this.DualPistol_L.SetActive(false);
					}
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		foreach (KeyValuePair<int, string> keyValuePair in this.weaponDic)
		{
			if (keyValuePair.Key == tmpIndex)
			{
				IEnumerator enumerator3 = base.transform.GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						object obj2 = enumerator3.Current;
						Transform transform2 = (Transform)obj2;
						if (transform2.gameObject.name == keyValuePair.Value + "(Clone)")
						{
							transform2.gameObject.SetActive(true);
							this.mAudioSource.clip = this.takeInAudio;
							this.mAudioSource.Play();
							if (keyValuePair.Value == "DualPistol")
							{
								this.DualPistol_L.SetActive(true);
							}
						}
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator3 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
				break;
			}
		}
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x000998B4 File Offset: 0x00097CB4
	private void SetRemoteWeaponPlused(GGNetworkWeaponPropertiesList mNetworkWeaponProperties)
	{
		for (int i = 0; i < mNetworkWeaponProperties.weaponPropertiesList.Count; i++)
		{
			foreach (KeyValuePair<int, string> keyValuePair in this.weaponDic)
			{
				if (keyValuePair.Key == mNetworkWeaponProperties.weaponPropertiesList[i].weaponType)
				{
					GameObject gameObject = Resources.Load("Prefabs/Weapons_Remote/" + keyValuePair.Value) as GameObject;
					GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Weapons_Remote/" + keyValuePair.Value), Vector3.zero, Quaternion.identity) as GameObject;
					gameObject2.transform.parent = base.transform;
					WeaponSynForMultiplayer component = gameObject2.GetComponent<WeaponSynForMultiplayer>();
					component.SetWeaponTransform(gameObject.transform.localPosition, gameObject.transform.localEulerAngles, gameObject.transform.localScale);
					component.upgradeLv = mNetworkWeaponProperties.weaponPropertiesList[i].upgradeLv;
					component.weaponName = keyValuePair.Value;
					break;
				}
			}
		}
		this.IsWeaponInstantiateFinish = true;
	}

	// Token: 0x040013DB RID: 5083
	private float SwitchTime = 0.5f;

	// Token: 0x040013DC RID: 5084
	public int index;

	// Token: 0x040013DD RID: 5085
	public GameObject DualPistol_L;

	// Token: 0x040013DE RID: 5086
	public Material[] UpgradeMaterial;

	// Token: 0x040013DF RID: 5087
	public Material[] UpgradeMaterial_Close;

	// Token: 0x040013E0 RID: 5088
	private AudioSource mAudioSource;

	// Token: 0x040013E1 RID: 5089
	public AudioClip takeInAudio;

	// Token: 0x040013E2 RID: 5090
	private Dictionary<int, string> weaponDic = new Dictionary<int, string>();

	// Token: 0x040013E3 RID: 5091
	public bool IsWeaponInstantiateFinish;
}
