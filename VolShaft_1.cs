using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class VolShaft_1 : MonoBehaviour
{
	// Token: 0x06000A76 RID: 2678 RVA: 0x0004C0DC File Offset: 0x0004A4DC
	private void OnPostRender()
	{
		if (!base.enabled)
		{
			return;
		}
		Vector4 lightPos;
		if (this.shadowLight.type == LightType.Directional)
		{
			Vector3 direction = -this.shadowLight.transform.forward;
			direction = base.transform.InverseTransformDirection(direction);
			lightPos = new Vector4(direction.x, direction.y, -direction.z, 0f);
		}
		else
		{
			Vector3 position = this.shadowLight.transform.position;
			position = base.transform.InverseTransformPoint(position);
			lightPos = new Vector4(position.x, position.y, -position.z, 1f);
		}
		for (int i = 0; i < this.objs.Length; i++)
		{
			this.DrawShaft(this.objs[i], lightPos, this.mats[i]);
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0004C1C0 File Offset: 0x0004A5C0
	private void DrawShaft(MeshFilter obj, Vector4 lightPos, Material exMat)
	{
		exMat.SetVector("litPos", lightPos);
		Mesh sharedMesh = obj.sharedMesh;
		Transform transform = obj.transform;
		exMat.SetPass(0);
		Graphics.DrawMeshNow(sharedMesh, transform.localToWorldMatrix);
	}

	// Token: 0x04000950 RID: 2384
	public Light shadowLight;

	// Token: 0x04000951 RID: 2385
	public int pass;

	// Token: 0x04000952 RID: 2386
	public MeshFilter[] objs;

	// Token: 0x04000953 RID: 2387
	public Material[] mats;
}
