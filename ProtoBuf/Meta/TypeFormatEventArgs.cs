using System;

namespace ProtoBuf.Meta
{
	// Token: 0x02000662 RID: 1634
	public class TypeFormatEventArgs : EventArgs
	{
		// Token: 0x06002F95 RID: 12181 RVA: 0x0015B747 File Offset: 0x00159B47
		internal TypeFormatEventArgs(string formattedName)
		{
			if (Helpers.IsNullOrEmpty(formattedName))
			{
				throw new ArgumentNullException("formattedName");
			}
			this.formattedName = formattedName;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x0015B76C File Offset: 0x00159B6C
		internal TypeFormatEventArgs(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
			this.typeFixed = true;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x0015B793 File Offset: 0x00159B93
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x0015B79B File Offset: 0x00159B9B
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type != value)
				{
					if (this.typeFixed)
					{
						throw new InvalidOperationException("The type is fixed and cannot be changed");
					}
					this.type = value;
				}
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x0015B7C6 File Offset: 0x00159BC6
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x0015B7CE File Offset: 0x00159BCE
		public string FormattedName
		{
			get
			{
				return this.formattedName;
			}
			set
			{
				if (this.formattedName != value)
				{
					if (!this.typeFixed)
					{
						throw new InvalidOperationException("The formatted-name is fixed and cannot be changed");
					}
					this.formattedName = value;
				}
			}
		}

		// Token: 0x04002DD4 RID: 11732
		private Type type;

		// Token: 0x04002DD5 RID: 11733
		private string formattedName;

		// Token: 0x04002DD6 RID: 11734
		private readonly bool typeFixed;
	}
}
