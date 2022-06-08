using System;
using UnityEngine;

// Token: 0x020006C5 RID: 1733
public class VCUtils
{
	// Token: 0x060032FA RID: 13050 RVA: 0x001667CB File Offset: 0x00164BCB
	public static bool Approximately(float a, float b)
	{
		return Mathf.Approximately(a, b);
	}

	// Token: 0x060032FB RID: 13051 RVA: 0x001667D4 File Offset: 0x00164BD4
	public static float GetSign(float val)
	{
		if (val < 0f)
		{
			return -1f;
		}
		return 1f;
	}

	// Token: 0x060032FC RID: 13052 RVA: 0x001667EC File Offset: 0x00164BEC
	public static Camera GetCamera(GameObject go)
	{
		foreach (Camera camera in UnityEngine.Object.FindObjectsOfType(typeof(Camera)))
		{
			if ((camera.cullingMask & 1 << go.layer) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	// Token: 0x060032FD RID: 13053 RVA: 0x00166840 File Offset: 0x00164C40
	public static void ScaleRect(ref Rect r, Vector2 scale)
	{
		VCUtils.ScaleRect(ref r, scale.x, scale.y);
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x00166858 File Offset: 0x00164C58
	public static void ScaleRect(ref Rect r, float scaleX, float scaleY)
	{
		Vector2 center = r.center;
		r.width *= scaleX;
		r.height *= scaleY;
		r.center = center;
	}

	// Token: 0x060032FF RID: 13055 RVA: 0x0016688F File Offset: 0x00164C8F
	public static void DestroyWithError(GameObject go, string message)
	{
		Debug.LogError(message);
		if (VCTouchController.Instance != null && VCTouchController.Instance.gameObject == go)
		{
			VCTouchController.ResetInstance();
		}
		UnityEngine.Object.Destroy(go);
	}

	// Token: 0x06003300 RID: 13056 RVA: 0x001668C7 File Offset: 0x00164CC7
	public static void AddTouchController(GameObject go)
	{
		go.AddComponent<VCTouchController>();
	}
}
