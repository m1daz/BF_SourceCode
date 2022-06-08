using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class FirstStartController : MonoBehaviour
{
	// Token: 0x060014D0 RID: 5328 RVA: 0x000B2556 File Offset: 0x000B0956
	private void Awake()
	{
		base.StartCoroutine(this.GetVersionFromServer("bf"));
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x000B256A File Offset: 0x000B096A
	private void Start()
	{
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000B256C File Offset: 0x000B096C
	private void Update()
	{
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x000B2570 File Offset: 0x000B0970
	private IEnumerator GetVersionFromServer(string name)
	{
		WWW w = new WWW(string.Concat(new object[]
		{
			"http://52.26.193.247:",
			81,
			"/",
			name
		}));
		yield return w;
		if (string.IsNullOrEmpty(w.error))
		{
			string[] array = w.text.Split(new char[]
			{
				'"'
			});
			if (array.Length > 7)
			{
				string text = array[3];
				int num = int.Parse(array[7]);
				string[] array2 = text.Split(new char[]
				{
					'_'
				});
				string[] array3 = GameVersionController.GameVersion.Split(new char[]
				{
					'_'
				});
				if (array2.Length == 3 && array3.Length == 3)
				{
					int num2 = 100 * int.Parse(array2[0]) + 10 * int.Parse(array2[1]) + int.Parse(array2[2]);
					int num3 = 100 * int.Parse(array3[0]) + 10 * int.Parse(array3[1]) + int.Parse(array3[2]);
					if (num2 > num3)
					{
						if (num == 100)
						{
							EventDelegate btnEventName = new EventDelegate(this, "MustOpenAppToUpdate");
							if (UITipController.mInstance != null)
							{
								UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonCloseTip, "New version is available!", Color.white, "UPDATE NOW!", null, btnEventName, null, null);
								UITipController.mInstance.oneButtonCloseTipCloseBtnObj.SetActive(false);
							}
						}
						else if (UnityEngine.Random.Range(1, 100) < num)
						{
							EventDelegate btnEventName2 = new EventDelegate(this, "OpenAppToUpdate");
							if (UITipController.mInstance != null)
							{
								UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonCloseTip, "New version is available!", Color.white, "UPDATE NOW!", null, btnEventName2, null, null);
							}
						}
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x000B2592 File Offset: 0x000B0992
	public void OpenAppToUpdate()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000B259E File Offset: 0x000B099E
	public void MustOpenAppToUpdate()
	{
	}

	// Token: 0x0400177F RID: 6015
	private const string ip = "52.26.193.247";

	// Token: 0x04001780 RID: 6016
	private const int port = 81;
}
