using System;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public class DPadParameterUI : MonoBehaviour
{
	// Token: 0x06001E28 RID: 7720 RVA: 0x000E7887 File Offset: 0x000E5C87
	public void SetClassicalInertia(bool value)
	{
		ETCInput.SetAxisInertia("Horizontal", value);
		ETCInput.SetAxisInertia("Vertical", value);
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x000E789F File Offset: 0x000E5C9F
	public void SetTimePushInertia(bool value)
	{
		ETCInput.SetAxisInertia("HorizontalTP", value);
		ETCInput.SetAxisInertia("VerticalTP", value);
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x000E78B7 File Offset: 0x000E5CB7
	public void SetClassicalTwoAxesCount()
	{
		ETCInput.SetDPadAxesCount("DPadClassical", ETCBase.DPadAxis.Two_Axis);
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x000E78C4 File Offset: 0x000E5CC4
	public void SetClassicalFourAxesCount()
	{
		ETCInput.SetDPadAxesCount("DPadClassical", ETCBase.DPadAxis.Four_Axis);
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000E78D1 File Offset: 0x000E5CD1
	public void SetTimePushTwoAxesCount()
	{
		ETCInput.SetDPadAxesCount("DPadTimePush", ETCBase.DPadAxis.Two_Axis);
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000E78DE File Offset: 0x000E5CDE
	public void SetTimePushFourAxesCount()
	{
		ETCInput.SetDPadAxesCount("DPadTimePush", ETCBase.DPadAxis.Four_Axis);
	}
}
