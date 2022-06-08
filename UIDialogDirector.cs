using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class UIDialogDirector : MonoBehaviour
{
	// Token: 0x060015BF RID: 5567 RVA: 0x000B9761 File Offset: 0x000B7B61
	private void Awake()
	{
		UIDialogDirector.mInstance = this;
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000B9769 File Offset: 0x000B7B69
	private void Start()
	{
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000B976B File Offset: 0x000B7B6B
	private void Update()
	{
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000B976D File Offset: 0x000B7B6D
	public void DisplayNeedHuntingTicketDialog()
	{
		this.mGoNeedHuntingTicketDialog.SetActive(true);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x000B977B File Offset: 0x000B7B7B
	public void HideNeedHuntingTicketDialog()
	{
		this.mGoNeedHuntingTicketDialog.SetActive(false);
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000B9789 File Offset: 0x000B7B89
	private void OnDestroy()
	{
		if (UIDialogDirector.mInstance != null)
		{
			UIDialogDirector.mInstance = null;
		}
	}

	// Token: 0x04001897 RID: 6295
	public static UIDialogDirector mInstance;

	// Token: 0x04001898 RID: 6296
	public GameObject mGoNeedHuntingTicketDialog;
}
