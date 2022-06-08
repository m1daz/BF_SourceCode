using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	// Token: 0x0200064F RID: 1615
	internal abstract class AttributeMap
	{
		// Token: 0x06002EB3 RID: 11955
		public abstract bool TryGet(string key, bool publicOnly, out object value);

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00154F46 File Offset: 0x00153346
		public bool TryGet(string key, out object value)
		{
			return this.TryGet(key, true, out value);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06002EB5 RID: 11957
		public abstract Type AttributeType { get; }

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00154F54 File Offset: 0x00153354
		public static AttributeMap[] Create(TypeModel model, Type type, bool inherit)
		{
			object[] customAttributes = type.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00154F98 File Offset: 0x00153398
		public static AttributeMap[] Create(TypeModel model, MemberInfo member, bool inherit)
		{
			object[] customAttributes = member.GetCustomAttributes(inherit);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00154FDC File Offset: 0x001533DC
		public static AttributeMap[] Create(TypeModel model, Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(false);
			AttributeMap[] array = new AttributeMap[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = new AttributeMap.ReflectionAttributeMap((Attribute)customAttributes[i]);
			}
			return array;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06002EB9 RID: 11961
		public abstract object Target { get; }

		// Token: 0x02000650 RID: 1616
		private sealed class ReflectionAttributeMap : AttributeMap
		{
			// Token: 0x06002EBA RID: 11962 RVA: 0x0015501F File Offset: 0x0015341F
			public ReflectionAttributeMap(Attribute attribute)
			{
				this.attribute = attribute;
			}

			// Token: 0x1700037E RID: 894
			// (get) Token: 0x06002EBB RID: 11963 RVA: 0x0015502E File Offset: 0x0015342E
			public override object Target
			{
				get
				{
					return this.attribute;
				}
			}

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x06002EBC RID: 11964 RVA: 0x00155036 File Offset: 0x00153436
			public override Type AttributeType
			{
				get
				{
					return this.attribute.GetType();
				}
			}

			// Token: 0x06002EBD RID: 11965 RVA: 0x00155044 File Offset: 0x00153444
			public override bool TryGet(string key, bool publicOnly, out object value)
			{
				MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(this.attribute.GetType(), publicOnly);
				MemberInfo[] array = instanceFieldsAndProperties;
				int i = 0;
				while (i < array.Length)
				{
					MemberInfo memberInfo = array[i];
					if (string.Equals(memberInfo.Name, key, StringComparison.OrdinalIgnoreCase))
					{
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo != null)
						{
							value = propertyInfo.GetValue(this.attribute, null);
							return true;
						}
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							value = fieldInfo.GetValue(this.attribute);
							return true;
						}
						throw new NotSupportedException(memberInfo.GetType().Name);
					}
					else
					{
						i++;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x04002D91 RID: 11665
			private readonly Attribute attribute;
		}
	}
}
