using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class GGStrongholdTowerProjectile : MonoBehaviour
{
	// Token: 0x06001130 RID: 4400 RVA: 0x0009937C File Offset: 0x0009777C
	private void Start()
	{
		this.Data = GGNetworkKit.mInstance.GetPropDataByViewID(base.GetComponent<PhotonView>().viewID);
		if (this.Data != null)
		{
			base.GetComponent<Rigidbody>().velocity = base.transform.TransformDirection(new Vector3(0f, 6f, (float)this.Data[0] * 0.25f));
		}
		else
		{
			Debug.Log("data is null, viewid: " + base.GetComponent<PhotonView>().viewID);
		}
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x0009940B File Offset: 0x0009780B
	private void FixedUpdate()
	{
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x00099410 File Offset: 0x00097810
	private void OnCollisionEnter(Collision collision)
	{
		if (this.killCreateCount == 0)
		{
			this.contact = collision.contacts[0];
			this.rotation = Quaternion.FromToRotation(Vector3.up, this.contact.normal);
			if (this.destroyDelay > 0f)
			{
				return;
			}
			this.Kill();
			this.killCreateCount = 1;
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00099478 File Offset: 0x00097878
	private void Kill()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.explosion, base.transform.position, this.rotation);
		if (this.Data != null)
		{
			gameObject.GetComponent<GGStrongholdTowerDetonateForce>().team = (GGTeamType)this.Data[1];
		}
		base.transform.Find("TowerGrenadeObj/FirePlace_0").GetComponent<ParticleSystem>().Stop();
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			base.StartCoroutine(this.DestroyProjectile(3f));
		}
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x00099500 File Offset: 0x00097900
	private IEnumerator DestroyProjectile(float delay)
	{
		yield return new WaitForSeconds(delay);
		GGNetworkKit.mInstance.DestroySceneObjectRPC(base.gameObject);
		yield break;
	}

	// Token: 0x040013CB RID: 5067
	public GameObject explosion;

	// Token: 0x040013CC RID: 5068
	public float destroyDelay;

	// Token: 0x040013CD RID: 5069
	public float timeOut = 3f;

	// Token: 0x040013CE RID: 5070
	public GameObject[] objectsToDestroy;

	// Token: 0x040013CF RID: 5071
	private ContactPoint contact;

	// Token: 0x040013D0 RID: 5072
	private Quaternion rotation;

	// Token: 0x040013D1 RID: 5073
	public string shooter = string.Empty;

	// Token: 0x040013D2 RID: 5074
	public float DetonatorDamage;

	// Token: 0x040013D3 RID: 5075
	public GGTeamType team = GGTeamType.Nil;

	// Token: 0x040013D4 RID: 5076
	public bool isPlused;

	// Token: 0x040013D5 RID: 5077
	private object[] Data = new object[2];

	// Token: 0x040013D6 RID: 5078
	private int killCreateCount;
}
