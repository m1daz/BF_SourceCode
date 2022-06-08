using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class GGSniperScope : MonoBehaviour
{
	// Token: 0x06000E9C RID: 3740 RVA: 0x0007A3F9 File Offset: 0x000787F9
	private void Awake()
	{
		this.weapScript = base.gameObject.GetComponent<GGWeaponScript>();
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0007A40C File Offset: 0x0007880C
	private void OnGUI()
	{
		if (this.weapScript.aimed)
		{
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)Screen.height * 1.8f / 2f, (float)Screen.height / 2f - (float)Screen.height / 2f, (float)Screen.height * 1.8f, (float)Screen.height), this.scopeTexture);
			for (int i = 0; i < this.objectsToDeactivate.Length; i++)
			{
				this.objectsToDeactivate[i].SetActiveRecursively(false);
			}
		}
		else
		{
			for (int j = 0; j < this.objectsToDeactivate.Length; j++)
			{
				this.objectsToDeactivate[j].SetActiveRecursively(true);
			}
		}
	}

	// Token: 0x04000FF5 RID: 4085
	public Texture2D scopeTexture;

	// Token: 0x04000FF6 RID: 4086
	private GameObject[] objectsToDeactivate;

	// Token: 0x04000FF7 RID: 4087
	private GGWeaponScript weapScript;
}
