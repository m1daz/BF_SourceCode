using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000052 RID: 82
	public class LayerMaskConverter : JsonConverter
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0001411D File Offset: 0x0001251D
		public override bool CanConvert(Type type)
		{
			return type == typeof(LayerMask);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001412C File Offset: 0x0001252C
		public override object ReadJson(Type type, Dictionary<string, object> values)
		{
			return (int)values["value"];
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00014148 File Offset: 0x00012548
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			return new Dictionary<string, object>
			{
				{
					"value",
					((LayerMask)value).value
				}
			};
		}
	}
}
