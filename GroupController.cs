using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class GroupController : MonoBehaviour
{
	// Token: 0x060002EF RID: 751 RVA: 0x000156E1 File Offset: 0x00013AE1
	public void Start()
	{
		this.cam = Camera.main;
		this.sim = (UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator).GetSimulator();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00015710 File Offset: 0x00013B10
	public void Update()
	{
		if (Screen.fullScreen && Screen.width != Screen.resolutions[Screen.resolutions.Length - 1].width)
		{
			Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
		}
		if (this.adjustCamera)
		{
			List<Agent> agents = this.sim.GetAgents();
			float num = 0f;
			for (int i = 0; i < agents.Count; i++)
			{
				float num2 = Mathf.Max(Mathf.Abs(agents[i].Position.x), Mathf.Abs(agents[i].Position.z));
				if (num2 > num)
				{
					num = num2;
				}
			}
			float a = num / Mathf.Tan(this.cam.fieldOfView * 0.017453292f / 2f);
			float b = num / Mathf.Tan(Mathf.Atan(Mathf.Tan(this.cam.fieldOfView * 0.017453292f / 2f) * this.cam.aspect));
			this.cam.transform.position = Vector3.Lerp(this.cam.transform.position, new Vector3(0f, Mathf.Max(a, b) * 1.1f, 0f), Time.smoothDeltaTime * 2f);
		}
		if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Mouse0))
		{
			this.Order();
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x000158BC File Offset: 0x00013CBC
	private void OnGUI()
	{
		if (Event.current.type == EventType.MouseUp && Event.current.button == 0 && !Input.GetKey(KeyCode.A))
		{
			this.Select(this.start, this.end);
			this.wasDown = false;
		}
		if (Event.current.type == EventType.MouseDrag && Event.current.button == 0)
		{
			this.end = Event.current.mousePosition;
			if (!this.wasDown)
			{
				this.start = this.end;
				this.wasDown = true;
			}
		}
		if (Input.GetKey(KeyCode.A))
		{
			this.wasDown = false;
		}
		if (this.wasDown)
		{
			Rect position = Rect.MinMaxRect(Mathf.Min(this.start.x, this.end.x), Mathf.Min(this.start.y, this.end.y), Mathf.Max(this.start.x, this.end.x), Mathf.Max(this.start.y, this.end.y));
			if (position.width > 4f && position.height > 4f)
			{
				GUI.Box(position, string.Empty, this.selectionBox);
			}
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00015A20 File Offset: 0x00013E20
	public void Order()
	{
		Ray ray = this.cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			float num = 0f;
			for (int i = 0; i < this.selection.Count; i++)
			{
				num += this.selection[i].GetComponent<RVOController>().radius;
			}
			float num2 = num / 3.1415927f;
			num2 *= 2f;
			for (int j = 0; j < this.selection.Count; j++)
			{
				float num3 = 6.2831855f * (float)j / (float)this.selection.Count;
				Vector3 target = raycastHit.point + new Vector3(Mathf.Cos(num3), 0f, Mathf.Sin(num3)) * num2;
				this.selection[j].SetTarget(target);
				this.selection[j].SetColor(this.GetColor(num3));
				this.selection[j].RecalculatePath();
			}
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00015B40 File Offset: 0x00013F40
	public void Select(Vector2 _start, Vector2 _end)
	{
		_start.y = (float)Screen.height - _start.y;
		_end.y = (float)Screen.height - _end.y;
		Vector2 b = Vector2.Min(_start, _end);
		Vector2 a = Vector2.Max(_start, _end);
		if ((a - b).sqrMagnitude < 16f)
		{
			return;
		}
		this.selection.Clear();
		RVOExampleAgent[] array = UnityEngine.Object.FindObjectsOfType(typeof(RVOExampleAgent)) as RVOExampleAgent[];
		for (int i = 0; i < array.Length; i++)
		{
			Vector2 vector = this.cam.WorldToScreenPoint(array[i].transform.position);
			if (vector.x > b.x && vector.y > b.y && vector.x < a.x && vector.y < a.y)
			{
				this.selection.Add(array[i]);
			}
		}
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00015C54 File Offset: 0x00014054
	public Color GetColor(float angle)
	{
		return GroupController.HSVToRGB(angle * 57.295776f, 1f, 1f);
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00015C6C File Offset: 0x0001406C
	private static Color HSVToRGB(float h, float s, float v)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = s * v;
		float num5 = h / 60f;
		float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
		if (num5 < 1f)
		{
			num = num4;
			num2 = num6;
		}
		else if (num5 < 2f)
		{
			num = num6;
			num2 = num4;
		}
		else if (num5 < 3f)
		{
			num2 = num4;
			num3 = num6;
		}
		else if (num5 < 4f)
		{
			num2 = num6;
			num3 = num4;
		}
		else if (num5 < 5f)
		{
			num = num6;
			num3 = num4;
		}
		else if (num5 < 6f)
		{
			num = num4;
			num3 = num6;
		}
		float num7 = v - num4;
		num += num7;
		num2 += num7;
		num3 += num7;
		return new Color(num, num2, num3);
	}

	// Token: 0x0400023B RID: 571
	public GUIStyle selectionBox;

	// Token: 0x0400023C RID: 572
	public bool adjustCamera = true;

	// Token: 0x0400023D RID: 573
	private Vector2 start;

	// Token: 0x0400023E RID: 574
	private Vector2 end;

	// Token: 0x0400023F RID: 575
	private bool wasDown;

	// Token: 0x04000240 RID: 576
	private List<RVOExampleAgent> selection = new List<RVOExampleAgent>();

	// Token: 0x04000241 RID: 577
	private Simulator sim;

	// Token: 0x04000242 RID: 578
	private Camera cam;

	// Token: 0x04000243 RID: 579
	private const float rad2Deg = 57.295776f;
}
