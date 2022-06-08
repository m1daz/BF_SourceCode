using System;
using System.Collections.Generic;
using System.Reflection;
using Photon;
using UnityEngine;

// Token: 0x0200011D RID: 285
[AddComponentMenu("Photon Networking/Photon View &v")]
public class PhotonView : Photon.MonoBehaviour
{
	// Token: 0x17000105 RID: 261
	// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00044C06 File Offset: 0x00043006
	// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00044C34 File Offset: 0x00043034
	public int prefix
	{
		get
		{
			if (this.prefixBackup == -1 && PhotonNetwork.networkingPeer != null)
			{
				this.prefixBackup = (int)PhotonNetwork.networkingPeer.currentLevelPrefix;
			}
			return this.prefixBackup;
		}
		set
		{
			this.prefixBackup = value;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00044C3D File Offset: 0x0004303D
	// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00044C66 File Offset: 0x00043066
	public object[] instantiationData
	{
		get
		{
			if (!this.didAwake)
			{
				this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
			}
			return this.instantiationDataField;
		}
		set
		{
			this.instantiationDataField = value;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00044C6F File Offset: 0x0004306F
	// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00044C78 File Offset: 0x00043078
	public int viewID
	{
		get
		{
			return this.viewIdField;
		}
		set
		{
			bool flag = this.didAwake && this.viewIdField == 0;
			this.ownerId = value / PhotonNetwork.MAX_VIEW_IDS;
			this.viewIdField = value;
			if (flag)
			{
				PhotonNetwork.networkingPeer.RegisterPhotonView(this);
			}
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00044CC2 File Offset: 0x000430C2
	public bool isSceneView
	{
		get
		{
			return this.CreatorActorNr == 0;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00044CCD File Offset: 0x000430CD
	public PhotonPlayer owner
	{
		get
		{
			return PhotonPlayer.Find(this.ownerId);
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00044CDA File Offset: 0x000430DA
	public int OwnerActorNr
	{
		get
		{
			return this.ownerId;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x060008DA RID: 2266 RVA: 0x00044CE2 File Offset: 0x000430E2
	public bool isOwnerActive
	{
		get
		{
			return this.ownerId != 0 && PhotonNetwork.networkingPeer.mActors.ContainsKey(this.ownerId);
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x060008DB RID: 2267 RVA: 0x00044D07 File Offset: 0x00043107
	public int CreatorActorNr
	{
		get
		{
			return this.viewIdField / PhotonNetwork.MAX_VIEW_IDS;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x060008DC RID: 2268 RVA: 0x00044D15 File Offset: 0x00043115
	public bool isMine
	{
		get
		{
			return this.ownerId == PhotonNetwork.player.ID || (this.isSceneView && PhotonNetwork.isMasterClient);
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00044D42 File Offset: 0x00043142
	protected internal void Awake()
	{
		if (this.viewID != 0)
		{
			PhotonNetwork.networkingPeer.RegisterPhotonView(this);
			this.instantiationDataField = PhotonNetwork.networkingPeer.FetchInstantiationData(this.instantiationId);
		}
		this.didAwake = true;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00044D77 File Offset: 0x00043177
	public void RequestOwnership()
	{
		PhotonNetwork.networkingPeer.RequestOwnership(this.viewID, this.ownerId);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00044D8F File Offset: 0x0004318F
	public void TransferOwnership(PhotonPlayer newOwner)
	{
		this.TransferOwnership(newOwner.ID);
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00044D9D File Offset: 0x0004319D
	public void TransferOwnership(int newOwnerId)
	{
		PhotonNetwork.networkingPeer.TransferOwnership(this.viewID, newOwnerId);
		this.ownerId = newOwnerId;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00044DB8 File Offset: 0x000431B8
	protected internal void OnDestroy()
	{
		if (!this.removedFromLocalViewList)
		{
			bool flag = PhotonNetwork.networkingPeer.LocalCleanPhotonView(this);
			bool isLoadingLevel = Application.isLoadingLevel;
			if (flag && !isLoadingLevel && this.instantiationId > 0 && !PhotonHandler.AppQuits && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
			{
				Debug.Log("PUN-instantiated '" + base.gameObject.name + "' got destroyed by engine. This is OK when loading levels. Otherwise use: PhotonNetwork.Destroy().");
			}
		}
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00044E30 File Offset: 0x00043230
	public void SerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		this.SerializeComponent(this.observed, stream, info);
		if (this.ObservedComponents != null && this.ObservedComponents.Count > 0)
		{
			for (int i = 0; i < this.ObservedComponents.Count; i++)
			{
				this.SerializeComponent(this.ObservedComponents[i], stream, info);
			}
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00044E98 File Offset: 0x00043298
	public void DeserializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		this.DeserializeComponent(this.observed, stream, info);
		if (this.ObservedComponents != null && this.ObservedComponents.Count > 0)
		{
			for (int i = 0; i < this.ObservedComponents.Count; i++)
			{
				this.DeserializeComponent(this.ObservedComponents[i], stream, info);
			}
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00044F00 File Offset: 0x00043300
	protected internal void DeserializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		if (component == null)
		{
			return;
		}
		if (component is UnityEngine.MonoBehaviour)
		{
			this.ExecuteComponentOnSerialize(component, stream, info);
		}
		else if (component is Transform)
		{
			Transform transform = (Transform)component;
			switch (this.onSerializeTransformOption)
			{
			case OnSerializeTransform.OnlyPosition:
				transform.localPosition = (Vector3)stream.ReceiveNext();
				break;
			case OnSerializeTransform.OnlyRotation:
				transform.localRotation = (Quaternion)stream.ReceiveNext();
				break;
			case OnSerializeTransform.OnlyScale:
				transform.localScale = (Vector3)stream.ReceiveNext();
				break;
			case OnSerializeTransform.PositionAndRotation:
				transform.localPosition = (Vector3)stream.ReceiveNext();
				transform.localRotation = (Quaternion)stream.ReceiveNext();
				break;
			case OnSerializeTransform.All:
				transform.localPosition = (Vector3)stream.ReceiveNext();
				transform.localRotation = (Quaternion)stream.ReceiveNext();
				transform.localScale = (Vector3)stream.ReceiveNext();
				break;
			}
		}
		else if (component is Rigidbody)
		{
			Rigidbody rigidbody = (Rigidbody)component;
			OnSerializeRigidBody onSerializeRigidBody = this.onSerializeRigidBodyOption;
			if (onSerializeRigidBody != OnSerializeRigidBody.All)
			{
				if (onSerializeRigidBody != OnSerializeRigidBody.OnlyAngularVelocity)
				{
					if (onSerializeRigidBody == OnSerializeRigidBody.OnlyVelocity)
					{
						rigidbody.velocity = (Vector3)stream.ReceiveNext();
					}
				}
				else
				{
					rigidbody.angularVelocity = (Vector3)stream.ReceiveNext();
				}
			}
			else
			{
				rigidbody.velocity = (Vector3)stream.ReceiveNext();
				rigidbody.angularVelocity = (Vector3)stream.ReceiveNext();
			}
		}
		else if (component is Rigidbody2D)
		{
			Rigidbody2D rigidbody2D = (Rigidbody2D)component;
			OnSerializeRigidBody onSerializeRigidBody2 = this.onSerializeRigidBodyOption;
			if (onSerializeRigidBody2 != OnSerializeRigidBody.All)
			{
				if (onSerializeRigidBody2 != OnSerializeRigidBody.OnlyAngularVelocity)
				{
					if (onSerializeRigidBody2 == OnSerializeRigidBody.OnlyVelocity)
					{
						rigidbody2D.velocity = (Vector2)stream.ReceiveNext();
					}
				}
				else
				{
					rigidbody2D.angularVelocity = (float)stream.ReceiveNext();
				}
			}
			else
			{
				rigidbody2D.velocity = (Vector2)stream.ReceiveNext();
				rigidbody2D.angularVelocity = (float)stream.ReceiveNext();
			}
		}
		else
		{
			Debug.LogError("Type of observed is unknown when receiving.");
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00045138 File Offset: 0x00043538
	protected internal void SerializeComponent(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		if (component == null)
		{
			return;
		}
		if (component is UnityEngine.MonoBehaviour)
		{
			this.ExecuteComponentOnSerialize(component, stream, info);
		}
		else if (component is Transform)
		{
			Transform transform = (Transform)component;
			switch (this.onSerializeTransformOption)
			{
			case OnSerializeTransform.OnlyPosition:
				stream.SendNext(transform.localPosition);
				break;
			case OnSerializeTransform.OnlyRotation:
				stream.SendNext(transform.localRotation);
				break;
			case OnSerializeTransform.OnlyScale:
				stream.SendNext(transform.localScale);
				break;
			case OnSerializeTransform.PositionAndRotation:
				stream.SendNext(transform.localPosition);
				stream.SendNext(transform.localRotation);
				break;
			case OnSerializeTransform.All:
				stream.SendNext(transform.localPosition);
				stream.SendNext(transform.localRotation);
				stream.SendNext(transform.localScale);
				break;
			}
		}
		else if (component is Rigidbody)
		{
			Rigidbody rigidbody = (Rigidbody)component;
			OnSerializeRigidBody onSerializeRigidBody = this.onSerializeRigidBodyOption;
			if (onSerializeRigidBody != OnSerializeRigidBody.All)
			{
				if (onSerializeRigidBody != OnSerializeRigidBody.OnlyAngularVelocity)
				{
					if (onSerializeRigidBody == OnSerializeRigidBody.OnlyVelocity)
					{
						stream.SendNext(rigidbody.velocity);
					}
				}
				else
				{
					stream.SendNext(rigidbody.angularVelocity);
				}
			}
			else
			{
				stream.SendNext(rigidbody.velocity);
				stream.SendNext(rigidbody.angularVelocity);
			}
		}
		else if (component is Rigidbody2D)
		{
			Rigidbody2D rigidbody2D = (Rigidbody2D)component;
			OnSerializeRigidBody onSerializeRigidBody2 = this.onSerializeRigidBodyOption;
			if (onSerializeRigidBody2 != OnSerializeRigidBody.All)
			{
				if (onSerializeRigidBody2 != OnSerializeRigidBody.OnlyAngularVelocity)
				{
					if (onSerializeRigidBody2 == OnSerializeRigidBody.OnlyVelocity)
					{
						stream.SendNext(rigidbody2D.velocity);
					}
				}
				else
				{
					stream.SendNext(rigidbody2D.angularVelocity);
				}
			}
			else
			{
				stream.SendNext(rigidbody2D.velocity);
				stream.SendNext(rigidbody2D.angularVelocity);
			}
		}
		else
		{
			Debug.LogError("Observed type is not serializable: " + component.GetType());
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0004537C File Offset: 0x0004377C
	protected internal void ExecuteComponentOnSerialize(Component component, PhotonStream stream, PhotonMessageInfo info)
	{
		IPunObservable punObservable = component as IPunObservable;
		if (punObservable != null)
		{
			punObservable.OnPhotonSerializeView(stream, info);
		}
		else if (component != null)
		{
			MethodInfo methodInfo = null;
			if (!this.m_OnSerializeMethodInfos.TryGetValue(component, out methodInfo))
			{
				if (!NetworkingPeer.GetMethod(component as UnityEngine.MonoBehaviour, PhotonNetworkingMessage.OnPhotonSerializeView.ToString(), out methodInfo))
				{
					Debug.LogError("The observed monobehaviour (" + component.name + ") of this PhotonView does not implement OnPhotonSerializeView()!");
					methodInfo = null;
				}
				this.m_OnSerializeMethodInfos.Add(component, methodInfo);
			}
			if (methodInfo != null)
			{
				methodInfo.Invoke(component, new object[]
				{
					stream,
					info
				});
			}
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00045435 File Offset: 0x00043835
	public void RefreshRpcMonoBehaviourCache()
	{
		this.RpcMonoBehaviours = base.GetComponents<UnityEngine.MonoBehaviour>();
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00045443 File Offset: 0x00043843
	public void RPC(string methodName, PhotonTargets target, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, target, false, parameters);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0004544F File Offset: 0x0004384F
	public void RpcSecure(string methodName, PhotonTargets target, bool encrypt, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, target, encrypt, parameters);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0004545C File Offset: 0x0004385C
	public void RPC(string methodName, PhotonPlayer targetPlayer, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, targetPlayer, false, parameters);
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00045468 File Offset: 0x00043868
	public void RpcSecure(string methodName, PhotonPlayer targetPlayer, bool encrypt, params object[] parameters)
	{
		PhotonNetwork.RPC(this, methodName, targetPlayer, encrypt, parameters);
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00045475 File Offset: 0x00043875
	public static PhotonView Get(Component component)
	{
		return component.GetComponent<PhotonView>();
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0004547D File Offset: 0x0004387D
	public static PhotonView Get(GameObject gameObj)
	{
		return gameObj.GetComponent<PhotonView>();
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00045485 File Offset: 0x00043885
	public static PhotonView Find(int viewID)
	{
		return PhotonNetwork.networkingPeer.GetPhotonView(viewID);
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00045494 File Offset: 0x00043894
	public override string ToString()
	{
		return string.Format("View ({3}){0} on {1} {2}", new object[]
		{
			this.viewID,
			(!(base.gameObject != null)) ? "GO==null" : base.gameObject.name,
			(!this.isSceneView) ? string.Empty : "(scene)",
			this.prefix
		});
	}

	// Token: 0x040007CD RID: 1997
	public int ownerId;

	// Token: 0x040007CE RID: 1998
	public int group;

	// Token: 0x040007CF RID: 1999
	protected internal bool mixedModeIsReliable;

	// Token: 0x040007D0 RID: 2000
	public bool OwnerShipWasTransfered;

	// Token: 0x040007D1 RID: 2001
	public int prefixBackup = -1;

	// Token: 0x040007D2 RID: 2002
	internal object[] instantiationDataField;

	// Token: 0x040007D3 RID: 2003
	protected internal object[] lastOnSerializeDataSent;

	// Token: 0x040007D4 RID: 2004
	protected internal object[] lastOnSerializeDataReceived;

	// Token: 0x040007D5 RID: 2005
	public Component observed;

	// Token: 0x040007D6 RID: 2006
	public ViewSynchronization synchronization;

	// Token: 0x040007D7 RID: 2007
	public OnSerializeTransform onSerializeTransformOption = OnSerializeTransform.PositionAndRotation;

	// Token: 0x040007D8 RID: 2008
	public OnSerializeRigidBody onSerializeRigidBodyOption = OnSerializeRigidBody.All;

	// Token: 0x040007D9 RID: 2009
	public OwnershipOption ownershipTransfer;

	// Token: 0x040007DA RID: 2010
	public List<Component> ObservedComponents;

	// Token: 0x040007DB RID: 2011
	private Dictionary<Component, MethodInfo> m_OnSerializeMethodInfos = new Dictionary<Component, MethodInfo>(3);

	// Token: 0x040007DC RID: 2012
	[SerializeField]
	private int viewIdField;

	// Token: 0x040007DD RID: 2013
	public int instantiationId;

	// Token: 0x040007DE RID: 2014
	protected internal bool didAwake;

	// Token: 0x040007DF RID: 2015
	[SerializeField]
	protected internal bool isRuntimeInstantiated;

	// Token: 0x040007E0 RID: 2016
	protected internal bool removedFromLocalViewList;

	// Token: 0x040007E1 RID: 2017
	internal UnityEngine.MonoBehaviour[] RpcMonoBehaviours;

	// Token: 0x040007E2 RID: 2018
	private MethodInfo OnSerializeMethodInfo;

	// Token: 0x040007E3 RID: 2019
	private bool failedToFindOnSerialize;
}
