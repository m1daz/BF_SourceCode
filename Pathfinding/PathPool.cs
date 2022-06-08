using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000030 RID: 48
	public static class PathPool<T> where T : Path, new()
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000C1E8 File Offset: 0x0000A5E8
		public static void Recycle(T path)
		{
			object obj = PathPool<T>.pool;
			lock (obj)
			{
				if (path.GetType() != typeof(T))
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Cannot recycle path of type '",
						path.GetType().Name,
						"' in a pool for path type '",
						typeof(T).Name,
						"'.\nMost likely the path type does not have support for recycling. Please do not call Recycle () on that path"
					}));
				}
				path.recycled = true;
				path.OnEnterPool();
				PathPool<T>.pool.Push(path);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000C2B0 File Offset: 0x0000A6B0
		public static void Warmup(int count, int length)
		{
			ListPool<Node>.Warmup(count, length);
			ListPool<Vector3>.Warmup(count, length);
			Path[] array = new Path[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = PathPool<T>.GetPath();
				array[i].Claim(array);
			}
			for (int j = 0; j < count; j++)
			{
				array[j].Release(array);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000C315 File Offset: 0x0000A715
		public static int GetTotalCreated()
		{
			return PathPool<T>.totalCreated;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000C31C File Offset: 0x0000A71C
		public static int GetSize()
		{
			return PathPool<T>.pool.Count;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000C328 File Offset: 0x0000A728
		public static T GetPath()
		{
			object obj = PathPool<T>.pool;
			T result;
			lock (obj)
			{
				T t;
				if (PathPool<T>.pool.Count > 0)
				{
					t = PathPool<T>.pool.Pop();
				}
				else
				{
					t = Activator.CreateInstance<T>();
					PathPool<T>.totalCreated++;
				}
				t.recycled = false;
				t.Reset();
				result = t;
			}
			return result;
		}

		// Token: 0x04000169 RID: 361
		private static Stack<T> pool = new Stack<T>();

		// Token: 0x0400016A RID: 362
		private static int totalCreated;
	}
}
