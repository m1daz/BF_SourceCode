using System;
using System.IO;
using System.Text;
using Ionic.Zip;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000054 RID: 84
	public class AstarSerializer
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00014405 File Offset: 0x00012805
		public AstarSerializer(AstarData data)
		{
			this.data = data;
			this.settings = SerializeSettings.Settings;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00014431 File Offset: 0x00012831
		public AstarSerializer(AstarData data, SerializeSettings settings)
		{
			this.data = data;
			this.settings = settings;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00014459 File Offset: 0x00012859
		private static StringBuilder GetStringBuilder()
		{
			AstarSerializer._stringBuilder.Length = 0;
			return AstarSerializer._stringBuilder;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001446B File Offset: 0x0001286B
		public void AddChecksum(byte[] bytes)
		{
			this.checksum = Checksum.GetChecksum(bytes, this.checksum);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001447F File Offset: 0x0001287F
		public uint GetChecksum()
		{
			return this.checksum;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00014488 File Offset: 0x00012888
		public void OpenSerialize()
		{
			this.zip = new ZipFile();
			this.zip.AlternateEncoding = Encoding.UTF8;
			this.zip.AlternateEncodingUsage = ZipOption.Always;
			this.writerSettings = new JsonWriterSettings();
			this.writerSettings.AddTypeConverter(new VectorConverter());
			this.writerSettings.AddTypeConverter(new BoundsConverter());
			this.writerSettings.AddTypeConverter(new LayerMaskConverter());
			this.writerSettings.AddTypeConverter(new MatrixConverter());
			this.writerSettings.AddTypeConverter(new GuidConverter());
			this.writerSettings.AddTypeConverter(new UnityObjectConverter());
			this.writerSettings.PrettyPrint = this.settings.prettyPrint;
			this.meta = new GraphMeta();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014548 File Offset: 0x00012948
		public byte[] CloseSerialize()
		{
			byte[] array = this.SerializeMeta();
			this.AddChecksum(array);
			this.zip.AddEntry("meta.json", array);
			MemoryStream memoryStream = new MemoryStream();
			this.zip.Save(memoryStream);
			memoryStream.Close();
			array = memoryStream.ToArray();
			this.zip.Dispose();
			this.zip = null;
			return array;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000145A8 File Offset: 0x000129A8
		public void SerializeGraphs(NavGraph[] _graphs)
		{
			if (this.graphs != null)
			{
				throw new InvalidOperationException("Cannot serialize graphs multiple times.");
			}
			this.graphs = _graphs;
			if (this.zip == null)
			{
				throw new NullReferenceException("You must not call CloseSerialize before a call to this function");
			}
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				byte[] array = this.Serialize(this.graphs[i]);
				this.AddChecksum(array);
				this.zip.AddEntry("graph" + i + ".json", array);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00014650 File Offset: 0x00012A50
		public void SerializeUserConnections(UserConnection[] conns)
		{
			if (conns == null)
			{
				conns = new UserConnection[0];
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
			jsonWriter.Write(conns);
			byte[] bytes = this.encoding.GetBytes(stringBuilder.ToString());
			if (bytes.Length <= 2)
			{
				return;
			}
			this.AddChecksum(bytes);
			this.zip.AddEntry("connections.json", bytes);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000146BC File Offset: 0x00012ABC
		private byte[] SerializeMeta()
		{
			this.meta.version = AstarPath.Version;
			this.meta.graphs = this.data.graphs.Length;
			this.meta.guids = new string[this.data.graphs.Length];
			this.meta.typeNames = new string[this.data.graphs.Length];
			this.meta.nodeCounts = new int[this.data.graphs.Length];
			for (int i = 0; i < this.data.graphs.Length; i++)
			{
				this.meta.guids[i] = this.data.graphs[i].guid.ToString();
				this.meta.typeNames[i] = this.data.graphs[i].GetType().FullName;
				this.meta.nodeCounts[i] = ((this.data.graphs[i].nodes != null) ? this.data.graphs[i].nodes.Length : 0);
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
			jsonWriter.Write(this.meta);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00014828 File Offset: 0x00012C28
		public byte[] Serialize(NavGraph graph)
		{
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
			jsonWriter.Write(graph);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00014860 File Offset: 0x00012C60
		public void SerializeNodes()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			if (this.graphs == null)
			{
				throw new InvalidOperationException("Cannot serialize nodes with no serialized graphs (call SerializeGraphs first)");
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				byte[] array = this.SerializeNodes(i);
				this.AddChecksum(array);
				this.zip.AddEntry("graph" + i + "_nodes.binary", array);
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				byte[] array2 = this.SerializeNodeConnections(j);
				this.AddChecksum(array2);
				this.zip.AddEntry("graph" + j + "_conns.binary", array2);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00014928 File Offset: 0x00012D28
		private byte[] SerializeNodes(int index)
		{
			NavGraph navGraph = this.graphs[index];
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			Node[] array = navGraph.nodes;
			if (array == null)
			{
				array = new Node[0];
			}
			binaryWriter.Write(1);
			foreach (Node node in array)
			{
				if (node == null)
				{
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
				}
				else
				{
					binaryWriter.Write(node.position.x);
					binaryWriter.Write(node.position.y);
					binaryWriter.Write(node.position.z);
				}
			}
			binaryWriter.Write(2);
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == null)
				{
					binaryWriter.Write(0);
				}
				else
				{
					binaryWriter.Write(array[j].penalty);
				}
			}
			binaryWriter.Write(3);
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == null)
				{
					binaryWriter.Write(0);
				}
				else
				{
					binaryWriter.Write(array[k].flags);
				}
			}
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00014A70 File Offset: 0x00012E70
		public void SerializeExtraInfo()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				byte[] array = this.graphs[i].SerializeExtraInfo();
				if (array != null)
				{
					this.AddChecksum(array);
					this.zip.AddEntry("graph" + i + "_extra.binary", array);
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00014AEC File Offset: 0x00012EEC
		private byte[] SerializeNodeConnections(int index)
		{
			NavGraph navGraph = this.graphs[index];
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			if (navGraph.nodes == null)
			{
				return new byte[0];
			}
			Node[] nodes = navGraph.nodes;
			for (int i = 0; i < nodes.Length; i++)
			{
				Node node = nodes[i];
				if (node.connections == null)
				{
					binaryWriter.Write(0);
				}
				else
				{
					if (node.connections.Length != node.connectionCosts.Length)
					{
						throw new IndexOutOfRangeException(string.Concat(new object[]
						{
							"Node.connections.Length != Node.connectionCosts.Length. In node ",
							i,
							" in graph ",
							index
						}));
					}
					binaryWriter.Write((ushort)node.connections.Length);
					for (int j = 0; j < node.connections.Length; j++)
					{
						binaryWriter.Write(node.connections[j].GetNodeIndex());
						binaryWriter.Write(node.connectionCosts[j]);
					}
				}
			}
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00014C08 File Offset: 0x00013008
		public bool OpenDeserialize(byte[] bytes)
		{
			this.readerSettings = new JsonReaderSettings();
			this.readerSettings.AddTypeConverter(new VectorConverter());
			this.readerSettings.AddTypeConverter(new BoundsConverter());
			this.readerSettings.AddTypeConverter(new LayerMaskConverter());
			this.readerSettings.AddTypeConverter(new MatrixConverter());
			this.readerSettings.AddTypeConverter(new GuidConverter());
			this.readerSettings.AddTypeConverter(new UnityObjectConverter());
			this.str = new MemoryStream();
			this.str.Write(bytes, 0, bytes.Length);
			this.str.Position = 0L;
			try
			{
				this.zip = ZipFile.Read(this.str);
			}
			catch (ZipException arg)
			{
				Debug.LogWarning("Caught exception when loading from zip\n" + arg);
				this.str.Close();
				return false;
			}
			this.meta = this.DeserializeMeta(this.zip["meta.json"]);
			if (this.meta.version > AstarPath.Version)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Trying to load data from a newer version of the A* Pathfinding Project\nCurrent version: ",
					AstarPath.Version,
					" Data version: ",
					this.meta.version
				}));
			}
			else if (this.meta.version < AstarPath.Version)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Trying to load data from an older version of the A* Pathfinding Project\nCurrent version: ",
					AstarPath.Version,
					" Data version: ",
					this.meta.version,
					"\nThis is usually fine, it just means you have upgraded to a new version"
				}));
			}
			return true;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00014DC0 File Offset: 0x000131C0
		public void CloseDeserialize()
		{
			this.str.Close();
			this.zip.Dispose();
			this.zip = null;
			this.str = null;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00014DE8 File Offset: 0x000131E8
		public NavGraph[] DeserializeGraphs()
		{
			this.graphs = new NavGraph[this.meta.graphs];
			for (int i = 0; i < this.meta.graphs; i++)
			{
				Type graphType = this.meta.GetGraphType(i);
				ZipEntry zipEntry = this.zip["graph" + i + ".json"];
				if (zipEntry == null)
				{
					throw new FileNotFoundException(string.Concat(new object[]
					{
						"Could not find data for graph ",
						i,
						" in zip. Entry 'graph+",
						i,
						".json' does not exist"
					}));
				}
				string @string = this.GetString(zipEntry);
				NavGraph navGraph = this.data.CreateGraph(graphType);
				JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
				jsonReader.PopulateObject(navGraph);
				this.graphs[i] = navGraph;
				if (this.graphs[i].guid.ToString() != this.meta.guids[i])
				{
					throw new Exception("Guid in graph file not equal to guid defined in meta file. Have you edited the data manually?\n" + this.graphs[i].guid.ToString() + " != " + this.meta.guids[i]);
				}
			}
			return this.graphs;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00014F44 File Offset: 0x00013344
		public UserConnection[] DeserializeUserConnections()
		{
			ZipEntry zipEntry = this.zip["connections.json"];
			if (zipEntry == null)
			{
				return new UserConnection[0];
			}
			string @string = this.GetString(zipEntry);
			JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
			return (UserConnection[])jsonReader.Deserialize(typeof(UserConnection[]));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00014F9C File Offset: 0x0001339C
		public void DeserializeNodes()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.zip.ContainsEntry("graph" + i + "_nodes.binary"))
				{
					this.graphs[i].nodes = this.graphs[i].CreateNodes(this.meta.nodeCounts[i]);
				}
				else
				{
					this.graphs[i].nodes = this.graphs[i].CreateNodes(0);
				}
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				ZipEntry zipEntry = this.zip["graph" + j + "_nodes.binary"];
				if (zipEntry != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					zipEntry.Extract(memoryStream);
					memoryStream.Position = 0L;
					BinaryReader reader = new BinaryReader(memoryStream);
					this.DeserializeNodes(j, reader);
				}
			}
			for (int k = 0; k < this.graphs.Length; k++)
			{
				ZipEntry zipEntry2 = this.zip["graph" + k + "_conns.binary"];
				if (zipEntry2 != null)
				{
					MemoryStream memoryStream2 = new MemoryStream();
					zipEntry2.Extract(memoryStream2);
					memoryStream2.Position = 0L;
					BinaryReader reader2 = new BinaryReader(memoryStream2);
					this.DeserializeNodeConnections(k, reader2);
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00015114 File Offset: 0x00013514
		public void DeserializeExtraInfo()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				ZipEntry zipEntry = this.zip["graph" + i + "_extra.binary"];
				if (zipEntry != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					zipEntry.Extract(memoryStream);
					byte[] bytes = memoryStream.ToArray();
					this.graphs[i].DeserializeExtraInfo(bytes);
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00015188 File Offset: 0x00013588
		public void PostDeserialization()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				this.graphs[i].PostDeserialization();
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000151BC File Offset: 0x000135BC
		private void DeserializeNodes(int index, BinaryReader reader)
		{
			Node[] nodes = this.graphs[index].nodes;
			if (nodes == null)
			{
				throw new Exception(string.Concat(new object[]
				{
					"No nodes exist in graph ",
					index,
					" even though it has been requested to create ",
					this.meta.nodeCounts[index],
					" nodes"
				}));
			}
			if (reader.BaseStream.Length < (long)(nodes.Length * 20))
			{
				throw new Exception(string.Concat(new object[]
				{
					"Expected more data than was available in stream when reading node data for graph ",
					index,
					" at position ",
					reader.BaseStream.Position
				}));
			}
			int num = reader.ReadInt32();
			if (num != 1)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Expected chunk 1 (positions) when reading node data for graph ",
					index,
					" at position ",
					reader.BaseStream.Position - 4L,
					" in stream"
				}));
			}
			for (int i = 0; i < nodes.Length; i++)
			{
				nodes[i].position = new Int3(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
			}
			num = reader.ReadInt32();
			if (num != 2)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Expected chunk 2 (penalties) when reading node data for graph ",
					index,
					" at position ",
					reader.BaseStream.Position - 4L,
					" in stream"
				}));
			}
			for (int j = 0; j < nodes.Length; j++)
			{
				nodes[j].penalty = reader.ReadUInt32();
			}
			num = reader.ReadInt32();
			if (num != 3)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Expected chunk 3 (flags) when reading node data for graph ",
					index,
					" at position ",
					reader.BaseStream.Position - 4L,
					" in stream"
				}));
			}
			for (int k = 0; k < nodes.Length; k++)
			{
				nodes[k].flags = reader.ReadInt32();
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000153F8 File Offset: 0x000137F8
		private void DeserializeNodeConnections(int index, BinaryReader reader)
		{
			foreach (Node node in this.graphs[index].nodes)
			{
				int num = (int)reader.ReadUInt16();
				node.connections = new Node[num];
				node.connectionCosts = new int[num];
				for (int j = 0; j < num; j++)
				{
					int index2 = reader.ReadInt32();
					int num2 = reader.ReadInt32();
					node.connections[j] = this.GetNodeWithIndex(index2);
					node.connectionCosts[j] = num2;
				}
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001548C File Offset: 0x0001388C
		public Node GetNodeWithIndex(int index)
		{
			if (this.graphs == null)
			{
				throw new InvalidOperationException("Cannot find node with index because graphs have not been serialized/deserialized yet");
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i].nodes.Length > index)
				{
					return this.graphs[i].nodes[index];
				}
				index -= this.graphs[i].nodes.Length;
			}
			Debug.LogError("Could not find node with index " + index);
			return null;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00015518 File Offset: 0x00013918
		private string GetString(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			string result = streamReader.ReadToEnd();
			memoryStream.Position = 0L;
			streamReader.Close();
			return result;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00015558 File Offset: 0x00013958
		private GraphMeta DeserializeMeta(ZipEntry entry)
		{
			string @string = this.GetString(entry);
			JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
			return (GraphMeta)jsonReader.Deserialize(typeof(GraphMeta));
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00015590 File Offset: 0x00013990
		public static void SaveToFile(string path, byte[] data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				fileStream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000155D4 File Offset: 0x000139D4
		public static byte[] LoadFromFile(string path)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				byte[] array = new byte[(int)fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x04000226 RID: 550
		private AstarData data;

		// Token: 0x04000227 RID: 551
		public JsonWriterSettings writerSettings;

		// Token: 0x04000228 RID: 552
		public JsonReaderSettings readerSettings;

		// Token: 0x04000229 RID: 553
		private ZipFile zip;

		// Token: 0x0400022A RID: 554
		private MemoryStream str;

		// Token: 0x0400022B RID: 555
		private GraphMeta meta;

		// Token: 0x0400022C RID: 556
		private SerializeSettings settings;

		// Token: 0x0400022D RID: 557
		private NavGraph[] graphs;

		// Token: 0x0400022E RID: 558
		private const string jsonExt = ".json";

		// Token: 0x0400022F RID: 559
		private const string binaryExt = ".binary";

		// Token: 0x04000230 RID: 560
		private uint checksum = uint.MaxValue;

		// Token: 0x04000231 RID: 561
		private UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04000232 RID: 562
		private static StringBuilder _stringBuilder = new StringBuilder();
	}
}
