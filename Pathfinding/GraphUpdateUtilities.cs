using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x020000C1 RID: 193
	public static class GraphUpdateUtilities
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x00036970 File Offset: 0x00034D70
		[Obsolete("This function has been moved to Pathfinding.Util.PathUtilities. Please use the version in that class")]
		public static bool IsPathPossible(Node n1, Node n2)
		{
			return n1.walkable && n2.walkable && n1.area == n2.area;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0003699C File Offset: 0x00034D9C
		[Obsolete("This function has been moved to Pathfinding.Util.PathUtilities. Please use the version in that class")]
		public static bool IsPathPossible(List<Node> nodes)
		{
			int area = nodes[0].area;
			for (int i = 0; i < nodes.Count; i++)
			{
				if (!nodes[i].walkable || nodes[i].area != area)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000369F4 File Offset: 0x00034DF4
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, Node node1, Node node2, bool alwaysRevert = false)
		{
			List<Node> list = ListPool<Node>.Claim();
			list.Add(node1);
			list.Add(node2);
			bool result = GraphUpdateUtilities.UpdateGraphsNoBlock(guo, list, alwaysRevert);
			ListPool<Node>.Release(list);
			return result;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00036A28 File Offset: 0x00034E28
		public static bool UpdateGraphsNoBlock(GraphUpdateObject guo, List<Node> nodes, bool alwaysRevert = false)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (!nodes[i].walkable)
				{
					return false;
				}
			}
			guo.trackChangedNodes = true;
			bool worked = true;
			AstarPath.RegisterSafeUpdate(delegate
			{
				AstarPath.active.UpdateGraphs(guo);
				AstarPath.active.QueueGraphUpdates();
				AstarPath.ForceCallThreadSafeCallbacks();
				worked = (worked && PathUtilities.IsPathPossible(nodes));
				if (!worked || alwaysRevert)
				{
					guo.RevertFromBackup();
					AstarPath.active.FloodFill();
				}
			}, true);
			AstarPath.active.FlushThreadSafeCallbacks();
			guo.trackChangedNodes = false;
			return worked;
		}
	}
}
