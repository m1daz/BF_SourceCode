using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class AstarProfiler
{
	// Token: 0x060005C5 RID: 1477 RVA: 0x0003609A File Offset: 0x0003449A
	private AstarProfiler()
	{
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x000360A2 File Offset: 0x000344A2
	[Conditional("ProfileAstar")]
	public static void InitializeFastProfile(string[] profileNames)
	{
		AstarProfiler.fastProfileNames = profileNames;
		AstarProfiler.fastProfiles = new AstarProfiler.ProfilePoint[profileNames.Length];
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x000360B7 File Offset: 0x000344B7
	[Conditional("ProfileAstar")]
	public static void StartFastProfile(int tag)
	{
		AstarProfiler.fastProfiles[tag].lastRecorded = DateTime.UtcNow;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x000360D0 File Offset: 0x000344D0
	[Conditional("ProfileAstar")]
	public static void EndFastProfile(int tag)
	{
		DateTime utcNow = DateTime.UtcNow;
		AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[tag];
		profilePoint.totalTime += utcNow - profilePoint.lastRecorded;
		profilePoint.totalCalls++;
		AstarProfiler.fastProfiles[tag] = profilePoint;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00036133 File Offset: 0x00034533
	[Conditional("UNITY_PRO_PROFILER")]
	public static void EndProfile()
	{
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00036138 File Offset: 0x00034538
	[Conditional("ProfileAstar")]
	public static void StartProfile(string tag)
	{
		AstarProfiler.ProfilePoint value;
		AstarProfiler.profiles.TryGetValue(tag, out value);
		value.lastRecorded = DateTime.UtcNow;
		AstarProfiler.profiles[tag] = value;
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0003616C File Offset: 0x0003456C
	[Conditional("ProfileAstar")]
	public static void EndProfile(string tag)
	{
		if (!AstarProfiler.profiles.ContainsKey(tag))
		{
			UnityEngine.Debug.LogError("Can only end profiling for a tag which has already been started (tag was " + tag + ")");
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		AstarProfiler.ProfilePoint value = AstarProfiler.profiles[tag];
		value.totalTime += utcNow - value.lastRecorded;
		value.totalCalls++;
		AstarProfiler.profiles[tag] = value;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x000361EC File Offset: 0x000345EC
	[Conditional("ProfileAstar")]
	public static void Reset()
	{
		AstarProfiler.profiles.Clear();
		AstarProfiler.startTime = DateTime.UtcNow;
		if (AstarProfiler.fastProfiles != null)
		{
			for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
			{
				AstarProfiler.fastProfiles[i] = default(AstarProfiler.ProfilePoint);
			}
		}
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00036248 File Offset: 0x00034648
	[Conditional("ProfileAstar")]
	public static void PrintFastResults()
	{
		TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
		stringBuilder.Append("Name\t\t|\tTotal Time\t|\tTotal Calls\t|\tAvg/Call\t");
		for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
		{
			string text = AstarProfiler.fastProfileNames[i];
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[i];
			double totalMilliseconds = profilePoint.totalTime.TotalMilliseconds;
			int totalCalls = profilePoint.totalCalls;
			if (totalCalls >= 1)
			{
				stringBuilder.Append("\n").Append(text.PadLeft(10)).Append("|   ");
				stringBuilder.Append(totalMilliseconds.ToString("0.0").PadLeft(10)).Append("|   ");
				stringBuilder.Append(totalCalls.ToString().PadLeft(10)).Append("|   ");
				stringBuilder.Append((totalMilliseconds / (double)totalCalls).ToString("0.000").PadLeft(10));
			}
		}
		stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
		stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
		stringBuilder.Append(" seconds\n============================");
		UnityEngine.Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x000363A8 File Offset: 0x000347A8
	[Conditional("ProfileAstar")]
	public static void PrintResults()
	{
		TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
		int num = 5;
		foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair in AstarProfiler.profiles)
		{
			num = Math.Max(keyValuePair.Key.Length, num);
		}
		stringBuilder.Append(" Name ".PadRight(num)).Append("|").Append(" Total Time\t".PadRight(20)).Append("|").Append(" Total Calls ".PadRight(20)).Append("|").Append(" Avg/Call ".PadRight(20));
		foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair2 in AstarProfiler.profiles)
		{
			double totalMilliseconds = keyValuePair2.Value.totalTime.TotalMilliseconds;
			int totalCalls = keyValuePair2.Value.totalCalls;
			if (totalCalls >= 1)
			{
				string key = keyValuePair2.Key;
				stringBuilder.Append("\n").Append(key.PadRight(num)).Append("| ");
				stringBuilder.Append(totalMilliseconds.ToString("0.0").PadRight(20)).Append("| ");
				stringBuilder.Append(totalCalls.ToString().PadRight(20)).Append("| ");
				stringBuilder.Append((totalMilliseconds / (double)totalCalls).ToString("0.000").PadRight(20));
			}
		}
		stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
		stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
		stringBuilder.Append(" seconds\n============================");
		UnityEngine.Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x040004B8 RID: 1208
	private static Dictionary<string, AstarProfiler.ProfilePoint> profiles = new Dictionary<string, AstarProfiler.ProfilePoint>();

	// Token: 0x040004B9 RID: 1209
	private static DateTime startTime = DateTime.UtcNow;

	// Token: 0x040004BA RID: 1210
	public static AstarProfiler.ProfilePoint[] fastProfiles;

	// Token: 0x040004BB RID: 1211
	public static string[] fastProfileNames;

	// Token: 0x020000BF RID: 191
	public struct ProfilePoint
	{
		// Token: 0x040004BC RID: 1212
		public DateTime lastRecorded;

		// Token: 0x040004BD RID: 1213
		public TimeSpan totalTime;

		// Token: 0x040004BE RID: 1214
		public int totalCalls;
	}
}
