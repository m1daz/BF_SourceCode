using System;

namespace Pathfinding
{
	// Token: 0x0200002A RID: 42
	public class BinaryHeapM
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000B6D8 File Offset: 0x00009AD8
		public BinaryHeapM(int numberOfElements)
		{
			this.binaryHeap = new NodeRun[numberOfElements];
			this.numberOfItems = 2;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000B6FE File Offset: 0x00009AFE
		public void Clear()
		{
			this.numberOfItems = 1;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000B707 File Offset: 0x00009B07
		public NodeRun GetNode(int i)
		{
			return this.binaryHeap[i];
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000B714 File Offset: 0x00009B14
		public void Add(NodeRun node)
		{
			if (node == null)
			{
				throw new Exception("Sending null node to BinaryHeap");
			}
			if (this.numberOfItems == this.binaryHeap.Length)
			{
				int num = Math.Max(this.binaryHeap.Length + 4, (int)Math.Round((double)((float)this.binaryHeap.Length * this.growthFactor)));
				if (num > 262144)
				{
					throw new Exception("Binary Heap Size really large (2^18). A heap size this large is probably the cause of pathfinding running in an infinite loop. \nRemove this check (in BinaryHeap.cs) if you are sure that it is not caused by a bug");
				}
				NodeRun[] array = new NodeRun[num];
				for (int i = 0; i < this.binaryHeap.Length; i++)
				{
					array[i] = this.binaryHeap[i];
				}
				this.binaryHeap = array;
			}
			this.binaryHeap[this.numberOfItems] = node;
			int num2 = this.numberOfItems;
			uint f = node.f;
			while (num2 != 1)
			{
				int num3 = num2 / 2;
				if (f >= this.binaryHeap[num3].f)
				{
					break;
				}
				this.binaryHeap[num2] = this.binaryHeap[num3];
				this.binaryHeap[num3] = node;
				num2 = num3;
			}
			this.numberOfItems++;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000B830 File Offset: 0x00009C30
		public NodeRun Remove()
		{
			this.numberOfItems--;
			NodeRun result = this.binaryHeap[1];
			this.binaryHeap[1] = this.binaryHeap[this.numberOfItems];
			int num = 1;
			int num2;
			do
			{
				num2 = num;
				int num3 = num2 * 2;
				if (num3 + 1 <= this.numberOfItems)
				{
					if (this.binaryHeap[num2].f >= this.binaryHeap[num3].f)
					{
						num = num3;
					}
					if (this.binaryHeap[num].f >= this.binaryHeap[num3 + 1].f)
					{
						num = num3 + 1;
					}
				}
				else if (num3 <= this.numberOfItems && this.binaryHeap[num2].f >= this.binaryHeap[num3].f)
				{
					num = num3;
				}
				if (num2 != num)
				{
					NodeRun nodeRun = this.binaryHeap[num2];
					this.binaryHeap[num2] = this.binaryHeap[num];
					this.binaryHeap[num] = nodeRun;
				}
			}
			while (num2 != num);
			return result;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000B92C File Offset: 0x00009D2C
		public void Rebuild()
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				NodeRun nodeRun = this.binaryHeap[i];
				uint f = nodeRun.f;
				while (num != 1)
				{
					int num2 = num / 2;
					if (f >= this.binaryHeap[num2].f)
					{
						break;
					}
					this.binaryHeap[num] = this.binaryHeap[num2];
					this.binaryHeap[num2] = nodeRun;
					num = num2;
				}
			}
		}

		// Token: 0x04000153 RID: 339
		private NodeRun[] binaryHeap;

		// Token: 0x04000154 RID: 340
		public int numberOfItems;

		// Token: 0x04000155 RID: 341
		public float growthFactor = 2f;
	}
}
