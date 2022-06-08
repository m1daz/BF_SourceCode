using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000040 RID: 64
	public class AstarSerializer
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00011750 File Offset: 0x0000FB50
		protected AstarSerializer()
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000117AC File Offset: 0x0000FBAC
		public AstarSerializer(AstarPath script)
		{
			this.active = script;
			this.astarData = script.astarData;
			this.mask = -1;
			this.mask -= AstarSerializer.SMask.SaveNodes;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001183C File Offset: 0x0000FC3C
		public static AstarSerializer GetDeserializer(Version version, AstarPath script)
		{
			if (version == AstarPath.Version)
			{
				return new AstarSerializer(script);
			}
			if (version > AstarPath.Version)
			{
				return new AstarSerializer(script);
			}
			if (version >= new Version(3, 0, 7))
			{
				return new AstarSerializer3_07(script);
			}
			if (version >= new Version(3, 0, 5))
			{
				return new AstarSerializer3_05(script);
			}
			if (version >= new Version(3, 0, 4))
			{
				return new AstarSerializer3_04(script);
			}
			return new AstarSerializer3_01(script);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000118CC File Offset: 0x0000FCCC
		public void SetUpGraphRefs(NavGraph[] graphs)
		{
			this.graphRefGuids = new int[this.loadedGraphGuids.Length];
			for (int i = 0; i < this.loadedGraphGuids.Length; i++)
			{
				Pathfinding.Util.Guid rhs = new Pathfinding.Util.Guid(this.loadedGraphGuids[i]);
				this.graphRefGuids[i] = -1;
				for (int j = 0; j < graphs.Length; j++)
				{
					if (graphs[j].guid == rhs)
					{
						this.graphRefGuids[i] = i;
					}
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00011950 File Offset: 0x0000FD50
		public void OpenSerialize()
		{
			this.onlySaveSettings = true;
			MemoryStream output = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(output);
			this.writerStream = binaryWriter;
			this.writerStream.Write(0);
			this.anchors = new Dictionary<string, int>();
			this.SerializeSerializationInfo();
			this.InitializeSerializeNodes();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001199C File Offset: 0x0000FD9C
		public AstarSerializer OpenDeserialize(byte[] data)
		{
			MemoryStream input = new MemoryStream(data);
			this.readerStream = new BinaryReader(input);
			this.DeserializeAnchors();
			return this.DeserializeSerializationInfo();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000119C8 File Offset: 0x0000FDC8
		public void SerializeSerializationInfo()
		{
			this.AddAnchor("SerializerSettings");
			BinaryWriter binaryWriter = this.writerStream;
			binaryWriter.Write(AstarPath.Version.ToString());
			binaryWriter.Write(this.astarData.graphs.Length);
			for (int i = 0; i < this.astarData.graphs.Length; i++)
			{
				binaryWriter.Write(this.astarData.graphs[i].guid.ToString());
			}
			binaryWriter.Write(this.mask);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00011A60 File Offset: 0x0000FE60
		public AstarSerializer DeserializeSerializationInfo()
		{
			if (!this.MoveToAnchor("SerializerSettings"))
			{
				throw new NullReferenceException("Anchor SerializerSettings was not found in the data");
			}
			BinaryReader binaryReader = this.readerStream;
			Version version = null;
			try
			{
				version = new Version(binaryReader.ReadString());
			}
			catch (Exception innerException)
			{
				Debug.LogError("Couldn't parse A* version ");
				this.error = AstarSerializer.SerializerError.WrongVersion;
				throw new FormatException("Couldn't parse A* version", innerException);
			}
			AstarSerializer astarSerializer = this;
			if (!AstarSerializer.IgnoreVersionDifferences)
			{
				if (version > AstarPath.Version)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Loading graph saved with a newer version of the A* Pathfinding Project, trying to load, but you might get errors.\nFile version: ",
						version,
						" Current A* version: ",
						AstarPath.Version
					}));
				}
				else if (version != AstarPath.Version)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Loading graphs saved with an older version of the A* Pathfinding Project, trying to load.\nFile version: ",
						version,
						" Current A* version: ",
						AstarPath.Version
					}));
					if (version < new Version(3, 0, 4))
					{
						astarSerializer = new AstarSerializer3_01(this.active);
					}
					else if (version < new Version(3, 0, 5))
					{
						astarSerializer = new AstarSerializer3_04(this.active);
					}
					else if (version < new Version(3, 0, 6))
					{
						astarSerializer = new AstarSerializer3_05(this.active);
					}
					astarSerializer.readerStream = this.readerStream;
					astarSerializer.anchors = this.anchors;
				}
			}
			int num = binaryReader.ReadInt32();
			astarSerializer.loadedGraphGuids = new string[num];
			for (int i = 0; i < num; i++)
			{
				astarSerializer.loadedGraphGuids[i] = binaryReader.ReadString();
			}
			astarSerializer.mask = binaryReader.ReadInt32();
			return astarSerializer;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00011C28 File Offset: 0x00010028
		public void SerializeSettings(NavGraph graph, AstarPath active)
		{
			ISerializableGraph serializableGraph = graph as ISerializableGraph;
			if (serializableGraph == null)
			{
				Debug.LogError("The graph specified is not serializable, the graph is of type " + graph.GetType());
				return;
			}
			serializableGraph.SerializeSettings(this);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011C5F File Offset: 0x0001005F
		public void InitializeSerializeNodes()
		{
			throw new NotImplementedException("This function is deprecated");
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011C6C File Offset: 0x0001006C
		public void SerializeNodes(NavGraph graph, AstarPath active)
		{
			if (this.mask == AstarSerializer.SMask.SaveNodes)
			{
				ISerializableGraph serializableGraph = graph as ISerializableGraph;
				if (serializableGraph == null)
				{
					Debug.LogError("The graph specified is not serializable, the graph is of type " + graph.GetType());
					return;
				}
				if (graph.nodes == null || graph.nodes.Length == 0)
				{
					this.writerStream.Write(0);
					return;
				}
				this.writerStream.Write(graph.nodes.Length);
				Debug.Log("Stored nodes  " + this.writerStream.BaseStream.Position);
				this.AddVariableAnchor("DeserializeGraphNodes");
				serializableGraph.SerializeNodes(graph.nodes, this);
				this.AddVariableAnchor("DeserializeNodes");
				if (this.mask == AstarSerializer.SMask.RunLengthEncoding)
				{
					int num = (int)graph.nodes[0].penalty;
					int num2 = 0;
					for (int i = 1; i < graph.nodes.Length; i++)
					{
						if ((ulong)graph.nodes[i].penalty != (ulong)((long)num) || i - num2 >= 254)
						{
							this.writerStream.Write((byte)(i - num2));
							this.writerStream.Write(num);
							num = (int)graph.nodes[i].penalty;
							num2 = i;
						}
					}
					this.writerStream.Write((byte)(graph.nodes.Length - num2));
					this.writerStream.Write(num);
					num = graph.nodes[0].flags;
					num2 = 0;
					for (int j = 1; j < graph.nodes.Length; j++)
					{
						if (graph.nodes[j].flags != num || j - num2 >= 255)
						{
							this.writerStream.Write((byte)(j - num2));
							this.writerStream.Write(num);
							num = graph.nodes[j].flags;
							num2 = j;
						}
					}
					this.writerStream.Write((byte)(graph.nodes.Length - num2));
					this.writerStream.Write(num);
				}
				for (int k = 0; k < graph.nodes.Length; k++)
				{
					this.SerializeNode(graph.nodes[k], this.writerStream);
				}
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00011EB0 File Offset: 0x000102B0
		public void DeserializeNodes(NavGraph graph, NavGraph[] graphs, int graphIndex, AstarPath active)
		{
			if (this.mask == AstarSerializer.SMask.SaveNodes)
			{
				ISerializableGraph serializableGraph = graph as ISerializableGraph;
				if (serializableGraph == null)
				{
					Debug.LogError("The graph specified is not serializable, the graph is of type " + graph.GetType());
					return;
				}
				int num = this.readerStream.ReadInt32();
				graph.nodes = serializableGraph.CreateNodes(num);
				if (num == 0)
				{
					return;
				}
				for (int i = 0; i < graph.nodes.Length; i++)
				{
					graph.nodes[i].graphIndex = graphIndex;
				}
				Debug.Log("Loading " + num + " nodes");
				if (!this.MoveToVariableAnchor("DeserializeGraphNodes"))
				{
					Debug.LogError("Error loading nodes - Couldn't find anchor");
				}
				serializableGraph.DeSerializeNodes(graph.nodes, this);
				if (!this.MoveToVariableAnchor("DeserializeNodes"))
				{
					Debug.LogError("Error loading nodes - Couldn't find anchor");
					return;
				}
				if (this.mask == AstarSerializer.SMask.RunLengthEncoding)
				{
					int num3;
					for (int j = 0; j < graph.nodes.Length; j = num3)
					{
						int num2 = (int)this.readerStream.ReadByte();
						int penalty = this.readerStream.ReadInt32();
						num3 = j + num2;
						if (num3 > graph.nodes.Length)
						{
							Debug.LogError(string.Concat(new object[]
							{
								"Run Length Encoding is too long ",
								num2,
								" ",
								num3,
								" ",
								graph.nodes.Length,
								" ",
								j
							}));
							num3 = graph.nodes.Length;
						}
						for (int k = j; k < num3; k++)
						{
							graph.nodes[k].penalty = (uint)penalty;
						}
					}
					int num4;
					for (int j = 0; j < graph.nodes.Length; j += num4)
					{
						num4 = (int)this.readerStream.ReadByte();
						int flags = this.readerStream.ReadInt32();
						int num5 = j + num4;
						if (num5 > graph.nodes.Length)
						{
							Debug.LogError(string.Concat(new object[]
							{
								"Run Length Encoding is too long ",
								num4,
								" ",
								num5,
								" ",
								graph.nodes.Length,
								" ",
								j
							}));
							num5 = graph.nodes.Length;
						}
						for (int l = j; l < num5; l++)
						{
							graph.nodes[l].flags = flags;
						}
					}
				}
				for (int m = 0; m < graph.nodes.Length; m++)
				{
					this.DeSerializeNode(graph.nodes[m], graphs, graphIndex, this.readerStream);
				}
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00012190 File Offset: 0x00010590
		public void SerializeEditorSettings(NavGraph graph, ISerializableGraphEditor editor, AstarPath active)
		{
			if (editor == null)
			{
				Debug.LogError("The editor specified is Null");
				return;
			}
			this.positionAtCounter = (int)this.writerStream.BaseStream.Position;
			this.writerStream.Write(0);
			this.counter = 0;
			editor.SerializeSettings(graph, this);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000121E0 File Offset: 0x000105E0
		public void DeSerializeEditorSettings(NavGraph graph, ISerializableGraphEditor editor, AstarPath active)
		{
			if (editor == null)
			{
				Debug.LogError("The editor specified is Null");
				return;
			}
			editor.DeSerializeSettings(graph, this);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000121FC File Offset: 0x000105FC
		public void DeSerializeSettings(NavGraph graph, AstarPath active)
		{
			ISerializableGraph serializableGraph = graph as ISerializableGraph;
			if (serializableGraph == null)
			{
				Debug.LogError("The graph specified is not (de)serializable (how it could be serialized in the first place is a mystery) the graph was of type " + graph.GetType());
				return;
			}
			graph.open = this.readerStream.ReadBoolean();
			serializableGraph.DeSerializeSettings(this);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00012244 File Offset: 0x00010644
		public void SerializeAnchors()
		{
			if (this.anchors == null)
			{
				Debug.LogError("The anchors dictionary is null");
				return;
			}
			int value = (int)this.writerStream.BaseStream.Position;
			this.writerStream.Write(this.anchors.Count);
			foreach (KeyValuePair<string, int> keyValuePair in this.anchors)
			{
				this.writerStream.Write(keyValuePair.Key);
				this.writerStream.Write(keyValuePair.Value);
			}
			int num = (int)this.writerStream.BaseStream.Position;
			this.writerStream.BaseStream.Position = 0L;
			this.writerStream.Write(value);
			this.writerStream.BaseStream.Position = (long)num;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00012340 File Offset: 0x00010740
		public void DeserializeAnchors()
		{
			this.readerStream.BaseStream.Position = 0L;
			int num = this.readerStream.ReadInt32();
			int num2 = (int)this.readerStream.BaseStream.Position;
			this.readerStream.BaseStream.Position = (long)num;
			int num3 = this.readerStream.ReadInt32();
			this.anchors = new Dictionary<string, int>(num3);
			for (int i = 0; i < num3; i++)
			{
				this.anchors.Add(this.readerStream.ReadString(), this.readerStream.ReadInt32());
			}
			this.readerStream.BaseStream.Position = (long)num2;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000123EC File Offset: 0x000107EC
		public void AddVariableAnchor(string name)
		{
			this.AddAnchor(this.sPrefix + "#" + name);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00012408 File Offset: 0x00010808
		public void AddAnchor(string name)
		{
			if (this.anchors.ContainsKey(name))
			{
				Debug.Log("Duplicate Anchor : " + name + " - A graph's serialization method is probably faulty");
			}
			else
			{
				this.anchors.Add(name, (int)this.writerStream.BaseStream.Position);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001245D File Offset: 0x0001085D
		public bool MoveToVariableAnchor(string name)
		{
			return this.MoveToAnchor(this.sPrefix + "#" + name);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00012478 File Offset: 0x00010878
		public bool MoveToAnchor(string name)
		{
			int num;
			if (this.anchors.TryGetValue(name, out num))
			{
				this.readerStream.BaseStream.Position = (long)num;
				return true;
			}
			return false;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000124B0 File Offset: 0x000108B0
		public virtual void SerializeUserConnections(UserConnection[] userConnections)
		{
			BinaryWriter binaryWriter = this.writerStream;
			this.AddAnchor("UserConnections");
			if (userConnections != null)
			{
				binaryWriter.Write(userConnections.Length);
				foreach (UserConnection userConnection in userConnections)
				{
					binaryWriter.Write(userConnection.p1.x);
					binaryWriter.Write(userConnection.p1.y);
					binaryWriter.Write(userConnection.p1.z);
					binaryWriter.Write(userConnection.p2.x);
					binaryWriter.Write(userConnection.p2.y);
					binaryWriter.Write(userConnection.p2.z);
					binaryWriter.Write(userConnection.doOverrideCost);
					binaryWriter.Write(userConnection.overrideCost);
					binaryWriter.Write(userConnection.oneWay);
					binaryWriter.Write(userConnection.width);
					binaryWriter.Write((int)userConnection.type);
					binaryWriter.Write(userConnection.enable);
					binaryWriter.Write(userConnection.doOverrideWalkability);
					binaryWriter.Write(userConnection.doOverridePenalty);
					binaryWriter.Write(userConnection.overridePenalty);
				}
			}
			else
			{
				binaryWriter.Write(0);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000125D4 File Offset: 0x000109D4
		public virtual UserConnection[] DeserializeUserConnections()
		{
			BinaryReader binaryReader = this.readerStream;
			if (this.MoveToAnchor("UserConnections"))
			{
				int num = binaryReader.ReadInt32();
				UserConnection[] array = new UserConnection[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = new UserConnection
					{
						p1 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
						p2 = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
						doOverrideCost = binaryReader.ReadBoolean(),
						overrideCost = binaryReader.ReadInt32(),
						oneWay = binaryReader.ReadBoolean(),
						width = binaryReader.ReadSingle(),
						type = (ConnectionType)binaryReader.ReadInt32(),
						enable = binaryReader.ReadBoolean(),
						doOverrideWalkability = binaryReader.ReadBoolean(),
						doOverridePenalty = binaryReader.ReadBoolean(),
						overridePenalty = binaryReader.ReadUInt32()
					};
				}
				return array;
			}
			return new UserConnection[0];
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000126DD File Offset: 0x00010ADD
		private void SerializeNode(Node node, BinaryWriter stream)
		{
			throw new NotSupportedException("This function has been deprecated");
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000126EC File Offset: 0x00010AEC
		private void DeSerializeNode(Node node, NavGraph[] graphs, int graphIndex, BinaryReader stream)
		{
			if (this.mask == AstarSerializer.SMask.SaveNodePositions)
			{
				node.position = new Int3(stream.ReadInt32(), stream.ReadInt32(), stream.ReadInt32());
			}
			if (this.mask != AstarSerializer.SMask.RunLengthEncoding)
			{
				node.penalty = (uint)stream.ReadInt32();
				node.flags = stream.ReadInt32();
			}
			if (this.mask == AstarSerializer.SMask.SaveNodeConnections)
			{
				if (this.tmpConnections == null)
				{
					this.tmpConnections = new List<Node>();
					this.tmpConnectionCosts = new List<int>();
				}
				else
				{
					this.tmpConnections.Clear();
					this.tmpConnectionCosts.Clear();
				}
				int num = (int)stream.ReadByte();
				for (int i = 0; i < num; i++)
				{
					int num2 = stream.ReadInt32();
					int num3 = num2 >> 26 & 63;
					num2 &= 67108863;
					int num4 = stream.ReadInt32();
					bool flag = false;
					if (num3 != graphIndex)
					{
						flag = stream.ReadBoolean();
					}
					if (this.graphRefGuids[graphIndex] != -1)
					{
						Node node2 = this.active.astarData.GetNode(this.graphRefGuids[num3], num2, graphs);
						if (node2 != null)
						{
							this.tmpConnections.Add(node2);
							if (this.mask == AstarSerializer.SMask.SaveNodeConnectionCosts)
							{
								this.tmpConnectionCosts.Add(num4);
							}
							if (flag)
							{
								node2.AddConnection(node, num4);
							}
						}
					}
				}
				node.connections = this.tmpConnections.ToArray();
				if (this.mask == AstarSerializer.SMask.SaveNodeConnectionCosts)
				{
					node.connectionCosts = this.tmpConnectionCosts.ToArray();
				}
				else
				{
					node.connectionCosts = new int[node.connections.Length];
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000128BC File Offset: 0x00010CBC
		public void WriteError()
		{
			if (this.positionAtError == -1L)
			{
				return;
			}
			if (this.writerStream == null)
			{
				Debug.LogError("You should only call the WriteError function when Serializing, not when DeSerializing");
				return;
			}
			long position = this.writerStream.BaseStream.Position;
			this.writerStream.BaseStream.Position = this.positionAtError;
			this.writerStream.Write(true);
			this.writerStream.BaseStream.Position = position;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00012931 File Offset: 0x00010D31
		public void Close()
		{
			if (this.readerStream != null)
			{
				this.readerStream.Close();
			}
			if (this.writerStream != null)
			{
				if (this.anchors != null)
				{
					this.SerializeAnchors();
				}
				this.writerStream.Close();
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00012970 File Offset: 0x00010D70
		public virtual void AddUnityReferenceValue(string key, UnityEngine.Object value)
		{
			BinaryWriter binaryWriter = this.writerStream;
			this.AddVariableAnchor(key);
			if (value == null)
			{
				binaryWriter.Write(0);
				return;
			}
			binaryWriter.Write(1);
			if (value == this.active.gameObject)
			{
				binaryWriter.Write(-128);
			}
			else if (value == this.active.transform)
			{
				binaryWriter.Write(-129);
			}
			else
			{
				binaryWriter.Write(value.GetInstanceID());
			}
			binaryWriter.Write(value.name);
			Component component = value as Component;
			GameObject gameObject = value as GameObject;
			if (component == null && gameObject == null)
			{
				binaryWriter.Write(string.Empty);
			}
			else
			{
				if (component != null && gameObject == null)
				{
					gameObject = component.gameObject;
				}
				UnityReferenceHelper unityReferenceHelper = gameObject.GetComponent<UnityReferenceHelper>();
				if (unityReferenceHelper == null)
				{
					Debug.Log("Adding UnityReferenceHelper to Unity Reference '" + value.name + "'");
					unityReferenceHelper = gameObject.AddComponent<UnityReferenceHelper>();
				}
				unityReferenceHelper.Reset();
				binaryWriter.Write(unityReferenceHelper.GetGUID());
			}
			binaryWriter.Write(false);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00012AAC File Offset: 0x00010EAC
		public void AddValue(string key, object value)
		{
			BinaryWriter binaryWriter = this.writerStream;
			this.AddVariableAnchor(key);
			if (value == null)
			{
				binaryWriter.Write(0);
				return;
			}
			binaryWriter.Write(1);
			Type type = value.GetType();
			if (type == typeof(int))
			{
				binaryWriter.Write((int)value);
			}
			else if (type == typeof(string))
			{
				string value2 = (string)value;
				binaryWriter.Write(value2);
			}
			else if (type == typeof(float))
			{
				binaryWriter.Write((float)value);
			}
			else if (type == typeof(bool))
			{
				binaryWriter.Write((bool)value);
			}
			else if (type == typeof(Vector3))
			{
				Vector3 vector = (Vector3)value;
				binaryWriter.Write(vector.x);
				binaryWriter.Write(vector.y);
				binaryWriter.Write(vector.z);
			}
			else if (type == typeof(Vector2))
			{
				Vector2 vector2 = (Vector2)value;
				binaryWriter.Write(vector2.x);
				binaryWriter.Write(vector2.y);
			}
			else if (type == typeof(Matrix4x4))
			{
				Matrix4x4 matrix4x = (Matrix4x4)value;
				for (int i = 0; i < 16; i++)
				{
					binaryWriter.Write(matrix4x[i]);
				}
			}
			else if (type == typeof(Bounds))
			{
				Bounds bounds = (Bounds)value;
				binaryWriter.Write(bounds.center.x);
				binaryWriter.Write(bounds.center.y);
				binaryWriter.Write(bounds.center.z);
				binaryWriter.Write(bounds.extents.x);
				binaryWriter.Write(bounds.extents.y);
				binaryWriter.Write(bounds.extents.z);
			}
			else
			{
				ISerializableObject serializableObject = value as ISerializableObject;
				if (serializableObject != null)
				{
					string text = this.sPrefix;
					this.sPrefix = this.sPrefix + key + ".";
					serializableObject.SerializeSettings(this);
					this.sPrefix = text;
				}
				else
				{
					UnityEngine.Object x = value as UnityEngine.Object;
					if (x != null)
					{
						Debug.LogWarning("Unity Object References should be added using AddUnityReferenceValue");
						this.WriteError();
						binaryWriter.BaseStream.Position -= 1L;
						binaryWriter.Write(2);
					}
					else
					{
						Debug.LogError("Can't serialize type '" + type.Name + "'");
						this.WriteError();
						binaryWriter.BaseStream.Position -= 1L;
						binaryWriter.Write(2);
					}
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00012D8C File Offset: 0x0001118C
		public virtual UnityEngine.Object GetUnityReferenceValue(string key, Type type, UnityEngine.Object defaultValue = null)
		{
			if (!this.MoveToVariableAnchor(key))
			{
				Debug.Log("Couldn't find key '" + key + "' in the data, returning default");
				return ((!(defaultValue == null)) ? defaultValue : this.GetDefaultValue(type)) as UnityEngine.Object;
			}
			BinaryReader binaryReader = this.readerStream;
			int num = (int)binaryReader.ReadByte();
			if (num == 0)
			{
				return ((!(defaultValue == null)) ? defaultValue : this.GetDefaultValue(type)) as UnityEngine.Object;
			}
			if (num == 2)
			{
				Debug.Log("The variable '" + key + "' was not serialized correctly and can therefore not be deserialized");
				return ((!(defaultValue == null)) ? defaultValue : this.GetDefaultValue(type)) as UnityEngine.Object;
			}
			int num2 = binaryReader.ReadInt32();
			string text = binaryReader.ReadString();
			if (num2 == -128)
			{
				return this.active.gameObject;
			}
			if (num2 == -129)
			{
				return this.active.transform;
			}
			string b = binaryReader.ReadString();
			UnityReferenceHelper[] array = UnityEngine.Object.FindSceneObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[];
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].GetGUID() == b)
				{
					if (type == typeof(GameObject))
					{
						return array[i].gameObject;
					}
					return array[i].GetComponent(type);
				}
				else
				{
					i++;
				}
			}
			binaryReader.ReadBoolean();
			UnityEngine.Object[] array2 = Resources.LoadAll(text, type);
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].name == text || array2.Length == 1)
				{
					return array2[j];
				}
			}
			return null;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00012F44 File Offset: 0x00011344
		public static string StripPathOfFolder(string path, string relativeFolder)
		{
			int num = path.IndexOf(relativeFolder);
			if (num == -1)
			{
				return string.Empty;
			}
			path = path.Remove(0, num + relativeFolder.Length);
			if (path.StartsWith("/"))
			{
				path = path.Remove(0, 1);
			}
			return path;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00012F92 File Offset: 0x00011392
		public object GetDefaultValue(Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00012FA8 File Offset: 0x000113A8
		public object GetValue(string key, Type type, object defaultValue = null)
		{
			if (!this.MoveToVariableAnchor(key))
			{
				Debug.Log(string.Concat(new string[]
				{
					"Couldn't find key '",
					key,
					"' in the data, returning default (",
					(defaultValue != null) ? defaultValue.ToString() : "null",
					")"
				}));
				return (defaultValue != null) ? defaultValue : this.GetDefaultValue(type);
			}
			BinaryReader binaryReader = this.readerStream;
			int num = (int)binaryReader.ReadByte();
			if (num == 0)
			{
				return (defaultValue != null) ? defaultValue : this.GetDefaultValue(type);
			}
			if (num == 2)
			{
				Debug.Log("The variable '" + key + "' was not serialized correctly and can therefore not be deserialized");
				return (defaultValue != null) ? defaultValue : this.GetDefaultValue(type);
			}
			object obj = null;
			if (type == typeof(int))
			{
				obj = binaryReader.ReadInt32();
			}
			else if (type == typeof(string))
			{
				obj = binaryReader.ReadString();
			}
			else if (type == typeof(float))
			{
				obj = binaryReader.ReadSingle();
			}
			else if (type == typeof(bool))
			{
				obj = binaryReader.ReadBoolean();
			}
			else if (type == typeof(Vector3))
			{
				obj = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
			}
			else if (type == typeof(Vector2))
			{
				obj = new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
			}
			else if (type == typeof(Matrix4x4))
			{
				Matrix4x4 matrix4x = default(Matrix4x4);
				for (int i = 0; i < 16; i++)
				{
					matrix4x[i] = binaryReader.ReadSingle();
				}
				obj = matrix4x;
			}
			else if (type == typeof(Bounds))
			{
				obj = new Bounds
				{
					center = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()),
					extents = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle())
				};
			}
			else
			{
				if (type.GetConstructor(Type.EmptyTypes) != null)
				{
					object obj2 = Activator.CreateInstance(type);
					ISerializableObject serializableObject = (ISerializableObject)obj2;
					if (serializableObject != null)
					{
						string text = this.sPrefix;
						this.sPrefix = this.sPrefix + key + ".";
						serializableObject.DeSerializeSettings(this);
						obj = serializableObject;
						this.sPrefix = text;
					}
				}
				if (obj == null)
				{
					Debug.LogError("Can't deSerialize type '" + type.Name + "'");
					obj = ((defaultValue != null) ? defaultValue : this.GetDefaultValue(type));
				}
			}
			return obj;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001328B File Offset: 0x0001168B
		public byte[] Compress(byte[] bytes)
		{
			return bytes;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001328E File Offset: 0x0001168E
		public byte[] DeCompress(byte[] bytes)
		{
			return bytes;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00013294 File Offset: 0x00011694
		public void Sort(int[] a, byte[] b)
		{
			bool flag = true;
			while (flag)
			{
				flag = false;
				for (int i = 0; i < a.Length - 1; i++)
				{
					if (a[i] > a[i + 1])
					{
						int num = a[i];
						a[i] = a[i + 1];
						a[i + 1] = num;
						byte b2 = b[i];
						b[i] = b[i + 1];
						b[i + 1] = b2;
						flag = true;
					}
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000132FC File Offset: 0x000116FC
		public void SaveToFile(string path, byte[] data)
		{
			FileStream fileStream = File.Create(path);
			fileStream.Write(data, 0, data.Length);
			fileStream.Close();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00013324 File Offset: 0x00011724
		public byte[] LoadFromFile(string path)
		{
			if (!File.Exists(path))
			{
				this.error = AstarSerializer.SerializerError.DoesNotExist;
				Debug.LogError("File does not exist : " + path);
				return new byte[0];
			}
			FileStream fileStream = File.Open(path, FileMode.Open);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return array;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00013384 File Offset: 0x00011784
		public static void TestLoadFile(string path)
		{
			FileStream fileStream = File.Open(path, FileMode.Open);
			fileStream.Close();
		}

		// Token: 0x04000202 RID: 514
		public BinaryWriter writerStream;

		// Token: 0x04000203 RID: 515
		public BinaryReader readerStream;

		// Token: 0x04000204 RID: 516
		public AstarPath active;

		// Token: 0x04000205 RID: 517
		public AstarData astarData;

		// Token: 0x04000206 RID: 518
		public bool replaceOldGraphs = true;

		// Token: 0x04000207 RID: 519
		public AstarSerializer.BitMask mask = -1 & ~AstarSerializer.SMask.SaveNodes;

		// Token: 0x04000208 RID: 520
		public static bool IgnoreVersionDifferences;

		// Token: 0x04000209 RID: 521
		public bool onlySaveSettings;

		// Token: 0x0400020A RID: 522
		public bool compress = true;

		// Token: 0x0400020B RID: 523
		public string[] loadedGraphGuids;

		// Token: 0x0400020C RID: 524
		public int[] graphRefGuids;

		// Token: 0x0400020D RID: 525
		public AstarSerializer.SerializerError error;

		// Token: 0x0400020E RID: 526
		public Dictionary<string, int> anchors;

		// Token: 0x0400020F RID: 527
		public static AstarSerializer.ReadUnityReference_Editor readUnityReference_Editor;

		// Token: 0x04000210 RID: 528
		public static AstarSerializer.WriteUnityReference_Editor writeUnityReference_Editor;

		// Token: 0x04000211 RID: 529
		public Dictionary<string, int> serializedVariables = new Dictionary<string, int>();

		// Token: 0x04000212 RID: 530
		public Hashtable serializedData;

		// Token: 0x04000213 RID: 531
		public string sPrefix = string.Empty;

		// Token: 0x04000214 RID: 532
		public byte prefix;

		// Token: 0x04000215 RID: 533
		public int counter;

		// Token: 0x04000216 RID: 534
		public int positionAtCounter = -1;

		// Token: 0x04000217 RID: 535
		public long positionAtError = -1L;

		// Token: 0x04000218 RID: 536
		private List<Node> tmpConnections;

		// Token: 0x04000219 RID: 537
		private List<int> tmpConnectionCosts;

		// Token: 0x02000041 RID: 65
		public class SMask
		{
			// Token: 0x06000289 RID: 649 RVA: 0x000133A9 File Offset: 0x000117A9
			public static string BitName(int i)
			{
				switch (i)
				{
				case 0:
					return "Nodes";
				case 1:
					return "Node Connections";
				case 2:
					return "Node Connection Costs";
				case 3:
					return "Node Positions";
				case 4:
					return "Run Length Encoding";
				default:
					return null;
				}
			}

			// Token: 0x0400021A RID: 538
			public static int SaveNodes = 1;

			// Token: 0x0400021B RID: 539
			public static int SaveNodeConnections = 2;

			// Token: 0x0400021C RID: 540
			public static int SaveNodeConnectionCosts = 4;

			// Token: 0x0400021D RID: 541
			public static int SaveNodePositions = 8;

			// Token: 0x0400021E RID: 542
			public static int RunLengthEncoding = 16;
		}

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x0600028C RID: 652
		public delegate UnityEngine.Object ReadUnityReference_Editor(AstarSerializer serializer, string name, int instanceID, Type type);

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x06000290 RID: 656
		public delegate void WriteUnityReference_Editor(AstarSerializer serializer, UnityEngine.Object ob);

		// Token: 0x02000044 RID: 68
		public enum SerializerError
		{
			// Token: 0x04000220 RID: 544
			Nothing,
			// Token: 0x04000221 RID: 545
			WrongMagic,
			// Token: 0x04000222 RID: 546
			WrongVersion,
			// Token: 0x04000223 RID: 547
			DoesNotExist
		}

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000294 RID: 660
		public delegate void DeSerializationInterrupt(AstarSerializer serializer, bool newer, System.Guid guid);

		// Token: 0x02000046 RID: 70
		public struct BitMask
		{
			// Token: 0x06000297 RID: 663 RVA: 0x0001340A File Offset: 0x0001180A
			public BitMask(int v)
			{
				this.value = v;
			}

			// Token: 0x06000298 RID: 664 RVA: 0x00013413 File Offset: 0x00011813
			public static bool operator ==(AstarSerializer.BitMask mask, int value)
			{
				return (mask.value & value) == value;
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00013421 File Offset: 0x00011821
			public static bool operator !=(AstarSerializer.BitMask mask, int value)
			{
				return (mask.value & value) != value;
			}

			// Token: 0x0600029A RID: 666 RVA: 0x00013432 File Offset: 0x00011832
			public static AstarSerializer.BitMask operator |(AstarSerializer.BitMask mask, int value)
			{
				mask.value |= value;
				return mask;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00013444 File Offset: 0x00011844
			public static AstarSerializer.BitMask operator &(AstarSerializer.BitMask mask, int value)
			{
				mask.value &= value;
				return mask;
			}

			// Token: 0x0600029C RID: 668 RVA: 0x00013456 File Offset: 0x00011856
			public static AstarSerializer.BitMask operator +(AstarSerializer.BitMask mask, int value)
			{
				mask.value |= value;
				return mask;
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00013468 File Offset: 0x00011868
			public static AstarSerializer.BitMask operator -(AstarSerializer.BitMask mask, int value)
			{
				mask.value &= ~value;
				return mask;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0001347B File Offset: 0x0001187B
			public static implicit operator AstarSerializer.BitMask(int v)
			{
				return new AstarSerializer.BitMask(v);
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00013483 File Offset: 0x00011883
			public static implicit operator int(AstarSerializer.BitMask v)
			{
				return v.value;
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x0001348C File Offset: 0x0001188C
			public override bool Equals(object o)
			{
				return o != null && (((AstarSerializer.BitMask)o).value & this.value) == this.value;
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x000134BE File Offset: 0x000118BE
			public override int GetHashCode()
			{
				return this.value;
			}

			// Token: 0x04000224 RID: 548
			private int value;
		}
	}
}
