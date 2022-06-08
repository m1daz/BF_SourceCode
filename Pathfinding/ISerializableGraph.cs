using System;

namespace Pathfinding
{
	// Token: 0x02000049 RID: 73
	public interface ISerializableGraph : ISerializableObject
	{
		// Token: 0x060002A6 RID: 678
		void SerializeNodes(Node[] nodes, AstarSerializer serializer);

		// Token: 0x060002A7 RID: 679
		void DeSerializeNodes(Node[] nodes, AstarSerializer serializer);

		// Token: 0x060002A8 RID: 680
		Node[] CreateNodes(int num);
	}
}
