using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class RTS_NewSyntaxe : MonoBehaviour
{
	// Token: 0x06001C0F RID: 7183 RVA: 0x000DDFB1 File Offset: 0x000DC3B1
	private void Start()
	{
		this.cube = null;
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x000DDFBC File Offset: 0x000DC3BC
	private void Update()
	{
		Gesture current = EasyTouch.current;
		if (current.type == EasyTouch.EvtType.On_SimpleTap && current.pickedObject != null && current.pickedObject.name == "Cube")
		{
			this.ResteColor();
			this.cube = current.pickedObject;
			this.cube.GetComponent<Renderer>().material.color = Color.red;
			base.transform.Translate(Vector2.up, Space.World);
		}
		if (current.type == EasyTouch.EvtType.On_Swipe && current.touchCount == 1)
		{
			base.transform.Translate(Vector3.left * current.deltaPosition.x / (float)Screen.width);
			base.transform.Translate(Vector3.back * current.deltaPosition.y / (float)Screen.height);
		}
		if (current.type == EasyTouch.EvtType.On_Pinch)
		{
			Camera.main.fieldOfView += current.deltaPinch * 10f * Time.deltaTime;
		}
		if (current.type == EasyTouch.EvtType.On_Twist)
		{
			base.transform.Rotate(Vector3.up * current.twistAngle);
		}
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x000DE10F File Offset: 0x000DC50F
	private void ResteColor()
	{
		if (this.cube != null)
		{
			this.cube.GetComponent<Renderer>().material.color = new Color(0.23529412f, 0.56078434f, 0.7882353f);
		}
	}

	// Token: 0x04001DA0 RID: 7584
	private GameObject cube;
}
