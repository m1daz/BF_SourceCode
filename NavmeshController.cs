using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class NavmeshController : MonoBehaviour
{
	// Token: 0x06000014 RID: 20 RVA: 0x00002A15 File Offset: 0x00000E15
	public void Start()
	{
		AstarPath.OnAwakeSettings = (OnVoidDelegate)Delegate.Combine(AstarPath.OnAwakeSettings, new OnVoidDelegate(this.OnAstarAwake));
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002A37 File Offset: 0x00000E37
	private void OnAstarAwake()
	{
		AstarPath.OnLatePostScan = (OnScanDelegate)Delegate.Combine(AstarPath.OnLatePostScan, new OnScanDelegate(this.OnRescan));
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002A59 File Offset: 0x00000E59
	private void OnDisable()
	{
		AstarPath.OnAwakeSettings = (OnVoidDelegate)Delegate.Remove(AstarPath.OnAwakeSettings, new OnVoidDelegate(this.OnAstarAwake));
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002A7B File Offset: 0x00000E7B
	private void OnRescan(AstarPath active)
	{
		this.Teleport();
		Debug.LogWarning("On Rescan");
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002A8D File Offset: 0x00000E8D
	public void Teleport()
	{
		this.prevNode = null;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002A98 File Offset: 0x00000E98
	public Vector3 SimpleMove(Vector3 currentPosition, Vector3 direction)
	{
		this.forwardPlanning = ((this.forwardPlanning >= 0.01f) ? this.forwardPlanning : 0.01f);
		if (this.controller == null)
		{
			this.controller = base.GetComponent<CharacterController>();
		}
		if (this.controller == null)
		{
			Debug.LogError("No CharacterController is attached to the GameObject");
			return direction;
		}
		direction = this.ClampMove(currentPosition, direction);
		this.controller.SimpleMove(direction);
		return direction;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002B20 File Offset: 0x00000F20
	public Vector3 ClampMove(Vector3 currentPosition, Vector3 direction)
	{
		this.forwardPlanning = ((this.forwardPlanning >= 0.01f) ? this.forwardPlanning : 0.01f);
		Vector3 vector = currentPosition + direction * this.forwardPlanning;
		vector = this.ClampToNavmesh(vector);
		direction = (vector - currentPosition) * (1f / this.forwardPlanning);
		return direction;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002B8C File Offset: 0x00000F8C
	public Vector3 ClampToNavmesh(Vector3 target)
	{
		if (this.prevNode == null)
		{
			this.prevNode = AstarPath.active.GetNearest(base.transform.position).node;
			this.prevPos = base.transform.position;
		}
		Vector3 result;
		this.prevNode = this.ClampAlongNavmesh(this.prevPos, this.prevNode, target, out result);
		this.prevPos = result;
		return result;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002BFC File Offset: 0x00000FFC
	public Node ClampAlongNavmesh(Vector3 startPos, Node startNode, Vector3 endPos, out Vector3 clampedPos)
	{
		clampedPos = endPos;
		Stack<Node> stack = this.tmpStack;
		List<Node> list = this.tmpClosed;
		stack.Clear();
		list.Clear();
		float num = float.PositiveInfinity;
		Node result = null;
		Vector3 vector = (startPos + endPos) / 2f;
		float num2 = Mathfx.MagnitudeXZ(startPos, endPos) / 2f;
		Vector3 vector2 = startPos;
		stack.Push(startNode);
		list.Add(startNode);
		INavmesh navmesh = AstarData.GetGraph(startNode) as INavmesh;
		if (navmesh == null)
		{
			return startNode;
		}
		while (stack.Count > 0)
		{
			Node node = stack.Pop();
			MeshNode meshNode = node as MeshNode;
			if (NavMeshGraph.ContainsPoint(meshNode, endPos, navmesh.vertices))
			{
				result = node;
				vector2 = endPos;
				break;
			}
			int i = 0;
			int i2 = 2;
			while (i < 3)
			{
				int vertexIndex = meshNode.GetVertexIndex(i2);
				int vertexIndex2 = meshNode.GetVertexIndex(i);
				bool flag = true;
				MeshNode meshNode2 = null;
				for (int j = 0; j < node.connections.Length; j++)
				{
					meshNode2 = (node.connections[j] as MeshNode);
					if (meshNode2 != null)
					{
						int k = 0;
						int i3 = 2;
						while (k < 3)
						{
							int vertexIndex3 = meshNode2.GetVertexIndex(i3);
							int vertexIndex4 = meshNode2.GetVertexIndex(k);
							if ((vertexIndex3 == vertexIndex && vertexIndex4 == vertexIndex2) || (vertexIndex3 == vertexIndex2 && vertexIndex4 == vertexIndex))
							{
								flag = false;
								break;
							}
							i3 = k++;
						}
						if (!flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					Vector3 vector3 = Mathfx.NearestPointStrictXZ((Vector3)navmesh.vertices[vertexIndex], (Vector3)navmesh.vertices[vertexIndex2], endPos);
					float num3 = Mathfx.MagnitudeXZ(vector3, endPos);
					if (num3 < num)
					{
						vector2 = vector3;
						num = num3;
						result = node;
					}
				}
				else if (!list.Contains(meshNode2))
				{
					list.Add(meshNode2);
					Vector3 vector3 = Mathfx.NearestPointStrictXZ((Vector3)navmesh.vertices[vertexIndex], (Vector3)navmesh.vertices[vertexIndex2], vector);
					float num3 = Mathfx.MagnitudeXZ(vector3, vector);
					if (num3 <= num2)
					{
						stack.Push(meshNode2);
					}
				}
				i2 = i++;
			}
		}
		clampedPos = vector2;
		return result;
	}

	// Token: 0x0400001D RID: 29
	public float forwardPlanning;

	// Token: 0x0400001E RID: 30
	protected Vector3 prevPos;

	// Token: 0x0400001F RID: 31
	protected Node prevNode;

	// Token: 0x04000020 RID: 32
	protected CharacterController controller;

	// Token: 0x04000021 RID: 33
	private Stack<Node> tmpStack = new Stack<Node>(16);

	// Token: 0x04000022 RID: 34
	private List<Node> tmpClosed = new List<Node>(32);
}
