using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Photon
{
	// Token: 0x0200010B RID: 267
	public class PunBehaviour : MonoBehaviour, IPunCallbacks
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x00040DA1 File Offset: 0x0003F1A1
		public virtual void OnConnectedToPhoton()
		{
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00040DA3 File Offset: 0x0003F1A3
		public virtual void OnLeftRoom()
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00040DA5 File Offset: 0x0003F1A5
		public virtual void OnMasterClientSwitched(PhotonPlayer newMasterClient)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00040DA7 File Offset: 0x0003F1A7
		public virtual void OnPhotonCreateRoomFailed(object[] codeAndMsg)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00040DA9 File Offset: 0x0003F1A9
		public virtual void OnPhotonJoinRoomFailed(object[] codeAndMsg)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00040DAB File Offset: 0x0003F1AB
		public virtual void OnCreatedRoom()
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00040DAD File Offset: 0x0003F1AD
		public virtual void OnJoinedLobby()
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00040DAF File Offset: 0x0003F1AF
		public virtual void OnLeftLobby()
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00040DB1 File Offset: 0x0003F1B1
		public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00040DB3 File Offset: 0x0003F1B3
		public virtual void OnDisconnectedFromPhoton()
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00040DB5 File Offset: 0x0003F1B5
		public virtual void OnConnectionFail(DisconnectCause cause)
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00040DB7 File Offset: 0x0003F1B7
		public virtual void OnPhotonInstantiate(PhotonMessageInfo info)
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00040DB9 File Offset: 0x0003F1B9
		public virtual void OnReceivedRoomListUpdate()
		{
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00040DBB File Offset: 0x0003F1BB
		public virtual void OnJoinedRoom()
		{
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00040DBD File Offset: 0x0003F1BD
		public virtual void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00040DBF File Offset: 0x0003F1BF
		public virtual void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
		{
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00040DC1 File Offset: 0x0003F1C1
		public virtual void OnPhotonRandomJoinFailed(object[] codeAndMsg)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00040DC3 File Offset: 0x0003F1C3
		public virtual void OnConnectedToMaster()
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00040DC5 File Offset: 0x0003F1C5
		public virtual void OnPhotonMaxCccuReached()
		{
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00040DC7 File Offset: 0x0003F1C7
		public virtual void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00040DC9 File Offset: 0x0003F1C9
		public virtual void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00040DCB File Offset: 0x0003F1CB
		public virtual void OnUpdatedFriendList()
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00040DCD File Offset: 0x0003F1CD
		public virtual void OnCustomAuthenticationFailed(string debugMessage)
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00040DCF File Offset: 0x0003F1CF
		public virtual void OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00040DD1 File Offset: 0x0003F1D1
		public virtual void OnWebRpcResponse(OperationResponse response)
		{
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00040DD3 File Offset: 0x0003F1D3
		public virtual void OnOwnershipRequest(object[] viewAndPlayer)
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00040DD5 File Offset: 0x0003F1D5
		public virtual void OnLobbyStatisticsUpdate()
		{
		}
	}
}
