using System;
using UnityEngine;

// Token: 0x020006BF RID: 1727
public class VCCollideableObject : MonoBehaviour
{
	// Token: 0x060032E1 RID: 13025 RVA: 0x00163D3D File Offset: 0x0016213D
	protected void InitCollider(GameObject colliderGo)
	{
		this._collider = colliderGo.GetComponent<Collider>();
		this._colliderCamera = VCUtils.GetCamera(colliderGo);
	}

	// Token: 0x060032E2 RID: 13026 RVA: 0x00163D58 File Offset: 0x00162158
	public bool AABBContains(Vector2 pos)
	{
		if (this._collider == null)
		{
			return false;
		}
		this._tempVec = this._colliderCamera.WorldToScreenPoint(this._collider.bounds.min);
		if (pos.x < this._tempVec.x || pos.y < this._tempVec.y)
		{
			return false;
		}
		this._tempVec = this._colliderCamera.WorldToScreenPoint(this._collider.bounds.max);
		return pos.x <= this._tempVec.x && pos.y <= this._tempVec.y;
	}

	// Token: 0x04002F43 RID: 12099
	protected Camera _colliderCamera;

	// Token: 0x04002F44 RID: 12100
	protected Collider _collider;

	// Token: 0x04002F45 RID: 12101
	private Vector3 _tempVec;
}
