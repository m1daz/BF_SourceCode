using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000332 RID: 818
	public struct ObscuredULong : IEquatable<ObscuredULong>
	{
		// Token: 0x060019FD RID: 6653 RVA: 0x000D28B9 File Offset: 0x000D0CB9
		private ObscuredULong(ulong value)
		{
			this.currentCryptoKey = ObscuredULong.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0UL;
			this.inited = true;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000D28DC File Offset: 0x000D0CDC
		public static void SetNewCryptoKey(ulong newKey)
		{
			ObscuredULong.cryptoKey = newKey;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000D28E4 File Offset: 0x000D0CE4
		public ulong GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredULong.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredULong.Encrypt(this.hiddenValue, ObscuredULong.cryptoKey);
				this.currentCryptoKey = ObscuredULong.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000D2934 File Offset: 0x000D0D34
		public void SetEncrypted(ulong encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredULong.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000D2953 File Offset: 0x000D0D53
		public static ulong Encrypt(ulong value)
		{
			return ObscuredULong.Encrypt(value, 0UL);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000D295D File Offset: 0x000D0D5D
		public static ulong Decrypt(ulong value)
		{
			return ObscuredULong.Decrypt(value, 0UL);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000D2967 File Offset: 0x000D0D67
		public static ulong Encrypt(ulong value, ulong key)
		{
			if (key == 0UL)
			{
				return value ^ ObscuredULong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000D297C File Offset: 0x000D0D7C
		public static ulong Decrypt(ulong value, ulong key)
		{
			if (key == 0UL)
			{
				return value ^ ObscuredULong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000D2994 File Offset: 0x000D0D94
		private ulong InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredULong.cryptoKey;
				this.hiddenValue = ObscuredULong.Encrypt(0UL);
				this.fakeValue = 0UL;
				this.inited = true;
			}
			ulong key = ObscuredULong.cryptoKey;
			if (this.currentCryptoKey != ObscuredULong.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			ulong num = ObscuredULong.Decrypt(this.hiddenValue, key);
			if (ObscuredULong.onCheatingDetected != null && this.fakeValue != 0UL && num != this.fakeValue)
			{
				ObscuredULong.onCheatingDetected();
				ObscuredULong.onCheatingDetected = null;
			}
			return num;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000D2A34 File Offset: 0x000D0E34
		public static implicit operator ObscuredULong(ulong value)
		{
			ObscuredULong result = new ObscuredULong(ObscuredULong.Encrypt(value));
			if (ObscuredULong.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000D2A61 File Offset: 0x000D0E61
		public static implicit operator ulong(ObscuredULong value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000D2A6C File Offset: 0x000D0E6C
		public static ObscuredULong operator ++(ObscuredULong input)
		{
			ulong value = input.InternalDecrypt() + 1UL;
			input.hiddenValue = ObscuredULong.Encrypt(value, input.currentCryptoKey);
			if (ObscuredULong.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000D2AAC File Offset: 0x000D0EAC
		public static ObscuredULong operator --(ObscuredULong input)
		{
			ulong value = input.InternalDecrypt() - 1UL;
			input.hiddenValue = ObscuredULong.Encrypt(value, input.currentCryptoKey);
			if (ObscuredULong.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000D2AEC File Offset: 0x000D0EEC
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredULong))
			{
				return false;
			}
			ObscuredULong obscuredULong = (ObscuredULong)obj;
			return this.hiddenValue == obscuredULong.hiddenValue;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000D2B1C File Offset: 0x000D0F1C
		public bool Equals(ObscuredULong obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000D2B30 File Offset: 0x000D0F30
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000D2B54 File Offset: 0x000D0F54
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000D2B78 File Offset: 0x000D0F78
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000D2B94 File Offset: 0x000D0F94
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000D2BB0 File Offset: 0x000D0FB0
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C69 RID: 7273
		public static Action onCheatingDetected;

		// Token: 0x04001C6A RID: 7274
		private static ulong cryptoKey = 444443UL;

		// Token: 0x04001C6B RID: 7275
		private ulong currentCryptoKey;

		// Token: 0x04001C6C RID: 7276
		private ulong hiddenValue;

		// Token: 0x04001C6D RID: 7277
		private ulong fakeValue;

		// Token: 0x04001C6E RID: 7278
		private bool inited;
	}
}
