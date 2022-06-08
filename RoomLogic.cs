using System;
using UnityEngine;

// Token: 0x020004ED RID: 1261
public class RoomLogic : MonoBehaviour
{
	// Token: 0x06002382 RID: 9090 RVA: 0x0010FB94 File Offset: 0x0010DF94
	public void Awake()
	{
		if (string.IsNullOrEmpty(PhotonNetwork.playerName))
		{
			PhotonNetwork.playerName = "Guest" + UnityEngine.Random.Range(1, 999999);
		}
		this.roomName = "Room" + UnityEngine.Random.Range(1, 999999);
		Debug.Log("region: " + PlayerPrefs.GetString("GGServerRegion"));
		if (PlayerPrefs.GetString("GGServerRegion") == string.Empty)
		{
			PlayerPrefs.SetString("GGServerRegion", GGServerRegion.US.ToString());
			this.mRegionServer = GGServerRegion.US;
			GGNetworkKit.mInstance.SwitchServer(this.mRegionServer);
		}
		this.DisplayCurrentRegion();
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x0010FC58 File Offset: 0x0010E058
	private void DisplayCurrentRegion()
	{
		string @string = PlayerPrefs.GetString("GGServerRegion");
		if (@string != null)
		{
			if (!(@string == "Deafault"))
			{
				if (!(@string == "US"))
				{
					if (!(@string == "EU"))
					{
						if (!(@string == "ASIA"))
						{
							if (!(@string == "JP"))
							{
								if (@string == "AU")
								{
									this.mRegionServer = GGServerRegion.AU;
								}
							}
							else
							{
								this.mRegionServer = GGServerRegion.JP;
							}
						}
						else
						{
							this.mRegionServer = GGServerRegion.ASIA;
						}
					}
					else
					{
						this.mRegionServer = GGServerRegion.EU;
					}
				}
				else
				{
					this.mRegionServer = GGServerRegion.US;
				}
			}
			else
			{
				this.mRegionServer = GGServerRegion.US;
			}
		}
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x0010FD24 File Offset: 0x0010E124
	private void Update()
	{
		GGError ggerror = this.mError;
		if (ggerror != GGError.RoomJoinRoomNotExistOrFull)
		{
			if (ggerror != GGError.RoomJoinPasswordNotCorrect)
			{
				if (ggerror != GGError.NoConnection)
				{
					if (ggerror != GGError.RoomCreateSameNameRoom)
					{
						this.mDisplayMessage = string.Empty;
					}
					else
					{
						this.mDisplayMessage = "The room name has been used!";
					}
				}
				else
				{
					this.mDisplayMessage = "Disconnect from server!";
				}
			}
			else
			{
				this.mDisplayMessage = "The password isn't correct!";
			}
		}
		else
		{
			this.mDisplayMessage = "The room is not exist or full!";
		}
	}

	// Token: 0x06002385 RID: 9093 RVA: 0x0010FDAC File Offset: 0x0010E1AC
	public void OnGUI()
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
		if (this.mDisplayCreateRoomWindow)
		{
			GUI.skin.box.fontStyle = FontStyle.Bold;
			GUI.Box(new Rect((float)((Screen.width - 400) / 2), (float)((Screen.height - 350) / 2), 400f, 300f), "Join or Create a Room");
			GUILayout.BeginArea(new Rect((float)((Screen.width - 400) / 2), (float)((Screen.height - 350) / 2), 400f, 300f));
			GUILayout.Space(25f);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Player name:", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			});
			PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName, new GUILayoutOption[0]);
			GUILayout.Space(105f);
			if (GUI.changed)
			{
				PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(15f);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Roomname:", new GUILayoutOption[]
			{
				GUILayout.Width(80f)
			});
			this.roomName = GUILayout.TextField(this.roomName, new GUILayoutOption[0]);
			if (GUILayout.Button(this.mRegionServer.ToString(), new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
				if (this.mRegionServer == GGServerRegion.EU)
				{
					this.mRegionServer = GGServerRegion.US;
				}
				else if (this.mRegionServer == GGServerRegion.US)
				{
					this.mRegionServer = GGServerRegion.EU;
				}
				else if (this.mRegionServer == GGServerRegion.EU)
				{
					this.mRegionServer = GGServerRegion.ASIA;
				}
				else if (this.mRegionServer == GGServerRegion.ASIA)
				{
					this.mRegionServer = GGServerRegion.JP;
				}
				else if (this.mRegionServer == GGServerRegion.JP)
				{
					this.mRegionServer = GGServerRegion.EU;
				}
				PlayerPrefs.SetString("GGServerRegion", this.mRegionServer.ToString());
				GGNetworkKit.mInstance.SwitchServer(this.mRegionServer);
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Space(105f);
			if (GUI.changed)
			{
				PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("players", new GUILayoutOption[]
			{
				GUILayout.Width(80f)
			});
			this.playerNum = GUILayout.TextField(this.playerNum, new GUILayoutOption[0]);
			GUILayout.Label("map", new GUILayoutOption[]
			{
				GUILayout.Width(80f)
			});
			this.mapName = GUILayout.TextField(this.mapName, new GUILayoutOption[0]);
			if (GUILayout.Button(this.modeType.ToString(), new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
				if (this.modeType == GGModeType.KillingCompetition)
				{
					this.modeType = GGModeType.StrongHold;
				}
				else if (this.modeType == GGModeType.StrongHold)
				{
					this.modeType = GGModeType.TeamDeathMatch;
				}
				else if (this.modeType == GGModeType.TeamDeathMatch)
				{
					this.modeType = GGModeType.KillingCompetition;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (GUILayout.Button("Search", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
				this.mDisplaySearchList = true;
			}
			this.SerachCondition = GUILayout.TextField(this.SerachCondition, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (GUILayout.Button("Create Room", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
				this.mapName = "MGameScene_1";
				GGNetworkKit.mInstance.CreateRoom(this.roomName, int.Parse(this.playerNum), this.mapName, this.mapName, this.modeType, false, this.mCreateRoomPassword, GGPlayModeType.Sport, 0);
			}
			if (GUILayout.Button("Create Encryption Room", new GUILayoutOption[]
			{
				GUILayout.Width(150f)
			}))
			{
				this.mDisplayCreateRoomPasswordWindow = true;
				this.mDisplayCreateRoomWindow = false;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(this.mDisplayMessage, new GUILayoutOption[]
			{
				GUILayout.Width(300f)
			});
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.Space(15f);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(string.Concat(new object[]
			{
				PhotonNetwork.countOfPlayers,
				" users are online in ",
				PhotonNetwork.countOfRooms,
				" rooms."
			}), new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Join Random", new GUILayoutOption[]
			{
				GUILayout.Width(100f)
			}))
			{
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(15f);
			if (PhotonNetwork.GetRoomList().Length != 0)
			{
				this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
				if (!this.mDisplaySearchList)
				{
					foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						if ((bool)roomInfo.customProperties["encryption"])
						{
							this.mIsEncryption = "Encryption";
							this.mJoinInServerPassword = roomInfo.customProperties["password"].ToString();
						}
						else
						{
							this.mIsEncryption = "          ";
							this.mJoinInServerPassword = string.Empty;
						}
						GUILayout.Label(string.Concat(new object[]
						{
							roomInfo.name,
							" ",
							roomInfo.playerCount,
							"/",
							roomInfo.maxPlayers,
							" ",
							this.mIsEncryption
						}), new GUILayoutOption[0]);
						if (GUILayout.Button("Join Room", new GUILayoutOption[0]))
						{
							if ((bool)roomInfo.customProperties["encryption"])
							{
								this.mDisplayJoinRoomPasswordWindow = true;
								this.mDisplayCreateRoomWindow = false;
								this.mJoinInRoomName = roomInfo.name;
							}
							else
							{
								PhotonNetwork.JoinRoom(roomInfo.name);
							}
						}
						GUILayout.EndHorizontal();
					}
				}
				else
				{
					RoomInfo[] array = GGNetworkKit.mInstance.SearchRoomByName(1, this.SerachCondition);
					foreach (RoomInfo roomInfo2 in array)
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						if ((bool)roomInfo2.customProperties["encryption"])
						{
							this.mIsEncryption = "Encryption";
							this.mJoinInServerPassword = roomInfo2.customProperties["password"].ToString();
						}
						else
						{
							this.mIsEncryption = "          ";
							this.mJoinInServerPassword = string.Empty;
						}
						GUILayout.Label(string.Concat(new object[]
						{
							roomInfo2.name,
							" ",
							roomInfo2.playerCount,
							"/",
							roomInfo2.maxPlayers,
							" ",
							this.mIsEncryption
						}), new GUILayoutOption[0]);
						if (GUILayout.Button("Join Room", new GUILayoutOption[0]))
						{
							if ((bool)roomInfo2.customProperties["encryption"])
							{
								this.mDisplayJoinRoomPasswordWindow = true;
								this.mDisplayCreateRoomWindow = false;
								this.mJoinInRoomName = roomInfo2.name;
							}
							else
							{
								PhotonNetwork.JoinRoom(roomInfo2.name);
							}
						}
						GUILayout.EndHorizontal();
					}
				}
				GUILayout.EndScrollView();
			}
			GUILayout.EndArea();
		}
		if (this.mDisplayCreateRoomPasswordWindow)
		{
			this.rectCreateRoomInputPasswordWindow = GUI.Window(0, this.rectCreateRoomInputPasswordWindow, new GUI.WindowFunction(this.CreateRoomInputPasswordWindowFunc), "Input password");
		}
		if (this.mDisplayJoinRoomPasswordWindow)
		{
			this.rectJoinRoomInputPasswordWindow = GUI.Window(1, this.rectJoinRoomInputPasswordWindow, new GUI.WindowFunction(this.JoinRoomInputPasswordWindowFunc), "Input password");
		}
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x00110610 File Offset: 0x0010EA10
	private int SortCompareByKillNum(GGNetworkPlayerProperties PP1, GGNetworkPlayerProperties PP2)
	{
		int result = 0;
		if (PP1.killNum > PP2.killNum)
		{
			result = -1;
		}
		else if (PP1.killNum < PP2.killNum)
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x0011064C File Offset: 0x0010EA4C
	private void CreateRoomInputPasswordWindowFunc(int windowID)
	{
		this.mCreateRoomPassword = GUILayout.TextField(this.mCreateRoomPassword, new GUILayoutOption[0]);
		if (GUI.Button(new Rect(50f, 150f, 100f, 20f), "OK") && this.mDisplayCreateRoomPasswordWindow)
		{
			this.mDisplayJoinRoomPasswordWindow = false;
			this.mDisplayCreateRoomPasswordWindow = false;
			this.mDisplayCreateRoomWindow = false;
			GGNetworkKit.mInstance.CreateRoom(this.roomName, int.Parse(this.playerNum), this.mapName, this.mapName, this.modeType, true, this.mCreateRoomPassword, GGPlayModeType.Sport, 0);
		}
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x001106F0 File Offset: 0x0010EAF0
	private void JoinRoomInputPasswordWindowFunc(int windowID)
	{
		this.mJoinInInputPassword = GUILayout.TextField(this.mJoinInInputPassword, new GUILayoutOption[0]);
		GUILayout.Label(this.mDisplayMessage, new GUILayoutOption[0]);
		if (GUI.Button(new Rect(25f, 150f, 70f, 20f), "Cancel"))
		{
			this.mDisplayJoinRoomPasswordWindow = false;
			this.mDisplayCreateRoomPasswordWindow = false;
			this.mDisplayCreateRoomWindow = true;
		}
		if (GUI.Button(new Rect(100f, 150f, 70f, 20f), "OK"))
		{
			if (this.mJoinInServerPassword == this.mJoinInInputPassword)
			{
				if (this.mDisplayJoinRoomPasswordWindow)
				{
					this.mDisplayJoinRoomPasswordWindow = false;
					this.mDisplayCreateRoomPasswordWindow = false;
					this.mDisplayCreateRoomWindow = false;
					GGNetworkKit.mInstance.JoinRoom(this.mJoinInRoomName);
				}
			}
			else
			{
				this.mError = GGError.RoomJoinPasswordNotCorrect;
			}
		}
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x001107DD File Offset: 0x0010EBDD
	private void OnJoinedLobby()
	{
	}

	// Token: 0x04002437 RID: 9271
	private string roomName = string.Empty;

	// Token: 0x04002438 RID: 9272
	private string playerNum = "16";

	// Token: 0x04002439 RID: 9273
	private string mapName = "map1";

	// Token: 0x0400243A RID: 9274
	private GGModeType modeType = GGModeType.KillingCompetition;

	// Token: 0x0400243B RID: 9275
	private string SerachCondition = string.Empty;

	// Token: 0x0400243C RID: 9276
	private bool mDisplaySearchList;

	// Token: 0x0400243D RID: 9277
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x0400243E RID: 9278
	private bool connectLobbyFailed;

	// Token: 0x0400243F RID: 9279
	public static readonly string SceneNameGame = "PhotonTest";

	// Token: 0x04002440 RID: 9280
	public GGError mError = GGError.Other;

	// Token: 0x04002441 RID: 9281
	public GGServerRegion mRegionServer;

	// Token: 0x04002442 RID: 9282
	private Rect rectCreateRoomInputPasswordWindow = new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height / 2 - 100), 200f, 200f);

	// Token: 0x04002443 RID: 9283
	private Rect rectJoinRoomInputPasswordWindow = new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height / 2 - 100), 200f, 200f);

	// Token: 0x04002444 RID: 9284
	private string mCreateRoomPassword = string.Empty;

	// Token: 0x04002445 RID: 9285
	private bool mDisplayCreateRoomPasswordWindow;

	// Token: 0x04002446 RID: 9286
	private bool mDisplayJoinRoomPasswordWindow;

	// Token: 0x04002447 RID: 9287
	private bool mDisplayCreateRoomWindow = true;

	// Token: 0x04002448 RID: 9288
	private string mIsEncryption = "          ";

	// Token: 0x04002449 RID: 9289
	private string mJoinInRoomName = string.Empty;

	// Token: 0x0400244A RID: 9290
	private string mJoinInInputPassword = string.Empty;

	// Token: 0x0400244B RID: 9291
	private string mJoinInServerPassword = string.Empty;

	// Token: 0x0400244C RID: 9292
	private string mDisplayMessage = string.Empty;
}
