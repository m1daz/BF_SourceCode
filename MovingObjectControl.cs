using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class MovingObjectControl : MonoBehaviour
{
	// Token: 0x06000601 RID: 1537 RVA: 0x00037550 File Offset: 0x00035950
	private void Start()
	{
		if (this.ID == 1)
		{
			this.movingObjectMaxTime = 36f;
		}
		else if (this.ID == 2)
		{
			this.movingObjectMaxTime = 36f;
		}
		else if (this.ID == 3)
		{
			this.movingObjectMaxTime = 36f;
		}
		else if (this.ID == 4)
		{
			this.movingObjectMaxTime = 36f;
		}
		this.velocityX = (this.MovingEnd.position.x - this.MovingStart.position.x) / this.movingObjectMaxTime;
		this.velocityZ = (this.MovingEnd.position.z - this.MovingStart.position.z) / this.movingObjectMaxTime;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00037630 File Offset: 0x00035A30
	private void Update()
	{
		if (!this.isGetGlobalinfo)
		{
			if (!(GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo() != null))
			{
				return;
			}
			for (int i = 0; i < GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List.Count; i++)
			{
				if (this.ID == GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[i].ID)
				{
					this.tempIndex = i;
					if (!GGNetworkKit.mInstance.IsMasterClient())
					{
						base.transform.position = new Vector3(GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].X, GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].Y, GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].Z);
						this.movingObjectTime = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].curTime;
					}
				}
			}
			this.isGetGlobalinfo = true;
		}
		this.movingObjectTime += Time.deltaTime;
		if (this.movingObjectTime <= this.movingObjectMaxTime)
		{
			base.transform.position = Vector3.Slerp(base.transform.position, this.MovingEnd.position, this.trailCurve.Evaluate(this.movingObjectTime) * 0.0004f);
		}
		else if (this.movingObjectTime > this.movingObjectMaxTime && this.movingObjectTime <= this.movingObjectMaxTime + Time.deltaTime * 2f)
		{
			this.lerpIndex = 0.02f;
		}
		else if (this.movingObjectTime > this.movingObjectMaxTime && this.movingObjectTime <= this.movingObjectMaxTime * 2f)
		{
			base.transform.position = Vector3.Slerp(base.transform.position, this.MovingStart.position, this.trailCurve.Evaluate(this.movingObjectTime - this.movingObjectMaxTime) * 0.0004f);
		}
		else if (this.movingObjectTime > this.movingObjectMaxTime * 2f)
		{
			this.lerpIndex = 0.02f;
			this.movingObjectTime = 0f;
		}
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].X = base.transform.position.x;
			GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].Y = base.transform.position.y;
			GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].Z = base.transform.position.z;
			GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().mElevatorPositionCollection.vector3List[this.tempIndex].curTime = this.movingObjectTime;
		}
	}

	// Token: 0x040004CB RID: 1227
	public int ID;

	// Token: 0x040004CC RID: 1228
	private int tempIndex;

	// Token: 0x040004CD RID: 1229
	public float movingObjectTime;

	// Token: 0x040004CE RID: 1230
	private float movingObjectMaxTime;

	// Token: 0x040004CF RID: 1231
	private bool isGetGlobalinfo;

	// Token: 0x040004D0 RID: 1232
	public Transform MovingStart;

	// Token: 0x040004D1 RID: 1233
	public Transform MovingEnd;

	// Token: 0x040004D2 RID: 1234
	private float velocityX;

	// Token: 0x040004D3 RID: 1235
	private float velocityZ;

	// Token: 0x040004D4 RID: 1236
	private float lerpIndex = 0.05f;

	// Token: 0x040004D5 RID: 1237
	public AnimationCurve trailCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 2f),
		new Keyframe(2f, 4f),
		new Keyframe(4f, 8f),
		new Keyframe(6f, 10f),
		new Keyframe(8f, 10f),
		new Keyframe(10f, 14f),
		new Keyframe(12f, 18f),
		new Keyframe(14f, 20f),
		new Keyframe(16f, 20f),
		new Keyframe(18f, 20f),
		new Keyframe(20f, 20f),
		new Keyframe(22f, 20f),
		new Keyframe(24f, 20f),
		new Keyframe(26f, 30f),
		new Keyframe(28f, 40f),
		new Keyframe(30f, 50f),
		new Keyframe(32f, 60f),
		new Keyframe(34f, 80f),
		new Keyframe(36f, 100f),
		new Keyframe(38f, 120f)
	});
}
