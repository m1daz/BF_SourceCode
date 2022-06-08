using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x02000058 RID: 88
[RequireComponent(typeof(MeshFilter))]
public class LightweightRVO : MonoBehaviour
{
	// Token: 0x060002F7 RID: 759 RVA: 0x00015DD4 File Offset: 0x000141D4
	public void Start()
	{
		this.mesh = new Mesh();
		RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
		if (rvosimulator == null)
		{
			Debug.LogError("No RVOSimulator could be found in the scene. Please add a RVOSimulator component to any GameObject");
			return;
		}
		this.sim = rvosimulator.GetSimulator();
		base.GetComponent<MeshFilter>().mesh = this.mesh;
		this.CreateAgents(this.agentCount);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00015E44 File Offset: 0x00014244
	public void OnGUI()
	{
		if (GUILayout.Button("2", new GUILayoutOption[0]))
		{
			this.CreateAgents(2);
		}
		if (GUILayout.Button("100", new GUILayoutOption[0]))
		{
			this.CreateAgents(100);
		}
		if (GUILayout.Button("1000", new GUILayoutOption[0]))
		{
			this.CreateAgents(1000);
		}
		if (GUILayout.Button("5000", new GUILayoutOption[0]))
		{
			this.CreateAgents(5000);
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00015ECC File Offset: 0x000142CC
	private float uniformDistance(float radius)
	{
		float num = UnityEngine.Random.value + UnityEngine.Random.value;
		if (num > 1f)
		{
			return radius * (2f - num);
		}
		return radius * num;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00015F00 File Offset: 0x00014300
	public void CreateAgents(int num)
	{
		this.agentCount = num;
		this.agents = new List<IAgent>(this.agentCount);
		this.goals = new List<Vector3>(this.agentCount);
		this.colors = new List<Color>(this.agentCount);
		this.sim.ClearAgents();
		if (this.type == LightweightRVO.RVOExampleType.Circle)
		{
			float d = Mathf.Sqrt((float)this.agentCount * this.radius * this.radius * 4f / 3.1415927f) * this.exampleScale * 0.05f;
			for (int i = 0; i < this.agentCount; i++)
			{
				Vector3 vector = new Vector3(Mathf.Cos((float)i * 3.1415927f * 2f / (float)this.agentCount), 0f, Mathf.Sin((float)i * 3.1415927f * 2f / (float)this.agentCount)) * d;
				IAgent item = this.sim.AddAgent(vector);
				this.agents.Add(item);
				this.goals.Add(-vector);
				this.colors.Add(LightweightRVO.HSVToRGB((float)i * 360f / (float)this.agentCount, 1f, 1f));
			}
		}
		else if (this.type == LightweightRVO.RVOExampleType.Line)
		{
			for (int j = 0; j < this.agentCount; j++)
			{
				Vector3 position = new Vector3((float)((j % 2 != 0) ? -1 : 1) * this.exampleScale, 0f, (float)(j / 2) * this.radius * 2.5f);
				IAgent item2 = this.sim.AddAgent(position);
				this.agents.Add(item2);
				this.goals.Add(new Vector3(-position.x, position.y, position.z));
				this.colors.Add((j % 2 != 0) ? Color.blue : Color.red);
			}
		}
		else if (this.type == LightweightRVO.RVOExampleType.Point)
		{
			for (int k = 0; k < this.agentCount; k++)
			{
				Vector3 position2 = new Vector3(Mathf.Cos((float)k * 3.1415927f * 2f / (float)this.agentCount), 0f, Mathf.Sin((float)k * 3.1415927f * 2f / (float)this.agentCount)) * this.exampleScale;
				IAgent item3 = this.sim.AddAgent(position2);
				this.agents.Add(item3);
				this.goals.Add(new Vector3(0f, position2.y, 0f));
				this.colors.Add(LightweightRVO.HSVToRGB((float)k * 360f / (float)this.agentCount, 1f, 1f));
			}
		}
		else if (this.type == LightweightRVO.RVOExampleType.RandomStreams)
		{
			float num2 = Mathf.Sqrt((float)this.agentCount * this.radius * this.radius * 4f / 3.1415927f) * this.exampleScale * 0.05f;
			for (int l = 0; l < this.agentCount; l++)
			{
				float f = UnityEngine.Random.value * 3.1415927f * 2f;
				float num3 = UnityEngine.Random.value * 3.1415927f * 2f;
				Vector3 position3 = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * this.uniformDistance(num2);
				IAgent item4 = this.sim.AddAgent(position3);
				this.agents.Add(item4);
				this.goals.Add(new Vector3(Mathf.Cos(num3), 0f, Mathf.Sin(num3)) * this.uniformDistance(num2));
				this.colors.Add(LightweightRVO.HSVToRGB(num3 * 57.29578f, 1f, 1f));
			}
		}
		for (int m = 0; m < this.agents.Count; m++)
		{
			IAgent agent = this.agents[m];
			agent.Radius = this.radius;
			agent.MaxSpeed = this.maxSpeed;
			agent.AgentTimeHorizon = this.agentTimeHorizon;
			agent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			agent.MaxNeighbours = this.maxNeighbours;
			agent.NeighbourDist = this.neighbourDist;
		}
		this.verts = new Vector3[4 * this.agents.Count];
		this.uv = new Vector2[this.verts.Length];
		this.tris = new int[this.agents.Count * 2 * 3];
		this.meshColors = new Color[this.verts.Length];
	}

	// Token: 0x060002FB RID: 763 RVA: 0x000163E0 File Offset: 0x000147E0
	public void Update()
	{
		if (this.agents == null || this.mesh == null)
		{
			return;
		}
		if (this.agents.Count != this.goals.Count)
		{
			Debug.LogError("Agent count does not match goal count");
			return;
		}
		for (int i = 0; i < this.agents.Count; i++)
		{
			Vector3 interpolatedPosition = this.agents[i].InterpolatedPosition;
			Vector3 vector = this.goals[i] - interpolatedPosition;
			vector = Vector3.ClampMagnitude(vector, 1f);
			this.agents[i].DesiredVelocity = vector * this.agents[i].MaxSpeed;
		}
		for (int j = 0; j < this.agents.Count; j++)
		{
			IAgent agent = this.agents[j];
			Vector3 vector2 = agent.Velocity.normalized * agent.Radius;
			if (vector2 == Vector3.zero)
			{
				vector2 = new Vector3(0f, 0f, agent.Radius);
			}
			Vector3 b = Vector3.Cross(Vector3.up, vector2);
			Vector3 a = agent.InterpolatedPosition + this.renderingOffset;
			int num = 4 * j;
			int num2 = 6 * j;
			this.verts[num] = a + vector2 - b;
			this.verts[num + 1] = a + vector2 + b;
			this.verts[num + 2] = a - vector2 + b;
			this.verts[num + 3] = a - vector2 - b;
			this.uv[num] = new Vector2(0f, 1f);
			this.uv[num + 1] = new Vector2(1f, 1f);
			this.uv[num + 2] = new Vector2(1f, 0f);
			this.uv[num + 3] = new Vector2(0f, 0f);
			this.meshColors[num] = this.colors[j];
			this.meshColors[num + 1] = this.colors[j];
			this.meshColors[num + 2] = this.colors[j];
			this.meshColors[num + 3] = this.colors[j];
			this.tris[num2] = num;
			this.tris[num2 + 1] = num + 1;
			this.tris[num2 + 2] = num + 2;
			this.tris[num2 + 3] = num;
			this.tris[num2 + 4] = num + 2;
			this.tris[num2 + 5] = num + 3;
		}
		this.mesh.Clear();
		this.mesh.vertices = this.verts;
		this.mesh.uv = this.uv;
		this.mesh.colors = this.meshColors;
		this.mesh.triangles = this.tris;
		this.mesh.RecalculateNormals();
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00016788 File Offset: 0x00014B88
	private static Color HSVToRGB(float h, float s, float v)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = s * v;
		float num5 = h / 60f;
		float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
		if (num5 < 1f)
		{
			num = num4;
			num2 = num6;
		}
		else if (num5 < 2f)
		{
			num = num6;
			num2 = num4;
		}
		else if (num5 < 3f)
		{
			num2 = num4;
			num3 = num6;
		}
		else if (num5 < 4f)
		{
			num2 = num6;
			num3 = num4;
		}
		else if (num5 < 5f)
		{
			num = num6;
			num3 = num4;
		}
		else if (num5 < 6f)
		{
			num = num4;
			num3 = num6;
		}
		float num7 = v - num4;
		num += num7;
		num2 += num7;
		num3 += num7;
		return new Color(num, num2, num3);
	}

	// Token: 0x04000244 RID: 580
	public int agentCount = 100;

	// Token: 0x04000245 RID: 581
	public float exampleScale = 100f;

	// Token: 0x04000246 RID: 582
	public LightweightRVO.RVOExampleType type;

	// Token: 0x04000247 RID: 583
	public float radius = 3f;

	// Token: 0x04000248 RID: 584
	public float maxSpeed = 2f;

	// Token: 0x04000249 RID: 585
	public float agentTimeHorizon = 10f;

	// Token: 0x0400024A RID: 586
	public float obstacleTimeHorizon = 10f;

	// Token: 0x0400024B RID: 587
	public int maxNeighbours = 10;

	// Token: 0x0400024C RID: 588
	public float neighbourDist = 15f;

	// Token: 0x0400024D RID: 589
	public Vector3 renderingOffset = Vector3.up * 0.1f;

	// Token: 0x0400024E RID: 590
	private Mesh mesh;

	// Token: 0x0400024F RID: 591
	private Simulator sim;

	// Token: 0x04000250 RID: 592
	private List<IAgent> agents;

	// Token: 0x04000251 RID: 593
	private List<Vector3> goals;

	// Token: 0x04000252 RID: 594
	private List<Color> colors;

	// Token: 0x04000253 RID: 595
	private Vector3[] verts;

	// Token: 0x04000254 RID: 596
	private Vector2[] uv;

	// Token: 0x04000255 RID: 597
	private int[] tris;

	// Token: 0x04000256 RID: 598
	private Color[] meshColors;

	// Token: 0x02000059 RID: 89
	public enum RVOExampleType
	{
		// Token: 0x04000258 RID: 600
		Circle,
		// Token: 0x04000259 RID: 601
		Line,
		// Token: 0x0400025A RID: 602
		Point,
		// Token: 0x0400025B RID: 603
		RandomStreams
	}
}
