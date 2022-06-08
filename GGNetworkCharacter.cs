using System;
using System.Collections;
using Photon;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
public class GGNetworkCharacter : Photon.MonoBehaviour
{
	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0011229C File Offset: 0x0011069C
	// (set) Token: 0x060023E5 RID: 9189 RVA: 0x001122A4 File Offset: 0x001106A4
	public int mBlood
	{
		get
		{
			return this._mBlood;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this._mBlood, ref this._mEncryptBlood, value);
		}
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x001122C0 File Offset: 0x001106C0
	private void Awake()
	{
		if (Application.loadedLevelName == "SingleMode_1" || Application.loadedLevelName == "SingleMode_2" || Application.loadedLevelName == "SingleMode_3" || Application.loadedLevelName == "SingleMode_4" || Application.loadedLevelName == "UIHelp")
		{
			this.isSingleMode = true;
		}
		if (!this.isSingleMode)
		{
			this.maincameraTransform = base.transform.Find("LookObject");
		}
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x00112359 File Offset: 0x00110759
	public void InitEncryptData()
	{
		this._mEncryptBlood = GameProtecter.mInstance.EncryptInt1(this._mBlood);
	}

	// Token: 0x060023E8 RID: 9192 RVA: 0x00112374 File Offset: 0x00110774
	public void Start()
	{
		this.InitEncryptData();
		if (!this.isSingleMode)
		{
			this.InitPlayerProperties();
			if (base.photonView.isMine)
			{
				this.myArmorInfo = GrowthManagerKit.GetCurSettedArmorInfo();
				if (this.myArmorInfo.GetAutoSupplyTimeRest() > 0f)
				{
					this.myArmorInfo.mIsInAutoSupplyStatus = true;
				}
			}
			this.mCharacterCurFireState = this.mCharacterFireState;
			this.mCharacterPreFireState = this.mCharacterCurFireState;
			this.mCharacterFireStateQueue.Enqueue(this.mCharacterCurFireState);
		}
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x00112404 File Offset: 0x00110804
	private void Update()
	{
		if (!this.isSingleMode)
		{
			if (base.photonView.isMine)
			{
				this.mCharacterCurFireState = this.mCharacterFireState;
				if (this.mCharacterPreFireState != this.mCharacterCurFireState)
				{
					this.mCharacterFireStateQueue.Enqueue(this.mCharacterCurFireState);
					this.mCharacterPreFireState = this.mCharacterCurFireState;
				}
			}
		}
		this.myTeam = this.mPlayerProperties.team;
		this.myName = this.mPlayerProperties.name;
		if (base.photonView != null)
		{
			if (!base.photonView.isMine)
			{
				if (this.mCharacterWalkState == GGCharacterWalkState.Dead)
				{
					if (GGNetworkKit.mInstance != null && !GGNetworkKit.mInstance.mDeadMirrorPlayerList.Contains(this.mPlayerProperties.name))
					{
						GGNetworkKit.mInstance.mDeadMirrorPlayerList.Add(this.mPlayerProperties.name);
					}
				}
				else if (GGNetworkKit.mInstance.mDeadMirrorPlayerList.Contains(this.mPlayerProperties.name))
				{
					GGNetworkKit.mInstance.mDeadMirrorPlayerList.Remove(this.mPlayerProperties.name);
				}
			}
			else if (this.mCharacterWalkState == GGCharacterWalkState.Dead && GGNetworkKit.mInstance.mDamageListFromOtherPlayers.Count > 0)
			{
				GGNetworkKit.mInstance.mDamageListFromOtherPlayers.Clear();
			}
		}
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x0011257C File Offset: 0x0011097C
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.isSingleMode)
		{
			if (stream.isWriting)
			{
				stream.SendNext(base.transform.position);
				stream.SendNext(base.transform.rotation);
				stream.SendNext((short)this.mBlood);
				stream.SendNext(this.mCharacterWalkState);
				stream.SendNext((this.mCharacterFireStateQueue.Count <= 0) ? this.mCharacterFireState : ((GGCharacterFireState)this.mCharacterFireStateQueue.Dequeue()));
				stream.SendNext(this.maincameraTransform.localEulerAngles.x);
				stream.SendNext((byte)this.mGearType);
				stream.SendNext((short)this.mWeaponType);
				stream.SendNext(GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkPlayerProperties>(this.mPlayerProperties));
				stream.SendNext((byte)this.mZombieLv);
				stream.SendNext((byte)this.zombieSkinIndex);
			}
			else
			{
				this.mPosition = (Vector3)stream.ReceiveNext();
				this.mRotation = (Quaternion)stream.ReceiveNext();
				this.mBlood = (int)((short)stream.ReceiveNext());
				this.mCharacterWalkState = (GGCharacterWalkState)stream.ReceiveNext();
				this.mCharacterFireState = (GGCharacterFireState)stream.ReceiveNext();
				this.cameraRotationX = (float)stream.ReceiveNext();
				this.mGearType = (int)((byte)stream.ReceiveNext());
				this.mWeaponType = (int)((short)stream.ReceiveNext());
				this.mPlayerProperties = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkPlayerProperties>((byte[])stream.ReceiveNext());
				this.mZombieLv = (int)((byte)stream.ReceiveNext());
				this.zombieSkinIndex = (int)((byte)stream.ReceiveNext());
			}
		}
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x0011276C File Offset: 0x00110B6C
	public void InitPlayerProperties()
	{
		if (base.photonView.isMine)
		{
			this.mPlayerProperties = new GGNetworkPlayerProperties();
			if (PhotonNetwork.player == null)
			{
				Debug.Log("zzzzzzzzzzz");
			}
			else
			{
				Debug.Log("Have Player");
				Debug.Log(PhotonNetwork.player.ID);
			}
			this.mPlayerProperties.rank = (short)PhotonNetwork.player.customProperties["rank"];
			this.mPlayerProperties.name = (string)PhotonNetwork.player.customProperties["name"];
			this.mPlayerProperties.id = PhotonNetwork.player.ID;
			this.mPlayerProperties.ping = (short)GGNetworkKit.mInstance.GetPing();
			this.mPlayerProperties.team = GGTeamType.Nil;
			this.mPlayerProperties.isDataValid = true;
			GGNetworkKit.mInstance.DanamicSwitchTeam();
		}
		else
		{
			this.mPlayerProperties = new GGNetworkPlayerProperties();
			this.mPlayerProperties.rank = 0;
			this.mPlayerProperties.name = string.Empty;
			this.mPlayerProperties.id = 0;
			this.mPlayerProperties.ping = 0;
			this.mPlayerProperties.team = GGTeamType.Nil;
			this.mPlayerProperties.isDataValid = false;
		}
	}

	// Token: 0x0400245C RID: 9308
	public Vector3 mPosition = Vector3.zero;

	// Token: 0x0400245D RID: 9309
	public Quaternion mRotation = Quaternion.identity;

	// Token: 0x0400245E RID: 9310
	public float cameraRotationX;

	// Token: 0x0400245F RID: 9311
	public string _mEncryptBlood = string.Empty;

	// Token: 0x04002460 RID: 9312
	public int _mBlood = 100;

	// Token: 0x04002461 RID: 9313
	public GGCharacterWalkState mCharacterWalkState;

	// Token: 0x04002462 RID: 9314
	public GGCharacterFireState mCharacterFireState;

	// Token: 0x04002463 RID: 9315
	public int mGearType;

	// Token: 0x04002464 RID: 9316
	public int mWeaponType;

	// Token: 0x04002465 RID: 9317
	public GGNetworkPlayerProperties mPlayerProperties = new GGNetworkPlayerProperties();

	// Token: 0x04002466 RID: 9318
	public int mZombieLv;

	// Token: 0x04002467 RID: 9319
	public int zombieSkinIndex;

	// Token: 0x04002468 RID: 9320
	public bool isNeedSyn = true;

	// Token: 0x04002469 RID: 9321
	public GGCharacterFireState mCharacterCurFireState;

	// Token: 0x0400246A RID: 9322
	public GGCharacterFireState mCharacterPreFireState;

	// Token: 0x0400246B RID: 9323
	public Queue mCharacterFireStateQueue = new Queue();

	// Token: 0x0400246C RID: 9324
	public GGTeamType myTeam;

	// Token: 0x0400246D RID: 9325
	public string myName;

	// Token: 0x0400246E RID: 9326
	public GArmorItemInfo myArmorInfo = new GArmorItemInfo();

	// Token: 0x0400246F RID: 9327
	public Transform maincameraTransform;

	// Token: 0x04002470 RID: 9328
	public bool isSingleMode;
}
