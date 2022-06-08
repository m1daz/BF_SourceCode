using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A0 RID: 160
	public class ModifierConverter
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00030921 File Offset: 0x0002ED21
		public static bool AllBits(ModifierData a, ModifierData b)
		{
			return (a & b) == b;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00030929 File Offset: 0x0002ED29
		public static bool AnyBits(ModifierData a, ModifierData b)
		{
			return (a & b) != ModifierData.None;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00030934 File Offset: 0x0002ED34
		public static ModifierData Convert(Path p, ModifierData input, ModifierData output)
		{
			if (!ModifierConverter.CanConvert(input, output))
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Can't convert ",
					input,
					" to ",
					output
				}));
				return ModifierData.None;
			}
			if (ModifierConverter.AnyBits(input, output))
			{
				return input;
			}
			if (ModifierConverter.AnyBits(input, ModifierData.Nodes) && ModifierConverter.AnyBits(output, ModifierData.Vector))
			{
				p.vectorPath.Clear();
				for (int i = 0; i < p.vectorPath.Count; i++)
				{
					p.vectorPath.Add((Vector3)p.path[i].position);
				}
				return ModifierData.VectorPath | ((!ModifierConverter.AnyBits(input, ModifierData.StrictNodePath)) ? ModifierData.None : ModifierData.StrictVectorPath);
			}
			Debug.LogError(string.Concat(new object[]
			{
				"This part should not be reached - Error in ModifierConverted\nInput: ",
				input,
				" (",
				(int)input,
				")\nOutput: ",
				output,
				" (",
				(int)output,
				")"
			}));
			return ModifierData.None;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00030A64 File Offset: 0x0002EE64
		public static bool CanConvert(ModifierData input, ModifierData output)
		{
			ModifierData b = ModifierConverter.CanConvertTo(input);
			return ModifierConverter.AnyBits(output, b);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00030A80 File Offset: 0x0002EE80
		public static ModifierData CanConvertTo(ModifierData a)
		{
			if (a == ModifierData.All)
			{
				return ModifierData.All;
			}
			ModifierData modifierData = a;
			if (ModifierConverter.AnyBits(a, ModifierData.Nodes))
			{
				modifierData |= ModifierData.VectorPath;
			}
			if (ModifierConverter.AnyBits(a, ModifierData.StrictNodePath))
			{
				modifierData |= ModifierData.StrictVectorPath;
			}
			if (ModifierConverter.AnyBits(a, ModifierData.StrictVectorPath))
			{
				modifierData |= ModifierData.VectorPath;
			}
			return modifierData;
		}
	}
}
