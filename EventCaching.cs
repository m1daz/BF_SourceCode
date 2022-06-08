using System;

// Token: 0x020000F8 RID: 248
public enum EventCaching : byte
{
	// Token: 0x040006B4 RID: 1716
	DoNotCache,
	// Token: 0x040006B5 RID: 1717
	[Obsolete]
	MergeCache,
	// Token: 0x040006B6 RID: 1718
	[Obsolete]
	ReplaceCache,
	// Token: 0x040006B7 RID: 1719
	[Obsolete]
	RemoveCache,
	// Token: 0x040006B8 RID: 1720
	AddToRoomCache,
	// Token: 0x040006B9 RID: 1721
	AddToRoomCacheGlobal,
	// Token: 0x040006BA RID: 1722
	RemoveFromRoomCache,
	// Token: 0x040006BB RID: 1723
	RemoveFromRoomCacheForActorsLeft,
	// Token: 0x040006BC RID: 1724
	SliceIncreaseIndex = 10,
	// Token: 0x040006BD RID: 1725
	SliceSetIndex,
	// Token: 0x040006BE RID: 1726
	SlicePurgeIndex,
	// Token: 0x040006BF RID: 1727
	SlicePurgeUpToIndex
}
