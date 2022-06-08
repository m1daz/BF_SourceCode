using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D5 RID: 1493
public class UIGeometry
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06002A62 RID: 10850 RVA: 0x0013E6B4 File Offset: 0x0013CAB4
	public bool hasVertices
	{
		get
		{
			return this.verts.Count > 0;
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06002A63 RID: 10851 RVA: 0x0013E6C4 File Offset: 0x0013CAC4
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.Count > 0 && this.mRtpVerts.Count == this.verts.Count;
		}
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x0013E6FD File Offset: 0x0013CAFD
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x06002A65 RID: 10853 RVA: 0x0013E72C File Offset: 0x0013CB2C
	public void ApplyTransform(Matrix4x4 widgetToPanel, bool generateNormals = true)
	{
		if (this.verts.Count > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int count = this.verts.Count;
			while (i < count)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			if (generateNormals)
			{
				this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
				Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
				this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
			}
		}
		else
		{
			this.mRtpVerts.Clear();
		}
	}

	// Token: 0x06002A66 RID: 10854 RVA: 0x0013E7FC File Offset: 0x0013CBFC
	public void WriteToBuffers(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.Count > 0)
		{
			if (n == null)
			{
				int i = 0;
				int count = this.mRtpVerts.Count;
				while (i < count)
				{
					v.Add(this.mRtpVerts[i]);
					u.Add(this.uvs[i]);
					c.Add(this.cols[i]);
					i++;
				}
			}
			else
			{
				int j = 0;
				int count2 = this.mRtpVerts.Count;
				while (j < count2)
				{
					v.Add(this.mRtpVerts[j]);
					u.Add(this.uvs[j]);
					c.Add(this.cols[j]);
					n.Add(this.mRtpNormal);
					t.Add(this.mRtpTan);
					j++;
				}
			}
			if (u2 != null)
			{
				Vector4 zero = Vector4.zero;
				int k = 0;
				int count3 = this.verts.Count;
				while (k < count3)
				{
					zero.x = this.verts[k].x;
					zero.y = this.verts[k].y;
					u2.Add(zero);
					k++;
				}
			}
			if (this.onCustomWrite != null)
			{
				this.onCustomWrite(v, u, c, n, t, u2);
			}
		}
	}

	// Token: 0x04002A55 RID: 10837
	public List<Vector3> verts = new List<Vector3>();

	// Token: 0x04002A56 RID: 10838
	public List<Vector2> uvs = new List<Vector2>();

	// Token: 0x04002A57 RID: 10839
	public List<Color> cols = new List<Color>();

	// Token: 0x04002A58 RID: 10840
	public UIGeometry.OnCustomWrite onCustomWrite;

	// Token: 0x04002A59 RID: 10841
	private List<Vector3> mRtpVerts = new List<Vector3>();

	// Token: 0x04002A5A RID: 10842
	private Vector3 mRtpNormal;

	// Token: 0x04002A5B RID: 10843
	private Vector4 mRtpTan;

	// Token: 0x020005D6 RID: 1494
	// (Invoke) Token: 0x06002A68 RID: 10856
	public delegate void OnCustomWrite(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2);
}
