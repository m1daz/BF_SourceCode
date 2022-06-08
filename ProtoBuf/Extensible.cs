using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000648 RID: 1608
	public abstract class Extensible : IExtensible
	{
		// Token: 0x06002E7B RID: 11899 RVA: 0x0015440D File Offset: 0x0015280D
		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return this.GetExtensionObject(createIfMissing);
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x00154416 File Offset: 0x00152816
		protected virtual IExtension GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x00154424 File Offset: 0x00152824
		public static IExtension GetExtensionObject(ref IExtension extensionObject, bool createIfMissing)
		{
			if (createIfMissing && extensionObject == null)
			{
				extensionObject = new BufferExtension();
			}
			return extensionObject;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0015443C File Offset: 0x0015283C
		public static void AppendValue<TValue>(IExtensible instance, int tag, TValue value)
		{
			Extensible.AppendValue<TValue>(instance, tag, DataFormat.Default, value);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x00154447 File Offset: 0x00152847
		public static void AppendValue<TValue>(IExtensible instance, int tag, DataFormat format, TValue value)
		{
			ExtensibleUtil.AppendExtendValue(RuntimeTypeModel.Default, instance, tag, format, value);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0015445C File Offset: 0x0015285C
		public static TValue GetValue<TValue>(IExtensible instance, int tag)
		{
			return Extensible.GetValue<TValue>(instance, tag, DataFormat.Default);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x00154468 File Offset: 0x00152868
		public static TValue GetValue<TValue>(IExtensible instance, int tag, DataFormat format)
		{
			TValue result;
			Extensible.TryGetValue<TValue>(instance, tag, format, out result);
			return result;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00154481 File Offset: 0x00152881
		public static bool TryGetValue<TValue>(IExtensible instance, int tag, out TValue value)
		{
			return Extensible.TryGetValue<TValue>(instance, tag, DataFormat.Default, out value);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x0015448C File Offset: 0x0015288C
		public static bool TryGetValue<TValue>(IExtensible instance, int tag, DataFormat format, out TValue value)
		{
			return Extensible.TryGetValue<TValue>(instance, tag, format, false, out value);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00154498 File Offset: 0x00152898
		public static bool TryGetValue<TValue>(IExtensible instance, int tag, DataFormat format, bool allowDefinedTag, out TValue value)
		{
			value = default(TValue);
			bool result = false;
			foreach (TValue tvalue in ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, format, true, allowDefinedTag))
			{
				value = tvalue;
				result = true;
			}
			return result;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0015450C File Offset: 0x0015290C
		public static IEnumerable<TValue> GetValues<TValue>(IExtensible instance, int tag)
		{
			return ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, DataFormat.Default, false, false);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x00154518 File Offset: 0x00152918
		public static IEnumerable<TValue> GetValues<TValue>(IExtensible instance, int tag, DataFormat format)
		{
			return ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, format, false, false);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x00154524 File Offset: 0x00152924
		public static bool TryGetValue(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format, bool allowDefinedTag, out object value)
		{
			value = null;
			bool result = false;
			IEnumerator enumerator = ExtensibleUtil.GetExtendedValues(model, type, instance, tag, format, true, allowDefinedTag).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					value = obj;
					result = true;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x00154594 File Offset: 0x00152994
		public static IEnumerable GetValues(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format)
		{
			return ExtensibleUtil.GetExtendedValues(model, type, instance, tag, format, false, false);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x001545A3 File Offset: 0x001529A3
		public static void AppendValue(TypeModel model, IExtensible instance, int tag, DataFormat format, object value)
		{
			ExtensibleUtil.AppendExtendValue(model, instance, tag, format, value);
		}

		// Token: 0x04002D74 RID: 11636
		private IExtension extensionObject;
	}
}
