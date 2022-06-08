using System;

namespace ProtoBuf
{
	// Token: 0x0200066F RID: 1647
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ProtoEnumAttribute : Attribute
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x0015C9F8 File Offset: 0x0015ADF8
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x0015CA00 File Offset: 0x0015AE00
		public int Value
		{
			get
			{
				return this.enumValue;
			}
			set
			{
				this.enumValue = value;
				this.hasValue = true;
			}
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0015CA10 File Offset: 0x0015AE10
		public bool HasValue()
		{
			return this.hasValue;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x0015CA18 File Offset: 0x0015AE18
		// (set) Token: 0x06003037 RID: 12343 RVA: 0x0015CA20 File Offset: 0x0015AE20
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04002E16 RID: 11798
		private bool hasValue;

		// Token: 0x04002E17 RID: 11799
		private int enumValue;

		// Token: 0x04002E18 RID: 11800
		private string name;
	}
}
