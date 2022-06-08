using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
[AddComponentMenu("FingerGestures/Toolbox/Input Manager")]
public class TBInputManager : MonoBehaviour
{
	// Token: 0x06002064 RID: 8292 RVA: 0x000F27B0 File Offset: 0x000F0BB0
	private void Start()
	{
		if (!this.raycastCamera)
		{
			this.raycastCamera = Camera.main;
		}
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x000F27D0 File Offset: 0x000F0BD0
	private void OnEnable()
	{
		if (this.trackFingerDown)
		{
			FingerGestures.OnFingerDown += this.FingerGestures_OnFingerDown;
		}
		if (this.trackFingerUp)
		{
			FingerGestures.OnFingerUp += this.FingerGestures_OnFingerUp;
		}
		if (this.trackDrag)
		{
			FingerGestures.OnFingerDragBegin += this.FingerGestures_OnFingerDragBegin;
		}
		if (this.trackTap)
		{
			FingerGestures.OnFingerTap += this.FingerGestures_OnFingerTap;
		}
		if (this.trackLongPress)
		{
			FingerGestures.OnFingerLongPress += this.FingerGestures_OnFingerLongPress;
		}
		if (this.trackSwipe)
		{
			FingerGestures.OnFingerSwipe += this.FingerGestures_OnFingerSwipe;
		}
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x000F2888 File Offset: 0x000F0C88
	private void OnDisable()
	{
		FingerGestures.OnFingerDown -= this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerUp -= this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerDragBegin -= this.FingerGestures_OnFingerDragBegin;
		FingerGestures.OnFingerTap -= this.FingerGestures_OnFingerTap;
		FingerGestures.OnFingerLongPress -= this.FingerGestures_OnFingerLongPress;
		FingerGestures.OnFingerSwipe -= this.FingerGestures_OnFingerSwipe;
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x000F28FC File Offset: 0x000F0CFC
	private void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
		TBFingerUp tbfingerUp = this.PickComponent<TBFingerUp>(fingerPos);
		if (tbfingerUp)
		{
			tbfingerUp.RaiseFingerUp(fingerIndex, fingerPos, timeHeldDown);
		}
	}

	// Token: 0x06002068 RID: 8296 RVA: 0x000F2928 File Offset: 0x000F0D28
	private void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
		TBFingerDown tbfingerDown = this.PickComponent<TBFingerDown>(fingerPos);
		if (tbfingerDown)
		{
			tbfingerDown.RaiseFingerDown(fingerIndex, fingerPos);
		}
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x000F2954 File Offset: 0x000F0D54
	private void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
	{
		TBDrag tbdrag = this.PickComponent<TBDrag>(startPos);
		if (tbdrag && !tbdrag.Dragging)
		{
			tbdrag.BeginDrag(fingerIndex, fingerPos);
			tbdrag.OnDragMove += this.draggable_OnDragMove;
			tbdrag.OnDragEnd += this.draggable_OnDragEnd;
		}
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x000F29AC File Offset: 0x000F0DAC
	private bool ProjectScreenPointOnDragPlane(Vector3 refPos, Vector2 screenPos, out Vector3 worldPos)
	{
		worldPos = refPos;
		switch (this.dragPlaneType)
		{
		case TBInputManager.DragPlaneType.XY:
			worldPos = this.raycastCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(refPos.z - this.raycastCamera.transform.position.z)));
			return true;
		case TBInputManager.DragPlaneType.XZ:
			worldPos = this.raycastCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(refPos.y - this.raycastCamera.transform.position.y)));
			return true;
		case TBInputManager.DragPlaneType.ZY:
			worldPos = this.raycastCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(refPos.x - this.raycastCamera.transform.position.x)));
			return true;
		case TBInputManager.DragPlaneType.UseCollider:
		{
			Ray ray = this.raycastCamera.ScreenPointToRay(screenPos);
			RaycastHit raycastHit;
			if (!this.dragPlaneCollider.Raycast(ray, out raycastHit, 3.4028235E+38f))
			{
				return false;
			}
			worldPos = raycastHit.point + this.dragPlaneOffset * raycastHit.normal;
			return true;
		}
		case TBInputManager.DragPlaneType.Camera:
		{
			Transform transform = this.raycastCamera.transform;
			Plane plane = new Plane(-transform.forward, refPos);
			Ray ray2 = this.raycastCamera.ScreenPointToRay(screenPos);
			float distance = 0f;
			if (!plane.Raycast(ray2, out distance))
			{
				return false;
			}
			worldPos = ray2.GetPoint(distance);
			return true;
		}
		default:
			return false;
		}
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x000F2B7C File Offset: 0x000F0F7C
	private void draggable_OnDragMove(TBDrag sender)
	{
		Vector2 screenPos = sender.FingerPos - sender.MoveDelta;
		Vector3 b;
		Vector3 a;
		if (this.ProjectScreenPointOnDragPlane(sender.transform.position, screenPos, out b) && this.ProjectScreenPointOnDragPlane(sender.transform.position, sender.FingerPos, out a))
		{
			Vector3 b2 = a - b;
			sender.transform.position += b2;
		}
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x000F2BF1 File Offset: 0x000F0FF1
	private void draggable_OnDragEnd(TBDrag source)
	{
		source.OnDragMove -= this.draggable_OnDragMove;
		source.OnDragEnd -= this.draggable_OnDragEnd;
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x000F2C18 File Offset: 0x000F1018
	private void FingerGestures_OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
		TBTap tbtap = this.PickComponent<TBTap>(fingerPos);
		if (tbtap)
		{
			tbtap.RaiseTap(fingerIndex, fingerPos, tapCount);
		}
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x000F2C44 File Offset: 0x000F1044
	private void FingerGestures_OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
	{
		TBLongPress tblongPress = this.PickComponent<TBLongPress>(fingerPos);
		if (tblongPress)
		{
			tblongPress.RaiseLongPress(fingerIndex, fingerPos);
		}
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x000F2C70 File Offset: 0x000F1070
	private void FingerGestures_OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
		TBSwipe tbswipe = this.PickComponent<TBSwipe>(startPos);
		if (tbswipe)
		{
			tbswipe.RaiseSwipe(fingerIndex, startPos, direction, velocity);
		}
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x000F2C9C File Offset: 0x000F109C
	private GameObject PickObject(Vector2 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 3.4028235E+38f, ~this.ignoreLayers))
		{
			return raycastHit.collider.gameObject;
		}
		return null;
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x000F2CE8 File Offset: 0x000F10E8
	private T PickComponent<T>(Vector2 screenPos) where T : TBComponent
	{
		GameObject gameObject = this.PickObject(screenPos);
		if (!gameObject)
		{
			return (T)((object)null);
		}
		return gameObject.GetComponent<T>();
	}

	// Token: 0x0400212E RID: 8494
	public bool trackFingerUp = true;

	// Token: 0x0400212F RID: 8495
	public bool trackFingerDown = true;

	// Token: 0x04002130 RID: 8496
	public bool trackDrag = true;

	// Token: 0x04002131 RID: 8497
	public bool trackTap = true;

	// Token: 0x04002132 RID: 8498
	public bool trackLongPress = true;

	// Token: 0x04002133 RID: 8499
	public bool trackSwipe = true;

	// Token: 0x04002134 RID: 8500
	public Camera raycastCamera;

	// Token: 0x04002135 RID: 8501
	public LayerMask ignoreLayers = 0;

	// Token: 0x04002136 RID: 8502
	public TBInputManager.DragPlaneType dragPlaneType = TBInputManager.DragPlaneType.Camera;

	// Token: 0x04002137 RID: 8503
	public Collider dragPlaneCollider;

	// Token: 0x04002138 RID: 8504
	public float dragPlaneOffset;

	// Token: 0x0200045C RID: 1116
	public enum DragPlaneType
	{
		// Token: 0x0400213A RID: 8506
		XY,
		// Token: 0x0400213B RID: 8507
		XZ,
		// Token: 0x0400213C RID: 8508
		ZY,
		// Token: 0x0400213D RID: 8509
		UseCollider,
		// Token: 0x0400213E RID: 8510
		Camera
	}
}
