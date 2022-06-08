using System;
using UnityEngine;

namespace GrowthSystem
{
	// Token: 0x020001CB RID: 459
	public class ItemManager : MonoBehaviour
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x00057E07 File Offset: 0x00056207
		private void Awake()
		{
			this.mItemBaseValue = new ItemBaseValue();
			ItemManager.mInstance = this;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00057E1A File Offset: 0x0005621A
		private void OnDestroy()
		{
			ItemManager.mInstance = null;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00057E22 File Offset: 0x00056222
		private void Start()
		{
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00057E24 File Offset: 0x00056224
		private void Update()
		{
		}

		// Token: 0x04000CF0 RID: 3312
		public static ItemManager mInstance;

		// Token: 0x04000CF1 RID: 3313
		public ItemBaseValue mItemBaseValue;
	}
}
