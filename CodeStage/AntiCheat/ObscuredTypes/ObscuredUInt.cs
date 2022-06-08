using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000331 RID: 817
	public struct ObscuredUInt : IEquatable<ObscuredUInt>
	{
		// Token: 0x060019E8 RID: 6632 RVA: 0x000D25A4 File Offset: 0x000D09A4
		private ObscuredUInt(uint value)
		{
			this.currentCryptoKey = ObscuredUInt.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0U;
			this.inited = true;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000D25C6 File Offset: 0x000D09C6
		public static void SetNewCryptoKey(uint newKey)
		{
			ObscuredUInt.cryptoKey = newKey;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000D25D0 File Offset: 0x000D09D0
		public uint GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredUInt.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredUInt.Encrypt(this.hiddenValue, ObscuredUInt.cryptoKey);
				this.currentCryptoKey = ObscuredUInt.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000D2620 File Offset: 0x000D0A20
		public void SetEncrypted(uint encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredUInt.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000D263F File Offset: 0x000D0A3F
		public static uint Encrypt(uint value)
		{
			return ObscuredUInt.Encrypt(value, 0U);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000D2648 File Offset: 0x000D0A48
		public static uint Decrypt(uint value)
		{
			return ObscuredUInt.Decrypt(value, 0U);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000D2651 File Offset: 0x000D0A51
		public static uint Encrypt(uint value, uint key)
		{
			if (key == 0U)
			{
				return value ^ ObscuredUInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000D2664 File Offset: 0x000D0A64
		public static uint Decrypt(uint value, uint key)
		{
			if (key == 0U)
			{
				return value ^ ObscuredUInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000D2678 File Offset: 0x000D0A78
		private uint InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredUInt.cryptoKey;
				this.hiddenValue = ObscuredUInt.Encrypt(0U);
				this.fakeValue = 0U;
				this.inited = true;
			}
			uint key = ObscuredUInt.cryptoKey;
			if (this.currentCryptoKey != ObscuredUInt.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			uint num = ObscuredUInt.Decrypt(this.hiddenValue, key);
			if (ObscuredUInt.onCheatingDetected != null && this.fakeValue != 0U && num != this.fakeValue)
			{
				ObscuredUInt.onCheatingDetected();
				ObscuredUInt.onCheatingDetected = null;
			}
			return num;
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000D2714 File Offset: 0x000D0B14
		public static implicit operator ObscuredUInt(uint value)
		{
			ObscuredUInt result = new ObscuredUInt(ObscuredUInt.Encrypt(value));
			if (ObscuredUInt.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000D2741 File Offset: 0x000D0B41
		public static implicit operator uint(ObscuredUInt value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000D274C File Offset: 0x000D0B4C
		public static ObscuredUInt operator ++(ObscuredUInt input)
		{
			uint value = input.InternalDecrypt() + 1U;
			input.hiddenValue = ObscuredUInt.Encrypt(value, input.currentCryptoKey);
			if (ObscuredUInt.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000D278C File Offset: 0x000D0B8C
		public static ObscuredUInt operator --(ObscuredUInt input)
		{
			uint value = input.InternalDecrypt() - 1U;
			input.hiddenValue = ObscuredUInt.Encrypt(value, input.currentCryptoKey);
			if (ObscuredUInt.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000D27CC File Offset: 0x000D0BCC
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredUInt))
			{
				return false;
			}
			ObscuredUInt obscuredUInt = (ObscuredUInt)obj;
			return this.hiddenValue == obscuredUInt.hiddenValue;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000D27FC File Offset: 0x000D0BFC
		public bool Equals(ObscuredUInt obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000D2810 File Offset: 0x000D0C10
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000D2834 File Offset: 0x000D0C34
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000D2850 File Offset: 0x000D0C50
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000D2874 File Offset: 0x000D0C74
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000D2890 File Offset: 0x000D0C90
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C63 RID: 7267
		public static Action onCheatingDetected;

		// Token: 0x04001C64 RID: 7268
		private static uint cryptoKey = 240513U;

		// Token: 0x04001C65 RID: 7269
		private uint currentCryptoKey;

		// Token: 0x04001C66 RID: 7270
		private uint hiddenValue;

		// Token: 0x04001C67 RID: 7271
		private uint fakeValue;

		// Token: 0x04001C68 RID: 7272
		private bool inited;
	}
}
