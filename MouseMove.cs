using System;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class MouseMove : MonoBehaviour
{
	// Token: 0x06002092 RID: 8338 RVA: 0x000F3285 File Offset: 0x000F1685
	private void Start()
	{
		this._originalPos = base.transform.position;
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x000F3298 File Offset: 0x000F1698
	private void Update()
	{
		Vector3 vector = Input.mousePosition;
		vector.x /= (float)Screen.width;
		vector.y /= (float)Screen.height;
		vector.x -= 0.5f;
		vector.y -= 0.5f;
		vector *= 2f * this._sensitivity;
		base.transform.position = this._originalPos + vector;
	}

	// Token: 0x04002155 RID: 8533
	[SerializeField]
	private float _sensitivity = 0.5f;

	// Token: 0x04002156 RID: 8534
	private Vector3 _originalPos;
}
