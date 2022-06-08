using System;
using UnityEngine;

// Token: 0x0200043B RID: 1083
public class FingerEventsSamplePart1 : SampleBase
{
	// Token: 0x06001F6C RID: 8044 RVA: 0x000EF5FD File Offset: 0x000ED9FD
	protected override string GetHelpText()
	{
		return "This sample lets you visualize and understand the FingerDown, FingerStationary and FingerUp events.\r\n\r\nINSTRUCTIONS:\r\n- Press, hold and release the red and blue spheres\r\n- Press & hold the green sphere without moving for a few seconds";
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x000EF604 File Offset: 0x000EDA04
	protected override void Start()
	{
		base.Start();
		if (this.fingerStationaryObject)
		{
			this.stationaryParticleEmitter = this.fingerStationaryObject.GetComponentInChildren<ParticleEmitter>();
		}
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x000EF62D File Offset: 0x000EDA2D
	private void StopStationaryParticleEmitter()
	{
		this.stationaryParticleEmitter.emit = false;
		base.UI.StatusText = string.Empty;
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x000EF64C File Offset: 0x000EDA4C
	private void OnEnable()
	{
		Debug.Log("Registering finger gesture events from C# script");
		FingerGestures.OnFingerDown += this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerUp += this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerStationaryBegin += this.FingerGestures_OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary += this.FingerGestures_OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd += this.FingerGestures_OnFingerStationaryEnd;
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x000EF6B8 File Offset: 0x000EDAB8
	private void OnDisable()
	{
		FingerGestures.OnFingerDown -= this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerUp -= this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerStationaryBegin -= this.FingerGestures_OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary -= this.FingerGestures_OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd -= this.FingerGestures_OnFingerStationaryEnd;
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x000EF71A File Offset: 0x000EDB1A
	private void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
		this.CheckSpawnParticles(fingerPos, this.fingerDownObject);
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x000EF72C File Offset: 0x000EDB2C
	private void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
		this.CheckSpawnParticles(fingerPos, this.fingerUpObject);
		FingerGestures.Finger finger = FingerGestures.GetFinger(fingerIndex);
		Debug.Log(string.Concat(new object[]
		{
			"Finger was lifted up at ",
			finger.Position,
			" and moved ",
			finger.DistanceFromStart.ToString("N0"),
			" pixels from its initial position at ",
			finger.StartPosition
		}));
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x000EF7A8 File Offset: 0x000EDBA8
	private void FingerGestures_OnFingerStationaryBegin(int fingerIndex, Vector2 fingerPos)
	{
		if (this.stationaryFingerIndex != -1)
		{
			return;
		}
		GameObject gameObject = SampleBase.PickObject(fingerPos);
		if (gameObject == this.fingerStationaryObject)
		{
			base.UI.StatusText = "Begin stationary on finger " + fingerIndex;
			this.stationaryFingerIndex = fingerIndex;
			this.originalMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;
			gameObject.GetComponent<Renderer>().sharedMaterial = this.stationaryMaterial;
		}
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x000EF820 File Offset: 0x000EDC20
	private void FingerGestures_OnFingerStationary(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
		if (elapsedTime < this.chargeDelay)
		{
			return;
		}
		GameObject x = SampleBase.PickObject(fingerPos);
		if (x == this.fingerStationaryObject)
		{
			float num = Mathf.Clamp01((elapsedTime - this.chargeDelay) / this.chargeTime);
			float num2 = Mathf.Lerp(this.minSationaryParticleEmissionCount, this.maxSationaryParticleEmissionCount, num);
			this.stationaryParticleEmitter.minEmission = num2;
			this.stationaryParticleEmitter.maxEmission = num2;
			this.stationaryParticleEmitter.emit = true;
			base.UI.StatusText = "Charge: " + (100f * num).ToString("N1") + "%";
		}
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x000EF8CC File Offset: 0x000EDCCC
	private void FingerGestures_OnFingerStationaryEnd(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
		if (fingerIndex == this.stationaryFingerIndex)
		{
			base.UI.StatusText = string.Concat(new object[]
			{
				"Stationary ended on finger ",
				fingerIndex,
				" - ",
				elapsedTime.ToString("N1"),
				" seconds elapsed"
			});
			this.StopStationaryParticleEmitter();
			this.fingerStationaryObject.GetComponent<Renderer>().sharedMaterial = this.originalMaterial;
			this.stationaryFingerIndex = -1;
		}
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x000EF950 File Offset: 0x000EDD50
	private bool CheckSpawnParticles(Vector2 fingerPos, GameObject requiredObject)
	{
		GameObject gameObject = SampleBase.PickObject(fingerPos);
		if (!gameObject || gameObject != requiredObject)
		{
			return false;
		}
		this.SpawnParticles(gameObject);
		return true;
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x000EF988 File Offset: 0x000EDD88
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x04002084 RID: 8324
	public GameObject fingerDownObject;

	// Token: 0x04002085 RID: 8325
	public GameObject fingerStationaryObject;

	// Token: 0x04002086 RID: 8326
	public GameObject fingerUpObject;

	// Token: 0x04002087 RID: 8327
	public float chargeDelay = 0.5f;

	// Token: 0x04002088 RID: 8328
	public float chargeTime = 5f;

	// Token: 0x04002089 RID: 8329
	public float minSationaryParticleEmissionCount = 5f;

	// Token: 0x0400208A RID: 8330
	public float maxSationaryParticleEmissionCount = 50f;

	// Token: 0x0400208B RID: 8331
	public Material stationaryMaterial;

	// Token: 0x0400208C RID: 8332
	public int requiredTapCount = 2;

	// Token: 0x0400208D RID: 8333
	private ParticleEmitter stationaryParticleEmitter;

	// Token: 0x0400208E RID: 8334
	private int stationaryFingerIndex = -1;

	// Token: 0x0400208F RID: 8335
	private Material originalMaterial;
}
