using System;
using System.IO;
using ProtoBuf;
using UnityEngine;

// Token: 0x0200052A RID: 1322
public class GGNetworkObjectSerialize : MonoBehaviour
{
	// Token: 0x06002584 RID: 9604 RVA: 0x001165BD File Offset: 0x001149BD
	private void Awake()
	{
		GGNetworkObjectSerialize.mInstance = this;
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x001165C5 File Offset: 0x001149C5
	private void Start()
	{
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x001165C8 File Offset: 0x001149C8
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

	// Token: 0x06002587 RID: 9607 RVA: 0x0011662C File Offset: 0x00114A2C
	public T DeserializeBinary<T>(byte[] serializationBytes)
	{
		T result = default(T);
		using (MemoryStream memoryStream = new MemoryStream(serializationBytes))
		{
			result = Serializer.Deserialize<T>(memoryStream);
		}
		return result;
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x00116674 File Offset: 0x00114A74
	private void Update()
	{
	}

	// Token: 0x04002622 RID: 9762
	public static GGNetworkObjectSerialize mInstance;
}
