using System;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class SampleUI : MonoBehaviour
{
	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x000F0160 File Offset: 0x000EE560
	// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x000F0168 File Offset: 0x000EE568
	public string StatusText
	{
		get
		{
			return this.statusText;
		}
		set
		{
			this.statusText = value;
		}
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x000F0174 File Offset: 0x000EE574
	private void Awake()
	{
		this.titleStyle = new GUIStyle(this.skin.label);
		this.titleStyle.alignment = TextAnchor.MiddleCenter;
		this.titleStyle.normal.textColor = this.titleColor;
		this.statusStyle = new GUIStyle(this.skin.label);
		this.statusStyle.alignment = TextAnchor.LowerCenter;
		this.helpStyle = new GUIStyle(this.skin.label);
		this.helpStyle.alignment = TextAnchor.UpperLeft;
		this.helpStyle.padding.left = 5;
		this.helpStyle.padding.right = 5;
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x000F021F File Offset: 0x000EE61F
	public static void ApplyVirtualScreen()
	{
		GUI.matrix = Matrix4x4.Scale(new Vector3((float)Screen.width / SampleUI.VirtualScreenWidth, (float)Screen.height / SampleUI.VirtualScreenHeight, 1f));
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x000F0250 File Offset: 0x000EE650
	protected virtual void OnGUI()
	{
		if (this.skin != null)
		{
			GUI.skin = this.skin;
		}
		SampleUI.ApplyVirtualScreen();
		GUI.Box(this.topBarRect, string.Empty);
		if (GUI.Button(this.backButtonRect, "Back"))
		{
			Application.LoadLevel("start");
		}
		GUI.Label(this.titleRect, "FingerGestures - " + base.name, this.titleStyle);
		if (this.showStatusText)
		{
			GUI.Label(this.statusTextRect, this.statusText, this.statusStyle);
		}
		if (this.helpText.Length > 0 && this.showHelpButton && !this.showHelp && GUI.Button(this.helpButtonRect, "Help"))
		{
			this.showHelp = true;
		}
		if (this.showHelp)
		{
			GUI.Box(this.helpRect, "Help");
			GUILayout.BeginArea(this.helpRect);
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Space(25f);
			GUILayout.Label(this.helpText, this.helpStyle, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Close", new GUILayoutOption[]
			{
				GUILayout.Height(40f)
			}))
			{
				this.showHelp = false;
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}

	// Token: 0x040020A4 RID: 8356
	public GUISkin skin;

	// Token: 0x040020A5 RID: 8357
	public Color titleColor = Color.white;

	// Token: 0x040020A6 RID: 8358
	private GUIStyle titleStyle;

	// Token: 0x040020A7 RID: 8359
	private GUIStyle statusStyle;

	// Token: 0x040020A8 RID: 8360
	private GUIStyle helpStyle;

	// Token: 0x040020A9 RID: 8361
	private Rect topBarRect = new Rect(0f, -4f, 600f, 56f);

	// Token: 0x040020AA RID: 8362
	private Rect backButtonRect = new Rect(5f, 2f, 80f, 46f);

	// Token: 0x040020AB RID: 8363
	private Rect titleRect = new Rect(100f, 2f, 400f, 46f);

	// Token: 0x040020AC RID: 8364
	private Rect helpButtonRect = new Rect(515f, 2f, 80f, 46f);

	// Token: 0x040020AD RID: 8365
	private Rect statusTextRect = new Rect(30f, 336f, 540f, 60f);

	// Token: 0x040020AE RID: 8366
	private Rect helpRect = new Rect(50f, 60f, 500f, 300f);

	// Token: 0x040020AF RID: 8367
	private string statusText = string.Empty;

	// Token: 0x040020B0 RID: 8368
	public bool showStatusText = true;

	// Token: 0x040020B1 RID: 8369
	public string helpText = string.Empty;

	// Token: 0x040020B2 RID: 8370
	public bool showHelpButton = true;

	// Token: 0x040020B3 RID: 8371
	public bool showHelp;

	// Token: 0x040020B4 RID: 8372
	public static readonly float VirtualScreenWidth = 600f;

	// Token: 0x040020B5 RID: 8373
	public static readonly float VirtualScreenHeight = 400f;
}
