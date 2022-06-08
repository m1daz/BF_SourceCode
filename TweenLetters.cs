using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005EB RID: 1515
public class TweenLetters : UITweener
{
	// Token: 0x06002B43 RID: 11075 RVA: 0x001400EC File Offset: 0x0013E4EC
	private void OnEnable()
	{
		this.mVertexCount = -1;
		UILabel uilabel = this.mLabel;
		uilabel.onPostFill = (UIWidget.OnPostFillCallback)Delegate.Combine(uilabel.onPostFill, new UIWidget.OnPostFillCallback(this.OnPostFill));
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x0014011C File Offset: 0x0013E51C
	private void OnDisable()
	{
		UILabel uilabel = this.mLabel;
		uilabel.onPostFill = (UIWidget.OnPostFillCallback)Delegate.Remove(uilabel.onPostFill, new UIWidget.OnPostFillCallback(this.OnPostFill));
	}

	// Token: 0x06002B45 RID: 11077 RVA: 0x00140145 File Offset: 0x0013E545
	private void Awake()
	{
		this.mLabel = base.GetComponent<UILabel>();
		this.mCurrent = this.hoverOver;
	}

	// Token: 0x06002B46 RID: 11078 RVA: 0x0014015F File Offset: 0x0013E55F
	public override void Play(bool forward)
	{
		this.mCurrent = ((!forward) ? this.hoverOut : this.hoverOver);
		base.Play(forward);
	}

	// Token: 0x06002B47 RID: 11079 RVA: 0x00140188 File Offset: 0x0013E588
	private void OnPostFill(UIWidget widget, int bufferOffset, List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		if (verts == null)
		{
			return;
		}
		int count = verts.Count;
		if (verts == null || count == 0)
		{
			return;
		}
		if (this.mLabel == null)
		{
			return;
		}
		try
		{
			int quadsPerCharacter = this.mLabel.quadsPerCharacter;
			int num = count / quadsPerCharacter / 4;
			string printedText = this.mLabel.printedText;
			if (this.mVertexCount != count)
			{
				this.mVertexCount = count;
				this.SetLetterOrder(num);
				this.GetLetterDuration(num);
			}
			Matrix4x4 identity = Matrix4x4.identity;
			Vector3 pos = Vector3.zero;
			Quaternion q = Quaternion.identity;
			Vector3 s = Vector3.one;
			Vector3 b = Vector3.zero;
			Quaternion a = Quaternion.Euler(this.mCurrent.rot);
			Vector3 vector = Vector3.zero;
			Color value = Color.clear;
			float num2 = base.tweenFactor * this.duration;
			for (int i = 0; i < quadsPerCharacter; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int num3 = this.mLetterOrder[j];
					int num4 = i * num * 4 + num3 * 4;
					if (num4 < count)
					{
						float start = this.mLetter[num3].start;
						float num5 = Mathf.Clamp(num2 - start, 0f, this.mLetter[num3].duration) / this.mLetter[num3].duration;
						num5 = this.animationCurve.Evaluate(num5);
						b = TweenLetters.GetCenter(verts, num4, 4);
						Vector2 offset = this.mLetter[num3].offset;
						pos = Vector3.LerpUnclamped(this.mCurrent.pos + new Vector3(offset.x, offset.y, 0f), Vector3.zero, num5);
						q = Quaternion.SlerpUnclamped(a, Quaternion.identity, num5);
						s = Vector3.LerpUnclamped(this.mCurrent.scale, Vector3.one, num5);
						float num6 = Mathf.LerpUnclamped(this.mCurrent.alpha, 1f, num5);
						identity.SetTRS(pos, q, s);
						for (int k = num4; k < num4 + 4; k++)
						{
							vector = verts[k];
							vector -= b;
							vector = identity.MultiplyPoint3x4(vector);
							vector += b;
							verts[k] = vector;
							value = cols[k];
							value.a *= num6;
							cols[k] = value;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06002B48 RID: 11080 RVA: 0x0014043C File Offset: 0x0013E83C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.mLabel.MarkAsChanged();
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x0014044C File Offset: 0x0013E84C
	private void SetLetterOrder(int letterCount)
	{
		if (letterCount == 0)
		{
			this.mLetter = null;
			this.mLetterOrder = null;
			return;
		}
		this.mLetterOrder = new int[letterCount];
		this.mLetter = new TweenLetters.LetterProperties[letterCount];
		for (int i = 0; i < letterCount; i++)
		{
			this.mLetterOrder[i] = ((this.mCurrent.animationOrder != TweenLetters.AnimationLetterOrder.Reverse) ? i : (letterCount - 1 - i));
			int num = this.mLetterOrder[i];
			this.mLetter[num] = new TweenLetters.LetterProperties();
			this.mLetter[num].offset = new Vector2(UnityEngine.Random.Range(-this.mCurrent.offsetRange.x, this.mCurrent.offsetRange.x), UnityEngine.Random.Range(-this.mCurrent.offsetRange.y, this.mCurrent.offsetRange.y));
		}
		if (this.mCurrent.animationOrder == TweenLetters.AnimationLetterOrder.Random)
		{
			System.Random random = new System.Random();
			int j = letterCount;
			while (j > 1)
			{
				int num2 = random.Next(--j + 1);
				int num3 = this.mLetterOrder[num2];
				this.mLetterOrder[num2] = this.mLetterOrder[j];
				this.mLetterOrder[j] = num3;
			}
		}
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x0014058C File Offset: 0x0013E98C
	private void GetLetterDuration(int letterCount)
	{
		if (this.mCurrent.randomDurations)
		{
			for (int i = 0; i < this.mLetter.Length; i++)
			{
				this.mLetter[i].start = UnityEngine.Random.Range(0f, this.mCurrent.randomness.x * this.duration);
				float num = UnityEngine.Random.Range(this.mCurrent.randomness.y * this.duration, this.duration);
				this.mLetter[i].duration = num - this.mLetter[i].start;
			}
		}
		else
		{
			float num2 = this.duration / (float)letterCount;
			float num3 = 1f - this.mCurrent.overlap;
			float num4 = num2 * (float)letterCount * num3;
			float duration = this.ScaleRange(num2, num4 + num2 * this.mCurrent.overlap, this.duration);
			float num5 = 0f;
			for (int j = 0; j < this.mLetter.Length; j++)
			{
				int num6 = this.mLetterOrder[j];
				this.mLetter[num6].start = num5;
				this.mLetter[num6].duration = duration;
				num5 += this.mLetter[num6].duration * num3;
			}
		}
	}

	// Token: 0x06002B4B RID: 11083 RVA: 0x001406DB File Offset: 0x0013EADB
	private float ScaleRange(float value, float baseMax, float limitMax)
	{
		return limitMax * value / baseMax;
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x001406E4 File Offset: 0x0013EAE4
	private static Vector3 GetCenter(List<Vector3> verts, int firstVert, int length)
	{
		Vector3 a = verts[firstVert];
		for (int i = firstVert + 1; i < firstVert + length; i++)
		{
			a += verts[i];
		}
		return a / (float)length;
	}

	// Token: 0x04002ADB RID: 10971
	public TweenLetters.AnimationProperties hoverOver;

	// Token: 0x04002ADC RID: 10972
	public TweenLetters.AnimationProperties hoverOut;

	// Token: 0x04002ADD RID: 10973
	private UILabel mLabel;

	// Token: 0x04002ADE RID: 10974
	private int mVertexCount = -1;

	// Token: 0x04002ADF RID: 10975
	private int[] mLetterOrder;

	// Token: 0x04002AE0 RID: 10976
	private TweenLetters.LetterProperties[] mLetter;

	// Token: 0x04002AE1 RID: 10977
	private TweenLetters.AnimationProperties mCurrent;

	// Token: 0x020005EC RID: 1516
	[DoNotObfuscateNGUI]
	public enum AnimationLetterOrder
	{
		// Token: 0x04002AE3 RID: 10979
		Forward,
		// Token: 0x04002AE4 RID: 10980
		Reverse,
		// Token: 0x04002AE5 RID: 10981
		Random
	}

	// Token: 0x020005ED RID: 1517
	private class LetterProperties
	{
		// Token: 0x04002AE6 RID: 10982
		public float start;

		// Token: 0x04002AE7 RID: 10983
		public float duration;

		// Token: 0x04002AE8 RID: 10984
		public Vector2 offset;
	}

	// Token: 0x020005EE RID: 1518
	[Serializable]
	public class AnimationProperties
	{
		// Token: 0x04002AE9 RID: 10985
		public TweenLetters.AnimationLetterOrder animationOrder = TweenLetters.AnimationLetterOrder.Random;

		// Token: 0x04002AEA RID: 10986
		[Range(0f, 1f)]
		public float overlap = 0.5f;

		// Token: 0x04002AEB RID: 10987
		public bool randomDurations;

		// Token: 0x04002AEC RID: 10988
		[MinMaxRange(0f, 1f)]
		public Vector2 randomness = new Vector2(0.25f, 0.75f);

		// Token: 0x04002AED RID: 10989
		public Vector2 offsetRange = Vector2.zero;

		// Token: 0x04002AEE RID: 10990
		public Vector3 pos = Vector3.zero;

		// Token: 0x04002AEF RID: 10991
		public Vector3 rot = Vector3.zero;

		// Token: 0x04002AF0 RID: 10992
		public Vector3 scale = Vector3.one;

		// Token: 0x04002AF1 RID: 10993
		public float alpha = 1f;
	}
}
