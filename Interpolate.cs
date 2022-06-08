using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class Interpolate
{
	// Token: 0x06002098 RID: 8344 RVA: 0x000F3491 File Offset: 0x000F1891
	private static Vector3 Identity(Vector3 v)
	{
		return v;
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x000F3494 File Offset: 0x000F1894
	private static Vector3 TransformDotPosition(Transform t)
	{
		return t.position;
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x000F349C File Offset: 0x000F189C
	private static IEnumerable<float> NewTimer(float duration)
	{
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			yield return elapsedTime;
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= duration)
			{
				yield return elapsedTime;
			}
		}
		yield break;
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x000F34C0 File Offset: 0x000F18C0
	private static IEnumerable<float> NewCounter(int start, int end, int step)
	{
		for (int i = start; i <= end; i += step)
		{
			yield return (float)i;
		}
		yield break;
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x000F34F4 File Offset: 0x000F18F4
	public static IEnumerator NewEase(Interpolate.Function ease, Vector3 start, Vector3 end, float duration)
	{
		IEnumerable<float> driver = Interpolate.NewTimer(duration);
		return Interpolate.NewEase(ease, start, end, duration, driver);
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x000F3514 File Offset: 0x000F1914
	public static IEnumerator NewEase(Interpolate.Function ease, Vector3 start, Vector3 end, int slices)
	{
		IEnumerable<float> driver = Interpolate.NewCounter(0, slices + 1, 1);
		return Interpolate.NewEase(ease, start, end, (float)(slices + 1), driver);
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x000F353C File Offset: 0x000F193C
	private static IEnumerator NewEase(Interpolate.Function ease, Vector3 start, Vector3 end, float total, IEnumerable<float> driver)
	{
		Vector3 distance = end - start;
		foreach (float num in driver)
		{
			float i = num;
			yield return Interpolate.Ease(ease, start, distance, i, total);
		}
		yield break;
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x000F3574 File Offset: 0x000F1974
	private static Vector3 Ease(Interpolate.Function ease, Vector3 start, Vector3 distance, float elapsedTime, float duration)
	{
		start.x = ease(start.x, distance.x, elapsedTime, duration);
		start.y = ease(start.y, distance.y, elapsedTime, duration);
		start.z = ease(start.z, distance.z, elapsedTime, duration);
		return start;
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000F35DC File Offset: 0x000F19DC
	public static Interpolate.Function Ease(Interpolate.EaseType type)
	{
		Interpolate.Function result = null;
		switch (type)
		{
		case Interpolate.EaseType.Linear:
			if (Interpolate.<>f__mg$cache0 == null)
			{
				Interpolate.<>f__mg$cache0 = new Interpolate.Function(Interpolate.Linear);
			}
			result = Interpolate.<>f__mg$cache0;
			break;
		case Interpolate.EaseType.EaseInQuad:
			if (Interpolate.<>f__mg$cache1 == null)
			{
				Interpolate.<>f__mg$cache1 = new Interpolate.Function(Interpolate.EaseInQuad);
			}
			result = Interpolate.<>f__mg$cache1;
			break;
		case Interpolate.EaseType.EaseOutQuad:
			if (Interpolate.<>f__mg$cache2 == null)
			{
				Interpolate.<>f__mg$cache2 = new Interpolate.Function(Interpolate.EaseOutQuad);
			}
			result = Interpolate.<>f__mg$cache2;
			break;
		case Interpolate.EaseType.EaseInOutQuad:
			if (Interpolate.<>f__mg$cache3 == null)
			{
				Interpolate.<>f__mg$cache3 = new Interpolate.Function(Interpolate.EaseInOutQuad);
			}
			result = Interpolate.<>f__mg$cache3;
			break;
		case Interpolate.EaseType.EaseInCubic:
			if (Interpolate.<>f__mg$cache4 == null)
			{
				Interpolate.<>f__mg$cache4 = new Interpolate.Function(Interpolate.EaseInCubic);
			}
			result = Interpolate.<>f__mg$cache4;
			break;
		case Interpolate.EaseType.EaseOutCubic:
			if (Interpolate.<>f__mg$cache5 == null)
			{
				Interpolate.<>f__mg$cache5 = new Interpolate.Function(Interpolate.EaseOutCubic);
			}
			result = Interpolate.<>f__mg$cache5;
			break;
		case Interpolate.EaseType.EaseInOutCubic:
			if (Interpolate.<>f__mg$cache6 == null)
			{
				Interpolate.<>f__mg$cache6 = new Interpolate.Function(Interpolate.EaseInOutCubic);
			}
			result = Interpolate.<>f__mg$cache6;
			break;
		case Interpolate.EaseType.EaseInQuart:
			if (Interpolate.<>f__mg$cache7 == null)
			{
				Interpolate.<>f__mg$cache7 = new Interpolate.Function(Interpolate.EaseInQuart);
			}
			result = Interpolate.<>f__mg$cache7;
			break;
		case Interpolate.EaseType.EaseOutQuart:
			if (Interpolate.<>f__mg$cache8 == null)
			{
				Interpolate.<>f__mg$cache8 = new Interpolate.Function(Interpolate.EaseOutQuart);
			}
			result = Interpolate.<>f__mg$cache8;
			break;
		case Interpolate.EaseType.EaseInOutQuart:
			if (Interpolate.<>f__mg$cache9 == null)
			{
				Interpolate.<>f__mg$cache9 = new Interpolate.Function(Interpolate.EaseInOutQuart);
			}
			result = Interpolate.<>f__mg$cache9;
			break;
		case Interpolate.EaseType.EaseInQuint:
			if (Interpolate.<>f__mg$cacheA == null)
			{
				Interpolate.<>f__mg$cacheA = new Interpolate.Function(Interpolate.EaseInQuint);
			}
			result = Interpolate.<>f__mg$cacheA;
			break;
		case Interpolate.EaseType.EaseOutQuint:
			if (Interpolate.<>f__mg$cacheB == null)
			{
				Interpolate.<>f__mg$cacheB = new Interpolate.Function(Interpolate.EaseOutQuint);
			}
			result = Interpolate.<>f__mg$cacheB;
			break;
		case Interpolate.EaseType.EaseInOutQuint:
			if (Interpolate.<>f__mg$cacheC == null)
			{
				Interpolate.<>f__mg$cacheC = new Interpolate.Function(Interpolate.EaseInOutQuint);
			}
			result = Interpolate.<>f__mg$cacheC;
			break;
		case Interpolate.EaseType.EaseInSine:
			if (Interpolate.<>f__mg$cacheD == null)
			{
				Interpolate.<>f__mg$cacheD = new Interpolate.Function(Interpolate.EaseInSine);
			}
			result = Interpolate.<>f__mg$cacheD;
			break;
		case Interpolate.EaseType.EaseOutSine:
			if (Interpolate.<>f__mg$cacheE == null)
			{
				Interpolate.<>f__mg$cacheE = new Interpolate.Function(Interpolate.EaseOutSine);
			}
			result = Interpolate.<>f__mg$cacheE;
			break;
		case Interpolate.EaseType.EaseInOutSine:
			if (Interpolate.<>f__mg$cacheF == null)
			{
				Interpolate.<>f__mg$cacheF = new Interpolate.Function(Interpolate.EaseInOutSine);
			}
			result = Interpolate.<>f__mg$cacheF;
			break;
		case Interpolate.EaseType.EaseInExpo:
			if (Interpolate.<>f__mg$cache10 == null)
			{
				Interpolate.<>f__mg$cache10 = new Interpolate.Function(Interpolate.EaseInExpo);
			}
			result = Interpolate.<>f__mg$cache10;
			break;
		case Interpolate.EaseType.EaseOutExpo:
			if (Interpolate.<>f__mg$cache11 == null)
			{
				Interpolate.<>f__mg$cache11 = new Interpolate.Function(Interpolate.EaseOutExpo);
			}
			result = Interpolate.<>f__mg$cache11;
			break;
		case Interpolate.EaseType.EaseInOutExpo:
			if (Interpolate.<>f__mg$cache12 == null)
			{
				Interpolate.<>f__mg$cache12 = new Interpolate.Function(Interpolate.EaseInOutExpo);
			}
			result = Interpolate.<>f__mg$cache12;
			break;
		case Interpolate.EaseType.EaseInCirc:
			if (Interpolate.<>f__mg$cache13 == null)
			{
				Interpolate.<>f__mg$cache13 = new Interpolate.Function(Interpolate.EaseInCirc);
			}
			result = Interpolate.<>f__mg$cache13;
			break;
		case Interpolate.EaseType.EaseOutCirc:
			if (Interpolate.<>f__mg$cache14 == null)
			{
				Interpolate.<>f__mg$cache14 = new Interpolate.Function(Interpolate.EaseOutCirc);
			}
			result = Interpolate.<>f__mg$cache14;
			break;
		case Interpolate.EaseType.EaseInOutCirc:
			if (Interpolate.<>f__mg$cache15 == null)
			{
				Interpolate.<>f__mg$cache15 = new Interpolate.Function(Interpolate.EaseInOutCirc);
			}
			result = Interpolate.<>f__mg$cache15;
			break;
		}
		return result;
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000F3954 File Offset: 0x000F1D54
	public static IEnumerable<Vector3> NewBezier(Interpolate.Function ease, Transform[] nodes, float duration)
	{
		IEnumerable<float> steps = Interpolate.NewTimer(duration);
		if (Interpolate.<>f__mg$cache16 == null)
		{
			Interpolate.<>f__mg$cache16 = new Interpolate.ToVector3<Transform>(Interpolate.TransformDotPosition);
		}
		return Interpolate.NewBezier<Transform>(ease, nodes, Interpolate.<>f__mg$cache16, duration, steps);
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x000F3990 File Offset: 0x000F1D90
	public static IEnumerable<Vector3> NewBezier(Interpolate.Function ease, Transform[] nodes, int slices)
	{
		IEnumerable<float> steps = Interpolate.NewCounter(0, slices + 1, 1);
		if (Interpolate.<>f__mg$cache17 == null)
		{
			Interpolate.<>f__mg$cache17 = new Interpolate.ToVector3<Transform>(Interpolate.TransformDotPosition);
		}
		return Interpolate.NewBezier<Transform>(ease, nodes, Interpolate.<>f__mg$cache17, (float)(slices + 1), steps);
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x000F39D4 File Offset: 0x000F1DD4
	public static IEnumerable<Vector3> NewBezier(Interpolate.Function ease, Vector3[] points, float duration)
	{
		IEnumerable<float> steps = Interpolate.NewTimer(duration);
		if (Interpolate.<>f__mg$cache18 == null)
		{
			Interpolate.<>f__mg$cache18 = new Interpolate.ToVector3<Vector3>(Interpolate.Identity);
		}
		return Interpolate.NewBezier<Vector3>(ease, points, Interpolate.<>f__mg$cache18, duration, steps);
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x000F3A10 File Offset: 0x000F1E10
	public static IEnumerable<Vector3> NewBezier(Interpolate.Function ease, Vector3[] points, int slices)
	{
		IEnumerable<float> steps = Interpolate.NewCounter(0, slices + 1, 1);
		if (Interpolate.<>f__mg$cache19 == null)
		{
			Interpolate.<>f__mg$cache19 = new Interpolate.ToVector3<Vector3>(Interpolate.Identity);
		}
		return Interpolate.NewBezier<Vector3>(ease, points, Interpolate.<>f__mg$cache19, (float)(slices + 1), steps);
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x000F3A54 File Offset: 0x000F1E54
	private static IEnumerable<Vector3> NewBezier<T>(Interpolate.Function ease, IList nodes, Interpolate.ToVector3<T> toVector3, float maxStep, IEnumerable<float> steps)
	{
		if (nodes.Count >= 2)
		{
			Vector3[] points = new Vector3[nodes.Count];
			foreach (float num in steps)
			{
				float step = num;
				for (int i = 0; i < nodes.Count; i++)
				{
					points[i] = toVector3((T)((object)nodes[i]));
				}
				yield return Interpolate.Bezier(ease, points, step, maxStep);
			}
		}
		yield break;
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x000F3A94 File Offset: 0x000F1E94
	private static Vector3 Bezier(Interpolate.Function ease, Vector3[] points, float elapsedTime, float duration)
	{
		for (int i = points.Length - 1; i > 0; i--)
		{
			for (int j = 0; j < i; j++)
			{
				points[j].x = ease(points[j].x, points[j + 1].x - points[j].x, elapsedTime, duration);
				points[j].y = ease(points[j].y, points[j + 1].y - points[j].y, elapsedTime, duration);
				points[j].z = ease(points[j].z, points[j + 1].z - points[j].z, elapsedTime, duration);
			}
		}
		return points[0];
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x000F3B86 File Offset: 0x000F1F86
	public static IEnumerable<Vector3> NewCatmullRom(Transform[] nodes, int slices, bool loop)
	{
		if (Interpolate.<>f__mg$cache1A == null)
		{
			Interpolate.<>f__mg$cache1A = new Interpolate.ToVector3<Transform>(Interpolate.TransformDotPosition);
		}
		return Interpolate.NewCatmullRom<Transform>(nodes, Interpolate.<>f__mg$cache1A, slices, loop);
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000F3BAD File Offset: 0x000F1FAD
	public static IEnumerable<Vector3> NewCatmullRom(Vector3[] points, int slices, bool loop)
	{
		if (Interpolate.<>f__mg$cache1B == null)
		{
			Interpolate.<>f__mg$cache1B = new Interpolate.ToVector3<Vector3>(Interpolate.Identity);
		}
		return Interpolate.NewCatmullRom<Vector3>(points, Interpolate.<>f__mg$cache1B, slices, loop);
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x000F3BD4 File Offset: 0x000F1FD4
	private static IEnumerable<Vector3> NewCatmullRom<T>(IList nodes, Interpolate.ToVector3<T> toVector3, int slices, bool loop)
	{
		if (nodes.Count >= 2)
		{
			yield return toVector3((T)((object)nodes[0]));
			int last = nodes.Count - 1;
			int current = 0;
			while (loop || current < last)
			{
				if (loop && current > last)
				{
					current = 0;
				}
				int previous = (current != 0) ? (current - 1) : ((!loop) ? current : last);
				int start = current;
				int end = (current != last) ? (current + 1) : ((!loop) ? current : 0);
				int next = (end != last) ? (end + 1) : ((!loop) ? end : 0);
				int stepCount = slices + 1;
				for (int step = 1; step <= stepCount; step++)
				{
					yield return Interpolate.CatmullRom(toVector3((T)((object)nodes[previous])), toVector3((T)((object)nodes[start])), toVector3((T)((object)nodes[end])), toVector3((T)((object)nodes[next])), (float)step, (float)stepCount);
				}
				current++;
			}
		}
		yield break;
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x000F3C0C File Offset: 0x000F200C
	private static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime, float duration)
	{
		float num = elapsedTime / duration;
		float num2 = num * num;
		float num3 = num2 * num;
		return previous * (-0.5f * num3 + num2 - 0.5f * num) + start * (1.5f * num3 + -2.5f * num2 + 1f) + end * (-1.5f * num3 + 2f * num2 + 0.5f * num) + next * (0.5f * num3 - 0.5f * num2);
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x000F3C9A File Offset: 0x000F209A
	private static float Linear(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * (elapsedTime / duration) + start;
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x000F3CAD File Offset: 0x000F20AD
	private static float EaseInQuad(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime + start;
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x000F3CCC File Offset: 0x000F20CC
	private static float EaseOutQuad(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return -distance * elapsedTime * (elapsedTime - 2f) + start;
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x000F3CF4 File Offset: 0x000F20F4
	private static float EaseInOutQuad(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 1f;
		return -distance / 2f * (elapsedTime * (elapsedTime - 2f) - 1f) + start;
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x000F3D59 File Offset: 0x000F2159
	private static float EaseInCubic(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime * elapsedTime + start;
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x000F3D7A File Offset: 0x000F217A
	private static float EaseOutCubic(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return distance * (elapsedTime * elapsedTime * elapsedTime + 1f) + start;
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x000F3DAC File Offset: 0x000F21AC
	private static float EaseInOutCubic(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 2f;
		return distance / 2f * (elapsedTime * elapsedTime * elapsedTime + 2f) + start;
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x000F3E0E File Offset: 0x000F220E
	private static float EaseInQuart(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x000F3E31 File Offset: 0x000F2231
	private static float EaseOutQuart(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return -distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 1f) + start;
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x000F3E64 File Offset: 0x000F2264
	private static float EaseInOutQuart(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 2f;
		return -distance / 2f * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 2f) + start;
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x000F3ECB File Offset: 0x000F22CB
	private static float EaseInQuint(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x000F3EF0 File Offset: 0x000F22F0
	private static float EaseOutQuint(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 1f) + start;
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x000F3F24 File Offset: 0x000F2324
	private static float EaseInOutQuint(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 2f;
		return distance / 2f * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 2f) + start;
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x000F3F8E File Offset: 0x000F238E
	private static float EaseInSine(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return -distance * Mathf.Cos(elapsedTime / duration * 1.5707964f) + distance + start;
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x000F3FAF File Offset: 0x000F23AF
	private static float EaseOutSine(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * Mathf.Sin(elapsedTime / duration * 1.5707964f) + start;
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x000F3FCD File Offset: 0x000F23CD
	private static float EaseInOutSine(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return -distance / 2f * (Mathf.Cos(3.1415927f * elapsedTime / duration) - 1f) + start;
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x000F3FF8 File Offset: 0x000F23F8
	private static float EaseInExpo(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * Mathf.Pow(2f, 10f * (elapsedTime / duration - 1f)) + start;
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x000F4021 File Offset: 0x000F2421
	private static float EaseOutExpo(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * (-Mathf.Pow(2f, -10f * elapsedTime / duration) + 1f) + start;
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x000F404C File Offset: 0x000F244C
	private static float EaseInOutExpo(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * Mathf.Pow(2f, 10f * (elapsedTime - 1f)) + start;
		}
		elapsedTime -= 1f;
		return distance / 2f * (-Mathf.Pow(2f, -10f * elapsedTime) + 2f) + start;
	}

	// Token: 0x060020BE RID: 8382 RVA: 0x000F40CD File Offset: 0x000F24CD
	private static float EaseInCirc(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return -distance * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) - 1f) + start;
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000F40FE File Offset: 0x000F24FE
	private static float EaseOutCirc(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return distance * Mathf.Sqrt(1f - elapsedTime * elapsedTime) + start;
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x000F4134 File Offset: 0x000F2534
	private static float EaseInOutCirc(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return -distance / 2f * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) - 1f) + start;
		}
		elapsedTime -= 2f;
		return distance / 2f * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) + 1f) + start;
	}

	// Token: 0x04002162 RID: 8546
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache0;

	// Token: 0x04002163 RID: 8547
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache1;

	// Token: 0x04002164 RID: 8548
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache2;

	// Token: 0x04002165 RID: 8549
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache3;

	// Token: 0x04002166 RID: 8550
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache4;

	// Token: 0x04002167 RID: 8551
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache5;

	// Token: 0x04002168 RID: 8552
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache6;

	// Token: 0x04002169 RID: 8553
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache7;

	// Token: 0x0400216A RID: 8554
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache8;

	// Token: 0x0400216B RID: 8555
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache9;

	// Token: 0x0400216C RID: 8556
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cacheA;

	// Token: 0x0400216D RID: 8557
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cacheB;

	// Token: 0x0400216E RID: 8558
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cacheC;

	// Token: 0x0400216F RID: 8559
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cacheD;

	// Token: 0x04002170 RID: 8560
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cacheE;

	// Token: 0x04002171 RID: 8561
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cacheF;

	// Token: 0x04002172 RID: 8562
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache10;

	// Token: 0x04002173 RID: 8563
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache11;

	// Token: 0x04002174 RID: 8564
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache12;

	// Token: 0x04002175 RID: 8565
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache13;

	// Token: 0x04002176 RID: 8566
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache14;

	// Token: 0x04002177 RID: 8567
	[CompilerGenerated]
	private static Interpolate.Function <>f__mg$cache15;

	// Token: 0x04002178 RID: 8568
	[CompilerGenerated]
	private static Interpolate.ToVector3<Transform> <>f__mg$cache16;

	// Token: 0x04002179 RID: 8569
	[CompilerGenerated]
	private static Interpolate.ToVector3<Transform> <>f__mg$cache17;

	// Token: 0x0400217A RID: 8570
	[CompilerGenerated]
	private static Interpolate.ToVector3<Vector3> <>f__mg$cache18;

	// Token: 0x0400217B RID: 8571
	[CompilerGenerated]
	private static Interpolate.ToVector3<Vector3> <>f__mg$cache19;

	// Token: 0x0400217C RID: 8572
	[CompilerGenerated]
	private static Interpolate.ToVector3<Transform> <>f__mg$cache1A;

	// Token: 0x0400217D RID: 8573
	[CompilerGenerated]
	private static Interpolate.ToVector3<Vector3> <>f__mg$cache1B;

	// Token: 0x02000467 RID: 1127
	public enum EaseType
	{
		// Token: 0x0400217F RID: 8575
		Linear,
		// Token: 0x04002180 RID: 8576
		EaseInQuad,
		// Token: 0x04002181 RID: 8577
		EaseOutQuad,
		// Token: 0x04002182 RID: 8578
		EaseInOutQuad,
		// Token: 0x04002183 RID: 8579
		EaseInCubic,
		// Token: 0x04002184 RID: 8580
		EaseOutCubic,
		// Token: 0x04002185 RID: 8581
		EaseInOutCubic,
		// Token: 0x04002186 RID: 8582
		EaseInQuart,
		// Token: 0x04002187 RID: 8583
		EaseOutQuart,
		// Token: 0x04002188 RID: 8584
		EaseInOutQuart,
		// Token: 0x04002189 RID: 8585
		EaseInQuint,
		// Token: 0x0400218A RID: 8586
		EaseOutQuint,
		// Token: 0x0400218B RID: 8587
		EaseInOutQuint,
		// Token: 0x0400218C RID: 8588
		EaseInSine,
		// Token: 0x0400218D RID: 8589
		EaseOutSine,
		// Token: 0x0400218E RID: 8590
		EaseInOutSine,
		// Token: 0x0400218F RID: 8591
		EaseInExpo,
		// Token: 0x04002190 RID: 8592
		EaseOutExpo,
		// Token: 0x04002191 RID: 8593
		EaseInOutExpo,
		// Token: 0x04002192 RID: 8594
		EaseInCirc,
		// Token: 0x04002193 RID: 8595
		EaseOutCirc,
		// Token: 0x04002194 RID: 8596
		EaseInOutCirc
	}

	// Token: 0x02000468 RID: 1128
	// (Invoke) Token: 0x060020C2 RID: 8386
	public delegate Vector3 ToVector3<T>(T v);

	// Token: 0x02000469 RID: 1129
	// (Invoke) Token: 0x060020C6 RID: 8390
	public delegate float Function(float a, float b, float c, float d);
}
