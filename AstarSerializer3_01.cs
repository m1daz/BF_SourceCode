using System;
using System.IO;
using Pathfinding;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class AstarSerializer3_01 : AstarSerializer3_04
{
	// Token: 0x060002A9 RID: 681 RVA: 0x00013A08 File Offset: 0x00011E08
	public AstarSerializer3_01(AstarPath script) : base(script)
	{
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00013A14 File Offset: 0x00011E14
	public override void SerializeUserConnections(UserConnection[] userConnections)
	{
		Debug.Log("Loading from 3.0.1");
		BinaryWriter writerStream = this.writerStream;
		base.AddAnchor("UserConnections");
		if (userConnections != null)
		{
			writerStream.Write(userConnections.Length);
			foreach (UserConnection userConnection in userConnections)
			{
				writerStream.Write(userConnection.p1.x);
				writerStream.Write(userConnection.p1.y);
				writerStream.Write(userConnection.p1.z);
				writerStream.Write(userConnection.p2.x);
				writerStream.Write(userConnection.p2.y);
				writerStream.Write(userConnection.p2.z);
				writerStream.Write(userConnection.overrideCost);
				writerStream.Write(userConnection.oneWay);
				writerStream.Write(userConnection.width);
				Debug.Log("End - " + writerStream.BaseStream.Position);
			}
		}
		else
		{
			writerStream.Write(0);
		}
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00013B1C File Offset: 0x00011F1C
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
					width = readerStream.ReadSingle()
				};
			}
			return array;
		}
		return new UserConnection[0];
	}
}
