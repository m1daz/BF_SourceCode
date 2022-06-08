using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class GGNetWorkAIProperty : Photon.MonoBehaviour
{
	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00081949 File Offset: 0x0007FD49
	// (set) Token: 0x06000F2A RID: 3882 RVA: 0x00081951 File Offset: 0x0007FD51
	public int mBlood
	{
		get
		{
			return this._mBlood;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this._mBlood, ref this._mEncryptBlood, value);
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x0008196A File Offset: 0x0007FD6A
	private void Awake()
	{
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0008196C File Offset: 0x0007FD6C
	public void InitEncryptData()
	{
		this._mEncryptBlood = GameProtecter.mInstance.EncryptInt1(this._mBlood);
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00081984 File Offset: 0x0007FD84
	public void Start()
	{
		this.InitEncryptData();
		this.InitPlayerProperties();
		if (base.photonView.isMine)
		{
			this.mCurSkillIndex = this.mSkillIndex;
			this.mPreSkillIndex = this.mCurSkillIndex;
			this.mSkillIndexQueue.Enqueue(this.mCurSkillIndex);
		}
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000819DC File Offset: 0x0007FDDC
	private void Update()
	{
		if (base.photonView.isMine)
		{
			this.mCurSkillIndex = this.mSkillIndex;
			if (this.mPreSkillIndex != this.mCurSkillIndex)
			{
				this.mSkillIndexQueue.Enqueue(this.mCurSkillIndex);
				this.mPreSkillIndex = this.mCurSkillIndex;
			}
		}
		else
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.mPosition, Time.deltaTime * 8f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.mRotation, Time.deltaTime * 8f);
		}
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00081A98 File Offset: 0x0007FE98
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
			stream.SendNext((short)this.mBlood);
			stream.SendNext((this.mSkillIndexQueue.Count <= 0) ? this.mSkillIndex : ((int)((byte)this.mSkillIndexQueue.Dequeue())));
			stream.SendNext(GGNetworkObjectSerialize.mInstance.SerializeBinary<List<byte>>(this.mSkillStruct));
		}
		else
		{
			this.mPosition = (Vector3)stream.ReceiveNext();
			this.mRotation = (Quaternion)stream.ReceiveNext();
			this.mBlood = (int)((short)stream.ReceiveNext());
			this.mSkillIndex = (int)((byte)stream.ReceiveNext());
			this.mSkillStruct = GGNetworkObjectSerialize.mInstance.DeserializeBinary<List<byte>>((byte[])stream.ReceiveNext());
		}
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00081B9F File Offset: 0x0007FF9F
	public void InitPlayerProperties()
	{
		if (base.photonView.isMine)
		{
			this.mPlayerProperties.isDataValid = true;
		}
		else
		{
			this.mPlayerProperties.isDataValid = false;
		}
	}

	// Token: 0x040010EB RID: 4331
	public Vector3 mPosition = Vector3.zero;

	// Token: 0x040010EC RID: 4332
	public Quaternion mRotation = Quaternion.identity;

	// Token: 0x040010ED RID: 4333
	public float cameraRotationX;

	// Token: 0x040010EE RID: 4334
	public string _mEncryptBlood;

	// Token: 0x040010EF RID: 4335
	public int _mBlood = 20000;

	// Token: 0x040010F0 RID: 4336
	public int mSkillIndex;

	// Token: 0x040010F1 RID: 4337
	public List<byte> mSkillStruct = new List<byte>();

	// Token: 0x040010F2 RID: 4338
	public GGNetworkPlayerProperties mPlayerProperties = new GGNetworkPlayerProperties();

	// Token: 0x040010F3 RID: 4339
	public int mCurSkillIndex;

	// Token: 0x040010F4 RID: 4340
	public int mPreSkillIndex;

	// Token: 0x040010F5 RID: 4341
	public Queue mSkillIndexQueue = new Queue();

	// Token: 0x040010F6 RID: 4342
	public GGTeamType myTeam;
}
