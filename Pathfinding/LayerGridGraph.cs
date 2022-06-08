using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200006D RID: 109
	public class LayerGridGraph : GridGraph, IFunnelGraph, IRaycastableGraph, IUpdatableGraph
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0001DC28 File Offset: 0x0001C028
		public override Node[] CreateNodes(int number)
		{
			LevelGridNode[] array = new LevelGridNode[number];
			for (int i = 0; i < number; i++)
			{
				array[i] = new LevelGridNode();
				array[i].penalty = this.initialPenalty;
			}
			return array;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001DC65 File Offset: 0x0001C065
		public override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveGridGraphFromStatic();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001DC73 File Offset: 0x0001C073
		public new void RemoveGridGraphFromStatic()
		{
			Debug.Log("Destroying...");
			LevelGridNode.RemoveGridGraph(this);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001DC85 File Offset: 0x0001C085
		public override bool uniformWidhtDepthGrid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001DC88 File Offset: 0x0001C088
		public new void UpdateArea(GraphUpdateObject o)
		{
			if (this.nodes == null || this.nodes.Length != this.width * this.depth * this.layerCount)
			{
				Debug.LogWarning("The Grid Graph is not scanned, cannot update area ");
				return;
			}
			Bounds bounds = o.bounds;
			Vector3 a;
			Vector3 a2;
			base.GetBoundsMinMax(bounds, base.inverseMatrix, out a, out a2);
			int xmin = Mathf.RoundToInt(a.x - 0.5f);
			int xmax = Mathf.RoundToInt(a2.x - 0.5f);
			int ymin = Mathf.RoundToInt(a.z - 0.5f);
			int ymax = Mathf.RoundToInt(a2.z - 0.5f);
			IntRect intRect = new IntRect(xmin, ymin, xmax, ymax);
			IntRect intRect2 = intRect;
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect3 = intRect;
			Matrix4x4 lhs = this.matrix;
			lhs *= Matrix4x4.TRS(new Vector3(0.5f, 0f, 0.5f), Quaternion.identity, Vector3.one);
			bool flag = o.updatePhysics || o.modifyWalkability;
			bool flag2 = o is LayerGridGraphUpdate && ((LayerGridGraphUpdate)o).recalculateNodes;
			bool preserveExistingNodes = !(o is LayerGridGraphUpdate) || ((LayerGridGraphUpdate)o).preserveExistingNodes;
			if (o.trackChangedNodes && flag2)
			{
				Debug.LogError("Cannot track changed nodes when creating or deleting nodes.\nWill not update LayerGridGraph");
				return;
			}
			if (o.updatePhysics && !o.modifyWalkability && this.collision.collisionCheck)
			{
				Vector3 a3 = new Vector3(this.collision.diameter, 0f, this.collision.diameter) * 0.5f;
				a -= a3 * 1.02f;
				a2 += a3 * 1.02f;
				intRect3 = new IntRect(Mathf.RoundToInt(a.x - 0.5f), Mathf.RoundToInt(a.z - 0.5f), Mathf.RoundToInt(a2.x - 0.5f), Mathf.RoundToInt(a2.z - 0.5f));
				intRect2 = IntRect.Union(intRect3, intRect2);
			}
			if (flag && this.erodeIterations > 0)
			{
				intRect2 = intRect2.Expand(this.erodeIterations + 1);
			}
			IntRect intRect4 = IntRect.Intersection(intRect2, b);
			if (!flag2)
			{
				for (int i = intRect4.xmin; i <= intRect4.xmax; i++)
				{
					for (int j = intRect4.ymin; j <= intRect4.ymax; j++)
					{
						for (int k = 0; k < this.layerCount; k++)
						{
							o.WillUpdateNode(this.nodes[k * this.width * this.depth + j * this.width + i]);
						}
					}
				}
			}
			if (o.updatePhysics && !o.modifyWalkability)
			{
				this.collision.Initialize(this.matrix, this.nodeSize);
				intRect4 = IntRect.Intersection(intRect3, b);
				bool flag3 = false;
				for (int l = intRect4.xmin; l <= intRect4.xmax; l++)
				{
					for (int m = intRect4.ymin; m <= intRect4.ymax; m++)
					{
						flag3 |= this.RecalculateCell(l, m, preserveExistingNodes);
					}
				}
				for (int n = intRect4.xmin; n <= intRect4.xmax; n++)
				{
					for (int num = intRect4.ymin; num <= intRect4.ymax; num++)
					{
						for (int num2 = 0; num2 < this.layerCount; num2++)
						{
							int num3 = num2 * this.width * this.depth + num * this.width + n;
							LevelGridNode levelGridNode = this.nodes[num3] as LevelGridNode;
							if (levelGridNode != null)
							{
								this.CalculateConnections(this.nodes, levelGridNode, n, num, num2);
							}
						}
					}
				}
				if (flag3)
				{
					AstarPath.active.DataUpdate();
				}
			}
			intRect4 = IntRect.Intersection(intRect, b);
			for (int num4 = intRect4.xmin; num4 <= intRect4.xmax; num4++)
			{
				for (int num5 = intRect4.ymin; num5 <= intRect4.ymax; num5++)
				{
					for (int num6 = 0; num6 < this.layerCount; num6++)
					{
						int num7 = num6 * this.width * this.depth + num5 * this.width + num4;
						LevelGridNode levelGridNode2 = this.nodes[num7] as LevelGridNode;
						if (levelGridNode2 != null)
						{
							if (flag)
							{
								levelGridNode2.walkable = levelGridNode2.Bit15;
								o.Apply(levelGridNode2);
								levelGridNode2.Bit15 = levelGridNode2.walkable;
							}
							else
							{
								o.Apply(levelGridNode2);
							}
						}
					}
				}
			}
			if (flag && this.erodeIterations == 0)
			{
				intRect4 = IntRect.Intersection(intRect2, b);
				for (int num8 = intRect4.xmin; num8 <= intRect4.xmax; num8++)
				{
					for (int num9 = intRect4.ymin; num9 <= intRect4.ymax; num9++)
					{
						for (int num10 = 0; num10 < this.layerCount; num10++)
						{
							int num11 = num10 * this.width * this.depth + num9 * this.width + num8;
							LevelGridNode levelGridNode3 = this.nodes[num11] as LevelGridNode;
							if (levelGridNode3 != null)
							{
								this.CalculateConnections(this.nodes, levelGridNode3, num8, num9, num10);
							}
						}
					}
				}
			}
			else if (flag && this.erodeIterations > 0)
			{
				IntRect a4 = IntRect.Union(intRect, intRect3).Expand(this.erodeIterations);
				IntRect a5 = a4.Expand(this.erodeIterations);
				a4 = IntRect.Intersection(a4, b);
				a5 = IntRect.Intersection(a5, b);
				for (int num12 = a5.xmin; num12 <= a5.xmax; num12++)
				{
					for (int num13 = a5.ymin; num13 <= a5.ymax; num13++)
					{
						for (int num14 = 0; num14 < this.layerCount; num14++)
						{
							int num15 = num14 * this.width * this.depth + num13 * this.width + num12;
							LevelGridNode levelGridNode4 = this.nodes[num15] as LevelGridNode;
							if (levelGridNode4 != null)
							{
								bool walkable = levelGridNode4.walkable;
								levelGridNode4.walkable = levelGridNode4.Bit15;
								if (!a4.Contains(num12, num13))
								{
									levelGridNode4.Bit16 = walkable;
								}
							}
						}
					}
				}
				for (int num16 = a5.xmin; num16 <= a5.xmax; num16++)
				{
					for (int num17 = a5.ymin; num17 <= a5.ymax; num17++)
					{
						for (int num18 = 0; num18 < this.layerCount; num18++)
						{
							int num19 = num18 * this.width * this.depth + num17 * this.width + num16;
							LevelGridNode levelGridNode5 = this.nodes[num19] as LevelGridNode;
							if (levelGridNode5 != null)
							{
								this.CalculateConnections(this.nodes, levelGridNode5, num16, num17, num18);
							}
						}
					}
				}
				this.ErodeWalkableArea(a5.xmin, a5.ymin, a5.xmax + 1, a5.ymax + 1);
				for (int num20 = a5.xmin; num20 <= a5.xmax; num20++)
				{
					for (int num21 = a5.ymin; num21 <= a5.ymax; num21++)
					{
						if (!a4.Contains(num20, num21))
						{
							for (int num22 = 0; num22 < this.layerCount; num22++)
							{
								int num23 = num22 * this.width * this.depth + num21 * this.width + num20;
								LevelGridNode levelGridNode6 = this.nodes[num23] as LevelGridNode;
								if (levelGridNode6 != null)
								{
									levelGridNode6.walkable = levelGridNode6.Bit16;
								}
							}
						}
					}
				}
				for (int num24 = a5.xmin; num24 <= a5.xmax; num24++)
				{
					for (int num25 = a5.ymin; num25 <= a5.ymax; num25++)
					{
						for (int num26 = 0; num26 < this.layerCount; num26++)
						{
							int num27 = num26 * this.width * this.depth + num25 * this.width + num24;
							LevelGridNode levelGridNode7 = this.nodes[num27] as LevelGridNode;
							if (levelGridNode7 != null)
							{
								this.CalculateConnections(this.nodes, levelGridNode7, num24, num25, num26);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001E5F0 File Offset: 0x0001C9F0
		public override void Scan()
		{
			this.scans++;
			if (this.nodeSize <= 0f)
			{
				return;
			}
			base.GenerateMatrix();
			if (this.width > 1024 || this.depth > 1024)
			{
				Debug.LogError("One of the grid's sides is longer than 1024 nodes");
				return;
			}
			this.SetUpOffsetsAndCosts();
			int gridIndex = LevelGridNode.SetGridGraph(this);
			this.maxClimb = Mathf.Clamp(this.maxClimb, 0f, this.characterHeight);
			LinkedLevelCell[] array = new LinkedLevelCell[this.width * this.depth];
			if (this.collision == null)
			{
				this.collision = new GraphCollision();
			}
			this.collision.Initialize(this.matrix, this.nodeSize);
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					array[i * this.width + j] = new LinkedLevelCell();
					LinkedLevelCell linkedLevelCell = array[i * this.width + j];
					Vector3 position = this.matrix.MultiplyPoint3x4(new Vector3((float)j + 0.5f, 0f, (float)i + 0.5f));
					RaycastHit[] array2 = this.collision.CheckHeightAll(position);
					for (int k = 0; k < array2.Length / 2; k++)
					{
						RaycastHit raycastHit = array2[k];
						array2[k] = array2[array2.Length - 1 - k];
						array2[array2.Length - 1 - k] = raycastHit;
					}
					if (array2.Length > 0)
					{
						LinkedLevelNode linkedLevelNode = null;
						for (int l = 0; l < array2.Length; l++)
						{
							LinkedLevelNode linkedLevelNode2 = new LinkedLevelNode();
							linkedLevelNode2.position = array2[l].point;
							if (linkedLevelNode != null && linkedLevelNode2.position.y - linkedLevelNode.position.y <= this.mergeSpanRange)
							{
								linkedLevelNode.position = linkedLevelNode2.position;
								linkedLevelNode.hit = array2[l];
								linkedLevelNode.walkable = this.collision.Check(linkedLevelNode2.position);
							}
							else
							{
								linkedLevelNode2.walkable = this.collision.Check(linkedLevelNode2.position);
								linkedLevelNode2.hit = array2[l];
								linkedLevelNode2.height = float.PositiveInfinity;
								if (linkedLevelCell.first == null)
								{
									linkedLevelCell.first = linkedLevelNode2;
									linkedLevelNode = linkedLevelNode2;
								}
								else
								{
									linkedLevelNode.next = linkedLevelNode2;
									linkedLevelNode.height = linkedLevelNode2.position.y - linkedLevelNode.position.y;
									linkedLevelNode = linkedLevelNode.next;
								}
							}
						}
					}
					else
					{
						linkedLevelCell.first = new LinkedLevelNode
						{
							position = position,
							height = float.PositiveInfinity,
							walkable = !this.collision.unwalkableWhenNoGround
						};
					}
				}
			}
			int num = 0;
			this.layerCount = 0;
			for (int m = 0; m < this.depth; m++)
			{
				for (int n = 0; n < this.width; n++)
				{
					LinkedLevelCell linkedLevelCell2 = array[m * this.width + n];
					LinkedLevelNode linkedLevelNode3 = linkedLevelCell2.first;
					int num2 = 0;
					do
					{
						num2++;
						num++;
						linkedLevelNode3 = linkedLevelNode3.next;
					}
					while (linkedLevelNode3 != null);
					this.layerCount = ((num2 <= this.layerCount) ? this.layerCount : num2);
				}
			}
			if (this.layerCount > 255)
			{
				Debug.LogError("Too many layers, a maximum of LevelGridNode.MaxLayerCount are allowed (found " + this.layerCount + ")");
				return;
			}
			this.nodes = this.CreateNodes(this.width * this.depth * this.layerCount);
			int num3 = 0;
			float num4 = Mathf.Cos(this.maxSlope * 0.017453292f);
			for (int num5 = 0; num5 < this.depth; num5++)
			{
				for (int num6 = 0; num6 < this.width; num6++)
				{
					LinkedLevelCell linkedLevelCell3 = array[num5 * this.width + num6];
					LinkedLevelNode linkedLevelNode4 = linkedLevelCell3.first;
					linkedLevelCell3.index = num3;
					int num7 = 0;
					int num8 = 0;
					do
					{
						LevelGridNode levelGridNode = this.nodes[num5 * this.width + num6 + this.width * this.depth * num8] as LevelGridNode;
						levelGridNode.position = (Int3)linkedLevelNode4.position;
						levelGridNode.walkable = linkedLevelNode4.walkable;
						levelGridNode.Bit15 = levelGridNode.walkable;
						if (linkedLevelNode4.hit.normal != Vector3.zero && (this.penaltyAngle || num4 < 1f))
						{
							float num9 = Vector3.Dot(linkedLevelNode4.hit.normal.normalized, this.collision.up);
							if (this.penaltyAngle)
							{
								levelGridNode.penalty += (uint)Mathf.RoundToInt((1f - num9) * this.penaltyAngleFactor);
							}
							if (num9 < num4)
							{
								levelGridNode.walkable = false;
							}
						}
						levelGridNode.SetIndex(num5 * this.width + num6);
						if (linkedLevelNode4.height < this.characterHeight)
						{
							levelGridNode.walkable = false;
						}
						num3++;
						num7++;
						linkedLevelNode4 = linkedLevelNode4.next;
						num8++;
					}
					while (linkedLevelNode4 != null);
					while (num8 < this.layerCount)
					{
						this.nodes[num5 * this.width + num6 + this.width * this.depth * num8] = null;
						num8++;
					}
					linkedLevelCell3.count = num7;
				}
			}
			this.nodeCellIndices = new int[array.Length];
			for (int num10 = 0; num10 < this.depth; num10++)
			{
				for (int num11 = 0; num11 < this.width; num11++)
				{
					for (int num12 = 0; num12 < this.layerCount; num12++)
					{
						Node node = this.nodes[num10 * this.width + num11 + this.width * this.depth * num12];
						this.CalculateConnections(this.nodes, node, num11, num10, num12);
					}
				}
			}
			for (int num13 = 0; num13 < this.nodes.Length; num13++)
			{
				LevelGridNode levelGridNode2 = this.nodes[num13] as LevelGridNode;
				if (levelGridNode2 != null)
				{
					this.UpdatePenalty(levelGridNode2);
					levelGridNode2.SetGridIndex(gridIndex);
					if (!levelGridNode2.HasAnyGridConnections())
					{
						levelGridNode2.walkable = false;
					}
				}
			}
			this.ErodeWalkableArea(0, 0, this.width, this.depth);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001ED04 File Offset: 0x0001D104
		public bool RecalculateCell(int x, int z, bool preserveExistingNodes)
		{
			LinkedLevelCell linkedLevelCell = new LinkedLevelCell();
			Vector3 position = this.matrix.MultiplyPoint3x4(new Vector3((float)x + 0.5f, 0f, (float)z + 0.5f));
			RaycastHit[] array = this.collision.CheckHeightAll(position);
			for (int i = 0; i < array.Length / 2; i++)
			{
				RaycastHit raycastHit = array[i];
				array[i] = array[array.Length - 1 - i];
				array[array.Length - 1 - i] = raycastHit;
			}
			bool result = false;
			if (array.Length > 0)
			{
				LinkedLevelNode linkedLevelNode = null;
				for (int j = 0; j < array.Length; j++)
				{
					LinkedLevelNode linkedLevelNode2 = new LinkedLevelNode();
					linkedLevelNode2.position = array[j].point;
					if (linkedLevelNode != null && linkedLevelNode2.position.y - linkedLevelNode.position.y <= this.mergeSpanRange)
					{
						linkedLevelNode.position = linkedLevelNode2.position;
						linkedLevelNode.hit = array[j];
						linkedLevelNode.walkable = this.collision.Check(linkedLevelNode2.position);
					}
					else
					{
						linkedLevelNode2.walkable = this.collision.Check(linkedLevelNode2.position);
						linkedLevelNode2.hit = array[j];
						linkedLevelNode2.height = float.PositiveInfinity;
						if (linkedLevelCell.first == null)
						{
							linkedLevelCell.first = linkedLevelNode2;
							linkedLevelNode = linkedLevelNode2;
						}
						else
						{
							linkedLevelNode.next = linkedLevelNode2;
							linkedLevelNode.height = linkedLevelNode2.position.y - linkedLevelNode.position.y;
							linkedLevelNode = linkedLevelNode.next;
						}
					}
				}
			}
			else
			{
				linkedLevelCell.first = new LinkedLevelNode
				{
					position = position,
					height = float.PositiveInfinity,
					walkable = !this.collision.unwalkableWhenNoGround
				};
			}
			LinkedLevelNode linkedLevelNode3 = linkedLevelCell.first;
			int num = 0;
			int k = 0;
			for (;;)
			{
				if (k >= this.layerCount)
				{
					if (k + 1 > 255)
					{
						break;
					}
					this.AddLayers(1);
					result = true;
				}
				LevelGridNode levelGridNode = this.nodes[z * this.width + x + this.width * this.depth * k] as LevelGridNode;
				if (levelGridNode == null || !preserveExistingNodes)
				{
					this.nodes[z * this.width + x + this.width * this.depth * k] = new LevelGridNode();
					levelGridNode = (this.nodes[z * this.width + x + this.width * this.depth * k] as LevelGridNode);
					levelGridNode.penalty = this.initialPenalty;
					levelGridNode.SetGridIndex(LevelGridNode.SetGridGraph(this));
					result = true;
				}
				levelGridNode.connections = null;
				levelGridNode.position = (Int3)linkedLevelNode3.position;
				levelGridNode.walkable = linkedLevelNode3.walkable;
				levelGridNode.Bit15 = levelGridNode.walkable;
				if (linkedLevelNode3.hit.normal != Vector3.zero)
				{
					float num2 = Vector3.Dot(linkedLevelNode3.hit.normal.normalized, this.collision.up);
					if (this.penaltyAngle)
					{
						levelGridNode.penalty += (uint)Mathf.RoundToInt((1f - num2) * this.penaltyAngleFactor);
					}
					float num3 = Mathf.Cos(this.maxSlope * 0.017453292f);
					if (num2 < num3)
					{
						levelGridNode.walkable = false;
					}
				}
				levelGridNode.SetIndex(z * this.width + x);
				if (linkedLevelNode3.height < this.characterHeight)
				{
					levelGridNode.walkable = false;
				}
				num++;
				linkedLevelNode3 = linkedLevelNode3.next;
				k++;
				if (linkedLevelNode3 == null)
				{
					goto Block_14;
				}
			}
			Debug.LogError("Too many layers, a maximum of LevelGridNode.MaxLayerCount are allowed (required " + (k + 1) + ")");
			return result;
			Block_14:
			while (k < this.layerCount)
			{
				this.nodes[z * this.width + x + this.width * this.depth * k] = null;
				k++;
			}
			linkedLevelCell.count = num;
			return result;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001F160 File Offset: 0x0001D560
		public void AddLayers(int count)
		{
			int num = this.layerCount + count;
			if (num > 255)
			{
				Debug.LogError("Too many layers, a maximum of LevelGridNode.MaxLayerCount are allowed (required " + num + ")");
				return;
			}
			Node[] nodes = this.nodes;
			this.nodes = new Node[this.width * this.depth * num];
			for (int i = 0; i < nodes.Length; i++)
			{
				this.nodes[i] = nodes[i];
			}
			this.layerCount = num;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001F1E3 File Offset: 0x0001D5E3
		public virtual void UpdatePenalty(LevelGridNode node)
		{
			node.penalty = 0U;
			if (this.penaltyPosition)
			{
				node.penalty = (uint)Mathf.RoundToInt(((float)node.position.y - this.penaltyPositionOffset) * this.penaltyPositionFactor);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001F21C File Offset: 0x0001D61C
		public override void ErodeWalkableArea(int xmin, int zmin, int xmax, int zmax)
		{
			xmin = ((xmin >= 0) ? ((xmin <= this.width) ? xmin : this.width) : 0);
			xmax = ((xmax >= 0) ? ((xmax <= this.width) ? xmax : this.width) : 0);
			zmin = ((zmin >= 0) ? ((zmin <= this.depth) ? zmin : this.depth) : 0);
			zmax = ((zmax >= 0) ? ((zmax <= this.depth) ? zmax : this.depth) : 0);
			for (int i = 0; i < this.erodeIterations; i++)
			{
				for (int j = 0; j < this.layerCount; j++)
				{
					for (int k = zmin; k < zmax; k++)
					{
						for (int l = xmin; l < xmax; l++)
						{
							LevelGridNode levelGridNode = this.nodes[k * this.width + l + this.width * this.depth * j] as LevelGridNode;
							if (levelGridNode != null)
							{
								if (levelGridNode.walkable)
								{
									bool flag = false;
									for (int m = 0; m < 4; m++)
									{
										if (!levelGridNode.GetConnection(m))
										{
											flag = true;
											break;
										}
									}
									if (flag)
									{
										levelGridNode.walkable = false;
									}
								}
							}
						}
					}
				}
				for (int n = 0; n < this.layerCount; n++)
				{
					for (int num = zmin; num < zmax; num++)
					{
						for (int num2 = xmin; num2 < xmax; num2++)
						{
							Node node = this.nodes[num * this.width + num2 + this.width * this.depth * n];
							if (node != null)
							{
								this.CalculateConnections(this.nodes, node, num2, num, n);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001F42C File Offset: 0x0001D82C
		public void CalculateConnections(Node[] nodes, Node node, int x, int z, int layerIndex)
		{
			if (node == null)
			{
				return;
			}
			LevelGridNode levelGridNode = (LevelGridNode)node;
			levelGridNode.ResetAllGridConnections();
			if (!node.walkable)
			{
				return;
			}
			float num;
			if (layerIndex == this.layerCount - 1 || nodes[levelGridNode.GetIndex() + this.width * this.depth * (layerIndex + 1)] == null)
			{
				num = float.PositiveInfinity;
			}
			else
			{
				num = (float)Math.Abs(levelGridNode.position.y - nodes[levelGridNode.GetIndex() + this.width * this.depth * (layerIndex + 1)].position.y) * 0.001f;
			}
			for (int i = 0; i < 4; i++)
			{
				int num2 = x + this.neighbourXOffsets[i];
				int num3 = z + this.neighbourZOffsets[i];
				if (num2 >= 0 && num3 >= 0 && num2 < this.width && num3 < this.depth)
				{
					int num4 = num3 * this.width + num2;
					int value = 255;
					for (int j = 0; j < this.layerCount; j++)
					{
						Node node2 = nodes[num4 + this.width * this.depth * j];
						if (node2 != null && node2.walkable)
						{
							float num5;
							if (j == this.layerCount - 1 || nodes[num4 + this.width * this.depth * (j + 1)] == null)
							{
								num5 = float.PositiveInfinity;
							}
							else
							{
								num5 = (float)Math.Abs(node2.position.y - nodes[num4 + this.width * this.depth * (j + 1)].position.y) * 0.001f;
							}
							float num6 = Mathf.Max((float)node2.position.y * 0.001f, (float)levelGridNode.position.y * 0.001f);
							float num7 = Mathf.Min((float)node2.position.y * 0.001f + num5, (float)levelGridNode.position.y * 0.001f + num);
							float num8 = num7 - num6;
							if (num8 >= this.characterHeight && (float)Mathf.Abs(node2.position.y - levelGridNode.position.y) * 0.001f <= this.maxClimb)
							{
								value = j;
							}
						}
					}
					levelGridNode.SetConnectionValue(i, value);
				}
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001F6B0 File Offset: 0x0001DAB0
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, Node hint = null)
		{
			if (this.nodes == null || this.depth * this.width * this.layerCount != this.nodes.Length)
			{
				return default(NNInfo);
			}
			position = base.inverseMatrix.MultiplyPoint3x4(position);
			int num = Mathf.Clamp(Mathf.RoundToInt(position.x - 0.5f), 0, this.width - 1);
			int num2 = Mathf.Clamp(Mathf.RoundToInt(position.z - 0.5f), 0, this.depth - 1);
			int num3 = this.width * num2 + num;
			float num4 = float.PositiveInfinity;
			Node node = null;
			for (int i = 0; i < this.layerCount; i++)
			{
				Node node2 = this.nodes[num3 + this.width * this.depth * i];
				if (node2 != null)
				{
					float sqrMagnitude = ((Vector3)node2.position - position).sqrMagnitude;
					if (sqrMagnitude < num4)
					{
						num4 = sqrMagnitude;
						node = node2;
					}
				}
			}
			return new NNInfo(node);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001F7D0 File Offset: 0x0001DBD0
		private Node GetNearestNode(Vector3 position, int x, int z, NNConstraint constraint)
		{
			int num = this.width * z + x;
			float num2 = float.PositiveInfinity;
			Node result = null;
			for (int i = 0; i < this.layerCount; i++)
			{
				Node node = this.nodes[num + this.width * this.depth * i];
				if (node != null)
				{
					float sqrMagnitude = ((Vector3)node.position - position).sqrMagnitude;
					if (sqrMagnitude < num2 && constraint.Suitable(node))
					{
						num2 = sqrMagnitude;
						result = node;
					}
				}
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001F864 File Offset: 0x0001DC64
		public override NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.nodes == null || this.depth * this.width * this.layerCount != this.nodes.Length || this.layerCount == 0)
			{
				return default(NNInfo);
			}
			Vector3 vector = position;
			position = base.inverseMatrix.MultiplyPoint3x4(position);
			int num = Mathf.Clamp(Mathf.RoundToInt(position.x - 0.5f), 0, this.width - 1);
			int num2 = Mathf.Clamp(Mathf.RoundToInt(position.z - 0.5f), 0, this.depth - 1);
			float num3 = float.PositiveInfinity;
			int num4 = this.getNearestForceOverlap;
			Node node = this.GetNearestNode(vector, num, num2, constraint);
			if (node != null)
			{
				num3 = ((Vector3)node.position - vector).sqrMagnitude;
			}
			if (node != null)
			{
				if (num4 == 0)
				{
					return new NNInfo(node);
				}
				num4--;
			}
			float num5 = (!constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistance;
			float num6 = num5 * num5;
			int num7 = 1;
			for (;;)
			{
				int i = num2 + num7;
				if (this.nodeSize * (float)num7 > num5)
				{
					break;
				}
				int j;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						Node nearestNode = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode != null)
						{
							float sqrMagnitude = ((Vector3)nearestNode.position - vector).sqrMagnitude;
							if (sqrMagnitude < num3 && sqrMagnitude < num6)
							{
								num3 = sqrMagnitude;
								node = nearestNode;
							}
						}
					}
				}
				i = num2 - num7;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						Node nearestNode2 = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode2 != null)
						{
							float sqrMagnitude2 = ((Vector3)nearestNode2.position - vector).sqrMagnitude;
							if (sqrMagnitude2 < num3 && sqrMagnitude2 < num6)
							{
								num3 = sqrMagnitude2;
								node = nearestNode2;
							}
						}
					}
				}
				j = num - num7;
				i = num2 - num7 + 1;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						Node nearestNode3 = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode3 != null)
						{
							float sqrMagnitude3 = ((Vector3)nearestNode3.position - vector).sqrMagnitude;
							if (sqrMagnitude3 < num3 && sqrMagnitude3 < num6)
							{
								num3 = sqrMagnitude3;
								node = nearestNode3;
							}
						}
					}
				}
				j = num + num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						Node nearestNode4 = this.GetNearestNode(vector, j, i, constraint);
						if (nearestNode4 != null)
						{
							float sqrMagnitude4 = ((Vector3)nearestNode4.position - vector).sqrMagnitude;
							if (sqrMagnitude4 < num3 && sqrMagnitude4 < num6)
							{
								num3 = sqrMagnitude4;
								node = nearestNode4;
							}
						}
					}
				}
				if (node != null)
				{
					if (num4 == 0)
					{
						goto Block_37;
					}
					num4--;
				}
				num7++;
			}
			return new NNInfo(node);
			Block_37:
			return new NNInfo(node);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001FC64 File Offset: 0x0001E064
		public new void BuildFunnelCorridor(List<Node> path, int sIndex, int eIndex, List<Vector3> left, List<Vector3> right)
		{
			for (int i = sIndex; i < eIndex; i++)
			{
				LevelGridNode n = path[i] as LevelGridNode;
				LevelGridNode n2 = path[i + 1] as LevelGridNode;
				this.AddPortal(n, n2, left, right);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001FCAB File Offset: 0x0001E0AB
		public new void AddPortal(Node n1, Node n2, List<Vector3> left, List<Vector3> right)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001FCB0 File Offset: 0x0001E0B0
		public void AddPortal(LevelGridNode n1, LevelGridNode n2, List<Vector3> left, List<Vector3> right)
		{
			if (n1 == n2)
			{
				return;
			}
			int index = n1.GetIndex();
			int index2 = n2.GetIndex();
			int num = index % this.width;
			int num2 = index2 % this.width;
			int num3 = index / this.width;
			int num4 = index2 / this.width;
			Vector3 vector = (Vector3)n1.position;
			Vector3 vector2 = (Vector3)n2.position;
			int num5 = Mathf.Abs(num - num2);
			int num6 = Mathf.Abs(num3 - num4);
			if (num5 > 1 || num6 > 1)
			{
				left.Add(vector);
				right.Add(vector);
				left.Add(vector2);
				right.Add(vector2);
			}
			else if (num5 + num6 <= 1)
			{
				Vector3 vector3 = vector2 - vector;
				vector3 *= 0.5f;
				Vector3 vector4 = Vector3.Cross(vector3.normalized, Vector3.up);
				vector4 *= this.nodeSize * 0.5f;
				left.Add(vector + vector3 - vector4);
				right.Add(vector + vector3 + vector4);
			}
			else
			{
				Vector3 vector5 = vector2 - vector;
				Vector3 a = (vector + vector2) * 0.5f;
				Vector3 vector6 = Vector3.Cross(vector5.normalized, Vector3.up);
				vector6 *= this.nodeSize * 0.5f;
				left.Add(a - vector6);
				right.Add(a + vector6);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001FE48 File Offset: 0x0001E248
		public new bool Linecast(Vector3 _a, Vector3 _b)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(_a, _b, null, out graphHitInfo);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001FE60 File Offset: 0x0001E260
		public new bool Linecast(Vector3 _a, Vector3 _b, Node hint)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(_a, _b, hint, out graphHitInfo);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001FE78 File Offset: 0x0001E278
		public new bool Linecast(Vector3 _a, Vector3 _b, Node hint, out GraphHitInfo hit)
		{
			return this.SnappedLinecast(_a, _b, hint, out hit);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001FE88 File Offset: 0x0001E288
		public new bool SnappedLinecast(Vector3 _a, Vector3 _b, Node hint, out GraphHitInfo hit)
		{
			hit = default(GraphHitInfo);
			LevelGridNode levelGridNode = base.GetNearest(_a, NNConstraint.None).node as LevelGridNode;
			LevelGridNode levelGridNode2 = base.GetNearest(_b, NNConstraint.None).node as LevelGridNode;
			if (levelGridNode == null || levelGridNode2 == null)
			{
				hit.node = null;
				hit.point = _a;
				return true;
			}
			_a = base.inverseMatrix.MultiplyPoint3x4((Vector3)levelGridNode.position);
			_a.x -= 0.5f;
			_a.z -= 0.5f;
			_b = base.inverseMatrix.MultiplyPoint3x4((Vector3)levelGridNode2.position);
			_b.x -= 0.5f;
			_b.z -= 0.5f;
			Int3 ob = new Int3(Mathf.RoundToInt(_a.x), Mathf.RoundToInt(_a.y), Mathf.RoundToInt(_a.z));
			Int3 @int = new Int3(Mathf.RoundToInt(_b.x), Mathf.RoundToInt(_b.y), Mathf.RoundToInt(_b.z));
			hit.origin = (Vector3)ob;
			if (!levelGridNode.walkable)
			{
				hit.node = levelGridNode;
				hit.point = this.matrix.MultiplyPoint3x4(new Vector3((float)ob.x + 0.5f, 0f, (float)ob.z + 0.5f));
				hit.point.y = ((Vector3)hit.node.position).y;
				return true;
			}
			int num = Mathf.Abs(ob.x - @int.x);
			int num2 = Mathf.Abs(ob.z - @int.z);
			LevelGridNode levelGridNode4;
			for (LevelGridNode levelGridNode3 = levelGridNode; levelGridNode3 != levelGridNode2; levelGridNode3 = levelGridNode4)
			{
				if (levelGridNode3.GetIndex() == levelGridNode2.GetIndex())
				{
					hit.node = levelGridNode3;
					hit.point = (Vector3)levelGridNode3.position;
					return true;
				}
				num = Math.Abs(ob.x - @int.x);
				num2 = Math.Abs(ob.z - @int.z);
				int num3 = 0;
				if (num >= num2)
				{
					num3 = ((@int.x <= ob.x) ? 3 : 1);
				}
				else if (num2 > num)
				{
					num3 = ((@int.z <= ob.z) ? 0 : 2);
				}
				if (!this.CheckConnection(levelGridNode3, num3))
				{
					hit.node = levelGridNode3;
					hit.point = (Vector3)levelGridNode3.position;
					return true;
				}
				levelGridNode4 = (this.nodes[levelGridNode3.GetIndex() + this.neighbourOffsets[num3] + this.width * this.depth * levelGridNode3.GetConnectionValue(num3)] as LevelGridNode);
				if (!levelGridNode4.walkable)
				{
					hit.node = levelGridNode4;
					hit.point = (Vector3)levelGridNode4.position;
					return true;
				}
				ob = (Int3)base.inverseMatrix.MultiplyPoint3x4((Vector3)levelGridNode4.position);
			}
			return false;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000201EE File Offset: 0x0001E5EE
		public bool CheckConnection(LevelGridNode node, int dir)
		{
			return node.GetConnection(dir);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000201F7 File Offset: 0x0001E5F7
		public new void SerializeSettings(AstarSerializer serializer)
		{
			this.SerializeSettings(serializer);
			serializer.AddValue("mergeSpanRange", this.mergeSpanRange);
			serializer.AddValue("characterHeight", this.characterHeight);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0002022C File Offset: 0x0001E62C
		public new void DeSerializeSettings(AstarSerializer serializer)
		{
			this.DeSerializeSettings(serializer);
			this.mergeSpanRange = (float)serializer.GetValue("mergeSpanRange", typeof(float), this.mergeSpanRange);
			this.characterHeight = (float)serializer.GetValue("characterHeight", typeof(float), this.characterHeight);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00020298 File Offset: 0x0001E698
		public override void OnDrawGizmos(bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			base.OnDrawGizmos(false);
			if (this.nodes == null || this.nodeCellIndices == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				LevelGridNode levelGridNode = this.nodes[i] as LevelGridNode;
				if (levelGridNode != null)
				{
					Gizmos.color = this.NodeColor(levelGridNode, AstarPath.active.debugPathData);
					if (AstarPath.active.showSearchTree && !base.InSearchTree(levelGridNode, AstarPath.active.debugPath))
					{
						return;
					}
					for (int j = 0; j < 4; j++)
					{
						int connectionValue = levelGridNode.GetConnectionValue(j);
						if (connectionValue != 255)
						{
							int num = levelGridNode.GetIndex() + this.neighbourOffsets[j] + this.width * this.depth * connectionValue;
							if (num >= 0 && num <= this.nodes.Length)
							{
								Node node = this.nodes[num];
								Gizmos.DrawLine((Vector3)levelGridNode.position, (Vector3)node.position);
							}
						}
					}
				}
			}
		}

		// Token: 0x040002FA RID: 762
		public int[] nodeCellIndices;

		// Token: 0x040002FB RID: 763
		private int layerCount;

		// Token: 0x040002FC RID: 764
		[JsonMember]
		public float mergeSpanRange = 0.5f;

		// Token: 0x040002FD RID: 765
		[JsonMember]
		public float characterHeight = 0.4f;
	}
}
