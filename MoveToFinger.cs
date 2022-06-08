using System;
using UnityEngine;

// Token: 0x02000462 RID: 1122
public class MoveToFinger : MonoBehaviour
{
	// Token: 0x0600208B RID: 8331 RVA: 0x000F3191 File Offset: 0x000F1591
	private void OnEnable()
	{
		FingerGestures.OnFingerDown += this.OnFingerDown;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x000F31A4 File Offset: 0x000F15A4
	private void OnDisable()
	{
		FingerGestures.OnFingerDown -= this.OnFingerDown;
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x000F31B7 File Offset: 0x000F15B7
	private void OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
		base.transform.position = this.GetWorldPos(fingerPos);
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x000F31CC File Offset: 0x000F15CC
	private Vector3 GetWorldPos(Vector2 screenPos)
	{
		Camera main = Camera.main;
		return main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(base.transform.position.z - main.transform.position.z)));
	}
}
