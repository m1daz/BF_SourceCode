using System;

// Token: 0x02000150 RID: 336
public interface IPunTurnManagerCallbacks
{
	// Token: 0x060009D6 RID: 2518
	void OnTurnBegins(int turn);

	// Token: 0x060009D7 RID: 2519
	void OnTurnCompleted(int turn);

	// Token: 0x060009D8 RID: 2520
	void OnPlayerMove(PhotonPlayer player, int turn, object move);

	// Token: 0x060009D9 RID: 2521
	void OnPlayerFinished(PhotonPlayer player, int turn, object move);

	// Token: 0x060009DA RID: 2522
	void OnTurnTimeEnds(int turn);
}
