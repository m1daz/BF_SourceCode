using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x020000DE RID: 222
internal static class CustomTypes
{
	// Token: 0x060006A3 RID: 1699 RVA: 0x00039058 File Offset: 0x00037458
	internal static void Register()
	{
		Type typeFromHandle = typeof(Vector2);
		byte code = 87;
		if (CustomTypes.<>f__mg$cache0 == null)
		{
			CustomTypes.<>f__mg$cache0 = new SerializeStreamMethod(CustomTypes.SerializeVector2);
		}
		SerializeStreamMethod serializeMethod = CustomTypes.<>f__mg$cache0;
		if (CustomTypes.<>f__mg$cache1 == null)
		{
			CustomTypes.<>f__mg$cache1 = new DeserializeStreamMethod(CustomTypes.DeserializeVector2);
		}
		PhotonPeer.RegisterType(typeFromHandle, code, serializeMethod, CustomTypes.<>f__mg$cache1);
		Type typeFromHandle2 = typeof(Vector3);
		byte code2 = 86;
		if (CustomTypes.<>f__mg$cache2 == null)
		{
			CustomTypes.<>f__mg$cache2 = new SerializeStreamMethod(CustomTypes.SerializeVector3);
		}
		SerializeStreamMethod serializeMethod2 = CustomTypes.<>f__mg$cache2;
		if (CustomTypes.<>f__mg$cache3 == null)
		{
			CustomTypes.<>f__mg$cache3 = new DeserializeStreamMethod(CustomTypes.DeserializeVector3);
		}
		PhotonPeer.RegisterType(typeFromHandle2, code2, serializeMethod2, CustomTypes.<>f__mg$cache3);
		Type typeFromHandle3 = typeof(Quaternion);
		byte code3 = 81;
		if (CustomTypes.<>f__mg$cache4 == null)
		{
			CustomTypes.<>f__mg$cache4 = new SerializeStreamMethod(CustomTypes.SerializeQuaternion);
		}
		SerializeStreamMethod serializeMethod3 = CustomTypes.<>f__mg$cache4;
		if (CustomTypes.<>f__mg$cache5 == null)
		{
			CustomTypes.<>f__mg$cache5 = new DeserializeStreamMethod(CustomTypes.DeserializeQuaternion);
		}
		PhotonPeer.RegisterType(typeFromHandle3, code3, serializeMethod3, CustomTypes.<>f__mg$cache5);
		Type typeFromHandle4 = typeof(PhotonPlayer);
		byte code4 = 80;
		if (CustomTypes.<>f__mg$cache6 == null)
		{
			CustomTypes.<>f__mg$cache6 = new SerializeStreamMethod(CustomTypes.SerializePhotonPlayer);
		}
		SerializeStreamMethod serializeMethod4 = CustomTypes.<>f__mg$cache6;
		if (CustomTypes.<>f__mg$cache7 == null)
		{
			CustomTypes.<>f__mg$cache7 = new DeserializeStreamMethod(CustomTypes.DeserializePhotonPlayer);
		}
		PhotonPeer.RegisterType(typeFromHandle4, code4, serializeMethod4, CustomTypes.<>f__mg$cache7);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00039198 File Offset: 0x00037598
	private static short SerializeVector3(StreamBuffer outStream, object customobject)
	{
		Vector3 vector = (Vector3)customobject;
		int num = 0;
		object obj = CustomTypes.memVector3;
		lock (obj)
		{
			byte[] array = CustomTypes.memVector3;
			Protocol.Serialize(vector.x, array, ref num);
			Protocol.Serialize(vector.y, array, ref num);
			Protocol.Serialize(vector.z, array, ref num);
			outStream.Write(array, 0, 12);
		}
		return 12;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00039218 File Offset: 0x00037618
	private static object DeserializeVector3(StreamBuffer inStream, short length)
	{
		Vector3 vector = default(Vector3);
		object obj = CustomTypes.memVector3;
		lock (obj)
		{
			inStream.Read(CustomTypes.memVector3, 0, 12);
			int num = 0;
			Protocol.Deserialize(out vector.x, CustomTypes.memVector3, ref num);
			Protocol.Deserialize(out vector.y, CustomTypes.memVector3, ref num);
			Protocol.Deserialize(out vector.z, CustomTypes.memVector3, ref num);
		}
		return vector;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x000392A8 File Offset: 0x000376A8
	private static short SerializeVector2(StreamBuffer outStream, object customobject)
	{
		Vector2 vector = (Vector2)customobject;
		object obj = CustomTypes.memVector2;
		lock (obj)
		{
			byte[] array = CustomTypes.memVector2;
			int num = 0;
			Protocol.Serialize(vector.x, array, ref num);
			Protocol.Serialize(vector.y, array, ref num);
			outStream.Write(array, 0, 8);
		}
		return 8;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00039314 File Offset: 0x00037714
	private static object DeserializeVector2(StreamBuffer inStream, short length)
	{
		Vector2 vector = default(Vector2);
		object obj = CustomTypes.memVector2;
		lock (obj)
		{
			inStream.Read(CustomTypes.memVector2, 0, 8);
			int num = 0;
			Protocol.Deserialize(out vector.x, CustomTypes.memVector2, ref num);
			Protocol.Deserialize(out vector.y, CustomTypes.memVector2, ref num);
		}
		return vector;
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00039390 File Offset: 0x00037790
	private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
	{
		Quaternion quaternion = (Quaternion)customobject;
		object obj = CustomTypes.memQuarternion;
		lock (obj)
		{
			byte[] array = CustomTypes.memQuarternion;
			int num = 0;
			Protocol.Serialize(quaternion.w, array, ref num);
			Protocol.Serialize(quaternion.x, array, ref num);
			Protocol.Serialize(quaternion.y, array, ref num);
			Protocol.Serialize(quaternion.z, array, ref num);
			outStream.Write(array, 0, 16);
		}
		return 16;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0003941C File Offset: 0x0003781C
	private static object DeserializeQuaternion(StreamBuffer inStream, short length)
	{
		Quaternion quaternion = default(Quaternion);
		object obj = CustomTypes.memQuarternion;
		lock (obj)
		{
			inStream.Read(CustomTypes.memQuarternion, 0, 16);
			int num = 0;
			Protocol.Deserialize(out quaternion.w, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.x, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.y, CustomTypes.memQuarternion, ref num);
			Protocol.Deserialize(out quaternion.z, CustomTypes.memQuarternion, ref num);
		}
		return quaternion;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x000394BC File Offset: 0x000378BC
	private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
	{
		int id = ((PhotonPlayer)customobject).ID;
		object obj = CustomTypes.memPlayer;
		short result;
		lock (obj)
		{
			byte[] array = CustomTypes.memPlayer;
			int num = 0;
			Protocol.Serialize(id, array, ref num);
			outStream.Write(array, 0, 4);
			result = 4;
		}
		return result;
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0003951C File Offset: 0x0003791C
	private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
	{
		object obj = CustomTypes.memPlayer;
		int key;
		lock (obj)
		{
			inStream.Read(CustomTypes.memPlayer, 0, (int)length);
			int num = 0;
			Protocol.Deserialize(out key, CustomTypes.memPlayer, ref num);
		}
		if (PhotonNetwork.networkingPeer.mActors.ContainsKey(key))
		{
			return PhotonNetwork.networkingPeer.mActors[key];
		}
		return null;
	}

	// Token: 0x040005AB RID: 1451
	public static readonly byte[] memVector3 = new byte[12];

	// Token: 0x040005AC RID: 1452
	public static readonly byte[] memVector2 = new byte[8];

	// Token: 0x040005AD RID: 1453
	public static readonly byte[] memQuarternion = new byte[16];

	// Token: 0x040005AE RID: 1454
	public static readonly byte[] memPlayer = new byte[4];

	// Token: 0x040005AF RID: 1455
	[CompilerGenerated]
	private static SerializeStreamMethod <>f__mg$cache0;

	// Token: 0x040005B0 RID: 1456
	[CompilerGenerated]
	private static DeserializeStreamMethod <>f__mg$cache1;

	// Token: 0x040005B1 RID: 1457
	[CompilerGenerated]
	private static SerializeStreamMethod <>f__mg$cache2;

	// Token: 0x040005B2 RID: 1458
	[CompilerGenerated]
	private static DeserializeStreamMethod <>f__mg$cache3;

	// Token: 0x040005B3 RID: 1459
	[CompilerGenerated]
	private static SerializeStreamMethod <>f__mg$cache4;

	// Token: 0x040005B4 RID: 1460
	[CompilerGenerated]
	private static DeserializeStreamMethod <>f__mg$cache5;

	// Token: 0x040005B5 RID: 1461
	[CompilerGenerated]
	private static SerializeStreamMethod <>f__mg$cache6;

	// Token: 0x040005B6 RID: 1462
	[CompilerGenerated]
	private static DeserializeStreamMethod <>f__mg$cache7;
}
