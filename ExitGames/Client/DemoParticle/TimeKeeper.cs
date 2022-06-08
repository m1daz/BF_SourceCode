using System;

namespace ExitGames.Client.DemoParticle
{
	// Token: 0x02000159 RID: 345
	public class TimeKeeper
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x0004A535 File Offset: 0x00048935
		public TimeKeeper(int interval)
		{
			this.IsEnabled = true;
			this.Interval = interval;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0004A556 File Offset: 0x00048956
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0004A55E File Offset: 0x0004895E
		public int Interval { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0004A567 File Offset: 0x00048967
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0004A56F File Offset: 0x0004896F
		public bool IsEnabled { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0004A578 File Offset: 0x00048978
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0004A5AA File Offset: 0x000489AA
		public bool ShouldExecute
		{
			get
			{
				return this.IsEnabled && (this.shouldExecute || Environment.TickCount - this.lastExecutionTime > this.Interval);
			}
			set
			{
				this.shouldExecute = value;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0004A5B3 File Offset: 0x000489B3
		public void Reset()
		{
			this.shouldExecute = false;
			this.lastExecutionTime = Environment.TickCount;
		}

		// Token: 0x040008C2 RID: 2242
		private int lastExecutionTime = Environment.TickCount;

		// Token: 0x040008C3 RID: 2243
		private bool shouldExecute;
	}
}
