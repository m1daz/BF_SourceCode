using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x02000024 RID: 36
[AddComponentMenu("Pathfinding/Pathfinder")]
public class AstarPath : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007226 File Offset: 0x00005626
	public static Version Version
	{
		get
		{
			return new Version(3, 2, 4);
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007230 File Offset: 0x00005630
	public Type[] graphTypes
	{
		get
		{
			return this.astarData.graphTypes;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000723D File Offset: 0x0000563D
	// (set) Token: 0x060000F3 RID: 243 RVA: 0x00007260 File Offset: 0x00005660
	public NavGraph[] graphs
	{
		get
		{
			if (this.astarData == null)
			{
				this.astarData = new AstarData();
			}
			return this.astarData.graphs;
		}
		set
		{
			if (this.astarData == null)
			{
				this.astarData = new AstarData();
			}
			this.astarData.graphs = value;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x00007284 File Offset: 0x00005684
	public float maxNearestNodeDistanceSqr
	{
		get
		{
			return this.maxNearestNodeDistance * this.maxNearestNodeDistance;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007293 File Offset: 0x00005693
	public NodeRunData debugPathData
	{
		get
		{
			if (this.debugPath == null)
			{
				return null;
			}
			return this.debugPath.runData;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000F6 RID: 246 RVA: 0x000072AD File Offset: 0x000056AD
	public static int ActiveThreadsCount
	{
		get
		{
			return AstarPath.numActiveThreads;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x000072B4 File Offset: 0x000056B4
	public static int NumParallelThreads
	{
		get
		{
			return (AstarPath.threadInfos == null) ? 0 : AstarPath.threadInfos.Length;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000F8 RID: 248 RVA: 0x000072D0 File Offset: 0x000056D0
	public static bool IsUsingMultithreading
	{
		get
		{
			if (AstarPath.threads != null && AstarPath.threads.Length > 0)
			{
				return true;
			}
			if (AstarPath.threads != null && AstarPath.threads.Length == 0 && AstarPath.threadEnumerator != null)
			{
				return false;
			}
			throw new Exception("Not 'using threading' and not 'not using threading'... Are you sure pathfinding is set up correctly?\nIf scripts are reloaded in unity editor during play this could happen.");
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007322 File Offset: 0x00005722
	public bool IsAnyGraphUpdatesQueued
	{
		get
		{
			return this.graphUpdateQueue != null && this.graphUpdateQueue.Count > 0;
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00007340 File Offset: 0x00005740
	private static void ResetQueueStates()
	{
		AstarPath.pathQueueFlag.Reset();
		AstarPath.threadSafeUpdateFlag.Reset();
		AstarPath.safeUpdateFlag.Reset();
		AstarPath.threadSafeUpdateState = false;
		AstarPath.doSetQueueState = true;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000736F File Offset: 0x0000576F
	private static void TrickAbortThreads()
	{
		AstarPath.active.acceptNewPaths = false;
		AstarPath.pathQueueFlag.Set();
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00007388 File Offset: 0x00005788
	public string[] GetTagNames()
	{
		if (this.tagNames == null || this.tagNames.Length != 32)
		{
			this.tagNames = new string[32];
			for (int i = 0; i < this.tagNames.Length; i++)
			{
				this.tagNames[i] = string.Empty + i;
			}
			this.tagNames[0] = "Basic Ground";
		}
		return this.tagNames;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00007400 File Offset: 0x00005800
	public static string[] FindTagNames()
	{
		if (AstarPath.active != null)
		{
			return AstarPath.active.GetTagNames();
		}
		AstarPath astarPath = UnityEngine.Object.FindObjectOfType(typeof(AstarPath)) as AstarPath;
		if (astarPath != null)
		{
			AstarPath.active = astarPath;
			return astarPath.GetTagNames();
		}
		return new string[]
		{
			"There is no AstarPath component in the scene"
		};
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00007464 File Offset: 0x00005864
	public ushort GetNextPathID()
	{
		if (this.nextFreePathID == 0)
		{
			this.nextFreePathID += 1;
			Debug.Log("65K cleanup");
			if (AstarPath.On65KOverflow != null)
			{
				OnVoidDelegate on65KOverflow = AstarPath.On65KOverflow;
				AstarPath.On65KOverflow = null;
				on65KOverflow();
			}
		}
		ushort result;
		this.nextFreePathID = (result = this.nextFreePathID) + 1;
		return result;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000074C4 File Offset: 0x000058C4
	private void OnDrawGizmos()
	{
		if (AstarPath.active == null)
		{
			AstarPath.active = this;
		}
		else if (AstarPath.active != this)
		{
			return;
		}
		if (this.graphs == null)
		{
			return;
		}
		for (int i = 0; i < this.graphs.Length; i++)
		{
			if (this.graphs[i] != null)
			{
				if (this.graphs[i].drawGizmos)
				{
					this.graphs[i].OnDrawGizmos(this.showNavGraphs);
				}
			}
		}
		if (this.showUnwalkableNodes && this.showNavGraphs)
		{
			Gizmos.color = AstarColor.UnwalkableNode;
			for (int j = 0; j < this.graphs.Length; j++)
			{
				if (this.graphs[j] != null && this.graphs[j].nodes != null)
				{
					Node[] nodes = this.graphs[j].nodes;
					for (int k = 0; k < nodes.Length; k++)
					{
						if (nodes[k] != null && !nodes[k].walkable)
						{
							Gizmos.DrawCube((Vector3)nodes[k].position, Vector3.one * this.unwalkableNodeDebugSize);
						}
					}
				}
			}
		}
		if (this.OnDrawGizmosCallback != null)
		{
			this.OnDrawGizmosCallback();
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00007628 File Offset: 0x00005A28
	private void OnGUI()
	{
		if (Input.GetKey("l"))
		{
			GUI.Label(new Rect((float)(Screen.width - 100), 5f, 100f, 25f), (1f / Time.smoothDeltaTime).ToString("0") + " fps");
		}
		if (this.logPathResults == PathLog.InGame && this.inGameDebugPath != string.Empty)
		{
			GUI.Label(new Rect(5f, 5f, 400f, 600f), this.inGameDebugPath);
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000076D0 File Offset: 0x00005AD0
	private static void AstarLog(string s)
	{
		if (AstarPath.active == null)
		{
			Debug.Log("No AstarPath object was found : " + s);
			return;
		}
		if (AstarPath.active.logPathResults != PathLog.None && AstarPath.active.logPathResults != PathLog.OnlyErrors)
		{
			Debug.Log(s);
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00007723 File Offset: 0x00005B23
	private static void AstarLogError(string s)
	{
		if (AstarPath.active == null)
		{
			Debug.Log("No AstarPath object was found : " + s);
			return;
		}
		if (AstarPath.active.logPathResults != PathLog.None)
		{
			Debug.LogError(s);
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000775C File Offset: 0x00005B5C
	private void LogPathResults(Path p)
	{
		if (this.logPathResults == PathLog.None || (this.logPathResults == PathLog.OnlyErrors && !p.error))
		{
			return;
		}
		string text = p.DebugString(this.logPathResults);
		if (this.logPathResults == PathLog.InGame)
		{
			this.inGameDebugPath = text;
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000077B1 File Offset: 0x00005BB1
	private void Update()
	{
		AstarPath.TryCallThreadSafeCallbacks();
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000077B8 File Offset: 0x00005BB8
	private static void TryCallThreadSafeCallbacks()
	{
		if (AstarPath.threadSafeUpdateState)
		{
			if (AstarPath.OnThreadSafeCallback != null)
			{
				OnVoidDelegate onThreadSafeCallback = AstarPath.OnThreadSafeCallback;
				AstarPath.OnThreadSafeCallback = null;
				onThreadSafeCallback();
			}
			AstarPath.threadSafeUpdateFlag.Set();
			AstarPath.threadSafeUpdateState = false;
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x000077FC File Offset: 0x00005BFC
	public static void ForceCallThreadSafeCallbacks()
	{
		if (!AstarPath.threadSafeUpdateState)
		{
			throw new InvalidOperationException("You should only call this function from a thread safe callback. That does not seem to be the case for this call.");
		}
		if (AstarPath.OnThreadSafeCallback != null)
		{
			OnVoidDelegate onThreadSafeCallback = AstarPath.OnThreadSafeCallback;
			AstarPath.OnThreadSafeCallback = null;
			onThreadSafeCallback();
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000783A File Offset: 0x00005C3A
	public void QueueGraphUpdates()
	{
		if (!this.isRegisteredForUpdate)
		{
			this.isRegisteredForUpdate = true;
			AstarPath.RegisterSafeUpdate(new OnVoidDelegate(this.DoUpdateGraphs), true);
		}
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00007860 File Offset: 0x00005C60
	private IEnumerator DelayedGraphUpdate()
	{
		this.graphUpdateRoutineRunning = true;
		yield return new WaitForSeconds(this.maxGraphUpdateFreq - (Time.time - this.lastGraphUpdate));
		this.QueueGraphUpdates();
		this.graphUpdateRoutineRunning = false;
		yield break;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000787B File Offset: 0x00005C7B
	[Obsolete("Use GraphUpdateUtilities.UpdateGraphsNoBlock instead")]
	public bool WillBlockPath(GraphUpdateObject ob, Node n1, Node n2)
	{
		return GraphUpdateUtilities.UpdateGraphsNoBlock(ob, n1, n2, false);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00007886 File Offset: 0x00005C86
	[Obsolete("Use GraphUpdateUtilities.IsPathPossible instead")]
	public static bool IsPathPossible(Node n1, Node n2)
	{
		return n1.area == n2.area;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00007896 File Offset: 0x00005C96
	public void UpdateGraphs(Bounds bounds, float t)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds), t);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x000078A5 File Offset: 0x00005CA5
	public void UpdateGraphs(GraphUpdateObject ob, float t)
	{
		base.StartCoroutine(this.UpdateGraphsInteral(ob, t));
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000078B8 File Offset: 0x00005CB8
	private IEnumerator UpdateGraphsInteral(GraphUpdateObject ob, float t)
	{
		yield return new WaitForSeconds(t);
		this.UpdateGraphs(ob);
		yield break;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x000078E1 File Offset: 0x00005CE1
	public void UpdateGraphs(Bounds bounds)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds));
	}

	// Token: 0x0600010F RID: 271 RVA: 0x000078F0 File Offset: 0x00005CF0
	public void UpdateGraphs(GraphUpdateObject ob)
	{
		if (this.graphUpdateQueue == null)
		{
			this.graphUpdateQueue = new Queue<GraphUpdateObject>();
		}
		this.graphUpdateQueue.Enqueue(ob);
		if (this.isUpdatingGraphs)
		{
			return;
		}
		if (this.isScanning)
		{
			this.DoUpdateGraphs();
			return;
		}
		if (this.limitGraphUpdates && Time.time - this.lastGraphUpdate < this.maxGraphUpdateFreq)
		{
			if (!this.graphUpdateRoutineRunning)
			{
				base.StartCoroutine(this.DelayedGraphUpdate());
			}
		}
		else
		{
			this.QueueGraphUpdates();
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00007982 File Offset: 0x00005D82
	public void FlushGraphUpdates()
	{
		if (this.IsAnyGraphUpdatesQueued)
		{
			this.QueueGraphUpdates();
			this.FlushThreadSafeCallbacks();
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000799C File Offset: 0x00005D9C
	private void DoUpdateGraphs()
	{
		this.isRegisteredForUpdate = false;
		this.isUpdatingGraphs = true;
		this.lastGraphUpdate = Time.time;
		if (this.OnGraphsWillBeUpdated2 != null)
		{
			OnVoidDelegate onGraphsWillBeUpdated = this.OnGraphsWillBeUpdated2;
			this.OnGraphsWillBeUpdated2 = null;
			onGraphsWillBeUpdated();
		}
		if (this.OnGraphsWillBeUpdated != null)
		{
			OnVoidDelegate onGraphsWillBeUpdated2 = this.OnGraphsWillBeUpdated;
			this.OnGraphsWillBeUpdated = null;
			onGraphsWillBeUpdated2();
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
		bool flag = false;
		if (this.graphUpdateQueue != null)
		{
			while (this.graphUpdateQueue.Count > 0)
			{
				GraphUpdateObject graphUpdateObject = this.graphUpdateQueue.Dequeue();
				if (graphUpdateObject.requiresFloodFill)
				{
					flag = true;
				}
				IEnumerator enumerator = this.astarData.GetUpdateableGraphs().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						IUpdatableGraph updatableGraph = (IUpdatableGraph)obj;
						NavGraph graph = updatableGraph as NavGraph;
						if (graphUpdateObject.nnConstraint == null || graphUpdateObject.nnConstraint.SuitableGraph(AstarPath.active.astarData.GetGraphIndex(graph), graph))
						{
							updatableGraph.UpdateArea(graphUpdateObject);
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		this.isUpdatingGraphs = false;
		if (flag && !this.isScanning)
		{
			this.FloodFill();
		}
		if (AstarPath.OnGraphsUpdated != null && !this.isScanning)
		{
			AstarPath.OnGraphsUpdated(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00007B24 File Offset: 0x00005F24
	public void FlushThreadSafeCallbacks()
	{
		if (AstarPath.OnThreadSafeCallback == null)
		{
			return;
		}
		bool flag = true;
		if (AstarPath.IsUsingMultithreading)
		{
			int i = 0;
			while (i < AstarPath.threadInfos.Length)
			{
				bool flag2 = false;
				while (!flag2)
				{
					if (AstarPath.threadSafeUpdateState)
					{
						break;
					}
					flag2 = Monitor.TryEnter(AstarPath.threadInfos[i].Lock, 10);
				}
				if (AstarPath.threadSafeUpdateState)
				{
					if (i != 0 || flag2)
					{
						throw new Exception(string.Concat(new object[]
						{
							"Wait wut! This should not happen! ",
							i,
							" ",
							flag2
						}));
					}
					flag = false;
					break;
				}
				else
				{
					i++;
				}
			}
			AstarPath.threadSafeUpdateState = true;
		}
		else
		{
			while (!AstarPath.threadSafeUpdateState && AstarPath.threadEnumerator.MoveNext())
			{
			}
		}
		AstarPath.TryCallThreadSafeCallbacks();
		AstarPath.doSetQueueState = true;
		AstarPath.pathQueueFlag.Set();
		if (AstarPath.IsUsingMultithreading && flag)
		{
			for (int j = 0; j < AstarPath.threadInfos.Length; j++)
			{
				Monitor.Exit(AstarPath.threadInfos[j].Lock);
			}
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00007C60 File Offset: 0x00006060
	public void LogProfiler()
	{
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00007C62 File Offset: 0x00006062
	public void ResetProfiler()
	{
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00007C64 File Offset: 0x00006064
	public static int CalculateThreadCount(ThreadCount count)
	{
		if (count != ThreadCount.Automatic)
		{
			return (int)count;
		}
		int num = SystemInfo.processorCount;
		int systemMemorySize = SystemInfo.systemMemorySize;
		if (num <= 1)
		{
			return 0;
		}
		if (systemMemorySize <= 512)
		{
			return 0;
		}
		if (systemMemorySize <= 1024)
		{
			num = Math.Min(num, 2);
		}
		return Math.Min(num, 6);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00007CBC File Offset: 0x000060BC
	public void Awake()
	{
		AstarPath.active = this;
		if (UnityEngine.Object.FindObjectsOfType(typeof(AstarPath)).Length > 1)
		{
			Debug.LogError("You should NOT have more than one AstarPath component in the scene at any time.\nThis can cause serious errors since the AstarPath component builds around a singleton pattern.");
		}
		base.useGUILayout = false;
		if (AstarPath.OnAwakeSettings != null)
		{
			AstarPath.OnAwakeSettings();
		}
		int num = AstarPath.CalculateThreadCount(this.threadCount);
		AstarPath.threads = new Thread[num];
		AstarPath.threadInfos = new PathThreadInfo[Math.Max(num, 1)];
		for (int i = 0; i < AstarPath.threadInfos.Length; i++)
		{
			AstarPath.threadInfos[i] = new PathThreadInfo(i, this, new NodeRunData());
		}
		for (int j = 0; j < AstarPath.threads.Length; j++)
		{
			AstarPath.threads[j] = new Thread(new ParameterizedThreadStart(AstarPath.CalculatePathsThreaded));
			AstarPath.threads[j].IsBackground = true;
		}
		this.Initialize();
		base.StartCoroutine(this.ReturnsPathsHandler());
		if (this.scanOnStartup && (!this.astarData.cacheStartup || this.astarData.data_cachedStartup == null))
		{
			this.Scan();
		}
		this.UpdatePathThreadInfoNodes();
		if (AstarPath.threads.Length > 0)
		{
			Thread thread = new Thread(new ParameterizedThreadStart(AstarPath.LockThread));
			thread.Start(this);
		}
		for (int k = 0; k < AstarPath.threads.Length; k++)
		{
			if (this.logPathResults == PathLog.Heavy)
			{
				Debug.Log("Starting pathfinding thread " + k);
			}
			AstarPath.threads[k].Start(AstarPath.threadInfos[k]);
		}
		if (AstarPath.threads.Length == 0)
		{
			base.StartCoroutine(AstarPath.CalculatePathsHandler(AstarPath.threadInfos[0]));
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00007EA4 File Offset: 0x000062A4
	public void DataUpdate()
	{
		if (AstarPath.active != this)
		{
			throw new Exception("Singleton pattern broken. Make sure you only have one AstarPath object in the scene");
		}
		if (this.astarData == null)
		{
			throw new NullReferenceException("AstarData is null... Astar not set up correctly?");
		}
		if (this.astarData.graphs == null)
		{
			this.astarData.graphs = new NavGraph[0];
		}
		this.astarData.AssignNodeIndices();
		if (Application.isPlaying)
		{
			this.astarData.CreateNodeRuns(AstarPath.threadInfos.Length);
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00007F2C File Offset: 0x0000632C
	public void UpdatePathThreadInfoNodes()
	{
		for (int i = 0; i < AstarPath.threadInfos.Length; i++)
		{
			PathThreadInfo pathThreadInfo = AstarPath.threadInfos[i];
			if (pathThreadInfo.threadIndex != i)
			{
				throw new Exception(string.Concat(new object[]
				{
					"threadInfos[",
					i,
					"] did not have a matching index member. Expected ",
					i,
					" found ",
					pathThreadInfo.threadIndex
				}));
			}
			NodeRunData runData = pathThreadInfo.runData;
			if (runData == null)
			{
				throw new NullReferenceException("A thread info.node run data was null");
			}
			runData.nodes = this.astarData.nodeRuns[i];
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00007FE8 File Offset: 0x000063E8
	public void SetUpReferences()
	{
		AstarPath.active = this;
		if (this.astarData == null)
		{
			this.astarData = new AstarData();
		}
		if (this.astarData.userConnections == null)
		{
			this.astarData.userConnections = new UserConnection[0];
		}
		if (this.colorSettings == null)
		{
			this.colorSettings = new AstarColor();
		}
		this.colorSettings.OnEnable();
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00008054 File Offset: 0x00006454
	private void Initialize()
	{
		this.SetUpReferences();
		this.astarData.FindGraphTypes();
		this.astarData.Awake();
		for (int i = 0; i < this.astarData.graphs.Length; i++)
		{
			this.astarData.graphs[i].Awake();
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000080B0 File Offset: 0x000064B0
	public void OnDestroy()
	{
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("+++ AstarPath Component Destroyed - Cleaning Up Pathfinding Data +++");
		}
		AstarPath.TrickAbortThreads();
		if (AstarPath.threads != null)
		{
			for (int i = 0; i < AstarPath.threads.Length; i++)
			{
				if (!AstarPath.threads[i].Join(50))
				{
					Debug.LogError("Could not terminate pathfinding thread[" + i + "] in 50ms, trying Thread.Abort");
					AstarPath.threads[i].Abort();
				}
			}
		}
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Destroying Graphs");
		}
		if (this.astarData.graphs != null)
		{
			for (int j = 0; j < this.astarData.graphs.Length; j++)
			{
				this.astarData.graphs[j].OnDestroy();
			}
		}
		this.astarData.graphs = null;
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Returning Paths");
		}
		this.ReturnPaths(false);
		AstarPath.pathReturnStack.PopAll();
		if (this.logPathResults == PathLog.Heavy)
		{
			Debug.Log("Cleaning up variables");
		}
		this.floodStack = null;
		this.graphUpdateQueue = null;
		this.OnDrawGizmosCallback = null;
		AstarPath.OnAwakeSettings = null;
		AstarPath.OnGraphPreScan = null;
		AstarPath.OnGraphPostScan = null;
		AstarPath.OnPathPreSearch = null;
		AstarPath.OnPathPostSearch = null;
		AstarPath.OnPreScan = null;
		AstarPath.OnPostScan = null;
		AstarPath.OnLatePostScan = null;
		AstarPath.On65KOverflow = null;
		AstarPath.OnGraphsUpdated = null;
		AstarPath.OnSafeCallback = null;
		AstarPath.OnThreadSafeCallback = null;
		AstarPath.pathQueue.Clear();
		AstarPath.threads = null;
		AstarPath.threadInfos = null;
		AstarPath.numActiveThreads = 0;
		AstarPath.ResetQueueStates();
		AstarPath.PathsCompleted = 0;
		AstarPath.active = null;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000825D File Offset: 0x0000665D
	public void FloodFill(Node seed)
	{
		this.FloodFill(seed, this.lastUniqueAreaIndex + 1);
		this.lastUniqueAreaIndex++;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000827C File Offset: 0x0000667C
	public void FloodFill(Node seed, int area)
	{
		if (area > 255)
		{
			Debug.LogError("Too high area index - The maximum area index is 255");
			return;
		}
		if (area < 0)
		{
			Debug.LogError("Too low area index - The minimum area index is 0");
			return;
		}
		if (this.floodStack == null)
		{
			this.floodStack = new Stack<Node>(1024);
		}
		Stack<Node> stack = this.floodStack;
		stack.Clear();
		stack.Push(seed);
		seed.area = area;
		while (stack.Count > 0)
		{
			stack.Pop().FloodFill(stack, area);
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00008308 File Offset: 0x00006708
	public void FloodFill()
	{
		if (this.astarData.graphs == null)
		{
			return;
		}
		int num = 0;
		this.lastUniqueAreaIndex = 0;
		if (this.floodStack == null)
		{
			this.floodStack = new Stack<Node>(1024);
		}
		Stack<Node> stack = this.floodStack;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			if (navGraph.nodes != null)
			{
				for (int j = 0; j < navGraph.nodes.Length; j++)
				{
					if (navGraph.nodes[j] != null)
					{
						navGraph.nodes[j].area = 0;
					}
				}
			}
		}
		int num2 = 0;
		for (int k = 0; k < this.graphs.Length; k++)
		{
			NavGraph navGraph2 = this.graphs[k];
			if (navGraph2.nodes == null)
			{
				Debug.LogWarning("Graph " + k + " has not defined any nodes");
			}
			else
			{
				for (int l = 0; l < navGraph2.nodes.Length; l++)
				{
					if (navGraph2.nodes[l] != null && navGraph2.nodes[l].walkable && navGraph2.nodes[l].area == 0)
					{
						num++;
						if (num > 255)
						{
							Debug.LogError("Too many areas - The maximum number of areas is 256");
							num--;
							break;
						}
						stack.Clear();
						stack.Push(navGraph2.nodes[l]);
						int num3 = 1;
						navGraph2.nodes[l].area = num;
						while (stack.Count > 0)
						{
							num3++;
							stack.Pop().FloodFill(stack, num);
						}
						if (num3 < this.minAreaSize)
						{
							stack.Clear();
							stack.Push(navGraph2.nodes[l]);
							navGraph2.nodes[l].area = 254;
							while (stack.Count > 0)
							{
								stack.Pop().FloodFill(stack, 254);
							}
							num2++;
							num--;
						}
					}
				}
			}
		}
		this.lastUniqueAreaIndex = num;
		if (num2 > 0)
		{
			AstarPath.AstarLog(string.Concat(new object[]
			{
				num2,
				" small areas were detected (fewer than ",
				this.minAreaSize,
				" nodes),these might have the same IDs as other areas, but it shouldn't affect pathfinding in any significant way (you might get All Nodes Searched as a reason for path failure).\nWhich areas are defined as 'small' is controlled by the 'Min Area Size' variable, it can be changed in the A* inspector-->Settings-->Min Area Size\nThe small areas will use the area id 254"
			}));
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00008580 File Offset: 0x00006980
	public void Scan()
	{
		IEnumerator<Progress> enumerator = this.ScanLoop().GetEnumerator();
		while (enumerator.MoveNext())
		{
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000085AC File Offset: 0x000069AC
	public IEnumerable<Progress> ScanLoop()
	{
		if (this.graphs == null)
		{
			yield break;
		}
		this.isScanning = true;
		yield return new Progress(0.02f, "Updating graph shortcuts");
		this.astarData.UpdateShortcuts();
		yield return new Progress(0.05f, "Pre processing graphs");
		if (AstarPath.OnPreScan != null)
		{
			AstarPath.OnPreScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreScan);
		DateTime startTime = DateTime.UtcNow;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph graph = this.graphs[i];
			if (AstarPath.OnGraphPreScan != null)
			{
				yield return new Progress(Mathfx.MapTo(0.05f, 0.7f, ((float)i + 0.5f) / (float)(this.graphs.Length + 1)), string.Concat(new object[]
				{
					"Scanning graph ",
					i + 1,
					" of ",
					this.graphs.Length,
					" - Pre processing"
				}));
				AstarPath.OnGraphPreScan(graph);
			}
			yield return new Progress(Mathfx.MapTo(0.05f, 0.7f, ((float)i + 1f) / (float)(this.graphs.Length + 1)), string.Concat(new object[]
			{
				"Scanning graph ",
				i + 1,
				" of ",
				this.graphs.Length
			}));
			graph.Scan();
			yield return new Progress(Mathfx.MapTo(0.05f, 0.7f, ((float)i + 1.1f) / (float)(this.graphs.Length + 1)), string.Concat(new object[]
			{
				"Scanning graph ",
				i + 1,
				" of ",
				this.graphs.Length,
				" - Assigning graph indices"
			}));
			if (graph.nodes != null)
			{
				for (int j = 0; j < graph.nodes.Length; j++)
				{
					if (graph.nodes[j] != null)
					{
						graph.nodes[j].graphIndex = i;
					}
				}
			}
			if (AstarPath.OnGraphPostScan != null)
			{
				yield return new Progress(Mathfx.MapTo(0.05f, 0.7f, ((float)i + 1.5f) / (float)(this.graphs.Length + 1)), string.Concat(new object[]
				{
					"Scanning graph ",
					i + 1,
					" of ",
					this.graphs.Length,
					" - Post processing"
				}));
				AstarPath.OnGraphPostScan(graph);
			}
		}
		yield return new Progress(0.8f, "Post processing graphs");
		if (AstarPath.OnPostScan != null)
		{
			AstarPath.OnPostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostScan);
		this.isScanning = false;
		yield return new Progress(0.85f, "Applying links");
		this.ApplyLinks();
		yield return new Progress(0.9f, "Computing areas");
		this.FloodFill();
		yield return new Progress(0.92f, "Updating misc. data");
		this.DataUpdate();
		yield return new Progress(0.95f, "Late post processing");
		if (AstarPath.OnLatePostScan != null)
		{
			AstarPath.OnLatePostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.LatePostScan);
		this.lastScanTime = (float)(DateTime.UtcNow - startTime).TotalSeconds;
		GC.Collect();
		AstarPath.AstarLog("Scanning - Process took " + (this.lastScanTime * 1000f).ToString("0") + " ms to complete ");
		yield break;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x000085CF File Offset: 0x000069CF
	public void NodeCountChanged()
	{
		if (Application.isPlaying)
		{
			this.astarData.CreateNodeRuns(Math.Max(AstarPath.threads.Length, 1));
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x000085F4 File Offset: 0x000069F4
	public void ApplyLinks()
	{
		for (int i = 0; i < this.astarData.userConnections.Length; i++)
		{
			UserConnection userConnection = this.astarData.userConnections[i];
			if (userConnection.type == ConnectionType.Connection)
			{
				Node node = this.GetNearest(userConnection.p1).node;
				Node node2 = this.GetNearest(userConnection.p2).node;
				if (node != null && node2 != null)
				{
					int cost = (!userConnection.doOverrideCost) ? (node.position - node2.position).costMagnitude : userConnection.overrideCost;
					if (userConnection.enable)
					{
						node.AddConnection(node2, cost);
						if (!userConnection.oneWay)
						{
							node2.AddConnection(node, cost);
						}
					}
					else
					{
						node.RemoveConnection(node2);
						if (!userConnection.oneWay)
						{
							node2.RemoveConnection(node);
						}
					}
				}
			}
			else
			{
				Node node3 = this.GetNearest(userConnection.p1).node;
				if (node3 != null)
				{
					if (userConnection.doOverrideWalkability)
					{
						node3.walkable = userConnection.enable;
						if (!node3.walkable)
						{
							node3.UpdateNeighbourConnections();
							node3.UpdateConnections();
						}
					}
					if (userConnection.doOverridePenalty)
					{
						node3.penalty = userConnection.overridePenalty;
					}
				}
			}
		}
		NodeLink[] array = UnityEngine.Object.FindObjectsOfType(typeof(NodeLink)) as NodeLink[];
		for (int j = 0; j < array.Length; j++)
		{
			array[j].Apply();
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000087A0 File Offset: 0x00006BA0
	public static void WaitForPath(Path p)
	{
		if (AstarPath.active == null)
		{
			throw new Exception("Pathfinding is not correctly initialized in this scene (yet?). AstarPath.active is null.\nDo not call this function in Awake");
		}
		if (p == null)
		{
			throw new ArgumentNullException("Path must not be null");
		}
		if (!AstarPath.active.acceptNewPaths)
		{
			return;
		}
		if (p.GetState() == PathState.Created)
		{
			throw new Exception("The specified path has not been started yet.");
		}
		AstarPath.waitForPathDepth++;
		if (AstarPath.waitForPathDepth == 5)
		{
			Debug.LogError("You are calling the WaitForPath function recursively (maybe from a path callback). Please don't do this.");
		}
		if (p.GetState() < PathState.ReturnQueue)
		{
			if (AstarPath.IsUsingMultithreading)
			{
				while (p.GetState() < PathState.ReturnQueue)
				{
					if (AstarPath.ActiveThreadsCount == 0)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Pathfinding Threads seems to have crashed. No threads are running.");
					}
					Thread.Sleep(1);
					AstarPath.TryCallThreadSafeCallbacks();
				}
			}
			else
			{
				while (p.GetState() < PathState.ReturnQueue)
				{
					if (AstarPath.pathQueue.Count == 0 && p.GetState() != PathState.Processing)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Critical error. Path Queue is empty but the path state is '" + p.GetState() + "'");
					}
					AstarPath.threadEnumerator.MoveNext();
					AstarPath.TryCallThreadSafeCallbacks();
				}
			}
		}
		AstarPath.active.ReturnPaths(false);
		AstarPath.waitForPathDepth--;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000088F8 File Offset: 0x00006CF8
	public static void RegisterSafeUpdate(OnVoidDelegate callback, bool threadSafe)
	{
		if (callback == null || !Application.isPlaying)
		{
			return;
		}
		if (AstarPath.threadSafeUpdateState)
		{
			callback();
			return;
		}
		if (AstarPath.IsUsingMultithreading)
		{
			int num = 0;
			for (int i = 0; i < AstarPath.threadInfos.Length; i++)
			{
				if (!Monitor.TryEnter(AstarPath.threadInfos[i].Lock))
				{
					break;
				}
				num = i;
			}
			if (num == AstarPath.threadInfos.Length - 1)
			{
				AstarPath.threadSafeUpdateState = true;
				callback();
				AstarPath.threadSafeUpdateState = false;
			}
			for (int j = 0; j <= num; j++)
			{
				Monitor.Exit(AstarPath.threadInfos[j].Lock);
			}
			if (num != AstarPath.threadInfos.Length - 1)
			{
				AstarPath.doSetQueueState = false;
				AstarPath.pathQueueFlag.Reset();
				object obj = AstarPath.safeUpdateLock;
				lock (obj)
				{
					if (threadSafe)
					{
						AstarPath.OnThreadSafeCallback = (OnVoidDelegate)Delegate.Combine(AstarPath.OnThreadSafeCallback, callback);
					}
					else
					{
						AstarPath.OnSafeCallback = (OnVoidDelegate)Delegate.Combine(AstarPath.OnSafeCallback, callback);
					}
					AstarPath.safeUpdateFlag.Set();
				}
			}
		}
		else if (AstarPath.threadSafeUpdateState)
		{
			callback();
		}
		else
		{
			object obj2 = AstarPath.safeUpdateLock;
			lock (obj2)
			{
				if (threadSafe)
				{
					AstarPath.OnThreadSafeCallback = (OnVoidDelegate)Delegate.Combine(AstarPath.OnThreadSafeCallback, callback);
				}
				else
				{
					AstarPath.OnSafeCallback = (OnVoidDelegate)Delegate.Combine(AstarPath.OnSafeCallback, callback);
				}
			}
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00008AB8 File Offset: 0x00006EB8
	public static void StartPath(Path p)
	{
		if (AstarPath.active == null)
		{
			Debug.LogError("There is no AstarPath object in the scene");
			return;
		}
		if (p.GetState() != PathState.Created)
		{
			throw new Exception(string.Concat(new object[]
			{
				"The path has an invalid state. Expected ",
				PathState.Created,
				" found ",
				p.GetState(),
				"\nMake sure you are not requesting the same path twice"
			}));
		}
		if (!AstarPath.active.acceptNewPaths)
		{
			p.Error();
			p.LogError("No new paths are accepted");
			return;
		}
		if (AstarPath.active.graphs == null || AstarPath.active.graphs.Length == 0)
		{
			Debug.LogError("There are no graphs in the scene");
			p.Error();
			p.LogError("There are no graphs in the scene");
			Debug.LogError(p.errorLog);
			return;
		}
		p.Claim(AstarPath.active);
		object obj = AstarPath.pathQueue;
		lock (obj)
		{
			p.AdvanceState(PathState.PathQueue);
			AstarPath.pathQueue.Enqueue(p);
			if (AstarPath.doSetQueueState)
			{
				AstarPath.pathQueueFlag.Set();
			}
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00008BF0 File Offset: 0x00006FF0
	public void OnApplicationQuit()
	{
		if (AstarPath.threads == null)
		{
			return;
		}
		for (int i = 0; i < AstarPath.threads.Length; i++)
		{
			AstarPath.threads[i].Abort();
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00008C2C File Offset: 0x0000702C
	public IEnumerator ReturnsPathsHandler()
	{
		for (;;)
		{
			this.ReturnPaths(true);
			yield return 0;
		}
		yield break;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00008C48 File Offset: 0x00007048
	public void ReturnPaths(bool timeSlice)
	{
		Path next = AstarPath.pathReturnStack.PopAll();
		if (this.pathReturnPop == null)
		{
			this.pathReturnPop = next;
		}
		else
		{
			Path next2 = this.pathReturnPop;
			while (next2.next != null)
			{
				next2 = next2.next;
			}
			next2.next = next;
		}
		long num = (!timeSlice) ? 0L : (DateTime.UtcNow.Ticks + 5000L);
		int num2 = 0;
		while (this.pathReturnPop != null)
		{
			Path path = this.pathReturnPop;
			this.pathReturnPop = this.pathReturnPop.next;
			path.next = null;
			path.ReturnPath();
			path.AdvanceState(PathState.Returned);
			path.ReleaseSilent(this);
			num2++;
			if (num2 > 5 && timeSlice)
			{
				num2 = 0;
				if (DateTime.UtcNow.Ticks >= num)
				{
					return;
				}
			}
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00008D38 File Offset: 0x00007138
	private static void LockThread(object _astar)
	{
		AstarPath astarPath = (AstarPath)_astar;
		while (astarPath.acceptNewPaths)
		{
			AstarPath.safeUpdateFlag.WaitOne();
			PathThreadInfo[] array = AstarPath.threadInfos;
			if (array == null)
			{
				Debug.LogError("Path Thread Infos are null");
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				Monitor.Enter(array[i].Lock);
			}
			object obj = AstarPath.safeUpdateLock;
			lock (obj)
			{
				AstarPath.safeUpdateFlag.Reset();
				OnVoidDelegate onSafeCallback = AstarPath.OnSafeCallback;
				AstarPath.OnSafeCallback = null;
				if (onSafeCallback != null)
				{
					onSafeCallback();
				}
				if (AstarPath.OnThreadSafeCallback != null)
				{
					AstarPath.threadSafeUpdateFlag.Reset();
				}
				else
				{
					AstarPath.threadSafeUpdateFlag.Set();
				}
			}
			AstarPath.threadSafeUpdateState = true;
			AstarPath.threadSafeUpdateFlag.WaitOne();
			AstarPath.doSetQueueState = true;
			AstarPath.pathQueueFlag.Set();
			for (int j = 0; j < array.Length; j++)
			{
				Monitor.Exit(array[j].Lock);
			}
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00008E64 File Offset: 0x00007264
	private static void CalculatePathsThreaded(object _threadInfo)
	{
		Interlocked.Increment(ref AstarPath.numActiveThreads);
		PathThreadInfo pathThreadInfo;
		try
		{
			pathThreadInfo = (PathThreadInfo)_threadInfo;
		}
		catch (Exception ex)
		{
			Debug.LogError("Arguments to pathfinding threads must be of type ThreadStartInfo\n" + ex);
			throw new ArgumentException("Argument must be of type ThreadStartInfo", ex);
		}
		AstarPath astar = pathThreadInfo.astar;
		try
		{
			NodeRunData runData = pathThreadInfo.runData;
			if (runData.nodes == null)
			{
				throw new NullReferenceException("NodeRuns must be assigned to the threadInfo.runData.nodes field before threads are started\nthreadInfo is an argument to the thread functions");
			}
			long num = (long)(astar.maxFrameTime * 10000f);
			long num2 = DateTime.UtcNow.Ticks + num;
			for (;;)
			{
				IL_7C:
				Path path = null;
				while (astar.acceptNewPaths)
				{
					AstarPath.pathQueueFlag.WaitOne();
					if (!astar.acceptNewPaths)
					{
						goto Block_8;
					}
					object obj = AstarPath.pathQueue;
					lock (obj)
					{
						if (AstarPath.pathQueue.Count > 0)
						{
							path = AstarPath.pathQueue.Dequeue();
							goto IL_10C;
						}
						AstarPath.pathQueueFlag.Reset();
					}
					continue;
					IL_10C:
					Monitor.Enter(pathThreadInfo.Lock);
					num = (long)(astar.maxFrameTime * 10000f);
					path.PrepareBase(runData);
					path.AdvanceState(PathState.Processing);
					if (AstarPath.OnPathPreSearch != null)
					{
						AstarPath.OnPathPreSearch(path);
					}
					long ticks = DateTime.UtcNow.Ticks;
					long num3 = 0L;
					path.Prepare();
					if (!path.IsDone())
					{
						astar.debugPath = path;
						path.Initialize();
						while (!path.IsDone())
						{
							path.CalculateStep(num2);
							path.searchIterations++;
							if (path.IsDone())
							{
								break;
							}
							num3 += DateTime.UtcNow.Ticks - ticks;
							Thread.Sleep(0);
							ticks = DateTime.UtcNow.Ticks;
							num2 = ticks + num;
							if (!astar.acceptNewPaths)
							{
								path.Error();
							}
						}
						num3 += DateTime.UtcNow.Ticks - ticks;
						path.duration = (float)num3 * 0.0001f;
					}
					astar.LogPathResults(path);
					if (AstarPath.OnPathPostSearch != null)
					{
						AstarPath.OnPathPostSearch(path);
					}
					AstarPath.pathReturnStack.Push(path);
					path.AdvanceState(PathState.ReturnQueue);
					Monitor.Exit(pathThreadInfo.Lock);
					if (DateTime.UtcNow.Ticks > num2)
					{
						Thread.Sleep(1);
						num2 = DateTime.UtcNow.Ticks + num;
					}
					goto IL_7C;
				}
				break;
			}
			Interlocked.Decrement(ref AstarPath.numActiveThreads);
			return;
			Block_8:
			Interlocked.Decrement(ref AstarPath.numActiveThreads);
			return;
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException)
			{
				if (astar.logPathResults == PathLog.Heavy)
				{
					Debug.LogWarning("Shutting down pathfinding thread #" + pathThreadInfo.threadIndex + " with Thread.Abort call");
				}
				Interlocked.Decrement(ref AstarPath.numActiveThreads);
				return;
			}
			Debug.LogError(ex2);
		}
		Debug.LogError("Error : This part should never be reached");
		Interlocked.Decrement(ref AstarPath.numActiveThreads);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000091C4 File Offset: 0x000075C4
	private static IEnumerator CalculatePathsHandler(object _threadData)
	{
		AstarPath.threadEnumerator = AstarPath.CalculatePaths(_threadData);
		while (AstarPath.threadEnumerator.MoveNext())
		{
			yield return 0;
		}
		yield break;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x000091E0 File Offset: 0x000075E0
	private static IEnumerator CalculatePaths(object _threadInfo)
	{
		Interlocked.Increment(ref AstarPath.numActiveThreads);
		PathThreadInfo threadInfo;
		try
		{
			threadInfo = (PathThreadInfo)_threadInfo;
		}
		catch (Exception ex)
		{
			Debug.LogError("Arguments to pathfinding threads must be of type ThreadStartInfo\n" + ex);
			throw new ArgumentException("Argument must be of type ThreadStartInfo", ex);
		}
		int numPaths = 0;
		NodeRunData runData = threadInfo.runData;
		if (runData.nodes == null)
		{
			throw new NullReferenceException("NodeRuns must be assigned to the threadInfo.runData.nodes field before threads are started\nthreadInfo is an argument to the thread functions");
		}
		long maxTicks = (long)(AstarPath.active.maxFrameTime * 10000f);
		long targetTick = DateTime.UtcNow.Ticks + maxTicks;
		AstarPath.threadSafeUpdateState = true;
		for (;;)
		{
			IL_D1:
			Path p = null;
			while (AstarPath.active.acceptNewPaths)
			{
				if (AstarPath.pathQueue.Count > 0)
				{
					p = AstarPath.pathQueue.Dequeue();
				}
				OnVoidDelegate tmp = AstarPath.OnSafeCallback;
				AstarPath.OnSafeCallback = null;
				if (tmp != null)
				{
					tmp();
				}
				AstarPath.TryCallThreadSafeCallbacks();
				AstarPath.threadSafeUpdateState = true;
				if (p == null)
				{
					yield return 0;
				}
				if (p != null)
				{
					maxTicks = (long)(AstarPath.active.maxFrameTime * 10000f);
					AstarPath.threadSafeUpdateState = false;
					p.PrepareBase(runData);
					p.AdvanceState(PathState.Processing);
					if (AstarPath.OnPathPreSearch != null)
					{
						AstarPath.OnPathPreSearch(p);
					}
					numPaths++;
					long startTicks = DateTime.UtcNow.Ticks;
					long totalTicks = 0L;
					p.Prepare();
					if (!p.IsDone())
					{
						AstarPath.active.debugPath = p;
						p.Initialize();
						while (!p.IsDone())
						{
							p.CalculateStep(targetTick);
							p.searchIterations++;
							if (p.IsDone())
							{
								break;
							}
							totalTicks += DateTime.UtcNow.Ticks - startTicks;
							yield return 0;
							startTicks = DateTime.UtcNow.Ticks;
							if (!AstarPath.active.acceptNewPaths)
							{
								p.Error();
							}
							targetTick = DateTime.UtcNow.Ticks + maxTicks;
						}
						totalTicks += DateTime.UtcNow.Ticks - startTicks;
						p.duration = (float)totalTicks * 0.0001f;
					}
					AstarPath.active.LogPathResults(p);
					if (AstarPath.OnPathPostSearch != null)
					{
						AstarPath.OnPathPostSearch(p);
					}
					AstarPath.pathReturnStack.Push(p);
					p.AdvanceState(PathState.ReturnQueue);
					AstarPath.threadSafeUpdateState = true;
					if (DateTime.UtcNow.Ticks > targetTick)
					{
						yield return 0;
						targetTick = DateTime.UtcNow.Ticks + maxTicks;
						numPaths = 0;
					}
					goto IL_D1;
				}
			}
			break;
		}
		Interlocked.Decrement(ref AstarPath.numActiveThreads);
		yield break;
		yield break;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000091FB File Offset: 0x000075FB
	public NNInfo GetNearest(Vector3 position)
	{
		return this.GetNearest(position, NNConstraint.None);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00009209 File Offset: 0x00007609
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint)
	{
		return this.GetNearest(position, constraint, null);
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00009214 File Offset: 0x00007614
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint, Node hint)
	{
		if (this.graphs == null)
		{
			return default(NNInfo);
		}
		if (constraint == null)
		{
			constraint = NNConstraint.None;
		}
		float num = float.PositiveInfinity;
		NNInfo result = default(NNInfo);
		int num2 = -1;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			if (constraint.SuitableGraph(i, navGraph))
			{
				NNInfo nninfo;
				if (this.fullGetNearestSearch)
				{
					nninfo = navGraph.GetNearestForce(position, constraint);
				}
				else
				{
					nninfo = navGraph.GetNearest(position, constraint);
				}
				if (nninfo.node != null)
				{
					float magnitude = (nninfo.clampedPosition - position).magnitude;
					if (this.prioritizeGraphs && magnitude < this.prioritizeGraphsLimit)
					{
						result = nninfo;
						num2 = i;
						break;
					}
					if (magnitude < num)
					{
						num = magnitude;
						result = nninfo;
						num2 = i;
					}
				}
			}
		}
		if (num2 == -1)
		{
			return result;
		}
		if (result.constrainedNode != null)
		{
			result.node = result.constrainedNode;
			result.clampedPosition = result.constClampedPosition;
		}
		if (!this.fullGetNearestSearch && result.node != null && !constraint.Suitable(result.node))
		{
			NNInfo nearestForce = this.graphs[num2].GetNearestForce(position, constraint);
			if (nearestForce.node != null)
			{
				result = nearestForce;
			}
		}
		if (!constraint.Suitable(result.node) || (constraint.constrainDistance && (result.clampedPosition - position).sqrMagnitude > this.maxNearestNodeDistanceSqr))
		{
			return default(NNInfo);
		}
		return result;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000093E0 File Offset: 0x000077E0
	public Node GetNearest(Ray ray)
	{
		if (this.graphs == null)
		{
			return null;
		}
		float num = float.PositiveInfinity;
		Node result = null;
		Vector3 direction = ray.direction;
		Vector3 origin = ray.origin;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			Node[] nodes = navGraph.nodes;
			if (nodes != null)
			{
				foreach (Node node in nodes)
				{
					if (node != null)
					{
						Vector3 vector = (Vector3)node.position;
						Vector3 a = origin + Vector3.Dot(vector - origin, direction) * direction;
						float num2 = Mathf.Abs(a.x - vector.x);
						num2 *= num2;
						if (num2 <= num)
						{
							num2 = Mathf.Abs(a.z - vector.z);
							num2 *= num2;
							if (num2 <= num)
							{
								float sqrMagnitude = (a - vector).sqrMagnitude;
								if (sqrMagnitude < num)
								{
									num = sqrMagnitude;
									result = node;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x040000C0 RID: 192
	public static readonly AstarPath.AstarDistribution Distribution = AstarPath.AstarDistribution.AssetStore;

	// Token: 0x040000C1 RID: 193
	public static readonly bool HasPro = true;

	// Token: 0x040000C2 RID: 194
	public AstarData astarData;

	// Token: 0x040000C3 RID: 195
	public static AstarPath active;

	// Token: 0x040000C4 RID: 196
	public bool showNavGraphs = true;

	// Token: 0x040000C5 RID: 197
	public bool showUnwalkableNodes = true;

	// Token: 0x040000C6 RID: 198
	public GraphDebugMode debugMode;

	// Token: 0x040000C7 RID: 199
	public float debugFloor;

	// Token: 0x040000C8 RID: 200
	public float debugRoof = 20000f;

	// Token: 0x040000C9 RID: 201
	public bool showSearchTree;

	// Token: 0x040000CA RID: 202
	public float unwalkableNodeDebugSize = 0.3f;

	// Token: 0x040000CB RID: 203
	public bool stepByStep = true;

	// Token: 0x040000CC RID: 204
	public PathLog logPathResults = PathLog.Normal;

	// Token: 0x040000CD RID: 205
	public float maxNearestNodeDistance = 100f;

	// Token: 0x040000CE RID: 206
	public bool scanOnStartup = true;

	// Token: 0x040000CF RID: 207
	public bool fullGetNearestSearch;

	// Token: 0x040000D0 RID: 208
	public bool prioritizeGraphs;

	// Token: 0x040000D1 RID: 209
	public float prioritizeGraphsLimit = 1f;

	// Token: 0x040000D2 RID: 210
	public AstarColor colorSettings;

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	protected string[] tagNames;

	// Token: 0x040000D4 RID: 212
	public Heuristic heuristic = Heuristic.Euclidean;

	// Token: 0x040000D5 RID: 213
	public float heuristicScale = 1f;

	// Token: 0x040000D6 RID: 214
	public ThreadCount threadCount;

	// Token: 0x040000D7 RID: 215
	public float maxFrameTime = 1f;

	// Token: 0x040000D8 RID: 216
	public const int InitialBinaryHeapSize = 512;

	// Token: 0x040000D9 RID: 217
	public bool recyclePaths;

	// Token: 0x040000DA RID: 218
	public int minAreaSize = 10;

	// Token: 0x040000DB RID: 219
	public bool limitGraphUpdates = true;

	// Token: 0x040000DC RID: 220
	public float maxGraphUpdateFreq = 0.2f;

	// Token: 0x040000DD RID: 221
	public static int PathsCompleted = 0;

	// Token: 0x040000DE RID: 222
	public static long TotalSearchedNodes = 0L;

	// Token: 0x040000DF RID: 223
	public static long TotalSearchTime = 0L;

	// Token: 0x040000E0 RID: 224
	public float lastScanTime;

	// Token: 0x040000E1 RID: 225
	public Path debugPath;

	// Token: 0x040000E2 RID: 226
	public string inGameDebugPath;

	// Token: 0x040000E3 RID: 227
	public bool isScanning;

	// Token: 0x040000E4 RID: 228
	private bool acceptNewPaths = true;

	// Token: 0x040000E5 RID: 229
	private static int numActiveThreads = 0;

	// Token: 0x040000E6 RID: 230
	private bool graphUpdateRoutineRunning;

	// Token: 0x040000E7 RID: 231
	private bool isUpdatingGraphs;

	// Token: 0x040000E8 RID: 232
	private bool isRegisteredForUpdate;

	// Token: 0x040000E9 RID: 233
	public static OnVoidDelegate OnAwakeSettings;

	// Token: 0x040000EA RID: 234
	public static OnGraphDelegate OnGraphPreScan;

	// Token: 0x040000EB RID: 235
	public static OnGraphDelegate OnGraphPostScan;

	// Token: 0x040000EC RID: 236
	public static OnPathDelegate OnPathPreSearch;

	// Token: 0x040000ED RID: 237
	public static OnPathDelegate OnPathPostSearch;

	// Token: 0x040000EE RID: 238
	public static OnScanDelegate OnPreScan;

	// Token: 0x040000EF RID: 239
	public static OnScanDelegate OnPostScan;

	// Token: 0x040000F0 RID: 240
	public static OnScanDelegate OnLatePostScan;

	// Token: 0x040000F1 RID: 241
	public static OnScanDelegate OnGraphsUpdated;

	// Token: 0x040000F2 RID: 242
	public static OnVoidDelegate On65KOverflow;

	// Token: 0x040000F3 RID: 243
	private static OnVoidDelegate OnSafeCallback;

	// Token: 0x040000F4 RID: 244
	private static OnVoidDelegate OnThreadSafeCallback;

	// Token: 0x040000F5 RID: 245
	public OnVoidDelegate OnDrawGizmosCallback;

	// Token: 0x040000F6 RID: 246
	public OnVoidDelegate OnGraphsWillBeUpdated;

	// Token: 0x040000F7 RID: 247
	public OnVoidDelegate OnGraphsWillBeUpdated2;

	// Token: 0x040000F8 RID: 248
	[NonSerialized]
	public Queue<GraphUpdateObject> graphUpdateQueue;

	// Token: 0x040000F9 RID: 249
	[NonSerialized]
	public Stack<Node> floodStack;

	// Token: 0x040000FA RID: 250
	public static Queue<Path> pathQueue = new Queue<Path>();

	// Token: 0x040000FB RID: 251
	public static Thread[] threads;

	// Token: 0x040000FC RID: 252
	public static PathThreadInfo[] threadInfos;

	// Token: 0x040000FD RID: 253
	public static IEnumerator threadEnumerator;

	// Token: 0x040000FE RID: 254
	public static LockFreeStack pathReturnStack = new LockFreeStack();

	// Token: 0x040000FF RID: 255
	public static Stack<Path> PathPool;

	// Token: 0x04000100 RID: 256
	public bool showGraphs;

	// Token: 0x04000101 RID: 257
	public int lastUniqueAreaIndex;

	// Token: 0x04000102 RID: 258
	private static readonly ManualResetEvent pathQueueFlag = new ManualResetEvent(false);

	// Token: 0x04000103 RID: 259
	private static readonly ManualResetEvent threadSafeUpdateFlag = new ManualResetEvent(false);

	// Token: 0x04000104 RID: 260
	private static readonly ManualResetEvent safeUpdateFlag = new ManualResetEvent(false);

	// Token: 0x04000105 RID: 261
	private static bool threadSafeUpdateState = false;

	// Token: 0x04000106 RID: 262
	private static readonly object safeUpdateLock = new object();

	// Token: 0x04000107 RID: 263
	private static bool doSetQueueState = true;

	// Token: 0x04000108 RID: 264
	private float lastGraphUpdate = -9999f;

	// Token: 0x04000109 RID: 265
	private ushort nextFreePathID = 1;

	// Token: 0x0400010A RID: 266
	private static int waitForPathDepth = 0;

	// Token: 0x0400010B RID: 267
	private Path pathReturnPop;

	// Token: 0x02000025 RID: 37
	public enum AstarDistribution
	{
		// Token: 0x0400010D RID: 269
		WebsiteDownload,
		// Token: 0x0400010E RID: 270
		AssetStore
	}
}
