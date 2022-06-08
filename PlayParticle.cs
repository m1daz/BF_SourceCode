using System;
using UnityEngine;

// Token: 0x020006AB RID: 1707
public class PlayParticle : MonoBehaviour
{
	// Token: 0x0600323D RID: 12861 RVA: 0x001634CE File Offset: 0x001618CE
	private void Start()
	{
		this.ShowParticle = 0;
		if (this.myParticles.Length != 0)
		{
			this.myParticles[this.ShowParticle].SetActive(true);
		}
		this.tip = false;
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x001634FE File Offset: 0x001618FE
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.prevParticle();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.nextParticle();
		}
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x00163530 File Offset: 0x00161930
	private void OnGUI()
	{
		if (GUI.Button(new Rect((float)Screen.width * 0.7f, 0f, (float)Screen.width * 0.05f, (float)Screen.width * 0.05f), ">"))
		{
			this.nextParticle();
		}
		if (this.myParticles.Length != 0)
		{
			if (this.myParticles[this.ShowParticle] != null)
			{
				GUI.Box(new Rect((float)Screen.width * 0.3f, 0f, (float)Screen.width * 0.4f, (float)Screen.width * 0.05f), "NO. : " + this.ShowParticle.ToString() + "\n" + this.myParticles[this.ShowParticle].name);
			}
			else
			{
				GUI.Box(new Rect((float)Screen.width * 0.3f, 0f, (float)Screen.width * 0.4f, (float)Screen.width * 0.05f), "NO. : " + this.ShowParticle.ToString() + "\n東西不見啦!!");
			}
		}
		else
		{
			this.tip = true;
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.25f, 0f, (float)Screen.width * 0.05f, (float)Screen.width * 0.05f), "<"))
		{
			this.prevParticle();
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.95f, 0f, (float)Screen.width * 0.05f, (float)Screen.width * 0.05f), "?"))
		{
			this.tip = !this.tip;
		}
		if (this.tip && GUI.Button(new Rect((float)Screen.width * 0f, (float)Screen.width * 0.1f, (float)Screen.width * 1f, (float)Screen.width * 0.3f), "Press < and > to see the next one!"))
		{
			this.tip = !this.tip;
		}
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x0016375C File Offset: 0x00161B5C
	private void nextParticle()
	{
		if (this.myParticles[this.ShowParticle] != null)
		{
			this.myParticles[this.ShowParticle].SetActive(false);
		}
		this.ShowParticle++;
		if (this.ShowParticle >= this.myParticles.Length)
		{
			this.ShowParticle = 0;
		}
		if (this.myParticles[this.ShowParticle] != null)
		{
			this.myParticles[this.ShowParticle].SetActive(true);
		}
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x001637E8 File Offset: 0x00161BE8
	private void prevParticle()
	{
		if (this.myParticles[this.ShowParticle] != null)
		{
			this.myParticles[this.ShowParticle].SetActive(false);
		}
		this.ShowParticle--;
		if (this.ShowParticle < 0)
		{
			this.ShowParticle = this.myParticles.Length - 1;
		}
		if (this.myParticles[this.ShowParticle] != null)
		{
			this.myParticles[this.ShowParticle].SetActive(true);
		}
	}

	// Token: 0x04002EC7 RID: 11975
	public GameObject[] myParticles;

	// Token: 0x04002EC8 RID: 11976
	private int ShowParticle;

	// Token: 0x04002EC9 RID: 11977
	private bool tip;

	// Token: 0x04002ECA RID: 11978
	private bool rotate;
}
