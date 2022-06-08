using System;
using System.ComponentModel;

namespace ProtoBuf
{
	// Token: 0x02000645 RID: 1605
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	public sealed class ProtoBeforeDeserializationAttribute : Attribute
	{
	}
}
