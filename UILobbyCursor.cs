using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class UILobbyCursor : MonoBehaviour
{
	// Token: 0x060015C6 RID: 5574 RVA: 0x000B97A9 File Offset: 0x000B7BA9
	private void Start()
	{
		this.mCursorSprite = base.gameObject.GetComponent<UISprite>();
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x000B97BC File Offset: 0x000B7BBC
	private void Update()
	{
		this.mOpTimeForUpdateCursorStatus += Time.deltaTime;
		if (this.mOpTimeForUpdateCursorStatus > UILobbyCursor.OPIntervalForUpdateCursorStatus)
		{
			this.mOpTimeForUpdateCursorStatus = 0f;
			if (base.gameObject.GetComponentInParent<UIInput>().value.Length == 0)
			{
				if (this.mCursorSprite.enabled)
				{
					this.mCursorSprite.enabled = false;
				}
				else
				{
					this.mCursorSprite.enabled = true;
				}
			}
			else if (this.mCursorSprite.enabled)
			{
				this.mCursorSprite.enabled = false;
			}
		}
	}

	// Token: 0x04001899 RID: 6297
	private UISprite mCursorSprite;

	// Token: 0x0400189A RID: 6298
	private float mOpTimeForUpdateCursorStatus;

	// Token: 0x0400189B RID: 6299
	private static float OPIntervalForUpdateCursorStatus = 0.6f;
}
