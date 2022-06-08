using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PublicKeyConvert
{
	// Token: 0x0200046E RID: 1134
	public class PEMKeyLoader
	{
		// Token: 0x060020E4 RID: 8420 RVA: 0x000F5C04 File Offset: 0x000F4004
		private static bool CompareBytearrays(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			int num = 0;
			foreach (byte b2 in a)
			{
				if (b2 != b[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000F5C4C File Offset: 0x000F404C
		public static RSACryptoServiceProvider CryptoServiceProviderFromPublicKeyInfo(byte[] x509key)
		{
			byte[] a = new byte[15];
			if (x509key == null || x509key.Length == 0)
			{
				return null;
			}
			int num = x509key.Length;
			MemoryStream input = new MemoryStream(x509key);
			BinaryReader binaryReader = new BinaryReader(input);
			RSACryptoServiceProvider result;
			try
			{
				ushort num2 = binaryReader.ReadUInt16();
				if (num2 == 33072)
				{
					binaryReader.ReadByte();
				}
				else
				{
					if (num2 != 33328)
					{
						return null;
					}
					binaryReader.ReadInt16();
				}
				a = binaryReader.ReadBytes(15);
				if (!PEMKeyLoader.CompareBytearrays(a, PEMKeyLoader.SeqOID))
				{
					result = null;
				}
				else
				{
					num2 = binaryReader.ReadUInt16();
					if (num2 == 33027)
					{
						binaryReader.ReadByte();
					}
					else
					{
						if (num2 != 33283)
						{
							return null;
						}
						binaryReader.ReadInt16();
					}
					byte b = binaryReader.ReadByte();
					if (b != 0)
					{
						result = null;
					}
					else
					{
						num2 = binaryReader.ReadUInt16();
						if (num2 == 33072)
						{
							binaryReader.ReadByte();
						}
						else
						{
							if (num2 != 33328)
							{
								return null;
							}
							binaryReader.ReadInt16();
						}
						num2 = binaryReader.ReadUInt16();
						byte b2 = 0;
						byte b3;
						if (num2 == 33026)
						{
							b3 = binaryReader.ReadByte();
						}
						else
						{
							if (num2 != 33282)
							{
								return null;
							}
							b2 = binaryReader.ReadByte();
							b3 = binaryReader.ReadByte();
						}
						byte[] array = new byte[4];
						array[0] = b3;
						array[1] = b2;
						byte[] value = array;
						int num3 = BitConverter.ToInt32(value, 0);
						if (binaryReader.PeekChar() == 0)
						{
							binaryReader.ReadByte();
							num3--;
						}
						byte[] modulus = binaryReader.ReadBytes(num3);
						if (binaryReader.ReadByte() != 2)
						{
							result = null;
						}
						else
						{
							int count = (int)binaryReader.ReadByte();
							byte[] exponent = binaryReader.ReadBytes(count);
							RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
							rsacryptoServiceProvider.ImportParameters(new RSAParameters
							{
								Modulus = modulus,
								Exponent = exponent
							});
							result = rsacryptoServiceProvider;
						}
					}
				}
			}
			finally
			{
				binaryReader.Close();
			}
			return result;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000F5E94 File Offset: 0x000F4294
		public static RSACryptoServiceProvider CryptoServiceProviderFromPublicKeyInfo(string base64EncodedKey)
		{
			try
			{
				return PEMKeyLoader.CryptoServiceProviderFromPublicKeyInfo(Convert.FromBase64String(base64EncodedKey));
			}
			catch (FormatException)
			{
			}
			return null;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000F5ECC File Offset: 0x000F42CC
		public static byte[] X509KeyFromFile(string filename)
		{
			if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
			{
				return null;
			}
			StreamReader streamReader = File.OpenText(filename);
			string value = streamReader.ReadToEnd();
			streamReader.Close();
			StringBuilder stringBuilder = new StringBuilder(value);
			stringBuilder.Replace("-----BEGIN PUBLIC KEY-----", string.Empty);
			stringBuilder.Replace("-----END PUBLIC KEY-----", string.Empty);
			byte[] array;
			try
			{
				array = Convert.FromBase64String(stringBuilder.ToString());
			}
			catch (FormatException)
			{
				Stream stream = new FileStream(filename, FileMode.Open);
				int num = (int)stream.Length;
				array = new byte[num];
				stream.Read(array, 0, num);
				stream.Close();
			}
			return array;
		}

		// Token: 0x040021B6 RID: 8630
		private static byte[] SeqOID = new byte[]
		{
			48,
			13,
			6,
			9,
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			1,
			1,
			5,
			0
		};
	}
}
