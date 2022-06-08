using System;
using System.Collections;
using System.Collections.Generic;
using RioLog;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
public class GGNetworkInteraction : MonoBehaviour
{
	// Token: 0x060022D4 RID: 8916 RVA: 0x00103BD6 File Offset: 0x00101FD6
	private void Start()
	{
		this.Init();
		this.examplecharacter = GameObject.FindWithTag("Player");
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x00103BEE File Offset: 0x00101FEE
	private void Init()
	{
		this.InitEvent();
	}

	// Token: 0x060022D6 RID: 8918 RVA: 0x00103BF8 File Offset: 0x00101FF8
	private void InitEvent()
	{
		GGNetworkKit.mInstance.UserEvent += this.HandleUserEvent;
		GGNetworkKit.mInstance.MessageOk += this.HandleMessageOk;
		GGNetworkKit.mInstance.PropsReward += this.HandlePropsReward;
	}

	// Token: 0x060022D7 RID: 8919 RVA: 0x00103C48 File Offset: 0x00102048
	private void HandlePropsReward(GGPropsEventArgs propsEventArgs)
	{
		string propsTag = propsEventArgs.propsTag;
		if (propsTag != null)
		{
			if (!(propsTag == "Props"))
			{
				if (!(propsTag == string.Empty))
				{
				}
			}
			else
			{
				RioQerdoDebug.Log(GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.name + " get props!");
			}
		}
	}

	// Token: 0x060022D8 RID: 8920 RVA: 0x00103CBC File Offset: 0x001020BC
	private void HandleMessageOk(GGMessage message)
	{
		GGMessageType messageType = message.messageType;
		switch (messageType)
		{
		case GGMessageType.MessagePlayerConnected:
			break;
		case GGMessageType.MessageNotifySkin:
		{
			GGNetworkSkin ggnetworkSkin = new GGNetworkSkin();
			ggnetworkSkin.data = SkinManager.GetMySettedSkinTexData();
			ggnetworkSkin.ownerID = PhotonNetwork.player.ID;
			ggnetworkSkin.needTargetSkin = true;
			if (message.messageContent.ID == 2)
			{
				Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
				foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
				{
					GameObject value = keyValuePair.Value;
					PhotonView component = value.GetComponent<PhotonView>();
					PhotonPlayer photonPlayer = PhotonPlayer.Find(component.ownerId);
					if (!photonPlayer.isLocal)
					{
						GGNetworkKit.mInstance.SendSkin(ggnetworkSkin, photonPlayer);
					}
				}
			}
			else
			{
				foreach (PhotonPlayer photonPlayer2 in GGNetworkKit.mInstance.GetPlayerList())
				{
					if (!photonPlayer2.isLocal)
					{
						GGNetworkKit.mInstance.SendSkin(ggnetworkSkin, photonPlayer2);
					}
				}
			}
			break;
		}
		case GGMessageType.MessageNotifyHat:
		{
			GGNetworkHat ggnetworkHat = new GGNetworkHat();
			ggnetworkHat.data = HatManager.GetMySettedHatPackData();
			ggnetworkHat.ownerID = PhotonNetwork.player.ID;
			ggnetworkHat.needTargetHat = true;
			if (message.messageContent.ID == 2)
			{
				Dictionary<int, GameObject> playerGameObjectList2 = GGNetworkKit.mInstance.GetPlayerGameObjectList();
				foreach (KeyValuePair<int, GameObject> keyValuePair2 in playerGameObjectList2)
				{
					GameObject value2 = keyValuePair2.Value;
					PhotonView component2 = value2.GetComponent<PhotonView>();
					PhotonPlayer photonPlayer3 = PhotonPlayer.Find(component2.ownerId);
					if (!photonPlayer3.isLocal)
					{
						GGNetworkKit.mInstance.SendHat(ggnetworkHat, photonPlayer3);
					}
				}
			}
			else
			{
				foreach (PhotonPlayer photonPlayer4 in GGNetworkKit.mInstance.GetPlayerList())
				{
					if (!photonPlayer4.isLocal)
					{
						RioQerdoDebug.Log("send my hat to all the people");
						GGNetworkKit.mInstance.SendHat(ggnetworkHat, photonPlayer4);
					}
				}
			}
			break;
		}
		case GGMessageType.MessageNotifyCape:
		{
			RioQerdoDebug.Log("MessageNotifyCape");
			GGNetworkCape ggnetworkCape = new GGNetworkCape();
			ggnetworkCape.data = CapeManager.GetMySettedCapePackData();
			ggnetworkCape.ownerID = PhotonNetwork.player.ID;
			ggnetworkCape.needTargetCape = true;
			if (message.messageContent.ID == 2)
			{
				Dictionary<int, GameObject> playerGameObjectList3 = GGNetworkKit.mInstance.GetPlayerGameObjectList();
				foreach (KeyValuePair<int, GameObject> keyValuePair3 in playerGameObjectList3)
				{
					GameObject value3 = keyValuePair3.Value;
					PhotonView component3 = value3.GetComponent<PhotonView>();
					PhotonPlayer photonPlayer5 = PhotonPlayer.Find(component3.ownerId);
					if (!photonPlayer5.isLocal)
					{
						GGNetworkKit.mInstance.SendCape(ggnetworkCape, photonPlayer5);
					}
				}
			}
			else
			{
				foreach (PhotonPlayer photonPlayer6 in GGNetworkKit.mInstance.GetPlayerList())
				{
					if (!photonPlayer6.isLocal)
					{
						RioQerdoDebug.Log("send my hat to all the people");
						GGNetworkKit.mInstance.SendCape(ggnetworkCape, photonPlayer6);
					}
				}
			}
			break;
		}
		case GGMessageType.MessageNotifyWeaponProperties:
		{
			RioQerdoDebug.Log("*********************************MessageNotifyWeaponProperties");
			GGNetworkWeaponPropertiesList ggnetworkWeaponPropertiesList = new GGNetworkWeaponPropertiesList();
			ggnetworkWeaponPropertiesList.weaponPropertiesList = GGWeaponManager.mInstance.GetWeaponProperties();
			ggnetworkWeaponPropertiesList.needTargetWeaponProperties = true;
			ggnetworkWeaponPropertiesList.ownerID = PhotonNetwork.player.ID;
			if (message.messageContent.ID == 2)
			{
				Dictionary<int, GameObject> playerGameObjectList4 = GGNetworkKit.mInstance.GetPlayerGameObjectList();
				foreach (KeyValuePair<int, GameObject> keyValuePair4 in playerGameObjectList4)
				{
					GameObject value4 = keyValuePair4.Value;
					PhotonView component4 = value4.GetComponent<PhotonView>();
					PhotonPlayer photonPlayer7 = PhotonPlayer.Find(component4.ownerId);
					if (!photonPlayer7.isLocal)
					{
						GGNetworkKit.mInstance.SendWeaponProperties(ggnetworkWeaponPropertiesList, photonPlayer7);
					}
				}
			}
			else
			{
				foreach (PhotonPlayer photonPlayer8 in GGNetworkKit.mInstance.GetPlayerList())
				{
					if (!photonPlayer8.isLocal)
					{
						RioQerdoDebug.Log("send my weapon is power to all the people");
						GGNetworkKit.mInstance.SendWeaponProperties(ggnetworkWeaponPropertiesList, photonPlayer8);
					}
				}
			}
			break;
		}
		default:
			switch (messageType)
			{
			case GGMessageType.MessageNotifyBoot:
			{
				RioQerdoDebug.Log("MessageNotifyBoot");
				GGNetworkBoot ggnetworkBoot = new GGNetworkBoot();
				ggnetworkBoot.data = BootManager.GetMySettedBootPackData();
				ggnetworkBoot.ownerID = PhotonNetwork.player.ID;
				ggnetworkBoot.needTargetBoot = true;
				if (message.messageContent.ID == 2)
				{
					Dictionary<int, GameObject> playerGameObjectList5 = GGNetworkKit.mInstance.GetPlayerGameObjectList();
					foreach (KeyValuePair<int, GameObject> keyValuePair5 in playerGameObjectList5)
					{
						GameObject value5 = keyValuePair5.Value;
						PhotonView component5 = value5.GetComponent<PhotonView>();
						PhotonPlayer photonPlayer9 = PhotonPlayer.Find(component5.ownerId);
						if (!photonPlayer9.isLocal)
						{
							GGNetworkKit.mInstance.SendBoot(ggnetworkBoot, photonPlayer9);
						}
					}
				}
				else
				{
					foreach (PhotonPlayer photonPlayer10 in GGNetworkKit.mInstance.GetPlayerList())
					{
						if (!photonPlayer10.isLocal)
						{
							RioQerdoDebug.Log("send my boot to all the people");
							GGNetworkKit.mInstance.SendBoot(ggnetworkBoot, photonPlayer10);
						}
					}
				}
				break;
			}
			case GGMessageType.MessageNotifyMutationTrap:
			{
				Vector3 position = new Vector3(message.messageContent.X, message.messageContent.Y, message.messageContent.Z);
				GGNetworkKit.mInstance.CreateSeceneObject("SpeedTrap", position);
				break;
			}
			case GGMessageType.MessageNotifyMutationHorror:
				GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetWorkPlayerlogic>().BeJingxiaEffect();
				break;
			case GGMessageType.MessageNotifyMutationBlind:
				GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetWorkPlayerlogic>().BlindRender();
				break;
			case GGMessageType.MessageNotifyHolidayFireworks:
			{
				Dictionary<int, GameObject> playerGameObjectList6 = GGNetworkKit.mInstance.GetPlayerGameObjectList();
				foreach (KeyValuePair<int, GameObject> keyValuePair6 in playerGameObjectList6)
				{
					GameObject value6 = keyValuePair6.Value;
					PhotonView component6 = value6.GetComponent<PhotonView>();
					if (component6.viewID == message.messageContent.ID2)
					{
						value6.GetComponent<FireworkShooter>().FireworkShoot(message.messageContent.ID);
					}
				}
				break;
			}
			default:
				if (messageType != GGMessageType.MessagePlayerSeasonInfo)
				{
					if (messageType == GGMessageType.MessageNotifyNightmare)
					{
						GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetWorkPlayerlogic>().Nightmare();
					}
				}
				else
				{
					GGPlayerSeasonInfo mySeasonInfo = GGNetworkKit.mInstance.GetMySeasonInfo();
					int id = message.messageContent.ID;
					PhotonPlayer targetPlayer = PhotonPlayer.Find(id);
					GGNetworkKit.mInstance.SendPlayerSeasonInfo(mySeasonInfo, targetPlayer);
				}
				break;
			}
			break;
		case GGMessageType.MessageNotifyAllNecessaryData:
		{
			int id2 = message.messageContent.ID;
			this.SendNessaryData(id2);
			break;
		}
		}
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x00104454 File Offset: 0x00102854
	private void HandleUserEvent(GGEventType eventType, byte[] data)
	{
		switch (eventType)
		{
		case GGEventType.EventSkinReceive:
		{
			RioQerdoDebug.Log("receive other's skin");
			GGNetworkSkin ggnetworkSkin = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkSkin>(data);
			Material material = SkinManager.DeserializeSkinDataToMat(ggnetworkSkin.data);
			material.name = ggnetworkSkin.ownerID.ToString() + "_SkinMat";
			int ownerID = ggnetworkSkin.ownerID;
			RioQerdoDebug.Log("skin: " + ownerID);
			GameObject mirrorByPlayerID = GGNetworkKit.mInstance.GetMirrorByPlayerID(ownerID);
			if (mirrorByPlayerID != null)
			{
				mirrorByPlayerID.transform.Find("Player_1_sinkmesh/Player").SendMessage("SetNetworkPlayerSkin", material, SendMessageOptions.RequireReceiver);
			}
			if (ggnetworkSkin.needTargetSkin)
			{
				RioQerdoDebug.Log("send my skin to new player");
				GGNetworkSkin ggnetworkSkin2 = new GGNetworkSkin();
				ggnetworkSkin2.data = SkinManager.GetMySettedSkinTexData();
				ggnetworkSkin2.ownerID = PhotonNetwork.player.ID;
				ggnetworkSkin2.needTargetSkin = false;
				GGNetworkKit.mInstance.SendSkin(ggnetworkSkin2, GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(ownerID));
			}
			break;
		}
		case GGEventType.EventTeamChange:
			if (this.examplecharacter == null)
			{
				this.examplecharacter = GameObject.FindWithTag("Player");
			}
			if (this.examplecharacter != null && this.examplecharacter.GetComponent<GGNetWorkPlayerlogic>().isOnPauseStart)
			{
				this.examplecharacter.GetComponent<GGNetWorkPlayerlogic>().RandomPosition();
			}
			break;
		case GGEventType.EventOnJoinLobby:
			GGNetworkKit.mInstance.SetPlayerName(UIUserDataController.GetDefaultRoleName());
			GGNetworkKit.mInstance.CustomPlayerProperties(UIUserDataController.GetDefaultRoleName(), GrowthManagerKit.GetCharacterLevel());
			if (Application.loadedLevelName == "DisconnectionReJoinScene")
			{
				GameObject gameObject = GameObject.FindWithTag("SwitchSceneInfo");
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				string @string = PlayerPrefs.GetString("LastRoomNameBeforeDisconnect");
				GGNetworkKit.mInstance.JoinRoom(@string);
			}
			if (GGNetworkKit.mInstance.mCanJoinFriendRoom)
			{
				GGNetworkKit.mInstance.mCanJoinFriendRoom = false;
				string string2 = PlayerPrefs.GetString("GGNetworkJoinFriendRoomName", string.Empty);
				if (string2 != string.Empty)
				{
					if (UIFriendSystemDirector.mInstance != null)
					{
						UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
					}
					GGNetworkKit.mInstance.JoinRoom(string2);
				}
			}
			if (UILobbySelectDirector.mInstance != null && UITipController.mInstance != null && UITipController.mInstance.TipActiveSelf(UITipController.TipType.LobbyLoadingOneButton))
			{
				UITipController.mInstance.HideTip(UITipController.TipType.LobbyLoadingOneButton);
			}
			if (UILobbySelectEntertainmentDirector.mInstance != null && UITipController.mInstance != null && UITipController.mInstance.TipActiveSelf(UITipController.TipType.LobbyLoadingOneButton))
			{
				UITipController.mInstance.HideTip(UITipController.TipType.LobbyLoadingOneButton);
			}
			break;
		case GGEventType.EventNewRound:
			GGNetworkKit.mInstance.mWaitForSeconds = 0.1f;
			GGNetworkKit.mInstance.mCheckNecessaryDataTime = 2f;
			this.mOpTimeForCheckNessaryData = 0f;
			GameObject.Find("PhotonGame").GetComponent<AudioListener>().enabled = false;
			GGNetworkKit.mInstance.CreatePlayer("ExampleCharacter", new Vector3(0f, -500f, 0f));
			GGNetworkKit.mInstance.RemoveDisconnectFromRoomCoroutine();
			break;
		case GGEventType.EventEndCurrentRound:
		{
			GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().Reset();
			GGModeType gameMode = GGNetworkKit.mInstance.GetGameMode();
			if (gameMode != GGModeType.TeamDeathMatch)
			{
				if (gameMode == GGModeType.KillingCompetition)
				{
					GameObject.Find("PhotonGame").GetComponent<GGNetworkMode2>().Reset();
				}
				else if (gameMode == GGModeType.StrongHold)
				{
					GameObject.Find("PhotonGame").GetComponent<GGNetworkMode3>().Reset();
				}
				else if (gameMode == GGModeType.Explosion)
				{
					GameObject.Find("PhotonGame").GetComponent<GGNetworkMode4>().Reset();
					GGExplosionModeTimerBombLogic.mInstance.Reset();
				}
				else if (gameMode == GGModeType.Mutation)
				{
					GameObject.Find("PhotonGame").GetComponent<GGNetworkMode5>().Reset();
					GGMutationModeControl.mInstance.reset();
				}
				else if (gameMode == GGModeType.KnifeCompetition)
				{
					GameObject.Find("PhotonGame").GetComponent<GGNetworkMode6>().Reset();
				}
			}
			GameObject.Find("PhotonGame").GetComponent<AudioListener>().enabled = true;
			GGNetworkKit.mInstance.DestoryNetworkObject(GGNetworkKit.mInstance.GetMainPlayer());
			break;
		}
		case GGEventType.EventHatReceive:
		{
			RioQerdoDebug.Log("receive other's hat");
			GGNetworkHat ggnetworkHat = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkHat>(data);
			HatNetWorkPackObj hatNetWorkPackObj = HatManager.DeserializeHatDataToPackObj(ggnetworkHat.data);
			int ownerID2 = ggnetworkHat.ownerID;
			if (!hatNetWorkPackObj.isNull)
			{
				hatNetWorkPackObj.mat.name = ggnetworkHat.ownerID.ToString() + "_HatMat";
				GameObject mirrorByPlayerID2 = GGNetworkKit.mInstance.GetMirrorByPlayerID(ownerID2);
				if (mirrorByPlayerID2 != null)
				{
					mirrorByPlayerID2.transform.Find("Player_1_sinkmesh/Player").SendMessage("SetNetworkPlayerHat", hatNetWorkPackObj, SendMessageOptions.RequireReceiver);
				}
			}
			if (ggnetworkHat.needTargetHat)
			{
				RioQerdoDebug.Log("send my hat to new player");
				GGNetworkHat ggnetworkHat2 = new GGNetworkHat();
				ggnetworkHat2.data = HatManager.GetMySettedHatPackData();
				ggnetworkHat2.ownerID = PhotonNetwork.player.ID;
				ggnetworkHat2.needTargetHat = false;
				GGNetworkKit.mInstance.SendHat(ggnetworkHat2, GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(ownerID2));
			}
			break;
		}
		case GGEventType.EventCapeReceive:
		{
			RioQerdoDebug.Log("receive other's cape");
			GGNetworkCape ggnetworkCape = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkCape>(data);
			CapeNetWorkPackObj capeNetWorkPackObj = CapeManager.DeserializeCapeDataToPackObj(ggnetworkCape.data);
			int ownerID3 = ggnetworkCape.ownerID;
			if (!capeNetWorkPackObj.isNull)
			{
				capeNetWorkPackObj.mat.name = ggnetworkCape.ownerID.ToString() + "_CapeMat";
				GameObject mirrorByPlayerID3 = GGNetworkKit.mInstance.GetMirrorByPlayerID(ownerID3);
				if (mirrorByPlayerID3 != null)
				{
					mirrorByPlayerID3.transform.Find("Player_1_sinkmesh/Player").SendMessage("SetNetworkPlayerCape", capeNetWorkPackObj, SendMessageOptions.RequireReceiver);
				}
			}
			if (ggnetworkCape.needTargetCape)
			{
				RioQerdoDebug.Log("send my cape to new player");
				GGNetworkCape ggnetworkCape2 = new GGNetworkCape();
				ggnetworkCape2.data = CapeManager.GetMySettedCapePackData();
				ggnetworkCape2.ownerID = PhotonNetwork.player.ID;
				ggnetworkCape2.needTargetCape = false;
				GGNetworkKit.mInstance.SendCape(ggnetworkCape2, GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(ownerID3));
			}
			break;
		}
		case GGEventType.EventWeaponPropertiesReceive:
		{
			RioQerdoDebug.Log("receive other's weapon's properties");
			GGNetworkWeaponPropertiesList ggnetworkWeaponPropertiesList = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkWeaponPropertiesList>(data);
			int ownerID4 = ggnetworkWeaponPropertiesList.ownerID;
			RioQerdoDebug.Log("weapon: " + ownerID4);
			GameObject mirrorByPlayerID4 = GGNetworkKit.mInstance.GetMirrorByPlayerID(ownerID4);
			if (ggnetworkWeaponPropertiesList.weaponPropertiesList != null)
			{
				RioQerdoDebug.LogError("SetWeaponPlusedAndDeleteUnusedWeapon");
				mirrorByPlayerID4.transform.Find("Player_1_sinkmesh/H/S/C/R0/R1/R2/WeaponManager").SendMessage("SetRemoteWeaponPlused", ggnetworkWeaponPropertiesList, SendMessageOptions.RequireReceiver);
			}
			if (ggnetworkWeaponPropertiesList.needTargetWeaponProperties)
			{
				GGNetworkWeaponPropertiesList ggnetworkWeaponPropertiesList2 = new GGNetworkWeaponPropertiesList();
				ggnetworkWeaponPropertiesList2.weaponPropertiesList = GGWeaponManager.mInstance.GetWeaponProperties();
				ggnetworkWeaponPropertiesList2.ownerID = PhotonNetwork.player.ID;
				ggnetworkWeaponPropertiesList2.needTargetWeaponProperties = false;
				GGNetworkKit.mInstance.SendWeaponProperties(ggnetworkWeaponPropertiesList2, GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(ownerID4));
			}
			break;
		}
		case GGEventType.EventBootReceive:
		{
			RioQerdoDebug.Log("receive other's boot");
			GGNetworkBoot ggnetworkBoot = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkBoot>(data);
			BootNetWorkPackObj bootNetWorkPackObj = BootManager.DeserializeBootDataToPackObj(ggnetworkBoot.data);
			int ownerID5 = ggnetworkBoot.ownerID;
			if (!bootNetWorkPackObj.isNull)
			{
				bootNetWorkPackObj.mat.name = ggnetworkBoot.ownerID.ToString() + "_BootMat";
				GameObject mirrorByPlayerID5 = GGNetworkKit.mInstance.GetMirrorByPlayerID(ownerID5);
				if (mirrorByPlayerID5 != null)
				{
					mirrorByPlayerID5.transform.Find("Player_1_sinkmesh/Player").SendMessage("SetNetworkPlayerBoot", bootNetWorkPackObj, SendMessageOptions.RequireReceiver);
				}
			}
			if (ggnetworkBoot.needTargetBoot)
			{
				RioQerdoDebug.Log("send my boot to new player");
				GGNetworkBoot ggnetworkBoot2 = new GGNetworkBoot();
				ggnetworkBoot2.data = BootManager.GetMySettedBootPackData();
				ggnetworkBoot2.ownerID = PhotonNetwork.player.ID;
				ggnetworkBoot2.needTargetBoot = false;
				GGNetworkKit.mInstance.SendBoot(ggnetworkBoot2, GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(ownerID5));
			}
			break;
		}
		case GGEventType.EventPlayerSeasonInfo:
		{
			GGPlayerSeasonInfo seasonInfo = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGPlayerSeasonInfo>(data);
			if (UIOtherPlayerInfoDirector.mInstance != null)
			{
				UIOtherPlayerInfoDirector.mInstance.RefreshOtherPlayerProfileUI(seasonInfo);
			}
			break;
		}
		default:
			switch (eventType)
			{
			case GGEventType.EventNoConnection:
				RioQerdoDebug.Log("GGEventType.EventNoConnection");
				if (UILobbySelectDirector.mInstance != null && !UITipController.mInstance.TipActiveSelf(UITipController.TipType.LoadingTip) && !UILobbySelectDirector.mInstance.mNoConnection.activeSelf)
				{
					UILobbySelectDirector.mInstance.mNoConnection.SetActive(true);
				}
				if (UILobbySelectEntertainmentDirector.mInstance != null && !UITipController.mInstance.TipActiveSelf(UITipController.TipType.LoadingTip) && !UILobbySelectEntertainmentDirector.mInstance.mNoConnection.activeSelf)
				{
					UILobbySelectEntertainmentDirector.mInstance.mNoConnection.SetActive(true);
				}
				if (UIPauseDirector.mInstance != null)
				{
					UIPauseDirector.mInstance.PushDisconnectPanel();
				}
				if (Application.loadedLevelName == "DisconnectionReJoinScene")
				{
					GameObject gameObject2 = GameObject.FindWithTag("SwitchSceneInfo");
					if (gameObject2 != null)
					{
						UnityEngine.Object.Destroy(gameObject2);
					}
					if (UIDisconnectionDirector.mInstance != null)
					{
						UIDisconnectionDirector.mInstance.PopDisconnectFailPanel();
					}
				}
				if (UIFriendSystemDirector.mInstance != null && UITipController.mInstance.TipActiveSelf(UITipController.TipType.LoadingTip))
				{
					GGNetworkKit.mInstance.mCanJoinFriendRoom = true;
					UITipController.mInstance.HideCurTip();
				}
				break;
			case GGEventType.RoomCreateSameNameRoom:
				if (UILobbySelectDirector.mInstance != null)
				{
					UILobbySelectDirector.mInstance.DisplayGeneralTipsDialog("Room Name Already Exists!", Color.white, "OK");
				}
				if (UILobbySelectEntertainmentDirector.mInstance != null)
				{
					UILobbySelectEntertainmentDirector.mInstance.DisplayGeneralTipsDialog("Room Name Already Exists!", Color.white, "OK");
				}
				break;
			case GGEventType.RoomJoinRoomNotExistOrFull:
				if (UILobbySelectDirector.mInstance != null)
				{
					if (UILobbySelectDirector.mInstance.IsChosenRoomFull())
					{
						UILobbySelectDirector.mInstance.DisplayGeneralTipsDialog("Room Is Full!", Color.white, "OK");
					}
					else
					{
						UILobbySelectDirector.mInstance.DisplayGeneralTipsDialog("Room Is Not Exist!", Color.white, "OK");
					}
				}
				if (UILobbySelectEntertainmentDirector.mInstance != null)
				{
					if (UILobbySelectEntertainmentDirector.mInstance.IsChosenRoomFull())
					{
						UILobbySelectEntertainmentDirector.mInstance.DisplayGeneralTipsDialog("Room Is Full!", Color.white, "OK");
					}
					else
					{
						UILobbySelectEntertainmentDirector.mInstance.DisplayGeneralTipsDialog("Room Is Not Exist!", Color.white, "OK");
					}
				}
				if (UIDisconnectionDirector.mInstance != null)
				{
					UIDisconnectionDirector.mInstance.PopDisconnectFailPanel();
				}
				if (UIFriendSystemDirector.mInstance != null)
				{
					UIFriendSystemDirector.mInstance.JoinroomFailed();
				}
				break;
			case GGEventType.RoomJoinPasswordNotCorrect:
				break;
			default:
				if (eventType != GGEventType.LoadingPanelHide)
				{
					if (eventType == GGEventType.EventPlayerLeave)
					{
						GGNetworkGlobalInfo mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
						if (mGlobalInfo != null && mGlobalInfo.modeInfo.IsStartExplosion && !mGlobalInfo.modeInfo.activeTimerBomb)
						{
							bool flag = true;
							List<GGNetworkPlayerProperties> redPlayerPropertiesList = GGNetworkKit.mInstance.GetRedPlayerPropertiesList();
							for (int i = 0; i < redPlayerPropertiesList.Count; i++)
							{
								if (redPlayerPropertiesList[i].isTakeTimerBomb)
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								GGNetworkKit.mInstance.CreateSeceneObject("ExplosionModeTimerBombDroped", new Vector3(mGlobalInfo.modeInfo.TimerBombPositionX, mGlobalInfo.modeInfo.TimerBombPositionY, mGlobalInfo.modeInfo.TimerBombPositionZ), Quaternion.identity);
							}
						}
					}
				}
				else
				{
					if (UIFriendSystemDirector.mInstance != null && UITipController.mInstance.TipActiveSelf(UITipController.TipType.LoadingTip))
					{
						UITipController.mInstance.HideCurTip();
					}
					if (UILobbySelectDirector.mInstance != null)
					{
						UITipController.mInstance.HideCurTip();
						EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServerInInteraction");
						UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
					}
					if (UILobbySelectEntertainmentDirector.mInstance != null)
					{
						UITipController.mInstance.HideCurTip();
						EventDelegate btnEventName2 = new EventDelegate(this, "HideConnectingToServerInInteraction");
						UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName2, null, null);
					}
				}
				break;
			case GGEventType.EventDisconnectedToConnected:
				RioQerdoDebug.Log("EventDisconnectedToConnected");
				if (UILobbySelectDirector.mInstance != null)
				{
					if (UITipController.mInstance.TipActiveSelf(UITipController.TipType.LobbyLoadingOneButton))
					{
						UITipController.mInstance.HideTip(UITipController.TipType.LobbyLoadingOneButton);
						if (UILobbySelectDirector.mInstance != null)
						{
							RioQerdoDebug.Log("UILobbySelectDirector.mInstance.LobbySelectStopAllCoroutines");
							UILobbySelectDirector.mInstance.LobbySelectStopAllCoroutines();
						}
					}
					if (UILobbySelectDirector.mInstance.mNoConnection.activeSelf)
					{
						UILobbySelectDirector.mInstance.mNoConnection.SetActive(false);
					}
				}
				if (UILobbySelectEntertainmentDirector.mInstance != null)
				{
					if (UITipController.mInstance.TipActiveSelf(UITipController.TipType.LobbyLoadingOneButton))
					{
						UITipController.mInstance.HideTip(UITipController.TipType.LobbyLoadingOneButton);
						if (UILobbySelectEntertainmentDirector.mInstance != null)
						{
							RioQerdoDebug.Log("UILobbySelectEntertainmentDirector.mInstance.LobbySelectStopAllCoroutines");
							UILobbySelectEntertainmentDirector.mInstance.LobbySelectStopAllCoroutines();
						}
					}
					if (UILobbySelectEntertainmentDirector.mInstance.mNoConnection.activeSelf)
					{
						UILobbySelectEntertainmentDirector.mInstance.mNoConnection.SetActive(false);
					}
				}
				break;
			case GGEventType.RandomJoinRoomNotAvailableRoom:
				if (UILobbySelectDirector.mInstance != null)
				{
					UILobbySelectDirector.mInstance.DisplayGeneralTipsDialog("There is no available room！", Color.white, "OK");
				}
				if (UILobbySelectEntertainmentDirector.mInstance != null)
				{
					UILobbySelectEntertainmentDirector.mInstance.DisplayGeneralTipsDialog("There is no available room！", Color.white, "OK");
				}
				break;
			case GGEventType.FirstJoinDynamicSwitchPlayerTeam:
				if (UIPauseDirector.mInstance != null && UIPauseDirector.mInstance.pauseNode.activeSelf)
				{
					UIPauseDirector.mInstance.ShowLobbyNode();
				}
				break;
			case GGEventType.RoomToLobbyWhenNotClickNewRound:
				Application.LoadLevel("UILobby");
				break;
			case GGEventType.RoomCreatedSuccess:
				GGNetworkKit.mInstance.SetPlayerName(UIUserDataController.GetDefaultRoleName());
				GGNetworkKit.mInstance.CustomPlayerProperties(UIUserDataController.GetDefaultRoleName(), GrowthManagerKit.GetCharacterLevel());
				break;
			}
			break;
		}
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x001052B0 File Offset: 0x001036B0
	public void HideConnectingToServerInInteraction()
	{
		if (UILobbySelectDirector.mInstance != null)
		{
			UILobbySelectDirector.mInstance.LobbySelectStopAllCoroutines();
		}
		if (UILobbySelectEntertainmentDirector.mInstance != null)
		{
			UILobbySelectEntertainmentDirector.mInstance.LobbySelectStopAllCoroutines();
		}
		if (UITipController.mInstance != null)
		{
			UITipController.mInstance.HideCurTip();
		}
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x0010530C File Offset: 0x0010370C
	private IEnumerator DelayGetNessaryResource()
	{
		yield return new WaitForSeconds(6f);
		GGNetworkKit.mInstance.SendNecessaryResourceNewRound();
		yield break;
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x00105320 File Offset: 0x00103720
	private void Update()
	{
		this.mOpTimeForCheckNessaryData += Time.deltaTime;
		if (this.mOpTimeForCheckNessaryData > GGNetworkKit.mInstance.mCheckNecessaryDataTime)
		{
			GGNetworkKit.mInstance.mCheckNecessaryDataTime = 5f;
			this.mOpTimeForCheckNessaryData = 0f;
			this.CheckNessaryData();
		}
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x00105374 File Offset: 0x00103774
	private void SendNessaryData(int playerID)
	{
		PhotonPlayer photonPlayerByPlayerID = GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(playerID);
		GGNetworkSkin ggnetworkSkin = new GGNetworkSkin();
		ggnetworkSkin.data = SkinManager.GetMySettedSkinTexData();
		ggnetworkSkin.ownerID = PhotonNetwork.player.ID;
		ggnetworkSkin.needTargetSkin = false;
		GGNetworkKit.mInstance.SendSkin(ggnetworkSkin, photonPlayerByPlayerID);
		GGNetworkHat ggnetworkHat = new GGNetworkHat();
		ggnetworkHat.data = HatManager.GetMySettedHatPackData();
		ggnetworkHat.ownerID = PhotonNetwork.player.ID;
		ggnetworkHat.needTargetHat = false;
		GGNetworkKit.mInstance.SendHat(ggnetworkHat, photonPlayerByPlayerID);
		GGNetworkCape ggnetworkCape = new GGNetworkCape();
		ggnetworkCape.data = CapeManager.GetMySettedCapePackData();
		ggnetworkCape.ownerID = PhotonNetwork.player.ID;
		ggnetworkCape.needTargetCape = false;
		GGNetworkKit.mInstance.SendCape(ggnetworkCape, photonPlayerByPlayerID);
		GGNetworkWeaponPropertiesList ggnetworkWeaponPropertiesList = new GGNetworkWeaponPropertiesList();
		if (GGNetworkKit.mInstance.GetMainPlayer() != null)
		{
			ggnetworkWeaponPropertiesList.weaponPropertiesList = GGNetworkKit.mInstance.GetMainPlayer().transform.Find("LookObject/Main Camera/Weapon Camera/WeaponManager").GetComponent<GGWeaponManager>().GetWeaponProperties();
			ggnetworkWeaponPropertiesList.ownerID = PhotonNetwork.player.ID;
			ggnetworkWeaponPropertiesList.needTargetWeaponProperties = false;
			GGNetworkKit.mInstance.SendWeaponProperties(ggnetworkWeaponPropertiesList, photonPlayerByPlayerID);
		}
		GGNetworkBoot ggnetworkBoot = new GGNetworkBoot();
		ggnetworkBoot.data = BootManager.GetMySettedBootPackData();
		ggnetworkBoot.ownerID = PhotonNetwork.player.ID;
		ggnetworkBoot.needTargetBoot = false;
		GGNetworkKit.mInstance.SendBoot(ggnetworkBoot, photonPlayerByPlayerID);
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x001054D0 File Offset: 0x001038D0
	private void CheckNessaryData()
	{
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			PhotonView component = value.GetComponent<PhotonView>();
			RioQerdoDebug.Log("My view id + " + component.viewID);
			if (!component.isMine)
			{
				string name = value.transform.Find("Player_1_sinkmesh/Player").GetComponent<Renderer>().material.name;
				if (!name.Contains("_SkinMat"))
				{
					GGMessage ggmessage = new GGMessage();
					ggmessage.messageType = GGMessageType.MessageNotifyAllNecessaryData;
					ggmessage.messageContent = new GGMessageContent();
					ggmessage.messageContent.ID = PhotonNetwork.player.ID;
					RioQerdoDebug.Log("Johnny + " + component.ownerId);
					GGNetworkKit.mInstance.SendMessage(ggmessage, component.ownerId);
				}
			}
		}
	}

	// Token: 0x04002390 RID: 9104
	private GameObject examplecharacter;

	// Token: 0x04002391 RID: 9105
	public GameObject goSwitchSceneInfo;

	// Token: 0x04002392 RID: 9106
	private float mOpTimeForCheckNessaryData;
}
