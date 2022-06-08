using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class PhotonTransformViewScaleControl
{
	// Token: 0x0600095D RID: 2397 RVA: 0x00047ADF File Offset: 0x00045EDF
	public PhotonTransformViewScaleControl(PhotonTransformViewScaleModel model)
	{
		this.m_Model = model;
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00047AF9 File Offset: 0x00045EF9
	public Vector3 GetNetworkScale()
	{
		return this.m_NetworkScale;
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00047B04 File Offset: 0x00045F04
	public Vector3 GetScale(Vector3 currentScale)
	{
		switch (this.m_Model.InterpolateOption)
		{
		default:
			return this.m_NetworkScale;
		case PhotonTransformViewScaleModel.InterpolateOptions.MoveTowards:
			return Vector3.MoveTowards(currentScale, this.m_NetworkScale, this.m_Model.InterpolateMoveTowardsSpeed * Time.deltaTime);
		case PhotonTransformViewScaleModel.InterpolateOptions.Lerp:
			return Vector3.Lerp(currentScale, this.m_NetworkScale, this.m_Model.InterpolateLerpSpeed * Time.deltaTime);
		}
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00047B78 File Offset: 0x00045F78
	public void OnPhotonSerializeView(Vector3 currentScale, PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.m_Model.SynchronizeEnabled)
		{
			return;
		}
		if (stream.isWriting)
		{
			stream.SendNext(currentScale);
			this.m_NetworkScale = currentScale;
		}
		else
		{
			this.m_NetworkScale = (Vector3)stream.ReceiveNext();
		}
	}

	// Token: 0x0400085E RID: 2142
	private PhotonTransformViewScaleModel m_Model;

	// Token: 0x0400085F RID: 2143
	private Vector3 m_NetworkScale = Vector3.one;
}
