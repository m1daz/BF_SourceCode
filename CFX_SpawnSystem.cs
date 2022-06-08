using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000534 RID: 1332
public class CFX_SpawnSystem : MonoBehaviour
{
	// Token: 0x060025BF RID: 9663 RVA: 0x00118A10 File Offset: 0x00116E10
	public static GameObject GetNextObject(GameObject sourceObj, bool activateObject = true)
	{
		int instanceID = sourceObj.GetInstanceID();
		if (!CFX_SpawnSystem.instance.poolCursors.ContainsKey(instanceID))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"[CFX_SpawnSystem.GetNextPoolObject()] Object hasn't been preloaded: ",
				sourceObj.name,
				" (ID:",
				instanceID,
				")"
			}));
			return null;
		}
		int index = CFX_SpawnSystem.instance.poolCursors[instanceID];
		Dictionary<int, int> dictionary;
		int key;
		(dictionary = CFX_SpawnSystem.instance.poolCursors)[key = instanceID] = dictionary[key] + 1;
		if (CFX_SpawnSystem.instance.poolCursors[instanceID] >= CFX_SpawnSystem.instance.instantiatedObjects[instanceID].Count)
		{
			CFX_SpawnSystem.instance.poolCursors[instanceID] = 0;
		}
		GameObject gameObject = CFX_SpawnSystem.instance.instantiatedObjects[instanceID][index];
		if (activateObject)
		{
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x00118B06 File Offset: 0x00116F06
	public static void PreloadObject(GameObject sourceObj, int poolSize = 1)
	{
		CFX_SpawnSystem.instance.addObjectToPool(sourceObj, poolSize);
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x00118B14 File Offset: 0x00116F14
	public static void UnloadObjects(GameObject sourceObj)
	{
		CFX_SpawnSystem.instance.removeObjectsFromPool(sourceObj);
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x060025C2 RID: 9666 RVA: 0x00118B21 File Offset: 0x00116F21
	public static bool AllObjectsLoaded
	{
		get
		{
			return CFX_SpawnSystem.instance.allObjectsLoaded;
		}
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x00118B30 File Offset: 0x00116F30
	private void addObjectToPool(GameObject sourceObject, int number)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			this.instantiatedObjects.Add(instanceID, new List<GameObject>());
			this.poolCursors.Add(instanceID, 0);
		}
		for (int i = 0; i < number; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(sourceObject);
			gameObject.SetActive(false);
			WFX_AutoDestructShuriken[] componentsInChildren = gameObject.GetComponentsInChildren<WFX_AutoDestructShuriken>(true);
			foreach (WFX_AutoDestructShuriken wfx_AutoDestructShuriken in componentsInChildren)
			{
				wfx_AutoDestructShuriken.OnlyDeactivate = true;
			}
			this.instantiatedObjects[instanceID].Add(gameObject);
			if (this.hideObjectsInHierarchy)
			{
				gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
		}
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x00118BEC File Offset: 0x00116FEC
	private void removeObjectsFromPool(GameObject sourceObject)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"[CFX_SpawnSystem.removeObjectsFromPool()] There aren't any preloaded object for: ",
				sourceObject.name,
				" (ID:",
				instanceID,
				")"
			}));
			return;
		}
		for (int i = this.instantiatedObjects[instanceID].Count - 1; i >= 0; i--)
		{
			GameObject obj = this.instantiatedObjects[instanceID][i];
			this.instantiatedObjects[instanceID].RemoveAt(i);
			UnityEngine.Object.Destroy(obj);
		}
		this.instantiatedObjects.Remove(instanceID);
		this.poolCursors.Remove(instanceID);
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x00118CB5 File Offset: 0x001170B5
	private void Awake()
	{
		if (CFX_SpawnSystem.instance != null)
		{
			Debug.LogWarning("CFX_SpawnSystem: There should only be one instance of CFX_SpawnSystem per Scene!");
		}
		CFX_SpawnSystem.instance = this;
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x00118CD8 File Offset: 0x001170D8
	private void Start()
	{
		this.allObjectsLoaded = false;
		for (int i = 0; i < this.objectsToPreload.Length; i++)
		{
			CFX_SpawnSystem.PreloadObject(this.objectsToPreload[i], this.objectsToPreloadTimes[i]);
		}
		this.allObjectsLoaded = true;
	}

	// Token: 0x0400265E RID: 9822
	private static CFX_SpawnSystem instance;

	// Token: 0x0400265F RID: 9823
	public GameObject[] objectsToPreload = new GameObject[0];

	// Token: 0x04002660 RID: 9824
	public int[] objectsToPreloadTimes = new int[0];

	// Token: 0x04002661 RID: 9825
	public bool hideObjectsInHierarchy;

	// Token: 0x04002662 RID: 9826
	private bool allObjectsLoaded;

	// Token: 0x04002663 RID: 9827
	private Dictionary<int, List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();

	// Token: 0x04002664 RID: 9828
	private Dictionary<int, int> poolCursors = new Dictionary<int, int>();
}
