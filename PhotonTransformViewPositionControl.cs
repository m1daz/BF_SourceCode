using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class PhotonTransformViewPositionControl
{
	// Token: 0x0600094E RID: 2382 RVA: 0x00047473 File Offset: 0x00045873
	public PhotonTransformViewPositionControl(PhotonTransformViewPositionModel model)
	{
		this.m_Model = model;
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x000474A0 File Offset: 0x000458A0
	private Vector3 GetOldestStoredNetworkPosition()
	{
		Vector3 result = this.m_NetworkPosition;
		if (this.m_OldNetworkPositions.Count > 0)
		{
			result = this.m_OldNetworkPositions.Peek();
		}
		return result;
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x000474D2 File Offset: 0x000458D2
	public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
	{
		this.m_SynchronizedSpeed = speed;
		this.m_SynchronizedTurnSpeed = turnSpeed;
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x000474E4 File Offset: 0x000458E4
	public Vector3 UpdatePosition(Vector3 currentPosition)
	{
		Vector3 vector = this.GetNetworkPosition() + this.GetExtrapolatedPositionOffset();
		switch (this.m_Model.InterpolateOption)
		{
		case PhotonTransformViewPositionModel.InterpolateOptions.Disabled:
			if (!this.m_UpdatedPositionAfterOnSerialize)
			{
				currentPosition = vector;
				this.m_UpdatedPositionAfterOnSerialize = true;
			}
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.FixedSpeed:
			currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * this.m_Model.InterpolateMoveTowardsSpeed);
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed:
			if (this.m_OldNetworkPositions.Count != 0)
			{
				float num = Vector3.Distance(this.m_NetworkPosition, this.GetOldestStoredNetworkPosition()) / (float)this.m_OldNetworkPositions.Count * (float)PhotonNetwork.sendRateOnSerialize;
				currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * num);
			}
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues:
			if (this.m_SynchronizedSpeed.magnitude == 0f)
			{
				currentPosition = vector;
			}
			else
			{
				currentPosition = Vector3.MoveTowards(currentPosition, vector, Time.deltaTime * this.m_SynchronizedSpeed.magnitude);
			}
			break;
		case PhotonTransformViewPositionModel.InterpolateOptions.Lerp:
			currentPosition = Vector3.Lerp(currentPosition, vector, Time.deltaTime * this.m_Model.InterpolateLerpSpeed);
			break;
		}
		if (this.m_Model.TeleportEnabled && Vector3.Distance(currentPosition, this.GetNetworkPosition()) > this.m_Model.TeleportIfDistanceGreaterThan)
		{
			currentPosition = this.GetNetworkPosition();
		}
		return currentPosition;
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00047647 File Offset: 0x00045A47
	public Vector3 GetNetworkPosition()
	{
		return this.m_NetworkPosition;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00047650 File Offset: 0x00045A50
	public Vector3 GetExtrapolatedPositionOffset()
	{
		float num = (float)(PhotonNetwork.time - this.m_LastSerializeTime);
		if (this.m_Model.ExtrapolateIncludingRoundTripTime)
		{
			num += (float)PhotonNetwork.GetPing() / 1000f;
		}
		Vector3 result = Vector3.zero;
		PhotonTransformViewPositionModel.ExtrapolateOptions extrapolateOption = this.m_Model.ExtrapolateOption;
		if (extrapolateOption != PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues)
		{
			if (extrapolateOption != PhotonTransformViewPositionModel.ExtrapolateOptions.FixedSpeed)
			{
				if (extrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.EstimateSpeedAndTurn)
				{
					Vector3 a = (this.m_NetworkPosition - this.GetOldestStoredNetworkPosition()) * (float)PhotonNetwork.sendRateOnSerialize;
					result = a * num;
				}
			}
			else
			{
				Vector3 normalized = (this.m_NetworkPosition - this.GetOldestStoredNetworkPosition()).normalized;
				result = normalized * this.m_Model.ExtrapolateSpeed * num;
			}
		}
		else
		{
			Quaternion rotation = Quaternion.Euler(0f, this.m_SynchronizedTurnSpeed * num, 0f);
			result = rotation * (this.m_SynchronizedSpeed * num);
		}
		return result;
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00047750 File Offset: 0x00045B50
	public void OnPhotonSerializeView(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
	{
		if (!this.m_Model.SynchronizeEnabled)
		{
			return;
		}
		if (stream.isWriting)
		{
			this.SerializeData(currentPosition, stream, info);
		}
		else
		{
			this.DeserializeData(stream, info);
		}
		this.m_LastSerializeTime = PhotonNetwork.time;
		this.m_UpdatedPositionAfterOnSerialize = false;
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x000477A4 File Offset: 0x00045BA4
	private void SerializeData(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
	{
		stream.SendNext(currentPosition);
		this.m_NetworkPosition = currentPosition;
		if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
		{
			stream.SendNext(this.m_SynchronizedSpeed);
			stream.SendNext(this.m_SynchronizedTurnSpeed);
		}
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00047808 File Offset: 0x00045C08
	private void DeserializeData(PhotonStream stream, PhotonMessageInfo info)
	{
		Vector3 networkPosition = (Vector3)stream.ReceiveNext();
		if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
		{
			this.m_SynchronizedSpeed = (Vector3)stream.ReceiveNext();
			this.m_SynchronizedTurnSpeed = (float)stream.ReceiveNext();
		}
		if (this.m_OldNetworkPositions.Count == 0)
		{
			this.m_NetworkPosition = networkPosition;
		}
		this.m_OldNetworkPositions.Enqueue(this.m_NetworkPosition);
		this.m_NetworkPosition = networkPosition;
		while (this.m_OldNetworkPositions.Count > this.m_Model.ExtrapolateNumberOfStoredPositions)
		{
			this.m_OldNetworkPositions.Dequeue();
		}
	}

	// Token: 0x04000833 RID: 2099
	private PhotonTransformViewPositionModel m_Model;

	// Token: 0x04000834 RID: 2100
	private float m_CurrentSpeed;

	// Token: 0x04000835 RID: 2101
	private double m_LastSerializeTime;

	// Token: 0x04000836 RID: 2102
	private Vector3 m_SynchronizedSpeed = Vector3.zero;

	// Token: 0x04000837 RID: 2103
	private float m_SynchronizedTurnSpeed;

	// Token: 0x04000838 RID: 2104
	private Vector3 m_NetworkPosition;

	// Token: 0x04000839 RID: 2105
	private Queue<Vector3> m_OldNetworkPositions = new Queue<Vector3>();

	// Token: 0x0400083A RID: 2106
	private bool m_UpdatedPositionAfterOnSerialize = true;
}
