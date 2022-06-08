using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200008C RID: 140
[RequireComponent(typeof(CharacterController))]
public class LocalAvoidance : MonoBehaviour
{
	// Token: 0x060004A1 RID: 1185 RVA: 0x0002D4A9 File Offset: 0x0002B8A9
	private void Start()
	{
		this.controller = base.GetComponent<CharacterController>();
		this.agents = (UnityEngine.Object.FindObjectsOfType(typeof(LocalAvoidance)) as LocalAvoidance[]);
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0002D4D1 File Offset: 0x0002B8D1
	public void Update()
	{
		this.SimpleMove(base.transform.forward * this.speed);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0002D4EF File Offset: 0x0002B8EF
	public Vector3 GetVelocity()
	{
		return this.preVelocity;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0002D4F7 File Offset: 0x0002B8F7
	public void LateUpdate()
	{
		this.preVelocity = this.velocity;
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0002D508 File Offset: 0x0002B908
	public void SimpleMove(Vector3 desiredMovement)
	{
		Vector3 b = UnityEngine.Random.insideUnitSphere * 0.1f;
		b.y = 0f;
		Vector3 vector = this.ClampMovement(desiredMovement + b);
		if (vector != Vector3.zero)
		{
			vector /= this.delta;
		}
		if (this.drawGizmos)
		{
			Debug.DrawRay(base.transform.position, desiredMovement, Color.magenta);
			Debug.DrawRay(base.transform.position, vector, Color.yellow);
			Debug.DrawRay(base.transform.position + vector, Vector3.up, Color.yellow);
		}
		this.controller.SimpleMove(vector);
		this.velocity = this.controller.velocity;
		Debug.DrawRay(base.transform.position, this.velocity, Color.blue);
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0002D5EC File Offset: 0x0002B9EC
	public Vector3 ClampMovement(Vector3 direction)
	{
		Vector3 vector = direction * this.delta;
		Vector3 vector2 = base.transform.position + direction;
		Vector3 vector3 = vector2;
		float num = 0f;
		int num2 = 0;
		this.vos.Clear();
		float magnitude = this.velocity.magnitude;
		foreach (LocalAvoidance localAvoidance in this.agents)
		{
			if (!(localAvoidance == this) && !(localAvoidance == null))
			{
				Vector3 vector4 = localAvoidance.transform.position - base.transform.position;
				float magnitude2 = vector4.magnitude;
				float num3 = this.radius + localAvoidance.radius;
				if (magnitude2 <= vector.magnitude * this.delta + num3 + magnitude + localAvoidance.GetVelocity().magnitude)
				{
					if (num2 <= 50)
					{
						num2++;
						LocalAvoidance.VO vo = new LocalAvoidance.VO();
						vo.origin = base.transform.position + Vector3.Lerp(this.velocity * this.delta, localAvoidance.GetVelocity() * this.delta, this.responability);
						vo.direction = vector4.normalized;
						if (num3 > vector4.magnitude)
						{
							vo.angle = 1.5707964f;
						}
						else
						{
							vo.angle = (float)Math.Asin((double)(num3 / magnitude2));
						}
						vo.limit = magnitude2 - num3;
						if (vo.limit < 0f)
						{
							vo.origin += vo.direction * vo.limit;
							vo.limit = 0f;
						}
						float num4 = Mathf.Atan2(vo.direction.z, vo.direction.x);
						vo.pRight = new Vector3(Mathf.Cos(num4 + vo.angle), 0f, Mathf.Sin(num4 + vo.angle));
						vo.pLeft = new Vector3(Mathf.Cos(num4 - vo.angle), 0f, Mathf.Sin(num4 - vo.angle));
						vo.nLeft = new Vector3(Mathf.Cos(num4 + vo.angle - 1.5707964f), 0f, Mathf.Sin(num4 + vo.angle - 1.5707964f));
						vo.nRight = new Vector3(Mathf.Cos(num4 - vo.angle + 1.5707964f), 0f, Mathf.Sin(num4 - vo.angle + 1.5707964f));
						this.vos.Add(vo);
					}
				}
			}
		}
		if (this.resType == LocalAvoidance.ResolutionType.Geometric)
		{
			for (int j = 0; j < this.vos.Count; j++)
			{
				if (this.vos[j].Contains(vector3))
				{
					num = float.PositiveInfinity;
					if (this.drawGizmos)
					{
						Debug.DrawRay(vector3, Vector3.down, Color.red);
					}
					vector3 = base.transform.position;
					break;
				}
			}
			if (this.drawGizmos)
			{
				for (int k = 0; k < this.vos.Count; k++)
				{
					this.vos[k].Draw(Color.black);
				}
			}
			if (num == 0f)
			{
				return vector;
			}
			List<LocalAvoidance.VOLine> list = new List<LocalAvoidance.VOLine>();
			for (int l = 0; l < this.vos.Count; l++)
			{
				LocalAvoidance.VO vo2 = this.vos[l];
				float num5 = (float)Math.Atan2((double)vo2.direction.z, (double)vo2.direction.x);
				Vector3 vector5 = vo2.origin + new Vector3((float)Math.Cos((double)(num5 + vo2.angle)), 0f, (float)Math.Sin((double)(num5 + vo2.angle))) * vo2.limit;
				Vector3 vector6 = vo2.origin + new Vector3((float)Math.Cos((double)(num5 - vo2.angle)), 0f, (float)Math.Sin((double)(num5 - vo2.angle))) * vo2.limit;
				Vector3 end = vector5 + new Vector3((float)Math.Cos((double)(num5 + vo2.angle)), 0f, (float)Math.Sin((double)(num5 + vo2.angle))) * 100f;
				Vector3 end2 = vector6 + new Vector3((float)Math.Cos((double)(num5 - vo2.angle)), 0f, (float)Math.Sin((double)(num5 - vo2.angle))) * 100f;
				int num6 = (!Polygon.Left(vo2.origin, vo2.origin + vo2.direction, base.transform.position + this.velocity)) ? 2 : 1;
				list.Add(new LocalAvoidance.VOLine(vo2, vector5, end, true, 1, num6 == 1));
				list.Add(new LocalAvoidance.VOLine(vo2, vector6, end2, true, 2, num6 == 2));
				list.Add(new LocalAvoidance.VOLine(vo2, vector5, vector6, false, 3, false));
				bool flag = false;
				bool flag2 = false;
				if (!flag)
				{
					for (int m = 0; m < this.vos.Count; m++)
					{
						if (m != l && this.vos[m].Contains(vector5))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					for (int n = 0; n < this.vos.Count; n++)
					{
						if (n != l && this.vos[n].Contains(vector6))
						{
							flag2 = true;
							break;
						}
					}
				}
				vo2.AddInt(0f, flag, 1);
				vo2.AddInt(0f, flag2, 2);
				vo2.AddInt(0f, flag, 3);
				vo2.AddInt(1f, flag2, 3);
			}
			for (int num7 = 0; num7 < list.Count; num7++)
			{
				for (int num8 = num7 + 1; num8 < list.Count; num8++)
				{
					LocalAvoidance.VOLine voline = list[num7];
					LocalAvoidance.VOLine voline2 = list[num8];
					if (voline.vo != voline2.vo)
					{
						float num9;
						float num10;
						if (Polygon.IntersectionFactor(voline.start, voline.end, voline2.start, voline2.end, out num9, out num10))
						{
							if (num9 >= 0f && num10 >= 0f && (voline.inf || num9 <= 1f) && (voline2.inf || num10 <= 1f))
							{
								Vector3 p = voline.start + (voline.end - voline.start) * num9;
								bool flag3 = voline.wrongSide || voline2.wrongSide;
								if (!flag3)
								{
									for (int num11 = 0; num11 < this.vos.Count; num11++)
									{
										if (this.vos[num11] != voline.vo && this.vos[num11] != voline2.vo && this.vos[num11].Contains(p))
										{
											flag3 = true;
											break;
										}
									}
								}
								voline.vo.AddInt(num9, flag3, voline.id);
								voline2.vo.AddInt(num10, flag3, voline2.id);
								if (this.drawGizmos)
								{
									Debug.DrawRay(voline.start + (voline.end - voline.start) * num9, Vector3.up, (!flag3) ? Color.green : Color.magenta);
								}
							}
						}
					}
				}
			}
			for (int num12 = 0; num12 < this.vos.Count; num12++)
			{
				Vector3 vector7;
				if (this.vos[num12].FinalInts(vector2, base.transform.position + this.velocity, this.drawGizmos, out vector7))
				{
					float sqrMagnitude = (vector7 - vector2).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						vector3 = vector7;
						num = sqrMagnitude;
						if (this.drawGizmos)
						{
							Debug.DrawLine(vector2 + Vector3.up, vector3 + Vector3.up, Color.red);
						}
					}
				}
			}
			if (this.drawGizmos)
			{
				Debug.DrawLine(vector2 + Vector3.up, vector3 + Vector3.up, Color.red);
			}
			return Vector3.ClampMagnitude(vector3 - base.transform.position, vector.magnitude * this.maxSpeedScale);
		}
		else
		{
			if (this.resType == LocalAvoidance.ResolutionType.Sampled)
			{
				Vector3 a = vector;
				Vector3 normalized = a.normalized;
				Vector3 a2 = Vector3.Cross(normalized, Vector3.up);
				int num13 = 10;
				int num14 = 0;
				while (num14 < 10)
				{
					float num15 = (float)(3.141592653589793 * (double)this.circlePoint / (double)num13);
					float num16 = (float)(3.141592653589793 - (double)this.circlePoint * 3.141592653589793) * 0.5f;
					for (int num17 = 0; num17 < num13; num17++)
					{
						float num18 = num15 * (float)num17;
						Vector3 vector8 = base.transform.position + vector - (a * (float)Math.Sin((double)(num18 + num16)) * (float)num14 * this.circleScale + a2 * (float)Math.Cos((double)(num18 + num16)) * (float)num14 * this.circleScale);
						if (this.CheckSample(vector8, this.vos))
						{
							return vector8 - base.transform.position;
						}
					}
					num14++;
					num13 += 2;
				}
				for (int num19 = 0; num19 < this.samples.Length; num19++)
				{
					Vector3 vector9 = base.transform.position + this.samples[num19].x * a2 + this.samples[num19].z * normalized + this.samples[num19].y * a;
					if (this.CheckSample(vector9, this.vos))
					{
						return vector9 - base.transform.position;
					}
				}
				return Vector3.zero;
			}
			return Vector3.zero;
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002E160 File Offset: 0x0002C560
	public bool CheckSample(Vector3 sample, List<LocalAvoidance.VO> vos)
	{
		bool flag = false;
		for (int i = 0; i < vos.Count; i++)
		{
			if (vos[i].Contains(sample))
			{
				if (this.drawGizmos)
				{
					Debug.DrawRay(sample, Vector3.up, Color.red);
				}
				flag = true;
				break;
			}
		}
		if (this.drawGizmos && !flag)
		{
			Debug.DrawRay(sample, Vector3.up, Color.yellow);
		}
		return !flag;
	}

	// Token: 0x040003AC RID: 940
	public float speed = 2f;

	// Token: 0x040003AD RID: 941
	public float delta = 1f;

	// Token: 0x040003AE RID: 942
	public float responability = 0.5f;

	// Token: 0x040003AF RID: 943
	public LocalAvoidance.ResolutionType resType = LocalAvoidance.ResolutionType.Geometric;

	// Token: 0x040003B0 RID: 944
	private Vector3 velocity;

	// Token: 0x040003B1 RID: 945
	public float radius = 0.5f;

	// Token: 0x040003B2 RID: 946
	public float maxSpeedScale = 1.5f;

	// Token: 0x040003B3 RID: 947
	public Vector3[] samples;

	// Token: 0x040003B4 RID: 948
	public float sampleScale = 1f;

	// Token: 0x040003B5 RID: 949
	public float circleScale = 0.5f;

	// Token: 0x040003B6 RID: 950
	public float circlePoint = 0.5f;

	// Token: 0x040003B7 RID: 951
	public bool drawGizmos;

	// Token: 0x040003B8 RID: 952
	protected CharacterController controller;

	// Token: 0x040003B9 RID: 953
	protected LocalAvoidance[] agents;

	// Token: 0x040003BA RID: 954
	private Vector3 preVelocity;

	// Token: 0x040003BB RID: 955
	public const float Rad2Deg = 57.29578f;

	// Token: 0x040003BC RID: 956
	private const int maxVOCounter = 50;

	// Token: 0x040003BD RID: 957
	private List<LocalAvoidance.VO> vos = new List<LocalAvoidance.VO>();

	// Token: 0x0200008D RID: 141
	public enum ResolutionType
	{
		// Token: 0x040003BF RID: 959
		Sampled,
		// Token: 0x040003C0 RID: 960
		Geometric
	}

	// Token: 0x0200008E RID: 142
	public struct VOLine
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0002E1DF File Offset: 0x0002C5DF
		public VOLine(LocalAvoidance.VO vo, Vector3 start, Vector3 end, bool inf, int id, bool wrongSide)
		{
			this.vo = vo;
			this.start = start;
			this.end = end;
			this.inf = inf;
			this.id = id;
			this.wrongSide = wrongSide;
		}

		// Token: 0x040003C1 RID: 961
		public LocalAvoidance.VO vo;

		// Token: 0x040003C2 RID: 962
		public Vector3 start;

		// Token: 0x040003C3 RID: 963
		public Vector3 end;

		// Token: 0x040003C4 RID: 964
		public bool inf;

		// Token: 0x040003C5 RID: 965
		public int id;

		// Token: 0x040003C6 RID: 966
		public bool wrongSide;
	}

	// Token: 0x0200008F RID: 143
	public struct VOIntersection
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0002E20E File Offset: 0x0002C60E
		public VOIntersection(LocalAvoidance.VO vo1, LocalAvoidance.VO vo2, float factor1, float factor2, bool inside = false)
		{
			this.vo1 = vo1;
			this.vo2 = vo2;
			this.factor1 = factor1;
			this.factor2 = factor2;
			this.inside = inside;
		}

		// Token: 0x040003C7 RID: 967
		public LocalAvoidance.VO vo1;

		// Token: 0x040003C8 RID: 968
		public LocalAvoidance.VO vo2;

		// Token: 0x040003C9 RID: 969
		public float factor1;

		// Token: 0x040003CA RID: 970
		public float factor2;

		// Token: 0x040003CB RID: 971
		public bool inside;
	}

	// Token: 0x02000090 RID: 144
	public class HalfPlane
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0002E23D File Offset: 0x0002C63D
		public bool Contains(Vector3 p)
		{
			p -= this.point;
			return Vector3.Dot(this.normal, p) >= 0f;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0002E264 File Offset: 0x0002C664
		public Vector3 ClosestPoint(Vector3 p)
		{
			p -= this.point;
			Vector3 vector = Vector3.Cross(this.normal, Vector3.up);
			float d = Vector3.Dot(vector, p);
			return this.point + vector * d;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0002E2AC File Offset: 0x0002C6AC
		public Vector3 ClosestPoint(Vector3 p, float minX, float maxX)
		{
			p -= this.point;
			Vector3 vector = Vector3.Cross(this.normal, Vector3.up);
			if (vector.x < 0f)
			{
				vector = -vector;
			}
			float num = Vector3.Dot(vector, p);
			float min = (minX - this.point.x) / vector.x;
			float max = (maxX - this.point.x) / vector.x;
			num = Mathf.Clamp(num, min, max);
			return this.point + vector * num;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0002E340 File Offset: 0x0002C740
		public Vector3 Intersection(LocalAvoidance.HalfPlane hp)
		{
			Vector3 dir = Vector3.Cross(this.normal, Vector3.up);
			Vector3 dir2 = Vector3.Cross(hp.normal, Vector3.up);
			return Polygon.IntersectionPointOptimized(this.point, dir, hp.point, dir2);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0002E384 File Offset: 0x0002C784
		public void DrawBounds(float left, float right)
		{
			Vector3 a = Vector3.Cross(this.normal, Vector3.up);
			if (a.x < 0f)
			{
				a = -a;
			}
			float d = (left - this.point.x) / a.x;
			float d2 = (right - this.point.x) / a.x;
			Debug.DrawLine(this.point + a * d + Vector3.up * 0.1f, this.point + a * d2 + Vector3.up * 0.1f, Color.yellow);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0002E43C File Offset: 0x0002C83C
		public void Draw()
		{
			Vector3 a = Vector3.Cross(this.normal, Vector3.up);
			Debug.DrawLine(this.point - a * 10f, this.point + a * 10f, Color.blue);
			Debug.DrawRay(this.point, this.normal, new Color(0.8f, 0.1f, 0.2f));
		}

		// Token: 0x040003CC RID: 972
		public Vector3 point;

		// Token: 0x040003CD RID: 973
		public Vector3 normal;
	}

	// Token: 0x02000091 RID: 145
	public enum IntersectionState
	{
		// Token: 0x040003CF RID: 975
		Inside,
		// Token: 0x040003D0 RID: 976
		Outside,
		// Token: 0x040003D1 RID: 977
		Enter,
		// Token: 0x040003D2 RID: 978
		Exit
	}

	// Token: 0x02000092 RID: 146
	public struct IntersectionPair : IComparable<LocalAvoidance.IntersectionPair>
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0002E4B5 File Offset: 0x0002C8B5
		public IntersectionPair(float factor, bool inside)
		{
			this.factor = factor;
			this.state = ((!inside) ? LocalAvoidance.IntersectionState.Outside : LocalAvoidance.IntersectionState.Inside);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0002E4D1 File Offset: 0x0002C8D1
		public void SetState(LocalAvoidance.IntersectionState s)
		{
			this.state = s;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002E4DA File Offset: 0x0002C8DA
		public int CompareTo(LocalAvoidance.IntersectionPair o)
		{
			if (o.factor < this.factor)
			{
				return 1;
			}
			if (o.factor > this.factor)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x040003D3 RID: 979
		public float factor;

		// Token: 0x040003D4 RID: 980
		public LocalAvoidance.IntersectionState state;
	}

	// Token: 0x02000093 RID: 147
	public class VO
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0002E530 File Offset: 0x0002C930
		public void AddInt(float factor, bool inside, int id)
		{
			if (id != 1)
			{
				if (id != 2)
				{
					if (id == 3)
					{
						this.ints3.Add(new LocalAvoidance.IntersectionPair(factor, inside));
					}
				}
				else
				{
					this.ints2.Add(new LocalAvoidance.IntersectionPair(factor, inside));
				}
			}
			else
			{
				this.ints1.Add(new LocalAvoidance.IntersectionPair(factor, inside));
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0002E59C File Offset: 0x0002C99C
		public bool FinalInts(Vector3 target, Vector3 closeEdgeConstraint, bool drawGizmos, out Vector3 closest)
		{
			this.ints1.Sort();
			this.ints2.Sort();
			this.ints3.Sort();
			float num = (float)Math.Atan2((double)this.direction.z, (double)this.direction.x);
			Vector3 vector = Vector3.Cross(this.direction, Vector3.up);
			Vector3 b = vector * (float)Math.Tan((double)this.angle) * this.limit;
			Vector3 vector2 = this.origin + this.direction * this.limit + b;
			Vector3 vector3 = this.origin + this.direction * this.limit - b;
			Vector3 vector4 = vector2 + new Vector3((float)Math.Cos((double)(num + this.angle)), 0f, (float)Math.Sin((double)(num + this.angle))) * 100f;
			Vector3 vector5 = vector3 + new Vector3((float)Math.Cos((double)(num - this.angle)), 0f, (float)Math.Sin((double)(num - this.angle))) * 100f;
			bool flag = false;
			closest = Vector3.zero;
			int num2 = (Vector3.Dot(closeEdgeConstraint - this.origin, vector) <= 0f) ? 1 : 2;
			for (int i = 1; i <= 3; i++)
			{
				if (i != num2)
				{
					List<LocalAvoidance.IntersectionPair> list = (i != 1) ? ((i != 2) ? this.ints3 : this.ints2) : this.ints1;
					Vector3 vector6 = (i != 1 && i != 3) ? vector3 : vector2;
					Vector3 vector7 = (i != 1) ? ((i != 2) ? vector3 : vector5) : vector4;
					float num3 = Mathfx.NearestPointFactor(vector6, vector7, target);
					float num4 = float.PositiveInfinity;
					float num5 = float.NegativeInfinity;
					bool flag2 = false;
					for (int j = 0; j < list.Count - ((i != 3) ? 0 : 1); j++)
					{
						if (drawGizmos)
						{
							Debug.DrawRay(vector6 + (vector7 - vector6) * list[j].factor, Vector3.down, (list[j].state != LocalAvoidance.IntersectionState.Outside) ? Color.red : Color.green);
						}
						if (list[j].state == LocalAvoidance.IntersectionState.Outside && ((j == list.Count - 1 && (j == 0 || list[j - 1].state != LocalAvoidance.IntersectionState.Outside)) || (j < list.Count - 1 && list[j + 1].state == LocalAvoidance.IntersectionState.Outside)))
						{
							flag2 = true;
							float factor = list[j].factor;
							float num6 = (j != list.Count - 1) ? list[j + 1].factor : ((i != 3) ? float.PositiveInfinity : 1f);
							if (drawGizmos)
							{
								Debug.DrawLine(vector6 + (vector7 - vector6) * factor + Vector3.up, vector6 + (vector7 - vector6) * Mathf.Clamp01(num6) + Vector3.up, Color.green);
							}
							if (factor <= num3 && num6 >= num3)
							{
								num4 = num3;
								num5 = num3;
								break;
							}
							if (num6 < num3 && num6 > num5)
							{
								num5 = num6;
							}
							else if (factor > num3 && factor < num4)
							{
								num4 = factor;
							}
						}
					}
					if (flag2)
					{
						float d = (num4 != float.NegativeInfinity) ? ((num5 != float.PositiveInfinity) ? ((Mathf.Abs(num3 - num4) >= Mathf.Abs(num3 - num5)) ? num5 : num4) : num4) : num5;
						Vector3 vector8 = vector6 + (vector7 - vector6) * d;
						if (!flag || (vector8 - target).sqrMagnitude < (closest - target).sqrMagnitude)
						{
							closest = vector8;
						}
						if (drawGizmos)
						{
							Debug.DrawLine(target, closest, Color.yellow);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0002EA74 File Offset: 0x0002CE74
		public bool Contains(Vector3 p)
		{
			return Vector3.Dot(this.nLeft, p - this.origin) > 0f && Vector3.Dot(this.nRight, p - this.origin) > 0f && Vector3.Dot(this.direction, p - this.origin) > this.limit;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0002EAE5 File Offset: 0x0002CEE5
		public float ScoreContains(Vector3 p)
		{
			return 0f;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002EAEC File Offset: 0x0002CEEC
		public void Draw(Color c)
		{
			float num = (float)Math.Atan2((double)this.direction.z, (double)this.direction.x);
			Vector3 b = Vector3.Cross(this.direction, Vector3.up) * (float)Math.Tan((double)this.angle) * this.limit;
			Debug.DrawLine(this.origin + this.direction * this.limit + b, this.origin + this.direction * this.limit - b, c);
			Debug.DrawRay(this.origin + this.direction * this.limit + b, new Vector3((float)Math.Cos((double)(num + this.angle)), 0f, (float)Math.Sin((double)(num + this.angle))) * 10f, c);
			Debug.DrawRay(this.origin + this.direction * this.limit - b, new Vector3((float)Math.Cos((double)(num - this.angle)), 0f, (float)Math.Sin((double)(num - this.angle))) * 10f, c);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002EC44 File Offset: 0x0002D044
		public static explicit operator LocalAvoidance.HalfPlane(LocalAvoidance.VO vo)
		{
			return new LocalAvoidance.HalfPlane
			{
				point = vo.origin + vo.direction * vo.limit,
				normal = -vo.direction
			};
		}

		// Token: 0x040003D5 RID: 981
		public Vector3 origin;

		// Token: 0x040003D6 RID: 982
		public Vector3 direction;

		// Token: 0x040003D7 RID: 983
		public float angle;

		// Token: 0x040003D8 RID: 984
		public float limit;

		// Token: 0x040003D9 RID: 985
		public Vector3 pLeft;

		// Token: 0x040003DA RID: 986
		public Vector3 pRight;

		// Token: 0x040003DB RID: 987
		public Vector3 nLeft;

		// Token: 0x040003DC RID: 988
		public Vector3 nRight;

		// Token: 0x040003DD RID: 989
		public List<LocalAvoidance.IntersectionPair> ints1 = new List<LocalAvoidance.IntersectionPair>();

		// Token: 0x040003DE RID: 990
		public List<LocalAvoidance.IntersectionPair> ints2 = new List<LocalAvoidance.IntersectionPair>();

		// Token: 0x040003DF RID: 991
		public List<LocalAvoidance.IntersectionPair> ints3 = new List<LocalAvoidance.IntersectionPair>();
	}
}
