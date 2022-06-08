using System;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x0200031F RID: 799
	[AddComponentMenu("")]
	public class SpeedHackDetector : MonoBehaviour
	{
		// Token: 0x060018C3 RID: 6339 RVA: 0x000CEC89 File Offset: 0x000CD089
		private SpeedHackDetector()
		{
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x000CECB4 File Offset: 0x000CD0B4
		public static SpeedHackDetector Instance
		{
			get
			{
				if (SpeedHackDetector.instance == null)
				{
					SpeedHackDetector speedHackDetector = (SpeedHackDetector)UnityEngine.Object.FindObjectOfType(typeof(SpeedHackDetector));
					if (speedHackDetector == null)
					{
						GameObject gameObject = new GameObject("Speed Hack Detector");
						speedHackDetector = gameObject.AddComponent<SpeedHackDetector>();
					}
					return speedHackDetector;
				}
				return SpeedHackDetector.instance;
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000CED0B File Offset: 0x000CD10B
		public static void Dispose()
		{
			SpeedHackDetector.Instance.DisposeInternal();
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000CED17 File Offset: 0x000CD117
		private void DisposeInternal()
		{
			this.StopMonitoringInternal();
			SpeedHackDetector.instance = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000CED30 File Offset: 0x000CD130
		private void Awake()
		{
			if (!this.IsPlacedCorrectly())
			{
				Debug.LogWarning("[ACT] Speed Hack Detector is placed in scene incorrectly and will be auto-destroyed! Please, use \"GameObject->Create Other->Code Stage->Anti-Cheat Toolkit->Speed Hack Detector\" menu to correct this!");
				UnityEngine.Object.Destroy(this);
				return;
			}
			SpeedHackDetector.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000CED5F File Offset: 0x000CD15F
		private bool IsPlacedCorrectly()
		{
			return base.name == "Speed Hack Detector" && base.GetComponentsInChildren<Component>().Length == 2 && base.transform.childCount == 0;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000CED95 File Offset: 0x000CD195
		private void OnLevelWasLoaded(int index)
		{
			if (!this.keepAlive)
			{
				SpeedHackDetector.Dispose();
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000CEDA7 File Offset: 0x000CD1A7
		private void OnDisable()
		{
			this.StopMonitoringInternal();
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000CEDAF File Offset: 0x000CD1AF
		private void OnApplicationQuit()
		{
			this.DisposeInternal();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000CEDB7 File Offset: 0x000CD1B7
		public static void StartDetection(Action callback)
		{
			SpeedHackDetector.StartDetection(callback, SpeedHackDetector.Instance.interval);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000CEDC9 File Offset: 0x000CD1C9
		public static void StartDetection(Action callback, float checkInterval)
		{
			SpeedHackDetector.StartDetection(callback, checkInterval, SpeedHackDetector.Instance.maxFalsePositives);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000CEDDC File Offset: 0x000CD1DC
		public static void StartDetection(Action callback, float checkInterval, byte maxErrors)
		{
			if (SpeedHackDetector.Instance.running)
			{
				Debug.LogWarning("[ACT] Speed Hack Detector already running!");
				return;
			}
			SpeedHackDetector.Instance.StartDetectionInternal(callback, checkInterval, maxErrors);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000CEE08 File Offset: 0x000CD208
		private void StartDetectionInternal(Action callback, float checkInterval, byte maxErrors)
		{
			this.onSpeedHackDetected = callback;
			this.interval = checkInterval;
			this.maxFalsePositives = maxErrors;
			this.ticksOnStart = DateTime.UtcNow.Ticks;
			this.ticksOnStartVulnerable = (long)Environment.TickCount * 10000L;
			base.InvokeRepeating("OnTimer", checkInterval, checkInterval);
			this.errorsCount = 0;
			this.running = true;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000CEE6B File Offset: 0x000CD26B
		public static void StopMonitoring()
		{
			SpeedHackDetector.Instance.StopMonitoringInternal();
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000CEE77 File Offset: 0x000CD277
		private void StopMonitoringInternal()
		{
			if (this.running)
			{
				base.CancelInvoke("OnTimer");
				this.onSpeedHackDetected = null;
				this.running = false;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000CEEA0 File Offset: 0x000CD2A0
		private void OnTimer()
		{
			long ticks = DateTime.UtcNow.Ticks;
			long num = (long)Environment.TickCount * 10000L;
			if (Mathf.Abs((float)(num - this.ticksOnStartVulnerable - (ticks - this.ticksOnStart))) > 5000000f)
			{
				this.errorsCount++;
				Debug.LogWarning("[ACT] SpeedHackDetector: detection! Silent detections left: " + ((int)this.maxFalsePositives - this.errorsCount));
				if (this.errorsCount > (int)this.maxFalsePositives)
				{
					if (this.onSpeedHackDetected != null)
					{
						this.onSpeedHackDetected();
					}
					if (this.autoDispose)
					{
						SpeedHackDetector.Dispose();
					}
					else
					{
						SpeedHackDetector.StopMonitoring();
					}
				}
			}
		}

		// Token: 0x04001BDE RID: 7134
		private const string COMPONENT_NAME = "Speed Hack Detector";

		// Token: 0x04001BDF RID: 7135
		private const int THRESHOLD = 5000000;

		// Token: 0x04001BE0 RID: 7136
		private static SpeedHackDetector instance;

		// Token: 0x04001BE1 RID: 7137
		public bool autoDispose = true;

		// Token: 0x04001BE2 RID: 7138
		public bool keepAlive = true;

		// Token: 0x04001BE3 RID: 7139
		public Action onSpeedHackDetected;

		// Token: 0x04001BE4 RID: 7140
		public float interval = 1f;

		// Token: 0x04001BE5 RID: 7141
		public byte maxFalsePositives = 3;

		// Token: 0x04001BE6 RID: 7142
		private int errorsCount;

		// Token: 0x04001BE7 RID: 7143
		private long ticksOnStart;

		// Token: 0x04001BE8 RID: 7144
		private long ticksOnStartVulnerable;

		// Token: 0x04001BE9 RID: 7145
		private bool running;
	}
}
