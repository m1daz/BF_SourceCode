using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class PhotonTransformViewRotationControl
{
	// Token: 0x06000958 RID: 2392 RVA: 0x000479DD File Offset: 0x00045DDD
	public PhotonTransformViewRotationControl(PhotonTransformViewRotationModel model)
	{
		this.m_Model = model;
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x000479EC File Offset: 0x00045DEC
	public Quaternion GetNetworkRotation()
	{
		return this.m_NetworkRotation;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000479F4 File Offset: 0x00045DF4
	public Quaternion GetRotation(Quaternion currentRotation)
	{
		switch (this.m_Model.InterpolateOption)
		{
		default:
			return this.m_NetworkRotation;
		case PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards:
			return Quaternion.RotateTowards(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateRotateTowardsSpeed * Time.deltaTime);
		case PhotonTransformViewRotationModel.InterpolateOptions.Lerp:
			return Quaternion.Lerp(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
		}
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00047A68 File Offset: 0x00045E68
	public void OnPhotonSerializeView(Quaternion currentRotation, PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.m_Model.SynchronizeEnabled)
		{
			return;
		}
		if (stream.isWriting)
		{
			stream.SendNext(currentRotation);
			this.m_NetworkRotation = currentRotation;
		}
		else
		{
			this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
		}
	}

	// Token: 0x04000854 RID: 2132
	private PhotonTransformViewRotationModel m_Model;

	// Token: 0x04000855 RID: 2133
	private Quaternion m_NetworkRotation;
}
