using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003E1 RID: 993
	public class EasyTouchInput
	{
		// Token: 0x06001DED RID: 7661 RVA: 0x000E653F File Offset: 0x000E493F
		public int TouchCount()
		{
			return this.getTouchCount(true);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000E6548 File Offset: 0x000E4948
		private int getTouchCount(bool realTouch)
		{
			int num = 0;
			if (realTouch || EasyTouch.instance.enableRemote)
			{
				num = Input.touchCount;
			}
			else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
			{
				num = 1;
				if (EasyTouch.GetSecondeFingerSimulation())
				{
					if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(EasyTouch.instance.twistKey) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(EasyTouch.instance.swipeKey))
					{
						num = 2;
					}
					if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(EasyTouch.instance.swipeKey))
					{
						num = 2;
					}
				}
				if (num == 0)
				{
					this.complexCenter = Vector2.zero;
					this.oldMousePosition[0] = new Vector2(-1f, -1f);
					this.oldMousePosition[1] = new Vector2(-1f, -1f);
				}
			}
			return num;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000E667C File Offset: 0x000E4A7C
		public Finger GetMouseTouch(int fingerIndex, Finger myFinger)
		{
			Finger finger;
			if (myFinger != null)
			{
				finger = myFinger;
			}
			else
			{
				finger = new Finger();
				finger.gesture = EasyTouch.GestureType.None;
			}
			if (fingerIndex == 1 && (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(EasyTouch.instance.swipeKey)))
			{
				finger.fingerIndex = fingerIndex;
				finger.position = this.oldFinger2Position;
				finger.deltaPosition = finger.position - this.oldFinger2Position;
				finger.tapCount = this.tapCount[fingerIndex];
				finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
				finger.phase = TouchPhase.Ended;
				return finger;
			}
			if (Input.GetMouseButton(0))
			{
				finger.fingerIndex = fingerIndex;
				finger.position = this.GetPointerPosition(fingerIndex);
				if ((double)(Time.realtimeSinceStartup - this.tapeTime[fingerIndex]) > 0.5)
				{
					this.tapCount[fingerIndex] = 0;
				}
				if (Input.GetMouseButtonDown(0) || (fingerIndex == 1 && (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(EasyTouch.instance.twistKey) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(EasyTouch.instance.swipeKey))))
				{
					finger.position = this.GetPointerPosition(fingerIndex);
					finger.deltaPosition = Vector2.zero;
					this.tapCount[fingerIndex] = this.tapCount[fingerIndex] + 1;
					finger.tapCount = this.tapCount[fingerIndex];
					this.startActionTime[fingerIndex] = Time.realtimeSinceStartup;
					this.deltaTime[fingerIndex] = this.startActionTime[fingerIndex];
					finger.deltaTime = 0f;
					finger.phase = TouchPhase.Began;
					if (fingerIndex == 1)
					{
						this.oldFinger2Position = finger.position;
						this.oldMousePosition[fingerIndex] = finger.position;
					}
					else
					{
						this.oldMousePosition[fingerIndex] = finger.position;
					}
					if (this.tapCount[fingerIndex] == 1)
					{
						this.tapeTime[fingerIndex] = Time.realtimeSinceStartup;
					}
					return finger;
				}
				finger.deltaPosition = finger.position - this.oldMousePosition[fingerIndex];
				finger.tapCount = this.tapCount[fingerIndex];
				finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
				if (finger.deltaPosition.sqrMagnitude < 1f)
				{
					finger.phase = TouchPhase.Stationary;
				}
				else
				{
					finger.phase = TouchPhase.Moved;
				}
				this.oldMousePosition[fingerIndex] = finger.position;
				this.deltaTime[fingerIndex] = Time.realtimeSinceStartup;
				return finger;
			}
			else
			{
				if (Input.GetMouseButtonUp(0))
				{
					finger.fingerIndex = fingerIndex;
					finger.position = this.GetPointerPosition(fingerIndex);
					finger.deltaPosition = finger.position - this.oldMousePosition[fingerIndex];
					finger.tapCount = this.tapCount[fingerIndex];
					finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
					finger.phase = TouchPhase.Ended;
					this.oldMousePosition[fingerIndex] = finger.position;
					return finger;
				}
				return null;
			}
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000E69C4 File Offset: 0x000E4DC4
		public Vector2 GetSecondFingerPosition()
		{
			Vector2 result = new Vector2(-1f, -1f);
			if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(EasyTouch.instance.twistKey)) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(EasyTouch.instance.swipeKey)))
			{
				if (!this.bComplex)
				{
					this.bComplex = true;
					this.deltaFingerPosition = Input.mousePosition - this.oldFinger2Position;
				}
				result = this.GetComplex2finger();
				return result;
			}
			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(EasyTouch.instance.twistKey))
			{
				result = this.GetPinchTwist2Finger(false);
				this.bComplex = false;
				return result;
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(EasyTouch.instance.swipeKey))
			{
				result = this.GetComplex2finger();
				this.bComplex = false;
				return result;
			}
			return result;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000E6AC8 File Offset: 0x000E4EC8
		private Vector2 GetPointerPosition(int index)
		{
			if (index == 0)
			{
				return Input.mousePosition;
			}
			return this.GetSecondFingerPosition();
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000E6AF0 File Offset: 0x000E4EF0
		private Vector2 GetPinchTwist2Finger(bool newSim = false)
		{
			Vector2 result;
			if (this.complexCenter == Vector2.zero)
			{
				result.x = (float)Screen.width / 2f - (Input.mousePosition.x - (float)Screen.width / 2f);
				result.y = (float)Screen.height / 2f - (Input.mousePosition.y - (float)Screen.height / 2f);
			}
			else
			{
				result.x = this.complexCenter.x - (Input.mousePosition.x - this.complexCenter.x);
				result.y = this.complexCenter.y - (Input.mousePosition.y - this.complexCenter.y);
			}
			this.oldFinger2Position = result;
			return result;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000E6BD4 File Offset: 0x000E4FD4
		private Vector2 GetComplex2finger()
		{
			Vector2 result;
			result.x = Input.mousePosition.x - this.deltaFingerPosition.x;
			result.y = Input.mousePosition.y - this.deltaFingerPosition.y;
			this.complexCenter = new Vector2((Input.mousePosition.x + result.x) / 2f, (Input.mousePosition.y + result.y) / 2f);
			this.oldFinger2Position = result;
			return result;
		}

		// Token: 0x04001EF5 RID: 7925
		private Vector2[] oldMousePosition = new Vector2[2];

		// Token: 0x04001EF6 RID: 7926
		private int[] tapCount = new int[2];

		// Token: 0x04001EF7 RID: 7927
		private float[] startActionTime = new float[2];

		// Token: 0x04001EF8 RID: 7928
		private float[] deltaTime = new float[2];

		// Token: 0x04001EF9 RID: 7929
		private float[] tapeTime = new float[2];

		// Token: 0x04001EFA RID: 7930
		private bool bComplex;

		// Token: 0x04001EFB RID: 7931
		private Vector2 deltaFingerPosition;

		// Token: 0x04001EFC RID: 7932
		private Vector2 oldFinger2Position;

		// Token: 0x04001EFD RID: 7933
		private Vector2 complexCenter;
	}
}
