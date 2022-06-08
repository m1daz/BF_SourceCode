using System;
using System.Collections.Generic;
using Photon;

// Token: 0x020004F5 RID: 1269
public class GGNetworkChat : MonoBehaviour
{
	// Token: 0x060023F1 RID: 9201 RVA: 0x0011296A File Offset: 0x00110D6A
	private void Awake()
	{
		GGNetworkChat.mInstance = this;
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x00112972 File Offset: 0x00110D72
	private void Start()
	{
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x00112974 File Offset: 0x00110D74
	public void ChatMessage(GGNetworkChatMessage chatmessage, int playerID = -1)
	{
		chatmessage.displayed = false;
		if (playerID == -1)
		{
			base.photonView.RPC("ChatMessageInRoom", PhotonTargets.All, new object[]
			{
				GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkChatMessage>(chatmessage)
			});
		}
		else
		{
			PhotonPlayer photonPlayerByPlayerID = GGNetworkKit.mInstance.GetPhotonPlayerByPlayerID(playerID);
			base.photonView.RPC("ChatMessageInRoom", photonPlayerByPlayerID, new object[]
			{
				GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkChatMessage>(chatmessage)
			});
			this.ChatMessageInRoom(GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkChatMessage>(chatmessage));
		}
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x001129FB File Offset: 0x00110DFB
	public void WhoKillWhoMessage(GGNetworkKillMessage killmessage)
	{
		killmessage.displayed = false;
		base.photonView.RPC("KillMessageInRoom", PhotonTargets.All, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkKillMessage>(killmessage)
		});
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x00112A29 File Offset: 0x00110E29
	public void SystemMessage(GGNetworkSystemMessage systemmessage)
	{
		systemmessage.displayed = false;
		base.photonView.RPC("SystemMessageInRoom", PhotonTargets.All, new object[]
		{
			GGNetworkObjectSerialize.mInstance.SerializeBinary<GGNetworkSystemMessage>(systemmessage)
		});
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x00112A58 File Offset: 0x00110E58
	public GGNetworkKillMessage PopKillMessage()
	{
		if (this.mKillMessages.Count > 0)
		{
			GGNetworkKillMessage result = this.mKillMessages[0];
			this.mKillMessages.RemoveAt(0);
			return result;
		}
		return null;
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x00112A94 File Offset: 0x00110E94
	public GGNetworkChatMessage PopChatMessage()
	{
		if (this.mChatMessages.Count > 0)
		{
			GGNetworkChatMessage result = this.mChatMessages[0];
			this.mChatMessages.RemoveAt(0);
			return result;
		}
		return null;
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x00112AD0 File Offset: 0x00110ED0
	public GGNetworkSystemAndChatMessage PopSystemAndChatMessage()
	{
		if (this.mSystemAndChatMessage.Count > 0)
		{
			GGNetworkSystemAndChatMessage result = this.mSystemAndChatMessage[0];
			this.mSystemAndChatMessage.RemoveAt(0);
			return result;
		}
		return null;
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x00112B0C File Offset: 0x00110F0C
	public List<GGNetworkKillMessage> GetNetworkKillMessage()
	{
		List<GGNetworkKillMessage> list = new List<GGNetworkKillMessage>();
		bool flag = true;
		foreach (GGNetworkKillMessage ggnetworkKillMessage in this.mKillMessages)
		{
			if (!ggnetworkKillMessage.displayed)
			{
				if (flag)
				{
					flag = false;
				}
				list.Add(ggnetworkKillMessage);
			}
		}
		return list;
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x00112B84 File Offset: 0x00110F84
	public List<GGNetworkSystemAndChatMessage> GetNetworkSystemAndChatMessage()
	{
		List<GGNetworkSystemAndChatMessage> list = new List<GGNetworkSystemAndChatMessage>();
		bool flag = true;
		foreach (GGNetworkSystemAndChatMessage ggnetworkSystemAndChatMessage in this.mSystemAndChatMessage)
		{
			if (!ggnetworkSystemAndChatMessage.displayed)
			{
				if (flag)
				{
					flag = false;
				}
				list.Add(ggnetworkSystemAndChatMessage);
			}
		}
		return list;
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x00112BFC File Offset: 0x00110FFC
	public List<GGNetworkChatMessage> GetChatMessages()
	{
		return this.mChatMessages;
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x00112C04 File Offset: 0x00111004
	[PunRPC]
	private void KillMessageInRoom(byte[] byteArrayKillMessage)
	{
		GGNetworkKillMessage item = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkKillMessage>(byteArrayKillMessage);
		this.mKillMessages.Add(item);
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x00112C2C File Offset: 0x0011102C
	[PunRPC]
	private void ChatMessageInRoom(byte[] byteArrayChatMessage)
	{
		GGNetworkChatMessage ggnetworkChatMessage = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkChatMessage>(byteArrayChatMessage);
		this.mChatMessages.Add(ggnetworkChatMessage);
		if (!this.mBlackList.Contains(ggnetworkChatMessage.name))
		{
			GGNetworkSystemAndChatMessage ggnetworkSystemAndChatMessage = new GGNetworkSystemAndChatMessage();
			ggnetworkSystemAndChatMessage.chatMesaageType = GGChatMessageType.ChatMessage;
			ggnetworkSystemAndChatMessage.team = ggnetworkChatMessage.team;
			ggnetworkSystemAndChatMessage.name = ggnetworkChatMessage.name;
			ggnetworkSystemAndChatMessage.content = ggnetworkChatMessage.content;
			ggnetworkSystemAndChatMessage.displayed = false;
			ggnetworkSystemAndChatMessage.receiverTeam = ggnetworkChatMessage.receiverTeam;
			ggnetworkSystemAndChatMessage.receiverName = ggnetworkChatMessage.receiverName;
			ggnetworkSystemAndChatMessage.chatmessagesubtype = ggnetworkChatMessage.chatmessagesubtype;
			this.mSystemAndChatMessage.Add(ggnetworkSystemAndChatMessage);
			foreach (GGNetworkSystemAndChatMessage ggnetworkSystemAndChatMessage2 in this.mSystemAndChatMessage)
			{
			}
		}
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x00112D18 File Offset: 0x00111118
	[PunRPC]
	private void SystemMessageInRoom(byte[] byteArraySystemMessage)
	{
		GGNetworkSystemMessage ggnetworkSystemMessage = GGNetworkObjectSerialize.mInstance.DeserializeBinary<GGNetworkSystemMessage>(byteArraySystemMessage);
		this.mSystemMessages.Add(ggnetworkSystemMessage);
		GGNetworkSystemAndChatMessage ggnetworkSystemAndChatMessage = new GGNetworkSystemAndChatMessage();
		ggnetworkSystemAndChatMessage.chatMesaageType = GGChatMessageType.SystemMessage;
		ggnetworkSystemAndChatMessage.displayed = false;
		ggnetworkSystemAndChatMessage.color = ggnetworkSystemMessage.color;
		ggnetworkSystemAndChatMessage.content = ggnetworkSystemMessage.content;
		this.mSystemAndChatMessage.Add(ggnetworkSystemAndChatMessage);
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x00112D78 File Offset: 0x00111178
	private void Update()
	{
		if (this.mSystemMessages.Count > this.MAXMESSAGECOUNT)
		{
			this.mSystemMessages.RemoveRange(0, this.REMOVEMESSAGECOUNT);
		}
		if (this.mSystemAndChatMessage.Count > this.MAXMESSAGECOUNT)
		{
			this.mSystemAndChatMessage.RemoveRange(0, this.REMOVEMESSAGECOUNT);
		}
	}

	// Token: 0x0400248B RID: 9355
	public static GGNetworkChat mInstance;

	// Token: 0x0400248C RID: 9356
	private List<GGNetworkChatMessage> mChatMessages = new List<GGNetworkChatMessage>();

	// Token: 0x0400248D RID: 9357
	private List<GGNetworkKillMessage> mKillMessages = new List<GGNetworkKillMessage>();

	// Token: 0x0400248E RID: 9358
	private List<GGNetworkSystemMessage> mSystemMessages = new List<GGNetworkSystemMessage>();

	// Token: 0x0400248F RID: 9359
	private List<GGNetworkSystemAndChatMessage> mSystemAndChatMessage = new List<GGNetworkSystemAndChatMessage>();

	// Token: 0x04002490 RID: 9360
	public List<string> mBlackList = new List<string>();

	// Token: 0x04002491 RID: 9361
	private int MAXMESSAGECOUNT = 100;

	// Token: 0x04002492 RID: 9362
	private int REMOVEMESSAGECOUNT = 20;
}
