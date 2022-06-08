using System;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class GGNetworkPlayerSkinControl : MonoBehaviour
{
	// Token: 0x06001101 RID: 4353 RVA: 0x00096E34 File Offset: 0x00095234
	private void Start()
	{
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x00096E36 File Offset: 0x00095236
	private void Update()
	{
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x00096E38 File Offset: 0x00095238
	private void SetNetworkPlayerSkin(Material mSkinMaterial)
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (base.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue)
			{
				base.gameObject.GetComponent<Renderer>().material = mSkinMaterial;
			}
		}
		else
		{
			base.gameObject.GetComponent<Renderer>().material = mSkinMaterial;
		}
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x00096E9C File Offset: 0x0009529C
	private void SetNetworkPlayerHat(HatNetWorkPackObj mHatPack)
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (base.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue)
			{
				int modelIndex = mHatPack.detail.modelIndex;
				if (modelIndex > this.hatModelPrefab.Length)
				{
					return;
				}
				if (!this.isHaveHat)
				{
					this.HatModel = UnityEngine.Object.Instantiate<GameObject>(this.hatModelPrefab[modelIndex - 1], this.hatModelTransform[modelIndex - 1].position, this.hatModelTransform[modelIndex - 1].rotation);
					this.HatModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/S/C/N001/Head/NetworkPlayerHat");
					this.HatModel.GetComponent<Renderer>().material = mHatPack.mat;
					this.isHaveHat = true;
				}
			}
		}
		else
		{
			int modelIndex2 = mHatPack.detail.modelIndex;
			if (modelIndex2 > this.hatModelPrefab.Length)
			{
				return;
			}
			if (!this.isHaveHat)
			{
				this.HatModel = UnityEngine.Object.Instantiate<GameObject>(this.hatModelPrefab[modelIndex2 - 1], this.hatModelTransform[modelIndex2 - 1].position, this.hatModelTransform[modelIndex2 - 1].rotation);
				this.HatModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/S/C/N001/Head/NetworkPlayerHat");
				this.HatModel.GetComponent<Renderer>().material = mHatPack.mat;
				this.isHaveHat = true;
			}
		}
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00097014 File Offset: 0x00095414
	private void SetNetworkPlayerCape(CapeNetWorkPackObj mCapePack)
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (base.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue)
			{
				int modelIndex = mCapePack.detail.modelIndex;
				if (modelIndex > this.capeModelPrefab.Length)
				{
					return;
				}
				if (!this.isHaveCape)
				{
					this.CapeModel = UnityEngine.Object.Instantiate<GameObject>(this.capeModelPrefab[modelIndex - 1], this.capeModelTransform[modelIndex - 1].position, this.capeModelTransform[modelIndex - 1].rotation);
					this.CapeModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/S/C/NetworkPlayerCape");
					this.CapeModel.transform.Find("cape_" + modelIndex.ToString()).GetComponent<Renderer>().material = mCapePack.mat;
					this.isHaveCape = true;
				}
			}
		}
		else
		{
			int modelIndex2 = mCapePack.detail.modelIndex;
			if (modelIndex2 > this.capeModelPrefab.Length)
			{
				return;
			}
			if (!this.isHaveCape)
			{
				this.CapeModel = UnityEngine.Object.Instantiate<GameObject>(this.capeModelPrefab[modelIndex2 - 1], this.capeModelTransform[modelIndex2 - 1].position, this.capeModelTransform[modelIndex2 - 1].rotation);
				this.CapeModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/S/C/NetworkPlayerCape");
				this.CapeModel.transform.Find("cape_" + modelIndex2.ToString()).GetComponent<Renderer>().material = mCapePack.mat;
				this.isHaveCape = true;
			}
		}
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x000971D0 File Offset: 0x000955D0
	private void SetNetworkPlayerBoot(BootNetWorkPackObj mShoePack)
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (base.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue)
			{
				int modelIndex = mShoePack.detail.modelIndex;
				if (modelIndex > this.leftshoeModelPrefab.Length)
				{
					return;
				}
				if (!this.isHaveShoe)
				{
					this.LeftShoeModel = UnityEngine.Object.Instantiate<GameObject>(this.leftshoeModelPrefab[modelIndex - 1], this.leftshoeModelTransform[modelIndex - 1].position, this.leftshoeModelTransform[modelIndex - 1].rotation);
					this.LeftShoeModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/LL0/LL1/LL2/LL3/NetworkPlayerShoes_Left");
					this.LeftShoeModel.GetComponent<Renderer>().material = mShoePack.mat;
					this.RightShoeModel = UnityEngine.Object.Instantiate<GameObject>(this.rightshoeModelPrefab[modelIndex - 1], this.rightshoeModelTransform[modelIndex - 1].position, this.rightshoeModelTransform[modelIndex - 1].rotation);
					this.RightShoeModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/RR0/RR1/RR2/RR3/NetworkPlayerShoes_Right");
					this.RightShoeModel.GetComponent<Renderer>().material = mShoePack.mat;
					this.isHaveShoe = true;
				}
			}
		}
		else
		{
			int modelIndex2 = mShoePack.detail.modelIndex;
			if (modelIndex2 > this.leftshoeModelPrefab.Length)
			{
				return;
			}
			if (!this.isHaveShoe)
			{
				this.LeftShoeModel = UnityEngine.Object.Instantiate<GameObject>(this.leftshoeModelPrefab[modelIndex2 - 1], this.leftshoeModelTransform[modelIndex2 - 1].position, this.leftshoeModelTransform[modelIndex2 - 1].rotation);
				this.LeftShoeModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/LL0/LL1/LL2/LL3/NetworkPlayerShoes_Left");
				this.LeftShoeModel.GetComponent<Renderer>().material = mShoePack.mat;
				this.RightShoeModel = UnityEngine.Object.Instantiate<GameObject>(this.rightshoeModelPrefab[modelIndex2 - 1], this.rightshoeModelTransform[modelIndex2 - 1].position, this.rightshoeModelTransform[modelIndex2 - 1].rotation);
				this.RightShoeModel.transform.parent = base.transform.root.Find("Player_1_sinkmesh/H/RR0/RR1/RR2/RR3/NetworkPlayerShoes_Right");
				this.RightShoeModel.GetComponent<Renderer>().material = mShoePack.mat;
				this.isHaveShoe = true;
			}
		}
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x00097424 File Offset: 0x00095824
	private void MutationModeSetCapeAndHat()
	{
		if (this.HatModel != null && this.HatModel.activeSelf)
		{
			this.HatModel.SetActive(false);
		}
		if (this.CapeModel != null && this.CapeModel.activeSelf)
		{
			this.CapeModel.SetActive(false);
		}
		if (this.LeftShoeModel != null && this.LeftShoeModel.activeSelf)
		{
			this.LeftShoeModel.SetActive(false);
		}
		if (this.RightShoeModel != null && this.RightShoeModel.activeSelf)
		{
			this.RightShoeModel.SetActive(false);
		}
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x000974E5 File Offset: 0x000958E5
	private void MutationModeSetSkin(int zombieSkinIndex)
	{
		base.gameObject.GetComponent<Renderer>().material.mainTexture = this.zombieSkinTexture[zombieSkinIndex];
	}

	// Token: 0x0400135B RID: 4955
	public GameObject[] hatModelPrefab;

	// Token: 0x0400135C RID: 4956
	public Transform[] hatModelTransform;

	// Token: 0x0400135D RID: 4957
	public GameObject[] capeModelPrefab;

	// Token: 0x0400135E RID: 4958
	public Transform[] capeModelTransform;

	// Token: 0x0400135F RID: 4959
	public GameObject[] leftshoeModelPrefab;

	// Token: 0x04001360 RID: 4960
	public GameObject[] rightshoeModelPrefab;

	// Token: 0x04001361 RID: 4961
	public Transform[] leftshoeModelTransform;

	// Token: 0x04001362 RID: 4962
	public Transform[] rightshoeModelTransform;

	// Token: 0x04001363 RID: 4963
	private bool isHaveCape;

	// Token: 0x04001364 RID: 4964
	private bool isHaveHat;

	// Token: 0x04001365 RID: 4965
	private bool isHaveShoe;

	// Token: 0x04001366 RID: 4966
	private GameObject CapeModel;

	// Token: 0x04001367 RID: 4967
	private GameObject HatModel;

	// Token: 0x04001368 RID: 4968
	private GameObject LeftShoeModel;

	// Token: 0x04001369 RID: 4969
	private GameObject RightShoeModel;

	// Token: 0x0400136A RID: 4970
	public Texture[] zombieSkinTexture;
}
