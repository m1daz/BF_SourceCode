using System;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public class OneFingerGestureSample : SampleBase
{
	// Token: 0x06001FB6 RID: 8118 RVA: 0x000F082F File Offset: 0x000EEC2F
	protected override string GetHelpText()
	{
		return "This sample demonstrates some of the supported single-finger gestures:\r\n\r\n- Drag: press the red sphere and move your finger to drag it around  \r\n\r\n- LongPress: keep your finger pressed on the cyan sphere for at least " + FingerGestures.Defaults.Fingers[0].LongPress.Duration + " seconds\r\n\r\n- Tap: rapidly press & release the purple sphere \r\n\r\n- Swipe: press the yellow sphere and move your finger in one of the four cardinal directions, then release. The speed of the motion is taken into account.";
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x000F085C File Offset: 0x000EEC5C
	private void OnEnable()
	{
		Debug.Log("Registering finger gesture events from C# script");
		FingerGestures.OnFingerLongPress += this.FingerGestures_OnFingerLongPress;
		FingerGestures.OnFingerTap += this.FingerGestures_OnFingerTap;
		FingerGestures.OnFingerSwipe += this.FingerGestures_OnFingerSwipe;
		FingerGestures.OnFingerDragBegin += this.FingerGestures_OnFingerDragBegin;
		FingerGestures.OnFingerDragMove += this.FingerGestures_OnFingerDragMove;
		FingerGestures.OnFingerDragEnd += this.FingerGestures_OnFingerDragEnd;
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x000F08DC File Offset: 0x000EECDC
	private void OnDisable()
	{
		FingerGestures.OnFingerLongPress -= this.FingerGestures_OnFingerLongPress;
		FingerGestures.OnFingerTap -= this.FingerGestures_OnFingerTap;
		FingerGestures.OnFingerSwipe -= this.FingerGestures_OnFingerSwipe;
		FingerGestures.OnFingerDragBegin -= this.FingerGestures_OnFingerDragBegin;
		FingerGestures.OnFingerDragMove -= this.FingerGestures_OnFingerDragMove;
		FingerGestures.OnFingerDragEnd -= this.FingerGestures_OnFingerDragEnd;
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x000F094F File Offset: 0x000EED4F
	private void FingerGestures_OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
	{
		if (this.CheckSpawnParticles(fingerPos, this.longPressObject))
		{
			base.UI.StatusText = "Performed a long-press with finger " + fingerIndex;
		}
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x000F0980 File Offset: 0x000EED80
	private void FingerGestures_OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
		if (tapCount >= this.requiredTapCount)
		{
			Debug.Log("Tapcount: " + tapCount);
			if (this.CheckSpawnParticles(fingerPos, this.tapObject))
			{
				base.UI.StatusText = string.Concat(new object[]
				{
					"Tapped ",
					tapCount,
					" times with finger ",
					fingerIndex
				});
			}
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x000F09F8 File Offset: 0x000EEDF8
	private void FingerGestures_OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
		GameObject gameObject = SampleBase.PickObject(startPos);
		if (gameObject == this.swipeObject)
		{
			base.UI.StatusText = string.Concat(new object[]
			{
				"Swiped ",
				direction,
				" with finger ",
				fingerIndex
			});
			SwipeParticlesEmitter componentInChildren = gameObject.GetComponentInChildren<SwipeParticlesEmitter>();
			if (componentInChildren)
			{
				componentInChildren.Emit(direction, velocity);
			}
		}
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x000F0A70 File Offset: 0x000EEE70
	private void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
	{
		GameObject gameObject = SampleBase.PickObject(startPos);
		if (gameObject == this.dragObject)
		{
			base.UI.StatusText = "Started dragging with finger " + fingerIndex;
			this.dragFingerIndex = fingerIndex;
			this.SpawnParticles(gameObject);
		}
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x000F0ABE File Offset: 0x000EEEBE
	private void FingerGestures_OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
	{
		if (fingerIndex == this.dragFingerIndex)
		{
			this.dragObject.transform.position = SampleBase.GetWorldPos(fingerPos);
		}
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x000F0AE2 File Offset: 0x000EEEE2
	private void FingerGestures_OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
	{
		if (fingerIndex == this.dragFingerIndex)
		{
			base.UI.StatusText = "Stopped dragging with finger " + fingerIndex;
			this.dragFingerIndex = -1;
			this.SpawnParticles(this.dragObject);
		}
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x000F0B20 File Offset: 0x000EEF20
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

	// Token: 0x06001FC0 RID: 8128 RVA: 0x000F0B58 File Offset: 0x000EEF58
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x040020C5 RID: 8389
	public GameObject longPressObject;

	// Token: 0x040020C6 RID: 8390
	public GameObject tapObject;

	// Token: 0x040020C7 RID: 8391
	public GameObject swipeObject;

	// Token: 0x040020C8 RID: 8392
	public GameObject dragObject;

	// Token: 0x040020C9 RID: 8393
	public int requiredTapCount = 2;

	// Token: 0x040020CA RID: 8394
	private int dragFingerIndex = -1;
}
