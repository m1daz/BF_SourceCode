using System;

namespace Pathfinding
{
	// Token: 0x02000047 RID: 71
	public interface ISerializableGraphEditor
	{
		// Token: 0x060002A2 RID: 674
		void SerializeSettings(NavGraph target, AstarSerializer serializer);

		// Token: 0x060002A3 RID: 675
		void DeSerializeSettings(NavGraph target, AstarSerializer serializer);
	}
}
