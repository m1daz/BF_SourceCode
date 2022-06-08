using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class MultiFingerSwipeSample : SampleBase
{
	// Token: 0x06001FB0 RID: 8112 RVA: 0x000F06EA File Offset: 0x000EEAEA
	protected override string GetHelpText()
	{
		return "Swipe: press the yellow sphere with " + this.swipeGesture.RequiredFingerCount + " fingers and move them in one of the four cardinal directions, then release. The speed of the motion is taken into account.";
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x000F070B File Offset: 0x000EEB0B
	protected override void Start()
	{
		base.Start();
		this.swipeGesture.OnSwipe += this.OnSwipe;
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x000F072C File Offset: 0x000EEB2C
	private void OnSwipe(SwipeGestureRecognizer source)
	{
		GameObject gameObject = SampleBase.PickObject(source.StartPosition);
		if (gameObject == this.sphereObject)
		{
			base.UI.StatusText = string.Concat(new object[]
			{
				"Swiped ",
				source.Direction,
				" with velocity: ",
				source.Velocity
			});
			SwipeParticlesEmitter componentInChildren = gameObject.GetComponentInChildren<SwipeParticlesEmitter>();
			if (componentInChildren)
			{
				componentInChildren.Emit(source.Direction, source.Velocity);
			}
		}
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x000F07BC File Offset: 0x000EEBBC
	private bool CheckSpawnParticles(Vector2 fingerPos, GameObject requiredObject)
	{
		GameObject gameObject = SampleBase.PickObject(fingerPos);
		if (!gameObject || gameObject != requiredObject)
		{
			return false;
		}
		this.SpawnParticles(gameObject);
		return true;
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x000F07F4 File Offset: 0x000EEBF4
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x040020C1 RID: 8385
	public SwipeGestureRecognizer swipeGesture;

	// Token: 0x040020C2 RID: 8386
	public GameObject sphereObject;

	// Token: 0x040020C3 RID: 8387
	public float baseEmitSpeed = 4f;

	// Token: 0x040020C4 RID: 8388
	public float swipeVelocityEmitSpeedScale = 0.001f;
}
