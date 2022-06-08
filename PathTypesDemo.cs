using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class PathTypesDemo : MonoBehaviour
{
	// Token: 0x0600030A RID: 778 RVA: 0x00017058 File Offset: 0x00015458
	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 vector = ray.origin + ray.direction * (ray.origin.y / -ray.direction.y);
		this.end.position = vector;
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
		{
			this.multipoints.Add(vector);
		}
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
		{
			this.multipoints.Clear();
		}
		if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > 225f)
		{
			this.DemoPath();
		}
		if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl) && this.lastPath.IsDone())
		{
			this.DemoPath();
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00017160 File Offset: 0x00015560
	public void OnGUI()
	{
		GUILayout.BeginArea(new Rect(5f, 5f, 220f, (float)(Screen.height - 10)), string.Empty, "Box");
		switch (this.activeDemo)
		{
		case 0:
			GUILayout.Label("Basic path. Finds a path from point A to point B.", new GUILayoutOption[0]);
			break;
		case 1:
			GUILayout.Label("Multi Target Path. Finds a path quickly from one point to many others in a single search.", new GUILayoutOption[0]);
			break;
		case 2:
			GUILayout.Label("Randomized Path. Finds a path with a specified length in a random direction or biased towards some point when using a larger aim strenggth.", new GUILayoutOption[0]);
			break;
		case 3:
			GUILayout.Label("Flee Path. Tries to flee from a specified point. Remember to set Flee Strength!", new GUILayoutOption[0]);
			break;
		case 4:
			GUILayout.Label("Finds all nodes which it costs less than some value to reach.", new GUILayoutOption[0]);
			break;
		case 5:
			GUILayout.Label("Searches the whole graph from a specific point. FloodPathTracer can then be used to quickly find a path to that point", new GUILayoutOption[0]);
			break;
		case 6:
			GUILayout.Label("Traces a path to where the FloodPath started. Compare the claculation times for this path with ABPath!\nGreat for TD games", new GUILayoutOption[0]);
			break;
		}
		GUILayout.Space(5f);
		GUILayout.Label("Note that the paths are rendered without ANY post-processing applied, so they might look a bit edgy", new GUILayoutOption[0]);
		GUILayout.Space(5f);
		GUILayout.Label("Click anywhere to recalculate the path. Hold Ctrl to continuously recalculate the path while the mouse is pressed.", new GUILayoutOption[0]);
		if (this.activeDemo == 2 || this.activeDemo == 3 || this.activeDemo == 4)
		{
			GUILayout.Label("Search Distance (" + this.searchLength + ")", new GUILayoutOption[0]);
			this.searchLength = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)this.searchLength, 0f, 100000f, new GUILayoutOption[0]));
		}
		if (this.activeDemo == 2 || this.activeDemo == 3)
		{
			GUILayout.Label("Spread (" + this.spread + ")", new GUILayoutOption[0]);
			this.spread = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)this.spread, 0f, 40000f, new GUILayoutOption[0]));
			GUILayout.Label("Replace Chance (" + this.replaceChance + ")", new GUILayoutOption[0]);
			this.replaceChance = GUILayout.HorizontalSlider(this.replaceChance, 0f, 1f, new GUILayoutOption[0]);
			GUILayout.Label(string.Concat(new object[]
			{
				(this.activeDemo != 2) ? "Flee strength" : "Aim strength",
				" (",
				this.aimStrength,
				")"
			}), new GUILayoutOption[0]);
			this.aimStrength = GUILayout.HorizontalSlider(this.aimStrength, 0f, 1f, new GUILayoutOption[0]);
		}
		if (this.activeDemo == 1)
		{
			GUILayout.Label("Hold shift and click to add new target points. Hold ctr and click to remove all target points", new GUILayoutOption[0]);
		}
		if (GUILayout.Button("A to B path", new GUILayoutOption[0]))
		{
			this.activeDemo = 0;
		}
		if (GUILayout.Button("Multi Target Path", new GUILayoutOption[0]))
		{
			this.activeDemo = 1;
		}
		if (GUILayout.Button("Random Path", new GUILayoutOption[0]))
		{
			this.activeDemo = 2;
		}
		if (GUILayout.Button("Flee path", new GUILayoutOption[0]))
		{
			this.activeDemo = 3;
		}
		if (GUILayout.Button("Constant Path", new GUILayoutOption[0]))
		{
			this.activeDemo = 4;
		}
		if (GUILayout.Button("Flood Path", new GUILayoutOption[0]))
		{
			this.activeDemo = 5;
		}
		if (GUILayout.Button("Flood Path Tracer", new GUILayoutOption[0]))
		{
			this.activeDemo = 6;
		}
		GUILayout.EndArea();
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00017508 File Offset: 0x00015908
	public void OnPathComplete(Path p)
	{
		if (this.lastRender == null)
		{
			return;
		}
		if (p.error)
		{
			this.ClearPrevious();
			return;
		}
		if (p.GetType() == typeof(MultiTargetPath))
		{
			List<GameObject> list = new List<GameObject>(this.lastRender);
			this.lastRender.Clear();
			MultiTargetPath multiTargetPath = p as MultiTargetPath;
			for (int i = 0; i < multiTargetPath.vectorPaths.Length; i++)
			{
				if (multiTargetPath.vectorPaths[i] != null)
				{
					List<Vector3> list2 = multiTargetPath.vectorPaths[i];
					GameObject gameObject;
					if (list.Count > i && list[i].GetComponent<LineRenderer>() != null)
					{
						gameObject = list[i];
						list.RemoveAt(i);
					}
					else
					{
						gameObject = new GameObject("LineRenderer_" + i, new Type[]
						{
							typeof(LineRenderer)
						});
					}
					LineRenderer component = gameObject.GetComponent<LineRenderer>();
					component.sharedMaterial = this.lineMat;
					component.SetWidth(this.lineWidth, this.lineWidth);
					component.SetVertexCount(list2.Count);
					for (int j = 0; j < list2.Count; j++)
					{
						component.SetPosition(j, list2[j] + this.pathOffset);
					}
					this.lastRender.Add(gameObject);
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				UnityEngine.Object.Destroy(list[k]);
			}
		}
		else if (p.GetType() == typeof(ConstantPath))
		{
			this.ClearPrevious();
			ConstantPath constantPath = p as ConstantPath;
			List<Node> allNodes = constantPath.allNodes;
			HashSet<Node> hashSet = new HashSet<Node>();
			Mesh mesh = new Mesh();
			List<Vector3> list3 = new List<Vector3>();
			bool flag = false;
			for (int l = allNodes.Count - 1; l >= 0; l--)
			{
				if (!hashSet.Contains(allNodes[l]))
				{
					Vector3 a = (Vector3)allNodes[l].position + this.pathOffset;
					if (list3.Count == 65000 && !flag)
					{
						Debug.LogError("Too many nodes, rendering a mesh would throw 65K vertex error. Using Debug.DrawRay instead for the rest of the nodes");
						flag = true;
					}
					if (flag)
					{
						Debug.DrawRay(a, Vector3.up, Color.blue);
					}
					else
					{
						hashSet.Add(allNodes[l]);
						GridGraph gridGraph = AstarData.GetGraph(allNodes[l]) as GridGraph;
						float d = 1f;
						if (gridGraph != null)
						{
							d = gridGraph.nodeSize;
						}
						list3.Add(a + new Vector3(-0.5f, 0f, -0.5f) * d);
						list3.Add(a + new Vector3(0.5f, 0f, -0.5f) * d);
						list3.Add(a + new Vector3(-0.5f, 0f, 0.5f) * d);
						list3.Add(a + new Vector3(0.5f, 0f, 0.5f) * d);
					}
				}
			}
			Vector3[] array = list3.ToArray();
			int[] array2 = new int[3 * array.Length / 2];
			int m = 0;
			int num = 0;
			while (m < array.Length)
			{
				array2[num] = m;
				array2[num + 1] = m + 1;
				array2[num + 2] = m + 2;
				array2[num + 3] = m + 1;
				array2[num + 4] = m + 3;
				array2[num + 5] = m + 2;
				num += 6;
				m += 4;
			}
			Vector2[] array3 = new Vector2[array.Length];
			for (int n = 0; n < array3.Length; n += 4)
			{
				array3[n] = new Vector2(0f, 0f);
				array3[n + 1] = new Vector2(1f, 0f);
				array3[n + 2] = new Vector2(0f, 1f);
				array3[n + 3] = new Vector2(1f, 1f);
			}
			mesh.vertices = array;
			mesh.triangles = array2;
			mesh.uv = array3;
			mesh.RecalculateNormals();
			GameObject gameObject2 = new GameObject("Mesh", new Type[]
			{
				typeof(MeshRenderer),
				typeof(MeshFilter)
			});
			MeshFilter component2 = gameObject2.GetComponent<MeshFilter>();
			component2.mesh = mesh;
			MeshRenderer component3 = gameObject2.GetComponent<MeshRenderer>();
			component3.material = this.squareMat;
			this.lastRender.Add(gameObject2);
		}
		else
		{
			this.ClearPrevious();
			GameObject gameObject3 = new GameObject("LineRenderer", new Type[]
			{
				typeof(LineRenderer)
			});
			LineRenderer component4 = gameObject3.GetComponent<LineRenderer>();
			component4.sharedMaterial = this.lineMat;
			component4.SetWidth(this.lineWidth, this.lineWidth);
			component4.SetVertexCount(p.vectorPath.Count);
			for (int num2 = 0; num2 < p.vectorPath.Count; num2++)
			{
				component4.SetPosition(num2, p.vectorPath[num2] + this.pathOffset);
			}
			this.lastRender.Add(gameObject3);
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00017AAC File Offset: 0x00015EAC
	public void ClearPrevious()
	{
		for (int i = 0; i < this.lastRender.Count; i++)
		{
			UnityEngine.Object.Destroy(this.lastRender[i]);
		}
		this.lastRender.Clear();
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00017AF1 File Offset: 0x00015EF1
	public void OnApplicationQuit()
	{
		this.ClearPrevious();
		this.lastRender = null;
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00017B00 File Offset: 0x00015F00
	public void DemoPath()
	{
		Path path = null;
		if (this.activeDemo == 0)
		{
			path = ABPath.Construct(this.start.position, this.end.position, new OnPathDelegate(this.OnPathComplete));
		}
		else if (this.activeDemo == 1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(this.multipoints.ToArray(), this.end.position, null, new OnPathDelegate(this.OnPathComplete));
			path = multiTargetPath;
		}
		else if (this.activeDemo == 2)
		{
			RandomPath randomPath = RandomPath.Construct(this.start.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
			randomPath.spread = this.spread;
			randomPath.aimStrength = this.aimStrength;
			randomPath.aim = this.end.position;
			randomPath.replaceChance = this.replaceChance;
			path = randomPath;
		}
		else if (this.activeDemo == 3)
		{
			FleePath fleePath = FleePath.Construct(this.start.position, this.end.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
			fleePath.aimStrength = this.aimStrength;
			fleePath.replaceChance = this.replaceChance;
			fleePath.spread = this.spread;
			path = fleePath;
		}
		else if (this.activeDemo == 4)
		{
			ConstantPath constantPath = ConstantPath.Construct(this.end.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
			path = constantPath;
		}
		else if (this.activeDemo == 5)
		{
			FloodPath floodPath = FloodPath.Construct(this.end.position, null);
			this.lastFlood = floodPath;
			path = floodPath;
		}
		else if (this.activeDemo == 6 && this.lastFlood != null)
		{
			FloodPathTracer floodPathTracer = FloodPathTracer.Construct(this.end.position, this.lastFlood, new OnPathDelegate(this.OnPathComplete));
			path = floodPathTracer;
		}
		if (path != null)
		{
			AstarPath.StartPath(path);
			this.lastPath = path;
		}
	}

	// Token: 0x0400026E RID: 622
	public int activeDemo;

	// Token: 0x0400026F RID: 623
	public Transform start;

	// Token: 0x04000270 RID: 624
	public Transform end;

	// Token: 0x04000271 RID: 625
	public Vector3 pathOffset;

	// Token: 0x04000272 RID: 626
	public Material lineMat;

	// Token: 0x04000273 RID: 627
	public Material squareMat;

	// Token: 0x04000274 RID: 628
	public float lineWidth;

	// Token: 0x04000275 RID: 629
	public int searchLength = 1000;

	// Token: 0x04000276 RID: 630
	public int spread = 100;

	// Token: 0x04000277 RID: 631
	public float replaceChance = 0.1f;

	// Token: 0x04000278 RID: 632
	public float aimStrength;

	// Token: 0x04000279 RID: 633
	private Path lastPath;

	// Token: 0x0400027A RID: 634
	private List<GameObject> lastRender = new List<GameObject>();

	// Token: 0x0400027B RID: 635
	private List<Vector3> multipoints = new List<Vector3>();

	// Token: 0x0400027C RID: 636
	private FloodPath lastFlood;
}
