using System;
using System.Collections;

namespace ProtoBuf.Meta
{
	// Token: 0x02000652 RID: 1618
	internal class BasicList : IEnumerable
	{
		// Token: 0x06002EC4 RID: 11972 RVA: 0x001550F8 File Offset: 0x001534F8
		public void CopyTo(Array array, int offset)
		{
			this.head.CopyTo(array, offset);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x00155108 File Offset: 0x00153508
		public int Add(object value)
		{
			return (this.head = this.head.Append(value)).Length - 1;
		}

		// Token: 0x17000381 RID: 897
		public object this[int index]
		{
			get
			{
				return this.head[index];
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0015513F File Offset: 0x0015353F
		public void Trim()
		{
			this.head = this.head.Trim();
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x00155152 File Offset: 0x00153552
		public int Count
		{
			get
			{
				return this.head.Length;
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0015515F File Offset: 0x0015355F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00155171 File Offset: 0x00153571
		public BasicList.NodeEnumerator GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0015517E File Offset: 0x0015357E
		internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
		{
			return this.head.IndexOf(predicate, ctx);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0015518D File Offset: 0x0015358D
		internal int IndexOfString(string value)
		{
			return this.head.IndexOfString(value);
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0015519B File Offset: 0x0015359B
		internal int IndexOfReference(object instance)
		{
			return this.head.IndexOfReference(instance);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x001551AC File Offset: 0x001535AC
		internal bool Contains(object value)
		{
			foreach (object objA in this)
			{
				if (object.Equals(objA, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x001551E8 File Offset: 0x001535E8
		internal static BasicList GetContiguousGroups(int[] keys, object[] values)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length < keys.Length)
			{
				throw new ArgumentException("Not all keys are covered by values", "values");
			}
			BasicList basicList = new BasicList();
			BasicList.Group group = null;
			for (int i = 0; i < keys.Length; i++)
			{
				if (i == 0 || keys[i] != keys[i - 1])
				{
					group = null;
				}
				if (group == null)
				{
					group = new BasicList.Group(keys[i]);
					basicList.Add(group);
				}
				group.Items.Add(values[i]);
			}
			return basicList;
		}

		// Token: 0x04002D92 RID: 11666
		private static readonly BasicList.Node nil = new BasicList.Node(null, 0);

		// Token: 0x04002D93 RID: 11667
		protected BasicList.Node head = BasicList.nil;

		// Token: 0x02000653 RID: 1619
		public struct NodeEnumerator : IEnumerator
		{
			// Token: 0x06002ED1 RID: 11985 RVA: 0x00155298 File Offset: 0x00153698
			internal NodeEnumerator(BasicList.Node node)
			{
				this.position = -1;
				this.node = node;
			}

			// Token: 0x06002ED2 RID: 11986 RVA: 0x001552A8 File Offset: 0x001536A8
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x001552B1 File Offset: 0x001536B1
			public object Current
			{
				get
				{
					return this.node[this.position];
				}
			}

			// Token: 0x06002ED4 RID: 11988 RVA: 0x001552C4 File Offset: 0x001536C4
			public bool MoveNext()
			{
				int length = this.node.Length;
				return this.position <= length && ++this.position < length;
			}

			// Token: 0x04002D94 RID: 11668
			private int position;

			// Token: 0x04002D95 RID: 11669
			private readonly BasicList.Node node;
		}

		// Token: 0x02000654 RID: 1620
		internal sealed class Node
		{
			// Token: 0x06002ED5 RID: 11989 RVA: 0x00155300 File Offset: 0x00153700
			internal Node(object[] data, int length)
			{
				this.data = data;
				this.length = length;
			}

			// Token: 0x17000384 RID: 900
			public object this[int index]
			{
				get
				{
					if (index >= 0 && index < this.length)
					{
						return this.data[index];
					}
					throw new ArgumentOutOfRangeException("index");
				}
				set
				{
					if (index >= 0 && index < this.length)
					{
						this.data[index] = value;
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x0015536C File Offset: 0x0015376C
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x06002ED9 RID: 11993 RVA: 0x00155374 File Offset: 0x00153774
			public void RemoveLastWithMutate()
			{
				if (this.length == 0)
				{
					throw new InvalidOperationException();
				}
				this.length--;
			}

			// Token: 0x06002EDA RID: 11994 RVA: 0x00155398 File Offset: 0x00153798
			public BasicList.Node Append(object value)
			{
				int num = this.length + 1;
				object[] array;
				if (this.data == null)
				{
					array = new object[10];
				}
				else if (this.length == this.data.Length)
				{
					array = new object[this.data.Length * 2];
					Array.Copy(this.data, array, this.length);
				}
				else
				{
					array = this.data;
				}
				array[this.length] = value;
				return new BasicList.Node(array, num);
			}

			// Token: 0x06002EDB RID: 11995 RVA: 0x00155418 File Offset: 0x00153818
			public BasicList.Node Trim()
			{
				if (this.length == 0 || this.length == this.data.Length)
				{
					return this;
				}
				object[] destinationArray = new object[this.length];
				Array.Copy(this.data, destinationArray, this.length);
				return new BasicList.Node(destinationArray, this.length);
			}

			// Token: 0x06002EDC RID: 11996 RVA: 0x00155470 File Offset: 0x00153870
			internal int IndexOfString(string value)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (value == (string)this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06002EDD RID: 11997 RVA: 0x001554B0 File Offset: 0x001538B0
			internal int IndexOfReference(object instance)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (instance == this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06002EDE RID: 11998 RVA: 0x001554E8 File Offset: 0x001538E8
			internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (predicate(this.data[i], ctx))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06002EDF RID: 11999 RVA: 0x00155523 File Offset: 0x00153923
			internal void CopyTo(Array array, int offset)
			{
				if (this.length > 0)
				{
					Array.Copy(this.data, 0, array, offset, this.length);
				}
			}

			// Token: 0x06002EE0 RID: 12000 RVA: 0x00155545 File Offset: 0x00153945
			internal void Clear()
			{
				if (this.data != null)
				{
					Array.Clear(this.data, 0, this.data.Length);
				}
				this.length = 0;
			}

			// Token: 0x04002D96 RID: 11670
			private readonly object[] data;

			// Token: 0x04002D97 RID: 11671
			private int length;
		}

		// Token: 0x02000655 RID: 1621
		// (Invoke) Token: 0x06002EE2 RID: 12002
		internal delegate bool MatchPredicate(object value, object ctx);

		// Token: 0x02000656 RID: 1622
		internal sealed class Group
		{
			// Token: 0x06002EE5 RID: 12005 RVA: 0x0015556D File Offset: 0x0015396D
			public Group(int first)
			{
				this.First = first;
				this.Items = new BasicList();
			}

			// Token: 0x04002D98 RID: 11672
			public readonly int First;

			// Token: 0x04002D99 RID: 11673
			public readonly BasicList Items;
		}
	}
}
