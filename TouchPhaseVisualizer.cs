using System;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class TouchPhaseVisualizer : MonoBehaviour
{
	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000F1513 File Offset: 0x000EF913
	// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x000F151C File Offset: 0x000EF91C
	public TouchPhase Phase
	{
		get
		{
			return this.phase;
		}
		set
		{
			if (this.phase != value)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Phase transition: ",
					this.phase,
					" -> ",
					value
				}));
				this.phase = value;
			}
		}
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x000F1573 File Offset: 0x000EF973
	private void Update()
	{
		this.touchDown = (Input.touchCount > 0);
		if (this.touchDown)
		{
			this.Phase = Input.touches[0].phase;
		}
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x000F15A4 File Offset: 0x000EF9A4
	private void OnGUI()
	{
		if (this.touchDown)
		{
			GUI.Label(this.rectLabel, this.Phase.ToString());
		}
		else
		{
			GUI.Label(this.rectLabel, "N/A");
		}
	}

	// Token: 0x040020ED RID: 8429
	public Rect rectLabel = new Rect(50f, 50f, 200f, 200f);

	// Token: 0x040020EE RID: 8430
	private bool touchDown;

	// Token: 0x040020EF RID: 8431
	private TouchPhase phase = TouchPhase.Canceled;
}
