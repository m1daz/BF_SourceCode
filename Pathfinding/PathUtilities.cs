using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x020000C3 RID: 195
	public static class PathUtilities
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00036BA0 File Offset: 0x00034FA0
		public static bool IsPathPossible(Node n1, Node n2)
		{
			return n1.walkable && n2.walkable && n1.area == n2.area;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00036BCC File Offset: 0x00034FCC
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

		// Token: 0x060005E3 RID: 1507 RVA: 0x00036C24 File Offset: 0x00035024
		public static List<Node> GetReachableNodes(Node seed, int tagMask = -1)
		{
			Stack<Node> stack = StackPool<Node>.Claim();
			List<Node> list = ListPool<Node>.Claim();
			HashSet<Node> map = new HashSet<Node>();
			NodeDelegate nodeDelegate;
			if (tagMask == -1)
			{
				nodeDelegate = delegate(Node node)
				{
					if (node.walkable && map.Add(node))
					{
						list.Add(node);
						stack.Push(node);
					}
				};
			}
			else
			{
				nodeDelegate = delegate(Node node)
				{
					if (node.walkable && (tagMask >> node.tags & 1) != 0 && map.Add(node))
					{
						list.Add(node);
						stack.Push(node);
					}
				};
			}
			nodeDelegate(seed);
			while (stack.Count > 0)
			{
				stack.Pop().GetConnections(nodeDelegate);
			}
			StackPool<Node>.Release(stack);
			return list;
		}
	}
}
