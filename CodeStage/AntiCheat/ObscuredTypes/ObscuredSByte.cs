using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200032E RID: 814
	public struct ObscuredSByte : IEquatable<ObscuredSByte>
	{
		// Token: 0x060019B1 RID: 6577 RVA: 0x000D1CD8 File Offset: 0x000D00D8
		private ObscuredSByte(sbyte value)
		{
			this.currentCryptoKey = ObscuredSByte.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000D1CFA File Offset: 0x000D00FA
		public static void SetNewCryptoKey(sbyte newKey)
		{
			ObscuredSByte.cryptoKey = newKey;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000D1D04 File Offset: 0x000D0104
		public sbyte GetEncrypted()
		{
			if ((int)this.currentCryptoKey != (int)ObscuredSByte.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredSByte.EncryptDecrypt(this.hiddenValue, ObscuredSByte.cryptoKey);
				this.currentCryptoKey = ObscuredSByte.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000D1D56 File Offset: 0x000D0156
		public void SetEncrypted(sbyte encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredSByte.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000D1D75 File Offset: 0x000D0175
		public static sbyte EncryptDecrypt(sbyte value)
		{
			return ObscuredSByte.EncryptDecrypt(value, 0);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000D1D7E File Offset: 0x000D017E
		public static sbyte EncryptDecrypt(sbyte value, sbyte key)
		{
			if ((int)key == 0)
			{
				return (sbyte)((int)value ^ (int)ObscuredSByte.cryptoKey);
			}
			return (sbyte)((int)value ^ (int)key);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000D1D98 File Offset: 0x000D0198
		private sbyte InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredSByte.cryptoKey;
				this.hiddenValue = ObscuredSByte.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			sbyte key = ObscuredSByte.cryptoKey;
			if ((int)this.currentCryptoKey != (int)ObscuredSByte.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			sbyte b = ObscuredSByte.EncryptDecrypt(this.hiddenValue, key);
			if (ObscuredSByte.onCheatingDetected != null && (int)this.fakeValue != 0 && (int)b != (int)this.fakeValue)
			{
				ObscuredSByte.onCheatingDetected();
				ObscuredSByte.onCheatingDetected = null;
			}
			return b;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000D1E38 File Offset: 0x000D0238
		public static implicit operator ObscuredSByte(sbyte value)
		{
			ObscuredSByte result = new ObscuredSByte(ObscuredSByte.EncryptDecrypt(value));
			if (ObscuredSByte.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000D1E65 File Offset: 0x000D0265
		public static implicit operator sbyte(ObscuredSByte value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000D1E70 File Offset: 0x000D0270
		public static ObscuredSByte operator ++(ObscuredSByte input)
		{
			sbyte value = (sbyte)((int)input.InternalDecrypt() + 1);
			input.hiddenValue = ObscuredSByte.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredSByte.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000D1EB0 File Offset: 0x000D02B0
		public static ObscuredSByte operator --(ObscuredSByte input)
		{
			sbyte value = (sbyte)((int)input.InternalDecrypt() - 1);
			input.hiddenValue = ObscuredSByte.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredSByte.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000D1EF0 File Offset: 0x000D02F0
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredSByte))
			{
				return false;
			}
			ObscuredSByte obscuredSByte = (ObscuredSByte)obj;
			return (int)this.hiddenValue == (int)obscuredSByte.hiddenValue;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000D1F22 File Offset: 0x000D0322
		public bool Equals(ObscuredSByte obj)
		{
			return (int)this.hiddenValue == (int)obj.hiddenValue;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000D1F38 File Offset: 0x000D0338
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000D1F5C File Offset: 0x000D035C
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000D1F78 File Offset: 0x000D0378
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000D1F9C File Offset: 0x000D039C
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000D1FB8 File Offset: 0x000D03B8
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C51 RID: 7249
		public static Action onCheatingDetected;

		// Token: 0x04001C52 RID: 7250
		private static sbyte cryptoKey = 112;

		// Token: 0x04001C53 RID: 7251
		private sbyte currentCryptoKey;

		// Token: 0x04001C54 RID: 7252
		private sbyte hiddenValue;

		// Token: 0x04001C55 RID: 7253
		private sbyte fakeValue;

		// Token: 0x04001C56 RID: 7254
		private bool inited;
	}
}
