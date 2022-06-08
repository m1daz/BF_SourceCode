using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class GGNetWorkPlayerAnimationControl : MonoBehaviour
{
	// Token: 0x06001081 RID: 4225 RVA: 0x0008E19C File Offset: 0x0008C59C
	private void Start()
	{
		this.animatorControl = base.GetComponent<Animator>();
		this.DamagePositions = base.transform.Find("DamagePositions").gameObject;
		this.mNetworkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
		this.preWalkAction = GGCharacterWalkState.Idle;
		this.preFireAction = GGCharacterFireState.Idle;
		this.preWeaponIndex = 0;
		this.animatorControl.SetInteger("WeaponID", 0);
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0008E20C File Offset: 0x0008C60C
	private void Update()
	{
		if (this.preWeaponIndex != this.mNetworkCharacter.mWeaponType)
		{
			this.preWeaponIndex = this.mNetworkCharacter.mWeaponType;
			this.animatorControl.SetInteger("WeaponID", this.preWeaponIndex);
		}
		this.currentWalkAction = this.mNetworkCharacter.mCharacterWalkState;
		if (this.preWalkAction != this.currentWalkAction)
		{
			if (this.preWalkAction != GGCharacterWalkState.Dead)
			{
				if (this.currentWalkAction == GGCharacterWalkState.Walk)
				{
					this.animatorControl.SetFloat("speed", 1f);
				}
				else if (this.currentWalkAction == GGCharacterWalkState.Idle)
				{
					this.animatorControl.SetFloat("speed", 0f);
				}
				else if (this.currentWalkAction == GGCharacterWalkState.Dead)
				{
					this.animatorControl.SetBool("dead", true);
					this.DamagePositions.SetActiveRecursively(false);
					if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
					{
						if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.PlayerDeathEffect, base.transform.root.position + new Vector3(0f, 1.3f, 0f), Quaternion.Euler(new Vector3(270f, 0f, 0f)));
						}
						else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.ZombieDeathEffect, base.transform.root.position + new Vector3(0f, 1.3f, 0f), Quaternion.Euler(new Vector3(270f, 0f, 0f)));
						}
					}
					else
					{
						UnityEngine.Object.Instantiate<GameObject>(this.PlayerDeathEffect, base.transform.root.position + new Vector3(0f, 1.3f, 0f), Quaternion.Euler(new Vector3(270f, 0f, 0f)));
					}
				}
			}
			else if (this.currentWalkAction != GGCharacterWalkState.Dead)
			{
				this.animatorControl.SetBool("live", true);
				this.DamagePositions.SetActiveRecursively(true);
				base.transform.root.position = this.mNetworkCharacter.mPosition;
				base.transform.root.rotation = this.mNetworkCharacter.mRotation;
			}
			this.preWalkAction = this.currentWalkAction;
		}
		this.currentFireAction = this.mNetworkCharacter.mCharacterFireState;
		if (this.preFireAction != this.currentFireAction)
		{
			if (this.currentFireAction == GGCharacterFireState.Idle)
			{
				this.animatorControl.SetBool("fire", false);
				this.animatorControl.SetBool("fire_R", false);
				this.animatorControl.SetBool("fire_L", false);
			}
			else if (this.currentFireAction == GGCharacterFireState.Reload)
			{
				this.animatorControl.SetBool("reload", true);
			}
			else if (this.currentFireAction == GGCharacterFireState.Fire)
			{
				this.animatorControl.SetBool("fire", true);
			}
			this.preFireAction = this.currentFireAction;
		}
		if (this.currentFireAction == GGCharacterFireState.Fire && this.PredualGunHandIndex != this.dualGunHandIndex)
		{
			if (this.dualGunHandIndex % 2 == 1)
			{
				this.animatorControl.SetBool("fire_R", true);
				this.animatorControl.SetBool("fire_L", false);
			}
			else
			{
				this.animatorControl.SetBool("fire_L", true);
				this.animatorControl.SetBool("fire_R", false);
			}
			this.PredualGunHandIndex = this.dualGunHandIndex;
		}
		if (!this.mNetworkCharacter.isSingleMode)
		{
			if (this.mNetworkCharacter.mPlayerProperties.team != this.preTeam)
			{
				this.preTeam = this.mNetworkCharacter.mPlayerProperties.team;
				base.transform.root.position = this.mNetworkCharacter.mPosition;
			}
			if (this.currentWalkAction != GGCharacterWalkState.Dead)
			{
				base.transform.root.rotation = Quaternion.Lerp(base.transform.root.rotation, this.mNetworkCharacter.mRotation, Time.deltaTime * 8f);
			}
			base.transform.root.position = Vector3.Lerp(base.transform.root.position, this.mNetworkCharacter.mPosition, Time.deltaTime * 8f);
		}
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0008E6A5 File Offset: 0x0008CAA5
	private void ReloadWhenAttack()
	{
		this.animatorControl.SetBool("reload", true);
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0008E6B8 File Offset: 0x0008CAB8
	private void ChangeWeaponIdToNull()
	{
		this.animatorControl.SetInteger("WeaponID", 0);
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0008E6CB File Offset: 0x0008CACB
	private void AutoStopReload()
	{
		this.animatorControl.SetBool("reload", false);
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0008E6DE File Offset: 0x0008CADE
	private void AutoStopFire()
	{
		this.animatorControl.SetBool("fire", false);
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0008E6F1 File Offset: 0x0008CAF1
	private void DeadOver()
	{
		this.animatorControl.SetBool("dead", false);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0008E704 File Offset: 0x0008CB04
	private void LiveOver()
	{
		this.animatorControl.SetBool("live", false);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0008E718 File Offset: 0x0008CB18
	private void RPGMissileShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0008E7A4 File Offset: 0x0008CBA4
	private void RPGMissileDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0008E830 File Offset: 0x0008CC30
	private void M67Show()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0008E8BC File Offset: 0x0008CCBC
	private void M67DontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0008E948 File Offset: 0x0008CD48
	private void MilkBombShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0008E9D4 File Offset: 0x0008CDD4
	private void MilkBombDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0008EA60 File Offset: 0x0008CE60
	private void GingerBreadBombShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0008EAEC File Offset: 0x0008CEEC
	private void GingerBreadBombDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0008EB78 File Offset: 0x0008CF78
	private void STENClipShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x0008EC04 File Offset: 0x0008D004
	private void STENClipDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0008EC90 File Offset: 0x0008D090
	private void SmokeBombShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0008ED1C File Offset: 0x0008D11C
	private void SmokeBombDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x0008EDA8 File Offset: 0x0008D1A8
	private void FlashBombShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x0008EE34 File Offset: 0x0008D234
	private void FlashBombDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0008EEC0 File Offset: 0x0008D2C0
	private void SnowmanBombShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0008EF4C File Offset: 0x0008D34C
	private void SnowmanBombDontShow()
	{
		IEnumerator enumerator = this.multiplayerWeaponManager.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					transform.gameObject.BroadcastMessage("WeaponPartShow", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x040012DB RID: 4827
	private Animator animatorControl;

	// Token: 0x040012DC RID: 4828
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x040012DD RID: 4829
	private GGCharacterWalkState preWalkAction;

	// Token: 0x040012DE RID: 4830
	private GGCharacterWalkState currentWalkAction;

	// Token: 0x040012DF RID: 4831
	private GGCharacterFireState preFireAction;

	// Token: 0x040012E0 RID: 4832
	private GGCharacterFireState currentFireAction;

	// Token: 0x040012E1 RID: 4833
	private GGTeamType preTeam = GGTeamType.Nil;

	// Token: 0x040012E2 RID: 4834
	private int preWeaponIndex;

	// Token: 0x040012E3 RID: 4835
	private GameObject DamagePositions;

	// Token: 0x040012E4 RID: 4836
	public GameObject PlayerDeathEffect;

	// Token: 0x040012E5 RID: 4837
	public GameObject ZombieDeathEffect;

	// Token: 0x040012E6 RID: 4838
	public int dualGunHandIndex;

	// Token: 0x040012E7 RID: 4839
	public int PredualGunHandIndex;

	// Token: 0x040012E8 RID: 4840
	public GameObject multiplayerWeaponManager;
}
