using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class UITwist : MonoBehaviour
{
	// Token: 0x06001C0B RID: 7179 RVA: 0x000DDF18 File Offset: 0x000DC318
	public void OnEnable()
	{
		EasyTouch.On_Twist += this.On_Twist;
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000DDF2B File Offset: 0x000DC32B
	public void OnDestroy()
	{
		EasyTouch.On_Twist -= this.On_Twist;
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000DDF40 File Offset: 0x000DC340
	private void On_Twist(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.Rotate(new Vector3(0f, 0f, gesture.twistAngle));
		}
	}
}
