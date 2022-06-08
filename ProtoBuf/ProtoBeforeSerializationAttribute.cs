using System;
using System.ComponentModel;

namespace ProtoBuf
{
	// Token: 0x02000643 RID: 1603
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	public sealed class ProtoBeforeSerializationAttribute : Attribute
	{
	}
}
