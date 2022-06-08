using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200047D RID: 1149
public class GGCloudServiceHistoryMessage : MonoBehaviour
{
	// Token: 0x06002140 RID: 8512 RVA: 0x000F7CCC File Offset: 0x000F60CC
	private void Awake()
	{
		GGCloudServiceHistoryMessage.mInstance = this;
		this.tempDirectory = Application.persistentDataPath + "/";
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x000F7CE9 File Offset: 0x000F60E9
	private void Start()
	{
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x000F7CEC File Offset: 0x000F60EC
	public Dictionary<CSHistoryMessageKey, List<CSMessage>> GetHistoryMessage(string userName)
	{
		object obj = this.mLock1;
		Dictionary<CSHistoryMessageKey, List<CSMessage>> result;
		lock (obj)
		{
			try
			{
				if (!Directory.Exists(this.tempDirectory))
				{
					Directory.CreateDirectory(this.tempDirectory);
				}
				string path = this.tempDirectory + userName + "HistoryMessage.hm";
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
				long length = fileStream.Length;
				byte[] array = new byte[length];
				fileStream.Seek(0L, SeekOrigin.Begin);
				fileStream.Read(array, 0, array.Length);
				fileStream.Flush();
				fileStream.Close();
				Dictionary<CSHistoryMessageKey, List<CSMessage>> dictionary = GGCloudServiceObjectSerialize.mInstance.DeserializeBinary<Dictionary<CSHistoryMessageKey, List<CSMessage>>>(array);
				result = dictionary;
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
				result = new Dictionary<CSHistoryMessageKey, List<CSMessage>>();
			}
		}
		return result;
	}

	// Token: 0x06002143 RID: 8515 RVA: 0x000F7DC8 File Offset: 0x000F61C8
	public void SaveToHistoryMessage(string userName, Dictionary<CSHistoryMessageKey, List<CSMessage>> newDic)
	{
		object obj = this.mLock2;
		lock (obj)
		{
			Dictionary<CSHistoryMessageKey, List<CSMessage>> historyMessage = this.GetHistoryMessage(userName);
			foreach (KeyValuePair<CSHistoryMessageKey, List<CSMessage>> keyValuePair in newDic)
			{
				try
				{
					if (historyMessage.ContainsKey(keyValuePair.Key))
					{
						foreach (CSMessage item in keyValuePair.Value)
						{
							if (historyMessage[keyValuePair.Key].Count > 15)
							{
								historyMessage[keyValuePair.Key].RemoveRange(0, 5);
							}
							historyMessage[keyValuePair.Key].Add(item);
						}
					}
					else
					{
						historyMessage.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				catch (Exception ex)
				{
					Debug.Log(ex.ToString());
				}
			}
			try
			{
				if (!Directory.Exists(this.tempDirectory))
				{
					Directory.CreateDirectory(this.tempDirectory);
				}
				string path = this.tempDirectory + userName + "HistoryMessage.hm";
				FileStream fileStream = new FileStream(path, FileMode.Truncate);
				byte[] array = GGCloudServiceObjectSerialize.mInstance.SerializeBinary<Dictionary<CSHistoryMessageKey, List<CSMessage>>>(historyMessage);
				fileStream.Seek(0L, SeekOrigin.Begin);
				fileStream.Write(array, 0, array.Length);
				fileStream.Flush();
				fileStream.Close();
			}
			catch (Exception ex2)
			{
				Debug.Log(ex2.ToString());
			}
		}
	}

	// Token: 0x06002144 RID: 8516 RVA: 0x000F7FEC File Offset: 0x000F63EC
	private void Update()
	{
	}

	// Token: 0x04002207 RID: 8711
	public static GGCloudServiceHistoryMessage mInstance;

	// Token: 0x04002208 RID: 8712
	public object mLock1 = new object();

	// Token: 0x04002209 RID: 8713
	public object mLock2 = new object();

	// Token: 0x0400220A RID: 8714
	private string tempDirectory = string.Empty;
}
