using System;
using UnityEngine;

// Token: 0x020003E6 RID: 998
[AddComponentMenu("EasyTouch Controls/Set Direct Action Transform ")]
public class ETCSetDirectActionTransform : MonoBehaviour
{
	// Token: 0x06001E04 RID: 7684 RVA: 0x000E6E44 File Offset: 0x000E5244
	private void Start()
	{
		if (!string.IsNullOrEmpty(this.axisName1))
		{
			ETCInput.SetAxisDirecTransform(this.axisName1, base.transform);
		}
		if (!string.IsNullOrEmpty(this.axisName2))
		{
			ETCInput.SetAxisDirecTransform(this.axisName2, base.transform);
		}
	}

	// Token: 0x04001F28 RID: 7976
	public string axisName1;

	// Token: 0x04001F29 RID: 7977
	public string axisName2;
}
