using System;

namespace UnionAssets.FLE
{
	// Token: 0x020000DA RID: 218
	public class CEvent
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00038D3A File Offset: 0x0003713A
		public CEvent(int id, string name, object data, IDispatcher dispatcher)
		{
			this._id = id;
			this._name = name;
			this._data = data;
			this._dispatcher = dispatcher;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00038D5F File Offset: 0x0003715F
		public void stopPropagation()
		{
			this._isStoped = true;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00038D68 File Offset: 0x00037168
		public void stopImmediatePropagation()
		{
			this._isStoped = true;
			this._isLocked = true;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00038D78 File Offset: 0x00037178
		public bool canBeDisptached(object val)
		{
			if (this._isLocked)
			{
				return false;
			}
			if (this._isStoped)
			{
				return this._currentTarget == val;
			}
			this._currentTarget = val;
			return true;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00038DAA File Offset: 0x000371AA
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00038DB2 File Offset: 0x000371B2
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00038DBA File Offset: 0x000371BA
		public object data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00038DC2 File Offset: 0x000371C2
		public IDispatcher target
		{
			get
			{
				return this._dispatcher;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00038DCA File Offset: 0x000371CA
		public IDispatcher dispatcher
		{
			get
			{
				return this._dispatcher;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00038DD2 File Offset: 0x000371D2
		public object currentTarget
		{
			get
			{
				return this._currentTarget;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00038DDA File Offset: 0x000371DA
		public bool isStoped
		{
			get
			{
				return this._isStoped;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00038DE2 File Offset: 0x000371E2
		public bool isLocked
		{
			get
			{
				return this._isLocked;
			}
		}

		// Token: 0x0400058D RID: 1421
		private int _id;

		// Token: 0x0400058E RID: 1422
		private string _name;

		// Token: 0x0400058F RID: 1423
		private object _data;

		// Token: 0x04000590 RID: 1424
		private IDispatcher _dispatcher;

		// Token: 0x04000591 RID: 1425
		private bool _isStoped;

		// Token: 0x04000592 RID: 1426
		private bool _isLocked;

		// Token: 0x04000593 RID: 1427
		public object _currentTarget;
	}
}
