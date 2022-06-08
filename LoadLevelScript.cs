using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003F0 RID: 1008
public class LoadLevelScript : MonoBehaviour
{
	// Token: 0x06001E48 RID: 7752 RVA: 0x000E7E89 File Offset: 0x000E6289
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x000E7E95 File Offset: 0x000E6295
	public void LoadJoystickEvent()
	{
		SceneManager.LoadScene("Joystick-Event-Input");
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x000E7EA1 File Offset: 0x000E62A1
	public void LoadJoysticParameter()
	{
		SceneManager.LoadScene("Joystick-Parameter");
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x000E7EAD File Offset: 0x000E62AD
	public void LoadDPadEvent()
	{
		SceneManager.LoadScene("DPad-Event-Input");
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x000E7EB9 File Offset: 0x000E62B9
	public void LoadDPadClassicalTime()
	{
		SceneManager.LoadScene("DPad-Classical-Time");
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x000E7EC5 File Offset: 0x000E62C5
	public void LoadTouchPad()
	{
		SceneManager.LoadScene("TouchPad-Event-Input");
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x000E7ED1 File Offset: 0x000E62D1
	public void LoadButton()
	{
		SceneManager.LoadScene("Button-Event-Input");
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x000E7EDD File Offset: 0x000E62DD
	public void LoadFPS()
	{
		SceneManager.LoadScene("FPS_Example");
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x000E7EE9 File Offset: 0x000E62E9
	public void LoadThird()
	{
		SceneManager.LoadScene("ThirdPerson+Jump");
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x000E7EF5 File Offset: 0x000E62F5
	public void LoadThirddungeon()
	{
		SceneManager.LoadScene("ThirdPersonDungeon+Jump");
	}
}
