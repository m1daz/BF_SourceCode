using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000329 RID: 809
	public struct ObscuredInt : IEquatable<ObscuredInt>
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x000D0A31 File Offset: 0x000CEE31
		private ObscuredInt(int value)
		{
			this.currentCryptoKey = ObscuredInt.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000D0A53 File Offset: 0x000CEE53
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredInt.cryptoKey = newKey;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000D0A5C File Offset: 0x000CEE5C
		public int GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredInt.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredInt.Encrypt(this.hiddenValue, ObscuredInt.cryptoKey);
				this.currentCryptoKey = ObscuredInt.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000D0AAC File Offset: 0x000CEEAC
		public void SetEncrypted(int encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredInt.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000D0ACB File Offset: 0x000CEECB
		public static int Encrypt(int value)
		{
			return ObscuredInt.Encrypt(value, 0);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000D0AD4 File Offset: 0x000CEED4
		public static int Decrypt(int value)
		{
			return ObscuredInt.Decrypt(value, 0);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000D0ADD File Offset: 0x000CEEDD
		public static int Encrypt(int value, int key)
		{
			if (key == 0)
			{
				return value ^ ObscuredInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000D0AF0 File Offset: 0x000CEEF0
		public static int Decrypt(int value, int key)
		{
			if (key == 0)
			{
				return value ^ ObscuredInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x000D0B04 File Offset: 0x000CEF04
		private int InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredInt.cryptoKey;
				this.hiddenValue = ObscuredInt.Encrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			int key = ObscuredInt.cryptoKey;
			if (this.currentCryptoKey != ObscuredInt.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			int num = ObscuredInt.Decrypt(this.hiddenValue, key);
			if (ObscuredInt.onCheatingDetected != null && this.fakeValue != 0 && num != this.fakeValue)
			{
				ObscuredInt.onCheatingDetected();
				ObscuredInt.onCheatingDetected = null;
			}
			return num;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000D0BA0 File Offset: 0x000CEFA0
		public static implicit operator ObscuredInt(int value)
		{
			ObscuredInt result = new ObscuredInt(ObscuredInt.Encrypt(value));
			if (ObscuredInt.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000D0BCD File Offset: 0x000CEFCD
		public static implicit operator int(ObscuredInt value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000D0BD8 File Offset: 0x000CEFD8
		public static ObscuredInt operator ++(ObscuredInt input)
		{
			int value = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredInt.Encrypt(value, input.currentCryptoKey);
			if (ObscuredInt.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000D0C18 File Offset: 0x000CF018
		public static ObscuredInt operator --(ObscuredInt input)
		{
			int value = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredInt.Encrypt(value, input.currentCryptoKey);
			if (ObscuredInt.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000D0C58 File Offset: 0x000CF058
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredInt))
			{
				return false;
			}
			ObscuredInt obscuredInt = (ObscuredInt)obj;
			return this.hiddenValue == obscuredInt.hiddenValue;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000D0C88 File Offset: 0x000CF088
		public bool Equals(ObscuredInt obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000D0C9C File Offset: 0x000CF09C
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000D0CC0 File Offset: 0x000CF0C0
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000D0CE4 File Offset: 0x000CF0E4
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000D0D00 File Offset: 0x000CF100
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000D0D1C File Offset: 0x000CF11C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C31 RID: 7217
		public static Action onCheatingDetected;

		// Token: 0x04001C32 RID: 7218
		private static int cryptoKey = 444444;

		// Token: 0x04001C33 RID: 7219
		private int currentCryptoKey;

		// Token: 0x04001C34 RID: 7220
		private int hiddenValue;

		// Token: 0x04001C35 RID: 7221
		public int fakeValue;

		// Token: 0x04001C36 RID: 7222
		private bool inited;
	}
}
