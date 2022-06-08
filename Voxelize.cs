using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.Threading;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class Voxelize
{
	// Token: 0x06000463 RID: 1123 RVA: 0x000286E4 File Offset: 0x00026AE4
	public Voxelize(float ch, float cs, float wc, float wh, float ms)
	{
		this.cellSize = cs;
		this.cellHeight = ch;
		this.walkableHeight = wh;
		this.walkableClimb = wc;
		this.maxSlope = ms;
		Voxelize.CellScale = new Vector3(this.cellSize, this.cellHeight, this.cellSize);
		Voxelize.CellScaleDivision = new Vector3(1f / this.cellSize, 1f / this.cellHeight, 1f / this.cellSize);
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x000287BB File Offset: 0x00026BBB
	public void OnGUI()
	{
		GUI.Label(new Rect(5f, 5f, 200f, (float)Screen.height), this.debugString);
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x000287E4 File Offset: 0x00026BE4
	public static Bounds CollectMeshes(MeshFilter[] filters, RecastGraph.ExtraMesh[] extraMeshes, Bounds bounds, out Vector3[] verts, out int[] tris)
	{
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		foreach (MeshFilter meshFilter in filters)
		{
			if (!(meshFilter.GetComponent<Renderer>() == null) && !(meshFilter.sharedMesh == null))
			{
				if (meshFilter.GetComponent<Renderer>().bounds.Intersects(bounds))
				{
					Vector3[] vertices = meshFilter.sharedMesh.vertices;
					int[] triangles = meshFilter.sharedMesh.triangles;
					Matrix4x4 localToWorldMatrix = meshFilter.transform.localToWorldMatrix;
					for (int j = 0; j < vertices.Length; j++)
					{
						vertices[j] = localToWorldMatrix.MultiplyPoint3x4(vertices[j]);
					}
					for (int k = 0; k < triangles.Length; k++)
					{
						triangles[k] += list.Count;
					}
					list.AddRange(vertices);
					list2.AddRange(triangles);
				}
			}
		}
		if (extraMeshes != null)
		{
			foreach (RecastGraph.ExtraMesh extraMesh in extraMeshes)
			{
				if (extraMesh.bounds.Intersects(bounds))
				{
					Vector3[] vertices2 = extraMesh.vertices;
					int[] triangles2 = extraMesh.triangles;
					if (list2.Capacity < list2.Count + triangles2.Length)
					{
						list2.Capacity = Mathf.Max(2 * list2.Capacity, list2.Count + triangles2.Length);
					}
					int count = list.Count;
					for (int m = 0; m < triangles2.Length; m++)
					{
						list2.Add(triangles2[m] + count);
					}
					if (extraMesh.matrix.isIdentity)
					{
						list.AddRange(vertices2);
					}
					else
					{
						Matrix4x4 matrix = extraMesh.matrix;
						for (int n = 0; n < vertices2.Length; n++)
						{
							list.Add(matrix.MultiplyPoint3x4(vertices2[n]));
						}
					}
				}
			}
		}
		verts = list.ToArray();
		tris = list2.ToArray();
		return bounds;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00028A20 File Offset: 0x00026E20
	public VoxelArea VoxelizeMesh(MeshFilter[] filters, RecastGraph.ExtraMesh[] extraMeshes = null)
	{
		Vector3[] array;
		int[] array2;
		Bounds b = Voxelize.CollectMeshes(filters, extraMeshes, this.forcedBounds, out array, out array2);
		Voxelize.CellScale = new Vector3(this.cellSize, this.cellHeight, this.cellSize);
		Voxelize.CellScaleDivision = new Vector3(1f / this.cellSize, 1f / this.cellHeight, 1f / this.cellSize);
		float num = 1f / this.cellSize;
		float num2 = 1f / this.cellHeight;
		this.voxelWalkableHeight = (uint)(this.walkableHeight / this.cellHeight);
		this.voxelWalkableClimb = Mathf.RoundToInt(this.walkableClimb / this.cellHeight);
		Vector3 min = b.min;
		this.voxelOffset = min;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] -= b.min;
			Vector3[] array3 = array;
			int num3 = i;
			array3[num3].x = array3[num3].x * num;
			Vector3[] array4 = array;
			int num4 = i;
			array4[num4].y = array4[num4].y * num2;
			Vector3[] array5 = array;
			int num5 = i;
			array5[num5].z = array5[num5].z * num;
		}
		b.size = Vector3.Scale(b.size, Voxelize.CellScaleDivision);
		this.voxelArea = new VoxelArea(b);
		float num6 = Mathf.Cos(this.maxSlope * 0.017453292f);
		float[] array6 = new float[9];
		float[] array7 = new float[21];
		float[] array8 = new float[21];
		float[] array9 = new float[21];
		float[] array10 = new float[21];
		for (int j = 0; j < array2.Length; j += 3)
		{
			Vector3 vector = array[array2[j]];
			Vector3 vector2 = array[array2[j + 1]];
			Vector3 vector3 = array[array2[j + 2]];
			int num7 = Mathf.FloorToInt(Utility.Min(vector.x, vector2.x, vector3.x));
			int value = Mathf.FloorToInt(Utility.Min(vector.z, vector2.z, vector3.z));
			int num8 = Mathf.CeilToInt(Utility.Max(vector.x, vector2.x, vector3.x));
			int value2 = Mathf.CeilToInt(Utility.Max(vector.z, vector2.z, vector3.z));
			num7 = Mathf.Clamp(num7, 0, this.voxelArea.width - 1);
			num8 = Mathf.Clamp(num8, 0, this.voxelArea.width - 1);
			value = Mathf.Clamp(value, 0, this.voxelArea.depth - 1);
			value2 = Mathf.Clamp(value2, 0, this.voxelArea.depth - 1);
			float num9 = Vector3.Dot(Vector3.Cross(vector2 - vector, vector3 - vector).normalized, Vector3.up);
			int area;
			if (num9 < num6)
			{
				area = Voxelize.UnwalkableArea;
			}
			else
			{
				area = 1;
			}
			Utility.CopyVector(array6, 0, vector);
			Utility.CopyVector(array6, 3, vector2);
			Utility.CopyVector(array6, 6, vector3);
			for (int k = num7; k <= num8; k++)
			{
				int num10 = Utility.ClipPolygon(array6, 3, array7, 1f, (float)(-(float)k) + 0.5f, 0);
				if (num10 >= 3)
				{
					num10 = Utility.ClipPolygon(array7, num10, array8, -1f, (float)k + 0.5f, 0);
					if (num10 >= 3)
					{
						float num11 = array8[2];
						float num12 = array8[2];
						for (int l = 1; l < num10; l++)
						{
							float b2 = array8[l * 3 + 2];
							num11 = Mathf.Min(num11, b2);
							num12 = Mathf.Max(num12, b2);
						}
						int num13 = Mathfx.Clamp(Mathf.RoundToInt(num11), 0, this.voxelArea.depth - 1);
						int num14 = Mathfx.Clamp(Mathf.RoundToInt(num12), 0, this.voxelArea.depth - 1);
						for (int m = num13; m <= num14; m++)
						{
							int num15 = Utility.ClipPolygon(array8, num10, array9, 1f, (float)(-(float)m) + 0.5f, 2);
							if (num15 >= 3)
							{
								num15 = Utility.ClipPolygonY(array9, num15, array10, -1f, (float)m + 0.5f, 2);
								if (num15 >= 3)
								{
									float num16 = array10[1];
									float num17 = array10[1];
									for (int n = 1; n < num15; n++)
									{
										float b3 = array10[n * 3 + 1];
										num16 = Mathf.Min(num16, b3);
										num17 = Mathf.Max(num17, b3);
									}
									int num18 = Mathf.CeilToInt(num17);
									if (this.includeOutOfBounds || num18 >= 0)
									{
										num18 = ((num18 >= 0) ? num18 : 0);
										int num19 = Mathf.FloorToInt(num16 + 1f);
										this.voxelArea.cells[m * this.voxelArea.width + k].AddSpan((uint)((num19 < 0) ? 0 : num19), (uint)num18, area, this.voxelWalkableClimb);
									}
								}
							}
						}
					}
				}
			}
		}
		this.FilterLedges(this.voxelWalkableHeight, this.voxelWalkableClimb, this.cellSize, this.cellHeight, min);
		this.FilterLowHeightSpans(this.voxelWalkableHeight, this.cellSize, this.cellHeight, min);
		this.BuildCompactField();
		this.BuildVoxelConnections();
		return this.voxelArea;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00028FB0 File Offset: 0x000273B0
	public void BuildCompactField()
	{
		int spanCount = this.voxelArea.GetSpanCount();
		this.voxelArea.compactSpans = new CompactVoxelSpan[spanCount];
		this.voxelArea.areaTypes = new int[spanCount];
		uint num = 0U;
		int width = this.voxelArea.width;
		int depth = this.voxelArea.depth;
		int num2 = width * depth;
		int i = 0;
		int num3 = 0;
		while (i < num2)
		{
			for (int j = 0; j < width; j++)
			{
				VoxelSpan voxelSpan = this.voxelArea.cells[j + i].firstSpan;
				if (voxelSpan == null)
				{
					this.voxelArea.compactCells[j + i] = new CompactVoxelCell(0U, 0U);
				}
				else
				{
					uint i2 = num;
					uint num4 = 0U;
					while (voxelSpan != null)
					{
						if (voxelSpan.area != 0)
						{
							int top = (int)voxelSpan.top;
							int num5 = (voxelSpan.next == null) ? VoxelArea.MaxHeightInt : ((int)voxelSpan.next.bottom);
							this.voxelArea.compactSpans[(int)((UIntPtr)num)] = new CompactVoxelSpan((ushort)Mathfx.Clamp(top, 0, 65535), (uint)Mathfx.Clamp(num5 - top, 0, 255));
							this.voxelArea.areaTypes[(int)((UIntPtr)num)] = voxelSpan.area;
							num += 1U;
							num4 += 1U;
						}
						voxelSpan = voxelSpan.next;
					}
					this.voxelArea.compactCells[j + i] = new CompactVoxelCell(i2, num4);
				}
			}
			i += width;
			num3++;
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0002915C File Offset: 0x0002755C
	public void BuildVoxelConnections()
	{
		int wd = this.voxelArea.width * this.voxelArea.depth;
		Parallel.For(0, this.voxelArea.depth, delegate(int pz)
		{
			int num = pz * this.voxelArea.width;
			for (int i = 0; i < this.voxelArea.width; i++)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[i + num];
				Vector3 vector = new Vector3((float)i, 0f, (float)pz) * this.cellSize + this.voxelOffset;
				int j = (int)compactVoxelCell.index;
				int num2 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (j < num2)
				{
					CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[j];
					this.voxelArea.compactSpans[j].con = uint.MaxValue;
					vector.y = (float)compactVoxelSpan.y * this.cellHeight + this.voxelOffset.y;
					for (int k = 0; k < 4; k++)
					{
						int num3 = i + this.voxelArea.DirectionX[k];
						int num4 = num + this.voxelArea.DirectionZ[k];
						if (num3 >= 0 && num4 >= 0 && num4 < wd && num3 < this.voxelArea.width)
						{
							CompactVoxelCell compactVoxelCell2 = this.voxelArea.compactCells[num3 + num4];
							int l = (int)compactVoxelCell2.index;
							int num5 = (int)(compactVoxelCell2.index + compactVoxelCell2.count);
							while (l < num5)
							{
								CompactVoxelSpan compactVoxelSpan2 = this.voxelArea.compactSpans[l];
								int num6 = (int)Mathfx.Max(compactVoxelSpan.y, compactVoxelSpan2.y);
								int num7 = Mathfx.Min((int)((uint)compactVoxelSpan.y + compactVoxelSpan.h), (int)((uint)compactVoxelSpan2.y + compactVoxelSpan2.h));
								if ((long)(num7 - num6) >= (long)((ulong)this.voxelWalkableHeight) && Mathfx.Abs((int)(compactVoxelSpan2.y - compactVoxelSpan.y)) <= this.voxelWalkableClimb)
								{
									uint num8 = (uint)(l - (int)compactVoxelCell2.index);
									if ((ulong)num8 > (ulong)((long)Voxelize.MaxLayers))
									{
										Debug.LogError("Too many layers");
									}
									else
									{
										this.voxelArea.compactSpans[j].SetConnection(k, num8);
									}
								}
								l++;
							}
						}
					}
					j++;
				}
			}
		});
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x000291B0 File Offset: 0x000275B0
	public void BuildPolyMesh(VoxelContourSet cset, int nvp, out VoxelMesh mesh)
	{
		nvp = 3;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < cset.conts.Length; i++)
		{
			if (cset.conts[i].nverts >= 3)
			{
				num += cset.conts[i].nverts;
				num2 += cset.conts[i].nverts - 2;
				num3 = Mathfx.Max(num3, cset.conts[i].nverts);
			}
		}
		if (num >= 65534)
		{
			Debug.LogWarning("To many vertices for unity to render - Unity might screw up rendering, but hopefully the navmesh will work ok");
		}
		Int3[] array = new Int3[num];
		int[] array2 = new int[num2 * nvp];
		Memory.MemSet<int>(array2, 255, 4);
		int[] array3 = new int[num3];
		int[] array4 = new int[num3 * 3];
		int num4 = 0;
		int num5 = 0;
		for (int j = 0; j < cset.conts.Length; j++)
		{
			VoxelContour voxelContour = cset.conts[j];
			if (voxelContour.nverts >= 3)
			{
				for (int k = 0; k < voxelContour.nverts; k++)
				{
					array3[k] = k;
					voxelContour.verts[k * 4 + 2] /= this.voxelArea.width;
				}
				int num6 = this.Triangulate(voxelContour.nverts, voxelContour.verts, ref array3, ref array4);
				int num7 = num4;
				for (int l = 0; l < num6 * 3; l++)
				{
					array2[num5] = array4[l] + num7;
					num5++;
				}
				for (int m = 0; m < voxelContour.nverts; m++)
				{
					array[num4] = new Int3(voxelContour.verts[m * 4], voxelContour.verts[m * 4 + 1], voxelContour.verts[m * 4 + 2]);
					num4++;
				}
			}
		}
		mesh = default(VoxelMesh);
		Int3[] array5 = new Int3[num4];
		for (int n = 0; n < num4; n++)
		{
			array5[n] = array[n];
		}
		int[] array6 = new int[num5];
		Buffer.BlockCopy(array2, 0, array6, 0, num5 * 4);
		mesh.verts = array5;
		mesh.tris = array6;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00029424 File Offset: 0x00027824
	public void RemoveDegenerateSegments(List<int> simplified)
	{
		for (int i = 0; i < simplified.Count / 4; i++)
		{
			int num = i + 1;
			if (num >= simplified.Count / 4)
			{
				num = 0;
			}
			if (simplified[i * 4] == simplified[num * 4] && simplified[i * 4 + 2] == simplified[num * 4 + 2])
			{
				simplified.RemoveRange(i, 4);
			}
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00029498 File Offset: 0x00027898
	public static bool Diagonal(int i, int j, int n, int[] verts, int[] indices)
	{
		return Voxelize.InCone(i, j, n, verts, indices) && Voxelize.Diagonalie(i, j, n, verts, indices);
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x000294B8 File Offset: 0x000278B8
	public static bool InCone(int i, int j, int n, int[] verts, int[] indices)
	{
		int num = (indices[i] & 268435455) * 4;
		int num2 = (indices[j] & 268435455) * 4;
		int c = (indices[Voxelize.Next(i, n)] & 268435455) * 4;
		int num3 = (indices[Voxelize.Prev(i, n)] & 268435455) * 4;
		if (Voxelize.LeftOn(num3, num, c, verts))
		{
			return Voxelize.Left(num, num2, num3, verts) && Voxelize.Left(num2, num, c, verts);
		}
		return !Voxelize.LeftOn(num, num2, c, verts) || !Voxelize.LeftOn(num2, num, num3, verts);
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0002954B File Offset: 0x0002794B
	public static bool Left(int a, int b, int c, int[] verts)
	{
		return Voxelize.Area2(a, b, c, verts) < 0;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00029559 File Offset: 0x00027959
	public static bool LeftOn(int a, int b, int c, int[] verts)
	{
		return Voxelize.Area2(a, b, c, verts) <= 0;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0002956A File Offset: 0x0002796A
	public static bool Collinear(int a, int b, int c, int[] verts)
	{
		return Voxelize.Area2(a, b, c, verts) == 0;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00029578 File Offset: 0x00027978
	public static int Area2(int a, int b, int c, int[] verts)
	{
		return (verts[b] - verts[a]) * (verts[c + 2] - verts[a + 2]) - (verts[c] - verts[a]) * (verts[b + 2] - verts[a + 2]);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x000295A4 File Offset: 0x000279A4
	private static bool Diagonalie(int i, int j, int n, int[] verts, int[] indices)
	{
		int a = (indices[i] & 268435455) * 4;
		int num = (indices[j] & 268435455) * 4;
		for (int k = 0; k < n; k++)
		{
			int num2 = Voxelize.Next(k, n);
			if (k != i && num2 != i && k != j && num2 != j)
			{
				int num3 = (indices[k] & 268435455) * 4;
				int num4 = (indices[num2] & 268435455) * 4;
				if (!Voxelize.Vequal(a, num3, verts) && !Voxelize.Vequal(num, num3, verts) && !Voxelize.Vequal(a, num4, verts) && !Voxelize.Vequal(num, num4, verts))
				{
					if (Voxelize.Intersect(a, num, num3, num4, verts))
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0002966E File Offset: 0x00027A6E
	public static bool Xorb(bool x, bool y)
	{
		return !x ^ !y;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0002967C File Offset: 0x00027A7C
	public static bool IntersectProp(int a, int b, int c, int d, int[] verts)
	{
		return !Voxelize.Collinear(a, b, c, verts) && !Voxelize.Collinear(a, b, d, verts) && !Voxelize.Collinear(c, d, a, verts) && !Voxelize.Collinear(c, d, b, verts) && Voxelize.Xorb(Voxelize.Left(a, b, c, verts), Voxelize.Left(a, b, d, verts)) && Voxelize.Xorb(Voxelize.Left(c, d, a, verts), Voxelize.Left(c, d, b, verts));
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00029704 File Offset: 0x00027B04
	private static bool Between(int a, int b, int c, int[] verts)
	{
		if (!Voxelize.Collinear(a, b, c, verts))
		{
			return false;
		}
		if (verts[a] != verts[b])
		{
			return (verts[a] <= verts[c] && verts[c] <= verts[b]) || (verts[a] >= verts[c] && verts[c] >= verts[b]);
		}
		return (verts[a + 2] <= verts[c + 2] && verts[c + 2] <= verts[b + 2]) || (verts[a + 2] >= verts[c + 2] && verts[c + 2] >= verts[b + 2]);
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x000297A4 File Offset: 0x00027BA4
	private static bool Intersect(int a, int b, int c, int d, int[] verts)
	{
		return Voxelize.IntersectProp(a, b, c, d, verts) || (Voxelize.Between(a, b, c, verts) || Voxelize.Between(a, b, d, verts) || Voxelize.Between(c, d, a, verts) || Voxelize.Between(c, d, b, verts));
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00029802 File Offset: 0x00027C02
	private static bool Vequal(int a, int b, int[] verts)
	{
		return verts[a] == verts[b] && verts[a + 2] == verts[b + 2];
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0002981E File Offset: 0x00027C1E
	public static int Prev(int i, int n)
	{
		return (i - 1 < 0) ? (n - 1) : (i - 1);
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00029834 File Offset: 0x00027C34
	public static int Next(int i, int n)
	{
		return (i + 1 >= n) ? 0 : (i + 1);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00029848 File Offset: 0x00027C48
	public int Triangulate(int n, int[] verts, ref int[] indices, ref int[] tris)
	{
		int num = 0;
		int[] array = tris;
		int num2 = 0;
		for (int i = 0; i < n; i++)
		{
			int num3 = Voxelize.Next(i, n);
			int j = Voxelize.Next(num3, n);
			if (Voxelize.Diagonal(i, j, n, verts, indices))
			{
				indices[num3] |= 1073741824;
			}
		}
		while (n > 3)
		{
			int num4 = -1;
			int num5 = -1;
			for (int k = 0; k < n; k++)
			{
				int num6 = Voxelize.Next(k, n);
				if ((indices[num6] & 1073741824) != 0)
				{
					int num7 = (indices[k] & 268435455) * 4;
					int num8 = (indices[Voxelize.Next(num6, n)] & 268435455) * 4;
					int num9 = verts[num8] - verts[num7];
					int num10 = verts[num8 + 2] - verts[num7 + 2];
					int num11 = num9 * num9 + num10 * num10;
					if (num4 < 0 || num11 < num4)
					{
						num4 = num11;
						num5 = k;
					}
				}
			}
			if (num5 == -1)
			{
				Debug.LogError("This should not happen");
				return -num;
			}
			int num12 = num5;
			int num13 = Voxelize.Next(num12, n);
			int num14 = Voxelize.Next(num13, n);
			array[num2] = (indices[num12] & 268435455);
			num2++;
			array[num2] = (indices[num13] & 268435455);
			num2++;
			array[num2] = (indices[num14] & 268435455);
			num2++;
			num++;
			n--;
			for (int l = num13; l < n; l++)
			{
				indices[l] = indices[l + 1];
			}
			if (num13 >= n)
			{
				num13 = 0;
			}
			num12 = Voxelize.Prev(num13, n);
			if (Voxelize.Diagonal(Voxelize.Prev(num12, n), num13, n, verts, indices))
			{
				indices[num12] |= 1073741824;
			}
			else
			{
				indices[num12] &= 268435455;
			}
			if (Voxelize.Diagonal(num12, Voxelize.Next(num13, n), n, verts, indices))
			{
				indices[num13] |= 1073741824;
			}
			else
			{
				indices[num13] &= 268435455;
			}
		}
		array[num2] = (indices[0] & 268435455);
		num2++;
		array[num2] = (indices[1] & 268435455);
		num2++;
		array[num2] = (indices[2] & 268435455);
		num2++;
		return num + 1;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00029AAC File Offset: 0x00027EAC
	public void DrawLine(int a, int b, int[] indices, int[] verts, Color col)
	{
		int num = (indices[a] & 268435455) * 4;
		int num2 = (indices[b] & 268435455) * 4;
		Debug.DrawLine(this.ConvertPosCorrZ(verts[num], verts[num + 1], verts[num + 2]), this.ConvertPosCorrZ(verts[num2], verts[num2 + 1], verts[num2 + 2]), col);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00029B04 File Offset: 0x00027F04
	public void SimplifyContour(List<int> verts, List<int> simplified, float maxError, int maxEdgeLenght, int buildFlags)
	{
		bool flag = false;
		for (int i = 0; i < verts.Count; i += 4)
		{
			if ((verts[i + 3] & Voxelize.ContourRegMask) != 0)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			int j = 0;
			int num = verts.Count / 4;
			while (j < num)
			{
				int num2 = (j + 1) % num;
				bool flag2 = (verts[j * 4 + 3] & Voxelize.ContourRegMask) != (verts[num2 * 4 + 3] & Voxelize.ContourRegMask);
				bool flag3 = (verts[j * 4 + 3] & Voxelize.RC_AREA_BORDER) != (verts[num2 * 4 + 3] & Voxelize.RC_AREA_BORDER);
				if (flag2 || flag3)
				{
					simplified.Add(verts[j * 4]);
					simplified.Add(verts[j * 4 + 1]);
					simplified.Add(verts[j * 4 + 2]);
					simplified.Add(j);
				}
				j++;
			}
		}
		if (simplified.Count == 0)
		{
			int num3 = verts[0];
			int item = verts[1];
			int num4 = verts[2];
			int item2 = 0;
			int num5 = verts[0];
			int item3 = verts[1];
			int num6 = verts[2];
			int item4 = 0;
			for (int k = 0; k < verts.Count; k += 4)
			{
				int num7 = verts[k];
				int num8 = verts[k + 1];
				int num9 = verts[k + 2];
				if (num7 < num3 || (num7 == num3 && num9 < num4))
				{
					num3 = num7;
					item = num8;
					num4 = num9;
					item2 = k / 4;
				}
				if (num7 > num5 || (num7 == num5 && num9 > num6))
				{
					num5 = num7;
					item3 = num8;
					num6 = num9;
					item4 = k / 4;
				}
			}
			simplified.Add(num3);
			simplified.Add(item);
			simplified.Add(num4);
			simplified.Add(item2);
			simplified.Add(num5);
			simplified.Add(item3);
			simplified.Add(num6);
			simplified.Add(item4);
		}
		int num10 = verts.Count / 4;
		maxError *= maxError;
		int l = 0;
		while (l < simplified.Count / 4)
		{
			int num11 = (l + 1) % (simplified.Count / 4);
			int num12 = simplified[l * 4];
			int num13 = simplified[l * 4 + 2];
			int num14 = simplified[l * 4 + 3];
			int num15 = simplified[num11 * 4];
			int num16 = simplified[num11 * 4 + 2];
			int num17 = simplified[num11 * 4 + 3];
			float num18 = 0f;
			int num19 = -1;
			int num20;
			int num21;
			int num22;
			if (num15 > num12 || (num15 == num12 && num16 > num13))
			{
				num20 = 1;
				num21 = (num14 + num20) % num10;
				num22 = num17;
			}
			else
			{
				num20 = num10 - 1;
				num21 = (num17 + num20) % num10;
				num22 = num14;
			}
			if ((verts[num21 * 4 + 3] & Voxelize.ContourRegMask) == 0 || (verts[num21 * 4 + 3] & Voxelize.RC_AREA_BORDER) == Voxelize.RC_AREA_BORDER)
			{
				while (num21 != num22)
				{
					float num23 = Mathfx.DistancePointSegment(verts[num21 * 4], verts[num21 * 4 + 2] / this.voxelArea.width, num12, num13 / this.voxelArea.width, num15, num16 / this.voxelArea.width);
					if (num23 > num18)
					{
						num18 = num23;
						num19 = num21;
					}
					num21 = (num21 + num20) % num10;
				}
			}
			if (num19 != -1 && num18 > maxError)
			{
				simplified.AddRange(new int[4]);
				int num24 = simplified.Count / 4;
				for (int m = num24 - 1; m > l; m--)
				{
					simplified[m * 4] = simplified[(m - 1) * 4];
					simplified[m * 4 + 1] = simplified[(m - 1) * 4 + 1];
					simplified[m * 4 + 2] = simplified[(m - 1) * 4 + 2];
					simplified[m * 4 + 3] = simplified[(m - 1) * 4 + 3];
				}
				simplified[(l + 1) * 4] = verts[num19 * 4];
				simplified[(l + 1) * 4 + 1] = verts[num19 * 4 + 1];
				simplified[(l + 1) * 4 + 2] = verts[num19 * 4 + 2];
				simplified[(l + 1) * 4 + 3] = num19;
			}
			else
			{
				l++;
			}
		}
		float num25 = this.maxEdgeLength / this.cellSize;
		if (num25 > 0f && (buildFlags & (Voxelize.RC_CONTOUR_TESS_WALL_EDGES | Voxelize.RC_CONTOUR_TESS_AREA_EDGES)) != 0)
		{
			int n = 0;
			while (n < simplified.Count / 4)
			{
				if (simplified.Count / 4 > 200)
				{
					break;
				}
				int num26 = (n + 1) % (simplified.Count / 4);
				int num27 = simplified[n * 4];
				int num28 = simplified[n * 4 + 2];
				int num29 = simplified[n * 4 + 3];
				int num30 = simplified[num26 * 4];
				int num31 = simplified[num26 * 4 + 2];
				int num32 = simplified[num26 * 4 + 3];
				int num33 = -1;
				int num34 = (num29 + 1) % num10;
				bool flag4 = false;
				if ((buildFlags & Voxelize.RC_CONTOUR_TESS_WALL_EDGES) == 1 && (verts[num34 * 4 + 3] & Voxelize.ContourRegMask) == 0)
				{
					flag4 = true;
				}
				if ((buildFlags & Voxelize.RC_CONTOUR_TESS_AREA_EDGES) == 1 && (verts[num34 * 4 + 3] & Voxelize.RC_AREA_BORDER) == 1)
				{
					flag4 = true;
				}
				if (flag4)
				{
					int num35 = num30 - num27;
					int num36 = num31 / this.voxelArea.width - num28 / this.voxelArea.width;
					if ((float)(num35 * num35 + num36 * num36) > num25 * num25)
					{
						if (num30 > num27 || (num30 == num27 && num31 > num28))
						{
							int num37 = (num32 >= num29) ? (num32 - num29) : (num32 + num10 - num29);
							num33 = (num29 + num37 / 2) % num10;
						}
						else
						{
							int num38 = (num32 >= num29) ? (num32 - num29) : (num32 + num10 - num29);
							num33 = (num29 + (num38 + 1) / 2) % num10;
						}
					}
				}
				if (num33 != -1)
				{
					simplified.AddRange(new int[4]);
					int num39 = simplified.Count / 4;
					for (int num40 = num39 - 1; num40 > n; num40--)
					{
						simplified[num40 * 4] = simplified[(num40 - 1) * 4];
						simplified[num40 * 4 + 1] = simplified[(num40 - 1) * 4 + 1];
						simplified[num40 * 4 + 2] = simplified[(num40 - 1) * 4 + 2];
						simplified[num40 * 4 + 3] = simplified[(num40 - 1) * 4 + 3];
					}
					simplified[(n + 1) * 4] = verts[num33 * 4];
					simplified[(n + 1) * 4 + 1] = verts[num33 * 4 + 1];
					simplified[(n + 1) * 4 + 2] = verts[num33 * 4 + 2];
					simplified[(n + 1) * 4 + 3] = num33;
				}
				else
				{
					n++;
				}
			}
		}
		for (int num41 = 0; num41 < simplified.Count / 4; num41++)
		{
			int num42 = (simplified[num41 * 4 + 3] + 1) % num10;
			int num43 = simplified[num41 * 4 + 3];
			simplified[num41 * 4 + 3] = ((verts[num42 * 4 + 3] & Voxelize.ContourRegMask) | (verts[num43 * 4 + 3] & Voxelize.RC_BORDER_VERTEX));
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0002A2EC File Offset: 0x000286EC
	public void WalkContour(int x, int z, int i, int[] flags, List<int> verts)
	{
		int num = 0;
		while ((flags[i] & 1 << num) == 0)
		{
			num++;
		}
		int num2 = num;
		int num3 = i;
		int num4 = this.voxelArea.areaTypes[i];
		int num5 = 0;
		while (num5++ < 40000)
		{
			if ((flags[i] & 1 << num) != 0)
			{
				bool flag = false;
				bool flag2 = false;
				int num6 = x;
				int cornerHeight = this.GetCornerHeight(x, z, i, num, ref flag);
				int num7 = z;
				if (num != 0)
				{
					if (num != 1)
					{
						if (num == 2)
						{
							num6++;
						}
					}
					else
					{
						num6++;
						num7 += this.voxelArea.width;
					}
				}
				else
				{
					num7 += this.voxelArea.width;
				}
				int num8 = 0;
				CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[i];
				if ((long)compactVoxelSpan.GetConnection(num) != (long)((ulong)Voxelize.NotConnected))
				{
					int num9 = x + this.voxelArea.DirectionX[num];
					int num10 = z + this.voxelArea.DirectionZ[num];
					int num11 = (int)(this.voxelArea.compactCells[num9 + num10].index + (uint)compactVoxelSpan.GetConnection(num));
					num8 = this.voxelArea.compactSpans[num11].reg;
					if (num4 != this.voxelArea.areaTypes[num11])
					{
						flag2 = true;
					}
				}
				if (flag)
				{
					num8 |= Voxelize.RC_BORDER_VERTEX;
				}
				if (flag2)
				{
					num8 |= Voxelize.RC_AREA_BORDER;
				}
				verts.Add(num6);
				verts.Add(cornerHeight);
				verts.Add(num7);
				verts.Add(num8);
				flags[i] &= ~(1 << num);
				num = (num + 1 & 3);
			}
			else
			{
				int num12 = -1;
				int num13 = x + this.voxelArea.DirectionX[num];
				int num14 = z + this.voxelArea.DirectionZ[num];
				CompactVoxelSpan compactVoxelSpan2 = this.voxelArea.compactSpans[i];
				if ((long)compactVoxelSpan2.GetConnection(num) != (long)((ulong)Voxelize.NotConnected))
				{
					CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[num13 + num14];
					num12 = (int)(compactVoxelCell.index + (uint)compactVoxelSpan2.GetConnection(num));
				}
				if (num12 == -1)
				{
					Debug.LogError("This should not happen");
					return;
				}
				x = num13;
				z = num14;
				i = num12;
				num = (num + 3 & 3);
			}
			if (num3 == i && num2 == num)
			{
				break;
			}
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0002A580 File Offset: 0x00028980
	public Vector3 ConvertPos(int x, int y, int z)
	{
		return Vector3.Scale(new Vector3((float)x + 0.5f, (float)y, (float)z / (float)this.voxelArea.width + 0.5f), Voxelize.CellScale) + this.voxelOffset;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0002A5C8 File Offset: 0x000289C8
	public Vector3 ConvertPosCorrZ(int x, int y, int z)
	{
		return Vector3.Scale(new Vector3((float)x, (float)y, (float)z), Voxelize.CellScale) + this.voxelOffset;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0002A5F8 File Offset: 0x000289F8
	public Vector3 ConvertPosWithoutOffset(int x, int y, int z)
	{
		return Vector3.Scale(new Vector3((float)x, (float)y, (float)z / (float)this.voxelArea.width), Voxelize.CellScale) + this.voxelOffset;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0002A634 File Offset: 0x00028A34
	public int GetCornerHeight(int x, int z, int i, int dir, ref bool isBorderVertex)
	{
		CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[i];
		int num = (int)compactVoxelSpan.y;
		int num2 = dir + 1 & 3;
		uint[] array = new uint[4];
		array[0] = (uint)(this.voxelArea.compactSpans[i].reg | this.voxelArea.areaTypes[i] << 16);
		if ((long)compactVoxelSpan.GetConnection(dir) != (long)((ulong)Voxelize.NotConnected))
		{
			int num3 = x + this.voxelArea.DirectionX[dir];
			int num4 = z + this.voxelArea.DirectionZ[dir];
			int num5 = (int)(this.voxelArea.compactCells[num3 + num4].index + (uint)compactVoxelSpan.GetConnection(dir));
			CompactVoxelSpan compactVoxelSpan2 = this.voxelArea.compactSpans[num5];
			num = Mathfx.Max(num, (int)compactVoxelSpan2.y);
			array[1] = (uint)(compactVoxelSpan2.reg | this.voxelArea.areaTypes[num5] << 16);
			if ((long)compactVoxelSpan2.GetConnection(num2) != (long)((ulong)Voxelize.NotConnected))
			{
				int num6 = num3 + this.voxelArea.DirectionX[num2];
				int num7 = num4 + this.voxelArea.DirectionZ[num2];
				int num8 = (int)(this.voxelArea.compactCells[num6 + num7].index + (uint)compactVoxelSpan2.GetConnection(num2));
				CompactVoxelSpan compactVoxelSpan3 = this.voxelArea.compactSpans[num8];
				num = Mathfx.Max(num, (int)compactVoxelSpan3.y);
				array[2] = (uint)(compactVoxelSpan3.reg | this.voxelArea.areaTypes[num8] << 16);
			}
		}
		if ((long)compactVoxelSpan.GetConnection(num2) != (long)((ulong)Voxelize.NotConnected))
		{
			int num9 = x + this.voxelArea.DirectionX[num2];
			int num10 = z + this.voxelArea.DirectionZ[num2];
			int num11 = (int)(this.voxelArea.compactCells[num9 + num10].index + (uint)compactVoxelSpan.GetConnection(num2));
			CompactVoxelSpan compactVoxelSpan4 = this.voxelArea.compactSpans[num11];
			num = Mathfx.Max(num, (int)compactVoxelSpan4.y);
			array[3] = (uint)(compactVoxelSpan4.reg | this.voxelArea.areaTypes[num11] << 16);
			if ((long)compactVoxelSpan4.GetConnection(dir) != (long)((ulong)Voxelize.NotConnected))
			{
				int num12 = num9 + this.voxelArea.DirectionX[dir];
				int num13 = num10 + this.voxelArea.DirectionZ[dir];
				int num14 = (int)(this.voxelArea.compactCells[num12 + num13].index + (uint)compactVoxelSpan4.GetConnection(dir));
				CompactVoxelSpan compactVoxelSpan5 = this.voxelArea.compactSpans[num14];
				num = Mathfx.Max(num, (int)compactVoxelSpan5.y);
				array[2] = (uint)(compactVoxelSpan5.reg | this.voxelArea.areaTypes[num14] << 16);
			}
		}
		for (int j = 0; j < 4; j++)
		{
			int num15 = j;
			int num16 = j + 1 & 3;
			int num17 = j + 2 & 3;
			int num18 = j + 3 & 3;
			bool flag = (array[num15] & array[num16] & (uint)Voxelize.BorderReg) != 0U && array[num15] == array[num16];
			bool flag2 = ((array[num17] | array[num18]) & (uint)Voxelize.BorderReg) == 0U;
			bool flag3 = array[num17] >> 16 == array[num18] >> 16;
			bool flag4 = array[num15] != 0U && array[num16] != 0U && array[num17] != 0U && array[num18] != 0U;
			if (flag && flag2 && flag3 && flag4)
			{
				isBorderVertex = true;
				break;
			}
		}
		return num;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0002A9E8 File Offset: 0x00028DE8
	public void BuildRegions()
	{
		int width = this.voxelArea.width;
		int depth = this.voxelArea.depth;
		int num = width * depth;
		int num2 = 8;
		int num3 = 3;
		int num4 = this.voxelArea.compactSpans.Length;
		List<int> list = ListPool<int>.Claim(1024);
		ushort num5 = 1;
		ushort[] array = new ushort[num4];
		ushort[] array2 = new ushort[num4];
		ushort[] array3 = new ushort[num4];
		ushort[] array4 = new ushort[num4];
		this.MarkRectWithRegion(0, num3, 0, depth, num5 | Voxelize.BorderReg, array);
		num5 += 1;
		this.MarkRectWithRegion(width - num3, width, 0, depth, num5 | Voxelize.BorderReg, array);
		num5 += 1;
		this.MarkRectWithRegion(0, width, 0, num3, num5 | Voxelize.BorderReg, array);
		num5 += 1;
		this.MarkRectWithRegion(0, width, depth - num3, depth, num5 | Voxelize.BorderReg, array);
		num5 += 1;
		uint num6 = (uint)(this.voxelArea.maxDistance + 1) & 4294967294U;
		int num7 = 0;
		while (num6 > 0U)
		{
			num6 = ((num6 < 2U) ? 0U : (num6 - 2U));
			if (this.ExpandRegions(num2, num6, array, array2, array3, array4, list) != array)
			{
				ushort[] array5 = array;
				array = array3;
				array3 = array5;
				array5 = array2;
				array2 = array4;
				array4 = array5;
			}
			int i = 0;
			int num8 = 0;
			while (i < num)
			{
				for (int j = 0; j < this.voxelArea.width; j++)
				{
					CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[i + j];
					int k = (int)compactVoxelCell.index;
					int num9 = (int)(compactVoxelCell.index + compactVoxelCell.count);
					while (k < num9)
					{
						if ((uint)this.voxelArea.dist[k] >= num6 && array[k] == 0 && this.voxelArea.areaTypes[k] != Voxelize.UnwalkableArea)
						{
							if (this.FloodRegion(j, i, k, num6, num5, array, array2, list))
							{
								num5 += 1;
							}
						}
						k++;
					}
				}
				i += width;
				num8++;
			}
			num7++;
		}
		if (this.ExpandRegions(num2 * 8, 0U, array, array2, array3, array4, list) != array)
		{
			array = array3;
		}
		this.voxelArea.maxRegions = (int)num5;
		for (int l = 0; l < this.voxelArea.compactSpans.Length; l++)
		{
			this.voxelArea.compactSpans[l].reg = (int)array[l];
		}
		ListPool<int>.Release(list);
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0002ACAC File Offset: 0x000290AC
	public void FilterSmallRegions(ushort[] reg, int minRegionSize, int maxRegions)
	{
		int[] array = new int[maxRegions + 1];
		int num = array.Length;
		foreach (int num2 in reg)
		{
			if (num2 < num)
			{
				array[num2]++;
			}
		}
		for (int j = 0; j < reg.Length; j++)
		{
			int num3 = (int)reg[j];
			if (num3 < num)
			{
				if (array[num3] < minRegionSize)
				{
					reg[j] = 0;
				}
			}
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0002AD30 File Offset: 0x00029130
	public Vector3 ConvertPosition(int x, int z, int i)
	{
		CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[i];
		return new Vector3((float)x * this.cellSize, (float)compactVoxelSpan.y * this.cellHeight, (float)z / (float)this.voxelArea.width * this.cellSize) + this.voxelOffset;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0002AD94 File Offset: 0x00029194
	public ushort[] ExpandRegions(int maxIterations, uint level, ushort[] srcReg, ushort[] srcDist, ushort[] dstReg, ushort[] dstDist, List<int> stack)
	{
		int width = this.voxelArea.width;
		int depth = this.voxelArea.depth;
		int num = width * depth;
		stack.Clear();
		int i = 0;
		int num2 = 0;
		while (i < num)
		{
			for (int j = 0; j < this.voxelArea.width; j++)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[i + j];
				int k = (int)compactVoxelCell.index;
				int num3 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (k < num3)
				{
					if ((uint)this.voxelArea.dist[k] >= level && srcReg[k] == 0 && this.voxelArea.areaTypes[k] != Voxelize.UnwalkableArea)
					{
						stack.Add(j);
						stack.Add(i);
						stack.Add(k);
					}
					k++;
				}
			}
			i += width;
			num2++;
		}
		int num4 = 0;
		if (stack.Count > 0)
		{
			for (;;)
			{
				int num5 = 0;
				Buffer.BlockCopy(srcReg, 0, dstReg, 0, srcReg.Length * 2);
				Buffer.BlockCopy(srcDist, 0, dstDist, 0, dstDist.Length * 2);
				for (int l = 0; l < stack.Count; l += 3)
				{
					int num6 = stack[l];
					int num7 = stack[l + 1];
					int num8 = stack[l + 2];
					if (num8 < 0)
					{
						num5++;
					}
					else
					{
						ushort num9 = srcReg[num8];
						ushort num10 = ushort.MaxValue;
						CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[num8];
						int num11 = this.voxelArea.areaTypes[num8];
						for (int m = 0; m < 4; m++)
						{
							if ((long)compactVoxelSpan.GetConnection(m) != (long)((ulong)Voxelize.NotConnected))
							{
								int num12 = num6 + this.voxelArea.DirectionX[m];
								int num13 = num7 + this.voxelArea.DirectionZ[m];
								int num14 = (int)(this.voxelArea.compactCells[num12 + num13].index + (uint)compactVoxelSpan.GetConnection(m));
								if (num11 == this.voxelArea.areaTypes[num14])
								{
									if (srcReg[num14] > 0 && (srcReg[num14] & Voxelize.BorderReg) == 0 && srcDist[num14] + 2 < num10)
									{
										num9 = srcReg[num14];
										num10 = srcDist[num14] + 2;
									}
								}
							}
						}
						if (num9 != 0)
						{
							stack[l + 2] = -1;
							dstReg[num8] = num9;
							dstDist[num8] = num10;
						}
						else
						{
							num5++;
						}
					}
				}
				ushort[] array = srcReg;
				srcReg = dstReg;
				dstReg = array;
				array = srcDist;
				srcDist = dstDist;
				dstDist = array;
				if (num5 * 3 == stack.Count)
				{
					break;
				}
				if (level > 0U)
				{
					num4++;
					if (num4 >= maxIterations)
					{
						break;
					}
				}
			}
		}
		return srcReg;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0002B09C File Offset: 0x0002949C
	public bool FloodRegion(int x, int z, int i, uint level, ushort r, ushort[] srcReg, ushort[] srcDist, List<int> stack)
	{
		int num = this.voxelArea.areaTypes[i];
		stack.Clear();
		stack.Add(x);
		stack.Add(z);
		stack.Add(i);
		srcReg[i] = r;
		srcDist[i] = 0;
		int num2 = (int)((level < 2U) ? 0U : (level - 2U));
		int num3 = 0;
		while (stack.Count > 0)
		{
			int num4 = stack[stack.Count - 1];
			stack.RemoveAt(stack.Count - 1);
			int num5 = stack[stack.Count - 1];
			stack.RemoveAt(stack.Count - 1);
			int num6 = stack[stack.Count - 1];
			stack.RemoveAt(stack.Count - 1);
			CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[num4];
			ushort num7 = 0;
			for (int j = 0; j < 4; j++)
			{
				if ((long)compactVoxelSpan.GetConnection(j) != (long)((ulong)Voxelize.NotConnected))
				{
					int num8 = num6 + this.voxelArea.DirectionX[j];
					int num9 = num5 + this.voxelArea.DirectionZ[j];
					int num10 = (int)(this.voxelArea.compactCells[num8 + num9].index + (uint)compactVoxelSpan.GetConnection(j));
					if (this.voxelArea.areaTypes[num10] == num)
					{
						ushort num11 = srcReg[num10];
						if ((num11 & Voxelize.BorderReg) != Voxelize.BorderReg)
						{
							if (num11 != 0 && num11 != r)
							{
								num7 = num11;
							}
							CompactVoxelSpan compactVoxelSpan2 = this.voxelArea.compactSpans[num10];
							int num12 = j + 1 & 3;
							if ((long)compactVoxelSpan2.GetConnection(num12) != (long)((ulong)Voxelize.NotConnected))
							{
								int num13 = num8 + this.voxelArea.DirectionX[num12];
								int num14 = num9 + this.voxelArea.DirectionZ[num12];
								int num15 = (int)(this.voxelArea.compactCells[num13 + num14].index + (uint)compactVoxelSpan2.GetConnection(num12));
								if (this.voxelArea.areaTypes[num15] == num)
								{
									num11 = srcReg[num15];
									if (num11 != 0 && num11 != r)
									{
										num7 = num11;
									}
								}
							}
						}
					}
				}
			}
			if (num7 != 0)
			{
				srcReg[num4] = 0;
			}
			else
			{
				num3++;
				for (int k = 0; k < 4; k++)
				{
					if ((long)compactVoxelSpan.GetConnection(k) != (long)((ulong)Voxelize.NotConnected))
					{
						int num16 = num6 + this.voxelArea.DirectionX[k];
						int num17 = num5 + this.voxelArea.DirectionZ[k];
						int num18 = (int)(this.voxelArea.compactCells[num16 + num17].index + (uint)compactVoxelSpan.GetConnection(k));
						if (this.voxelArea.areaTypes[num18] == num)
						{
							if ((int)this.voxelArea.dist[num18] >= num2 && srcReg[num18] == 0)
							{
								srcReg[num18] = r;
								srcDist[num18] = 0;
								stack.Add(num16);
								stack.Add(num17);
								stack.Add(num18);
							}
						}
					}
				}
			}
		}
		return num3 > 0;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0002B3F0 File Offset: 0x000297F0
	public void MarkRectWithRegion(int minx, int maxx, int minz, int maxz, ushort region, ushort[] srcReg)
	{
		int num = maxz * this.voxelArea.width;
		for (int i = minz * this.voxelArea.width; i < num; i += this.voxelArea.width)
		{
			for (int j = minx; j < maxx; j++)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[i + j];
				int k = (int)compactVoxelCell.index;
				int num2 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (k < num2)
				{
					if (this.voxelArea.areaTypes[k] != Voxelize.UnwalkableArea)
					{
						srcReg[k] = region;
					}
					k++;
				}
			}
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0002B4AC File Offset: 0x000298AC
	public void ErodeWalkableArea(int radius)
	{
		ushort[] array = new ushort[this.voxelArea.compactSpans.Length];
		Memory.MemSet<ushort>(array, ushort.MaxValue, 2);
		this.CalculateDistanceField(array);
		for (int i = 0; i < array.Length; i++)
		{
			if ((int)array[i] < radius)
			{
				this.voxelArea.areaTypes[i] = Voxelize.UnwalkableArea;
			}
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0002B510 File Offset: 0x00029910
	public void BuildDistanceField()
	{
		ushort[] array = new ushort[this.voxelArea.compactSpans.Length];
		Memory.MemSet<ushort>(array, ushort.MaxValue, 2);
		this.voxelArea.maxDistance = this.CalculateDistanceField(array);
		ushort[] array2 = new ushort[this.voxelArea.compactSpans.Length];
		array2 = this.BoxBlur(array, array2);
		this.voxelArea.dist = array2;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0002B578 File Offset: 0x00029978
	public ushort CalculateDistanceField(ushort[] src)
	{
		int num = this.voxelArea.width * this.voxelArea.depth;
		for (int i = 0; i < num; i += this.voxelArea.width)
		{
			for (int j = 0; j < this.voxelArea.width; j++)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[j + i];
				int k = (int)compactVoxelCell.index;
				int num2 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (k < num2)
				{
					CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[k];
					int num3 = this.voxelArea.areaTypes[k];
					int num4 = 0;
					for (int l = 0; l < 4; l++)
					{
						if ((long)compactVoxelSpan.GetConnection(l) != (long)((ulong)Voxelize.NotConnected))
						{
							int num5 = j + this.voxelArea.DirectionX[l];
							int num6 = i + this.voxelArea.DirectionZ[l];
							int num7 = (int)((ulong)this.voxelArea.compactCells[num5 + num6].index + (ulong)((long)compactVoxelSpan.GetConnection(l)));
							if (num3 == this.voxelArea.areaTypes[num7])
							{
								num4++;
							}
						}
					}
					if (num4 != 4)
					{
						src[k] = 0;
					}
					k++;
				}
			}
		}
		for (int m = 0; m < num; m += this.voxelArea.width)
		{
			for (int n = 0; n < this.voxelArea.width; n++)
			{
				CompactVoxelCell compactVoxelCell2 = this.voxelArea.compactCells[n + m];
				int num8 = (int)compactVoxelCell2.index;
				int num9 = (int)(compactVoxelCell2.index + compactVoxelCell2.count);
				while (num8 < num9)
				{
					CompactVoxelSpan compactVoxelSpan2 = this.voxelArea.compactSpans[num8];
					if ((long)compactVoxelSpan2.GetConnection(0) != (long)((ulong)Voxelize.NotConnected))
					{
						int num10 = n + this.voxelArea.DirectionX[0];
						int num11 = m + this.voxelArea.DirectionZ[0];
						int num12 = (int)((ulong)this.voxelArea.compactCells[num10 + num11].index + (ulong)((long)compactVoxelSpan2.GetConnection(0)));
						if (src[num12] + 2 < src[num8])
						{
							src[num8] = src[num12] + 2;
						}
						CompactVoxelSpan compactVoxelSpan3 = this.voxelArea.compactSpans[num12];
						if ((long)compactVoxelSpan3.GetConnection(3) != (long)((ulong)Voxelize.NotConnected))
						{
							int num13 = num10 + this.voxelArea.DirectionX[3];
							int num14 = num11 + this.voxelArea.DirectionZ[3];
							int num15 = (int)((ulong)this.voxelArea.compactCells[num13 + num14].index + (ulong)((long)compactVoxelSpan3.GetConnection(3)));
							if (src[num15] + 3 < src[num8])
							{
								src[num8] = src[num15] + 3;
							}
						}
					}
					if ((long)compactVoxelSpan2.GetConnection(3) != (long)((ulong)Voxelize.NotConnected))
					{
						int num16 = n + this.voxelArea.DirectionX[3];
						int num17 = m + this.voxelArea.DirectionZ[3];
						int num18 = (int)((ulong)this.voxelArea.compactCells[num16 + num17].index + (ulong)((long)compactVoxelSpan2.GetConnection(3)));
						if (src[num18] + 2 < src[num8])
						{
							src[num8] = src[num18] + 2;
						}
						CompactVoxelSpan compactVoxelSpan4 = this.voxelArea.compactSpans[num18];
						if ((long)compactVoxelSpan4.GetConnection(2) != (long)((ulong)Voxelize.NotConnected))
						{
							int num19 = num16 + this.voxelArea.DirectionX[2];
							int num20 = num17 + this.voxelArea.DirectionZ[2];
							int num21 = (int)((ulong)this.voxelArea.compactCells[num19 + num20].index + (ulong)((long)compactVoxelSpan4.GetConnection(2)));
							if (src[num21] + 3 < src[num8])
							{
								src[num8] = src[num21] + 3;
							}
						}
					}
					num8++;
				}
			}
		}
		for (int num22 = num - this.voxelArea.width; num22 >= 0; num22 -= this.voxelArea.width)
		{
			for (int num23 = this.voxelArea.width - 1; num23 >= 0; num23--)
			{
				CompactVoxelCell compactVoxelCell3 = this.voxelArea.compactCells[num23 + num22];
				int num24 = (int)compactVoxelCell3.index;
				int num25 = (int)(compactVoxelCell3.index + compactVoxelCell3.count);
				while (num24 < num25)
				{
					CompactVoxelSpan compactVoxelSpan5 = this.voxelArea.compactSpans[num24];
					if ((long)compactVoxelSpan5.GetConnection(2) != (long)((ulong)Voxelize.NotConnected))
					{
						int num26 = num23 + this.voxelArea.DirectionX[2];
						int num27 = num22 + this.voxelArea.DirectionZ[2];
						int num28 = (int)((ulong)this.voxelArea.compactCells[num26 + num27].index + (ulong)((long)compactVoxelSpan5.GetConnection(2)));
						if (src[num28] + 2 < src[num24])
						{
							src[num24] = src[num28] + 2;
						}
						CompactVoxelSpan compactVoxelSpan6 = this.voxelArea.compactSpans[num28];
						if ((long)compactVoxelSpan6.GetConnection(1) != (long)((ulong)Voxelize.NotConnected))
						{
							int num29 = num26 + this.voxelArea.DirectionX[1];
							int num30 = num27 + this.voxelArea.DirectionZ[1];
							int num31 = (int)((ulong)this.voxelArea.compactCells[num29 + num30].index + (ulong)((long)compactVoxelSpan6.GetConnection(1)));
							if (src[num31] + 3 < src[num24])
							{
								src[num24] = src[num31] + 3;
							}
						}
					}
					if ((long)compactVoxelSpan5.GetConnection(1) != (long)((ulong)Voxelize.NotConnected))
					{
						int num32 = num23 + this.voxelArea.DirectionX[1];
						int num33 = num22 + this.voxelArea.DirectionZ[1];
						int num34 = (int)((ulong)this.voxelArea.compactCells[num32 + num33].index + (ulong)((long)compactVoxelSpan5.GetConnection(1)));
						if (src[num34] + 2 < src[num24])
						{
							src[num24] = src[num34] + 2;
						}
						CompactVoxelSpan compactVoxelSpan7 = this.voxelArea.compactSpans[num34];
						if ((long)compactVoxelSpan7.GetConnection(0) != (long)((ulong)Voxelize.NotConnected))
						{
							int num35 = num32 + this.voxelArea.DirectionX[0];
							int num36 = num33 + this.voxelArea.DirectionZ[0];
							int num37 = (int)((ulong)this.voxelArea.compactCells[num35 + num36].index + (ulong)((long)compactVoxelSpan7.GetConnection(0)));
							if (src[num37] + 3 < src[num24])
							{
								src[num24] = src[num37] + 3;
							}
						}
					}
					num24++;
				}
			}
		}
		ushort num38 = 0;
		for (int num39 = 0; num39 < this.voxelArea.compactSpans.Length; num39++)
		{
			num38 = Mathfx.Max(src[num39], num38);
		}
		return num38;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0002BCA4 File Offset: 0x0002A0A4
	public ushort[] BoxBlur(ushort[] src, ushort[] dst)
	{
		ushort num = 20;
		int num2 = this.voxelArea.width * this.voxelArea.depth;
		for (int i = num2 - this.voxelArea.width; i >= 0; i -= this.voxelArea.width)
		{
			for (int j = this.voxelArea.width - 1; j >= 0; j--)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[j + i];
				int k = (int)compactVoxelCell.index;
				int num3 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (k < num3)
				{
					CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[k];
					ushort num4 = src[k];
					if (num4 < num)
					{
						dst[k] = num4;
					}
					else
					{
						int num5 = (int)num4;
						for (int l = 0; l < 4; l++)
						{
							if ((long)compactVoxelSpan.GetConnection(l) != (long)((ulong)Voxelize.NotConnected))
							{
								int num6 = j + this.voxelArea.DirectionX[l];
								int num7 = i + this.voxelArea.DirectionZ[l];
								int num8 = (int)((ulong)this.voxelArea.compactCells[num6 + num7].index + (ulong)((long)compactVoxelSpan.GetConnection(l)));
								num5 += (int)src[num8];
								CompactVoxelSpan compactVoxelSpan2 = this.voxelArea.compactSpans[num8];
								int num9 = l + 1 & 3;
								if ((long)compactVoxelSpan2.GetConnection(num9) != (long)((ulong)Voxelize.NotConnected))
								{
									int num10 = num6 + this.voxelArea.DirectionX[num9];
									int num11 = num7 + this.voxelArea.DirectionZ[num9];
									int num12 = (int)((ulong)this.voxelArea.compactCells[num10 + num11].index + (ulong)((long)compactVoxelSpan2.GetConnection(num9)));
									num5 += (int)src[num12];
								}
								else
								{
									num5 += (int)num4;
								}
							}
							else
							{
								num5 += (int)(num4 * 2);
							}
						}
						dst[k] = (ushort)((float)(num5 + 5) / 9f);
					}
					k++;
				}
			}
		}
		return dst;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0002BECC File Offset: 0x0002A2CC
	public void BuildContours(float maxError, int maxEdgeLength, VoxelContourSet cset, int buildFlags)
	{
		int width = this.voxelArea.width;
		int depth = this.voxelArea.depth;
		int num = width * depth;
		int capacity = Mathf.Max(8, 8);
		List<VoxelContour> list = new List<VoxelContour>(capacity);
		int[] array = new int[this.voxelArea.compactSpans.Length];
		for (int i = 0; i < num; i += this.voxelArea.width)
		{
			for (int j = 0; j < this.voxelArea.width; j++)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[j + i];
				int k = (int)compactVoxelCell.index;
				int num2 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (k < num2)
				{
					int num3 = 0;
					CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[k];
					if (compactVoxelSpan.reg == 0 || (compactVoxelSpan.reg & (int)Voxelize.BorderReg) == (int)Voxelize.BorderReg)
					{
						array[k] = 0;
					}
					else
					{
						for (int l = 0; l < 4; l++)
						{
							int num4 = 0;
							if ((long)compactVoxelSpan.GetConnection(l) != (long)((ulong)Voxelize.NotConnected))
							{
								int num5 = j + this.voxelArea.DirectionX[l];
								int num6 = i + this.voxelArea.DirectionZ[l];
								int num7 = (int)(this.voxelArea.compactCells[num5 + num6].index + (uint)compactVoxelSpan.GetConnection(l));
								num4 = this.voxelArea.compactSpans[num7].reg;
							}
							if (num4 == compactVoxelSpan.reg)
							{
								num3 |= 1 << l;
							}
						}
						array[k] = (num3 ^ 15);
					}
					k++;
				}
			}
		}
		List<int> list2 = new List<int>(256);
		List<int> list3 = new List<int>(64);
		for (int m = 0; m < num; m += this.voxelArea.width)
		{
			for (int n = 0; n < this.voxelArea.width; n++)
			{
				CompactVoxelCell compactVoxelCell2 = this.voxelArea.compactCells[n + m];
				int num8 = (int)compactVoxelCell2.index;
				int num9 = (int)(compactVoxelCell2.index + compactVoxelCell2.count);
				while (num8 < num9)
				{
					if (array[num8] == 0 || array[num8] == 15)
					{
						array[num8] = 0;
					}
					else
					{
						int reg = this.voxelArea.compactSpans[num8].reg;
						if (reg != 0 && (reg & (int)Voxelize.BorderReg) != (int)Voxelize.BorderReg)
						{
							int area = this.voxelArea.areaTypes[num8];
							list2.Clear();
							list3.Clear();
							this.WalkContour(n, m, num8, array, list2);
							this.SimplifyContour(list2, list3, maxError, maxEdgeLength, buildFlags);
							this.RemoveDegenerateSegments(list3);
							list.Add(new VoxelContour
							{
								verts = list3.ToArray(),
								rverts = list2.ToArray(),
								nverts = list3.Count / 4,
								reg = reg,
								area = area
							});
						}
					}
					num8++;
				}
			}
		}
		for (int num10 = 0; num10 < list.Count; num10++)
		{
			VoxelContour value = list[num10];
			if (this.CalcAreaOfPolygon2D(value.verts, value.nverts) < 0)
			{
				int num11 = -1;
				for (int num12 = 0; num12 < list.Count; num12++)
				{
					if (num10 != num12)
					{
						if (list[num12].nverts > 0 && list[num12].reg == value.reg && this.CalcAreaOfPolygon2D(list[num12].verts, list[num12].nverts) > 0)
						{
							num11 = num12;
							break;
						}
					}
				}
				if (num11 == -1)
				{
					Debug.LogError("rcBuildContours: Could not find merge target for bad contour " + num10 + ".");
				}
				else
				{
					Debug.LogWarning("Fixing contour");
					VoxelContour value2 = list[num11];
					int num13 = 0;
					int num14 = 0;
					this.GetClosestIndices(value2.verts, value2.nverts, value.verts, value.nverts, ref num13, ref num14);
					if (num13 == -1 || num14 == -1)
					{
						Debug.LogWarning(string.Concat(new object[]
						{
							"rcBuildContours: Failed to find merge points for ",
							num10,
							" and ",
							num11,
							"."
						}));
					}
					else
					{
						int num15 = num13 * 4;
						int num16 = num14 * 4;
						Vector3 start = Vector3.Scale(new Vector3((float)value2.verts[num15], (float)value2.verts[num15 + 1], (float)value2.verts[num15 + 2] / (float)this.voxelArea.width), Voxelize.CellScale) + this.voxelOffset;
						Vector3 end = Vector3.Scale(new Vector3((float)value.verts[num16], (float)value.verts[num16 + 1], (float)value.verts[num16 + 2] / (float)this.voxelArea.width), Voxelize.CellScale) + this.voxelOffset;
						Debug.DrawLine(start, end, Color.green);
						if (!Voxelize.MergeContours(ref value2, ref value, num13, num14))
						{
							Debug.LogWarning(string.Concat(new object[]
							{
								"rcBuildContours: Failed to merge contours ",
								num10,
								" and ",
								num11,
								"."
							}));
						}
						else
						{
							list[num11] = value2;
							list[num10] = value;
						}
					}
				}
			}
		}
		cset.conts = list.ToArray();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0002C4F4 File Offset: 0x0002A8F4
	public int CalcAreaOfPolygon2D(int[] verts, int nverts)
	{
		int num = 0;
		int i = 0;
		int num2 = nverts - 1;
		while (i < nverts)
		{
			int num3 = i * 4;
			int num4 = num2 * 4;
			num += verts[num3] * (verts[num4 + 2] / this.voxelArea.width) - verts[num4] * (verts[num3 + 2] / this.voxelArea.width);
			num2 = i++;
		}
		return (num + 1) / 2;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0002C55C File Offset: 0x0002A95C
	private void GetClosestIndices(int[] vertsa, int nvertsa, int[] vertsb, int nvertsb, ref int ia, ref int ib)
	{
		int num = 268435455;
		ia = -1;
		ib = -1;
		for (int i = 0; i < nvertsa; i++)
		{
			int num2 = (i + 1) % nvertsa;
			int num3 = (i + nvertsa - 1) % nvertsa;
			int num4 = i * 4;
			int b = num2 * 4;
			int a = num3 * 4;
			for (int j = 0; j < nvertsb; j++)
			{
				int num5 = j * 4;
				if (Voxelize.Ileft(a, num4, num5, vertsa, vertsa, vertsb) && Voxelize.Ileft(num4, b, num5, vertsa, vertsa, vertsb))
				{
					int num6 = vertsb[num5] - vertsa[num4];
					int num7 = vertsb[num5 + 2] / this.voxelArea.width - vertsa[num4 + 2] / this.voxelArea.width;
					int num8 = num6 * num6 + num7 * num7;
					if (num8 < num)
					{
						ia = i;
						ib = j;
						num = num8;
					}
				}
			}
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0002C63C File Offset: 0x0002AA3C
	public static bool MergeContours(ref VoxelContour ca, ref VoxelContour cb, int ia, int ib)
	{
		int num = ca.nverts + cb.nverts + 2;
		int[] array = new int[num * 4];
		int num2 = 0;
		for (int i = 0; i <= ca.nverts; i++)
		{
			int num3 = num2 * 4;
			int num4 = (ia + i) % ca.nverts * 4;
			array[num3] = ca.verts[num4];
			array[num3 + 1] = ca.verts[num4 + 1];
			array[num3 + 2] = ca.verts[num4 + 2];
			array[num3 + 3] = ca.verts[num4 + 3];
			num2++;
		}
		for (int j = 0; j <= cb.nverts; j++)
		{
			int num5 = num2 * 4;
			int num6 = (ib + j) % cb.nverts * 4;
			array[num5] = cb.verts[num6];
			array[num5 + 1] = cb.verts[num6 + 1];
			array[num5 + 2] = cb.verts[num6 + 2];
			array[num5 + 3] = cb.verts[num6 + 3];
			num2++;
		}
		ca.verts = array;
		ca.nverts = num2;
		cb.verts = new int[0];
		cb.nverts = 0;
		return true;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0002C767 File Offset: 0x0002AB67
	public static bool Ileft(int a, int b, int c, int[] va, int[] vb, int[] vc)
	{
		return (vb[b] - va[a]) * (vc[c + 2] - va[a + 2]) - (vc[c] - va[a]) * (vb[b + 2] - va[a + 2]) <= 0;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0002C79C File Offset: 0x0002AB9C
	[Obsolete("This function is not complete and should not be used")]
	public void ErodeVoxels(int radius)
	{
		if (radius > 255)
		{
			Debug.LogError("Max Erode Radius is 255");
			radius = 255;
		}
		int num = this.voxelArea.width * this.voxelArea.depth;
		int[] array = new int[this.voxelArea.compactSpans.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 255;
		}
		for (int j = 0; j < num; j += this.voxelArea.width)
		{
			for (int k = 0; k < this.voxelArea.width; k++)
			{
				CompactVoxelCell compactVoxelCell = this.voxelArea.compactCells[k + j];
				int l = (int)compactVoxelCell.index;
				int num2 = (int)(compactVoxelCell.index + compactVoxelCell.count);
				while (l < num2)
				{
					if (this.voxelArea.areaTypes[l] != Voxelize.UnwalkableArea)
					{
						CompactVoxelSpan compactVoxelSpan = this.voxelArea.compactSpans[l];
						int num3 = 0;
						for (int m = 0; m < 4; m++)
						{
							if ((long)compactVoxelSpan.GetConnection(m) != (long)((ulong)Voxelize.NotConnected))
							{
								num3++;
							}
						}
						if (num3 != 4)
						{
							array[l] = 0;
						}
					}
					l++;
				}
			}
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0002C904 File Offset: 0x0002AD04
	public void FilterLowHeightSpans(uint voxelWalkableHeight, float cs, float ch, Vector3 min)
	{
		int num = this.voxelArea.width * this.voxelArea.depth;
		int i = 0;
		int num2 = 0;
		while (i < num)
		{
			for (int j = 0; j < this.voxelArea.width; j++)
			{
				for (VoxelSpan voxelSpan = this.voxelArea.cells[i + j].firstSpan; voxelSpan != null; voxelSpan = voxelSpan.next)
				{
					uint top = voxelSpan.top;
					uint num3 = (voxelSpan.next == null) ? VoxelArea.MaxHeight : voxelSpan.next.bottom;
					if (num3 - top < voxelWalkableHeight)
					{
						voxelSpan.area = 0;
					}
				}
			}
			i += this.voxelArea.width;
			num2++;
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0002C9D8 File Offset: 0x0002ADD8
	public void FilterLedges(uint voxelWalkableHeight, int voxelWalkableClimb, float cs, float ch, Vector3 min)
	{
		int num = this.voxelArea.width * this.voxelArea.depth;
		int i = 0;
		int num2 = 0;
		while (i < num)
		{
			for (int j = 0; j < this.voxelArea.width; j++)
			{
				for (VoxelSpan voxelSpan = this.voxelArea.cells[i + j].firstSpan; voxelSpan != null; voxelSpan = voxelSpan.next)
				{
					if (voxelSpan.area != 0)
					{
						int top = (int)voxelSpan.top;
						int a = (voxelSpan.next == null) ? VoxelArea.MaxHeightInt : ((int)voxelSpan.next.bottom);
						int num3 = VoxelArea.MaxHeightInt;
						int num4 = (int)voxelSpan.top;
						int num5 = (int)voxelSpan.top;
						for (int k = 0; k < 4; k++)
						{
							int num6 = j + this.voxelArea.DirectionX[k];
							int num7 = i + this.voxelArea.DirectionZ[k];
							if (num6 < 0 || num7 < 0 || num7 >= num || num6 >= this.voxelArea.width)
							{
								voxelSpan.area = 0;
								break;
							}
							VoxelSpan firstSpan = this.voxelArea.cells[num6 + num7].firstSpan;
							int num8 = -voxelWalkableClimb;
							int b = (firstSpan == null) ? VoxelArea.MaxHeightInt : ((int)firstSpan.bottom);
							if ((long)(Mathfx.Min(a, b) - Mathfx.Max(top, num8)) > (long)((ulong)voxelWalkableHeight))
							{
								num3 = Mathfx.Min(num3, num8 - top);
							}
							for (VoxelSpan voxelSpan2 = firstSpan; voxelSpan2 != null; voxelSpan2 = voxelSpan2.next)
							{
								num8 = (int)voxelSpan2.top;
								b = ((voxelSpan2.next == null) ? VoxelArea.MaxHeightInt : ((int)voxelSpan2.next.bottom));
								if ((long)(Mathfx.Min(a, b) - Mathfx.Max(top, num8)) > (long)((ulong)voxelWalkableHeight))
								{
									num3 = Mathfx.Min(num3, num8 - top);
									if (Mathfx.Abs(num8 - top) <= voxelWalkableClimb)
									{
										if (num8 < num4)
										{
											num4 = num8;
										}
										if (num8 > num5)
										{
											num5 = num8;
										}
									}
								}
							}
						}
						if (num3 < -voxelWalkableClimb)
						{
							voxelSpan.area = 0;
						}
						else if (num5 - num4 > voxelWalkableClimb)
						{
							voxelSpan.area = 0;
						}
					}
				}
			}
			i += this.voxelArea.width;
			num2++;
		}
	}

	// Token: 0x0400036D RID: 877
	public static uint NotConnected = 63U;

	// Token: 0x0400036E RID: 878
	public float walkableHeight = 0.8f;

	// Token: 0x0400036F RID: 879
	public float walkableClimb = 0.8f;

	// Token: 0x04000370 RID: 880
	public int voxelWalkableClimb;

	// Token: 0x04000371 RID: 881
	public uint voxelWalkableHeight;

	// Token: 0x04000372 RID: 882
	public float cellSize = 0.2f;

	// Token: 0x04000373 RID: 883
	public float cellHeight = 0.1f;

	// Token: 0x04000374 RID: 884
	public float maxEdgeLength = 20f;

	// Token: 0x04000375 RID: 885
	public float maxSlope = 30f;

	// Token: 0x04000376 RID: 886
	public Vector3 voxelOffset;

	// Token: 0x04000377 RID: 887
	public bool includeOutOfBounds;

	// Token: 0x04000378 RID: 888
	public Bounds forcedBounds;

	// Token: 0x04000379 RID: 889
	public int runTimes = 10;

	// Token: 0x0400037A RID: 890
	public VoxelArea voxelArea;

	// Token: 0x0400037B RID: 891
	public VoxelContourSet countourSet;

	// Token: 0x0400037C RID: 892
	public static int MaxLayers = 65535;

	// Token: 0x0400037D RID: 893
	public static int MaxRegions = 500;

	// Token: 0x0400037E RID: 894
	public static int UnwalkableArea;

	// Token: 0x0400037F RID: 895
	private static ushort BorderReg = 32768;

	// Token: 0x04000380 RID: 896
	private static int RC_BORDER_VERTEX = 65536;

	// Token: 0x04000381 RID: 897
	private static int RC_AREA_BORDER = 131072;

	// Token: 0x04000382 RID: 898
	public static int VERTEX_BUCKET_COUNT = 4096;

	// Token: 0x04000383 RID: 899
	public static int RC_CONTOUR_TESS_WALL_EDGES = 1;

	// Token: 0x04000384 RID: 900
	public static int RC_CONTOUR_TESS_AREA_EDGES = 2;

	// Token: 0x04000385 RID: 901
	private static int ContourRegMask = 65535;

	// Token: 0x04000386 RID: 902
	public string debugString = string.Empty;

	// Token: 0x04000387 RID: 903
	public static Vector3 CellScale;

	// Token: 0x04000388 RID: 904
	public static Vector3 CellScaleDivision;
}
