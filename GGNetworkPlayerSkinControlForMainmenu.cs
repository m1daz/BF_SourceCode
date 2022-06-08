using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class GGNetworkPlayerSkinControlForMainmenu : MonoBehaviour
{
	// Token: 0x0600110A RID: 4362 RVA: 0x00097524 File Offset: 0x00095924
	private void Start()
	{
		int num = this.hatModelPrefab.Length;
		int num2 = this.capeModelPrefab.Length;
		int num3 = this.mTexture.Length;
		int num4 = this.leftshoeModelPrefab.Length;
		UnityEngine.Object.Instantiate<GameObject>(this.DecorationManage, new Vector3(0f, 0f, 0f), Quaternion.identity);
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0009757A File Offset: 0x0009597A
	private void Update()
	{
		if (!this.isEquiped)
		{
			this.InitPlayerEquipment();
			this.isEquiped = true;
		}
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x00097594 File Offset: 0x00095994
	private IEnumerator DelayInitPlayerEquipment()
	{
		yield return new WaitForSeconds(0.1f);
		this.InitPlayerEquipment();
		this.isEquiped = true;
		yield break;
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x000975B0 File Offset: 0x000959B0
	private void InitPlayerEquipment()
	{
		if (HatManager.mInstance.myCharacterHatEntity != null)
		{
			this.hatIndex = HatManager.mInstance.myCharacterHatEntity.detail.modelIndex;
			this.SetNetworkPlayerHat(this.hatIndex);
		}
		if (CapeManager.mInstance.myCharacterCapeEntity != null)
		{
			this.capeIndex = CapeManager.mInstance.myCharacterCapeEntity.detail.modelIndex;
			this.SetNetworkPlayerCape(this.capeIndex);
		}
		if (BootManager.mInstance.myCharacterBootEntity != null)
		{
			this.shoeIndex = BootManager.mInstance.myCharacterBootEntity.detail.modelIndex;
			this.SetNetworkPlayerShoe(this.shoeIndex);
		}
		if (SkinManager.mInstance.myCharacterSkinEntity != null)
		{
			this.skinTex = SkinManager.mInstance.myCharacterSkinEntity.tex;
			this.SetNetworkPlayerSkin(this.skinTex);
		}
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0009768C File Offset: 0x00095A8C
	private void SetNetworkPlayerSkin(Texture skinTex)
	{
		base.transform.GetComponent<Renderer>().material.mainTexture = skinTex;
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x000976A4 File Offset: 0x00095AA4
	private void SetNetworkPlayerHat(int hatIndex)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hatModelPrefab[hatIndex - 1], this.hatModelTransform[hatIndex - 1].position, this.hatModelTransform[hatIndex - 1].rotation);
		gameObject.transform.parent = base.transform.root.Find("H/S/C/N001/Head/NetworkPlayerHat");
		gameObject.transform.localScale *= 2f;
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0009771C File Offset: 0x00095B1C
	private void SetNetworkPlayerCape(int capeIndex)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.capeModelPrefab[capeIndex - 1], this.capeModelTransform[capeIndex - 1].position, this.capeModelTransform[capeIndex - 1].rotation);
		gameObject.transform.parent = base.transform.root.Find("H/S/C/NetworkPlayerCape");
		gameObject.transform.localScale *= 2f;
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00097794 File Offset: 0x00095B94
	private void SetNetworkPlayerShoe(int shoeIndex)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.leftshoeModelPrefab[shoeIndex - 1], this.leftshoeModelTransform[shoeIndex - 1].position, this.leftshoeModelTransform[shoeIndex - 1].rotation);
		gameObject.transform.parent = base.transform.root.Find("H/LL0/LL1/LL2/LL3/NetworkPlayerShoes_Left");
		gameObject.transform.localScale *= 2f;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.rightshoeModelPrefab[shoeIndex - 1], this.rightshoeModelTransform[shoeIndex - 1].position, this.rightshoeModelTransform[shoeIndex - 1].rotation);
		gameObject2.transform.parent = base.transform.root.Find("H/RR0/RR1/RR2/RR3/NetworkPlayerShoes_Right");
		gameObject2.transform.localScale *= 2f;
	}

	// Token: 0x0400136B RID: 4971
	public GameObject DecorationManage;

	// Token: 0x0400136C RID: 4972
	public GameObject[] hatModelPrefab;

	// Token: 0x0400136D RID: 4973
	public Transform[] hatModelTransform;

	// Token: 0x0400136E RID: 4974
	public GameObject[] capeModelPrefab;

	// Token: 0x0400136F RID: 4975
	public Transform[] capeModelTransform;

	// Token: 0x04001370 RID: 4976
	public GameObject[] leftshoeModelPrefab;

	// Token: 0x04001371 RID: 4977
	public GameObject[] rightshoeModelPrefab;

	// Token: 0x04001372 RID: 4978
	public Transform[] leftshoeModelTransform;

	// Token: 0x04001373 RID: 4979
	public Transform[] rightshoeModelTransform;

	// Token: 0x04001374 RID: 4980
	private int hatIndex = 1;

	// Token: 0x04001375 RID: 4981
	private int capeIndex = 1;

	// Token: 0x04001376 RID: 4982
	private int shoeIndex = 1;

	// Token: 0x04001377 RID: 4983
	private Texture skinTex;

	// Token: 0x04001378 RID: 4984
	public Texture[] mTexture;

	// Token: 0x04001379 RID: 4985
	private bool isEquiped;
}
