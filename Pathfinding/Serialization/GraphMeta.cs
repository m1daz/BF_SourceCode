using System;

namespace Pathfinding.Serialization
{
	// Token: 0x02000055 RID: 85
	internal class GraphMeta
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00015640 File Offset: 0x00013A40
		public Type GetGraphType(int i)
		{
			Type type = Type.GetType(this.typeNames[i]);
			if (type != null)
			{
				return type;
			}
			throw new Exception("No graph of type '" + this.typeNames[i] + "' could be created, type does not exist");
		}

		// Token: 0x04000233 RID: 563
		public Version version;

		// Token: 0x04000234 RID: 564
		public int graphs;

		// Token: 0x04000235 RID: 565
		public string[] guids;

		// Token: 0x04000236 RID: 566
		public string[] typeNames;

		// Token: 0x04000237 RID: 567
		public int[] nodeCounts;
	}
}
