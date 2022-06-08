using System;

// Token: 0x02000500 RID: 1280
public enum GGMessageType
{
	// Token: 0x040024CC RID: 9420
	MessageModeResult,
	// Token: 0x040024CD RID: 9421
	MessageNewRound,
	// Token: 0x040024CE RID: 9422
	MessageStrondhold,
	// Token: 0x040024CF RID: 9423
	MessageACKModeResult = 50,
	// Token: 0x040024D0 RID: 9424
	MessageACKActiveNewRound,
	// Token: 0x040024D1 RID: 9425
	MessageKillOneEnemy = 100,
	// Token: 0x040024D2 RID: 9426
	MessageKillingCompetitionTeamKillerIncrease,
	// Token: 0x040024D3 RID: 9427
	MessageStrondholdOccupy,
	// Token: 0x040024D4 RID: 9428
	MessagePlayerConnected,
	// Token: 0x040024D5 RID: 9429
	MessageNotifySkin,
	// Token: 0x040024D6 RID: 9430
	MessageNotifyHat,
	// Token: 0x040024D7 RID: 9431
	MessageNotifyCape,
	// Token: 0x040024D8 RID: 9432
	MessageNotifyWeaponProperties,
	// Token: 0x040024D9 RID: 9433
	MessageNotifyelevator,
	// Token: 0x040024DA RID: 9434
	MessageElevatorReceive,
	// Token: 0x040024DB RID: 9435
	MessageNotifyAllNecessaryData,
	// Token: 0x040024DC RID: 9436
	MessageTimerBombInstall,
	// Token: 0x040024DD RID: 9437
	MessageTimerBombRemove,
	// Token: 0x040024DE RID: 9438
	MessageMutationModeKillOneEnemy,
	// Token: 0x040024DF RID: 9439
	MessagePlayerStartMutation,
	// Token: 0x040024E0 RID: 9440
	MessageMutationModePlayerCanAttack,
	// Token: 0x040024E1 RID: 9441
	MessageMutationModePlayerTranslate,
	// Token: 0x040024E2 RID: 9442
	MessageNotifyBoot,
	// Token: 0x040024E3 RID: 9443
	MessageNotifyMutationTrap,
	// Token: 0x040024E4 RID: 9444
	MessageNotifyMutationHorror,
	// Token: 0x040024E5 RID: 9445
	MessageNotifyMutationBlind,
	// Token: 0x040024E6 RID: 9446
	MessageNotifyHolidayFireworks,
	// Token: 0x040024E7 RID: 9447
	MessageGodLike,
	// Token: 0x040024E8 RID: 9448
	MessageStrongholdTeamKillerIncrease,
	// Token: 0x040024E9 RID: 9449
	MessagePlayerActiveTimerBomb,
	// Token: 0x040024EA RID: 9450
	MessageExplosionModeNextRoundStart,
	// Token: 0x040024EB RID: 9451
	MessageTimerBombTake,
	// Token: 0x040024EC RID: 9452
	MessageTimerBombActiveInScene,
	// Token: 0x040024ED RID: 9453
	SendTimerBombSynPositionMessage,
	// Token: 0x040024EE RID: 9454
	MessageExplosionModeSingleRoundWin,
	// Token: 0x040024EF RID: 9455
	MessageTimerBombDrop,
	// Token: 0x040024F0 RID: 9456
	MessagePlayerSeasonInfo = 140,
	// Token: 0x040024F1 RID: 9457
	MessageKnifeCompetitionTeamKillerIncrease,
	// Token: 0x040024F2 RID: 9458
	MessageHuntingModeNextRoundStart,
	// Token: 0x040024F3 RID: 9459
	MessageHuntingModeSingleRoundWin,
	// Token: 0x040024F4 RID: 9460
	MessageHuntingModeSingleRoundLose,
	// Token: 0x040024F5 RID: 9461
	MessageHuntingModeBossKilled,
	// Token: 0x040024F6 RID: 9462
	MessagePlayerAutoMove,
	// Token: 0x040024F7 RID: 9463
	MessagePlayerSpeedSlow,
	// Token: 0x040024F8 RID: 9464
	MessagePlayerAcidRain,
	// Token: 0x040024F9 RID: 9465
	MessageHuntingModePlayerMoveOut,
	// Token: 0x040024FA RID: 9466
	MessageHuntingModeEarthShake,
	// Token: 0x040024FB RID: 9467
	MessageNotifyNightmare,
	// Token: 0x040024FC RID: 9468
	MessageNull = 255
}
