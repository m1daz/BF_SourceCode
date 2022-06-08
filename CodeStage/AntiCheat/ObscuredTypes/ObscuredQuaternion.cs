using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200032D RID: 813
	public struct ObscuredQuaternion
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x000D1971 File Offset: 0x000CFD71
		private ObscuredQuaternion(Quaternion value)
		{
			this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = Quaternion.identity;
			this.inited = true;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000D1997 File Offset: 0x000CFD97
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredQuaternion.cryptoKey = newKey;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000D19A0 File Offset: 0x000CFDA0
		public Quaternion GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredQuaternion.cryptoKey)
			{
				Quaternion value = this.InternalDecrypt();
				this.hiddenValue = ObscuredQuaternion.Encrypt(value, ObscuredQuaternion.cryptoKey);
				this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000D19E6 File Offset: 0x000CFDE6
		public void SetEncrypted(Quaternion encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredQuaternion.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000D1A05 File Offset: 0x000CFE05
		public static Quaternion Encrypt(Quaternion value)
		{
			return ObscuredQuaternion.Encrypt(value, 0);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000D1A10 File Offset: 0x000CFE10
		public static Quaternion Encrypt(Quaternion value, int key)
		{
			if (key == 0)
			{
				key = ObscuredQuaternion.cryptoKey;
			}
			value.x = (float)ObscuredDouble.Encrypt((double)value.x, (long)key);
			value.y = (float)ObscuredDouble.Encrypt((double)value.y, (long)key);
			value.z = (float)ObscuredDouble.Encrypt((double)value.z, (long)key);
			value.w = (float)ObscuredDouble.Encrypt((double)value.w, (long)key);
			return value;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000D1A87 File Offset: 0x000CFE87
		public static Quaternion Decrypt(Quaternion value)
		{
			return ObscuredQuaternion.Decrypt(value, 0);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000D1A90 File Offset: 0x000CFE90
		public static Quaternion Decrypt(Quaternion value, int key)
		{
			if (key == 0)
			{
				key = ObscuredQuaternion.cryptoKey;
			}
			value.x = (float)ObscuredDouble.Decrypt((long)value.x, (long)key);
			value.y = (float)ObscuredDouble.Decrypt((long)value.y, (long)key);
			value.z = (float)ObscuredDouble.Decrypt((long)value.z, (long)key);
			value.w = (float)ObscuredDouble.Decrypt((long)value.w, (long)key);
			return value;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000D1B08 File Offset: 0x000CFF08
		private Quaternion InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
				this.hiddenValue = ObscuredQuaternion.Encrypt(Quaternion.identity);
				this.fakeValue = Quaternion.identity;
				this.inited = true;
			}
			int num = ObscuredQuaternion.cryptoKey;
			if (this.currentCryptoKey != ObscuredQuaternion.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			Quaternion result;
			result.x = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.x, (long)num);
			result.y = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.y, (long)num);
			result.z = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.z, (long)num);
			result.w = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.w, (long)num);
			if (ObscuredQuaternion.onCheatingDetected != null && !this.fakeValue.Equals(Quaternion.identity) && !result.Equals(this.fakeValue))
			{
				ObscuredQuaternion.onCheatingDetected();
				ObscuredQuaternion.onCheatingDetected = null;
			}
			return result;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000D1C30 File Offset: 0x000D0030
		public static implicit operator ObscuredQuaternion(Quaternion value)
		{
			ObscuredQuaternion result = new ObscuredQuaternion(ObscuredQuaternion.Encrypt(value));
			if (ObscuredQuaternion.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000D1C5D File Offset: 0x000D005D
		public static implicit operator Quaternion(ObscuredQuaternion value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000D1C68 File Offset: 0x000D0068
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000D1C8C File Offset: 0x000D008C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000D1CB0 File Offset: 0x000D00B0
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x04001C4B RID: 7243
		public static Action onCheatingDetected;

		// Token: 0x04001C4C RID: 7244
		private static int cryptoKey = 120205;

		// Token: 0x04001C4D RID: 7245
		private int currentCryptoKey;

		// Token: 0x04001C4E RID: 7246
		private Quaternion hiddenValue;

		// Token: 0x04001C4F RID: 7247
		public Quaternion fakeValue;

		// Token: 0x04001C50 RID: 7248
		private bool inited;
	}
}
