using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ProtoBuf
{
	// Token: 0x0200064A RID: 1610
	internal sealed class Helpers
	{
		// Token: 0x06002E8D RID: 11917 RVA: 0x00154BC6 File Offset: 0x00152FC6
		private Helpers()
		{
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x00154BCE File Offset: 0x00152FCE
		public static StringBuilder AppendLine(StringBuilder builder)
		{
			return builder.AppendLine();
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x00154BD6 File Offset: 0x00152FD6
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.Length == 0;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x00154BEA File Offset: 0x00152FEA
		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message, object obj)
		{
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00154BEC File Offset: 0x00152FEC
		[Conditional("DEBUG")]
		public static void DebugWriteLine(string message)
		{
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x00154BEE File Offset: 0x00152FEE
		[Conditional("TRACE")]
		public static void TraceWriteLine(string message)
		{
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x00154BF0 File Offset: 0x00152FF0
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x00154BF2 File Offset: 0x00152FF2
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition, string message, params object[] args)
		{
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x00154BF4 File Offset: 0x00152FF4
		[Conditional("DEBUG")]
		public static void DebugAssert(bool condition)
		{
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x00154BF8 File Offset: 0x00152FF8
		public static void Sort(int[] keys, object[] values)
		{
			bool flag;
			do
			{
				flag = false;
				for (int i = 1; i < keys.Length; i++)
				{
					if (keys[i - 1] > keys[i])
					{
						int num = keys[i];
						keys[i] = keys[i - 1];
						keys[i - 1] = num;
						object obj = values[i];
						values[i] = values[i - 1];
						values[i - 1] = obj;
						flag = true;
					}
				}
			}
			while (flag);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x00154C54 File Offset: 0x00153054
		public static void BlockCopy(byte[] from, int fromIndex, byte[] to, int toIndex, int count)
		{
			Buffer.BlockCopy(from, fromIndex, to, toIndex, count);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x00154C61 File Offset: 0x00153061
		public static bool IsInfinity(float value)
		{
			return float.IsInfinity(value);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x00154C69 File Offset: 0x00153069
		internal static MethodInfo GetInstanceMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x00154C74 File Offset: 0x00153074
		internal static MethodInfo GetStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x00154C7F File Offset: 0x0015307F
		internal static MethodInfo GetInstanceMethod(Type declaringType, string name, Type[] types)
		{
			if (types == null)
			{
				types = Helpers.EmptyTypes;
			}
			return declaringType.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x00154C9A File Offset: 0x0015309A
		internal static bool IsSubclassOf(Type type, Type baseClass)
		{
			return type.IsSubclassOf(baseClass);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x00154CA3 File Offset: 0x001530A3
		public static bool IsInfinity(double value)
		{
			return double.IsInfinity(value);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x00154CAC File Offset: 0x001530AC
		public static ProtoTypeCode GetTypeCode(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return (ProtoTypeCode)typeCode;
			}
			if (type == typeof(TimeSpan))
			{
				return ProtoTypeCode.TimeSpan;
			}
			if (type == typeof(Guid))
			{
				return ProtoTypeCode.Guid;
			}
			if (type == typeof(Uri))
			{
				return ProtoTypeCode.Uri;
			}
			if (type == typeof(byte[]))
			{
				return ProtoTypeCode.ByteArray;
			}
			if (type == typeof(Type))
			{
				return ProtoTypeCode.Type;
			}
			return ProtoTypeCode.Unknown;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x00154D79 File Offset: 0x00153179
		internal static Type GetUnderlyingType(Type type)
		{
			return Nullable.GetUnderlyingType(type);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00154D81 File Offset: 0x00153181
		internal static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00154D89 File Offset: 0x00153189
		internal static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x00154D94 File Offset: 0x00153194
		internal static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetGetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x00154DEC File Offset: 0x001531EC
		internal static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic, bool allowInternal)
		{
			if (property == null)
			{
				return null;
			}
			MethodInfo methodInfo = property.GetSetMethod(nonPublic);
			if (methodInfo == null && !nonPublic && allowInternal)
			{
				methodInfo = property.GetGetMethod(true);
				if (methodInfo == null && !methodInfo.IsAssembly && !methodInfo.IsFamilyOrAssembly)
				{
					methodInfo = null;
				}
			}
			return methodInfo;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x00154E42 File Offset: 0x00153242
		internal static ConstructorInfo GetConstructor(Type type, Type[] parameterTypes, bool nonPublic)
		{
			return type.GetConstructor((!nonPublic) ? (BindingFlags.Instance | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), null, parameterTypes, null);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00154E5C File Offset: 0x0015325C
		internal static ConstructorInfo[] GetConstructors(Type type, bool nonPublic)
		{
			return type.GetConstructors((!nonPublic) ? (BindingFlags.Instance | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00154E73 File Offset: 0x00153273
		internal static PropertyInfo GetProperty(Type type, string name, bool nonPublic)
		{
			return type.GetProperty(name, (!nonPublic) ? (BindingFlags.Instance | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00154E8B File Offset: 0x0015328B
		internal static object ParseEnum(Type type, string value)
		{
			return Enum.Parse(type, value, true);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00154E98 File Offset: 0x00153298
		internal static MemberInfo[] GetInstanceFieldsAndProperties(Type type, bool publicOnly)
		{
			BindingFlags bindingAttr = (!publicOnly) ? (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : (BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] properties = type.GetProperties(bindingAttr);
			FieldInfo[] fields = type.GetFields(bindingAttr);
			MemberInfo[] array = new MemberInfo[fields.Length + properties.Length];
			properties.CopyTo(array, 0);
			fields.CopyTo(array, properties.Length);
			return array;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00154EE8 File Offset: 0x001532E8
		internal static Type GetMemberType(MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				return ((FieldInfo)member).FieldType;
			}
			if (memberType != MemberTypes.Property)
			{
				return null;
			}
			return ((PropertyInfo)member).PropertyType;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x00154F29 File Offset: 0x00153329
		internal static bool IsAssignableFrom(Type target, Type type)
		{
			return target.IsAssignableFrom(type);
		}

		// Token: 0x04002D75 RID: 11637
		public static readonly Type[] EmptyTypes = Type.EmptyTypes;
	}
}
