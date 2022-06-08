using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x02000160 RID: 352
	public class ChatPeer : PhotonPeer
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x0004B8E8 File Offset: 0x00049CE8
		public ChatPeer(IPhotonPeerListener listener, ConnectionProtocol protocol) : base(listener, protocol)
		{
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0004B8F2 File Offset: 0x00049CF2
		public string NameServerAddress
		{
			get
			{
				return this.GetNameServerAddress();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0004B8FA File Offset: 0x00049CFA
		internal virtual bool IsProtocolSecure
		{
			get
			{
				return base.UsedProtocol == ConnectionProtocol.WebSocketSecure;
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0004B908 File Offset: 0x00049D08
		[Conditional("UNITY")]
		private void ConfigUnitySockets()
		{
			Type type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
			if (type == null)
			{
				type = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
			}
			if (type != null)
			{
				this.SocketImplementationConfig[ConnectionProtocol.WebSocket] = type;
				this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = type;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0004B954 File Offset: 0x00049D54
		private string GetNameServerAddress()
		{
			int num = 0;
			ChatPeer.ProtocolToNameServerPort.TryGetValue(base.TransportProtocol, out num);
			switch (base.TransportProtocol)
			{
			case ConnectionProtocol.Udp:
			case ConnectionProtocol.Tcp:
				return string.Format("{0}:{1}", "ns.exitgames.com", num);
			case ConnectionProtocol.WebSocket:
				return string.Format("ws://{0}:{1}", "ns.exitgames.com", num);
			case ConnectionProtocol.WebSocketSecure:
				return string.Format("wss://{0}:{1}", "ns.exitgames.com", num);
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0004B9E7 File Offset: 0x00049DE7
		public bool Connect()
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "Connecting to nameserver " + this.NameServerAddress);
			}
			return this.Connect(this.NameServerAddress, "NameServer");
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0004BA24 File Offset: 0x00049E24
		public bool AuthenticateOnNameServer(string appId, string appVersion, string region, AuthenticationValues authValues)
		{
			if (this.DebugOut >= DebugLevel.INFO)
			{
				base.Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary[220] = appVersion;
			dictionary[224] = appId;
			dictionary[210] = region;
			if (authValues != null)
			{
				if (!string.IsNullOrEmpty(authValues.UserId))
				{
					dictionary[225] = authValues.UserId;
				}
				if (authValues != null && authValues.AuthType != CustomAuthenticationType.None)
				{
					dictionary[217] = (byte)authValues.AuthType;
					if (!string.IsNullOrEmpty(authValues.Token))
					{
						dictionary[221] = authValues.Token;
					}
					else
					{
						if (!string.IsNullOrEmpty(authValues.AuthGetParameters))
						{
							dictionary[216] = authValues.AuthGetParameters;
						}
						if (authValues.AuthPostData != null)
						{
							dictionary[214] = authValues.AuthPostData;
						}
					}
				}
			}
			return this.OpCustom(230, dictionary, true, 0, base.IsEncryptionAvailable);
		}

		// Token: 0x0400090A RID: 2314
		public const string NameServerHost = "ns.exitgames.com";

		// Token: 0x0400090B RID: 2315
		public const string NameServerHttp = "http://ns.exitgamescloud.com:80/photon/n";

		// Token: 0x0400090C RID: 2316
		private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>
		{
			{
				ConnectionProtocol.Udp,
				5058
			},
			{
				ConnectionProtocol.Tcp,
				4533
			},
			{
				ConnectionProtocol.WebSocket,
				9093
			},
			{
				ConnectionProtocol.WebSocketSecure,
				19093
			}
		};
	}
}
