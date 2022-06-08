using System;

namespace ProtoBuf.Meta
{
	// Token: 0x02000651 RID: 1617
	internal sealed class MutableList : BasicList
	{
		// Token: 0x17000380 RID: 896
		public new object this[int index]
		{
			get
			{
				return this.head[index];
			}
			set
			{
				this.head[index] = value;
			}
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x001555AC File Offset: 0x001539AC
		public void RemoveLast()
		{
			this.head.RemoveLastWithMutate();
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x001555B9 File Offset: 0x001539B9
		public void Clear()
		{
			this.head.Clear();
		}
	}
}
