using System;
using UnityEngine;

// Token: 0x0200044E RID: 1102
public class TwoFingerGestureSample : SampleBase
{
	// Token: 0x06001FE3 RID: 8163 RVA: 0x000F11CC File Offset: 0x000EF5CC
	protected override string GetHelpText()
	{
		return string.Concat(new object[]
		{
			"This sample demonstrates some of the supported two-finger gestures:\r\n\r\n- Drag: press the red sphere with two fingers and move them to drag the sphere around  \r\n\r\n- LongPress: keep your two fingers pressed on the cyan sphere for at least ",
			FingerGestures.Defaults.Fingers[0].LongPress.Duration,
			" seconds\r\n\r\n- Tap: rapidly press & release the purple sphere ",
			this.requiredTapCount,
			" times with two fingers\r\n\r\n- Swipe: press the yellow sphere with two fingers and move them in one of the four cardinal directions, then release your fingers. The speed of the motion is taken into account."
		});
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x000F1228 File Offset: 0x000EF628
	private void OnEnable()
	{
		Debug.Log("Registering finger gesture events from C# script");
		FingerGestures.OnTwoFingerLongPress += this.FingerGestures_OnTwoFingerLongPress;
		FingerGestures.OnTwoFingerTap += this.FingerGestures_OnTwoFingerTap;
		FingerGestures.OnTwoFingerSwipe += this.FingerGestures_OnTwoFingerSwipe;
		FingerGestures.OnTwoFingerDragBegin += this.FingerGestures_OnTwoFingerDragBegin;
		FingerGestures.OnTwoFingerDragMove += this.FingerGestures_OnTwoFingerDragMove;
		FingerGestures.OnTwoFingerDragEnd += this.FingerGestures_OnTwoFingerDragEnd;
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x000F12A8 File Offset: 0x000EF6A8
	private void OnDisable()
	{
		FingerGestures.OnTwoFingerLongPress -= this.FingerGestures_OnTwoFingerLongPress;
		FingerGestures.OnTwoFingerTap -= this.FingerGestures_OnTwoFingerTap;
		FingerGestures.OnTwoFingerSwipe -= this.FingerGestures_OnTwoFingerSwipe;
		FingerGestures.OnTwoFingerDragBegin -= this.FingerGestures_OnTwoFingerDragBegin;
		FingerGestures.OnTwoFingerDragMove -= this.FingerGestures_OnTwoFingerDragMove;
		FingerGestures.OnTwoFingerDragEnd -= this.FingerGestures_OnTwoFingerDragEnd;
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x000F131B File Offset: 0x000EF71B
	private void FingerGestures_OnTwoFingerLongPress(Vector2 fingerPos)
	{
		if (this.CheckSpawnParticles(fingerPos, this.longPressObject))
		{
			base.UI.StatusText = "Performed a two-finger long-press";
		}
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x000F1340 File Offset: 0x000EF740
	private void FingerGestures_OnTwoFingerTap(Vector2 fingerPos, int tapCount)
	{
		if (tapCount == this.requiredTapCount && this.CheckSpawnParticles(fingerPos, this.tapObject))
		{
			base.UI.StatusText = "Tapped " + this.requiredTapCount + " times with two fingers";
		}
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x000F1390 File Offset: 0x000EF790
	private void FingerGestures_OnTwoFingerSwipe(Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
		GameObject gameObject = SampleBase.PickObject(startPos);
		if (gameObject == this.swipeObject)
		{
			base.UI.StatusText = "Swiped " + direction + " with two fingers";
			SwipeParticlesEmitter componentInChildren = gameObject.GetComponentInChildren<SwipeParticlesEmitter>();
			if (componentInChildren)
			{
				componentInChildren.Emit(direction, velocity);
			}
		}
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x000F13F0 File Offset: 0x000EF7F0
	private void FingerGestures_OnTwoFingerDragBegin(Vector2 fingerPos, Vector2 startPos)
	{
		GameObject gameObject = SampleBase.PickObject(startPos);
		if (gameObject == this.dragObject)
		{
			this.dragging = true;
			base.UI.StatusText = "Started dragging with two fingers";
			this.SpawnParticles(gameObject);
		}
	}

	// Token: 0x06001FEA RID: 8170 RVA: 0x000F1433 File Offset: 0x000EF833
	private void FingerGestures_OnTwoFingerDragMove(Vector2 fingerPos, Vector2 delta)
	{
		if (this.dragging)
		{
			this.dragObject.transform.position = SampleBase.GetWorldPos(fingerPos);
		}
	}

	// Token: 0x06001FEB RID: 8171 RVA: 0x000F1456 File Offset: 0x000EF856
	private void FingerGestures_OnTwoFingerDragEnd(Vector2 fingerPos)
	{
		if (this.dragging)
		{
			base.UI.StatusText = "Stopped dragging with two fingers";
			this.SpawnParticles(this.dragObject);
			this.dragging = false;
		}
	}

	// Token: 0x06001FEC RID: 8172 RVA: 0x000F1488 File Offset: 0x000EF888
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

	// Token: 0x06001FED RID: 8173 RVA: 0x000F14C0 File Offset: 0x000EF8C0
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x040020E7 RID: 8423
	public GameObject longPressObject;

	// Token: 0x040020E8 RID: 8424
	public GameObject tapObject;

	// Token: 0x040020E9 RID: 8425
	public GameObject swipeObject;

	// Token: 0x040020EA RID: 8426
	public GameObject dragObject;

	// Token: 0x040020EB RID: 8427
	public int requiredTapCount = 2;

	// Token: 0x040020EC RID: 8428
	private bool dragging;
}
