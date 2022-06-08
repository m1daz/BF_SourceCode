using System;
using System.IO;

namespace ProtoBuf
{
	// Token: 0x0200064D RID: 1613
	public interface IExtension
	{
		// Token: 0x06002EAD RID: 11949
		Stream BeginAppend();

		// Token: 0x06002EAE RID: 11950
		void EndAppend(Stream stream, bool commit);

		// Token: 0x06002EAF RID: 11951
		Stream BeginQuery();

		// Token: 0x06002EB0 RID: 11952
		void EndQuery(Stream stream);

		// Token: 0x06002EB1 RID: 11953
		int GetLength();
	}
}
