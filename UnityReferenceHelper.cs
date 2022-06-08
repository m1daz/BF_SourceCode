using System;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x020000C6 RID: 198
[ExecuteInEditMode]
public class UnityReferenceHelper : MonoBehaviour
{
	// Token: 0x060005EE RID: 1518 RVA: 0x00036FA8 File Offset: 0x000353A8
	public string GetGUID()
	{
		return this.guid;
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00036FB0 File Offset: 0x000353B0
	public void Awake()
	{
		this.Reset();
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00036FB8 File Offset: 0x000353B8
	public void Reset()
	{
		if (this.guid == null || this.guid == string.Empty)
		{
			this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
			Debug.Log("Created new GUID - " + this.guid);
		}
		else
		{
			foreach (UnityReferenceHelper unityReferenceHelper in UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[])
			{
				if (unityReferenceHelper != this && this.guid == unityReferenceHelper.guid)
				{
					this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
					Debug.Log("Created new GUID - " + this.guid);
					return;
				}
			}
		}
	}

	// Token: 0x040004C9 RID: 1225
	[HideInInspector]
	[SerializeField]
	private string guid;
}
