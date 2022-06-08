using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A9 RID: 169
	public class FleePath : RandomPath
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00032FC1 File Offset: 0x000313C1
		[Obsolete("Please use the Construct method instead")]
		public FleePath(Vector3 start, Vector3 avoid, int length, OnPathDelegate callbackDelegate = null) : base(start, length, callbackDelegate)
		{
			throw new Exception("Please use the Construct method instead");
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00032FD7 File Offset: 0x000313D7
		public FleePath()
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00032FE0 File Offset: 0x000313E0
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool<FleePath>.GetPath();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00033000 File Offset: 0x00031400
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = avoid - start;
			this.aim *= 10f;
			this.aim = start - this.aim;
		}
	}
}
