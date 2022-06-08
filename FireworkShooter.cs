using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class FireworkShooter : MonoBehaviour
{
	// Token: 0x06000E4D RID: 3661 RVA: 0x000771EB File Offset: 0x000755EB
	private void Start()
	{
		UIPlayDirector.OnFirework += this.OnFirework;
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000771FE File Offset: 0x000755FE
	private void Update()
	{
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x00077200 File Offset: 0x00075600
	private void OnDisable()
	{
		UIPlayDirector.OnFirework -= this.OnFirework;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00077214 File Offset: 0x00075614
	public void OnFirework(int fireWorkIndex)
	{
		if (base.gameObject.tag == "Player")
		{
			GGMessage ggmessage = new GGMessage();
			ggmessage.messageType = GGMessageType.MessageNotifyHolidayFireworks;
			ggmessage.messageContent = new GGMessageContent();
			ggmessage.messageContent.ID = fireWorkIndex;
			ggmessage.messageContent.ID2 = base.gameObject.GetPhotonView().viewID;
			GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x00077287 File Offset: 0x00075687
	public void FireworkShoot(int Index)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.FireworkPrefab[Index - 1], this.FireworkShootTransform.position, this.FireworkShootTransform.rotation);
	}

	// Token: 0x04000F54 RID: 3924
	public Transform FireworkShootTransform;

	// Token: 0x04000F55 RID: 3925
	public GameObject[] FireworkPrefab;
}
