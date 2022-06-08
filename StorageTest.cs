using System;
using System.Collections.Generic;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public class StorageTest : MonoBehaviour
{
	// Token: 0x060022AD RID: 8877 RVA: 0x001021E1 File Offset: 0x001005E1
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x060022AE RID: 8878 RVA: 0x00102204 File Offset: 0x00100604
	private void Update()
	{
	}

	// Token: 0x060022AF RID: 8879 RVA: 0x00102208 File Offset: 0x00100608
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1000f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Insert JsonDoc"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.InsertJSONDocument("fffgdg", "fffgdg", this.cons.json, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "Find AllDocuments"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindAllDocuments(this.cons.dbName, this.collectionName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "Find AllCollections"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindAllCollections(this.cons.dbName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "Find DocumentById"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindDocumentById(this.cons.dbName, this.collectionName, this.cons.docId, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "Find DocumentByKeyValue"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindDocumentByKeyValue(this.cons.dbName, this.collectionName, this.cons.key, this.cons.val, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "UpdateDocumentByKeyValue"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.UpdateDocumentByKeyValue(this.cons.dbName, this.collectionName, this.cons.key, this.cons.val, this.cons.newJson, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "UpdateDocumentById"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.UpdateDocumentByDocId(this.cons.dbName, this.collectionName, this.cons.docId, this.cons.newJson, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "Delete DocumentById"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.DeleteDocumentById(this.cons.dbName, this.collectionName, this.cons.docId, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "Delete DocumentsByKeyValue"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.DeleteDocumentsByKeyValue(this.cons.dbName, this.collectionName, this.cons.key, this.cons.val, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "DeleteAllDocuments"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.DeleteAllDocuments(this.cons.dbName, this.collectionName, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "FindDocumentByKeyValue"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindDocumentByKeyValue(this.cons.dbName, this.collectionName, this.cons.key, this.cons.val, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "FindAllDocumentsCount"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindAllDocumentsCount(this.cons.dbName, this.collectionName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 300f, 200f, 30f), "FindAllDocumentsByPaging"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.FindAllDocuments(this.cons.dbName, this.collectionName, this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 300f, 200f, 30f), "InsertJsonWithGeoTag"))
		{
			this.storageService = this.sp.BuildStorageService();
			GeoTag geoTag = new GeoTag();
			geoTag.SetLat(-73.99171);
			geoTag.SetLng(40.738868);
			this.storageService.SetGeoTag(geoTag);
			this.storageService.InsertJSONDocument(this.cons.dbName, this.collectionName, this.cons.json, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 300f, 200f, 30f), "FindDocumentsByLocation"))
		{
			this.storageService = this.sp.BuildStorageService();
			GeoTag geoTag2 = new GeoTag();
			geoTag2.SetLat(-73.99171);
			geoTag2.SetLng(40.738868);
			GeoQuery query = QueryBuilder.BuildGeoQuery(geoTag2, GeoOperator.NEAR, 100.0);
			this.storageService.FindDocumentsByLocation(this.cons.dbName, this.collectionName, query, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 350f, 200f, 30f), "FindAllDocumentsSelectKeys"))
		{
			this.storageService = this.sp.BuildStorageService();
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("Name");
			this.storageService.SetSelectKeys(hashSet);
			this.storageService.FindAllDocuments(this.cons.dbName, this.collectionName, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 350f, 200f, 30f), "FindDocumentsByQuery"))
		{
			this.storageService = this.sp.BuildStorageService();
			Query q = QueryBuilder.Build("AppName", "de", Operator.LIKE);
			Query q2 = QueryBuilder.Build("AppId", "123hg4bdb", Operator.LIKE);
			Query query2 = QueryBuilder.CompoundOperator(q, Operator.OR, q2);
			this.storageService.FindDocumentsByQuery(this.cons.dbName, this.collectionName, query2, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 350f, 200f, 30f), "SaveOrUpdateDocument"))
		{
			this.storageService = this.sp.BuildStorageService();
			this.storageService.SaveOrUpdateDocumentByKeyValue(this.cons.dbName, this.collectionName, this.cons.key, this.cons.val, this.cons.newJson, this.callBack);
		}
	}

	// Token: 0x04002370 RID: 9072
	private ServiceAPI sp;

	// Token: 0x04002371 RID: 9073
	private StorageService storageService;

	// Token: 0x04002372 RID: 9074
	private Constant cons = new Constant();

	// Token: 0x04002373 RID: 9075
	public string collectionName = "379368233";

	// Token: 0x04002374 RID: 9076
	public int max = 2;

	// Token: 0x04002375 RID: 9077
	public int offSet = 1;

	// Token: 0x04002376 RID: 9078
	private StorageResponse callBack = new StorageResponse();

	// Token: 0x04002377 RID: 9079
	public string success;

	// Token: 0x04002378 RID: 9080
	public string box;
}
