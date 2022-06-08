using System;
using System.IO;

namespace ProtoBuf
{
	// Token: 0x02000641 RID: 1601
	public sealed class BufferExtension : IExtension
	{
		// Token: 0x06002E6B RID: 11883 RVA: 0x0015417D File Offset: 0x0015257D
		int IExtension.GetLength()
		{
			return (this.buffer != null) ? this.buffer.Length : 0;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x00154198 File Offset: 0x00152598
		Stream IExtension.BeginAppend()
		{
			return new MemoryStream();
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x001541A0 File Offset: 0x001525A0
		void IExtension.EndAppend(Stream stream, bool commit)
		{
			try
			{
				int num;
				if (commit && (num = (int)stream.Length) > 0)
				{
					MemoryStream memoryStream = (MemoryStream)stream;
					if (this.buffer == null)
					{
						this.buffer = memoryStream.ToArray();
					}
					else
					{
						int num2 = this.buffer.Length;
						byte[] to = new byte[num2 + num];
						Helpers.BlockCopy(this.buffer, 0, to, 0, num2);
						Helpers.BlockCopy(memoryStream.GetBuffer(), 0, to, num2, num);
						this.buffer = to;
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x00154244 File Offset: 0x00152644
		Stream IExtension.BeginQuery()
		{
			return (this.buffer != null) ? new MemoryStream(this.buffer) : Stream.Null;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x00154268 File Offset: 0x00152668
		void IExtension.EndQuery(Stream stream)
		{
			try
			{
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
		}

		// Token: 0x04002D6A RID: 11626
		private byte[] buffer;
	}
}
