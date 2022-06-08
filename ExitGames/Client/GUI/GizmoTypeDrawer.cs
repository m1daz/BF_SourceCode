using System;
using UnityEngine;

namespace ExitGames.Client.GUI
{
	// Token: 0x020000EB RID: 235
	public class GizmoTypeDrawer
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x000399F4 File Offset: 0x00037DF4
		public static void Draw(Vector3 center, GizmoType type, Color color, float size)
		{
			Gizmos.color = color;
			switch (type)
			{
			case GizmoType.WireSphere:
				Gizmos.DrawWireSphere(center, size * 0.5f);
				break;
			case GizmoType.Sphere:
				Gizmos.DrawSphere(center, size * 0.5f);
				break;
			case GizmoType.WireCube:
				Gizmos.DrawWireCube(center, Vector3.one * size);
				break;
			case GizmoType.Cube:
				Gizmos.DrawCube(center, Vector3.one * size);
				break;
			}
		}
	}
}
