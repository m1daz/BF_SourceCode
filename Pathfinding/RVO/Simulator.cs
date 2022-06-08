using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200003A RID: 58
	public class Simulator
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000F948 File Offset: 0x0000DD48
		public Simulator(int workers, bool doubleBuffering)
		{
			this.workers = new Simulator.Worker[workers];
			this.doubleBuffering = doubleBuffering;
			for (int i = 0; i < workers; i++)
			{
				this.workers[i] = new Simulator.Worker(this);
			}
			this.kdTree = new KDTree(this);
			this.agents = new List<Agent>();
			this.obstacles = new List<ObstacleVertex>();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000F9E0 File Offset: 0x0000DDE0
		public KDTree KDTree
		{
			get
			{
				return this.kdTree;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000F9E8 File Offset: 0x0000DDE8
		public float FrameDeltaTime
		{
			get
			{
				return this.frameDeltaTime;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000F9F0 File Offset: 0x0000DDF0
		public float DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000F9F8 File Offset: 0x0000DDF8
		public float PrevDeltaTime
		{
			get
			{
				return this.prevDeltaTime;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000FA00 File Offset: 0x0000DE00
		public bool Multithreading
		{
			get
			{
				return this.workers != null && this.workers.Length > 0;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000FA1B File Offset: 0x0000DE1B
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000FA23 File Offset: 0x0000DE23
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000FA36 File Offset: 0x0000DE36
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000FA3E File Offset: 0x0000DE3E
		public bool Interpolation
		{
			get
			{
				return this.interpolation;
			}
			set
			{
				this.interpolation = value;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000FA47 File Offset: 0x0000DE47
		public List<Agent> GetAgents()
		{
			return this.agents;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000FA4F File Offset: 0x0000DE4F
		public List<ObstacleVertex> GetObstacles()
		{
			return this.obstacles;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000FA58 File Offset: 0x0000DE58
		public void ClearAgents()
		{
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			for (int j = 0; j < this.agents.Count; j++)
			{
				this.agents[j].simulator = null;
			}
			this.agents.Clear();
			this.kdTree.RebuildAgents();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000FAE8 File Offset: 0x0000DEE8
		protected override void Finalize()
		{
			try
			{
				if (this.workers != null)
				{
					for (int i = 0; i < this.workers.Length; i++)
					{
						this.workers[i].Terminate();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000FB44 File Offset: 0x0000DF44
		public IAgent AddAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != null && agent2.simulator == this)
			{
				throw new ArgumentException("The agent is already in the simulation");
			}
			if (agent2.simulator != null)
			{
				throw new ArgumentException("The agent is already added to another simulation");
			}
			agent2.simulator = this;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.agents.Add(agent2);
			this.kdTree.RebuildAgents();
			return agent;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000FC18 File Offset: 0x0000E018
		public IAgent AddAgent(Vector3 position)
		{
			Agent agent = new Agent(position);
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.agents.Add(agent);
			this.kdTree.RebuildAgents();
			agent.simulator = this;
			return agent;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000FC88 File Offset: 0x0000E088
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != this)
			{
				throw new ArgumentException("The agent is not added to this simulation");
			}
			agent2.simulator = null;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			if (!this.agents.Remove(agent2))
			{
				throw new ArgumentException("Critical Bug! This should not happen. Please report this.");
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000FD40 File Offset: 0x0000E140
		public ObstacleVertex AddObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.obstacles.Add(v);
			this.UpdateObstacles();
			return v;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000FDAD File Offset: 0x0000E1AD
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height)
		{
			return this.AddObstacle(vertices, height, Matrix4x4.identity);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000FDBC File Offset: 0x0000E1BC
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			ObstacleVertex obstacleVertex = null;
			ObstacleVertex obstacleVertex2 = null;
			bool flag = matrix == Matrix4x4.identity;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			for (int j = 0; j < vertices.Length; j++)
			{
				ObstacleVertex obstacleVertex3 = new ObstacleVertex();
				if (obstacleVertex == null)
				{
					obstacleVertex = obstacleVertex3;
				}
				else
				{
					obstacleVertex2.next = obstacleVertex3;
				}
				obstacleVertex3.prev = obstacleVertex2;
				obstacleVertex3.position = ((!flag) ? matrix.MultiplyPoint3x4(vertices[j]) : vertices[j]);
				obstacleVertex3.height = height;
				obstacleVertex2 = obstacleVertex3;
			}
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.prev = obstacleVertex2;
			ObstacleVertex obstacleVertex4 = obstacleVertex;
			do
			{
				Vector3 vector = obstacleVertex4.next.position - obstacleVertex4.position;
				ObstacleVertex obstacleVertex5 = obstacleVertex4;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				obstacleVertex5.dir = vector2.normalized;
				if (vertices.Length == 2)
				{
					obstacleVertex4.convex = true;
				}
				else
				{
					obstacleVertex4.convex = Polygon.IsClockwiseMargin(obstacleVertex4.next.position, obstacleVertex4.position, obstacleVertex4.prev.position);
				}
				obstacleVertex4 = obstacleVertex4.next;
			}
			while (obstacleVertex4 != obstacleVertex);
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000FF68 File Offset: 0x0000E368
		public ObstacleVertex AddObstacle(Vector3 a, Vector3 b, float height)
		{
			ObstacleVertex obstacleVertex = new ObstacleVertex();
			ObstacleVertex obstacleVertex2 = new ObstacleVertex();
			obstacleVertex.prev = obstacleVertex2;
			obstacleVertex2.prev = obstacleVertex;
			obstacleVertex.next = obstacleVertex2;
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.position = a;
			obstacleVertex2.position = b;
			obstacleVertex.height = height;
			obstacleVertex2.height = height;
			obstacleVertex.convex = true;
			obstacleVertex2.convex = true;
			ObstacleVertex obstacleVertex3 = obstacleVertex;
			Vector2 vector = new Vector2(b.x - a.x, b.z - a.z);
			obstacleVertex3.dir = vector.normalized;
			obstacleVertex2.dir = -obstacleVertex.dir;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0001005C File Offset: 0x0000E45C
		public void UpdateObstacle(ObstacleVertex obstacle, Vector3[] vertices, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (obstacle == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			if (obstacle.split)
			{
				throw new ArgumentException("Obstacle is not a start vertex. You should only pass those ObstacleVertices got from AddObstacle method calls");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			int num = 0;
			ObstacleVertex obstacleVertex = obstacle;
			for (;;)
			{
				while (obstacleVertex.next.split)
				{
					obstacleVertex.next = obstacleVertex.next.next;
					obstacleVertex.next.prev = obstacleVertex;
				}
				if (num >= vertices.Length)
				{
					break;
				}
				obstacleVertex.position = matrix.MultiplyPoint3x4(vertices[num]);
				num++;
				obstacleVertex = obstacleVertex.next;
				if (obstacleVertex == obstacle)
				{
					goto Block_9;
				}
			}
			Debug.DrawLine(obstacleVertex.prev.position, obstacleVertex.position, Color.red);
			throw new ArgumentException("Obstacle has more vertices than supplied for updating (" + vertices.Length + " supplied)");
			Block_9:
			obstacleVertex = obstacle;
			do
			{
				Vector3 vector = obstacleVertex.next.position - obstacleVertex.position;
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				obstacleVertex2.dir = vector2.normalized;
				if (vertices.Length == 2)
				{
					obstacleVertex.convex = true;
				}
				else
				{
					obstacleVertex.convex = Polygon.IsClockwiseMargin(obstacleVertex.next.position, obstacleVertex.position, obstacleVertex.prev.position);
				}
				obstacleVertex = obstacleVertex.next;
			}
			while (obstacleVertex != obstacle);
			this.ScheduleCleanObstacles();
			this.UpdateObstacles();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00010224 File Offset: 0x0000E624
		private void ScheduleCleanObstacles()
		{
			this.doCleanObstacles = true;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00010230 File Offset: 0x0000E630
		private void CleanObstacles()
		{
			for (int i = 0; i < this.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					while (obstacleVertex2.next.split)
					{
						obstacleVertex2.next = obstacleVertex2.next.next;
						obstacleVertex2.next.prev = obstacleVertex2;
					}
					obstacleVertex2 = obstacleVertex2.next;
				}
				while (obstacleVertex2 != obstacleVertex);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000102A8 File Offset: 0x0000E6A8
		public void RemoveObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Vertex must not be null");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.obstacles.Remove(v);
			this.UpdateObstacles();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00010315 File Offset: 0x0000E715
		public void UpdateObstacles()
		{
			this.doUpdateObstacles = true;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00010320 File Offset: 0x0000E720
		public void Update()
		{
			if (this.lastStep < 0f)
			{
				this.lastStep = Time.time;
				this.deltaTime = this.DesiredDeltaTime;
			}
			if (Time.time - this.lastStep > this.DesiredDeltaTime)
			{
				this.prevDeltaTime = this.DeltaTime;
				this.deltaTime = Time.time - this.lastStep;
				this.lastStep = Time.time;
				this.frameTimeBufferIndex++;
				this.frameTimeBufferIndex %= this.frameTimeBuffer.Length;
				this.frameTimeBuffer[this.frameTimeBufferIndex] = this.deltaTime;
				float num = 0f;
				for (int i = 0; i < this.frameTimeBuffer.Length; i++)
				{
					num += this.frameTimeBuffer[i];
				}
				num /= (float)this.frameTimeBuffer.Length;
				this.deltaTime = num;
				this.deltaTime = Math.Max(this.deltaTime, 0.0005f);
				this.frameDeltaTime = this.DeltaTime;
				if (this.Multithreading)
				{
					if (this.doubleBuffering)
					{
						for (int j = 0; j < this.workers.Length; j++)
						{
							this.workers[j].WaitOne();
						}
						if (!this.Interpolation)
						{
							for (int k = 0; k < this.agents.Count; k++)
							{
								this.agents[k].Interpolate(1f);
							}
						}
					}
					if (this.doCleanObstacles)
					{
						this.CleanObstacles();
						this.doCleanObstacles = false;
						this.doUpdateObstacles = true;
					}
					if (this.doUpdateObstacles)
					{
						this.doUpdateObstacles = false;
						this.kdTree.BuildObstacleTree();
					}
					this.kdTree.BuildAgentTree();
					for (int l = 0; l < this.workers.Length; l++)
					{
						this.workers[l].start = l * this.agents.Count / this.workers.Length;
						this.workers[l].end = (l + 1) * this.agents.Count / this.workers.Length;
					}
					for (int m = 0; m < this.workers.Length; m++)
					{
						this.workers[m].Execute(1);
					}
					for (int n = 0; n < this.workers.Length; n++)
					{
						this.workers[n].WaitOne();
					}
					for (int num2 = 0; num2 < this.workers.Length; num2++)
					{
						this.workers[num2].Execute(2);
					}
					for (int num3 = 0; num3 < this.workers.Length; num3++)
					{
						this.workers[num3].WaitOne();
					}
					for (int num4 = 0; num4 < this.workers.Length; num4++)
					{
						this.workers[num4].Execute(0);
					}
					if (!this.doubleBuffering)
					{
						for (int num5 = 0; num5 < this.workers.Length; num5++)
						{
							this.workers[num5].WaitOne();
						}
						if (!this.Interpolation)
						{
							for (int num6 = 0; num6 < this.agents.Count; num6++)
							{
								this.agents[num6].Interpolate(1f);
							}
						}
					}
				}
				else
				{
					if (this.doCleanObstacles)
					{
						this.CleanObstacles();
						this.doCleanObstacles = false;
						this.doUpdateObstacles = true;
					}
					if (this.doUpdateObstacles)
					{
						this.doUpdateObstacles = false;
						this.kdTree.BuildObstacleTree();
					}
					this.kdTree.BuildAgentTree();
					for (int num7 = 0; num7 < this.agents.Count; num7++)
					{
						this.agents[num7].Update();
					}
					for (int num8 = 0; num8 < this.agents.Count; num8++)
					{
						this.agents[num8].BufferSwitch();
					}
					for (int num9 = 0; num9 < this.agents.Count; num9++)
					{
						this.agents[num9].CalculateNeighbours();
						this.agents[num9].CalculateVelocity();
					}
					if (!this.Interpolation)
					{
						for (int num10 = 0; num10 < this.agents.Count; num10++)
						{
							this.agents[num10].Interpolate(1f);
						}
					}
				}
			}
			this.frameDeltaTime = Time.time - this.lastFrame;
			this.lastFrame = Time.time;
			if (this.Interpolation)
			{
				for (int num11 = 0; num11 < this.agents.Count; num11++)
				{
					this.agents[num11].Interpolate((Time.time + this.frameDeltaTime - this.lastStep) / this.DeltaTime);
				}
			}
		}

		// Token: 0x040001D6 RID: 470
		private bool doubleBuffering = true;

		// Token: 0x040001D7 RID: 471
		private float desiredDeltaTime = 0.05f;

		// Token: 0x040001D8 RID: 472
		private bool interpolation = true;

		// Token: 0x040001D9 RID: 473
		private Simulator.Worker[] workers;

		// Token: 0x040001DA RID: 474
		private List<Agent> agents;

		// Token: 0x040001DB RID: 475
		private List<ObstacleVertex> obstacles;

		// Token: 0x040001DC RID: 476
		private KDTree kdTree;

		// Token: 0x040001DD RID: 477
		private float frameDeltaTime;

		// Token: 0x040001DE RID: 478
		private float deltaTime;

		// Token: 0x040001DF RID: 479
		private float prevDeltaTime;

		// Token: 0x040001E0 RID: 480
		private float lastStep = -99999f;

		// Token: 0x040001E1 RID: 481
		private float lastFrame;

		// Token: 0x040001E2 RID: 482
		private bool doUpdateObstacles;

		// Token: 0x040001E3 RID: 483
		private bool doCleanObstacles;

		// Token: 0x040001E4 RID: 484
		private int frameTimeBufferIndex;

		// Token: 0x040001E5 RID: 485
		private float[] frameTimeBuffer = new float[5];

		// Token: 0x0200003B RID: 59
		private class Worker
		{
			// Token: 0x0600024B RID: 587 RVA: 0x00010850 File Offset: 0x0000EC50
			public Worker(Simulator sim)
			{
				this.simulator = sim;
				this.thread = new Thread(new ThreadStart(this.Run));
				this.thread.Start();
			}

			// Token: 0x0600024C RID: 588 RVA: 0x000108A4 File Offset: 0x0000ECA4
			public void Execute(int task)
			{
				this.task = task;
				this.waitFlag.Reset();
				this.runFlag.Set();
			}

			// Token: 0x0600024D RID: 589 RVA: 0x000108C5 File Offset: 0x0000ECC5
			public void WaitOne()
			{
				this.waitFlag.WaitOne();
			}

			// Token: 0x0600024E RID: 590 RVA: 0x000108D3 File Offset: 0x0000ECD3
			public void Terminate()
			{
				this.terminate = true;
			}

			// Token: 0x0600024F RID: 591 RVA: 0x000108DC File Offset: 0x0000ECDC
			public void Run()
			{
				this.runFlag.WaitOne();
				while (!this.terminate)
				{
					try
					{
						List<Agent> agents = this.simulator.GetAgents();
						if (this.task == 0)
						{
							for (int i = this.start; i < this.end; i++)
							{
								agents[i].CalculateNeighbours();
								agents[i].CalculateVelocity();
							}
						}
						else if (this.task == 1)
						{
							for (int j = this.start; j < this.end; j++)
							{
								agents[j].Update();
							}
						}
						else
						{
							if (this.task != 2)
							{
								Debug.LogError("Invalid Task Number: " + this.task);
								throw new Exception("Invalid Task Number: " + this.task);
							}
							for (int k = this.start; k < this.end; k++)
							{
								agents[k].BufferSwitch();
							}
						}
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
					this.waitFlag.Set();
					this.runFlag.WaitOne();
				}
			}

			// Token: 0x040001E6 RID: 486
			public Thread thread;

			// Token: 0x040001E7 RID: 487
			public int start;

			// Token: 0x040001E8 RID: 488
			public int end;

			// Token: 0x040001E9 RID: 489
			public int task;

			// Token: 0x040001EA RID: 490
			public AutoResetEvent runFlag = new AutoResetEvent(false);

			// Token: 0x040001EB RID: 491
			public ManualResetEvent waitFlag = new ManualResetEvent(true);

			// Token: 0x040001EC RID: 492
			public Simulator simulator;

			// Token: 0x040001ED RID: 493
			private bool terminate;
		}
	}
}
