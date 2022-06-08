using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200055A RID: 1370
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Interaction/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x170001ED RID: 493
	// (get) Token: 0x0600264E RID: 9806 RVA: 0x0011BF1D File Offset: 0x0011A31D
	public bool isActive
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x0011BF25 File Offset: 0x0011A325
	public void ResetToBeginning()
	{
		this.Finish();
		this.mReset = true;
		this.mActive = true;
		this.mNextChar = 0f;
		this.mCurrentOffset = 0;
		this.Update();
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x0011BF54 File Offset: 0x0011A354
	public void Finish()
	{
		if (this.mActive)
		{
			this.mActive = false;
			if (!this.mReset)
			{
				this.mCurrentOffset = this.mFullText.Length;
				this.mFade.Clear();
				this.mLabel.text = this.mFullText;
			}
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
		}
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x0011BFE9 File Offset: 0x0011A3E9
	private void OnEnable()
	{
		this.mReset = true;
		this.mActive = true;
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x0011BFF9 File Offset: 0x0011A3F9
	private void OnDisable()
	{
		this.Finish();
	}

	// Token: 0x06002653 RID: 9811 RVA: 0x0011C004 File Offset: 0x0011A404
	private void Update()
	{
		if (!this.mActive)
		{
			return;
		}
		if (this.mReset)
		{
			this.mCurrentOffset = 0;
			this.mReset = false;
			this.mLabel = base.GetComponent<UILabel>();
			this.mFullText = this.mLabel.processedText;
			this.mFade.Clear();
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
		}
		if (string.IsNullOrEmpty(this.mFullText))
		{
			return;
		}
		int length = this.mFullText.Length;
		while (this.mCurrentOffset < length && this.mNextChar <= RealTime.time)
		{
			int num = this.mCurrentOffset;
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			if (this.mLabel.supportEncoding)
			{
				while (NGUIText.ParseSymbol(this.mFullText, ref this.mCurrentOffset))
				{
				}
			}
			this.mCurrentOffset++;
			if (this.mCurrentOffset > length)
			{
				break;
			}
			float num2 = 1f / (float)this.charsPerSecond;
			char c = (num >= length) ? '\n' : this.mFullText[num];
			if (c == '\n')
			{
				num2 += this.delayOnNewLine;
			}
			else if (num + 1 == length || this.mFullText[num + 1] <= ' ')
			{
				if (c == '.')
				{
					if (num + 2 < length && this.mFullText[num + 1] == '.' && this.mFullText[num + 2] == '.')
					{
						num2 += this.delayOnPeriod * 3f;
						num += 2;
					}
					else
					{
						num2 += this.delayOnPeriod;
					}
				}
				else if (c == '!' || c == '?')
				{
					num2 += this.delayOnPeriod;
				}
			}
			if (this.mNextChar == 0f)
			{
				this.mNextChar = RealTime.time + num2;
			}
			else
			{
				this.mNextChar += num2;
			}
			if (this.fadeInTime != 0f)
			{
				TypewriterEffect.FadeEntry item = default(TypewriterEffect.FadeEntry);
				item.index = num;
				item.alpha = 0f;
				item.text = this.mFullText.Substring(num, this.mCurrentOffset - num);
				this.mFade.Add(item);
			}
			else
			{
				this.mLabel.text = ((!this.keepFullDimensions) ? this.mFullText.Substring(0, this.mCurrentOffset) : (this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset)));
				if (!this.keepFullDimensions && this.scrollView != null)
				{
					this.scrollView.UpdatePosition();
				}
			}
		}
		if (this.mCurrentOffset >= length && this.mFade.size == 0)
		{
			this.mLabel.text = this.mFullText;
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
			this.mActive = false;
		}
		else if (this.mFade.size != 0)
		{
			int i = 0;
			while (i < this.mFade.size)
			{
				TypewriterEffect.FadeEntry value = this.mFade[i];
				value.alpha += RealTime.deltaTime / this.fadeInTime;
				if (value.alpha < 1f)
				{
					this.mFade[i] = value;
					i++;
				}
				else
				{
					this.mFade.RemoveAt(i);
				}
			}
			if (this.mFade.size == 0)
			{
				if (this.keepFullDimensions)
				{
					this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset);
				}
				else
				{
					this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset);
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < this.mFade.size; j++)
				{
					TypewriterEffect.FadeEntry fadeEntry = this.mFade[j];
					if (j == 0)
					{
						stringBuilder.Append(this.mFullText.Substring(0, fadeEntry.index));
					}
					stringBuilder.Append('[');
					stringBuilder.Append(NGUIText.EncodeAlpha(fadeEntry.alpha));
					stringBuilder.Append(']');
					stringBuilder.Append(fadeEntry.text);
				}
				if (this.keepFullDimensions)
				{
					stringBuilder.Append("[00]");
					stringBuilder.Append(this.mFullText.Substring(this.mCurrentOffset));
				}
				this.mLabel.text = stringBuilder.ToString();
			}
		}
	}

	// Token: 0x04002706 RID: 9990
	public static TypewriterEffect current;

	// Token: 0x04002707 RID: 9991
	public int charsPerSecond = 20;

	// Token: 0x04002708 RID: 9992
	public float fadeInTime;

	// Token: 0x04002709 RID: 9993
	public float delayOnPeriod;

	// Token: 0x0400270A RID: 9994
	public float delayOnNewLine;

	// Token: 0x0400270B RID: 9995
	public UIScrollView scrollView;

	// Token: 0x0400270C RID: 9996
	public bool keepFullDimensions;

	// Token: 0x0400270D RID: 9997
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x0400270E RID: 9998
	private UILabel mLabel;

	// Token: 0x0400270F RID: 9999
	private string mFullText = string.Empty;

	// Token: 0x04002710 RID: 10000
	private int mCurrentOffset;

	// Token: 0x04002711 RID: 10001
	private float mNextChar;

	// Token: 0x04002712 RID: 10002
	private bool mReset = true;

	// Token: 0x04002713 RID: 10003
	private bool mActive;

	// Token: 0x04002714 RID: 10004
	private BetterList<TypewriterEffect.FadeEntry> mFade = new BetterList<TypewriterEffect.FadeEntry>();

	// Token: 0x0200055B RID: 1371
	private struct FadeEntry
	{
		// Token: 0x04002715 RID: 10005
		public int index;

		// Token: 0x04002716 RID: 10006
		public string text;

		// Token: 0x04002717 RID: 10007
		public float alpha;
	}
}
