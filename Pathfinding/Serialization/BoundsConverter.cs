using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000051 RID: 81
	public class BoundsConverter : JsonConverter
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00013F91 File Offset: 0x00012391
		public override bool CanConvert(Type type)
		{
			return type == typeof(Bounds);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00013FA0 File Offset: 0x000123A0
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			return new Bounds
			{
				center = new Vector3(base.CastFloat(values["cx"]), base.CastFloat(values["cy"]), base.CastFloat(values["cz"])),
				extents = new Vector3(base.CastFloat(values["ex"]), base.CastFloat(values["ey"]), base.CastFloat(values["ez"]))
			};
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001403C File Offset: 0x0001243C
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Bounds bounds = (Bounds)value;
			return new Dictionary<string, object>
			{
				{
					"cx",
					bounds.center.x
				},
				{
					"cy",
					bounds.center.y
				},
				{
					"cz",
					bounds.center.z
				},
				{
					"ex",
					bounds.extents.x
				},
				{
					"ey",
					bounds.extents.y
				},
				{
					"ez",
					bounds.extents.z
				}
			};
		}
	}
}
