using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class TextureEditorTester : MonoBehaviour
{
	// Token: 0x06001AFF RID: 6911 RVA: 0x000DA084 File Offset: 0x000D8484
	private void Start()
	{
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x000DA086 File Offset: 0x000D8486
	private void Update()
	{
		if (Time.frameCount % 32 == 0)
		{
			this.m_EditTarget.SetPixel(7, 7, Color.blue);
			this.m_EditTarget.Apply();
		}
	}

	// Token: 0x04001D47 RID: 7495
	public Texture2D m_EditTarget;
}
