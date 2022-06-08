using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class UIRoomInfoPrefab : MonoBehaviour
{
	// Token: 0x06001663 RID: 5731 RVA: 0x000BFF71 File Offset: 0x000BE371
	private void Start()
	{
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x000BFF73 File Offset: 0x000BE373
	private void Update()
	{
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x000BFF75 File Offset: 0x000BE375
	public void ChangeMapView(string mapName)
	{
		if (UILobbySelectDirector.mInstance != null)
		{
			UILobbySelectDirector.mInstance.ChangeMapView(mapName);
		}
		if (UILobbySelectEntertainmentDirector.mInstance != null)
		{
			UILobbySelectEntertainmentDirector.mInstance.ChangeMapView(mapName);
		}
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000BFFAD File Offset: 0x000BE3AD
	public void SetChosenRoom(GameObject ChosenRoom)
	{
		if (UILobbySelectDirector.mInstance != null)
		{
			UILobbySelectDirector.mInstance.SetChosenRoom(ChosenRoom);
		}
		if (UILobbySelectEntertainmentDirector.mInstance != null)
		{
			UILobbySelectEntertainmentDirector.mInstance.SetChosenRoom(ChosenRoom);
		}
	}

	// Token: 0x04001938 RID: 6456
	public GameObject mGORoomModeSprite;

	// Token: 0x04001939 RID: 6457
	public GameObject mGOMapNameLabel;

	// Token: 0x0400193A RID: 6458
	public GameObject mGORoomNameLabel;

	// Token: 0x0400193B RID: 6459
	public GameObject mGOPlayersLabel;

	// Token: 0x0400193C RID: 6460
	public GameObject mGOChosenRoomHightLight;

	// Token: 0x0400193D RID: 6461
	public GameObject mGONotJoinableSprite;

	// Token: 0x0400193E RID: 6462
	public GameObject mGOLockSprite;

	// Token: 0x0400193F RID: 6463
	public GameObject mGOHuntingDifficultySprite;
}
