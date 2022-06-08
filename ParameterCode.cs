using System;

// Token: 0x020000F3 RID: 243
public class ParameterCode
{
	// Token: 0x04000651 RID: 1617
	public const byte SuppressRoomEvents = 237;

	// Token: 0x04000652 RID: 1618
	public const byte EmptyRoomTTL = 236;

	// Token: 0x04000653 RID: 1619
	public const byte PlayerTTL = 235;

	// Token: 0x04000654 RID: 1620
	public const byte EventForward = 234;

	// Token: 0x04000655 RID: 1621
	[Obsolete("Use: IsInactive")]
	public const byte IsComingBack = 233;

	// Token: 0x04000656 RID: 1622
	public const byte IsInactive = 233;

	// Token: 0x04000657 RID: 1623
	public const byte CheckUserOnJoin = 232;

	// Token: 0x04000658 RID: 1624
	public const byte ExpectedValues = 231;

	// Token: 0x04000659 RID: 1625
	public const byte Address = 230;

	// Token: 0x0400065A RID: 1626
	public const byte PeerCount = 229;

	// Token: 0x0400065B RID: 1627
	public const byte GameCount = 228;

	// Token: 0x0400065C RID: 1628
	public const byte MasterPeerCount = 227;

	// Token: 0x0400065D RID: 1629
	public const byte UserId = 225;

	// Token: 0x0400065E RID: 1630
	public const byte ApplicationId = 224;

	// Token: 0x0400065F RID: 1631
	public const byte Position = 223;

	// Token: 0x04000660 RID: 1632
	public const byte MatchMakingType = 223;

	// Token: 0x04000661 RID: 1633
	public const byte GameList = 222;

	// Token: 0x04000662 RID: 1634
	public const byte Secret = 221;

	// Token: 0x04000663 RID: 1635
	public const byte AppVersion = 220;

	// Token: 0x04000664 RID: 1636
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureNodeInfo = 210;

	// Token: 0x04000665 RID: 1637
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureLocalNodeId = 209;

	// Token: 0x04000666 RID: 1638
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureMasterNodeId = 208;

	// Token: 0x04000667 RID: 1639
	public const byte RoomName = 255;

	// Token: 0x04000668 RID: 1640
	public const byte Broadcast = 250;

	// Token: 0x04000669 RID: 1641
	public const byte ActorList = 252;

	// Token: 0x0400066A RID: 1642
	public const byte ActorNr = 254;

	// Token: 0x0400066B RID: 1643
	public const byte PlayerProperties = 249;

	// Token: 0x0400066C RID: 1644
	public const byte CustomEventContent = 245;

	// Token: 0x0400066D RID: 1645
	public const byte Data = 245;

	// Token: 0x0400066E RID: 1646
	public const byte Code = 244;

	// Token: 0x0400066F RID: 1647
	public const byte GameProperties = 248;

	// Token: 0x04000670 RID: 1648
	public const byte Properties = 251;

	// Token: 0x04000671 RID: 1649
	public const byte TargetActorNr = 253;

	// Token: 0x04000672 RID: 1650
	public const byte ReceiverGroup = 246;

	// Token: 0x04000673 RID: 1651
	public const byte Cache = 247;

	// Token: 0x04000674 RID: 1652
	public const byte CleanupCacheOnLeave = 241;

	// Token: 0x04000675 RID: 1653
	public const byte Group = 240;

	// Token: 0x04000676 RID: 1654
	public const byte Remove = 239;

	// Token: 0x04000677 RID: 1655
	public const byte PublishUserId = 239;

	// Token: 0x04000678 RID: 1656
	public const byte Add = 238;

	// Token: 0x04000679 RID: 1657
	public const byte Info = 218;

	// Token: 0x0400067A RID: 1658
	public const byte ClientAuthenticationType = 217;

	// Token: 0x0400067B RID: 1659
	public const byte ClientAuthenticationParams = 216;

	// Token: 0x0400067C RID: 1660
	public const byte JoinMode = 215;

	// Token: 0x0400067D RID: 1661
	public const byte ClientAuthenticationData = 214;

	// Token: 0x0400067E RID: 1662
	public const byte MasterClientId = 203;

	// Token: 0x0400067F RID: 1663
	public const byte FindFriendsRequestList = 1;

	// Token: 0x04000680 RID: 1664
	public const byte FindFriendsResponseOnlineList = 1;

	// Token: 0x04000681 RID: 1665
	public const byte FindFriendsResponseRoomIdList = 2;

	// Token: 0x04000682 RID: 1666
	public const byte LobbyName = 213;

	// Token: 0x04000683 RID: 1667
	public const byte LobbyType = 212;

	// Token: 0x04000684 RID: 1668
	public const byte LobbyStats = 211;

	// Token: 0x04000685 RID: 1669
	public const byte Region = 210;

	// Token: 0x04000686 RID: 1670
	public const byte UriPath = 209;

	// Token: 0x04000687 RID: 1671
	public const byte WebRpcParameters = 208;

	// Token: 0x04000688 RID: 1672
	public const byte WebRpcReturnCode = 207;

	// Token: 0x04000689 RID: 1673
	public const byte WebRpcReturnMessage = 206;

	// Token: 0x0400068A RID: 1674
	public const byte CacheSliceIndex = 205;

	// Token: 0x0400068B RID: 1675
	public const byte Plugins = 204;

	// Token: 0x0400068C RID: 1676
	public const byte NickName = 202;

	// Token: 0x0400068D RID: 1677
	public const byte PluginName = 201;

	// Token: 0x0400068E RID: 1678
	public const byte PluginVersion = 200;

	// Token: 0x0400068F RID: 1679
	public const byte ExpectedProtocol = 195;

	// Token: 0x04000690 RID: 1680
	public const byte CustomInitData = 194;

	// Token: 0x04000691 RID: 1681
	public const byte EncryptionMode = 193;

	// Token: 0x04000692 RID: 1682
	public const byte EncryptionData = 192;
}
