using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000335 RID: 821
	public struct ObscuredVector3
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x000D33B8 File Offset: 0x000D17B8
		private ObscuredVector3(Vector3 value)
		{
			this.currentCryptoKey = ObscuredVector3.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = new Vector3(0f, 0f);
			this.inited = true;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x000D33E8 File Offset: 0x000D17E8
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x000D345D File Offset: 0x000D185D
		public float x
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.x);
				if (ObscuredVector3.onCheatingDetected != null && this.fakeValue != new Vector3(0f, 0f) && Math.Abs(num - this.fakeValue.x) > 0.0005f)
				{
					ObscuredVector3.onCheatingDetected();
					ObscuredVector3.onCheatingDetected = null;
				}
				return num;
			}
			set
			{
				this.hiddenValue.x = this.InternalEncryptField(value);
				if (ObscuredVector3.onCheatingDetected != null)
				{
					this.fakeValue.x = value;
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000D3488 File Offset: 0x000D1888
		// (set) Token: 0x06001A40 RID: 6720 RVA: 0x000D34FD File Offset: 0x000D18FD
		public float y
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.y);
				if (ObscuredVector3.onCheatingDetected != null && this.fakeValue != new Vector3(0f, 0f) && Math.Abs(num - this.fakeValue.y) > 0.0005f)
				{
					ObscuredVector3.onCheatingDetected();
					ObscuredVector3.onCheatingDetected = null;
				}
				return num;
			}
			set
			{
				this.hiddenValue.y = this.InternalEncryptField(value);
				if (ObscuredVector3.onCheatingDetected != null)
				{
					this.fakeValue.y = value;
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x000D3528 File Offset: 0x000D1928
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x000D359D File Offset: 0x000D199D
		public float z
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.z);
				if (ObscuredVector3.onCheatingDetected != null && this.fakeValue != new Vector3(0f, 0f) && Math.Abs(num - this.fakeValue.z) > 0.0005f)
				{
					ObscuredVector3.onCheatingDetected();
					ObscuredVector3.onCheatingDetected = null;
				}
				return num;
			}
			set
			{
				this.hiddenValue.z = this.InternalEncryptField(value);
				if (ObscuredVector3.onCheatingDetected != null)
				{
					this.fakeValue.z = value;
				}
			}
		}

		// Token: 0x1700015E RID: 350
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				}
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000D3653 File Offset: 0x000D1A53
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredVector3.cryptoKey = newKey;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000D365C File Offset: 0x000D1A5C
		public Vector3 GetEncrypted()
		{
			if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
			{
				Vector3 value = this.InternalDecrypt();
				this.hiddenValue = ObscuredVector3.Encrypt(value, ObscuredVector3.cryptoKey);
				this.currentCryptoKey = ObscuredVector3.cryptoKey;
			}
			return this.hiddenValue;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000D36A2 File Offset: 0x000D1AA2
		public void SetEncrypted(Vector3 encrypted)
		{
			this.hiddenValue = encrypted;
			if (ObscuredVector3.onCheatingDetected != null)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000D36C1 File Offset: 0x000D1AC1
		public static Vector3 Encrypt(Vector3 value)
		{
			return ObscuredVector3.Encrypt(value, 0);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000D36CC File Offset: 0x000D1ACC
		public static Vector3 Encrypt(Vector3 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector3.cryptoKey;
			}
			value.x = (float)ObscuredDouble.Encrypt((double)value.x, (long)key);
			value.y = (float)ObscuredDouble.Encrypt((double)value.y, (long)key);
			value.z = (float)ObscuredDouble.Encrypt((double)value.z, (long)key);
			return value;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000D372C File Offset: 0x000D1B2C
		public static Vector3 Decrypt(Vector3 value)
		{
			return ObscuredVector3.Decrypt(value, 0);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000D3738 File Offset: 0x000D1B38
		public static Vector3 Decrypt(Vector3 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector3.cryptoKey;
			}
			value.x = (float)ObscuredDouble.Decrypt((long)value.x, (long)key);
			value.y = (float)ObscuredDouble.Decrypt((long)value.y, (long)key);
			value.z = (float)ObscuredDouble.Decrypt((long)value.z, (long)key);
			return value;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000D3798 File Offset: 0x000D1B98
		private Vector3 InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredVector3.cryptoKey;
				this.hiddenValue = ObscuredVector3.Encrypt(new Vector3(0f, 0f));
				this.fakeValue = new Vector3(0f, 0f);
				this.inited = true;
			}
			int num = ObscuredVector3.cryptoKey;
			if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			Vector3 result;
			result.x = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.x, (long)num);
			result.y = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.y, (long)num);
			result.z = (float)ObscuredDouble.Decrypt((long)this.hiddenValue.z, (long)num);
			if (ObscuredVector3.onCheatingDetected != null && !this.fakeValue.Equals(new Vector3(0f, 0f)) && !result.Equals(this.fakeValue))
			{
				ObscuredVector3.onCheatingDetected();
				ObscuredVector3.onCheatingDetected = null;
			}
			return result;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000D38C4 File Offset: 0x000D1CC4
		private float InternalDecryptField(float encrypted)
		{
			int num = ObscuredVector3.cryptoKey;
			if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			return (float)ObscuredDouble.Decrypt((long)encrypted, (long)num);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000D38FC File Offset: 0x000D1CFC
		private float InternalEncryptField(float encrypted)
		{
			return (float)ObscuredDouble.Encrypt((double)encrypted, (long)ObscuredVector3.cryptoKey);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000D391C File Offset: 0x000D1D1C
		public static implicit operator ObscuredVector3(Vector3 value)
		{
			ObscuredVector3 result = new ObscuredVector3(ObscuredVector3.Encrypt(value));
			if (ObscuredVector3.onCheatingDetected != null)
			{
				result.fakeValue = value;
			}
			return result;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000D3949 File Offset: 0x000D1D49
		public static implicit operator Vector3(ObscuredVector3 value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000D3954 File Offset: 0x000D1D54
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000D3978 File Offset: 0x000D1D78
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000D399C File Offset: 0x000D1D9C
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x04001C7B RID: 7291
		public static Action onCheatingDetected;

		// Token: 0x04001C7C RID: 7292
		private static int cryptoKey = 120207;

		// Token: 0x04001C7D RID: 7293
		private int currentCryptoKey;

		// Token: 0x04001C7E RID: 7294
		private Vector3 hiddenValue;

		// Token: 0x04001C7F RID: 7295
		private Vector3 fakeValue;

		// Token: 0x04001C80 RID: 7296
		private bool inited;
	}
}
