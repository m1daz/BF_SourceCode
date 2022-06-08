using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000692 RID: 1682
	internal class ListDecorator : ProtoDecoratorBase
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x00160DF8 File Offset: 0x0015F1F8
		protected ListDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull) : base(tail)
		{
			if (returnList)
			{
				this.options |= 8;
			}
			if (overwriteList)
			{
				this.options |= 16;
			}
			if (supportNull)
			{
				this.options |= 32;
			}
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			if (writePacked)
			{
				this.options |= 4;
			}
			this.packedWireType = packedWireType;
			if (declaredType == null)
			{
				throw new ArgumentNullException("declaredType");
			}
			if (declaredType.IsArray)
			{
				throw new ArgumentException("Cannot treat arrays as lists", "declaredType");
			}
			this.declaredType = declaredType;
			this.concreteType = concreteType;
			if (this.RequireAdd)
			{
				bool flag;
				this.add = TypeModel.ResolveListAdd(model, declaredType, tail.ExpectedType, out flag);
				if (flag)
				{
					this.options |= 1;
					string fullName = declaredType.FullName;
					if (fullName != null && fullName.StartsWith("System.Data.Linq.EntitySet`1[["))
					{
						this.options |= 2;
					}
				}
				if (this.add == null)
				{
					throw new InvalidOperationException("Unable to resolve a suitable Add method for " + declaredType.FullName);
				}
			}
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x00160F77 File Offset: 0x0015F377
		internal static bool CanPack(WireType wireType)
		{
			switch (wireType)
			{
			case WireType.Variant:
			case WireType.Fixed64:
			case WireType.Fixed32:
				break;
			default:
				if (wireType != WireType.SignedVariant)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x00160FA6 File Offset: 0x0015F3A6
		private bool IsList
		{
			get
			{
				return (this.options & 1) != 0;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x00160FB6 File Offset: 0x0015F3B6
		private bool SuppressIList
		{
			get
			{
				return (this.options & 2) != 0;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x00160FC6 File Offset: 0x0015F3C6
		private bool WritePacked
		{
			get
			{
				return (this.options & 4) != 0;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x00160FD6 File Offset: 0x0015F3D6
		private bool SupportNull
		{
			get
			{
				return (this.options & 32) != 0;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x00160FE7 File Offset: 0x0015F3E7
		private bool ReturnList
		{
			get
			{
				return (this.options & 8) != 0;
			}
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00160FF8 File Offset: 0x0015F3F8
		internal static ListDecorator Create(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull)
		{
			MethodInfo builderFactory;
			MethodInfo methodInfo;
			MethodInfo addRange;
			MethodInfo finish;
			if (returnList && ImmutableCollectionDecorator.IdentifyImmutable(model, declaredType, out builderFactory, out methodInfo, out addRange, out finish))
			{
				return new ImmutableCollectionDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull, builderFactory, methodInfo, addRange, finish);
			}
			return new ListDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull);
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x0016104F File Offset: 0x0015F44F
		protected virtual bool RequireAdd
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x00161052 File Offset: 0x0015F452
		public override Type ExpectedType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x0016105A File Offset: 0x0015F45A
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x00161062 File Offset: 0x0015F462
		public override bool ReturnsValue
		{
			get
			{
				return this.ReturnList;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x0016106A File Offset: 0x0015F46A
		protected bool AppendToCollection
		{
			get
			{
				return (this.options & 16) == 0;
			}
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00161078 File Offset: 0x0015F478
		protected MethodInfo GetEnumeratorInfo(TypeModel model, out MethodInfo moveNext, out MethodInfo current)
		{
			Type type = null;
			Type expectedType = this.ExpectedType;
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(expectedType, "GetEnumerator", null);
			Type expectedType2 = this.Tail.ExpectedType;
			Type returnType;
			Type type2;
			if (instanceMethod != null)
			{
				returnType = instanceMethod.ReturnType;
				type2 = returnType;
				moveNext = Helpers.GetInstanceMethod(type2, "MoveNext", null);
				PropertyInfo property = Helpers.GetProperty(type2, "Current", false);
				current = ((property != null) ? Helpers.GetGetMethod(property, false, false) : null);
				if (moveNext == null && model.MapType(ListDecorator.ienumeratorType).IsAssignableFrom(type2))
				{
					moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext", null);
				}
				if (moveNext != null && moveNext.ReturnType == model.MapType(typeof(bool)) && current != null && current.ReturnType == expectedType2)
				{
					return instanceMethod;
				}
				MethodInfo methodInfo;
				current = (methodInfo = null);
				moveNext = methodInfo;
			}
			Type type3 = model.MapType(typeof(IEnumerable<>), false);
			if (type3 != null)
			{
				type3 = type3.MakeGenericType(new Type[]
				{
					expectedType2
				});
				type = type3;
			}
			if (type != null && type.IsAssignableFrom(expectedType))
			{
				instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
				returnType = instanceMethod.ReturnType;
				type2 = returnType;
				moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext");
				current = Helpers.GetGetMethod(Helpers.GetProperty(type2, "Current", false), false, false);
				return instanceMethod;
			}
			type = model.MapType(ListDecorator.ienumerableType);
			instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
			returnType = instanceMethod.ReturnType;
			type2 = returnType;
			moveNext = Helpers.GetInstanceMethod(type2, "MoveNext");
			current = Helpers.GetGetMethod(Helpers.GetProperty(type2, "Current", false), false, false);
			return instanceMethod;
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x0016123C File Offset: 0x0015F63C
		public override void Write(object value, ProtoWriter dest)
		{
			bool writePacked = this.WritePacked;
			SubItemToken token;
			if (writePacked)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag = !this.SupportNull;
			IEnumerator enumerator = ((IEnumerable)value).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (flag && obj == null)
					{
						throw new NullReferenceException();
					}
					this.Tail.Write(obj, dest);
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
			if (writePacked)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x0016130C File Offset: 0x0015F70C
		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			object obj = value;
			if (value == null)
			{
				value = Activator.CreateInstance(this.concreteType);
			}
			bool flag = this.IsList && !this.SuppressIList;
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				if (flag)
				{
					IList list = (IList)value;
					while (ProtoReader.HasSubValue(this.packedWireType, source))
					{
						list.Add(this.Tail.Read(null, source));
					}
				}
				else
				{
					object[] array = new object[1];
					while (ProtoReader.HasSubValue(this.packedWireType, source))
					{
						array[0] = this.Tail.Read(null, source);
						this.add.Invoke(value, array);
					}
				}
				ProtoReader.EndSubItem(token, source);
			}
			else if (flag)
			{
				IList list2 = (IList)value;
				do
				{
					list2.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			else
			{
				object[] array2 = new object[1];
				do
				{
					array2[0] = this.Tail.Read(null, source);
					this.add.Invoke(value, array2);
				}
				while (source.TryReadFieldHeader(field));
			}
			return (obj != value) ? value : null;
		}

		// Token: 0x04002E77 RID: 11895
		private readonly byte options;

		// Token: 0x04002E78 RID: 11896
		private const byte OPTIONS_IsList = 1;

		// Token: 0x04002E79 RID: 11897
		private const byte OPTIONS_SuppressIList = 2;

		// Token: 0x04002E7A RID: 11898
		private const byte OPTIONS_WritePacked = 4;

		// Token: 0x04002E7B RID: 11899
		private const byte OPTIONS_ReturnList = 8;

		// Token: 0x04002E7C RID: 11900
		private const byte OPTIONS_OverwriteList = 16;

		// Token: 0x04002E7D RID: 11901
		private const byte OPTIONS_SupportNull = 32;

		// Token: 0x04002E7E RID: 11902
		private readonly Type declaredType;

		// Token: 0x04002E7F RID: 11903
		private readonly Type concreteType;

		// Token: 0x04002E80 RID: 11904
		private readonly MethodInfo add;

		// Token: 0x04002E81 RID: 11905
		private readonly int fieldNumber;

		// Token: 0x04002E82 RID: 11906
		protected readonly WireType packedWireType;

		// Token: 0x04002E83 RID: 11907
		private static readonly Type ienumeratorType = typeof(IEnumerator);

		// Token: 0x04002E84 RID: 11908
		private static readonly Type ienumerableType = typeof(IEnumerable);
	}
}
