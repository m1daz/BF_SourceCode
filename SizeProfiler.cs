using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Pathfinding;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class SizeProfiler
{
	// Token: 0x060005E5 RID: 1509 RVA: 0x00036D68 File Offset: 0x00035168
	public static void Initialize()
	{
		SizeProfiler.profiles.Clear();
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00036D74 File Offset: 0x00035174
	[Conditional("ASTAR_SizeProfile")]
	public static void Begin(string s, BinaryWriter stream)
	{
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00036D76 File Offset: 0x00035176
	[Conditional("ASTAR_SizeProfile")]
	public static void Begin(string s, BinaryWriter stream, bool autoClosing)
	{
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00036D78 File Offset: 0x00035178
	[Conditional("ASTAR_SizeProfile")]
	public static void Begin(string s, Stream stream, bool autoClosing)
	{
		if (SizeProfiler.hasClosed || SizeProfiler.profiles.ContainsKey(SizeProfiler.lastOpen))
		{
		}
		SizeProfiler.ProfileSizePoint value = default(SizeProfiler.ProfileSizePoint);
		if (!SizeProfiler.profiles.ContainsKey(s))
		{
			SizeProfiler.profiles[s] = default(SizeProfiler.ProfileSizePoint);
		}
		else
		{
			value = SizeProfiler.profiles[s];
		}
		if (value.open)
		{
			UnityEngine.Debug.LogWarning("Opening an already open entry (" + s + ")");
		}
		value.lastBegin = stream.Position;
		value.open = true;
		if (autoClosing)
		{
			SizeProfiler.hasClosed = false;
			SizeProfiler.lastOpen = s;
		}
		SizeProfiler.profiles[s] = value;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00036E34 File Offset: 0x00035234
	[Conditional("ASTAR_SizeProfile")]
	public static void End(string s, BinaryWriter stream)
	{
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00036E38 File Offset: 0x00035238
	[Conditional("ASTAR_SizeProfile")]
	public static void End(string s, Stream stream)
	{
		if (!SizeProfiler.profiles.ContainsKey(s))
		{
			UnityEngine.Debug.LogError("Can't end profile before one has started (" + s + ")");
			return;
		}
		SizeProfiler.ProfileSizePoint value = SizeProfiler.profiles[s];
		if (!value.open)
		{
			UnityEngine.Debug.LogWarning("Cannot close an already closed entry (" + s + ")");
			return;
		}
		SizeProfiler.hasClosed = true;
		value.totalSize += stream.Position - value.lastBegin;
		value.open = false;
		SizeProfiler.profiles[s] = value;
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00036ED0 File Offset: 0x000352D0
	[Conditional("ASTAR_SizeProfile")]
	public static void Log()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("============================\n\t\t\t\tSize Profile results:\n============================\n");
		foreach (KeyValuePair<string, SizeProfiler.ProfileSizePoint> keyValuePair in SizeProfiler.profiles)
		{
			stringBuilder.Append(keyValuePair.Key);
			stringBuilder.Append("\tused\t");
			stringBuilder.Append(Mathfx.FormatBytes((int)keyValuePair.Value.totalSize));
			stringBuilder.Append("\n");
		}
		UnityEngine.Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x040004C3 RID: 1219
	private static Dictionary<string, SizeProfiler.ProfileSizePoint> profiles = new Dictionary<string, SizeProfiler.ProfileSizePoint>();

	// Token: 0x040004C4 RID: 1220
	private static string lastOpen = string.Empty;

	// Token: 0x040004C5 RID: 1221
	private static bool hasClosed = false;

	// Token: 0x020000C5 RID: 197
	public struct ProfileSizePoint
	{
		// Token: 0x040004C6 RID: 1222
		public long lastBegin;

		// Token: 0x040004C7 RID: 1223
		public long totalSize;

		// Token: 0x040004C8 RID: 1224
		public bool open;
	}
}
