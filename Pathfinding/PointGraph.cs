using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000076 RID: 118
	[JsonOptIn]
	public class PointGraph : NavGraph, ISerializableGraph, IUpdatableGraph, ISerializableObject
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00023B7C File Offset: 0x00021F7C
		public static int CountChildren(Transform tr)
		{
			int num = 0;
			IEnumerator enumerator = tr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform tr2 = (Transform)obj;
					num++;
					num += PointGraph.CountChildren(tr2);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return num;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00023BE8 File Offset: 0x00021FE8
		public void AddChildren(ref int c, Transform tr)
		{
			IEnumerator enumerator = tr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					this.nodes[c].position = (Int3)transform.position;
					this.nodes[c].walkable = true;
					this.nodeGameObjects[c] = transform.gameObject;
					c++;
					this.AddChildren(ref c, transform);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00023C88 File Offset: 0x00022088
		public override void Scan()
		{
			if (this.root == null)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag(this.searchTag);
				this.nodeGameObjects = array;
				if (array == null)
				{
					this.CreateNodes(0);
					return;
				}
				this.nodes = this.CreateNodes(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					this.nodes[i].position = (Int3)array[i].transform.position;
					this.nodes[i].walkable = true;
				}
			}
			else if (!this.recursive)
			{
				this.nodes = this.CreateNodes(this.root.childCount);
				this.nodeGameObjects = new GameObject[this.nodes.Length];
				int num = 0;
				IEnumerator enumerator = this.root.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						this.nodes[num].position = (Int3)transform.position;
						this.nodes[num].walkable = true;
						this.nodeGameObjects[num] = transform.gameObject;
						num++;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			else
			{
				this.nodes = this.CreateNodes(PointGraph.CountChildren(this.root));
				this.nodeGameObjects = new GameObject[this.nodes.Length];
				int num2 = 0;
				this.AddChildren(ref num2, this.root);
			}
			List<Node> list = new List<Node>(3);
			List<int> list2 = new List<int>(3);
			for (int j = 0; j < this.nodes.Length; j++)
			{
				list.Clear();
				list2.Clear();
				Node node = this.nodes[j];
				for (int k = 0; k < this.nodes.Length; k++)
				{
					if (j != k)
					{
						Node node2 = this.nodes[k];
						float num3 = 0f;
						if (this.IsValidConnection(node, node2, out num3))
						{
							list.Add(node2);
							list2.Add(Mathf.RoundToInt(num3 * 1000f));
						}
					}
				}
				node.connections = list.ToArray();
				node.connectionCosts = list2.ToArray();
			}
			this.nodeGameObjects = null;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00023EF8 File Offset: 0x000222F8
		public bool IsValidConnection(Node a, Node b, out float dist)
		{
			dist = 0f;
			if (!a.walkable || !b.walkable)
			{
				return false;
			}
			Vector3 vector = (Vector3)(a.position - b.position);
			if ((!Mathf.Approximately(this.limits.x, 0f) && Mathf.Abs(vector.x) > this.limits.x) || (!Mathf.Approximately(this.limits.y, 0f) && Mathf.Abs(vector.y) > this.limits.y) || (!Mathf.Approximately(this.limits.z, 0f) && Mathf.Abs(vector.z) > this.limits.z))
			{
				return false;
			}
			dist = vector.magnitude;
			if (this.maxDistance == 0f || dist < this.maxDistance)
			{
				if (!this.raycast)
				{
					return true;
				}
				Ray ray = new Ray((Vector3)a.position, (Vector3)(b.position - a.position));
				Ray ray2 = new Ray((Vector3)b.position, (Vector3)(a.position - b.position));
				if (this.thickRaycast)
				{
					if (!Physics.SphereCast(ray, this.thickRaycastRadius, dist, this.mask) && !Physics.SphereCast(ray2, this.thickRaycastRadius, dist, this.mask))
					{
						return true;
					}
				}
				else if (!Physics.Raycast(ray, dist, this.mask) && !Physics.Raycast(ray2, dist, this.mask))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000240E8 File Offset: 0x000224E8
		public void UpdateArea(GraphUpdateObject guo)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (guo.bounds.Contains((Vector3)this.nodes[i].position))
				{
					guo.WillUpdateNode(this.nodes[i]);
					guo.Apply(this.nodes[i]);
				}
			}
			if (guo.updatePhysics)
			{
				Bounds bounds = guo.bounds;
				if (this.thickRaycast)
				{
					bounds.Expand(this.thickRaycastRadius * 2f);
				}
				List<Node> list = ListPool<Node>.Claim();
				List<int> list2 = ListPool<int>.Claim();
				for (int j = 0; j < this.nodes.Length; j++)
				{
					Node node = this.nodes[j];
					Vector3 a = (Vector3)node.position;
					List<Node> list3 = null;
					List<int> list4 = null;
					for (int k = 0; k < this.nodes.Length; k++)
					{
						if (k != j)
						{
							Vector3 b = (Vector3)this.nodes[k].position;
							if (Polygon.LineIntersectsBounds(bounds, a, b))
							{
								Node node2 = this.nodes[k];
								bool flag = node.ContainsConnection(node2);
								float num;
								if (!flag && this.IsValidConnection(node, node2, out num))
								{
									if (list3 == null)
									{
										list.Clear();
										list2.Clear();
										list3 = list;
										list4 = list2;
										list3.AddRange(node.connections);
										list4.AddRange(node.connectionCosts);
									}
									int item = Mathf.RoundToInt(num * 1000f);
									list3.Add(node2);
									list4.Add(item);
								}
								else if (flag && !this.IsValidConnection(node, node2, out num))
								{
									if (list3 == null)
									{
										list.Clear();
										list2.Clear();
										list3 = list;
										list4 = list2;
										list3.AddRange(node.connections);
										list4.AddRange(node.connectionCosts);
									}
									int num2 = list3.IndexOf(node2);
									if (num2 != -1)
									{
										list3.RemoveAt(num2);
										list4.RemoveAt(num2);
									}
								}
							}
						}
					}
					if (list3 != null)
					{
						node.connections = list3.ToArray();
						node.connectionCosts = list4.ToArray();
					}
				}
				ListPool<Node>.Release(list);
				ListPool<int>.Release(list2);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00024349 File Offset: 0x00022749
		public void SerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0002434B File Offset: 0x0002274B
		public void DeSerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00024350 File Offset: 0x00022750
		public void SerializeSettings(AstarSerializer serializer)
		{
			serializer.AddUnityReferenceValue("root", this.root);
			serializer.AddValue("maxDistance", this.maxDistance);
			serializer.AddValue("limits", this.limits);
			serializer.AddValue("mask", this.mask.value);
			serializer.AddValue("thickRaycast", this.thickRaycast);
			serializer.AddValue("thickRaycastRadius", this.thickRaycastRadius);
			serializer.AddValue("searchTag", this.searchTag);
			serializer.AddValue("recursive", this.recursive);
			serializer.AddValue("raycast", this.raycast);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00024420 File Offset: 0x00022820
		public void DeSerializeSettings(AstarSerializer serializer)
		{
			this.root = (Transform)serializer.GetUnityReferenceValue("root", typeof(Transform), null);
			this.maxDistance = (float)serializer.GetValue("maxDistance", typeof(float), null);
			this.limits = (Vector3)serializer.GetValue("limits", typeof(Vector3), null);
			this.mask.value = (int)serializer.GetValue("mask", typeof(int), null);
			this.thickRaycast = (bool)serializer.GetValue("thickRaycast", typeof(bool), null);
			this.thickRaycastRadius = (float)serializer.GetValue("thickRaycastRadius", typeof(float), null);
			this.searchTag = (string)serializer.GetValue("searchTag", typeof(string), null);
			this.recursive = (bool)serializer.GetValue("recursive", typeof(bool), null);
			this.raycast = (bool)serializer.GetValue("raycast", typeof(bool), true);
		}

		// Token: 0x0400031E RID: 798
		[JsonMember]
		public Transform root;

		// Token: 0x0400031F RID: 799
		[JsonMember]
		public string searchTag;

		// Token: 0x04000320 RID: 800
		[JsonMember]
		public float maxDistance;

		// Token: 0x04000321 RID: 801
		[JsonMember]
		public Vector3 limits;

		// Token: 0x04000322 RID: 802
		[JsonMember]
		public bool raycast = true;

		// Token: 0x04000323 RID: 803
		[JsonMember]
		public bool thickRaycast;

		// Token: 0x04000324 RID: 804
		[JsonMember]
		public float thickRaycastRadius = 1f;

		// Token: 0x04000325 RID: 805
		[JsonMember]
		public bool recursive = true;

		// Token: 0x04000326 RID: 806
		public bool autoLinkNodes = true;

		// Token: 0x04000327 RID: 807
		[JsonMember]
		public LayerMask mask;

		// Token: 0x04000328 RID: 808
		private GameObject[] nodeGameObjects;
	}
}
