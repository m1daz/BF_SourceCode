using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000321 RID: 801
	public struct ObscuredByte : IEquatable<ObscuredByte>
	{
		// Token: 0x060018E3 RID: 6371 RVA: 0x000CF20D File Offset: 0x000CD60D
		private ObscuredByte(byte value)
		{
			this.currentCryptoKey = ObscuredByte.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000CF22F File Offset: 0x000CD62F
		public static void SetNewCryptoKey(byte newKey)
		{
			ObscuredByte.cryptoKey = newKey;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000CF238 File Offset: 0x000CD638
		public byte GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredByte.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredByte.EncryptDecrypt(this.hiddenValue, ObscuredByte.cryptoKey);
				this.currentCryptoKey = ObscuredByte.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x000CF288 File Offset: 0x000CD688
		public void SetEncrypted(byte encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredByte.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000CF2A7 File Offset: 0x000CD6A7
		public static byte EncryptDecrypt(byte value)
		{
			return ObscuredByte.EncryptDecrypt(value, 0);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000CF2B0 File Offset: 0x000CD6B0
		public static byte EncryptDecrypt(byte value, byte key)
		{
			if (key == 0)
			{
				return value ^ ObscuredByte.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000CF2C8 File Offset: 0x000CD6C8
		private byte InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredByte.cryptoKey;
				this.hiddenValue = ObscuredByte.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			byte key = ObscuredByte.cryptoKey;
			if (this.currentCryptoKey != ObscuredByte.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			byte b = ObscuredByte.EncryptDecrypt(this.hiddenValue, key);
			if (ObscuredByte.onCheatingDetected != null && this.fakeValue != 0 && b != this.fakeValue)
			{
				ObscuredByte.onCheatingDetected();
				ObscuredByte.onCheatingDetected = null;
			}
			return b;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000CF364 File Offset: 0x000CD764
		public static implicit operator ObscuredByte(byte value)
		{
			ObscuredByte result = new ObscuredByte(ObscuredByte.EncryptDecrypt(value));
			if (ObscuredByte.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000CF391 File Offset: 0x000CD791
		public static implicit operator byte(ObscuredByte value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000CF39C File Offset: 0x000CD79C
		public static ObscuredByte operator ++(ObscuredByte input)
		{
			byte value = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredByte.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredByte.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000CF3DC File Offset: 0x000CD7DC
		public static ObscuredByte operator --(ObscuredByte input)
		{
			byte value = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredByte.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredByte.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000CF41C File Offset: 0x000CD81C
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredByte))
			{
				return false;
			}
			ObscuredByte obscuredByte = (ObscuredByte)obj;
			return this.hiddenValue == obscuredByte.hiddenValue;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000CF44C File Offset: 0x000CD84C
		public bool Equals(ObscuredByte obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000CF460 File Offset: 0x000CD860
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000CF484 File Offset: 0x000CD884
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000CF4A0 File Offset: 0x000CD8A0
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000CF4C4 File Offset: 0x000CD8C4
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000CF4E0 File Offset: 0x000CD8E0
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001BF0 RID: 7152
		public static Action onCheatingDetected;

		// Token: 0x04001BF1 RID: 7153
		private static byte cryptoKey = 244;

		// Token: 0x04001BF2 RID: 7154
		private byte currentCryptoKey;

		// Token: 0x04001BF3 RID: 7155
		private byte hiddenValue;

		// Token: 0x04001BF4 RID: 7156
		private byte fakeValue;

		// Token: 0x04001BF5 RID: 7157
		private bool inited;
	}
}
