using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000634 RID: 1588
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06002E0D RID: 11789 RVA: 0x0015126F File Offset: 0x0014F66F
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06002E0E RID: 11790 RVA: 0x0015127C File Offset: 0x0014F67C
	// (set) Token: 0x06002E0F RID: 11791 RVA: 0x00151284 File Offset: 0x0014F684
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06002E10 RID: 11792 RVA: 0x0015128D File Offset: 0x0014F68D
	// (set) Token: 0x06002E11 RID: 11793 RVA: 0x00151295 File Offset: 0x0014F695
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06002E12 RID: 11794 RVA: 0x001512B5 File Offset: 0x0014F6B5
	// (set) Token: 0x06002E13 RID: 11795 RVA: 0x001512BD File Offset: 0x0014F6BD
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06002E14 RID: 11796 RVA: 0x001512C6 File Offset: 0x0014F6C6
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x001512CE File Offset: 0x0014F6CE
	protected virtual void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x001512D8 File Offset: 0x0014F6D8
	protected virtual void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && this.mFPS > 0)
		{
			this.mDelta += Mathf.Min(1f, RealTime.deltaTime);
			float num = 1f / (float)this.mFPS;
			while (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.frameIndex >= this.mSpriteNames.Count)
				{
					this.frameIndex = 0;
					this.mActive = this.mLoop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.frameIndex];
					if (this.mSnap)
					{
						this.mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x001513E8 File Offset: 0x0014F7E8
	public void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UISpriteData> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UISpriteData uispriteData = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || uispriteData.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(uispriteData.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x001514B8 File Offset: 0x0014F8B8
	public void Play()
	{
		this.mActive = true;
	}

	// Token: 0x06002E19 RID: 11801 RVA: 0x001514C1 File Offset: 0x0014F8C1
	public void Pause()
	{
		this.mActive = false;
	}

	// Token: 0x06002E1A RID: 11802 RVA: 0x001514CC File Offset: 0x0014F8CC
	public void ResetToBeginning()
	{
		this.mActive = true;
		this.frameIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.frameIndex];
			if (this.mSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04002CF7 RID: 11511
	public int frameIndex;

	// Token: 0x04002CF8 RID: 11512
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	// Token: 0x04002CF9 RID: 11513
	[HideInInspector]
	[SerializeField]
	protected string mPrefix = string.Empty;

	// Token: 0x04002CFA RID: 11514
	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	// Token: 0x04002CFB RID: 11515
	[HideInInspector]
	[SerializeField]
	protected bool mSnap = true;

	// Token: 0x04002CFC RID: 11516
	protected UISprite mSprite;

	// Token: 0x04002CFD RID: 11517
	protected float mDelta;

	// Token: 0x04002CFE RID: 11518
	protected bool mActive = true;

	// Token: 0x04002CFF RID: 11519
	protected List<string> mSpriteNames = new List<string>();
}
