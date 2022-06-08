﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class MeleeWeaponTrail : MonoBehaviour
{
	// Token: 0x17000184 RID: 388
	// (set) Token: 0x060020CA RID: 8394 RVA: 0x000F4B72 File Offset: 0x000F2F72
	public bool Emit
	{
		set
		{
			this._emit = value;
		}
	}

	// Token: 0x17000185 RID: 389
	// (set) Token: 0x060020CB RID: 8395 RVA: 0x000F4B7B File Offset: 0x000F2F7B
	public bool Use
	{
		set
		{
			this._use = value;
		}
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x000F4B84 File Offset: 0x000F2F84
	private void Start()
	{
		this._lastPosition = base.transform.position;
		this._trailObject = new GameObject("Trail");
		this._trailObject.transform.parent = null;
		this._trailObject.transform.position = Vector3.zero;
		this._trailObject.transform.rotation = Quaternion.identity;
		this._trailObject.transform.localScale = Vector3.one;
		this._trailObject.AddComponent(typeof(MeshFilter));
		this._trailObject.AddComponent(typeof(MeshRenderer));
		this._trailObject.GetComponent<Renderer>().material = this._material;
		this._trailMesh = new Mesh();
		this._trailMesh.name = base.name + "TrailMesh";
		this._trailObject.GetComponent<MeshFilter>().mesh = this._trailMesh;
		this._minVertexDistanceSqr = this._minVertexDistance * this._minVertexDistance;
		this._maxVertexDistanceSqr = this._maxVertexDistance * this._maxVertexDistance;
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x000F4CA6 File Offset: 0x000F30A6
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(this._trailObject);
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x000F4CB4 File Offset: 0x000F30B4
	private void Update()
	{
		if (!this._use)
		{
			return;
		}
		if (this._emit && this._emitTime != 0f)
		{
			this._emitTime -= Time.deltaTime;
			if (this._emitTime == 0f)
			{
				this._emitTime = -1f;
			}
			if (this._emitTime < 0f)
			{
				this._emit = false;
			}
		}
		if (!this._emit && this._points.Count == 0 && this._autoDestruct)
		{
			UnityEngine.Object.Destroy(this._trailObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (!Camera.main)
		{
			return;
		}
		float sqrMagnitude = (this._lastPosition - base.transform.position).sqrMagnitude;
		if (this._emit)
		{
			if (sqrMagnitude > this._minVertexDistanceSqr)
			{
				bool flag = false;
				if (this._points.Count < 3)
				{
					flag = true;
				}
				else
				{
					Vector3 from = this._points[this._points.Count - 2].tipPosition - this._points[this._points.Count - 3].tipPosition;
					Vector3 to = this._points[this._points.Count - 1].tipPosition - this._points[this._points.Count - 2].tipPosition;
					if (Vector3.Angle(from, to) > this._maxAngle || sqrMagnitude > this._maxVertexDistanceSqr)
					{
						flag = true;
					}
				}
				if (flag)
				{
					MeleeWeaponTrail.Point point = new MeleeWeaponTrail.Point();
					point.basePosition = this._base.position;
					point.tipPosition = this._tip.position;
					point.timeCreated = Time.time;
					this._points.Add(point);
					this._lastPosition = base.transform.position;
					if (this._points.Count == 1)
					{
						this._smoothedPoints.Add(point);
					}
					else if (this._points.Count > 1)
					{
						for (int i = 0; i < 1 + this.subdivisions; i++)
						{
							this._smoothedPoints.Add(point);
						}
					}
					if (this._points.Count >= 4)
					{
						IEnumerable<Vector3> collection = Interpolate.NewCatmullRom(new Vector3[]
						{
							this._points[this._points.Count - 4].tipPosition,
							this._points[this._points.Count - 3].tipPosition,
							this._points[this._points.Count - 2].tipPosition,
							this._points[this._points.Count - 1].tipPosition
						}, this.subdivisions, false);
						IEnumerable<Vector3> collection2 = Interpolate.NewCatmullRom(new Vector3[]
						{
							this._points[this._points.Count - 4].basePosition,
							this._points[this._points.Count - 3].basePosition,
							this._points[this._points.Count - 2].basePosition,
							this._points[this._points.Count - 1].basePosition
						}, this.subdivisions, false);
						List<Vector3> list = new List<Vector3>(collection);
						List<Vector3> list2 = new List<Vector3>(collection2);
						float timeCreated = this._points[this._points.Count - 4].timeCreated;
						float timeCreated2 = this._points[this._points.Count - 1].timeCreated;
						for (int j = 0; j < list.Count; j++)
						{
							int num = this._smoothedPoints.Count - (list.Count - j);
							if (num > -1 && num < this._smoothedPoints.Count)
							{
								MeleeWeaponTrail.Point point2 = new MeleeWeaponTrail.Point();
								point2.basePosition = list2[j];
								point2.tipPosition = list[j];
								point2.timeCreated = Mathf.Lerp(timeCreated, timeCreated2, (float)j / (float)list.Count);
								this._smoothedPoints[num] = point2;
							}
						}
					}
				}
				else
				{
					this._points[this._points.Count - 1].basePosition = this._base.position;
					this._points[this._points.Count - 1].tipPosition = this._tip.position;
					this._smoothedPoints[this._smoothedPoints.Count - 1].basePosition = this._base.position;
					this._smoothedPoints[this._smoothedPoints.Count - 1].tipPosition = this._tip.position;
				}
			}
			else
			{
				if (this._points.Count > 0)
				{
					this._points[this._points.Count - 1].basePosition = this._base.position;
					this._points[this._points.Count - 1].tipPosition = this._tip.position;
				}
				if (this._smoothedPoints.Count > 0)
				{
					this._smoothedPoints[this._smoothedPoints.Count - 1].basePosition = this._base.position;
					this._smoothedPoints[this._smoothedPoints.Count - 1].tipPosition = this._tip.position;
				}
			}
		}
		this.RemoveOldPoints(this._points);
		if (this._points.Count == 0)
		{
			this._trailMesh.Clear();
		}
		this.RemoveOldPoints(this._smoothedPoints);
		if (this._smoothedPoints.Count == 0)
		{
			this._trailMesh.Clear();
		}
		List<MeleeWeaponTrail.Point> smoothedPoints = this._smoothedPoints;
		if (smoothedPoints.Count > 1)
		{
			Vector3[] array = new Vector3[smoothedPoints.Count * 2];
			Vector2[] array2 = new Vector2[smoothedPoints.Count * 2];
			int[] array3 = new int[(smoothedPoints.Count - 1) * 6];
			Color[] array4 = new Color[smoothedPoints.Count * 2];
			for (int k = 0; k < smoothedPoints.Count; k++)
			{
				MeleeWeaponTrail.Point point3 = smoothedPoints[k];
				float num2 = (Time.time - point3.timeCreated) / this._lifeTime;
				Color color = Color.Lerp(Color.white, Color.clear, num2);
				if (this._colors != null && this._colors.Length > 0)
				{
					float num3 = num2 * (float)(this._colors.Length - 1);
					float num4 = Mathf.Floor(num3);
					float num5 = Mathf.Clamp(Mathf.Ceil(num3), 1f, (float)(this._colors.Length - 1));
					float t = Mathf.InverseLerp(num4, num5, num3);
					if (num4 >= (float)this._colors.Length)
					{
						num4 = (float)(this._colors.Length - 1);
					}
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					if (num5 >= (float)this._colors.Length)
					{
						num5 = (float)(this._colors.Length - 1);
					}
					if (num5 < 0f)
					{
						num5 = 0f;
					}
					color = Color.Lerp(this._colors[(int)num4], this._colors[(int)num5], t);
				}
				float num6 = 0f;
				if (this._sizes != null && this._sizes.Length > 0)
				{
					float num7 = num2 * (float)(this._sizes.Length - 1);
					float num8 = Mathf.Floor(num7);
					float num9 = Mathf.Clamp(Mathf.Ceil(num7), 1f, (float)(this._sizes.Length - 1));
					float t2 = Mathf.InverseLerp(num8, num9, num7);
					if (num8 >= (float)this._sizes.Length)
					{
						num8 = (float)(this._sizes.Length - 1);
					}
					if (num8 < 0f)
					{
						num8 = 0f;
					}
					if (num9 >= (float)this._sizes.Length)
					{
						num9 = (float)(this._sizes.Length - 1);
					}
					if (num9 < 0f)
					{
						num9 = 0f;
					}
					num6 = Mathf.Lerp(this._sizes[(int)num8], this._sizes[(int)num9], t2);
				}
				Vector3 a = point3.tipPosition - point3.basePosition;
				array[k * 2] = point3.basePosition - a * (num6 * 0.5f);
				array[k * 2 + 1] = point3.tipPosition + a * (num6 * 0.5f);
				array4[k * 2] = (array4[k * 2 + 1] = color);
				float x = (float)k / (float)smoothedPoints.Count;
				array2[k * 2] = new Vector2(x, 0f);
				array2[k * 2 + 1] = new Vector2(x, 1f);
				if (k > 0)
				{
					array3[(k - 1) * 6] = k * 2 - 2;
					array3[(k - 1) * 6 + 1] = k * 2 - 1;
					array3[(k - 1) * 6 + 2] = k * 2;
					array3[(k - 1) * 6 + 3] = k * 2 + 1;
					array3[(k - 1) * 6 + 4] = k * 2;
					array3[(k - 1) * 6 + 5] = k * 2 - 1;
				}
			}
			this._trailMesh.Clear();
			this._trailMesh.vertices = array;
			this._trailMesh.colors = array4;
			this._trailMesh.uv = array2;
			this._trailMesh.triangles = array3;
		}
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x000F5758 File Offset: 0x000F3B58
	private void RemoveOldPoints(List<MeleeWeaponTrail.Point> pointList)
	{
		List<MeleeWeaponTrail.Point> list = new List<MeleeWeaponTrail.Point>();
		foreach (MeleeWeaponTrail.Point point in pointList)
		{
			if (Time.time - point.timeCreated > this._lifeTime)
			{
				list.Add(point);
			}
		}
		foreach (MeleeWeaponTrail.Point item in list)
		{
			pointList.Remove(item);
		}
	}

	// Token: 0x04002195 RID: 8597
	[SerializeField]
	private bool _emit = true;

	// Token: 0x04002196 RID: 8598
	private bool _use = true;

	// Token: 0x04002197 RID: 8599
	[SerializeField]
	private float _emitTime;

	// Token: 0x04002198 RID: 8600
	[SerializeField]
	private Material _material;

	// Token: 0x04002199 RID: 8601
	[SerializeField]
	private float _lifeTime = 1f;

	// Token: 0x0400219A RID: 8602
	[SerializeField]
	private Color[] _colors;

	// Token: 0x0400219B RID: 8603
	[SerializeField]
	private float[] _sizes;

	// Token: 0x0400219C RID: 8604
	[SerializeField]
	private float _minVertexDistance = 0.1f;

	// Token: 0x0400219D RID: 8605
	[SerializeField]
	private float _maxVertexDistance = 10f;

	// Token: 0x0400219E RID: 8606
	private float _minVertexDistanceSqr;

	// Token: 0x0400219F RID: 8607
	private float _maxVertexDistanceSqr;

	// Token: 0x040021A0 RID: 8608
	[SerializeField]
	private float _maxAngle = 3f;

	// Token: 0x040021A1 RID: 8609
	[SerializeField]
	private bool _autoDestruct;

	// Token: 0x040021A2 RID: 8610
	[SerializeField]
	private int subdivisions = 4;

	// Token: 0x040021A3 RID: 8611
	[SerializeField]
	private Transform _base;

	// Token: 0x040021A4 RID: 8612
	[SerializeField]
	private Transform _tip;

	// Token: 0x040021A5 RID: 8613
	private List<MeleeWeaponTrail.Point> _points = new List<MeleeWeaponTrail.Point>();

	// Token: 0x040021A6 RID: 8614
	private List<MeleeWeaponTrail.Point> _smoothedPoints = new List<MeleeWeaponTrail.Point>();

	// Token: 0x040021A7 RID: 8615
	private GameObject _trailObject;

	// Token: 0x040021A8 RID: 8616
	private Mesh _trailMesh;

	// Token: 0x040021A9 RID: 8617
	private Vector3 _lastPosition;

	// Token: 0x0200046B RID: 1131
	[Serializable]
	public class Point
	{
		// Token: 0x040021AA RID: 8618
		public float timeCreated;

		// Token: 0x040021AB RID: 8619
		public Vector3 basePosition;

		// Token: 0x040021AC RID: 8620
		public Vector3 tipPosition;
	}
}
