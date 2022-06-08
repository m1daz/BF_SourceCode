using System;
using UnityEngine;

// Token: 0x020005D7 RID: 1495
public abstract class UIRect : MonoBehaviour
{
	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06002A6C RID: 10860 RVA: 0x00136ECF File Offset: 0x001352CF
	public GameObject cachedGameObject
	{
		get
		{
			if (this.mGo == null)
			{
				this.mGo = base.gameObject;
			}
			return this.mGo;
		}
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06002A6D RID: 10861 RVA: 0x00136EF4 File Offset: 0x001352F4
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

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06002A6E RID: 10862 RVA: 0x00136F19 File Offset: 0x00135319
	public Camera anchorCamera
	{
		get
		{
			if (!this.mCam || !this.mAnchorsCached)
			{
				this.ResetAnchors();
			}
			return this.mCam;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06002A6F RID: 10863 RVA: 0x00136F44 File Offset: 0x00135344
	public bool isFullyAnchored
	{
		get
		{
			return this.leftAnchor.target && this.rightAnchor.target && this.topAnchor.target && this.bottomAnchor.target;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06002A70 RID: 10864 RVA: 0x00136FA3 File Offset: 0x001353A3
	public virtual bool isAnchoredHorizontally
	{
		get
		{
			return this.leftAnchor.target || this.rightAnchor.target;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06002A71 RID: 10865 RVA: 0x00136FCD File Offset: 0x001353CD
	public virtual bool isAnchoredVertically
	{
		get
		{
			return this.bottomAnchor.target || this.topAnchor.target;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06002A72 RID: 10866 RVA: 0x00136FF7 File Offset: 0x001353F7
	public virtual bool canBeAnchored
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06002A73 RID: 10867 RVA: 0x00136FFA File Offset: 0x001353FA
	public UIRect parent
	{
		get
		{
			if (!this.mParentFound)
			{
				this.mParentFound = true;
				this.mParent = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
			}
			return this.mParent;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06002A74 RID: 10868 RVA: 0x0013702C File Offset: 0x0013542C
	public UIRoot root
	{
		get
		{
			if (this.parent != null)
			{
				return this.mParent.root;
			}
			if (!this.mRootSet)
			{
				this.mRootSet = true;
				this.mRoot = NGUITools.FindInParents<UIRoot>(this.cachedTransform);
			}
			return this.mRoot;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06002A75 RID: 10869 RVA: 0x00137080 File Offset: 0x00135480
	public bool isAnchored
	{
		get
		{
			return (this.leftAnchor.target || this.rightAnchor.target || this.topAnchor.target || this.bottomAnchor.target) && this.canBeAnchored;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06002A76 RID: 10870
	// (set) Token: 0x06002A77 RID: 10871
	public abstract float alpha { get; set; }

	// Token: 0x06002A78 RID: 10872
	public abstract float CalculateFinalAlpha(int frameID);

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06002A79 RID: 10873
	public abstract Vector3[] localCorners { get; }

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06002A7A RID: 10874
	public abstract Vector3[] worldCorners { get; }

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06002A7B RID: 10875 RVA: 0x001370EC File Offset: 0x001354EC
	protected float cameraRayDistance
	{
		get
		{
			if (this.anchorCamera == null)
			{
				return 0f;
			}
			if (!this.mCam.orthographic)
			{
				Transform cachedTransform = this.cachedTransform;
				Transform transform = this.mCam.transform;
				Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
				Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);
				float result;
				if (plane.Raycast(ray, out result))
				{
					return result;
				}
			}
			return Mathf.Lerp(this.mCam.nearClipPlane, this.mCam.farClipPlane, 0.5f);
		}
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x001371A0 File Offset: 0x001355A0
	public virtual void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < this.mChildren.size; i++)
			{
				this.mChildren.buffer[i].Invalidate(true);
			}
		}
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x001371EC File Offset: 0x001355EC
	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		if (this.anchorCamera != null)
		{
			return this.mCam.GetSides(this.cameraRayDistance, relativeTo);
		}
		Vector3 position = this.cachedTransform.position;
		for (int i = 0; i < 4; i++)
		{
			UIRect.mSides[i] = position;
		}
		if (relativeTo != null)
		{
			for (int j = 0; j < 4; j++)
			{
				UIRect.mSides[j] = relativeTo.InverseTransformPoint(UIRect.mSides[j]);
			}
		}
		return UIRect.mSides;
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x00137298 File Offset: 0x00135698
	protected Vector3 GetLocalPos(UIRect.AnchorPoint ac, Transform trans)
	{
		if (ac.targetCam == null)
		{
			this.FindCameraFor(ac);
		}
		if (this.anchorCamera == null || ac.targetCam == null)
		{
			return this.cachedTransform.localPosition;
		}
		Rect rect = ac.targetCam.rect;
		Vector3 vector = ac.targetCam.WorldToViewportPoint(ac.target.position);
		Vector3 vector2 = new Vector3(vector.x * rect.width + rect.x, vector.y * rect.height + rect.y, vector.z);
		vector2 = this.mCam.ViewportToWorldPoint(vector2);
		if (trans != null)
		{
			vector2 = trans.InverseTransformPoint(vector2);
		}
		vector2.x = Mathf.Floor(vector2.x + 0.5f);
		vector2.y = Mathf.Floor(vector2.y + 0.5f);
		return vector2;
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x0013739E File Offset: 0x0013579E
	protected virtual void OnEnable()
	{
		this.mUpdateFrame = -1;
		if (this.updateAnchors == UIRect.AnchorUpdate.OnEnable)
		{
			this.mAnchorsCached = false;
			this.mUpdateAnchors = true;
		}
		if (this.mStarted)
		{
			this.OnInit();
		}
		this.mUpdateFrame = -1;
	}

	// Token: 0x06002A80 RID: 10880 RVA: 0x001373D8 File Offset: 0x001357D8
	protected virtual void OnInit()
	{
		this.mChanged = true;
		this.mRootSet = false;
		this.mParentFound = false;
		if (this.parent != null)
		{
			this.mParent.mChildren.Add(this);
		}
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x00137411 File Offset: 0x00135811
	protected virtual void OnDisable()
	{
		if (this.mParent)
		{
			this.mParent.mChildren.Remove(this);
		}
		this.mParent = null;
		this.mRoot = null;
		this.mRootSet = false;
		this.mParentFound = false;
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x00137451 File Offset: 0x00135851
	protected virtual void Awake()
	{
		this.mStarted = false;
		this.mGo = base.gameObject;
		this.mTrans = base.transform;
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x00137472 File Offset: 0x00135872
	protected void Start()
	{
		this.mStarted = true;
		this.OnInit();
		this.OnStart();
	}

	// Token: 0x06002A84 RID: 10884 RVA: 0x00137488 File Offset: 0x00135888
	public void Update()
	{
		if (!this.mCam)
		{
			this.ResetAndUpdateAnchors();
		}
		else if (!this.mAnchorsCached)
		{
			this.ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (this.mUpdateFrame != frameCount)
		{
			if (this.updateAnchors == UIRect.AnchorUpdate.OnUpdate || this.mUpdateAnchors)
			{
				this.UpdateAnchorsInternal(frameCount);
			}
			this.OnUpdate();
		}
	}

	// Token: 0x06002A85 RID: 10885 RVA: 0x001374F8 File Offset: 0x001358F8
	protected void UpdateAnchorsInternal(int frame)
	{
		this.mUpdateFrame = frame;
		this.mUpdateAnchors = false;
		bool flag = false;
		if (this.leftAnchor.target)
		{
			flag = true;
			if (this.leftAnchor.rect != null && this.leftAnchor.rect.mUpdateFrame != frame)
			{
				this.leftAnchor.rect.Update();
			}
		}
		if (this.bottomAnchor.target)
		{
			flag = true;
			if (this.bottomAnchor.rect != null && this.bottomAnchor.rect.mUpdateFrame != frame)
			{
				this.bottomAnchor.rect.Update();
			}
		}
		if (this.rightAnchor.target)
		{
			flag = true;
			if (this.rightAnchor.rect != null && this.rightAnchor.rect.mUpdateFrame != frame)
			{
				this.rightAnchor.rect.Update();
			}
		}
		if (this.topAnchor.target)
		{
			flag = true;
			if (this.topAnchor.rect != null && this.topAnchor.rect.mUpdateFrame != frame)
			{
				this.topAnchor.rect.Update();
			}
		}
		if (flag)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x06002A86 RID: 10886 RVA: 0x0013766D File Offset: 0x00135A6D
	public void UpdateAnchors()
	{
		if (this.isAnchored)
		{
			this.mUpdateFrame = -1;
			this.mUpdateAnchors = true;
			this.UpdateAnchorsInternal(Time.frameCount);
		}
	}

	// Token: 0x06002A87 RID: 10887
	protected abstract void OnAnchor();

	// Token: 0x06002A88 RID: 10888 RVA: 0x00137693 File Offset: 0x00135A93
	public void SetAnchor(Transform t)
	{
		this.leftAnchor.target = t;
		this.rightAnchor.target = t;
		this.topAnchor.target = t;
		this.bottomAnchor.target = t;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x001376D4 File Offset: 0x00135AD4
	public void SetAnchor(GameObject go)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x00137738 File Offset: 0x00135B38
	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = 0f;
		this.rightAnchor.relative = 1f;
		this.bottomAnchor.relative = 0f;
		this.topAnchor.relative = 1f;
		this.leftAnchor.absolute = left;
		this.rightAnchor.absolute = right;
		this.bottomAnchor.absolute = bottom;
		this.topAnchor.absolute = top;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x0013780C File Offset: 0x00135C0C
	public void SetAnchor(GameObject go, float left, float bottom, float right, float top)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = 0;
		this.rightAnchor.absolute = 0;
		this.bottomAnchor.absolute = 0;
		this.topAnchor.absolute = 0;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A8C RID: 10892 RVA: 0x001378D0 File Offset: 0x00135CD0
	public void SetAnchor(GameObject go, float left, int leftOffset, float bottom, int bottomOffset, float right, int rightOffset, float top, int topOffset)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = leftOffset;
		this.rightAnchor.absolute = rightOffset;
		this.bottomAnchor.absolute = bottomOffset;
		this.topAnchor.absolute = topOffset;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A8D RID: 10893 RVA: 0x00137998 File Offset: 0x00135D98
	public void SetAnchor(float left, int leftOffset, float bottom, int bottomOffset, float right, int rightOffset, float top, int topOffset)
	{
		Transform parent = this.cachedTransform.parent;
		this.leftAnchor.target = parent;
		this.rightAnchor.target = parent;
		this.topAnchor.target = parent;
		this.bottomAnchor.target = parent;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = leftOffset;
		this.rightAnchor.absolute = rightOffset;
		this.bottomAnchor.absolute = bottomOffset;
		this.topAnchor.absolute = topOffset;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A8E RID: 10894 RVA: 0x00137A54 File Offset: 0x00135E54
	public void SetScreenRect(int left, int top, int width, int height)
	{
		this.SetAnchor(0f, left, 1f, -top - height, 0f, left + width, 1f, -top);
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x00137A88 File Offset: 0x00135E88
	public void ResetAnchors()
	{
		this.mAnchorsCached = true;
		this.leftAnchor.rect = ((!this.leftAnchor.target) ? null : this.leftAnchor.target.GetComponent<UIRect>());
		this.bottomAnchor.rect = ((!this.bottomAnchor.target) ? null : this.bottomAnchor.target.GetComponent<UIRect>());
		this.rightAnchor.rect = ((!this.rightAnchor.target) ? null : this.rightAnchor.target.GetComponent<UIRect>());
		this.topAnchor.rect = ((!this.topAnchor.target) ? null : this.topAnchor.target.GetComponent<UIRect>());
		this.mCam = NGUITools.FindCameraForLayer(this.cachedGameObject.layer);
		this.FindCameraFor(this.leftAnchor);
		this.FindCameraFor(this.bottomAnchor);
		this.FindCameraFor(this.rightAnchor);
		this.FindCameraFor(this.topAnchor);
		this.mUpdateAnchors = true;
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x00137BC1 File Offset: 0x00135FC1
	public void ResetAndUpdateAnchors()
	{
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06002A91 RID: 10897
	public abstract void SetRect(float x, float y, float width, float height);

	// Token: 0x06002A92 RID: 10898 RVA: 0x00137BD0 File Offset: 0x00135FD0
	private void FindCameraFor(UIRect.AnchorPoint ap)
	{
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
		}
		else
		{
			ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
		}
	}

	// Token: 0x06002A93 RID: 10899 RVA: 0x00137C28 File Offset: 0x00136028
	public virtual void ParentHasChanged()
	{
		this.mParentFound = false;
		UIRect y = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
		if (this.mParent != y)
		{
			if (this.mParent)
			{
				this.mParent.mChildren.Remove(this);
			}
			this.mParent = y;
			if (this.mParent)
			{
				this.mParent.mChildren.Add(this);
			}
			this.mRootSet = false;
		}
	}

	// Token: 0x06002A94 RID: 10900
	protected abstract void OnStart();

	// Token: 0x06002A95 RID: 10901 RVA: 0x00137CAF File Offset: 0x001360AF
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x04002A5C RID: 10844
	public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

	// Token: 0x04002A5D RID: 10845
	public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x04002A5E RID: 10846
	public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

	// Token: 0x04002A5F RID: 10847
	public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x04002A60 RID: 10848
	public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnUpdate;

	// Token: 0x04002A61 RID: 10849
	[NonSerialized]
	protected GameObject mGo;

	// Token: 0x04002A62 RID: 10850
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x04002A63 RID: 10851
	[NonSerialized]
	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	// Token: 0x04002A64 RID: 10852
	[NonSerialized]
	protected bool mChanged = true;

	// Token: 0x04002A65 RID: 10853
	[NonSerialized]
	protected bool mParentFound;

	// Token: 0x04002A66 RID: 10854
	[NonSerialized]
	private bool mUpdateAnchors = true;

	// Token: 0x04002A67 RID: 10855
	[NonSerialized]
	private int mUpdateFrame = -1;

	// Token: 0x04002A68 RID: 10856
	[NonSerialized]
	private bool mAnchorsCached;

	// Token: 0x04002A69 RID: 10857
	[NonSerialized]
	private UIRoot mRoot;

	// Token: 0x04002A6A RID: 10858
	[NonSerialized]
	private UIRect mParent;

	// Token: 0x04002A6B RID: 10859
	[NonSerialized]
	private bool mRootSet;

	// Token: 0x04002A6C RID: 10860
	[NonSerialized]
	protected Camera mCam;

	// Token: 0x04002A6D RID: 10861
	protected bool mStarted;

	// Token: 0x04002A6E RID: 10862
	[NonSerialized]
	public float finalAlpha = 1f;

	// Token: 0x04002A6F RID: 10863
	protected static Vector3[] mSides = new Vector3[4];

	// Token: 0x020005D8 RID: 1496
	[Serializable]
	public class AnchorPoint
	{
		// Token: 0x06002A97 RID: 10903 RVA: 0x00137CBE File Offset: 0x001360BE
		public AnchorPoint()
		{
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00137CC6 File Offset: 0x001360C6
		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00137CD5 File Offset: 0x001360D5
		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00137CF0 File Offset: 0x001360F0
		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00137D12 File Offset: 0x00136112
		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			this.SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00137D2C File Offset: 0x0013612C
		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				this.Set(rel0, abs0);
			}
			else if (num2 < num && num2 < num3)
			{
				this.Set(rel1, abs1);
			}
			else
			{
				this.Set(rel2, abs2);
			}
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00137D94 File Offset: 0x00136194
		public void SetHorizontal(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 position = this.target.position;
				if (parent != null)
				{
					position = parent.InverseTransformPoint(position);
				}
				this.absolute = Mathf.FloorToInt(localPos - position.x + 0.5f);
			}
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x00137E38 File Offset: 0x00136238
		public void SetVertical(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 position = this.target.position;
				if (parent != null)
				{
					position = parent.InverseTransformPoint(position);
				}
				this.absolute = Mathf.FloorToInt(localPos - position.y + 0.5f);
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x00137EDC File Offset: 0x001362DC
		public Vector3[] GetSides(Transform relativeTo)
		{
			if (this.target != null)
			{
				if (this.rect != null)
				{
					return this.rect.GetSides(relativeTo);
				}
				Camera component = this.target.GetComponent<Camera>();
				if (component != null)
				{
					return component.GetSides(relativeTo);
				}
			}
			return null;
		}

		// Token: 0x04002A70 RID: 10864
		public Transform target;

		// Token: 0x04002A71 RID: 10865
		public float relative;

		// Token: 0x04002A72 RID: 10866
		public int absolute;

		// Token: 0x04002A73 RID: 10867
		[NonSerialized]
		public UIRect rect;

		// Token: 0x04002A74 RID: 10868
		[NonSerialized]
		public Camera targetCam;
	}

	// Token: 0x020005D9 RID: 1497
	[DoNotObfuscateNGUI]
	public enum AnchorUpdate
	{
		// Token: 0x04002A76 RID: 10870
		OnEnable,
		// Token: 0x04002A77 RID: 10871
		OnUpdate,
		// Token: 0x04002A78 RID: 10872
		OnStart
	}
}
