using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class TestButton : MonoBehaviour
{
	// Token: 0x060005F6 RID: 1526 RVA: 0x000370F7 File Offset: 0x000354F7
	private void Start()
	{
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x000370F9 File Offset: 0x000354F9
	private void Update()
	{
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x000370FC File Offset: 0x000354FC
	private void OnGUI()
	{
		if (GUI.Button(new Rect(400f, 100f, 50f, 50f), "blue"))
		{
			GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>().mPlayerProperties.team = GGTeamType.blue;
		}
		if (GUI.Button(new Rect(400f, 200f, 50f, 50f), "red"))
		{
			GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>().mPlayerProperties.team = GGTeamType.red;
		}
	}
}
