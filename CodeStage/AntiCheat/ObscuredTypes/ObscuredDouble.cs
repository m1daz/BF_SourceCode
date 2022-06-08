using System;
using System.Runtime.InteropServices;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000325 RID: 805
	public struct ObscuredDouble : IEquatable<ObscuredDouble>
	{
		// Token: 0x0600191D RID: 6429 RVA: 0x000CFF4E File Offset: 0x000CE34E
		private ObscuredDouble(byte[] value)
		{
			this.currentCryptoKey = ObscuredDouble.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0.0;
			this.inited = true;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000CFF78 File Offset: 0x000CE378
		public static void SetNewCryptoKey(long newKey)
		{
			ObscuredDouble.cryptoKey = newKey;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000CFF80 File Offset: 0x000CE380
		public long GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredDouble.cryptoKey)
			{
				double value = this.InternalDecrypt();
				this.hiddenValue = ObscuredDouble.InternalEncrypt(value);
				this.currentCryptoKey = ObscuredDouble.cryptoKey;
			}
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.b1 = this.hiddenValue[0];
			doubleLongBytesUnion.b2 = this.hiddenValue[1];
			doubleLongBytesUnion.b3 = this.hiddenValue[2];
			doubleLongBytesUnion.b4 = this.hiddenValue[3];
			doubleLongBytesUnion.b5 = this.hiddenValue[4];
			doubleLongBytesUnion.b6 = this.hiddenValue[5];
			doubleLongBytesUnion.b7 = this.hiddenValue[6];
			doubleLongBytesUnion.b8 = this.hiddenValue[7];
			return doubleLongBytesUnion.l;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000D0044 File Offset: 0x000CE444
		public void SetEncrypted(long encrypted)
		{
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.l = encrypted;
			this.hiddenValue = new byte[]
			{
				doubleLongBytesUnion.b1,
				doubleLongBytesUnion.b2,
				doubleLongBytesUnion.b3,
				doubleLongBytesUnion.b4,
				doubleLongBytesUnion.b5,
				doubleLongBytesUnion.b6,
				doubleLongBytesUnion.b7,
				doubleLongBytesUnion.b8
			};
			if (ObscuredDouble.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000D00D3 File Offset: 0x000CE4D3
		public static long Encrypt(double value)
		{
			return ObscuredDouble.Encrypt(value, ObscuredDouble.cryptoKey);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000D00E0 File Offset: 0x000CE4E0
		public static long Encrypt(double value, long key)
		{
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.d = value;
			doubleLongBytesUnion.l ^= key;
			return doubleLongBytesUnion.l;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000D0114 File Offset: 0x000CE514
		private static byte[] InternalEncrypt(double value)
		{
			return ObscuredDouble.InternalEncrypt(value, 0L);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000D0120 File Offset: 0x000CE520
		private static byte[] InternalEncrypt(double value, long key)
		{
			long num = key;
			if (num == 0L)
			{
				num = ObscuredDouble.cryptoKey;
			}
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.d = value;
			doubleLongBytesUnion.l ^= num;
			return new byte[]
			{
				doubleLongBytesUnion.b1,
				doubleLongBytesUnion.b2,
				doubleLongBytesUnion.b3,
				doubleLongBytesUnion.b4,
				doubleLongBytesUnion.b5,
				doubleLongBytesUnion.b6,
				doubleLongBytesUnion.b7,
				doubleLongBytesUnion.b8
			};
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000D01B3 File Offset: 0x000CE5B3
		public static double Decrypt(long value)
		{
			return ObscuredDouble.Decrypt(value, ObscuredDouble.cryptoKey);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000D01C0 File Offset: 0x000CE5C0
		public static double Decrypt(long value, long key)
		{
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.l = (value ^ key);
			return doubleLongBytesUnion.d;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000D01E8 File Offset: 0x000CE5E8
		private double InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredDouble.cryptoKey;
				this.hiddenValue = ObscuredDouble.InternalEncrypt(0.0);
				this.fakeValue = 0.0;
				this.inited = true;
			}
			long num = ObscuredDouble.cryptoKey;
			if (this.currentCryptoKey != ObscuredDouble.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.b1 = this.hiddenValue[0];
			doubleLongBytesUnion.b2 = this.hiddenValue[1];
			doubleLongBytesUnion.b3 = this.hiddenValue[2];
			doubleLongBytesUnion.b4 = this.hiddenValue[3];
			doubleLongBytesUnion.b5 = this.hiddenValue[4];
			doubleLongBytesUnion.b6 = this.hiddenValue[5];
			doubleLongBytesUnion.b7 = this.hiddenValue[6];
			doubleLongBytesUnion.b8 = this.hiddenValue[7];
			doubleLongBytesUnion.l ^= num;
			double d = doubleLongBytesUnion.d;
			if (ObscuredDouble.onCheatingDetected != null && this.fakeValue != 0.0 && Math.Abs(d - this.fakeValue) > 1E-06)
			{
				ObscuredDouble.onCheatingDetected();
				ObscuredDouble.onCheatingDetected = null;
			}
			return d;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000D0334 File Offset: 0x000CE734
		public static implicit operator ObscuredDouble(double value)
		{
			ObscuredDouble result = new ObscuredDouble(ObscuredDouble.InternalEncrypt(value));
			if (ObscuredDouble.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000D0361 File Offset: 0x000CE761
		public static implicit operator double(ObscuredDouble value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000D036C File Offset: 0x000CE76C
		public static ObscuredDouble operator ++(ObscuredDouble input)
		{
			double value = input.InternalDecrypt() + 1.0;
			input.hiddenValue = ObscuredDouble.InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredDouble.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000D03B4 File Offset: 0x000CE7B4
		public static ObscuredDouble operator --(ObscuredDouble input)
		{
			double value = input.InternalDecrypt() - 1.0;
			input.hiddenValue = ObscuredDouble.InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredDouble.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000D03FC File Offset: 0x000CE7FC
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000D0420 File Offset: 0x000CE820
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000D043C File Offset: 0x000CE83C
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000D0458 File Offset: 0x000CE858
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000D0478 File Offset: 0x000CE878
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredDouble))
			{
				return false;
			}
			double num = ((ObscuredDouble)obj).InternalDecrypt();
			double num2 = this.InternalDecrypt();
			return num == num2 || (double.IsNaN(num) && double.IsNaN(num2));
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000D04C8 File Offset: 0x000CE8C8
		public bool Equals(ObscuredDouble obj)
		{
			double num = obj.InternalDecrypt();
			double num2 = this.InternalDecrypt();
			return num == num2 || (double.IsNaN(num) && double.IsNaN(num2));
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000D0504 File Offset: 0x000CE904
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x04001C15 RID: 7189
		public static Action onCheatingDetected;

		// Token: 0x04001C16 RID: 7190
		private static long cryptoKey = 210987L;

		// Token: 0x04001C17 RID: 7191
		private long currentCryptoKey;

		// Token: 0x04001C18 RID: 7192
		private byte[] hiddenValue;

		// Token: 0x04001C19 RID: 7193
		private double fakeValue;

		// Token: 0x04001C1A RID: 7194
		private bool inited;

		// Token: 0x02000326 RID: 806
		[StructLayout(2)]
		private struct DoubleLongBytesUnion
		{
			// Token: 0x04001C1B RID: 7195
			[FieldOffset(0)]
			public double d;

			// Token: 0x04001C1C RID: 7196
			[FieldOffset(0)]
			public long l;

			// Token: 0x04001C1D RID: 7197
			[FieldOffset(0)]
			public byte b1;

			// Token: 0x04001C1E RID: 7198
			[FieldOffset(1)]
			public byte b2;

			// Token: 0x04001C1F RID: 7199
			[FieldOffset(2)]
			public byte b3;

			// Token: 0x04001C20 RID: 7200
			[FieldOffset(3)]
			public byte b4;

			// Token: 0x04001C21 RID: 7201
			[FieldOffset(4)]
			public byte b5;

			// Token: 0x04001C22 RID: 7202
			[FieldOffset(5)]
			public byte b6;

			// Token: 0x04001C23 RID: 7203
			[FieldOffset(6)]
			public byte b7;

			// Token: 0x04001C24 RID: 7204
			[FieldOffset(7)]
			public byte b8;
		}
	}
}
