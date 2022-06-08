using System;

namespace UnionAssets.FLE
{
	// Token: 0x020000D9 RID: 217
	public interface IDispatcher
	{
		// Token: 0x0600067B RID: 1659
		void addEventListener(string eventName, EventHandlerFunction handler);

		// Token: 0x0600067C RID: 1660
		void addEventListener(int eventID, EventHandlerFunction handler);

		// Token: 0x0600067D RID: 1661
		void addEventListener(string eventName, DataEventHandlerFunction handler);

		// Token: 0x0600067E RID: 1662
		void addEventListener(int eventID, DataEventHandlerFunction handler);

		// Token: 0x0600067F RID: 1663
		void removeEventListener(string eventName, EventHandlerFunction handler);

		// Token: 0x06000680 RID: 1664
		void removeEventListener(int eventID, EventHandlerFunction handler);

		// Token: 0x06000681 RID: 1665
		void removeEventListener(string eventName, DataEventHandlerFunction handler);

		// Token: 0x06000682 RID: 1666
		void removeEventListener(int eventID, DataEventHandlerFunction handler);

		// Token: 0x06000683 RID: 1667
		void dispatchEvent(int eventID);

		// Token: 0x06000684 RID: 1668
		void dispatchEvent(int eventID, object data);

		// Token: 0x06000685 RID: 1669
		void dispatchEvent(string eventName);

		// Token: 0x06000686 RID: 1670
		void dispatchEvent(string eventName, object data);

		// Token: 0x06000687 RID: 1671
		void dispatch(int eventID);

		// Token: 0x06000688 RID: 1672
		void dispatch(int eventID, object data);

		// Token: 0x06000689 RID: 1673
		void dispatch(string eventName);

		// Token: 0x0600068A RID: 1674
		void dispatch(string eventName, object data);

		// Token: 0x0600068B RID: 1675
		void clearEvents();
	}
}
