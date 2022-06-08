using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000037 RID: 55
	public interface IAgent
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001E9 RID: 489
		Vector3 InterpolatedPosition { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001EA RID: 490
		// (set) Token: 0x060001EB RID: 491
		Vector3 Position { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001EC RID: 492
		// (set) Token: 0x060001ED RID: 493
		Vector3 DesiredVelocity { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001EE RID: 494
		// (set) Token: 0x060001EF RID: 495
		Vector3 Velocity { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001F0 RID: 496
		// (set) Token: 0x060001F1 RID: 497
		bool Locked { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001F2 RID: 498
		// (set) Token: 0x060001F3 RID: 499
		float Radius { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001F4 RID: 500
		// (set) Token: 0x060001F5 RID: 501
		float Height { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001F6 RID: 502
		// (set) Token: 0x060001F7 RID: 503
		float MaxSpeed { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001F8 RID: 504
		// (set) Token: 0x060001F9 RID: 505
		float NeighbourDist { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001FA RID: 506
		// (set) Token: 0x060001FB RID: 507
		float AgentTimeHorizon { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001FC RID: 508
		// (set) Token: 0x060001FD RID: 509
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001FE RID: 510
		// (set) Token: 0x060001FF RID: 511
		bool DebugDraw { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000200 RID: 512
		// (set) Token: 0x06000201 RID: 513
		int MaxNeighbours { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000202 RID: 514
		List<ObstacleVertex> NeighbourObstacles { get; }
	}
}
