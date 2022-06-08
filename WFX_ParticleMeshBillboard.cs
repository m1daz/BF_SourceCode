using System;
using UnityEngine;

// Token: 0x02000533 RID: 1331
[RequireComponent(typeof(ParticleSystemRenderer))]
public class WFX_ParticleMeshBillboard : MonoBehaviour
{
	// Token: 0x060025BC RID: 9660 RVA: 0x0011885C File Offset: 0x00116C5C
	private void Awake()
	{
		this.mesh = UnityEngine.Object.Instantiate<Mesh>(base.GetComponent<ParticleSystemRenderer>().mesh);
		base.GetComponent<ParticleSystemRenderer>().mesh = this.mesh;
		this.vertices = new Vector3[this.mesh.vertices.Length];
		for (int i = 0; i < this.vertices.Length; i++)
		{
			this.vertices[i] = this.mesh.vertices[i];
		}
		this.rvertices = new Vector3[this.vertices.Length];
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x001188FC File Offset: 0x00116CFC
	private void OnWillRenderObject()
	{
		if (this.mesh == null || Camera.current == null)
		{
			return;
		}
		Quaternion rotation = Quaternion.LookRotation(Camera.current.transform.forward, Camera.current.transform.up);
		Quaternion rotation2 = Quaternion.Inverse(base.transform.rotation);
		for (int i = 0; i < this.rvertices.Length; i++)
		{
			this.rvertices[i] = rotation * this.vertices[i];
			this.rvertices[i] = rotation2 * this.rvertices[i];
		}
		this.mesh.vertices = this.rvertices;
	}

	// Token: 0x0400265B RID: 9819
	private Mesh mesh;

	// Token: 0x0400265C RID: 9820
	private Vector3[] vertices;

	// Token: 0x0400265D RID: 9821
	private Vector3[] rvertices;
}
