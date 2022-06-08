using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon;
using RioLog;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class GGNetworkAdapter : Photon.MonoBehaviour
{
	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06002390 RID: 9104 RVA: 0x001108A3 File Offset: 0x0010ECA3
	// (set) Token: 0x06002391 RID: 9105 RVA: 0x001108AB File Offset: 0x0010ECAB
	public bool IsNeedDisconnecting
	{
		get
		{
			return this.isNeedDisconnecting;
		}
		set
		{
			this.isNeedDisconnecting = value;
		}
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x001108B4 File Offset: 0x0010ECB4
	private void OnDisable()
	{
		if (this.IsNeedDisconnecting)
		{
			RioQerdoDebug.Log("OnDisable->Disconnect");
			if (PhotonNetwork.connected)
			{
				PhotonNetwork.Disconnect();
			}
		}
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x001108DA File Offset: 0x0010ECDA
	public void DisconnectPhotonNetwork()
	{
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
		}
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x001108EB File Offset: 0x0010ECEB
	private void Awake()
	{
		GGNetworkAdapter.mInstance = this;
		GGNetworkAdapter.mScenePhotonView = base.GetComponent<PhotonView>();
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x001108FE File Offset: 0x0010ECFE
	private void Start()
	{
		this.initTest();
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06002396 RID: 9110 RVA: 0x00110906 File Offset: 0x0010ED06
	public bool isMasterClient
	{
		get
		{
			return PhotonNetwork.isMasterClient;
		}
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x0011090D File Offset: 0x0010ED0D
	public bool ViewIsMasterClient(PhotonView view)
	{
		return view.owner.isMasterClient;
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x0011091A File Offset: 0x0010ED1A
	public void ConnectToNetwork(string gameVersion)
	{
		PhotonNetwork.ConnectUsingSettings(gameVersion);
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x00110923 File Offset: 0x0010ED23
	public void ConnectToBestCloudServer(string gameVersion)
	{
		PhotonNetwork.ConnectToBestCloudServer(gameVersion);
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x0011092C File Offset: 0x0010ED2C
	public GGServerRegion GetClosestRegion()
	{
		switch (PhotonHandler.BestRegionCodeInPreferences)
		{
		case CloudRegionCode.eu:
			return GGServerRegion.EU;
		case CloudRegionCode.us:
			return GGServerRegion.US;
		case CloudRegionCode.asia:
			return GGServerRegion.ASIA;
		case CloudRegionCode.jp:
			return GGServerRegion.JP;
		case CloudRegionCode.au:
			return GGServerRegion.AU;
		}
		return GGServerRegion.US;
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x0600239B RID: 9115 RVA: 0x0011098F File Offset: 0x0010ED8F
	public int MasterClientID
	{
		get
		{
			return PhotonNetwork.masterClient.ID;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x0600239C RID: 9116 RVA: 0x0011099B File Offset: 0x0010ED9B
	public bool IsMine
	{
		get
		{
			return base.photonView.isMine;
		}
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x001109A8 File Offset: 0x0010EDA8
	public void CreateRoom(string roomName, int maxPlayers, string mapName, string sceneName, GGModeType mode, bool encryption, string password, GGPlayModeType playMode, int huntingDifficulty)
	{
		RioQerdoDebug.Log("CreateRoom");
		string[] customRoomPropertiesForLobby = new string[]
		{
			"roomname",
			"map",
			"scene",
			"mode",
			"encryption",
			"password",
			"playmode",
			"huntingdifficulty"
		};
		Hashtable hashtable = new Hashtable();
		hashtable.Add("roomname", roomName);
		hashtable.Add("map", mapName);
		hashtable.Add("scene", sceneName);
		hashtable.Add("mode", mode);
		hashtable.Add("encryption", encryption);
		hashtable.Add("password", password);
		hashtable.Add("playmode", playMode);
		hashtable.Add("huntingdifficulty", huntingDifficulty);
		PhotonNetwork.CreateRoom(roomName, new RoomOptions
		{
			maxPlayers = (byte)maxPlayers,
			isVisible = true,
			isOpen = true,
			customRoomProperties = hashtable,
			customRoomPropertiesForLobby = customRoomPropertiesForLobby
		}, null);
		if (!GGNetworkKit.mInstance.bExcuteJoinRoomTimeOut)
		{
			GGNetworkKit.mInstance.bExcuteJoinRoomTimeOut = true;
			base.StartCoroutine(GGNetworkKit.mInstance.JoinRoomTimeOut());
		}
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x00110AE4 File Offset: 0x0010EEE4
	public void JoinRoom(string roomName)
	{
		RioQerdoDebug.Log("JoinRoom: " + roomName);
		PhotonNetwork.JoinRoom(roomName);
		if (!GGNetworkKit.mInstance.bExcuteJoinRoomTimeOut)
		{
			GGNetworkKit.mInstance.bExcuteJoinRoomTimeOut = true;
			base.StartCoroutine(GGNetworkKit.mInstance.JoinRoomTimeOut());
		}
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x00110B34 File Offset: 0x0010EF34
	public void JoinRandomRoom(GGPlayModeType playMode, GGModeType mode, bool canJoinHuntingMode)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("encryption", false);
		hashtable.Add("playmode", playMode);
		if (playMode == GGPlayModeType.Sport)
		{
			if (mode != GGModeType.Other)
			{
				hashtable.Add("mode", mode);
			}
		}
		else if (playMode == GGPlayModeType.Entertainment)
		{
			if (mode == GGModeType.Other)
			{
				if (!canJoinHuntingMode)
				{
					int index = UnityEngine.Random.Range(0, this.mEntertainmentModeIndexList.Count);
					GGModeType ggmodeType = (GGModeType)this.mEntertainmentModeIndexList[index];
					hashtable.Add("mode", ggmodeType);
				}
			}
			else if (mode == GGModeType.Hunting)
			{
				if (!canJoinHuntingMode)
				{
					UIDialogDirector.mInstance.DisplayNeedHuntingTicketDialog();
					return;
				}
				hashtable.Add("mode", mode);
			}
			else
			{
				hashtable.Add("mode", mode);
			}
		}
		PhotonNetwork.JoinRandomRoom(hashtable, 0);
		if (!GGNetworkKit.mInstance.bExcuteJoinRoomTimeOut)
		{
			GGNetworkKit.mInstance.bExcuteJoinRoomTimeOut = true;
			base.StartCoroutine(GGNetworkKit.mInstance.JoinRoomTimeOut());
		}
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x00110C59 File Offset: 0x0010F059
	public void JoinFriendRoom(string roomName, GGServerRegion region)
	{
		PlayerPrefs.SetString("GGNetworkJoinFriendRoomName", roomName);
		this.SwitchServer(region);
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x00110C6D File Offset: 0x0010F06D
	public GGModeType GetGameMode()
	{
		if (PhotonNetwork.room != null)
		{
			return (GGModeType)PhotonNetwork.room.customProperties["mode"];
		}
		return GGModeType.Other;
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x00110C98 File Offset: 0x0010F098
	public GGPlayModeType GetPlayMode()
	{
		if (PhotonNetwork.room != null)
		{
			return (GGPlayModeType)PhotonNetwork.room.customProperties["playmode"];
		}
		return GGPlayModeType.Other;
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x00110CC3 File Offset: 0x0010F0C3
	public bool GetRoomEncryption()
	{
		return (bool)PhotonNetwork.room.customProperties["encryption"];
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x00110CE0 File Offset: 0x0010F0E0
	public string GetRoomIsEncryption()
	{
		return ((bool)PhotonNetwork.room.customProperties["encryption"]).ToString();
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x00110D14 File Offset: 0x0010F114
	public string GetEncryptionRoomPassword()
	{
		return (string)PhotonNetwork.room.customProperties["password"];
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x00110D2F File Offset: 0x0010F12F
	public string GetMapName()
	{
		return (string)PhotonNetwork.room.customProperties["map"];
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x00110D4A File Offset: 0x0010F14A
	public string GetSceneName()
	{
		return (string)PhotonNetwork.room.customProperties["scene"];
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x00110D65 File Offset: 0x0010F165
	public string GetRoomName()
	{
		if (PhotonNetwork.room != null)
		{
			return (string)PhotonNetwork.room.customProperties["roomname"];
		}
		return string.Empty;
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x00110D90 File Offset: 0x0010F190
	public int GetMaxPlayers()
	{
		return PhotonNetwork.room.maxPlayers;
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x00110D9C File Offset: 0x0010F19C
	public int GetHuntingDifficulty()
	{
		if (PhotonNetwork.room != null)
		{
			return (int)PhotonNetwork.room.customProperties["huntingdifficulty"];
		}
		return 0;
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x00110DC3 File Offset: 0x0010F1C3
	public int GetPing()
	{
		return PhotonNetwork.GetPing();
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x00110DCC File Offset: 0x0010F1CC
	public void CreatePlayer(string prefabName, Vector3 position)
	{
		RioQerdoDebug.Log("CreatePlayer.....111111111");
		PhotonNetwork.Instantiate(prefabName, position, Quaternion.identity, 0).tag = "Player";
		RioQerdoDebug.Log("CreatePlayer.....222222222");
		this.mMainPlayer = GameObject.FindWithTag("Player");
		RioQerdoDebug.Log("CreatePlayer.....33333333333");
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x00110E1E File Offset: 0x0010F21E
	public void SetPlayerName(string playerName)
	{
		PhotonNetwork.playerName = playerName;
	}

	// Token: 0x060023AE RID: 9134 RVA: 0x00110E28 File Offset: 0x0010F228
	public void CustomPlayerProperties(string name, int rank)
	{
		string[] array = new string[]
		{
			"name",
			"rank"
		};
		Hashtable hashtable = new Hashtable();
		hashtable.Add("name", name);
		hashtable.Add("rank", (short)rank);
		PhotonNetwork.player.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x060023AF RID: 9135 RVA: 0x00110E80 File Offset: 0x0010F280
	public void SendMessage(GGMessage message, GGTarget target)
	{
		if (target != GGTarget.MasterClient)
		{
			if (target != GGTarget.All)
			{
				if (target == GGTarget.Others)
				{
					GGNetworkAdapter.mScenePhotonView.RPC("SendedMessage", PhotonTargets.Others, new object[]
					{
						GGNetworkObjectSerialize.mInstance.SerializeBinary<GGMessage>(message)
					});
				}
			}
			else
			{
				GGNetworkAdapter.mScenePhotonView.RPC("SendedMessage", PhotonTargets.All, new object[]
				{
					GGNetworkObjectSerialize.mInstance.SerializeBinary<GGMessage>(message)
				});
			}
		}
		else
		{
			GGNetworkAdapter.mScenePhotonView.RPC("SendedMessage", PhotonTargets.MasterClient, new object[]
			{
				GGNetworkObjectSerialize.mInstance.SerializeBinary<GGMessage>(message)
			});
		}
	}

	// Token: 0x060023B0 RID: 9136 RVA: 0x00110F21 File Offset: 0x0010F321
	public void SendMessage(GGMessage message, int ActorID)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedMessage", PhotonPlayer.Find(ActorID), new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGMessage>(message)
		});
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x00110F4C File Offset: 0x0010F34C
	public void SendMessage(GGMessage message, PhotonPlayer player)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedMessage", player, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGMessage>(message)
		});
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x00110F72 File Offset: 0x0010F372
	public int GetAllPageNum()
	{
		return this.maxpage;
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x00110F7C File Offset: 0x0010F37C
	private int GetCorrectPage(int page, int length)
	{
		int num = length / GGNetworkAdapter.MAXROOMNUMPERPAGE;
		int num2 = length % GGNetworkAdapter.MAXROOMNUMPERPAGE;
		this.maxpage = ((num2 <= 0) ? num : (num + 1));
		if (page > this.maxpage)
		{
			return this.maxpage;
		}
		if (page < 1)
		{
			return 1;
		}
		return page;
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x00110FCC File Offset: 0x0010F3CC
	private void initTest()
	{
		for (int i = 0; i < 75; i++)
		{
			if (i <= 20)
			{
				RoomInfo roomInfo = new RoomInfo("room" + i, null);
				roomInfo.customProperties["mode"] = GGModeType.StrongHold;
				roomInfo.customProperties["encryption"] = false;
				roomInfo.customProperties["password"] = string.Empty;
				roomInfo.customProperties["roomname"] = "room" + i;
				roomInfo.customProperties["map"] = "1";
				this.TmpConditionRoomlist.Add(roomInfo);
			}
			else if (i <= 40)
			{
				RoomInfo roomInfo2 = new RoomInfo("room" + i, null);
				roomInfo2.customProperties["mode"] = GGModeType.TeamDeathMatch;
				roomInfo2.customProperties["encryption"] = true;
				roomInfo2.customProperties["password"] = string.Empty;
				roomInfo2.customProperties["roomname"] = "room" + i;
				roomInfo2.customProperties["map"] = "1";
				this.TmpConditionRoomlist.Add(roomInfo2);
			}
			else if (i <= 60)
			{
				RoomInfo roomInfo3 = new RoomInfo("room" + i, null);
				roomInfo3.customProperties["mode"] = GGModeType.KillingCompetition;
				roomInfo3.customProperties["encryption"] = false;
				roomInfo3.customProperties["password"] = string.Empty;
				roomInfo3.customProperties["roomname"] = "room" + i;
				roomInfo3.customProperties["map"] = "1";
				this.TmpConditionRoomlist.Add(roomInfo3);
			}
			else if (i <= 80)
			{
				RoomInfo roomInfo4 = new RoomInfo("room" + i, null);
				roomInfo4.customProperties["mode"] = GGModeType.StrongHold;
				roomInfo4.customProperties["encryption"] = true;
				roomInfo4.customProperties["password"] = string.Empty;
				roomInfo4.customProperties["roomname"] = "room" + i;
				roomInfo4.customProperties["map"] = "1";
				this.TmpConditionRoomlist.Add(roomInfo4);
			}
		}
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x00111286 File Offset: 0x0010F686
	public void SetCurPage(int page)
	{
		this.curPage = page;
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x0011128F File Offset: 0x0010F68F
	public int GetCurPage()
	{
		return this.curPage;
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x00111298 File Offset: 0x0010F698
	public bool GetRoomList(GGRoomListOperationType opType, GGRoomFilter filter, out List<RoomInfo> resultList)
	{
		int num = this.curPage;
		int num2 = this.curPage;
		bool result = true;
		switch (opType)
		{
		case GGRoomListOperationType.Up:
			num2--;
			break;
		case GGRoomListOperationType.Down:
			num2++;
			break;
		}
		List<RoomInfo> list = new List<RoomInfo>();
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			if (this.IsRoomPassFilter(roomInfo, filter))
			{
				list.Add(roomInfo);
			}
		}
		try
		{
			this.curPage = this.GetCorrectPage(num2, list.Count);
			if (this.curPage == num)
			{
				result = false;
			}
			if (list.Count == 0)
			{
				resultList = list;
			}
			else
			{
				int count = list.Count;
				int num3 = count - (this.curPage - 1) * GGNetworkAdapter.MAXROOMNUMPERPAGE;
				num3 = ((num3 < GGNetworkAdapter.MAXROOMNUMPERPAGE) ? num3 : GGNetworkAdapter.MAXROOMNUMPERPAGE);
				resultList = list.GetRange((this.curPage - 1) * GGNetworkAdapter.MAXROOMNUMPERPAGE, num3);
			}
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log("!@#$%^&**(())_+!@@##~@@#$%^&*!@#$%^&*!@#$%^&*");
			RoomInfo[] roomList2 = PhotonNetwork.GetRoomList();
			resultList = new List<RoomInfo>();
			for (int j = 0; j < roomList2.Length; j++)
			{
				resultList.Add(roomList2[j]);
			}
		}
		return result;
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x00111408 File Offset: 0x0010F808
	public bool IsRoomPassFilter(RoomInfo info, GGRoomFilter filter)
	{
		if (!(filter.word == string.Empty))
		{
			return info.name.ToUpper().Contains(filter.word.ToUpper());
		}
		if ((GGModeType)info.customProperties["mode"] != filter.mode && filter.mode != GGModeType.Other)
		{
			return false;
		}
		if ((GGPlayModeType)info.customProperties["playmode"] != filter.playMode)
		{
			return false;
		}
		switch (filter.playerRange)
		{
		case GGPlayersDisplayInterval.Interval1:
			if (info.maxPlayers < 2 || info.maxPlayers > 6)
			{
				return false;
			}
			break;
		case GGPlayersDisplayInterval.Interval2:
			if (info.maxPlayers < 7 || info.maxPlayers > 10)
			{
				return false;
			}
			break;
		case GGPlayersDisplayInterval.Interval3:
			if (info.maxPlayers < 11 || info.maxPlayers > 14)
			{
				return false;
			}
			break;
		case GGPlayersDisplayInterval.Interval4:
			if (info.maxPlayers < 15 || info.maxPlayers > 20)
			{
				return false;
			}
			break;
		}
		return true;
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x00111550 File Offset: 0x0010F950
	public RoomInfo[] SearchRoomByMaxplayersMode(int page, GGPlayersDisplayInterval interval, GGModeType mode)
	{
		List<RoomInfo> list = new List<RoomInfo>();
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			int maxPlayers = (int)roomInfo.maxPlayers;
			if (mode == GGModeType.Other)
			{
				switch (interval)
				{
				case GGPlayersDisplayInterval.All:
					list.Add(roomInfo);
					break;
				case GGPlayersDisplayInterval.Interval1:
					if (maxPlayers >= 2 && maxPlayers <= 6)
					{
						list.Add(roomInfo);
					}
					break;
				case GGPlayersDisplayInterval.Interval2:
					if (maxPlayers >= 7 && maxPlayers <= 10)
					{
						list.Add(roomInfo);
					}
					break;
				case GGPlayersDisplayInterval.Interval3:
					if (maxPlayers >= 11 && maxPlayers <= 14)
					{
						list.Add(roomInfo);
					}
					break;
				case GGPlayersDisplayInterval.Interval4:
					if (maxPlayers >= 15 && maxPlayers <= 20)
					{
						list.Add(roomInfo);
					}
					break;
				}
			}
			else if ((GGModeType)roomInfo.customProperties["mode"] == mode)
			{
				switch (interval)
				{
				case GGPlayersDisplayInterval.All:
					list.Add(roomInfo);
					break;
				case GGPlayersDisplayInterval.Interval1:
					if (maxPlayers >= 2 && maxPlayers <= 6)
					{
						list.Add(roomInfo);
					}
					break;
				case GGPlayersDisplayInterval.Interval2:
					if (maxPlayers >= 7 && maxPlayers <= 10)
					{
						list.Add(roomInfo);
					}
					break;
				case GGPlayersDisplayInterval.Interval3:
					if (maxPlayers >= 11 && maxPlayers <= 14)
					{
						list.Add(roomInfo);
					}
					break;
				case GGPlayersDisplayInterval.Interval4:
					if (maxPlayers >= 15 && maxPlayers <= 20)
					{
						list.Add(roomInfo);
					}
					break;
				}
			}
		}
		RoomInfo[] result;
		try
		{
			page = this.GetCorrectPage(page, list.Count);
			if (list.Count == 0)
			{
				result = PhotonNetwork.GetRoomList();
			}
			else
			{
				int count = list.Count;
				int num = count - (page - 1) * 10;
				num = ((num < GGNetworkAdapter.MAXROOMNUMPERPAGE) ? num : GGNetworkAdapter.MAXROOMNUMPERPAGE);
				result = list.GetRange((page - 1) * 10, num).ToArray();
			}
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log("!@#$%^&**(())_+!@@##~@@#$%^&*!@#$%^&*!@#$%^&*");
			result = PhotonNetwork.GetRoomList();
		}
		return result;
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x0011178C File Offset: 0x0010FB8C
	public RoomInfo[] SearchRoomByName(int page, string condition)
	{
		List<RoomInfo> list = new List<RoomInfo>();
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			if (roomInfo.name.Contains(condition))
			{
				list.Add(roomInfo);
			}
		}
		RoomInfo[] result;
		try
		{
			page = this.GetCorrectPage(page, list.Count);
			if (list.Count == 0)
			{
				result = PhotonNetwork.GetRoomList();
			}
			else
			{
				int count = list.Count;
				int num = count - (page - 1) * 10;
				num = ((num < GGNetworkAdapter.MAXROOMNUMPERPAGE) ? num : GGNetworkAdapter.MAXROOMNUMPERPAGE);
				result = list.GetRange((page - 1) * 10, num).ToArray();
			}
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log("!@#$%^&**(())_+!@@##~@@#$%^&*!@#$%^&*!@#$%^&*");
			result = PhotonNetwork.GetRoomList();
		}
		return result;
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x0011186C File Offset: 0x0010FC6C
	public void CreateNetworkObject(string objectName, Vector3 position, Quaternion rotation)
	{
		PhotonNetwork.Instantiate(objectName, position, rotation, 0);
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x00111878 File Offset: 0x0010FC78
	public void CreateSeceneObject(string objectName, Vector3 position, Quaternion rotation)
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.InstantiateSceneObject(objectName, position, rotation, 0, null);
		}
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x0011188F File Offset: 0x0010FC8F
	public void CreateSeceneObject(string objectName, Vector3 position, Quaternion rotation, object[] propData)
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.InstantiateSceneObject(objectName, position, rotation, 0, propData);
		}
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x001118A7 File Offset: 0x0010FCA7
	public void DestoryNetworkObject(GameObject go)
	{
		if (go != null)
		{
			PhotonNetwork.Destroy(go);
		}
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x001118BC File Offset: 0x0010FCBC
	public void DestroySceneObjectRPC(GameObject TodestoryGameobject)
	{
		GGPropsEventArgs ggpropsEventArgs = new GGPropsEventArgs();
		ggpropsEventArgs.propsViewID = TodestoryGameobject.GetPhotonView().viewID;
		GGNetworkAdapter.mScenePhotonView.RPC("DestroyedSceneObjectRPC", PhotonTargets.MasterClient, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGPropsEventArgs>(ggpropsEventArgs)
		});
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x00111904 File Offset: 0x0010FD04
	[PunRPC]
	private void DestroyedSceneObjectRPC(byte[] byteArrayPropsEventArgs)
	{
		GGPropsEventArgs ggpropsEventArgs = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGPropsEventArgs>(byteArrayPropsEventArgs);
		if (PhotonView.Find(ggpropsEventArgs.propsViewID) != null)
		{
			GameObject gameObject = PhotonView.Find(ggpropsEventArgs.propsViewID).gameObject;
			if (gameObject != null)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x00111958 File Offset: 0x0010FD58
	public void DestroySceneObjectMutex(GameObject TodestoryGameobject)
	{
		GGPropsEventArgs ggpropsEventArgs = new GGPropsEventArgs();
		ggpropsEventArgs.propsTag = TodestoryGameobject.tag;
		ggpropsEventArgs.propsViewID = TodestoryGameobject.GetPhotonView().viewID;
		ggpropsEventArgs.playerViewID = GGNetworkKit.mInstance.GetMainPlayer().GetPhotonView().viewID;
		GGNetworkAdapter.mScenePhotonView.RPC("DestroyedSceneObject", PhotonTargets.MasterClient, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGPropsEventArgs>(ggpropsEventArgs)
		});
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x001119C6 File Offset: 0x0010FDC6
	public void DestroySceneObject(GameObject TodestoryGameobject)
	{
		if (TodestoryGameobject != null)
		{
			PhotonNetwork.Destroy(TodestoryGameobject);
		}
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x001119DC File Offset: 0x0010FDDC
	[PunRPC]
	public void DestroyedSceneObject(byte[] byteArrayPropsEventArgs, PhotonMessageInfo mi)
	{
		object obj = this.mLockObject;
		lock (obj)
		{
			GGPropsEventArgs ggpropsEventArgs = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGPropsEventArgs>(byteArrayPropsEventArgs);
			GameObject gameObject = PhotonView.Find(ggpropsEventArgs.propsViewID).gameObject;
			if (gameObject != null && PhotonNetwork.isMasterClient)
			{
				PhotonNetwork.Destroy(gameObject);
				if (mi.sender != null)
				{
					GGNetworkAdapter.mScenePhotonView.RPC("PropsRewarded", mi.sender, new object[]
					{
						byteArrayPropsEventArgs
					});
				}
			}
		}
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x00111A78 File Offset: 0x0010FE78
	public void DamageToPlayer(GGDamageEventArgs damageEventArgs, PhotonView photonview)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("DamageedToPlayer", photonview.owner, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGDamageEventArgs>(damageEventArgs)
		});
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x00111AA3 File Offset: 0x0010FEA3
	public void DamageToAI(GGDamageEventArgs damageEventArgs, PhotonView photonview)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("DamageedToAI", PhotonTargets.MasterClient, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGDamageEventArgs>(damageEventArgs)
		});
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x00111AC9 File Offset: 0x0010FEC9
	public void HandOutModeResult(GGModeEventArgs modeEventArgs)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("HandOutModeedResult", PhotonTargets.Others, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGModeEventArgs>(modeEventArgs)
		});
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x00111AEF File Offset: 0x0010FEEF
	public void SendCape(GGNetworkCape cape, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedCape", targetPlayer, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkCape>(cape)
		});
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x00111B15 File Offset: 0x0010FF15
	public void SendBoot(GGNetworkBoot boot, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedBoot", targetPlayer, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkBoot>(boot)
		});
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x00111B3B File Offset: 0x0010FF3B
	public void SendWeaponProperties(GGNetworkWeaponPropertiesList weaponPropertiesList, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedWeaponProperties", targetPlayer, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkWeaponPropertiesList>(weaponPropertiesList)
		});
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x00111B61 File Offset: 0x0010FF61
	public void SendHat(GGNetworkHat hat, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedHat", targetPlayer, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkHat>(hat)
		});
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x00111B87 File Offset: 0x0010FF87
	public void SendSkin(GGNetworkSkin skin, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedSkin", targetPlayer, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkSkin>(skin)
		});
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x00111BAD File Offset: 0x0010FFAD
	public void SendPlayerSeasonInfo(GGPlayerSeasonInfo seasoninfo, PhotonPlayer targetPlayer)
	{
		GGNetworkAdapter.mScenePhotonView.RPC("SendedPlayerSeasonInfo", targetPlayer, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGPlayerSeasonInfo>(seasoninfo)
		});
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x00111BD4 File Offset: 0x0010FFD4
	public void SendNecessaryResourceNewRound()
	{
		this.SendMessage(new GGMessage
		{
			messageType = GGMessageType.MessageNotifySkin,
			messageContent = new GGMessageContent(),
			messageContent = 
			{
				ID = 2
			}
		}, PhotonNetwork.player);
		this.SendMessage(new GGMessage
		{
			messageType = GGMessageType.MessageNotifyHat,
			messageContent = new GGMessageContent(),
			messageContent = 
			{
				ID = 2
			}
		}, PhotonNetwork.player);
		this.SendMessage(new GGMessage
		{
			messageType = GGMessageType.MessageNotifyCape,
			messageContent = new GGMessageContent(),
			messageContent = 
			{
				ID = 2
			}
		}, PhotonNetwork.player);
		this.SendMessage(new GGMessage
		{
			messageType = GGMessageType.MessageNotifyWeaponProperties,
			messageContent = new GGMessageContent(),
			messageContent = 
			{
				ID = 2
			}
		}, PhotonNetwork.player);
		this.SendMessage(new GGMessage
		{
			messageType = GGMessageType.MessageNotifyBoot,
			messageContent = new GGMessageContent(),
			messageContent = 
			{
				ID = 2
			}
		}, PhotonNetwork.player);
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x00111CDC File Offset: 0x001100DC
	public GameObject GetMirrorByPlayerID(int playerID)
	{
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			PhotonView component = value.GetComponent<PhotonView>();
			if (playerID == component.ownerId)
			{
				return value;
			}
		}
		return null;
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x00111D60 File Offset: 0x00110160
	public PhotonPlayer GetPhotonPlayerByPlayerID(int playerID)
	{
		return PhotonPlayer.Find(playerID);
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x00111D68 File Offset: 0x00110168
	public Room GetCurrentRoom()
	{
		return PhotonNetwork.room;
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x00111D6F File Offset: 0x0011016F
	public void KickPlayer(PhotonPlayer photonkickplayer)
	{
		if (this.isMasterClient && !photonkickplayer.isLocal)
		{
			GGNetworkAdapter.mScenePhotonView.RPC("KickedPlayer", photonkickplayer, null);
			PhotonNetwork.CloseConnection(photonkickplayer);
		}
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x00111D9F File Offset: 0x0011019F
	[PunRPC]
	public void KickedPlayer()
	{
		base.SendMessage("KickedMe", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060023D3 RID: 9171 RVA: 0x00111DAD File Offset: 0x001101AD
	public PhotonPlayer[] GetPlayerList()
	{
		return PhotonNetwork.playerList;
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x00111DB4 File Offset: 0x001101B4
	public GGServerRegion GetCurrentServerRegion()
	{
		GGServerRegion result = GGServerRegion.US;
		string serverAddress = PhotonNetwork.PhotonServerSettings.ServerAddress;
		if (serverAddress != null)
		{
			if (!(serverAddress == "app-us.exitgamescloud.com"))
			{
				if (!(serverAddress == "app-eu.exitgamescloud.com"))
				{
					if (!(serverAddress == "app-asia.exitgamescloud.com"))
					{
						if (!(serverAddress == "app-jp.exitgamescloud.com"))
						{
							if (serverAddress == "app-au.exitgamescloud.com")
							{
								result = GGServerRegion.AU;
							}
						}
						else
						{
							result = GGServerRegion.JP;
						}
					}
					else
					{
						result = GGServerRegion.ASIA;
					}
				}
				else
				{
					result = GGServerRegion.EU;
				}
			}
			else
			{
				result = GGServerRegion.US;
			}
		}
		return result;
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x00111E50 File Offset: 0x00110250
	public void SwitchServer(GGServerRegion sr)
	{
		CloudRegionCode preferredRegion = CloudRegionCode.us;
		RioQerdoDebug.Log(PhotonNetwork.PhotonServerSettings.ServerAddress);
		UIUserDataController.SetDefaultServer((int)sr);
		PhotonNetwork.Disconnect();
		PhotonNetwork.PhotonServerSettings.PreferredRegion = preferredRegion;
		this.SetCurPage(1);
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x00111E8B File Offset: 0x0011028B
	public void LoadLevel(string levelName)
	{
		PhotonNetwork.LoadLevel(levelName);
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x00111E93 File Offset: 0x00110293
	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x00111E9B File Offset: 0x0011029B
	public int GetPlayersNumInRoom()
	{
		return PhotonNetwork.countOfPlayersInRooms;
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x00111EA2 File Offset: 0x001102A2
	public ConnectionState GetConnectionState()
	{
		return PhotonNetwork.connectionState;
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x00111EA9 File Offset: 0x001102A9
	public void SetRoomJoinableStatus(bool status)
	{
		if (PhotonNetwork.room != null)
		{
			PhotonNetwork.room.open = status;
		}
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x00111EC0 File Offset: 0x001102C0
	private void OnJoinedRoom()
	{
		base.StopCoroutine("JoinRoomTimeOut");
		string roomName = this.GetRoomName();
		PlayerPrefs.SetString("LastRoomNameBeforeDisconnect", roomName);
		string sceneName = this.GetSceneName();
		this.IsNeedDisconnecting = false;
		GGNetworkKit.mInstance.LoadLevel(sceneName);
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x00111F03 File Offset: 0x00110303
	public void OnLeftRoom()
	{
		RioQerdoDebug.Log("OnLeftRoom");
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x00111F10 File Offset: 0x00110310
	private void OnReceivedRoomListUpdate()
	{
		RioQerdoDebug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
		}
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x00111F58 File Offset: 0x00110358
	public void SwitchMasterClient()
	{
		PhotonPlayer[] playerList = this.GetPlayerList();
		foreach (PhotonPlayer photonPlayer in playerList)
		{
			if (photonPlayer.ID != PhotonNetwork.player.ID && this.isMasterClient)
			{
				PhotonNetwork.SetMasterClient(photonPlayer);
			}
		}
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x00111FAD File Offset: 0x001103AD
	public void OnFailedToConnectToPhoton(object parameters)
	{
		this.connectLobbyFailed = true;
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x00111FB8 File Offset: 0x001103B8
	public Dictionary<int, GameObject> GetInstantiatedGameObjectList()
	{
		Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
		foreach (KeyValuePair<int, PhotonView> keyValuePair in PhotonNetwork.networkingPeer.photonViewList)
		{
			dictionary.Add(keyValuePair.Key, keyValuePair.Value.gameObject);
		}
		return dictionary;
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x00112034 File Offset: 0x00110434
	private void Update()
	{
		if (!PhotonNetwork.connected)
		{
			if (this.connectLobbyFailed)
			{
				this.connectLobbyFailed = false;
				GGNetworkKit.mInstance.ConnectToNetwork();
			}
			return;
		}
		this.mLastUpdateRoomStatusTime += Time.deltaTime;
		if (this.mLastUpdateRoomStatusTime > GGNetworkAdapter.CONSTANTUDATESTATUSTIME)
		{
			this.mLastUpdateRoomStatusTime = 0f;
			if (PhotonNetwork.room != null)
			{
				string roomName = this.GetRoomName();
				string mapName = this.GetMapName();
				string text = this.GetGameMode().ToString();
				string text2 = PhotonNetwork.room.playerCount.ToString();
				string text3 = this.GetMaxPlayers().ToString();
				string text4 = "1";
				if (PhotonNetwork.player != null)
				{
					text4 = PhotonNetwork.player.customProperties["rank"].ToString();
				}
				string text5 = ((int)this.GetCurrentServerRegion()).ToString();
				string roomIsEncryption = this.GetRoomIsEncryption();
				string encryptionRoomPassword = this.GetEncryptionRoomPassword();
				string text6 = PhotonNetwork.room.open.ToString();
				string message = string.Concat(new string[]
				{
					roomName,
					";",
					mapName,
					";",
					text,
					";",
					text2,
					";",
					text3,
					";",
					text4,
					";",
					text5,
					";",
					roomIsEncryption,
					";",
					encryptionRoomPassword,
					";",
					text6
				});
				if (GGNetworkChatSys.mInstance != null && GGNetworkChatSys.mInstance.chatClient != null)
				{
					GGNetworkChatSys.mInstance.chatClient.SetOnlineStatus(6, message);
				}
			}
		}
	}

	// Token: 0x0400244F RID: 9295
	public static GGNetworkAdapter mInstance;

	// Token: 0x04002450 RID: 9296
	private static PhotonView mScenePhotonView;

	// Token: 0x04002451 RID: 9297
	private object mLockObject = new object();

	// Token: 0x04002452 RID: 9298
	public GameObject mMainPlayer;

	// Token: 0x04002453 RID: 9299
	private bool connectLobbyFailed;

	// Token: 0x04002454 RID: 9300
	private bool isNeedDisconnecting = true;

	// Token: 0x04002455 RID: 9301
	private float mLastUpdateRoomStatusTime;

	// Token: 0x04002456 RID: 9302
	private static float CONSTANTUDATESTATUSTIME = 3f;

	// Token: 0x04002457 RID: 9303
	public static int MAXROOMNUMPERPAGE = 100;

	// Token: 0x04002458 RID: 9304
	private List<int> mEntertainmentModeIndexList = new List<int>
	{
		0,
		2,
		4,
		5
	};

	// Token: 0x04002459 RID: 9305
	private int maxpage = 1;

	// Token: 0x0400245A RID: 9306
	private List<RoomInfo> TmpConditionRoomlist = new List<RoomInfo>();

	// Token: 0x0400245B RID: 9307
	private int curPage = 1;
}
