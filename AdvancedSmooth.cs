using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000094 RID: 148
[AddComponentMenu("Pathfinding/Modifiers/Advanced Smooth")]
[Serializable]
public class AdvancedSmooth : MonoModifier
{
	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060004BC RID: 1212 RVA: 0x0002ED6D File Offset: 0x0002D16D
	public override ModifierData input
	{
		get
		{
			return ModifierData.VectorPath;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060004BD RID: 1213 RVA: 0x0002ED70 File Offset: 0x0002D170
	public override ModifierData output
	{
		get
		{
			return ModifierData.VectorPath;
		}
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0002ED74 File Offset: 0x0002D174
	public override void Apply(Path p, ModifierData source)
	{
		Vector3[] array = p.vectorPath.ToArray();
		if (array == null || array.Length <= 2)
		{
			return;
		}
		List<Vector3> list = new List<Vector3>();
		list.Add(array[0]);
		AdvancedSmooth.TurnConstructor.turningRadius = this.turningRadius;
		for (int i = 1; i < array.Length - 1; i++)
		{
			List<AdvancedSmooth.Turn> turnList = new List<AdvancedSmooth.Turn>();
			AdvancedSmooth.TurnConstructor.Setup(i, array);
			this.turnConstruct1.Prepare(i, array);
			this.turnConstruct2.Prepare(i, array);
			AdvancedSmooth.TurnConstructor.PostPrepare();
			if (i == 1)
			{
				this.turnConstruct1.PointToTangent(turnList);
				this.turnConstruct2.PointToTangent(turnList);
			}
			else
			{
				this.turnConstruct1.TangentToTangent(turnList);
				this.turnConstruct2.TangentToTangent(turnList);
			}
			this.EvaluatePaths(turnList, list);
			if (i == array.Length - 2)
			{
				this.turnConstruct1.TangentToPoint(turnList);
				this.turnConstruct2.TangentToPoint(turnList);
			}
			this.EvaluatePaths(turnList, list);
		}
		list.Add(array[array.Length - 1]);
		p.vectorPath = list;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002EE90 File Offset: 0x0002D290
	private void EvaluatePaths(List<AdvancedSmooth.Turn> turnList, List<Vector3> output)
	{
		turnList.Sort();
		for (int i = 0; i < turnList.Count; i++)
		{
			if (i == 0)
			{
				turnList[i].GetPath(output);
			}
		}
		turnList.Clear();
		if (AdvancedSmooth.TurnConstructor.changedPreviousTangent)
		{
			this.turnConstruct1.OnTangentUpdate();
			this.turnConstruct2.OnTangentUpdate();
		}
	}

	// Token: 0x040003E0 RID: 992
	public float turningRadius = 1f;

	// Token: 0x040003E1 RID: 993
	public AdvancedSmooth.MaxTurn turnConstruct1 = new AdvancedSmooth.MaxTurn();

	// Token: 0x040003E2 RID: 994
	public AdvancedSmooth.ConstantTurn turnConstruct2 = new AdvancedSmooth.ConstantTurn();

	// Token: 0x02000095 RID: 149
	[Serializable]
	public class MaxTurn : AdvancedSmooth.TurnConstructor
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x0002F2FC File Offset: 0x0002D6FC
		public override void OnTangentUpdate()
		{
			this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
			this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
			this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
			this.vaLeft = this.vaRight + 3.141592653589793;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0002F37C File Offset: 0x0002D77C
		public override void Prepare(int i, Vector3[] vectorPath)
		{
			this.preRightCircleCenter = this.rightCircleCenter;
			this.preLeftCircleCenter = this.leftCircleCenter;
			this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
			this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
			this.preVaRight = this.vaRight;
			this.preVaLeft = this.vaLeft;
			this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
			this.vaLeft = this.vaRight + 3.141592653589793;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0002F42C File Offset: 0x0002D82C
		public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
		{
			this.alfaRightRight = base.Atan2(this.rightCircleCenter - this.preRightCircleCenter);
			this.alfaLeftLeft = base.Atan2(this.leftCircleCenter - this.preLeftCircleCenter);
			this.alfaRightLeft = base.Atan2(this.leftCircleCenter - this.preRightCircleCenter);
			this.alfaLeftRight = base.Atan2(this.rightCircleCenter - this.preLeftCircleCenter);
			double num = (double)(this.leftCircleCenter - this.preRightCircleCenter).magnitude;
			double num2 = (double)(this.rightCircleCenter - this.preLeftCircleCenter).magnitude;
			bool flag = false;
			bool flag2 = false;
			if (num < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
			{
				num = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
				flag = true;
			}
			if (num2 < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
			{
				num2 = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
				flag2 = true;
			}
			this.deltaRightLeft = ((!flag) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num)) : 0.0);
			this.deltaLeftRight = ((!flag2) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num2)) : 0.0);
			this.betaRightRight = base.ClockwiseAngle(this.preVaRight, this.alfaRightRight - 1.5707963267948966);
			this.betaRightLeft = base.ClockwiseAngle(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft);
			this.betaLeftRight = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight);
			this.betaLeftLeft = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966);
			this.betaRightRight += base.ClockwiseAngle(this.alfaRightRight - 1.5707963267948966, this.vaRight);
			this.betaRightLeft += base.CounterClockwiseAngle(this.alfaRightLeft + this.deltaRightLeft, this.vaLeft);
			this.betaLeftRight += base.ClockwiseAngle(this.alfaLeftRight - this.deltaLeftRight, this.vaRight);
			this.betaLeftLeft += base.CounterClockwiseAngle(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft);
			this.betaRightRight = base.GetLengthFromAngle(this.betaRightRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
			this.betaRightLeft = base.GetLengthFromAngle(this.betaRightLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
			this.betaLeftRight = base.GetLengthFromAngle(this.betaLeftRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
			this.betaLeftLeft = base.GetLengthFromAngle(this.betaLeftLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
			Vector3 a = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
			Vector3 a2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
			Vector3 a3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
			Vector3 a4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
			Vector3 b = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
			Vector3 b2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft + 3.141592653589793) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
			Vector3 b3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight + 3.141592653589793) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
			Vector3 b4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
			this.betaRightRight += (double)(a - b).magnitude;
			this.betaRightLeft += (double)(a2 - b2).magnitude;
			this.betaLeftRight += (double)(a3 - b3).magnitude;
			this.betaLeftLeft += (double)(a4 - b4).magnitude;
			if (flag)
			{
				this.betaRightLeft += 10000000.0;
			}
			if (flag2)
			{
				this.betaLeftRight += 10000000.0;
			}
			turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightRight, this, 2));
			turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightLeft, this, 3));
			turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftRight, this, 4));
			turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftLeft, this, 5));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0002F998 File Offset: 0x0002DD98
		public override void PointToTangent(List<AdvancedSmooth.Turn> turnList)
		{
			bool flag = false;
			bool flag2 = false;
			float magnitude = (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude;
			float magnitude2 = (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude;
			if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
			{
				flag = true;
			}
			if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
			{
				flag2 = true;
			}
			double num = (!flag) ? base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter) : 0.0;
			double num2 = (!flag) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude))) : 0.0;
			this.gammaRight = num + num2;
			double num3 = (!flag) ? base.ClockwiseAngle(this.gammaRight, this.vaRight) : 0.0;
			double num4 = (!flag2) ? base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter) : 0.0;
			double num5 = (!flag2) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude))) : 0.0;
			this.gammaLeft = num4 - num5;
			double num6 = (!flag2) ? base.CounterClockwiseAngle(this.gammaLeft, this.vaLeft) : 0.0;
			if (!flag)
			{
				turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 0));
			}
			if (!flag2)
			{
				turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 1));
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0002FB6C File Offset: 0x0002DF6C
		public override void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
		{
			bool flag = false;
			bool flag2 = false;
			float magnitude = (AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter).magnitude;
			float magnitude2 = (AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter).magnitude;
			if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
			{
				flag = true;
			}
			if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
			{
				flag2 = true;
			}
			if (!flag)
			{
				double num = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter);
				double num2 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude));
				this.gammaRight = num - num2;
				double num3 = base.ClockwiseAngle(this.vaRight, this.gammaRight);
				turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 6));
			}
			if (!flag2)
			{
				double num4 = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter);
				double num5 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude2));
				this.gammaLeft = num4 + num5;
				double num6 = base.CounterClockwiseAngle(this.vaLeft, this.gammaLeft);
				turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 7));
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0002FC9C File Offset: 0x0002E09C
		public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
		{
			switch (turn.id)
			{
			case 0:
				base.AddCircleSegment(this.gammaRight, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 1:
				base.AddCircleSegment(this.gammaLeft, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 2:
				base.AddCircleSegment(this.preVaRight, this.alfaRightRight - 1.5707963267948966, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				base.AddCircleSegment(this.alfaRightRight - 1.5707963267948966, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 3:
				base.AddCircleSegment(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				base.AddCircleSegment(this.alfaRightLeft - this.deltaRightLeft + 3.141592653589793, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 4:
				base.AddCircleSegment(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				base.AddCircleSegment(this.alfaLeftRight + this.deltaLeftRight + 3.141592653589793, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 5:
				base.AddCircleSegment(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				base.AddCircleSegment(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 6:
				base.AddCircleSegment(this.vaRight, this.gammaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			case 7:
				base.AddCircleSegment(this.vaLeft, this.gammaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
				break;
			}
		}

		// Token: 0x040003E3 RID: 995
		private Vector3 preRightCircleCenter = Vector3.zero;

		// Token: 0x040003E4 RID: 996
		private Vector3 preLeftCircleCenter = Vector3.zero;

		// Token: 0x040003E5 RID: 997
		private Vector3 rightCircleCenter;

		// Token: 0x040003E6 RID: 998
		private Vector3 leftCircleCenter;

		// Token: 0x040003E7 RID: 999
		private double vaRight;

		// Token: 0x040003E8 RID: 1000
		private double vaLeft;

		// Token: 0x040003E9 RID: 1001
		private double preVaLeft;

		// Token: 0x040003EA RID: 1002
		private double preVaRight;

		// Token: 0x040003EB RID: 1003
		private double gammaLeft;

		// Token: 0x040003EC RID: 1004
		private double gammaRight;

		// Token: 0x040003ED RID: 1005
		private double betaRightRight;

		// Token: 0x040003EE RID: 1006
		private double betaRightLeft;

		// Token: 0x040003EF RID: 1007
		private double betaLeftRight;

		// Token: 0x040003F0 RID: 1008
		private double betaLeftLeft;

		// Token: 0x040003F1 RID: 1009
		private double deltaRightLeft;

		// Token: 0x040003F2 RID: 1010
		private double deltaLeftRight;

		// Token: 0x040003F3 RID: 1011
		private double alfaRightRight;

		// Token: 0x040003F4 RID: 1012
		private double alfaLeftLeft;

		// Token: 0x040003F5 RID: 1013
		private double alfaRightLeft;

		// Token: 0x040003F6 RID: 1014
		private double alfaLeftRight;
	}

	// Token: 0x02000096 RID: 150
	[Serializable]
	public class ConstantTurn : AdvancedSmooth.TurnConstructor
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0002FED8 File Offset: 0x0002E2D8
		public override void Prepare(int i, Vector3[] vectorPath)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0002FEDC File Offset: 0x0002E2DC
		public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
		{
			Vector3 dir = Vector3.Cross(AdvancedSmooth.TurnConstructor.t1, Vector3.up);
			Vector3 vector = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.prev;
			Vector3 start = vector * 0.5f + AdvancedSmooth.TurnConstructor.prev;
			vector = Vector3.Cross(vector, Vector3.up);
			bool flag;
			this.circleCenter = Polygon.IntersectionPointOptimized(AdvancedSmooth.TurnConstructor.prev, dir, start, vector, out flag);
			if (!flag)
			{
				return;
			}
			this.gamma1 = base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.circleCenter);
			this.gamma2 = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.circleCenter);
			this.clockwise = !Polygon.Left(this.circleCenter, AdvancedSmooth.TurnConstructor.prev, AdvancedSmooth.TurnConstructor.prev + AdvancedSmooth.TurnConstructor.t1);
			double num = (!this.clockwise) ? base.CounterClockwiseAngle(this.gamma1, this.gamma2) : base.ClockwiseAngle(this.gamma1, this.gamma2);
			num = base.GetLengthFromAngle(num, (double)(this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
			turnList.Add(new AdvancedSmooth.Turn((float)num, this, 0));
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00030014 File Offset: 0x0002E414
		public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
		{
			base.AddCircleSegment(this.gamma1, this.gamma2, this.clockwise, this.circleCenter, output, (this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
			AdvancedSmooth.TurnConstructor.normal = (AdvancedSmooth.TurnConstructor.current - this.circleCenter).normalized;
			AdvancedSmooth.TurnConstructor.t2 = Vector3.Cross(AdvancedSmooth.TurnConstructor.normal, Vector3.up).normalized;
			AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
			if (!this.clockwise)
			{
				AdvancedSmooth.TurnConstructor.t2 = -AdvancedSmooth.TurnConstructor.t2;
				AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
			}
			AdvancedSmooth.TurnConstructor.changedPreviousTangent = true;
		}

		// Token: 0x040003F7 RID: 1015
		private Vector3 circleCenter;

		// Token: 0x040003F8 RID: 1016
		private double gamma1;

		// Token: 0x040003F9 RID: 1017
		private double gamma2;

		// Token: 0x040003FA RID: 1018
		private bool clockwise;
	}

	// Token: 0x02000097 RID: 151
	public abstract class TurnConstructor
	{
		// Token: 0x060004CC RID: 1228
		public abstract void Prepare(int i, Vector3[] vectorPath);

		// Token: 0x060004CD RID: 1229 RVA: 0x0002EF09 File Offset: 0x0002D309
		public virtual void OnTangentUpdate()
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002EF0B File Offset: 0x0002D30B
		public virtual void PointToTangent(List<AdvancedSmooth.Turn> turnList)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0002EF0D File Offset: 0x0002D30D
		public virtual void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0002EF0F File Offset: 0x0002D30F
		public virtual void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
		{
		}

		// Token: 0x060004D1 RID: 1233
		public abstract void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output);

		// Token: 0x060004D2 RID: 1234 RVA: 0x0002EF14 File Offset: 0x0002D314
		public static void Setup(int i, Vector3[] vectorPath)
		{
			AdvancedSmooth.TurnConstructor.current = vectorPath[i];
			AdvancedSmooth.TurnConstructor.prev = vectorPath[i - 1];
			AdvancedSmooth.TurnConstructor.next = vectorPath[i + 1];
			AdvancedSmooth.TurnConstructor.prev.y = AdvancedSmooth.TurnConstructor.current.y;
			AdvancedSmooth.TurnConstructor.next.y = AdvancedSmooth.TurnConstructor.current.y;
			AdvancedSmooth.TurnConstructor.t1 = AdvancedSmooth.TurnConstructor.t2;
			AdvancedSmooth.TurnConstructor.t2 = (AdvancedSmooth.TurnConstructor.next - AdvancedSmooth.TurnConstructor.current).normalized - (AdvancedSmooth.TurnConstructor.prev - AdvancedSmooth.TurnConstructor.current).normalized;
			AdvancedSmooth.TurnConstructor.t2 = AdvancedSmooth.TurnConstructor.t2.normalized;
			AdvancedSmooth.TurnConstructor.prevNormal = AdvancedSmooth.TurnConstructor.normal;
			AdvancedSmooth.TurnConstructor.normal = Vector3.Cross(AdvancedSmooth.TurnConstructor.t2, Vector3.up);
			AdvancedSmooth.TurnConstructor.normal = AdvancedSmooth.TurnConstructor.normal.normalized;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0002EFFE File Offset: 0x0002D3FE
		public static void PostPrepare()
		{
			AdvancedSmooth.TurnConstructor.changedPreviousTangent = false;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0002F008 File Offset: 0x0002D408
		public void AddCircleSegment(double startAngle, double endAngle, bool clockwise, Vector3 center, List<Vector3> output, float radius)
		{
			double num = 0.06283185307179587;
			if (clockwise)
			{
				while (endAngle > startAngle + 6.283185307179586)
				{
					endAngle -= 6.283185307179586;
				}
				while (endAngle < startAngle)
				{
					endAngle += 6.283185307179586;
				}
			}
			else
			{
				while (endAngle < startAngle - 6.283185307179586)
				{
					endAngle += 6.283185307179586;
				}
				while (endAngle > startAngle)
				{
					endAngle -= 6.283185307179586;
				}
			}
			if (clockwise)
			{
				for (double num2 = startAngle; num2 < endAngle; num2 += num)
				{
					output.Add(this.AngleToVector(num2) * radius + center);
				}
			}
			else
			{
				for (double num3 = startAngle; num3 > endAngle; num3 -= num)
				{
					output.Add(this.AngleToVector(num3) * radius + center);
				}
			}
			output.Add(this.AngleToVector(endAngle) * radius + center);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0002F128 File Offset: 0x0002D528
		public void DebugCircleSegment(Vector3 center, double startAngle, double endAngle, double radius, Color color)
		{
			double num = 0.06283185307179587;
			while (endAngle < startAngle)
			{
				endAngle += 6.283185307179586;
			}
			Vector3 start = this.AngleToVector(startAngle) * (float)radius + center;
			for (double num2 = startAngle + num; num2 < endAngle; num2 += num)
			{
				Debug.DrawLine(start, this.AngleToVector(num2) * (float)radius + center);
			}
			Debug.DrawLine(start, this.AngleToVector(endAngle) * (float)radius + center);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0002F1B8 File Offset: 0x0002D5B8
		public void DebugCircle(Vector3 center, double radius, Color color)
		{
			double num = 0.06283185307179587;
			Vector3 start = this.AngleToVector(-num) * (float)radius + center;
			for (double num2 = 0.0; num2 < 6.283185307179586; num2 += num)
			{
				Vector3 vector = this.AngleToVector(num2) * (float)radius + center;
				Debug.DrawLine(start, vector, color);
				start = vector;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0002F226 File Offset: 0x0002D626
		public double GetLengthFromAngle(double angle, double radius)
		{
			return radius * angle;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0002F22B File Offset: 0x0002D62B
		public double ClockwiseAngle(double from, double to)
		{
			return this.ClampAngle(to - from);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0002F236 File Offset: 0x0002D636
		public double CounterClockwiseAngle(double from, double to)
		{
			return this.ClampAngle(from - to);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0002F241 File Offset: 0x0002D641
		public Vector3 AngleToVector(double a)
		{
			return new Vector3((float)Math.Cos(a), 0f, (float)Math.Sin(a));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0002F25B File Offset: 0x0002D65B
		public double ToDegrees(double rad)
		{
			return rad * 57.295780181884766;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0002F268 File Offset: 0x0002D668
		public double ClampAngle(double a)
		{
			while (a < 0.0)
			{
				a += 6.283185307179586;
			}
			while (a > 6.283185307179586)
			{
				a -= 6.283185307179586;
			}
			return a;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0002F2B8 File Offset: 0x0002D6B8
		public double Atan2(Vector3 v)
		{
			return Math.Atan2((double)v.z, (double)v.x);
		}

		// Token: 0x040003FB RID: 1019
		public float constantBias;

		// Token: 0x040003FC RID: 1020
		public float factorBias = 1f;

		// Token: 0x040003FD RID: 1021
		public static float turningRadius = 1f;

		// Token: 0x040003FE RID: 1022
		public const double ThreeSixtyRadians = 6.283185307179586;

		// Token: 0x040003FF RID: 1023
		public static Vector3 prev;

		// Token: 0x04000400 RID: 1024
		public static Vector3 current;

		// Token: 0x04000401 RID: 1025
		public static Vector3 next;

		// Token: 0x04000402 RID: 1026
		public static Vector3 t1;

		// Token: 0x04000403 RID: 1027
		public static Vector3 t2;

		// Token: 0x04000404 RID: 1028
		public static Vector3 normal;

		// Token: 0x04000405 RID: 1029
		public static Vector3 prevNormal;

		// Token: 0x04000406 RID: 1030
		public static bool changedPreviousTangent;
	}

	// Token: 0x02000098 RID: 152
	public struct Turn : IComparable<AdvancedSmooth.Turn>
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x000300CF File Offset: 0x0002E4CF
		public Turn(float length, AdvancedSmooth.TurnConstructor constructor, int id = 0)
		{
			this.length = length;
			this.id = id;
			this.constructor = constructor;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000300E6 File Offset: 0x0002E4E6
		public float score
		{
			get
			{
				return this.length * this.constructor.factorBias + this.constructor.constantBias;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00030106 File Offset: 0x0002E506
		public void GetPath(List<Vector3> output)
		{
			this.constructor.GetPath(this, output);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0003011A File Offset: 0x0002E51A
		public int CompareTo(AdvancedSmooth.Turn t)
		{
			return (t.score <= this.score) ? ((t.score >= this.score) ? 0 : 1) : -1;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0003014D File Offset: 0x0002E54D
		public static bool operator <(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
		{
			return lhs.score < rhs.score;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0003015F File Offset: 0x0002E55F
		public static bool operator >(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
		{
			return lhs.score > rhs.score;
		}

		// Token: 0x04000407 RID: 1031
		public float length;

		// Token: 0x04000408 RID: 1032
		public int id;

		// Token: 0x04000409 RID: 1033
		public AdvancedSmooth.TurnConstructor constructor;
	}
}
