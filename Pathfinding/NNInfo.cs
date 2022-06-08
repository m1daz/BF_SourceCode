using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000B RID: 11
	public struct NNInfo
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003BC0 File Offset: 0x00001FC0
		public NNInfo(Node node)
		{
			this.node = node;
			this.constrainedNode = null;
			this.constClampedPosition = Vector3.zero;
			if (node != null)
			{
				this.clampedPosition = (Vector3)node.position;
			}
			else
			{
				this.clampedPosition = Vector3.zero;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C0D File Offset: 0x0000200D
		public void SetConstrained(Node constrainedNode, Vector3 clampedPosition)
		{
			this.constrainedNode = constrainedNode;
			this.constClampedPosition = clampedPosition;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003C20 File Offset: 0x00002020
		public void UpdateInfo()
		{
			if (this.node != null)
			{
				this.clampedPosition = (Vector3)this.node.position;
			}
			else
			{
				this.clampedPosition = Vector3.zero;
			}
			if (this.constrainedNode != null)
			{
				this.constClampedPosition = (Vector3)this.constrainedNode.position;
			}
			else
			{
				this.constClampedPosition = Vector3.zero;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003C8F File Offset: 0x0000208F
		public static explicit operator Vector3(NNInfo ob)
		{
			return ob.clampedPosition;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003C98 File Offset: 0x00002098
		public static explicit operator Node(NNInfo ob)
		{
			return ob.node;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003CA1 File Offset: 0x000020A1
		public static explicit operator NNInfo(Node ob)
		{
			return new NNInfo(ob);
		}

		// Token: 0x04000065 RID: 101
		public Node node;

		// Token: 0x04000066 RID: 102
		public Node constrainedNode;

		// Token: 0x04000067 RID: 103
		public Vector3 clampedPosition;

		// Token: 0x04000068 RID: 104
		public Vector3 constClampedPosition;
	}
}
