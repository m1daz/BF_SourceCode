using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x0200015B RID: 347
	public class ChatClient : IPhotonPeerListener
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x0004A724 File Offset: 0x00048B24
		public ChatClient(IChatClientListener listener, ConnectionProtocol protocol = ConnectionProtocol.Udp)
		{
			this.listener = listener;
			this.State = ChatState.Uninitialized;
			this.chatPeer = new ChatPeer(this, protocol);
			this.PublicChannels = new Dictionary<string, ChatChannel>();
			this.PrivateChannels = new Dictionary<string, ChatChannel>();
			this.PublicChannelsUnsubscribing = new HashSet<string>();
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0004A786 File Offset: 0x00048B86
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0004A78E File Offset: 0x00048B8E
		public string NameServerAddress { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0004A797 File Offset: 0x00048B97
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0004A79F File Offset: 0x00048B9F
		public string FrontendAddress { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0004A7A8 File Offset: 0x00048BA8
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0004A7B0 File Offset: 0x00048BB0
		public string ChatRegion
		{
			get
			{
				return this.chatRegion;
			}
			set
			{
				this.chatRegion = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0004A7B9 File Offset: 0x00048BB9
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0004A7C1 File Offset: 0x00048BC1
		public ChatState State { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0004A7CA File Offset: 0x00048BCA
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0004A7D2 File Offset: 0x00048BD2
		public ChatDisconnectCause DisconnectedCause { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0004A7DB File Offset: 0x00048BDB
		public bool CanChat
		{
			get
			{
				return this.State == ChatState.ConnectedToFrontEnd && this.HasPeer;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0004A7F2 File Offset: 0x00048BF2
		public bool CanChatInChannel(string channelName)
		{
			return this.CanChat && this.PublicChannels.ContainsKey(channelName) && !this.PublicChannelsUnsubscribing.Contains(channelName);
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0004A822 File Offset: 0x00048C22
		private bool HasPeer
		{
			get
			{
				return this.chatPeer != null;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0004A830 File Offset: 0x00048C30
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0004A838 File Offset: 0x00048C38
		public string AppVersion { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0004A841 File Offset: 0x00048C41
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0004A849 File Offset: 0x00048C49
		public string AppId { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0004A852 File Offset: 0x00048C52
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x0004A85A File Offset: 0x00048C5A
		public AuthenticationValues AuthValues { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0004A863 File Offset: 0x00048C63
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0004A881 File Offset: 0x00048C81
		public string UserId
		{
			get
			{
				return (this.AuthValues == null) ? null : this.AuthValues.UserId;
			}
			private set
			{
				if (this.AuthValues == null)
				{
					this.AuthValues = new AuthenticationValues();
				}
				this.AuthValues.UserId = value;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0004A8A8 File Offset: 0x00048CA8
		public bool Connect(string appId, string appVersion, AuthenticationValues authValues)
		{
			this.chatPeer.TimePingInterval = 3000;
			this.DisconnectedCause = ChatDisconnectCause.None;
			if (authValues == null)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Connect failed: no authentication values specified");
				}
				return false;
			}
			this.AuthValues = authValues;
			if (this.AuthValues.UserId == null || this.AuthValues.UserId == string.Empty)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Connect failed: no UserId specified in authentication values");
				}
				return false;
			}
			this.AppId = appId;
			this.AppVersion = appVersion;
			this.didAuthenticate = false;
			this.msDeltaForServiceCalls = 100;
			this.chatPeer.QuickResendAttempts = 2;
			this.chatPeer.SentCountAllowance = 7;
			this.PublicChannels.Clear();
			this.PrivateChannels.Clear();
			this.PublicChannelsUnsubscribing.Clear();
			this.NameServerAddress = this.chatPeer.NameServerAddress;
			bool flag = this.chatPeer.Connect();
			if (flag)
			{
				this.State = ChatState.ConnectingToNameServer;
			}
			return flag;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0004A9C8 File Offset: 0x00048DC8
		public void Service()
		{
			if (this.HasPeer && (Environment.TickCount - this.msTimestampOfLastServiceCall > this.msDeltaForServiceCalls || this.msTimestampOfLastServiceCall == 0))
			{
				this.msTimestampOfLastServiceCall = Environment.TickCount;
				this.chatPeer.Service();
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0004AA18 File Offset: 0x00048E18
		public void Disconnect()
		{
			if (this.HasPeer && this.chatPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.chatPeer.Disconnect();
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0004AA40 File Offset: 0x00048E40
		public void StopThread()
		{
			if (this.HasPeer)
			{
				this.chatPeer.StopThread();
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0004AA58 File Offset: 0x00048E58
		public bool Subscribe(string[] channels)
		{
			return this.Subscribe(channels, 0);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0004AA64 File Offset: 0x00048E64
		public bool Subscribe(string[] channels, int messagesFromHistory)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Subscribe called while not connected to front end server.");
				}
				return false;
			}
			if (channels == null || channels.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Subscribe can't be called for empty or null channels-list.");
				}
				return false;
			}
			return this.SendChannelOperation(channels, 0, messagesFromHistory);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0004AAD4 File Offset: 0x00048ED4
		public bool Unsubscribe(string[] channels)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Unsubscribe called while not connected to front end server.");
				}
				return false;
			}
			if (channels == null || channels.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Unsubscribe can't be called for empty or null channels-list.");
				}
				return false;
			}
			foreach (string item in channels)
			{
				this.PublicChannelsUnsubscribing.Add(item);
			}
			return this.SendChannelOperation(channels, 1, 0);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0004AB68 File Offset: 0x00048F68
		public bool PublishMessage(string channelName, object message)
		{
			return this.publishMessage(channelName, message, true);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0004AB73 File Offset: 0x00048F73
		internal bool PublishMessageUnreliable(string channelName, object message)
		{
			return this.publishMessage(channelName, message, false);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0004AB80 File Offset: 0x00048F80
		private bool publishMessage(string channelName, object message, bool reliable)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "PublishMessage called while not connected to front end server.");
				}
				return false;
			}
			if (string.IsNullOrEmpty(channelName) || message == null)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "PublishMessage parameters must be non-null and not empty.");
				}
				return false;
			}
			Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>
			{
				{
					1,
					channelName
				},
				{
					3,
					message
				}
			};
			return this.chatPeer.OpCustom(2, customOpParameters, reliable);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0004AC0D File Offset: 0x0004900D
		public bool SendPrivateMessage(string target, object message)
		{
			return this.SendPrivateMessage(target, message, false);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0004AC18 File Offset: 0x00049018
		public bool SendPrivateMessage(string target, object message, bool encrypt)
		{
			return this.sendPrivateMessage(target, message, encrypt, true);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004AC24 File Offset: 0x00049024
		internal bool SendPrivateMessageUnreliable(string target, object message, bool encrypt)
		{
			return this.sendPrivateMessage(target, message, encrypt, false);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0004AC30 File Offset: 0x00049030
		private bool sendPrivateMessage(string target, object message, bool encrypt, bool reliable)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "SendPrivateMessage called while not connected to front end server.");
				}
				return false;
			}
			if (string.IsNullOrEmpty(target) || message == null)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "SendPrivateMessage parameters must be non-null and not empty.");
				}
				return false;
			}
			Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>
			{
				{
					225,
					target
				},
				{
					3,
					message
				}
			};
			return this.chatPeer.OpCustom(3, customOpParameters, reliable, 0, encrypt);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0004ACC8 File Offset: 0x000490C8
		private bool SetOnlineStatus(int status, object message, bool skipMessage)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "SetOnlineStatus called while not connected to front end server.");
				}
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>
			{
				{
					10,
					status
				}
			};
			if (skipMessage)
			{
				dictionary[12] = true;
			}
			else
			{
				dictionary[3] = message;
			}
			return this.chatPeer.OpCustom(5, dictionary, true);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0004AD44 File Offset: 0x00049144
		public bool SetOnlineStatus(int status)
		{
			return this.SetOnlineStatus(status, null, true);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0004AD4F File Offset: 0x0004914F
		public bool SetOnlineStatus(int status, object message)
		{
			return this.SetOnlineStatus(status, message, false);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0004AD5C File Offset: 0x0004915C
		public bool AddFriends(string[] friends)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "AddFriends called while not connected to front end server.");
				}
				return false;
			}
			if (friends == null || friends.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "AddFriends can't be called for empty or null list.");
				}
				return false;
			}
			if (friends.Length > 1024)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Concat(new object[]
					{
						"AddFriends max list size exceeded: ",
						friends.Length,
						" > ",
						1024
					}));
				}
				return false;
			}
			Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>
			{
				{
					11,
					friends
				}
			};
			return this.chatPeer.OpCustom(6, customOpParameters, true);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0004AE3C File Offset: 0x0004923C
		public bool RemoveFriends(string[] friends)
		{
			if (!this.CanChat)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "RemoveFriends called while not connected to front end server.");
				}
				return false;
			}
			if (friends == null || friends.Length == 0)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "RemoveFriends can't be called for empty or null list.");
				}
				return false;
			}
			if (friends.Length > 1024)
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, string.Concat(new object[]
					{
						"RemoveFriends max list size exceeded: ",
						friends.Length,
						" > ",
						1024
					}));
				}
				return false;
			}
			Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>
			{
				{
					11,
					friends
				}
			};
			return this.chatPeer.OpCustom(7, customOpParameters, true);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0004AF19 File Offset: 0x00049319
		public string GetPrivateChannelNameByUser(string userName)
		{
			return string.Format("{0}:{1}", this.UserId, userName);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0004AF2C File Offset: 0x0004932C
		public bool TryGetChannel(string channelName, bool isPrivate, out ChatChannel channel)
		{
			if (!isPrivate)
			{
				return this.PublicChannels.TryGetValue(channelName, out channel);
			}
			return this.PrivateChannels.TryGetValue(channelName, out channel);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0004AF50 File Offset: 0x00049350
		public bool TryGetChannel(string channelName, out ChatChannel channel)
		{
			bool flag = this.PublicChannels.TryGetValue(channelName, out channel);
			return flag || this.PrivateChannels.TryGetValue(channelName, out channel);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0004AF84 File Offset: 0x00049384
		public void SendAcksOnly()
		{
			if (this.chatPeer != null)
			{
				this.chatPeer.SendAcksOnly();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0004AFAB File Offset: 0x000493AB
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0004AF9D File Offset: 0x0004939D
		public DebugLevel DebugOut
		{
			get
			{
				return this.chatPeer.DebugOut;
			}
			set
			{
				this.chatPeer.DebugOut = value;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0004AFB8 File Offset: 0x000493B8
		void IPhotonPeerListener.DebugReturn(DebugLevel level, string message)
		{
			this.listener.DebugReturn(level, message);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0004AFC8 File Offset: 0x000493C8
		void IPhotonPeerListener.OnEvent(EventData eventData)
		{
			switch (eventData.Code)
			{
			case 0:
				this.HandleChatMessagesEvent(eventData);
				break;
			case 2:
				this.HandlePrivateMessageEvent(eventData);
				break;
			case 4:
				this.HandleStatusUpdate(eventData);
				break;
			case 5:
				this.HandleSubscribeEvent(eventData);
				break;
			case 6:
				this.HandleUnsubscribeEvent(eventData);
				break;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0004B040 File Offset: 0x00049440
		void IPhotonPeerListener.OnOperationResponse(OperationResponse operationResponse)
		{
			byte operationCode = operationResponse.OperationCode;
			switch (operationCode)
			{
			case 0:
			case 1:
			case 2:
			case 3:
				break;
			default:
				if (operationCode == 230)
				{
					this.HandleAuthResponse(operationResponse);
					return;
				}
				break;
			}
			if (operationResponse.ReturnCode != 0 && this.DebugOut >= DebugLevel.ERROR)
			{
				if (operationResponse.ReturnCode == -2)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, string.Format("Chat Operation {0} unknown on server. Check your AppId and make sure it's for a Chat application.", operationResponse.OperationCode));
				}
				else
				{
					this.listener.DebugReturn(DebugLevel.ERROR, string.Format("Chat Operation {0} failed (Code: {1}). Debug Message: {2}", operationResponse.OperationCode, operationResponse.ReturnCode, operationResponse.DebugMessage));
				}
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0004B108 File Offset: 0x00049508
		void IPhotonPeerListener.OnStatusChanged(StatusCode statusCode)
		{
			if (statusCode != StatusCode.Connect)
			{
				if (statusCode != StatusCode.Disconnect)
				{
					if (statusCode != StatusCode.EncryptionEstablished)
					{
						if (statusCode == StatusCode.EncryptionFailedToEstablish)
						{
							this.State = ChatState.Disconnecting;
							this.chatPeer.Disconnect();
						}
					}
					else if (!this.didAuthenticate)
					{
						this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
						if (!this.didAuthenticate && this.DebugOut >= DebugLevel.ERROR)
						{
							((IPhotonPeerListener)this).DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + this.State);
						}
					}
				}
				else if (this.State == ChatState.Authenticated)
				{
					this.ConnectToFrontEnd();
				}
				else
				{
					this.State = ChatState.Disconnected;
					this.listener.OnChatStateChange(ChatState.Disconnected);
					this.listener.OnDisconnected();
				}
			}
			else
			{
				if (!this.chatPeer.IsProtocolSecure)
				{
					this.chatPeer.EstablishEncryption();
				}
				else if (!this.didAuthenticate)
				{
					this.didAuthenticate = this.chatPeer.AuthenticateOnNameServer(this.AppId, this.AppVersion, this.chatRegion, this.AuthValues);
					if (!this.didAuthenticate && this.DebugOut >= DebugLevel.ERROR)
					{
						((IPhotonPeerListener)this).DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, AuthValues and if you're connected. State: " + this.State);
					}
				}
				if (this.State == ChatState.ConnectingToNameServer)
				{
					this.State = ChatState.ConnectedToNameServer;
					this.listener.OnChatStateChange(this.State);
				}
				else if (this.State == ChatState.ConnectingToFrontEnd)
				{
					this.AuthenticateOnFrontEnd();
				}
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0004B2D0 File Offset: 0x000496D0
		private bool SendChannelOperation(string[] channels, byte operation, int historyLength)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>
			{
				{
					0,
					channels
				}
			};
			if (historyLength != 0)
			{
				dictionary.Add(14, historyLength);
			}
			return this.chatPeer.OpCustom(operation, dictionary, true);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0004B310 File Offset: 0x00049710
		private void HandlePrivateMessageEvent(EventData eventData)
		{
			object message = eventData.Parameters[3];
			string text = (string)eventData.Parameters[5];
			string privateChannelNameByUser;
			if (this.UserId != null && this.UserId.Equals(text))
			{
				string userName = (string)eventData.Parameters[225];
				privateChannelNameByUser = this.GetPrivateChannelNameByUser(userName);
			}
			else
			{
				privateChannelNameByUser = this.GetPrivateChannelNameByUser(text);
			}
			ChatChannel chatChannel;
			if (!this.PrivateChannels.TryGetValue(privateChannelNameByUser, out chatChannel))
			{
				chatChannel = new ChatChannel(privateChannelNameByUser);
				chatChannel.IsPrivate = true;
				chatChannel.MessageLimit = this.MessageLimit;
				this.PrivateChannels.Add(chatChannel.Name, chatChannel);
			}
			chatChannel.Add(text, message);
			this.listener.OnPrivateMessage(text, message, privateChannelNameByUser);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0004B3E0 File Offset: 0x000497E0
		private void HandleChatMessagesEvent(EventData eventData)
		{
			object[] messages = (object[])eventData.Parameters[2];
			string[] senders = (string[])eventData.Parameters[4];
			string text = (string)eventData.Parameters[1];
			ChatChannel chatChannel;
			if (!this.PublicChannels.TryGetValue(text, out chatChannel))
			{
				if (this.DebugOut >= DebugLevel.WARNING)
				{
					this.listener.DebugReturn(DebugLevel.WARNING, "Channel " + text + " for incoming message event not found.");
				}
				return;
			}
			chatChannel.Add(senders, messages);
			this.listener.OnGetMessages(text, senders, messages);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0004B478 File Offset: 0x00049878
		private void HandleSubscribeEvent(EventData eventData)
		{
			string[] array = (string[])eventData.Parameters[0];
			bool[] array2 = (bool[])eventData.Parameters[15];
			for (int i = 0; i < array.Length; i++)
			{
				if (array2[i])
				{
					string text = array[i];
					if (!this.PublicChannels.ContainsKey(text))
					{
						ChatChannel chatChannel = new ChatChannel(text);
						chatChannel.MessageLimit = this.MessageLimit;
						this.PublicChannels.Add(chatChannel.Name, chatChannel);
					}
				}
			}
			this.listener.OnSubscribed(array, array2);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0004B514 File Offset: 0x00049914
		private void HandleUnsubscribeEvent(EventData eventData)
		{
			foreach (string text in (string[])eventData[0])
			{
				this.PublicChannels.Remove(text);
				this.PublicChannelsUnsubscribing.Remove(text);
			}
			string[] array;
			this.listener.OnUnsubscribed(array);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0004B56C File Offset: 0x0004996C
		private void HandleAuthResponse(OperationResponse operationResponse)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				this.listener.DebugReturn(DebugLevel.INFO, operationResponse.ToStringFull() + " on: " + this.chatPeer.NameServerAddress);
			}
			if (operationResponse.ReturnCode == 0)
			{
				if (this.State == ChatState.ConnectedToNameServer)
				{
					this.State = ChatState.Authenticated;
					this.listener.OnChatStateChange(this.State);
					if (operationResponse.Parameters.ContainsKey(221))
					{
						if (this.AuthValues == null)
						{
							this.AuthValues = new AuthenticationValues();
						}
						this.AuthValues.Token = (operationResponse[221] as string);
						this.FrontendAddress = (string)operationResponse[230];
						this.chatPeer.Disconnect();
					}
					else if (this.DebugOut >= DebugLevel.ERROR)
					{
						this.listener.DebugReturn(DebugLevel.ERROR, "No secret in authentication response.");
					}
				}
				else if (this.State == ChatState.ConnectingToFrontEnd)
				{
					this.msDeltaForServiceCalls *= 4;
					this.State = ChatState.ConnectedToFrontEnd;
					this.listener.OnChatStateChange(this.State);
					this.listener.OnConnected();
				}
			}
			else
			{
				short returnCode = operationResponse.ReturnCode;
				switch (returnCode)
				{
				case 32755:
					this.DisconnectedCause = ChatDisconnectCause.CustomAuthenticationFailed;
					break;
				case 32756:
					this.DisconnectedCause = ChatDisconnectCause.InvalidRegion;
					break;
				case 32757:
					this.DisconnectedCause = ChatDisconnectCause.MaxCcuReached;
					break;
				default:
					if (returnCode != -3)
					{
						if (returnCode == 32767)
						{
							this.DisconnectedCause = ChatDisconnectCause.InvalidAuthentication;
						}
					}
					else
					{
						this.DisconnectedCause = ChatDisconnectCause.OperationNotAllowedInCurrentState;
					}
					break;
				}
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Authentication request error: " + operationResponse.ReturnCode + ". Disconnecting.");
				}
				this.State = ChatState.Disconnecting;
				this.chatPeer.Disconnect();
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0004B768 File Offset: 0x00049B68
		private void HandleStatusUpdate(EventData eventData)
		{
			string user = (string)eventData.Parameters[5];
			int status = (int)eventData.Parameters[10];
			object message = null;
			bool flag = eventData.Parameters.ContainsKey(3);
			if (flag)
			{
				message = eventData.Parameters[3];
			}
			this.listener.OnStatusUpdate(user, status, flag, message);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0004B7CC File Offset: 0x00049BCC
		private void ConnectToFrontEnd()
		{
			this.State = ChatState.ConnectingToFrontEnd;
			if (this.DebugOut >= DebugLevel.INFO)
			{
				this.listener.DebugReturn(DebugLevel.INFO, "Connecting to frontend " + this.FrontendAddress);
			}
			this.chatPeer.Connect(this.FrontendAddress, "chat");
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0004B820 File Offset: 0x00049C20
		private bool AuthenticateOnFrontEnd()
		{
			if (this.AuthValues == null)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Can't authenticate on front end server. Authentication Values are not set");
				}
				return false;
			}
			if (this.AuthValues.Token == null || this.AuthValues.Token == string.Empty)
			{
				if (this.DebugOut >= DebugLevel.ERROR)
				{
					this.listener.DebugReturn(DebugLevel.ERROR, "Can't authenticate on front end server. Secret is not set");
				}
				return false;
			}
			Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>
			{
				{
					221,
					this.AuthValues.Token
				}
			};
			return this.chatPeer.OpCustom(230, customOpParameters, true);
		}

		// Token: 0x040008CB RID: 2251
		private const int FriendRequestListMax = 1024;

		// Token: 0x040008CE RID: 2254
		private string chatRegion = "EU";

		// Token: 0x040008D4 RID: 2260
		public int MessageLimit;

		// Token: 0x040008D5 RID: 2261
		public readonly Dictionary<string, ChatChannel> PublicChannels;

		// Token: 0x040008D6 RID: 2262
		public readonly Dictionary<string, ChatChannel> PrivateChannels;

		// Token: 0x040008D7 RID: 2263
		private readonly HashSet<string> PublicChannelsUnsubscribing;

		// Token: 0x040008D8 RID: 2264
		private readonly IChatClientListener listener;

		// Token: 0x040008D9 RID: 2265
		public ChatPeer chatPeer;

		// Token: 0x040008DA RID: 2266
		private bool didAuthenticate;

		// Token: 0x040008DB RID: 2267
		private int msDeltaForServiceCalls = 50;

		// Token: 0x040008DC RID: 2268
		private int msTimestampOfLastServiceCall;

		// Token: 0x040008DD RID: 2269
		private const string ChatAppName = "chat";
	}
}
