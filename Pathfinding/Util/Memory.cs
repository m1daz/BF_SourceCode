using System;

namespace Pathfinding.Util
{
	// Token: 0x020000BB RID: 187
	public static class Memory
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x00035CA8 File Offset: 0x000340A8
		public static void MemSet(byte[] array, byte value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, array.Length);
			while (i < num2)
			{
				array[i++] = value;
			}
			num2 = array.Length;
			while (i < num2)
			{
				Buffer.BlockCopy(array, 0, array, i, Math.Min(num, num2 - i));
				i += num;
				num *= 2;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00035D14 File Offset: 0x00034114
		public static void MemSet<T>(T[] array, T value, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, array.Length);
			while (i < num2)
			{
				array[i++] = value;
			}
			num2 = array.Length;
			while (i < num2)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, num2 - i) * byteSize);
				i += num;
				num *= 2;
			}
		}
	}
}
