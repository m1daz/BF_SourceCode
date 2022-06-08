using System;
using System.Collections.Generic;

namespace Pathfinding.Nodes
{
	// Token: 0x02000074 RID: 116
	public class GridNode : Node
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00023300 File Offset: 0x00021700
		public bool HasAnyGridConnections()
		{
			return (this.flags & 255) != 0;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00023314 File Offset: 0x00021714
		public bool GetConnection(int i)
		{
			return (this.flags >> i & 1) == 1;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00023326 File Offset: 0x00021726
		public void SetConnection(int i, int value)
		{
			this.flags = ((this.flags & ~(1 << i)) | value << i);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00023343 File Offset: 0x00021743
		public void SetConnectionRaw(int i, int value)
		{
			this.flags |= value << i;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00023358 File Offset: 0x00021758
		public int GetGridIndex()
		{
			return this.indices >> 24;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00023363 File Offset: 0x00021763
		public int GetIndex()
		{
			return this.indices & 16777215;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00023371 File Offset: 0x00021771
		public void SetIndex(int i)
		{
			this.indices &= -16777216;
			this.indices |= i;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00023393 File Offset: 0x00021793
		public void SetGridIndex(int gridIndex)
		{
			this.indices &= 16777215;
			this.indices |= gridIndex << 24;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000233B8 File Offset: 0x000217B8
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x000233C0 File Offset: 0x000217C0
		public bool WalkableErosion
		{
			get
			{
				return base.Bit15;
			}
			set
			{
				base.Bit15 = value;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000233CC File Offset: 0x000217CC
		public void UpdateGridConnections()
		{
			GridGraph gridGraph = GridNode.gridGraphs[this.indices >> 24];
			int index = this.GetIndex();
			int num = index % gridGraph.width;
			int num2 = index / gridGraph.width;
			gridGraph.CalculateConnections(gridGraph.nodes, num, num2, this);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			int[] neighbourXOffsets = gridGraph.neighbourXOffsets;
			int[] neighbourZOffsets = gridGraph.neighbourZOffsets;
			for (int i = 0; i < 8; i++)
			{
				int num3 = num + neighbourXOffsets[i];
				int num4 = num2 + neighbourZOffsets[i];
				if (num3 >= 0 && num4 >= 0 && num3 < gridGraph.width && num4 < gridGraph.depth)
				{
					GridNode node = (GridNode)gridGraph.nodes[index + neighbourOffsets[i]];
					gridGraph.CalculateConnections(gridGraph.nodes, num3, num4, node);
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000234A8 File Offset: 0x000218A8
		public override bool RemoveConnection(Node node)
		{
			bool result = base.RemoveConnection(node);
			GridGraph gridGraph = GridNode.gridGraphs[this.indices >> 24];
			int num = this.indices & 16777215;
			int num2 = num % gridGraph.width;
			int num3 = num / gridGraph.width;
			gridGraph.CalculateConnections(gridGraph.nodes, num2, num3, this);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			int[] neighbourXOffsets = gridGraph.neighbourXOffsets;
			int[] neighbourZOffsets = gridGraph.neighbourZOffsets;
			for (int i = 0; i < 8; i++)
			{
				int num4 = num2 + neighbourXOffsets[i];
				int num5 = num3 + neighbourZOffsets[i];
				if (num4 >= 0 && num5 >= 0 && num4 < gridGraph.width && num5 < gridGraph.depth)
				{
					GridNode gridNode = (GridNode)gridGraph.nodes[num + neighbourOffsets[i]];
					if (gridNode == node)
					{
						this.SetConnection(i, 0);
						return true;
					}
				}
			}
			return result;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00023596 File Offset: 0x00021996
		public override void UpdateConnections()
		{
			base.UpdateConnections();
			this.UpdateGridConnections();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000235A4 File Offset: 0x000219A4
		public override void UpdateAllG(NodeRun nodeR, NodeRunData nodeRunData)
		{
			base.BaseUpdateAllG(nodeR, nodeRunData);
			int index = this.GetIndex();
			int[] neighbourOffsets = GridNode.gridGraphs[this.indices >> 24].neighbourOffsets;
			Node[] nodes = GridNode.gridGraphs[this.indices >> 24].nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.GetConnection(i))
				{
					Node node = nodes[index + neighbourOffsets[i]];
					NodeRun nodeRun = node.GetNodeRun(nodeRunData);
					if (nodeRun.parent == nodeR && nodeRun.pathID == nodeRunData.pathID)
					{
						node.UpdateAllG(nodeRun, nodeRunData);
					}
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00023644 File Offset: 0x00021A44
		public override void GetConnections(NodeDelegate callback)
		{
			base.GetConnectionsBase(callback);
			GridGraph gridGraph = GridNode.gridGraphs[this.indices >> 24];
			int index = this.GetIndex();
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			Node[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if ((this.flags >> i & 1) == 1)
				{
					Node node = nodes[index + neighbourOffsets[i]];
					callback(node);
				}
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000236BC File Offset: 0x00021ABC
		public override void FloodFill(Stack<Node> stack, int area)
		{
			base.FloodFill(stack, area);
			GridGraph gridGraph = GridNode.gridGraphs[this.indices >> 24];
			int num = this.indices & 16777215;
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			Node[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if ((this.flags >> i & 1) == 1)
				{
					Node node = nodes[num + neighbourOffsets[i]];
					if (node.walkable && node.area != area)
					{
						stack.Push(node);
						node.area = area;
					}
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00023759 File Offset: 0x00021B59
		public override int[] InitialOpen(BinaryHeapM open, Int3 targetPosition, Int3 position, Path path, bool doOpen)
		{
			if (doOpen)
			{
			}
			return base.InitialOpen(open, targetPosition, position, path, doOpen);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00023770 File Offset: 0x00021B70
		public override void Open(NodeRunData nodeRunData, NodeRun nodeR, Int3 targetPosition, Path path)
		{
			base.BaseOpen(nodeRunData, nodeR, targetPosition, path);
			GridGraph gridGraph = GridNode.gridGraphs[this.indices >> 24];
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			int[] neighbourCosts = gridGraph.neighbourCosts;
			Node[] nodes = gridGraph.nodes;
			int index = this.GetIndex();
			for (int i = 0; i < 8; i++)
			{
				if (this.GetConnection(i))
				{
					Node node = nodes[index + neighbourOffsets[i]];
					if (path.CanTraverse(node))
					{
						NodeRun nodeRun = node.GetNodeRun(nodeRunData);
						if (nodeRun.pathID != nodeRunData.pathID)
						{
							nodeRun.parent = nodeR;
							nodeRun.pathID = nodeRunData.pathID;
							nodeRun.cost = (uint)neighbourCosts[i];
							node.UpdateH(targetPosition, path.heuristic, path.heuristicScale, nodeRun);
							node.UpdateG(nodeRun, nodeRunData);
							nodeRunData.open.Add(nodeRun);
						}
						else
						{
							uint num = (uint)neighbourCosts[i];
							if (nodeR.g + num + node.penalty + path.GetTagPenalty(node.tags) < nodeRun.g)
							{
								nodeRun.cost = num;
								nodeRun.parent = nodeR;
								node.UpdateAllG(nodeRun, nodeRunData);
							}
							else if (nodeRun.g + num + base.penalty + path.GetTagPenalty(base.tags) < nodeR.g)
							{
								nodeR.parent = nodeRun;
								nodeR.cost = num;
								this.UpdateAllG(nodeR, nodeRunData);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000238FC File Offset: 0x00021CFC
		public static void RemoveGridGraph(GridGraph graph)
		{
			if (GridNode.gridGraphs == null)
			{
				return;
			}
			int i = 0;
			while (i < GridNode.gridGraphs.Length)
			{
				if (GridNode.gridGraphs[i] == graph)
				{
					if (GridNode.gridGraphs.Length == 1)
					{
						GridNode.gridGraphs = null;
						return;
					}
					for (int j = i + 1; j < GridNode.gridGraphs.Length; j++)
					{
						GridGraph gridGraph = GridNode.gridGraphs[j];
						if (gridGraph.nodes != null)
						{
							for (int k = 0; k < gridGraph.nodes.Length; k++)
							{
								if (gridGraph.nodes[k] != null)
								{
									((GridNode)gridGraph.nodes[k]).SetGridIndex(j - 1);
								}
							}
						}
					}
					GridGraph[] array = new GridGraph[GridNode.gridGraphs.Length - 1];
					for (int l = 0; l < i; l++)
					{
						array[l] = GridNode.gridGraphs[l];
					}
					for (int m = i + 1; m < GridNode.gridGraphs.Length; m++)
					{
						array[m - 1] = GridNode.gridGraphs[m];
					}
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00023A18 File Offset: 0x00021E18
		public static int SetGridGraph(GridGraph graph)
		{
			if (GridNode.gridGraphs == null)
			{
				GridNode.gridGraphs = new GridGraph[1];
			}
			else
			{
				for (int i = 0; i < GridNode.gridGraphs.Length; i++)
				{
					if (GridNode.gridGraphs[i] == graph)
					{
						return i;
					}
				}
				GridGraph[] array = new GridGraph[GridNode.gridGraphs.Length + 1];
				for (int j = 0; j < GridNode.gridGraphs.Length; j++)
				{
					array[j] = GridNode.gridGraphs[j];
				}
				GridNode.gridGraphs = array;
			}
			GridNode.gridGraphs[GridNode.gridGraphs.Length - 1] = graph;
			return GridNode.gridGraphs.Length - 1;
		}

		// Token: 0x04000319 RID: 793
		protected int indices;

		// Token: 0x0400031A RID: 794
		public static GridGraph[] gridGraphs;
	}
}
