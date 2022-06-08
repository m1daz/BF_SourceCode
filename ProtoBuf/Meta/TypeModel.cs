using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProtoBuf.Meta
{
	// Token: 0x02000664 RID: 1636
	public abstract class TypeModel
	{
		// Token: 0x06002FA0 RID: 12192 RVA: 0x001586A5 File Offset: 0x00156AA5
		protected internal Type MapType(Type type)
		{
			return this.MapType(type, true);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x001586AF File Offset: 0x00156AAF
		protected internal virtual Type MapType(Type type, bool demand)
		{
			return type;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x001586B4 File Offset: 0x00156AB4
		private WireType GetWireType(ProtoTypeCode code, DataFormat format, ref Type type, out int modelKey)
		{
			modelKey = -1;
			if (Helpers.IsEnum(type))
			{
				modelKey = this.GetKey(ref type);
				return WireType.Variant;
			}
			switch (code)
			{
			case ProtoTypeCode.Boolean:
			case ProtoTypeCode.Char:
			case ProtoTypeCode.SByte:
			case ProtoTypeCode.Byte:
			case ProtoTypeCode.Int16:
			case ProtoTypeCode.UInt16:
			case ProtoTypeCode.Int32:
			case ProtoTypeCode.UInt32:
				return (format != DataFormat.FixedSize) ? WireType.Variant : WireType.Fixed32;
			case ProtoTypeCode.Int64:
			case ProtoTypeCode.UInt64:
				return (format != DataFormat.FixedSize) ? WireType.Variant : WireType.Fixed64;
			case ProtoTypeCode.Single:
				return WireType.Fixed32;
			case ProtoTypeCode.Double:
				return WireType.Fixed64;
			case ProtoTypeCode.Decimal:
			case ProtoTypeCode.DateTime:
			case ProtoTypeCode.String:
				break;
			default:
				switch (code)
				{
				case ProtoTypeCode.TimeSpan:
				case ProtoTypeCode.ByteArray:
				case ProtoTypeCode.Guid:
				case ProtoTypeCode.Uri:
					break;
				default:
					if ((modelKey = this.GetKey(ref type)) >= 0)
					{
						return WireType.String;
					}
					return WireType.None;
				}
				break;
			}
			return WireType.String;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x00158780 File Offset: 0x00156B80
		internal bool TrySerializeAuxiliaryType(ProtoWriter writer, Type type, DataFormat format, int tag, object value, bool isInsideList)
		{
			if (type == null)
			{
				type = value.GetType();
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			if (num >= 0)
			{
				if (Helpers.IsEnum(type))
				{
					this.Serialize(num, value, writer);
					return true;
				}
				ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				switch (wireType + 1)
				{
				case WireType.Variant:
					throw ProtoWriter.CreateException(writer);
				case WireType.StartGroup:
				case WireType.EndGroup:
				{
					SubItemToken token = ProtoWriter.StartSubItem(value, writer);
					this.Serialize(num, value, writer);
					ProtoWriter.EndSubItem(token, writer);
					return true;
				}
				}
				this.Serialize(num, value, writer);
				return true;
			}
			else
			{
				if (wireType != WireType.None)
				{
					ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				}
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					ProtoWriter.WriteBoolean((bool)value, writer);
					return true;
				case ProtoTypeCode.Char:
					ProtoWriter.WriteUInt16((ushort)((char)value), writer);
					return true;
				case ProtoTypeCode.SByte:
					ProtoWriter.WriteSByte((sbyte)value, writer);
					return true;
				case ProtoTypeCode.Byte:
					ProtoWriter.WriteByte((byte)value, writer);
					return true;
				case ProtoTypeCode.Int16:
					ProtoWriter.WriteInt16((short)value, writer);
					return true;
				case ProtoTypeCode.UInt16:
					ProtoWriter.WriteUInt16((ushort)value, writer);
					return true;
				case ProtoTypeCode.Int32:
					ProtoWriter.WriteInt32((int)value, writer);
					return true;
				case ProtoTypeCode.UInt32:
					ProtoWriter.WriteUInt32((uint)value, writer);
					return true;
				case ProtoTypeCode.Int64:
					ProtoWriter.WriteInt64((long)value, writer);
					return true;
				case ProtoTypeCode.UInt64:
					ProtoWriter.WriteUInt64((ulong)value, writer);
					return true;
				case ProtoTypeCode.Single:
					ProtoWriter.WriteSingle((float)value, writer);
					return true;
				case ProtoTypeCode.Double:
					ProtoWriter.WriteDouble((double)value, writer);
					return true;
				case ProtoTypeCode.Decimal:
					BclHelpers.WriteDecimal((decimal)value, writer);
					return true;
				case ProtoTypeCode.DateTime:
					BclHelpers.WriteDateTime((DateTime)value, writer);
					return true;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						BclHelpers.WriteTimeSpan((TimeSpan)value, writer);
						return true;
					case ProtoTypeCode.ByteArray:
						ProtoWriter.WriteBytes((byte[])value, writer);
						return true;
					case ProtoTypeCode.Guid:
						BclHelpers.WriteGuid((Guid)value, writer);
						return true;
					case ProtoTypeCode.Uri:
						ProtoWriter.WriteString(((Uri)value).AbsoluteUri, writer);
						return true;
					default:
					{
						IEnumerable enumerable = value as IEnumerable;
						if (enumerable == null)
						{
							return false;
						}
						if (isInsideList)
						{
							throw TypeModel.CreateNestedListsNotSupported();
						}
						IEnumerator enumerator = enumerable.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								if (obj == null)
								{
									throw new NullReferenceException();
								}
								if (!this.TrySerializeAuxiliaryType(writer, null, format, tag, obj, true))
								{
									TypeModel.ThrowUnexpectedType(obj.GetType());
								}
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
						return true;
					}
					}
					break;
				case ProtoTypeCode.String:
					ProtoWriter.WriteString((string)value, writer);
					return true;
				}
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x00158A5C File Offset: 0x00156E5C
		private void SerializeCore(ProtoWriter writer, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				this.Serialize(key, value, writer);
			}
			else if (!this.TrySerializeAuxiliaryType(writer, type, DataFormat.Default, 1, value, false))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x00158AB6 File Offset: 0x00156EB6
		public void Serialize(Stream dest, object value)
		{
			this.Serialize(dest, value, null);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00158AC4 File Offset: 0x00156EC4
		public void Serialize(Stream dest, object value, SerializationContext context)
		{
			using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
			{
				protoWriter.SetRootObject(value);
				this.SerializeCore(protoWriter, value);
				protoWriter.Close();
			}
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00158B14 File Offset: 0x00156F14
		public void Serialize(ProtoWriter dest, object value)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest.CheckDepthFlushlock();
			dest.SetRootObject(value);
			this.SerializeCore(dest, value);
			dest.CheckDepthFlushlock();
			ProtoWriter.Flush(dest);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x00158B48 File Offset: 0x00156F48
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			int num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, fieldNumber, null, out num);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00158B68 File Offset: 0x00156F68
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			int num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out num);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x00158B88 File Offset: 0x00156F88
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead)
		{
			bool flag;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out bytesRead, out flag, null);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00158BAC File Offset: 0x00156FAC
		private object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead, out bool haveObject, SerializationContext context)
		{
			haveObject = false;
			bytesRead = 0;
			if (type == null && (style != PrefixStyle.Base128 || resolver == null))
			{
				throw new InvalidOperationException("A type must be provided unless base-128 prefixing is being used in combination with a resolver");
			}
			for (;;)
			{
				bool flag = expectedField > 0 || resolver != null;
				int num2;
				int num3;
				int num = ProtoReader.ReadLengthPrefix(source, flag, style, out num2, out num3);
				if (num3 == 0)
				{
					break;
				}
				bytesRead += num3;
				if (num < 0)
				{
					return value;
				}
				bool flag2;
				if (style != PrefixStyle.Base128)
				{
					flag2 = false;
				}
				else if (flag && expectedField == 0 && type == null && resolver != null)
				{
					type = resolver(num2);
					flag2 = (type == null);
				}
				else
				{
					flag2 = (expectedField != num2);
				}
				if (flag2)
				{
					if (num == 2147483647)
					{
						goto Block_12;
					}
					ProtoReader.Seek(source, num, null);
					bytesRead += num;
				}
				if (!flag2)
				{
					goto Block_13;
				}
			}
			return value;
			Block_12:
			throw new InvalidOperationException();
			Block_13:
			ProtoReader protoReader = null;
			object result;
			try
			{
				int num;
				protoReader = ProtoReader.Create(source, this, context, num);
				int key = this.GetKey(ref type);
				if (key >= 0 && !Helpers.IsEnum(type))
				{
					value = this.Deserialize(key, value, protoReader);
				}
				else if (!this.TryDeserializeAuxiliaryType(protoReader, DataFormat.Default, 1, type, ref value, true, false, true, false) && num != 0)
				{
					TypeModel.ThrowUnexpectedType(type);
				}
				bytesRead += protoReader.Position;
				haveObject = true;
				result = value;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00158D30 File Offset: 0x00157130
		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			return this.DeserializeItems(source, type, style, expectedField, resolver, null);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00158D40 File Offset: 0x00157140
		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator(this, source, type, style, expectedField, resolver, context);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00158D51 File Offset: 0x00157151
		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField)
		{
			return this.DeserializeItems<T>(source, style, expectedField, null);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00158D5D File Offset: 0x0015715D
		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator<T>(this, source, style, expectedField, context);
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00158D6A File Offset: 0x0015716A
		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			this.SerializeWithLengthPrefix(dest, value, type, style, fieldNumber, null);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x00158D7C File Offset: 0x0015717C
		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber, SerializationContext context)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				type = this.MapType(value.GetType());
			}
			int key = this.GetKey(ref type);
			using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
			{
				switch (style)
				{
				case PrefixStyle.None:
					this.Serialize(key, value, protoWriter);
					break;
				case PrefixStyle.Base128:
				case PrefixStyle.Fixed32:
				case PrefixStyle.Fixed32BigEndian:
					ProtoWriter.WriteObject(value, key, protoWriter, style, fieldNumber);
					break;
				default:
					throw new ArgumentOutOfRangeException("style");
				}
				protoWriter.Close();
			}
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x00158E30 File Offset: 0x00157230
		public object Deserialize(Stream source, object value, Type type)
		{
			return this.Deserialize(source, value, type, null);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x00158E3C File Offset: 0x0015723C
		public object Deserialize(Stream source, object value, Type type, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, -1);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x00158EA0 File Offset: 0x001572A0
		private bool PrepareDeserialize(object value, ref Type type)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("type");
				}
				type = this.MapType(value.GetType());
			}
			bool result = true;
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
				result = false;
			}
			return result;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x00158EE9 File Offset: 0x001572E9
		public object Deserialize(Stream source, object value, Type type, int length)
		{
			return this.Deserialize(source, value, type, length, null);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x00158EF8 File Offset: 0x001572F8
		public object Deserialize(Stream source, object value, Type type, int length, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, length);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00158F5C File Offset: 0x0015735C
		public object Deserialize(ProtoReader source, object value, Type type)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			if (value != null)
			{
				source.SetRootObject(value);
			}
			object result = this.DeserializeCore(source, type, value, noAutoCreate);
			source.CheckFullyConsumed();
			return result;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00158FA4 File Offset: 0x001573A4
		private object DeserializeCore(ProtoReader reader, Type type, object value, bool noAutoCreate)
		{
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				return this.Deserialize(key, value, reader);
			}
			this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, noAutoCreate, false);
			return value;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x00158FEC File Offset: 0x001573EC
		internal static MethodInfo ResolveListAdd(TypeModel model, Type listType, Type itemType, out bool isList)
		{
			isList = model.MapType(TypeModel.ilist).IsAssignableFrom(listType);
			Type[] array = new Type[]
			{
				itemType
			};
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			if (instanceMethod == null)
			{
				bool flag = listType.IsInterface && listType == model.MapType(typeof(IEnumerable<>)).MakeGenericType(array);
				Type type = model.MapType(typeof(ICollection<>)).MakeGenericType(array);
				if (flag || type.IsAssignableFrom(listType))
				{
					instanceMethod = Helpers.GetInstanceMethod(type, "Add", array);
				}
			}
			if (instanceMethod == null)
			{
				foreach (Type type2 in listType.GetInterfaces())
				{
					if (type2.Name == "IProducerConsumerCollection`1" && type2.IsGenericType && type2.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.IProducerConsumerCollection`1")
					{
						instanceMethod = Helpers.GetInstanceMethod(type2, "TryAdd", array);
						if (instanceMethod != null)
						{
							break;
						}
					}
				}
			}
			if (instanceMethod == null)
			{
				array[0] = model.MapType(typeof(object));
				instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			}
			if (instanceMethod == null && isList)
			{
				instanceMethod = Helpers.GetInstanceMethod(model.MapType(TypeModel.ilist), "Add", array);
			}
			return instanceMethod;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0015915C File Offset: 0x0015755C
		internal static Type GetListItemType(TypeModel model, Type listType)
		{
			if (listType == model.MapType(typeof(string)) || listType.IsArray || !model.MapType(typeof(IEnumerable)).IsAssignableFrom(listType))
			{
				return null;
			}
			BasicList basicList = new BasicList();
			foreach (MethodInfo methodInfo in listType.GetMethods())
			{
				if (!methodInfo.IsStatic && !(methodInfo.Name != "Add"))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					Type parameterType;
					if (parameters.Length == 1 && !basicList.Contains(parameterType = parameters[0].ParameterType))
					{
						basicList.Add(parameterType);
					}
				}
			}
			string name = listType.Name;
			if (name == null || (name.IndexOf("Queue") < 0 && name.IndexOf("Stack") < 0))
			{
				TypeModel.TestEnumerableListPatterns(model, basicList, listType);
				foreach (Type iType in listType.GetInterfaces())
				{
					TypeModel.TestEnumerableListPatterns(model, basicList, iType);
				}
			}
			foreach (PropertyInfo propertyInfo in listType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(propertyInfo.Name != "Item") && !basicList.Contains(propertyInfo.PropertyType))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 1 && indexParameters[0].ParameterType == model.MapType(typeof(int)))
					{
						basicList.Add(propertyInfo.PropertyType);
					}
				}
			}
			int count = basicList.Count;
			if (count == 0)
			{
				return null;
			}
			if (count != 1)
			{
				if (count == 2)
				{
					if (TypeModel.CheckDictionaryAccessors(model, (Type)basicList[0], (Type)basicList[1]))
					{
						return (Type)basicList[0];
					}
					if (TypeModel.CheckDictionaryAccessors(model, (Type)basicList[1], (Type)basicList[0]))
					{
						return (Type)basicList[1];
					}
				}
				return null;
			}
			return (Type)basicList[0];
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x001593D4 File Offset: 0x001577D4
		private static void TestEnumerableListPatterns(TypeModel model, BasicList candidates, Type iType)
		{
			if (iType.IsGenericType)
			{
				Type genericTypeDefinition = iType.GetGenericTypeDefinition();
				if (genericTypeDefinition == model.MapType(typeof(IEnumerable<>)) || genericTypeDefinition == model.MapType(typeof(ICollection<>)) || genericTypeDefinition.FullName == "System.Collections.Concurrent.IProducerConsumerCollection`1")
				{
					Type[] genericArguments = iType.GetGenericArguments();
					if (!candidates.Contains(genericArguments[0]))
					{
						candidates.Add(genericArguments[0]);
					}
				}
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x00159453 File Offset: 0x00157853
		private static bool CheckDictionaryAccessors(TypeModel model, Type pair, Type value)
		{
			return pair.IsGenericType && pair.GetGenericTypeDefinition() == model.MapType(typeof(KeyValuePair<, >)) && pair.GetGenericArguments()[1] == value;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0015948C File Offset: 0x0015788C
		private bool TryDeserializeList(TypeModel model, ProtoReader reader, DataFormat format, int tag, Type listType, Type itemType, ref object value)
		{
			bool flag;
			MethodInfo methodInfo = TypeModel.ResolveListAdd(model, listType, itemType, out flag);
			if (methodInfo == null)
			{
				throw new NotSupportedException("Unknown list variant: " + listType.FullName);
			}
			bool result = false;
			object obj = null;
			IList list = value as IList;
			object[] array = (!flag) ? new object[1] : null;
			BasicList basicList = (!listType.IsArray) ? null : new BasicList();
			while (this.TryDeserializeAuxiliaryType(reader, format, tag, itemType, ref obj, true, true, true, true))
			{
				result = true;
				if (value == null && basicList == null)
				{
					value = TypeModel.CreateListInstance(listType, itemType);
					list = (value as IList);
				}
				if (list != null)
				{
					list.Add(obj);
				}
				else if (basicList != null)
				{
					basicList.Add(obj);
				}
				else
				{
					array[0] = obj;
					methodInfo.Invoke(value, array);
				}
				obj = null;
			}
			if (basicList != null)
			{
				if (value != null)
				{
					if (basicList.Count != 0)
					{
						Array array2 = (Array)value;
						Array array3 = Array.CreateInstance(itemType, array2.Length + basicList.Count);
						Array.Copy(array2, array3, array2.Length);
						basicList.CopyTo(array3, array2.Length);
						value = array3;
					}
				}
				else
				{
					Array array3 = Array.CreateInstance(itemType, basicList.Count);
					basicList.CopyTo(array3, 0);
					value = array3;
				}
			}
			return result;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0015960C File Offset: 0x00157A0C
		private static object CreateListInstance(Type listType, Type itemType)
		{
			Type type = listType;
			if (listType.IsArray)
			{
				return Array.CreateInstance(itemType, 0);
			}
			if (!listType.IsClass || listType.IsAbstract || Helpers.GetConstructor(listType, Helpers.EmptyTypes, true) == null)
			{
				bool flag = false;
				string fullName;
				if (listType.IsInterface && (fullName = listType.FullName) != null && fullName.IndexOf("Dictionary") >= 0)
				{
					if (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(IDictionary<, >))
					{
						Type[] genericArguments = listType.GetGenericArguments();
						type = typeof(Dictionary<, >).MakeGenericType(genericArguments);
						flag = true;
					}
					if (!flag && listType == typeof(IDictionary))
					{
						type = typeof(Hashtable);
						flag = true;
					}
				}
				if (!flag)
				{
					type = typeof(List<>).MakeGenericType(new Type[]
					{
						itemType
					});
					flag = true;
				}
				if (!flag)
				{
					type = typeof(ArrayList);
				}
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x00159718 File Offset: 0x00157B18
		internal bool TryDeserializeAuxiliaryType(ProtoReader reader, DataFormat format, int tag, Type type, ref object value, bool skipOtherFields, bool asListItem, bool autoCreate, bool insideList)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			bool flag = false;
			if (wireType == WireType.None)
			{
				Type type2 = TypeModel.GetListItemType(this, type);
				if (type2 == null && type.IsArray && type.GetArrayRank() == 1 && type != typeof(byte[]))
				{
					type2 = type.GetElementType();
				}
				if (type2 != null)
				{
					if (insideList)
					{
						throw TypeModel.CreateNestedListsNotSupported();
					}
					flag = this.TryDeserializeList(this, reader, format, tag, type, type2, ref value);
					if (!flag && autoCreate)
					{
						value = TypeModel.CreateListInstance(type, type2);
					}
					return flag;
				}
				else
				{
					TypeModel.ThrowUnexpectedType(type);
				}
			}
			while (!flag || !asListItem)
			{
				int num2 = reader.ReadFieldHeader();
				if (num2 <= 0)
				{
					IL_35E:
					if (!flag && !asListItem && autoCreate && type != typeof(string))
					{
						value = Activator.CreateInstance(type);
					}
					return flag;
				}
				if (num2 != tag)
				{
					if (!skipOtherFields)
					{
						throw ProtoReader.AddErrorData(new InvalidOperationException("Expected field " + tag.ToString() + ", but found " + num2.ToString()), reader);
					}
					reader.SkipField();
				}
				else
				{
					flag = true;
					reader.Hint(wireType);
					if (num >= 0)
					{
						if (wireType != WireType.String && wireType != WireType.StartGroup)
						{
							value = this.Deserialize(num, value, reader);
						}
						else
						{
							SubItemToken token = ProtoReader.StartSubItem(reader);
							value = this.Deserialize(num, value, reader);
							ProtoReader.EndSubItem(token, reader);
						}
					}
					else
					{
						switch (typeCode)
						{
						case ProtoTypeCode.Boolean:
							value = reader.ReadBoolean();
							break;
						case ProtoTypeCode.Char:
							value = (char)reader.ReadUInt16();
							break;
						case ProtoTypeCode.SByte:
							value = reader.ReadSByte();
							break;
						case ProtoTypeCode.Byte:
							value = reader.ReadByte();
							break;
						case ProtoTypeCode.Int16:
							value = reader.ReadInt16();
							break;
						case ProtoTypeCode.UInt16:
							value = reader.ReadUInt16();
							break;
						case ProtoTypeCode.Int32:
							value = reader.ReadInt32();
							break;
						case ProtoTypeCode.UInt32:
							value = reader.ReadUInt32();
							break;
						case ProtoTypeCode.Int64:
							value = reader.ReadInt64();
							break;
						case ProtoTypeCode.UInt64:
							value = reader.ReadUInt64();
							break;
						case ProtoTypeCode.Single:
							value = reader.ReadSingle();
							break;
						case ProtoTypeCode.Double:
							value = reader.ReadDouble();
							break;
						case ProtoTypeCode.Decimal:
							value = BclHelpers.ReadDecimal(reader);
							break;
						case ProtoTypeCode.DateTime:
							value = BclHelpers.ReadDateTime(reader);
							break;
						default:
							switch (typeCode)
							{
							case ProtoTypeCode.TimeSpan:
								value = BclHelpers.ReadTimeSpan(reader);
								break;
							case ProtoTypeCode.ByteArray:
								value = ProtoReader.AppendBytes((byte[])value, reader);
								break;
							case ProtoTypeCode.Guid:
								value = BclHelpers.ReadGuid(reader);
								break;
							case ProtoTypeCode.Uri:
								value = new Uri(reader.ReadString());
								break;
							}
							break;
						case ProtoTypeCode.String:
							value = reader.ReadString();
							break;
						}
					}
				}
			}
			goto IL_35E;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00159AB5 File Offset: 0x00157EB5
		public static RuntimeTypeModel Create()
		{
			return new RuntimeTypeModel(false);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00159AC0 File Offset: 0x00157EC0
		protected internal static Type ResolveProxies(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (type.IsGenericParameter)
			{
				return null;
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				return underlyingType;
			}
			string fullName = type.FullName;
			if (fullName != null && fullName.StartsWith("System.Data.Entity.DynamicProxies."))
			{
				return type.BaseType;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				string fullName2 = interfaces[i].FullName;
				if (fullName2 != null)
				{
					if (fullName2 == "NHibernate.Proxy.INHibernateProxy" || fullName2 == "NHibernate.Proxy.DynamicProxy.IProxy" || fullName2 == "NHibernate.Intercept.IFieldInterceptorAccessor")
					{
						return type.BaseType;
					}
				}
			}
			return null;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00159B81 File Offset: 0x00157F81
		public bool IsDefined(Type type)
		{
			return this.GetKey(ref type) >= 0;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00159B94 File Offset: 0x00157F94
		protected internal int GetKey(ref Type type)
		{
			if (type == null)
			{
				return -1;
			}
			int keyImpl = this.GetKeyImpl(type);
			if (keyImpl < 0)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null)
				{
					type = type2;
					keyImpl = this.GetKeyImpl(type);
				}
			}
			return keyImpl;
		}

		// Token: 0x06002FC4 RID: 12228
		protected abstract int GetKeyImpl(Type type);

		// Token: 0x06002FC5 RID: 12229
		protected internal abstract void Serialize(int key, object value, ProtoWriter dest);

		// Token: 0x06002FC6 RID: 12230
		protected internal abstract object Deserialize(int key, object value, ProtoReader source);

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00159BD8 File Offset: 0x00157FD8
		public object DeepClone(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ProtoWriter protoWriter = new ProtoWriter(memoryStream, this, null))
					{
						protoWriter.SetRootObject(value);
						this.Serialize(key, value, protoWriter);
						protoWriter.Close();
					}
					memoryStream.Position = 0L;
					ProtoReader protoReader = null;
					try
					{
						protoReader = ProtoReader.Create(memoryStream, this, null, -1);
						return this.Deserialize(key, null, protoReader);
					}
					finally
					{
						ProtoReader.Recycle(protoReader);
					}
				}
			}
			if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				byte[] array2 = new byte[array.Length];
				Helpers.BlockCopy(array, 0, array2, 0, array.Length);
				return array2;
			}
			int num;
			if (this.GetWireType(Helpers.GetTypeCode(type), DataFormat.Default, ref type, out num) != WireType.None && num < 0)
			{
				return value;
			}
			object result;
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (ProtoWriter protoWriter2 = new ProtoWriter(memoryStream2, this, null))
				{
					if (!this.TrySerializeAuxiliaryType(protoWriter2, type, DataFormat.Default, 1, value, false))
					{
						TypeModel.ThrowUnexpectedType(type);
					}
					protoWriter2.Close();
				}
				memoryStream2.Position = 0L;
				ProtoReader reader = null;
				try
				{
					reader = ProtoReader.Create(memoryStream2, this, null, -1);
					value = null;
					this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false);
					result = value;
				}
				finally
				{
					ProtoReader.Recycle(reader);
				}
			}
			return result;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00159DB8 File Offset: 0x001581B8
		protected internal static void ThrowUnexpectedSubtype(Type expected, Type actual)
		{
			if (expected != TypeModel.ResolveProxies(actual))
			{
				throw new InvalidOperationException("Unexpected sub-type: " + actual.FullName);
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00159DDC File Offset: 0x001581DC
		protected internal static void ThrowUnexpectedType(Type type)
		{
			string str = (type != null) ? type.FullName : "(unknown)";
			if (type != null)
			{
				Type baseType = type.BaseType;
				if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition().Name == "GeneratedMessage`2")
				{
					throw new InvalidOperationException("Are you mixing protobuf-net and protobuf-csharp-port? See http://stackoverflow.com/q/11564914; type: " + str);
				}
			}
			throw new InvalidOperationException("Type is not expected, and no contract can be inferred: " + str);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x00159E59 File Offset: 0x00158259
		internal static Exception CreateNestedListsNotSupported()
		{
			return new NotSupportedException("Nested or jagged lists and arrays are not supported");
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00159E65 File Offset: 0x00158265
		public static void ThrowCannotCreateInstance(Type type)
		{
			throw new ProtoException("No parameterless constructor found for " + ((type != null) ? type.Name : "(null)"));
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00159E8C File Offset: 0x0015828C
		internal static string SerializeType(TypeModel model, Type type)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(type);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (!Helpers.IsNullOrEmpty(typeFormatEventArgs.FormattedName))
					{
						return typeFormatEventArgs.FormattedName;
					}
				}
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00159ED8 File Offset: 0x001582D8
		internal static Type DeserializeType(TypeModel model, string value)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(value);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (typeFormatEventArgs.Type != null)
					{
						return typeFormatEventArgs.Type;
					}
				}
			}
			return Type.GetType(value);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00159F1F File Offset: 0x0015831F
		public bool CanSerializeContractType(Type type)
		{
			return this.CanSerialize(type, false, true, true);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x00159F2B File Offset: 0x0015832B
		public bool CanSerialize(Type type)
		{
			return this.CanSerialize(type, true, true, true);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00159F37 File Offset: 0x00158337
		public bool CanSerializeBasicType(Type type)
		{
			return this.CanSerialize(type, true, false, true);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00159F44 File Offset: 0x00158344
		private bool CanSerialize(Type type, bool allowBasic, bool allowContract, bool allowLists)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			if (typeCode != ProtoTypeCode.Empty && typeCode != ProtoTypeCode.Unknown)
			{
				return allowBasic;
			}
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				return allowContract;
			}
			if (allowLists)
			{
				Type type2 = null;
				if (type.IsArray)
				{
					if (type.GetArrayRank() == 1)
					{
						type2 = type.GetElementType();
					}
				}
				else
				{
					type2 = TypeModel.GetListItemType(this, type);
				}
				if (type2 != null)
				{
					return this.CanSerialize(type2, allowBasic, allowContract, false);
				}
			}
			return false;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00159FEA File Offset: 0x001583EA
		public virtual string GetSchema(Type type)
		{
			throw new NotSupportedException();
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06002FD3 RID: 12243 RVA: 0x00159FF4 File Offset: 0x001583F4
		// (remove) Token: 0x06002FD4 RID: 12244 RVA: 0x0015A02C File Offset: 0x0015842C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event TypeFormatEventHandler DynamicTypeFormatting;

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0015A062 File Offset: 0x00158462
		internal virtual Type GetType(string fullName, Assembly context)
		{
			return TypeModel.ResolveKnownType(fullName, this, context);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0015A06C File Offset: 0x0015846C
		[MethodImpl(8)]
		internal static Type ResolveKnownType(string name, TypeModel model, Assembly assembly)
		{
			if (Helpers.IsNullOrEmpty(name))
			{
				return null;
			}
			try
			{
				Type type = Type.GetType(name);
				if (type != null)
				{
					return type;
				}
			}
			catch
			{
			}
			try
			{
				int num = name.IndexOf(',');
				string name2 = ((num <= 0) ? name : name.Substring(0, num)).Trim();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				Type type2 = (assembly != null) ? assembly.GetType(name2) : null;
				if (type2 != null)
				{
					return type2;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x04002DD7 RID: 11735
		private static readonly Type ilist = typeof(IList);

		// Token: 0x02000665 RID: 1637
		private sealed class DeserializeItemsIterator<T> : TypeModel.DeserializeItemsIterator, IEnumerator<T>, IEnumerable<T>, IEnumerator, IDisposable, IEnumerable
		{
			// Token: 0x06002FD8 RID: 12248 RVA: 0x0015A1F7 File Offset: 0x001585F7
			public DeserializeItemsIterator(TypeModel model, Stream source, PrefixStyle style, int expectedField, SerializationContext context) : base(model, source, model.MapType(typeof(T)), style, expectedField, null, context)
			{
			}

			// Token: 0x06002FD9 RID: 12249 RVA: 0x0015A217 File Offset: 0x00158617
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this;
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06002FDA RID: 12250 RVA: 0x0015A21A File Offset: 0x0015861A
			public new T Current
			{
				get
				{
					return (T)((object)base.Current);
				}
			}

			// Token: 0x06002FDB RID: 12251 RVA: 0x0015A227 File Offset: 0x00158627
			void IDisposable.Dispose()
			{
			}
		}

		// Token: 0x02000666 RID: 1638
		private class DeserializeItemsIterator : IEnumerator, IEnumerable
		{
			// Token: 0x06002FDC RID: 12252 RVA: 0x0015A138 File Offset: 0x00158538
			public DeserializeItemsIterator(TypeModel model, Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
			{
				this.haveObject = true;
				this.source = source;
				this.type = type;
				this.style = style;
				this.expectedField = expectedField;
				this.resolver = resolver;
				this.model = model;
				this.context = context;
			}

			// Token: 0x06002FDD RID: 12253 RVA: 0x0015A187 File Offset: 0x00158587
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			// Token: 0x06002FDE RID: 12254 RVA: 0x0015A18C File Offset: 0x0015858C
			public bool MoveNext()
			{
				if (this.haveObject)
				{
					int num;
					this.current = this.model.DeserializeWithLengthPrefix(this.source, null, this.type, this.style, this.expectedField, this.resolver, out num, out this.haveObject, this.context);
				}
				return this.haveObject;
			}

			// Token: 0x06002FDF RID: 12255 RVA: 0x0015A1E8 File Offset: 0x001585E8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x0015A1EF File Offset: 0x001585EF
			public object Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x04002DD9 RID: 11737
			private bool haveObject;

			// Token: 0x04002DDA RID: 11738
			private object current;

			// Token: 0x04002DDB RID: 11739
			private readonly Stream source;

			// Token: 0x04002DDC RID: 11740
			private readonly Type type;

			// Token: 0x04002DDD RID: 11741
			private readonly PrefixStyle style;

			// Token: 0x04002DDE RID: 11742
			private readonly int expectedField;

			// Token: 0x04002DDF RID: 11743
			private readonly Serializer.TypeResolver resolver;

			// Token: 0x04002DE0 RID: 11744
			private readonly TypeModel model;

			// Token: 0x04002DE1 RID: 11745
			private readonly SerializationContext context;
		}

		// Token: 0x02000667 RID: 1639
		protected internal enum CallbackType
		{
			// Token: 0x04002DE3 RID: 11747
			BeforeSerialize,
			// Token: 0x04002DE4 RID: 11748
			AfterSerialize,
			// Token: 0x04002DE5 RID: 11749
			BeforeDeserialize,
			// Token: 0x04002DE6 RID: 11750
			AfterDeserialize
		}
	}
}
