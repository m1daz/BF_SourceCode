using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class AstarColor
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000037A8 File Offset: 0x00001BA8
		public AstarColor()
		{
			this._NodeConnection = new Color(1f, 1f, 1f, 0.9f);
			this._UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);
			this._BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);
			this._ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);
			this._ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);
			this._MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);
			this._MeshColor = new Color(0.125f, 0.686f, 0f, 0.19f);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003894 File Offset: 0x00001C94
		public static Color GetAreaColor(int area)
		{
			if (AstarColor.AreaColors == null || area >= AstarColor.AreaColors.Length)
			{
				return Mathfx.IntToColor(area, 1f);
			}
			return AstarColor.AreaColors[area];
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000038CC File Offset: 0x00001CCC
		public void OnEnable()
		{
			AstarColor.NodeConnection = this._NodeConnection;
			AstarColor.UnwalkableNode = this._UnwalkableNode;
			AstarColor.BoundsHandles = this._BoundsHandles;
			AstarColor.ConnectionLowLerp = this._ConnectionLowLerp;
			AstarColor.ConnectionHighLerp = this._ConnectionHighLerp;
			AstarColor.MeshEdgeColor = this._MeshEdgeColor;
			AstarColor.MeshColor = this._MeshColor;
			AstarColor.AreaColors = this._AreaColors;
		}

		// Token: 0x04000047 RID: 71
		public Color _NodeConnection;

		// Token: 0x04000048 RID: 72
		public Color _UnwalkableNode;

		// Token: 0x04000049 RID: 73
		public Color _BoundsHandles;

		// Token: 0x0400004A RID: 74
		public Color _ConnectionLowLerp;

		// Token: 0x0400004B RID: 75
		public Color _ConnectionHighLerp;

		// Token: 0x0400004C RID: 76
		public Color _MeshEdgeColor;

		// Token: 0x0400004D RID: 77
		public Color _MeshColor;

		// Token: 0x0400004E RID: 78
		public Color[] _AreaColors;

		// Token: 0x0400004F RID: 79
		public static Color NodeConnection = new Color(1f, 1f, 1f, 0.9f);

		// Token: 0x04000050 RID: 80
		public static Color UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04000051 RID: 81
		public static Color BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);

		// Token: 0x04000052 RID: 82
		public static Color ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);

		// Token: 0x04000053 RID: 83
		public static Color ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04000054 RID: 84
		public static Color MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000055 RID: 85
		public static Color MeshColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000056 RID: 86
		private static Color[] AreaColors;
	}
}
