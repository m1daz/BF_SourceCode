using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;

namespace Pathfinding.Serialization
{
	// Token: 0x0200004F RID: 79
	public class GuidConverter : JsonConverter
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00013E19 File Offset: 0x00012219
		public override bool CanConvert(Type type)
		{
			return type == typeof(Pathfinding.Util.Guid);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00013E28 File Offset: 0x00012228
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			string str = (string)values["value"];
			return new Pathfinding.Util.Guid(str);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00013E54 File Offset: 0x00012254
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Pathfinding.Util.Guid guid = (Pathfinding.Util.Guid)value;
			return new Dictionary<string, object>
			{
				{
					"value",
					guid.ToString()
				}
			};
		}
	}
}
