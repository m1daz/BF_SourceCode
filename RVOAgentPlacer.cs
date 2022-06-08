using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class RVOAgentPlacer : MonoBehaviour
{
	// Token: 0x060002FE RID: 766 RVA: 0x00016898 File Offset: 0x00014C98
	private IEnumerator Start()
	{
		yield return 0;
		for (int i = 0; i < this.agents; i++)
		{
			float num = (float)i / (float)this.agents * 3.1415927f * 2f;
			Vector3 vector = new Vector3((float)Math.Cos((double)num), 0f, (float)Math.Sin((double)num)) * this.ringSize;
			Vector3 target = -vector + this.goalOffset;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, Vector3.zero, Quaternion.Euler(0f, num + 180f, 0f));
			RVOExampleAgent component = gameObject.GetComponent<RVOExampleAgent>();
			if (component == null)
			{
				Debug.LogError("Prefab does not have an RVOExampleAgent component attached");
				yield break;
			}
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = vector;
			component.repathRate = this.repathRate;
			component.SetTarget(target);
			component.SetColor(this.GetColor(num));
		}
		yield break;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x000168B3 File Offset: 0x00014CB3
	public Color GetColor(float angle)
	{
		return RVOAgentPlacer.HSVToRGB(angle * 57.295776f, 1f, 1f);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x000168CC File Offset: 0x00014CCC
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

	// Token: 0x0400025C RID: 604
	public int agents = 100;

	// Token: 0x0400025D RID: 605
	public float ringSize = 100f;

	// Token: 0x0400025E RID: 606
	public LayerMask mask;

	// Token: 0x0400025F RID: 607
	public GameObject prefab;

	// Token: 0x04000260 RID: 608
	public Vector3 goalOffset;

	// Token: 0x04000261 RID: 609
	public float repathRate = 1f;

	// Token: 0x04000262 RID: 610
	private const float rad2Deg = 57.295776f;
}
