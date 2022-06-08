using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public abstract class MonoModifier : MonoBehaviour, IPathModifier
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0002EC93 File Offset: 0x0002D093
		public void OnEnable()
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002EC95 File Offset: 0x0002D095
		public void OnDisable()
		{
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0002EC97 File Offset: 0x0002D097
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0002EC9F File Offset: 0x0002D09F
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000508 RID: 1288
		public abstract ModifierData input { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000509 RID: 1289
		public abstract ModifierData output { get; }

		// Token: 0x0600050A RID: 1290 RVA: 0x0002ECA8 File Offset: 0x0002D0A8
		public void Awake()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002ECD3 File Offset: 0x0002D0D3
		public void OnDestroy()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002ECF2 File Offset: 0x0002D0F2
		[Obsolete]
		public virtual void ApplyOriginal(Path p)
		{
		}

		// Token: 0x0600050D RID: 1293
		public abstract void Apply(Path p, ModifierData source);

		// Token: 0x0600050E RID: 1294 RVA: 0x0002ECF4 File Offset: 0x0002D0F4
		[Obsolete]
		public virtual void PreProcess(Path p)
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002ECF8 File Offset: 0x0002D0F8
		[Obsolete]
		public virtual Vector3[] Apply(Node[] path, Vector3 start, Vector3 end, int startIndex, int endIndex, NavGraph graph)
		{
			Vector3[] array = new Vector3[endIndex - startIndex];
			for (int i = startIndex; i < endIndex; i++)
			{
				array[i - startIndex] = (Vector3)path[i].position;
			}
			return array;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002ED41 File Offset: 0x0002D141
		[Obsolete]
		public virtual Vector3[] Apply(Vector3[] path, Vector3 start, Vector3 end)
		{
			return path;
		}

		// Token: 0x04000423 RID: 1059
		[NonSerialized]
		public Seeker seeker;

		// Token: 0x04000424 RID: 1060
		public int priority;
	}
}
