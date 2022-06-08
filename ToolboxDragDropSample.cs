using System;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class ToolboxDragDropSample : SampleBase
{
	// Token: 0x06001FD5 RID: 8149 RVA: 0x000F0F1C File Offset: 0x000EF31C
	private void SetDragPlaneMode(ToolboxDragDropSample.DragPlaneMode mode)
	{
		if (mode != ToolboxDragDropSample.DragPlaneMode.XY)
		{
			if (mode != ToolboxDragDropSample.DragPlaneMode.Plane)
			{
				if (mode == ToolboxDragDropSample.DragPlaneMode.Sphere)
				{
					this.RestoreInitialPositions();
					this.dragSphere.gameObject.active = true;
					this.dragPlane.gameObject.active = false;
					this.inputMgr.dragPlaneCollider = this.dragSphere;
					this.inputMgr.dragPlaneType = TBInputManager.DragPlaneType.UseCollider;
				}
			}
			else
			{
				this.RestoreInitialPositions();
				this.dragSphere.gameObject.active = false;
				this.dragPlane.gameObject.active = true;
				this.inputMgr.dragPlaneCollider = this.dragPlane;
				this.inputMgr.dragPlaneType = TBInputManager.DragPlaneType.UseCollider;
			}
		}
		else
		{
			this.RestoreInitialPositions();
			this.dragSphere.gameObject.active = false;
			this.dragPlane.gameObject.active = false;
			this.inputMgr.dragPlaneType = TBInputManager.DragPlaneType.XY;
		}
		this.dragPlaneMode = mode;
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x000F1018 File Offset: 0x000EF418
	private void SaveInitialPositions()
	{
		this.initialPositions = new Vector3[this.dragObjects.Length];
		for (int i = 0; i < this.initialPositions.Length; i++)
		{
			this.initialPositions[i] = this.dragObjects[i].position;
		}
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x000F1070 File Offset: 0x000EF470
	private void RestoreInitialPositions()
	{
		for (int i = 0; i < this.initialPositions.Length; i++)
		{
			this.dragObjects[i].position = this.initialPositions[i];
		}
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x000F10B4 File Offset: 0x000EF4B4
	protected override string GetHelpText()
	{
		return "This sample demonstrates the use of the Toolbox's Drag & Drop scripts";
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x000F10BB File Offset: 0x000EF4BB
	protected override void Start()
	{
		base.Start();
		this.SaveInitialPositions();
		this.SetDragPlaneMode(ToolboxDragDropSample.DragPlaneMode.XY);
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x000F10D0 File Offset: 0x000EF4D0
	private void OnGUI()
	{
		if (base.UI.showHelp)
		{
			return;
		}
		SampleUI.ApplyVirtualScreen();
		ToolboxDragDropSample.DragPlaneMode dragPlaneMode = this.dragPlaneMode;
		string text;
		ToolboxDragDropSample.DragPlaneMode dragPlaneMode2;
		if (dragPlaneMode != ToolboxDragDropSample.DragPlaneMode.Plane)
		{
			if (dragPlaneMode != ToolboxDragDropSample.DragPlaneMode.Sphere)
			{
				text = "Drag On XZ";
				dragPlaneMode2 = ToolboxDragDropSample.DragPlaneMode.Plane;
			}
			else
			{
				text = "Drag On Sphere";
				dragPlaneMode2 = ToolboxDragDropSample.DragPlaneMode.XY;
			}
		}
		else
		{
			text = "Drag On Plane";
			dragPlaneMode2 = ToolboxDragDropSample.DragPlaneMode.Sphere;
		}
		if (GUI.Button(this.dragModeButtonRect, text))
		{
			this.SetDragPlaneMode(dragPlaneMode2);
		}
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x000F114C File Offset: 0x000EF54C
	private void ToggleLight()
	{
		this.pointlight.enabled = !this.pointlight.enabled;
	}

	// Token: 0x040020D9 RID: 8409
	public TBInputManager inputMgr;

	// Token: 0x040020DA RID: 8410
	public Transform[] dragObjects;

	// Token: 0x040020DB RID: 8411
	public Collider dragSphere;

	// Token: 0x040020DC RID: 8412
	public Collider dragPlane;

	// Token: 0x040020DD RID: 8413
	public Light pointlight;

	// Token: 0x040020DE RID: 8414
	private ToolboxDragDropSample.DragPlaneMode dragPlaneMode;

	// Token: 0x040020DF RID: 8415
	private Vector3[] initialPositions;

	// Token: 0x040020E0 RID: 8416
	public Rect dragModeButtonRect;

	// Token: 0x0200044B RID: 1099
	private enum DragPlaneMode
	{
		// Token: 0x040020E2 RID: 8418
		XY,
		// Token: 0x040020E3 RID: 8419
		Plane,
		// Token: 0x040020E4 RID: 8420
		Sphere
	}
}
