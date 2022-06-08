using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020006C3 RID: 1731
public class VCTouchController : MonoBehaviour
{
	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x060032EA RID: 13034 RVA: 0x001663D7 File Offset: 0x001647D7
	// (set) Token: 0x060032EB RID: 13035 RVA: 0x001663DE File Offset: 0x001647DE
	public static VCTouchController Instance { get; private set; }

	// Token: 0x060032EC RID: 13036 RVA: 0x001663E6 File Offset: 0x001647E6
	public static void ResetInstance()
	{
		VCTouchController.Instance = null;
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x001663F0 File Offset: 0x001647F0
	private void Awake()
	{
		base.useGUILayout = false;
		if (VCTouchController.Instance != null)
		{
			VCUtils.DestroyWithError(base.gameObject, "Only one VCTouchController can be in a scene!  Destroying the gameObject with this component.");
			return;
		}
		VCTouchController.Instance = this;
		this.touches = new List<VCTouchWrapper>();
		for (int i = 0; i < 5; i++)
		{
			this.touches.Add(new VCTouchWrapper());
		}
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x00166458 File Offset: 0x00164858
	private void Update()
	{
		Input.multiTouchEnabled = this.multitouch;
		Touch[] array = Input.touches;
		for (int i = 0; i < array.Length; i++)
		{
			Touch t = array[i];
			VCTouchWrapper vctouchWrapper = this.touches.FirstOrDefault((VCTouchWrapper x) => x.fingerId == t.fingerId);
			if (vctouchWrapper == null)
			{
				VCTouchWrapper vctouchWrapper2 = this.touches.FirstOrDefault((VCTouchWrapper x) => x.fingerId == -1);
				if (vctouchWrapper2 == null)
				{
					Debug.LogWarning("Cannot find and available TouchWrapper to assign Touch info from!  Consider increase kMaxTouches.");
				}
				else
				{
					vctouchWrapper2.Set(t);
				}
			}
			else
			{
				vctouchWrapper.visited = true;
				vctouchWrapper.deltaPosition = t.position - vctouchWrapper.position;
				vctouchWrapper.position = t.position;
				vctouchWrapper.phase = t.phase;
			}
		}
		foreach (VCTouchWrapper vctouchWrapper3 in this.touches)
		{
			if (!vctouchWrapper3.visited)
			{
				vctouchWrapper3.Reset();
			}
			else
			{
				vctouchWrapper3.visited = false;
			}
		}
		this._activeTouchesCache = null;
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x060032EF RID: 13039 RVA: 0x001665C8 File Offset: 0x001649C8
	public List<VCTouchWrapper> ActiveTouches
	{
		get
		{
			if (this._activeTouchesCache == null)
			{
				this._activeTouchesCache = (from x in this.touches
				where x.Active
				select x).ToList<VCTouchWrapper>();
			}
			return this._activeTouchesCache;
		}
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x0016661C File Offset: 0x00164A1C
	public VCTouchWrapper GetTouch(int fingerId)
	{
		return this.touches.FirstOrDefault((VCTouchWrapper x) => x.fingerId == fingerId);
	}

	// Token: 0x04002F49 RID: 12105
	public bool multitouch = true;

	// Token: 0x04002F4A RID: 12106
	public bool ignoreMultitouchErrorMovement;

	// Token: 0x04002F4B RID: 12107
	public float multiTouchErrorSqrMagnitudeMax = 1000f;

	// Token: 0x04002F4C RID: 12108
	[HideInInspector]
	public List<VCTouchWrapper> touches;

	// Token: 0x04002F4D RID: 12109
	private List<VCTouchWrapper> _activeTouchesCache;

	// Token: 0x04002F4E RID: 12110
	private const int kMaxTouches = 5;
}
