using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000053 RID: 83
	public class VectorConverter : JsonConverter
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00014182 File Offset: 0x00012582
		public override bool CanConvert(Type type)
		{
			return type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Vector4);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000141B4 File Offset: 0x000125B4
		public override object ReadJson(Type type, Dictionary<string, object> values)
		{
			if (type == typeof(Vector2))
			{
				return new Vector2(base.CastFloat(values["x"]), base.CastFloat(values["y"]));
			}
			if (type == typeof(Vector3))
			{
				return new Vector3(base.CastFloat(values["x"]), base.CastFloat(values["y"]), base.CastFloat(values["z"]));
			}
			if (type == typeof(Vector4))
			{
				return new Vector4(base.CastFloat(values["x"]), base.CastFloat(values["y"]), base.CastFloat(values["z"]), base.CastFloat(values["w"]));
			}
			throw new NotImplementedException("Can only read Vector2,3,4. Not objects of type " + type);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000142BC File Offset: 0x000126BC
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			if (type == typeof(Vector2))
			{
				Vector2 vector = (Vector2)value;
				return new Dictionary<string, object>
				{
					{
						"x",
						vector.x
					},
					{
						"y",
						vector.y
					}
				};
			}
			if (type == typeof(Vector3))
			{
				Vector3 vector2 = (Vector3)value;
				return new Dictionary<string, object>
				{
					{
						"x",
						vector2.x
					},
					{
						"y",
						vector2.y
					},
					{
						"z",
						vector2.z
					}
				};
			}
			if (type == typeof(Vector4))
			{
				Vector4 vector3 = (Vector4)value;
				return new Dictionary<string, object>
				{
					{
						"x",
						vector3.x
					},
					{
						"y",
						vector3.y
					},
					{
						"z",
						vector3.z
					},
					{
						"w",
						vector3.w
					}
				};
			}
			throw new NotImplementedException("Can only write Vector2,3,4. Not objects of type " + type);
		}
	}
}
