using System;
using Photon;
using UnityEngine;

// Token: 0x02000154 RID: 340
[RequireComponent(typeof(PhotonView))]
public class ShowInfoOfPlayer : Photon.MonoBehaviour
{
	// Token: 0x060009E6 RID: 2534 RVA: 0x00049E68 File Offset: 0x00048268
	private void Start()
	{
		if (this.font == null)
		{
			this.font = (Font)Resources.FindObjectsOfTypeAll(typeof(Font))[0];
			Debug.LogWarning("No font defined. Found font: " + this.font);
		}
		if (this.tm == null)
		{
			this.textGo = new GameObject("3d text");
			this.textGo.transform.parent = base.gameObject.transform;
			this.textGo.transform.localPosition = Vector3.zero;
			MeshRenderer meshRenderer = this.textGo.AddComponent<MeshRenderer>();
			meshRenderer.material = this.font.material;
			this.tm = this.textGo.AddComponent<TextMesh>();
			this.tm.font = this.font;
			this.tm.anchor = TextAnchor.MiddleCenter;
			if (this.CharacterSize > 0f)
			{
				this.tm.characterSize = this.CharacterSize;
			}
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00049F74 File Offset: 0x00048374
	private void Update()
	{
		bool flag = !this.DisableOnOwnObjects || base.photonView.isMine;
		if (this.textGo != null)
		{
			this.textGo.SetActive(flag);
		}
		if (!flag)
		{
			return;
		}
		PhotonPlayer owner = base.photonView.owner;
		if (owner != null)
		{
			this.tm.text = ((!string.IsNullOrEmpty(owner.name)) ? owner.name : ("player" + owner.ID));
		}
		else if (base.photonView.isSceneView)
		{
			this.tm.text = "scn";
		}
		else
		{
			this.tm.text = "n/a";
		}
	}

	// Token: 0x040008B7 RID: 2231
	private GameObject textGo;

	// Token: 0x040008B8 RID: 2232
	private TextMesh tm;

	// Token: 0x040008B9 RID: 2233
	public float CharacterSize;

	// Token: 0x040008BA RID: 2234
	public Font font;

	// Token: 0x040008BB RID: 2235
	public bool DisableOnOwnObjects;
}
