using System;
using UnityEngine;

// Token: 0x020005FA RID: 1530
public class UI2DSpriteAnimation : MonoBehaviour
{
	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x00141CB4 File Offset: 0x001400B4
	public bool isPlaying
	{
		get
		{
			return base.enabled;
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x00141CBC File Offset: 0x001400BC
	// (set) Token: 0x06002BCA RID: 11210 RVA: 0x00141CC4 File Offset: 0x001400C4
	public int framesPerSecond
	{
		get
		{
			return this.framerate;
		}
		set
		{
			this.framerate = value;
		}
	}

	// Token: 0x06002BCB RID: 11211 RVA: 0x00141CD0 File Offset: 0x001400D0
	public void Play()
	{
		if (this.frames != null && this.frames.Length > 0)
		{
			if (!base.enabled && !this.loop)
			{
				int num = (this.framerate <= 0) ? (this.frameIndex - 1) : (this.frameIndex + 1);
				if (num < 0 || num >= this.frames.Length)
				{
					this.frameIndex = ((this.framerate >= 0) ? 0 : (this.frames.Length - 1));
				}
			}
			base.enabled = true;
			this.UpdateSprite();
		}
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x00141D72 File Offset: 0x00140172
	public void Pause()
	{
		base.enabled = false;
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x00141D7B File Offset: 0x0014017B
	public void ResetToBeginning()
	{
		this.frameIndex = ((this.framerate >= 0) ? 0 : (this.frames.Length - 1));
		this.UpdateSprite();
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x00141DA5 File Offset: 0x001401A5
	private void Start()
	{
		this.Play();
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x00141DB0 File Offset: 0x001401B0
	private void Update()
	{
		if (this.frames == null || this.frames.Length == 0)
		{
			base.enabled = false;
		}
		else if (this.framerate != 0)
		{
			float num = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
			if (this.mUpdate < num)
			{
				this.mUpdate = num;
				int num2 = (this.framerate <= 0) ? (this.frameIndex - 1) : (this.frameIndex + 1);
				if (!this.loop && (num2 < 0 || num2 >= this.frames.Length))
				{
					base.enabled = false;
					return;
				}
				this.frameIndex = NGUIMath.RepeatIndex(num2, this.frames.Length);
				this.UpdateSprite();
			}
		}
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x00141E80 File Offset: 0x00140280
	private void UpdateSprite()
	{
		if (this.mUnitySprite == null && this.mNguiSprite == null)
		{
			this.mUnitySprite = base.GetComponent<SpriteRenderer>();
			this.mNguiSprite = base.GetComponent<UI2DSprite>();
			if (this.mUnitySprite == null && this.mNguiSprite == null)
			{
				base.enabled = false;
				return;
			}
		}
		float num = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
		if (this.framerate != 0)
		{
			this.mUpdate = num + Mathf.Abs(1f / (float)this.framerate);
		}
		if (this.mUnitySprite != null)
		{
			this.mUnitySprite.sprite = this.frames[this.frameIndex];
		}
		else if (this.mNguiSprite != null)
		{
			this.mNguiSprite.nextSprite = this.frames[this.frameIndex];
		}
	}

	// Token: 0x04002B38 RID: 11064
	public int frameIndex;

	// Token: 0x04002B39 RID: 11065
	[SerializeField]
	protected int framerate = 20;

	// Token: 0x04002B3A RID: 11066
	public bool ignoreTimeScale = true;

	// Token: 0x04002B3B RID: 11067
	public bool loop = true;

	// Token: 0x04002B3C RID: 11068
	public Sprite[] frames;

	// Token: 0x04002B3D RID: 11069
	private SpriteRenderer mUnitySprite;

	// Token: 0x04002B3E RID: 11070
	private UI2DSprite mNguiSprite;

	// Token: 0x04002B3F RID: 11071
	private float mUpdate;
}
