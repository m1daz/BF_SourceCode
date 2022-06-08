using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000638 RID: 1592
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06002E2A RID: 11818 RVA: 0x00152044 File Offset: 0x00150444
	protected BetterList<UITextList.Paragraph> paragraphs
	{
		get
		{
			if (this.mParagraphs == null && !UITextList.mHistory.TryGetValue(base.name, out this.mParagraphs))
			{
				this.mParagraphs = new BetterList<UITextList.Paragraph>();
				UITextList.mHistory.Add(base.name, this.mParagraphs);
			}
			return this.mParagraphs;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x06002E2B RID: 11819 RVA: 0x0015209E File Offset: 0x0015049E
	public int paragraphCount
	{
		get
		{
			return this.paragraphs.size;
		}
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x06002E2C RID: 11820 RVA: 0x001520AB File Offset: 0x001504AB
	public bool isValid
	{
		get
		{
			return this.textLabel != null && this.textLabel.ambigiousFont != null;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x06002E2D RID: 11821 RVA: 0x001520D2 File Offset: 0x001504D2
	// (set) Token: 0x06002E2E RID: 11822 RVA: 0x001520DC File Offset: 0x001504DC
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			value = Mathf.Clamp01(value);
			if (this.isValid && this.mScroll != value)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.value = value;
				}
				else
				{
					this.mScroll = value;
					this.UpdateVisibleText();
				}
			}
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06002E2F RID: 11823 RVA: 0x00152137 File Offset: 0x00150537
	protected float lineHeight
	{
		get
		{
			return (!(this.textLabel != null)) ? 20f : ((float)this.textLabel.fontSize + this.textLabel.effectiveSpacingY);
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06002E30 RID: 11824 RVA: 0x0015216C File Offset: 0x0015056C
	protected int scrollHeight
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			return Mathf.Max(0, this.mTotalLines - num);
		}
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x001521AD File Offset: 0x001505AD
	public void Clear()
	{
		this.paragraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x001521C0 File Offset: 0x001505C0
	private void Start()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.scrollBar != null)
		{
			EventDelegate.Add(this.scrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
		}
		this.textLabel.overflowMethod = UILabel.Overflow.ClampContent;
		if (this.style == UITextList.Style.Chat)
		{
			this.textLabel.pivot = UIWidget.Pivot.BottomLeft;
			this.scrollValue = 1f;
		}
		else
		{
			this.textLabel.pivot = UIWidget.Pivot.TopLeft;
			this.scrollValue = 0f;
		}
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x00152263 File Offset: 0x00150663
	private void Update()
	{
		if (this.isValid && (this.textLabel.width != this.mLastWidth || this.textLabel.height != this.mLastHeight))
		{
			this.Rebuild();
		}
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x001522A4 File Offset: 0x001506A4
	public void OnScroll(float val)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			val *= this.lineHeight;
			this.scrollValue = this.mScroll - val / (float)scrollHeight;
		}
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x001522DC File Offset: 0x001506DC
	public void OnDrag(Vector2 delta)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			float num = delta.y / this.lineHeight;
			this.scrollValue = this.mScroll + num / (float)scrollHeight;
		}
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x00152316 File Offset: 0x00150716
	private void OnScrollBar()
	{
		this.mScroll = UIProgressBar.current.value;
		this.UpdateVisibleText();
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x0015232E File Offset: 0x0015072E
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x00152338 File Offset: 0x00150738
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.paragraphs.size < this.paragraphHistory)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		this.Rebuild();
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x0015239C File Offset: 0x0015079C
	protected void Rebuild()
	{
		if (this.isValid)
		{
			this.mLastWidth = this.textLabel.width;
			this.mLastHeight = this.textLabel.height;
			this.textLabel.UpdateNGUIText();
			NGUIText.rectHeight = 1000000;
			NGUIText.regionHeight = 1000000;
			this.mTotalLines = 0;
			for (int i = 0; i < this.paragraphs.size; i++)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[i];
				string text;
				NGUIText.WrapText(paragraph.text, out text, false, true, false);
				paragraph.lines = text.Split(new char[]
				{
					'\n'
				});
				this.mTotalLines += paragraph.lines.Length;
			}
			this.mTotalLines = 0;
			int j = 0;
			int size = this.mParagraphs.size;
			while (j < size)
			{
				this.mTotalLines += this.mParagraphs.buffer[j].lines.Length;
				j++;
			}
			if (this.scrollBar != null)
			{
				UIScrollBar uiscrollBar = this.scrollBar as UIScrollBar;
				if (uiscrollBar != null)
				{
					uiscrollBar.barSize = ((this.mTotalLines != 0) ? (1f - (float)this.scrollHeight / (float)this.mTotalLines) : 1f);
				}
			}
			this.UpdateVisibleText();
		}
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x00152510 File Offset: 0x00150910
	protected void UpdateVisibleText()
	{
		if (this.isValid)
		{
			if (this.mTotalLines == 0)
			{
				this.textLabel.text = string.Empty;
				return;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			int num2 = Mathf.Max(0, this.mTotalLines - num);
			int num3 = Mathf.RoundToInt(this.mScroll * (float)num2);
			if (num3 < 0)
			{
				num3 = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num4 = 0;
			int size = this.paragraphs.size;
			while (num > 0 && num4 < size)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[num4];
				int num5 = 0;
				int num6 = paragraph.lines.Length;
				while (num > 0 && num5 < num6)
				{
					string value = paragraph.lines[num5];
					if (num3 > 0)
					{
						num3--;
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append("\n");
						}
						stringBuilder.Append(value);
						num--;
					}
					num5++;
				}
				num4++;
			}
			this.textLabel.text = stringBuilder.ToString();
		}
	}

	// Token: 0x04002D25 RID: 11557
	public UILabel textLabel;

	// Token: 0x04002D26 RID: 11558
	public UIProgressBar scrollBar;

	// Token: 0x04002D27 RID: 11559
	public UITextList.Style style;

	// Token: 0x04002D28 RID: 11560
	public int paragraphHistory = 100;

	// Token: 0x04002D29 RID: 11561
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x04002D2A RID: 11562
	protected float mScroll;

	// Token: 0x04002D2B RID: 11563
	protected int mTotalLines;

	// Token: 0x04002D2C RID: 11564
	protected int mLastWidth;

	// Token: 0x04002D2D RID: 11565
	protected int mLastHeight;

	// Token: 0x04002D2E RID: 11566
	private BetterList<UITextList.Paragraph> mParagraphs;

	// Token: 0x04002D2F RID: 11567
	private static Dictionary<string, BetterList<UITextList.Paragraph>> mHistory = new Dictionary<string, BetterList<UITextList.Paragraph>>();

	// Token: 0x02000639 RID: 1593
	[DoNotObfuscateNGUI]
	public enum Style
	{
		// Token: 0x04002D31 RID: 11569
		Text,
		// Token: 0x04002D32 RID: 11570
		Chat
	}

	// Token: 0x0200063A RID: 1594
	protected class Paragraph
	{
		// Token: 0x04002D33 RID: 11571
		public string text;

		// Token: 0x04002D34 RID: 11572
		public string[] lines;
	}
}
