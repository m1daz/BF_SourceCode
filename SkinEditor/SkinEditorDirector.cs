using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x0200035A RID: 858
	public class SkinEditorDirector : MonoBehaviour
	{
		// Token: 0x06001ADC RID: 6876 RVA: 0x000D8268 File Offset: 0x000D6668
		private void RefreshModuleView(GameObject mObj, SkinModuleType smType)
		{
			MeshRenderer[] componentsInChildren = mObj.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				if (meshRenderer.transform.name.Contains("Top"))
				{
					meshRenderer.material.mainTexture = this.m_EditingTex;
					Rect skinSideRect = SkinDefine.GetSkinSideRect(smType, SkinModuleSideType.Top);
					meshRenderer.material.mainTextureScale = new Vector2(skinSideRect.width / (float)SkinDefine.TexWidth, skinSideRect.height / (float)SkinDefine.TexHeight);
					meshRenderer.material.mainTextureOffset = new Vector2(skinSideRect.x / (float)SkinDefine.TexWidth, skinSideRect.y / (float)SkinDefine.TexHeight);
				}
				else if (meshRenderer.transform.name.Contains("Bottom"))
				{
					meshRenderer.material.mainTexture = this.m_EditingTex;
					Rect skinSideRect = SkinDefine.GetSkinSideRect(smType, SkinModuleSideType.Bottom);
					meshRenderer.material.mainTextureScale = new Vector2(skinSideRect.width / (float)SkinDefine.TexWidth, skinSideRect.height / (float)SkinDefine.TexHeight);
					meshRenderer.material.mainTextureOffset = new Vector2(skinSideRect.x / (float)SkinDefine.TexWidth, skinSideRect.y / (float)SkinDefine.TexHeight);
				}
				else if (meshRenderer.transform.name.Contains("Right"))
				{
					meshRenderer.material.mainTexture = this.m_EditingTex;
					Rect skinSideRect = SkinDefine.GetSkinSideRect(smType, SkinModuleSideType.Right);
					meshRenderer.material.mainTextureScale = new Vector2(skinSideRect.width / (float)SkinDefine.TexWidth, skinSideRect.height / (float)SkinDefine.TexHeight);
					meshRenderer.material.mainTextureOffset = new Vector2(skinSideRect.x / (float)SkinDefine.TexWidth, skinSideRect.y / (float)SkinDefine.TexHeight);
				}
				else if (meshRenderer.transform.name.Contains("Front"))
				{
					meshRenderer.material.mainTexture = this.m_EditingTex;
					Rect skinSideRect = SkinDefine.GetSkinSideRect(smType, SkinModuleSideType.Front);
					meshRenderer.material.mainTextureScale = new Vector2(skinSideRect.width / (float)SkinDefine.TexWidth, skinSideRect.height / (float)SkinDefine.TexHeight);
					meshRenderer.material.mainTextureOffset = new Vector2(skinSideRect.x / (float)SkinDefine.TexWidth, skinSideRect.y / (float)SkinDefine.TexHeight);
				}
				else if (meshRenderer.transform.name.Contains("Left"))
				{
					meshRenderer.material.mainTexture = this.m_EditingTex;
					Rect skinSideRect = SkinDefine.GetSkinSideRect(smType, SkinModuleSideType.Left);
					meshRenderer.material.mainTextureScale = new Vector2(skinSideRect.width / (float)SkinDefine.TexWidth, skinSideRect.height / (float)SkinDefine.TexHeight);
					meshRenderer.material.mainTextureOffset = new Vector2(skinSideRect.x / (float)SkinDefine.TexWidth, skinSideRect.y / (float)SkinDefine.TexHeight);
				}
				else if (meshRenderer.transform.name.Contains("Back"))
				{
					meshRenderer.material.mainTexture = this.m_EditingTex;
					Rect skinSideRect = SkinDefine.GetSkinSideRect(smType, SkinModuleSideType.Back);
					meshRenderer.material.mainTextureScale = new Vector2(skinSideRect.width / (float)SkinDefine.TexWidth, skinSideRect.height / (float)SkinDefine.TexHeight);
					meshRenderer.material.mainTextureOffset = new Vector2(skinSideRect.x / (float)SkinDefine.TexWidth, skinSideRect.y / (float)SkinDefine.TexHeight);
				}
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000D8614 File Offset: 0x000D6A14
		private void SwitchViewLevel(ViewLevelType fromLevel, ViewLevelType toLevel, SkinModuleType appendModule, SkinModuleSideType appendSide)
		{
			if (fromLevel == ViewLevelType.Level_1_Overview && toLevel == ViewLevelType.Level_2_Module)
			{
				this.isNeedUIRefresh = true;
				this.m_ViewLevel_1_Overview.SetActive(false);
				switch (appendModule)
				{
				case SkinModuleType.Head:
					this.m_ViewLevel_2_Head.SetActive(true);
					this.m_View_Level_Head_Label.SetActive(true);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Head_Camera;
					this.RefreshModuleView(this.m_ViewLevel_2_Head, SkinModuleType.Head);
					break;
				case SkinModuleType.Body:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(true);
					this.m_View_Level_Body_Label.SetActive(true);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Body_Camera;
					this.RefreshModuleView(this.m_ViewLevel_2_Body, SkinModuleType.Body);
					break;
				case SkinModuleType.Leg:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(true);
					this.m_View_Level_Arm_Leg_Label.SetActive(true);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Leg_Camera;
					this.RefreshModuleView(this.m_ViewLevel_2_Leg, SkinModuleType.Leg);
					break;
				case SkinModuleType.Arm:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(true);
					this.m_View_Level_Arm_Leg_Label.SetActive(true);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Arm_Camera;
					this.RefreshModuleView(this.m_ViewLevel_2_Arm, SkinModuleType.Arm);
					break;
				case SkinModuleType.Foot:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(true);
					this.m_View_Level_Foot_Label.SetActive(true);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Foot_Camera;
					this.RefreshModuleView(this.m_ViewLevel_2_Foot, SkinModuleType.Foot);
					break;
				}
				this.m_CurModule = appendModule;
			}
			else if (fromLevel == ViewLevelType.Level_2_Module && toLevel == ViewLevelType.Level_3_Panel)
			{
				this.isNeedUIRefresh = true;
				this.m_CurModuleSide = appendSide;
				this.m_ViewLevel_2_Head.SetActive(false);
				this.m_ViewLevel_2_Body.SetActive(false);
				this.m_ViewLevel_2_Leg.SetActive(false);
				this.m_ViewLevel_2_Arm.SetActive(false);
				this.m_ViewLevel_2_Foot.SetActive(false);
				this.m_View_Level_Head_Label.SetActive(false);
				this.m_View_Level_Body_Label.SetActive(false);
				this.m_View_Level_Arm_Leg_Label.SetActive(false);
				this.m_View_Level_Foot_Label.SetActive(false);
				this.m_ViewLevel_3_Panel.SetActive(true);
				this.m_CurActivedCamera = this.m_ViewLevel_3_Panel_Camera;
			}
			else if (fromLevel == ViewLevelType.Level_3_Panel && toLevel == ViewLevelType.Level_2_Module)
			{
				this.isNeedUIRefresh = true;
				switch (appendModule)
				{
				case SkinModuleType.Head:
					this.m_ViewLevel_2_Head.SetActive(true);
					this.m_View_Level_Head_Label.SetActive(true);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Head_Camera;
					this.curSelectedTitle = "Head";
					break;
				case SkinModuleType.Body:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(true);
					this.m_View_Level_Body_Label.SetActive(true);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Body_Camera;
					this.curSelectedTitle = "Body";
					break;
				case SkinModuleType.Leg:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(true);
					this.m_View_Level_Arm_Leg_Label.SetActive(true);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Leg_Camera;
					this.curSelectedTitle = "Leg";
					break;
				case SkinModuleType.Arm:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(true);
					this.m_View_Level_Arm_Leg_Label.SetActive(true);
					this.m_ViewLevel_2_Foot.SetActive(false);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Arm_Camera;
					this.curSelectedTitle = "Arm";
					break;
				case SkinModuleType.Foot:
					this.m_ViewLevel_2_Head.SetActive(false);
					this.m_ViewLevel_2_Body.SetActive(false);
					this.m_ViewLevel_2_Leg.SetActive(false);
					this.m_ViewLevel_2_Arm.SetActive(false);
					this.m_ViewLevel_2_Foot.SetActive(true);
					this.m_View_Level_Foot_Label.SetActive(true);
					this.m_CurActivedCamera = this.m_ViewLevel_2_Foot_Camera;
					this.curSelectedTitle = "Foot";
					break;
				}
				this.m_ViewLevel_3_Panel.SetActive(false);
				this.m_CurModule = appendModule;
				this.m_CurModuleSide = SkinModuleSideType.Nil;
			}
			else if (fromLevel == ViewLevelType.Level_2_Module && toLevel == ViewLevelType.Level_1_Overview)
			{
				this.isNeedUIRefresh = true;
				this.m_ViewLevel_2_Head.SetActive(false);
				this.m_ViewLevel_2_Body.SetActive(false);
				this.m_ViewLevel_2_Leg.SetActive(false);
				this.m_ViewLevel_2_Arm.SetActive(false);
				this.m_ViewLevel_2_Foot.SetActive(false);
				this.m_View_Level_Head_Label.SetActive(false);
				this.m_View_Level_Body_Label.SetActive(false);
				this.m_View_Level_Arm_Leg_Label.SetActive(false);
				this.m_View_Level_Foot_Label.SetActive(false);
				this.m_ViewLevel_3_Panel.SetActive(false);
				this.m_ViewLevel_1_Overview.SetActive(true);
				this.m_CurModule = SkinModuleType.Nil;
				this.m_CurModuleSide = SkinModuleSideType.Nil;
				this.m_CurActivedCamera = this.m_ViewLevel_1_Overview_Camera;
				this.curSelectedTitle = "Overview";
			}
			this.m_CurViewLevel = toLevel;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x000D8C3C File Offset: 0x000D703C
		public void UserEventChk()
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && this.m_CurViewLevel != ViewLevelType.Level_3_Panel)
			{
				Ray ray = this.m_CurActivedCamera.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 500f))
				{
					this.m_SelectedBox.transform.position = raycastHit.transform.position;
					this.m_SelectedBox.transform.rotation = raycastHit.transform.rotation;
					this.m_SelectedBox.transform.localScale = raycastHit.transform.localScale;
				}
			}
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				this.m_SelectedBox.transform.position = new Vector3(0f, 1000f, 0f);
				if (this.m_CurViewLevel == ViewLevelType.Level_1_Overview)
				{
					Ray ray2 = this.m_CurActivedCamera.ScreenPointToRay(Input.GetTouch(0).position);
					RaycastHit raycastHit2;
					if (Physics.Raycast(ray2, out raycastHit2, 500f))
					{
						string name = raycastHit2.transform.name;
						switch (name)
						{
						case "Head":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Head, SkinModuleSideType.Nil);
							break;
						case "Body":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Body, SkinModuleSideType.Nil);
							break;
						case "Leg_R":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Leg, SkinModuleSideType.Nil);
							break;
						case "Leg_L":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Leg, SkinModuleSideType.Nil);
							break;
						case "Arm_R":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Arm, SkinModuleSideType.Nil);
							break;
						case "Arm_L":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Arm, SkinModuleSideType.Nil);
							break;
						case "Foot_R":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Foot, SkinModuleSideType.Nil);
							break;
						case "Foot_L":
							this.SwitchViewLevel(ViewLevelType.Level_1_Overview, ViewLevelType.Level_2_Module, SkinModuleType.Foot, SkinModuleSideType.Nil);
							break;
						}
						string[] array = raycastHit2.transform.name.Split(new char[]
						{
							'_'
						});
						this.curSelectedTitle = array[0];
					}
				}
				else if (this.m_CurViewLevel == ViewLevelType.Level_2_Module)
				{
					Ray ray3 = this.m_CurActivedCamera.ScreenPointToRay(Input.GetTouch(0).position);
					RaycastHit raycastHit3;
					if (Physics.Raycast(ray3, out raycastHit3, 100f))
					{
						if (raycastHit3.transform.name.Contains("Top"))
						{
							this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_3_Panel, this.m_CurModule, SkinModuleSideType.Top);
						}
						else if (raycastHit3.transform.name.Contains("Bottom"))
						{
							this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_3_Panel, this.m_CurModule, SkinModuleSideType.Bottom);
						}
						else if (raycastHit3.transform.name.Contains("Right"))
						{
							this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_3_Panel, this.m_CurModule, SkinModuleSideType.Right);
						}
						else if (raycastHit3.transform.name.Contains("Front"))
						{
							this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_3_Panel, this.m_CurModule, SkinModuleSideType.Front);
						}
						else if (raycastHit3.transform.name.Contains("Left"))
						{
							this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_3_Panel, this.m_CurModule, SkinModuleSideType.Left);
						}
						else if (raycastHit3.transform.name.Contains("Back"))
						{
							this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_3_Panel, this.m_CurModule, SkinModuleSideType.Back);
						}
						this.curSelectedTitle = raycastHit3.transform.name;
					}
				}
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000D9090 File Offset: 0x000D7490
		private void Awake()
		{
			SkinEditorDirector.mInstance = this;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000D9098 File Offset: 0x000D7498
		private void OnDestroy()
		{
			if (GameObject.Find("DecorationManagerInMenu") != null)
			{
				UnityEngine.Object.DestroyImmediate(GameObject.Find("DecorationManagerInMenu"));
			}
			SkinEditorDirector.mInstance = null;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000D90C4 File Offset: 0x000D74C4
		private void Start()
		{
			this.InitFromConfig(SkinManager.mInstance.editorCfg);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000D90D6 File Offset: 0x000D74D6
		private void Update()
		{
			this.UserEventChk();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000D90E0 File Offset: 0x000D74E0
		private void InitFromConfig(SkinManager.EditorLogicCfg cfg)
		{
			if (cfg.isNewSkin)
			{
				this.curTexFileName = SkinIOController.CreateSkin();
				base.StartCoroutine(this.LoadTexture(SkinIOController.GetCustomSkinFolderPath() + this.curTexFileName));
			}
			else
			{
				this.curTexFileName = cfg.skinNameToEdit + ".png";
				base.StartCoroutine(this.LoadTexture(SkinIOController.GetCustomSkinFolderPath() + this.curTexFileName));
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000D915A File Offset: 0x000D755A
		private void RefreshModelTexture()
		{
			this.characterModel.GetComponent<SkinnedMeshRenderer>().material.mainTexture = this.m_EditingTex;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000D9178 File Offset: 0x000D7578
		private IEnumerator LoadTexture(string path)
		{
			WWW www = new WWW("file://" + path);
			yield return www;
			this.m_EditingTex = www.texture;
			this.m_EditingTex.filterMode = FilterMode.Point;
			this.RefreshModelTexture();
			yield break;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000D919C File Offset: 0x000D759C
		public void Back()
		{
			switch (this.m_CurViewLevel)
			{
			case ViewLevelType.Level_1_Overview:
				if (this.saveCount == 0 && SkinManager.mInstance.editorCfg.isNewSkin)
				{
					SkinIOController.DeleteSkin(this.curTexFileName);
				}
				if (!this.isUploaded)
				{
					GGCloudServiceAdapter.mInstance.mUploadService.RemoveFileByUser(UIUserDataController.GetDefaultRoleName() + "@Private@" + this.curTexFileName, UIUserDataController.GetDefaultUserName(), new SkinRemoveCallBack(this.curTexFileName, true));
					Application.LoadLevel("UINewStore");
				}
				else
				{
					Application.LoadLevel("UINewStore");
				}
				break;
			case ViewLevelType.Level_2_Module:
				this.SwitchViewLevel(ViewLevelType.Level_2_Module, ViewLevelType.Level_1_Overview, SkinModuleType.Nil, SkinModuleSideType.Nil);
				break;
			case ViewLevelType.Level_3_Panel:
				this.SwitchViewLevel(ViewLevelType.Level_3_Panel, ViewLevelType.Level_2_Module, this.m_CurModule, SkinModuleSideType.Nil);
				break;
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000D9288 File Offset: 0x000D7688
		public void Save()
		{
			SkinIOController.SaveSkin(this.curTexFileName, this.m_EditingTex.EncodeToPNG());
			GrowthManagerKit.SetSkinSharedMark(this.curTexFileName.Remove(this.curTexFileName.Length - 4), false);
			this.isSaved = true;
			this.isUploaded = false;
			this.saveCount++;
		}

		// Token: 0x04001D24 RID: 7460
		public static SkinEditorDirector mInstance;

		// Token: 0x04001D25 RID: 7461
		public GameObject m_ViewLevel_1_Overview;

		// Token: 0x04001D26 RID: 7462
		public GameObject m_ViewLevel_2_Head;

		// Token: 0x04001D27 RID: 7463
		public GameObject m_ViewLevel_2_Body;

		// Token: 0x04001D28 RID: 7464
		public GameObject m_ViewLevel_2_Leg;

		// Token: 0x04001D29 RID: 7465
		public GameObject m_ViewLevel_2_Arm;

		// Token: 0x04001D2A RID: 7466
		public GameObject m_ViewLevel_2_Foot;

		// Token: 0x04001D2B RID: 7467
		public GameObject m_ViewLevel_3_Panel;

		// Token: 0x04001D2C RID: 7468
		public Camera m_CurActivedCamera;

		// Token: 0x04001D2D RID: 7469
		public Camera m_ViewLevel_1_Overview_Camera;

		// Token: 0x04001D2E RID: 7470
		public Camera m_ViewLevel_2_Head_Camera;

		// Token: 0x04001D2F RID: 7471
		public Camera m_ViewLevel_2_Body_Camera;

		// Token: 0x04001D30 RID: 7472
		public Camera m_ViewLevel_2_Leg_Camera;

		// Token: 0x04001D31 RID: 7473
		public Camera m_ViewLevel_2_Arm_Camera;

		// Token: 0x04001D32 RID: 7474
		public Camera m_ViewLevel_2_Foot_Camera;

		// Token: 0x04001D33 RID: 7475
		public Camera m_ViewLevel_3_Panel_Camera;

		// Token: 0x04001D34 RID: 7476
		public GameObject m_View_Level_Body_Label;

		// Token: 0x04001D35 RID: 7477
		public GameObject m_View_Level_Head_Label;

		// Token: 0x04001D36 RID: 7478
		public GameObject m_View_Level_Arm_Leg_Label;

		// Token: 0x04001D37 RID: 7479
		public GameObject m_View_Level_Foot_Label;

		// Token: 0x04001D38 RID: 7480
		public SkinDrawingPanel m_SkinDrawer;

		// Token: 0x04001D39 RID: 7481
		public Transform m_SelectedBox;

		// Token: 0x04001D3A RID: 7482
		public ViewLevelType m_CurViewLevel = ViewLevelType.Level_1_Overview;

		// Token: 0x04001D3B RID: 7483
		public SkinModuleType m_CurModule = SkinModuleType.Nil;

		// Token: 0x04001D3C RID: 7484
		public SkinModuleSideType m_CurModuleSide = SkinModuleSideType.Nil;

		// Token: 0x04001D3D RID: 7485
		public Texture2D m_EditingTex;

		// Token: 0x04001D3E RID: 7486
		public List<Color> m_ColorHistroy = new List<Color>();

		// Token: 0x04001D3F RID: 7487
		private string curTexFileName;

		// Token: 0x04001D40 RID: 7488
		public GameObject characterModel;

		// Token: 0x04001D41 RID: 7489
		public int saveCount;

		// Token: 0x04001D42 RID: 7490
		public bool isSaved = true;

		// Token: 0x04001D43 RID: 7491
		public bool isUploaded = true;

		// Token: 0x04001D44 RID: 7492
		public bool isNeedUIRefresh;

		// Token: 0x04001D45 RID: 7493
		public string curSelectedTitle = "Overview";
	}
}
