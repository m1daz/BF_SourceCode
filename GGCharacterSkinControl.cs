using System;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public class GGCharacterSkinControl : MonoBehaviour
{
	// Token: 0x06000D88 RID: 3464 RVA: 0x0007035F File Offset: 0x0006E75F
	private void Start()
	{
		this.characterHandMaterial.mainTexture = SkinManager.mInstance.myCharacterSkinEntity.tex;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0007037B File Offset: 0x0006E77B
	private void Update()
	{
	}

	// Token: 0x04000DAE RID: 3502
	public Material characterHandMaterial;
}
