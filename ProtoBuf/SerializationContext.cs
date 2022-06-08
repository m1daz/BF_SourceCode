using System;

namespace ProtoBuf
{
	// Token: 0x02000679 RID: 1657
	public sealed class SerializationContext
	{
		// Token: 0x060030DE RID: 12510 RVA: 0x00160061 File Offset: 0x0015E461
		static SerializationContext()
		{
			SerializationContext.@default.Freeze();
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0016007F File Offset: 0x0015E47F
		internal void Freeze()
		{
			this.frozen = true;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00160088 File Offset: 0x0015E488
		private void ThrowIfFrozen()
		{
			if (this.frozen)
			{
				throw new InvalidOperationException("The serialization-context cannot be changed once it is in use");
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x001600A0 File Offset: 0x0015E4A0
		// (set) Token: 0x060030E3 RID: 12515 RVA: 0x001600A8 File Offset: 0x0015E4A8
		public object Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (this.context != value)
				{
					this.ThrowIfFrozen();
					this.context = value;
				}
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x001600C3 File Offset: 0x0015E4C3
		internal static SerializationContext Default
		{
			get
			{
				return SerializationContext.@default;
			}
		}

		// Token: 0x04002E52 RID: 11858
		private bool frozen;

		// Token: 0x04002E53 RID: 11859
		private object context;

		// Token: 0x04002E54 RID: 11860
		private static readonly SerializationContext @default = new SerializationContext();
	}
}
