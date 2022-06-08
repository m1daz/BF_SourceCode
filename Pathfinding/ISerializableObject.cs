using System;

namespace Pathfinding
{
	// Token: 0x02000048 RID: 72
	public interface ISerializableObject
	{
		// Token: 0x060002A4 RID: 676
		void SerializeSettings(AstarSerializer serializer);

		// Token: 0x060002A5 RID: 677
		void DeSerializeSettings(AstarSerializer serializer);
	}
}
