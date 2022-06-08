using System;

namespace ProtoBuf
{
	// Token: 0x0200064C RID: 1612
	public interface IExtensible
	{
		// Token: 0x06002EAC RID: 11948
		IExtension GetExtensionObject(bool createIfMissing);
	}
}
