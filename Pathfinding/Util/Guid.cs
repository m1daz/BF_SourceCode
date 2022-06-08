using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x020000C0 RID: 192
	public struct Guid
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x00036600 File Offset: 0x00034A00
		public Guid(byte[] bytes)
		{
			this._a = ((ulong)bytes[0] << 0 | (ulong)bytes[1] << 8 | (ulong)bytes[2] << 16 | (ulong)bytes[3] << 24 | (ulong)bytes[4] << 32 | (ulong)bytes[5] << 40 | (ulong)bytes[6] << 48 | (ulong)bytes[7] << 56);
			this._b = ((ulong)bytes[8] << 0 | (ulong)bytes[9] << 8 | (ulong)bytes[10] << 16 | (ulong)bytes[11] << 24 | (ulong)bytes[12] << 32 | (ulong)bytes[13] << 40 | (ulong)bytes[14] << 48 | (ulong)bytes[15] << 56);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0003669C File Offset: 0x00034A9C
		public Guid(string str)
		{
			this._a = 0UL;
			this._b = 0UL;
			if (str.Length < 32)
			{
				throw new FormatException("Invalid Guid format");
			}
			int i = 0;
			int num = 0;
			int num2 = 60;
			while (i < 16)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c = str[num];
				if (c != '-')
				{
					int num3 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c));
					if (num3 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c + " is not a hexadecimal character");
					}
					this._a |= (ulong)((ulong)((long)num3) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
			num2 = 60;
			while (i < 32)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c2 = str[num];
				if (c2 != '-')
				{
					int num4 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c2));
					if (num4 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c2 + " is not a hexadecimal character");
					}
					this._b |= (ulong)((ulong)((long)num4) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000367FD File Offset: 0x00034BFD
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00036808 File Offset: 0x00034C08
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(this._a);
			byte[] bytes2 = BitConverter.GetBytes(this._b);
			for (int i = 0; i < 8; i++)
			{
				array[i] = bytes[i];
				array[i + 8] = bytes2[i];
			}
			return array;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00036858 File Offset: 0x00034C58
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Random random = new Random();
			random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00036880 File Offset: 0x00034C80
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000368A8 File Offset: 0x00034CA8
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000368D4 File Offset: 0x00034CD4
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00036919 File Offset: 0x00034D19
		public override int GetHashCode()
		{
			return (int)this._a;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00036924 File Offset: 0x00034D24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._a.ToString("x16")).Append('-').Append(this._b.ToString("x16"));
			return stringBuilder.ToString();
		}

		// Token: 0x040004BF RID: 1215
		private ulong _a;

		// Token: 0x040004C0 RID: 1216
		private ulong _b;

		// Token: 0x040004C1 RID: 1217
		private const string hex = "0123456789ABCDEF";
	}
}
