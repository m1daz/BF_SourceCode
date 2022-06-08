using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000660 RID: 1632
	public sealed class SubType
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x0015B620 File Offset: 0x00159A20
		public SubType(int fieldNumber, MetaType derivedType, DataFormat format)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			this.fieldNumber = fieldNumber;
			this.derivedType = derivedType;
			this.dataFormat = format;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x0015B660 File Offset: 0x00159A60
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x0015B668 File Offset: 0x00159A68
		public MetaType DerivedType
		{
			get
			{
				return this.derivedType;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x0015B670 File Offset: 0x00159A70
		internal IProtoSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = this.BuildSerializer();
				}
				return this.serializer;
			}
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0015B690 File Offset: 0x00159A90
		private IProtoSerializer BuildSerializer()
		{
			WireType wireType = WireType.String;
			if (this.dataFormat == DataFormat.Group)
			{
				wireType = WireType.StartGroup;
			}
			IProtoSerializer tail = new SubItemSerializer(this.derivedType.Type, this.derivedType.GetKey(false, false), this.derivedType, false);
			return new TagDecorator(this.fieldNumber, wireType, false, tail);
		}

		// Token: 0x04002DCF RID: 11727
		private readonly int fieldNumber;

		// Token: 0x04002DD0 RID: 11728
		private readonly MetaType derivedType;

		// Token: 0x04002DD1 RID: 11729
		private readonly DataFormat dataFormat;

		// Token: 0x04002DD2 RID: 11730
		private IProtoSerializer serializer;

		// Token: 0x02000661 RID: 1633
		internal sealed class Comparer : IComparer, IComparer<SubType>
		{
			// Token: 0x06002F92 RID: 12178 RVA: 0x0015B6E8 File Offset: 0x00159AE8
			public int Compare(object x, object y)
			{
				return this.Compare(x as SubType, y as SubType);
			}

			// Token: 0x06002F93 RID: 12179 RVA: 0x0015B6FC File Offset: 0x00159AFC
			public int Compare(SubType x, SubType y)
			{
				if (object.ReferenceEquals(x, y))
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}

			// Token: 0x04002DD3 RID: 11731
			public static readonly SubType.Comparer Default = new SubType.Comparer();
		}
	}
}
