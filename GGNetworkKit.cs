using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using RioLog;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class GGNetworkKit : MonoBehaviour
{
	// Token: 0x1400005D RID: 93
	// (add) Token: 0x0600249B RID: 9371 RVA: 0x001142F8 File Offset: 0x001126F8
	// (remove) Token: 0x0600249C RID: 9372 RVA: 0x00114330 File Offset: 0x00112730
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event DamageEventHandler ReceiveDamage;

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x0600249D RID: 9373 RVA: 0x00114368 File Offset: 0x00112768
	// (remove) Token: 0x0600249E RID: 9374 RVA: 0x001143A0 File Offset: 0x001127A0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event DamageEventHandler AIReceiveDamage;

	// Token: 0x1400005F RID: 95
	// (add) Token: 0x0600249F RID: 9375 RVA: 0x001143D8 File Offset: 0x001127D8
	// (remove) Token: 0x060024A0 RID: 9376 RVA: 0x00114410 File Offset: 0x00112810
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event ModeEventHandler ModeResult;

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x060024A1 RID: 9377 RVA: 0x00114448 File Offset: 0x00112848
	// (remove) Token: 0x060024A2 RID: 9378 RVA: 0x00114480 File Offset: 0x00112880
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event PropsEventHandler PropsReward;

	// Token: 0x14000061 RID: 97
	// (add) Token: 0x060024A3 RID: 9379 RVA: 0x001144B8 File Offset: 0x001128B8
	// (remove) Token: 0x060024A4 RID: 9380 RVA: 0x001144F0 File Offset: 0x001128F0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event MessageEventHandler MessageOk;

	// Token: 0x14000062 RID: 98
	// (add) Token: 0x060024A5 RID: 9381 RVA: 0x00114528 File Offset: 0x00112928
	// (remove) Token: 0x060024A6 RID: 9382 RVA: 0x00114560 File Offset: 0x00112960
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event ErrorEventHandler ErrorEvent;

	// Token: 0x14000063 RID: 99
	// (add) Token: 0x060024A7 RID: 9383 RVA: 0x00114598 File Offset: 0x00112998
	// (remove) Token: 0x060024A8 RID: 9384 RVA: 0x001145D0 File Offset: 0x001129D0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event UserEventHandler UserEvent;

	// Token: 0x060024A9 RID: 9385 RVA: 0x00114606 File Offset: 0x00112A06
	private void Awake()
	{
		GGNetworkKit.mInstance = this;
		PhotonNetwork.PhotonServerSettings.AppID = "c6c96d02-8373-427d-ac4f-0ba21e86ae1b";
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x00114620 File Offset: 0x00112A20
	private void Start()
	{
		if (!PhotonNetwork.connected)
		{
			if (UIUserDataController.GetDefaultServer() == GGServerRegion.DEFAULT)
			{
				this.SwitchServer(GGServerRegion.US);
				this.ConnectToNetwork();
			}
			else
			{
				if (UIUserDataController.GetDefaultServer() != this.GetCurrentServerRegion())
				{
					this.SwitchServer(UIUserDataController.GetDefaultServer());
				}
				this.ConnectToNetwork();
			}
		}
		this.Init();
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			RioQerdoDebug.Log("home.........");
			UnityAnalyticsIntegration.mInstance.NewUserLoadedHome();
			UIUserDataController.SetFirstPlay(1);
		}
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x001146A3 File Offset: 0x00112AA3
	private void Init()
	{
		this.mLastTime = (double)Time.time;
		this.mLastConnectionState = ConnectionState.InitializingApplication;
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x001146B8 File Offset: 0x00112AB8
	public GGServerRegion GetClosestRegion()
	{
		return GGNetworkAdapter.mInstance.GetClosestRegion();
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x001146C4 File Offset: 0x00112AC4
	public GGServerRegion GetCurrentServerRegion()
	{
		return GGNetworkAdapter.mInstance.GetCurrentServerRegion();
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x001146D0 File Offset: 0x00112AD0
	public bool IsMasterClient()
	{
		return GGNetworkAdapter.mInstance.isMasterClient;
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x001146DC File Offset: 0x00112ADC
	public bool ViewIsMasterClient(PhotonView view)
	{
		return GGNetworkAdapter.mInstance.ViewIsMasterClient(view);
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x001146EC File Offset: 0x00112AEC
	public void CreateRoom(string roomName, int maxPlayers, string mapName, string sceneName, GGModeType mode, bool encryption, string password, GGPlayModeType playMode, int huntingDifficulty)
	{
		GGNetworkAdapter.mInstance.CreateRoom(roomName, maxPlayers, mapName, sceneName, mode, encryption, password, playMode, huntingDifficulty);
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x00114714 File Offset: 0x00112B14
	public void JoinFriendRoom(string roomName, GGServerRegion region, string modeName)
	{
		if (modeName == "Hunting" && GrowthManagerKit.GetHuntingTickets() < 1)
		{
			if (UIDialogDirector.mInstance != null)
			{
				UIDialogDirector.mInstance.DisplayNeedHuntingTicketDialog();
				if (UITipController.mInstance != null)
				{
					UITipController.mInstance.HideCurTip();
				}
			}
		}
		else
		{
			GGNetworkAdapter.mInstance.JoinFriendRoom(roomName, region);
		}
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x00114781 File Offset: 0x00112B81
	public void JoinRoom(string roomName)
	{
		GGNetworkAdapter.mInstance.JoinRoom(roomName);
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x0011478E File Offset: 0x00112B8E
	public void JoinRandomRoom(GGPlayModeType playMode, GGModeType mode, bool canJoinHuntingMode)
	{
		GGNetworkAdapter.mInstance.JoinRandomRoom(playMode, mode, canJoinHuntingMode);
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x0011479D File Offset: 0x00112B9D
	public GGModeType GetGameMode()
	{
		return GGNetworkAdapter.mInstance.GetGameMode();
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x001147A9 File Offset: 0x00112BA9
	public GGPlayModeType GetPlayMode()
	{
		return GGNetworkAdapter.mInstance.GetPlayMode();
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x001147B5 File Offset: 0x00112BB5
	public string GetMapName()
	{
		return GGNetworkAdapter.mInstance.GetMapName();
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x001147C1 File Offset: 0x00112BC1
	public int GetMaxPlayers()
	{
		return GGNetworkAdapter.mInstance.GetMaxPlayers();
	}

	// Token: 0x060024B8 RID: 9400 RVA: 0x001147CD File Offset: 0x00112BCD
	public string GetRoomName()
	{
		return GGNetworkAdapter.mInstance.GetRoomName();
	}

	// Token: 0x060024B9 RID: 9401 RVA: 0x001147D9 File Offset: 0x00112BD9
	public bool GetRoomEncryption()
	{
		return GGNetworkAdapter.mInstance.GetRoomEncryption();
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x001147E5 File Offset: 0x00112BE5
	public int GetHuntingDifficulty()
	{
		return GGNetworkAdapter.mInstance.GetHuntingDifficulty();
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x001147F1 File Offset: 0x00112BF1
	public List<GGNetworkPlayerProperties> GetResultBlueRankInfoList()
	{
		if (GGNetworkManageGlobalInfo.mInstance.blueRankInfoList == null)
		{
			return new List<GGNetworkPlayerProperties>();
		}
		return GGNetworkManageGlobalInfo.mInstance.blueRankInfoList;
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x00114814 File Offset: 0x00112C14
	private int SortCompareByRating(GGNetworkPlayerProperties PP1, GGNetworkPlayerProperties PP2)
	{
		int result = 0;
		if (PP1.rating > PP2.rating)
		{
			result = -1;
		}
		else if (PP1.rating < PP2.rating)
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x0011484F File Offset: 0x00112C4F
	public List<GGNetworkPlayerProperties> GetResultRedRankInfoList()
	{
		if (GGNetworkManageGlobalInfo.mInstance.redRankInfoList == null)
		{
			return new List<GGNetworkPlayerProperties>();
		}
		return GGNetworkManageGlobalInfo.mInstance.redRankInfoList;
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x00114870 File Offset: 0x00112C70
	public void DestoryNetworkObject(GameObject go)
	{
		GGNetworkAdapter.mInstance.DestoryNetworkObject(go);
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x0011487D File Offset: 0x00112C7D
	public void EndCurrentRound()
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventEndCurrentRound, null);
		}
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x00114897 File Offset: 0x00112C97
	public void NewRound()
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventNewRound, null);
		}
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x001148B1 File Offset: 0x00112CB1
	public int GetMaxRoomNumPerPage()
	{
		return GGNetworkAdapter.MAXROOMNUMPERPAGE;
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x001148B8 File Offset: 0x00112CB8
	public int GetPing()
	{
		return GGNetworkAdapter.mInstance.GetPing();
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x001148C4 File Offset: 0x00112CC4
	public void CreatePlayer(string prefabName, Vector3 position)
	{
		GGNetworkAdapter.mInstance.CreatePlayer(prefabName, position);
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x001148D2 File Offset: 0x00112CD2
	public void SetPlayerName(string playerName)
	{
		GGNetworkAdapter.mInstance.SetPlayerName(playerName);
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x001148DF File Offset: 0x00112CDF
	public void SetIsNeedDisconnecting(bool isNeedDisonnecting)
	{
		GGNetworkAdapter.mInstance.IsNeedDisconnecting = isNeedDisonnecting;
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x001148EC File Offset: 0x00112CEC
	public bool GetRoomList(GGRoomListOperationType opType, GGRoomFilter filter, out List<RoomInfo> resultList)
	{
		return GGNetworkAdapter.mInstance.GetRoomList(opType, filter, out resultList);
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x001148FB File Offset: 0x00112CFB
	public RoomInfo[] SearchRoomByName(int page, string condition)
	{
		return GGNetworkAdapter.mInstance.SearchRoomByName(page, condition);
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x00114909 File Offset: 0x00112D09
	public RoomInfo[] SearchRoomByMaxplayersMode(int page, GGPlayersDisplayInterval interval, GGModeType mode)
	{
		return GGNetworkAdapter.mInstance.SearchRoomByMaxplayersMode(page, interval, mode);
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x00114918 File Offset: 0x00112D18
	public int GetPropNum()
	{
		return this.GetPropDataList().Count;
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x00114925 File Offset: 0x00112D25
	public void CreateNetworkObject(string objectName, Vector3 position, Quaternion rotation)
	{
		GGNetworkAdapter.mInstance.CreateNetworkObject(objectName, position, rotation);
	}

	// Token: 0x060024CB RID: 9419 RVA: 0x00114934 File Offset: 0x00112D34
	public void CreateSeceneObject(string objectName, Vector3 position, Quaternion rotation)
	{
		GGNetworkAdapter.mInstance.CreateSeceneObject(objectName, position, rotation);
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x00114943 File Offset: 0x00112D43
	public void CreateSeceneObject(string objectName, Vector3 position)
	{
		GGNetworkAdapter.mInstance.CreateSeceneObject(objectName, position, Quaternion.identity);
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x00114956 File Offset: 0x00112D56
	public void CreateSeceneObject(string objectName, Vector3 position, object[] data)
	{
		GGNetworkAdapter.mInstance.CreateSeceneObject(objectName, position, Quaternion.identity, data);
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x0011496A File Offset: 0x00112D6A
	public void CreateSeceneObject(string objectName, Vector3 position, Quaternion rotation, object[] data)
	{
		GGNetworkAdapter.mInstance.CreateSeceneObject(objectName, position, rotation, data);
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x0011497B File Offset: 0x00112D7B
	public void DestorySceneObject(GameObject TodestoryGameobject)
	{
		GGNetworkAdapter.mInstance.DestroySceneObject(TodestoryGameobject);
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x00114988 File Offset: 0x00112D88
	public void DestroySceneObjectRPC(GameObject TodestoryGameobject)
	{
		GGNetworkAdapter.mInstance.DestroySceneObjectRPC(TodestoryGameobject);
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x00114995 File Offset: 0x00112D95
	public void DestorySceneObjectMutex(GameObject TodestoryGameobject)
	{
		GGNetworkAdapter.mInstance.DestroySceneObjectMutex(TodestoryGameobject);
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x001149A4 File Offset: 0x00112DA4
	[PunRPC]
	public void PropsRewarded(byte[] byteArrayPropsEventArgs)
	{
		GGPropsEventArgs propsEventArgs = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGPropsEventArgs>(byteArrayPropsEventArgs);
		if (this.PropsReward != null)
		{
			this.PropsReward(propsEventArgs);
		}
	}

	// Token: 0x060024D3 RID: 9427 RVA: 0x001149D4 File Offset: 0x00112DD4
	public void HandOutModeResult(GGModeEventArgs modeEventArgs)
	{
		GGNetworkAdapter.mInstance.HandOutModeResult(modeEventArgs);
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x001149E4 File Offset: 0x00112DE4
	[PunRPC]
	public void HandOutModeedResult(byte[] byteArrayModeEventArgs)
	{
		GGModeEventArgs modeEventArgs = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGModeEventArgs>(byteArrayModeEventArgs);
		if (this.ModeResult != null)
		{
			this.ModeResult(modeEventArgs);
		}
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x00114A14 File Offset: 0x00112E14
	public void SendCape(GGNetworkCape cape, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mInstance.SendCape(cape, targetPlayer);
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x00114A22 File Offset: 0x00112E22
	[PunRPC]
	private void SendedCape(byte[] byteArrayCape)
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventCapeReceive, byteArrayCape);
		}
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x00114A3C File Offset: 0x00112E3C
	public void SendBoot(GGNetworkBoot boot, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mInstance.SendBoot(boot, targetPlayer);
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x00114A4A File Offset: 0x00112E4A
	[PunRPC]
	private void SendedBoot(byte[] byteArrayBoot)
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventBootReceive, byteArrayBoot);
		}
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x00114A64 File Offset: 0x00112E64
	public void SendWeaponProperties(GGNetworkWeaponPropertiesList weaponPropertiesList, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mInstance.SendWeaponProperties(weaponPropertiesList, targetPlayer);
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x00114A72 File Offset: 0x00112E72
	[PunRPC]
	private void SendedWeaponProperties(byte[] byteArrayWeaponProperties)
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventWeaponPropertiesReceive, byteArrayWeaponProperties);
		}
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x00114A8C File Offset: 0x00112E8C
	public void SendHat(GGNetworkHat hat, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mInstance.SendHat(hat, targetPlayer);
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x00114A9A File Offset: 0x00112E9A
	[PunRPC]
	private void SendedHat(byte[] byteArrayHat)
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventHatReceive, byteArrayHat);
		}
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x00114AB4 File Offset: 0x00112EB4
	public void SendSkin(GGNetworkSkin skin, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mInstance.SendSkin(skin, targetPlayer);
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x00114AC2 File Offset: 0x00112EC2
	[PunRPC]
	private void SendedSkin(byte[] byteArraySkin)
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventSkinReceive, byteArraySkin);
		}
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x00114ADC File Offset: 0x00112EDC
	public void DamageToPlayer(GGDamageEventArgs damageEventArgs, PhotonView photonview)
	{
		GGNetworkAdapter.mInstance.DamageToPlayer(damageEventArgs, photonview);
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x00114AEA File Offset: 0x00112EEA
	public void DamageToAI(GGDamageEventArgs damageEventArgs, PhotonView photonview)
	{
		GGNetworkAdapter.mInstance.DamageToAI(damageEventArgs, photonview);
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x00114AF8 File Offset: 0x00112EF8
	[PunRPC]
	public void DamageedToPlayer(byte[] byteArrayDamageEventArgs)
	{
		RioQerdoDebug.Log(" DamageedToPlayer: ");
		GGDamageEventArgs ggdamageEventArgs = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGDamageEventArgs>(byteArrayDamageEventArgs);
		if (this.mDamageListFromOtherPlayers.ContainsKey(ggdamageEventArgs.name))
		{
			this.mDamageListFromOtherPlayers[ggdamageEventArgs.name].Enqueue(ggdamageEventArgs);
		}
		else
		{
			Queue<GGDamageEventArgs> queue = new Queue<GGDamageEventArgs>();
			queue.Enqueue(ggdamageEventArgs);
			this.mDamageListFromOtherPlayers.Add(ggdamageEventArgs.name, queue);
		}
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x00114B6C File Offset: 0x00112F6C
	[PunRPC]
	public void DamageedToAI(byte[] byteArrayDamageEventArgs)
	{
		GGDamageEventArgs damageEventArgs = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGDamageEventArgs>(byteArrayDamageEventArgs);
		if (this.AIReceiveDamage != null)
		{
			this.AIReceiveDamage(damageEventArgs);
		}
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x00114B9C File Offset: 0x00112F9C
	public void RequestPlayerSeasonInfo(int playerID)
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessagePlayerSeasonInfo;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.ID = PhotonNetwork.player.ID;
		GGNetworkKit.mInstance.SendMessage(ggmessage, playerID);
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x00114BE8 File Offset: 0x00112FE8
	public GGPlayerSeasonInfo GetMySeasonInfo()
	{
		GGPlayerSeasonInfo ggplayerSeasonInfo = new GGPlayerSeasonInfo();
		CareerStatValuesSeason careerStatValuesOfSeason = GrowthManagerKit.GetCareerStatValuesOfSeason();
		ggplayerSeasonInfo.Score = careerStatValuesOfSeason.seasonScore;
		ggplayerSeasonInfo.Rank = careerStatValuesOfSeason.seasonRank;
		ggplayerSeasonInfo.Killing = careerStatValuesOfSeason.totalKilling;
		ggplayerSeasonInfo.HeadShot = careerStatValuesOfSeason.totalHeadshotKilling;
		ggplayerSeasonInfo.GodLike = careerStatValuesOfSeason.totalGodLikeKilling;
		ggplayerSeasonInfo.KillingJoin = careerStatValuesOfSeason.totalKillingCompetitionModeJoinCount;
		ggplayerSeasonInfo.KillingVictory = careerStatValuesOfSeason.totalKillingCompetitionModeVictoryCount;
		ggplayerSeasonInfo.KillingVictoryRate = careerStatValuesOfSeason.killingCompetitionModeVictoryRate;
		ggplayerSeasonInfo.KillingMVP = careerStatValuesOfSeason.totalKillingCompetitionModeMvpCount;
		ggplayerSeasonInfo.StrongholdJoin = careerStatValuesOfSeason.totalStrongholdModeJoinCount;
		ggplayerSeasonInfo.StrongholdVictory = careerStatValuesOfSeason.totalStrongholdModeVictoryCount;
		ggplayerSeasonInfo.StrongholdVictoryRate = careerStatValuesOfSeason.strongholdModeVictoryRate;
		ggplayerSeasonInfo.StrongholdMVP = careerStatValuesOfSeason.totalStrongholdModeMvpCount;
		ggplayerSeasonInfo.ExplositionJoin = careerStatValuesOfSeason.totalExplosionModeJoinCount;
		ggplayerSeasonInfo.ExplositionVictory = careerStatValuesOfSeason.totalExplosionModeVictoryCount;
		ggplayerSeasonInfo.ExplositionVictoryRate = careerStatValuesOfSeason.explosionModeVictoryRate;
		ggplayerSeasonInfo.ExplositionMVP = careerStatValuesOfSeason.totalExplosionModeMvpCount;
		ggplayerSeasonInfo.RoleName = (string)PhotonNetwork.player.customProperties["name"];
		ggplayerSeasonInfo.CharacterLevel = (int)PhotonNetwork.player.customProperties["rank"];
		return ggplayerSeasonInfo;
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x00114D0C File Offset: 0x0011310C
	public void SendPlayerSeasonInfo(GGPlayerSeasonInfo seasonInfo, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mInstance.SendPlayerSeasonInfo(seasonInfo, targetPlayer);
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x00114D1A File Offset: 0x0011311A
	[PunRPC]
	private void SendedPlayerSeasonInfo(byte[] byteArraySeasonInfo)
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventPlayerSeasonInfo, byteArraySeasonInfo);
		}
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00114D35 File Offset: 0x00113135
	public void KickPlayer(PhotonPlayer kickplayer)
	{
		GGNetworkAdapter.mInstance.KickPlayer(kickplayer);
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x00114D42 File Offset: 0x00113142
	public PhotonPlayer[] GetPlayerList()
	{
		return GGNetworkAdapter.mInstance.GetPlayerList();
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x00114D50 File Offset: 0x00113150
	public string[] GetOtherPlayersName()
	{
		PhotonPlayer[] playerList = this.GetPlayerList();
		List<string> list = new List<string>();
		foreach (PhotonPlayer photonPlayer in playerList)
		{
			if (!photonPlayer.isLocal)
			{
				list.Add(photonPlayer.name);
			}
		}
		return list.ToArray();
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00114DA6 File Offset: 0x001131A6
	public void SwitchServer(GGServerRegion sr)
	{
		GGNetworkAdapter.mInstance.SwitchServer(sr);
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x00114DB4 File Offset: 0x001131B4
	public void ConnectToNetwork()
	{
		if (GGNetworkAdapter.mInstance != null)
		{
			string gameVersion = "gog_ios_" + GameVersionController.PhotonGameVersion;
			GGNetworkAdapter.mInstance.ConnectToNetwork(gameVersion);
		}
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x00114DEC File Offset: 0x001131EC
	public void ConnectToBestCloudServer()
	{
		string gameVersion = "gog_ios_" + GameVersionController.PhotonGameVersion;
		PhotonNetwork.ConnectToBestCloudServer(gameVersion);
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x00114E10 File Offset: 0x00113210
	public bool IsMine()
	{
		return GGNetworkAdapter.mInstance.IsMine;
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x00114E1C File Offset: 0x0011321C
	public GGNetworkManagePlayerProperties GetManagePlayerProperties()
	{
		return GGNetworkManagePlayerProperties.mInstance;
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x00114E23 File Offset: 0x00113223
	public GGNetworkManageGlobalInfo GetManageGlobalInfo()
	{
		return GGNetworkManageGlobalInfo.mInstance;
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00114E2A File Offset: 0x0011322A
	public GameObject GetMainPlayer()
	{
		if (GGNetworkAdapter.mInstance.mMainPlayer == null)
		{
			GGNetworkAdapter.mInstance.mMainPlayer = GameObject.FindWithTag("Player");
		}
		return GGNetworkAdapter.mInstance.mMainPlayer;
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x00114E5F File Offset: 0x0011325F
	public GameObject GetMirrorByPlayerID(int playerID)
	{
		return GGNetworkAdapter.mInstance.GetMirrorByPlayerID(playerID);
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x00114E6C File Offset: 0x0011326C
	public PhotonPlayer GetPhotonPlayerByPlayerID(int playerID)
	{
		return GGNetworkAdapter.mInstance.GetPhotonPlayerByPlayerID(playerID);
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x00114E7C File Offset: 0x0011327C
	public Dictionary<int, GameObject> GetPlayerGameObjectList()
	{
		Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
		Dictionary<int, GameObject> instantiatedGameObjectList = GGNetworkAdapter.mInstance.GetInstantiatedGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in instantiatedGameObjectList)
		{
			if (keyValuePair.Value != null && keyValuePair.Value.name.Contains("ExampleCharacter"))
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		return dictionary;
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x00114F20 File Offset: 0x00113320
	public Dictionary<int, object[]> GetTowerGrenadeDataList()
	{
		Dictionary<int, object[]> dictionary = new Dictionary<int, object[]>();
		Dictionary<int, GameObject> instantiatedGameObjectList = GGNetworkAdapter.mInstance.GetInstantiatedGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in instantiatedGameObjectList)
		{
			if (keyValuePair.Value != null && keyValuePair.Value.tag == "TowerGrenade")
			{
				object[] instantiationData = keyValuePair.Value.GetPhotonView().instantiationData;
				if (instantiationData != null)
				{
					dictionary.Add(keyValuePair.Key, instantiationData);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00114FD8 File Offset: 0x001133D8
	public Dictionary<int, object[]> GetPropDataList()
	{
		Dictionary<int, object[]> dictionary = new Dictionary<int, object[]>();
		Dictionary<int, GameObject> instantiatedGameObjectList = GGNetworkAdapter.mInstance.GetInstantiatedGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in instantiatedGameObjectList)
		{
			if (keyValuePair.Value != null && keyValuePair.Value.tag == "Props")
			{
				object[] instantiationData = keyValuePair.Value.GetPhotonView().instantiationData;
				if (instantiationData != null)
				{
					dictionary.Add(keyValuePair.Key, instantiationData);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x00115090 File Offset: 0x00113490
	public object[] GetPropDataByViewID(int viewID)
	{
		Dictionary<int, object[]> towerGrenadeDataList = this.GetTowerGrenadeDataList();
		if (towerGrenadeDataList.ContainsKey(viewID))
		{
			return towerGrenadeDataList[viewID];
		}
		return null;
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x001150BC File Offset: 0x001134BC
	public int GetPropRandomPositionIndex(int maxIndex)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < maxIndex; i++)
		{
			list.Add(i);
		}
		Dictionary<int, object[]> propDataList = this.GetPropDataList();
		foreach (KeyValuePair<int, object[]> keyValuePair in propDataList)
		{
			if (keyValuePair.Value.Length > 0)
			{
				list.Remove((int)keyValuePair.Value[0]);
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			return list[index];
		}
		return UnityEngine.Random.Range(0, maxIndex);
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x00115184 File Offset: 0x00113584
	public void LoadLevel(string levelName)
	{
		GGNetworkAdapter.mInstance.LoadLevel(levelName);
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x00115191 File Offset: 0x00113591
	public void LeaveRoom()
	{
		GGNetworkAdapter.mInstance.LeaveRoom();
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x0011519D File Offset: 0x0011359D
	public List<GGNetworkPlayerProperties> GetPlayerPropertiesList()
	{
		return GGNetworkManagePlayerProperties.mInstance.GetPlayerPropertiesList();
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x001151A9 File Offset: 0x001135A9
	public List<GGNetworkPlayerProperties> GetRedPlayerPropertiesList()
	{
		return GGNetworkManagePlayerProperties.mInstance.GetRedPlayerPropertiesList();
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x001151B5 File Offset: 0x001135B5
	public List<GameObject> GetRedLivePlayerObserverCameraList()
	{
		return GGNetworkManagePlayerProperties.mInstance.GetRedLivePlayerObserverCameraList();
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x001151C1 File Offset: 0x001135C1
	public List<GGNetworkPlayerProperties> GetBluePlayerPropertiesList()
	{
		return GGNetworkManagePlayerProperties.mInstance.GetBluePlayerPropertiesList();
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x001151CD File Offset: 0x001135CD
	public List<GameObject> GetBlueLivePlayerObserverCameraList()
	{
		return GGNetworkManagePlayerProperties.mInstance.GetBlueLivePlayerObserverCameraList();
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x001151D9 File Offset: 0x001135D9
	public void DanamicSwitchTeam()
	{
		base.StartCoroutine(this.EnumeratorDanamicSwitchTeam());
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x001151E8 File Offset: 0x001135E8
	protected IEnumerator EnumeratorDanamicSwitchTeam()
	{
		yield return new WaitForSeconds(this.mWaitForSeconds);
		GameObject goMainplayer = this.GetMainPlayer();
		if (goMainplayer == null)
		{
			if (UIPauseDirector.mInstance.pauseNode.activeSelf)
			{
				UIPauseDirector.mInstance.ShowLobbyNode();
			}
		}
		else
		{
			if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
			{
				goMainplayer.GetComponent<GGNetworkCharacter>().mPlayerProperties.team = GGTeamType.blue;
			}
			else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
			{
				goMainplayer.GetComponent<GGNetworkCharacter>().mPlayerProperties.team = GGTeamType.blue;
			}
			else
			{
				goMainplayer.GetComponent<GGNetworkCharacter>().mPlayerProperties.team = this.GetProperTeam();
			}
			if (this.UserEvent != null)
			{
				this.UserEvent(GGEventType.FirstJoinDynamicSwitchPlayerTeam, null);
			}
		}
		yield break;
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x00115204 File Offset: 0x00113604
	public IEnumerator JoinRoomTimeOut()
	{
		yield return new WaitForSeconds(20f);
		this.bExcuteJoinRoomTimeOut = false;
		GGNetworkAdapter.mInstance.DisconnectPhotonNetwork();
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.LoadingPanelHide, null);
		}
		yield break;
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x00115220 File Offset: 0x00113620
	public bool SwitchPlayerTeam()
	{
		GGTeamType team = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.team;
		if (team == GGTeamType.red)
		{
			if (this.GetBluePlayerPropertiesList().Count - this.GetRedPlayerPropertiesList().Count >= 0)
			{
				return false;
			}
			GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.team = GGTeamType.blue;
			if (this.UserEvent != null)
			{
				this.UserEvent(GGEventType.EventTeamChange, null);
			}
			return true;
		}
		else
		{
			if (team != GGTeamType.blue)
			{
				return false;
			}
			if (this.GetRedPlayerPropertiesList().Count - this.GetBluePlayerPropertiesList().Count >= 0)
			{
				return false;
			}
			GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.team = GGTeamType.red;
			if (this.UserEvent != null)
			{
				this.UserEvent(GGEventType.EventTeamChange, null);
			}
			return true;
		}
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x001152FB File Offset: 0x001136FB
	public List<GGNetworkPlayerProperties> GetOtherPlayerPropertiesList()
	{
		return GGNetworkManagePlayerProperties.mInstance.GetOtherPlayerPropertiesList();
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x00115307 File Offset: 0x00113707
	public void CustomPlayerProperties(string name, int rank)
	{
		GGNetworkAdapter.mInstance.CustomPlayerProperties(name, rank);
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x00115315 File Offset: 0x00113715
	public void DoModeLogic(GGNetworkMode mode)
	{
		GGNetworkManageGlobalInfo.mInstance.DoModeLogic(mode);
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x00115322 File Offset: 0x00113722
	public void SendMessage(GGMessage message, GGTarget target)
	{
		GGNetworkAdapter.mInstance.SendMessage(message, target);
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x00115330 File Offset: 0x00113730
	public void SendMessage(GGMessage message, int playerID)
	{
		GGNetworkAdapter.mInstance.SendMessage(message, playerID);
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x0011533E File Offset: 0x0011373E
	public void SendMessage(GGMessage message, PhotonPlayer player)
	{
		GGNetworkAdapter.mInstance.SendMessage(message, player);
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x0011534C File Offset: 0x0011374C
	[PunRPC]
	private void SendedMessage(byte[] byteArrayMessage)
	{
		RioQerdoDebug.Log("receive message");
		GGMessage message = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGMessage>(byteArrayMessage);
		if (this.MessageOk != null)
		{
			this.MessageOk(message);
		}
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x00115386 File Offset: 0x00113786
	public void OnCreatedRoom()
	{
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.RoomCreatedSuccess, null);
		}
		RioQerdoDebug.Log("OnCreatedRoom");
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x001153AE File Offset: 0x001137AE
	private void OnPhotonJoinRoomFailed()
	{
		base.StopCoroutine("JoinRoomTimeOut");
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.RoomJoinRoomNotExistOrFull, null);
		}
		RioQerdoDebug.Log("OnPhotonJoinRoomFailed");
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x001153E1 File Offset: 0x001137E1
	public void OnFailedToConnectToPhoton(object parameters)
	{
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x001153E3 File Offset: 0x001137E3
	private void OnPhotonCreateRoomFailed()
	{
		base.StopCoroutine("JoinRoomTimeOut");
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.RoomCreateSameNameRoom, null);
		}
		RioQerdoDebug.Log("A CreateRoom call failed, most likely the room name is already in use.");
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x00115416 File Offset: 0x00113816
	private void OnDisconnectedFromPhoton()
	{
		RioQerdoDebug.Log("OnDisconnectedFromPhoton");
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x00115422 File Offset: 0x00113822
	private void OnPhotonRandomJoinFailed()
	{
		RioQerdoDebug.Log("OnPhotonRandomJoinFailed");
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.RandomJoinRoomNotAvailableRoom, null);
		}
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x0011544A File Offset: 0x0011384A
	public void SendNecessaryResourceNewRound()
	{
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x0011544C File Offset: 0x0011384C
	private void DisplayPlayerDisconnectInfo(PhotonPlayer player)
	{
		GGNetworkSystemMessage ggnetworkSystemMessage = new GGNetworkSystemMessage();
		ggnetworkSystemMessage.content = player + " leave";
		ggnetworkSystemMessage.color = GGColor.White;
		GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage);
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x00115484 File Offset: 0x00113884
	private void DisplayPlayerJoinInInfo(PhotonPlayer player)
	{
		GGNetworkSystemMessage ggnetworkSystemMessage = new GGNetworkSystemMessage();
		ggnetworkSystemMessage.color = GGColor.White;
		ggnetworkSystemMessage.content = player + " join";
		GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage);
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x001154BA File Offset: 0x001138BA
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		RioQerdoDebug.Log("OnPhotonPlayerDisconnected: " + player);
		if (this.IsMasterClient())
		{
			this.DisplayPlayerDisconnectInfo(player);
			if (this.UserEvent != null)
			{
				this.UserEvent(GGEventType.EventPlayerLeave, null);
			}
		}
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x001154FA File Offset: 0x001138FA
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		RioQerdoDebug.Log("OnPhotonPlayerConnected...");
		if (this.IsMasterClient())
		{
			this.DisplayPlayerJoinInInfo(player);
		}
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x00115518 File Offset: 0x00113918
	private void OnJoinedLobby()
	{
		RioQerdoDebug.Log("OnJoinedLobby");
		if (this.UserEvent != null)
		{
			this.UserEvent(GGEventType.EventOnJoinLobby, null);
		}
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x0011553C File Offset: 0x0011393C
	private void OnConnectedToPhoton()
	{
		RioQerdoDebug.Log("This client has connected to a server");
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x00115548 File Offset: 0x00113948
	private void OnJoinedRoom()
	{
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x0011554A File Offset: 0x0011394A
	public void SwitchMasterClient()
	{
		GGNetworkAdapter.mInstance.SwitchMasterClient();
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x00115558 File Offset: 0x00113958
	private void OnMasterClientSwitched()
	{
		RioQerdoDebug.Log("OnMasterClientSwitched: " + GGNetworkAdapter.mInstance.MasterClientID);
		if (GameObject.FindWithTag("GlobalInfoManager") == null)
		{
			this.CreateSeceneObject("GlobalInfoManager", new Vector3(-100f, -100f, -100f), Quaternion.identity);
		}
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x001155BC File Offset: 0x001139BC
	public GGPlayModeType GetGamePlayModeType()
	{
		return UIPlayModeSelectDirector.mPlayModeType;
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x001155C3 File Offset: 0x001139C3
	public void SetRoomListCurrentPage(int page)
	{
		GGNetworkAdapter.mInstance.SetCurPage(page);
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x001155D0 File Offset: 0x001139D0
	public int GetRoomListCurrentPageNum()
	{
		return GGNetworkAdapter.mInstance.GetCurPage();
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x001155DC File Offset: 0x001139DC
	public int GetRoomListAllPageNum()
	{
		return GGNetworkAdapter.mInstance.GetAllPageNum();
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x001155E8 File Offset: 0x001139E8
	public GGTeamType GetProperTeam()
	{
		int count = this.GetManagePlayerProperties().GetBluePlayerPropertiesList().Count;
		int count2 = this.GetManagePlayerProperties().GetRedPlayerPropertiesList().Count;
		RioQerdoDebug.Log("bluePlayerNum: " + count);
		RioQerdoDebug.Log("redPlayerNum: " + count2);
		if (count < count2)
		{
			return GGTeamType.blue;
		}
		if (count > count2)
		{
			return GGTeamType.red;
		}
		return (UnityEngine.Random.Range(0, 1001) <= 500) ? GGTeamType.red : GGTeamType.blue;
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x0011566F File Offset: 0x00113A6F
	public int GetPlayersNumInRoom()
	{
		return GGNetworkAdapter.mInstance.GetPlayersNumInRoom();
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x0011567B File Offset: 0x00113A7B
	public ConnectionState GetConnectionState()
	{
		return GGNetworkAdapter.mInstance.GetConnectionState();
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x00115687 File Offset: 0x00113A87
	private Room GetCurrentRoom()
	{
		return GGNetworkAdapter.mInstance.GetCurrentRoom();
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x00115693 File Offset: 0x00113A93
	public void SetRoomJoinableStatus(bool status)
	{
		GGNetworkAdapter.mInstance.SetRoomJoinableStatus(status);
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x001156A0 File Offset: 0x00113AA0
	private void Update()
	{
		this.CheckConnectionState();
		this.mCheckCacDamageTime += Time.deltaTime;
		if (this.mCheckCacDamageTime > GGNetworkKit.CACULATEDAMAGEDELTATIME)
		{
			this.mCheckCacDamageTime = 0f;
			foreach (string key in this.mDeadMirrorPlayerList)
			{
				if (this.mDamageListFromOtherPlayers.ContainsKey(key))
				{
					this.mDamageListFromOtherPlayers.Remove(key);
				}
			}
			if (this.mDamageListFromOtherPlayers.Count > 0)
			{
				foreach (KeyValuePair<string, Queue<GGDamageEventArgs>> keyValuePair in this.mDamageListFromOtherPlayers)
				{
					if (keyValuePair.Value.Count > 0 && this.ReceiveDamage != null)
					{
						this.ReceiveDamage(keyValuePair.Value.Dequeue());
					}
				}
			}
		}
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x001157D0 File Offset: 0x00113BD0
	public void DisconnectFromRoom()
	{
		base.StartCoroutine(this.EnumeratorDisconnectFromRoom());
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x001157DF File Offset: 0x00113BDF
	public void RemoveDisconnectFromRoomCoroutine()
	{
		base.StopCoroutine("EnumeratorDisconnectFromRoom");
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x001157EC File Offset: 0x00113BEC
	private IEnumerator EnumeratorDisconnectFromRoom()
	{
		yield return new WaitForSeconds(60f);
		if (GameObject.FindWithTag("Player") == null)
		{
			this.LeaveRoom();
			if (this.UserEvent != null)
			{
				this.UserEvent(GGEventType.RoomToLobbyWhenNotClickNewRound, null);
			}
		}
		yield break;
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x00115808 File Offset: 0x00113C08
	private void CheckConnectionState()
	{
		if ((double)Time.time - this.mLastTime >= 1.0)
		{
			ConnectionState connectionState = this.GetConnectionState();
			if (connectionState == ConnectionState.Connected)
			{
				if (this.mLastConnectionState != ConnectionState.Connected)
				{
					RioQerdoDebug.Log("EventDisconnectedToConnected");
					if (this.UserEvent != null)
					{
						this.UserEvent(GGEventType.EventDisconnectedToConnected, null);
					}
				}
			}
			else if (connectionState != ConnectionState.InitializingApplication)
			{
				RioQerdoDebug.Log("curConnectionState: " + connectionState);
				if (this.UserEvent != null)
				{
					this.UserEvent(GGEventType.EventNoConnection, null);
				}
				this.ConnectToNetwork();
			}
			this.mLastTime = (double)Time.time;
			this.mLastConnectionState = connectionState;
		}
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x001158CB File Offset: 0x00113CCB
	public void Test()
	{
	}

	// Token: 0x040025E1 RID: 9697
	public static GGNetworkKit mInstance;

	// Token: 0x040025E9 RID: 9705
	private double mLastTime;

	// Token: 0x040025EA RID: 9706
	private ConnectionState mLastConnectionState;

	// Token: 0x040025EB RID: 9707
	public string mCurRoomName = string.Empty;

	// Token: 0x040025EC RID: 9708
	public float mWaitForSeconds = 2.5f;

	// Token: 0x040025ED RID: 9709
	public float mCheckNecessaryDataTime = 2.5f;

	// Token: 0x040025EE RID: 9710
	public float mCheckCacDamageTime;

	// Token: 0x040025EF RID: 9711
	public static float CACULATEDAMAGEDELTATIME = 0.18f;

	// Token: 0x040025F0 RID: 9712
	public bool mCanJoinFriendRoom;

	// Token: 0x040025F1 RID: 9713
	public Dictionary<string, Queue<GGDamageEventArgs>> mDamageListFromOtherPlayers = new Dictionary<string, Queue<GGDamageEventArgs>>();

	// Token: 0x040025F2 RID: 9714
	public List<string> mDeadMirrorPlayerList = new List<string>();

	// Token: 0x040025F3 RID: 9715
	public bool bExcuteJoinRoomTimeOut;
}
