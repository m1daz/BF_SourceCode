using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000551 RID: 1361
[AddComponentMenu("NGUI/Examples/Play Idle Animations")]
public class PlayIdleAnimations : MonoBehaviour
{
	// Token: 0x0600262E RID: 9774 RVA: 0x0011B618 File Offset: 0x00119A18
	private void Start()
	{
		this.mAnim = base.GetComponentInChildren<Animation>();
		if (this.mAnim == null)
		{
			Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has no Animation component");
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			IEnumerator enumerator = this.mAnim.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (animationState.clip.name == "idle")
					{
						animationState.layer = 0;
						this.mIdle = animationState.clip;
						this.mAnim.Play(this.mIdle.name);
					}
					else if (animationState.clip.name.StartsWith("idle"))
					{
						animationState.layer = 1;
						this.mBreaks.Add(animationState.clip);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			if (this.mBreaks.Count == 0)
			{
				UnityEngine.Object.Destroy(this);
			}
		}
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x0011B74C File Offset: 0x00119B4C
	private void Update()
	{
		if (this.mNextBreak < Time.time)
		{
			if (this.mBreaks.Count == 1)
			{
				AnimationClip animationClip = this.mBreaks[0];
				this.mNextBreak = Time.time + animationClip.length + UnityEngine.Random.Range(5f, 15f);
				this.mAnim.CrossFade(animationClip.name);
			}
			else
			{
				int num = UnityEngine.Random.Range(0, this.mBreaks.Count - 1);
				if (this.mLastIndex == num)
				{
					num++;
					if (num >= this.mBreaks.Count)
					{
						num = 0;
					}
				}
				this.mLastIndex = num;
				AnimationClip animationClip2 = this.mBreaks[num];
				this.mNextBreak = Time.time + animationClip2.length + UnityEngine.Random.Range(2f, 8f);
				this.mAnim.CrossFade(animationClip2.name);
			}
		}
	}

	// Token: 0x040026E9 RID: 9961
	private Animation mAnim;

	// Token: 0x040026EA RID: 9962
	private AnimationClip mIdle;

	// Token: 0x040026EB RID: 9963
	private List<AnimationClip> mBreaks = new List<AnimationClip>();

	// Token: 0x040026EC RID: 9964
	private float mNextBreak;

	// Token: 0x040026ED RID: 9965
	private int mLastIndex;
}
