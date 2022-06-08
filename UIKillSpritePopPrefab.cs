using System;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class UIKillSpritePopPrefab : MonoBehaviour
{
	// Token: 0x060012EC RID: 4844 RVA: 0x000A9A01 File Offset: 0x000A7E01
	private void Start()
	{
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000A9A03 File Offset: 0x000A7E03
	private void Update()
	{
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x000A9A05 File Offset: 0x000A7E05
	public void SetKillSprite(string spriteName)
	{
		if (this.killSprite != null)
		{
			this.killSprite.mainTexture = (Resources.Load("UI/Images/General/" + spriteName) as Texture);
		}
	}

	// Token: 0x040015CB RID: 5579
	public UITexture killSprite;
}
