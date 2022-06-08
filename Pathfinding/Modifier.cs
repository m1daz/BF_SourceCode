using System;

namespace Pathfinding
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public abstract class Modifier : IPathModifier
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060004FA RID: 1274
		public abstract ModifierData input { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060004FB RID: 1275
		public abstract ModifierData output { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000308D3 File Offset: 0x0002ECD3
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x000308DB File Offset: 0x0002ECDB
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000308E4 File Offset: 0x0002ECE4
		public void Awake(Seeker s)
		{
			this.seeker = s;
			if (s != null)
			{
				s.RegisterModifier(this);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00030900 File Offset: 0x0002ED00
		public void OnDestroy(Seeker s)
		{
			if (s != null)
			{
				s.DeregisterModifier(this);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00030915 File Offset: 0x0002ED15
		[Obsolete]
		public virtual void ApplyOriginal(Path p)
		{
		}

		// Token: 0x06000501 RID: 1281
		public abstract void Apply(Path p, ModifierData source);

		// Token: 0x06000502 RID: 1282 RVA: 0x00030917 File Offset: 0x0002ED17
		[Obsolete]
		public virtual void PreProcess(Path p)
		{
		}

		// Token: 0x04000421 RID: 1057
		public int priority;

		// Token: 0x04000422 RID: 1058
		[NonSerialized]
		public Seeker seeker;
	}
}
