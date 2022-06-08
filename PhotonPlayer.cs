using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class PhotonPlayer : IComparable<PhotonPlayer>, IComparable<int>, IEquatable<PhotonPlayer>, IEquatable<int>
{
	// Token: 0x060008A6 RID: 2214 RVA: 0x00043E7F File Offset: 0x0004227F
	public PhotonPlayer(bool isLocal, int actorID, string name)
	{
		this.customProperties = new Hashtable();
		this.isLocal = isLocal;
		this.actorID = actorID;
		this.nameField = name;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00043EB9 File Offset: 0x000422B9
	protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
	{
		this.customProperties = new Hashtable();
		this.isLocal = isLocal;
		this.actorID = actorID;
		this.InternalCacheProperties(properties);
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00043EF3 File Offset: 0x000422F3
	public int ID
	{
		get
		{
			return this.actorID;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00043EFB File Offset: 0x000422FB
	// (set) Token: 0x060008AA RID: 2218 RVA: 0x00043F04 File Offset: 0x00042304
	public string name
	{
		get
		{
			return this.nameField;
		}
		set
		{
			if (!this.isLocal)
			{
				Debug.LogError("Error: Cannot change the name of a remote player!");
				return;
			}
			if (string.IsNullOrEmpty(value) || value.Equals(this.nameField))
			{
				return;
			}
			this.nameField = value;
			PhotonNetwork.playerName = value;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060008AB RID: 2219 RVA: 0x00043F51 File Offset: 0x00042351
	// (set) Token: 0x060008AC RID: 2220 RVA: 0x00043F59 File Offset: 0x00042359
	public string userId { get; internal set; }

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060008AD RID: 2221 RVA: 0x00043F62 File Offset: 0x00042362
	public bool isMasterClient
	{
		get
		{
			return PhotonNetwork.networkingPeer.mMasterClientId == this.ID;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060008AE RID: 2222 RVA: 0x00043F76 File Offset: 0x00042376
	// (set) Token: 0x060008AF RID: 2223 RVA: 0x00043F7E File Offset: 0x0004237E
	public bool isInactive { get; set; }

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00043F87 File Offset: 0x00042387
	// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00043F8F File Offset: 0x0004238F
	public Hashtable customProperties { get; internal set; }

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00043F98 File Offset: 0x00042398
	public Hashtable allProperties
	{
		get
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Merge(this.customProperties);
			hashtable[byte.MaxValue] = this.name;
			return hashtable;
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x00043FD0 File Offset: 0x000423D0
	public override bool Equals(object p)
	{
		PhotonPlayer photonPlayer = p as PhotonPlayer;
		return photonPlayer != null && this.GetHashCode() == photonPlayer.GetHashCode();
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00043FFB File Offset: 0x000423FB
	public override int GetHashCode()
	{
		return this.ID;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00044003 File Offset: 0x00042403
	internal void InternalChangeLocalID(int newID)
	{
		if (!this.isLocal)
		{
			Debug.LogError("ERROR You should never change PhotonPlayer IDs!");
			return;
		}
		this.actorID = newID;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00044024 File Offset: 0x00042424
	internal void InternalCacheProperties(Hashtable properties)
	{
		if (properties == null || properties.Count == 0 || this.customProperties.Equals(properties))
		{
			return;
		}
		if (properties.ContainsKey(255))
		{
			this.nameField = (string)properties[byte.MaxValue];
		}
		if (properties.ContainsKey(253))
		{
			this.userId = (string)properties[253];
		}
		if (properties.ContainsKey(254))
		{
			this.isInactive = (bool)properties[254];
		}
		this.customProperties.MergeStringKeys(properties);
		this.customProperties.StripKeysWithNullValues();
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x000440FC File Offset: 0x000424FC
	public void SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedValues = null, bool webForward = false)
	{
		if (propertiesToSet == null)
		{
			return;
		}
		Hashtable hashtable = propertiesToSet.StripToStringKeys();
		Hashtable hashtable2 = expectedValues.StripToStringKeys();
		bool flag = hashtable2 == null || hashtable2.Count == 0;
		bool flag2 = this.actorID > 0 && !PhotonNetwork.offlineMode;
		if (flag2)
		{
			PhotonNetwork.networkingPeer.OpSetPropertiesOfActor(this.actorID, hashtable, hashtable2, webForward);
		}
		if (!flag2 || flag)
		{
			this.InternalCacheProperties(hashtable);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, new object[]
			{
				this,
				hashtable
			});
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0004418B File Offset: 0x0004258B
	public static PhotonPlayer Find(int ID)
	{
		if (PhotonNetwork.networkingPeer != null)
		{
			return PhotonNetwork.networkingPeer.GetPlayerWithId(ID);
		}
		return null;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x000441A4 File Offset: 0x000425A4
	public PhotonPlayer Get(int id)
	{
		return PhotonPlayer.Find(id);
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x000441AC File Offset: 0x000425AC
	public PhotonPlayer GetNext()
	{
		return this.GetNextFor(this.ID);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x000441BA File Offset: 0x000425BA
	public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
	{
		if (currentPlayer == null)
		{
			return null;
		}
		return this.GetNextFor(currentPlayer.ID);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x000441D0 File Offset: 0x000425D0
	public PhotonPlayer GetNextFor(int currentPlayerId)
	{
		if (PhotonNetwork.networkingPeer == null || PhotonNetwork.networkingPeer.mActors == null || PhotonNetwork.networkingPeer.mActors.Count < 2)
		{
			return null;
		}
		Dictionary<int, PhotonPlayer> mActors = PhotonNetwork.networkingPeer.mActors;
		int num = int.MaxValue;
		int num2 = currentPlayerId;
		foreach (int num3 in mActors.Keys)
		{
			if (num3 < num2)
			{
				num2 = num3;
			}
			else if (num3 > currentPlayerId && num3 < num)
			{
				num = num3;
			}
		}
		return (num == int.MaxValue) ? mActors[num2] : mActors[num];
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x000442A8 File Offset: 0x000426A8
	public int CompareTo(PhotonPlayer other)
	{
		if (other == null)
		{
			return 0;
		}
		return this.GetHashCode().CompareTo(other.GetHashCode());
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x000442D4 File Offset: 0x000426D4
	public int CompareTo(int other)
	{
		return this.GetHashCode().CompareTo(other);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x000442F0 File Offset: 0x000426F0
	public bool Equals(PhotonPlayer other)
	{
		return other != null && this.GetHashCode().Equals(other.GetHashCode());
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0004431C File Offset: 0x0004271C
	public bool Equals(int other)
	{
		return this.GetHashCode().Equals(other);
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00044338 File Offset: 0x00042738
	public override string ToString()
	{
		if (string.IsNullOrEmpty(this.name))
		{
			return string.Format("#{0:00}{1}{2}", this.ID, (!this.isInactive) ? " " : " (inactive)", (!this.isMasterClient) ? string.Empty : "(master)");
		}
		return string.Format("'{0}'{1}{2}", this.name, (!this.isInactive) ? " " : " (inactive)", (!this.isMasterClient) ? string.Empty : "(master)");
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x000443E4 File Offset: 0x000427E4
	public string ToStringFull()
	{
		return string.Format("#{0:00} '{1}'{2} {3}", new object[]
		{
			this.ID,
			this.name,
			(!this.isInactive) ? string.Empty : " (inactive)",
			this.customProperties.ToStringFull()
		});
	}

	// Token: 0x040007A4 RID: 1956
	private int actorID = -1;

	// Token: 0x040007A5 RID: 1957
	private string nameField = string.Empty;

	// Token: 0x040007A7 RID: 1959
	public readonly bool isLocal;

	// Token: 0x040007AA RID: 1962
	public object TagObject;
}
