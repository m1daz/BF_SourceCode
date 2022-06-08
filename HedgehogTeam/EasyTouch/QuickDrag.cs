using System;
using UnityEngine;
using UnityEngine.Events;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x02000392 RID: 914
	[AddComponentMenu("EasyTouch/Quick Drag")]
	public class QuickDrag : QuickBase
	{
		// Token: 0x06001C57 RID: 7255 RVA: 0x000DF4A8 File Offset: 0x000DD8A8
		public QuickDrag()
		{
			this.quickActionName = "QuickDrag" + Guid.NewGuid().ToString().Substring(0, 7);
			this.axesAction = QuickBase.AffectedAxesAction.XY;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000DF4EC File Offset: 0x000DD8EC
		public override void OnEnable()
		{
			base.OnEnable();
			EasyTouch.On_TouchStart += this.On_TouchStart;
			EasyTouch.On_TouchDown += this.On_TouchDown;
			EasyTouch.On_TouchUp += this.On_TouchUp;
			EasyTouch.On_Drag += this.On_Drag;
			EasyTouch.On_DragStart += this.On_DragStart;
			EasyTouch.On_DragEnd += this.On_DragEnd;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000DF565 File Offset: 0x000DD965
		public override void OnDisable()
		{
			base.OnDisable();
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000DF573 File Offset: 0x000DD973
		private void OnDestroy()
		{
			this.UnsubscribeEvent();
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000DF57C File Offset: 0x000DD97C
		private void UnsubscribeEvent()
		{
			EasyTouch.On_TouchStart -= this.On_TouchStart;
			EasyTouch.On_TouchDown -= this.On_TouchDown;
			EasyTouch.On_TouchUp -= this.On_TouchUp;
			EasyTouch.On_Drag -= this.On_Drag;
			EasyTouch.On_DragStart -= this.On_DragStart;
			EasyTouch.On_DragEnd -= this.On_DragEnd;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000DF5EF File Offset: 0x000DD9EF
		private void OnCollisionEnter()
		{
			if (this.isStopOncollisionEnter && this.isOnDrag)
			{
				this.StopDrag();
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000DF610 File Offset: 0x000DDA10
		private void On_TouchStart(Gesture gesture)
		{
			if (this.realType == QuickBase.GameObjectType.UI && gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && this.fingerIndex == -1)
			{
				this.fingerIndex = gesture.fingerIndex;
				base.transform.SetAsLastSibling();
				this.onDragStart.Invoke(gesture);
				this.isOnDrag = true;
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000DF69C File Offset: 0x000DDA9C
		private void On_TouchDown(Gesture gesture)
		{
			if (this.isOnDrag && this.fingerIndex == gesture.fingerIndex && this.realType == QuickBase.GameObjectType.UI && gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
			{
				base.transform.position += gesture.deltaPosition;
				if (gesture.deltaPosition != Vector2.zero)
				{
					this.onDrag.Invoke(gesture);
				}
				this.lastGesture = gesture;
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000DF756 File Offset: 0x000DDB56
		private void On_TouchUp(Gesture gesture)
		{
			if (this.fingerIndex == gesture.fingerIndex && this.realType == QuickBase.GameObjectType.UI)
			{
				this.lastGesture = gesture;
				this.StopDrag();
			}
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000DF784 File Offset: 0x000DDB84
		private void On_DragStart(Gesture gesture)
		{
			if (this.realType != QuickBase.GameObjectType.UI && ((!this.enablePickOverUI && gesture.pickedUIElement == null) || this.enablePickOverUI) && gesture.pickedObject == base.gameObject && !this.isOnDrag)
			{
				this.isOnDrag = true;
				this.fingerIndex = gesture.fingerIndex;
				Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
				this.deltaPosition = touchToWorldPoint - base.transform.position;
				if (this.resetPhysic)
				{
					if (this.cachedRigidBody)
					{
						this.cachedRigidBody.isKinematic = true;
					}
					if (this.cachedRigidBody2D)
					{
						this.cachedRigidBody2D.isKinematic = true;
					}
				}
				this.onDragStart.Invoke(gesture);
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000DF878 File Offset: 0x000DDC78
		private void On_Drag(Gesture gesture)
		{
			if (this.fingerIndex == gesture.fingerIndex && (this.realType == QuickBase.GameObjectType.Obj_2D || this.realType == QuickBase.GameObjectType.Obj_3D) && gesture.pickedObject == base.gameObject && this.fingerIndex == gesture.fingerIndex)
			{
				Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position) - this.deltaPosition;
				base.transform.position = this.GetPositionAxes(position);
				if (gesture.deltaPosition != Vector2.zero)
				{
					this.onDrag.Invoke(gesture);
				}
				this.lastGesture = gesture;
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000DF931 File Offset: 0x000DDD31
		private void On_DragEnd(Gesture gesture)
		{
			if (this.fingerIndex == gesture.fingerIndex)
			{
				this.lastGesture = gesture;
				this.StopDrag();
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000DF954 File Offset: 0x000DDD54
		private Vector3 GetPositionAxes(Vector3 position)
		{
			Vector3 result = position;
			switch (this.axesAction)
			{
			case QuickBase.AffectedAxesAction.X:
				result = new Vector3(position.x, base.transform.position.y, base.transform.position.z);
				break;
			case QuickBase.AffectedAxesAction.Y:
				result = new Vector3(base.transform.position.x, position.y, base.transform.position.z);
				break;
			case QuickBase.AffectedAxesAction.Z:
				result = new Vector3(base.transform.position.x, base.transform.position.y, position.z);
				break;
			case QuickBase.AffectedAxesAction.XY:
				result = new Vector3(position.x, position.y, base.transform.position.z);
				break;
			case QuickBase.AffectedAxesAction.XZ:
				result = new Vector3(position.x, base.transform.position.y, position.z);
				break;
			case QuickBase.AffectedAxesAction.YZ:
				result = new Vector3(base.transform.position.x, position.y, position.z);
				break;
			}
			return result;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000DFAC8 File Offset: 0x000DDEC8
		public void StopDrag()
		{
			this.fingerIndex = -1;
			if (this.resetPhysic)
			{
				if (this.cachedRigidBody)
				{
					this.cachedRigidBody.isKinematic = this.isKinematic;
				}
				if (this.cachedRigidBody2D)
				{
					this.cachedRigidBody2D.isKinematic = this.isKinematic2D;
				}
			}
			this.isOnDrag = false;
			this.onDragEnd.Invoke(this.lastGesture);
		}

		// Token: 0x04001DE7 RID: 7655
		[SerializeField]
		public QuickDrag.OnDragStart onDragStart;

		// Token: 0x04001DE8 RID: 7656
		[SerializeField]
		public QuickDrag.OnDrag onDrag;

		// Token: 0x04001DE9 RID: 7657
		[SerializeField]
		public QuickDrag.OnDragEnd onDragEnd;

		// Token: 0x04001DEA RID: 7658
		public bool isStopOncollisionEnter;

		// Token: 0x04001DEB RID: 7659
		private Vector3 deltaPosition;

		// Token: 0x04001DEC RID: 7660
		private bool isOnDrag;

		// Token: 0x04001DED RID: 7661
		private Gesture lastGesture;

		// Token: 0x02000393 RID: 915
		[Serializable]
		public class OnDragStart : UnityEvent<Gesture>
		{
		}

		// Token: 0x02000394 RID: 916
		[Serializable]
		public class OnDrag : UnityEvent<Gesture>
		{
		}

		// Token: 0x02000395 RID: 917
		[Serializable]
		public class OnDragEnd : UnityEvent<Gesture>
		{
		}
	}
}
