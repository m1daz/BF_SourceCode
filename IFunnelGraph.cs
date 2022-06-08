using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200009B RID: 155
public interface IFunnelGraph
{
	// Token: 0x060004F0 RID: 1264
	void BuildFunnelCorridor(List<Node> path, int sIndex, int eIndex, List<Vector3> left, List<Vector3> right);

	// Token: 0x060004F1 RID: 1265
	void AddPortal(Node n1, Node n2, List<Vector3> left, List<Vector3> right);
}
