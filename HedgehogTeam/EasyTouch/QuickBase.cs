using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x0200038E RID: 910
	public class QuickBase : MonoBehaviour
	{
		// Token: 0x06001C4E RID: 7246 RVA: 0x000DEFEC File Offset: 0x000DD3EC
		private void Awake()
		{
			this.cachedRigidBody = base.GetComponent<Rigidbody>();
			if (this.cachedRigidBody)
			{
				this.isKinematic = this.cachedRigidBody.isKinematic;
			}
			this.cachedRigidBody2D = base.GetComponent<Rigidbody2D>();
			if (this.cachedRigidBody2D)
			{
				this.isKinematic2D = this.cachedRigidBody2D.isKinematic;
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000DF054 File Offset: 0x000DD454
		public virtual void Start()
		{
			EasyTouch.SetEnableAutoSelect(true);
			this.realType = QuickBase.GameObjectType.Obj_3D;
			if (base.GetComponent<Collider>())
			{
				this.realType = QuickBase.GameObjectType.Obj_3D;
			}
			else if (base.GetComponent<Collider2D>())
			{
				this.realType = QuickBase.GameObjectType.Obj_2D;
			}
			else if (base.GetComponent<CanvasRenderer>())
			{
				this.realType = QuickBase.GameObjectType.UI;
			}
			QuickBase.GameObjectType gameObjectType = this.realType;
			if (gameObjectType != QuickBase.GameObjectType.Obj_3D)
			{
				if (gameObjectType != QuickBase.GameObjectType.Obj_2D)
				{
					if (gameObjectType == QuickBase.GameObjectType.UI)
					{
						EasyTouch.instance.enableUIMode = true;
						EasyTouch.SetUICompatibily(false);
					}
				}
				else
				{
					EasyTouch.SetEnable2DCollider(true);
					LayerMask mask = EasyTouch.Get2DPickableLayer();
					mask |= 1 << base.gameObject.layer;
					EasyTouch.Set2DPickableLayer(mask);
				}
			}
			else
			{
				LayerMask mask = EasyTouch.Get3DPickableLayer();
				mask |= 1 << base.gameObject.layer;
				EasyTouch.Set3DPickableLayer(mask);
			}
			if (this.enablePickOverUI)
			{
				EasyTouch.instance.enableUIMode = true;
				EasyTouch.SetUICompatibily(false);
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000DF172 File Offset: 0x000DD572
		public virtual void OnEnable()
		{
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000DF174 File Offset: 0x000DD574
		public virtual void OnDisable()
		{
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000DF178 File Offset: 0x000DD578
		protected Vector3 GetInfluencedAxis()
		{
			Vector3 zero = Vector3.zero;
			switch (this.axesAction)
			{
			case QuickBase.AffectedAxesAction.X:
				zero = new Vector3(1f, 0f, 0f);
				break;
			case QuickBase.AffectedAxesAction.Y:
				zero = new Vector3(0f, 1f, 0f);
				break;
			case QuickBase.AffectedAxesAction.Z:
				zero = new Vector3(0f, 0f, 1f);
				break;
			case QuickBase.AffectedAxesAction.XY:
				zero = new Vector3(1f, 1f, 0f);
				break;
			case QuickBase.AffectedAxesAction.XZ:
				zero = new Vector3(1f, 0f, 1f);
				break;
			case QuickBase.AffectedAxesAction.YZ:
				zero = new Vector3(0f, 1f, 1f);
				break;
			case QuickBase.AffectedAxesAction.XYZ:
				zero = new Vector3(1f, 1f, 1f);
				break;
			}
			return zero;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000DF278 File Offset: 0x000DD678
		protected void DoDirectAction(float value)
		{
			Vector3 influencedAxis = this.GetInfluencedAxis();
			switch (this.directAction)
			{
			case QuickBase.DirectAction.Rotate:
				base.transform.Rotate(influencedAxis * value, Space.World);
				break;
			case QuickBase.DirectAction.RotateLocal:
				base.transform.Rotate(influencedAxis * value, Space.Self);
				break;
			case QuickBase.DirectAction.Translate:
				if (this.directCharacterController == null)
				{
					base.transform.Translate(influencedAxis * value, Space.World);
				}
				else
				{
					Vector3 motion = influencedAxis * value;
					this.directCharacterController.Move(motion);
				}
				break;
			case QuickBase.DirectAction.TranslateLocal:
				if (this.directCharacterController == null)
				{
					base.transform.Translate(influencedAxis * value, Space.Self);
				}
				else
				{
					Vector3 motion2 = this.directCharacterController.transform.TransformDirection(influencedAxis) * value;
					this.directCharacterController.Move(motion2);
				}
				break;
			case QuickBase.DirectAction.Scale:
				base.transform.localScale += influencedAxis * value;
				break;
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000DF39C File Offset: 0x000DD79C
		public void EnabledQuickComponent(string quickActionName)
		{
			QuickBase[] components = base.GetComponents<QuickBase>();
			foreach (QuickBase quickBase in components)
			{
				if (quickBase.quickActionName == quickActionName)
				{
					quickBase.enabled = true;
				}
			}
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000DF3E4 File Offset: 0x000DD7E4
		public void DisabledQuickComponent(string quickActionName)
		{
			QuickBase[] components = base.GetComponents<QuickBase>();
			foreach (QuickBase quickBase in components)
			{
				if (quickBase.quickActionName == quickActionName)
				{
					quickBase.enabled = false;
				}
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000DF42C File Offset: 0x000DD82C
		public void DisabledAllSwipeExcepted(string quickActionName)
		{
			QuickSwipe[] array = UnityEngine.Object.FindObjectsOfType(typeof(QuickSwipe)) as QuickSwipe[];
			foreach (QuickSwipe quickSwipe in array)
			{
				if (quickSwipe.quickActionName != quickActionName || (quickSwipe.quickActionName == quickActionName && quickSwipe.gameObject != base.gameObject))
				{
					quickSwipe.enabled = false;
				}
			}
		}

		// Token: 0x04001DC2 RID: 7618
		public string quickActionName;

		// Token: 0x04001DC3 RID: 7619
		public bool isMultiTouch;

		// Token: 0x04001DC4 RID: 7620
		public bool is2Finger;

		// Token: 0x04001DC5 RID: 7621
		public bool isOnTouch;

		// Token: 0x04001DC6 RID: 7622
		public bool enablePickOverUI;

		// Token: 0x04001DC7 RID: 7623
		public bool resetPhysic;

		// Token: 0x04001DC8 RID: 7624
		public QuickBase.DirectAction directAction;

		// Token: 0x04001DC9 RID: 7625
		public QuickBase.AffectedAxesAction axesAction;

		// Token: 0x04001DCA RID: 7626
		public float sensibility = 1f;

		// Token: 0x04001DCB RID: 7627
		public CharacterController directCharacterController;

		// Token: 0x04001DCC RID: 7628
		public bool inverseAxisValue;

		// Token: 0x04001DCD RID: 7629
		protected Rigidbody cachedRigidBody;

		// Token: 0x04001DCE RID: 7630
		protected bool isKinematic;

		// Token: 0x04001DCF RID: 7631
		protected Rigidbody2D cachedRigidBody2D;

		// Token: 0x04001DD0 RID: 7632
		protected bool isKinematic2D;

		// Token: 0x04001DD1 RID: 7633
		protected QuickBase.GameObjectType realType;

		// Token: 0x04001DD2 RID: 7634
		protected int fingerIndex = -1;

		// Token: 0x0200038F RID: 911
		protected enum GameObjectType
		{
			// Token: 0x04001DD4 RID: 7636
			Auto,
			// Token: 0x04001DD5 RID: 7637
			Obj_3D,
			// Token: 0x04001DD6 RID: 7638
			Obj_2D,
			// Token: 0x04001DD7 RID: 7639
			UI
		}

		// Token: 0x02000390 RID: 912
		public enum DirectAction
		{
			// Token: 0x04001DD9 RID: 7641
			None,
			// Token: 0x04001DDA RID: 7642
			Rotate,
			// Token: 0x04001DDB RID: 7643
			RotateLocal,
			// Token: 0x04001DDC RID: 7644
			Translate,
			// Token: 0x04001DDD RID: 7645
			TranslateLocal,
			// Token: 0x04001DDE RID: 7646
			Scale
		}

		// Token: 0x02000391 RID: 913
		public enum AffectedAxesAction
		{
			// Token: 0x04001DE0 RID: 7648
			X,
			// Token: 0x04001DE1 RID: 7649
			Y,
			// Token: 0x04001DE2 RID: 7650
			Z,
			// Token: 0x04001DE3 RID: 7651
			XY,
			// Token: 0x04001DE4 RID: 7652
			XZ,
			// Token: 0x04001DE5 RID: 7653
			YZ,
			// Token: 0x04001DE6 RID: 7654
			XYZ
		}
	}
}
