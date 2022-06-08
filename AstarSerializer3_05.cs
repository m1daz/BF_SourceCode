using System;
using System.IO;
using Pathfinding;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class AstarSerializer3_05 : AstarSerializer3_07
{
	// Token: 0x060002AE RID: 686 RVA: 0x00013855 File Offset: 0x00011C55
	public AstarSerializer3_05(AstarPath script) : base(script)
	{
	}

	// Token: 0x060002AF RID: 687 RVA: 0x00013860 File Offset: 0x00011C60
	public override UserConnection[] DeserializeUserConnections()
	{
		BinaryReader readerStream = this.readerStream;
		if (base.MoveToAnchor("UserConnections"))
		{
			int num = readerStream.ReadInt32();
			UserConnection[] array = new UserConnection[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new UserConnection
				{
					p1 = new Vector3(readerStream.ReadSingle(), readerStream.ReadSingle(), readerStream.ReadSingle()),
					p2 = new Vector3(readerStream.ReadSingle(), readerStream.ReadSingle(), readerStream.ReadSingle()),
					doOverrideCost = readerStream.ReadBoolean(),
					overrideCost = readerStream.ReadInt32(),
					oneWay = readerStream.ReadBoolean(),
					width = readerStream.ReadSingle(),
					type = (ConnectionType)readerStream.ReadInt32()
				};
			}
			return array;
		}
		return new UserConnection[0];
	}
}
