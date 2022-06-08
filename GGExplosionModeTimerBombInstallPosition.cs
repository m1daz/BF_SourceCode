using System;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class GGExplosionModeTimerBombInstallPosition : MonoBehaviour
{
	// Token: 0x0600104A RID: 4170 RVA: 0x0008B2A0 File Offset: 0x000896A0
	private void Start()
	{
		this.MainPlayer = GGNetworkKit.mInstance.GetMainPlayer();
		if (this.MainPlayer != null)
		{
			this.mNetWorkPlayerlogic = this.MainPlayer.GetComponent<GGNetWorkPlayerlogic>();
			this.mGGNetworkCharacter = this.MainPlayer.GetComponent<GGNetworkCharacter>();
		}
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x0008B2F0 File Offset: 0x000896F0
	private void Update()
	{
		if (this.MainPlayer == null)
		{
			if (Time.frameCount % 32 == 0)
			{
				this.MainPlayer = GameObject.FindWithTag("Player");
				if (this.MainPlayer != null)
				{
					this.mNetWorkPlayerlogic = this.MainPlayer.GetComponent<GGNetWorkPlayerlogic>();
					this.mGGNetworkCharacter = this.MainPlayer.GetComponent<GGNetworkCharacter>();
				}
			}
			return;
		}
		if (this.mGGNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
		{
			if (base.GetComponent<Renderer>().material.color != Color.green)
			{
				base.GetComponent<Renderer>().material.color = Color.green;
			}
		}
		else if (base.GetComponent<Renderer>().material.color != Color.red)
		{
			base.GetComponent<Renderer>().material.color = Color.red;
		}
		if (!this.mGGNetworkCharacter.mPlayerProperties.isObserver)
		{
			base.transform.LookAt(this.MainPlayer.transform, Vector3.up);
			base.transform.localEulerAngles = new Vector3(90f, base.transform.localEulerAngles.y, base.transform.localEulerAngles.z);
		}
		else if (this.mNetWorkPlayerlogic.ObserverCamera != null)
		{
			base.transform.LookAt(this.mNetWorkPlayerlogic.ObserverCamera.transform, Vector3.up);
			base.transform.localEulerAngles = new Vector3(90f, base.transform.localEulerAngles.y, base.transform.localEulerAngles.z);
		}
		this.floatingTimeCount += Time.deltaTime;
		if (this.floatingTimeCount < 2f)
		{
			if (this.floatingTimeCount < 1f)
			{
				base.transform.localPosition += new Vector3(0f, 0.3f * Time.deltaTime, 0f);
				base.transform.localPosition = Vector3.Min(base.transform.localPosition, new Vector3(0f, 2.3f, 0f));
			}
			else if (this.floatingTimeCount > 1f)
			{
				base.transform.localPosition -= new Vector3(0f, 0.3f * Time.deltaTime, 0f);
				base.transform.localPosition = Vector3.Max(base.transform.localPosition, new Vector3(0f, 1.7f, 0f));
			}
		}
		else
		{
			this.floatingTimeCount = 0f;
		}
	}

	// Token: 0x04001259 RID: 4697
	private GameObject MainPlayer;

	// Token: 0x0400125A RID: 4698
	private GGNetWorkPlayerlogic mNetWorkPlayerlogic;

	// Token: 0x0400125B RID: 4699
	private GGNetworkCharacter mGGNetworkCharacter;

	// Token: 0x0400125C RID: 4700
	private float floatingTimeCount;

	// Token: 0x0400125D RID: 4701
	public Texture[] PositionIconTexture;
}
