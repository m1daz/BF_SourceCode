using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200069B RID: 1691
	internal sealed class StringSerializer : IProtoSerializer
	{
		// Token: 0x060031C2 RID: 12738 RVA: 0x001620D2 File Offset: 0x001604D2
		public StringSerializer(TypeModel model)
		{
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x001620DA File Offset: 0x001604DA
		public Type ExpectedType
		{
			get
			{
				return StringSerializer.expectedType;
			}
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x001620E1 File Offset: 0x001604E1
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString((string)value, dest);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x001620EF File Offset: 0x001604EF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x001620F2 File Offset: 0x001604F2
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x001620F5 File Offset: 0x001604F5
		public object Read(object value, ProtoReader source)
		{
			return source.ReadString();
		}

		// Token: 0x04002E94 RID: 11924
		private static readonly Type expectedType = typeof(string);
	}
}
