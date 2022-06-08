using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A2 RID: 1698
	internal sealed class TypeSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06003204 RID: 12804 RVA: 0x00162998 File Offset: 0x00160D98
		public TypeSerializer(TypeModel model, Type forType, int[] fieldNumbers, IProtoSerializer[] serializers, MethodInfo[] baseCtorCallbacks, bool isRootType, bool useConstructor, CallbackSet callbacks, Type constructType, MethodInfo factory)
		{
			Helpers.Sort(fieldNumbers, serializers);
			bool flag = false;
			for (int i = 1; i < fieldNumbers.Length; i++)
			{
				if (fieldNumbers[i] == fieldNumbers[i - 1])
				{
					throw new InvalidOperationException("Duplicate field-number detected; " + fieldNumbers[i].ToString() + " on: " + forType.FullName);
				}
				if (!flag && serializers[i].ExpectedType != forType)
				{
					flag = true;
				}
			}
			this.forType = forType;
			this.factory = factory;
			if (constructType == null)
			{
				constructType = forType;
			}
			else if (!forType.IsAssignableFrom(constructType))
			{
				throw new InvalidOperationException(forType.FullName + " cannot be assigned from " + constructType.FullName);
			}
			this.constructType = constructType;
			this.serializers = serializers;
			this.fieldNumbers = fieldNumbers;
			this.callbacks = callbacks;
			this.isRootType = isRootType;
			this.useConstructor = useConstructor;
			if (baseCtorCallbacks != null && baseCtorCallbacks.Length == 0)
			{
				baseCtorCallbacks = null;
			}
			this.baseCtorCallbacks = baseCtorCallbacks;
			if (Helpers.GetUnderlyingType(forType) != null)
			{
				throw new ArgumentException("Cannot create a TypeSerializer for nullable types", "forType");
			}
			if (model.MapType(TypeSerializer.iextensible).IsAssignableFrom(forType))
			{
				if (forType.IsValueType || !isRootType || flag)
				{
					throw new NotSupportedException("IExtensible is not supported in structs or classes with inheritance");
				}
				this.isExtensible = true;
			}
			this.hasConstructor = (!constructType.IsAbstract && Helpers.GetConstructor(constructType, Helpers.EmptyTypes, true) != null);
			if (constructType != forType && useConstructor && !this.hasConstructor)
			{
				throw new ArgumentException("The supplied default implementation cannot be created: " + constructType.FullName, "constructType");
			}
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00162B68 File Offset: 0x00160F68
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			if (this.callbacks != null && this.callbacks[callbackType] != null)
			{
				return true;
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				if (this.serializers[i].ExpectedType != this.forType && ((IProtoTypeSerializer)this.serializers[i]).HasCallbacks(callbackType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x00162BDF File Offset: 0x00160FDF
		public Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x00162BE7 File Offset: 0x00160FE7
		private bool CanHaveInheritance
		{
			get
			{
				return (this.forType.IsClass || this.forType.IsInterface) && !this.forType.IsSealed;
			}
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x00162C1A File Offset: 0x0016101A
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return true;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00162C1D File Offset: 0x0016101D
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return this.CreateInstance(source, false);
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00162C28 File Offset: 0x00161028
		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			if (this.callbacks != null)
			{
				this.InvokeCallback(this.callbacks[callbackType], value, context);
			}
			IProtoTypeSerializer protoTypeSerializer = (IProtoTypeSerializer)this.GetMoreSpecificSerializer(value);
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00162C74 File Offset: 0x00161074
		private IProtoSerializer GetMoreSpecificSerializer(object value)
		{
			if (!this.CanHaveInheritance)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == this.forType)
			{
				return null;
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				IProtoSerializer protoSerializer = this.serializers[i];
				if (protoSerializer.ExpectedType != this.forType && Helpers.IsAssignableFrom(protoSerializer.ExpectedType, type))
				{
					return protoSerializer;
				}
			}
			if (type == this.constructType)
			{
				return null;
			}
			TypeModel.ThrowUnexpectedSubtype(this.forType, type);
			return null;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00162D04 File Offset: 0x00161104
		public void Write(object value, ProtoWriter dest)
		{
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.BeforeSerialize, dest.Context);
			}
			IProtoSerializer moreSpecificSerializer = this.GetMoreSpecificSerializer(value);
			if (moreSpecificSerializer != null)
			{
				moreSpecificSerializer.Write(value, dest);
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				IProtoSerializer protoSerializer = this.serializers[i];
				if (protoSerializer.ExpectedType == this.forType)
				{
					protoSerializer.Write(value, dest);
				}
			}
			if (this.isExtensible)
			{
				ProtoWriter.AppendExtensionData((IExtensible)value, dest);
			}
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.AfterSerialize, dest.Context);
			}
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00162DAC File Offset: 0x001611AC
		public object Read(object value, ProtoReader source)
		{
			if (this.isRootType && value != null)
			{
				this.Callback(value, TypeModel.CallbackType.BeforeDeserialize, source.Context);
			}
			int num = 0;
			int num2 = 0;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				bool flag = false;
				if (num3 < num)
				{
					num2 = (num = 0);
				}
				for (int i = num2; i < this.fieldNumbers.Length; i++)
				{
					if (this.fieldNumbers[i] == num3)
					{
						IProtoSerializer protoSerializer = this.serializers[i];
						Type expectedType = protoSerializer.ExpectedType;
						if (value == null)
						{
							if (expectedType == this.forType)
							{
								value = this.CreateInstance(source, true);
							}
						}
						else if (expectedType != this.forType && ((IProtoTypeSerializer)protoSerializer).CanCreateInstance() && expectedType.IsSubclassOf(value.GetType()))
						{
							value = ProtoReader.Merge(source, value, ((IProtoTypeSerializer)protoSerializer).CreateInstance(source));
						}
						if (protoSerializer.ReturnsValue)
						{
							value = protoSerializer.Read(value, source);
						}
						else
						{
							protoSerializer.Read(value, source);
						}
						num2 = i;
						num = num3;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (value == null)
					{
						value = this.CreateInstance(source, true);
					}
					if (this.isExtensible)
					{
						source.AppendExtensionData((IExtensible)value);
					}
					else
					{
						source.SkipField();
					}
				}
			}
			if (value == null)
			{
				value = this.CreateInstance(source, true);
			}
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.AfterDeserialize, source.Context);
			}
			return value;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x00162F38 File Offset: 0x00161338
		private object InvokeCallback(MethodInfo method, object obj, SerializationContext context)
		{
			object result = null;
			if (method != null)
			{
				ParameterInfo[] parameters = method.GetParameters();
				int num = parameters.Length;
				object[] array;
				bool flag;
				if (num != 0)
				{
					array = new object[parameters.Length];
					flag = true;
					for (int i = 0; i < array.Length; i++)
					{
						Type parameterType = parameters[i].ParameterType;
						object obj2;
						if (parameterType == typeof(SerializationContext))
						{
							obj2 = context;
						}
						else if (parameterType == typeof(Type))
						{
							obj2 = this.constructType;
						}
						else
						{
							obj2 = null;
							flag = false;
						}
						array[i] = obj2;
					}
				}
				else
				{
					array = null;
					flag = true;
				}
				if (!flag)
				{
					throw CallbackSet.CreateInvalidCallbackSignature(method);
				}
				result = method.Invoke(obj, array);
			}
			return result;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x00163000 File Offset: 0x00161400
		private object CreateInstance(ProtoReader source, bool includeLocalCallback)
		{
			object obj;
			if (this.factory != null)
			{
				obj = this.InvokeCallback(this.factory, null, source.Context);
			}
			else if (this.useConstructor)
			{
				if (!this.hasConstructor)
				{
					TypeModel.ThrowCannotCreateInstance(this.constructType);
				}
				obj = Activator.CreateInstance(this.constructType, true);
			}
			else
			{
				obj = BclHelpers.GetUninitializedObject(this.constructType);
			}
			ProtoReader.NoteObject(obj, source);
			if (this.baseCtorCallbacks != null)
			{
				for (int i = 0; i < this.baseCtorCallbacks.Length; i++)
				{
					this.InvokeCallback(this.baseCtorCallbacks[i], obj, source.Context);
				}
			}
			if (includeLocalCallback && this.callbacks != null)
			{
				this.InvokeCallback(this.callbacks.BeforeDeserialize, obj, source.Context);
			}
			return obj;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x001630DC File Offset: 0x001614DC
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x001630DF File Offset: 0x001614DF
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002EA6 RID: 11942
		private readonly Type forType;

		// Token: 0x04002EA7 RID: 11943
		private readonly Type constructType;

		// Token: 0x04002EA8 RID: 11944
		private readonly IProtoSerializer[] serializers;

		// Token: 0x04002EA9 RID: 11945
		private readonly int[] fieldNumbers;

		// Token: 0x04002EAA RID: 11946
		private readonly bool isRootType;

		// Token: 0x04002EAB RID: 11947
		private readonly bool useConstructor;

		// Token: 0x04002EAC RID: 11948
		private readonly bool isExtensible;

		// Token: 0x04002EAD RID: 11949
		private readonly bool hasConstructor;

		// Token: 0x04002EAE RID: 11950
		private readonly CallbackSet callbacks;

		// Token: 0x04002EAF RID: 11951
		private readonly MethodInfo[] baseCtorCallbacks;

		// Token: 0x04002EB0 RID: 11952
		private readonly MethodInfo factory;

		// Token: 0x04002EB1 RID: 11953
		private static readonly Type iextensible = typeof(IExtensible);
	}
}
