using System;

// Token: 0x0200010C RID: 268
public struct PhotonMessageInfo
{
	// Token: 0x060007DB RID: 2011 RVA: 0x00040DD7 File Offset: 0x0003F1D7
	public PhotonMessageInfo(PhotonPlayer player, int timestamp, PhotonView view)
	{
		this.sender = player;
		this.timeInt = timestamp;
		this.photonView = view;
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060007DC RID: 2012 RVA: 0x00040DF0 File Offset: 0x0003F1F0
	public double timestamp
	{
		get
		{
			uint num = (uint)this.timeInt;
			double num2 = num;
			return num2 / 1000.0;
		}
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00040E13 File Offset: 0x0003F213
	public override string ToString()
	{
		return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", this.timestamp, this.sender);
	}

	// Token: 0x0400075A RID: 1882
	private readonly int timeInt;

	// Token: 0x0400075B RID: 1883
	public readonly PhotonPlayer sender;

	// Token: 0x0400075C RID: 1884
	public readonly PhotonView photonView;
}
