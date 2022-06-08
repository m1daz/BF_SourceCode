using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020006AA RID: 1706
public class ServerTimeDirector : MonoBehaviour
{
	// Token: 0x06003234 RID: 12852 RVA: 0x00163252 File Offset: 0x00161652
	private void Awake()
	{
		ServerTimeDirector.mInstance = this;
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x0016325C File Offset: 0x0016165C
	private void Start()
	{
		this.FreeCoinsunbiasedTimerEndTimestamp = this.ReadTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
		this.WriteTimestamp(this.mFreeCoinsUnbiasedTimer, this.FreeCoinsunbiasedTimerEndTimestamp);
		if ((UnbiasedTime.Instance.Now() - this.FreeCoinsunbiasedTimerEndTimestamp).TotalMinutes >= (double)this.FreeCoinsMinuesInterval)
		{
			this.WriteTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
			UIUserDataController.SetIsFreeCoins(true);
		}
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x001632DC File Offset: 0x001616DC
	private void Update()
	{
		this.mFreeCoinsUpdateIntervalTimer += (double)Time.deltaTime;
		if (this.mFreeCoinsUpdateIntervalTimer > this.FreeCoinsUpdateInterval)
		{
			this.mFreeCoinsUpdateIntervalTimer = 0.0;
			this.FreeCoinsunbiasedTimerEndTimestamp = this.ReadTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
			if ((UnbiasedTime.Instance.Now() - this.FreeCoinsunbiasedTimerEndTimestamp).TotalMinutes >= (double)this.FreeCoinsMinuesInterval)
			{
				this.WriteTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
				UIUserDataController.SetIsFreeCoins(true);
			}
		}
	}

	// Token: 0x06003237 RID: 12855 RVA: 0x00163380 File Offset: 0x00161780
	private void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			this.FreeCoinsunbiasedTimerEndTimestamp = this.ReadTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
			if ((UnbiasedTime.Instance.Now() - this.FreeCoinsunbiasedTimerEndTimestamp).TotalMinutes >= (double)this.FreeCoinsMinuesInterval)
			{
				this.WriteTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
				UIUserDataController.SetIsFreeCoins(true);
			}
		}
	}

	// Token: 0x06003238 RID: 12856 RVA: 0x001633FC File Offset: 0x001617FC
	private void OnApplicationQuit()
	{
		this.FreeCoinsunbiasedTimerEndTimestamp = this.ReadTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
		if ((UnbiasedTime.Instance.Now() - this.FreeCoinsunbiasedTimerEndTimestamp).TotalMinutes >= (double)this.FreeCoinsMinuesInterval)
		{
			this.WriteTimestamp(this.mFreeCoinsUnbiasedTimer, UnbiasedTime.Instance.Now());
			UIUserDataController.SetIsFreeCoins(true);
		}
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x0016346C File Offset: 0x0016186C
	private DateTime ReadTimestamp(string key, DateTime defaultValue)
	{
		long num = Convert.ToInt64(ObscuredPrefs.GetString(key, "0"));
		if (num == 0L)
		{
			return defaultValue;
		}
		return DateTime.FromBinary(num);
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x0016349C File Offset: 0x0016189C
	private void WriteTimestamp(string key, DateTime time)
	{
		ObscuredPrefs.SetString(key, time.ToBinary().ToString());
	}

	// Token: 0x04002EC1 RID: 11969
	public static ServerTimeDirector mInstance;

	// Token: 0x04002EC2 RID: 11970
	private DateTime FreeCoinsunbiasedTimerEndTimestamp;

	// Token: 0x04002EC3 RID: 11971
	private int FreeCoinsMinuesInterval = 30;

	// Token: 0x04002EC4 RID: 11972
	private string mFreeCoinsUnbiasedTimer = "freecoinsunbiasedTime";

	// Token: 0x04002EC5 RID: 11973
	private double mFreeCoinsUpdateIntervalTimer;

	// Token: 0x04002EC6 RID: 11974
	private double FreeCoinsUpdateInterval = 10.0;
}
