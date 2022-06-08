using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class LookAtTargetFireWork : MonoBehaviour
{
	// Token: 0x06002090 RID: 8336 RVA: 0x000F3237 File Offset: 0x000F1637
	private void Update()
	{
		this._lookAtTarget = Vector3.Lerp(this._lookAtTarget, this._target.position, Time.deltaTime * this._speed);
		base.transform.LookAt(this._lookAtTarget);
	}

	// Token: 0x04002152 RID: 8530
	[SerializeField]
	private Transform _target;

	// Token: 0x04002153 RID: 8531
	[SerializeField]
	private float _speed = 0.5f;

	// Token: 0x04002154 RID: 8532
	private Vector3 _lookAtTarget;
}
