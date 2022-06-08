using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class DynamicGridObstacle : MonoBehaviour
{
	// Token: 0x06000322 RID: 802 RVA: 0x00018519 File Offset: 0x00016919
	private void Start()
	{
		this.col = base.GetComponent<Collider>();
		if (base.GetComponent<Collider>() == null)
		{
			Debug.LogError("A collider must be attached to the GameObject for DynamicGridObstacle to work");
		}
		base.StartCoroutine(this.UpdateGraphs());
	}

	// Token: 0x06000323 RID: 803 RVA: 0x00018550 File Offset: 0x00016950
	private IEnumerator UpdateGraphs()
	{
		if (this.col == null || AstarPath.active == null)
		{
			Debug.LogWarning("No collider is attached to the GameObject. Canceling check");
			yield break;
		}
		while (this.col)
		{
			while (this.isWaitingForUpdate)
			{
				yield return new WaitForSeconds(this.checkTime);
			}
			Bounds newBounds = this.col.bounds;
			Bounds merged = newBounds;
			merged.Encapsulate(this.prevBounds);
			Vector3 minDiff = merged.min - newBounds.min;
			Vector3 maxDiff = merged.max - newBounds.max;
			if (Mathf.Abs(minDiff.x) > this.updateError || Mathf.Abs(minDiff.y) > this.updateError || Mathf.Abs(minDiff.z) > this.updateError || Mathf.Abs(maxDiff.x) > this.updateError || Mathf.Abs(maxDiff.y) > this.updateError || Mathf.Abs(maxDiff.z) > this.updateError)
			{
				this.isWaitingForUpdate = true;
				this.DoUpdateGraphs();
			}
			yield return new WaitForSeconds(this.checkTime);
		}
		this.OnDestroy();
		yield break;
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0001856C File Offset: 0x0001696C
	public void OnDestroy()
	{
		if (AstarPath.active != null)
		{
			GraphUpdateObject ob = new GraphUpdateObject(this.prevBounds);
			AstarPath.active.UpdateGraphs(ob);
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x000185A0 File Offset: 0x000169A0
	public void DoUpdateGraphs()
	{
		if (this.col == null)
		{
			return;
		}
		this.isWaitingForUpdate = false;
		Bounds bounds = this.col.bounds;
		Bounds bounds2 = bounds;
		bounds2.Encapsulate(this.prevBounds);
		if (this.BoundsVolume(bounds2) < this.BoundsVolume(bounds) + this.BoundsVolume(this.prevBounds))
		{
			AstarPath.active.UpdateGraphs(bounds2);
		}
		else
		{
			AstarPath.active.UpdateGraphs(this.prevBounds);
			AstarPath.active.UpdateGraphs(bounds);
		}
		this.prevBounds = bounds;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00018634 File Offset: 0x00016A34
	public float BoundsVolume(Bounds b)
	{
		return Math.Abs(b.size.x * b.size.y * b.size.z);
	}

	// Token: 0x04000291 RID: 657
	private Collider col;

	// Token: 0x04000292 RID: 658
	public float updateError = 1f;

	// Token: 0x04000293 RID: 659
	public float checkTime = 0.2f;

	// Token: 0x04000294 RID: 660
	private Bounds prevBounds;

	// Token: 0x04000295 RID: 661
	private bool isWaitingForUpdate;
}
