using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

// Token: 0x020005C9 RID: 1481
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06002A03 RID: 10755 RVA: 0x0013CBE8 File Offset: 0x0013AFE8
	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0013CBEF File Offset: 0x0013AFEF
	public static BetterList<UIDrawCall> activeList
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0013CBF6 File Offset: 0x0013AFF6
	public static BetterList<UIDrawCall> inactiveList
	{
		get
		{
			return UIDrawCall.mInactiveList;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06002A06 RID: 10758 RVA: 0x0013CBFD File Offset: 0x0013AFFD
	// (set) Token: 0x06002A07 RID: 10759 RVA: 0x0013CC05 File Offset: 0x0013B005
	public int renderQueue
	{
		get
		{
			return this.mRenderQueue;
		}
		set
		{
			if (this.mRenderQueue != value)
			{
				this.mRenderQueue = value;
				if (this.mDynamicMat != null)
				{
					this.mDynamicMat.renderQueue = value;
				}
			}
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06002A08 RID: 10760 RVA: 0x0013CC37 File Offset: 0x0013B037
	// (set) Token: 0x06002A09 RID: 10761 RVA: 0x0013CC3F File Offset: 0x0013B03F
	public int sortingOrder
	{
		get
		{
			return this.mSortingOrder;
		}
		set
		{
			if (this.mSortingOrder != value)
			{
				this.mSortingOrder = value;
				if (this.mRenderer != null)
				{
					this.mRenderer.sortingOrder = value;
				}
			}
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0013CC74 File Offset: 0x0013B074
	// (set) Token: 0x06002A0B RID: 10763 RVA: 0x0013CCC2 File Offset: 0x0013B0C2
	public string sortingLayerName
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mSortingLayerName))
			{
				return this.mSortingLayerName;
			}
			if (this.mRenderer == null)
			{
				return null;
			}
			this.mSortingLayerName = this.mRenderer.sortingLayerName;
			return this.mSortingLayerName;
		}
		set
		{
			if (this.mRenderer != null && this.mSortingLayerName != value)
			{
				this.mSortingLayerName = value;
				this.mRenderer.sortingLayerName = value;
			}
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06002A0C RID: 10764 RVA: 0x0013CCF9 File Offset: 0x0013B0F9
	public int finalRenderQueue
	{
		get
		{
			return (!(this.mDynamicMat != null)) ? this.mRenderQueue : this.mDynamicMat.renderQueue;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06002A0D RID: 10765 RVA: 0x0013CD22 File Offset: 0x0013B122
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06002A0E RID: 10766 RVA: 0x0013CD47 File Offset: 0x0013B147
	// (set) Token: 0x06002A0F RID: 10767 RVA: 0x0013CD4F File Offset: 0x0013B14F
	public Material baseMaterial
	{
		get
		{
			return this.mMaterial;
		}
		set
		{
			if (this.mMaterial != value)
			{
				this.mMaterial = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06002A10 RID: 10768 RVA: 0x0013CD70 File Offset: 0x0013B170
	public Material dynamicMaterial
	{
		get
		{
			return this.mDynamicMat;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06002A11 RID: 10769 RVA: 0x0013CD78 File Offset: 0x0013B178
	// (set) Token: 0x06002A12 RID: 10770 RVA: 0x0013CD80 File Offset: 0x0013B180
	public Texture mainTexture
	{
		get
		{
			return this.mTexture;
		}
		set
		{
			this.mTexture = value;
			if (this.mBlock == null)
			{
				this.mBlock = new MaterialPropertyBlock();
			}
			this.mBlock.SetTexture("_MainTex", value ?? Texture2D.whiteTexture);
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06002A13 RID: 10771 RVA: 0x0013CDBC File Offset: 0x0013B1BC
	// (set) Token: 0x06002A14 RID: 10772 RVA: 0x0013CDC4 File Offset: 0x0013B1C4
	public Shader shader
	{
		get
		{
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				this.mShader = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06002A15 RID: 10773 RVA: 0x0013CDE5 File Offset: 0x0013B1E5
	// (set) Token: 0x06002A16 RID: 10774 RVA: 0x0013CDF0 File Offset: 0x0013B1F0
	public UIDrawCall.ShadowMode shadowMode
	{
		get
		{
			return this.mShadowMode;
		}
		set
		{
			if (this.mShadowMode != value)
			{
				this.mShadowMode = value;
				if (this.mRenderer != null)
				{
					if (this.mShadowMode == UIDrawCall.ShadowMode.None)
					{
						this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
						this.mRenderer.receiveShadows = false;
					}
					else if (this.mShadowMode == UIDrawCall.ShadowMode.Receive)
					{
						this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
						this.mRenderer.receiveShadows = true;
					}
					else
					{
						this.mRenderer.shadowCastingMode = ShadowCastingMode.On;
						this.mRenderer.receiveShadows = true;
					}
				}
			}
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06002A17 RID: 10775 RVA: 0x0013CE8A File Offset: 0x0013B28A
	public int triangles
	{
		get
		{
			return (!(this.mMesh != null)) ? 0 : this.mTriangles;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06002A18 RID: 10776 RVA: 0x0013CEA9 File Offset: 0x0013B2A9
	public bool isClipped
	{
		get
		{
			return this.mClipCount != 0;
		}
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x0013CEB8 File Offset: 0x0013B2B8
	private void CreateMaterial()
	{
		this.mTextureClip = false;
		this.mLegacyShader = false;
		this.mClipCount = this.panel.clipCount;
		string text = (!(this.mShader != null)) ? ((!(this.mMaterial != null)) ? "Unlit/Transparent Colored" : this.mMaterial.shader.name) : this.mShader.name;
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text[text.Length - 2] == ' ')
		{
			int num = (int)text[text.Length - 1];
			if (num > 48 && num <= 57)
			{
				text = text.Substring(0, text.Length - 2);
			}
		}
		if (text.StartsWith("Hidden/"))
		{
			text = text.Substring(7);
		}
		text = text.Replace(" (SoftClip)", string.Empty);
		text = text.Replace(" (TextureClip)", string.Empty);
		if (this.panel != null && this.panel.clipping == UIDrawCall.Clipping.TextureMask)
		{
			this.mTextureClip = true;
			this.shader = Shader.Find("Hidden/" + text + " (TextureClip)");
		}
		else if (this.mClipCount != 0)
		{
			this.shader = Shader.Find(string.Concat(new object[]
			{
				"Hidden/",
				text,
				" ",
				this.mClipCount
			}));
			if (this.shader == null)
			{
				this.shader = Shader.Find(text + " " + this.mClipCount);
			}
			if (this.shader == null && this.mClipCount == 1)
			{
				this.mLegacyShader = true;
				this.shader = Shader.Find(text + " (SoftClip)");
			}
		}
		else
		{
			this.shader = Shader.Find(text);
		}
		if (this.shader == null)
		{
			this.shader = Shader.Find("Unlit/Transparent Colored");
		}
		if (this.mMaterial != null)
		{
			this.mDynamicMat = new Material(this.mMaterial);
			this.mDynamicMat.name = "[NGUI] " + this.mMaterial.name;
			this.mDynamicMat.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			this.mDynamicMat.CopyPropertiesFromMaterial(this.mMaterial);
			string[] shaderKeywords = this.mMaterial.shaderKeywords;
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				this.mDynamicMat.EnableKeyword(shaderKeywords[i]);
			}
			if (this.shader != null)
			{
				this.mDynamicMat.shader = this.shader;
			}
			else if (this.mClipCount != 0)
			{
				Debug.LogError(string.Concat(new object[]
				{
					text,
					" shader doesn't have a clipped shader version for ",
					this.mClipCount,
					" clip regions"
				}));
			}
		}
		else
		{
			this.mDynamicMat = new Material(this.shader);
			this.mDynamicMat.name = "[NGUI] " + this.shader.name;
			this.mDynamicMat.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		}
	}

	// Token: 0x06002A1A RID: 10778 RVA: 0x0013D22C File Offset: 0x0013B62C
	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.CreateMaterial();
		this.mDynamicMat.renderQueue = this.mRenderQueue;
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[]
			{
				this.mDynamicMat
			};
			this.mRenderer.sortingLayerName = this.mSortingLayerName;
			this.mRenderer.sortingOrder = this.mSortingOrder;
		}
		return this.mDynamicMat;
	}

	// Token: 0x06002A1B RID: 10779 RVA: 0x0013D2B0 File Offset: 0x0013B6B0
	private void UpdateMaterials()
	{
		if (this.panel == null)
		{
			return;
		}
		if (this.mRebuildMat || this.mDynamicMat == null || this.mClipCount != this.panel.clipCount || this.mTextureClip != (this.panel.clipping == UIDrawCall.Clipping.TextureMask))
		{
			this.RebuildMaterial();
			this.mRebuildMat = false;
		}
	}

	// Token: 0x06002A1C RID: 10780 RVA: 0x0013D328 File Offset: 0x0013B728
	public void UpdateGeometry(int widgetCount)
	{
		this.widgetCount = widgetCount;
		int count = this.verts.Count;
		if (count > 0 && count == this.uvs.Count && count == this.cols.Count && count % 4 == 0)
		{
			if (UIDrawCall.mColorSpace == ColorSpace.Uninitialized)
			{
				UIDrawCall.mColorSpace = QualitySettings.activeColorSpace;
			}
			if (UIDrawCall.mColorSpace == ColorSpace.Linear)
			{
				for (int i = 0; i < count; i++)
				{
					Color value = this.cols[i];
					value.r = Mathf.GammaToLinearSpace(value.r);
					value.g = Mathf.GammaToLinearSpace(value.g);
					value.b = Mathf.GammaToLinearSpace(value.b);
					value.a = Mathf.GammaToLinearSpace(value.a);
					this.cols[i] = value;
				}
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (count < 65000)
			{
				int num = (count >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (this.mMesh == null)
				{
					this.mMesh = new Mesh();
					this.mMesh.hideFlags = HideFlags.DontSave;
					this.mMesh.name = ((!(this.mMaterial != null)) ? "[NGUI] Mesh" : ("[NGUI] " + this.mMaterial.name));
					if (UIDrawCall.dx9BugWorkaround == 0)
					{
						this.mMesh.MarkDynamic();
					}
					flag = true;
				}
				bool flag2 = this.uvs.Count != count || this.cols.Count != count || this.uv2.Count != count || this.norms.Count != count || this.tans.Count != count;
				if (!flag2 && this.panel != null && this.panel.renderQueue != UIPanel.RenderQueue.Automatic)
				{
					flag2 = (this.mMesh == null || this.mMesh.vertexCount != this.verts.Count);
				}
				this.mTriangles = count >> 1;
				if (this.mMesh.vertexCount != count)
				{
					this.mMesh.Clear();
					flag = true;
				}
				this.mMesh.SetVertices(this.verts);
				this.mMesh.SetUVs(0, this.uvs);
				this.mMesh.SetColors(this.cols);
				this.mMesh.SetUVs(1, (this.uv2.Count != count) ? null : this.uv2);
				this.mMesh.SetNormals((this.norms.Count != count) ? null : this.norms);
				this.mMesh.SetTangents((this.tans.Count != count) ? null : this.tans);
				if (flag)
				{
					this.mIndices = this.GenerateCachedIndexBuffer(count, num);
					this.mMesh.triangles = this.mIndices;
				}
				if (flag2 || !this.alwaysOnScreen)
				{
					this.mMesh.RecalculateBounds();
				}
				this.mFilter.mesh = this.mMesh;
			}
			else
			{
				this.mTriangles = 0;
				if (this.mMesh != null)
				{
					this.mMesh.Clear();
				}
				Debug.LogError("Too many vertices on one panel: " + count);
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.AddComponent<MeshRenderer>();
				if (this.mShadowMode == UIDrawCall.ShadowMode.None)
				{
					this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
					this.mRenderer.receiveShadows = false;
				}
				else if (this.mShadowMode == UIDrawCall.ShadowMode.Receive)
				{
					this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
					this.mRenderer.receiveShadows = true;
				}
				else
				{
					this.mRenderer.shadowCastingMode = ShadowCastingMode.On;
					this.mRenderer.receiveShadows = true;
				}
			}
			if (this.mIsNew)
			{
				this.mIsNew = false;
				if (this.onCreateDrawCall != null)
				{
					this.onCreateDrawCall(this, this.mFilter, this.mRenderer);
				}
			}
			this.UpdateMaterials();
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + count);
		}
		this.verts.Clear();
		this.uvs.Clear();
		this.uv2.Clear();
		this.cols.Clear();
		this.norms.Clear();
		this.tans.Clear();
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x0013D884 File Offset: 0x0013BC84
	private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
	{
		int i = 0;
		int count = UIDrawCall.mCache.Count;
		while (i < count)
		{
			int[] array = UIDrawCall.mCache[i];
			if (array != null && array.Length == indexCount)
			{
				return array;
			}
			i++;
		}
		int[] array2 = new int[indexCount];
		int num = 0;
		for (int j = 0; j < vertexCount; j += 4)
		{
			array2[num++] = j;
			array2[num++] = j + 1;
			array2[num++] = j + 2;
			array2[num++] = j + 2;
			array2[num++] = j + 3;
			array2[num++] = j;
		}
		if (UIDrawCall.mCache.Count > 10)
		{
			UIDrawCall.mCache.RemoveAt(0);
		}
		UIDrawCall.mCache.Add(array2);
		return array2;
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x0013D960 File Offset: 0x0013BD60
	private void OnWillRenderObject()
	{
		this.UpdateMaterials();
		if (this.mBlock != null)
		{
			this.mRenderer.SetPropertyBlock(this.mBlock);
		}
		if (this.onRender != null)
		{
			this.onRender(this.mDynamicMat ?? this.mMaterial);
		}
		if (this.mDynamicMat == null || this.mClipCount == 0)
		{
			return;
		}
		if (this.mTextureClip)
		{
			Vector4 drawCallClipRange = this.panel.drawCallClipRange;
			Vector2 clipSoftness = this.panel.clipSoftness;
			Vector2 vector = new Vector2(1000f, 1000f);
			if (clipSoftness.x > 0f)
			{
				vector.x = drawCallClipRange.z / clipSoftness.x;
			}
			if (clipSoftness.y > 0f)
			{
				vector.y = drawCallClipRange.w / clipSoftness.y;
			}
			this.mDynamicMat.SetVector(UIDrawCall.ClipRange[0], new Vector4(-drawCallClipRange.x / drawCallClipRange.z, -drawCallClipRange.y / drawCallClipRange.w, 1f / drawCallClipRange.z, 1f / drawCallClipRange.w));
			this.mDynamicMat.SetTexture("_ClipTex", this.clipTexture);
		}
		else if (!this.mLegacyShader)
		{
			UIPanel parentPanel = this.panel;
			int num = 0;
			while (parentPanel != null)
			{
				if (parentPanel.hasClipping)
				{
					float angle = 0f;
					Vector4 drawCallClipRange2 = parentPanel.drawCallClipRange;
					if (parentPanel != this.panel)
					{
						Vector3 vector2 = parentPanel.cachedTransform.InverseTransformPoint(this.panel.cachedTransform.position);
						drawCallClipRange2.x -= vector2.x;
						drawCallClipRange2.y -= vector2.y;
						Vector3 eulerAngles = this.panel.cachedTransform.rotation.eulerAngles;
						Vector3 eulerAngles2 = parentPanel.cachedTransform.rotation.eulerAngles;
						Vector3 vector3 = eulerAngles2 - eulerAngles;
						vector3.x = NGUIMath.WrapAngle(vector3.x);
						vector3.y = NGUIMath.WrapAngle(vector3.y);
						vector3.z = NGUIMath.WrapAngle(vector3.z);
						if (Mathf.Abs(vector3.x) > 0.001f || Mathf.Abs(vector3.y) > 0.001f)
						{
							Debug.LogWarning("Panel can only be clipped properly if X and Y rotation is left at 0", this.panel);
						}
						angle = vector3.z;
					}
					this.SetClipping(num++, drawCallClipRange2, parentPanel.clipSoftness, angle);
				}
				parentPanel = parentPanel.parentPanel;
			}
		}
		else
		{
			Vector2 clipSoftness2 = this.panel.clipSoftness;
			Vector4 drawCallClipRange3 = this.panel.drawCallClipRange;
			Vector2 mainTextureOffset = new Vector2(-drawCallClipRange3.x / drawCallClipRange3.z, -drawCallClipRange3.y / drawCallClipRange3.w);
			Vector2 mainTextureScale = new Vector2(1f / drawCallClipRange3.z, 1f / drawCallClipRange3.w);
			Vector2 v = new Vector2(1000f, 1000f);
			if (clipSoftness2.x > 0f)
			{
				v.x = drawCallClipRange3.z / clipSoftness2.x;
			}
			if (clipSoftness2.y > 0f)
			{
				v.y = drawCallClipRange3.w / clipSoftness2.y;
			}
			this.mDynamicMat.mainTextureOffset = mainTextureOffset;
			this.mDynamicMat.mainTextureScale = mainTextureScale;
			this.mDynamicMat.SetVector("_ClipSharpness", v);
		}
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x0013DD2C File Offset: 0x0013C12C
	private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
	{
		angle *= -0.017453292f;
		Vector2 vector = new Vector2(1000f, 1000f);
		if (soft.x > 0f)
		{
			vector.x = cr.z / soft.x;
		}
		if (soft.y > 0f)
		{
			vector.y = cr.w / soft.y;
		}
		if (index < UIDrawCall.ClipRange.Length)
		{
			this.mDynamicMat.SetVector(UIDrawCall.ClipRange[index], new Vector4(-cr.x / cr.z, -cr.y / cr.w, 1f / cr.z, 1f / cr.w));
			this.mDynamicMat.SetVector(UIDrawCall.ClipArgs[index], new Vector4(vector.x, vector.y, Mathf.Sin(angle), Mathf.Cos(angle)));
		}
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x0013DE34 File Offset: 0x0013C234
	private void Awake()
	{
		if (UIDrawCall.dx9BugWorkaround == -1)
		{
			RuntimePlatform platform = Application.platform;
			UIDrawCall.dx9BugWorkaround = ((platform != RuntimePlatform.WindowsPlayer || SystemInfo.graphicsShaderLevel >= 40 || !SystemInfo.graphicsDeviceVersion.Contains("Direct3D")) ? 0 : 1);
		}
		if (UIDrawCall.ClipRange == null)
		{
			UIDrawCall.ClipRange = new int[]
			{
				Shader.PropertyToID("_ClipRange0"),
				Shader.PropertyToID("_ClipRange1"),
				Shader.PropertyToID("_ClipRange2"),
				Shader.PropertyToID("_ClipRange4")
			};
		}
		if (UIDrawCall.ClipArgs == null)
		{
			UIDrawCall.ClipArgs = new int[]
			{
				Shader.PropertyToID("_ClipArgs0"),
				Shader.PropertyToID("_ClipArgs1"),
				Shader.PropertyToID("_ClipArgs2"),
				Shader.PropertyToID("_ClipArgs3")
			};
		}
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x0013DF17 File Offset: 0x0013C317
	private void OnEnable()
	{
		this.mRebuildMat = true;
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x0013DF20 File Offset: 0x0013C320
	private void OnDisable()
	{
		this.depthStart = int.MaxValue;
		this.depthEnd = int.MinValue;
		this.panel = null;
		this.manager = null;
		this.mMaterial = null;
		this.mTexture = null;
		this.clipTexture = null;
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[0];
		}
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.mDynamicMat = null;
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x0013DF9A File Offset: 0x0013C39A
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh);
		this.mMesh = null;
	}

	// Token: 0x06002A24 RID: 10788 RVA: 0x0013DFAE File Offset: 0x0013C3AE
	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return UIDrawCall.Create(null, panel, mat, tex, shader);
	}

	// Token: 0x06002A25 RID: 10789 RVA: 0x0013DFBC File Offset: 0x0013C3BC
	private static UIDrawCall Create(string name, UIPanel pan, Material mat, Texture tex, Shader shader)
	{
		UIDrawCall uidrawCall = UIDrawCall.Create(name);
		uidrawCall.gameObject.layer = pan.cachedGameObject.layer;
		uidrawCall.baseMaterial = mat;
		uidrawCall.mainTexture = tex;
		uidrawCall.shader = shader;
		uidrawCall.renderQueue = pan.startingRenderQueue;
		uidrawCall.sortingOrder = pan.sortingOrder;
		uidrawCall.manager = pan;
		return uidrawCall;
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x0013E01C File Offset: 0x0013C41C
	private static UIDrawCall Create(string name)
	{
		while (UIDrawCall.mInactiveList.size > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList.Pop();
			if (uidrawCall != null)
			{
				UIDrawCall.mActiveList.Add(uidrawCall);
				if (name != null)
				{
					uidrawCall.name = name;
				}
				NGUITools.SetActive(uidrawCall.gameObject, true);
				return uidrawCall;
			}
		}
		GameObject gameObject = new GameObject(name);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		UIDrawCall uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
		UIDrawCall.mActiveList.Add(uidrawCall2);
		return uidrawCall2;
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x0013E09C File Offset: 0x0013C49C
	public static void ClearAll()
	{
		bool isPlaying = Application.isPlaying;
		int i = UIDrawCall.mActiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mActiveList[--i];
			if (uidrawCall)
			{
				if (isPlaying)
				{
					NGUITools.SetActive(uidrawCall.gameObject, false);
				}
				else
				{
					NGUITools.DestroyImmediate(uidrawCall.gameObject);
				}
			}
		}
		UIDrawCall.mActiveList.Clear();
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x0013E10D File Offset: 0x0013C50D
	public static void ReleaseAll()
	{
		UIDrawCall.ClearAll();
		UIDrawCall.ReleaseInactive();
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x0013E11C File Offset: 0x0013C51C
	public static void ReleaseInactive()
	{
		int i = UIDrawCall.mInactiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList[--i];
			if (uidrawCall)
			{
				NGUITools.DestroyImmediate(uidrawCall.gameObject);
			}
		}
		UIDrawCall.mInactiveList.Clear();
	}

	// Token: 0x06002A2A RID: 10794 RVA: 0x0013E170 File Offset: 0x0013C570
	public static int Count(UIPanel panel)
	{
		int num = 0;
		for (int i = 0; i < UIDrawCall.mActiveList.size; i++)
		{
			if (UIDrawCall.mActiveList[i].manager == panel)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06002A2B RID: 10795 RVA: 0x0013E1BC File Offset: 0x0013C5BC
	public static void Destroy(UIDrawCall dc)
	{
		if (dc)
		{
			if (dc.onCreateDrawCall != null)
			{
				NGUITools.Destroy(dc.gameObject);
				return;
			}
			dc.onRender = null;
			if (Application.isPlaying)
			{
				if (UIDrawCall.mActiveList.Remove(dc))
				{
					NGUITools.SetActive(dc.gameObject, false);
					UIDrawCall.mInactiveList.Add(dc);
					dc.mIsNew = true;
				}
			}
			else
			{
				UIDrawCall.mActiveList.Remove(dc);
				NGUITools.DestroyImmediate(dc.gameObject);
			}
		}
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x0013E248 File Offset: 0x0013C648
	public static void MoveToScene(Scene scene)
	{
		foreach (UIDrawCall uidrawCall in UIDrawCall.activeList)
		{
			SceneManager.MoveGameObjectToScene(uidrawCall.gameObject, scene);
		}
		foreach (UIDrawCall uidrawCall2 in UIDrawCall.inactiveList)
		{
			SceneManager.MoveGameObjectToScene(uidrawCall2.gameObject, scene);
		}
	}

	// Token: 0x04002A0F RID: 10767
	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	// Token: 0x04002A10 RID: 10768
	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	// Token: 0x04002A11 RID: 10769
	[HideInInspector]
	[NonSerialized]
	public int widgetCount;

	// Token: 0x04002A12 RID: 10770
	[HideInInspector]
	[NonSerialized]
	public int depthStart = int.MaxValue;

	// Token: 0x04002A13 RID: 10771
	[HideInInspector]
	[NonSerialized]
	public int depthEnd = int.MinValue;

	// Token: 0x04002A14 RID: 10772
	[HideInInspector]
	[NonSerialized]
	public UIPanel manager;

	// Token: 0x04002A15 RID: 10773
	[HideInInspector]
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x04002A16 RID: 10774
	[HideInInspector]
	[NonSerialized]
	public Texture2D clipTexture;

	// Token: 0x04002A17 RID: 10775
	[HideInInspector]
	[NonSerialized]
	public bool alwaysOnScreen;

	// Token: 0x04002A18 RID: 10776
	[HideInInspector]
	[NonSerialized]
	public List<Vector3> verts = new List<Vector3>();

	// Token: 0x04002A19 RID: 10777
	[HideInInspector]
	[NonSerialized]
	public List<Vector3> norms = new List<Vector3>();

	// Token: 0x04002A1A RID: 10778
	[HideInInspector]
	[NonSerialized]
	public List<Vector4> tans = new List<Vector4>();

	// Token: 0x04002A1B RID: 10779
	[HideInInspector]
	[NonSerialized]
	public List<Vector2> uvs = new List<Vector2>();

	// Token: 0x04002A1C RID: 10780
	[HideInInspector]
	[NonSerialized]
	public List<Vector4> uv2 = new List<Vector4>();

	// Token: 0x04002A1D RID: 10781
	[HideInInspector]
	[NonSerialized]
	public List<Color> cols = new List<Color>();

	// Token: 0x04002A1E RID: 10782
	[NonSerialized]
	private Material mMaterial;

	// Token: 0x04002A1F RID: 10783
	[NonSerialized]
	private Texture mTexture;

	// Token: 0x04002A20 RID: 10784
	[NonSerialized]
	private Shader mShader;

	// Token: 0x04002A21 RID: 10785
	[NonSerialized]
	private int mClipCount;

	// Token: 0x04002A22 RID: 10786
	[NonSerialized]
	private Transform mTrans;

	// Token: 0x04002A23 RID: 10787
	[NonSerialized]
	private Mesh mMesh;

	// Token: 0x04002A24 RID: 10788
	[NonSerialized]
	private MeshFilter mFilter;

	// Token: 0x04002A25 RID: 10789
	[NonSerialized]
	private MeshRenderer mRenderer;

	// Token: 0x04002A26 RID: 10790
	[NonSerialized]
	private Material mDynamicMat;

	// Token: 0x04002A27 RID: 10791
	[NonSerialized]
	private int[] mIndices;

	// Token: 0x04002A28 RID: 10792
	[NonSerialized]
	private UIDrawCall.ShadowMode mShadowMode;

	// Token: 0x04002A29 RID: 10793
	[NonSerialized]
	private bool mRebuildMat = true;

	// Token: 0x04002A2A RID: 10794
	[NonSerialized]
	private bool mLegacyShader;

	// Token: 0x04002A2B RID: 10795
	[NonSerialized]
	private int mRenderQueue = 3000;

	// Token: 0x04002A2C RID: 10796
	[NonSerialized]
	private int mTriangles;

	// Token: 0x04002A2D RID: 10797
	[NonSerialized]
	public bool isDirty;

	// Token: 0x04002A2E RID: 10798
	[NonSerialized]
	private bool mTextureClip;

	// Token: 0x04002A2F RID: 10799
	[NonSerialized]
	private bool mIsNew = true;

	// Token: 0x04002A30 RID: 10800
	public UIDrawCall.OnRenderCallback onRender;

	// Token: 0x04002A31 RID: 10801
	public UIDrawCall.OnCreateDrawCall onCreateDrawCall;

	// Token: 0x04002A32 RID: 10802
	[NonSerialized]
	private string mSortingLayerName;

	// Token: 0x04002A33 RID: 10803
	[NonSerialized]
	private int mSortingOrder;

	// Token: 0x04002A34 RID: 10804
	private static ColorSpace mColorSpace = ColorSpace.Uninitialized;

	// Token: 0x04002A35 RID: 10805
	private const int maxIndexBufferCache = 10;

	// Token: 0x04002A36 RID: 10806
	private static List<int[]> mCache = new List<int[]>(10);

	// Token: 0x04002A37 RID: 10807
	protected MaterialPropertyBlock mBlock;

	// Token: 0x04002A38 RID: 10808
	private static int[] ClipRange = null;

	// Token: 0x04002A39 RID: 10809
	private static int[] ClipArgs = null;

	// Token: 0x04002A3A RID: 10810
	private static int dx9BugWorkaround = -1;

	// Token: 0x020005CA RID: 1482
	[DoNotObfuscateNGUI]
	public enum Clipping
	{
		// Token: 0x04002A3C RID: 10812
		None,
		// Token: 0x04002A3D RID: 10813
		TextureMask,
		// Token: 0x04002A3E RID: 10814
		SoftClip = 3,
		// Token: 0x04002A3F RID: 10815
		ConstrainButDontClip
	}

	// Token: 0x020005CB RID: 1483
	// (Invoke) Token: 0x06002A2F RID: 10799
	public delegate void OnRenderCallback(Material mat);

	// Token: 0x020005CC RID: 1484
	// (Invoke) Token: 0x06002A33 RID: 10803
	public delegate void OnCreateDrawCall(UIDrawCall dc, MeshFilter filter, MeshRenderer ren);

	// Token: 0x020005CD RID: 1485
	[DoNotObfuscateNGUI]
	public enum ShadowMode
	{
		// Token: 0x04002A41 RID: 10817
		None,
		// Token: 0x04002A42 RID: 10818
		Receive,
		// Token: 0x04002A43 RID: 10819
		CastAndReceive
	}
}
