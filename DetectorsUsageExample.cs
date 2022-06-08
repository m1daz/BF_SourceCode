using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class DetectorsUsageExample : MonoBehaviour
{
	// Token: 0x06001885 RID: 6277 RVA: 0x000CCA3E File Offset: 0x000CAE3E
	private void Start()
	{
		SpeedHackDetector.StartDetection(new Action(this.OnSpeedHackDetected), 1f, 5);
		InjectionDetector.Instance.autoDispose = true;
		InjectionDetector.Instance.keepAlive = true;
		InjectionDetector.StartDetection(new Action(this.OnInjectionDetected));
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x000CCA7E File Offset: 0x000CAE7E
	private void OnSpeedHackDetected()
	{
		this.speedHackDetected = true;
		Debug.LogWarning("Speed hack detected!");
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x000CCA91 File Offset: 0x000CAE91
	private void OnInjectionDetected()
	{
		this.injectionDetected = true;
		Debug.LogWarning("Injection detected!");
	}

	// Token: 0x04001BA9 RID: 7081
	[HideInInspector]
	public bool injectionDetected;

	// Token: 0x04001BAA RID: 7082
	[HideInInspector]
	public bool speedHackDetected;
}
