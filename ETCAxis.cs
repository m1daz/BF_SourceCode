using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
[Serializable]
public class ETCAxis
{
	// Token: 0x06001E62 RID: 7778 RVA: 0x000E86D8 File Offset: 0x000E6AD8
	public ETCAxis(string axisName)
	{
		this.name = axisName;
		this.enable = true;
		this.speed = 15f;
		this.invertedAxis = false;
		this.isEnertia = false;
		this.inertia = 0f;
		this.inertiaThreshold = 0.08f;
		this.axisValue = 0f;
		this.axisSpeedValue = 0f;
		this.gravity = 0f;
		this.isAutoStab = false;
		this.autoStabThreshold = 0.01f;
		this.autoStabSpeed = 10f;
		this.maxAngle = 90f;
		this.minAngle = 90f;
		this.axisState = ETCAxis.AxisState.None;
		this.maxOverTimeValue = 1f;
		this.overTimeStep = 1f;
		this.isValueOverTime = false;
		this.axisThreshold = 0.5f;
		this.deadValue = 0.1f;
		this.actionOn = ETCAxis.ActionOn.Press;
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06001E63 RID: 7779 RVA: 0x000E87C8 File Offset: 0x000E6BC8
	// (set) Token: 0x06001E64 RID: 7780 RVA: 0x000E87D0 File Offset: 0x000E6BD0
	public Transform directTransform
	{
		get
		{
			return this._directTransform;
		}
		set
		{
			this._directTransform = value;
			if (this._directTransform != null)
			{
				this.directCharacterController = this._directTransform.GetComponent<CharacterController>();
				this.directRigidBody = this._directTransform.GetComponent<Rigidbody>();
			}
			else
			{
				this.directCharacterController = null;
			}
		}
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x000E8824 File Offset: 0x000E6C24
	public void InitAxis()
	{
		if (this.autoLinkTagPlayer)
		{
			this.player = GameObject.FindGameObjectWithTag(this.autoTag);
			if (this.player)
			{
				this.directTransform = this.player.transform;
			}
		}
		this.startAngle = this.GetAngle();
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x000E887C File Offset: 0x000E6C7C
	public void UpdateAxis(float realValue, bool isOnDrag, ETCBase.ControlType type, bool deltaTime = true)
	{
		if ((this.autoLinkTagPlayer && this.player == null) || (this.player && !this.player.activeSelf))
		{
			this.player = GameObject.FindGameObjectWithTag(this.autoTag);
			if (this.player)
			{
				this.directTransform = this.player.transform;
			}
		}
		if (this.isAutoStab && this.axisValue == 0f && this._directTransform)
		{
			this.DoAutoStabilisation();
		}
		if (this.invertedAxis)
		{
			realValue *= -1f;
		}
		if (this.isValueOverTime && realValue != 0f)
		{
			this.axisValue += this.overTimeStep * Mathf.Sign(realValue) * Time.deltaTime;
			if (Mathf.Sign(this.axisValue) > 0f)
			{
				this.axisValue = Mathf.Clamp(this.axisValue, 0f, this.maxOverTimeValue);
			}
			else
			{
				this.axisValue = Mathf.Clamp(this.axisValue, -this.maxOverTimeValue, 0f);
			}
		}
		this.ComputAxisValue(realValue, type, isOnDrag, deltaTime);
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x000E89D0 File Offset: 0x000E6DD0
	public void UpdateButton()
	{
		if ((this.autoLinkTagPlayer && this.player == null) || (this.player && !this.player.activeSelf))
		{
			this.player = GameObject.FindGameObjectWithTag(this.autoTag);
			if (this.player)
			{
				this.directTransform = this.player.transform;
			}
		}
		if (this.isValueOverTime)
		{
			this.axisValue += this.overTimeStep * Time.deltaTime;
			this.axisValue = Mathf.Clamp(this.axisValue, 0f, this.maxOverTimeValue);
		}
		else if (this.axisState == ETCAxis.AxisState.Press || this.axisState == ETCAxis.AxisState.Down)
		{
			this.axisValue = 1f;
		}
		else
		{
			this.axisValue = 0f;
		}
		ETCAxis.ActionOn actionOn = this.actionOn;
		if (actionOn != ETCAxis.ActionOn.Down)
		{
			if (actionOn == ETCAxis.ActionOn.Press)
			{
				this.axisSpeedValue = this.axisValue * this.speed * Time.deltaTime;
				if (this.axisState == ETCAxis.AxisState.Press)
				{
					this.DoDirectAction();
				}
			}
		}
		else
		{
			this.axisSpeedValue = this.axisValue * this.speed;
			if (this.axisState == ETCAxis.AxisState.Down)
			{
				this.DoDirectAction();
			}
		}
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x000E8B38 File Offset: 0x000E6F38
	public void ResetAxis()
	{
		if (!this.isEnertia || (this.isEnertia && Mathf.Abs(this.axisValue) < this.inertiaThreshold))
		{
			this.axisValue = 0f;
			this.axisSpeedValue = 0f;
		}
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x000E8B88 File Offset: 0x000E6F88
	public void DoDirectAction()
	{
		if (this.directTransform)
		{
			Vector3 influencedAxis = this.GetInfluencedAxis();
			switch (this.directAction)
			{
			case ETCAxis.DirectAction.Rotate:
				this.directTransform.Rotate(influencedAxis * this.axisSpeedValue, Space.World);
				break;
			case ETCAxis.DirectAction.RotateLocal:
				this.directTransform.Rotate(influencedAxis * this.axisSpeedValue, Space.Self);
				break;
			case ETCAxis.DirectAction.Translate:
				if (this.directCharacterController == null)
				{
					this.directTransform.Translate(influencedAxis * this.axisSpeedValue, Space.World);
				}
				else if (this.directCharacterController.isGrounded || !this.isLockinJump)
				{
					Vector3 motion = influencedAxis * this.axisSpeedValue;
					this.directCharacterController.Move(motion);
					this.lastMove = influencedAxis * (this.axisSpeedValue / Time.deltaTime);
				}
				else
				{
					this.directCharacterController.Move(this.lastMove * Time.deltaTime);
				}
				break;
			case ETCAxis.DirectAction.TranslateLocal:
				if (this.directCharacterController == null)
				{
					this.directTransform.Translate(influencedAxis * this.axisSpeedValue, Space.Self);
				}
				else if (this.directCharacterController.isGrounded || !this.isLockinJump)
				{
					Vector3 motion2 = this.directCharacterController.transform.TransformDirection(influencedAxis) * this.axisSpeedValue;
					this.directCharacterController.Move(motion2);
					this.lastMove = this.directCharacterController.transform.TransformDirection(influencedAxis) * (this.axisSpeedValue / Time.deltaTime);
				}
				else
				{
					this.directCharacterController.Move(this.lastMove * Time.deltaTime);
				}
				break;
			case ETCAxis.DirectAction.Scale:
				this.directTransform.localScale += influencedAxis * this.axisSpeedValue;
				break;
			case ETCAxis.DirectAction.Force:
				if (this.directRigidBody != null)
				{
					this.directRigidBody.AddForce(influencedAxis * this.axisValue * this.speed);
				}
				else
				{
					Debug.LogWarning("ETCAxis : " + this.name + " No rigidbody on gameobject : " + this._directTransform.name);
				}
				break;
			case ETCAxis.DirectAction.RelativeForce:
				if (this.directRigidBody != null)
				{
					this.directRigidBody.AddRelativeForce(influencedAxis * this.axisValue * this.speed);
				}
				else
				{
					Debug.LogWarning("ETCAxis : " + this.name + " No rigidbody on gameobject : " + this._directTransform.name);
				}
				break;
			case ETCAxis.DirectAction.Torque:
				if (this.directRigidBody != null)
				{
					this.directRigidBody.AddTorque(influencedAxis * this.axisValue * this.speed);
				}
				else
				{
					Debug.LogWarning("ETCAxis : " + this.name + " No rigidbody on gameobject : " + this._directTransform.name);
				}
				break;
			case ETCAxis.DirectAction.RelativeTorque:
				if (this.directRigidBody != null)
				{
					this.directRigidBody.AddRelativeTorque(influencedAxis * this.axisValue * this.speed);
				}
				else
				{
					Debug.LogWarning("ETCAxis : " + this.name + " No rigidbody on gameobject : " + this._directTransform.name);
				}
				break;
			case ETCAxis.DirectAction.Jump:
				if (this.directCharacterController != null && !this.isJump)
				{
					this.isJump = true;
					this.currentGravity = this.speed;
				}
				break;
			}
			if (this.isClampRotation && this.directAction == ETCAxis.DirectAction.RotateLocal)
			{
				this.DoAngleLimitation();
			}
		}
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x000E8F84 File Offset: 0x000E7384
	public void DoGravity()
	{
		if (this.directCharacterController != null && this.gravity != 0f)
		{
			if (!this.isJump)
			{
				Vector3 a = new Vector3(0f, -this.gravity, 0f);
				this.directCharacterController.Move(a * Time.deltaTime);
			}
			else
			{
				this.currentGravity -= this.gravity * Time.deltaTime;
				Vector3 a2 = new Vector3(0f, this.currentGravity, 0f);
				this.directCharacterController.Move(a2 * Time.deltaTime);
			}
			if (this.directCharacterController.isGrounded)
			{
				this.isJump = false;
				this.currentGravity = 0f;
			}
		}
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x000E905C File Offset: 0x000E745C
	private void ComputAxisValue(float realValue, ETCBase.ControlType type, bool isOnDrag, bool deltaTime)
	{
		if (this.enable)
		{
			if (type == ETCBase.ControlType.Joystick)
			{
				if (this.valueMethod == ETCAxis.AxisValueMethod.Classical)
				{
					float num = Mathf.Max(Mathf.Abs(realValue), 0.001f);
					float num2 = Mathf.Max(num - this.deadValue, 0f) / (1f - this.deadValue) / num;
					realValue *= num2;
				}
				else
				{
					realValue = this.curveValue.Evaluate(realValue);
				}
			}
			if (this.isEnertia)
			{
				realValue -= this.axisValue;
				realValue /= this.inertia;
				this.axisValue += realValue;
				if (Mathf.Abs(this.axisValue) < this.inertiaThreshold && !isOnDrag)
				{
					this.axisValue = 0f;
				}
			}
			else if (!this.isValueOverTime || (this.isValueOverTime && realValue == 0f))
			{
				this.axisValue = realValue;
			}
			if (deltaTime)
			{
				this.axisSpeedValue = this.axisValue * this.speed * Time.deltaTime;
			}
			else
			{
				this.axisSpeedValue = this.axisValue * this.speed;
			}
		}
		else
		{
			this.axisValue = 0f;
			this.axisSpeedValue = 0f;
		}
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x000E91A4 File Offset: 0x000E75A4
	private Vector3 GetInfluencedAxis()
	{
		Vector3 result = Vector3.zero;
		ETCAxis.AxisInfluenced axisInfluenced = this.axisInfluenced;
		if (axisInfluenced != ETCAxis.AxisInfluenced.X)
		{
			if (axisInfluenced != ETCAxis.AxisInfluenced.Y)
			{
				if (axisInfluenced == ETCAxis.AxisInfluenced.Z)
				{
					result = Vector3.forward;
				}
			}
			else
			{
				result = Vector3.up;
			}
		}
		else
		{
			result = Vector3.right;
		}
		return result;
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x000E91FC File Offset: 0x000E75FC
	private float GetAngle()
	{
		float num = 0f;
		if (this._directTransform != null)
		{
			ETCAxis.AxisInfluenced axisInfluenced = this.axisInfluenced;
			if (axisInfluenced != ETCAxis.AxisInfluenced.X)
			{
				if (axisInfluenced != ETCAxis.AxisInfluenced.Y)
				{
					if (axisInfluenced == ETCAxis.AxisInfluenced.Z)
					{
						num = this._directTransform.localRotation.eulerAngles.z;
					}
				}
				else
				{
					num = this._directTransform.localRotation.eulerAngles.y;
				}
			}
			else
			{
				num = this._directTransform.localRotation.eulerAngles.x;
			}
			if (num <= 360f && num >= 180f)
			{
				num -= 360f;
			}
		}
		return num;
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x000E92C8 File Offset: 0x000E76C8
	private void DoAutoStabilisation()
	{
		float num = this.GetAngle();
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		if (num > this.startAngle - this.autoStabThreshold || num < this.startAngle + this.autoStabThreshold)
		{
			float num2 = 0f;
			Vector3 zero = Vector3.zero;
			if (num > this.startAngle - this.autoStabThreshold)
			{
				num2 = num + this.autoStabSpeed / 100f * Mathf.Abs(num - this.startAngle) * Time.deltaTime * -1f;
			}
			if (num < this.startAngle + this.autoStabThreshold)
			{
				num2 = num + this.autoStabSpeed / 100f * Mathf.Abs(num - this.startAngle) * Time.deltaTime;
			}
			ETCAxis.AxisInfluenced axisInfluenced = this.axisInfluenced;
			if (axisInfluenced != ETCAxis.AxisInfluenced.X)
			{
				if (axisInfluenced != ETCAxis.AxisInfluenced.Y)
				{
					if (axisInfluenced == ETCAxis.AxisInfluenced.Z)
					{
						zero = new Vector3(this._directTransform.localRotation.eulerAngles.x, this._directTransform.localRotation.eulerAngles.y, num2);
					}
				}
				else
				{
					zero = new Vector3(this._directTransform.localRotation.eulerAngles.x, num2, this._directTransform.localRotation.eulerAngles.z);
				}
			}
			else
			{
				zero = new Vector3(num2, this._directTransform.localRotation.eulerAngles.y, this._directTransform.localRotation.eulerAngles.z);
			}
			this._directTransform.localRotation = Quaternion.Euler(zero);
		}
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x000E94A4 File Offset: 0x000E78A4
	private void DoAngleLimitation()
	{
		Quaternion localRotation = this._directTransform.localRotation;
		localRotation.x /= localRotation.w;
		localRotation.y /= localRotation.w;
		localRotation.z /= localRotation.w;
		localRotation.w = 1f;
		ETCAxis.AxisInfluenced axisInfluenced = this.axisInfluenced;
		if (axisInfluenced != ETCAxis.AxisInfluenced.X)
		{
			if (axisInfluenced != ETCAxis.AxisInfluenced.Y)
			{
				if (axisInfluenced == ETCAxis.AxisInfluenced.Z)
				{
					float num = 114.59156f * Mathf.Atan(localRotation.z);
					num = Mathf.Clamp(num, -this.minAngle, this.maxAngle);
					localRotation.z = Mathf.Tan(0.008726646f * num);
				}
			}
			else
			{
				float num = 114.59156f * Mathf.Atan(localRotation.y);
				num = Mathf.Clamp(num, -this.minAngle, this.maxAngle);
				localRotation.y = Mathf.Tan(0.008726646f * num);
			}
		}
		else
		{
			float num = 114.59156f * Mathf.Atan(localRotation.x);
			num = Mathf.Clamp(num, -this.minAngle, this.maxAngle);
			localRotation.x = Mathf.Tan(0.008726646f * num);
		}
		this._directTransform.localRotation = localRotation;
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x000E95F7 File Offset: 0x000E79F7
	public void InitDeadCurve()
	{
		this.curveValue = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		this.curveValue.postWrapMode = WrapMode.PingPong;
		this.curveValue.preWrapMode = WrapMode.PingPong;
	}

	// Token: 0x04001F6B RID: 8043
	public string name;

	// Token: 0x04001F6C RID: 8044
	public bool autoLinkTagPlayer;

	// Token: 0x04001F6D RID: 8045
	public string autoTag = "Player";

	// Token: 0x04001F6E RID: 8046
	public GameObject player;

	// Token: 0x04001F6F RID: 8047
	public bool enable;

	// Token: 0x04001F70 RID: 8048
	public bool invertedAxis;

	// Token: 0x04001F71 RID: 8049
	public float speed;

	// Token: 0x04001F72 RID: 8050
	public float deadValue;

	// Token: 0x04001F73 RID: 8051
	public ETCAxis.AxisValueMethod valueMethod;

	// Token: 0x04001F74 RID: 8052
	public AnimationCurve curveValue;

	// Token: 0x04001F75 RID: 8053
	public bool isEnertia;

	// Token: 0x04001F76 RID: 8054
	public float inertia;

	// Token: 0x04001F77 RID: 8055
	public float inertiaThreshold;

	// Token: 0x04001F78 RID: 8056
	public bool isAutoStab;

	// Token: 0x04001F79 RID: 8057
	public float autoStabThreshold;

	// Token: 0x04001F7A RID: 8058
	public float autoStabSpeed;

	// Token: 0x04001F7B RID: 8059
	private float startAngle;

	// Token: 0x04001F7C RID: 8060
	public bool isClampRotation;

	// Token: 0x04001F7D RID: 8061
	public float maxAngle;

	// Token: 0x04001F7E RID: 8062
	public float minAngle;

	// Token: 0x04001F7F RID: 8063
	public bool isValueOverTime;

	// Token: 0x04001F80 RID: 8064
	public float overTimeStep;

	// Token: 0x04001F81 RID: 8065
	public float maxOverTimeValue;

	// Token: 0x04001F82 RID: 8066
	public float axisValue;

	// Token: 0x04001F83 RID: 8067
	public float axisSpeedValue;

	// Token: 0x04001F84 RID: 8068
	public float axisThreshold;

	// Token: 0x04001F85 RID: 8069
	public bool isLockinJump;

	// Token: 0x04001F86 RID: 8070
	private Vector3 lastMove;

	// Token: 0x04001F87 RID: 8071
	public ETCAxis.AxisState axisState;

	// Token: 0x04001F88 RID: 8072
	[SerializeField]
	private Transform _directTransform;

	// Token: 0x04001F89 RID: 8073
	public ETCAxis.DirectAction directAction;

	// Token: 0x04001F8A RID: 8074
	public ETCAxis.AxisInfluenced axisInfluenced;

	// Token: 0x04001F8B RID: 8075
	public ETCAxis.ActionOn actionOn;

	// Token: 0x04001F8C RID: 8076
	public CharacterController directCharacterController;

	// Token: 0x04001F8D RID: 8077
	public Rigidbody directRigidBody;

	// Token: 0x04001F8E RID: 8078
	public float gravity;

	// Token: 0x04001F8F RID: 8079
	public float currentGravity;

	// Token: 0x04001F90 RID: 8080
	public bool isJump;

	// Token: 0x04001F91 RID: 8081
	public string unityAxis;

	// Token: 0x04001F92 RID: 8082
	public bool showGeneralInspector;

	// Token: 0x04001F93 RID: 8083
	public bool showDirectInspector;

	// Token: 0x04001F94 RID: 8084
	public bool showInertiaInspector;

	// Token: 0x04001F95 RID: 8085
	public bool showSimulatinInspector;

	// Token: 0x020003F8 RID: 1016
	public enum DirectAction
	{
		// Token: 0x04001F97 RID: 8087
		Rotate,
		// Token: 0x04001F98 RID: 8088
		RotateLocal,
		// Token: 0x04001F99 RID: 8089
		Translate,
		// Token: 0x04001F9A RID: 8090
		TranslateLocal,
		// Token: 0x04001F9B RID: 8091
		Scale,
		// Token: 0x04001F9C RID: 8092
		Force,
		// Token: 0x04001F9D RID: 8093
		RelativeForce,
		// Token: 0x04001F9E RID: 8094
		Torque,
		// Token: 0x04001F9F RID: 8095
		RelativeTorque,
		// Token: 0x04001FA0 RID: 8096
		Jump
	}

	// Token: 0x020003F9 RID: 1017
	public enum AxisInfluenced
	{
		// Token: 0x04001FA2 RID: 8098
		X,
		// Token: 0x04001FA3 RID: 8099
		Y,
		// Token: 0x04001FA4 RID: 8100
		Z
	}

	// Token: 0x020003FA RID: 1018
	public enum AxisValueMethod
	{
		// Token: 0x04001FA6 RID: 8102
		Classical,
		// Token: 0x04001FA7 RID: 8103
		Curve
	}

	// Token: 0x020003FB RID: 1019
	public enum AxisState
	{
		// Token: 0x04001FA9 RID: 8105
		None,
		// Token: 0x04001FAA RID: 8106
		Down,
		// Token: 0x04001FAB RID: 8107
		Press,
		// Token: 0x04001FAC RID: 8108
		Up,
		// Token: 0x04001FAD RID: 8109
		DownUp,
		// Token: 0x04001FAE RID: 8110
		DownDown,
		// Token: 0x04001FAF RID: 8111
		DownLeft,
		// Token: 0x04001FB0 RID: 8112
		DownRight,
		// Token: 0x04001FB1 RID: 8113
		PressUp,
		// Token: 0x04001FB2 RID: 8114
		PressDown,
		// Token: 0x04001FB3 RID: 8115
		PressLeft,
		// Token: 0x04001FB4 RID: 8116
		PressRight
	}

	// Token: 0x020003FC RID: 1020
	public enum ActionOn
	{
		// Token: 0x04001FB6 RID: 8118
		Down,
		// Token: 0x04001FB7 RID: 8119
		Press
	}
}
