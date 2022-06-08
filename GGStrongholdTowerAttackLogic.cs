using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class GGStrongholdTowerAttackLogic : MonoBehaviour
{
	// Token: 0x06001128 RID: 4392 RVA: 0x00098CAD File Offset: 0x000970AD
	private void Start()
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.StrongHold)
		{
			this.isStrongholdMode = true;
		}
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x00098CC8 File Offset: 0x000970C8
	private void Update()
	{
		if (!this.isStrongholdMode)
		{
			return;
		}
		this.FireTimeCount += Time.deltaTime;
		if (this.FireTimeCount > this.FireRate)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				this.colliders = Physics.OverlapSphere(this.FirePoint.position - new Vector3(0f, 5f, 0f), this.radius);
				foreach (Collider collider in this.colliders)
				{
					if (collider.gameObject.tag == "Player")
					{
						if (this.mStrongHold.holdState == GGStrondholdState.BlueOccupation && collider.gameObject.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							if (Vector3.Distance(this.FirePoint.position, collider.gameObject.transform.position) > 6f)
							{
								this.FirePoint.LookAt(collider.gameObject.transform.position, base.transform.up);
								float num = Vector3.Distance(this.FirePoint.position, collider.gameObject.transform.position);
								object[] array2 = new object[2];
								array2[0] = num;
								if (this.mStrongHold.holdState == GGStrondholdState.BlueOccupation)
								{
									array2[1] = GGTeamType.blue;
								}
								else if (this.mStrongHold.holdState == GGStrondholdState.RedOccupation)
								{
									array2[1] = GGTeamType.red;
								}
								GGNetworkKit.mInstance.CreateSeceneObject(this.TowerGrenadePrefab, this.FirePoint.position, this.FirePoint.rotation, array2);
								break;
							}
						}
						else if (this.mStrongHold.holdState == GGStrondholdState.RedOccupation && collider.gameObject.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && Vector3.Distance(this.FirePoint.position, collider.gameObject.transform.position) > 6f)
						{
							this.FirePoint.LookAt(collider.gameObject.transform.position, base.transform.up);
							float num2 = Vector3.Distance(this.FirePoint.position, collider.gameObject.transform.position);
							object[] array3 = new object[2];
							array3[0] = num2;
							if (this.mStrongHold.holdState == GGStrondholdState.BlueOccupation)
							{
								array3[1] = GGTeamType.blue;
							}
							else if (this.mStrongHold.holdState == GGStrondholdState.RedOccupation)
							{
								array3[1] = GGTeamType.red;
							}
							GGNetworkKit.mInstance.CreateSeceneObject(this.TowerGrenadePrefab, this.FirePoint.position, this.FirePoint.rotation, array3);
							break;
						}
					}
					else if (collider.gameObject.tag == "EnemyHeadTag")
					{
						if (this.mStrongHold.holdState == GGStrondholdState.BlueOccupation && collider.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							if (Vector3.Distance(this.FirePoint.position, collider.transform.root.position) > 6f)
							{
								this.FirePoint.LookAt(collider.transform.root.position, base.transform.up);
								float num3 = Vector3.Distance(this.FirePoint.position, collider.transform.root.position);
								object[] array4 = new object[2];
								array4[0] = num3;
								if (this.mStrongHold.holdState == GGStrondholdState.BlueOccupation)
								{
									array4[1] = GGTeamType.blue;
								}
								else if (this.mStrongHold.holdState == GGStrondholdState.RedOccupation)
								{
									array4[1] = GGTeamType.red;
								}
								GGNetworkKit.mInstance.CreateSeceneObject(this.TowerGrenadePrefab, this.FirePoint.position, this.FirePoint.rotation, array4);
								break;
							}
						}
						else if (this.mStrongHold.holdState == GGStrondholdState.RedOccupation && collider.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && Vector3.Distance(this.FirePoint.position, collider.transform.root.position) > 6f)
						{
							this.FirePoint.LookAt(collider.transform.root.position, base.transform.up);
							float num4 = Vector3.Distance(this.FirePoint.position, collider.transform.root.position);
							object[] array5 = new object[2];
							array5[0] = num4;
							if (this.mStrongHold.holdState == GGStrondholdState.BlueOccupation)
							{
								array5[1] = GGTeamType.blue;
							}
							else if (this.mStrongHold.holdState == GGStrondholdState.RedOccupation)
							{
								array5[1] = GGTeamType.red;
							}
							GGNetworkKit.mInstance.CreateSeceneObject(this.TowerGrenadePrefab, this.FirePoint.position, this.FirePoint.rotation, array5);
							break;
						}
					}
				}
			}
			this.FireTimeCount = 0f;
		}
	}

	// Token: 0x040013B4 RID: 5044
	public GGStrongHold mStrongHold;

	// Token: 0x040013B5 RID: 5045
	private bool canFire;

	// Token: 0x040013B6 RID: 5046
	public List<Transform> PlayerTransform;

	// Token: 0x040013B7 RID: 5047
	public Transform FirePoint;

	// Token: 0x040013B8 RID: 5048
	public float radius = 22f;

	// Token: 0x040013B9 RID: 5049
	private Collider[] colliders;

	// Token: 0x040013BA RID: 5050
	public string TowerGrenadePrefab = "StrongholdTowerGrenade";

	// Token: 0x040013BB RID: 5051
	public GameObject TowerFireObject;

	// Token: 0x040013BC RID: 5052
	private float FireTimeCount;

	// Token: 0x040013BD RID: 5053
	private float FireRate = 3f;

	// Token: 0x040013BE RID: 5054
	private bool isStrongholdMode;

	// Token: 0x040013BF RID: 5055
	public int TowerID;
}
