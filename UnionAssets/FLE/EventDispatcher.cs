using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnionAssets.FLE
{
	// Token: 0x020000D4 RID: 212
	public class EventDispatcher : MonoBehaviour, IDispatcher
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00038579 File Offset: 0x00036979
		public void addEventListener(string eventName, EventHandlerFunction handler)
		{
			this.addEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00038589 File Offset: 0x00036989
		public void addEventListener(int eventID, EventHandlerFunction handler)
		{
			this.addEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000385A0 File Offset: 0x000369A0
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

		// Token: 0x06000642 RID: 1602 RVA: 0x000385EF File Offset: 0x000369EF
		public void addEventListener(string eventName, DataEventHandlerFunction handler)
		{
			this.addEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000385FF File Offset: 0x000369FF
		public void addEventListener(int eventID, DataEventHandlerFunction handler)
		{
			this.addEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00038618 File Offset: 0x00036A18
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

		// Token: 0x06000645 RID: 1605 RVA: 0x00038667 File Offset: 0x00036A67
		public void removeEventListener(string eventName, EventHandlerFunction handler)
		{
			this.removeEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00038677 File Offset: 0x00036A77
		public void removeEventListener(int eventID, EventHandlerFunction handler)
		{
			this.removeEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00038690 File Offset: 0x00036A90
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

		// Token: 0x06000648 RID: 1608 RVA: 0x000386DB File Offset: 0x00036ADB
		public void removeEventListener(string eventName, DataEventHandlerFunction handler)
		{
			this.removeEventListener(eventName.GetHashCode(), handler, eventName);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000386EB File Offset: 0x00036AEB
		public void removeEventListener(int eventID, DataEventHandlerFunction handler)
		{
			this.removeEventListener(eventID, handler, eventID.ToString());
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00038704 File Offset: 0x00036B04
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

		// Token: 0x0600064B RID: 1611 RVA: 0x0003874F File Offset: 0x00036B4F
		public void dispatchEvent(string eventName)
		{
			this.dispatch(eventName.GetHashCode(), null, eventName);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0003875F File Offset: 0x00036B5F
		public void dispatchEvent(string eventName, object data)
		{
			this.dispatch(eventName.GetHashCode(), data, eventName);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0003876F File Offset: 0x00036B6F
		public void dispatchEvent(int eventID)
		{
			this.dispatch(eventID, null, string.Empty);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0003877E File Offset: 0x00036B7E
		public void dispatchEvent(int eventID, object data)
		{
			this.dispatch(eventID, data, string.Empty);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0003878D File Offset: 0x00036B8D
		public void dispatch(string eventName)
		{
			this.dispatch(eventName.GetHashCode(), null, eventName);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0003879D File Offset: 0x00036B9D
		public void dispatch(string eventName, object data)
		{
			this.dispatch(eventName.GetHashCode(), data, eventName);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000387AD File Offset: 0x00036BAD
		public void dispatch(int eventID)
		{
			this.dispatch(eventID, null, string.Empty);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000387BC File Offset: 0x00036BBC
		public void dispatch(int eventID, object data)
		{
			this.dispatch(eventID, data, string.Empty);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000387CC File Offset: 0x00036BCC
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

		// Token: 0x06000654 RID: 1620 RVA: 0x000388B0 File Offset: 0x00036CB0
		public void clearEvents()
		{
			this.listners.Clear();
			this.dataListners.Clear();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000388C8 File Offset: 0x00036CC8
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

		// Token: 0x06000656 RID: 1622 RVA: 0x00038904 File Offset: 0x00036D04
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

		// Token: 0x06000657 RID: 1623 RVA: 0x0003893E File Offset: 0x00036D3E
		protected virtual void OnDestroy()
		{
			this.clearEvents();
		}

		// Token: 0x0400057B RID: 1403
		private Dictionary<int, List<EventHandlerFunction>> listners = new Dictionary<int, List<EventHandlerFunction>>();

		// Token: 0x0400057C RID: 1404
		private Dictionary<int, List<DataEventHandlerFunction>> dataListners = new Dictionary<int, List<DataEventHandlerFunction>>();
	}
}
