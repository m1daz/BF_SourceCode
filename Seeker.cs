using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000004 RID: 4
[AddComponentMenu("Pathfinding/Seeker")]
public class Seeker : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002EC5 File Offset: 0x000012C5
	public Path GetCurrentPath()
	{
		return this.path;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002ECD File Offset: 0x000012CD
	public void Awake()
	{
		this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
		this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		this.startEndModifier.Awake(this);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002EFF File Offset: 0x000012FF
	public void OnDestroy()
	{
		this.startEndModifier.OnDestroy(this);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002F0D File Offset: 0x0000130D
	public void RegisterModifier(IPathModifier mod)
	{
		if (this.modifiers == null)
		{
			this.modifiers = new List<IPathModifier>(1);
		}
		this.modifiers.Add(mod);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002F32 File Offset: 0x00001332
	public void DeregisterModifier(IPathModifier mod)
	{
		if (this.modifiers == null)
		{
			return;
		}
		this.modifiers.Remove(mod);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002F4D File Offset: 0x0000134D
	public void PostProcess(Path p)
	{
		this.RunModifiers(Seeker.ModifierPass.PostProcess, p);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002F58 File Offset: 0x00001358
	public void RunModifiers(Seeker.ModifierPass pass, Path p)
	{
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = 0; i < this.modifiers.Count - 1; i++)
			{
				if (this.modifiers[i].Priority < this.modifiers[i + 1].Priority)
				{
					IPathModifier value = this.modifiers[i];
					this.modifiers[i] = this.modifiers[i + 1];
					this.modifiers[i + 1] = value;
					flag = true;
				}
			}
		}
		if (pass != Seeker.ModifierPass.PreProcess)
		{
			if (pass != Seeker.ModifierPass.PostProcessOriginal)
			{
				if (pass == Seeker.ModifierPass.PostProcess)
				{
					if (this.postProcessPath != null)
					{
						this.postProcessPath(p);
					}
				}
			}
			else if (this.postProcessOriginalPath != null)
			{
				this.postProcessOriginalPath(p);
			}
		}
		else if (this.preProcessPath != null)
		{
			this.preProcessPath(p);
		}
		if (this.modifiers.Count == 0)
		{
			return;
		}
		ModifierData modifierData = ModifierData.All;
		IPathModifier pathModifier = this.modifiers[0];
		for (int j = 0; j < this.modifiers.Count; j++)
		{
			MonoModifier monoModifier = this.modifiers[j] as MonoModifier;
			if (!(monoModifier != null) || monoModifier.enabled)
			{
				if (pass != Seeker.ModifierPass.PreProcess)
				{
					if (pass != Seeker.ModifierPass.PostProcessOriginal)
					{
						if (pass == Seeker.ModifierPass.PostProcess)
						{
							ModifierData modifierData2 = ModifierConverter.Convert(p, modifierData, this.modifiers[j].input);
							if (modifierData2 != ModifierData.None)
							{
								this.modifiers[j].Apply(p, modifierData2);
								modifierData = this.modifiers[j].output;
							}
							else
							{
								Debug.Log(string.Concat(new string[]
								{
									"Error converting ",
									(j <= 0) ? "original" : pathModifier.GetType().Name,
									"'s output to ",
									this.modifiers[j].GetType().Name,
									"'s input.\nTry rearranging the modifier priorities on the Seeker."
								}));
								modifierData = ModifierData.None;
							}
							pathModifier = this.modifiers[j];
						}
					}
					else
					{
						this.modifiers[j].ApplyOriginal(p);
					}
				}
				else
				{
					this.modifiers[j].PreProcess(p);
				}
				if (modifierData == ModifierData.None)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000031EF File Offset: 0x000015EF
	public bool IsDone()
	{
		return this.path == null || this.path.GetState() >= PathState.Returned;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00003210 File Offset: 0x00001610
	public void OnPathComplete(Path p)
	{
		this.OnPathComplete(p, true, true);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000321C File Offset: 0x0000161C
	public void OnPathComplete(Path p, bool runModifiers, bool sendCallbacks)
	{
		if (p != null && p != this.path && sendCallbacks)
		{
			return;
		}
		if (this == null || p == null || p != this.path)
		{
			return;
		}
		if (!this.path.error && runModifiers)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcessOriginal, this.path);
			this.RunModifiers(Seeker.ModifierPass.PostProcess, this.path);
		}
		if (sendCallbacks)
		{
			p.Claim(this);
			this.lastCompletedNodePath = p.path;
			this.lastCompletedVectorPath = p.vectorPath;
			if (this.tmpPathCallback != null)
			{
				this.tmpPathCallback(p);
			}
			if (this.pathCallback != null)
			{
				this.pathCallback(p);
			}
			if (this.prevPath != null)
			{
				this.prevPath.ReleaseSilent(this);
			}
			this.prevPath = p;
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003302 File Offset: 0x00001702
	public void OnPartialPathComplete(Path p)
	{
		this.OnPathComplete(p, true, false);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000330D File Offset: 0x0000170D
	public void OnMultiPathComplete(Path p)
	{
		this.OnPathComplete(p, false, true);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003318 File Offset: 0x00001718
	public Path GetNewPath(Vector3 start, Vector3 end)
	{
		return ABPath.Construct(start, end, null);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x0000332F File Offset: 0x0000172F
	public Path StartPath(Vector3 start, Vector3 end)
	{
		return this.StartPath(start, end, null, -1);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000333B File Offset: 0x0000173B
	public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
	{
		return this.StartPath(start, end, callback, -1);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003348 File Offset: 0x00001748
	public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, int graphMask)
	{
		Path newPath = this.GetNewPath(start, end);
		return this.StartPath(newPath, callback, graphMask);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003368 File Offset: 0x00001768
	public Path StartPath(Path p, OnPathDelegate callback = null, int graphMask = -1)
	{
		p.enabledTags = this.traversableTags.tagsChange;
		p.tagPenalties = this.tagPenalties;
		if (p.GetType() == typeof(MultiTargetPath))
		{
			return this.StartMultiTargetPath(p as MultiTargetPath, callback, -1);
		}
		if (this.path != null && this.path.GetState() <= PathState.Processing && this.lastPathID == (uint)this.path.pathID)
		{
			this.path.LogError("Canceled path because a new one was requested\nGameObject: " + base.gameObject.name);
		}
		this.path = p;
		Path path = this.path;
		path.callback = (OnPathDelegate)Delegate.Combine(path.callback, this.onPathDelegate);
		this.path.nnConstraint.graphMask = graphMask;
		this.tmpPathCallback = callback;
		this.lastPathID = (uint)this.path.pathID;
		this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
		AstarPath.StartPath(this.path);
		return this.path;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003478 File Offset: 0x00001878
	public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
	{
		MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
		multiTargetPath.pathsForAll = pathsForAll;
		return this.StartMultiTargetPath(multiTargetPath, callback, graphMask);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000034A4 File Offset: 0x000018A4
	public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
	{
		MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
		multiTargetPath.pathsForAll = pathsForAll;
		return this.StartMultiTargetPath(multiTargetPath, callback, graphMask);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000034D0 File Offset: 0x000018D0
	public MultiTargetPath StartMultiTargetPath(MultiTargetPath p, OnPathDelegate callback = null, int graphMask = -1)
	{
		if (this.path != null && this.path.GetState() <= PathState.Processing && this.lastPathID == (uint)this.path.pathID)
		{
			this.path.LogError("Canceled path because a new one was requested");
		}
		OnPathDelegate[] array = new OnPathDelegate[p.targetPoints.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.onPartialPathDelegate;
		}
		p.callbacks = array;
		p.callback = (OnPathDelegate)Delegate.Combine(p.callback, new OnPathDelegate(this.OnMultiPathComplete));
		p.nnConstraint.graphMask = graphMask;
		this.path = p;
		this.tmpPathCallback = callback;
		this.lastPathID = (uint)this.path.pathID;
		this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
		AstarPath.StartPath(this.path);
		return p;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000035B8 File Offset: 0x000019B8
	public IEnumerator DelayPathStart(Path p)
	{
		yield return 0;
		this.RunModifiers(Seeker.ModifierPass.PreProcess, p);
		AstarPath.StartPath(p);
		yield break;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000035DC File Offset: 0x000019DC
	public void OnDrawGizmos()
	{
		if (this.lastCompletedNodePath == null || !this.drawGizmos)
		{
			return;
		}
		if (this.detailedGizmos)
		{
			Gizmos.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
			if (this.lastCompletedNodePath != null)
			{
				for (int i = 0; i < this.lastCompletedNodePath.Count - 1; i++)
				{
					Gizmos.DrawLine((Vector3)this.lastCompletedNodePath[i].position, (Vector3)this.lastCompletedNodePath[i + 1].position);
				}
			}
		}
		Gizmos.color = new Color(0f, 1f, 0f, 1f);
		if (this.lastCompletedVectorPath != null)
		{
			for (int j = 0; j < this.lastCompletedVectorPath.Count - 1; j++)
			{
				Gizmos.DrawLine(this.lastCompletedVectorPath[j], this.lastCompletedVectorPath[j + 1]);
			}
		}
	}

	// Token: 0x04000023 RID: 35
	public bool drawGizmos = true;

	// Token: 0x04000024 RID: 36
	public bool detailedGizmos;

	// Token: 0x04000025 RID: 37
	[HideInInspector]
	public bool saveGetNearestHints = true;

	// Token: 0x04000026 RID: 38
	public StartEndModifier startEndModifier = new StartEndModifier();

	// Token: 0x04000027 RID: 39
	[HideInInspector]
	public TagMask traversableTags = new TagMask(-1, -1);

	// Token: 0x04000028 RID: 40
	[HideInInspector]
	public int[] tagPenalties = new int[32];

	// Token: 0x04000029 RID: 41
	public OnPathDelegate pathCallback;

	// Token: 0x0400002A RID: 42
	public OnPathDelegate preProcessPath;

	// Token: 0x0400002B RID: 43
	public OnPathDelegate postProcessOriginalPath;

	// Token: 0x0400002C RID: 44
	public OnPathDelegate postProcessPath;

	// Token: 0x0400002D RID: 45
	[NonSerialized]
	public List<Vector3> lastCompletedVectorPath;

	// Token: 0x0400002E RID: 46
	[NonSerialized]
	public List<Node> lastCompletedNodePath;

	// Token: 0x0400002F RID: 47
	[NonSerialized]
	protected Path path;

	// Token: 0x04000030 RID: 48
	protected Path prevPath;

	// Token: 0x04000031 RID: 49
	private Node startHint;

	// Token: 0x04000032 RID: 50
	private Node endHint;

	// Token: 0x04000033 RID: 51
	private OnPathDelegate onPathDelegate;

	// Token: 0x04000034 RID: 52
	private OnPathDelegate onPartialPathDelegate;

	// Token: 0x04000035 RID: 53
	private OnPathDelegate tmpPathCallback;

	// Token: 0x04000036 RID: 54
	protected uint lastPathID;

	// Token: 0x04000037 RID: 55
	private List<IPathModifier> modifiers = new List<IPathModifier>();

	// Token: 0x02000005 RID: 5
	public enum ModifierPass
	{
		// Token: 0x04000039 RID: 57
		PreProcess,
		// Token: 0x0400003A RID: 58
		PostProcessOriginal,
		// Token: 0x0400003B RID: 59
		PostProcess
	}
}
