using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007D RID: 125
	public class BBTreeBox
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x00026900 File Offset: 0x00024D00
		public BBTreeBox(BBTree tree, MeshNode node)
		{
			this.node = node;
			Vector3 vector = (Vector3)tree.graph.vertices[node[0]];
			Vector2 vector2 = new Vector2(vector.x, vector.z);
			Vector2 vector3 = vector2;
			for (int i = 1; i < 3; i++)
			{
				Vector3 vector4 = (Vector3)tree.graph.vertices[node[i]];
				vector2.x = Mathf.Min(vector2.x, vector4.x);
				vector2.y = Mathf.Min(vector2.y, vector4.z);
				vector3.x = Mathf.Max(vector3.x, vector4.x);
				vector3.y = Mathf.Max(vector3.y, vector4.z);
			}
			this.rect = Rect.MinMaxRect(vector2.x, vector2.y, vector3.x, vector3.y);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00026A17 File Offset: 0x00024E17
		public bool Contains(Vector3 p)
		{
			return this.rect.Contains(p);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00026A28 File Offset: 0x00024E28
		public void WriteChildren(int level)
		{
			for (int i = 0; i < level; i++)
			{
				Console.Write("  ");
			}
			if (this.node != null)
			{
				Console.WriteLine("Leaf ");
			}
			else
			{
				Console.WriteLine("Box ");
				this.c1.WriteChildren(level + 1);
				this.c2.WriteChildren(level + 1);
			}
		}

		// Token: 0x04000355 RID: 853
		public Rect rect;

		// Token: 0x04000356 RID: 854
		public MeshNode node;

		// Token: 0x04000357 RID: 855
		public BBTreeBox c1;

		// Token: 0x04000358 RID: 856
		public BBTreeBox c2;
	}
}
