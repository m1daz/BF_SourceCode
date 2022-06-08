using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200032A RID: 810
	public struct ObscuredLong : IEquatable<ObscuredLong>
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x000D0D45 File Offset: 0x000CF145
		private ObscuredLong(long value)
		{
			this.currentCryptoKey = ObscuredLong.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0L;
			this.inited = true;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000D0D68 File Offset: 0x000CF168
		public static void SetNewCryptoKey(long newKey)
		{
			ObscuredLong.cryptoKey = newKey;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000D0D70 File Offset: 0x000CF170
		public long GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredLong.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredLong.Encrypt(this.hiddenValue, ObscuredLong.cryptoKey);
				this.currentCryptoKey = ObscuredLong.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000D0DC0 File Offset: 0x000CF1C0
		public void SetEncrypted(long encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredLong.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000D0DDF File Offset: 0x000CF1DF
		public static long Encrypt(long value)
		{
			return ObscuredLong.Encrypt(value, 0L);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000D0DE9 File Offset: 0x000CF1E9
		public static long Decrypt(long value)
		{
			return ObscuredLong.Decrypt(value, 0L);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000D0DF3 File Offset: 0x000CF1F3
		public static long Encrypt(long value, long key)
		{
			if (key == 0L)
			{
				return value ^ ObscuredLong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000D0E08 File Offset: 0x000CF208
		public static long Decrypt(long value, long key)
		{
			if (key == 0L)
			{
				return value ^ ObscuredLong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000D0E20 File Offset: 0x000CF220
		private long InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredLong.cryptoKey;
				this.hiddenValue = ObscuredLong.Encrypt(0L);
				this.fakeValue = 0L;
				this.inited = true;
			}
			long key = ObscuredLong.cryptoKey;
			if (this.currentCryptoKey != ObscuredLong.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			long num = ObscuredLong.Decrypt(this.hiddenValue, key);
			if (ObscuredLong.onCheatingDetected != null && this.fakeValue != 0L && num != this.fakeValue)
			{
				ObscuredLong.onCheatingDetected();
				ObscuredLong.onCheatingDetected = null;
			}
			return num;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000D0EC0 File Offset: 0x000CF2C0
		public static implicit operator ObscuredLong(long value)
		{
			ObscuredLong result = new ObscuredLong(ObscuredLong.Encrypt(value));
			if (ObscuredLong.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000D0EED File Offset: 0x000CF2ED
		public static implicit operator long(ObscuredLong value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000D0EF8 File Offset: 0x000CF2F8
		public static ObscuredLong operator ++(ObscuredLong input)
		{
			long value = input.InternalDecrypt() + 1L;
			input.hiddenValue = ObscuredLong.Encrypt(value, input.currentCryptoKey);
			if (ObscuredLong.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000D0F38 File Offset: 0x000CF338
		public static ObscuredLong operator --(ObscuredLong input)
		{
			long value = input.InternalDecrypt() - 1L;
			input.hiddenValue = ObscuredLong.Encrypt(value, input.currentCryptoKey);
			if (ObscuredLong.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000D0F78 File Offset: 0x000CF378
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredLong))
			{
				return false;
			}
			ObscuredLong obscuredLong = (ObscuredLong)obj;
			return this.hiddenValue == obscuredLong.hiddenValue;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000D0FA8 File Offset: 0x000CF3A8
		public bool Equals(ObscuredLong obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000D0FBC File Offset: 0x000CF3BC
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000D0FE0 File Offset: 0x000CF3E0
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000D1004 File Offset: 0x000CF404
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000D1020 File Offset: 0x000CF420
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000D103C File Offset: 0x000CF43C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C37 RID: 7223
		public static Action onCheatingDetected;

		// Token: 0x04001C38 RID: 7224
		private static long cryptoKey = 444442L;

		// Token: 0x04001C39 RID: 7225
		private long currentCryptoKey;

		// Token: 0x04001C3A RID: 7226
		private long hiddenValue;

		// Token: 0x04001C3B RID: 7227
		private long fakeValue;

		// Token: 0x04001C3C RID: 7228
		private bool inited;
	}
}
