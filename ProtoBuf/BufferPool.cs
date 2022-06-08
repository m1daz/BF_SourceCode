using System;
using System.Threading;

namespace ProtoBuf
{
	// Token: 0x02000642 RID: 1602
	internal sealed class BufferPool
	{
		// Token: 0x06002E70 RID: 11888 RVA: 0x0015429C File Offset: 0x0015269C
		private BufferPool()
		{
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x001542A4 File Offset: 0x001526A4
		internal static void Flush()
		{
			for (int i = 0; i < BufferPool.pool.Length; i++)
			{
				Interlocked.Exchange(ref BufferPool.pool[i], null);
			}
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x001542DC File Offset: 0x001526DC
		internal static byte[] GetBuffer()
		{
			for (int i = 0; i < BufferPool.pool.Length; i++)
			{
				object obj;
				if ((obj = Interlocked.Exchange(ref BufferPool.pool[i], null)) != null)
				{
					return (byte[])obj;
				}
			}
			return new byte[1024];
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x0015432C File Offset: 0x0015272C
		internal static void ResizeAndFlushLeft(ref byte[] buffer, int toFitAtLeastBytes, int copyFromIndex, int copyBytes)
		{
			int num = buffer.Length * 2;
			if (num < toFitAtLeastBytes)
			{
				num = toFitAtLeastBytes;
			}
			byte[] array = new byte[num];
			if (copyBytes > 0)
			{
				Helpers.BlockCopy(buffer, copyFromIndex, array, 0, copyBytes);
			}
			if (buffer.Length == 1024)
			{
				BufferPool.ReleaseBufferToPool(ref buffer);
			}
			buffer = array;
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0015437C File Offset: 0x0015277C
		internal static void ReleaseBufferToPool(ref byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			if (buffer.Length == 1024)
			{
				for (int i = 0; i < BufferPool.pool.Length; i++)
				{
					if (Interlocked.CompareExchange(ref BufferPool.pool[i], buffer, null) == null)
					{
						break;
					}
				}
			}
			buffer = null;
		}

		// Token: 0x04002D6B RID: 11627
		private const int PoolSize = 20;

		// Token: 0x04002D6C RID: 11628
		internal const int BufferLength = 1024;

		// Token: 0x04002D6D RID: 11629
		private static readonly object[] pool = new object[20];
	}
}
