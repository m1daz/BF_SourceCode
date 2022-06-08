using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using PublicKeyConvert;
using SimpleJSON;
using UnityEngine;

// Token: 0x0200046F RID: 1135
public class PV
{
	// Token: 0x060020EA RID: 8426 RVA: 0x000F5FA8 File Offset: 0x000F43A8
	public static bool verifyEceipt(string strReceipt)
	{
		string text = string.Empty;
		string message = string.Empty;
		string publicKey = string.Empty;
		JObject jobject = JObject.Parse(strReceipt);
		JSONClass asObject = jobject.AsObject;
		Dictionary<string, JObject> dictionary = new Dictionary<string, JObject>();
		dictionary = asObject.m_Dict;
		if (dictionary.ContainsKey("signature"))
		{
			text = dictionary["signature"];
			text = text.Replace("\\", string.Empty);
		}
		if (dictionary.ContainsKey("json"))
		{
			message = dictionary["json"];
		}
		RSACryptoServiceProvider rsacryptoServiceProvider = PEMKeyLoader.CryptoServiceProviderFromPublicKeyInfo(GGCloudServiceKit.pkey);
		publicKey = rsacryptoServiceProvider.ToXmlString(false);
		return PV.verify(message, text, publicKey);
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x000F605C File Offset: 0x000F445C
	private static bool verify(string message, string base64Signature, string publicKey)
	{
		bool result = false;
		try
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.FromXmlString(publicKey);
			byte[] signature = Convert.FromBase64String(base64Signature);
			SHA1Managed halg = new SHA1Managed();
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			result = rsacryptoServiceProvider.VerifyData(bytes, halg, signature);
		}
		catch (Exception ex)
		{
			Debug.Log("exception: " + ex.ToString());
		}
		return result;
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x000F60D0 File Offset: 0x000F44D0
	public static string Encrypt(string str)
	{
		DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
		byte[] bytes = Encoding.Unicode.GetBytes(GGCloudServiceKit.EK);
		byte[] bytes2 = Encoding.Unicode.GetBytes(str);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
		cryptoStream.Write(bytes2, 0, bytes2.Length);
		cryptoStream.FlushFinalBlock();
		return Convert.ToBase64String(memoryStream.ToArray());
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x000F6134 File Offset: 0x000F4534
	public static string Decrypt(string str)
	{
		DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
		byte[] bytes = Encoding.Unicode.GetBytes(GGCloudServiceKit.EK);
		byte[] array = Convert.FromBase64String(str);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Write);
		cryptoStream.Write(array, 0, array.Length);
		cryptoStream.FlushFinalBlock();
		return Encoding.Unicode.GetString(memoryStream.ToArray());
	}

	// Token: 0x040021B7 RID: 8631
	public static string mk1 = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAhbis1BBBbfjePi8rSDML4My0196c0uLf";

	// Token: 0x040021B8 RID: 8632
	public static string mk2 = "G3TVJ9aqwoe9YkZHZ0+7sfS4gaEhLuCxHVcqTel+IW0pAt5299WTchn1DBA6A/irmc9Gt80xuJ6DCCyyER0nee9lij7Jh+6nV44X+hy5qrRdCcrFti1ehQrVbbxHgNmnR6RKBteXY3q0Mqc7WgyA33rmYi1/naIImtx+RExQT5VFHrhfsjtIUWIlkNoCaprtmR86xsjxdIM5CAy2f3ns6l/lKd2EtdpxP9Tb8TwiOvEMekPGu1awBlZFPDGvSnQpt9pz3JWnSf1W9qR8YoWdNnGbVlJeeokIX6vRmwtts+Y0saLBAiPK6Z5/2YIQCKgZ83vV21nSIvA0yfWrUQP/UAGjH5NWi3nwRDxa4Z9OaEcg32thw/VjvdwUDNPN1YXWX6QcPxyF9DtN320NBmyOcAgG5RrYJMyYG/R0ZwdXnFRTm2HTu6kM9aHldSsXZnskj4CB7AtzBH3qWl/8fuhHUwSo/5WzfaZQuzbe5LGZduCe7QcCmba7EVy24z/kmbLHySGVMPufJs3xgCHEeRYfZePQokheb39sQDPCRpm0Dzds3qj84fhHdAO+pqcLF5v7ZEj/H7cacq2ReCnFSbm2d2RiP7DsoYH87WM6OP8T1HFY1+v7YEt0wod+okbajxeCytC5+WvqOHIOXNqaCOXs+9XyNfPIBEzM379B5TugcsrTDP4QcKMsPrjbwbTP2zDToReP185SNaMjLWfl5IitKyKIG5Ku+oIKoD3fAffmXbqhMyCm6Fbw/eSKtKrkmGOAfUfLj11krIxyjpo5A8MmcPwkzEcybtQXumw4bh0m6QqT82BOZB1ywZADx9F60IxMN3t+s+pjWjomQtsCgHzZWw==";
}
