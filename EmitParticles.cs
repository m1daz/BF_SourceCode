using System;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public class EmitParticles : MonoBehaviour
{
	// Token: 0x06001F93 RID: 8083 RVA: 0x000EFFC1 File Offset: 0x000EE3C1
	public void Emit()
	{
		this.emitter.Emit();
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x000EFFCE File Offset: 0x000EE3CE
	public void EmitLeft()
	{
		this.emitter.transform.rotation = this.left.rotation;
		this.Emit();
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000EFFF1 File Offset: 0x000EE3F1
	public void EmitRight()
	{
		this.emitter.transform.rotation = this.right.rotation;
		this.Emit();
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x000F0014 File Offset: 0x000EE414
	public void EmitUp()
	{
		this.emitter.transform.rotation = this.up.rotation;
		this.Emit();
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x000F0037 File Offset: 0x000EE437
	public void EmitDown()
	{
		this.emitter.transform.rotation = this.down.rotation;
		this.Emit();
	}

	// Token: 0x0400209D RID: 8349
	public ParticleEmitter emitter;

	// Token: 0x0400209E RID: 8350
	public Transform left;

	// Token: 0x0400209F RID: 8351
	public Transform right;

	// Token: 0x040020A0 RID: 8352
	public Transform up;

	// Token: 0x040020A1 RID: 8353
	public Transform down;
}
