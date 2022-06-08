using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class CubeSelect : MonoBehaviour
{
	// Token: 0x06001B46 RID: 6982 RVA: 0x000DBA34 File Offset: 0x000D9E34
	private void OnEnable()
	{
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x000DBA47 File Offset: 0x000D9E47
	private void OnDestroy()
	{
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x000DBA5A File Offset: 0x000D9E5A
	private void Start()
	{
		this.cube = null;
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x000DBA64 File Offset: 0x000D9E64
	private void On_SimpleTap(Gesture gesture)
	{
		if (gesture.pickedObject != null && gesture.pickedObject.name == "Cube")
		{
			this.ResteColor();
			this.cube = gesture.pickedObject;
			this.cube.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x000DBAC8 File Offset: 0x000D9EC8
	private void ResteColor()
	{
		if (this.cube != null)
		{
			this.cube.GetComponent<Renderer>().material.color = new Color(0.23529412f, 0.56078434f, 0.7882353f);
		}
	}

	// Token: 0x04001D75 RID: 7541
	private GameObject cube;
}
