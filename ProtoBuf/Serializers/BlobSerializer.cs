using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200067F RID: 1663
	internal sealed class BlobSerializer : IProtoSerializer
	{
		// Token: 0x0600310A RID: 12554 RVA: 0x0016069C File Offset: 0x0015EA9C
		public BlobSerializer(TypeModel model, bool overwriteList)
		{
			this.overwriteList = overwriteList;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x001606AB File Offset: 0x0015EAAB
		public Type ExpectedType
		{
			get
			{
				return BlobSerializer.expectedType;
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x001606B2 File Offset: 0x0015EAB2
		public object Read(object value, ProtoReader source)
		{
			return ProtoReader.AppendBytes((!this.overwriteList) ? ((byte[])value) : null, source);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x001606D1 File Offset: 0x0015EAD1
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBytes((byte[])value, dest);
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x001606DF File Offset: 0x0015EADF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return !this.overwriteList;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x001606EA File Offset: 0x0015EAEA
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002E5F RID: 11871
		private static readonly Type expectedType = typeof(byte[]);

		// Token: 0x04002E60 RID: 11872
		private readonly bool overwriteList;
	}
}
