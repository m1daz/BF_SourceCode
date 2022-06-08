using System;
using Pathfinding;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class DoorController : MonoBehaviour
{
	// Token: 0x0600031D RID: 797 RVA: 0x00018420 File Offset: 0x00016820
	public void Start()
	{
		this.bounds = base.GetComponent<Collider>().bounds;
		this.SetState(this.open);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001843F File Offset: 0x0001683F
	private void OnGUI()
	{
		if (GUILayout.Button("Toggle Door", new GUILayoutOption[0]))
		{
			this.SetState(!this.open);
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00018468 File Offset: 0x00016868
	public void SetState(bool open)
	{
		this.open = open;
		GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.bounds);
		int num = (!open) ? this.closedtag : this.opentag;
		if (num > 31)
		{
			Debug.LogError("tag > 31");
			return;
		}
		graphUpdateObject.modifyTag = true;
		graphUpdateObject.setTag = num;
		AstarPath.active.UpdateGraphs(graphUpdateObject);
		if (open)
		{
			base.GetComponent<Animation>().Play("Open");
		}
		else
		{
			base.GetComponent<Animation>().Play("Close");
		}
	}

	// Token: 0x06000320 RID: 800 RVA: 0x000184F9 File Offset: 0x000168F9
	private void Update()
	{
	}

	// Token: 0x0400028D RID: 653
	private bool open;

	// Token: 0x0400028E RID: 654
	public int opentag = 1;

	// Token: 0x0400028F RID: 655
	public int closedtag = 1;

	// Token: 0x04000290 RID: 656
	private Bounds bounds;
}
