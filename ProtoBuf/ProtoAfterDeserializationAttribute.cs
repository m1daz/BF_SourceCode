using System;
using System.ComponentModel;

namespace ProtoBuf
{
	// Token: 0x02000646 RID: 1606
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	public sealed class ProtoAfterDeserializationAttribute : Attribute
	{
	}
}
