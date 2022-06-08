using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class PhotonStream
{
	// Token: 0x060007DF RID: 2015 RVA: 0x00040E38 File Offset: 0x0003F238
	public PhotonStream(bool write, object[] incomingData)
	{
		this.write = write;
		if (incomingData == null)
		{
			this.writeData = new Queue<object>(10);
		}
		else
		{
			this.readData = incomingData;
		}
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00040E66 File Offset: 0x0003F266
	public void SetReadStream(object[] incomingData, byte pos = 0)
	{
		this.readData = incomingData;
		this.currentItem = pos;
		this.write = false;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00040E7D File Offset: 0x0003F27D
	internal void ResetWriteStream()
	{
		this.writeData.Clear();
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00040E8A File Offset: 0x0003F28A
	public bool isWriting
	{
		get
		{
			return this.write;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00040E92 File Offset: 0x0003F292
	public bool isReading
	{
		get
		{
			return !this.write;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00040E9D File Offset: 0x0003F29D
	public int Count
	{
		get
		{
			return (!this.isWriting) ? this.readData.Length : this.writeData.Count;
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00040EC4 File Offset: 0x0003F2C4
	public object ReceiveNext()
	{
		if (this.write)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		object result = this.readData[(int)this.currentItem];
		this.currentItem += 1;
		return result;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00040F08 File Offset: 0x0003F308
	public object PeekNext()
	{
		if (this.write)
		{
			Debug.LogError("Error: you cannot read this stream that you are writing!");
			return null;
		}
		return this.readData[(int)this.currentItem];
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00040F3B File Offset: 0x0003F33B
	public void SendNext(object obj)
	{
		if (!this.write)
		{
			Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
			return;
		}
		this.writeData.Enqueue(obj);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00040F5F File Offset: 0x0003F35F
	public object[] ToArray()
	{
		return (!this.isWriting) ? this.readData : this.writeData.ToArray();
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00040F84 File Offset: 0x0003F384
	public void Serialize(ref bool myBool)
	{
		if (this.write)
		{
			this.writeData.Enqueue(myBool);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			myBool = (bool)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00040FEC File Offset: 0x0003F3EC
	public void Serialize(ref int myInt)
	{
		if (this.write)
		{
			this.writeData.Enqueue(myInt);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			myInt = (int)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00041054 File Offset: 0x0003F454
	public void Serialize(ref string value)
	{
		if (this.write)
		{
			this.writeData.Enqueue(value);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			value = (string)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x000410B4 File Offset: 0x0003F4B4
	public void Serialize(ref char value)
	{
		if (this.write)
		{
			this.writeData.Enqueue(value);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			value = (char)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0004111C File Offset: 0x0003F51C
	public void Serialize(ref short value)
	{
		if (this.write)
		{
			this.writeData.Enqueue(value);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			value = (short)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00041184 File Offset: 0x0003F584
	public void Serialize(ref float obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			obj = (float)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000411EC File Offset: 0x0003F5EC
	public void Serialize(ref PhotonPlayer obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			obj = (PhotonPlayer)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0004124C File Offset: 0x0003F64C
	public void Serialize(ref Vector3 obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			obj = (Vector3)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x000412BC File Offset: 0x0003F6BC
	public void Serialize(ref Vector2 obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			obj = (Vector2)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0004132C File Offset: 0x0003F72C
	public void Serialize(ref Quaternion obj)
	{
		if (this.write)
		{
			this.writeData.Enqueue(obj);
		}
		else if (this.readData.Length > (int)this.currentItem)
		{
			obj = (Quaternion)this.readData[(int)this.currentItem];
			this.currentItem += 1;
		}
	}

	// Token: 0x04000769 RID: 1897
	private bool write;

	// Token: 0x0400076A RID: 1898
	private Queue<object> writeData;

	// Token: 0x0400076B RID: 1899
	private object[] readData;

	// Token: 0x0400076C RID: 1900
	internal byte currentItem;
}
