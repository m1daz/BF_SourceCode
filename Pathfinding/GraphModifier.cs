using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200002C RID: 44
	public abstract class GraphModifier : MonoBehaviour
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000A2EB File Offset: 0x000086EB
		private static List<GraphModifier> GetActiveModifiers()
		{
			if (Application.isPlaying)
			{
				return GraphModifier.activeModifiers;
			}
			return new List<GraphModifier>(UnityEngine.Object.FindObjectsOfType(typeof(GraphModifier)) as GraphModifier[]);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A318 File Offset: 0x00008718
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			List<GraphModifier> list = GraphModifier.GetActiveModifiers();
			switch (type)
			{
			case GraphModifier.EventType.PostScan:
				for (int i = 0; i < list.Count; i++)
				{
					list[i].OnPostScan();
				}
				break;
			case GraphModifier.EventType.PreScan:
				for (int j = 0; j < list.Count; j++)
				{
					list[j].OnPreScan();
				}
				break;
			case GraphModifier.EventType.LatePostScan:
				GraphModifier.lastLateScanEvent = Time.frameCount;
				for (int k = 0; k < list.Count; k++)
				{
					list[k].OnLatePostScan();
				}
				break;
			case GraphModifier.EventType.PreUpdate:
				for (int l = 0; l < list.Count; l++)
				{
					list[l].OnGraphsPreUpdate();
				}
				break;
			case GraphModifier.EventType.PostUpdate:
				for (int m = 0; m < list.Count; m++)
				{
					list[m].OnGraphsPostUpdate();
				}
				break;
			case GraphModifier.EventType.PostCacheLoad:
				GraphModifier.lastPostCacheEvent = Time.frameCount;
				for (int n = 0; n < list.Count; n++)
				{
					list[n].OnPostCacheLoad();
				}
				break;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A461 File Offset: 0x00008861
		public virtual void OnEnable()
		{
			GraphModifier.activeModifiers.Add(this);
			if (GraphModifier.lastLateScanEvent == Time.frameCount)
			{
				this.OnLatePostScan();
			}
			if (GraphModifier.lastPostCacheEvent == Time.frameCount)
			{
				this.OnPostCacheLoad();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A498 File Offset: 0x00008898
		public virtual void OnDisable()
		{
			GraphModifier.activeModifiers.Remove(this);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A4A6 File Offset: 0x000088A6
		public virtual void OnPostScan()
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A4A8 File Offset: 0x000088A8
		public virtual void OnPreScan()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A4AA File Offset: 0x000088AA
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A4AC File Offset: 0x000088AC
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A4AE File Offset: 0x000088AE
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A4B0 File Offset: 0x000088B0
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x04000157 RID: 343
		private static List<GraphModifier> activeModifiers = new List<GraphModifier>();

		// Token: 0x04000158 RID: 344
		private static int lastLateScanEvent = -9999;

		// Token: 0x04000159 RID: 345
		private static int lastPostCacheEvent = -9999;

		// Token: 0x0200002D RID: 45
		public enum EventType
		{
			// Token: 0x0400015B RID: 347
			PostScan,
			// Token: 0x0400015C RID: 348
			PreScan,
			// Token: 0x0400015D RID: 349
			LatePostScan,
			// Token: 0x0400015E RID: 350
			PreUpdate,
			// Token: 0x0400015F RID: 351
			PostUpdate,
			// Token: 0x04000160 RID: 352
			PostCacheLoad
		}
	}
}
