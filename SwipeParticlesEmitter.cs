using System;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public class SwipeParticlesEmitter : MonoBehaviour
{
	// Token: 0x06001FAD RID: 8109 RVA: 0x000F060A File Offset: 0x000EEA0A
	private void Start()
	{
		if (!this.emitter)
		{
			this.emitter = base.GetComponent<ParticleEmitter>();
		}
		this.emitter.emit = false;
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x000F0634 File Offset: 0x000EEA34
	public void Emit(FingerGestures.SwipeDirection direction, float swipeVelocity)
	{
		Vector3 forward;
		if (direction == FingerGestures.SwipeDirection.Up)
		{
			forward = Vector3.up;
		}
		else if (direction == FingerGestures.SwipeDirection.Down)
		{
			forward = Vector3.down;
		}
		else if (direction == FingerGestures.SwipeDirection.Right)
		{
			forward = Vector3.right;
		}
		else
		{
			forward = Vector3.left;
		}
		this.emitter.transform.rotation = Quaternion.LookRotation(forward);
		Vector3 localVelocity = this.emitter.localVelocity;
		localVelocity.z = this.baseSpeed * this.swipeVelocityScale * swipeVelocity;
		this.emitter.localVelocity = localVelocity;
		this.emitter.Emit();
	}

	// Token: 0x040020BE RID: 8382
	public ParticleEmitter emitter;

	// Token: 0x040020BF RID: 8383
	public float baseSpeed = 4f;

	// Token: 0x040020C0 RID: 8384
	public float swipeVelocityScale = 0.001f;
}
