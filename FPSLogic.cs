using System;
using UnityEngine;

// Token: 0x020004E0 RID: 1248
public class FPSLogic : MonoBehaviour
{
	// Token: 0x060022BE RID: 8894 RVA: 0x001035B4 File Offset: 0x001019B4
	private void Awake()
	{
		GGNetworkKit.mInstance.CreatePlayer("PlayerPrefab", Vector3.zero);
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			GGNetworkKit.mInstance.CreateSeceneObject("PropPrefab", new Vector3(2f, 0f, 2f), Quaternion.identity);
		}
		if (GGNetworkKit.mInstance.IsMine())
		{
		}
	}

	// Token: 0x060022BF RID: 8895 RVA: 0x0010361B File Offset: 0x00101A1B
	private void Start()
	{
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x0010361D File Offset: 0x00101A1D
	private void Update()
	{
	}

	// Token: 0x060022C1 RID: 8897 RVA: 0x0010361F File Offset: 0x00101A1F
	private void KickedMe()
	{
		this.mDisplayKickedMeWindow = true;
		Debug.Log("KickedMe");
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x00103634 File Offset: 0x00101A34
	public void OnGUI()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(100f);
		if (GUILayout.Button("Main menu", new GUILayoutOption[0]))
		{
			GGNetworkKit.mInstance.LeaveRoom();
			GGNetworkKit.mInstance.LoadLevel("DemoRoom");
		}
		if (GUILayout.Button("Send", new GUILayoutOption[0]))
		{
			GGNetworkChatMessage chatmessage = new GGNetworkChatMessage();
			GGNetworkChat.mInstance.ChatMessage(chatmessage, -1);
		}
		if (GUILayout.Button("Mode", new GUILayoutOption[0]))
		{
		}
		if (GUILayout.Button("Result Ack", new GUILayoutOption[0]))
		{
			GGMessage ggmessage = new GGMessage();
			ggmessage.messageType = GGMessageType.MessageACKModeResult;
			GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
		}
		if (GUILayout.Button("New Round Ack", new GUILayoutOption[0]))
		{
			GGMessage ggmessage2 = new GGMessage();
			ggmessage2.messageType = GGMessageType.MessageNewRound;
			GGNetworkKit.mInstance.SendMessage(ggmessage2, GGTarget.MasterClient);
		}
		this.mOurChatMessages = GUILayout.TextField(this.mOurChatMessages, new GUILayoutOption[]
		{
			GUILayout.Width(200f)
		});
		if (GGNetworkChat.mInstance.GetChatMessages().Count > 0)
		{
			string content = GGNetworkChat.mInstance.GetChatMessages()[GGNetworkChat.mInstance.GetChatMessages().Count - 1].content;
			GUILayout.Label(content, new GUILayoutOption[]
			{
				GUILayout.Width(300f)
			});
		}
		GUILayout.EndHorizontal();
		if (this.mDisplayKickedMeWindow)
		{
			this.rectKickedMeWindow = GUI.Window(0, this.rectKickedMeWindow, new GUI.WindowFunction(this.KickedMeWindowFunc), "You have been kicked!");
		}
		if (this.mDisplayPlayerlistWindow)
		{
			this.rectPlayerlistWindow = GUI.Window(0, this.rectPlayerlistWindow, new GUI.WindowFunction(this.PlayerlistWindowFunc), "Player List");
		}
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x001037F8 File Offset: 0x00101BF8
	private void KickedMeWindowFunc(int windowID)
	{
		if (GUI.Button(new Rect(50f, 150f, 100f, 20f), "OK") && this.mDisplayKickedMeWindow)
		{
			GGNetworkKit.mInstance.LoadLevel("DemoRoom");
		}
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x00103848 File Offset: 0x00101C48
	private void PlayerlistWindowFunc(int windowID)
	{
		if (GGNetworkKit.mInstance.GetPlayerList().Length > 0)
		{
			this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
			foreach (PhotonPlayer photonPlayer in GGNetworkKit.mInstance.GetPlayerList())
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Space(205f);
				GUILayout.Label(string.Concat(new object[]
				{
					"grade: ",
					photonPlayer.customProperties["grade"],
					"   wealth: ",
					photonPlayer.customProperties["wealth"]
				}), new GUILayoutOption[0]);
				if (GUILayout.Button("kick player", new GUILayoutOption[0]))
				{
					GGNetworkKit.mInstance.KickPlayer(photonPlayer);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
		if (GUI.Button(new Rect((float)(Screen.width / 2 - 50), (float)(Screen.height / 2 + 50), 100f, 20f), "Start"))
		{
			this.mDisplayPlayerlistWindow = false;
		}
	}

	// Token: 0x04002389 RID: 9097
	private string mOurChatMessages = string.Empty;

	// Token: 0x0400238A RID: 9098
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x0400238B RID: 9099
	private bool mDisplayKickedMeWindow;

	// Token: 0x0400238C RID: 9100
	private Rect rectKickedMeWindow = new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height / 2 - 100), 200f, 200f);

	// Token: 0x0400238D RID: 9101
	private bool mDisplayPlayerlistWindow = true;

	// Token: 0x0400238E RID: 9102
	private Rect rectPlayerlistWindow = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
}
