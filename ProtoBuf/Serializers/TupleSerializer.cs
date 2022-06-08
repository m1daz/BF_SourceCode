using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A1 RID: 1697
	internal sealed class TupleSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x060031F8 RID: 12792 RVA: 0x00162658 File Offset: 0x00160A58
		public TupleSerializer(RuntimeTypeModel model, ConstructorInfo ctor, MemberInfo[] members)
		{
			if (ctor == null)
			{
				throw new ArgumentNullException("ctor");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			this.ctor = ctor;
			this.members = members;
			this.tails = new IProtoSerializer[members.Length];
			ParameterInfo[] parameters = ctor.GetParameters();
			for (int i = 0; i < members.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				Type type = null;
				Type concreteType = null;
				MetaType.ResolveListTypes(model, parameterType, ref type, ref concreteType);
				Type type2 = (type != null) ? type : parameterType;
				bool asReference = false;
				int num = model.FindOrAddAuto(type2, false, true, false);
				if (num >= 0)
				{
					asReference = model[type2].AsReferenceDefault;
				}
				WireType wireType;
				IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(model, DataFormat.Default, type2, out wireType, asReference, false, false, true);
				if (protoSerializer == null)
				{
					throw new InvalidOperationException("No serializer defined for type: " + type2.FullName);
				}
				protoSerializer = new TagDecorator(i + 1, wireType, false, protoSerializer);
				IProtoSerializer protoSerializer2;
				if (type == null)
				{
					protoSerializer2 = protoSerializer;
				}
				else if (parameterType.IsArray)
				{
					protoSerializer2 = new ArrayDecorator(model, protoSerializer, i + 1, false, wireType, parameterType, false, false);
				}
				else
				{
					protoSerializer2 = ListDecorator.Create(model, parameterType, concreteType, protoSerializer, i + 1, false, wireType, true, false, false);
				}
				this.tails[i] = protoSerializer2;
			}
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x001627A5 File Offset: 0x00160BA5
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x001627A8 File Offset: 0x00160BA8
		public Type ExpectedType
		{
			get
			{
				return this.ctor.DeclaringType;
			}
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x001627B5 File Offset: 0x00160BB5
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x001627B7 File Offset: 0x00160BB7
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x001627C0 File Offset: 0x00160BC0
		private object GetValue(object obj, int index)
		{
			PropertyInfo propertyInfo;
			if ((propertyInfo = (this.members[index] as PropertyInfo)) != null)
			{
				if (obj == null)
				{
					return (!Helpers.IsValueType(propertyInfo.PropertyType)) ? null : Activator.CreateInstance(propertyInfo.PropertyType);
				}
				return propertyInfo.GetValue(obj, null);
			}
			else
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = (this.members[index] as FieldInfo)) == null)
				{
					throw new InvalidOperationException();
				}
				if (obj == null)
				{
					return (!Helpers.IsValueType(fieldInfo.FieldType)) ? null : Activator.CreateInstance(fieldInfo.FieldType);
				}
				return fieldInfo.GetValue(obj);
			}
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x0016285C File Offset: 0x00160C5C
		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[this.members.Length];
			bool flag = false;
			if (value == null)
			{
				flag = true;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetValue(value, i);
			}
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				flag = true;
				if (num <= this.tails.Length)
				{
					IProtoSerializer protoSerializer = this.tails[num - 1];
					array[num - 1] = this.tails[num - 1].Read((!protoSerializer.RequiresOldValue) ? null : array[num - 1], source);
				}
				else
				{
					source.SkipField();
				}
			}
			return (!flag) ? value : this.ctor.Invoke(array);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x00162920 File Offset: 0x00160D20
		public void Write(object value, ProtoWriter dest)
		{
			for (int i = 0; i < this.tails.Length; i++)
			{
				object value2 = this.GetValue(value, i);
				if (value2 != null)
				{
					this.tails[i].Write(value2, dest);
				}
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x00162964 File Offset: 0x00160D64
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x00162967 File Offset: 0x00160D67
		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x0016296C File Offset: 0x00160D6C
		private Type GetMemberType(int index)
		{
			Type memberType = Helpers.GetMemberType(this.members[index]);
			if (memberType == null)
			{
				throw new InvalidOperationException();
			}
			return memberType;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00162994 File Offset: 0x00160D94
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		// Token: 0x04002EA3 RID: 11939
		private readonly MemberInfo[] members;

		// Token: 0x04002EA4 RID: 11940
		private readonly ConstructorInfo ctor;

		// Token: 0x04002EA5 RID: 11941
		private IProtoSerializer[] tails;
	}
}
