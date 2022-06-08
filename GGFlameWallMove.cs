using System;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class GGFlameWallMove : MonoBehaviour
{
	// Token: 0x06001038 RID: 4152 RVA: 0x0008A8EC File Offset: 0x00088CEC
	private void Start()
	{
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0008A8F0 File Offset: 0x00088CF0
	private void Update()
	{
		if (this.IsMoveUp)
		{
			if (base.transform.position.z > -26f)
			{
				base.transform.position -= new Vector3(0f, 0f, this.speed) * Time.deltaTime;
			}
			else
			{
				this.IsMoveDown = true;
				this.IsMoveUp = false;
			}
		}
		else if (this.IsMoveDown)
		{
			if (base.transform.position.z < 23f)
			{
				base.transform.position += new Vector3(0f, 0f, this.speed) * Time.deltaTime;
			}
			else
			{
				this.IsMoveDown = false;
				this.IsMoveUp = true;
			}
		}
	}

	// Token: 0x04001225 RID: 4645
	private bool IsMoveDown = true;

	// Token: 0x04001226 RID: 4646
	private bool IsMoveUp;

	// Token: 0x04001227 RID: 4647
	private float speed = 2f;
}
