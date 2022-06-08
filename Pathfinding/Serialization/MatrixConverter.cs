using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000050 RID: 80
	public class MatrixConverter : JsonConverter
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00013E9C File Offset: 0x0001229C
		public override bool CanConvert(Type type)
		{
			return type == typeof(Matrix4x4);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00013EAC File Offset: 0x000122AC
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			Array array = (Array)values["values"];
			if (array.Length != 16)
			{
				Debug.LogError("Number of elements in matrix was not 16 (got " + array.Length + ")");
				return matrix4x;
			}
			for (int i = 0; i < 16; i++)
			{
				matrix4x[i] = Convert.ToSingle(array.GetValue(i));
			}
			return matrix4x;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00013F34 File Offset: 0x00012334
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Matrix4x4 matrix4x = (Matrix4x4)value;
			for (int i = 0; i < this.values.Length; i++)
			{
				this.values[i] = matrix4x[i];
			}
			return new Dictionary<string, object>
			{
				{
					"values",
					this.values
				}
			};
		}

		// Token: 0x04000225 RID: 549
		private float[] values = new float[16];
	}
}
