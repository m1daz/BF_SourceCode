using System;
using System.Reflection;

namespace ProtoBuf
{
	// Token: 0x02000674 RID: 1652
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ProtoMemberAttribute : Attribute, IComparable, IComparable<ProtoMemberAttribute>
	{
		// Token: 0x06003045 RID: 12357 RVA: 0x0015CB1B File Offset: 0x0015AF1B
		public ProtoMemberAttribute(int tag) : this(tag, false)
		{
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0015CB25 File Offset: 0x0015AF25
		internal ProtoMemberAttribute(int tag, bool forced)
		{
			if (tag <= 0 && !forced)
			{
				throw new ArgumentOutOfRangeException("tag");
			}
			this.tag = tag;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0015CB4C File Offset: 0x0015AF4C
		public int CompareTo(object other)
		{
			return this.CompareTo(other as ProtoMemberAttribute);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x0015CB5C File Offset: 0x0015AF5C
		public int CompareTo(ProtoMemberAttribute other)
		{
			if (other == null)
			{
				return -1;
			}
			if (this == other)
			{
				return 0;
			}
			int num = this.tag.CompareTo(other.tag);
			if (num == 0)
			{
				num = string.CompareOrdinal(this.name, other.name);
			}
			return num;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x0015CBA5 File Offset: 0x0015AFA5
		// (set) Token: 0x0600304A RID: 12362 RVA: 0x0015CBAD File Offset: 0x0015AFAD
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x0015CBB6 File Offset: 0x0015AFB6
		// (set) Token: 0x0600304C RID: 12364 RVA: 0x0015CBBE File Offset: 0x0015AFBE
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

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x0015CBC7 File Offset: 0x0015AFC7
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x0015CBCF File Offset: 0x0015AFCF
		internal void Rebase(int tag)
		{
			this.tag = tag;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0015CBD8 File Offset: 0x0015AFD8
		// (set) Token: 0x06003050 RID: 12368 RVA: 0x0015CBE5 File Offset: 0x0015AFE5
		public bool IsRequired
		{
			get
			{
				return (this.options & MemberSerializationOptions.Required) == MemberSerializationOptions.Required;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Required;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.Required;
				}
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x0015CC0F File Offset: 0x0015B00F
		// (set) Token: 0x06003052 RID: 12370 RVA: 0x0015CC1C File Offset: 0x0015B01C
		public bool IsPacked
		{
			get
			{
				return (this.options & MemberSerializationOptions.Packed) == MemberSerializationOptions.Packed;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Packed;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.Packed;
				}
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x0015CC46 File Offset: 0x0015B046
		// (set) Token: 0x06003054 RID: 12372 RVA: 0x0015CC55 File Offset: 0x0015B055
		public bool OverwriteList
		{
			get
			{
				return (this.options & MemberSerializationOptions.OverwriteList) == MemberSerializationOptions.OverwriteList;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.OverwriteList;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.OverwriteList;
				}
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x0015CC80 File Offset: 0x0015B080
		// (set) Token: 0x06003056 RID: 12374 RVA: 0x0015CC8D File Offset: 0x0015B08D
		public bool AsReference
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReference) == MemberSerializationOptions.AsReference;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReference;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.AsReference;
				}
				this.options |= MemberSerializationOptions.AsReferenceHasValue;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x0015CCC6 File Offset: 0x0015B0C6
		// (set) Token: 0x06003058 RID: 12376 RVA: 0x0015CCD5 File Offset: 0x0015B0D5
		internal bool AsReferenceHasValue
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReferenceHasValue) == MemberSerializationOptions.AsReferenceHasValue;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReferenceHasValue;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.AsReferenceHasValue;
				}
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x0015CD00 File Offset: 0x0015B100
		// (set) Token: 0x0600305A RID: 12378 RVA: 0x0015CD0D File Offset: 0x0015B10D
		public bool DynamicType
		{
			get
			{
				return (this.options & MemberSerializationOptions.DynamicType) == MemberSerializationOptions.DynamicType;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.DynamicType;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.DynamicType;
				}
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x0015CD37 File Offset: 0x0015B137
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x0015CD3F File Offset: 0x0015B13F
		public MemberSerializationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x04002E1D RID: 11805
		internal MemberInfo Member;

		// Token: 0x04002E1E RID: 11806
		internal bool TagIsPinned;

		// Token: 0x04002E1F RID: 11807
		private string name;

		// Token: 0x04002E20 RID: 11808
		private DataFormat dataFormat;

		// Token: 0x04002E21 RID: 11809
		private int tag;

		// Token: 0x04002E22 RID: 11810
		private MemberSerializationOptions options;
	}
}
