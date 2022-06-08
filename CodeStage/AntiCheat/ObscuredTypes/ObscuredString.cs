using System;
using System.Text;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000330 RID: 816
	public sealed class ObscuredString
	{
		// Token: 0x060019D7 RID: 6615 RVA: 0x000D22C9 File Offset: 0x000D06C9
		private ObscuredString(string value)
		{
			this.currentCryptoKey = ObscuredString.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = null;
			this.inited = true;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000D22F1 File Offset: 0x000D06F1
		public static void SetNewCryptoKey(string newKey)
		{
			ObscuredString.cryptoKey = newKey;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000D22F9 File Offset: 0x000D06F9
		public string GetEncrypted()
		{
			return this.hiddenValue;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000D2301 File Offset: 0x000D0701
		public void SetEncrypted(string encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredString.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x000D2320 File Offset: 0x000D0720
		public static string EncryptDecrypt(string value)
		{
			return ObscuredString.EncryptDecrypt(value, string.Empty);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x000D2330 File Offset: 0x000D0730
		public static string EncryptDecrypt(string value, string key)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(key))
			{
				key = ObscuredString.cryptoKey;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = key.Length;
			int length2 = value.Length;
			for (int i = 0; i < length2; i++)
			{
				stringBuilder.Append(value[i] ^ key[i % length]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000D23A8 File Offset: 0x000D07A8
		private string InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredString.cryptoKey;
				this.hiddenValue = ObscuredString.EncryptDecrypt(string.Empty);
				this.fakeValue = string.Empty;
				this.inited = true;
			}
			string key = ObscuredString.cryptoKey;
			if (this.currentCryptoKey != ObscuredString.cryptoKey)
			{
				key = this.currentCryptoKey;
			}
			string text = ObscuredString.EncryptDecrypt(this.hiddenValue, key);
			if (ObscuredString.onCheatingDetected != null && this.fakeValue != null && text != this.fakeValue)
			{
				ObscuredString.onCheatingDetected();
				ObscuredString.onCheatingDetected = null;
			}
			return text;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000D2454 File Offset: 0x000D0854
		public static implicit operator ObscuredString(string value)
		{
			if (value == null)
			{
				return null;
			}
			ObscuredString obscuredString = new ObscuredString(ObscuredString.EncryptDecrypt(value));
			if (ObscuredString.onCheatingDetected != null)
			{
				obscuredString.fakeValue = value;
			}
			return obscuredString;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000D2487 File Offset: 0x000D0887
		public static implicit operator string(ObscuredString value)
		{
			if (value == null)
			{
				return null;
			}
			return value.InternalDecrypt();
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000D249D File Offset: 0x000D089D
		public override string ToString()
		{
			return this.InternalDecrypt();
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000D24A8 File Offset: 0x000D08A8
		public static bool operator ==(ObscuredString a, ObscuredString b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			string a2 = a.hiddenValue;
			string b2 = b.hiddenValue;
			return string.Equals(a2, b2);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000D24E6 File Offset: 0x000D08E6
		public static bool operator !=(ObscuredString a, ObscuredString b)
		{
			return !(a == b);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000D24F4 File Offset: 0x000D08F4
		public override bool Equals(object obj)
		{
			ObscuredString obscuredString = obj as ObscuredString;
			string b = null;
			if (obscuredString != null)
			{
				b = obscuredString.hiddenValue;
			}
			return string.Equals(this.hiddenValue, b);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000D252C File Offset: 0x000D092C
		public bool Equals(ObscuredString value)
		{
			string b = null;
			if (value != null)
			{
				b = value.hiddenValue;
			}
			return string.Equals(this.hiddenValue, b);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000D255C File Offset: 0x000D095C
		public bool Equals(ObscuredString value, StringComparison comparisonType)
		{
			string b = null;
			if (value != null)
			{
				b = value.InternalDecrypt();
			}
			return string.Equals(this.InternalDecrypt(), b, comparisonType);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000D258B File Offset: 0x000D098B
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x04001C5D RID: 7261
		public static Action onCheatingDetected;

		// Token: 0x04001C5E RID: 7262
		private static string cryptoKey = "4441";

		// Token: 0x04001C5F RID: 7263
		private string currentCryptoKey;

		// Token: 0x04001C60 RID: 7264
		private string hiddenValue;

		// Token: 0x04001C61 RID: 7265
		private string fakeValue;

		// Token: 0x04001C62 RID: 7266
		private bool inited;
	}
}
