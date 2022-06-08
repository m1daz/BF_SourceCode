using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class PhotonPingManager
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0004551C File Offset: 0x0004391C
	public Region BestRegion
	{
		get
		{
			Region result = null;
			int num = int.MaxValue;
			foreach (Region region in PhotonNetwork.networkingPeer.AvailableRegions)
			{
				UnityEngine.Debug.Log("BestRegion checks region: " + region);
				if (region.Ping != 0 && region.Ping < num)
				{
					num = region.Ping;
					result = region;
				}
			}
			return result;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000455B0 File Offset: 0x000439B0
	public bool Done
	{
		get
		{
			return this.PingsRunning == 0;
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x000455BC File Offset: 0x000439BC
	public IEnumerator PingSocket(Region region)
	{
		region.Ping = PhotonPingManager.Attempts * PhotonPingManager.MaxMilliseconsPerPing;
		this.PingsRunning++;
		PhotonPing ping;
		if (PhotonHandler.PingImplementation == typeof(PingNativeDynamic))
		{
			UnityEngine.Debug.Log("Using constructor for new PingNativeDynamic()");
			ping = new PingNativeDynamic();
		}
		else if (PhotonHandler.PingImplementation == typeof(PingMono))
		{
			ping = new PingMono();
		}
		else
		{
			ping = (PhotonPing)Activator.CreateInstance(PhotonHandler.PingImplementation);
		}
		float rttSum = 0f;
		int replyCount = 0;
		string cleanIpOfRegion = region.HostAndPort;
		int indexOfColon = cleanIpOfRegion.LastIndexOf(':');
		if (indexOfColon > 1)
		{
			cleanIpOfRegion = cleanIpOfRegion.Substring(0, indexOfColon);
		}
		cleanIpOfRegion = PhotonPingManager.ResolveHost(cleanIpOfRegion);
		for (int i = 0; i < PhotonPingManager.Attempts; i++)
		{
			bool overtime = false;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			try
			{
				ping.StartPing(cleanIpOfRegion);
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log("catched: " + arg);
				this.PingsRunning--;
				break;
			}
			while (!ping.Done())
			{
				if (sw.ElapsedMilliseconds >= (long)PhotonPingManager.MaxMilliseconsPerPing)
				{
					overtime = true;
					break;
				}
				yield return 0;
			}
			int rtt = (int)sw.ElapsedMilliseconds;
			if (!PhotonPingManager.IgnoreInitialAttempt || i != 0)
			{
				if (ping.Successful && !overtime)
				{
					rttSum += (float)rtt;
					replyCount++;
					region.Ping = (int)(rttSum / (float)replyCount);
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
		this.PingsRunning--;
		yield return null;
		yield break;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x000455E0 File Offset: 0x000439E0
	public static string ResolveHost(string hostName)
	{
		string text = string.Empty;
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
			if (hostAddresses.Length == 1)
			{
				return hostAddresses[0].ToString();
			}
			foreach (IPAddress ipaddress in hostAddresses)
			{
				if (ipaddress != null)
				{
					if (ipaddress.ToString().Contains(":"))
					{
						return ipaddress.ToString();
					}
					if (string.IsNullOrEmpty(text))
					{
						text = hostAddresses.ToString();
					}
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Exception caught! " + ex.Source + " Message: " + ex.Message);
		}
		return text;
	}

	// Token: 0x040007E4 RID: 2020
	public bool UseNative;

	// Token: 0x040007E5 RID: 2021
	public static int Attempts = 5;

	// Token: 0x040007E6 RID: 2022
	public static bool IgnoreInitialAttempt = true;

	// Token: 0x040007E7 RID: 2023
	public static int MaxMilliseconsPerPing = 800;

	// Token: 0x040007E8 RID: 2024
	private int PingsRunning;
}
