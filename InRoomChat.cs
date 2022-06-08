using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x0200013A RID: 314
[RequireComponent(typeof(PhotonView))]
public class InRoomChat : Photon.MonoBehaviour
{
	// Token: 0x06000976 RID: 2422 RVA: 0x000480A2 File Offset: 0x000464A2
	public void Start()
	{
		if (this.AlignBottom)
		{
			this.GuiRect.y = (float)Screen.height - this.GuiRect.height;
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x000480CC File Offset: 0x000464CC
	public void OnGUI()
	{
		if (!this.IsVisible || !PhotonNetwork.inRoom)
		{
			return;
		}
		if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
		{
			if (!string.IsNullOrEmpty(this.inputLine))
			{
				base.photonView.RPC("Chat", PhotonTargets.All, new object[]
				{
					this.inputLine
				});
				this.inputLine = string.Empty;
				GUI.FocusControl(string.Empty);
				return;
			}
			GUI.FocusControl("ChatInput");
		}
		GUI.SetNextControlName(string.Empty);
		GUILayout.BeginArea(this.GuiRect);
		this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		for (int i = this.messages.Count - 1; i >= 0; i--)
		{
			GUILayout.Label(this.messages[i], new GUILayoutOption[0]);
		}
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUI.SetNextControlName("ChatInput");
		this.inputLine = GUILayout.TextField(this.inputLine, new GUILayoutOption[0]);
		if (GUILayout.Button("Send", new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false)
		}))
		{
			base.photonView.RPC("Chat", PhotonTargets.All, new object[]
			{
				this.inputLine
			});
			this.inputLine = string.Empty;
			GUI.FocusControl(string.Empty);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00048268 File Offset: 0x00046668
	[PunRPC]
	public void Chat(string newLine, PhotonMessageInfo mi)
	{
		string str = "anonymous";
		if (mi.sender != null)
		{
			if (!string.IsNullOrEmpty(mi.sender.name))
			{
				str = mi.sender.name;
			}
			else
			{
				str = "player " + mi.sender.ID;
			}
		}
		this.messages.Add(str + ": " + newLine);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x000482E2 File Offset: 0x000466E2
	public void AddLine(string newLine)
	{
		this.messages.Add(newLine);
	}

	// Token: 0x04000876 RID: 2166
	public Rect GuiRect = new Rect(0f, 0f, 250f, 300f);

	// Token: 0x04000877 RID: 2167
	public bool IsVisible = true;

	// Token: 0x04000878 RID: 2168
	public bool AlignBottom;

	// Token: 0x04000879 RID: 2169
	public List<string> messages = new List<string>();

	// Token: 0x0400087A RID: 2170
	private string inputLine = string.Empty;

	// Token: 0x0400087B RID: 2171
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x0400087C RID: 2172
	public static readonly string ChatRPC = "Chat";
}
