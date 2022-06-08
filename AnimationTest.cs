using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class AnimationTest : MonoBehaviour
{
	// Token: 0x06000D43 RID: 3395 RVA: 0x0006D4EE File Offset: 0x0006B8EE
	private void Start()
	{
		this.AnimatorControl = base.GetComponent<Animator>();
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0006D4FC File Offset: 0x0006B8FC
	private void Update()
	{
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0006D500 File Offset: 0x0006B900
	private void OnGUI()
	{
		if (GUI.Button(new Rect(100f, 100f, 80f, 80f), "1"))
		{
			for (int i = 0; i < this.weapons.Length; i++)
			{
				if (this.weapons[i].name == "BallisticKnife")
				{
					this.weapons[i].SetActive(true);
				}
				else if (this.weapons[i].activeSelf)
				{
					this.weapons[i].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 1);
		}
		if (GUI.Button(new Rect(100f, 200f, 80f, 80f), "2"))
		{
			for (int j = 0; j < this.weapons.Length; j++)
			{
				if (this.weapons[j].name == "DesertEagle")
				{
					this.weapons[j].SetActive(true);
				}
				else if (this.weapons[j].activeSelf)
				{
					this.weapons[j].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 2);
		}
		if (GUI.Button(new Rect(100f, 300f, 80f, 80f), "3"))
		{
			for (int k = 0; k < this.weapons.Length; k++)
			{
				if (this.weapons[k].name == "AK47")
				{
					this.weapons[k].SetActive(true);
				}
				else if (this.weapons[k].activeSelf)
				{
					this.weapons[k].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 3);
		}
		if (GUI.Button(new Rect(100f, 400f, 80f, 80f), "4"))
		{
			for (int l = 0; l < this.weapons.Length; l++)
			{
				if (this.weapons[l].name == "M4")
				{
					this.weapons[l].SetActive(true);
				}
				else if (this.weapons[l].activeSelf)
				{
					this.weapons[l].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 4);
		}
		if (GUI.Button(new Rect(100f, 500f, 80f, 80f), "5"))
		{
			for (int m = 0; m < this.weapons.Length; m++)
			{
				if (this.weapons[m].name == "M87T")
				{
					this.weapons[m].SetActive(true);
				}
				else if (this.weapons[m].activeSelf)
				{
					this.weapons[m].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 5);
		}
		if (GUI.Button(new Rect(100f, 600f, 80f, 80f), "6"))
		{
			for (int n = 0; n < this.weapons.Length; n++)
			{
				if (this.weapons[n].name == "AWP")
				{
					this.weapons[n].SetActive(true);
				}
				else if (this.weapons[n].activeSelf)
				{
					this.weapons[n].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 6);
		}
		if (GUI.Button(new Rect(100f, 700f, 80f, 80f), "7"))
		{
			for (int num = 0; num < this.weapons.Length; num++)
			{
				if (this.weapons[num].name == "RPG")
				{
					this.weapons[num].SetActive(true);
				}
				else if (this.weapons[num].activeSelf)
				{
					this.weapons[num].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 7);
		}
		if (GUI.Button(new Rect(100f, 800f, 80f, 80f), "8"))
		{
			for (int num2 = 0; num2 < this.weapons.Length; num2++)
			{
				if (this.weapons[num2].name == "M67")
				{
					this.weapons[num2].SetActive(true);
				}
				else if (this.weapons[num2].activeSelf)
				{
					this.weapons[num2].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 8);
		}
		if (GUI.Button(new Rect(200f, 100f, 80f, 80f), "9"))
		{
			for (int num3 = 0; num3 < this.weapons.Length; num3++)
			{
				if (this.weapons[num3].name == "GLOCK21")
				{
					this.weapons[num3].SetActive(true);
				}
				else if (this.weapons[num3].activeSelf)
				{
					this.weapons[num3].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 9);
		}
		if (GUI.Button(new Rect(400f, 100f, 80f, 80f), "10"))
		{
			for (int num4 = 0; num4 < this.weapons.Length; num4++)
			{
				if (this.weapons[num4].name == "MP5KA5")
				{
					this.weapons[num4].SetActive(true);
				}
				else if (this.weapons[num4].activeSelf)
				{
					this.weapons[num4].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 10);
		}
		if (GUI.Button(new Rect(400f, 200f, 80f, 80f), "11"))
		{
			for (int num5 = 0; num5 < this.weapons.Length; num5++)
			{
				if (this.weapons[num5].name == "UZI")
				{
					this.weapons[num5].SetActive(true);
				}
				else if (this.weapons[num5].activeSelf)
				{
					this.weapons[num5].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 11);
		}
		if (GUI.Button(new Rect(200f, 200f, 80f, 80f), "12"))
		{
			for (int num6 = 0; num6 < this.weapons.Length; num6++)
			{
				if (this.weapons[num6].name == "G36K")
				{
					this.weapons[num6].SetActive(true);
				}
				else if (this.weapons[num6].activeSelf)
				{
					this.weapons[num6].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 12);
		}
		if (GUI.Button(new Rect(200f, 300f, 80f, 80f), "13"))
		{
			for (int num7 = 0; num7 < this.weapons.Length; num7++)
			{
				if (this.weapons[num7].name == "M249")
				{
					this.weapons[num7].SetActive(true);
				}
				else if (this.weapons[num7].activeSelf)
				{
					this.weapons[num7].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 13);
		}
		if (GUI.Button(new Rect(200f, 400f, 80f, 80f), "14"))
		{
			for (int num8 = 0; num8 < this.weapons.Length; num8++)
			{
				if (this.weapons[num8].name == "MilkBomb")
				{
					this.weapons[num8].SetActive(true);
				}
				else if (this.weapons[num8].activeSelf)
				{
					this.weapons[num8].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 14);
		}
		if (GUI.Button(new Rect(200f, 500f, 80f, 80f), "15"))
		{
			for (int num9 = 0; num9 < this.weapons.Length; num9++)
			{
				if (this.weapons[num9].name == "CandyRifle")
				{
					this.weapons[num9].SetActive(true);
				}
				else if (this.weapons[num9].activeSelf)
				{
					this.weapons[num9].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 15);
		}
		if (GUI.Button(new Rect(200f, 600f, 80f, 80f), "16"))
		{
			for (int num10 = 0; num10 < this.weapons.Length; num10++)
			{
				if (this.weapons[num10].name == "ChristmasSniper")
				{
					this.weapons[num10].SetActive(true);
				}
				else if (this.weapons[num10].activeSelf)
				{
					this.weapons[num10].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 16);
		}
		if (GUI.Button(new Rect(200f, 700f, 80f, 80f), "17"))
		{
			for (int num11 = 0; num11 < this.weapons.Length; num11++)
			{
				if (this.weapons[num11].name == "GingerBreadBomb")
				{
					this.weapons[num11].SetActive(true);
				}
				else if (this.weapons[num11].activeSelf)
				{
					this.weapons[num11].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 17);
		}
		if (GUI.Button(new Rect(200f, 800f, 80f, 80f), "18"))
		{
			for (int num12 = 0; num12 < this.weapons.Length; num12++)
			{
				if (this.weapons[num12].name == "GingerBreadKnife")
				{
					this.weapons[num12].SetActive(true);
				}
				else if (this.weapons[num12].activeSelf)
				{
					this.weapons[num12].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 18);
		}
		if (GUI.Button(new Rect(400f, 300f, 80f, 80f), "19"))
		{
			for (int num13 = 0; num13 < this.weapons.Length; num13++)
			{
				if (this.weapons[num13].name == "SantaGun")
				{
					this.weapons[num13].SetActive(true);
				}
				else if (this.weapons[num13].activeSelf)
				{
					this.weapons[num13].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 19);
		}
		if (GUI.Button(new Rect(400f, 400f, 80f, 80f), "20"))
		{
			for (int num14 = 0; num14 < this.weapons.Length; num14++)
			{
				if (this.weapons[num14].name == "AUG")
				{
					this.weapons[num14].SetActive(true);
				}
				else if (this.weapons[num14].activeSelf)
				{
					this.weapons[num14].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 20);
		}
		if (GUI.Button(new Rect(400f, 500f, 80f, 80f), "21"))
		{
			for (int num15 = 0; num15 < this.weapons.Length; num15++)
			{
				if (this.weapons[num15].name == "M3")
				{
					this.weapons[num15].SetActive(true);
				}
				else if (this.weapons[num15].activeSelf)
				{
					this.weapons[num15].SetActive(false);
				}
			}
			this.AnimatorControl.SetInteger("WeaponID", 21);
		}
		if (GUI.Button(new Rect(300f, 100f, 80f, 80f), "fire"))
		{
			this.AnimatorControl.SetBool("fire", true);
		}
		if (GUI.Button(new Rect(300f, 200f, 80f, 80f), "stopfire"))
		{
			this.AnimatorControl.SetBool("fire", false);
		}
		if (GUI.Button(new Rect(300f, 300f, 80f, 80f), "reload"))
		{
			this.AnimatorControl.SetBool("reload", true);
		}
		if (GUI.Button(new Rect(300f, 400f, 80f, 80f), "idle"))
		{
			this.AnimatorControl.SetFloat("speed", 0f);
		}
		if (GUI.Button(new Rect(300f, 500f, 80f, 80f), "walk"))
		{
			this.AnimatorControl.SetFloat("speed", 1f);
		}
		if (GUI.Button(new Rect(300f, 600f, 80f, 80f), "dead"))
		{
			this.AnimatorControl.SetBool("dead", true);
		}
		if (GUI.Button(new Rect(300f, 700f, 80f, 80f), "live"))
		{
			this.AnimatorControl.SetBool("live", true);
		}
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0006E46E File Offset: 0x0006C86E
	private void ChangeWeaponIdToNull()
	{
		this.AnimatorControl.SetInteger("WeaponID", 0);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0006E481 File Offset: 0x0006C881
	private void AutoStopReload()
	{
		this.AnimatorControl.SetBool("reload", false);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0006E494 File Offset: 0x0006C894
	private void AutoStopFire()
	{
		this.AnimatorControl.SetBool("fire", false);
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0006E4A7 File Offset: 0x0006C8A7
	private void DeadOver()
	{
		this.AnimatorControl.SetBool("dead", false);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0006E4BA File Offset: 0x0006C8BA
	private void LiveOver()
	{
		this.AnimatorControl.SetBool("live", false);
	}

	// Token: 0x04000D3C RID: 3388
	public GameObject[] weapons;

	// Token: 0x04000D3D RID: 3389
	private Animator AnimatorControl;
}
