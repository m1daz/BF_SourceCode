using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class GestureStateTracker : MonoBehaviour
{
	// Token: 0x06002086 RID: 8326 RVA: 0x000F30C2 File Offset: 0x000F14C2
	private void Awake()
	{
		if (!this.gesture)
		{
			this.gesture = base.GetComponent<GestureRecognizer>();
		}
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x000F30E0 File Offset: 0x000F14E0
	private void OnEnable()
	{
		if (this.gesture)
		{
			this.gesture.OnStateChanged += this.gesture_OnStateChanged;
		}
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x000F3109 File Offset: 0x000F1509
	private void OnDisable()
	{
		if (this.gesture)
		{
			this.gesture.OnStateChanged -= this.gesture_OnStateChanged;
		}
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x000F3134 File Offset: 0x000F1534
	private void gesture_OnStateChanged(GestureRecognizer source)
	{
		Debug.Log(string.Concat(new object[]
		{
			"Gesture ",
			source,
			" changed from ",
			source.PreviousState,
			" to ",
			source.State
		}));
	}

	// Token: 0x04002151 RID: 8529
	public GestureRecognizer gesture;
}
