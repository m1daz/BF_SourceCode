using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000697 RID: 1687
	internal sealed class PropertyDecorator : ProtoDecoratorBase
	{
		// Token: 0x060031A5 RID: 12709 RVA: 0x00161DDA File Offset: 0x001601DA
		public PropertyDecorator(TypeModel model, Type forType, PropertyInfo property, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.property = property;
			PropertyDecorator.SanityCheck(model, property, tail, out this.readOptionsWriteValue, true, true);
			this.shadowSetter = PropertyDecorator.GetShadowSetter(model, property);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x00161E10 File Offset: 0x00160210
		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x00161E18 File Offset: 0x00160218
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x00161E1B File Offset: 0x0016021B
		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00161E20 File Offset: 0x00160220
		private static void SanityCheck(TypeModel model, PropertyInfo property, IProtoSerializer tail, out bool writeValue, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			writeValue = (tail.ReturnsValue && (PropertyDecorator.GetShadowSetter(model, property) != null || (property.CanWrite && Helpers.GetSetMethod(property, nonPublic, allowInternal) != null)));
			if (!property.CanRead || Helpers.GetGetMethod(property, nonPublic, allowInternal) == null)
			{
				throw new InvalidOperationException("Cannot serialize property without a get accessor");
			}
			if (!writeValue && (!tail.RequiresOldValue || Helpers.IsValueType(tail.ExpectedType)))
			{
				throw new InvalidOperationException("Cannot apply changes to property " + property.DeclaringType.FullName + "." + property.Name);
			}
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x00161EE8 File Offset: 0x001602E8
		private static MethodInfo GetShadowSetter(TypeModel model, PropertyInfo property)
		{
			Type reflectedType = property.ReflectedType;
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(reflectedType, "Set" + property.Name, new Type[]
			{
				property.PropertyType
			});
			if (instanceMethod == null || !instanceMethod.IsPublic || instanceMethod.ReturnType != model.MapType(typeof(void)))
			{
				return null;
			}
			return instanceMethod;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x00161F51 File Offset: 0x00160351
		public override void Write(object value, ProtoWriter dest)
		{
			value = this.property.GetValue(value, null);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00161F78 File Offset: 0x00160378
		public override object Read(object value, ProtoReader source)
		{
			object value2 = (!this.Tail.RequiresOldValue) ? null : this.property.GetValue(value, null);
			object obj = this.Tail.Read(value2, source);
			if (this.readOptionsWriteValue && obj != null)
			{
				if (this.shadowSetter == null)
				{
					this.property.SetValue(value, obj, null);
				}
				else
				{
					this.shadowSetter.Invoke(value, new object[]
					{
						obj
					});
				}
			}
			return null;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x00162000 File Offset: 0x00160400
		internal static bool CanWrite(TypeModel model, MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.CanWrite || PropertyDecorator.GetShadowSetter(model, propertyInfo) != null;
			}
			return member is FieldInfo;
		}

		// Token: 0x04002E8D RID: 11917
		private readonly PropertyInfo property;

		// Token: 0x04002E8E RID: 11918
		private readonly Type forType;

		// Token: 0x04002E8F RID: 11919
		private readonly bool readOptionsWriteValue;

		// Token: 0x04002E90 RID: 11920
		private readonly MethodInfo shadowSetter;
	}
}
