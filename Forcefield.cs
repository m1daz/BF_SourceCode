using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
[AddComponentMenu("Forge3D/Force Field/Force Field")]
public class Forcefield : MonoBehaviour
{
	// Token: 0x06000E57 RID: 3671 RVA: 0x00077448 File Offset: 0x00075848
	private void Start()
	{
		this.mat = this.Field.GetComponent<Renderer>().material;
		this.mesh = this.Field.GetComponent<MeshFilter>();
		this.shaderPosID = new int[this.interpolators];
		this.shaderPowID = new int[this.interpolators];
		for (int i = 0; i < this.interpolators; i++)
		{
			this.shaderPosID[i] = Shader.PropertyToID("_Pos_" + i.ToString());
			this.shaderPowID[i] = Shader.PropertyToID("_Pow_" + i.ToString());
		}
		this.shaderPos = new Vector4[this.interpolators];
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00077510 File Offset: 0x00075910
	public void OnHit(Vector3 hitPoint, float hitPower = 0f, float hitAlpha = 1f)
	{
		if (this.curTime >= this.ReactSpeed)
		{
			Vector4 vector = this.mesh.transform.InverseTransformPoint(hitPoint);
			vector.w = Mathf.Clamp(hitAlpha, 0f, 1f);
			this.shaderPos[this.curProp] = vector;
			this.mat.SetFloat(this.shaderPowID[this.curProp], hitPower);
			this.curTime = 0f;
			this.curProp++;
			if (this.curProp == this.interpolators)
			{
				this.curProp = 0;
			}
		}
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000775C0 File Offset: 0x000759C0
	private void FadeMask()
	{
		for (int i = 0; i < this.interpolators; i++)
		{
			Vector4 b = this.shaderPos[i];
			b.w = 0f;
			this.shaderPos[i] = Vector4.Lerp(this.shaderPos[i], b, Time.deltaTime * this.DecaySpeed);
			this.mat.SetVector(this.shaderPosID[i], this.shaderPos[i]);
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0007765C File Offset: 0x00075A5C
	private void Update()
	{
		this.curTime += Time.deltaTime;
		Vector3 vector = base.transform.TransformDirection(Vector3.down);
		this.hitPoint = base.transform.position + vector.normalized * this.Length;
		this.OnHit(this.hitPoint, 0f, 1f);
		this.FadeMask();
	}

	// Token: 0x04000F5A RID: 3930
	public Material mat;

	// Token: 0x04000F5B RID: 3931
	private MeshFilter mesh;

	// Token: 0x04000F5C RID: 3932
	private int interpolators = 24;

	// Token: 0x04000F5D RID: 3933
	private int[] shaderPosID;

	// Token: 0x04000F5E RID: 3934
	private int[] shaderPowID;

	// Token: 0x04000F5F RID: 3935
	private Vector4[] shaderPos;

	// Token: 0x04000F60 RID: 3936
	private int curProp;

	// Token: 0x04000F61 RID: 3937
	private float curTime;

	// Token: 0x04000F62 RID: 3938
	public GameObject Field;

	// Token: 0x04000F63 RID: 3939
	public bool CollisionEnter;

	// Token: 0x04000F64 RID: 3940
	public bool CollisionStay;

	// Token: 0x04000F65 RID: 3941
	public bool CollisionExit;

	// Token: 0x04000F66 RID: 3942
	public float DecaySpeed = 2f;

	// Token: 0x04000F67 RID: 3943
	public float ReactSpeed = 0.2f;

	// Token: 0x04000F68 RID: 3944
	public Vector3 hitPoint;

	// Token: 0x04000F69 RID: 3945
	public float Length = 1f;
}
