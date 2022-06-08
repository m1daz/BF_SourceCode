using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200069C RID: 1692
	internal sealed class SubItemSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x060031C9 RID: 12745 RVA: 0x00162110 File Offset: 0x00160510
		public SubItemSerializer(Type type, int key, ISerializerProxy proxy, bool recursionCheck)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.type = type;
			this.proxy = proxy;
			this.key = key;
			this.recursionCheck = recursionCheck;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00162162 File Offset: 0x00160562
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).HasCallbacks(callbackType);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x0016217A File Offset: 0x0016057A
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CanCreateInstance();
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00162191 File Offset: 0x00160591
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			((IProtoTypeSerializer)this.proxy.Serializer).Callback(value, callbackType, context);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x001621AB File Offset: 0x001605AB
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CreateInstance(source);
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x001621C3 File Offset: 0x001605C3
		Type IProtoSerializer.ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x001621CB File Offset: 0x001605CB
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x001621CE File Offset: 0x001605CE
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x001621D1 File Offset: 0x001605D1
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			if (this.recursionCheck)
			{
				ProtoWriter.WriteObject(value, this.key, dest);
			}
			else
			{
				ProtoWriter.WriteRecursionSafeObject(value, this.key, dest);
			}
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x001621FD File Offset: 0x001605FD
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return ProtoReader.ReadObject(value, this.key, source);
		}

		// Token: 0x04002E95 RID: 11925
		private readonly int key;

		// Token: 0x04002E96 RID: 11926
		private readonly Type type;

		// Token: 0x04002E97 RID: 11927
		private readonly ISerializerProxy proxy;

		// Token: 0x04002E98 RID: 11928
		private readonly bool recursionCheck;
	}
}
