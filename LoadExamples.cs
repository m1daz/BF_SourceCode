using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200036F RID: 879
public class LoadExamples : MonoBehaviour
{
	// Token: 0x06001B5F RID: 7007 RVA: 0x000DBEF4 File Offset: 0x000DA2F4
	public void LoadExample(string level)
	{
		SceneManager.LoadScene(level);
	}
}
