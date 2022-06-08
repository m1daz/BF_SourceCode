using System;
using System.Collections;
using System.Collections.Generic;
using RioLog;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class UILobbySelectDirector : MonoBehaviour
{
	// Token: 0x060015CA RID: 5578 RVA: 0x000B9C12 File Offset: 0x000B8012
	private void Awake()
	{
		UILobbySelectDirector.mInstance = this;
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x000B9C1A File Offset: 0x000B801A
	private void Start()
	{
		this.Init();
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000B9C22 File Offset: 0x000B8022
	private void OnEnable()
	{
		this.InitRoomData();
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000B9C2A File Offset: 0x000B802A
	private void Init()
	{
		this.InitSignalConnection();
		this.InitRoomData();
		this.InitModeWidget();
		this.InitModeToAllMapIndex();
		this.InitRoomCreateMapView();
		this.InitRoomRandomFactor();
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x000B9C50 File Offset: 0x000B8050
	private void InitRoomRandomFactor()
	{
		this.mRoomRandomFactor = UnityEngine.Random.Range(0, GGNetworkKit.mInstance.GetMaxRoomNumPerPage());
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x000B9C68 File Offset: 0x000B8068
	private void InitRoomCreateMapView()
	{
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mCurrentMapIndexInMode = 0;
			this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()][0];
			string path = "UI/Images/Maps/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mMapNameSuffix[this.mCurrentTextureIndex - 1];
			this.mCurrentMapNameTexture = (Resources.Load(path) as Texture);
			this.mMapSpriteViewCreate.GetComponent<UITexture>().mainTexture = this.mCurrentMapNameTexture;
			this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex - 1];
			this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
			this.mRoomSceneName = "MGameScene_" + this.mCurrentTextureIndex.ToString();
		}
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000B9D48 File Offset: 0x000B8148
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

	// Token: 0x060015D1 RID: 5585 RVA: 0x000B9E4F File Offset: 0x000B824F
	private void InitSignalConnection()
	{
		this.mNoConnection.SetActive(false);
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000B9E60 File Offset: 0x000B8260
	private void InitRoomData()
	{
		this.mRoomName = UIUserDataController.GetCreatDefaultRoomName();
		this.mGoRoomName.GetComponent<UILabel>().text = this.mRoomName;
		this.mRoomPlayersNum = UIUserDataController.GetCreatDefaultPlayerNum();
		this.InitPlayerNum();
		this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex];
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

	// Token: 0x060015D3 RID: 5587 RVA: 0x000B9F44 File Offset: 0x000B8344
	private void InitPlayerNum()
	{
		if (UIUserDataController.GetDefaultMode() == 4 && this.mRoomPlayersNum < UILobbySelectDirector.MUTATIONMINPLAYERS)
		{
			this.mGoPlayersNum.GetComponent<UILabel>().text = UILobbySelectDirector.MUTATIONMINPLAYERS.ToString();
		}
		else if (UIUserDataController.GetDefaultMode() == 1 && this.mRoomPlayersNum < UILobbySelectDirector.STRONGHOLDEQUALPLAYERS)
		{
			this.mGoPlayersNum.GetComponent<UILabel>().text = UILobbySelectDirector.STRONGHOLDEQUALPLAYERS.ToString();
		}
		else if (UIUserDataController.GetDefaultMode() == 3)
		{
			this.mGoPlayersNum.GetComponent<UILabel>().text = UILobbySelectDirector.EXPLOSITIONEQUALPLAYERS.ToString();
		}
		else
		{
			this.mGoPlayersNum.GetComponent<UILabel>().text = this.mRoomPlayersNum.ToString();
		}
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000BA024 File Offset: 0x000B8424
	private void InitModeWidget()
	{
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

	// Token: 0x060015D5 RID: 5589 RVA: 0x000BA232 File Offset: 0x000B8632
	private void Update()
	{
		this.UpdateWaitingForRoomList();
		this.UpdateRoomInfoList();
		this.UpdateConnectionSignal();
		this.UpdateJoinCreateRandomButton();
		this.UpdateMaxPlayerNumForRoom();
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000BA252 File Offset: 0x000B8652
	private void OnDestroy()
	{
		if (UILobbySelectDirector.mInstance != null)
		{
			UILobbySelectDirector.mInstance = null;
		}
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000BA26C File Offset: 0x000B866C
	private void UpdateMaxPlayerNumForRoom()
	{
		this.mOpTimeForUpdatePlaersNumCreateRoom += Time.deltaTime;
		if (this.mOpTimeForUpdatePlaersNumCreateRoom > UILobbySelectDirector.OPIntervalForUpdatePlaersNumCreateRoom)
		{
			this.mOpTimeForUpdatePlaersNumCreateRoom = 0f;
			this.RestrictRoomMaxPlayerForEveryMode(int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text));
		}
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000BA2C4 File Offset: 0x000B86C4
	private void UpdateJoinCreateRandomButton()
	{
		if (!this.mGoJoinButton.activeSelf)
		{
			this.mOpTimeForUpdateJoinRandomCreateButton += Time.deltaTime;
			if (this.mOpTimeForUpdateJoinRandomCreateButton > UILobbySelectDirector.OPIntervalForUpdateJoinRandomCreateButton)
			{
				this.mOpTimeForUpdateJoinRandomCreateButton = 0f;
				if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
				{
					this.mGoJoinButton.SetActive(true);
					this.mGoRandomButton.SetActive(true);
					this.mGoCreateButton.SetActive(true);
				}
			}
		}
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x000BA344 File Offset: 0x000B8744
	private void UpdateWaitingForRoomList()
	{
		if (this.mGoWaitingforRoomList.activeSelf)
		{
			this.mOpTimeForUpdateWaitingRoomList += Time.deltaTime;
			if (this.mOpTimeForUpdateWaitingRoomList > UILobbySelectDirector.OPIntervalForUpdateWaitingRoomList)
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

	// Token: 0x060015DA RID: 5594 RVA: 0x000BA442 File Offset: 0x000B8842
	private void UpdateScrollViewContent()
	{
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x000BA444 File Offset: 0x000B8844
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

	// Token: 0x060015DC RID: 5596 RVA: 0x000BA4A0 File Offset: 0x000B88A0
	private void DisplayWaitingForRoomList()
	{
		if (!this.mGoWaitingforRoomList.activeSelf)
		{
			this.mGoWaitingforRoomList.SetActive(true);
		}
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x000BA4BE File Offset: 0x000B88BE
	private void HideWaitingForRoomList()
	{
		if (this.mGoWaitingforRoomList.activeSelf)
		{
			this.mGoWaitingforRoomList.SetActive(false);
		}
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000BA4DC File Offset: 0x000B88DC
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

	// Token: 0x060015DF RID: 5599 RVA: 0x000BABB0 File Offset: 0x000B8FB0
	private void UpdateRoomInfoList()
	{
		if (this.mGoLobbyLeftRoomList.activeSelf)
		{
			this.mOpTimeForUpdateRoomInfoList += Time.deltaTime;
			if (this.mOpTimeForUpdateRoomInfoList > UILobbySelectDirector.OPIntervalForUpdateRoomInfoList)
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

	// Token: 0x060015E0 RID: 5600 RVA: 0x000BB340 File Offset: 0x000B9740
	private void UpdateConnectionSignal()
	{
		this.mOpTimeForUpdateConnection += Time.deltaTime;
		if (this.mOpTimeForUpdateConnection > UILobbySelectDirector.OPIntervalForUpdateConnection)
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

	// Token: 0x060015E1 RID: 5601 RVA: 0x000BB438 File Offset: 0x000B9838
	public GGRoomFilter GetRoomFilter()
	{
		this.mRoomFilter.playMode = UIPlayModeSelectDirector.mPlayModeType;
		this.mRoomFilter.mode = this.mCurrentRoomListMode;
		this.mRoomFilter.playerRange = GGPlayersDisplayInterval.All;
		this.mRoomFilter.word = this.mSearchKey;
		return this.mRoomFilter;
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x000BB48C File Offset: 0x000B988C
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

	// Token: 0x060015E3 RID: 5603 RVA: 0x000BB4F8 File Offset: 0x000B98F8
	public void ModeFilterRoomByDeathMatch(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			this.mCurrentRoomListMode = GGModeType.TeamDeathMatch;
			UIUserDataController.SetDefaultMode(0);
			this.InitModeWidget();
			go.SetActive(true);
			this.mChosenRoomName = string.Empty;
			this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
			this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
		if (this.mGoCreateRoom.activeSelf)
		{
			this.mChangeCreateRoomMode = true;
			UIUserDataController.SetDefaultMode(0);
			this.mCurrentRoomListMode = GGModeType.TeamDeathMatch;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
		}
		this.InitPlayerNum();
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x000BB5A0 File Offset: 0x000B99A0
	public void ModeFilterRoomByStronghold(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetDefaultMode(1);
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
			UIUserDataController.SetDefaultMode(1);
			this.mCurrentRoomListMode = GGModeType.StrongHold;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
		}
		this.InitPlayerNum();
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x000BB648 File Offset: 0x000B9A48
	public void ModeFilterRoomByKilling(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetDefaultMode(2);
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
			UIUserDataController.SetDefaultMode(2);
			this.mCurrentRoomListMode = GGModeType.KillingCompetition;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
		}
		this.InitPlayerNum();
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x000BB6F0 File Offset: 0x000B9AF0
	public void ModeFilterRoomByExplosing(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetDefaultMode(3);
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
			UIUserDataController.SetDefaultMode(3);
			this.mCurrentRoomListMode = GGModeType.Explosion;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
		}
		this.InitPlayerNum();
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x000BB798 File Offset: 0x000B9B98
	public void ModeFilterRoomByMutation(GameObject go)
	{
		if (this.mGoRoomlist.activeSelf)
		{
			this.mChangeCreateRoomMode = false;
			GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
			UIUserDataController.SetDefaultMode(4);
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
			UIUserDataController.SetDefaultMode(4);
			this.mCurrentRoomListMode = GGModeType.Mutation;
			this.InitModeWidget();
			go.SetActive(true);
			this.InitRoomCreateMapView();
		}
		this.InitPlayerNum();
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x000BB840 File Offset: 0x000B9C40
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
		}
		this.InitPlayerNum();
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x000BB8E8 File Offset: 0x000B9CE8
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
			num = 4;
		}
		UIUserDataController.SetDefaultServer(num);
		UILabel component = this.mRegionName.GetComponent<UILabel>();
		GGServerRegion ggserverRegion = (GGServerRegion)num;
		component.text = ggserverRegion.ToString();
		GGNetworkKit.mInstance.SwitchServer((GGServerRegion)num);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x000BB984 File Offset: 0x000B9D84
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

	// Token: 0x060015EB RID: 5611 RVA: 0x000BBA1F File Offset: 0x000B9E1F
	public void SearchSubmit(string searchkey)
	{
		GGNetworkKit.mInstance.SetRoomListCurrentPage(1);
		this.mChosenRoomName = string.Empty;
		this.mSearchKey = searchkey;
		this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Tick);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000BBA58 File Offset: 0x000B9E58
	public void ChangeMapView(string mapName)
	{
		if (this.mDicKeyMapNameValueIndex.ContainsKey(mapName))
		{
			int num = this.mDicKeyMapNameValueIndex[mapName];
			string path = "UI/Images/Maps/Map_" + num.ToString() + "_" + this.mMapNameSuffix[num - 1];
			this.mRoomListCurrentMapNameTexture = (Resources.Load(path) as Texture);
			this.mMapSpriteView.GetComponent<UITexture>().mainTexture = this.mRoomListCurrentMapNameTexture;
		}
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x000BBAD4 File Offset: 0x000B9ED4
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

	// Token: 0x060015EE RID: 5614 RVA: 0x000BBB90 File Offset: 0x000B9F90
	public void LastMapWhenCreateRoom()
	{
		this.mCurrentMapIndexInMode--;
		if (this.mCurrentMapIndexInMode < 0)
		{
			this.mCurrentMapIndexInMode = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()].Length - 1;
		}
		this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()][this.mCurrentMapIndexInMode];
		string path = "UI/Images/Maps/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mMapNameSuffix[this.mCurrentTextureIndex - 1];
		this.mCurrentMapNameTexture = (Resources.Load(path) as Texture);
		this.mMapSpriteViewCreate.GetComponent<UITexture>().mainTexture = this.mCurrentMapNameTexture;
		this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex - 1];
		this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
		this.mRoomSceneName = "MGameScene_" + this.mCurrentTextureIndex.ToString();
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x000BBC94 File Offset: 0x000BA094
	public void NextMapWhenCreateRoom()
	{
		this.mCurrentMapIndexInMode++;
		if (this.mCurrentMapIndexInMode > this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()].Length - 1)
		{
			this.mCurrentMapIndexInMode = 0;
		}
		this.mCurrentTextureIndex = this.mDicKeyModeValveMapIndex[this.GetCreateRoomMode()][this.mCurrentMapIndexInMode];
		string path = "UI/Images/Maps/Map_" + this.mCurrentTextureIndex.ToString() + "_" + this.mMapNameSuffix[this.mCurrentTextureIndex - 1];
		this.mCurrentMapNameTexture = (Resources.Load(path) as Texture);
		this.mMapSpriteViewCreate.GetComponent<UITexture>().mainTexture = this.mCurrentMapNameTexture;
		this.mRoomMapName = this.mMapNameArray[this.mCurrentTextureIndex - 1];
		this.mGoMapName.GetComponent<UILabel>().text = this.mRoomMapName;
		this.mRoomSceneName = "MGameScene_" + this.mCurrentTextureIndex.ToString();
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x000BBD98 File Offset: 0x000BA198
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

	// Token: 0x060015F1 RID: 5617 RVA: 0x000BBDE8 File Offset: 0x000BA1E8
	public void SubmitRoomPassword(string password)
	{
		this.mRoomPassword = password;
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x000BBDF4 File Offset: 0x000BA1F4
	public void IncreasePlayersNum()
	{
		if (this.mRoomPlayersNum % 2 == 1)
		{
			this.mRoomPlayersNum++;
		}
		else
		{
			this.mRoomPlayersNum += 2;
		}
		if (this.mRoomPlayersNum > UILobbySelectDirector.MAXPLAYERS)
		{
			this.mRoomPlayersNum = UILobbySelectDirector.MAXPLAYERS;
		}
		int num = int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text);
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
		UIUserDataController.SetCreatDefaultPlayerNum(this.mRoomPlayersNum);
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x000BBE94 File Offset: 0x000BA294
	public void ReducePlayersNum()
	{
		this.mRoomPlayersNum -= 2;
		if (this.mRoomPlayersNum < UILobbySelectDirector.MINPLAYERS)
		{
			this.mRoomPlayersNum = UILobbySelectDirector.MINPLAYERS;
		}
		int num = int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text) - 2;
		switch (UIUserDataController.GetDefaultMode())
		{
		case 1:
			if (num < UILobbySelectDirector.STRONGHOLDEQUALPLAYERS)
			{
				num = UILobbySelectDirector.STRONGHOLDEQUALPLAYERS;
			}
			goto IL_BC;
		case 3:
			if (num < UILobbySelectDirector.EXPLOSITIONEQUALPLAYERS)
			{
				num = UILobbySelectDirector.EXPLOSITIONEQUALPLAYERS;
			}
			goto IL_BC;
		case 4:
			if (num < UILobbySelectDirector.MUTATIONMINPLAYERS)
			{
				num = UILobbySelectDirector.MUTATIONMINPLAYERS;
			}
			goto IL_BC;
		}
		if (num < UILobbySelectDirector.MINPLAYERS)
		{
			num = UILobbySelectDirector.MINPLAYERS;
		}
		IL_BC:
		this.mGoPlayersNum.GetComponent<UILabel>().text = num.ToString();
		UIUserDataController.SetCreatDefaultPlayerNum(this.mRoomPlayersNum);
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x000BBF85 File Offset: 0x000BA385
	public void RoomNameSubmit(string roomname)
	{
		roomname = WordFilter.mInstance.FilterString(roomname);
		this.mGoRoomName.GetComponent<UILabel>().text = roomname;
		this.mRoomName = roomname;
		UIUserDataController.SetCreatDefaultRoomName(this.mRoomName);
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x000BBFB8 File Offset: 0x000BA3B8
	public void RoomListCreateOnClick()
	{
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
		{
			this.mGoRoomlist.SetActive(false);
			this.mGoCreateRoom.SetActive(true);
			this.InitModeWidget();
			this.InitRoomCreateMapView();
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x000BC032 File Offset: 0x000BA432
	public void HideConnectingToServer()
	{
		this.LobbySelectStopAllCoroutines();
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x000BC044 File Offset: 0x000BA444
	public void LobbySelectStopAllCoroutines()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x000BC04C File Offset: 0x000BA44C
	public int RestrictRoomMaxPlayer(int tmpMaxPlayers)
	{
		if (tmpMaxPlayers > UILobbySelectDirector.MAXPLAYERS)
		{
			return UILobbySelectDirector.MAXPLAYERS;
		}
		return tmpMaxPlayers;
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x000BC060 File Offset: 0x000BA460
	public int RestrictRoomMaxPlayerForEveryMode(int RestrictMaxPlayer)
	{
		if (RestrictMaxPlayer > UILobbySelectDirector.MAXPLAYERS)
		{
			RestrictMaxPlayer = UILobbySelectDirector.MAXPLAYERS;
		}
		GGModeType defaultMode = (GGModeType)UIUserDataController.GetDefaultMode();
		switch (defaultMode)
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
			if (RestrictMaxPlayer > UILobbySelectDirector.EXPLOSITIONEQUALPLAYERS)
			{
				RestrictMaxPlayer = UILobbySelectDirector.EXPLOSITIONEQUALPLAYERS;
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
			if (defaultMode != GGModeType.Other)
			{
			}
			break;
		}
		this.mGoPlayersNum.GetComponent<UILabel>().text = RestrictMaxPlayer.ToString();
		return RestrictMaxPlayer;
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x000BC180 File Offset: 0x000BA580
	public void CreateRoomCreateOnClick()
	{
		RioQerdoDebug.Log("PhotonNetwork.insideLobby : " + GGNetworkKit.mInstance.GetConnectionState());
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected)
		{
			if (this.mRoomPassword != string.Empty)
			{
				this.mEncryption = true;
			}
			this.mRoomMode = (GGModeType)UIUserDataController.GetDefaultMode();
			GGPlayModeType mPlayModeType = UIPlayModeSelectDirector.mPlayModeType;
			int maxPlayers = this.RestrictRoomMaxPlayerForEveryMode(int.Parse(this.mGoPlayersNum.GetComponent<UILabel>().text));
			int huntingDifficult = UIUserDataController.GetHuntingDifficult();
			GGNetworkKit.mInstance.CreateRoom(this.mRoomName, maxPlayers, this.mRoomMapName, this.mRoomSceneName, this.mRoomMode, this.mEncryption, this.mRoomPassword, mPlayModeType, huntingDifficult);
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, string.Empty, string.Empty, null, null, null);
		}
		else
		{
			RioQerdoDebug.Log("PhotonNetwork.insideLobby : " + PhotonNetwork.insideLobby);
			EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000BC2B0 File Offset: 0x000BA6B0
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

	// Token: 0x060015FC RID: 5628 RVA: 0x000BC314 File Offset: 0x000BA714
	public void JoinOnclick()
	{
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected && PhotonNetwork.insideLobby)
		{
			if (this.mChosenRoomName != string.Empty)
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

	// Token: 0x060015FD RID: 5629 RVA: 0x000BC4AC File Offset: 0x000BA8AC
	public void RandomJoinOnClick()
	{
		if (GGNetworkKit.mInstance.GetConnectionState() == ConnectionState.Connected && PhotonNetwork.insideLobby)
		{
			GGNetworkKit.mInstance.JoinRandomRoom(UIPlayModeSelectDirector.mPlayModeType, this.mCurrentRoomListMode, false);
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, string.Empty, string.Empty, null, null, null);
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "HideConnectingToServer");
			UITipController.mInstance.SetTipData(UITipController.TipType.LobbyLoadingOneButton, "Connecting to server...", Color.white, "Cancel", string.Empty, btnEventName, null, null);
			this.DisplayCheckConnectionTipsDialog();
		}
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000BC544 File Offset: 0x000BA944
	public void DisplayCheckConnectionTipsDialog()
	{
		base.StartCoroutine(this.EnumeratorDisplayCheckConnectionTipsDialog(60f));
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x000BC558 File Offset: 0x000BA958
	public IEnumerator EnumeratorDisplayCheckConnectionTipsDialog(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		this.DisplayGeneralTipsDialog("Please check the network connection!", Color.red, "OK");
		yield break;
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x000BC57A File Offset: 0x000BA97A
	public void HideGeneralTipsDialog()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x000BC586 File Offset: 0x000BA986
	public void HideInputPasswordDialog()
	{
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x000BC588 File Offset: 0x000BA988
	public void DisplayGeneralTipsDialog(string str1, Color color, string str2)
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideGeneralTipsDialog");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, str1, color, str2, string.Empty, btnEventName, null, null);
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x000BC5B7 File Offset: 0x000BA9B7
	public void CancelInputPasswordToJoin()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x000BC5C4 File Offset: 0x000BA9C4
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

	// Token: 0x06001605 RID: 5637 RVA: 0x000BC67C File Offset: 0x000BAA7C
	public void NextPageOfRoomList()
	{
		this.mChosenRoomName = string.Empty;
		this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Down);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x000BC6A0 File Offset: 0x000BAAA0
	public void PreviousPageOfRommList()
	{
		this.mChosenRoomName = string.Empty;
		this.UpdateRoomInfoListImmediately(GGRoomListOperationType.Up);
		this.mScrollView.GetComponent<UIScrollView>().ResetPosition();
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x000BC6C4 File Offset: 0x000BAAC4
	public bool IsChosenRoomFull()
	{
		return (int)this.mChosenRoomInfo.maxPlayers - this.mChosenRoomInfo.playerCount <= 3;
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x000BC6E6 File Offset: 0x000BAAE6
	public int GetChosenRoomPlayerCount()
	{
		return this.mChosenRoomInfo.playerCount;
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x000BC6F3 File Offset: 0x000BAAF3
	public GGModeType GetCreateRoomMode()
	{
		if (this.mCurrentRoomListMode == GGModeType.Other || this.mChangeCreateRoomMode)
		{
			return (GGModeType)UIUserDataController.GetDefaultMode();
		}
		return this.mCurrentRoomListMode;
	}

	// Token: 0x0400189C RID: 6300
	public static UILobbySelectDirector mInstance;

	// Token: 0x0400189D RID: 6301
	public List<GameObject> mGoRoomInfoList = new List<GameObject>();

	// Token: 0x0400189E RID: 6302
	public List<RoomInfo> mRoomInfoList = new List<RoomInfo>();

	// Token: 0x0400189F RID: 6303
	public List<RoomInfo> mTmpRoomInfoList = new List<RoomInfo>();

	// Token: 0x040018A0 RID: 6304
	private List<GameObject> mGoToDestoryRoomInfoList = new List<GameObject>();

	// Token: 0x040018A1 RID: 6305
	private static int MAXPLAYERS = 20;

	// Token: 0x040018A2 RID: 6306
	private static int MINPLAYERS = 4;

	// Token: 0x040018A3 RID: 6307
	private static int MUTATIONMINPLAYERS = 5;

	// Token: 0x040018A4 RID: 6308
	private static int STRONGHOLDEQUALPLAYERS = 20;

	// Token: 0x040018A5 RID: 6309
	private static int EXPLOSITIONEQUALPLAYERS = 10;

	// Token: 0x040018A6 RID: 6310
	public GGRoomFilter mRoomFilter = new GGRoomFilter();

	// Token: 0x040018A7 RID: 6311
	private string mSearchKey = string.Empty;

	// Token: 0x040018A8 RID: 6312
	private float mOpTimeForUpdateRoomInfoList;

	// Token: 0x040018A9 RID: 6313
	private static float OPIntervalForUpdateRoomInfoList = 1.5f;

	// Token: 0x040018AA RID: 6314
	private float mOpTimeForUpdateConnection;

	// Token: 0x040018AB RID: 6315
	private static float OPIntervalForUpdateConnection = 2f;

	// Token: 0x040018AC RID: 6316
	private float mOpTimeForUpdateWaitingRoomList;

	// Token: 0x040018AD RID: 6317
	private static float OPIntervalForUpdateWaitingRoomList = 0.8f;

	// Token: 0x040018AE RID: 6318
	private float mOpTimeForUpdateJoinRandomCreateButton;

	// Token: 0x040018AF RID: 6319
	private static float OPIntervalForUpdateJoinRandomCreateButton = 1f;

	// Token: 0x040018B0 RID: 6320
	private float mOpTimeForUpdatePlaersNumCreateRoom;

	// Token: 0x040018B1 RID: 6321
	private static float OPIntervalForUpdatePlaersNumCreateRoom = 0.3f;

	// Token: 0x040018B2 RID: 6322
	public GameObject mGoRoomWidget;

	// Token: 0x040018B3 RID: 6323
	public GameObject mGoRoomlist;

	// Token: 0x040018B4 RID: 6324
	public GameObject mGoCreateRoom;

	// Token: 0x040018B5 RID: 6325
	public GameObject mScrollView;

	// Token: 0x040018B6 RID: 6326
	public GameObject mScrollViewBar;

	// Token: 0x040018B7 RID: 6327
	public GameObject mRegionName;

	// Token: 0x040018B8 RID: 6328
	public GameObject mMapSpriteView;

	// Token: 0x040018B9 RID: 6329
	public GameObject mForegroundConnection;

	// Token: 0x040018BA RID: 6330
	public GameObject mNoConnection;

	// Token: 0x040018BB RID: 6331
	public GameObject mMapSpriteViewCreate;

	// Token: 0x040018BC RID: 6332
	public GameObject mEncryptPassword;

	// Token: 0x040018BD RID: 6333
	public GameObject mGoPlayersNum;

	// Token: 0x040018BE RID: 6334
	public GameObject mGoRoomName;

	// Token: 0x040018BF RID: 6335
	public GameObject mGoLobbyLeftCreateRoom;

	// Token: 0x040018C0 RID: 6336
	public GameObject mGoLobbyLeftRoomList;

	// Token: 0x040018C1 RID: 6337
	public GameObject mGoMapName;

	// Token: 0x040018C2 RID: 6338
	public GameObject mGoModeRule;

	// Token: 0x040018C3 RID: 6339
	public GameObject mGoCurrentRoomPageNum;

	// Token: 0x040018C4 RID: 6340
	public GameObject mGoAllRoomPageNum;

	// Token: 0x040018C5 RID: 6341
	public GameObject mGoInputPasswordCreateRoom;

	// Token: 0x040018C6 RID: 6342
	public GameObject mGoWaitingforRoomList;

	// Token: 0x040018C7 RID: 6343
	public GameObject mGoJoinButton;

	// Token: 0x040018C8 RID: 6344
	public GameObject mGoRandomButton;

	// Token: 0x040018C9 RID: 6345
	public GameObject mGoCreateButton;

	// Token: 0x040018CA RID: 6346
	private RoomInfo mChosenRoomInfo;

	// Token: 0x040018CB RID: 6347
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

	// Token: 0x040018CC RID: 6348
	private string[] mConnectingServerContent = new string[]
	{
		"CONNECTING TO SERVER      ",
		"CONNECTING TO SERVER .    ",
		"CONNECTING TO SERVER . .  ",
		"CONNECTING TO SERVER . . ."
	};

	// Token: 0x040018CD RID: 6349
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

	// Token: 0x040018CE RID: 6350
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

	// Token: 0x040018CF RID: 6351
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

	// Token: 0x040018D0 RID: 6352
	private Texture mCurrentMapNameTexture;

	// Token: 0x040018D1 RID: 6353
	private Texture mRoomListCurrentMapNameTexture;

	// Token: 0x040018D2 RID: 6354
	private int mCurrentTextureIndex;

	// Token: 0x040018D3 RID: 6355
	private Dictionary<GGModeType, int[]> mDicKeyModeValveMapIndex = new Dictionary<GGModeType, int[]>();

	// Token: 0x040018D4 RID: 6356
	private int mCurrentMapIndexInMode;

	// Token: 0x040018D5 RID: 6357
	public string mChosenRoomName = string.Empty;

	// Token: 0x040018D6 RID: 6358
	private int mRoomRandomFactor;

	// Token: 0x040018D7 RID: 6359
	private GGModeType mCurrentRoomListMode = GGModeType.Other;

	// Token: 0x040018D8 RID: 6360
	private bool mChangeCreateRoomMode;

	// Token: 0x040018D9 RID: 6361
	public string mRoomName = string.Empty;

	// Token: 0x040018DA RID: 6362
	public string mRoomPassword = string.Empty;

	// Token: 0x040018DB RID: 6363
	public int mRoomPlayersNum = 10;

	// Token: 0x040018DC RID: 6364
	public GGModeType mRoomMode = GGModeType.Other;

	// Token: 0x040018DD RID: 6365
	public string mRoomMapName = string.Empty;

	// Token: 0x040018DE RID: 6366
	public string mRoomSceneName = "MGameScene_1";

	// Token: 0x040018DF RID: 6367
	public bool mEncryption;
}
