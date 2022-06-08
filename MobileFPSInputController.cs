using System;
using UnityEngine;

// Token: 0x020001F6 RID: 502
[RequireComponent(typeof(CharacterMotorCS))]
public class MobileFPSInputController : MonoBehaviour
{
	// Token: 0x06000DC5 RID: 3525 RVA: 0x00072674 File Offset: 0x00070A74
	private void Awake()
	{
		this.motor = base.GetComponent<CharacterMotorCS>();
		this.mNetworkPlayerLogic = base.GetComponent<GGNetWorkPlayerlogic>();
		if (Application.loadedLevelName == "UIHelp")
		{
			this.joystick = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/BasicSystem/Joystick") as GameObject).GetComponentInChildren<ETCJoystick>();
		}
		else
		{
			PhotonView component = base.GetComponent<PhotonView>();
			if (component.isMine)
			{
				this.joystick = UnityEngine.Object.Instantiate<GameObject>(Resources.Load("Prefabs/BasicSystem/Joystick") as GameObject).GetComponentInChildren<ETCJoystick>();
			}
		}
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00072702 File Offset: 0x00070B02
	private void Start()
	{
		if (Application.loadedLevelName == "UIHelp")
		{
			this.isUIHelp = true;
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00072720 File Offset: 0x00070B20
	private void Update()
	{
		Vector3 vector = new Vector3(this.joystick.axisX.axisValue, 0f, this.joystick.axisY.axisValue);
		if (MobileFPSInputController.isPause)
		{
			vector = Vector3.zero;
			return;
		}
		if (vector != Vector3.zero)
		{
			if (!this.isUIHelp)
			{
				if (this.mNetworkPlayerLogic.showInstallSchedule)
				{
					this.mNetworkPlayerLogic.OnInstallBtnReleased();
					this.mNetworkPlayerLogic.HideInstallBombButton();
				}
				else if (this.mNetworkPlayerLogic.showRemoveSchedule)
				{
					this.mNetworkPlayerLogic.OnUninstallBtnReleased();
					this.mNetworkPlayerLogic.HideUninstallBombButton();
				}
			}
			float num = vector.magnitude;
			vector /= num;
			num = Mathf.Min(1f, num);
			num *= num;
			vector *= num;
		}
		this.motor.inputMoveDirection = base.transform.rotation * vector;
	}

	// Token: 0x04000E3A RID: 3642
	private CharacterMotorCS motor;

	// Token: 0x04000E3B RID: 3643
	private GGNetWorkPlayerlogic mNetworkPlayerLogic;

	// Token: 0x04000E3C RID: 3644
	private bool isUIHelp;

	// Token: 0x04000E3D RID: 3645
	public static bool isPause;

	// Token: 0x04000E3E RID: 3646
	private ETCJoystick joystick;
}
