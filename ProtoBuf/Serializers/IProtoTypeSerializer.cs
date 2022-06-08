using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000690 RID: 1680
	internal interface IProtoTypeSerializer : IProtoSerializer
	{
		// Token: 0x06003175 RID: 12661
		bool HasCallbacks(TypeModel.CallbackType callbackType);

		// Token: 0x06003176 RID: 12662
		bool CanCreateInstance();

		// Token: 0x06003177 RID: 12663
		object CreateInstance(ProtoReader source);

		// Token: 0x06003178 RID: 12664
		void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context);
	}
}
