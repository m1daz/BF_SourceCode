using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000142 RID: 322
public class OnClickLoadSomething : MonoBehaviour
{
	// Token: 0x0600099C RID: 2460 RVA: 0x00048C28 File Offset: 0x00047028
	public void OnClick()
	{
		OnClickLoadSomething.ResourceTypeOption resourceTypeToLoad = this.ResourceTypeToLoad;
		if (resourceTypeToLoad != OnClickLoadSomething.ResourceTypeOption.Scene)
		{
			if (resourceTypeToLoad == OnClickLoadSomething.ResourceTypeOption.Web)
			{
				Application.OpenURL(this.ResourceToLoad);
			}
		}
		else
		{
			SceneManager.LoadScene(this.ResourceToLoad);
		}
	}

	// Token: 0x04000891 RID: 2193
	public OnClickLoadSomething.ResourceTypeOption ResourceTypeToLoad;

	// Token: 0x04000892 RID: 2194
	public string ResourceToLoad;

	// Token: 0x02000143 RID: 323
	public enum ResourceTypeOption : byte
	{
		// Token: 0x04000894 RID: 2196
		Scene,
		// Token: 0x04000895 RID: 2197
		Web
	}
}
