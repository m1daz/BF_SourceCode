using System;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public class StartMenu : MonoBehaviour
{
	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x000F0427 File Offset: 0x000EE827
	// (set) Token: 0x06001FA9 RID: 8105 RVA: 0x000F042F File Offset: 0x000EE82F
	public Transform CurrentMenuRoot
	{
		get
		{
			return this.currentMenuRoot;
		}
		set
		{
			this.currentMenuRoot = value;
		}
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x000F0438 File Offset: 0x000EE838
	private void Start()
	{
		this.CurrentMenuRoot = this.itemsTree;
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x000F0448 File Offset: 0x000EE848
	private void OnGUI()
	{
		SampleUI.ApplyVirtualScreen();
		GUILayout.BeginArea(this.screenRect);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Space(this.sideBorder);
		if (this.CurrentMenuRoot)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Space(15f);
			GUILayout.Label(this.CurrentMenuRoot.name, this.titleStyle, new GUILayoutOption[0]);
			for (int i = 0; i < this.CurrentMenuRoot.childCount; i++)
			{
				Transform child = this.CurrentMenuRoot.GetChild(i);
				if (GUILayout.Button(child.name, new GUILayoutOption[]
				{
					GUILayout.Height(this.buttonHeight)
				}))
				{
					MenuNode component = child.GetComponent<MenuNode>();
					if (component && component.sceneName != null && component.sceneName.Length > 0)
					{
						Application.LoadLevel(component.sceneName);
					}
					else if (child.childCount > 0)
					{
						this.CurrentMenuRoot = child;
					}
				}
				GUILayout.Space(5f);
			}
			GUILayout.FlexibleSpace();
			if (this.CurrentMenuRoot != this.itemsTree && this.CurrentMenuRoot.parent)
			{
				if (GUILayout.Button("<< BACK <<", new GUILayoutOption[]
				{
					GUILayout.Height(this.buttonHeight)
				}))
				{
					this.CurrentMenuRoot = this.CurrentMenuRoot.parent;
				}
				GUILayout.Space(15f);
			}
			GUILayout.EndVertical();
		}
		GUILayout.Space(this.sideBorder);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x040020B6 RID: 8374
	public GUIStyle titleStyle;

	// Token: 0x040020B7 RID: 8375
	public GUIStyle buttonStyle;

	// Token: 0x040020B8 RID: 8376
	public float buttonHeight = 80f;

	// Token: 0x040020B9 RID: 8377
	public Transform itemsTree;

	// Token: 0x040020BA RID: 8378
	private Transform currentMenuRoot;

	// Token: 0x040020BB RID: 8379
	private Rect screenRect = new Rect(0f, 0f, SampleUI.VirtualScreenWidth, SampleUI.VirtualScreenHeight);

	// Token: 0x040020BC RID: 8380
	public float menuWidth = 450f;

	// Token: 0x040020BD RID: 8381
	public float sideBorder = 30f;
}
