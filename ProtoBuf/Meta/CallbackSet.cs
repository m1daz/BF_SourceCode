using System;
using System.Reflection;

namespace ProtoBuf.Meta
{
	// Token: 0x02000657 RID: 1623
	public class CallbackSet
	{
		// Token: 0x06002EE6 RID: 12006 RVA: 0x001555C6 File Offset: 0x001539C6
		internal CallbackSet(MetaType metaType)
		{
			if (metaType == null)
			{
				throw new ArgumentNullException("metaType");
			}
			this.metaType = metaType;
		}

		// Token: 0x17000386 RID: 902
		internal MethodInfo this[TypeModel.CallbackType callbackType]
		{
			get
			{
				switch (callbackType)
				{
				case TypeModel.CallbackType.BeforeSerialize:
					return this.beforeSerialize;
				case TypeModel.CallbackType.AfterSerialize:
					return this.afterSerialize;
				case TypeModel.CallbackType.BeforeDeserialize:
					return this.beforeDeserialize;
				case TypeModel.CallbackType.AfterDeserialize:
					return this.afterDeserialize;
				default:
					throw new ArgumentException("Callback type not supported: " + callbackType.ToString(), "callbackType");
				}
			}
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x00155650 File Offset: 0x00153A50
		internal static bool CheckCallbackParameters(TypeModel model, MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				if (parameterType != model.MapType(typeof(SerializationContext)))
				{
					if (parameterType != model.MapType(typeof(Type)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x001556BC File Offset: 0x00153ABC
		private MethodInfo SanityCheckCallback(TypeModel model, MethodInfo callback)
		{
			this.metaType.ThrowIfFrozen();
			if (callback == null)
			{
				return callback;
			}
			if (callback.IsStatic)
			{
				throw new ArgumentException("Callbacks cannot be static", "callback");
			}
			if (callback.ReturnType != model.MapType(typeof(void)) || !CallbackSet.CheckCallbackParameters(model, callback))
			{
				throw CallbackSet.CreateInvalidCallbackSignature(callback);
			}
			return callback;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x00155726 File Offset: 0x00153B26
		internal static Exception CreateInvalidCallbackSignature(MethodInfo method)
		{
			return new NotSupportedException("Invalid callback signature in " + method.DeclaringType.FullName + "." + method.Name);
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x0015574D File Offset: 0x00153B4D
		// (set) Token: 0x06002EEC RID: 12012 RVA: 0x00155755 File Offset: 0x00153B55
		public MethodInfo BeforeSerialize
		{
			get
			{
				return this.beforeSerialize;
			}
			set
			{
				this.beforeSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x0015576F File Offset: 0x00153B6F
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x00155777 File Offset: 0x00153B77
		public MethodInfo BeforeDeserialize
		{
			get
			{
				return this.beforeDeserialize;
			}
			set
			{
				this.beforeDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x00155791 File Offset: 0x00153B91
		// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x00155799 File Offset: 0x00153B99
		public MethodInfo AfterSerialize
		{
			get
			{
				return this.afterSerialize;
			}
			set
			{
				this.afterSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x001557B3 File Offset: 0x00153BB3
		// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x001557BB File Offset: 0x00153BBB
		public MethodInfo AfterDeserialize
		{
			get
			{
				return this.afterDeserialize;
			}
			set
			{
				this.afterDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x001557D5 File Offset: 0x00153BD5
		public bool NonTrivial
		{
			get
			{
				return this.beforeSerialize != null || this.beforeDeserialize != null || this.afterSerialize != null || this.afterDeserialize != null;
			}
		}

		// Token: 0x04002D9A RID: 11674
		private readonly MetaType metaType;

		// Token: 0x04002D9B RID: 11675
		private MethodInfo beforeSerialize;

		// Token: 0x04002D9C RID: 11676
		private MethodInfo afterSerialize;

		// Token: 0x04002D9D RID: 11677
		private MethodInfo beforeDeserialize;

		// Token: 0x04002D9E RID: 11678
		private MethodInfo afterDeserialize;
	}
}
