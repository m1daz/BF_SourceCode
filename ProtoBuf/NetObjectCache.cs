using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x0200066A RID: 1642
	internal sealed class NetObjectCache
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x0015C56F File Offset: 0x0015A96F
		private MutableList List
		{
			get
			{
				if (this.underlyingList == null)
				{
					this.underlyingList = new MutableList();
				}
				return this.underlyingList;
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x0015C590 File Offset: 0x0015A990
		internal object GetKeyedObject(int key)
		{
			if (key-- == 0)
			{
				if (this.rootObject == null)
				{
					throw new ProtoException("No root object assigned");
				}
				return this.rootObject;
			}
			else
			{
				BasicList list = this.List;
				if (key < 0 || key >= list.Count)
				{
					throw new ProtoException("Internal error; a missing key occurred");
				}
				object obj = list[key];
				if (obj == null)
				{
					throw new ProtoException("A deferred key does not have a value yet");
				}
				return obj;
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x0015C604 File Offset: 0x0015AA04
		internal void SetKeyedObject(int key, object value)
		{
			if (key-- == 0)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.rootObject != null && this.rootObject != value)
				{
					throw new ProtoException("The root object cannot be reassigned");
				}
				this.rootObject = value;
			}
			else
			{
				MutableList list = this.List;
				if (key < list.Count)
				{
					object obj = list[key];
					if (obj == null)
					{
						list[key] = value;
					}
					else if (!object.ReferenceEquals(obj, value))
					{
						throw new ProtoException("Reference-tracked objects cannot change reference");
					}
				}
				else if (key != list.Add(value))
				{
					throw new ProtoException("Internal error; a key mismatch occurred");
				}
			}
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x0015C6C0 File Offset: 0x0015AAC0
		internal int AddObjectKey(object value, out bool existing)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value == this.rootObject)
			{
				existing = true;
				return 0;
			}
			string text = value as string;
			BasicList list = this.List;
			int num;
			if (text == null)
			{
				if (this.objectKeys == null)
				{
					this.objectKeys = new Dictionary<object, int>(NetObjectCache.ReferenceComparer.Default);
					num = -1;
				}
				else if (!this.objectKeys.TryGetValue(value, out num))
				{
					num = -1;
				}
			}
			else if (this.stringKeys == null)
			{
				this.stringKeys = new Dictionary<string, int>();
				num = -1;
			}
			else if (!this.stringKeys.TryGetValue(text, out num))
			{
				num = -1;
			}
			if (!(existing = (num >= 0)))
			{
				num = list.Add(value);
				if (text == null)
				{
					this.objectKeys.Add(value, num);
				}
				else
				{
					this.stringKeys.Add(text, num);
				}
			}
			return num + 1;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x0015C7B4 File Offset: 0x0015ABB4
		internal void RegisterTrappedObject(object value)
		{
			if (this.rootObject == null)
			{
				this.rootObject = value;
			}
			else if (this.underlyingList != null)
			{
				for (int i = this.trapStartIndex; i < this.underlyingList.Count; i++)
				{
					this.trapStartIndex = i + 1;
					if (this.underlyingList[i] == null)
					{
						this.underlyingList[i] = value;
						break;
					}
				}
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x0015C830 File Offset: 0x0015AC30
		internal void Clear()
		{
			this.trapStartIndex = 0;
			this.rootObject = null;
			if (this.underlyingList != null)
			{
				this.underlyingList.Clear();
			}
			if (this.stringKeys != null)
			{
				this.stringKeys.Clear();
			}
			if (this.objectKeys != null)
			{
				this.objectKeys.Clear();
			}
		}

		// Token: 0x04002DFD RID: 11773
		internal const int Root = 0;

		// Token: 0x04002DFE RID: 11774
		private MutableList underlyingList;

		// Token: 0x04002DFF RID: 11775
		private object rootObject;

		// Token: 0x04002E00 RID: 11776
		private int trapStartIndex;

		// Token: 0x04002E01 RID: 11777
		private Dictionary<string, int> stringKeys;

		// Token: 0x04002E02 RID: 11778
		private Dictionary<object, int> objectKeys;

		// Token: 0x0200066B RID: 1643
		private sealed class ReferenceComparer : IEqualityComparer<object>
		{
			// Token: 0x06003014 RID: 12308 RVA: 0x0015C88D File Offset: 0x0015AC8D
			private ReferenceComparer()
			{
			}

			// Token: 0x06003015 RID: 12309 RVA: 0x0015C895 File Offset: 0x0015AC95
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06003016 RID: 12310 RVA: 0x0015C89B File Offset: 0x0015AC9B
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x04002E03 RID: 11779
			public static readonly NetObjectCache.ReferenceComparer Default = new NetObjectCache.ReferenceComparer();
		}
	}
}
