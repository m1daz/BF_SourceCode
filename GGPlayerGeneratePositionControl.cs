using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class GGPlayerGeneratePositionControl : MonoBehaviour
{
	// Token: 0x06000DA6 RID: 3494 RVA: 0x00070CF8 File Offset: 0x0006F0F8
	private void Awake()
	{
		GGPlayerGeneratePositionControl.mInstance = this;
		if (this.BlueGeneratePositions_p != null)
		{
			IEnumerator enumerator = this.BlueGeneratePositions_p.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform item = (Transform)obj;
					this.BlueGeneratePositions.Add(item);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (this.RedGeneratePositions_p != null)
		{
			IEnumerator enumerator2 = this.RedGeneratePositions_p.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Transform item2 = (Transform)obj2;
					this.RedGeneratePositions.Add(item2);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
		if (this.MutationModeGeneratePositionsInWaitingRoom_p != null)
		{
			IEnumerator enumerator3 = this.MutationModeGeneratePositionsInWaitingRoom_p.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object obj3 = enumerator3.Current;
					Transform item3 = (Transform)obj3;
					this.MutationModeGeneratePositionsInWaitingRoom.Add(item3);
				}
			}
			finally
			{
				IDisposable disposable3;
				if ((disposable3 = (enumerator3 as IDisposable)) != null)
				{
					disposable3.Dispose();
				}
			}
		}
		if (this.MutationModeGeneratePositions_p != null)
		{
			IEnumerator enumerator4 = this.MutationModeGeneratePositions_p.GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					object obj4 = enumerator4.Current;
					Transform item4 = (Transform)obj4;
					this.MutationModeGeneratePositions.Add(item4);
				}
			}
			finally
			{
				IDisposable disposable4;
				if ((disposable4 = (enumerator4 as IDisposable)) != null)
				{
					disposable4.Dispose();
				}
			}
		}
		if (this.StrongholdModeBlueGeneratePositions_Area1_p != null)
		{
			IEnumerator enumerator5 = this.StrongholdModeBlueGeneratePositions_Area1_p.GetEnumerator();
			try
			{
				while (enumerator5.MoveNext())
				{
					object obj5 = enumerator5.Current;
					Transform item5 = (Transform)obj5;
					this.StrongholdModeBlueGeneratePositions_Area1.Add(item5);
				}
			}
			finally
			{
				IDisposable disposable5;
				if ((disposable5 = (enumerator5 as IDisposable)) != null)
				{
					disposable5.Dispose();
				}
			}
		}
		if (this.StrongholdModeBlueGeneratePositions_Area2_p != null)
		{
			IEnumerator enumerator6 = this.StrongholdModeBlueGeneratePositions_Area2_p.GetEnumerator();
			try
			{
				while (enumerator6.MoveNext())
				{
					object obj6 = enumerator6.Current;
					Transform item6 = (Transform)obj6;
					this.StrongholdModeBlueGeneratePositions_Area2.Add(item6);
				}
			}
			finally
			{
				IDisposable disposable6;
				if ((disposable6 = (enumerator6 as IDisposable)) != null)
				{
					disposable6.Dispose();
				}
			}
		}
		if (this.StrongholdModeBlueGeneratePositions_Area3_p != null)
		{
			IEnumerator enumerator7 = this.StrongholdModeBlueGeneratePositions_Area3_p.GetEnumerator();
			try
			{
				while (enumerator7.MoveNext())
				{
					object obj7 = enumerator7.Current;
					Transform item7 = (Transform)obj7;
					this.StrongholdModeBlueGeneratePositions_Area3.Add(item7);
				}
			}
			finally
			{
				IDisposable disposable7;
				if ((disposable7 = (enumerator7 as IDisposable)) != null)
				{
					disposable7.Dispose();
				}
			}
		}
		if (this.StrongholdModeRedGeneratePositions_Area1_p != null)
		{
			IEnumerator enumerator8 = this.StrongholdModeRedGeneratePositions_Area1_p.GetEnumerator();
			try
			{
				while (enumerator8.MoveNext())
				{
					object obj8 = enumerator8.Current;
					Transform item8 = (Transform)obj8;
					this.StrongholdModeRedGeneratePositions_Area1.Add(item8);
				}
			}
			finally
			{
				IDisposable disposable8;
				if ((disposable8 = (enumerator8 as IDisposable)) != null)
				{
					disposable8.Dispose();
				}
			}
		}
		if (this.StrongholdModeRedGeneratePositions_Area2_p != null)
		{
			IEnumerator enumerator9 = this.StrongholdModeRedGeneratePositions_Area2_p.GetEnumerator();
			try
			{
				while (enumerator9.MoveNext())
				{
					object obj9 = enumerator9.Current;
					Transform item9 = (Transform)obj9;
					this.StrongholdModeRedGeneratePositions_Area2.Add(item9);
				}
			}
			finally
			{
				IDisposable disposable9;
				if ((disposable9 = (enumerator9 as IDisposable)) != null)
				{
					disposable9.Dispose();
				}
			}
		}
		if (this.StrongholdModeRedGeneratePositions_Area3_p != null)
		{
			IEnumerator enumerator10 = this.StrongholdModeRedGeneratePositions_Area3_p.GetEnumerator();
			try
			{
				while (enumerator10.MoveNext())
				{
					object obj10 = enumerator10.Current;
					Transform item10 = (Transform)obj10;
					this.StrongholdModeRedGeneratePositions_Area3.Add(item10);
				}
			}
			finally
			{
				IDisposable disposable10;
				if ((disposable10 = (enumerator10 as IDisposable)) != null)
				{
					disposable10.Dispose();
				}
			}
		}
		if (this.ExplosionModeTimerBombPosition_p != null)
		{
			IEnumerator enumerator11 = this.ExplosionModeTimerBombPosition_p.GetEnumerator();
			try
			{
				while (enumerator11.MoveNext())
				{
					object obj11 = enumerator11.Current;
					Transform item11 = (Transform)obj11;
					this.ExplosionModeTimerBombPosition.Add(item11);
				}
			}
			finally
			{
				IDisposable disposable11;
				if ((disposable11 = (enumerator11 as IDisposable)) != null)
				{
					disposable11.Dispose();
				}
			}
		}
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x000711EC File Offset: 0x0006F5EC
	private void Update()
	{
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x000711EE File Offset: 0x0006F5EE
	private void onDestroy()
	{
		GGPlayerGeneratePositionControl.mInstance = null;
	}

	// Token: 0x04000DD7 RID: 3543
	public static GGPlayerGeneratePositionControl mInstance;

	// Token: 0x04000DD8 RID: 3544
	public Transform BlueGeneratePositions_p;

	// Token: 0x04000DD9 RID: 3545
	public Transform RedGeneratePositions_p;

	// Token: 0x04000DDA RID: 3546
	public Transform MutationModeGeneratePositionsInWaitingRoom_p;

	// Token: 0x04000DDB RID: 3547
	public Transform MutationModeGeneratePositions_p;

	// Token: 0x04000DDC RID: 3548
	public Transform StrongholdModeBlueGeneratePositions_Area1_p;

	// Token: 0x04000DDD RID: 3549
	public Transform StrongholdModeBlueGeneratePositions_Area2_p;

	// Token: 0x04000DDE RID: 3550
	public Transform StrongholdModeBlueGeneratePositions_Area3_p;

	// Token: 0x04000DDF RID: 3551
	public Transform StrongholdModeRedGeneratePositions_Area1_p;

	// Token: 0x04000DE0 RID: 3552
	public Transform StrongholdModeRedGeneratePositions_Area2_p;

	// Token: 0x04000DE1 RID: 3553
	public Transform StrongholdModeRedGeneratePositions_Area3_p;

	// Token: 0x04000DE2 RID: 3554
	public Transform ExplosionModeTimerBombPosition_p;

	// Token: 0x04000DE3 RID: 3555
	public List<Transform> BlueGeneratePositions;

	// Token: 0x04000DE4 RID: 3556
	public List<Transform> RedGeneratePositions;

	// Token: 0x04000DE5 RID: 3557
	public List<Transform> MutationModeGeneratePositionsInWaitingRoom;

	// Token: 0x04000DE6 RID: 3558
	public List<Transform> MutationModeGeneratePositions;

	// Token: 0x04000DE7 RID: 3559
	public List<Transform> StrongholdModeBlueGeneratePositions_Area1;

	// Token: 0x04000DE8 RID: 3560
	public List<Transform> StrongholdModeBlueGeneratePositions_Area2;

	// Token: 0x04000DE9 RID: 3561
	public List<Transform> StrongholdModeBlueGeneratePositions_Area3;

	// Token: 0x04000DEA RID: 3562
	public List<Transform> StrongholdModeRedGeneratePositions_Area1;

	// Token: 0x04000DEB RID: 3563
	public List<Transform> StrongholdModeRedGeneratePositions_Area2;

	// Token: 0x04000DEC RID: 3564
	public List<Transform> StrongholdModeRedGeneratePositions_Area3;

	// Token: 0x04000DED RID: 3565
	public List<Transform> ExplosionModeTimerBombPosition;
}
