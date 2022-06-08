using System;
using ProtoBuf;

// Token: 0x02000518 RID: 1304
[ProtoContract]
public class GGVector3
{
	// Token: 0x06002488 RID: 9352 RVA: 0x0011317B File Offset: 0x0011157B
	public GGVector3(float x, float y, float z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

	// Token: 0x04002575 RID: 9589
	[ProtoMember(1, IsRequired = true)]
	public float X;

	// Token: 0x04002576 RID: 9590
	[ProtoMember(2, IsRequired = true)]
	public float Y;

	// Token: 0x04002577 RID: 9591
	[ProtoMember(3, IsRequired = true)]
	public float Z;

	// Token: 0x04002578 RID: 9592
	[ProtoMember(4, IsRequired = true)]
	public int ID;

	// Token: 0x04002579 RID: 9593
	[ProtoMember(5, IsRequired = true)]
	public float curTime;
}
