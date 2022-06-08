using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000031 RID: 49
	public static class StackPool<T>
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000C3B8 File Offset: 0x0000A7B8
		public static Stack<T> Claim()
		{
			if (StackPool<T>.pool.Count > 0)
			{
				Stack<T> result = StackPool<T>.pool[StackPool<T>.pool.Count - 1];
				StackPool<T>.pool.RemoveAt(StackPool<T>.pool.Count - 1);
				return result;
			}
			return new Stack<T>();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C40C File Offset: 0x0000A80C
		public static void Warmup(int count)
		{
			Stack<T>[] array = new Stack<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = StackPool<T>.Claim();
			}
			for (int j = 0; j < count; j++)
			{
				StackPool<T>.Release(array[j]);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C454 File Offset: 0x0000A854
		public static void Release(Stack<T> stack)
		{
			for (int i = 0; i < StackPool<T>.pool.Count; i++)
			{
				if (StackPool<T>.pool[i] == stack)
				{
					Debug.LogError("The Stack is released even though it is inside the pool");
				}
			}
			stack.Clear();
			StackPool<T>.pool.Add(stack);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C4A8 File Offset: 0x0000A8A8
		public static void Clear()
		{
			StackPool<T>.pool.Clear();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C4B4 File Offset: 0x0000A8B4
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x0400016B RID: 363
		private static List<Stack<T>> pool = new List<Stack<T>>();
	}
}
