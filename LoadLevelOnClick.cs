using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200054D RID: 1357
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x06002624 RID: 9764 RVA: 0x0011B39F File Offset: 0x0011979F
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			SceneManager.LoadScene(this.levelName);
		}
	}

	// Token: 0x040026DF RID: 9951
	public string levelName;
}
