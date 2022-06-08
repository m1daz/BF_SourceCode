using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A6 RID: 1702
	internal sealed class UriDecorator : ProtoDecoratorBase
	{
		// Token: 0x06003228 RID: 12840 RVA: 0x00163175 File Offset: 0x00161575
		public UriDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x0016317E File Offset: 0x0016157E
		public override Type ExpectedType
		{
			get
			{
				return UriDecorator.expectedType;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x00163185 File Offset: 0x00161585
		public override bool RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x00163188 File Offset: 0x00161588
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x0016318B File Offset: 0x0016158B
		public override void Write(object value, ProtoWriter dest)
		{
			this.Tail.Write(((Uri)value).AbsoluteUri, dest);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x001631A4 File Offset: 0x001615A4
		public override object Read(object value, ProtoReader source)
		{
			string text = (string)this.Tail.Read(null, source);
			return (text.Length != 0) ? new Uri(text) : null;
		}

		// Token: 0x04002EB5 RID: 11957
		private static readonly Type expectedType = typeof(Uri);
	}
}
