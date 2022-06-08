using System;
using UnityEngine;

// Token: 0x020004EB RID: 1259
public class GGNetWorkPlayerGenerate : MonoBehaviour
{
	// Token: 0x0600237A RID: 9082 RVA: 0x0010F89E File Offset: 0x0010DC9E
	private void Awake()
	{
		GGNetworkKit.mInstance.CreatePlayer("ExampleCharacter", new Vector3(0f, -500f, 0f));
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x0010F8C4 File Offset: 0x0010DCC4
	private void Start()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.bullet, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.RPG, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.bomb1, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.bomb2, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.bomb3, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.bomb4, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.bomb5, new Vector3(0f, -4000f, 0f), new Quaternion(0f, 0f, 0f, 0f));
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x0010FA60 File Offset: 0x0010DE60
	private void Update()
	{
	}

	// Token: 0x04002430 RID: 9264
	public GameObject bullet;

	// Token: 0x04002431 RID: 9265
	public GameObject RPG;

	// Token: 0x04002432 RID: 9266
	public GameObject bomb1;

	// Token: 0x04002433 RID: 9267
	public GameObject bomb2;

	// Token: 0x04002434 RID: 9268
	public GameObject bomb3;

	// Token: 0x04002435 RID: 9269
	public GameObject bomb4;

	// Token: 0x04002436 RID: 9270
	public GameObject bomb5;
}
