using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class GGExplosionDamage : MonoBehaviour
{
	// Token: 0x06000E70 RID: 3696 RVA: 0x000795E4 File Offset: 0x000779E4
	private void Start()
	{
		Vector3 position = base.transform.position;
		Collider[] array = Physics.OverlapSphere(position, this.explosionRadius);
		foreach (Collider collider in array)
		{
			Vector3 a = collider.ClosestPointOnBounds(position);
			float num = Vector3.Distance(a, position);
			float num2 = 1f - Mathf.Clamp01(num / this.explosionRadius);
			num2 *= this.explosionDamage;
			collider.SendMessageUpwards("ApplyDamage", num2, SendMessageOptions.DontRequireReceiver);
		}
		base.StartCoroutine(this.EmitterControl());
		UnityEngine.Object.Destroy(base.gameObject, this.explosionTimeout);
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x00079690 File Offset: 0x00077A90
	private IEnumerator EmitterControl()
	{
		if (base.GetComponent<ParticleEmitter>())
		{
			base.GetComponent<ParticleEmitter>().emit = true;
			yield return new WaitForSeconds(0.5f);
			base.GetComponent<ParticleEmitter>().emit = false;
		}
		yield break;
	}

	// Token: 0x04000FA9 RID: 4009
	public float explosionRadius = 5f;

	// Token: 0x04000FAA RID: 4010
	public float explosionPower = 10f;

	// Token: 0x04000FAB RID: 4011
	public float explosionDamage = 100f;

	// Token: 0x04000FAC RID: 4012
	public float explosionTimeout = 2f;

	// Token: 0x04000FAD RID: 4013
	private GameObject player1;
}
