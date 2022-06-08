using System;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x02000354 RID: 852
	public class SkinDefine
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x000D7060 File Offset: 0x000D5460
		public static Rect GetSkinSideRect(SkinModuleType smType, SkinModuleSideType smSide)
		{
			switch (smType)
			{
			case SkinModuleType.Head:
				return new Rect((float)SkinDefine.HeadRectList[(int)smSide, 0], (float)SkinDefine.HeadRectList[(int)smSide, 1], (float)SkinDefine.HeadRectList[(int)smSide, 2], (float)SkinDefine.HeadRectList[(int)smSide, 3]);
			case SkinModuleType.Body:
				return new Rect((float)SkinDefine.BodyRectList[(int)smSide, 0], (float)SkinDefine.BodyRectList[(int)smSide, 1], (float)SkinDefine.BodyRectList[(int)smSide, 2], (float)SkinDefine.BodyRectList[(int)smSide, 3]);
			case SkinModuleType.Leg:
				return new Rect((float)SkinDefine.LegRectList[(int)smSide, 0], (float)SkinDefine.LegRectList[(int)smSide, 1], (float)SkinDefine.LegRectList[(int)smSide, 2], (float)SkinDefine.LegRectList[(int)smSide, 3]);
			case SkinModuleType.Arm:
				return new Rect((float)SkinDefine.ArmRectList[(int)smSide, 0], (float)SkinDefine.ArmRectList[(int)smSide, 1], (float)SkinDefine.ArmRectList[(int)smSide, 2], (float)SkinDefine.ArmRectList[(int)smSide, 3]);
			case SkinModuleType.Foot:
				return new Rect((float)SkinDefine.FootRectList[(int)smSide, 0], (float)SkinDefine.FootRectList[(int)smSide, 1], (float)SkinDefine.FootRectList[(int)smSide, 2], (float)SkinDefine.FootRectList[(int)smSide, 3]);
			default:
				return new Rect(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x04001D00 RID: 7424
		public static readonly int TexWidth = 64;

		// Token: 0x04001D01 RID: 7425
		public static readonly int TexHeight = 32;

		// Token: 0x04001D02 RID: 7426
		public static int[,] HeadRectList = new int[,]
		{
			{
				32,
				24,
				8,
				8
			},
			{
				40,
				24,
				8,
				8
			},
			{
				0,
				24,
				8,
				8
			},
			{
				8,
				24,
				8,
				8
			},
			{
				16,
				24,
				8,
				8
			},
			{
				24,
				24,
				8,
				8
			}
		};

		// Token: 0x04001D03 RID: 7427
		public static int[,] BodyRectList = new int[,]
		{
			{
				24,
				20,
				8,
				4
			},
			{
				24,
				16,
				8,
				4
			},
			{
				0,
				12,
				4,
				12
			},
			{
				4,
				12,
				8,
				12
			},
			{
				12,
				12,
				4,
				12
			},
			{
				16,
				12,
				8,
				12
			}
		};

		// Token: 0x04001D04 RID: 7428
		public static int[,] LegRectList = new int[,]
		{
			{
				36,
				8,
				4,
				4
			},
			{
				40,
				8,
				4,
				4
			},
			{
				20,
				2,
				4,
				10
			},
			{
				24,
				2,
				4,
				10
			},
			{
				28,
				2,
				4,
				10
			},
			{
				32,
				2,
				4,
				10
			}
		};

		// Token: 0x04001D05 RID: 7429
		public static int[,] ArmRectList = new int[,]
		{
			{
				16,
				8,
				4,
				4
			},
			{
				16,
				4,
				4,
				4
			},
			{
				0,
				0,
				4,
				12
			},
			{
				4,
				0,
				4,
				12
			},
			{
				8,
				0,
				4,
				12
			},
			{
				12,
				0,
				4,
				12
			}
		};

		// Token: 0x04001D06 RID: 7430
		public static int[,] FootRectList = new int[,]
		{
			{
				36,
				2,
				4,
				6
			},
			{
				46,
				2,
				4,
				6
			},
			{
				30,
				0,
				6,
				2
			},
			{
				36,
				0,
				4,
				2
			},
			{
				40,
				0,
				6,
				2
			},
			{
				46,
				0,
				4,
				2
			}
		};
	}
}
