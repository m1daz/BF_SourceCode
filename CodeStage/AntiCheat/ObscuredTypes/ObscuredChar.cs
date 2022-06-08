using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000322 RID: 802
	public struct ObscuredChar : IEquatable<ObscuredChar>
	{
		// Token: 0x060018F6 RID: 6390 RVA: 0x000CF509 File Offset: 0x000CD909
		private ObscuredChar(char value)
		{
			this.currentCryptoKey = ObscuredChar.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = '\0';
			this.inited = true;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000CF52B File Offset: 0x000CD92B
		public static void SetNewCryptoKey(char newKey)
		{
			ObscuredChar.cryptoKey = newKey;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000CF534 File Offset: 0x000CD934
		public char GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredChar.cryptoKey)
			{
				this.hiddenValue = this.InternalDecrypt();
				this.hiddenValue = ObscuredChar.EncryptDecrypt(this.hiddenValue, ObscuredChar.cryptoKey);
				this.currentCryptoKey = ObscuredChar.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000CF584 File Offset: 0x000CD984
		public void SetEncrypted(char encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredChar.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000CF5A3 File Offset: 0x000CD9A3
		public static char EncryptDecrypt(char value)
		{
			return ObscuredChar.EncryptDecrypt(value, '\0');
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000CF5AC File Offset: 0x000CD9AC
		public static char EncryptDecrypt(char value, char key)
		{
			if (key == '\0')
			{
				return value ^ ObscuredChar.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000CF5C4 File Offset: 0x000CD9C4
		private char InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredChar.cryptoKey;
				this.hiddenValue = ObscuredChar.EncryptDecrypt('\0');
				this.fakeValue = '\0';
				this.inited = true;
			}
			char key = ObscuredChar.cryptoKey;
			if (this.currentCryptoKey != ObscuredChar.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			char c = ObscuredChar.EncryptDecrypt(this.hiddenValue, key);
			if (ObscuredChar.onCheatingDetected != null && this.fakeValue != '\0' && c != this.fakeValue)
			{
				ObscuredChar.onCheatingDetected();
				ObscuredChar.onCheatingDetected = null;
			}
			return c;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000CF660 File Offset: 0x000CDA60
		public static implicit operator ObscuredChar(char value)
		{
			ObscuredChar result = new ObscuredChar(ObscuredChar.EncryptDecrypt(value));
			if (ObscuredChar.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000CF68D File Offset: 0x000CDA8D
		public static implicit operator char(ObscuredChar value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000CF698 File Offset: 0x000CDA98
		public static ObscuredChar operator ++(ObscuredChar input)
		{
			char value = input.InternalDecrypt() + '\u0001';
			input.hiddenValue = ObscuredChar.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredChar.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000CF6D8 File Offset: 0x000CDAD8
		public static ObscuredChar operator --(ObscuredChar input)
		{
			char value = input.InternalDecrypt() - '\u0001';
			input.hiddenValue = ObscuredChar.EncryptDecrypt(value, input.currentCryptoKey);
			if (ObscuredChar.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000CF718 File Offset: 0x000CDB18
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredChar))
			{
				return false;
			}
			ObscuredChar obscuredChar = (ObscuredChar)obj;
			return this.hiddenValue == obscuredChar.hiddenValue;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000CF748 File Offset: 0x000CDB48
		public bool Equals(ObscuredChar obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000CF75C File Offset: 0x000CDB5C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000CF780 File Offset: 0x000CDB80
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x04001BF6 RID: 7158
		public static Action onCheatingDetected;

		// Token: 0x04001BF7 RID: 7159
		private static char cryptoKey = '—';

		// Token: 0x04001BF8 RID: 7160
		private char currentCryptoKey;

		// Token: 0x04001BF9 RID: 7161
		private char hiddenValue;

		// Token: 0x04001BFA RID: 7162
		private char fakeValue;

		// Token: 0x04001BFB RID: 7163
		private bool inited;
	}
}
