using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200053F RID: 1343
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Item Database")]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x060025EA RID: 9706 RVA: 0x00119973 File Offset: 0x00117D73
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x00119994 File Offset: 0x00117D94
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x0011999C File Offset: 0x00117D9C
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x001199A4 File Offset: 0x00117DA4
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x001199EC File Offset: 0x00117DEC
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x00119A2C File Offset: 0x00117E2C
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		return (!(database != null)) ? null : database.GetItem(id32 & 65535);
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x00119A64 File Offset: 0x00117E64
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x00119AD8 File Offset: 0x00117ED8
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return invDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0400269A RID: 9882
	private static InvDatabase[] mList;

	// Token: 0x0400269B RID: 9883
	private static bool mIsDirty = true;

	// Token: 0x0400269C RID: 9884
	public int databaseID;

	// Token: 0x0400269D RID: 9885
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x0400269E RID: 9886
	public UIAtlas iconAtlas;
}
