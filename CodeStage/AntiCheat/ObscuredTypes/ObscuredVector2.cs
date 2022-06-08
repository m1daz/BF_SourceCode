using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000334 RID: 820
	public struct ObscuredVector2
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x000D2ED5 File Offset: 0x000D12D5
		private ObscuredVector2(Vector2 value)
		{
			this.currentCryptoKey = ObscuredVector2.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = new Vector2(0f, 0f);
			this.inited = true;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000D2F08 File Offset: 0x000D1308
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x000D2F7D File Offset: 0x000D137D
		public float x
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.x);
				if (ObscuredVector2.onCheatingDetected != null && this.fakeValue != new Vector2(0f, 0f) && Math.Abs(num - this.fakeValue.x) > 0.0005f)
				{
					ObscuredVector2.onCheatingDetected();
					ObscuredVector2.onCheatingDetected = null;
				}
				return num;
			}
			set
			{
				this.hiddenValue.x = this.InternalEncryptField(value);
				if (ObscuredVector2.onCheatingDetected != null)
				{
					this.fakeValue.x = value;
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000D2FA8 File Offset: 0x000D13A8
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x000D301D File Offset: 0x000D141D
		public float y
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.y);
				if (ObscuredVector2.onCheatingDetected != null && this.fakeValue != new Vector2(0f, 0f) && Math.Abs(num - this.fakeValue.y) > 0.0005f)
				{
					ObscuredVector2.onCheatingDetected();
					ObscuredVector2.onCheatingDetected = null;
				}
				return num;
			}
			set
			{
				this.hiddenValue.y = this.InternalEncryptField(value);
				if (ObscuredVector2.onCheatingDetected != null)
				{
					this.fakeValue.y = value;
				}
			}
		}

		// Token: 0x1700015A RID: 346
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.x;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
				}
				return this.y;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000D30AA File Offset: 0x000D14AA
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredVector2.cryptoKey = newKey;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000D30B4 File Offset: 0x000D14B4
		public Vector2 GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
			{
				Vector2 value = this.InternalDecrypt();
				this.hiddenValue = ObscuredVector2.Encrypt(value, ObscuredVector2.cryptoKey);
				this.currentCryptoKey = ObscuredVector2.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000D30FA File Offset: 0x000D14FA
		public void SetEncrypted(Vector2 encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredVector2.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000D3119 File Offset: 0x000D1519
		public static Vector2 Encrypt(Vector2 value)
		{
			return ObscuredVector2.Encrypt(value, 0);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000D3122 File Offset: 0x000D1522
		public static Vector2 Encrypt(Vector2 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector2.cryptoKey;
			}
			value.x = (float)ObscuredDouble.Encrypt((double)value.x, (long)key);
			value.y = (float)ObscuredDouble.Encrypt((double)value.y, (long)key);
			return value;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000D3160 File Offset: 0x000D1560
		public static Vector2 Decrypt(Vector2 value)
		{
			return ObscuredVector2.Decrypt(value, 0);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000D3169 File Offset: 0x000D1569
		public static Vector2 Decrypt(Vector2 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector2.cryptoKey;
			}
			value.x = (float)ObscuredDouble.Decrypt((long)value.x, (long)key);
			value.y = (float)ObscuredDouble.Decrypt((long)value.y, (long)key);
			return value;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000D31A8 File Offset: 0x000D15A8
		private Vector2 InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredVector2.cryptoKey;
				this.hiddenValue = ObscuredVector2.Encrypt(new Vector2(0f, 0f));
				this.fakeValue = new Vector2(0f, 0f);
				this.inited = true;
			}
			int num = ObscuredVector2.cryptoKey;
			if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			Vector2 result;
			result.x = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.x, (long)num);
			result.y = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.y, (long)num);
			if (ObscuredVector2.onCheatingDetected != null && !this.fakeValue.Equals(new Vector2(0f, 0f)) && !result.Equals(this.fakeValue))
			{
				ObscuredVector2.onCheatingDetected();
				ObscuredVector2.onCheatingDetected = null;
			}
			return result;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000D32B8 File Offset: 0x000D16B8
		private float InternalDecryptField(float encrypted)
		{
			int num = ObscuredVector2.cryptoKey;
			if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			return (float)ObscuredDouble.Decrypt((long)encrypted, (long)num);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000D32F0 File Offset: 0x000D16F0
		private float InternalEncryptField(float encrypted)
		{
			return (float)ObscuredDouble.Encrypt((double)encrypted, (long)ObscuredVector2.cryptoKey);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000D3310 File Offset: 0x000D1710
		public static implicit operator ObscuredVector2(Vector2 value)
		{
			ObscuredVector2 result = new ObscuredVector2(ObscuredVector2.Encrypt(value));
			if (ObscuredVector2.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000D333D File Offset: 0x000D173D
		public static implicit operator Vector2(ObscuredVector2 value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000D3348 File Offset: 0x000D1748
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000D336C File Offset: 0x000D176C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000D3390 File Offset: 0x000D1790
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x04001C75 RID: 7285
		public static Action onCheatingDetected;

		// Token: 0x04001C76 RID: 7286
		private static int cryptoKey = 120206;

		// Token: 0x04001C77 RID: 7287
		private int currentCryptoKey;

		// Token: 0x04001C78 RID: 7288
		private Vector2 hiddenValue;

		// Token: 0x04001C79 RID: 7289
		private Vector2 fakeValue;

		// Token: 0x04001C7A RID: 7290
		private bool inited;
	}
}
