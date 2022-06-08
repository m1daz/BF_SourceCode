using System;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class GGEnemyGenerateTriggerControl : MonoBehaviour
{
	// Token: 0x0600116D RID: 4461 RVA: 0x0009BA00 File Offset: 0x00099E00
	private void Start()
	{
		this.SingleModeEnemyManager = GameObject.Find("SingleModeEnemyManager");
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0009BA12 File Offset: 0x00099E12
	private void Update()
	{
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0009BA14 File Offset: 0x00099E14
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			string[] array = base.gameObject.name.Split(new char[]
			{
				'_'
			});
			this.SingleModeEnemyManager.SendMessage("GenerateEnemyByTrigger", int.Parse(array[1]), SendMessageOptions.DontRequireReceiver);
			other.gameObject.SendMessage("EnemyGenerate", SendMessageOptions.DontRequireReceiver);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400140A RID: 5130
	private GameObject SingleModeEnemyManager;
}
