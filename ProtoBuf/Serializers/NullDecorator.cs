using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000695 RID: 1685
	internal sealed class NullDecorator : ProtoDecoratorBase
	{
		// Token: 0x06003197 RID: 12695 RVA: 0x00161BE4 File Offset: 0x0015FFE4
		public NullDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
			if (!tail.ReturnsValue)
			{
				throw new NotSupportedException("NullDecorator only supports implementations that return values");
			}
			Type type = tail.ExpectedType;
			if (Helpers.IsValueType(type))
			{
				this.expectedType = model.MapType(typeof(Nullable<>)).MakeGenericType(new Type[]
				{
					type
				});
			}
			else
			{
				this.expectedType = type;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x00161C51 File Offset: 0x00160051
		public override Type ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x00161C59 File Offset: 0x00160059
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x00161C5C File Offset: 0x0016005C
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x00161C60 File Offset: 0x00160060
		public override object Read(object value, ProtoReader source)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				if (num == 1)
				{
					value = this.Tail.Read(value, source);
				}
				else
				{
					source.SkipField();
				}
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00161CB0 File Offset: 0x001600B0
		public override void Write(object value, ProtoWriter dest)
		{
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x04002E8A RID: 11914
		private readonly Type expectedType;

		// Token: 0x04002E8B RID: 11915
		public const int Tag = 1;
	}
}
