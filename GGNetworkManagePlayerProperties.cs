using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000528 RID: 1320
public class GGNetworkManagePlayerProperties : MonoBehaviour
{
	// Token: 0x06002577 RID: 9591 RVA: 0x00116019 File Offset: 0x00114419
	private void Awake()
	{
		GGNetworkManagePlayerProperties.mInstance = this;
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x00116024 File Offset: 0x00114424
	public GGNetworkPlayerProperties GetMainPlayerProperty()
	{
		if (GGNetworkAdapter.mInstance.mMainPlayer == null)
		{
			GGNetworkAdapter.mInstance.mMainPlayer = GameObject.FindWithTag("Player");
		}
		if (GGNetworkAdapter.mInstance.mMainPlayer == null)
		{
			return null;
		}
		return GGNetworkAdapter.mInstance.mMainPlayer.GetComponent<GGNetworkCharacter>().mPlayerProperties;
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x00116088 File Offset: 0x00114488
	public GGNetworkPlayerProperties GetPlayerPropertyByID(int id)
	{
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			if (component.mPlayerProperties.id == id)
			{
				return component.mPlayerProperties;
			}
		}
		return null;
	}

	// Token: 0x0600257A RID: 9594 RVA: 0x00116118 File Offset: 0x00114518
	public List<GGNetworkPlayerProperties> GetPlayerPropertiesList()
	{
		this.mPlayerPropertiesList.Clear();
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			this.mPlayerPropertiesList.Add(component.mPlayerProperties);
		}
		return this.mPlayerPropertiesList;
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x001161A8 File Offset: 0x001145A8
	public List<GGNetworkPlayerProperties> GetRedPlayerPropertiesList()
	{
		this.mRedPlayerPropertiesList.Clear();
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			if (component.mPlayerProperties.team == GGTeamType.red)
			{
				this.mRedPlayerPropertiesList.Add(component.mPlayerProperties);
			}
		}
		this.mRedPlayerPropertiesList.Sort(new Comparison<GGNetworkPlayerProperties>(this.SortCompareByKillNum));
		return this.mRedPlayerPropertiesList;
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x00116260 File Offset: 0x00114660
	public List<GameObject> GetRedLivePlayerObserverCameraList()
	{
		this.mRedLivePlayerObserverCameraList.Clear();
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			if (component.mPlayerProperties.team == GGTeamType.red && !component.mPlayerProperties.isObserver && component.mCharacterWalkState != GGCharacterWalkState.Dead)
			{
				this.mRedLivePlayerObserverCameraList.Add(value.transform.Find("ComunicationObjects/ObserverCameraControl").gameObject);
			}
		}
		return this.mRedLivePlayerObserverCameraList;
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x0011632C File Offset: 0x0011472C
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
		else if (PP1.killNum == PP2.killNum)
		{
			if (PP1.deadNum < PP2.deadNum)
			{
				result = -1;
			}
			else if (PP1.deadNum > PP2.deadNum)
			{
				result = 1;
			}
		}
		return result;
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x001163A8 File Offset: 0x001147A8
	public List<GGNetworkPlayerProperties> GetBluePlayerPropertiesList()
	{
		this.mBluePlayerPropertiesList.Clear();
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			if (component.mPlayerProperties.team == GGTeamType.blue)
			{
				this.mBluePlayerPropertiesList.Add(component.mPlayerProperties);
			}
		}
		this.mBluePlayerPropertiesList.Sort(new Comparison<GGNetworkPlayerProperties>(this.SortCompareByKillNum));
		return this.mBluePlayerPropertiesList;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x00116460 File Offset: 0x00114860
	public List<GameObject> GetBlueLivePlayerObserverCameraList()
	{
		this.mBlueLivePlayerObserverCameraList.Clear();
		Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			if (component.mPlayerProperties.team == GGTeamType.blue && !component.mPlayerProperties.isObserver && component.mCharacterWalkState != GGCharacterWalkState.Dead)
			{
				this.mBlueLivePlayerObserverCameraList.Add(value.transform.Find("ComunicationObjects/ObserverCameraControl").gameObject);
			}
		}
		return this.mBlueLivePlayerObserverCameraList;
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x0011652C File Offset: 0x0011492C
	public List<GGNetworkPlayerProperties> GetOtherPlayerPropertiesList()
	{
		this.mOtherPlayerPropertiesList.Clear();
		PhotonView[] array = UnityEngine.Object.FindObjectsOfType(typeof(PhotonView)) as PhotonView[];
		foreach (PhotonView photonView in array)
		{
			if (!photonView.isMine)
			{
				GGNetworkCharacter component = photonView.GetComponent<GGNetworkCharacter>();
				if (component != null)
				{
					this.mOtherPlayerPropertiesList.Add(component.mPlayerProperties);
				}
			}
		}
		return this.mOtherPlayerPropertiesList;
	}

	// Token: 0x0400261B RID: 9755
	public static GGNetworkManagePlayerProperties mInstance;

	// Token: 0x0400261C RID: 9756
	public List<GGNetworkPlayerProperties> mPlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400261D RID: 9757
	public List<GGNetworkPlayerProperties> mOtherPlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400261E RID: 9758
	public List<GGNetworkPlayerProperties> mRedPlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400261F RID: 9759
	public List<GGNetworkPlayerProperties> mBluePlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x04002620 RID: 9760
	public List<GameObject> mRedLivePlayerObserverCameraList = new List<GameObject>();

	// Token: 0x04002621 RID: 9761
	public List<GameObject> mBlueLivePlayerObserverCameraList = new List<GameObject>();
}
