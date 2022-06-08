using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class AndroidFileTransfer : MonoBehaviour
{
	// Token: 0x06000C62 RID: 3170 RVA: 0x0005CF68 File Offset: 0x0005B368
	public static void UnZipFileOnAndroid()
	{
		string streamingAssetsPath = Application.streamingAssetsPath;
		string str = Application.persistentDataPath + "/assets";
		string str2 = "/SkinEditor/SystemSkins/";
		string str3 = "/SkinEditor/TemplateSkins/";
		List<string> list = new List<string>();
		for (int i = 1; i <= AndroidFileTransfer.SYSTEMSKINNUM; i++)
		{
			string item = "Skin_" + i + ".png";
			list.Add(item);
		}
		string str4 = "/CapeEditor/SystemCapes/";
		string str5 = "/CapeEditor/TemplateCapes/";
		List<string> list2 = new List<string>();
		for (int j = 1; j <= AndroidFileTransfer.SYSTEMCAPENUM; j++)
		{
			string item2 = "Cape_" + j + ".png";
			list2.Add(item2);
		}
		string str6 = "/HatEditor/SystemHats/";
		string str7 = "/HatEditor/TemplateHats/";
		List<string> list3 = new List<string>();
		for (int k = 1; k <= AndroidFileTransfer.SYSTEMHATNUM; k++)
		{
			string item3 = "Hat_" + k + ".png";
			list3.Add(item3);
		}
		string str8 = "/BootEditor/SystemBoots/";
		string str9 = "/BootEditor/TemplateBoots/";
		List<string> list4 = new List<string>();
		for (int l = 1; l <= AndroidFileTransfer.SYSTEMBOOTNUM; l++)
		{
			string item4 = "Boot_" + l + ".png";
			list4.Add(item4);
		}
		try
		{
			foreach (string text in list)
			{
				string url = streamingAssetsPath + str2 + text;
				WWW www = new WWW(url);
				while (!www.isDone)
				{
				}
				if (!string.IsNullOrEmpty(www.error))
				{
					Debug.Log("############### error unpackwww: " + streamingAssetsPath);
					break;
				}
				string text2 = str + str2;
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				File.WriteAllBytes(text2 + text, www.bytes);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("read android data exception: " + ex.ToString());
		}
		try
		{
			string url2 = streamingAssetsPath + str3 + "EmptySkin.png";
			WWW www2 = new WWW(url2);
			while (!www2.isDone)
			{
			}
			if (!string.IsNullOrEmpty(www2.error))
			{
				Debug.Log("############### error unpackwww: " + streamingAssetsPath);
			}
			string text3 = str + str3;
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			File.WriteAllBytes(text3 + "EmptySkin.png", www2.bytes);
		}
		catch (Exception ex2)
		{
		}
		try
		{
			foreach (string text4 in list2)
			{
				string url3 = streamingAssetsPath + str4 + text4;
				WWW www3 = new WWW(url3);
				while (!www3.isDone)
				{
				}
				if (!string.IsNullOrEmpty(www3.error))
				{
					Debug.Log("############### error unpackwww: " + streamingAssetsPath);
					break;
				}
				string text5 = str + str4;
				if (!Directory.Exists(text5))
				{
					Directory.CreateDirectory(text5);
				}
				File.WriteAllBytes(text5 + text4, www3.bytes);
			}
		}
		catch (Exception ex3)
		{
			Debug.Log("read android data exception: " + ex3.ToString());
		}
		try
		{
			string url4 = streamingAssetsPath + str5 + "EmptyCape.png";
			WWW www4 = new WWW(url4);
			while (!www4.isDone)
			{
			}
			if (!string.IsNullOrEmpty(www4.error))
			{
				Debug.Log("############### error unpackwww: " + streamingAssetsPath);
			}
			string text6 = str + str5;
			if (!Directory.Exists(text6))
			{
				Directory.CreateDirectory(text6);
			}
			File.WriteAllBytes(text6 + "EmptyCape.png", www4.bytes);
		}
		catch (Exception ex4)
		{
		}
		try
		{
			foreach (string text7 in list3)
			{
				string url5 = streamingAssetsPath + str6 + text7;
				WWW www5 = new WWW(url5);
				while (!www5.isDone)
				{
				}
				if (!string.IsNullOrEmpty(www5.error))
				{
					Debug.Log("############### error unpackwww: " + streamingAssetsPath);
					break;
				}
				string text8 = str + str6;
				if (!Directory.Exists(text8))
				{
					Directory.CreateDirectory(text8);
				}
				File.WriteAllBytes(text8 + text7, www5.bytes);
			}
		}
		catch (Exception ex5)
		{
			Debug.Log("read android data exception: " + ex5.ToString());
		}
		try
		{
			string url6 = streamingAssetsPath + str7 + "EmptyHat.png";
			WWW www6 = new WWW(url6);
			while (!www6.isDone)
			{
			}
			if (!string.IsNullOrEmpty(www6.error))
			{
				Debug.Log("############### error unpackwww: " + streamingAssetsPath);
			}
			string text9 = str + str7;
			if (!Directory.Exists(text9))
			{
				Directory.CreateDirectory(text9);
			}
			File.WriteAllBytes(text9 + "EmptyHat.png", www6.bytes);
		}
		catch (Exception ex6)
		{
		}
		try
		{
			foreach (string text10 in list4)
			{
				string url7 = streamingAssetsPath + str8 + text10;
				WWW www7 = new WWW(url7);
				while (!www7.isDone)
				{
				}
				if (!string.IsNullOrEmpty(www7.error))
				{
					Debug.Log("############### error unpackwww: " + streamingAssetsPath);
					break;
				}
				string text11 = str + str8;
				if (!Directory.Exists(text11))
				{
					Directory.CreateDirectory(text11);
				}
				File.WriteAllBytes(text11 + text10, www7.bytes);
			}
		}
		catch (Exception ex7)
		{
			Debug.Log("read android data exception: " + ex7.ToString());
		}
		try
		{
			string url8 = streamingAssetsPath + str9 + "EmptyHat.png";
			WWW www8 = new WWW(url8);
			while (!www8.isDone)
			{
			}
			if (!string.IsNullOrEmpty(www8.error))
			{
				Debug.Log("############### error unpackwww: " + streamingAssetsPath);
			}
			string text12 = str + str9;
			if (!Directory.Exists(text12))
			{
				Directory.CreateDirectory(text12);
			}
			File.WriteAllBytes(text12 + "EmptyBoot.png", www8.bytes);
		}
		catch (Exception ex8)
		{
		}
	}

	// Token: 0x04000D0B RID: 3339
	private static int SYSTEMSKINNUM = 50;

	// Token: 0x04000D0C RID: 3340
	private static int SYSTEMCAPENUM = 8;

	// Token: 0x04000D0D RID: 3341
	private static int SYSTEMHATNUM = 11;

	// Token: 0x04000D0E RID: 3342
	private static int SYSTEMBOOTNUM = 3;
}
