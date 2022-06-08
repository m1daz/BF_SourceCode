using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200032F RID: 815
	public struct ObscuredShort : IEquatable<ObscuredShort>
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x000D1FDE File Offset: 0x000D03DE
		private ObscuredShort(short value)
		{
			this.currentCryptoKey = ObscuredShort.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000D2000 File Offset: 0x000D0400
		public static void SetNewCryptoKey(short newKey)
		{
			ObscuredShort.cryptoKey = newKey;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000D2008 File Offset: 0x000D0408
		public short GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredShort.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredShort.EncryptDecrypt(this.hiddenValue, ObscuredShort.cryptoKey);
				this.currentCryptoKey = ObscuredShort.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000D2058 File Offset: 0x000D0458
		public void SetEncrypted(short encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredShort.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000D2077 File Offset: 0x000D0477
		public static short EncryptDecrypt(short value)
		{
			return ObscuredShort.EncryptDecrypt(value, 0);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000D2080 File Offset: 0x000D0480
		public static short EncryptDecrypt(short value, short key)
		{
			if (key == 0)
			{
				return value ^ ObscuredShort.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000D2098 File Offset: 0x000D0498
		private short InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredShort.cryptoKey;
				this.hiddenValue = ObscuredShort.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			short key = ObscuredShort.cryptoKey;
			if (this.currentCryptoKey != ObscuredShort.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			short num = ObscuredShort.EncryptDecrypt(this.hiddenValue, key);
			if (ObscuredShort.onCheatingDetected != null && this.fakeValue != 0 && num != this.fakeValue)
			{
				ObscuredShort.onCheatingDetected();
				ObscuredShort.onCheatingDetected = null;
			}
			return num;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000D2134 File Offset: 0x000D0534
		public static implicit operator ObscuredShort(short value)
		{
			ObscuredShort result = new ObscuredShort(ObscuredShort.EncryptDecrypt(value));
			if (ObscuredShort.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000D2161 File Offset: 0x000D0561
		public static implicit operator short(ObscuredShort value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000D216C File Offset: 0x000D056C
		public static ObscuredShort operator ++(ObscuredShort input)
		{
			short value = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredShort.EncryptDecrypt(value);
			if (ObscuredShort.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000D21A4 File Offset: 0x000D05A4
		public static ObscuredShort operator --(ObscuredShort input)
		{
			short value = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredShort.EncryptDecrypt(value);
			if (ObscuredShort.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000D21DC File Offset: 0x000D05DC
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredShort))
			{
				return false;
			}
			ObscuredShort obscuredShort = (ObscuredShort)obj;
			return this.hiddenValue == obscuredShort.hiddenValue;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000D220C File Offset: 0x000D060C
		public bool Equals(ObscuredShort obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000D2220 File Offset: 0x000D0620
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000D2244 File Offset: 0x000D0644
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000D2260 File Offset: 0x000D0660
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000D2284 File Offset: 0x000D0684
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000D22A0 File Offset: 0x000D06A0
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C57 RID: 7255
		public static Action onCheatingDetected;

		// Token: 0x04001C58 RID: 7256
		private static short cryptoKey = 214;

		// Token: 0x04001C59 RID: 7257
		private short currentCryptoKey;

		// Token: 0x04001C5A RID: 7258
		private short hiddenValue;

		// Token: 0x04001C5B RID: 7259
		private short fakeValue;

		// Token: 0x04001C5C RID: 7260
		private bool inited;
	}
}
