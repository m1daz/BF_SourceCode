using System;
using System.IO;
using Pathfinding;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class AstarSerializer3_04 : AstarSerializer3_05
{
	// Token: 0x060002AC RID: 684 RVA: 0x00013935 File Offset: 0x00011D35
	public AstarSerializer3_04(AstarPath script) : base(script)
	{
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00013940 File Offset: 0x00011D40
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
