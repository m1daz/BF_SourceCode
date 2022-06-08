using System;
using System.Collections;
using System.Collections.Generic;
using RioLog;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class UILobbySelectEntertainmentDirector : MonoBehaviour
{
	// Token: 0x0600160C RID: 5644 RVA: 0x000BCC16 File Offset: 0x000BB016
	private void Awake()
	{
		UILobbySelectEntertainmentDirector.mInstance = this;
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x000BCC1E File Offset: 0x000BB01E
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x000BCC26 File Offset: 0x000BB026
	private void OnEnable()
	{
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x000BCC28 File Offset: 0x000BB028
	private void Init()
	{
		this.InitSignalConnection();
		this.InitMinPlayerNum();
		this.InitModeToAllMapIndex();
		this.InitRoomData();
		this.InitModeWidget();
		this.InitRoomCreateMapView();
		this.InitRoomRandomFactor();
		this.InitHuntingDifficulty();
		this.InitTicketNum();
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x000BCC60 File Offset: 0x000BB060
	private void InitTicketNum()
	{
		this.mGoRoomListTicketNum.text = GrowthManagerKit.GetHuntingTickets().ToString();
		this.mGoCreateRoomTicketNum.text = GrowthManagerKit.GetHuntingTickets().ToString();
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000BCCAC File Offset: 0x000BB0AC
	private void InitHuntingDifficulty()
	{
		if (UIUserDataController.GetHuntingDifficult() == 0)
		{
			this.mGoHuntingDifficultEasyBG.SetActive(true);
			this.mGoHuntingDifficultMiddleBG.SetActive(false);
		}
		else if (UIUserDataController.GetHuntingDifficult() == 1)
		{
			this.mGoHuntingDifficultEasyBG.SetActive(false);
			this.mGoHuntingDifficultMiddleBG.SetActive(true);
		}
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000BCD03 File Offset: 0x000BB103
	private void InitRoomRandomFactor()
	{
		this.mRoomRandomFactor = UnityEngine.Random.Range(0, GGNetworkKit.mInstance.GetMaxRoomNumPerPage());
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x000BCD1C File Offset: 0x000BB11C
	private void InitMinPlayerNum()
	{
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.TeamDeathMatch, UILobbySelectEntertainmentDirector.MINPLAYERS);
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.StrongHold, UILobbySelectEntertainmentDirector.MINPLAYERS);
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.KillingCompetition, UILobbySelectEntertainmentDirector.MINPLAYERS);
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.Explosion, UILobbySelectEntertainmentDirector.MINPLAYERS);
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.Mutation, UILobbySelectEntertainmentDirector.MUTATIONMINPLAYERS);
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.KnifeCompetition, UILobbySelectEntertainmentDirector.MINPLAYERS);
		this.mDicKeyModeValvePlayersNum.Add(GGModeType.Hunting, UILobbySelectEntertainmentDirector.HUNTINGMINPLAYERS);
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x000BCDA0 File Offset: 0x000BB1A0
	private void InitRoomCreateMapView()
	{
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mCurrentMapIndexInMode = 0;
			this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()][0];
			string path = string.Empty;
			if (this.GetCreateRoomMode() == GGModeType.Hunting)
			{
				path = "UI/Images/Maps/Hunting/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mHuntingMapNameSuffix[this.mCurrentTextureIndex - 1];
				this.mRoomMapName = this.mHuntingMapNameArray[this.mCurrentTextureIndex - 1];
				this.mRoomSceneName = "HGameScene_" + this.mCurrentTextureIndex.ToString();
			}
			else
			{
				path = "UI/Images/Maps/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mMapNameSuffix[this.mCurrentTextureIndex - 1];
				this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex - 1];
				this.mRoomSceneName = "MGameScene_" + this.mCurrentTextureIndex.ToString();
			}
			this.mCurrentMapNameTexture = (Resources.Load(path) as Texture);
			this.mMapSpriteViewCreate.GetComponent<UITexture>().mainTexture = this.mCurrentMapNameTexture;
			this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
		}
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000BCF00 File Offset: 0x000BB300
	private void InitModeToAllMapIndex()
	{
		int[] value = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20
		};
		int[] value2 = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			17,
			18,
			20
		};
		int[] value3 = new int[]
		{
			15
		};
		int[] value4 = new int[]
		{
			1,
			2,
			3,
			4,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			17,
			18,
			20
		};
		int[] value5 = new int[]
		{
			16,
			17
		};
		int[] value6 = new int[]
		{
			4,
			6,
			7,
			9,
			10,
			11,
			12,
			13,
			14,
			17,
			18,
			20
		};
		int[] value7 = new int[]
		{
			1,
			2,
			3,
			5,
			19
		};
		int[] value8 = new int[]
		{
			1
		};
		this.mDicKeyModeValveMapIndex.Add(GGModeType.Other, value);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.TeamDeathMatch, value2);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.StrongHold, value3);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.KillingCompetition, value4);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.Explosion, value5);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.Mutation, value6);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.KnifeCompetition, value7);
		this.mDicKeyModeValveMapIndex.Add(GGModeType.Hunting, value8);
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x000BD007 File Offset: 0x000BB407
	private void InitSignalConnection()
	{
		this.mNoConnection.SetActive(false);
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x000BD018 File Offset: 0x000BB418
	private void InitRoomData()
	{
		this.mRoomName = UIUserDataController.GetCreatDefaultRoomName();
		this.mGoRoomName.GetComponent<UILabel>().text = this.mRoomName;
		this.mRoomPlayersNum = UIUserDataController.GetCreatDefaultPlayerNum();
		this.InitPlayerNum();
		if (this.GetCreateRoomMode() == GGModeType.Hunting)
		{
			this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[GGModeType.Hunting][0];
			this.mRoomMapName = this.mHuntingMapNameArray[this.mCurrentTextureIndex - 1];
		}
		else
		{
			this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex];
		}
		this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
		this.mGoModeRule.GetComponent<UILabel>().text = this.mRoomModeRule[0].ToUpper();
		GGServerRegion defaultServer = UIUserDataController.GetDefaultServer();
		if (defaultServer == GGServerRegion.DEFAULT)
		{
			this.mRegionName.GetComponent<UILabel>().text = GGServerRegion.US.ToString().ToUpper();
		}
		else
		{
			this.mRegionName.GetComponent<UILabel>().text = defaultServer.ToString().ToUpper();
		}
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x000BD134 File Offset: 0x000BB534
	private void InitPlayerNum()
	{
		if (UIUserDataController.GetEntertainmentDefaultMode() == 4 && this.mRoomPlayersNum < UILobbySelectEntertainmentDirector.MUTATIONMINPLAYERS)
		{
			this.mGoPlayersNum.GetComponent<UILabel>().text = UILobbySelectEntertainmentDirector.MUTATIONMINPLAYERS.ToString();
		}
		else
		{
			this.mGoPlayersNum.GetComponent<UILabel>().text = this.mRoomPlayersNum.ToString();
		}
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x000BD1A4 File Offset: 0x000BB5A4
	private void InitModeWidget()
	{
		if (!GGCloudServiceKit.mInstance.bHuntingModeOpen)
		{
			this.mGoHuntingModeButton.SetActive(false);
			if (UIUserDataController.GetEntertainmentDefaultMode() == 6)
			{
				UIUserDataController.SetEntertainmentDefaultMode(0);
			}
		}
		int num = 0;
		GGModeType ggmodeType = GGModeType.Other;
		if (this.mGoRoomlist.activeSelf)
		{
			ggmodeType = this.mCurrentRoomListMode;
		}
		else if (this.mGoCreateRoom.activeSelf)
		{
			ggmodeType = this.GetCreateRoomMode();
		}
		switch (ggmodeType)
		{
		case GGModeType.TeamDeathMatch:
			num = 1;
			break;
		case GGModeType.StrongHold:
			num = 2;
			break;
		case GGModeType.KillingCompetition:
			num = 3;
			break;
		case GGModeType.Explosion:
			num = 4;
			break;
		case GGModeType.Mutation:
			num = 5;
			break;
		case GGModeType.KnifeCompetition:
			num = 6;
			break;
		case GGModeType.Hunting:
			num = 7;
			break;
		default:
			if (ggmodeType == GGModeType.Other)
			{
				num = 0;
			}
			break;
		}
		if (this.mGoRoomlist.activeSelf)
		{
			for (int i = 0; i < this.mGoLobbyLeftRoomList.transform.childCount; i++)
			{
				Transform transform = this.mGoLobbyLeftRoomList.transform.GetChild(i).transform.Find("BackgroundClick");
				if (transform != null)
				{
					if (num == i)
					{
						transform.gameObject.SetActive(true);
					}
					else if (transform.gameObject.activeSelf)
					{
						transform.gameObject.SetActive(false);
					}
				}
			}
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			for (int j = 0; j < this.mGoLobbyLeftCreateRoom.transform.childCount; j++)
			{
				Transform transform2 = this.mGoLobbyLeftCreateRoom.transform.GetChild(j).transform.Find("BackgroundClick");
				if (transform2 != null)
				{
					if (num == j)
					{
						transform2.gameObject.SetActive(true);
					}
					else if (transform2.gameObject.activeSelf)
					{
						transform2.gameObject.SetActive(false);
					}
				}
			}
		}
		if (this.mGoModeRule.activeSelf)
		{
			this.mGoModeRule.GetComponent<UILabel>().text = this.mRoomModeRule[num].ToUpper();
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x000BD3DE File Offset: 0x000BB7DE
	private void Update()
	{
		this.UpdateWaitingForRoomList();
		this.UpdateRoomInfoList();
		this.UpdateConnectionSignal();
		this.UpdateJoinCreateRandomButton();
		this.UpdateMaxPlayerNumForRoom();
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x000BD3FE File Offset: 0x000BB7FE
	private void OnDestroy()
	{
		if (UILobbySelectEntertainmentDirector.mInstance != null)
		{
			UILobbySelectEntertainmentDirector.mInstance = null;
		}
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000BD418 File Offset: 0x000BB818
	private void UpdateMaxPlayerNumForRoom()
	{
		this.mOpTimeForUpdatePlaersNumCreateRoom += Time.deltaTime;
		if (this.mOpTimeForUpdatePlaersNumCreateRoom > UILobbySelectEntertainmentDirector.OPIntervalForUpdatePlaersNumCreateRoom)
		{
			this.mOpTimeForUpdatePlaersNumCreateRoom = 0f;
			this.RestrictRoomMaxPlayerForEveryMode(int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text));
		}
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x000BD470 File Offset: 0x000BB870
	private void UpdateJoinCreateRandomButton()
	{
		if (!this.mGoJoinButton.activeSelf)
		{
			this.mOpTimeForUpdateJoinRandomCreateButton += Time.deltaTime;
			if (this.mOpTimeForUpdateJoinRandomCreateButton > UILobbySelectEntertainmentDirector.OPIntervalForUpdateJoinRandomCreateButton)
			{
				this.mOpTimeForUpdateJoinRandomCreateButton = 0f;
				if (PhotonNetwork.insideLobby)
				{
					this.mGoJoinButton.SetActive(true);
					this.mGoRandomButton.SetActive(true);
					this.mGoCreateButton.SetActive(true);
				}
			}
		}
		else if (!PhotonNetwork.insideLobby)
		{
			this.mGoJoinButton.SetActive(false);
			this.mGoRandomButton.SetActive(false);
			this.mGoCreateButton.SetActive(false);
		}
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x000BD51C File Offset: 0x000BB91C
	private void UpdateWaitingForRoomList()
	{
		if (this.mGoWaitingforRoomList.activeSelf)
		{
			this.mOpTimeForUpdateWaitingRoomList += Time.deltaTime;
			if (this.mOpTimeForUpdateWaitingRoomList > UILobbySelectEntertainmentDirector.OPIntervalForUpdateWaitingRoomList)
			{
				this.mOpTimeForUpdateWaitingRoomList = 0f;
				string text = this.mGoWaitingforRoomList.GetComponent<UILabel>().text;
				if (text == "Waiting")
				{
					this.mGoWaitingforRoomList.GetComponent<UILabel>().text = "Waiting .";
				}
				else if (text == "Waiting .")
				{
					this.mGoWaitingforRoomList.GetComponent<UILabel>().text = "Waiting . .";
				}
				else if (text == "Waiting . .")
				{
					this.mGoWaitingforRoomList.GetComponent<UILabel>().text = "Waiting . . .";
				}
				else if (text == "Waiting . . .")
				{
					this.mGoWaitingforRoomList.GetComponent<UILabel>().text = "Waiting";
				}
			}
		}
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x000BD61A File Offset: 0x000BBA1A
	private void UpdateScrollViewContent()
	{
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000BD61C File Offset: 0x000BBA1C
	private void UpdateChosenRoomInfo()
	{
		for (int i = 0; i < this.mRoomInfoList.Count; i++)
		{
			if (this.mRoomInfoList[i].name == this.mChosenRoomName)
			{
				this.mChosenRoomInfo = this.mRoomInfoList[i];
			}
		}
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000BD678 File Offset: 0x000BBA78
	private void DisplayWaitingForRoomList()
	{
		if (!this.mGoWaitingforRoomList.activeSelf)
		{
			this.mGoWaitingforRoomList.SetActive(true);
		}
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000BD696 File Offset: 0x000BBA96
	private void HideWaitingForRoomList()
	{
		if (this.mGoWaitingforRoomList.activeSelf)
		{
			this.mGoWaitingforRoomList.SetActive(false);
		}
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000BD6B4 File Offset: 0x000BBAB4
	private void UpdateRoomInfoListImmediately(GGRoomListOperationType opType)
	{
		if (this.mGoLobbyLeftRoomList.activeSelf)
		{
			this.mTmpRoomInfoList.Clear();
			if (GGNetworkKit.mInstance != null)
			{
				GGNetworkKit.mInstance.GetRoomList(opType, this.GetRoomFilter(), out this.mTmpRoomInfoList);
			}
			if (GGNetworkKit.mInstance.GetConnectionState() != ConnectionState.Connected && !UITipController.mInstance.TipActiveSelf(UITipController.TipType.LoadingTip))
			{
				this.mTmpRoomInfoList.Clear();
			}
			if (this.mSearchKey == string.Empty)
			{
				if (this.mTmpRoomInfoList.Count == 0)
				{
					if (this.mCurrentRoomListMode == GGModeType.Other)
					{
						this.DisplayWaitingForRoomList();
					}
					else if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
					{
						this.HideWaitingForRoomList();
					}
					else
					{
						this.DisplayWaitingForRoomList();
					}
				}
				else
				{
					this.HideWaitingForRoomList();
				}
			}
			else
			{
				this.HideWaitingForRoomList();
			}
			this.mRoomInfoList.Clear();
			if (this.mRoomRandomFactor >= this.mTmpRoomInfoList.Count)
			{
				this.mRoomInfoList = this.mTmpRoomInfoList;
			}
			else
			{
				List<RoomInfo> range = this.mTmpRoomInfoList.GetRange(this.mRoomRandomFactor, this.mTmpRoomInfoList.Count - this.mRoomRandomFactor);
				List<RoomInfo> range2 = this.mTmpRoomInfoList.GetRange(0, this.mRoomRandomFactor);
				this.mRoomInfoList.AddRange(range);
				this.mRoomInfoList.AddRange(range2);
			}
			if (this.mChosenRoomName == string.Empty && this.mRoomInfoList.Count > 0)
			{
				this.mChosenRoomName = this.mRoomInfoList[0].name;
			}
			int count = this.mGoRoomInfoList.Count;
			int count2 = this.mRoomInfoList.Count;
			if (count > count2)
			{
				this.mGoToDestoryRoomInfoList.Clear();
				for (int i = count2; i < count; i++)
				{
					this.mGoToDestoryRoomInfoList.Add(this.mGoRoomInfoList[i]);
				}
				foreach (GameObject gameObject in this.mGoToDestoryRoomInfoList)
				{
					this.mGoRoomInfoList.Remove(gameObject);
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			else if (count < count2)
			{
				for (int j = count; j < count2; j++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.mGoRoomWidget);
					gameObject2.transform.parent = this.mScrollView.transform;
					gameObject2.transform.localPosition = new Vector3(0f, 340f - (float)j * 90f, 0f);
					gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
					this.mGoRoomInfoList.Add(gameObject2);
				}
			}
			for (int k = 0; k < this.mRoomInfoList.Count; k++)
			{
				GameObject gameObject3 = this.mGoRoomInfoList[k];
				UIRoomInfoPrefab component = gameObject3.GetComponent<UIRoomInfoPrefab>();
				if (component.mGOChosenRoomHightLight.activeSelf && this.mRoomInfoList[k].name != this.mChosenRoomName)
				{
					component.mGOChosenRoomHightLight.SetActive(false);
				}
				GGModeType ggmodeType = (GGModeType)this.mRoomInfoList[k].customProperties["mode"];
				switch (ggmodeType)
				{
				case GGModeType.TeamDeathMatch:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoDeathMatch";
					break;
				case GGModeType.StrongHold:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoStronghold";
					break;
				case GGModeType.KillingCompetition:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoKilling";
					break;
				case GGModeType.Explosion:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoExplosion";
					break;
				case GGModeType.Mutation:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoMutation";
					break;
				case GGModeType.KnifeCompetition:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoKnife";
					break;
				case GGModeType.Hunting:
					component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoHunting";
					break;
				default:
					if (ggmodeType == GGModeType.Other)
					{
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoAll";
					}
					break;
				}
				component.mGOMapNameLabel.GetComponent<UILabel>().text = (string)this.mRoomInfoList[k].customProperties["map"];
				component.mGORoomNameLabel.GetComponent<UILabel>().text = this.mRoomInfoList[k].name;
				component.mGOPlayersLabel.GetComponent<UILabel>().text = this.mRoomInfoList[k].playerCount + "/" + this.mRoomInfoList[k].maxPlayers;
				if (this.mRoomInfoList[k].name == this.mChosenRoomName)
				{
					if (!component.mGOChosenRoomHightLight.activeSelf)
					{
						component.mGOChosenRoomHightLight.SetActive(true);
					}
					this.mChosenRoomInfo = this.mRoomInfoList[k];
					this.ChangeMapView((string)this.mRoomInfoList[k].customProperties["map"]);
				}
				if (!this.mRoomInfoList[k].open)
				{
					component.mGONotJoinableSprite.SetActive(true);
				}
				else if (component.mGONotJoinableSprite.activeSelf)
				{
					component.mGONotJoinableSprite.SetActive(false);
				}
				if ((bool)this.mRoomInfoList[k].customProperties["encryption"])
				{
					component.mGOLockSprite.SetActive(true);
				}
				else if (component.mGOLockSprite.activeSelf)
				{
					component.mGOLockSprite.SetActive(false);
				}
				if (ggmodeType == GGModeType.Hunting)
				{
					if (!component.mGOHuntingDifficultySprite.activeSelf)
					{
						component.mGOHuntingDifficultySprite.SetActive(true);
					}
					int num = (int)this.mRoomInfoList[k].customProperties["huntingdifficulty"];
					if (num == 0)
					{
						component.mGOHuntingDifficultySprite.GetComponent<UISprite>().spriteName = "LobbyHuntingDifficultyEasy";
					}
					else if (num == 1)
					{
						component.mGOHuntingDifficultySprite.GetComponent<UISprite>().spriteName = "LobbyHuntingDifficultyMiddle";
					}
				}
				else if (component.mGOHuntingDifficultySprite.activeSelf)
				{
					component.mGOHuntingDifficultySprite.SetActive(false);
				}
				int roomListAllPageNum = GGNetworkKit.mInstance.GetRoomListAllPageNum();
				this.mGoAllRoomPageNum.GetComponent<UILabel>().text = roomListAllPageNum.ToString();
				if (roomListAllPageNum == 0)
				{
					this.mGoCurrentRoomPageNum.GetComponent<UILabel>().text = "0";
				}
				else
				{
					this.mGoCurrentRoomPageNum.GetComponent<UILabel>().text = GGNetworkKit.mInstance.GetRoomListCurrentPageNum().ToString();
				}
			}
		}
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000BDE34 File Offset: 0x000BC234
	private void UpdateRoomInfoList()
	{
		if (this.mGoLobbyLeftRoomList.activeSelf)
		{
			this.mOpTimeForUpdateRoomInfoList += Time.deltaTime;
			if (this.mOpTimeForUpdateRoomInfoList > UILobbySelectEntertainmentDirector.OPIntervalForUpdateRoomInfoList)
			{
				this.mOpTimeForUpdateRoomInfoList = 0f;
				this.mTmpRoomInfoList.Clear();
				if (GGNetworkKit.mInstance != null)
				{
					GGNetworkKit.mInstance.GetRoomList(GGRoomListOperationType.Tick, this.GetRoomFilter(), out this.mTmpRoomInfoList);
				}
				if (GGNetworkKit.mInstance.GetConnectionState() != ConnectionState.Connected && !UITipController.mInstance.TipActiveSelf(UITipController.TipType.LoadingTip))
				{
					this.mTmpRoomInfoList.Clear();
				}
				if (this.mSearchKey == string.Empty)
				{
					if (this.mTmpRoomInfoList.Count == 0)
					{
						if (this.mCurrentRoomListMode == GGModeType.Other)
						{
							this.DisplayWaitingForRoomList();
						}
						else if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
						{
							this.HideWaitingForRoomList();
						}
						else
						{
							this.DisplayWaitingForRoomList();
						}
					}
					else
					{
						this.HideWaitingForRoomList();
					}
				}
				else
				{
					this.HideWaitingForRoomList();
				}
				this.mRoomInfoList.Clear();
				if (this.mRoomRandomFactor >= this.mTmpRoomInfoList.Count)
				{
					this.mRoomInfoList = this.mTmpRoomInfoList;
				}
				else
				{
					List<RoomInfo> range = this.mTmpRoomInfoList.GetRange(this.mRoomRandomFactor, this.mTmpRoomInfoList.Count - this.mRoomRandomFactor);
					List<RoomInfo> range2 = this.mTmpRoomInfoList.GetRange(0, this.mRoomRandomFactor);
					this.mRoomInfoList.AddRange(range);
					this.mRoomInfoList.AddRange(range2);
				}
				if (this.mChosenRoomName == string.Empty && this.mRoomInfoList.Count > 0)
				{
					this.mChosenRoomName = this.mRoomInfoList[0].name;
				}
				int count = this.mGoRoomInfoList.Count;
				int count2 = this.mRoomInfoList.Count;
				if (count > count2)
				{
					this.mGoToDestoryRoomInfoList.Clear();
					for (int i = count2; i < count; i++)
					{
						this.mGoToDestoryRoomInfoList.Add(this.mGoRoomInfoList[i]);
					}
					foreach (GameObject gameObject in this.mGoToDestoryRoomInfoList)
					{
						this.mGoRoomInfoList.Remove(gameObject);
						UnityEngine.Object.Destroy(gameObject);
					}
				}
				else if (count < count2)
				{
					for (int j = count; j < count2; j++)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.mGoRoomWidget);
						gameObject2.transform.parent = this.mScrollView.transform;
						gameObject2.transform.localPosition = new Vector3(0f, 340f - (float)j * 90f, 0f);
						gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
						this.mGoRoomInfoList.Add(gameObject2);
					}
				}
				for (int k = 0; k < this.mRoomInfoList.Count; k++)
				{
					GameObject gameObject3 = this.mGoRoomInfoList[k];
					UIRoomInfoPrefab component = gameObject3.GetComponent<UIRoomInfoPrefab>();
					if (component.mGOChosenRoomHightLight.activeSelf && this.mRoomInfoList[k].name != this.mChosenRoomName)
					{
						component.mGOChosenRoomHightLight.SetActive(false);
					}
					GGModeType ggmodeType = (GGModeType)this.mRoomInfoList[k].customProperties["mode"];
					switch (ggmodeType)
					{
					case GGModeType.TeamDeathMatch:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoDeathMatch";
						break;
					case GGModeType.StrongHold:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoStronghold";
						break;
					case GGModeType.KillingCompetition:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoKilling";
						break;
					case GGModeType.Explosion:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoExplosion";
						break;
					case GGModeType.Mutation:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoMutation";
						break;
					case GGModeType.KnifeCompetition:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoKnife";
						break;
					case GGModeType.Hunting:
						component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoHunting";
						break;
					default:
						if (ggmodeType == GGModeType.Other)
						{
							component.mGORoomModeSprite.GetComponent<UISprite>().spriteName = "LobbyModeLogoAll";
						}
						break;
					}
					component.mGOMapNameLabel.GetComponent<UILabel>().text = (string)this.mRoomInfoList[k].customProperties["map"];
					component.mGORoomNameLabel.GetComponent<UILabel>().text = this.mRoomInfoList[k].name;
					component.mGOPlayersLabel.GetComponent<UILabel>().text = this.mRoomInfoList[k].playerCount + "/" + this.mRoomInfoList[k].maxPlayers;
					if (this.mRoomInfoList[k].name == this.mChosenRoomName)
					{
						if (!component.mGOChosenRoomHightLight.activeSelf)
						{
							component.mGOChosenRoomHightLight.SetActive(true);
						}
						this.mChosenRoomInfo = this.mRoomInfoList[k];
						this.ChangeMapView((string)this.mRoomInfoList[k].customProperties["map"]);
					}
					if (!this.mRoomInfoList[k].open)
					{
						component.mGONotJoinableSprite.SetActive(true);
					}
					else if (component.mGONotJoinableSprite.activeSelf)
					{
						component.mGONotJoinableSprite.SetActive(false);
					}
					if ((bool)this.mRoomInfoList[k].customProperties["encryption"])
					{
						component.mGOLockSprite.SetActive(true);
					}
					else if (component.mGOLockSprite.activeSelf)
					{
						component.mGOLockSprite.SetActive(false);
					}
					if (ggmodeType == GGModeType.Hunting)
					{
						if (!component.mGOHuntingDifficultySprite.activeSelf)
						{
							component.mGOHuntingDifficultySprite.SetActive(true);
						}
						int num = (int)this.mRoomInfoList[k].customProperties["huntingdifficulty"];
						if (num == 0)
						{
							component.mGOHuntingDifficultySprite.GetComponent<UISprite>().spriteName = "LobbyHuntingDifficultyEasy";
						}
						else if (num == 1)
						{
							component.mGOHuntingDifficultySprite.GetComponent<UISprite>().spriteName = "LobbyHuntingDifficultyMiddle";
						}
					}
					else
					{
						component.mGOHuntingDifficultySprite.SetActive(false);
					}
				}
				int roomListAllPageNum = GGNetworkKit.mInstance.GetRoomListAllPageNum();
				this.mGoAllRoomPageNum.GetComponent<UILabel>().text = roomListAllPageNum.ToString();
				if (roomListAllPageNum == 0)
				{
					this.mGoCurrentRoomPageNum.GetComponent<UILabel>().text = "0";
				}
				else
				{
					this.mGoCurrentRoomPageNum.GetComponent<UILabel>().text = GGNetworkKit.mInstance.GetRoomListCurrentPageNum().ToString();
				}
			}
		}
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x000BE5D0 File Offset: 0x000BC9D0
	private void UpdateConnectionSignal()
	{
		this.mOpTimeForUpdateConnection += Time.deltaTime;
		if (this.mOpTimeForUpdateConnection > UILobbySelectEntertainmentDirector.OPIntervalForUpdateConnection)
		{
			this.mOpTimeForUpdateConnection = 0f;
			if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
			{
				int ping = GGNetworkKit.mInstance.GetPing();
				float fillAmount = 0f;
				if (ping >= 500)
				{
					fillAmount = 0.2f;
				}
				if (ping >= 300 && ping < 500)
				{
					fillAmount = 0.4f;
				}
				if (ping >= 200 && ping < 300)
				{
					fillAmount = 0.5f;
				}
				if (ping >= 100 && ping < 200)
				{
					fillAmount = 0.8f;
				}
				if (ping < 100)
				{
					fillAmount = 1f;
				}
				this.mForegroundConnection.GetComponent<UISprite>().fillAmount = fillAmount;
			}
			else
			{
				this.mForegroundConnection.GetComponent<UISprite>().fillAmount = 0f;
			}
		}
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000BE6C8 File Offset: 0x000BCAC8
	public GGRoomFilter GetRoomFilter()
	{
		this.mRoomFilter.playMode = UIPlayModeSelectDirector.mPlayModeType;
		this.mRoomFilter.mode = this.mCurrentRoomListMode;
		this.mRoomFilter.playerRange = GGPlayersDisplayInterval.All;
		this.mRoomFilter.word = this.mSearchKey;
		return this.mRoomFilter;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x000BE719 File Offset: 0x000BCB19
	private void HideHuntingDifficultSelect(bool hide)
	{
		if (hide)
		{
			this.mGoHuntingDifficultEasy.SetActive(false);
			this.mGoHuntingDifficultMiddle.SetActive(false);
		}
		else
		{
			this.mGoHuntingDifficultEasy.SetActive(true);
			this.mGoHuntingDifficultMiddle.SetActive(true);
		}
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000BE758 File Offset: 0x000BCB58
	public void ModeFilterRoomByAllMode(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			this.mCurrentRoomListMode = GGModeType.Other;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000BE7C4 File Offset: 0x000BCBC4
	public void ModeFilterRoomByDeathMatch(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			this.mCurrentRoomListMode = GGModeType.TeamDeathMatch;
			UIUserDataController.SetEntertainmentDefaultMode(0);
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(0);
			this.mCurrentRoomListMode = GGModeType.TeamDeathMatch;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(true);
		}
		this.InitPlayerNum();
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000BE874 File Offset: 0x000BCC74
	public void ModeFilterRoomByStronghold(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetEntertainmentDefaultMode(1);
			this.mCurrentRoomListMode = GGModeType.StrongHold;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(1);
			this.mCurrentRoomListMode = GGModeType.StrongHold;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(true);
		}
		this.InitPlayerNum();
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000BE924 File Offset: 0x000BCD24
	public void ModeFilterRoomByKilling(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetEntertainmentDefaultMode(2);
			this.mCurrentRoomListMode = GGModeType.KillingCompetition;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(2);
			this.mCurrentRoomListMode = GGModeType.KillingCompetition;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(true);
		}
		this.InitPlayerNum();
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x000BE9D4 File Offset: 0x000BCDD4
	public void ModeFilterRoomByExplosing(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetEntertainmentDefaultMode(3);
			this.mCurrentRoomListMode = GGModeType.Explosion;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(3);
			this.mCurrentRoomListMode = GGModeType.Explosion;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(true);
		}
		this.InitPlayerNum();
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x000BEA84 File Offset: 0x000BCE84
	public void ModeFilterRoomByMutation(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetEntertainmentDefaultMode(4);
			this.mCurrentRoomListMode = GGModeType.Mutation;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(4);
			this.mCurrentRoomListMode = GGModeType.Mutation;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(true);
		}
		this.InitPlayerNum();
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x000BEB34 File Offset: 0x000BCF34
	public void ModeFilterRoomByKnife(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetEntertainmentDefaultMode(5);
			this.mCurrentRoomListMode = GGModeType.KnifeCompetition;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(5);
			this.mCurrentRoomListMode = GGModeType.KnifeCompetition;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(true);
		}
		this.InitPlayerNum();
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x000BEBE4 File Offset: 0x000BCFE4
	public void ModeFilterRoomByHunting(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetEntertainmentDefaultMode(6);
			this.mCurrentRoomListMode = GGModeType.Hunting;
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetEntertainmentDefaultMode(6);
			this.mCurrentRoomListMode = GGModeType.Hunting;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
			this.HideHuntingDifficultSelect(false);
		}
		this.InitPlayerNum();
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x000BEC94 File Offset: 0x000BD094
	public void LastRegion()
	{
		if (this.mGoJoinButton.activeSelf)
		{
			this.mGoJoinButton.SetActive(false);
			this.mGoRandomButton.SetActive(false);
			this.mGoCreateButton.SetActive(false);
		}
		int num = (int)UIUserDataController.GetDefaultServer();
		num--;
		if (num < 0 || num > 4)
		{
			num = 0;
		}
		UIUserDataController.SetDefaultServer(num);
		UILabel component = this.mRegionName.GetComponent<UILabel>();
		GGServerRegion ggserverRegion = (GGServerRegion)num;
		component.text = ggserverRegion.ToString();
		GGNetworkKit.mInstance.SwitchServer((GGServerRegion)num);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x000BED30 File Offset: 0x000BD130
	public void NextRegion()
	{
		if (this.mGoJoinButton.activeSelf)
		{
			this.mGoJoinButton.SetActive(false);
			this.mGoRandomButton.SetActive(false);
			this.mGoCreateButton.SetActive(false);
		}
		int num = (int)UIUserDataController.GetDefaultServer();
		num++;
		if (num > 4 || num < 0)
		{
			num = 0;
		}
		UIUserDataController.SetDefaultServer(num);
		UILabel component = this.mRegionName.GetComponent<UILabel>();
		GGServerRegion ggserverRegion = (GGServerRegion)num;
		component.text = ggserverRegion.ToString();
		GGNetworkKit.mInstance.SwitchServer((GGServerRegion)num);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000BEDCB File Offset: 0x000BD1CB
	public void SearchSubmit(string searchkey)
	{
		GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
		this.mChosenRoomName = string.Empty;
		this.mSearchKey = searchkey;
		this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000BEE04 File Offset: 0x000BD204
	public void ChangeMapView(string mapName)
	{
		string path = string.Empty;
		if (this.mHuntingDicKeyMapNameValueIndex.ContainsKey(mapName))
		{
			int num = this.mHuntingDicKeyMapNameValueIndex[mapName];
			path = "UI/Images/Maps/Hunting/Map_" + num.ToString() + "_" + this.mHuntingMapNameSuffix[num - 1];
		}
		else
		{
			int num = this.mDicKeyMapNameValueIndex[mapName];
			path = "UI/Images/Maps/Map_" + num.ToString() + "_" + this.mMapNameSuffix[num - 1];
		}
		this.mRoomListCurrentMapNameTexture = (Resources.Load(path) as Texture);
		this.mMapSpriteView.GetComponent<UITexture>().mainTexture = this.mRoomListCurrentMapNameTexture;
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000BEEC0 File Offset: 0x000BD2C0
	public void SetChosenRoom(GameObject chosenRoom)
	{
		string text = chosenRoom.transform.Find("RoomNameLabel").GetComponent<UILabel>().text;
		if (this.mChosenRoomName == text)
		{
			this.JoinOnclick();
		}
		else
		{
			int childCount = this.mScrollView.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.mScrollView.transform.GetChild(i).transform.Find("ChosenRoomHightLight").gameObject.SetActive(false);
			}
			chosenRoom.transform.Find("ChosenRoomHightLight").gameObject.SetActive(true);
			this.mChosenRoomName = text;
			this.UpdateChosenRoomInfo();
		}
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000BEF7C File Offset: 0x000BD37C
	public void LastMapWhenCreateRoom()
	{
		this.mCurrentMapIndexInMode--;
		if (this.mCurrentMapIndexInMode < 0)
		{
			this.mCurrentMapIndexInMode = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()].Length - 1;
		}
		this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()][this.mCurrentMapIndexInMode];
		string path = string.Empty;
		if (this.GetCreateRoomMode() == GGModeType.Hunting)
		{
			path = "UI/Images/Maps/Hunting/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mHuntingMapNameSuffix[this.mCurrentTextureIndex - 1];
			this.mRoomMapName = this.mHuntingMapNameArray[this.mCurrentTextureIndex - 1];
			this.mRoomSceneName = "HGameScene_" + this.mCurrentTextureIndex.ToString();
		}
		else
		{
			path = "UI/Images/Maps/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mMapNameSuffix[this.mCurrentTextureIndex - 1];
			this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex - 1];
			this.mRoomSceneName = "MGameScene_" + this.mCurrentTextureIndex.ToString();
		}
		this.mCurrentMapNameTexture = (Resources.Load(path) as Texture);
		this.mMapSpriteViewCreate.GetComponent<UITexture>().mainTexture = this.mCurrentMapNameTexture;
		this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000BF0FC File Offset: 0x000BD4FC
	public void NextMapWhenCreateRoom()
	{
		this.mCurrentMapIndexInMode++;
		if (this.mCurrentMapIndexInMode > this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()].Length - 1)
		{
			this.mCurrentMapIndexInMode = 0;
		}
		this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()][this.mCurrentMapIndexInMode];
		string path = string.Empty;
		if (this.GetCreateRoomMode() == GGModeType.Hunting)
		{
			path = "UI/Images/Maps/Hunting/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mHuntingMapNameSuffix[this.mCurrentTextureIndex - 1];
			this.mRoomMapName = this.mHuntingMapNameArray[this.mCurrentTextureIndex - 1];
			this.mRoomSceneName = "HGameScene_" + this.mCurrentTextureIndex.ToString();
		}
		else
		{
			path = "UI/Images/Maps/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mMapNameSuffix[this.mCurrentTextureIndex - 1];
			this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex - 1];
			this.mRoomSceneName = "MGameScene_" + this.mCurrentTextureIndex.ToString();
		}
		this.mCurrentMapNameTexture = (Resources.Load(path) as Texture);
		this.mMapSpriteViewCreate.GetComponent<UITexture>().mainTexture = this.mCurrentMapNameTexture;
		this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000BF27C File Offset: 0x000BD67C
	public void EncryptToggle(bool isChecked, GameObject go)
	{
		if (isChecked)
		{
			this.mRoomPassword = string.Empty;
			this.mGoInputPasswordCreateRoom.GetComponent<UIInput>().value = string.Empty;
			this.mGoInputPasswordCreateRoom.SetActive(false);
		}
		else
		{
			this.mGoInputPasswordCreateRoom.SetActive(true);
		}
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000BF2CC File Offset: 0x000BD6CC
	public void SubmitRoomPassword(string password)
	{
		this.mRoomPassword = password;
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000BF2D8 File Offset: 0x000BD6D8
	public void IncreasePlayersNum()
	{
		int num = int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text);
		if (this.GetCreateRoomMode() == GGModeType.Hunting)
		{
			this.RestrictRoomMaxPlayerForEveryMode(UILobbySelectEntertainmentDirector.HUNTINGMAXPLAYERS);
		}
		else
		{
			if (this.mRoomPlayersNum % 2 == 1)
			{
				this.mRoomPlayersNum++;
			}
			else
			{
				this.mRoomPlayersNum += 2;
			}
			if (this.mRoomPlayersNum > UILobbySelectEntertainmentDirector.MAXPLAYERS)
			{
				this.mRoomPlayersNum = UILobbySelectEntertainmentDirector.MAXPLAYERS;
			}
			if (num % 2 == 1)
			{
				int restrictMaxPlayer = num + 1;
				this.RestrictRoomMaxPlayerForEveryMode(restrictMaxPlayer);
			}
			else
			{
				int restrictMaxPlayer2 = num + 2;
				this.RestrictRoomMaxPlayerForEveryMode(restrictMaxPlayer2);
			}
		}
		UIUserDataController.SetCreatDefaultPlayerNum(this.mRoomPlayersNum);
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000BF394 File Offset: 0x000BD794
	public void ReducePlayersNum()
	{
		int num;
		if (this.GetCreateRoomMode() == GGModeType.Hunting)
		{
			num = 1;
		}
		else
		{
			this.mRoomPlayersNum -= 2;
			if (this.mRoomPlayersNum < UILobbySelectEntertainmentDirector.MINPLAYERS)
			{
				this.mRoomPlayersNum = UILobbySelectEntertainmentDirector.MINPLAYERS;
			}
			num = int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text) - 2;
		}
		switch (UIUserDataController.GetEntertainmentDefaultMode())
		{
		case 1:
			if (num < UILobbySelectEntertainmentDirector.STRONGHOLDEQUALPLAYERS)
			{
				num = UILobbySelectEntertainmentDirector.STRONGHOLDEQUALPLAYERS;
			}
			goto IL_EF;
		case 3:
			if (num < UILobbySelectEntertainmentDirector.EXPLOSITIONEQUALPLAYERS)
			{
				num = UILobbySelectEntertainmentDirector.EXPLOSITIONEQUALPLAYERS;
			}
			goto IL_EF;
		case 4:
			if (num < UILobbySelectEntertainmentDirector.MUTATIONMINPLAYERS)
			{
				num = UILobbySelectEntertainmentDirector.MUTATIONMINPLAYERS;
			}
			goto IL_EF;
		case 6:
			if (num < UILobbySelectEntertainmentDirector.HUNTINGMINPLAYERS)
			{
				num = UILobbySelectEntertainmentDirector.HUNTINGMINPLAYERS;
			}
			goto IL_EF;
		}
		if (num < UILobbySelectEntertainmentDirector.MINPLAYERS)
		{
			num = UILobbySelectEntertainmentDirector.MINPLAYERS;
		}
		IL_EF:
		this.mGoPlayersNum.GetComponent<UILabel>().text = num.ToString();
		UIUserDataController.SetCreatDefaultPlayerNum(this.mRoomPlayersNum);
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000BF4B8 File Offset: 0x000BD8B8
	public void RoomNameSubmit(string roomname)
	{
		roomname = WordFilter.mInstance.FilterString(roomname);
		this.mGoRoomName.GetComponent<UILabel>().text = roomname;
		this.mRoomName = roomname;
		UIUserDataController.SetCreatDefaultRoomName(this.mRoomName);
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x000BF4EC File Offset: 0x000BD8EC
	public void RoomListCreateOnClick()
	{
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
		{
			this.mGoRoomlist.SetActive(false);
			this.mGoCreateRoom.SetActive(true);
			this.InitModeWidget();
			this.InitRoomCreateMapView();
			if (UIUserDataController.GetEntertainmentDefaultMode() == 6)
			{
				this.HideHuntingDifficultSelect(false);
			}
			else
			{
				this.HideHuntingDifficultSelect(true);
			}
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000BF584 File Offset: 0x000BD984
	public void HideConnectingToServer()
	{
		this.LobbySelectStopAllCoroutines();
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x000BF596 File Offset: 0x000BD996
	public void LobbySelectStopAllCoroutines()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x000BF59E File Offset: 0x000BD99E
	public int RestrictRoomMaxPlayer(int tmpMaxPlayers)
	{
		if (tmpMaxPlayers > UILobbySelectEntertainmentDirector.MAXPLAYERS)
		{
			return UILobbySelectEntertainmentDirector.MAXPLAYERS;
		}
		return tmpMaxPlayers;
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000BF5B4 File Offset: 0x000BD9B4
	public int RestrictRoomMaxPlayerForEveryMode(int RestrictMaxPlayer)
	{
		if (RestrictMaxPlayer > UILobbySelectEntertainmentDirector.MAXPLAYERS)
		{
			RestrictMaxPlayer = UILobbySelectEntertainmentDirector.MAXPLAYERS;
		}
		GGModeType entertainmentDefaultMode = (GGModeType)UIUserDataController.GetEntertainmentDefaultMode();
		switch (entertainmentDefaultMode)
		{
		case GGModeType.TeamDeathMatch:
		case GGModeType.KillingCompetition:
		{
			int num = this.mCurrentTextureIndex;
			switch (num)
			{
			case 1:
			case 2:
			case 3:
				if (RestrictMaxPlayer > 12)
				{
					RestrictMaxPlayer = 12;
				}
				goto IL_BA;
			default:
				if (num != 18)
				{
					goto IL_BA;
				}
				break;
			case 6:
			case 7:
			case 8:
			case 10:
				break;
			}
			if (RestrictMaxPlayer > 16)
			{
				RestrictMaxPlayer = 16;
			}
			IL_BA:
			break;
		}
		case GGModeType.StrongHold:
			break;
		case GGModeType.Explosion:
			if (RestrictMaxPlayer > UILobbySelectEntertainmentDirector.EXPLOSITIONEQUALPLAYERS)
			{
				RestrictMaxPlayer = UILobbySelectEntertainmentDirector.EXPLOSITIONEQUALPLAYERS;
			}
			break;
		case GGModeType.Mutation:
			break;
		case GGModeType.KnifeCompetition:
			break;
		case GGModeType.Hunting:
			if (RestrictMaxPlayer > 4)
			{
				RestrictMaxPlayer = 4;
			}
			break;
		default:
			if (entertainmentDefaultMode != GGModeType.Other)
			{
			}
			break;
		}
		this.mGoPlayersNum.GetComponent<UILabel>().text = RestrictMaxPlayer.ToString();
		return RestrictMaxPlayer;
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x000BF6D4 File Offset: 0x000BDAD4
	public void CreateRoomCreateOnClick()
	{
		RioQerdoDebug.Log("PhotonNetwork.insideLobby : " + PhotonNetwork.insideLobby);
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
		{
			this.mRoomMode = (GGModeType)UIUserDataController.GetEntertainmentDefaultMode();
			if (this.mRoomMode == GGModeType.Hunting && GrowthManagerKit.GetHuntingTickets() < UILobbySelectEntertainmentDirector.HUNTINGTICKETNUM)
			{
				UIDialogDirector.mInstance.DisplayNeedHuntingTicketDialog();
			}
			else
			{
				if (this.mRoomPassword != string.Empty)
				{
					this.mEncryption = true;
				}
				GGPlayModeType mPlayModeType = UIPlayModeSelectDirector.mPlayModeType;
				int maxPlayers = this.RestrictRoomMaxPlayerForEveryMode(int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text));
				int huntingDifficult = UIUserDataController.GetHuntingDifficult();
				GGNetworkKit.mInstance.CreateRoom(this.mRoomName, maxPlayers, this.mRoomMapName, this.mRoomSceneName, this.mRoomMode, this.mEncryption, this.mRoomPassword, mPlayModeType, huntingDifficult);
				UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, string.Empty, string.Empty, null, null, null);
			}
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x000BF810 File Offset: 0x000BDC10
	public void BackOnclick()
	{
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mGoRoomlist.SetActive(true);
			this.mGoCreateRoom.SetActive(false);
			this.mChangeCreateRoomMode = false;
			this.InitModeWidget();
		}
		else if (UIPlayModeSelectDirector.mInstance != null)
		{
			UIPlayModeSelectDirector.mInstance.BackToPlayModeSelect();
		}
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x000BF874 File Offset: 0x000BDC74
	public void JoinOnclick()
	{
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected && PhotonNetwork.insideLobby)
		{
			GGModeType ggmodeType = (GGModeType)this.mChosenRoomInfo.customProperties["mode"];
			if (ggmodeType == GGModeType.Hunting && GrowthManagerKit.GetHuntingTickets() < UILobbySelectEntertainmentDirector.HUNTINGTICKETNUM)
			{
				UIDialogDirector.mInstance.DisplayNeedHuntingTicketDialog();
			}
			else if (this.mChosenRoomName != string.Empty)
			{
				if (!this.mChosenRoomInfo.open)
				{
					EventDelegate btnEventName = new EventDelegate(this, "HideGeneralTipsDialog");
					UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Room is not joinable!", Color.white, "OK", string.Empty, btnEventName, null, null);
				}
				else if (this.mChosenRoomInfo.playerCount == (int)this.mChosenRoomInfo.maxPlayers)
				{
					EventDelegate btnEventName2 = new EventDelegate(this, "HideGeneralTipsDialog");
					UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Room is full!", Color.white, "OK", string.Empty, btnEventName2, null, null);
				}
				else if ((bool)this.mChosenRoomInfo.customProperties["encryption"])
				{
					EventDelegate btnEventName3 = new EventDelegate(this, "CancelInputPasswordToJoin");
					EventDelegate btnEventName4 = new EventDelegate(this, "InputPasswordToJoin");
					UITipController.mInstance.SetTipData(UITipController.TipType.InputPasswordTip, string.Empty, Color.red, "Cancel", "Join", btnEventName3, btnEventName4, null);
				}
				else
				{
					GGNetworkKit.mInstance.JoinRoom(this.mChosenRoomName);
					UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, string.Empty, string.Empty, null, null, null);
				}
			}
		}
		else
		{
			EventDelegate btnEventName5 = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName5, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x000BFA50 File Offset: 0x000BDE50
	public void RandomJoinOnClick()
	{
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected && PhotonNetwork.insideLobby)
		{
			bool canJoinHuntingMode = true;
			if (GrowthManagerKit.GetHuntingTickets() < UILobbySelectEntertainmentDirector.HUNTINGTICKETNUM)
			{
				canJoinHuntingMode = false;
			}
			GGNetworkKit.mInstance.JoinRandomRoom(UIPlayModeSelectDirector.mPlayModeType, this.mCurrentRoomListMode, canJoinHuntingMode);
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, string.Empty, string.Empty, null, null, null);
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000BFAFB File Offset: 0x000BDEFB
	public void DisplayCheckConnectionTipsDialog()
	{
		base.StartCoroutine(this.EnumeratorDisplayCheckConnectionTipsDialog(60f));
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x000BFB10 File Offset: 0x000BDF10
	public IEnumerator EnumeratorDisplayCheckConnectionTipsDialog(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		this.DisplayGeneralTipsDialog("Please check the network connection!", Color.red, "OK");
		yield break;
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x000BFB32 File Offset: 0x000BDF32
	public void HideGeneralTipsDialog()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000BFB3E File Offset: 0x000BDF3E
	public void HideInputPasswordDialog()
	{
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x000BFB40 File Offset: 0x000BDF40
	public void DisplayGeneralTipsDialog(string str1, Color color, string str2)
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideGeneralTipsDialog");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, str1, color, str2, string.Empty, btnEventName, null, null);
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x000BFB6F File Offset: 0x000BDF6F
	public void CancelInputPasswordToJoin()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x000BFB7C File Offset: 0x000BDF7C
	public void InputPasswordToJoin()
	{
		string b = (string)this.mChosenRoomInfo.customProperties["password"];
		if (UITipController.mInstance.passwordInput.value == b)
		{
			GGNetworkKit.mInstance.JoinRoom(this.mChosenRoomInfo.name);
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, string.Empty, string.Empty, null, null, null);
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "CancelInputPasswordToJoin");
			EventDelegate btnEventName2 = new EventDelegate(this, "InputPasswordToJoin");
			UITipController.mInstance.SetTipData(UITipController.TipType.InputPasswordTip, "Incorrect Password!", Color.red, "Cancel", "Join", btnEventName, btnEventName2, null);
		}
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x000BFC34 File Offset: 0x000BE034
	public void NextPageOfRoomList()
	{
		this.mChosenRoomName = string.Empty;
		this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Down);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000BFC58 File Offset: 0x000BE058
	public void PreviousPageOfRommList()
	{
		this.mChosenRoomName = string.Empty;
		this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Up);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x000BFC7C File Offset: 0x000BE07C
	public bool IsChosenRoomFull()
	{
		return (int)this.mChosenRoomInfo.maxPlayers - this.mChosenRoomInfo.playerCount <= 3;
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000BFC9E File Offset: 0x000BE09E
	public int GetChosenRoomPlayerCount()
	{
		return this.mChosenRoomInfo.playerCount;
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x000BFCAC File Offset: 0x000BE0AC
	public GGModeType GetCreateRoomMode()
	{
		if (this.mCurrentRoomListMode != GGModeType.Other && !this.mChangeCreateRoomMode)
		{
			return this.mCurrentRoomListMode;
		}
		GGModeType entertainmentDefaultMode = (GGModeType)UIUserDataController.GetEntertainmentDefaultMode();
		if (entertainmentDefaultMode == GGModeType.StrongHold || entertainmentDefaultMode == GGModeType.Explosion)
		{
			return GGModeType.TeamDeathMatch;
		}
		return entertainmentDefaultMode;
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x000BFCF2 File Offset: 0x000BE0F2
	public void SelectGameDifficultEasy()
	{
		UIUserDataController.SetHuntingDifficult(0);
		this.mGoHuntingDifficultEasyBG.SetActive(true);
		this.mGoHuntingDifficultMiddleBG.SetActive(false);
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x000BFD12 File Offset: 0x000BE112
	public void SelectGameDifficultMiddle()
	{
		UIUserDataController.SetHuntingDifficult(1);
		this.mGoHuntingDifficultEasyBG.SetActive(false);
		this.mGoHuntingDifficultMiddleBG.SetActive(true);
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000BFD32 File Offset: 0x000BE132
	public void PurchaseTicket()
	{
		if (GrowthManagerKit.GetCoins() > 300)
		{
			GrowthManagerKit.SubCoins(300);
			GrowthManagerKit.AddHuntingTickets(1);
			this.InitTicketNum();
		}
	}

	// Token: 0x040018E0 RID: 6368
	public static UILobbySelectEntertainmentDirector mInstance;

	// Token: 0x040018E1 RID: 6369
	public List<GameObject> mGoRoomInfoList = new List<GameObject>();

	// Token: 0x040018E2 RID: 6370
	public List<RoomInfo> mRoomInfoList = new List<RoomInfo>();

	// Token: 0x040018E3 RID: 6371
	public List<RoomInfo> mTmpRoomInfoList = new List<RoomInfo>();

	// Token: 0x040018E4 RID: 6372
	private List<GameObject> mGoToDestoryRoomInfoList = new List<GameObject>();

	// Token: 0x040018E5 RID: 6373
	private static int MAXPLAYERS = 20;

	// Token: 0x040018E6 RID: 6374
	private static int MINPLAYERS = 4;

	// Token: 0x040018E7 RID: 6375
	private static int MUTATIONMINPLAYERS = 5;

	// Token: 0x040018E8 RID: 6376
	private static int STRONGHOLDEQUALPLAYERS = 20;

	// Token: 0x040018E9 RID: 6377
	private static int EXPLOSITIONEQUALPLAYERS = 10;

	// Token: 0x040018EA RID: 6378
	private static int HUNTINGMAXPLAYERS = 4;

	// Token: 0x040018EB RID: 6379
	private static int HUNTINGMINPLAYERS = 1;

	// Token: 0x040018EC RID: 6380
	private static int HUNTINGTICKETNUM = 1;

	// Token: 0x040018ED RID: 6381
	public GGRoomFilter mRoomFilter = new GGRoomFilter();

	// Token: 0x040018EE RID: 6382
	private string mSearchKey = string.Empty;

	// Token: 0x040018EF RID: 6383
	private float mOpTimeForUpdateRoomInfoList;

	// Token: 0x040018F0 RID: 6384
	private static float OPIntervalForUpdateRoomInfoList = 1.5f;

	// Token: 0x040018F1 RID: 6385
	private float mOpTimeForUpdateConnection;

	// Token: 0x040018F2 RID: 6386
	private static float OPIntervalForUpdateConnection = 2f;

	// Token: 0x040018F3 RID: 6387
	private float mOpTimeForUpdateWaitingRoomList;

	// Token: 0x040018F4 RID: 6388
	private static float OPIntervalForUpdateWaitingRoomList = 0.8f;

	// Token: 0x040018F5 RID: 6389
	private float mOpTimeForUpdateJoinRandomCreateButton;

	// Token: 0x040018F6 RID: 6390
	private static float OPIntervalForUpdateJoinRandomCreateButton = 3f;

	// Token: 0x040018F7 RID: 6391
	private float mOpTimeForUpdatePlaersNumCreateRoom;

	// Token: 0x040018F8 RID: 6392
	private static float OPIntervalForUpdatePlaersNumCreateRoom = 0.3f;

	// Token: 0x040018F9 RID: 6393
	public GameObject mGoRoomWidget;

	// Token: 0x040018FA RID: 6394
	public GameObject mGoRoomlist;

	// Token: 0x040018FB RID: 6395
	public GameObject mGoCreateRoom;

	// Token: 0x040018FC RID: 6396
	public GameObject mScrollView;

	// Token: 0x040018FD RID: 6397
	public GameObject mScrollViewBar;

	// Token: 0x040018FE RID: 6398
	public GameObject mRegionName;

	// Token: 0x040018FF RID: 6399
	public GameObject mMapSpriteView;

	// Token: 0x04001900 RID: 6400
	public GameObject mForegroundConnection;

	// Token: 0x04001901 RID: 6401
	public GameObject mNoConnection;

	// Token: 0x04001902 RID: 6402
	public GameObject mMapSpriteViewCreate;

	// Token: 0x04001903 RID: 6403
	public GameObject mEncryptPassword;

	// Token: 0x04001904 RID: 6404
	public GameObject mGoPlayersNum;

	// Token: 0x04001905 RID: 6405
	public GameObject mGoRoomName;

	// Token: 0x04001906 RID: 6406
	public GameObject mGoLobbyLeftCreateRoom;

	// Token: 0x04001907 RID: 6407
	public GameObject mGoLobbyLeftRoomList;

	// Token: 0x04001908 RID: 6408
	public GameObject mGoMapName;

	// Token: 0x04001909 RID: 6409
	public GameObject mGoModeRule;

	// Token: 0x0400190A RID: 6410
	public GameObject mGoCurrentRoomPageNum;

	// Token: 0x0400190B RID: 6411
	public GameObject mGoAllRoomPageNum;

	// Token: 0x0400190C RID: 6412
	public GameObject mGoInputPasswordCreateRoom;

	// Token: 0x0400190D RID: 6413
	public GameObject mGoWaitingforRoomList;

	// Token: 0x0400190E RID: 6414
	public GameObject mGoJoinButton;

	// Token: 0x0400190F RID: 6415
	public GameObject mGoRandomButton;

	// Token: 0x04001910 RID: 6416
	public GameObject mGoCreateButton;

	// Token: 0x04001911 RID: 6417
	public GameObject mGoHuntingDifficultEasy;

	// Token: 0x04001912 RID: 6418
	public GameObject mGoHuntingDifficultMiddle;

	// Token: 0x04001913 RID: 6419
	public GameObject mGoHuntingDifficultEasyBG;

	// Token: 0x04001914 RID: 6420
	public GameObject mGoHuntingDifficultMiddleBG;

	// Token: 0x04001915 RID: 6421
	public GameObject mGoHuntingModeButton;

	// Token: 0x04001916 RID: 6422
	public UILabel mGoRoomListTicketNum;

	// Token: 0x04001917 RID: 6423
	public UILabel mGoCreateRoomTicketNum;

	// Token: 0x04001918 RID: 6424
	private RoomInfo mChosenRoomInfo;

	// Token: 0x04001919 RID: 6425
	private string[] mRoomModeRule = new string[]
	{
		"Mode  Rule\nAll  Mode,enjoy  yourselves.",
		"Mode  Rule\nYour  goal  is  killing  all  the  enemies.",
		"Mode  Rule\nGame starts automatically when players > 10. The team occupy the stronghold will respawn nearby, and battery will attack the enemy automatically. Every 2 minutes will statistic results of capture and settle resources until one team’s resources reach the limit.",
		"Mode  Rule\nThe  fisrt  team  who  kill  the  set  number  enemies  will  win.",
		"Mode  Rule\nRed  team  mission:  install  bomb  and  hold  on  until  the  bomb  explode or kill all the enemy.\nBlue  team  mission:  uninstall  bomb  and  hold  on  until  round  over or kill all the enemy. The team who win 5 rounds will win the game.",
		"Mode  Rule\nZombies  will  win  if  they  make  all  humans  mutate  into  zombies  in  10  minutes,  otherwise  humans  will  win.",
		"Mode  Rule\nYou can only use Knife.",
		"Mode  Rule\nYou can recieve 3 tickets every day. Defeat boss can get reward, also you can recieve rich reward if you defeat all. Promote your skill and equipment, use potions and props reasonable is the key！"
	};

	// Token: 0x0400191A RID: 6426
	private string[] mConnectingServerContent = new string[]
	{
		"CONNECTING TO SERVER      ",
		"CONNECTING TO SERVER .    ",
		"CONNECTING TO SERVER . .  ",
		"CONNECTING TO SERVER . . ."
	};

	// Token: 0x0400191B RID: 6427
	private string[] mMapNameArray = new string[]
	{
		"DEATH PLATFORM",
		"WAREHOUSE",
		"SNOW",
		"DUSTY",
		"KNIFE FACTORY",
		"DESERT A",
		"DESERT B",
		"CHESS HERO",
		"MAZE VILLAGE",
		"CHRISTMAS TOWN",
		"FLOATING CITY",
		"MACROSS",
		"FANTASY CASTLE",
		"CHRISTMAS HOUSE",
		"STORM TRENCH",
		"DESERT",
		"EXHIBITION",
		"SHIPPING PORT",
		"LAVA CELLAR",
		"GHOST TOWN"
	};

	// Token: 0x0400191C RID: 6428
	private Dictionary<string, int> mDicKeyMapNameValueIndex = new Dictionary<string, int>
	{
		{
			"DEATH PLATFORM",
			1
		},
		{
			"WAREHOUSE",
			2
		},
		{
			"SNOW",
			3
		},
		{
			"DUSTY",
			4
		},
		{
			"KNIFE FACTORY",
			5
		},
		{
			"DESERT A",
			6
		},
		{
			"DESERT B",
			7
		},
		{
			"CHESS HERO",
			8
		},
		{
			"MAZE VILLAGE",
			9
		},
		{
			"CHRISTMAS TOWN",
			10
		},
		{
			"FLOATING CITY",
			11
		},
		{
			"MACROSS",
			12
		},
		{
			"FANTASY CASTLE",
			13
		},
		{
			"CHRISTMAS HOUSE",
			14
		},
		{
			"STORM TRENCH",
			15
		},
		{
			"DESERT",
			16
		},
		{
			"EXHIBITION",
			17
		},
		{
			"SHIPPING PORT",
			18
		},
		{
			"LAVA CELLAR",
			19
		},
		{
			"GHOST TOWN",
			20
		}
	};

	// Token: 0x0400191D RID: 6429
	private string[] mMapNameSuffix = new string[]
	{
		"Platform",
		"Warehouse",
		"Snow",
		"Dusty",
		"KnifeFactory",
		"DesertA",
		"DesertB",
		"ChessHero",
		"MazeVillage",
		"ChristmasTown",
		"FloatingCity",
		"Macross",
		"FantasyCastle",
		"ChristmasHouse",
		"Trench",
		"Desert",
		"Exhibition",
		"ShippingPort",
		"LavaCellar",
		"GhostTown"
	};

	// Token: 0x0400191E RID: 6430
	private string[] mHuntingMapNameArray = new string[]
	{
		"MECHANICAL SQUARE"
	};

	// Token: 0x0400191F RID: 6431
	private Dictionary<string, int> mHuntingDicKeyMapNameValueIndex = new Dictionary<string, int>
	{
		{
			"MECHANICAL SQUARE",
			1
		}
	};

	// Token: 0x04001920 RID: 6432
	private string[] mHuntingMapNameSuffix = new string[]
	{
		"MechanicalSquare"
	};

	// Token: 0x04001921 RID: 6433
	private Texture mCurrentMapNameTexture;

	// Token: 0x04001922 RID: 6434
	private Texture mRoomListCurrentMapNameTexture;

	// Token: 0x04001923 RID: 6435
	private int mCurrentTextureIndex;

	// Token: 0x04001924 RID: 6436
	private Dictionary<GGModeType, int> mDicKeyModeValvePlayersNum = new Dictionary<GGModeType, int>();

	// Token: 0x04001925 RID: 6437
	private Dictionary<GGModeType, int[]> mDicKeyModeValveMapIndex = new Dictionary<GGModeType, int[]>();

	// Token: 0x04001926 RID: 6438
	private int mCurrentMapIndexInMode;

	// Token: 0x04001927 RID: 6439
	public string mChosenRoomName = string.Empty;

	// Token: 0x04001928 RID: 6440
	private int mRoomRandomFactor;

	// Token: 0x04001929 RID: 6441
	private GGModeType mCurrentRoomListMode = GGModeType.Other;

	// Token: 0x0400192A RID: 6442
	private bool mChangeCreateRoomMode;

	// Token: 0x0400192B RID: 6443
	public string mRoomName = string.Empty;

	// Token: 0x0400192C RID: 6444
	public string mRoomPassword = string.Empty;

	// Token: 0x0400192D RID: 6445
	public int mRoomPlayersNum = 10;

	// Token: 0x0400192E RID: 6446
	public GGModeType mRoomMode = GGModeType.Other;

	// Token: 0x0400192F RID: 6447
	public string mRoomMapName = string.Empty;

	// Token: 0x04001930 RID: 6448
	public string mRoomSceneName = "MGameScene_1";

	// Token: 0x04001931 RID: 6449
	public bool mEncryption;
}
