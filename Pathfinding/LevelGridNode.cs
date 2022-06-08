using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000070 RID: 112
	public class LevelGridNode : Node
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x000203D9 File Offset: 0x0001E7D9
		public void ResetAllGridConnections()
		{
			this.gridConnections = uint.MaxValue;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000203E2 File Offset: 0x0001E7E2
		public bool HasAnyGridConnections()
		{
			return this.gridConnections != uint.MaxValue;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000203F0 File Offset: 0x0001E7F0
		public bool GetConnection(int i)
		{
			return (this.gridConnections >> i * 8 & 255U) != 255U;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0002040F File Offset: 0x0001E80F
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = ((this.gridConnections & ~(255U << dir * 8)) | (uint)((uint)value << dir * 8));
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00020434 File Offset: 0x0001E834
		public int GetConnectionValue(int dir)
		{
			return (int)(this.gridConnections >> dir * 8 & 255U);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00020449 File Offset: 0x0001E849
		public int GetGridIndex()
		{
			return this.indices >> 24;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00020454 File Offset: 0x0001E854
		public int GetIndex()
		{
			return this.indices & 16777215;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00020462 File Offset: 0x0001E862
		public void SetIndex(int i)
		{
			this.indices &= -16777216;
			this.indices |= i;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00020484 File Offset: 0x0001E884
		public void SetGridIndex(int gridIndex)
		{
			this.indices &= 16777215;
			this.indices |= gridIndex << 24;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000204AC File Offset: 0x0001E8AC
		public override void UpdateAllG(NodeRun nodeR, NodeRunData nodeRunData)
		{
			base.BaseUpdateAllG(nodeR, nodeRunData);
			int index = this.GetIndex();
			LayerGridGraph layerGridGraph = LevelGridNode.gridGraphs[this.indices >> 24];
			int[] neighbourOffsets = layerGridGraph.neighbourOffsets;
			Node[] nodes = layerGridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					Node node = nodes[index + neighbourOffsets[i] + layerGridGraph.width * layerGridGraph.depth * connectionValue];
					NodeRun nodeRun = node.GetNodeRun(nodeRunData);
					if (nodeRun.parent == nodeR && nodeRun.pathID == nodeRunData.pathID)
					{
						node.UpdateAllG(nodeRun, nodeRunData);
					}
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00020560 File Offset: 0x0001E960
		public override void FloodFill(Stack<Node> stack, int area)
		{
			base.FloodFill(stack, area);
			int index = this.GetIndex();
			LayerGridGraph layerGridGraph = LevelGridNode.gridGraphs[this.indices >> 24];
			int[] neighbourOffsets = layerGridGraph.neighbourOffsets;
			Node[] nodes = layerGridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					Node node = nodes[index + neighbourOffsets[i] + layerGridGraph.width * layerGridGraph.depth * connectionValue];
					if (node.walkable && node.area != area)
					{
						stack.Push(node);
						node.area = area;
					}
				}
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0002060C File Offset: 0x0001EA0C
		public override void Open(NodeRunData nodeRunData, NodeRun nodeR, Int3 targetPosition, Path path)
		{
			base.BaseOpen(nodeRunData, nodeR, targetPosition, path);
			LayerGridGraph layerGridGraph = LevelGridNode.gridGraphs[this.indices >> 24];
			int[] neighbourOffsets = layerGridGraph.neighbourOffsets;
			int[] neighbourCosts = layerGridGraph.neighbourCosts;
			Node[] nodes = layerGridGraph.nodes;
			int index = this.GetIndex();
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					Node node = nodes[index + neighbourOffsets[i] + layerGridGraph.width * layerGridGraph.depth * connectionValue];
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
								nodeRunData.open.Add(nodeRun);
							}
							else if (nodeRun.g + num + base.penalty + path.GetTagPenalty(base.tags) < nodeR.g)
							{
								if (node.ContainsConnection(this))
								{
									nodeR.parent = nodeRun;
									nodeR.cost = num;
									this.UpdateAllG(nodeR, nodeRunData);
									nodeRunData.open.Add(nodeR);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000207E0 File Offset: 0x0001EBE0
		public static void RemoveGridGraph(LayerGridGraph graph)
		{
			if (LevelGridNode.gridGraphs == null)
			{
				return;
			}
			int i = 0;
			while (i < LevelGridNode.gridGraphs.Length)
			{
				if (LevelGridNode.gridGraphs[i] == graph)
				{
					if (LevelGridNode.gridGraphs.Length == 1)
					{
						LevelGridNode.gridGraphs = null;
						return;
					}
					for (int j = i + 1; j < LevelGridNode.gridGraphs.Length; j++)
					{
						LayerGridGraph layerGridGraph = LevelGridNode.gridGraphs[j];
						if (layerGridGraph.nodes != null)
						{
							for (int k = 0; k < layerGridGraph.nodes.Length; k++)
							{
								((LevelGridNode)layerGridGraph.nodes[k]).SetGridIndex(j - 1);
							}
						}
					}
					LayerGridGraph[] array = new LayerGridGraph[LevelGridNode.gridGraphs.Length - 1];
					for (int l = 0; l < i; l++)
					{
						array[l] = LevelGridNode.gridGraphs[l];
					}
					for (int m = i + 1; m < LevelGridNode.gridGraphs.Length; m++)
					{
						array[m - 1] = LevelGridNode.gridGraphs[m];
					}
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000208F0 File Offset: 0x0001ECF0
		public static int SetGridGraph(LayerGridGraph graph)
		{
			if (LevelGridNode.gridGraphs == null)
			{
				LevelGridNode.gridGraphs = new LayerGridGraph[1];
			}
			else
			{
				for (int i = 0; i < LevelGridNode.gridGraphs.Length; i++)
				{
					if (LevelGridNode.gridGraphs[i] == graph)
					{
						return i;
					}
				}
				if (LevelGridNode.gridGraphs.Length + 1 >= 256)
				{
					Debug.LogError("Too many grid graphs have been created, 256 is the maximum allowed number. If you get this error, please inform me (arongranberg.com) as I will have to code up some cleanup function for this if it turns out to be called often");
					return 0;
				}
				LayerGridGraph[] array = new LayerGridGraph[LevelGridNode.gridGraphs.Length + 1];
				for (int j = 0; j < LevelGridNode.gridGraphs.Length; j++)
				{
					array[j] = LevelGridNode.gridGraphs[j];
				}
				LevelGridNode.gridGraphs = array;
			}
			LevelGridNode.gridGraphs[LevelGridNode.gridGraphs.Length - 1] = graph;
			return LevelGridNode.gridGraphs.Length - 1;
		}

		// Token: 0x04000306 RID: 774
		protected int indices;

		// Token: 0x04000307 RID: 775
		protected uint gridConnections;

		// Token: 0x04000308 RID: 776
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x04000309 RID: 777
		public const int NoConnection = 255;

		// Token: 0x0400030A RID: 778
		public const int ConnectionMask = 255;

		// Token: 0x0400030B RID: 779
		private const int ConnectionStride = 8;

		// Token: 0x0400030C RID: 780
		public const int MaxLayerCount = 255;
	}
}
