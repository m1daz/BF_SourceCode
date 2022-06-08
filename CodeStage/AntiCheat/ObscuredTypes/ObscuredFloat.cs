using System;
using System.Runtime.InteropServices;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000327 RID: 807
	public struct ObscuredFloat : IEquatable<ObscuredFloat>
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x000D0532 File Offset: 0x000CE932
		private ObscuredFloat(byte[] value)
		{
			this.currentCryptoKey = ObscuredFloat.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0f;
			this.inited = true;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000D0558 File Offset: 0x000CE958
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredFloat.cryptoKey = newKey;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000D0560 File Offset: 0x000CE960
		public int GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredFloat.cryptoKey)
			{
				float value = this.InternalDecrypt();
				this.hiddenValue = ObscuredFloat.InternalEncrypt(value);
				this.currentCryptoKey = ObscuredFloat.cryptoKey;
			}
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.b1 = this.hiddenValue[0];
			floatIntBytesUnion.b2 = this.hiddenValue[1];
			floatIntBytesUnion.b3 = this.hiddenValue[2];
			floatIntBytesUnion.b4 = this.hiddenValue[3];
			return floatIntBytesUnion.i;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000D05E8 File Offset: 0x000CE9E8
		public void SetEncrypted(int encrypted)
		{
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.i = encrypted;
			this.hiddenValue = new byte[]
			{
				floatIntBytesUnion.b1,
				floatIntBytesUnion.b2,
				floatIntBytesUnion.b3,
				floatIntBytesUnion.b4
			};
			if (ObscuredFloat.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000D064F File Offset: 0x000CEA4F
		public static int Encrypt(float value)
		{
			return ObscuredFloat.Encrypt(value, ObscuredFloat.cryptoKey);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000D065C File Offset: 0x000CEA5C
		public static int Encrypt(float value, int key)
		{
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.f = value;
			floatIntBytesUnion.i ^= key;
			return floatIntBytesUnion.i;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000D0690 File Offset: 0x000CEA90
		private static byte[] InternalEncrypt(float value)
		{
			return ObscuredFloat.InternalEncrypt(value, 0);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000D069C File Offset: 0x000CEA9C
		private static byte[] InternalEncrypt(float value, int key)
		{
			int num = key;
			if (num == 0)
			{
				num = ObscuredFloat.cryptoKey;
			}
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.f = value;
			floatIntBytesUnion.i ^= num;
			return new byte[]
			{
				floatIntBytesUnion.b1,
				floatIntBytesUnion.b2,
				floatIntBytesUnion.b3,
				floatIntBytesUnion.b4
			};
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x000D0705 File Offset: 0x000CEB05
		public static float Decrypt(int value)
		{
			return ObscuredFloat.Decrypt(value, ObscuredFloat.cryptoKey);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x000D0714 File Offset: 0x000CEB14
		public static float Decrypt(int value, int key)
		{
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.i = (value ^ key);
			return floatIntBytesUnion.f;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000D073C File Offset: 0x000CEB3C
		private float InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredFloat.cryptoKey;
				this.hiddenValue = ObscuredFloat.InternalEncrypt(0f);
				this.fakeValue = 0f;
				this.inited = true;
			}
			int num = ObscuredFloat.cryptoKey;
			if (this.currentCryptoKey != ObscuredFloat.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.b1 = this.hiddenValue[0];
			floatIntBytesUnion.b2 = this.hiddenValue[1];
			floatIntBytesUnion.b3 = this.hiddenValue[2];
			floatIntBytesUnion.b4 = this.hiddenValue[3];
			floatIntBytesUnion.i ^= num;
			float f = floatIntBytesUnion.f;
			if (ObscuredFloat.onCheatingDetected != null && this.fakeValue != 0f && Math.Abs(f - this.fakeValue) > 1E-06f)
			{
				ObscuredFloat.onCheatingDetected();
				ObscuredFloat.onCheatingDetected = null;
			}
			return f;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000D083C File Offset: 0x000CEC3C
		public static implicit operator ObscuredFloat(float value)
		{
			ObscuredFloat result = new ObscuredFloat(ObscuredFloat.InternalEncrypt(value));
			if (ObscuredFloat.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000D0869 File Offset: 0x000CEC69
		public static implicit operator float(ObscuredFloat value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000D0874 File Offset: 0x000CEC74
		public static ObscuredFloat operator ++(ObscuredFloat input)
		{
			float value = input.InternalDecrypt() + 1f;
			input.hiddenValue = ObscuredFloat.InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredFloat.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000D08B8 File Offset: 0x000CECB8
		public static ObscuredFloat operator --(ObscuredFloat input)
		{
			float value = input.InternalDecrypt() - 1f;
			input.hiddenValue = ObscuredFloat.InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredFloat.onCheatingDetected != null)
			{
				input.fakeValue = value;
			}
			return input;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000D08FC File Offset: 0x000CECFC
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredFloat))
			{
				return false;
			}
			float num = ((ObscuredFloat)obj).InternalDecrypt();
			float num2 = this.InternalDecrypt();
			return (double)num == (double)num2 || (float.IsNaN(num) && float.IsNaN(num2));
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000D094C File Offset: 0x000CED4C
		public bool Equals(ObscuredFloat obj)
		{
			float num = obj.InternalDecrypt();
			float num2 = this.InternalDecrypt();
			return (double)num == (double)num2 || (float.IsNaN(num) && float.IsNaN(num2));
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x000D0988 File Offset: 0x000CED88
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000D09AC File Offset: 0x000CEDAC
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000D09D0 File Offset: 0x000CEDD0
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000D09EC File Offset: 0x000CEDEC
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000D0A08 File Offset: 0x000CEE08
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04001C25 RID: 7205
		public static Action onCheatingDetected;

		// Token: 0x04001C26 RID: 7206
		private static int cryptoKey = 230887;

		// Token: 0x04001C27 RID: 7207
		private int currentCryptoKey;

		// Token: 0x04001C28 RID: 7208
		private byte[] hiddenValue;

		// Token: 0x04001C29 RID: 7209
		private float fakeValue;

		// Token: 0x04001C2A RID: 7210
		private bool inited;

		// Token: 0x02000328 RID: 808
		[StructLayout(2)]
		private struct FloatIntBytesUnion
		{
			// Token: 0x04001C2B RID: 7211
			[FieldOffset(0)]
			public float f;

			// Token: 0x04001C2C RID: 7212
			[FieldOffset(0)]
			public int i;

			// Token: 0x04001C2D RID: 7213
			[FieldOffset(0)]
			public byte b1;

			// Token: 0x04001C2E RID: 7214
			[FieldOffset(1)]
			public byte b2;

			// Token: 0x04001C2F RID: 7215
			[FieldOffset(2)]
			public byte b3;

			// Token: 0x04001C30 RID: 7216
			[FieldOffset(3)]
			public byte b4;
		}
	}
}
