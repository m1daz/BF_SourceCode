using System;
using System.Collections.Generic;

namespace UnionAssets.FLE
{
	// Token: 0x020000D5 RID: 213
	public class EventDispatcherBase : IDispatcher
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x00038964 File Offset: 0x00036D64
		public void addEventListener(string eventName, EventHandlerFunction handler)
		{
			this.addEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00038974 File Offset: 0x00036D74
		public void addEventListener(int eventID, EventHandlerFunction handler)
		{
			this.addEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0003898C File Offset: 0x00036D8C
		private void addEventListener(int eventID, EventHandlerFunction handler, string eventGraphName)
		{
			if (this.listners.ContainsKey(eventID))
			{
				this.listners[eventID].Add(handler);
			}
			else
			{
				List<EventHandlerFunction> list = new List<EventHandlerFunction>();
				list.Add(handler);
				this.listners.Add(eventID, list);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000389DB File Offset: 0x00036DDB
		public void addEventListener(string eventName, DataEventHandlerFunction handler)
		{
			this.addEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000389EB File Offset: 0x00036DEB
		public void addEventListener(int eventID, DataEventHandlerFunction handler)
		{
			this.addEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00038A04 File Offset: 0x00036E04
		private void addEventListener(int eventID, DataEventHandlerFunction handler, string eventGraphName)
		{
			if (this.dataListners.ContainsKey(eventID))
			{
				this.dataListners[eventID].Add(handler);
			}
			else
			{
				List<DataEventHandlerFunction> list = new List<DataEventHandlerFunction>();
				list.Add(handler);
				this.dataListners.Add(eventID, list);
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00038A53 File Offset: 0x00036E53
		public void removeEventListener(string eventName, EventHandlerFunction handler)
		{
			this.removeEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00038A63 File Offset: 0x00036E63
		public void removeEventListener(int eventID, EventHandlerFunction handler)
		{
			this.removeEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00038A7C File Offset: 0x00036E7C
		public void removeEventListener(int eventID, EventHandlerFunction handler, string eventGraphName)
		{
			if (this.listners.ContainsKey(eventID))
			{
				List<EventHandlerFunction> list = this.listners[eventID];
				list.Remove(handler);
				if (list.Count == 0)
				{
					this.listners.Remove(eventID);
				}
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00038AC7 File Offset: 0x00036EC7
		public void removeEventListener(string eventName, DataEventHandlerFunction handler)
		{
			this.removeEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00038AD7 File Offset: 0x00036ED7
		public void removeEventListener(int eventID, DataEventHandlerFunction handler)
		{
			this.removeEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00038AF0 File Offset: 0x00036EF0
		public void removeEventListener(int eventID, DataEventHandlerFunction handler, string eventGraphName)
		{
			if (this.dataListners.ContainsKey(eventID))
			{
				List<DataEventHandlerFunction> list = this.dataListners[eventID];
				list.Remove(handler);
				if (list.Count == 0)
				{
					this.dataListners.Remove(eventID);
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00038B3B File Offset: 0x00036F3B
		public void dispatchEvent(string eventName)
		{
			this.dispatch(eventName.GetHashCode(), null, eventName);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00038B4B File Offset: 0x00036F4B
		public void dispatchEvent(string eventName, object data)
		{
			this.dispatch(eventName.GetHashCode(), data, eventName);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00038B5B File Offset: 0x00036F5B
		public void dispatchEvent(int eventID)
		{
			this.dispatch(eventID, null, string.Empty);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00038B6A File Offset: 0x00036F6A
		public void dispatchEvent(int eventID, object data)
		{
			this.dispatch(eventID, data, string.Empty);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00038B79 File Offset: 0x00036F79
		public void dispatch(string eventName)
		{
			this.dispatch(eventName.GetHashCode(), null, eventName);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00038B89 File Offset: 0x00036F89
		public void dispatch(string eventName, object data)
		{
			this.dispatch(eventName.GetHashCode(), data, eventName);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00038B99 File Offset: 0x00036F99
		public void dispatch(int eventID)
		{
			this.dispatch(eventID, null, string.Empty);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00038BA8 File Offset: 0x00036FA8
		public void dispatch(int eventID, object data)
		{
			this.dispatch(eventID, data, string.Empty);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00038BB8 File Offset: 0x00036FB8
		private void dispatch(int eventID, object data, string eventName)
		{
			CEvent cevent = new CEvent(eventID, eventName, data, this);
			if (this.dataListners.ContainsKey(eventID))
			{
				List<DataEventHandlerFunction> list = this.cloenArray(this.dataListners[eventID]);
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					if (cevent.canBeDisptached(list[i].Target))
					{
						list[i](cevent);
					}
				}
			}
			if (this.listners.ContainsKey(eventID))
			{
				List<EventHandlerFunction> list2 = this.cloenArray(this.listners[eventID]);
				int count2 = list2.Count;
				for (int j = 0; j < count2; j++)
				{
					if (cevent.canBeDisptached(list2[j].Target))
					{
						list2[j]();
					}
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00038C9C File Offset: 0x0003709C
		public void clearEvents()
		{
			this.listners.Clear();
			this.dataListners.Clear();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00038CB4 File Offset: 0x000370B4
		private List<EventHandlerFunction> cloenArray(List<EventHandlerFunction> list)
		{
			List<EventHandlerFunction> list2 = new List<EventHandlerFunction>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list2.Add(list[i]);
			}
			return list2;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00038CF0 File Offset: 0x000370F0
		private List<DataEventHandlerFunction> cloenArray(List<DataEventHandlerFunction> list)
		{
			List<DataEventHandlerFunction> list2 = new List<DataEventHandlerFunction>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list2.Add(list[i]);
			}
			return list2;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00038D2A File Offset: 0x0003712A
		public virtual void OnDestroy()
		{
			this.clearEvents();
		}

		// Token: 0x0400057D RID: 1405
		private Dictionary<int, List<EventHandlerFunction>> listners = new Dictionary<int, List<EventHandlerFunction>>();

		// Token: 0x0400057E RID: 1406
		private Dictionary<int, List<DataEventHandlerFunction>> dataListners = new Dictionary<int, List<DataEventHandlerFunction>>();
	}
}
