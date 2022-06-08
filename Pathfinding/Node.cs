using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000034 RID: 52
	public class Node
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000C5C9 File Offset: 0x0000A9C9
		public int GetNodeIndex()
		{
			return this.nodeIndex;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C5D1 File Offset: 0x0000A9D1
		public NodeRun GetNodeRun(NodeRunData data)
		{
			return data.nodes[this.nodeIndex];
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C5E0 File Offset: 0x0000A9E0
		public void SetNodeIndex(int index)
		{
			this.nodeIndex = index;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000C5E9 File Offset: 0x0000A9E9
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000C5F1 File Offset: 0x0000A9F1
		public uint penalty
		{
			get
			{
				return this._penalty;
			}
			set
			{
				if (value > 1048575U)
				{
					Debug.LogWarning("Very high penalty applied. Are you sure negative values haven't underflowed?\nPenalty values this high could with long paths cause overflows and in some cases infinity loops because of that.\nPenalty value applied: " + value);
				}
				this._penalty = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000C61A File Offset: 0x0000AA1A
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000C628 File Offset: 0x0000AA28
		public int tags
		{
			get
			{
				return this.flags >> 9 & 31;
			}
			set
			{
				this.flags = ((this.flags & -15873) | value << 9);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000C641 File Offset: 0x0000AA41
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000C655 File Offset: 0x0000AA55
		public bool Bit8
		{
			get
			{
				return (this.flags & 256) != 0;
			}
			set
			{
				this.flags = ((this.flags & -257) | ((!value) ? 0 : 256));
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000C67B File Offset: 0x0000AA7B
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000C68E File Offset: 0x0000AA8E
		public bool Bit15
		{
			get
			{
				return (this.flags >> 15 & 1) != 0;
			}
			set
			{
				this.flags = ((this.flags & -32769) | ((!value) ? 0 : 1) << 15);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000C6B3 File Offset: 0x0000AAB3
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000C6C6 File Offset: 0x0000AAC6
		public bool Bit16
		{
			get
			{
				return (this.flags >> 16 & 1) != 0;
			}
			set
			{
				this.flags = ((this.flags & -65537) | ((!value) ? 0 : 1) << 16);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000C6EB File Offset: 0x0000AAEB
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000C700 File Offset: 0x0000AB00
		public bool walkable
		{
			get
			{
				return (this.flags & 8388608) == 8388608;
			}
			set
			{
				this.flags = ((this.flags & -8388609) | ((!value) ? 0 : 8388608));
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000C726 File Offset: 0x0000AB26
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000C737 File Offset: 0x0000AB37
		public int area
		{
			get
			{
				return this.flags >> 24 & 255;
			}
			set
			{
				this.flags = ((this.flags & 16777215) | value << 24);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000C750 File Offset: 0x0000AB50
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000C75E File Offset: 0x0000AB5E
		public int graphIndex
		{
			get
			{
				return this.flags >> 18 & 31;
			}
			set
			{
				this.flags = ((this.flags & -8126465) | (value & 31) << 18);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C77C File Offset: 0x0000AB7C
		public void UpdateH(Int3 targetPosition, Heuristic heuristic, float scale, NodeRun nodeR)
		{
			if (heuristic == Heuristic.None)
			{
				nodeR.h = 0U;
				return;
			}
			if (heuristic == Heuristic.Euclidean)
			{
				nodeR.h = (uint)Mathfx.RoundToInt((this.position - targetPosition).magnitude * scale);
			}
			else if (heuristic == Heuristic.Manhattan)
			{
				nodeR.h = (uint)Mathfx.RoundToInt((float)(Node.Abs(this.position.x - targetPosition.x) + Node.Abs(this.position.y - targetPosition.y) + Node.Abs(this.position.z - targetPosition.z)) * scale);
			}
			else
			{
				int num = Node.Abs(this.position.x - targetPosition.x);
				int num2 = Node.Abs(this.position.z - targetPosition.z);
				if (num > num2)
				{
					nodeR.h = (uint)((14 * num2 + 10 * (num - num2)) / 10);
				}
				else
				{
					nodeR.h = (uint)((14 * num + 10 * (num2 - num)) / 10);
				}
				nodeR.h = (uint)Mathfx.RoundToInt(nodeR.h * scale);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C8AA File Offset: 0x0000ACAA
		public static int Abs(int x)
		{
			return (x >= 0) ? x : (-x);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C8BB File Offset: 0x0000ACBB
		public void UpdateG(NodeRun nodeR, NodeRunData nodeRunData)
		{
			nodeR.g = nodeR.parent.g + nodeR.cost + this.penalty + nodeRunData.path.GetTagPenalty(this.tags);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C8F0 File Offset: 0x0000ACF0
		protected void BaseUpdateAllG(NodeRun nodeR, NodeRunData nodeRunData)
		{
			this.UpdateG(nodeR, nodeRunData);
			nodeRunData.open.Add(nodeR);
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				NodeRun nodeRun = this.connections[i].GetNodeRun(nodeRunData);
				if (nodeRun.parent == nodeR && nodeRun.pathID == nodeRunData.pathID)
				{
					this.connections[i].UpdateAllG(nodeRun, nodeRunData);
				}
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000C971 File Offset: 0x0000AD71
		public virtual void UpdateAllG(NodeRun nodeR, NodeRunData nodeRunData)
		{
			this.BaseUpdateAllG(nodeR, nodeRunData);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000C97B File Offset: 0x0000AD7B
		public virtual int[] InitialOpen(BinaryHeapM open, Int3 targetPosition, Int3 position, Path path, bool doOpen)
		{
			return this.BaseInitialOpen(open, targetPosition, position, path, doOpen);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000C98C File Offset: 0x0000AD8C
		public int[] BaseInitialOpen(BinaryHeapM open, Int3 targetPosition, Int3 position, Path path, bool doOpen)
		{
			if (this.connectionCosts == null)
			{
				return null;
			}
			int[] result = this.connectionCosts;
			this.connectionCosts = new int[this.connectionCosts.Length];
			for (int i = 0; i < this.connectionCosts.Length; i++)
			{
				this.connectionCosts[i] = (this.connections[i].position - position).costMagnitude;
			}
			if (!doOpen)
			{
				for (int j = 0; j < this.connectionCosts.Length; j++)
				{
					Node node = this.connections[j];
					if (node.connections != null)
					{
						for (int k = 0; k < node.connections.Length; k++)
						{
							if (node.connections[k] == this)
							{
								node.connectionCosts[k] = this.connectionCosts[j];
								break;
							}
						}
					}
				}
			}
			if (doOpen)
			{
				this.connectionCosts = result;
			}
			return result;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000CA86 File Offset: 0x0000AE86
		public virtual void ResetCosts(int[] costs)
		{
			this.BaseResetCosts(costs);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000CA90 File Offset: 0x0000AE90
		public void BaseResetCosts(int[] costs)
		{
			this.connectionCosts = costs;
			if (this.connectionCosts == null)
			{
				return;
			}
			for (int i = 0; i < this.connectionCosts.Length; i++)
			{
				Node node = this.connections[i];
				if (node.connections != null)
				{
					for (int j = 0; j < node.connections.Length; j++)
					{
						if (node.connections[j] == this)
						{
							node.connectionCosts[j] = this.connectionCosts[i];
							break;
						}
					}
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000CB19 File Offset: 0x0000AF19
		public virtual void Open(NodeRunData nodeRunData, NodeRun nodeR, Int3 targetPosition, Path path)
		{
			this.BaseOpen(nodeRunData, nodeR, targetPosition, path);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000CB28 File Offset: 0x0000AF28
		public void BaseOpen(NodeRunData nodeRunData, NodeRun nodeR, Int3 targetPosition, Path path)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				Node node = this.connections[i];
				if (path.CanTraverse(node))
				{
					NodeRun nodeRun = node.GetNodeRun(nodeRunData);
					if (nodeRun.pathID != nodeRunData.pathID)
					{
						nodeRun.parent = nodeR;
						nodeRun.pathID = nodeRunData.pathID;
						nodeRun.cost = (uint)this.connectionCosts[i];
						node.UpdateH(targetPosition, path.heuristic, path.heuristicScale, nodeRun);
						node.UpdateG(nodeRun, nodeRunData);
						nodeRunData.open.Add(nodeRun);
					}
					else
					{
						uint num = (uint)this.connectionCosts[i];
						if (nodeR.g + num + node.penalty + path.GetTagPenalty(node.tags) < nodeRun.g)
						{
							nodeRun.cost = num;
							nodeRun.parent = nodeR;
							node.UpdateAllG(nodeRun, nodeRunData);
							nodeRunData.open.Add(nodeRun);
						}
						else if (nodeRun.g + num + this.penalty + path.GetTagPenalty(this.tags) < nodeR.g)
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

		// Token: 0x060001B8 RID: 440 RVA: 0x0000CC95 File Offset: 0x0000B095
		public virtual void GetConnections(NodeDelegate callback)
		{
			this.GetConnectionsBase(callback);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000CCA0 File Offset: 0x0000B0A0
		public void GetConnectionsBase(NodeDelegate callback)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].walkable)
				{
					callback(this.connections[i]);
				}
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000CCF2 File Offset: 0x0000B0F2
		public virtual void FloodFill(Stack<Node> stack, int area)
		{
			this.BaseFloodFill(stack, area);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000CCFC File Offset: 0x0000B0FC
		public void BaseFloodFill(Stack<Node> stack, int area)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].walkable && this.connections[i].area != area)
				{
					stack.Push(this.connections[i]);
					this.connections[i].area = area;
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000CD70 File Offset: 0x0000B170
		public virtual void UpdateConnections()
		{
			if (this.connections != null)
			{
				List<Node> list = null;
				List<int> list2 = null;
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (!this.connections[i].walkable)
					{
						if (list == null)
						{
							list = new List<Node>(this.connections.Length - 1);
							list2 = new List<int>(this.connections.Length - 1);
							for (int j = 0; j < i; j++)
							{
								list.Add(this.connections[j]);
								list2.Add(this.connectionCosts[j]);
							}
						}
					}
					else if (list != null)
					{
						list.Add(this.connections[i]);
						list2.Add(this.connectionCosts[i]);
					}
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000CE34 File Offset: 0x0000B234
		public virtual void UpdateNeighbourConnections()
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].UpdateConnections();
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000CE74 File Offset: 0x0000B274
		public virtual bool ContainsConnection(Node node)
		{
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i] == node)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000CEB8 File Offset: 0x0000B2B8
		public void AddConnection(Node node, int cost)
		{
			if (this.connections == null)
			{
				this.connections = new Node[0];
				this.connectionCosts = new int[0];
			}
			else
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i] == node)
					{
						this.connectionCosts[i] = cost;
						return;
					}
				}
			}
			Node[] array = this.connections;
			int[] array2 = this.connectionCosts;
			this.connections = new Node[this.connections.Length + 1];
			this.connectionCosts = new int[this.connections.Length];
			for (int j = 0; j < array.Length; j++)
			{
				this.connections[j] = array[j];
				this.connectionCosts[j] = array2[j];
			}
			this.connections[array.Length] = node;
			this.connectionCosts[array.Length] = cost;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000CF94 File Offset: 0x0000B394
		public virtual bool RemoveConnection(Node node)
		{
			if (this.connections == null)
			{
				return false;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i] == node)
				{
					this.connections[i] = this.connections[this.connections.Length - 1];
					this.connectionCosts[i] = this.connectionCosts[this.connectionCosts.Length - 1];
					Node[] array = new Node[this.connections.Length - 1];
					int[] array2 = new int[this.connections.Length - 1];
					for (int j = 0; j < this.connections.Length - 1; j++)
					{
						array[j] = this.connections[j];
						array2[j] = this.connectionCosts[j];
					}
					this.connections = array;
					this.connectionCosts = array2;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D06C File Offset: 0x0000B46C
		public void RecalculateConnectionCosts(bool neighbours)
		{
			for (int i = 0; i < this.connections.Length; i++)
			{
				this.connectionCosts[i] = (int)Math.Round((double)(this.position - this.connections[i].position).magnitude);
			}
			if (neighbours)
			{
				for (int j = 0; j < this.connections.Length; j++)
				{
					this.connections[j].RecalculateConnectionCosts(false);
				}
			}
		}

		// Token: 0x04000177 RID: 375
		private int nodeIndex;

		// Token: 0x04000178 RID: 376
		public Int3 position;

		// Token: 0x04000179 RID: 377
		private uint _penalty;

		// Token: 0x0400017A RID: 378
		public Node[] connections;

		// Token: 0x0400017B RID: 379
		public int[] connectionCosts;

		// Token: 0x0400017C RID: 380
		public int flags;

		// Token: 0x0400017D RID: 381
		private const int WalkableBitNumber = 23;

		// Token: 0x0400017E RID: 382
		private const int WalkableBit = 8388608;

		// Token: 0x0400017F RID: 383
		private const int AreaBitNumber = 24;

		// Token: 0x04000180 RID: 384
		private const int AreaBitsSize = 255;

		// Token: 0x04000181 RID: 385
		private const int NotAreaBits = 16777215;

		// Token: 0x04000182 RID: 386
		private const int GraphIndexBitNumber = 18;

		// Token: 0x04000183 RID: 387
		private const int GraphIndexBitsSize = 31;

		// Token: 0x04000184 RID: 388
		private const int NotGraphIndexBits = -8126465;
	}
}
