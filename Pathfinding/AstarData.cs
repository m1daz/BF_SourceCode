using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class AstarData
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004185 File Offset: 0x00002585
		public AstarPath active
		{
			get
			{
				return AstarPath.active;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000418C File Offset: 0x0000258C
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004194 File Offset: 0x00002594
		public void SetData(byte[] data, uint checksum)
		{
			this.data = data;
			this.dataChecksum = checksum;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000041A4 File Offset: 0x000025A4
		public void Awake()
		{
			this.userConnections = new UserConnection[0];
			this.graphs = new NavGraph[0];
			if (this.cacheStartup && this.data_cachedStartup != null)
			{
				this.LoadFromCache();
			}
			else
			{
				this.DeserializeGraphs();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000041F0 File Offset: 0x000025F0
		[Obsolete]
		public void CollectNodes(int numTemporary)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000041F4 File Offset: 0x000025F4
		public void AssignNodeIndices()
		{
			int num = 0;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i].nodes != null)
				{
					Node[] nodes = this.graphs[i].nodes;
					int j = 0;
					while (j < nodes.Length)
					{
						if (nodes[j] != null)
						{
							nodes[j].SetNodeIndex(num);
						}
						j++;
						num++;
					}
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000426C File Offset: 0x0000266C
		public void CreateNodeRuns(int numParallel)
		{
			if (this.graphs == null)
			{
				throw new Exception("Cannot create NodeRuns when no graphs exist. (Scan and or Load graphs first)");
			}
			int num = 0;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i].nodes != null)
				{
					num += this.graphs[i].nodes.Length;
				}
			}
			this.AssignNodeIndices();
			if (this.nodeRuns == null || this.nodeRuns.Length != numParallel)
			{
				this.nodeRuns = new NodeRun[numParallel][];
			}
			for (int j = 0; j < this.nodeRuns.Length; j++)
			{
				if (this.nodeRuns[j] == null || this.nodeRuns[j].Length != num)
				{
					this.nodeRuns[j] = new NodeRun[num];
				}
				else
				{
					Debug.Log("Saved some allocations");
				}
				int num2 = 0;
				for (int k = 0; k < this.graphs.Length; k++)
				{
					Node[] nodes = this.graphs[k].nodes;
					if (nodes != null)
					{
						int l = 0;
						while (l < nodes.Length)
						{
							if (nodes[l] != null)
							{
								if (this.nodeRuns[j][num2] == null)
								{
									this.nodeRuns[j][num2] = new NodeRun();
								}
								else
								{
									this.nodeRuns[j][num2].Reset();
								}
								this.nodeRuns[j][num2].Link(nodes[l], num2);
							}
							l++;
							num2++;
						}
					}
				}
			}
			this.active.UpdatePathThreadInfoNodes();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004400 File Offset: 0x00002800
		public void UpdateShortcuts()
		{
			this.navmesh = (NavMeshGraph)this.FindGraphOfType(typeof(NavMeshGraph));
			this.gridGraph = (GridGraph)this.FindGraphOfType(typeof(GridGraph));
			this.pointGraph = (PointGraph)this.FindGraphOfType(typeof(PointGraph));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000445E File Offset: 0x0000285E
		public void LoadFromCache()
		{
			if (this.data_cachedStartup != null && this.data_cachedStartup.Length > 0)
			{
				this.DeserializeGraphs(this.data_cachedStartup);
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostCacheLoad);
			}
			else
			{
				Debug.LogError("Can't load from cache since the cache is empty");
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000449A File Offset: 0x0000289A
		public void SaveCacheData(SerializeSettings settings)
		{
			this.data_cachedStartup = this.SerializeGraphs(settings);
			this.cacheStartup = true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000044B0 File Offset: 0x000028B0
		public byte[] SerializeGraphs()
		{
			return this.SerializeGraphs(SerializeSettings.Settings);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000044C0 File Offset: 0x000028C0
		public byte[] SerializeGraphs(SerializeSettings settings)
		{
			uint num;
			return this.SerializeGraphs(settings, out num);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000044D8 File Offset: 0x000028D8
		public byte[] SerializeGraphs(SerializeSettings settings, out uint checksum)
		{
			AstarSerializer astarSerializer = new AstarSerializer(this, settings);
			astarSerializer.OpenSerialize();
			this.SerializeGraphsPart(astarSerializer);
			byte[] result = astarSerializer.CloseSerialize();
			checksum = astarSerializer.GetChecksum();
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000450A File Offset: 0x0000290A
		public void SerializeGraphsPart(AstarSerializer sr)
		{
			sr.SerializeGraphs(this.graphs);
			sr.SerializeUserConnections(this.userConnections);
			sr.SerializeNodes();
			sr.SerializeExtraInfo();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004530 File Offset: 0x00002930
		public void DeserializeGraphs()
		{
			if (this.data != null)
			{
				this.DeserializeGraphs(this.data);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000454C File Offset: 0x0000294C
		public void DeserializeGraphs(byte[] bytes)
		{
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("Bytes should not be null when passed to DeserializeGraphs");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPart(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip). Trying to load with old deserializer (pre 3.1)...");
					AstarSerializer serializer = new AstarSerializer(this.active);
					this.DeserializeGraphs_oldInternal(serializer);
				}
				this.active.DataUpdate();
			}
			catch (Exception arg)
			{
				Debug.LogWarning("Caught exception while deserializing data.\n" + arg);
				this.data_backup = bytes;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000045F0 File Offset: 0x000029F0
		public void DeserializeGraphsAdditive(byte[] bytes)
		{
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("Bytes should not be null when passed to DeserializeGraphs");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPartAdditive(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).");
				}
				this.active.DataUpdate();
			}
			catch (Exception arg)
			{
				Debug.LogWarning("Caught exception while deserializing data.\n" + arg);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004678 File Offset: 0x00002A78
		public void DeserializeGraphsPart(AstarSerializer sr)
		{
			this.graphs = sr.DeserializeGraphs();
			this.userConnections = sr.DeserializeUserConnections();
			sr.DeserializeNodes();
			sr.DeserializeExtraInfo();
			sr.PostDeserialization();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000046A4 File Offset: 0x00002AA4
		public void DeserializeGraphsPartAdditive(AstarSerializer sr)
		{
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			if (this.userConnections == null)
			{
				this.userConnections = new UserConnection[0];
			}
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			list.AddRange(sr.DeserializeGraphs());
			this.graphs = list.ToArray();
			List<UserConnection> list2 = new List<UserConnection>(this.userConnections);
			list2.AddRange(sr.DeserializeUserConnections());
			this.userConnections = list2.ToArray();
			sr.DeserializeNodes();
			sr.DeserializeExtraInfo();
			sr.PostDeserialization();
			for (int i = 0; i < this.graphs.Length; i++)
			{
				for (int j = i + 1; j < this.graphs.Length; j++)
				{
					if (this.graphs[i].guid == this.graphs[j].guid)
					{
						Debug.LogWarning("Guid Conflict when importing graphs additively. Imported graph will get a new Guid.\nThis message is (relatively) harmless.");
						this.graphs[i].guid = Pathfinding.Util.Guid.NewGuid();
						break;
					}
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000047B2 File Offset: 0x00002BB2
		[Obsolete("This function is obsolete. Use DeserializeGraphs () instead")]
		public void DeserializeGraphs(AstarSerializer serializer)
		{
			this.DeserializeGraphs_oldInternal(serializer);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000047BB File Offset: 0x00002BBB
		public void DeserializeGraphs_oldInternal(AstarSerializer serializer)
		{
			this.DeserializeGraphs_oldInternal(serializer, this.data);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000047CA File Offset: 0x00002BCA
		[Obsolete("This function is obsolete. Use DeserializeGraphs (bytes) instead")]
		public void DeserializeGraphs(AstarSerializer serializer, byte[] bytes)
		{
			this.DeserializeGraphs_oldInternal(serializer, bytes);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047D4 File Offset: 0x00002BD4
		public void DeserializeGraphs_oldInternal(AstarSerializer serializer, byte[] bytes)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (bytes == null || bytes.Length == 0)
			{
				Debug.Log("No previous data, assigning default");
				this.graphs = new NavGraph[0];
				return;
			}
			Debug.Log("Deserializing...");
			serializer = serializer.OpenDeserialize(bytes);
			this.DeserializeGraphsPart(serializer);
			serializer.Close();
			DateTime utcNow2 = DateTime.UtcNow;
			Debug.Log("Deserialization complete - Process took " + ((float)(utcNow2 - utcNow).Ticks * 0.0001f).ToString("0.00") + " ms");
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000486C File Offset: 0x00002C6C
		public void DeserializeGraphsPart(AstarSerializer serializer)
		{
			if (serializer.error != AstarSerializer.SerializerError.Nothing)
			{
				this.data_backup = (serializer.readerStream.BaseStream as MemoryStream).ToArray();
				Debug.Log("Error encountered : " + serializer.error + "\nWriting data to AstarData.data_backup");
				this.graphs = new NavGraph[0];
				return;
			}
			try
			{
				int num = serializer.readerStream.ReadInt32();
				int num2 = serializer.readerStream.ReadInt32();
				if (num != num2)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Data is corrupt (",
						num,
						" != ",
						num2,
						")"
					}));
					this.graphs = new NavGraph[0];
				}
				else
				{
					NavGraph[] array = new NavGraph[num];
					for (int i = 0; i < array.Length; i++)
					{
						if (!serializer.MoveToAnchor("Graph" + i))
						{
							Debug.LogError("Couldn't find graph " + i + " in the data");
							Debug.Log("Logging... " + serializer.anchors.Count);
							foreach (KeyValuePair<string, int> keyValuePair in serializer.anchors)
							{
								Debug.Log("KeyValuePair " + keyValuePair.Key);
							}
							array[i] = null;
						}
						else
						{
							string text = serializer.readerStream.ReadString();
							text = text.Replace("ListGraph", "PointGraph");
							Pathfinding.Util.Guid guid = new Pathfinding.Util.Guid(serializer.readerStream.ReadString());
							NavGraph navGraph = this.GuidToGraph(guid);
							if (navGraph != null)
							{
								array[i] = navGraph;
							}
							else
							{
								array[i] = this.CreateGraph(text);
							}
							NavGraph navGraph2 = array[i];
							if (navGraph2 == null)
							{
								Debug.LogError("One of the graphs saved was of an unknown type, the graph was of type '" + text + "'");
								this.data_backup = this.data;
								this.graphs = new NavGraph[0];
								return;
							}
							array[i].guid = guid;
							serializer.sPrefix = i.ToString();
							serializer.DeSerializeSettings(navGraph2, this.active);
						}
					}
					serializer.SetUpGraphRefs(array);
					for (int j = 0; j < array.Length; j++)
					{
						NavGraph graph = array[j];
						if (serializer.MoveToAnchor("GraphNodes_Graph" + j))
						{
							serializer.mask = serializer.readerStream.ReadInt32();
							serializer.sPrefix = j.ToString() + "N";
							serializer.DeserializeNodes(graph, array, j, this.active);
							serializer.sPrefix = string.Empty;
						}
					}
					this.userConnections = serializer.DeserializeUserConnections();
					List<NavGraph> list = new List<NavGraph>(array);
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k] == null)
						{
							list.Remove(array[k]);
						}
					}
					this.graphs = list.ToArray();
				}
			}
			catch (Exception ex)
			{
				this.data_backup = (serializer.readerStream.BaseStream as MemoryStream).ToArray();
				Debug.LogWarning("Deserializing Error Encountered - Writing data to AstarData.data_backup:\n" + ex.ToString());
				this.graphs = new NavGraph[0];
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004C1C File Offset: 0x0000301C
		public void FindGraphTypes()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(AstarPath));
			Type[] types = assembly.GetTypes();
			List<Type> list = new List<Type>();
			foreach (Type type in types)
			{
				for (Type baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
				{
					if (baseType == typeof(NavGraph))
					{
						list.Add(type);
						break;
					}
				}
			}
			this.graphTypes = list.ToArray();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004CB0 File Offset: 0x000030B0
		public Type GetGraphType(string type)
		{
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.graphTypes[i];
				}
			}
			return null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004CF8 File Offset: 0x000030F8
		public NavGraph CreateGraph(string type)
		{
			Debug.Log("Creating Graph of type '" + type + "'");
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.CreateGraph(this.graphTypes[i]);
				}
			}
			Debug.LogError("Graph type (" + type + ") wasn't found");
			return null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004D70 File Offset: 0x00003170
		public NavGraph CreateGraph(Type type)
		{
			NavGraph navGraph = Activator.CreateInstance(type) as NavGraph;
			navGraph.active = this.active;
			return navGraph;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004D98 File Offset: 0x00003198
		public NavGraph AddGraph(string type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError("No NavGraph of type '" + type + "' could be found");
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004E0C File Offset: 0x0000320C
		public NavGraph AddGraph(Type type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i] == type)
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"No NavGraph of type '",
					type,
					"' could be found, ",
					this.graphTypes.Length,
					" graph types are avaliable"
				}));
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004EA0 File Offset: 0x000032A0
		public void AddGraph(NavGraph graph)
		{
			this.graphs = new List<NavGraph>(this.graphs)
			{
				graph
			}.ToArray();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004ECC File Offset: 0x000032CC
		public void RemoveGraph(NavGraph graph)
		{
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			list.Remove(graph);
			this.graphs = list.ToArray();
			graph.SafeOnDestroy();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004F00 File Offset: 0x00003300
		public static NavGraph GetGraph(Node node)
		{
			if (node == null)
			{
				return null;
			}
			AstarPath active = AstarPath.active;
			if (active == null)
			{
				return null;
			}
			AstarData astarData = active.astarData;
			if (astarData == null)
			{
				return null;
			}
			if (astarData.graphs == null)
			{
				return null;
			}
			int graphIndex = node.graphIndex;
			if (graphIndex < 0 || graphIndex >= astarData.graphs.Length)
			{
				return null;
			}
			return astarData.graphs[graphIndex];
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004F6B File Offset: 0x0000336B
		public Node GetNode(int graphIndex, int nodeIndex)
		{
			return this.GetNode(graphIndex, nodeIndex, this.graphs);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004F7C File Offset: 0x0000337C
		public Node GetNode(int graphIndex, int nodeIndex, NavGraph[] graphs)
		{
			if (graphs == null)
			{
				return null;
			}
			if (graphIndex < 0 || graphIndex >= graphs.Length)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Graph index is out of range",
					graphIndex,
					" [0-",
					graphs.Length - 1,
					"]"
				}));
				return null;
			}
			NavGraph navGraph = graphs[graphIndex];
			if (navGraph.nodes == null)
			{
				return null;
			}
			if (nodeIndex < 0 || nodeIndex >= navGraph.nodes.Length)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Node index is out of range : ",
					nodeIndex,
					" [0-",
					navGraph.nodes.Length - 1,
					"] (graph ",
					graphIndex,
					")"
				}));
				return null;
			}
			return navGraph.nodes[nodeIndex];
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005068 File Offset: 0x00003468
		public NavGraph FindGraphOfType(Type type)
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i].GetType() == type)
				{
					return this.graphs[i];
				}
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000050AC File Offset: 0x000034AC
		public IEnumerable FindGraphsOfType(Type type)
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i].GetType() == type)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000050D8 File Offset: 0x000034D8
		public IEnumerable GetUpdateableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] is IUpdatableGraph)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000050FC File Offset: 0x000034FC
		public IEnumerable GetRaycastableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] is IRaycastableGraph)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005120 File Offset: 0x00003520
		public int GetGraphIndex(NavGraph graph)
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (graph == this.graphs[i])
				{
					return i;
				}
			}
			Debug.LogError("Graph doesn't exist");
			return -1;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005164 File Offset: 0x00003564
		public int GuidToIndex(Pathfinding.Util.Guid guid)
		{
			if (this.graphs == null)
			{
				return -1;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					Debug.LogWarning("Graph " + i + " is null - This should not happen");
				}
				else if (this.graphs[i].guid == guid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000051E0 File Offset: 0x000035E0
		public NavGraph GuidToGraph(Pathfinding.Util.Guid guid)
		{
			if (this.graphs == null)
			{
				return null;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					Debug.LogWarning("Graph " + i + " is null - This should not happen");
				}
				else if (this.graphs[i].guid == guid)
				{
					return this.graphs[i];
				}
			}
			return null;
		}

		// Token: 0x040000B0 RID: 176
		[NonSerialized]
		public NavMeshGraph navmesh;

		// Token: 0x040000B1 RID: 177
		[NonSerialized]
		public GridGraph gridGraph;

		// Token: 0x040000B2 RID: 178
		[NonSerialized]
		public PointGraph pointGraph;

		// Token: 0x040000B3 RID: 179
		[NonSerialized]
		public NodeRun[][] nodeRuns;

		// Token: 0x040000B4 RID: 180
		public Type[] graphTypes;

		// Token: 0x040000B5 RID: 181
		[NonSerialized]
		public NavGraph[] graphs = new NavGraph[0];

		// Token: 0x040000B6 RID: 182
		[NonSerialized]
		public UserConnection[] userConnections = new UserConnection[0];

		// Token: 0x040000B7 RID: 183
		public bool hasBeenReverted;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		private byte[] data;

		// Token: 0x040000B9 RID: 185
		public uint dataChecksum;

		// Token: 0x040000BA RID: 186
		public byte[] data_backup;

		// Token: 0x040000BB RID: 187
		public byte[] data_cachedStartup;

		// Token: 0x040000BC RID: 188
		public byte[] revertData;

		// Token: 0x040000BD RID: 189
		public bool cacheStartup;

		// Token: 0x040000BE RID: 190
		public bool compress;
	}
}
