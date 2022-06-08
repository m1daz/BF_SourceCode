using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class RVOExampleAgent : MonoBehaviour
{
	// Token: 0x06000302 RID: 770 RVA: 0x00016B83 File Offset: 0x00014F83
	public void Awake()
	{
		this.seeker = base.GetComponent<Seeker>();
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00016B91 File Offset: 0x00014F91
	public void Start()
	{
		this.controller = base.GetComponent<RVOController>();
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00016B9F File Offset: 0x00014F9F
	public void SetTarget(Vector3 target)
	{
		this.target = target;
		this.RecalculatePath();
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00016BB0 File Offset: 0x00014FB0
	public void SetColor(Color col)
	{
		if (this.rends == null)
		{
			this.rends = base.GetComponentsInChildren<MeshRenderer>();
		}
		foreach (MeshRenderer meshRenderer in this.rends)
		{
			Color color = meshRenderer.material.GetColor("_TintColor");
			AnimationCurve curve = AnimationCurve.Linear(0f, color.r, 1f, col.r);
			AnimationCurve curve2 = AnimationCurve.Linear(0f, color.g, 1f, col.g);
			AnimationCurve curve3 = AnimationCurve.Linear(0f, color.b, 1f, col.b);
			AnimationClip animationClip = new AnimationClip();
			animationClip.SetCurve(string.Empty, typeof(Material), "_TintColor.r", curve);
			animationClip.SetCurve(string.Empty, typeof(Material), "_TintColor.g", curve2);
			animationClip.SetCurve(string.Empty, typeof(Material), "_TintColor.b", curve3);
			Animation animation = meshRenderer.gameObject.GetComponent<Animation>();
			if (animation == null)
			{
				animation = meshRenderer.gameObject.AddComponent<Animation>();
			}
			animationClip.wrapMode = WrapMode.Once;
			animation.AddClip(animationClip, "ColorAnim");
			animation.Play("ColorAnim");
		}
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00016D0C File Offset: 0x0001510C
	public void RecalculatePath()
	{
		this.canSearchAgain = false;
		this.nextRepath = Time.time + this.repathRate * (UnityEngine.Random.value + 0.5f);
		this.seeker.StartPath(base.transform.position, this.target, new OnPathDelegate(this.OnPathComplete));
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00016D68 File Offset: 0x00015168
	public void OnPathComplete(Path _p)
	{
		ABPath abpath = _p as ABPath;
		this.canSearchAgain = true;
		if (this.path != null)
		{
			this.path.Release(this);
		}
		this.path = abpath;
		abpath.Claim(this);
		if (abpath.error)
		{
			this.wp = 0;
			this.vectorPath = null;
			return;
		}
		Vector3 originalStartPoint = abpath.originalStartPoint;
		Vector3 position = base.transform.position;
		originalStartPoint.y = position.y;
		float magnitude = (position - originalStartPoint).magnitude;
		this.wp = 0;
		this.vectorPath = abpath.vectorPath;
		for (float num = 0f; num <= magnitude; num += this.moveNextDist * 0.6f)
		{
			this.wp--;
			Vector3 a = originalStartPoint + (position - originalStartPoint) * num;
			Vector3 b;
			do
			{
				this.wp++;
				b = this.vectorPath[this.wp];
				b.y = a.y;
			}
			while ((a - b).sqrMagnitude < this.moveNextDist * this.moveNextDist && this.wp != this.vectorPath.Count - 1);
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00016EBC File Offset: 0x000152BC
	public void Update()
	{
		if (Time.time >= this.nextRepath && this.canSearchAgain)
		{
			this.RecalculatePath();
		}
		Vector3 velocity = this.controller.velocity;
		if (velocity != Vector3.zero)
		{
			base.transform.rotation = Quaternion.LookRotation(velocity);
		}
		Vector3 vector = Vector3.zero;
		Vector3 position = base.transform.position;
		if (this.vectorPath != null && this.vectorPath.Count != 0)
		{
			Vector3 vector2 = this.vectorPath[this.wp];
			vector2.y = position.y;
			while ((position - vector2).sqrMagnitude < this.moveNextDist * this.moveNextDist && this.wp != this.vectorPath.Count - 1)
			{
				this.wp++;
				vector2 = this.vectorPath[this.wp];
				vector2.y = position.y;
			}
			vector = vector2 - position;
			float magnitude = vector.magnitude;
			if (magnitude > 0f)
			{
				float num = Mathf.Min(magnitude, this.controller.maxSpeed);
				vector *= num / magnitude;
			}
		}
		this.controller.Move(vector);
	}

	// Token: 0x04000263 RID: 611
	public float repathRate = 1f;

	// Token: 0x04000264 RID: 612
	private float nextRepath;

	// Token: 0x04000265 RID: 613
	private Vector3 target;

	// Token: 0x04000266 RID: 614
	private bool canSearchAgain = true;

	// Token: 0x04000267 RID: 615
	private RVOController controller;

	// Token: 0x04000268 RID: 616
	private Path path;

	// Token: 0x04000269 RID: 617
	private List<Vector3> vectorPath;

	// Token: 0x0400026A RID: 618
	private int wp;

	// Token: 0x0400026B RID: 619
	public float moveNextDist = 1f;

	// Token: 0x0400026C RID: 620
	private Seeker seeker;

	// Token: 0x0400026D RID: 621
	private MeshRenderer[] rends;
}
