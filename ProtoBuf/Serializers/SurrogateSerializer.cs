using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200069D RID: 1693
	internal sealed class SurrogateSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x060031D3 RID: 12755 RVA: 0x0016220C File Offset: 0x0016060C
		public SurrogateSerializer(TypeModel model, Type forType, Type declaredType, IProtoTypeSerializer rootTail)
		{
			this.forType = forType;
			this.declaredType = declaredType;
			this.rootTail = rootTail;
			this.toTail = this.GetConversion(model, true);
			this.fromTail = this.GetConversion(model, false);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00162246 File Offset: 0x00160646
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00162249 File Offset: 0x00160649
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x0016224C File Offset: 0x0016064C
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00162253 File Offset: 0x00160653
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x00162255 File Offset: 0x00160655
		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x00162258 File Offset: 0x00160658
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x0016225B File Offset: 0x0016065B
		public Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x00162264 File Offset: 0x00160664
		private static bool HasCast(TypeModel model, Type type, Type from, Type to, out MethodInfo op)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			Type type2 = null;
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.ReturnType == to)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == from)
					{
						if (type2 == null)
						{
							type2 = model.MapType(typeof(ProtoConverterAttribute), false);
							if (type2 == null)
							{
								break;
							}
						}
						if (methodInfo.IsDefined(type2, true))
						{
							op = methodInfo;
							return true;
						}
					}
				}
			}
			foreach (MethodInfo methodInfo2 in methods)
			{
				if ((!(methodInfo2.Name != "op_Implicit") || !(methodInfo2.Name != "op_Explicit")) && methodInfo2.ReturnType == to)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == from)
					{
						op = methodInfo2;
						return true;
					}
				}
			}
			op = null;
			return false;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00162388 File Offset: 0x00160788
		public MethodInfo GetConversion(TypeModel model, bool toTail)
		{
			Type to = (!toTail) ? this.forType : this.declaredType;
			Type from = (!toTail) ? this.declaredType : this.forType;
			MethodInfo result;
			if (SurrogateSerializer.HasCast(model, this.declaredType, from, to, out result) || SurrogateSerializer.HasCast(model, this.forType, from, to, out result))
			{
				return result;
			}
			throw new InvalidOperationException("No suitable conversion operator found for surrogate: " + this.forType.FullName + " / " + this.declaredType.FullName);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x0016241B File Offset: 0x0016081B
		public void Write(object value, ProtoWriter writer)
		{
			this.rootTail.Write(this.toTail.Invoke(null, new object[]
			{
				value
			}), writer);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x00162440 File Offset: 0x00160840
		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[]
			{
				value
			};
			value = this.toTail.Invoke(null, array);
			array[0] = this.rootTail.Read(value, source);
			return this.fromTail.Invoke(null, array);
		}

		// Token: 0x04002E99 RID: 11929
		private readonly Type forType;

		// Token: 0x04002E9A RID: 11930
		private readonly Type declaredType;

		// Token: 0x04002E9B RID: 11931
		private readonly MethodInfo toTail;

		// Token: 0x04002E9C RID: 11932
		private readonly MethodInfo fromTail;

		// Token: 0x04002E9D RID: 11933
		private IProtoTypeSerializer rootTail;
	}
}
