using System;

namespace Pathfinding.Serialization
{
	// Token: 0x02000056 RID: 86
	public class SerializeSettings
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00015690 File Offset: 0x00013A90
		public static SerializeSettings Settings
		{
			get
			{
				return new SerializeSettings
				{
					nodes = false
				};
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000156AC File Offset: 0x00013AAC
		public static SerializeSettings All
		{
			get
			{
				return new SerializeSettings
				{
					nodes = true
				};
			}
		}

		// Token: 0x04000238 RID: 568
		public bool nodes = true;

		// Token: 0x04000239 RID: 569
		public bool prettyPrint;

		// Token: 0x0400023A RID: 570
		public bool editorSettings;
	}
}
