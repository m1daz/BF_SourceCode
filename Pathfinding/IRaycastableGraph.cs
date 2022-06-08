using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000010 RID: 16
	public interface IRaycastableGraph
	{
		// Token: 0x06000054 RID: 84
		bool Linecast(Vector3 start, Vector3 end);

		// Token: 0x06000055 RID: 85
		bool Linecast(Vector3 start, Vector3 end, Node hint);

		// Token: 0x06000056 RID: 86
		bool Linecast(Vector3 start, Vector3 end, Node hint, out GraphHitInfo hit);
	}
}
