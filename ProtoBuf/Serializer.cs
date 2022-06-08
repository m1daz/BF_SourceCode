using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x0200067A RID: 1658
	public static class Serializer
	{
		// Token: 0x060030E5 RID: 12517 RVA: 0x001600CA File Offset: 0x0015E4CA
		public static string GetProto<T>()
		{
			return RuntimeTypeModel.Default.GetSchema(RuntimeTypeModel.Default.MapType(typeof(T)));
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x001600EA File Offset: 0x0015E4EA
		public static T DeepClone<T>(T instance)
		{
			return (instance != null) ? ((T)((object)RuntimeTypeModel.Default.DeepClone(instance))) : instance;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x00160112 File Offset: 0x0015E512
		public static T Merge<T>(Stream source, T instance)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, instance, typeof(T)));
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x00160134 File Offset: 0x0015E534
		public static T Deserialize<T>(Stream source)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, null, typeof(T)));
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x00160151 File Offset: 0x0015E551
		public static void Serialize<T>(Stream destination, T instance)
		{
			if (instance != null)
			{
				RuntimeTypeModel.Default.Serialize(destination, instance);
			}
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x00160170 File Offset: 0x0015E570
		public static TTo ChangeType<TFrom, TTo>(TFrom instance)
		{
			TTo result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize<TFrom>(memoryStream, instance);
				memoryStream.Position = 0L;
				result = Serializer.Deserialize<TTo>(memoryStream);
			}
			return result;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x001601BC File Offset: 0x0015E5BC
		public static void PrepareSerializer<T>()
		{
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x001601BE File Offset: 0x0015E5BE
		public static IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			return RuntimeTypeModel.Default.DeserializeItems<T>(source, style, fieldNumber);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x001601CD File Offset: 0x0015E5CD
		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style)
		{
			return Serializer.DeserializeWithLengthPrefix<T>(source, style, 0);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x001601D8 File Offset: 0x0015E5D8
		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, null, @default.MapType(typeof(T)), style, fieldNumber));
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0016020C File Offset: 0x0015E60C
		public static T MergeWithLengthPrefix<T>(Stream source, T instance, PrefixStyle style)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, instance, @default.MapType(typeof(T)), style, 0));
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x00160243 File Offset: 0x0015E643
		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style)
		{
			Serializer.SerializeWithLengthPrefix<T>(destination, instance, style, 0);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x00160250 File Offset: 0x0015E650
		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(typeof(T)), style, fieldNumber);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x00160284 File Offset: 0x0015E684
		public static bool TryReadLengthPrefix(Stream source, PrefixStyle style, out int length)
		{
			int num;
			int num2;
			length = ProtoReader.ReadLengthPrefix(source, false, style, out num, out num2);
			return num2 > 0;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x001602A4 File Offset: 0x0015E6A4
		public static bool TryReadLengthPrefix(byte[] buffer, int index, int count, PrefixStyle style, out int length)
		{
			bool result;
			using (Stream stream = new MemoryStream(buffer, index, count))
			{
				result = Serializer.TryReadLengthPrefix(stream, style, out length);
			}
			return result;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x001602E8 File Offset: 0x0015E6E8
		public static void FlushPool()
		{
			BufferPool.Flush();
		}

		// Token: 0x04002E55 RID: 11861
		private const string ProtoBinaryField = "proto";

		// Token: 0x04002E56 RID: 11862
		public const int ListItemTag = 1;

		// Token: 0x0200067B RID: 1659
		public static class NonGeneric
		{
			// Token: 0x060030F5 RID: 12533 RVA: 0x001602EF File Offset: 0x0015E6EF
			public static object DeepClone(object instance)
			{
				return (instance != null) ? RuntimeTypeModel.Default.DeepClone(instance) : null;
			}

			// Token: 0x060030F6 RID: 12534 RVA: 0x00160308 File Offset: 0x0015E708
			public static void Serialize(Stream dest, object instance)
			{
				if (instance != null)
				{
					RuntimeTypeModel.Default.Serialize(dest, instance);
				}
			}

			// Token: 0x060030F7 RID: 12535 RVA: 0x0016031C File Offset: 0x0015E71C
			public static object Deserialize(Type type, Stream source)
			{
				return RuntimeTypeModel.Default.Deserialize(source, null, type);
			}

			// Token: 0x060030F8 RID: 12536 RVA: 0x0016032B File Offset: 0x0015E72B
			public static object Merge(Stream source, object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return RuntimeTypeModel.Default.Deserialize(source, instance, instance.GetType(), null);
			}

			// Token: 0x060030F9 RID: 12537 RVA: 0x00160354 File Offset: 0x0015E754
			public static void SerializeWithLengthPrefix(Stream destination, object instance, PrefixStyle style, int fieldNumber)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				RuntimeTypeModel @default = RuntimeTypeModel.Default;
				@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(instance.GetType()), style, fieldNumber);
			}

			// Token: 0x060030FA RID: 12538 RVA: 0x0016038E File Offset: 0x0015E78E
			public static bool TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, Serializer.TypeResolver resolver, out object value)
			{
				value = RuntimeTypeModel.Default.DeserializeWithLengthPrefix(source, null, null, style, 0, resolver);
				return value != null;
			}

			// Token: 0x060030FB RID: 12539 RVA: 0x001603AA File Offset: 0x0015E7AA
			public static bool CanSerialize(Type type)
			{
				return RuntimeTypeModel.Default.IsDefined(type);
			}
		}

		// Token: 0x0200067C RID: 1660
		public static class GlobalOptions
		{
			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x060030FC RID: 12540 RVA: 0x001603B7 File Offset: 0x0015E7B7
			// (set) Token: 0x060030FD RID: 12541 RVA: 0x001603C3 File Offset: 0x0015E7C3
			[Obsolete("Please use RuntimeTypeModel.Default.InferTagFromNameDefault instead (or on a per-model basis)", false)]
			public static bool InferTagFromName
			{
				get
				{
					return RuntimeTypeModel.Default.InferTagFromNameDefault;
				}
				set
				{
					RuntimeTypeModel.Default.InferTagFromNameDefault = value;
				}
			}
		}

		// Token: 0x0200067D RID: 1661
		// (Invoke) Token: 0x060030FF RID: 12543
		public delegate Type TypeResolver(int fieldNumber);
	}
}
