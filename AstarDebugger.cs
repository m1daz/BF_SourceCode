using System;
using System.Runtime.CompilerServices;
using System.Text;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x02000028 RID: 40
[AddComponentMenu("Pathfinding/Debugger")]
[ExecuteInEditMode]
public class AstarDebugger : MonoBehaviour
{
	// Token: 0x06000147 RID: 327 RVA: 0x0000ACB4 File Offset: 0x000090B4
	public AstarDebugger()
	{
		AstarDebugger.PathTypeDebug[] array = new AstarDebugger.PathTypeDebug[7];
		int num = 0;
		string name = "ABPath";
		if (AstarDebugger.<>f__mg$cache0 == null)
		{
			AstarDebugger.<>f__mg$cache0 = new Func<int>(PathPool<ABPath>.GetSize);
		}
		Func<int> getSize = AstarDebugger.<>f__mg$cache0;
		if (AstarDebugger.<>f__mg$cache1 == null)
		{
			AstarDebugger.<>f__mg$cache1 = new Func<int>(PathPool<ABPath>.GetTotalCreated);
		}
		array[num] = new AstarDebugger.PathTypeDebug(name, getSize, AstarDebugger.<>f__mg$cache1);
		int num2 = 1;
		string name2 = "MultiTargetPath";
		if (AstarDebugger.<>f__mg$cache2 == null)
		{
			AstarDebugger.<>f__mg$cache2 = new Func<int>(PathPool<MultiTargetPath>.GetSize);
		}
		Func<int> getSize2 = AstarDebugger.<>f__mg$cache2;
		if (AstarDebugger.<>f__mg$cache3 == null)
		{
			AstarDebugger.<>f__mg$cache3 = new Func<int>(PathPool<MultiTargetPath>.GetTotalCreated);
		}
		array[num2] = new AstarDebugger.PathTypeDebug(name2, getSize2, AstarDebugger.<>f__mg$cache3);
		int num3 = 2;
		string name3 = "RandomPath";
		if (AstarDebugger.<>f__mg$cache4 == null)
		{
			AstarDebugger.<>f__mg$cache4 = new Func<int>(PathPool<RandomPath>.GetSize);
		}
		Func<int> getSize3 = AstarDebugger.<>f__mg$cache4;
		if (AstarDebugger.<>f__mg$cache5 == null)
		{
			AstarDebugger.<>f__mg$cache5 = new Func<int>(PathPool<RandomPath>.GetTotalCreated);
		}
		array[num3] = new AstarDebugger.PathTypeDebug(name3, getSize3, AstarDebugger.<>f__mg$cache5);
		int num4 = 3;
		string name4 = "FleePath";
		if (AstarDebugger.<>f__mg$cache6 == null)
		{
			AstarDebugger.<>f__mg$cache6 = new Func<int>(PathPool<FleePath>.GetSize);
		}
		Func<int> getSize4 = AstarDebugger.<>f__mg$cache6;
		if (AstarDebugger.<>f__mg$cache7 == null)
		{
			AstarDebugger.<>f__mg$cache7 = new Func<int>(PathPool<FleePath>.GetTotalCreated);
		}
		array[num4] = new AstarDebugger.PathTypeDebug(name4, getSize4, AstarDebugger.<>f__mg$cache7);
		int num5 = 4;
		string name5 = "ConstantPath";
		if (AstarDebugger.<>f__mg$cache8 == null)
		{
			AstarDebugger.<>f__mg$cache8 = new Func<int>(PathPool<ConstantPath>.GetSize);
		}
		Func<int> getSize5 = AstarDebugger.<>f__mg$cache8;
		if (AstarDebugger.<>f__mg$cache9 == null)
		{
			AstarDebugger.<>f__mg$cache9 = new Func<int>(PathPool<ConstantPath>.GetTotalCreated);
		}
		array[num5] = new AstarDebugger.PathTypeDebug(name5, getSize5, AstarDebugger.<>f__mg$cache9);
		int num6 = 5;
		string name6 = "FloodPath";
		if (AstarDebugger.<>f__mg$cacheA == null)
		{
			AstarDebugger.<>f__mg$cacheA = new Func<int>(PathPool<FloodPath>.GetSize);
		}
		Func<int> getSize6 = AstarDebugger.<>f__mg$cacheA;
		if (AstarDebugger.<>f__mg$cacheB == null)
		{
			AstarDebugger.<>f__mg$cacheB = new Func<int>(PathPool<FloodPath>.GetTotalCreated);
		}
		array[num6] = new AstarDebugger.PathTypeDebug(name6, getSize6, AstarDebugger.<>f__mg$cacheB);
		int num7 = 6;
		string name7 = "FloodPathTracer";
		if (AstarDebugger.<>f__mg$cacheC == null)
		{
			AstarDebugger.<>f__mg$cacheC = new Func<int>(PathPool<FloodPathTracer>.GetSize);
		}
		Func<int> getSize7 = AstarDebugger.<>f__mg$cacheC;
		if (AstarDebugger.<>f__mg$cacheD == null)
		{
			AstarDebugger.<>f__mg$cacheD = new Func<int>(PathPool<FloodPathTracer>.GetTotalCreated);
		}
		array[num7] = new AstarDebugger.PathTypeDebug(name7, getSize7, AstarDebugger.<>f__mg$cacheD);
		this.debugTypes = array;
		base..ctor();
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000AF58 File Offset: 0x00009358
	public void Start()
	{
		base.useGUILayout = false;
		this.fpsDrops = new float[this.fpsDropCounterSize];
		for (int i = 0; i < this.fpsDrops.Length; i++)
		{
			this.fpsDrops[i] = 1f / Time.deltaTime;
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000AFAC File Offset: 0x000093AC
	public void Update()
	{
		if (!this.show || (!Application.isPlaying && !this.showInEditor))
		{
			return;
		}
		int num = GC.CollectionCount(0);
		if (this.lastCollectNum != (float)num)
		{
			this.lastCollectNum = (float)num;
			this.delta = Time.realtimeSinceStartup - this.lastCollect;
			this.lastCollect = Time.realtimeSinceStartup;
			this.lastDeltaTime = Time.deltaTime;
			this.collectAlloc = this.allocMem;
		}
		this.allocMem = (int)GC.GetTotalMemory(false);
		this.peakAlloc = ((this.allocMem <= this.peakAlloc) ? this.peakAlloc : this.allocMem);
		if (Time.realtimeSinceStartup - this.lastAllocSet > 0.3f || !Application.isPlaying)
		{
			int num2 = this.allocMem - this.lastAllocMemory;
			this.lastAllocMemory = this.allocMem;
			this.lastAllocSet = Time.realtimeSinceStartup;
			this.delayedDeltaTime = Time.deltaTime;
			if (num2 >= 0)
			{
				this.allocRate = num2;
			}
		}
		if (this.lastFrameCount != Time.frameCount || !Application.isPlaying)
		{
			this.fpsDrops[Time.frameCount % this.fpsDrops.Length] = ((Time.deltaTime == 0f) ? float.PositiveInfinity : (1f / Time.deltaTime));
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000B110 File Offset: 0x00009510
	public void OnGUI()
	{
		if (!this.show || (!Application.isPlaying && !this.showInEditor))
		{
			return;
		}
		if (this.style == null)
		{
			this.style = new GUIStyle();
			this.style.normal.textColor = Color.white;
			this.style.padding = new RectOffset(5, 5, 5, 5);
		}
		if (Time.realtimeSinceStartup - this.lastUpdate > 0.2f || this.cachedText == null || !Application.isPlaying)
		{
			this.lastUpdate = Time.time;
			this.boxRect = new Rect(5f, (float)this.yOffset, 310f, 40f);
			this.text.Length = 0;
			this.text.AppendLine("A* Pathfinding Project Debugger");
			this.text.Append("A* Version: ").Append(AstarPath.Version.ToString());
			if (this.showMemProfile)
			{
				this.boxRect.height = this.boxRect.height + 200f;
				this.text.AppendLine();
				this.text.AppendLine();
				this.text.Append("Currently allocated".PadRight(25));
				this.text.Append(((float)this.allocMem / 1000000f).ToString("0.0 MB"));
				this.text.AppendLine();
				this.text.Append("Peak allocated".PadRight(25));
				this.text.Append(((float)this.peakAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
				this.text.Append("Last collect peak".PadRight(25));
				this.text.Append(((float)this.collectAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
				this.text.Append("Allocation rate".PadRight(25));
				this.text.Append(((float)this.allocRate / 1000000f).ToString("0.0 MB")).AppendLine();
				this.text.Append("Collection frequency".PadRight(25));
				this.text.Append(this.delta.ToString("0.00"));
				this.text.Append("s\n");
				this.text.Append("Last collect fps".PadRight(25));
				this.text.Append((1f / this.lastDeltaTime).ToString("0.0 fps"));
				this.text.Append(" (");
				this.text.Append(this.lastDeltaTime.ToString("0.000 s"));
				this.text.Append(")");
			}
			if (this.showFPS)
			{
				this.text.AppendLine();
				this.text.AppendLine();
				this.text.Append("FPS".PadRight(25)).Append((1f / this.delayedDeltaTime).ToString("0.0 fps"));
				float num = float.PositiveInfinity;
				for (int i = 0; i < this.fpsDrops.Length; i++)
				{
					if (this.fpsDrops[i] < num)
					{
						num = this.fpsDrops[i];
					}
				}
				this.text.AppendLine();
				this.text.Append(("Lowest fps (last " + this.fpsDrops.Length + ")").PadRight(25)).Append(num.ToString("0.0"));
			}
			if (this.showPathProfile)
			{
				AstarPath active = AstarPath.active;
				this.text.AppendLine();
				if (active == null)
				{
					this.text.Append("\nNo AstarPath Object In The Scene");
				}
				else
				{
					if (ListPool<Vector3>.GetSize() > this.maxVecPool)
					{
						this.maxVecPool = ListPool<Vector3>.GetSize();
					}
					if (ListPool<Node>.GetSize() > this.maxNodePool)
					{
						this.maxNodePool = ListPool<Node>.GetSize();
					}
					this.text.Append("\nPool Sizes (size/total created)");
					for (int j = 0; j < this.debugTypes.Length; j++)
					{
						this.debugTypes[j].Print(this.text);
					}
				}
			}
			this.cachedText = this.text.ToString();
		}
		if (this.font != null)
		{
			this.style.font = this.font;
			this.style.fontSize = this.fontSize;
		}
		this.boxRect.height = this.style.CalcHeight(new GUIContent(this.cachedText), this.boxRect.width);
		GUI.Box(this.boxRect, string.Empty);
		GUI.Label(this.boxRect, this.cachedText, this.style);
	}

	// Token: 0x04000124 RID: 292
	public int yOffset = 5;

	// Token: 0x04000125 RID: 293
	public bool show = true;

	// Token: 0x04000126 RID: 294
	public bool showInEditor;

	// Token: 0x04000127 RID: 295
	public bool showFPS;

	// Token: 0x04000128 RID: 296
	public bool showPathProfile;

	// Token: 0x04000129 RID: 297
	public bool showMemProfile;

	// Token: 0x0400012A RID: 298
	public Font font;

	// Token: 0x0400012B RID: 299
	public int fontSize = 12;

	// Token: 0x0400012C RID: 300
	private StringBuilder text = new StringBuilder();

	// Token: 0x0400012D RID: 301
	private string cachedText;

	// Token: 0x0400012E RID: 302
	private float lastUpdate = -999f;

	// Token: 0x0400012F RID: 303
	private float delayedDeltaTime = 1f;

	// Token: 0x04000130 RID: 304
	private float lastCollect;

	// Token: 0x04000131 RID: 305
	private float lastCollectNum;

	// Token: 0x04000132 RID: 306
	private float delta;

	// Token: 0x04000133 RID: 307
	private float lastDeltaTime;

	// Token: 0x04000134 RID: 308
	private int allocRate;

	// Token: 0x04000135 RID: 309
	private int lastAllocMemory;

	// Token: 0x04000136 RID: 310
	private float lastAllocSet = -9999f;

	// Token: 0x04000137 RID: 311
	private int allocMem;

	// Token: 0x04000138 RID: 312
	private int collectAlloc;

	// Token: 0x04000139 RID: 313
	private int peakAlloc;

	// Token: 0x0400013A RID: 314
	private int lastFrameCount = -1;

	// Token: 0x0400013B RID: 315
	private int fpsDropCounterSize = 200;

	// Token: 0x0400013C RID: 316
	private float[] fpsDrops;

	// Token: 0x0400013D RID: 317
	private Rect boxRect;

	// Token: 0x0400013E RID: 318
	private GUIStyle style;

	// Token: 0x0400013F RID: 319
	private int maxVecPool;

	// Token: 0x04000140 RID: 320
	private int maxNodePool;

	// Token: 0x04000141 RID: 321
	private AstarDebugger.PathTypeDebug[] debugTypes;

	// Token: 0x04000142 RID: 322
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache0;

	// Token: 0x04000143 RID: 323
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache1;

	// Token: 0x04000144 RID: 324
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache2;

	// Token: 0x04000145 RID: 325
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache3;

	// Token: 0x04000146 RID: 326
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache4;

	// Token: 0x04000147 RID: 327
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache5;

	// Token: 0x04000148 RID: 328
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache6;

	// Token: 0x04000149 RID: 329
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache7;

	// Token: 0x0400014A RID: 330
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache8;

	// Token: 0x0400014B RID: 331
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache9;

	// Token: 0x0400014C RID: 332
	[CompilerGenerated]
	private static Func<int> <>f__mg$cacheA;

	// Token: 0x0400014D RID: 333
	[CompilerGenerated]
	private static Func<int> <>f__mg$cacheB;

	// Token: 0x0400014E RID: 334
	[CompilerGenerated]
	private static Func<int> <>f__mg$cacheC;

	// Token: 0x0400014F RID: 335
	[CompilerGenerated]
	private static Func<int> <>f__mg$cacheD;

	// Token: 0x02000029 RID: 41
	private struct PathTypeDebug
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000B658 File Offset: 0x00009A58
		public PathTypeDebug(string name, Func<int> getSize, Func<int> getTotalCreated)
		{
			this.name = name;
			this.getSize = getSize;
			this.getTotalCreated = getTotalCreated;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000B670 File Offset: 0x00009A70
		public void Print(StringBuilder text)
		{
			int num = this.getTotalCreated();
			if (num > 0)
			{
				text.Append("\n").Append(("  " + this.name).PadRight(25)).Append(this.getSize()).Append("/").Append(num);
			}
		}

		// Token: 0x04000150 RID: 336
		private string name;

		// Token: 0x04000151 RID: 337
		private Func<int> getSize;

		// Token: 0x04000152 RID: 338
		private Func<int> getTotalCreated;
	}
}
