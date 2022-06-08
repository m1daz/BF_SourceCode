using System;
using System.Runtime.InteropServices;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000323 RID: 803
	public struct ObscuredDecimal : IEquatable<ObscuredDecimal>
	{
		// Token: 0x06001906 RID: 6406 RVA: 0x000CF7AD File Offset: 0x000CDBAD
		private ObscuredDecimal(byte[] value)
		{
			this.currentCryptoKey = ObscuredDecimal.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0m;
			this.inited = true;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000CF7D4 File Offset: 0x000CDBD4
		public static void SetNewCryptoKey(long newKey)
		{
			ObscuredDecimal.cryptoKey = newKey;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000CF7DC File Offset: 0x000CDBDC
		public decimal GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredDecimal.cryptoKey)
			{
				decimal value = this.InternalDecrypt();
				this.hiddenValue = ObscuredDecimal.InternalEncrypt(value);
				this.currentCryptoKey = ObscuredDecimal.cryptoKey;
			}
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.b1 = this.hiddenValue[0];
			decimalLongBytesUnion.b2 = this.hiddenValue[1];
			decimalLongBytesUnion.b3 = this.hiddenValue[2];
			decimalLongBytesUnion.b4 = this.hiddenValue[3];
			decimalLongBytesUnion.b5 = this.hiddenValue[4];
			decimalLongBytesUnion.b6 = this.hiddenValue[5];
			decimalLongBytesUnion.b7 = this.hiddenValue[6];
			decimalLongBytesUnion.b8 = this.hiddenValue[7];
			decimalLongBytesUnion.b9 = this.hiddenValue[8];
			decimalLongBytesUnion.b10 = this.hiddenValue[9];
			decimalLongBytesUnion.b11 = this.hiddenValue[10];
			decimalLongBytesUnion.b12 = this.hiddenValue[11];
			decimalLongBytesUnion.b13 = this.hiddenValue[12];
			decimalLongBytesUnion.b14 = this.hiddenValue[13];
			decimalLongBytesUnion.b15 = this.hiddenValue[14];
			decimalLongBytesUnion.b16 = this.hiddenValue[15];
			return decimalLongBytesUnion.d;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000CF920 File Offset: 0x000CDD20
		public void SetEncrypted(decimal encrypted)
		{
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = encrypted;
			this.hiddenValue = new byte[]
			{
				decimalLongBytesUnion.b1,
				decimalLongBytesUnion.b2,
				decimalLongBytesUnion.b3,
				decimalLongBytesUnion.b4,
				decimalLongBytesUnion.b5,
				decimalLongBytesUnion.b6,
				decimalLongBytesUnion.b7,
				decimalLongBytesUnion.b8,
				decimalLongBytesUnion.b9,
				decimalLongBytesUnion.b10,
				decimalLongBytesUnion.b11,
				decimalLongBytesUnion.b12,
				decimalLongBytesUnion.b13,
				decimalLongBytesUnion.b14,
				decimalLongBytesUnion.b15,
				decimalLongBytesUnion.b16
			};
			if (ObscuredDecimal.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000CFA07 File Offset: 0x000CDE07
		public static decimal Encrypt(decimal value)
		{
			return ObscuredDecimal.Encrypt(value, ObscuredDecimal.cryptoKey);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000CFA14 File Offset: 0x000CDE14
		public static decimal Encrypt(decimal value, long key)
		{
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= key;
			decimalLongBytesUnion.l2 ^= key;
			return decimalLongBytesUnion.d;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000CFA58 File Offset: 0x000CDE58
		private static byte[] InternalEncrypt(decimal value)
		{
			return ObscuredDecimal.InternalEncrypt(value, 0L);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000CFA64 File Offset: 0x000CDE64
		private static byte[] InternalEncrypt(decimal value, long key)
		{
			long num = key;
			if (num == 0L)
			{
				num = ObscuredDecimal.cryptoKey;
			}
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= num;
			decimalLongBytesUnion.l2 ^= num;
			return new byte[]
			{
				decimalLongBytesUnion.b1,
				decimalLongBytesUnion.b2,
				decimalLongBytesUnion.b3,
				decimalLongBytesUnion.b4,
				decimalLongBytesUnion.b5,
				decimalLongBytesUnion.b6,
				decimalLongBytesUnion.b7,
				decimalLongBytesUnion.b8,
				decimalLongBytesUnion.b9,
				decimalLongBytesUnion.b10,
				decimalLongBytesUnion.b11,
				decimalLongBytesUnion.b12,
				decimalLongBytesUnion.b13,
				decimalLongBytesUnion.b14,
				decimalLongBytesUnion.b15,
				decimalLongBytesUnion.b16
			};
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000CFB5F File Offset: 0x000CDF5F
		public static decimal Decrypt(decimal value)
		{
			return ObscuredDecimal.Decrypt(value, ObscuredDecimal.cryptoKey);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000CFB6C File Offset: 0x000CDF6C
		public static decimal Decrypt(decimal value, long key)
		{
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= key;
			decimalLongBytesUnion.l2 ^= key;
			return decimalLongBytesUnion.d;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000CFBB0 File Offset: 0x000CDFB0
		private decimal InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredDecimal.cryptoKey;
				this.hiddenValue = ObscuredDecimal.InternalEncrypt(0m);
				this.fakeValue = 0m;
				this.inited = true;
			}
			long num = ObscuredDecimal.cryptoKey;
			if (this.currentCryptoKey != ObscuredDecimal.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.b1 = this.hiddenValue[0];
			decimalLongBytesUnion.b2 = this.hiddenValue[1];
			decimalLongBytesUnion.b3 = this.hiddenValue[2];
			decimalLongBytesUnion.b4 = this.hiddenValue[3];
			decimalLongBytesUnion.b5 = this.hiddenValue[4];
			decimalLongBytesUnion.b6 = this.hiddenValue[5];
			decimalLongBytesUnion.b7 = this.hiddenValue[6];
			decimalLongBytesUnion.b8 = this.hiddenValue[7];
			decimalLongBytesUnion.b9 = this.hiddenValue[8];
			decimalLongBytesUnion.b10 = this.hiddenValue[9];
			decimalLongBytesUnion.b11 = this.hiddenValue[10];
			decimalLongBytesUnion.b12 = this.hiddenValue[11];
			decimalLongBytesUnion.b13 = this.hiddenValue[12];
			decimalLongBytesUnion.b14 = this.hiddenValue[13];
			decimalLongBytesUnion.b15 = this.hiddenValue[14];
			decimalLongBytesUnion.b16 = this.hiddenValue[15];
			decimalLongBytesUnion.l1 ^= num;
			decimalLongBytesUnion.l2 ^= num;
			decimal d = decimalLongBytesUnion.d;
			if (ObscuredDecimal.onCheatingDetected != null && this.fakeValue != 0m && d != this.fakeValue)
			{
				ObscuredDecimal.onCheatingDetected();
				ObscuredDecimal.onCheatingDetected = null;
			}
			return d;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000CFD80 File Offset: 0x000CE180
		public static implicit operator ObscuredDecimal(decimal value)
		{
			ObscuredDecimal result = new ObscuredDecimal(ObscuredDecimal.InternalEncrypt(value));
			if (ObscuredDecimal.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000CFDAD File Offset: 0x000CE1AD
		public static implicit operator decimal(ObscuredDecimal value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000CFDB8 File Offset: 0x000CE1B8
		public static ObscuredDecimal operator ++(ObscuredDecimal input)
		{
			decimal value = input.InternalDecrypt() + 1m;
			input.hiddenValue = ObscuredDecimal.InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredDecimal.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000CFE00 File Offset: 0x000CE200
		public static ObscuredDecimal operator --(ObscuredDecimal input)
		{
			decimal value = input.InternalDecrypt() - 1m;
			input.hiddenValue = ObscuredDecimal.InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredDecimal.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000CFE48 File Offset: 0x000CE248
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000CFE6C File Offset: 0x000CE26C
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000CFE88 File Offset: 0x000CE288
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000CFEA4 File Offset: 0x000CE2A4
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000CFEC4 File Offset: 0x000CE2C4
		public override bool Equals(object obj)
		{
			return obj is ObscuredDecimal && ((ObscuredDecimal)obj).InternalDecrypt().Equals(this.InternalDecrypt());
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000CFEFC File Offset: 0x000CE2FC
		public bool Equals(ObscuredDecimal obj)
		{
			return obj.InternalDecrypt().Equals(this.InternalDecrypt());
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000CFF20 File Offset: 0x000CE320
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x04001BFC RID: 7164
		public static Action onCheatingDetected;

		// Token: 0x04001BFD RID: 7165
		private static long cryptoKey = 209208L;

		// Token: 0x04001BFE RID: 7166
		private long currentCryptoKey;

		// Token: 0x04001BFF RID: 7167
		private byte[] hiddenValue;

		// Token: 0x04001C00 RID: 7168
		private decimal fakeValue;

		// Token: 0x04001C01 RID: 7169
		private bool inited;

		// Token: 0x02000324 RID: 804
		[StructLayout(2)]
		private struct DecimalLongBytesUnion
		{
			// Token: 0x04001C02 RID: 7170
			[FieldOffset(0)]
			public decimal d;

			// Token: 0x04001C03 RID: 7171
			[FieldOffset(0)]
			public long l1;

			// Token: 0x04001C04 RID: 7172
			[FieldOffset(8)]
			public long l2;

			// Token: 0x04001C05 RID: 7173
			[FieldOffset(0)]
			public byte b1;

			// Token: 0x04001C06 RID: 7174
			[FieldOffset(1)]
			public byte b2;

			// Token: 0x04001C07 RID: 7175
			[FieldOffset(2)]
			public byte b3;

			// Token: 0x04001C08 RID: 7176
			[FieldOffset(3)]
			public byte b4;

			// Token: 0x04001C09 RID: 7177
			[FieldOffset(4)]
			public byte b5;

			// Token: 0x04001C0A RID: 7178
			[FieldOffset(5)]
			public byte b6;

			// Token: 0x04001C0B RID: 7179
			[FieldOffset(6)]
			public byte b7;

			// Token: 0x04001C0C RID: 7180
			[FieldOffset(7)]
			public byte b8;

			// Token: 0x04001C0D RID: 7181
			[FieldOffset(8)]
			public byte b9;

			// Token: 0x04001C0E RID: 7182
			[FieldOffset(9)]
			public byte b10;

			// Token: 0x04001C0F RID: 7183
			[FieldOffset(10)]
			public byte b11;

			// Token: 0x04001C10 RID: 7184
			[FieldOffset(11)]
			public byte b12;

			// Token: 0x04001C11 RID: 7185
			[FieldOffset(12)]
			public byte b13;

			// Token: 0x04001C12 RID: 7186
			[FieldOffset(13)]
			public byte b14;

			// Token: 0x04001C13 RID: 7187
			[FieldOffset(14)]
			public byte b15;

			// Token: 0x04001C14 RID: 7188
			[FieldOffset(15)]
			public byte b16;
		}
	}
}
