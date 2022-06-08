using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

// Token: 0x0200005D RID: 93
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(CharacterController))]
public class AIFollow : MonoBehaviour
{
	// Token: 0x06000311 RID: 785 RVA: 0x00017D6F File Offset: 0x0001616F
	public void Start()
	{
		this.seeker = base.GetComponent<Seeker>();
		this.controller = base.GetComponent<CharacterController>();
		this.navmeshController = base.GetComponent<NavmeshController>();
		this.tr = base.transform;
		this.Repath();
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00017DA7 File Offset: 0x000161A7
	public void Reset()
	{
		this.path = null;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00017DB0 File Offset: 0x000161B0
	public void OnPathComplete(Path p)
	{
		base.StartCoroutine(this.WaitToRepath());
		if (p.error)
		{
			return;
		}
		this.path = p.vectorPath.ToArray();
		float num = float.PositiveInfinity;
		int num2 = 0;
		for (int i = 0; i < this.path.Length - 1; i++)
		{
			float num3 = Mathfx.DistancePointSegmentStrict(this.path[i], this.path[i + 1], this.tr.position);
			if (num3 < num)
			{
				num2 = 0;
				num = num3;
				this.pathIndex = i + 1;
			}
			else if (num2 > 6)
			{
				break;
			}
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00017E64 File Offset: 0x00016264
	public IEnumerator WaitToRepath()
	{
		float timeLeft = this.repathRate - (Time.time - this.lastPathSearch);
		yield return new WaitForSeconds(timeLeft);
		this.Repath();
		yield break;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00017E7F File Offset: 0x0001627F
	public void Stop()
	{
		this.canMove = false;
		this.canSearch = false;
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00017E8F File Offset: 0x0001628F
	public void Resume()
	{
		this.canMove = true;
		this.canSearch = true;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00017EA0 File Offset: 0x000162A0
	public virtual void Repath()
	{
		this.lastPathSearch = Time.time;
		if (this.seeker == null || this.target == null || !this.canSearch || !this.seeker.IsDone())
		{
			base.StartCoroutine(this.WaitToRepath());
			return;
		}
		Path p = ABPath.Construct(base.transform.position, this.target.position, null);
		this.seeker.StartPath(p, new OnPathDelegate(this.OnPathComplete), -1);
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00017F3C File Offset: 0x0001633C
	public void PathToTarget(Vector3 targetPoint)
	{
		this.lastPathSearch = Time.time;
		if (this.seeker == null)
		{
			return;
		}
		this.seeker.StartPath(base.transform.position, targetPoint, new OnPathDelegate(this.OnPathComplete));
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00017F8A File Offset: 0x0001638A
	public virtual void ReachedEndOfPath()
	{
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00017F8C File Offset: 0x0001638C
	public void Update()
	{
		if (this.path == null || this.pathIndex >= this.path.Length || this.pathIndex < 0 || !this.canMove)
		{
			return;
		}
		Vector3 a = this.path[this.pathIndex];
		a.y = this.tr.position.y;
		while ((a - this.tr.position).sqrMagnitude < this.pickNextWaypointDistance * this.pickNextWaypointDistance)
		{
			this.pathIndex++;
			if (this.pathIndex >= this.path.Length)
			{
				if ((a - this.tr.position).sqrMagnitude < this.pickNextWaypointDistance * this.targetReached * (this.pickNextWaypointDistance * this.targetReached))
				{
					this.ReachedEndOfPath();
					return;
				}
				this.pathIndex--;
				break;
			}
			else
			{
				a = this.path[this.pathIndex];
				a.y = this.tr.position.y;
			}
		}
		Vector3 forward = a - this.tr.position;
		this.tr.rotation = Quaternion.Slerp(this.tr.rotation, Quaternion.LookRotation(forward), this.rotationSpeed * Time.deltaTime);
		this.tr.eulerAngles = new Vector3(0f, this.tr.eulerAngles.y, 0f);
		Vector3 vector = base.transform.forward;
		vector *= this.speed;
		vector *= Mathf.Clamp01(Vector3.Dot(forward.normalized, this.tr.forward));
		if (this.navmeshController != null)
		{
			this.navmeshController.SimpleMove(this.tr.position, vector);
		}
		else if (this.controller != null)
		{
			this.controller.SimpleMove(vector);
		}
		else
		{
			base.transform.Translate(vector * Time.deltaTime, Space.World);
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x000181F4 File Offset: 0x000165F4
	public void OnDrawGizmos()
	{
		if (!this.drawGizmos || this.path == null || this.pathIndex >= this.path.Length || this.pathIndex < 0)
		{
			return;
		}
		Vector3 vector = this.path[this.pathIndex];
		vector.y = this.tr.position.y;
		Debug.DrawLine(base.transform.position, vector, Color.blue);
		float num = this.pickNextWaypointDistance;
		if (this.pathIndex == this.path.Length - 1)
		{
			num *= this.targetReached;
		}
		Vector3 start = vector + num * new Vector3(1f, 0f, 0f);
		float num2 = 0f;
		while ((double)num2 < 6.283185307179586)
		{
			Vector3 vector2 = vector + new Vector3((float)Math.Cos((double)num2) * num, 0f, (float)Math.Sin((double)num2) * num);
			Debug.DrawLine(start, vector2, Color.yellow);
			start = vector2;
			num2 += 0.1f;
		}
		Debug.DrawLine(start, vector + num * new Vector3(1f, 0f, 0f), Color.yellow);
	}

	// Token: 0x0400027D RID: 637
	public Transform target;

	// Token: 0x0400027E RID: 638
	public float repathRate = 0.1f;

	// Token: 0x0400027F RID: 639
	public float pickNextWaypointDistance = 1f;

	// Token: 0x04000280 RID: 640
	public float targetReached = 0.2f;

	// Token: 0x04000281 RID: 641
	public float speed = 5f;

	// Token: 0x04000282 RID: 642
	public float rotationSpeed = 1f;

	// Token: 0x04000283 RID: 643
	public bool drawGizmos;

	// Token: 0x04000284 RID: 644
	public bool canSearch = true;

	// Token: 0x04000285 RID: 645
	public bool canMove = true;

	// Token: 0x04000286 RID: 646
	protected Seeker seeker;

	// Token: 0x04000287 RID: 647
	protected CharacterController controller;

	// Token: 0x04000288 RID: 648
	protected NavmeshController navmeshController;

	// Token: 0x04000289 RID: 649
	protected Transform tr;

	// Token: 0x0400028A RID: 650
	protected float lastPathSearch = -9999f;

	// Token: 0x0400028B RID: 651
	protected int pathIndex;

	// Token: 0x0400028C RID: 652
	protected Vector3[] path;
}
