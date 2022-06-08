using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000333 RID: 819
	public struct ObscuredUShort : IEquatable<ObscuredUShort>
	{
		// Token: 0x06001A12 RID: 6674 RVA: 0x000D2BDA File Offset: 0x000D0FDA
		private ObscuredUShort(ushort value)
		{
			this.currentCryptoKey = ObscuredUShort.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000D2BFC File Offset: 0x000D0FFC
		public static void SetNewCryptoKey(ushort newKey)
		{
			ObscuredUShort.cryptoKey = newKey;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000D2C04 File Offset: 0x000D1004
		public ushort GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredUShort.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredUShort.EncryptDecrypt(this.hiddenValue, ObscuredUShort.cryptoKey);
				this.currentCryptoKey = ObscuredUShort.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000D2C54 File Offset: 0x000D1054
		public void SetEncrypted(ushort encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredUShort.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000D2C73 File Offset: 0x000D1073
		public static ushort EncryptDecrypt(ushort value)
		{
			return ObscuredUShort.EncryptDecrypt(value, 0);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000D2C7C File Offset: 0x000D107C
		public static ushort EncryptDecrypt(ushort value, ushort key)
		{
			if (key == 0)
			{
				return value ^ ObscuredUShort.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000D2C94 File Offset: 0x000D1094
		private ushort InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredUShort.cryptoKey;
				this.hiddenValue = ObscuredUShort.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			ushort key = ObscuredUShort.cryptoKey;
			if (this.currentCryptoKey != ObscuredUShort.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			ushort num = ObscuredUShort.EncryptDecrypt(this.hiddenValue, key);
			if (ObscuredUShort.onCheatingDetected != null && this.fakeValue != 0 && num != this.fakeValue)
			{
				ObscuredUShort.onCheatingDetected();
				ObscuredUShort.onCheatingDetected = null;
			}
			return num;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000D2D30 File Offset: 0x000D1130
		public static implicit operator ObscuredUShort(ushort value)
		{
			ObscuredUShort result = new ObscuredUShort(ObscuredUShort.EncryptDecrypt(value));
			if (ObscuredUShort.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000D2D5D File Offset: 0x000D115D
		public static implicit operator ushort(ObscuredUShort value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000D2D68 File Offset: 0x000D1168
		public static ObscuredUShort operator ++(ObscuredUShort input)
		{
			ushort value = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredUShort.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredUShort.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000D2DA8 File Offset: 0x000D11A8
		public static ObscuredUShort operator --(ObscuredUShort input)
		{
			ushort value = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredUShort.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredUShort.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000D2DE8 File Offset: 0x000D11E8
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredUShort))
			{
				return false;
			}
			ObscuredUShort obscuredUShort = (ObscuredUShort)obj;
			return this.hiddenValue == obscuredUShort.hiddenValue;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000D2E18 File Offset: 0x000D1218
		public bool Equals(ObscuredUShort obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000D2E2C File Offset: 0x000D122C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000D2E50 File Offset: 0x000D1250
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000D2E6C File Offset: 0x000D126C
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000D2E90 File Offset: 0x000D1290
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000D2EAC File Offset: 0x000D12AC
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C6F RID: 7279
		public static Action onCheatingDetected;

		// Token: 0x04001C70 RID: 7280
		private static ushort cryptoKey = 224;

		// Token: 0x04001C71 RID: 7281
		private ushort currentCryptoKey;

		// Token: 0x04001C72 RID: 7282
		private ushort hiddenValue;

		// Token: 0x04001C73 RID: 7283
		private ushort fakeValue;

		// Token: 0x04001C74 RID: 7284
		private bool inited;
	}
}
