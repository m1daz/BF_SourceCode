using System;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class UIPurchaseButtonEventDirector : MonoBehaviour
{
	// Token: 0x0600143F RID: 5183 RVA: 0x000B0EC7 File Offset: 0x000AF2C7
	private void Start()
	{
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x000B0EC9 File Offset: 0x000AF2C9
	private void Update()
	{
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x000B0ECB File Offset: 0x000AF2CB
	public void PurchaseButtonPressed()
	{
		this.PurchaseNode.SetActive(true);
		this.ExchangeNode.SetActive(false);
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x000B0EE5 File Offset: 0x000AF2E5
	public void ExchangeButtonPressed()
	{
		this.PurchaseNode.SetActive(false);
		this.ExchangeNode.SetActive(true);
	}

	// Token: 0x04001720 RID: 5920
	public GameObject PurchaseNode;

	// Token: 0x04001721 RID: 5921
	public GameObject ExchangeNode;
}
