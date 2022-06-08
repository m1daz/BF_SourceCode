using System;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000696 RID: 1686
	internal sealed class ParseableSerializer : IProtoSerializer
	{
		// Token: 0x0600319D RID: 12701 RVA: 0x00161CDF File Offset: 0x001600DF
		private ParseableSerializer(MethodInfo parse)
		{
			this.parse = parse;
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x00161CF0 File Offset: 0x001600F0
		public static ParseableSerializer TryCreate(Type type, TypeModel model)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MethodInfo method = type.GetMethod("Parse", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				model.MapType(typeof(string))
			}, null);
			if (method != null && method.ReturnType == type)
			{
				if (Helpers.IsValueType(type))
				{
					MethodInfo customToString = ParseableSerializer.GetCustomToString(type);
					if (customToString == null || customToString.ReturnType != model.MapType(typeof(string)))
					{
						return null;
					}
				}
				return new ParseableSerializer(method);
			}
			return null;
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x00161D86 File Offset: 0x00160186
		private static MethodInfo GetCustomToString(Type type)
		{
			return type.GetMethod("ToString", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public, null, Helpers.EmptyTypes, null);
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x00161D9C File Offset: 0x0016019C
		public Type ExpectedType
		{
			get
			{
				return this.parse.DeclaringType;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x00161DA9 File Offset: 0x001601A9
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x00161DAC File Offset: 0x001601AC
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00161DAF File Offset: 0x001601AF
		public object Read(object value, ProtoReader source)
		{
			return this.parse.Invoke(null, new object[]
			{
				source.ReadString()
			});
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x00161DCC File Offset: 0x001601CC
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString(value.ToString(), dest);
		}

		// Token: 0x04002E8C RID: 11916
		private readonly MethodInfo parse;
	}
}
