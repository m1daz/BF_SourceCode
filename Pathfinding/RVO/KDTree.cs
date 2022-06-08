using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200003C RID: 60
	public class KDTree
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00010A38 File Offset: 0x0000EE38
		public KDTree(Simulator simulator)
		{
			this.simulator = simulator;
			this.agentTree = new KDTree.AgentTreeNode[0];
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00010A53 File Offset: 0x0000EE53
		public void RebuildAgents()
		{
			this.rebuildAgents = true;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00010A5C File Offset: 0x0000EE5C
		public void BuildAgentTree()
		{
			List<Agent> list = this.simulator.GetAgents();
			if (this.agents == null || this.agents.Count != list.Count || this.rebuildAgents)
			{
				this.rebuildAgents = false;
				if (this.agents == null)
				{
					this.agents = new List<Agent>(list.Count);
				}
				else
				{
					this.agents.Clear();
				}
				this.agents.AddRange(this.simulator.GetAgents());
			}
			if (this.agentTree.Length != this.agents.Count * 2)
			{
				this.agentTree = new KDTree.AgentTreeNode[this.agents.Count * 2];
				for (int i = 0; i < this.agentTree.Length; i++)
				{
					this.agentTree[i] = default(KDTree.AgentTreeNode);
				}
			}
			if (this.agents.Count != 0)
			{
				this.BuildAgentTreeRecursive(0, this.agents.Count, 0);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00010B74 File Offset: 0x0000EF74
		private void BuildAgentTreeRecursive(int start, int end, int node)
		{
			KDTree.AgentTreeNode agentTreeNode = this.agentTree[node];
			agentTreeNode.start = start;
			agentTreeNode.end = end;
			agentTreeNode.xmin = (agentTreeNode.xmax = this.agents[start].position.x);
			agentTreeNode.ymin = (agentTreeNode.ymax = this.agents[start].position.z);
			for (int i = start + 1; i < end; i++)
			{
				agentTreeNode.xmin = Math.Min(agentTreeNode.xmin, this.agents[i].position.x);
				agentTreeNode.xmax = Math.Max(agentTreeNode.xmax, this.agents[i].position.x);
				agentTreeNode.ymin = Math.Min(agentTreeNode.ymin, this.agents[i].position.z);
				agentTreeNode.ymax = Math.Max(agentTreeNode.ymax, this.agents[i].position.z);
			}
			if (end - start > 10)
			{
				bool flag = agentTreeNode.xmax - agentTreeNode.xmin > agentTreeNode.ymax - agentTreeNode.ymin;
				float num = (!flag) ? (0.5f * (agentTreeNode.ymax + agentTreeNode.ymin)) : (0.5f * (agentTreeNode.xmax + agentTreeNode.xmin));
				int j = start;
				int num2 = end;
				while (j < num2)
				{
					while (j < num2 && ((!flag) ? this.agents[j].position.z : this.agents[j].position.x) < num)
					{
						j++;
					}
					while (num2 > j && ((!flag) ? this.agents[num2 - 1].position.z : this.agents[num2 - 1].position.x) >= num)
					{
						num2--;
					}
					if (j < num2)
					{
						Agent value = this.agents[j];
						this.agents[j] = this.agents[num2 - 1];
						this.agents[num2 - 1] = value;
						j++;
						num2--;
					}
				}
				int num3 = j - start;
				if (num3 == 0)
				{
					num3++;
					j++;
					num2++;
				}
				agentTreeNode.left = node + 1;
				agentTreeNode.right = node + 1 + (2 * num3 - 1);
				this.agentTree[node] = agentTreeNode;
				this.BuildAgentTreeRecursive(start, j, this.agentTree[node].left);
				this.BuildAgentTreeRecursive(j, end, this.agentTree[node].right);
			}
			else
			{
				this.agentTree[node] = agentTreeNode;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00010EB4 File Offset: 0x0000F2B4
		public void BuildObstacleTree()
		{
			List<ObstacleVertex> list = ListPool<ObstacleVertex>.Claim();
			List<ObstacleVertex> obstacles = this.simulator.GetObstacles();
			for (int i = 0; i < obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = obstacles[i];
				do
				{
					list.Add(obstacleVertex);
					obstacleVertex = obstacleVertex.next;
				}
				while (obstacleVertex != obstacles[i]);
			}
			KDTree.RecycleOTN(this.obstacleRoot);
			this.obstacleRoot = this.BuildObstacleTreeRecursive(list);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00010F25 File Offset: 0x0000F325
		private int countDepth(KDTree.ObstacleTreeNode node)
		{
			if (node == null)
			{
				return 0;
			}
			return 1 + this.countDepth(node.left) + this.countDepth(node.right);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00010F4C File Offset: 0x0000F34C
		private KDTree.ObstacleTreeNode BuildObstacleTreeRecursive(List<ObstacleVertex> obstacles)
		{
			if (obstacles.Count == 0)
			{
				ListPool<ObstacleVertex>.Release(obstacles);
				return null;
			}
			KDTree.ObstacleTreeNode otn = KDTree.GetOTN();
			int num = 0;
			int num2 = obstacles.Count;
			int num3 = obstacles.Count;
			for (int i = 0; i < obstacles.Count; i++)
			{
				int num4 = 0;
				int num5 = 0;
				ObstacleVertex obstacleVertex = obstacles[i];
				ObstacleVertex next = obstacleVertex.next;
				for (int j = 0; j < obstacles.Count; j++)
				{
					if (i != j)
					{
						ObstacleVertex obstacleVertex2 = obstacles[j];
						ObstacleVertex next2 = obstacleVertex2.next;
						float num6 = Polygon.TriangleArea(obstacleVertex.position, next.position, obstacleVertex2.position);
						float num7 = Polygon.TriangleArea(obstacleVertex.position, next.position, next2.position);
						if (num6 >= -1E-45f && num7 >= -1E-45f)
						{
							num4++;
						}
						else if (num6 <= 1E-45f && num7 <= 1E-45f)
						{
							num5++;
						}
						else
						{
							num4++;
							num5++;
						}
						int num8 = Math.Max(num4, num5);
						int num9 = Math.Min(num4, num5);
						int num10 = Math.Max(num2, num3);
						int num11 = Math.Min(num2, num3);
						if (num8 >= num10 && (num10 < num8 || num9 >= num11))
						{
							break;
						}
					}
				}
				int num12 = Math.Max(num4, num5);
				int num13 = Math.Min(num4, num5);
				int num14 = Math.Max(num2, num3);
				int num15 = Math.Min(num2, num3);
				if (num12 < num14 || (num14 >= num12 && num13 < num15))
				{
					num2 = num4;
					num3 = num5;
					num = i;
				}
			}
			List<ObstacleVertex> list = ListPool<ObstacleVertex>.Claim(num2);
			List<ObstacleVertex> list2 = ListPool<ObstacleVertex>.Claim(num3);
			int index = num;
			ObstacleVertex obstacleVertex3 = obstacles[index];
			ObstacleVertex next3 = obstacleVertex3.next;
			for (int k = 0; k < obstacles.Count; k++)
			{
				if (num != k)
				{
					ObstacleVertex obstacleVertex4 = obstacles[k];
					ObstacleVertex next4 = obstacleVertex4.next;
					float num16 = Polygon.TriangleArea(obstacleVertex3.position, next3.position, obstacleVertex4.position);
					float num17 = Polygon.TriangleArea(obstacleVertex3.position, next3.position, next4.position);
					if (num16 >= -1E-45f && num17 >= -1E-45f)
					{
						list.Add(obstacleVertex4);
					}
					else if (num16 <= 1E-45f && num17 <= 1E-45f)
					{
						list2.Add(obstacleVertex4);
					}
					else
					{
						float d = Polygon.IntersectionFactor(obstacleVertex4.position, next4.position, obstacleVertex3.position, next3.position);
						Vector3 position = obstacleVertex4.position + (next4.position - obstacleVertex4.position) * d;
						ObstacleVertex obstacleVertex5 = new ObstacleVertex();
						obstacleVertex5.position = position;
						obstacleVertex4.next = obstacleVertex5;
						obstacleVertex5.prev = obstacleVertex4;
						obstacleVertex5.next = next4;
						next4.prev = obstacleVertex5;
						obstacleVertex5.dir = obstacleVertex4.dir;
						obstacleVertex5.split = true;
						obstacleVertex5.convex = true;
						if (num16 > 0f)
						{
							list.Add(obstacleVertex4);
							list2.Add(obstacleVertex5);
						}
						else
						{
							list2.Add(obstacleVertex4);
							list.Add(obstacleVertex5);
						}
					}
				}
			}
			ListPool<ObstacleVertex>.Release(obstacles);
			otn.obstacle = obstacleVertex3;
			otn.left = this.BuildObstacleTreeRecursive(list);
			otn.right = this.BuildObstacleTreeRecursive(list2);
			return otn;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000112F2 File Offset: 0x0000F6F2
		public void GetAgentNeighbours(Agent agent, float rangeSq)
		{
			this.QueryAgentTreeRecursive(agent, ref rangeSq, 0);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000112FE File Offset: 0x0000F6FE
		public void GetObstacleNeighbours(Agent agent, float rangeSq)
		{
			this.QueryObstacleTreeRecursive(agent, rangeSq, this.obstacleRoot);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0001130E File Offset: 0x0000F70E
		private float Sqr(float v)
		{
			return v * v;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00011314 File Offset: 0x0000F714
		private void QueryAgentTreeRecursive(Agent agent, ref float rangeSq, int node)
		{
			if (this.agentTree[node].end - this.agentTree[node].start <= 10)
			{
				for (int i = this.agentTree[node].start; i < this.agentTree[node].end; i++)
				{
					rangeSq = agent.InsertAgentNeighbour(this.agents[i], rangeSq);
				}
			}
			else
			{
				float num = this.Sqr(Math.Max(0f, this.agentTree[this.agentTree[node].left].xmin - agent.position.x)) + this.Sqr(Math.Max(0f, agent.position.x - this.agentTree[this.agentTree[node].left].xmax)) + this.Sqr(Math.Max(0f, this.agentTree[this.agentTree[node].left].ymin - agent.position.z)) + this.Sqr(Math.Max(0f, agent.position.z - this.agentTree[this.agentTree[node].left].ymax));
				float num2 = this.Sqr(Math.Max(0f, this.agentTree[this.agentTree[node].right].xmin - agent.position.x)) + this.Sqr(Math.Max(0f, agent.position.x - this.agentTree[this.agentTree[node].right].xmax)) + this.Sqr(Math.Max(0f, this.agentTree[this.agentTree[node].right].ymin - agent.position.z)) + this.Sqr(Math.Max(0f, agent.position.z - this.agentTree[this.agentTree[node].right].ymax));
				if (num < num2)
				{
					if (num < rangeSq)
					{
						this.QueryAgentTreeRecursive(agent, ref rangeSq, this.agentTree[node].left);
						if (num2 < rangeSq)
						{
							this.QueryAgentTreeRecursive(agent, ref rangeSq, this.agentTree[node].right);
						}
					}
				}
				else if (num2 < rangeSq)
				{
					this.QueryAgentTreeRecursive(agent, ref rangeSq, this.agentTree[node].right);
					if (num < rangeSq)
					{
						this.QueryAgentTreeRecursive(agent, ref rangeSq, this.agentTree[node].left);
					}
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00011618 File Offset: 0x0000FA18
		private void QueryObstacleTreeRecursive(Agent agent, float rangeSq, KDTree.ObstacleTreeNode node)
		{
			if (node == null)
			{
				return;
			}
			ObstacleVertex obstacle = node.obstacle;
			ObstacleVertex next = obstacle.next;
			float num = Polygon.TriangleArea(obstacle.position, next.position, agent.position);
			this.QueryObstacleTreeRecursive(agent, rangeSq, (num < 0f) ? node.right : node.left);
			Vector3 vector = obstacle.position - next.position;
			vector.y = 0f;
			float num2 = this.Sqr(num) / vector.sqrMagnitude;
			if (num2 < rangeSq)
			{
				if (num < 0f)
				{
					agent.InsertObstacleNeighbour(node.obstacle, rangeSq);
				}
				this.QueryObstacleTreeRecursive(agent, rangeSq, (num < 0f) ? node.left : node.right);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000116E9 File Offset: 0x0000FAE9
		private static KDTree.ObstacleTreeNode GetOTN()
		{
			if (KDTree.OTNPool.Count > 0)
			{
				return KDTree.OTNPool.Pop();
			}
			return new KDTree.ObstacleTreeNode();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001170B File Offset: 0x0000FB0B
		private static void RecycleOTN(KDTree.ObstacleTreeNode node)
		{
			if (node == null)
			{
				return;
			}
			KDTree.OTNPool.Push(node);
			node.obstacle = null;
			KDTree.RecycleOTN(node.left);
			KDTree.RecycleOTN(node.right);
		}

		// Token: 0x040001EE RID: 494
		private const int MAX_LEAF_SIZE = 10;

		// Token: 0x040001EF RID: 495
		private KDTree.ObstacleTreeNode obstacleRoot;

		// Token: 0x040001F0 RID: 496
		private KDTree.AgentTreeNode[] agentTree;

		// Token: 0x040001F1 RID: 497
		private List<Agent> agents;

		// Token: 0x040001F2 RID: 498
		private Simulator simulator;

		// Token: 0x040001F3 RID: 499
		private bool rebuildAgents;

		// Token: 0x040001F4 RID: 500
		private static Stack<KDTree.ObstacleTreeNode> OTNPool = new Stack<KDTree.ObstacleTreeNode>();

		// Token: 0x0200003D RID: 61
		private struct AgentTreeNode
		{
			// Token: 0x040001F5 RID: 501
			public int start;

			// Token: 0x040001F6 RID: 502
			public int end;

			// Token: 0x040001F7 RID: 503
			public int left;

			// Token: 0x040001F8 RID: 504
			public int right;

			// Token: 0x040001F9 RID: 505
			public float xmax;

			// Token: 0x040001FA RID: 506
			public float ymax;

			// Token: 0x040001FB RID: 507
			public float xmin;

			// Token: 0x040001FC RID: 508
			public float ymin;
		}

		// Token: 0x0200003E RID: 62
		private class ObstacleTreeNode
		{
			// Token: 0x040001FD RID: 509
			public KDTree.ObstacleTreeNode left;

			// Token: 0x040001FE RID: 510
			public KDTree.ObstacleTreeNode right;

			// Token: 0x040001FF RID: 511
			public ObstacleVertex obstacle;
		}
	}
}
