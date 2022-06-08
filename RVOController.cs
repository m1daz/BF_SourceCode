using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[AddComponentMenu("Local Avoidance/RVO Controller")]
public class RVOController : MonoBehaviour
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000588 RID: 1416 RVA: 0x00034C38 File Offset: 0x00033038
	public Vector3 position
	{
		get
		{
			return this.rvoAgent.InterpolatedPosition;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000589 RID: 1417 RVA: 0x00034C45 File Offset: 0x00033045
	public Vector3 velocity
	{
		get
		{
			return this.rvoAgent.Velocity;
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00034C52 File Offset: 0x00033052
	public void OnDisable()
	{
		this.simulator.RemoveAgent(this.rvoAgent);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00034C68 File Offset: 0x00033068
	public void Awake()
	{
		this.tr = base.transform;
		RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
		if (rvosimulator == null)
		{
			Debug.LogError("No RVOSimulator component found in the scene. Please add one.");
			return;
		}
		this.simulator = rvosimulator.GetSimulator();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00034CBC File Offset: 0x000330BC
	public void OnEnable()
	{
		if (this.rvoAgent != null)
		{
			this.simulator.AddAgent(this.rvoAgent);
		}
		else
		{
			this.rvoAgent = this.simulator.AddAgent(base.transform.position);
		}
		this.UpdateAgentProperties();
		this.rvoAgent.Position = base.transform.position;
		this.adjustedY = this.rvoAgent.Position.y;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00034D3C File Offset: 0x0003313C
	protected void UpdateAgentProperties()
	{
		this.rvoAgent.Radius = this.radius;
		this.rvoAgent.MaxSpeed = this.maxSpeed;
		this.rvoAgent.Height = this.height;
		this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
		this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
		this.rvoAgent.Locked = this.locked;
		this.rvoAgent.DebugDraw = this.debug;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00034DC0 File Offset: 0x000331C0
	public void Move(Vector3 vel)
	{
		this.desiredVelocity = vel;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00034DC9 File Offset: 0x000331C9
	public void Teleport(Vector3 pos)
	{
		this.tr.position = pos;
		this.lastPosition = pos;
		this.rvoAgent.Position = pos;
		this.adjustedY = pos.y;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00034DF8 File Offset: 0x000331F8
	public void Update()
	{
		if (this.lastPosition != this.tr.position)
		{
			this.Teleport(this.tr.position);
		}
		this.UpdateAgentProperties();
		Vector3 interpolatedPosition = this.rvoAgent.InterpolatedPosition;
		interpolatedPosition.y = this.adjustedY;
		RaycastHit raycastHit;
		if (Physics.Raycast(interpolatedPosition + Vector3.up * this.height * 0.5f, Vector3.down, out raycastHit, float.PositiveInfinity, this.mask))
		{
			this.adjustedY = raycastHit.point.y;
		}
		else
		{
			this.adjustedY = 0f;
		}
		interpolatedPosition.y = this.adjustedY;
		this.rvoAgent.Position = new Vector3(this.rvoAgent.Position.x, this.adjustedY, this.rvoAgent.Position.z);
		List<ObstacleVertex> neighbourObstacles = this.rvoAgent.NeighbourObstacles;
		Vector3 a = Vector3.zero;
		for (int i = 0; i < neighbourObstacles.Count; i++)
		{
			Vector3 position = neighbourObstacles[i].position;
			Vector3 position2 = neighbourObstacles[i].next.position;
			Vector3 vector = this.position - Mathfx.NearestPointStrict(position, position2, this.position);
			if (!(vector == position) && !(vector == position2))
			{
				float sqrMagnitude = vector.sqrMagnitude;
				vector /= sqrMagnitude * this.falloff;
				a += vector;
			}
		}
		this.rvoAgent.DesiredVelocity = this.desiredVelocity + a * this.wallAvoidForce;
		this.tr.position = this.rvoAgent.InterpolatedPosition + Vector3.up * this.height * 0.5f + this.center;
		this.lastPosition = this.tr.position;
	}

	// Token: 0x04000485 RID: 1157
	public float radius = 5f;

	// Token: 0x04000486 RID: 1158
	public float maxSpeed = 2f;

	// Token: 0x04000487 RID: 1159
	public float height = 1f;

	// Token: 0x04000488 RID: 1160
	public bool locked;

	// Token: 0x04000489 RID: 1161
	public float agentTimeHorizon = 2f;

	// Token: 0x0400048A RID: 1162
	public float obstacleTimeHorizon = 2f;

	// Token: 0x0400048B RID: 1163
	public bool debug;

	// Token: 0x0400048C RID: 1164
	public LayerMask mask = -1;

	// Token: 0x0400048D RID: 1165
	public float wallAvoidForce = 1f;

	// Token: 0x0400048E RID: 1166
	public float falloff = 1f;

	// Token: 0x0400048F RID: 1167
	public Vector3 center;

	// Token: 0x04000490 RID: 1168
	private IAgent rvoAgent;

	// Token: 0x04000491 RID: 1169
	private Simulator simulator;

	// Token: 0x04000492 RID: 1170
	private float adjustedY;

	// Token: 0x04000493 RID: 1171
	private Transform tr;

	// Token: 0x04000494 RID: 1172
	private Vector3 desiredVelocity;

	// Token: 0x04000495 RID: 1173
	private Vector3 lastPosition;
}
