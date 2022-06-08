using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000531 RID: 1329
[RequireComponent(typeof(MeshFilter))]
public class WFX_BulletHoleDecal : MonoBehaviour
{
	// Token: 0x060025B4 RID: 9652 RVA: 0x001183AD File Offset: 0x001167AD
	private void Awake()
	{
		this.color = base.GetComponent<Renderer>().material.GetColor("_TintColor");
		this.orgAlpha = this.color.a;
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x001183DC File Offset: 0x001167DC
	private void OnEnable()
	{
		int num = UnityEngine.Random.Range(0, (int)(this.frames.x * this.frames.y));
		int num2 = (int)((float)num % this.frames.x);
		int num3 = (int)((float)num / this.frames.y);
		Vector2[] array = new Vector2[4];
		for (int i = 0; i < 4; i++)
		{
			array[i].x = (WFX_BulletHoleDecal.quadUVs[i].x + (float)num2) * (1f / this.frames.x);
			array[i].y = (WFX_BulletHoleDecal.quadUVs[i].y + (float)num3) * (1f / this.frames.y);
		}
		base.GetComponent<MeshFilter>().mesh.uv = array;
		if (this.randomRotation)
		{
			base.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f), Space.Self);
		}
		this.life = this.lifetime;
		this.fadeout = this.life * (this.fadeoutpercent / 100f);
		this.color.a = this.orgAlpha;
		base.GetComponent<Renderer>().material.SetColor("_TintColor", this.color);
		base.StopAllCoroutines();
		base.StartCoroutine("holeUpdate");
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x00118550 File Offset: 0x00116950
	private IEnumerator holeUpdate()
	{
		while (this.life > 0f)
		{
			this.life -= Time.deltaTime;
			if (this.life <= this.fadeout)
			{
				this.color.a = Mathf.Lerp(0f, this.orgAlpha, this.life / this.fadeout);
				base.GetComponent<Renderer>().material.SetColor("_TintColor", this.color);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400264F RID: 9807
	private static Vector2[] quadUVs = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 0f),
		new Vector2(1f, 1f)
	};

	// Token: 0x04002650 RID: 9808
	public float lifetime = 10f;

	// Token: 0x04002651 RID: 9809
	public float fadeoutpercent = 80f;

	// Token: 0x04002652 RID: 9810
	public Vector2 frames;

	// Token: 0x04002653 RID: 9811
	public bool randomRotation;

	// Token: 0x04002654 RID: 9812
	public bool deactivate;

	// Token: 0x04002655 RID: 9813
	private float life;

	// Token: 0x04002656 RID: 9814
	private float fadeout;

	// Token: 0x04002657 RID: 9815
	private Color color;

	// Token: 0x04002658 RID: 9816
	private float orgAlpha;
}
