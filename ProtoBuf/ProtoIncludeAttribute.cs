using System;
using System.ComponentModel;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000673 RID: 1651
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public sealed class ProtoIncludeAttribute : Attribute
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x0015CA79 File Offset: 0x0015AE79
		public ProtoIncludeAttribute(int tag, Type knownType) : this(tag, (knownType != null) ? knownType.AssemblyQualifiedName : string.Empty)
		{
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0015CA98 File Offset: 0x0015AE98
		public ProtoIncludeAttribute(int tag, string knownTypeName)
		{
			if (tag <= 0)
			{
				throw new ArgumentOutOfRangeException("tag", "Tags must be positive integers");
			}
			if (Helpers.IsNullOrEmpty(knownTypeName))
			{
				throw new ArgumentNullException("knownTypeName", "Known type cannot be blank");
			}
			this.tag = tag;
			this.knownTypeName = knownTypeName;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0015CAEB File Offset: 0x0015AEEB
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x0015CAF3 File Offset: 0x0015AEF3
		public string KnownTypeName
		{
			get
			{
				return this.knownTypeName;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0015CAFB File Offset: 0x0015AEFB
		public Type KnownType
		{
			get
			{
				return TypeModel.ResolveKnownType(this.KnownTypeName, null, null);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x0015CB0A File Offset: 0x0015AF0A
		// (set) Token: 0x06003044 RID: 12356 RVA: 0x0015CB12 File Offset: 0x0015AF12
		[DefaultValue(DataFormat.Default)]
		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.dataFormat = value;
			}
		}

		// Token: 0x04002E1A RID: 11802
		private readonly int tag;

		// Token: 0x04002E1B RID: 11803
		private readonly string knownTypeName;

		// Token: 0x04002E1C RID: 11804
		private DataFormat dataFormat;
	}
}
