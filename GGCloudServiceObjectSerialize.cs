using System;
using System.IO;
using ProtoBuf;
using UnityEngine;

// Token: 0x02000481 RID: 1153
public class GGCloudServiceObjectSerialize : MonoBehaviour
{
	// Token: 0x060021B0 RID: 8624 RVA: 0x000FA1B9 File Offset: 0x000F85B9
	private void Awake()
	{
		GGCloudServiceObjectSerialize.mInstance = this;
	}

	// Token: 0x060021B1 RID: 8625 RVA: 0x000FA1C1 File Offset: 0x000F85C1
	private void Start()
	{
	}

	// Token: 0x060021B2 RID: 8626 RVA: 0x000FA1C4 File Offset: 0x000F85C4
	public byte[] SerializeBinary<T>(object obj)
	{
		byte[] array = null;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			Serializer.Serialize<T>(memoryStream, (T)((object)obj));
			array = new byte[memoryStream.Position];
			byte[] buffer = memoryStream.GetBuffer();
			Array.Copy(buffer, array, array.Length);
		}
		return array;
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x000FA228 File Offset: 0x000F8628
	public T DeserializeBinary<T>(byte[] serializationBytes)
	{
		T result = default(T);
		using (MemoryStream memoryStream = new MemoryStream(serializationBytes))
		{
			result = Serializer.Deserialize<T>(memoryStream);
		}
		return result;
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x000FA270 File Offset: 0x000F8670
	private void Update()
	{
	}

	// Token: 0x0400223B RID: 8763
	public static GGCloudServiceObjectSerialize mInstance;
}
