using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class FrameTrailDamage : MonoBehaviour
{
	// Token: 0x06001029 RID: 4137 RVA: 0x0008A6F2 File Offset: 0x00088AF2
	private void Start()
	{
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0008A6F4 File Offset: 0x00088AF4
	private void Update()
	{
		if (this.isTouchPlayer)
		{
			this.checktime += Time.deltaTime;
			if (this.checktime > this.checktime_Count)
			{
				GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
				ggdamageEventArgs.damage = (short)this.damage;
				this.Player.transform.SendMessageUpwards("Event_Damage", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
				this.checktime = 0f;
			}
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0008A764 File Offset: 0x00088B64
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player")
		{
			this.isTouchPlayer = true;
			this.Player = other.gameObject;
		}
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0008A79D File Offset: 0x00088B9D
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player")
		{
			this.isTouchPlayer = false;
		}
	}

	// Token: 0x04001220 RID: 4640
	public bool isTouchPlayer;

	// Token: 0x04001221 RID: 4641
	private GameObject Player;

	// Token: 0x04001222 RID: 4642
	public int damage = 6;

	// Token: 0x04001223 RID: 4643
	private float checktime;

	// Token: 0x04001224 RID: 4644
	public float checktime_Count = 0.3f;
}
