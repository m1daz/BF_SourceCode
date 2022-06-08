using System;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class AxisXUi : MonoBehaviour
{
	// Token: 0x06001E3B RID: 7739 RVA: 0x000E7DE5 File Offset: 0x000E61E5
	public void ActivateAxisX(bool value)
	{
		ETCInput.SetAxisEnabled("Horizontal", value);
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x000E7DF2 File Offset: 0x000E61F2
	public void InvertedAxisX(bool value)
	{
		ETCInput.SetAxisInverted("Horizontal", value);
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x000E7DFF File Offset: 0x000E61FF
	public void DeadAxisX(float value)
	{
		ETCInput.SetAxisDeadValue("Horizontal", value);
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x000E7E0C File Offset: 0x000E620C
	public void SpeedAxisX(float value)
	{
		ETCInput.SetAxisSensitivity("Horizontal", value);
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x000E7E19 File Offset: 0x000E6219
	public void IsInertiaX(bool value)
	{
		ETCInput.SetAxisInertia("Horizontal", value);
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x000E7E26 File Offset: 0x000E6226
	public void InertiaSpeedX(float value)
	{
		ETCInput.SetAxisInertiaSpeed("Horizontal", value);
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x000E7E33 File Offset: 0x000E6233
	public void ActivateAxisY(bool value)
	{
		ETCInput.SetAxisEnabled("Vertical", value);
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x000E7E40 File Offset: 0x000E6240
	public void InvertedAxisY(bool value)
	{
		ETCInput.SetAxisInverted("Vertical", value);
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x000E7E4D File Offset: 0x000E624D
	public void DeadAxisY(float value)
	{
		ETCInput.SetAxisDeadValue("Vertical", value);
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x000E7E5A File Offset: 0x000E625A
	public void SpeedAxisY(float value)
	{
		ETCInput.SetAxisSensitivity("Vertical", value);
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x000E7E67 File Offset: 0x000E6267
	public void IsInertiaY(bool value)
	{
		ETCInput.SetAxisInertia("Vertical", value);
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x000E7E74 File Offset: 0x000E6274
	public void InertiaSpeedY(float value)
	{
		ETCInput.SetAxisInertiaSpeed("Vertical", value);
	}
}
