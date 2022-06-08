using System;

namespace ProtoBuf
{
	// Token: 0x0200066D RID: 1645
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class ProtoContractAttribute : Attribute
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x0015C8B7 File Offset: 0x0015ACB7
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x0015C8BF File Offset: 0x0015ACBF
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x0015C8C8 File Offset: 0x0015ACC8
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x0015C8D0 File Offset: 0x0015ACD0
		public int ImplicitFirstTag
		{
			get
			{
				return this.implicitFirstTag;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ImplicitFirstTag");
				}
				this.implicitFirstTag = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x0015C8EB File Offset: 0x0015ACEB
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x0015C8F4 File Offset: 0x0015ACF4
		public bool UseProtoMembersOnly
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x0015C8FE File Offset: 0x0015ACFE
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x0015C908 File Offset: 0x0015AD08
		public bool IgnoreListHandling
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x0015C913 File Offset: 0x0015AD13
		// (set) Token: 0x06003022 RID: 12322 RVA: 0x0015C91B File Offset: 0x0015AD1B
		public ImplicitFields ImplicitFields
		{
			get
			{
				return this.implicitFields;
			}
			set
			{
				this.implicitFields = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0015C924 File Offset: 0x0015AD24
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x0015C92D File Offset: 0x0015AD2D
		public bool InferTagFromName
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value);
				this.SetFlag(2, true);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x0015C93F File Offset: 0x0015AD3F
		internal bool InferTagFromNameHasValue
		{
			get
			{
				return this.HasFlag(2);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x0015C948 File Offset: 0x0015AD48
		// (set) Token: 0x06003027 RID: 12327 RVA: 0x0015C950 File Offset: 0x0015AD50
		public int DataMemberOffset
		{
			get
			{
				return this.dataMemberOffset;
			}
			set
			{
				this.dataMemberOffset = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x0015C959 File Offset: 0x0015AD59
		// (set) Token: 0x06003029 RID: 12329 RVA: 0x0015C962 File Offset: 0x0015AD62
		public bool SkipConstructor
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x0015C96C File Offset: 0x0015AD6C
		// (set) Token: 0x0600302B RID: 12331 RVA: 0x0015C976 File Offset: 0x0015AD76
		public bool AsReferenceDefault
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value);
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0015C981 File Offset: 0x0015AD81
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0015C98E File Offset: 0x0015AD8E
		private void SetFlag(byte flag, bool value)
		{
			if (value)
			{
				this.flags |= flag;
			}
			else
			{
				this.flags &= ~flag;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x0015C9BA File Offset: 0x0015ADBA
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x0015C9C4 File Offset: 0x0015ADC4
		public bool EnumPassthru
		{
			get
			{
				return this.HasFlag(64);
			}
			set
			{
				this.SetFlag(64, value);
				this.SetFlag(128, true);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x0015C9DB File Offset: 0x0015ADDB
		internal bool EnumPassthruHasValue
		{
			get
			{
				return this.HasFlag(128);
			}
		}

		// Token: 0x04002E09 RID: 11785
		private string name;

		// Token: 0x04002E0A RID: 11786
		private int implicitFirstTag;

		// Token: 0x04002E0B RID: 11787
		private ImplicitFields implicitFields;

		// Token: 0x04002E0C RID: 11788
		private int dataMemberOffset;

		// Token: 0x04002E0D RID: 11789
		private byte flags;

		// Token: 0x04002E0E RID: 11790
		private const byte OPTIONS_InferTagFromName = 1;

		// Token: 0x04002E0F RID: 11791
		private const byte OPTIONS_InferTagFromNameHasValue = 2;

		// Token: 0x04002E10 RID: 11792
		private const byte OPTIONS_UseProtoMembersOnly = 4;

		// Token: 0x04002E11 RID: 11793
		private const byte OPTIONS_SkipConstructor = 8;

		// Token: 0x04002E12 RID: 11794
		private const byte OPTIONS_IgnoreListHandling = 16;

		// Token: 0x04002E13 RID: 11795
		private const byte OPTIONS_AsReferenceDefault = 32;

		// Token: 0x04002E14 RID: 11796
		private const byte OPTIONS_EnumPassthru = 64;

		// Token: 0x04002E15 RID: 11797
		private const byte OPTIONS_EnumPassthruHasValue = 128;
	}
}
