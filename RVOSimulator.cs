using System;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[AddComponentMenu("Local Avoidance/RVO Simulator")]
public class RVOSimulator : MonoBehaviour
{
	// Token: 0x060005A9 RID: 1449 RVA: 0x000359F6 File Offset: 0x00033DF6
	public Simulator GetSimulator()
	{
		if (this.simulator == null)
		{
			this.Awake();
		}
		return this.simulator;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00035A10 File Offset: 0x00033E10
	private void Awake()
	{
		if (this.desiredSimulatonFPS < 1)
		{
			this.desiredSimulatonFPS = 1;
		}
		if (this.simulator == null)
		{
			int workers = AstarPath.CalculateThreadCount(this.workerThreads);
			this.simulator = new Simulator(workers, this.doubleBuffering);
			this.simulator.Interpolation = this.interpolation;
			this.simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulatonFPS;
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00035A84 File Offset: 0x00033E84
	private void Update()
	{
		if (this.desiredSimulatonFPS < 1)
		{
			this.desiredSimulatonFPS = 1;
		}
		this.GetSimulator().DesiredDeltaTime = 1f / (float)this.desiredSimulatonFPS;
		this.GetSimulator().Interpolation = this.interpolation;
		this.GetSimulator().Update();
	}

	// Token: 0x040004A5 RID: 1189
	public bool doubleBuffering = true;

	// Token: 0x040004A6 RID: 1190
	public bool interpolation = true;

	// Token: 0x040004A7 RID: 1191
	public int desiredSimulatonFPS = 20;

	// Token: 0x040004A8 RID: 1192
	public ThreadCount workerThreads = ThreadCount.Two;

	// Token: 0x040004A9 RID: 1193
	private Simulator simulator;
}
