using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000320 RID: 800
	public struct ObscuredBool : IEquatable<ObscuredBool>
	{
		// Token: 0x060018D3 RID: 6355 RVA: 0x000CEF60 File Offset: 0x000CD360
		private ObscuredBool(int value)
		{
			this.currentCryptoKey = ObscuredBool.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = null;
			this.inited = true;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000CEF95 File Offset: 0x000CD395
		public static void SetNewCryptoKey(byte newKey)
		{
			ObscuredBool.cryptoKey = newKey;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000CEFA0 File Offset: 0x000CD3A0
		public int GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredBool.cryptoKey)
			{
				bool value = this.InternalDecrypt();
				this.hiddenValue = ObscuredBool.Encrypt(value, ObscuredBool.cryptoKey);
				this.currentCryptoKey = ObscuredBool.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000CEFE6 File Offset: 0x000CD3E6
		public void SetEncrypted(int encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredBool.onCheatingDetected != null)
			{
				this.fakeValue = new bool?(this.InternalDecrypt());
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000CF00A File Offset: 0x000CD40A
		public static int Encrypt(bool value)
		{
			return ObscuredBool.Encrypt(value, 0);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000CF014 File Offset: 0x000CD414
		public static int Encrypt(bool value, byte key)
		{
			if (key == 0)
			{
				key = ObscuredBool.cryptoKey;
			}
			int num = (!value) ? 181 : 213;
			return num ^ (int)key;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000CF049 File Offset: 0x000CD449
		public static bool Decrypt(int value)
		{
			return ObscuredBool.Decrypt(value, 0);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000CF052 File Offset: 0x000CD452
		public static bool Decrypt(int value, byte key)
		{
			if (key == 0)
			{
				key = ObscuredBool.cryptoKey;
			}
			value ^= (int)key;
			return value != 181;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000CF074 File Offset: 0x000CD474
		private bool InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredBool.cryptoKey;
				this.hiddenValue = ObscuredBool.Encrypt(false);
				this.fakeValue = new bool?(false);
				this.inited = true;
			}
			byte b = ObscuredBool.cryptoKey;
			if (this.currentCryptoKey != ObscuredBool.cryptoKey)
			{
				b = this.currentCryptoKey;
			}
			int num = this.hiddenValue;
			num ^= (int)b;
			bool flag = num != 181;
			if (ObscuredBool.onCheatingDetected != null)
			{
				bool? flag2 = this.fakeValue;
				if (flag2 != null && flag != this.fakeValue)
				{
					ObscuredBool.onCheatingDetected();
					ObscuredBool.onCheatingDetected = null;
				}
			}
			return flag;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000CF13C File Offset: 0x000CD53C
		public static implicit operator ObscuredBool(bool value)
		{
			ObscuredBool result = new ObscuredBool(ObscuredBool.Encrypt(value));
			if (ObscuredBool.onCheatingDetected != null)
			{
				result.fakeValue = new bool?(value);
			}
			return result;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000CF16E File Offset: 0x000CD56E
		public static implicit operator bool(ObscuredBool value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000CF178 File Offset: 0x000CD578
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredBool))
			{
				return false;
			}
			ObscuredBool obscuredBool = (ObscuredBool)obj;
			return this.hiddenValue == obscuredBool.hiddenValue;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000CF1A8 File Offset: 0x000CD5A8
		public bool Equals(ObscuredBool obj)
		{
			return this.hiddenValue == obj.hiddenValue;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000CF1BC File Offset: 0x000CD5BC
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000CF1E0 File Offset: 0x000CD5E0
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x04001BEA RID: 7146
		public static Action onCheatingDetected;

		// Token: 0x04001BEB RID: 7147
		private static byte cryptoKey = 215;

		// Token: 0x04001BEC RID: 7148
		private byte currentCryptoKey;

		// Token: 0x04001BED RID: 7149
		private int hiddenValue;

		// Token: 0x04001BEE RID: 7150
		private bool? fakeValue;

		// Token: 0x04001BEF RID: 7151
		private bool inited;
	}
}
