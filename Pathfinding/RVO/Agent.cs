using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000038 RID: 56
	public class Agent : IAgent
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000DD3C File Offset: 0x0000C13C
		public Agent(Vector3 pos)
		{
			this.MaxSpeed = 2f;
			this.NeighbourDist = 15f;
			this.AgentTimeHorizon = 2f;
			this.ObstacleTimeHorizon = 2f;
			this.Height = 5f;
			this.Radius = 5f;
			this.MaxNeighbours = 10;
			this.Locked = false;
			this.position = pos;
			this.Position = this.position;
			this.prevSmoothPos = this.position;
			this.smoothPos = this.position;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000DE18 File Offset: 0x0000C218
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000DE20 File Offset: 0x0000C220
		public Vector3 Position { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000DE29 File Offset: 0x0000C229
		public Vector3 InterpolatedPosition
		{
			get
			{
				return this.smoothPos;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000DE31 File Offset: 0x0000C231
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000DE39 File Offset: 0x0000C239
		public Vector3 DesiredVelocity { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000DE42 File Offset: 0x0000C242
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000DE4A File Offset: 0x0000C24A
		public bool Locked { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000DE53 File Offset: 0x0000C253
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000DE5B File Offset: 0x0000C25B
		public float Radius { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000DE64 File Offset: 0x0000C264
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000DE6C File Offset: 0x0000C26C
		public float Height { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000DE75 File Offset: 0x0000C275
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000DE7D File Offset: 0x0000C27D
		public float MaxSpeed { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000DE86 File Offset: 0x0000C286
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000DE8E File Offset: 0x0000C28E
		public float NeighbourDist { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000DE97 File Offset: 0x0000C297
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000DE9F File Offset: 0x0000C29F
		public float AgentTimeHorizon { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000DEA8 File Offset: 0x0000C2A8
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000DEB0 File Offset: 0x0000C2B0
		public float ObstacleTimeHorizon { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000DEB9 File Offset: 0x0000C2B9
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000DEC1 File Offset: 0x0000C2C1
		public Vector3 Velocity { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000DECA File Offset: 0x0000C2CA
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000DED2 File Offset: 0x0000C2D2
		public bool DebugDraw { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000DEDB File Offset: 0x0000C2DB
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000DEE3 File Offset: 0x0000C2E3
		public int MaxNeighbours { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000DEEC File Offset: 0x0000C2EC
		public List<ObstacleVertex> NeighbourObstacles
		{
			get
			{
				return this.obstaclesBuffered;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000DEF4 File Offset: 0x0000C2F4
		public void PreUpdatePosition()
		{
			this.position += this.Velocity * this.simulator.PrevDeltaTime;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000DF20 File Offset: 0x0000C320
		public void BufferSwitch()
		{
			this.radius = this.Radius;
			this.height = this.Height;
			this.maxSpeed = this.MaxSpeed;
			this.neighbourDist = this.NeighbourDist;
			this.agentTimeHorizon = this.AgentTimeHorizon;
			this.obstacleTimeHorizon = this.ObstacleTimeHorizon;
			this.maxNeighbours = this.MaxNeighbours;
			this.desiredVelocity = this.DesiredVelocity;
			this.locked = this.Locked;
			this.Velocity = this.velocity;
			List<ObstacleVertex> list = this.obstaclesBuffered;
			this.obstaclesBuffered = this.obstacles;
			this.obstacles = list;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000DFC0 File Offset: 0x0000C3C0
		public void Update()
		{
			this.velocity = this.newVelocity;
			this.prevSmoothPos = this.smoothPos;
			this.position = this.Position;
			this.position += this.velocity * this.simulator.DeltaTime;
			this.Position = this.position;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000E024 File Offset: 0x0000C424
		public void Interpolate(float t)
		{
			if (t == 1f)
			{
				this.smoothPos = this.position;
			}
			else
			{
				this.smoothPos = this.prevSmoothPos + (this.position - this.prevSmoothPos) * t;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000E078 File Offset: 0x0000C478
		public void CalculateNeighbours()
		{
			this.neighbours.Clear();
			this.neighbourDists.Clear();
			Agent.watch1.Start();
			float num;
			if (this.MaxNeighbours > 0)
			{
				num = this.neighbourDist * this.neighbourDist;
				this.simulator.KDTree.GetAgentNeighbours(this, num);
			}
			Agent.watch1.Stop();
			this.obstacles.Clear();
			this.obstacleDists.Clear();
			Agent.watch2.Start();
			num = this.obstacleTimeHorizon * this.maxSpeed + this.radius;
			num *= num;
			this.simulator.KDTree.GetObstacleNeighbours(this, num);
			Agent.watch2.Stop();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E130 File Offset: 0x0000C530
		public float det(Vector3 a, Vector3 b)
		{
			return a.x * b.z - a.z * b.x;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E151 File Offset: 0x0000C551
		public float det(Vector2 a, Vector2 b)
		{
			return a.x * b.y - a.y * b.x;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E174 File Offset: 0x0000C574
		public void CalculateVelocity()
		{
			this.orcaLines.Clear();
			if (this.locked)
			{
				this.newVelocity = new Vector3(0f, 0f, 0f);
				return;
			}
			Vector2 a = new Vector2(this.Velocity.x, this.Velocity.z);
			for (int i = 0; i < this.obstacles.Count; i++)
			{
				float num = 1f / this.obstacleTimeHorizon;
				ObstacleVertex obstacleVertex = this.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex.next;
				float num2 = (!obstacleVertex.thin) ? this.radius : this.radius;
				float val = Math.Min(obstacleVertex.position.y, obstacleVertex2.position.y);
				float val2 = Math.Max(obstacleVertex.position.y + obstacleVertex.height, obstacleVertex2.position.y + obstacleVertex2.height);
				float num3 = Math.Max(val, this.position.y);
				float num4 = Math.Min(val2, this.position.y + this.height);
				if (num4 - num3 >= 0f)
				{
					Vector2 a2 = new Vector2(obstacleVertex.position.x - this.position.x, obstacleVertex.position.z - this.position.z);
					Vector2 a3 = new Vector2(obstacleVertex2.position.x - this.position.x, obstacleVertex2.position.z - this.position.z);
					Vector2 vector = new Vector2(obstacleVertex2.position.x - obstacleVertex.position.x, obstacleVertex2.position.z - obstacleVertex.position.z);
					bool flag = false;
					for (int j = 0; j < this.orcaLines.Count; j++)
					{
						if (this.det(num * a2 - this.orcaLines[j].point, this.orcaLines[j].dir) - num * num2 >= -1E-45f && this.det(num * a3 - this.orcaLines[j].point, this.orcaLines[j].dir) - num * num2 >= -1E-45f)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						float sqrMagnitude = a2.sqrMagnitude;
						float sqrMagnitude2 = a3.sqrMagnitude;
						float num5 = num2 * num2;
						float num6 = Vector2.Dot(-a2, vector) / vector.sqrMagnitude;
						float sqrMagnitude3 = (-a2 - num6 * vector).sqrMagnitude;
						if (num6 < 0f && sqrMagnitude <= num5)
						{
							if (obstacleVertex.convex)
							{
								Line item;
								item.point = new Vector2(0f, 0f);
								Vector2 vector2 = new Vector2(-a2.y, a2.x);
								item.dir = vector2.normalized;
								this.orcaLines.Add(item);
							}
						}
						else if (num6 > 1f && sqrMagnitude2 <= num5)
						{
							if (obstacleVertex2.convex && this.det(a3, obstacleVertex2.dir) >= 0f)
							{
								Line item;
								item.point = new Vector2(0f, 0f);
								Vector2 vector3 = new Vector2(-a3.y, a3.x);
								item.dir = vector3.normalized;
								this.orcaLines.Add(item);
							}
						}
						else if (num6 >= 0f && num6 < 1f && sqrMagnitude3 <= num5)
						{
							Line item;
							item.point = new Vector2(0f, 0f);
							item.dir = -obstacleVertex.dir;
							this.orcaLines.Add(item);
						}
						else
						{
							Vector2 vector4;
							Vector2 vector5;
							if (num6 < 0f && sqrMagnitude3 <= num5)
							{
								if (!obstacleVertex.convex)
								{
									goto IL_AB4;
								}
								obstacleVertex2 = obstacleVertex;
								float num7 = (float)Math.Sqrt((double)(sqrMagnitude - num5));
								vector4 = new Vector2(a2.x * num7 - a2.y * num2, a2.x * num2 + a2.y * num7) / sqrMagnitude;
								vector5 = new Vector2(a2.x * num7 + a2.y * num2, -a2.x * num2 + a2.y * num7) / sqrMagnitude;
							}
							else if (num6 > 1f && sqrMagnitude3 <= num5)
							{
								if (!obstacleVertex.convex)
								{
									goto IL_AB4;
								}
								obstacleVertex = obstacleVertex2;
								float num8 = (float)Math.Sqrt((double)(sqrMagnitude2 - num5));
								vector4 = new Vector2(a3.x * num8 - a3.y * num2, a3.x * num2 + a3.y * num8) / sqrMagnitude2;
								vector5 = new Vector2(a3.x * num8 + a3.y * num2, -a3.x * num2 + a3.y * num8) / sqrMagnitude2;
							}
							else
							{
								if (obstacleVertex.convex)
								{
									float num9 = (float)Math.Sqrt((double)(sqrMagnitude - num5));
									vector4 = new Vector2(a2.x * num9 - a2.y * num2, a2.x * num2 + a2.y * num9) / sqrMagnitude;
								}
								else
								{
									vector4 = -obstacleVertex.dir;
								}
								if (obstacleVertex2.convex)
								{
									float num10 = (float)Math.Sqrt((double)(sqrMagnitude2 - num5));
									vector5 = new Vector2(a3.x * num10 + a3.y * num2, -a3.x * num2 + a3.y * num10) / sqrMagnitude2;
								}
								else
								{
									vector5 = obstacleVertex.dir;
								}
							}
							ObstacleVertex prev = obstacleVertex.prev;
							bool flag2 = false;
							bool flag3 = false;
							if (obstacleVertex.convex && this.det(vector4, -prev.dir) >= 0f)
							{
								vector4 = -prev.dir;
								flag2 = true;
							}
							if (obstacleVertex2.convex && this.det(vector5, obstacleVertex2.dir) <= 0f)
							{
								vector5 = obstacleVertex2.dir;
								flag3 = true;
							}
							Vector2 vector6 = num * new Vector2(obstacleVertex.position.x - this.position.x, obstacleVertex.position.z - this.position.z);
							Vector2 vector7 = num * new Vector2(obstacleVertex2.position.x - this.position.x, obstacleVertex2.position.z - this.position.z);
							Vector2 vector8 = vector7 - vector6;
							float num11 = ((obstacleVertex != obstacleVertex2) ? Vector2.Dot(a - vector6, vector8) : 0.5f) / vector8.sqrMagnitude;
							float num12 = Vector2.Dot(a - vector6, vector4);
							float num13 = Vector2.Dot(a - vector7, vector5);
							if ((num11 < 0f && num12 < 0f) || (obstacleVertex == obstacleVertex2 && num12 < 0f && num13 < 0f))
							{
								Vector2 normalized = (a - vector6).normalized;
								Line item;
								item.dir = new Vector2(normalized.y, -normalized.x);
								item.point = vector6 + num2 * num * normalized;
								this.orcaLines.Add(item);
							}
							else if (num11 > 1f && num13 < 0f)
							{
								Vector2 normalized2 = (a - vector7).normalized;
								Line item;
								item.dir = new Vector2(normalized2.y, -normalized2.x);
								item.point = vector7 + num2 * num * normalized2;
								this.orcaLines.Add(item);
							}
							else
							{
								float num14 = (num11 >= 0f && num11 <= 1f && obstacleVertex != obstacleVertex2) ? (a - (vector6 + num11 * vector8)).sqrMagnitude : float.PositiveInfinity;
								float num15 = (num12 >= 0f) ? (a - (vector6 + num12 * vector4)).sqrMagnitude : float.PositiveInfinity;
								float num16 = (num13 >= 0f) ? (a - (vector7 + num13 * vector5)).sqrMagnitude : float.PositiveInfinity;
								if (num14 <= num15 && num14 <= num16)
								{
									Line item;
									item.dir = -obstacleVertex.dir;
									item.point = vector6 + num2 * num * new Vector2(-item.dir.y, item.dir.x);
									this.orcaLines.Add(item);
								}
								else if (num15 <= num16)
								{
									if (!flag2)
									{
										Line item;
										item.dir = vector4;
										item.point = vector6 + num2 * num * new Vector2(-item.dir.y, item.dir.x);
										this.orcaLines.Add(item);
									}
								}
								else if (!flag3)
								{
									Line item;
									item.dir = -vector5;
									item.point = vector7 + num2 * num * new Vector2(-item.dir.y, item.dir.x);
									this.orcaLines.Add(item);
								}
							}
						}
					}
				}
				IL_AB4:;
			}
			float num17 = 1f / this.agentTimeHorizon;
			int count = this.orcaLines.Count;
			for (int k = 0; k < this.neighbours.Count; k++)
			{
				Agent agent = this.neighbours[k];
				float num18 = Math.Min(this.position.y + this.height, agent.position.y + agent.height);
				float num19 = Math.Max(this.position.y, agent.position.y);
				if (num18 - num19 >= 0f)
				{
					Vector3 vector9 = agent.position - this.position;
					Vector3 vector10 = this.Velocity - agent.Velocity;
					Vector2 vector11 = new Vector2(vector9.x, vector9.z);
					Vector2 vector12 = new Vector2(vector10.x, vector10.z);
					float sqrMagnitude4 = vector11.sqrMagnitude;
					float num20 = this.radius + agent.radius;
					float num21 = num20 * num20;
					Line item2;
					Vector2 a5;
					if (sqrMagnitude4 > num21)
					{
						Vector2 vector13 = vector12 - num17 * vector11;
						float sqrMagnitude5 = vector13.sqrMagnitude;
						float num22 = Vector2.Dot(vector13, vector11);
						if (num22 < 0f && num22 * num22 > num21 * sqrMagnitude5)
						{
							float num23 = (float)Math.Sqrt((double)sqrMagnitude5);
							Vector2 a4 = vector13 / num23;
							item2.dir = new Vector2(a4.y, -a4.x);
							a5 = (num20 * num17 - num23) * a4;
						}
						else
						{
							float num24 = (float)Math.Sqrt((double)(sqrMagnitude4 - num21));
							if (this.det(vector11, vector13) > 0f)
							{
								item2.dir = new Vector2(vector11.x * num24 - vector11.y * num20, vector11.x * num20 + vector11.y * num24) / sqrMagnitude4;
							}
							else
							{
								item2.dir = -new Vector2(vector11.x * num24 + vector11.y * num20, -vector11.x * num20 + vector11.y * num24) / sqrMagnitude4;
							}
							float d = Vector2.Dot(vector12, item2.dir);
							a5 = d * item2.dir - vector12;
						}
					}
					else
					{
						float num25 = 1f / this.simulator.DeltaTime;
						Vector2 a6 = vector12 - num25 * vector11;
						float magnitude = a6.magnitude;
						Vector2 a7 = a6 / magnitude;
						item2.dir = new Vector2(a7.y, -a7.x);
						a5 = (num20 * num25 - magnitude) * a7;
					}
					if (agent.locked)
					{
						item2.point = a + 1f * a5;
					}
					item2.point = a + 0.5f * a5;
					this.orcaLines.Add(item2);
				}
			}
			Vector2 zero = Vector2.zero;
			int num26 = this.LinearProgram2(this.orcaLines, this.maxSpeed, new Vector2(this.desiredVelocity.x, this.desiredVelocity.z), false, ref zero);
			if (num26 < this.orcaLines.Count)
			{
				this.LinearProgram3(this.orcaLines, count, num26, this.maxSpeed, ref zero);
			}
			this.newVelocity = new Vector3(zero.x, 0f, zero.y);
			this.newVelocity = Vector3.ClampMagnitude(this.newVelocity, this.maxSpeed);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000F011 File Offset: 0x0000D411
		private float Sqr(float v)
		{
			return v * v;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000F018 File Offset: 0x0000D418
		public float InsertAgentNeighbour(Agent agent, float rangeSq)
		{
			if (this == agent)
			{
				return rangeSq;
			}
			float num = this.Sqr(agent.position.x - this.position.x) + this.Sqr(agent.position.z - this.position.z);
			if (num < rangeSq)
			{
				if (this.neighbours.Count < this.maxNeighbours)
				{
					this.neighbours.Add(agent);
					this.neighbourDists.Add(num);
				}
				int num2 = this.neighbours.Count - 1;
				while (num2 != 0 && num < this.neighbourDists[num2 - 1])
				{
					this.neighbours[num2] = this.neighbours[num2 - 1];
					this.neighbourDists[num2] = this.neighbourDists[num2 - 1];
					num2--;
				}
				this.neighbours[num2] = agent;
				this.neighbourDists[num2] = num;
				if (this.neighbours.Count == this.maxNeighbours)
				{
					rangeSq = this.neighbourDists[this.neighbourDists.Count - 1];
				}
			}
			return rangeSq;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000F14F File Offset: 0x0000D54F
		private static float DistSqPointLineSegment(Vector3 a, Vector3 b, Vector3 c)
		{
			return Agent.DistSqPointLineSegment(new Vector2(a.x, a.z), new Vector2(b.x, b.z), new Vector2(c.x, c.z));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000F190 File Offset: 0x0000D590
		private static float DistSqPointLineSegment(Vector2 a, Vector2 b, Vector2 c)
		{
			float num = Vector2.Dot(c - a, b - a) / (b - a).sqrMagnitude;
			if (num < 0f)
			{
				return (c - a).sqrMagnitude;
			}
			if (num > 1f)
			{
				return (c - b).sqrMagnitude;
			}
			return (c - (a + num * (b - a))).sqrMagnitude;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000F21C File Offset: 0x0000D61C
		public void InsertObstacleNeighbour(ObstacleVertex ob1, float rangeSq)
		{
			ObstacleVertex next = ob1.next;
			float num = Mathfx.DistancePointSegmentStrict(ob1.position, next.position, this.Position);
			if (num < rangeSq)
			{
				this.obstacles.Add(ob1);
				this.obstacleDists.Add(num);
				int num2 = this.obstacles.Count - 1;
				while (num2 != 0 && num < this.obstacleDists[num2 - 1])
				{
					this.obstacles[num2] = this.obstacles[num2 - 1];
					this.obstacleDists[num2] = this.obstacleDists[num2 - 1];
					num2--;
				}
				this.obstacles[num2] = ob1;
				this.obstacleDists[num2] = num;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000F2E8 File Offset: 0x0000D6E8
		private bool LinearProgram1(List<Line> lines, int lineNo, float radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
		{
			float num = Vector2.Dot(lines[lineNo].point, lines[lineNo].dir);
			float num2 = num * num + radius * radius - lines[lineNo].point.sqrMagnitude;
			if (num2 < 0f)
			{
				return false;
			}
			float num3 = (float)Math.Sqrt((double)num2);
			float num4 = -num - num3;
			float num5 = -num + num3;
			for (int i = 0; i < lineNo; i++)
			{
				float num6 = this.det(lines[lineNo].dir, lines[i].dir);
				float num7 = this.det(lines[i].dir, lines[lineNo].point - lines[i].point);
				if (Math.Abs(num6) <= 1E-45f)
				{
					if (num7 < 0f)
					{
						return false;
					}
				}
				else
				{
					float val = num7 / num6;
					if (num6 >= 0f)
					{
						num5 = Math.Min(num5, val);
					}
					else
					{
						num4 = Math.Max(num4, val);
					}
					if (num4 > num5)
					{
						return false;
					}
				}
			}
			if (directionOpt)
			{
				if (Vector2.Dot(optVelocity, lines[lineNo].dir) > 0f)
				{
					result = lines[lineNo].point + num5 * lines[lineNo].dir;
				}
				else
				{
					result = lines[lineNo].point + num4 * lines[lineNo].dir;
				}
			}
			else
			{
				float num8 = Vector2.Dot(lines[lineNo].dir, optVelocity - lines[lineNo].point);
				if (num8 < num4)
				{
					result = lines[lineNo].point + num4 * lines[lineNo].dir;
				}
				else if (num8 > num5)
				{
					result = lines[lineNo].point + num5 * lines[lineNo].dir;
				}
				else
				{
					result = lines[lineNo].point + num8 * lines[lineNo].dir;
				}
			}
			return true;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000F5B4 File Offset: 0x0000D9B4
		private int LinearProgram2(List<Line> lines, float radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
		{
			if (directionOpt)
			{
				result = optVelocity * radius;
			}
			else if (optVelocity.sqrMagnitude > radius * radius)
			{
				result = optVelocity.normalized * radius;
			}
			else
			{
				result = optVelocity;
			}
			for (int i = 0; i < lines.Count; i++)
			{
				if (this.det(lines[i].dir, lines[i].point - result) > 0f)
				{
					Vector2 vector = result;
					if (!this.LinearProgram1(lines, i, radius, optVelocity, directionOpt, ref result))
					{
						result = vector;
						return i;
					}
				}
			}
			return lines.Count;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000F688 File Offset: 0x0000DA88
		private void LinearProgram3(List<Line> lines, int numObstLines, int beginLine, float radius, ref Vector2 result)
		{
			float num = 0f;
			for (int i = beginLine; i < lines.Count; i++)
			{
				if (this.det(lines[i].dir, lines[i].point - result) > num)
				{
					this.projLines.Clear();
					for (int j = 0; j < numObstLines; j++)
					{
						this.projLines.Add(lines[j]);
					}
					int k = numObstLines;
					while (k < i)
					{
						float num2 = this.det(lines[i].dir, lines[k].dir);
						Line item;
						if (Math.Abs(num2) > 1E-45f)
						{
							item.point = lines[i].point + this.det(lines[k].dir, lines[i].point - lines[k].point) / num2 * lines[i].dir;
							goto IL_19B;
						}
						if (Vector2.Dot(lines[i].dir, lines[k].dir) <= 0f)
						{
							item.point = 0.5f * (lines[i].point + lines[k].point);
							goto IL_19B;
						}
						IL_1DE:
						k++;
						continue;
						IL_19B:
						item.dir = (lines[k].dir - lines[i].dir).normalized;
						this.projLines.Add(item);
						goto IL_1DE;
					}
					Vector2 vector = result;
					if (this.LinearProgram2(this.projLines, radius, new Vector2(-lines[i].dir.y, lines[i].dir.x), true, ref result) < this.projLines.Count)
					{
						result = vector;
					}
					num = this.det(lines[i].dir, lines[i].point - result);
				}
			}
		}

		// Token: 0x040001A9 RID: 425
		private Vector3 smoothPos;

		// Token: 0x040001AC RID: 428
		public float radius;

		// Token: 0x040001AD RID: 429
		public float height;

		// Token: 0x040001AE RID: 430
		public float maxSpeed;

		// Token: 0x040001AF RID: 431
		public float neighbourDist;

		// Token: 0x040001B0 RID: 432
		public float agentTimeHorizon;

		// Token: 0x040001B1 RID: 433
		public float obstacleTimeHorizon;

		// Token: 0x040001B2 RID: 434
		public float weight;

		// Token: 0x040001B3 RID: 435
		public bool locked;

		// Token: 0x040001B4 RID: 436
		public int maxNeighbours;

		// Token: 0x040001B5 RID: 437
		public Vector3 position;

		// Token: 0x040001B6 RID: 438
		public Vector3 desiredVelocity;

		// Token: 0x040001B7 RID: 439
		public Vector3 prevSmoothPos;

		// Token: 0x040001C2 RID: 450
		private Vector3 velocity;

		// Token: 0x040001C3 RID: 451
		private Vector3 newVelocity;

		// Token: 0x040001C4 RID: 452
		public Simulator simulator;

		// Token: 0x040001C5 RID: 453
		private List<Agent> neighbours = new List<Agent>();

		// Token: 0x040001C6 RID: 454
		private List<float> neighbourDists = new List<float>();

		// Token: 0x040001C7 RID: 455
		private List<ObstacleVertex> obstaclesBuffered = new List<ObstacleVertex>();

		// Token: 0x040001C8 RID: 456
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040001C9 RID: 457
		private List<float> obstacleDists = new List<float>();

		// Token: 0x040001CA RID: 458
		private List<Line> orcaLines = new List<Line>();

		// Token: 0x040001CB RID: 459
		private List<Line> projLines = new List<Line>();

		// Token: 0x040001CC RID: 460
		public static Stopwatch watch1 = new Stopwatch();

		// Token: 0x040001CD RID: 461
		public static Stopwatch watch2 = new Stopwatch();
	}
}
