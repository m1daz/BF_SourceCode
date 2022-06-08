using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class PhotonStreamQueue
{
	// Token: 0x060008C8 RID: 2248 RVA: 0x0004487B File Offset: 0x00042C7B
	public PhotonStreamQueue(int sampleRate)
	{
		this.m_SampleRate = sampleRate;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x000448B8 File Offset: 0x00042CB8
	private void BeginWritePackage()
	{
		if (Time.realtimeSinceStartup < this.m_LastSampleTime + 1f / (float)this.m_SampleRate)
		{
			this.m_IsWriting = false;
			return;
		}
		if (this.m_SampleCount == 1)
		{
			this.m_ObjectsPerSample = this.m_Objects.Count;
		}
		else if (this.m_SampleCount > 1 && this.m_Objects.Count / this.m_SampleCount != this.m_ObjectsPerSample)
		{
			Debug.LogWarning("The number of objects sent via a PhotonStreamQueue has to be the same each frame");
			Debug.LogWarning(string.Concat(new object[]
			{
				"Objects in List: ",
				this.m_Objects.Count,
				" / Sample Count: ",
				this.m_SampleCount,
				" = ",
				this.m_Objects.Count / this.m_SampleCount,
				" != ",
				this.m_ObjectsPerSample
			}));
		}
		this.m_IsWriting = true;
		this.m_SampleCount++;
		this.m_LastSampleTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x000449D9 File Offset: 0x00042DD9
	public void Reset()
	{
		this.m_SampleCount = 0;
		this.m_ObjectsPerSample = -1;
		this.m_LastSampleTime = float.NegativeInfinity;
		this.m_LastFrameCount = -1;
		this.m_Objects.Clear();
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00044A06 File Offset: 0x00042E06
	public void SendNext(object obj)
	{
		if (Time.frameCount != this.m_LastFrameCount)
		{
			this.BeginWritePackage();
		}
		this.m_LastFrameCount = Time.frameCount;
		if (!this.m_IsWriting)
		{
			return;
		}
		this.m_Objects.Add(obj);
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00044A41 File Offset: 0x00042E41
	public bool HasQueuedObjects()
	{
		return this.m_NextObjectIndex != -1;
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00044A50 File Offset: 0x00042E50
	public object ReceiveNext()
	{
		if (this.m_NextObjectIndex == -1)
		{
			return null;
		}
		if (this.m_NextObjectIndex >= this.m_Objects.Count)
		{
			this.m_NextObjectIndex -= this.m_ObjectsPerSample;
		}
		return this.m_Objects[this.m_NextObjectIndex++];
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00044AB0 File Offset: 0x00042EB0
	public void Serialize(PhotonStream stream)
	{
		if (this.m_Objects.Count > 0 && this.m_ObjectsPerSample < 0)
		{
			this.m_ObjectsPerSample = this.m_Objects.Count;
		}
		stream.SendNext(this.m_SampleCount);
		stream.SendNext(this.m_ObjectsPerSample);
		for (int i = 0; i < this.m_Objects.Count; i++)
		{
			stream.SendNext(this.m_Objects[i]);
		}
		this.m_Objects.Clear();
		this.m_SampleCount = 0;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00044B50 File Offset: 0x00042F50
	public void Deserialize(PhotonStream stream)
	{
		this.m_Objects.Clear();
		this.m_SampleCount = (int)stream.ReceiveNext();
		this.m_ObjectsPerSample = (int)stream.ReceiveNext();
		for (int i = 0; i < this.m_SampleCount * this.m_ObjectsPerSample; i++)
		{
			this.m_Objects.Add(stream.ReceiveNext());
		}
		if (this.m_Objects.Count > 0)
		{
			this.m_NextObjectIndex = 0;
		}
		else
		{
			this.m_NextObjectIndex = -1;
		}
	}

	// Token: 0x040007B2 RID: 1970
	private int m_SampleRate;

	// Token: 0x040007B3 RID: 1971
	private int m_SampleCount;

	// Token: 0x040007B4 RID: 1972
	private int m_ObjectsPerSample = -1;

	// Token: 0x040007B5 RID: 1973
	private float m_LastSampleTime = float.NegativeInfinity;

	// Token: 0x040007B6 RID: 1974
	private int m_LastFrameCount = -1;

	// Token: 0x040007B7 RID: 1975
	private int m_NextObjectIndex = -1;

	// Token: 0x040007B8 RID: 1976
	private List<object> m_Objects = new List<object>();

	// Token: 0x040007B9 RID: 1977
	private bool m_IsWriting;
}
