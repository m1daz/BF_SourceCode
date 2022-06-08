using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000002 RID: 2
[RequireComponent(typeof(Seeker))]
public class AIPath : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E9 File Offset: 0x000004E9
	public bool TargetReached
	{
		get
		{
			return this.targetReached;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000004F4
	protected virtual void Awake()
	{
		this.seeker = base.GetComponent<Seeker>();
		this.tr = base.transform;
		Seeker seeker = this.seeker;
		seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		this.controller = base.GetComponent<CharacterController>();
		this.navController = base.GetComponent<NavmeshController>();
		this.rvoController = base.GetComponent<RVOController>();
		this.rigid = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002171 File Offset: 0x00000571
	protected virtual void Start()
	{
		this.startHasRun = true;
		this.OnEnable();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002180 File Offset: 0x00000580
	protected virtual void OnEnable()
	{
		if (this.startHasRun)
		{
			base.StartCoroutine(this.RepeatTrySearchPath());
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000219C File Offset: 0x0000059C
	public IEnumerator RepeatTrySearchPath()
	{
		for (;;)
		{
			this.TrySearchPath();
			yield return new WaitForSeconds(this.repathRate);
		}
		yield break;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000021B8 File Offset: 0x000005B8
	public void TrySearchPath()
	{
		if (Time.time - this.lastRepath >= this.repathRate && this.canSearchAgain && this.canSearch)
		{
			this.SearchPath();
		}
		else
		{
			base.StartCoroutine(this.WaitForRepath());
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000220C File Offset: 0x0000060C
	protected IEnumerator WaitForRepath()
	{
		if (this.waitingForRepath)
		{
			yield break;
		}
		this.waitingForRepath = true;
		yield return new WaitForSeconds(this.repathRate - (Time.time - this.lastRepath));
		this.waitingForRepath = false;
		this.TrySearchPath();
		yield break;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002228 File Offset: 0x00000628
	public virtual void SearchPath()
	{
		if (this.target == null)
		{
			Debug.LogError("Target is null, aborting all search");
			this.canSearch = false;
			return;
		}
		this.lastRepath = Time.time;
		Vector3 position = this.target.position;
		this.canSearchAgain = false;
		this.seeker.StartPath(this.GetFeetPosition(), position);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002289 File Offset: 0x00000689
	public virtual void OnTargetReached()
	{
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000228B File Offset: 0x0000068B
	public void OnDestroy()
	{
		if (this.path != null)
		{
			this.path.Release(this);
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022A4 File Offset: 0x000006A4
	public virtual void OnPathComplete(Path _p)
	{
		ABPath abpath = _p as ABPath;
		if (abpath == null)
		{
			throw new Exception("This function only handles ABPaths, do not use special path types");
		}
		if (this.path != null)
		{
			this.path.Release(this);
		}
		abpath.Claim(this);
		this.path = abpath;
		this.currentWaypointIndex = 0;
		this.targetReached = false;
		this.canSearchAgain = true;
		if (this.closestOnPathCheck)
		{
			Vector3 vector = abpath.startPoint;
			Vector3 feetPosition = this.GetFeetPosition();
			float num = Vector3.Distance(vector, feetPosition);
			Vector3 vector2 = feetPosition - vector;
			vector2 /= num;
			int num2 = (int)(num / this.pickNextWaypointDist);
			for (int i = 0; i < num2; i++)
			{
				this.CalculateVelocity(vector);
				vector += vector2;
			}
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000236C File Offset: 0x0000076C
	public virtual Vector3 GetFeetPosition()
	{
		if (this.rvoController != null)
		{
			return this.tr.position - Vector3.up * this.rvoController.height * 0.5f;
		}
		if (this.controller != null)
		{
			return this.tr.position - Vector3.up * this.controller.height * 0.5f;
		}
		return this.tr.position;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002408 File Offset: 0x00000808
	public virtual void Update()
	{
		if (!this.canMove)
		{
			return;
		}
		Vector3 vector = this.CalculateVelocity(this.GetFeetPosition());
		if (this.targetDirection != Vector3.zero)
		{
			this.RotateTowards(this.targetDirection);
		}
		if (this.rvoController != null)
		{
			this.rvoController.Move(vector);
		}
		else if (this.navController != null)
		{
			this.navController.SimpleMove(this.GetFeetPosition(), vector);
		}
		else if (this.controller != null)
		{
			this.controller.SimpleMove(vector);
		}
		else if (this.rigid != null)
		{
			this.rigid.AddForce(vector);
		}
		else
		{
			base.transform.Translate(vector * Time.deltaTime, Space.World);
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000024F8 File Offset: 0x000008F8
	protected float XZSqrMagnitude(Vector3 a, Vector3 b)
	{
		float num = b.x - a.x;
		float num2 = b.z - a.z;
		return num * num + num2 * num2;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000252C File Offset: 0x0000092C
	protected Vector3 CalculateVelocity(Vector3 currentPosition)
	{
		if (this.path == null || this.path.vectorPath == null || this.path.vectorPath.Count == 0)
		{
			return Vector3.zero;
		}
		List<Vector3> vectorPath = this.path.vectorPath;
		if (vectorPath.Count == 1)
		{
			vectorPath.Insert(0, currentPosition);
		}
		if (this.currentWaypointIndex >= vectorPath.Count)
		{
			this.currentWaypointIndex = vectorPath.Count - 1;
		}
		if (this.currentWaypointIndex <= 1)
		{
			this.currentWaypointIndex = 1;
		}
		while (this.currentWaypointIndex < vectorPath.Count - 1)
		{
			float num = this.XZSqrMagnitude(vectorPath[this.currentWaypointIndex], currentPosition);
			if (num < this.pickNextWaypointDist * this.pickNextWaypointDist)
			{
				this.currentWaypointIndex++;
			}
			else
			{
				IL_E9:
				Vector3 vector = vectorPath[this.currentWaypointIndex] - vectorPath[this.currentWaypointIndex - 1];
				Vector3 a = this.CalculateTargetPoint(currentPosition, vectorPath[this.currentWaypointIndex - 1], vectorPath[this.currentWaypointIndex]);
				vector = a - currentPosition;
				vector.y = 0f;
				float magnitude = vector.magnitude;
				float num2 = Mathf.Clamp01(magnitude / this.slowdownDistance);
				this.targetDirection = vector;
				this.targetPoint = a;
				if (this.currentWaypointIndex == vectorPath.Count - 1 && magnitude <= this.endReachedDistance)
				{
					if (!this.targetReached)
					{
						this.targetReached = true;
						this.OnTargetReached();
					}
					return Vector3.zero;
				}
				Vector3 forward = this.tr.forward;
				float a2 = Vector3.Dot(vector.normalized, forward);
				float num3 = this.speed * Mathf.Max(a2, this.minMoveScale) * num2;
				if (Time.deltaTime > 0f)
				{
					num3 = Mathf.Clamp(num3, 0f, magnitude / (Time.deltaTime * 2f));
				}
				return forward * num3;
			}
		}
		goto IL_E9;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002748 File Offset: 0x00000B48
	protected virtual void RotateTowards(Vector3 dir)
	{
		Quaternion quaternion = this.tr.rotation;
		Quaternion b = Quaternion.LookRotation(dir);
		Vector3 eulerAngles = Quaternion.Slerp(quaternion, b, this.turningSpeed * Time.fixedDeltaTime).eulerAngles;
		eulerAngles.z = 0f;
		eulerAngles.x = 0f;
		quaternion = Quaternion.Euler(eulerAngles);
		this.tr.rotation = quaternion;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000027B0 File Offset: 0x00000BB0
	protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b)
	{
		a.y = p.y;
		b.y = p.y;
		float magnitude = (a - b).magnitude;
		if (magnitude == 0f)
		{
			return a;
		}
		float num = Mathfx.Clamp01(Mathfx.NearestPointFactor(a, b, p));
		Vector3 a2 = (b - a) * num + a;
		float magnitude2 = (a2 - p).magnitude;
		float num2 = Mathf.Clamp(this.forwardLook - magnitude2, 0f, this.forwardLook);
		float num3 = num2 / magnitude;
		num3 = Mathf.Clamp(num3 + num, 0f, 1f);
		return (b - a) * num3 + a;
	}

	// Token: 0x04000001 RID: 1
	public float repathRate = 0.5f;

	// Token: 0x04000002 RID: 2
	public Transform target;

	// Token: 0x04000003 RID: 3
	public bool canSearch = true;

	// Token: 0x04000004 RID: 4
	public bool canMove = true;

	// Token: 0x04000005 RID: 5
	public float speed = 3f;

	// Token: 0x04000006 RID: 6
	public float turningSpeed = 5f;

	// Token: 0x04000007 RID: 7
	public float slowdownDistance = 0.6f;

	// Token: 0x04000008 RID: 8
	public float pickNextWaypointDist = 2f;

	// Token: 0x04000009 RID: 9
	public float forwardLook = 1f;

	// Token: 0x0400000A RID: 10
	public float endReachedDistance = 0.2f;

	// Token: 0x0400000B RID: 11
	public bool closestOnPathCheck = true;

	// Token: 0x0400000C RID: 12
	public bool recyclePaths = true;

	// Token: 0x0400000D RID: 13
	protected float minMoveScale = 0.05f;

	// Token: 0x0400000E RID: 14
	protected Seeker seeker;

	// Token: 0x0400000F RID: 15
	protected Transform tr;

	// Token: 0x04000010 RID: 16
	private float lastRepath = -9999f;

	// Token: 0x04000011 RID: 17
	protected Path path;

	// Token: 0x04000012 RID: 18
	protected CharacterController controller;

	// Token: 0x04000013 RID: 19
	protected NavmeshController navController;

	// Token: 0x04000014 RID: 20
	protected RVOController rvoController;

	// Token: 0x04000015 RID: 21
	protected Rigidbody rigid;

	// Token: 0x04000016 RID: 22
	protected int currentWaypointIndex;

	// Token: 0x04000017 RID: 23
	protected bool targetReached;

	// Token: 0x04000018 RID: 24
	protected bool canSearchAgain = true;

	// Token: 0x04000019 RID: 25
	private bool startHasRun;

	// Token: 0x0400001A RID: 26
	private bool waitingForRepath;

	// Token: 0x0400001B RID: 27
	protected Vector3 targetPoint;

	// Token: 0x0400001C RID: 28
	protected Vector3 targetDirection;
}
