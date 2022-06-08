using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class OnClickInstantiate : MonoBehaviour
{
	// Token: 0x06000999 RID: 2457 RVA: 0x00048B18 File Offset: 0x00046F18
	private void OnClick()
	{
		if (!PhotonNetwork.inRoom)
		{
			return;
		}
		int instantiateType = this.InstantiateType;
		if (instantiateType != 0)
		{
			if (instantiateType == 1)
			{
				PhotonNetwork.InstantiateSceneObject(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity, 0, null);
			}
		}
		else
		{
			PhotonNetwork.Instantiate(this.Prefab.name, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
		}
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00048BC0 File Offset: 0x00046FC0
	private void OnGUI()
	{
		if (this.showGui)
		{
			GUILayout.BeginArea(new Rect((float)(Screen.width - 180), 0f, 180f, 50f));
			this.InstantiateType = GUILayout.Toolbar(this.InstantiateType, this.InstantiateTypeNames, new GUILayoutOption[0]);
			GUILayout.EndArea();
		}
	}

	// Token: 0x0400088D RID: 2189
	public GameObject Prefab;

	// Token: 0x0400088E RID: 2190
	public int InstantiateType;

	// Token: 0x0400088F RID: 2191
	private string[] InstantiateTypeNames = new string[]
	{
		"Mine",
		"Scene"
	};

	// Token: 0x04000890 RID: 2192
	public bool showGui;
}
