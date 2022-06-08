using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x0200002F RID: 47
	public static class ListPool<T>
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000C064 File Offset: 0x0000A464
		public static List<T> Claim()
		{
			if (ListPool<T>.pool.Count > 0)
			{
				List<T> result = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
				ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
				return result;
			}
			return new List<T>();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C0B8 File Offset: 0x0000A4B8
		public static List<T> Claim(int capacity)
		{
			if (ListPool<T>.pool.Count > 0)
			{
				List<T> list = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
				ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
				if (list.Capacity < capacity)
				{
					list.Capacity = capacity;
				}
				return list;
			}
			return new List<T>(capacity);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C120 File Offset: 0x0000A520
		public static void Warmup(int count, int size)
		{
			List<T>[] array = new List<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ListPool<T>.Claim(size);
			}
			for (int j = 0; j < count; j++)
			{
				ListPool<T>.Release(array[j]);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C16C File Offset: 0x0000A56C
		public static void Release(List<T> list)
		{
			for (int i = 0; i < ListPool<T>.pool.Count; i++)
			{
				if (ListPool<T>.pool[i] == list)
				{
					throw new InvalidOperationException("The List is released even though it is in the pool");
				}
			}
			list.Clear();
			ListPool<T>.pool.Add(list);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000C1C1 File Offset: 0x0000A5C1
		public static void Clear()
		{
			ListPool<T>.pool.Clear();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000C1CD File Offset: 0x0000A5CD
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x04000168 RID: 360
		private static List<List<T>> pool = new List<List<T>>();
	}
}
