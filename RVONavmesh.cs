using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[AddComponentMenu("Local Avoidance/RVO Navmesh")]
public class RVONavmesh : GraphModifier
{
	// Token: 0x06000592 RID: 1426 RVA: 0x0003504A File Offset: 0x0003344A
	public override void OnPostCacheLoad()
	{
		this.OnLatePostScan();
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00035054 File Offset: 0x00033454
	public override void OnLatePostScan()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.RemoveObstacles();
		NavGraph[] graphs = AstarPath.active.graphs;
		RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
		if (rvosimulator == null)
		{
			throw new NullReferenceException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
		}
		Simulator simulator = rvosimulator.GetSimulator();
		for (int i = 0; i < graphs.Length; i++)
		{
			this.AddGraphObstacles(simulator, graphs[i]);
		}
		simulator.UpdateObstacles();
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000350D4 File Offset: 0x000334D4
	public void RemoveObstacles()
	{
		if (this.lastSim == null)
		{
			return;
		}
		Simulator simulator = this.lastSim;
		this.lastSim = null;
		for (int i = 0; i < this.obstacles.Count; i++)
		{
			simulator.RemoveObstacle(this.obstacles[i]);
		}
		this.obstacles.Clear();
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00035134 File Offset: 0x00033534
	public void AddGraphObstacles(Simulator sim, NavGraph graph)
	{
		if (this.obstacles.Count > 0 && this.lastSim != null && this.lastSim != sim)
		{
			Debug.LogError("Simulator has changed but some old obstacles are still added for the previous simulator. Deleting previous obstacles.");
			this.RemoveObstacles();
		}
		this.lastSim = sim;
		INavmesh navmesh = graph as INavmesh;
		if (navmesh == null)
		{
			return;
		}
		Node[] nodes = graph.nodes;
		Int3[] vertices = navmesh.vertices;
		int[] array = new int[3];
		for (int i = 0; i < nodes.Length; i++)
		{
			MeshNode meshNode = nodes[i] as MeshNode;
			array[0] = (array[1] = (array[2] = 0));
			if (meshNode != null)
			{
				for (int j = 0; j < meshNode.connections.Length; j++)
				{
					MeshNode meshNode2 = meshNode.connections[j] as MeshNode;
					if (meshNode2 != null)
					{
						int num = -1;
						int num2 = -1;
						for (int k = 0; k < 3; k++)
						{
							for (int l = 0; l < 3; l++)
							{
								if (meshNode[k] == meshNode2[l] && num < 0)
								{
									num = k;
									break;
								}
								if (meshNode[k] == meshNode2[l])
								{
									num2 = k;
									break;
								}
							}
							if (num2 >= 0)
							{
								break;
							}
						}
						if (num2 != -1)
						{
							if ((num + 1) % 3 == num2)
							{
								array[num]++;
							}
							else
							{
								array[num2]++;
							}
						}
					}
				}
			}
			for (int m = 0; m < 3; m++)
			{
				if (array[m] == 0)
				{
					Vector3 vector = (Vector3)vertices[meshNode[m]];
					Vector3 vector2 = (Vector3)vertices[meshNode[(m + 1) % 3]];
					if (!Polygon.IsClockwise(vector, vector2, (Vector3)vertices[meshNode[(m + 2) % 3]]))
					{
						Vector3 vector3 = vector2;
						vector2 = vector;
						vector = vector3;
					}
					float val = Math.Abs(vector.y - vector2.y);
					val = Math.Max(val, 5f);
					this.obstacles.Add(sim.AddObstacle(vector, vector2, this.wallHeight));
				}
			}
		}
	}

	// Token: 0x04000496 RID: 1174
	public float wallHeight = 5f;

	// Token: 0x04000497 RID: 1175
	private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

	// Token: 0x04000498 RID: 1176
	private Simulator lastSim;
}
