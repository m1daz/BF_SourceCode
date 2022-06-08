using System;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
public class MinMaxRangeAttribute : PropertyAttribute
{
	// Token: 0x060028E5 RID: 10469 RVA: 0x0012C8F4 File Offset: 0x0012ACF4
	public MinMaxRangeAttribute(float minLimit, float maxLimit)
	{
		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}

	// Token: 0x04002981 RID: 10625
	public float minLimit;

	// Token: 0x04002982 RID: 10626
	public float maxLimit;
}
