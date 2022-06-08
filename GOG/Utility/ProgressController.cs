using System;

namespace GOG.Utility
{
	// Token: 0x020001D5 RID: 469
	public class ProgressController
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x0006D12A File Offset: 0x0006B52A
		public ProgressController(string title, string description)
		{
			this.mTitle = title;
			this.mDescription = description;
			this.mProgress = 0;
			this.mEnabled = true;
			this.mResult = string.Empty;
			this.mStatus = ProgressStatus.Progressing;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0006D160 File Offset: 0x0006B560
		public ProgressStatus ResultStatus
		{
			get
			{
				return this.mStatus;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0006D168 File Offset: 0x0006B568
		public int Progress
		{
			get
			{
				return this.mProgress;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0006D170 File Offset: 0x0006B570
		public string Title
		{
			get
			{
				return this.mTitle;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0006D178 File Offset: 0x0006B578
		public string Description
		{
			get
			{
				return this.mDescription;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0006D180 File Offset: 0x0006B580
		public bool Enabled
		{
			get
			{
				return this.mEnabled;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0006D188 File Offset: 0x0006B588
		public string Result
		{
			get
			{
				return this.mResult;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0006D190 File Offset: 0x0006B590
		public void Update(string title, string description, int progress, string result)
		{
			this.mTitle = title;
			this.mDescription = description;
			this.mProgress = progress;
			this.mResult = result;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0006D1AF File Offset: 0x0006B5AF
		public void Update(int progress)
		{
			this.mProgress = progress;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0006D1B8 File Offset: 0x0006B5B8
		public void EndProgress(string result)
		{
			this.mResult = result;
			this.mEnabled = false;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0006D1C8 File Offset: 0x0006B5C8
		public void EndProgress(string result, ProgressStatus status)
		{
			this.mResult = result;
			this.mEnabled = false;
			this.mStatus = status;
		}

		// Token: 0x04000D2B RID: 3371
		private int mProgress;

		// Token: 0x04000D2C RID: 3372
		private string mTitle;

		// Token: 0x04000D2D RID: 3373
		private string mDescription;

		// Token: 0x04000D2E RID: 3374
		private bool mEnabled;

		// Token: 0x04000D2F RID: 3375
		private string mResult;

		// Token: 0x04000D30 RID: 3376
		private ProgressStatus mStatus;
	}
}
