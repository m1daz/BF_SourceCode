using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
[RequireComponent(typeof(PhotonView))]
[AddComponentMenu("Photon Networking/Photon Transform View")]
public class PhotonTransformView : MonoBehaviour, IPunObservable
{
	// Token: 0x06000945 RID: 2373 RVA: 0x00047180 File Offset: 0x00045580
	private void Awake()
	{
		this.m_PhotonView = base.GetComponent<PhotonView>();
		this.m_PositionControl = new PhotonTransformViewPositionControl(this.m_PositionModel);
		this.m_RotationControl = new PhotonTransformViewRotationControl(this.m_RotationModel);
		this.m_ScaleControl = new PhotonTransformViewScaleControl(this.m_ScaleModel);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x000471CC File Offset: 0x000455CC
	private void OnEnable()
	{
		this.m_firstTake = true;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x000471D5 File Offset: 0x000455D5
	private void Update()
	{
		if (this.m_PhotonView == null || this.m_PhotonView.isMine || !PhotonNetwork.connected)
		{
			return;
		}
		this.UpdatePosition();
		this.UpdateRotation();
		this.UpdateScale();
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00047215 File Offset: 0x00045615
	private void UpdatePosition()
	{
		if (!this.m_PositionModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
		{
			return;
		}
		base.transform.localPosition = this.m_PositionControl.UpdatePosition(base.transform.localPosition);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00047254 File Offset: 0x00045654
	private void UpdateRotation()
	{
		if (!this.m_RotationModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
		{
			return;
		}
		base.transform.localRotation = this.m_RotationControl.GetRotation(base.transform.localRotation);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00047293 File Offset: 0x00045693
	private void UpdateScale()
	{
		if (!this.m_ScaleModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
		{
			return;
		}
		base.transform.localScale = this.m_ScaleControl.GetScale(base.transform.localScale);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x000472D2 File Offset: 0x000456D2
	public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
	{
		this.m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000472E4 File Offset: 0x000456E4
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		this.m_PositionControl.OnPhotonSerializeView(base.transform.localPosition, stream, info);
		this.m_RotationControl.OnPhotonSerializeView(base.transform.localRotation, stream, info);
		this.m_ScaleControl.OnPhotonSerializeView(base.transform.localScale, stream, info);
		if (!this.m_PhotonView.isMine && this.m_PositionModel.DrawErrorGizmo)
		{
			this.DoDrawEstimatedPositionError();
		}
		if (stream.isReading)
		{
			this.m_ReceivedNetworkUpdate = true;
			if (this.m_firstTake)
			{
				this.m_firstTake = false;
				base.transform.localPosition = this.m_PositionControl.GetNetworkPosition();
				base.transform.localRotation = this.m_RotationControl.GetNetworkRotation();
				base.transform.localScale = this.m_ScaleControl.GetNetworkScale();
			}
		}
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x000473C8 File Offset: 0x000457C8
	private void DoDrawEstimatedPositionError()
	{
		Vector3 vector = this.m_PositionControl.GetNetworkPosition();
		if (base.transform.parent != null)
		{
			vector = base.transform.parent.position + vector;
		}
		Debug.DrawLine(vector, base.transform.position, Color.red, 2f);
		Debug.DrawLine(base.transform.position, base.transform.position + Vector3.up, Color.green, 2f);
		Debug.DrawLine(vector, vector + Vector3.up, Color.red, 2f);
	}

	// Token: 0x0400082A RID: 2090
	[SerializeField]
	private PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();

	// Token: 0x0400082B RID: 2091
	[SerializeField]
	private PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();

	// Token: 0x0400082C RID: 2092
	[SerializeField]
	private PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();

	// Token: 0x0400082D RID: 2093
	private PhotonTransformViewPositionControl m_PositionControl;

	// Token: 0x0400082E RID: 2094
	private PhotonTransformViewRotationControl m_RotationControl;

	// Token: 0x0400082F RID: 2095
	private PhotonTransformViewScaleControl m_ScaleControl;

	// Token: 0x04000830 RID: 2096
	private PhotonView m_PhotonView;

	// Token: 0x04000831 RID: 2097
	private bool m_ReceivedNetworkUpdate;

	// Token: 0x04000832 RID: 2098
	private bool m_firstTake;
}
