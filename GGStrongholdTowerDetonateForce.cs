using System;
using UnityEngine;

// Token: 0x02000254 RID: 596
public class GGStrongholdTowerDetonateForce : MonoBehaviour
{
	// Token: 0x0600112B RID: 4395 RVA: 0x00099237 File Offset: 0x00097637
	private void Awake()
	{
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x00099239 File Offset: 0x00097639
	private void Start()
	{
		this.Explode();
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x00099241 File Offset: 0x00097641
	private void Update()
	{
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x00099244 File Offset: 0x00097644
	public void Explode()
	{
		this._explosionPosition = base.transform.position;
		this._colliders = Physics.OverlapSphere(this._explosionPosition, this.radius);
		foreach (Collider collider in this._colliders)
		{
			if (collider)
			{
				if (collider.transform.tag == "Player")
				{
					float num = UnityEngine.Random.Range(40f, 50f);
					Vector3 position = collider.transform.position;
					float num2 = Vector3.Distance(position, this._explosionPosition);
					short damage = (short)((1f - Mathf.Clamp01(num2 / this.radius)) * num);
					this.DamageEventArgs.damage = damage;
					if (this.team != collider.transform.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
					{
						collider.transform.SendMessageUpwards("Tower_Damage", this.DamageEventArgs, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// Token: 0x040013C0 RID: 5056
	private float _explodeDelay;

	// Token: 0x040013C1 RID: 5057
	public float radius = 5f;

	// Token: 0x040013C2 RID: 5058
	public float power;

	// Token: 0x040013C3 RID: 5059
	public GameObject fireObject;

	// Token: 0x040013C4 RID: 5060
	public float fireObjectLife;

	// Token: 0x040013C5 RID: 5061
	private Collider[] _colliders;

	// Token: 0x040013C6 RID: 5062
	private GameObject _tempFireObject;

	// Token: 0x040013C7 RID: 5063
	private Vector3 _explosionPosition;

	// Token: 0x040013C8 RID: 5064
	public float speed;

	// Token: 0x040013C9 RID: 5065
	public GGTeamType team;

	// Token: 0x040013CA RID: 5066
	private GGDamageEventArgs DamageEventArgs = new GGDamageEventArgs();
}
