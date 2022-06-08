using System;
using System.ComponentModel;

namespace ProtoBuf
{
	// Token: 0x02000644 RID: 1604
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	public sealed class ProtoAfterSerializationAttribute : Attribute
	{
	}
}
