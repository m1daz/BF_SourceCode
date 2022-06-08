using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200043C RID: 1084
public class FingerEventsSamplePart2 : SampleBase
{
	// Token: 0x06001F79 RID: 8057 RVA: 0x000EF9B8 File Offset: 0x000EDDB8
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = "Drag your fingers anywhere on the screen";
		this.paths = new FingerEventsSamplePart2.PathRenderer[FingerGestures.Instance.MaxFingers];
		for (int i = 0; i < this.paths.Length; i++)
		{
			this.paths[i] = new FingerEventsSamplePart2.PathRenderer(i, this.lineRendererPrefab);
		}
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x000EFA1D File Offset: 0x000EDE1D
	protected override string GetHelpText()
	{
		return "This sample lets you visualize the FingerDown, FingerMoveBegin, FingerMove, FingerMoveEnd and FingerUp events.\n\nINSTRUCTIONS:\nMove your finger accross the screen and observe what happens.\n\nLEGEND:\n- Red Circle = FingerDown position\n- Yellow Square = FingerMoveBegin position\n- Green Sphere = FingerMoveEnd position\n- Blue Circle = FingerUp position";
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x000EFA24 File Offset: 0x000EDE24
	private void OnEnable()
	{
		Debug.Log("Registering finger gesture events from C# script");
		FingerGestures.OnFingerDown += this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerUp += this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerMoveBegin += this.FingerGestures_OnFingerMoveBegin;
		FingerGestures.OnFingerMove += this.FingerGestures_OnFingerMove;
		FingerGestures.OnFingerMoveEnd += this.FingerGestures_OnFingerMoveEnd;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x000EFA90 File Offset: 0x000EDE90
	private void OnDisable()
	{
		FingerGestures.OnFingerDown -= this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerUp -= this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerMoveBegin -= this.FingerGestures_OnFingerMoveBegin;
		FingerGestures.OnFingerMove -= this.FingerGestures_OnFingerMove;
		FingerGestures.OnFingerMoveEnd -= this.FingerGestures_OnFingerMoveEnd;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x000EFAF4 File Offset: 0x000EDEF4
	private void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[fingerIndex];
		pathRenderer.Reset();
		pathRenderer.AddPoint(fingerPos, this.fingerDownMarkerPrefab);
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x000EFB20 File Offset: 0x000EDF20
	private void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[fingerIndex];
		pathRenderer.AddPoint(fingerPos, this.fingerUpMarkerPrefab);
		base.UI.StatusText = string.Concat(new object[]
		{
			"Finger ",
			fingerIndex,
			" was held down for ",
			timeHeldDown.ToString("N2"),
			" seconds"
		});
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x000EFB8C File Offset: 0x000EDF8C
	private void FingerGestures_OnFingerMoveBegin(int fingerIndex, Vector2 fingerPos)
	{
		base.UI.StatusText = "Started moving finger " + fingerIndex;
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[fingerIndex];
		pathRenderer.AddPoint(fingerPos, this.fingerMoveBeginMarkerPrefab);
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x000EFBCC File Offset: 0x000EDFCC
	private void FingerGestures_OnFingerMove(int fingerIndex, Vector2 fingerPos)
	{
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[fingerIndex];
		pathRenderer.AddPoint(fingerPos);
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x000EFBEC File Offset: 0x000EDFEC
	private void FingerGestures_OnFingerMoveEnd(int fingerIndex, Vector2 fingerPos)
	{
		base.UI.StatusText = "Stopped moving finger " + fingerIndex;
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[fingerIndex];
		pathRenderer.AddPoint(fingerPos, this.fingerMoveEndMarkerPrefab);
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x000EFC2C File Offset: 0x000EE02C
	private bool CheckSpawnParticles(Vector2 fingerPos, GameObject requiredObject)
	{
		GameObject gameObject = SampleBase.PickObject(fingerPos);
		if (!gameObject || gameObject != requiredObject)
		{
			return false;
		}
		this.SpawnParticles(gameObject);
		return true;
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x000EFC64 File Offset: 0x000EE064
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x04002090 RID: 8336
	public LineRenderer lineRendererPrefab;

	// Token: 0x04002091 RID: 8337
	public GameObject fingerDownMarkerPrefab;

	// Token: 0x04002092 RID: 8338
	public GameObject fingerMoveBeginMarkerPrefab;

	// Token: 0x04002093 RID: 8339
	public GameObject fingerMoveEndMarkerPrefab;

	// Token: 0x04002094 RID: 8340
	public GameObject fingerUpMarkerPrefab;

	// Token: 0x04002095 RID: 8341
	private FingerEventsSamplePart2.PathRenderer[] paths;

	// Token: 0x0200043D RID: 1085
	private class PathRenderer
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x000EFC8C File Offset: 0x000EE08C
		public PathRenderer(int index, LineRenderer lineRendererPrefab)
		{
			this.lineRenderer = UnityEngine.Object.Instantiate<LineRenderer>(lineRendererPrefab);
			this.lineRenderer.name = lineRendererPrefab.name + index;
			this.lineRenderer.enabled = true;
			this.UpdateLines();
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000EFCF0 File Offset: 0x000EE0F0
		public void Reset()
		{
			this.points.Clear();
			this.UpdateLines();
			foreach (GameObject obj in this.markers)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.markers.Clear();
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000EFD68 File Offset: 0x000EE168
		public void AddPoint(Vector2 screenPos)
		{
			this.AddPoint(screenPos, null);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000EFD74 File Offset: 0x000EE174
		public void AddPoint(Vector2 screenPos, GameObject markerPrefab)
		{
			Vector3 worldPos = SampleBase.GetWorldPos(screenPos);
			if (markerPrefab)
			{
				this.AddMarker(worldPos, markerPrefab);
			}
			this.points.Add(worldPos);
			this.UpdateLines();
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000EFDB4 File Offset: 0x000EE1B4
		private GameObject AddMarker(Vector2 pos, GameObject prefab)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, pos, Quaternion.identity);
			gameObject.name = string.Concat(new object[]
			{
				prefab.name,
				"(",
				this.markers.Count,
				")"
			});
			this.markers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000EFE20 File Offset: 0x000EE220
		private void UpdateLines()
		{
			this.lineRenderer.SetVertexCount(this.points.Count);
			for (int i = 0; i < this.points.Count; i++)
			{
				this.lineRenderer.SetPosition(i, this.points[i]);
			}
		}

		// Token: 0x04002096 RID: 8342
		private LineRenderer lineRenderer;

		// Token: 0x04002097 RID: 8343
		private List<Vector3> points = new List<Vector3>();

		// Token: 0x04002098 RID: 8344
		private List<GameObject> markers = new List<GameObject>();
	}
}
