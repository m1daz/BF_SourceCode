using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003E2 RID: 994
	[Serializable]
	public class ECamera
	{
		// Token: 0x06001DF4 RID: 7668 RVA: 0x000E6C6B File Offset: 0x000E506B
		public ECamera(Camera cam, bool gui)
		{
			this.camera = cam;
			this.guiCamera = gui;
		}

		// Token: 0x04001EFE RID: 7934
		public Camera camera;

		// Token: 0x04001EFF RID: 7935
		public bool guiCamera;
	}
}
