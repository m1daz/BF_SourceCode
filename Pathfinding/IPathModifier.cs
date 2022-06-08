using System;

namespace Pathfinding
{
	// Token: 0x0200009D RID: 157
	public interface IPathModifier
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060004F2 RID: 1266
		// (set) Token: 0x060004F3 RID: 1267
		int Priority { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060004F4 RID: 1268
		ModifierData input { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060004F5 RID: 1269
		ModifierData output { get; }

		// Token: 0x060004F6 RID: 1270
		void ApplyOriginal(Path p);

		// Token: 0x060004F7 RID: 1271
		void Apply(Path p, ModifierData source);

		// Token: 0x060004F8 RID: 1272
		void PreProcess(Path p);
	}
}
