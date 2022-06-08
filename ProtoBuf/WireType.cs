using System;

namespace ProtoBuf
{
	// Token: 0x020006A8 RID: 1704
	public enum WireType
	{
		// Token: 0x04002EB8 RID: 11960
		None = -1,
		// Token: 0x04002EB9 RID: 11961
		Variant,
		// Token: 0x04002EBA RID: 11962
		Fixed64,
		// Token: 0x04002EBB RID: 11963
		String,
		// Token: 0x04002EBC RID: 11964
		StartGroup,
		// Token: 0x04002EBD RID: 11965
		EndGroup,
		// Token: 0x04002EBE RID: 11966
		Fixed32,
		// Token: 0x04002EBF RID: 11967
		SignedVariant = 8
	}
}
