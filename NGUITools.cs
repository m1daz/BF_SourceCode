using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020005BB RID: 1467
public static class NGUITools
{
	// Token: 0x17000245 RID: 581
	// (get) Token: 0x0600294D RID: 10573 RVA: 0x00133268 File Offset: 0x00131668
	// (set) Token: 0x0600294E RID: 10574 RVA: 0x00133293 File Offset: 0x00131693
	public static float soundVolume
	{
		get
		{
			if (!NGUITools.mLoaded)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return NGUITools.mGlobalVolume;
		}
		set
		{
			if (NGUITools.mGlobalVolume != value)
			{
				NGUITools.mLoaded = true;
				NGUITools.mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x0600294F RID: 10575 RVA: 0x001332B7 File Offset: 0x001316B7
	public static bool fileAccess
	{
		get
		{
			return Application.platform != RuntimePlatform.WebGLPlayer;
		}
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x001332C8 File Offset: 0x001316C8
	public static AudioSource PlaySound(AudioClip clip)
	{
		return NGUITools.PlaySound(clip, 1f, 1f);
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x001332DA File Offset: 0x001316DA
	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return NGUITools.PlaySound(clip, volume, 1f);
	}

	// Token: 0x06002952 RID: 10578 RVA: 0x001332E8 File Offset: 0x001316E8
	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		float time = RealTime.time;
		if (NGUITools.mLastClip == clip && NGUITools.mLastTimestamp + 0.1f > time)
		{
			return null;
		}
		NGUITools.mLastClip = clip;
		NGUITools.mLastTimestamp = time;
		volume *= NGUITools.soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (NGUITools.mListener == null || !NGUITools.GetActive(NGUITools.mListener))
			{
				AudioListener[] array = UnityEngine.Object.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (NGUITools.GetActive(array[i]))
						{
							NGUITools.mListener = array[i];
							break;
						}
					}
				}
				if (NGUITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = (UnityEngine.Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (camera != null)
					{
						NGUITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (NGUITools.mListener != null && NGUITools.mListener.enabled && NGUITools.GetActive(NGUITools.mListener.gameObject))
			{
				if (!NGUITools.audioSource)
				{
					NGUITools.audioSource = NGUITools.mListener.GetComponent<AudioSource>();
					if (NGUITools.audioSource == null)
					{
						NGUITools.audioSource = NGUITools.mListener.gameObject.AddComponent<AudioSource>();
					}
				}
				NGUITools.audioSource.priority = 50;
				NGUITools.audioSource.pitch = pitch;
				NGUITools.audioSource.PlayOneShot(clip, volume);
				return NGUITools.audioSource;
			}
		}
		return null;
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x001334A4 File Offset: 0x001318A4
	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return UnityEngine.Random.Range(min, max + 1);
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x001334B8 File Offset: 0x001318B8
	public static string GetHierarchy(GameObject obj)
	{
		if (obj == null)
		{
			return string.Empty;
		}
		string text = obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			text = obj.name + "\\" + text;
		}
		return text;
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x0013351E File Offset: 0x0013191E
	public static T[] FindActive<T>() where T : Component
	{
		return UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
	}

	// Token: 0x06002956 RID: 10582 RVA: 0x00133534 File Offset: 0x00131934
	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera camera;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			camera = UICamera.list.buffer[i].cachedCamera;
			if (camera && (camera.cullingMask & num) != 0)
			{
				return camera;
			}
		}
		camera = Camera.main;
		if (camera && (camera.cullingMask & num) != 0)
		{
			return camera;
		}
		Camera[] array = new Camera[Camera.allCamerasCount];
		int allCameras = Camera.GetAllCameras(array);
		for (int j = 0; j < allCameras; j++)
		{
			camera = array[j];
			if (camera && camera.enabled && (camera.cullingMask & num) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x00133604 File Offset: 0x00131A04
	public static void AddWidgetCollider(GameObject go)
	{
		NGUITools.AddWidgetCollider(go, false);
	}

	// Token: 0x06002958 RID: 10584 RVA: 0x00133610 File Offset: 0x00131A10
	public static void AddWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			Collider component = go.GetComponent<Collider>();
			BoxCollider boxCollider = component as BoxCollider;
			if (boxCollider != null)
			{
				NGUITools.UpdateWidgetCollider(boxCollider, considerInactive);
				return;
			}
			if (component != null)
			{
				return;
			}
			BoxCollider2D boxCollider2D = go.GetComponent<BoxCollider2D>();
			if (boxCollider2D != null)
			{
				NGUITools.UpdateWidgetCollider(boxCollider2D, considerInactive);
				return;
			}
			UICamera uicamera = UICamera.FindCameraForLayer(go.layer);
			if (uicamera != null && (uicamera.eventType == UICamera.EventType.World_2D || uicamera.eventType == UICamera.EventType.UI_2D))
			{
				boxCollider2D = go.AddComponent<BoxCollider2D>();
				boxCollider2D.isTrigger = true;
				UIWidget component2 = go.GetComponent<UIWidget>();
				if (component2 != null)
				{
					component2.autoResizeBoxCollider = true;
				}
				NGUITools.UpdateWidgetCollider(boxCollider2D, considerInactive);
				return;
			}
			boxCollider = go.AddComponent<BoxCollider>();
			boxCollider.isTrigger = true;
			UIWidget component3 = go.GetComponent<UIWidget>();
			if (component3 != null)
			{
				component3.autoResizeBoxCollider = true;
			}
			NGUITools.UpdateWidgetCollider(boxCollider, considerInactive);
		}
	}

	// Token: 0x06002959 RID: 10585 RVA: 0x00133708 File Offset: 0x00131B08
	public static void UpdateWidgetCollider(GameObject go)
	{
		NGUITools.UpdateWidgetCollider(go, false);
	}

	// Token: 0x0600295A RID: 10586 RVA: 0x00133714 File Offset: 0x00131B14
	public static void UpdateWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			BoxCollider component = go.GetComponent<BoxCollider>();
			if (component != null)
			{
				NGUITools.UpdateWidgetCollider(component, considerInactive);
				return;
			}
			BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
			if (component2 != null)
			{
				NGUITools.UpdateWidgetCollider(component2, considerInactive);
			}
		}
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x00133764 File Offset: 0x00131B64
	public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive)
	{
		if (box != null)
		{
			GameObject gameObject = box.gameObject;
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector4 drawRegion = component.drawRegion;
				if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
				{
					Vector4 drawingDimensions = component.drawingDimensions;
					box.center = new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f);
					box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
				}
				else
				{
					Vector3[] localCorners = component.localCorners;
					box.center = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
					box.size = localCorners[2] - localCorners[0];
				}
			}
			else
			{
				Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
				box.center = bounds.center;
				box.size = new Vector3(bounds.size.x, bounds.size.y, 0f);
			}
		}
	}

	// Token: 0x0600295C RID: 10588 RVA: 0x001338F4 File Offset: 0x00131CF4
	public static void UpdateWidgetCollider(UIWidget w)
	{
		if (w == null)
		{
			return;
		}
		BoxCollider component = w.GetComponent<BoxCollider>();
		if (component != null)
		{
			NGUITools.UpdateWidgetCollider(w, component);
		}
		else
		{
			NGUITools.UpdateWidgetCollider(w, w.GetComponent<BoxCollider2D>());
		}
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x0013393C File Offset: 0x00131D3C
	public static void UpdateWidgetCollider(UIWidget w, BoxCollider box)
	{
		if (box != null && w != null)
		{
			Vector4 drawRegion = w.drawRegion;
			if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
			{
				Vector4 drawingDimensions = w.drawingDimensions;
				box.center = new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f);
				box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
			}
			else
			{
				Vector3[] localCorners = w.localCorners;
				box.center = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
				box.size = localCorners[2] - localCorners[0];
			}
		}
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x00133A68 File Offset: 0x00131E68
	public static void UpdateWidgetCollider(UIWidget w, BoxCollider2D box)
	{
		if (box != null && w != null)
		{
			Vector4 drawRegion = w.drawRegion;
			if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
			{
				Vector4 drawingDimensions = w.drawingDimensions;
				box.offset = new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f);
				box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
			}
			else
			{
				Vector3[] localCorners = w.localCorners;
				box.offset = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
				box.size = localCorners[2] - localCorners[0];
			}
		}
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x00133BA8 File Offset: 0x00131FA8
	public static void UpdateWidgetCollider(BoxCollider2D box, bool considerInactive)
	{
		if (box != null)
		{
			GameObject gameObject = box.gameObject;
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector4 drawRegion = component.drawRegion;
				if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
				{
					Vector4 drawingDimensions = component.drawingDimensions;
					box.offset = new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f);
					box.size = new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y);
				}
				else
				{
					Vector3[] localCorners = component.localCorners;
					box.offset = Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
					box.size = localCorners[2] - localCorners[0];
				}
			}
			else
			{
				Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(gameObject.transform, considerInactive);
				box.offset = bounds.center;
				box.size = new Vector2(bounds.size.x, bounds.size.y);
			}
		}
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x00133D4C File Offset: 0x0013214C
	public static string GetTypeName<T>()
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x00133DA0 File Offset: 0x001321A0
	public static string GetTypeName(UnityEngine.Object obj)
	{
		if (obj == null)
		{
			return "Null";
		}
		string text = obj.GetType().ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x00133E02 File Offset: 0x00132202
	public static void RegisterUndo(UnityEngine.Object obj, string name)
	{
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x00133E04 File Offset: 0x00132204
	public static void SetDirty(UnityEngine.Object obj, string undoName = "last change")
	{
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x00133E06 File Offset: 0x00132206
	public static GameObject AddChild(GameObject parent)
	{
		return parent.AddChild(true, -1);
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x00133E10 File Offset: 0x00132210
	public static GameObject AddChild(this GameObject parent, int layer)
	{
		return parent.AddChild(true, layer);
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x00133E1A File Offset: 0x0013221A
	public static GameObject AddChild(this GameObject parent, bool undo)
	{
		return parent.AddChild(undo, -1);
	}

	// Token: 0x06002967 RID: 10599 RVA: 0x00133E24 File Offset: 0x00132224
	public static GameObject AddChild(this GameObject parent, bool undo, int layer)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			if (layer == -1)
			{
				gameObject.layer = parent.layer;
			}
			else if (layer > -1 && layer < 32)
			{
				gameObject.layer = layer;
			}
		}
		return gameObject;
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x00133EA6 File Offset: 0x001322A6
	public static GameObject AddChild(this GameObject parent, GameObject prefab)
	{
		return parent.AddChild(prefab, -1);
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x00133EB0 File Offset: 0x001322B0
	public static GameObject AddChild(this GameObject parent, GameObject prefab, int layer)
	{
		GameObject gameObject = (!(parent != null)) ? UnityEngine.Object.Instantiate<GameObject>(prefab) : UnityEngine.Object.Instantiate<GameObject>(prefab, parent.transform);
		if (gameObject != null)
		{
			Transform transform = gameObject.transform;
			gameObject.name = prefab.name;
			if (parent != null)
			{
				if (layer == -1)
				{
					gameObject.layer = parent.layer;
				}
				else if (layer > -1 && layer < 32)
				{
					gameObject.layer = layer;
				}
			}
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x00133F64 File Offset: 0x00132364
	public static int CalculateRaycastDepth(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if (component != null)
		{
			return component.raycastDepth;
		}
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		if (componentsInChildren.Length == 0)
		{
			return 0;
		}
		int num = int.MaxValue;
		int i = 0;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			if (componentsInChildren[i].enabled)
			{
				num = Mathf.Min(num, componentsInChildren[i].raycastDepth);
			}
			i++;
		}
		return num;
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x00133FD8 File Offset: 0x001323D8
	public static int CalculateNextDepth(GameObject go)
	{
		if (go)
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			int num2 = componentsInChildren.Length;
			while (i < num2)
			{
				num = Mathf.Max(num, componentsInChildren[i].depth);
				i++;
			}
			return num + 1;
		}
		return 0;
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x00134024 File Offset: 0x00132424
	public static int CalculateNextDepth(GameObject go, bool ignoreChildrenWithColliders)
	{
		if (go && ignoreChildrenWithColliders)
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			int num2 = componentsInChildren.Length;
			while (i < num2)
			{
				UIWidget uiwidget = componentsInChildren[i];
				if (!(uiwidget.cachedGameObject != go) || (!(uiwidget.GetComponent<Collider>() != null) && !(uiwidget.GetComponent<Collider2D>() != null)))
				{
					num = Mathf.Max(num, uiwidget.depth);
				}
				i++;
			}
			return num + 1;
		}
		return NGUITools.CalculateNextDepth(go);
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x001340BC File Offset: 0x001324BC
	public static int AdjustDepth(GameObject go, int adjustment)
	{
		if (!(go != null))
		{
			return 0;
		}
		UIPanel uipanel = go.GetComponent<UIPanel>();
		if (uipanel != null)
		{
			foreach (UIPanel uipanel2 in go.GetComponentsInChildren<UIPanel>(true))
			{
				uipanel2.depth += adjustment;
			}
			return 1;
		}
		uipanel = NGUITools.FindInParents<UIPanel>(go);
		if (uipanel == null)
		{
			return 0;
		}
		UIWidget[] componentsInChildren2 = go.GetComponentsInChildren<UIWidget>(true);
		int j = 0;
		int num = componentsInChildren2.Length;
		while (j < num)
		{
			UIWidget uiwidget = componentsInChildren2[j];
			if (!(uiwidget.panel != uipanel))
			{
				uiwidget.depth += adjustment;
			}
			j++;
		}
		return 2;
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x00134184 File Offset: 0x00132584
	public static void BringForward(GameObject go)
	{
		int num = NGUITools.AdjustDepth(go, 1000);
		if (num == 1)
		{
			NGUITools.NormalizePanelDepths();
		}
		else if (num == 2)
		{
			NGUITools.NormalizeWidgetDepths();
		}
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x001341BC File Offset: 0x001325BC
	public static void PushBack(GameObject go)
	{
		int num = NGUITools.AdjustDepth(go, -1000);
		if (num == 1)
		{
			NGUITools.NormalizePanelDepths();
		}
		else if (num == 2)
		{
			NGUITools.NormalizeWidgetDepths();
		}
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x001341F2 File Offset: 0x001325F2
	public static void NormalizeDepths()
	{
		NGUITools.NormalizeWidgetDepths();
		NGUITools.NormalizePanelDepths();
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x001341FE File Offset: 0x001325FE
	public static void NormalizeWidgetDepths()
	{
		NGUITools.NormalizeWidgetDepths(NGUITools.FindActive<UIWidget>());
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x0013420A File Offset: 0x0013260A
	public static void NormalizeWidgetDepths(GameObject go)
	{
		NGUITools.NormalizeWidgetDepths(go.GetComponentsInChildren<UIWidget>());
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x00134218 File Offset: 0x00132618
	public static void NormalizeWidgetDepths(UIWidget[] list)
	{
		int num = list.Length;
		if (num > 0)
		{
			if (NGUITools.<>f__mg$cache0 == null)
			{
				NGUITools.<>f__mg$cache0 = new Comparison<UIWidget>(UIWidget.FullCompareFunc);
			}
			Array.Sort<UIWidget>(list, NGUITools.<>f__mg$cache0);
			int num2 = 0;
			int depth = list[0].depth;
			for (int i = 0; i < num; i++)
			{
				UIWidget uiwidget = list[i];
				if (uiwidget.depth == depth)
				{
					uiwidget.depth = num2;
				}
				else
				{
					depth = uiwidget.depth;
					num2 = (uiwidget.depth = num2 + 1);
				}
			}
		}
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x001342A4 File Offset: 0x001326A4
	public static void NormalizePanelDepths()
	{
		UIPanel[] array = NGUITools.FindActive<UIPanel>();
		int num = array.Length;
		if (num > 0)
		{
			UIPanel[] array2 = array;
			if (NGUITools.<>f__mg$cache1 == null)
			{
				NGUITools.<>f__mg$cache1 = new Comparison<UIPanel>(UIPanel.CompareFunc);
			}
			Array.Sort<UIPanel>(array2, NGUITools.<>f__mg$cache1);
			int num2 = 0;
			int depth = array[0].depth;
			for (int i = 0; i < num; i++)
			{
				UIPanel uipanel = array[i];
				if (uipanel.depth == depth)
				{
					uipanel.depth = num2;
				}
				else
				{
					depth = uipanel.depth;
					num2 = (uipanel.depth = num2 + 1);
				}
			}
		}
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x0013433A File Offset: 0x0013273A
	public static UIPanel CreateUI(bool advanced3D)
	{
		return NGUITools.CreateUI(null, advanced3D, -1);
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x00134344 File Offset: 0x00132744
	public static UIPanel CreateUI(bool advanced3D, int layer)
	{
		return NGUITools.CreateUI(null, advanced3D, layer);
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x00134350 File Offset: 0x00132750
	public static UIPanel CreateUI(Transform trans, bool advanced3D, int layer)
	{
		UIRoot uiroot = (!(trans != null)) ? null : NGUITools.FindInParents<UIRoot>(trans.gameObject);
		if (uiroot == null && UIRoot.list.Count > 0)
		{
			foreach (UIRoot uiroot2 in UIRoot.list)
			{
				if (uiroot2.gameObject.layer == layer)
				{
					uiroot = uiroot2;
					break;
				}
			}
		}
		if (uiroot == null)
		{
			int i = 0;
			int count = UIPanel.list.Count;
			while (i < count)
			{
				UIPanel uipanel = UIPanel.list[i];
				GameObject gameObject = uipanel.gameObject;
				if (gameObject.hideFlags == HideFlags.None && gameObject.layer == layer)
				{
					trans.parent = uipanel.transform;
					trans.localScale = Vector3.one;
					return uipanel;
				}
				i++;
			}
		}
		if (uiroot != null)
		{
			UICamera componentInChildren = uiroot.GetComponentInChildren<UICamera>();
			if (componentInChildren != null && componentInChildren.GetComponent<Camera>().orthographic == advanced3D)
			{
				trans = null;
				uiroot = null;
			}
		}
		if (uiroot == null)
		{
			GameObject gameObject2 = NGUITools.AddChild((GameObject)null, false);
			uiroot = gameObject2.AddComponent<UIRoot>();
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("UI");
			}
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("2D UI");
			}
			gameObject2.layer = layer;
			if (advanced3D)
			{
				gameObject2.name = "UI Root (3D)";
				uiroot.scalingStyle = UIRoot.Scaling.Constrained;
			}
			else
			{
				gameObject2.name = "UI Root";
				uiroot.scalingStyle = UIRoot.Scaling.Flexible;
			}
			uiroot.UpdateScale(true);
		}
		UIPanel uipanel2 = uiroot.GetComponentInChildren<UIPanel>();
		if (uipanel2 == null)
		{
			Camera[] array = NGUITools.FindActive<Camera>();
			float num = -1f;
			bool flag = false;
			int num2 = 1 << uiroot.gameObject.layer;
			foreach (Camera camera in array)
			{
				if (camera.clearFlags == CameraClearFlags.Color || camera.clearFlags == CameraClearFlags.Skybox)
				{
					flag = true;
				}
				num = Mathf.Max(num, camera.depth);
				camera.cullingMask &= ~num2;
			}
			Camera camera2 = uiroot.gameObject.AddChild(false);
			camera2.gameObject.AddComponent<UICamera>();
			camera2.clearFlags = ((!flag) ? CameraClearFlags.Color : CameraClearFlags.Depth);
			camera2.backgroundColor = Color.grey;
			camera2.cullingMask = num2;
			camera2.depth = num + 1f;
			if (advanced3D)
			{
				camera2.nearClipPlane = 0.1f;
				camera2.farClipPlane = 4f;
				camera2.transform.localPosition = new Vector3(0f, 0f, -700f);
			}
			else
			{
				camera2.orthographic = true;
				camera2.orthographicSize = 1f;
				camera2.nearClipPlane = -10f;
				camera2.farClipPlane = 10f;
			}
			AudioListener[] array2 = NGUITools.FindActive<AudioListener>();
			if (array2 == null || array2.Length == 0)
			{
				camera2.gameObject.AddComponent<AudioListener>();
			}
			uipanel2 = uiroot.gameObject.AddComponent<UIPanel>();
		}
		if (trans != null)
		{
			while (trans.parent != null)
			{
				trans = trans.parent;
			}
			if (NGUITools.IsChild(trans, uipanel2.transform))
			{
				uipanel2 = trans.gameObject.AddComponent<UIPanel>();
			}
			else
			{
				trans.parent = uipanel2.transform;
				trans.localScale = Vector3.one;
				trans.localPosition = Vector3.zero;
				uipanel2.cachedTransform.SetChildLayer(uipanel2.cachedGameObject.layer);
			}
		}
		return uipanel2;
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x0013473C File Offset: 0x00132B3C
	public static void SetChildLayer(this Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			child.gameObject.layer = layer;
			child.SetChildLayer(layer);
		}
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x0013477C File Offset: 0x00132B7C
	public static T AddChild<T>(this GameObject parent) where T : Component
	{
		GameObject gameObject = NGUITools.AddChild(parent);
		string typeName;
		if (!NGUITools.mTypeNames.TryGetValue(typeof(T), out typeName) || typeName == null)
		{
			typeName = NGUITools.GetTypeName<T>();
			NGUITools.mTypeNames[typeof(T)] = typeName;
		}
		gameObject.name = typeName;
		return gameObject.AddComponent<T>();
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x001347DC File Offset: 0x00132BDC
	public static T AddChild<T>(this GameObject parent, bool undo) where T : Component
	{
		GameObject gameObject = parent.AddChild(undo);
		string typeName;
		if (!NGUITools.mTypeNames.TryGetValue(typeof(T), out typeName) || typeName == null)
		{
			typeName = NGUITools.GetTypeName<T>();
			NGUITools.mTypeNames[typeof(T)] = typeName;
		}
		gameObject.name = typeName;
		return gameObject.AddComponent<T>();
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x0013483C File Offset: 0x00132C3C
	public static T AddWidget<T>(this GameObject go, int depth = 2147483647) where T : UIWidget
	{
		if (depth == 2147483647)
		{
			depth = NGUITools.CalculateNextDepth(go);
		}
		T result = go.AddChild<T>();
		result.width = 100;
		result.height = 100;
		result.depth = depth;
		return result;
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x00134890 File Offset: 0x00132C90
	public static UISprite AddSprite(this GameObject go, UIAtlas atlas, string spriteName, int depth = 2147483647)
	{
		UISpriteData uispriteData = (!(atlas != null)) ? null : atlas.GetSprite(spriteName);
		UISprite uisprite = go.AddWidget(depth);
		uisprite.type = ((uispriteData != null && uispriteData.hasBorder) ? UIBasicSprite.Type.Sliced : UIBasicSprite.Type.Simple);
		uisprite.atlas = atlas;
		uisprite.spriteName = spriteName;
		return uisprite;
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x001348EC File Offset: 0x00132CEC
	public static GameObject GetRoot(GameObject go)
	{
		Transform transform = go.transform;
		for (;;)
		{
			Transform parent = transform.parent;
			if (parent == null)
			{
				break;
			}
			transform = parent;
		}
		return transform.gameObject;
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x0013492C File Offset: 0x00132D2C
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return (T)((object)null);
		}
		return go.GetComponentInParent<T>();
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x00134954 File Offset: 0x00132D54
	public static T FindInParents<T>(Transform trans) where T : Component
	{
		if (trans == null)
		{
			return (T)((object)null);
		}
		return trans.GetComponentInParent<T>();
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x0013497C File Offset: 0x00132D7C
	public static void Destroy(UnityEngine.Object obj)
	{
		if (obj)
		{
			if (obj is Transform)
			{
				Transform transform = obj as Transform;
				GameObject gameObject = transform.gameObject;
				if (Application.isPlaying)
				{
					transform.parent = null;
					UnityEngine.Object.Destroy(gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
			}
			else if (obj is GameObject)
			{
				GameObject gameObject2 = obj as GameObject;
				Transform transform2 = gameObject2.transform;
				if (Application.isPlaying)
				{
					transform2.parent = null;
					UnityEngine.Object.Destroy(gameObject2);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(gameObject2);
				}
			}
			else if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x00134A30 File Offset: 0x00132E30
	public static void DestroyChildren(this Transform t)
	{
		bool isPlaying = Application.isPlaying;
		while (t.childCount != 0)
		{
			Transform child = t.GetChild(0);
			if (isPlaying)
			{
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(child.gameObject);
			}
		}
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x00134A83 File Offset: 0x00132E83
	public static void DestroyImmediate(UnityEngine.Object obj)
	{
		if (obj != null)
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
			else
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x00134AAC File Offset: 0x00132EAC
	public static void Broadcast(string funcName)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x00134AF0 File Offset: 0x00132EF0
	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x00134B33 File Offset: 0x00132F33
	public static bool IsChild(Transform parent, Transform child)
	{
		return child.IsChildOf(parent);
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x00134B3C File Offset: 0x00132F3C
	private static void Activate(Transform t)
	{
		NGUITools.Activate(t, false);
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x00134B48 File Offset: 0x00132F48
	private static void Activate(Transform t, bool compatibilityMode)
	{
		NGUITools.SetActiveSelf(t.gameObject, true);
		if (compatibilityMode)
		{
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				Transform child = t.GetChild(i);
				if (child.gameObject.activeSelf)
				{
					return;
				}
				i++;
			}
			int j = 0;
			int childCount2 = t.childCount;
			while (j < childCount2)
			{
				Transform child2 = t.GetChild(j);
				NGUITools.Activate(child2, true);
				j++;
			}
		}
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x00134BC5 File Offset: 0x00132FC5
	private static void Deactivate(Transform t)
	{
		NGUITools.SetActiveSelf(t.gameObject, false);
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x00134BD3 File Offset: 0x00132FD3
	public static void SetActive(GameObject go, bool state)
	{
		NGUITools.SetActive(go, state, true);
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x00134BDD File Offset: 0x00132FDD
	public static void SetActive(GameObject go, bool state, bool compatibilityMode)
	{
		if (go)
		{
			if (state)
			{
				NGUITools.Activate(go.transform, compatibilityMode);
				NGUITools.CallCreatePanel(go.transform);
			}
			else
			{
				NGUITools.Deactivate(go.transform);
			}
		}
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x00134C18 File Offset: 0x00133018
	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void CallCreatePanel(Transform t)
	{
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.CreatePanel();
		}
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.CallCreatePanel(t.GetChild(i));
			i++;
		}
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x00134C64 File Offset: 0x00133064
	public static void SetActiveChildren(GameObject go, bool state)
	{
		Transform transform = go.transform;
		if (state)
		{
			int i = 0;
			int childCount = transform.childCount;
			while (i < childCount)
			{
				Transform child = transform.GetChild(i);
				NGUITools.Activate(child);
				i++;
			}
		}
		else
		{
			int j = 0;
			int childCount2 = transform.childCount;
			while (j < childCount2)
			{
				Transform child2 = transform.GetChild(j);
				NGUITools.Deactivate(child2);
				j++;
			}
		}
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x00134CDA File Offset: 0x001330DA
	[Obsolete("Use NGUITools.GetActive instead")]
	public static bool IsActive(Behaviour mb)
	{
		return mb != null && mb.enabled && mb.gameObject.activeInHierarchy;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x00134D01 File Offset: 0x00133101
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(Behaviour mb)
	{
		return mb && mb.enabled && mb.gameObject.activeInHierarchy;
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x00134D27 File Offset: 0x00133127
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static bool GetActive(GameObject go)
	{
		return go && go.activeInHierarchy;
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x00134D3D File Offset: 0x0013313D
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x00134D48 File Offset: 0x00133148
	public static void SetLayer(GameObject go, int layer)
	{
		go.layer = layer;
		Transform transform = go.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			Transform child = transform.GetChild(i);
			NGUITools.SetLayer(child.gameObject, layer);
			i++;
		}
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x00134D90 File Offset: 0x00133190
	public static Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x00134DCC File Offset: 0x001331CC
	public static void MakePixelPerfect(Transform t)
	{
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		if (t.GetComponent<UIAnchor>() == null && t.GetComponent<UIRoot>() == null)
		{
			t.localPosition = NGUITools.Round(t.localPosition);
			t.localScale = NGUITools.Round(t.localScale);
		}
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.MakePixelPerfect(t.GetChild(i));
			i++;
		}
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x00134E5C File Offset: 0x0013325C
	public static void FitOnScreen(this Camera cam, Transform t, bool considerInactive = false, bool considerChildren = true)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(t, t, considerInactive, considerChildren);
		Vector3 a = cam.WorldToScreenPoint(t.position);
		Vector3 vector = a + bounds.min;
		Vector3 vector2 = a + bounds.max;
		int width = Screen.width;
		int height = Screen.height;
		Vector2 zero = Vector2.zero;
		if (vector.x < 0f)
		{
			zero.x = -vector.x;
		}
		else if (vector2.x > (float)width)
		{
			zero.x = (float)width - vector2.x;
		}
		if (vector.y < 0f)
		{
			zero.y = -vector.y;
		}
		else if (vector2.y > (float)height)
		{
			zero.y = (float)height - vector2.y;
		}
		if (zero.sqrMagnitude > 0f)
		{
			t.localPosition += new Vector3(zero.x, zero.y, 0f);
		}
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x00134F77 File Offset: 0x00133377
	public static void FitOnScreen(this Camera cam, Transform transform, Vector3 pos)
	{
		cam.FitOnScreen(transform, transform, pos, false);
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x00134F84 File Offset: 0x00133384
	public static void FitOnScreen(this Camera cam, Transform transform, Transform content, Vector3 pos, bool considerInactive = false)
	{
		Bounds bounds;
		cam.FitOnScreen(transform, content, pos, out bounds, considerInactive);
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x00134FA0 File Offset: 0x001333A0
	public static void FitOnScreen(this Camera cam, Transform transform, Transform content, Vector3 pos, out Bounds bounds, bool considerInactive = false)
	{
		bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, content, considerInactive, true);
		Vector3 min = bounds.min;
		Vector3 vector = bounds.max;
		Vector3 size = bounds.size;
		size.x += min.x;
		size.y -= vector.y;
		if (cam != null)
		{
			pos.x = Mathf.Clamp01(pos.x / (float)Screen.width);
			pos.y = Mathf.Clamp01(pos.y / (float)Screen.height);
			float num = cam.orthographicSize / transform.parent.lossyScale.y;
			float num2 = (float)Screen.height * 0.5f / num;
			vector = new Vector2(num2 * size.x / (float)Screen.width, num2 * size.y / (float)Screen.height);
			pos.x = Mathf.Min(pos.x, 1f - vector.x);
			pos.y = Mathf.Max(pos.y, vector.y);
			transform.position = cam.ViewportToWorldPoint(pos);
			pos = transform.localPosition;
			pos.x = Mathf.Round(pos.x);
			pos.y = Mathf.Round(pos.y);
		}
		else
		{
			if (pos.x + size.x > (float)Screen.width)
			{
				pos.x = (float)Screen.width - size.x;
			}
			if (pos.y - size.y < 0f)
			{
				pos.y = size.y;
			}
			pos.x -= (float)Screen.width * 0.5f;
			pos.y -= (float)Screen.height * 0.5f;
		}
		transform.localPosition = pos;
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x001351A4 File Offset: 0x001335A4
	public static bool Save(string fileName, byte[] bytes)
	{
		if (!NGUITools.fileAccess)
		{
			return false;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (bytes == null)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			return true;
		}
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(path);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
			return false;
		}
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
		return true;
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x0013522C File Offset: 0x0013362C
	public static byte[] Load(string fileName)
	{
		if (!NGUITools.fileAccess)
		{
			return null;
		}
		string path = Application.persistentDataPath + "/" + fileName;
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}
		return null;
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x0013526C File Offset: 0x0013366C
	public static Color ApplyPMA(Color c)
	{
		if (c.a != 1f)
		{
			c.r *= c.a;
			c.g *= c.a;
			c.b *= c.a;
		}
		return c;
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x001352CC File Offset: 0x001336CC
	public static void MarkParentAsChanged(GameObject go)
	{
		UIRect[] componentsInChildren = go.GetComponentsInChildren<UIRect>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			componentsInChildren[i].ParentHasChanged();
			i++;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x0600299C RID: 10652 RVA: 0x00135300 File Offset: 0x00133700
	// (set) Token: 0x0600299D RID: 10653 RVA: 0x00135320 File Offset: 0x00133720
	public static string clipboard
	{
		get
		{
			TextEditor textEditor = new TextEditor();
			textEditor.Paste();
			return textEditor.text;
		}
		set
		{
			TextEditor textEditor = new TextEditor();
			textEditor.text = value;
			textEditor.OnFocus();
			textEditor.Copy();
		}
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x00135346 File Offset: 0x00133746
	[Obsolete("Use NGUIText.EncodeColor instead")]
	public static string EncodeColor(Color c)
	{
		return NGUIText.EncodeColor24(c);
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x0013534E File Offset: 0x0013374E
	[Obsolete("Use NGUIText.ParseColor instead")]
	public static Color ParseColor(string text, int offset)
	{
		return NGUIText.ParseColor24(text, offset);
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x00135357 File Offset: 0x00133757
	[Obsolete("Use NGUIText.StripSymbols instead")]
	public static string StripSymbols(string text)
	{
		return NGUIText.StripSymbols(text);
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x00135360 File Offset: 0x00133760
	public static T AddMissingComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x0013538D File Offset: 0x0013378D
	public static Vector3[] GetSides(this Camera cam)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), null);
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x001353AC File Offset: 0x001337AC
	public static Vector3[] GetSides(this Camera cam, float depth)
	{
		return cam.GetSides(depth, null);
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x001353B6 File Offset: 0x001337B6
	public static Vector3[] GetSides(this Camera cam, Transform relativeTo)
	{
		return cam.GetSides(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x001353D8 File Offset: 0x001337D8
	public static Vector3[] GetSides(this Camera cam, float depth, Transform relativeTo)
	{
		if (cam.orthographic)
		{
			float orthographicSize = cam.orthographicSize;
			float num = -orthographicSize;
			float num2 = orthographicSize;
			float y = -orthographicSize;
			float y2 = orthographicSize;
			Rect rect = cam.rect;
			Vector2 screenSize = NGUITools.screenSize;
			float num3 = screenSize.x / screenSize.y;
			num3 *= rect.width / rect.height;
			num *= num3;
			num2 *= num3;
			Transform transform = cam.transform;
			Quaternion rotation = transform.rotation;
			Vector3 position = transform.position;
			int num4 = Mathf.RoundToInt(screenSize.x);
			int num5 = Mathf.RoundToInt(screenSize.y);
			if ((num4 & 1) == 1)
			{
				position.x -= 1f / screenSize.x;
			}
			if ((num5 & 1) == 1)
			{
				position.y += 1f / screenSize.y;
			}
			NGUITools.mSides[0] = rotation * new Vector3(num, 0f, depth) + position;
			NGUITools.mSides[1] = rotation * new Vector3(0f, y2, depth) + position;
			NGUITools.mSides[2] = rotation * new Vector3(num2, 0f, depth) + position;
			NGUITools.mSides[3] = rotation * new Vector3(0f, y, depth) + position;
		}
		else
		{
			NGUITools.mSides[0] = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, depth));
			NGUITools.mSides[1] = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, depth));
			NGUITools.mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth));
			NGUITools.mSides[3] = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, depth));
		}
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				NGUITools.mSides[i] = relativeTo.InverseTransformPoint(NGUITools.mSides[i]);
			}
		}
		return NGUITools.mSides;
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x00135658 File Offset: 0x00133A58
	public static Vector3[] GetWorldCorners(this Camera cam)
	{
		float depth = Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f);
		return cam.GetWorldCorners(depth, null);
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x00135684 File Offset: 0x00133A84
	public static Vector3[] GetWorldCorners(this Camera cam, float depth)
	{
		return cam.GetWorldCorners(depth, null);
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x0013568E File Offset: 0x00133A8E
	public static Vector3[] GetWorldCorners(this Camera cam, Transform relativeTo)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.nearClipPlane, cam.farClipPlane, 0.5f), relativeTo);
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x001356B0 File Offset: 0x00133AB0
	public static Vector3[] GetWorldCorners(this Camera cam, float depth, Transform relativeTo)
	{
		if (cam.orthographic)
		{
			float orthographicSize = cam.orthographicSize;
			float num = -orthographicSize;
			float num2 = orthographicSize;
			float y = -orthographicSize;
			float y2 = orthographicSize;
			Rect rect = cam.rect;
			Vector2 screenSize = NGUITools.screenSize;
			float num3 = screenSize.x / screenSize.y;
			num3 *= rect.width / rect.height;
			num *= num3;
			num2 *= num3;
			Transform transform = cam.transform;
			Quaternion rotation = transform.rotation;
			Vector3 position = transform.position;
			NGUITools.mSides[0] = rotation * new Vector3(num, y, depth) + position;
			NGUITools.mSides[1] = rotation * new Vector3(num, y2, depth) + position;
			NGUITools.mSides[2] = rotation * new Vector3(num2, y2, depth) + position;
			NGUITools.mSides[3] = rotation * new Vector3(num2, y, depth) + position;
		}
		else
		{
			NGUITools.mSides[0] = cam.ViewportToWorldPoint(new Vector3(0f, 0f, depth));
			NGUITools.mSides[1] = cam.ViewportToWorldPoint(new Vector3(0f, 1f, depth));
			NGUITools.mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 1f, depth));
			NGUITools.mSides[3] = cam.ViewportToWorldPoint(new Vector3(1f, 0f, depth));
		}
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				NGUITools.mSides[i] = relativeTo.InverseTransformPoint(NGUITools.mSides[i]);
			}
		}
		return NGUITools.mSides;
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x001358B8 File Offset: 0x00133CB8
	public static string GetFuncName(object obj, string method)
	{
		if (obj == null)
		{
			return "<null>";
		}
		string text = obj.GetType().ToString();
		int num = text.LastIndexOf('/');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		return (!string.IsNullOrEmpty(method)) ? (text + "/" + method) : text;
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x00135914 File Offset: 0x00133D14
	public static void Execute<T>(GameObject go, string funcName) where T : Component
	{
		T[] components = go.GetComponents<T>();
		foreach (T t in components)
		{
			MethodInfo method = t.GetType().GetMethod(funcName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null)
			{
				method.Invoke(t, null);
			}
		}
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x00135974 File Offset: 0x00133D74
	public static void ExecuteAll<T>(GameObject root, string funcName) where T : Component
	{
		NGUITools.Execute<T>(root, funcName);
		Transform transform = root.transform;
		int i = 0;
		int childCount = transform.childCount;
		while (i < childCount)
		{
			NGUITools.ExecuteAll<T>(transform.GetChild(i).gameObject, funcName);
			i++;
		}
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x001359BA File Offset: 0x00133DBA
	public static void ImmediatelyCreateDrawCalls(GameObject root)
	{
		NGUITools.ExecuteAll<UIWidget>(root, "Start");
		NGUITools.ExecuteAll<UIPanel>(root, "Start");
		NGUITools.ExecuteAll<UIWidget>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "LateUpdate");
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x060029AE RID: 10670 RVA: 0x001359F3 File Offset: 0x00133DF3
	public static Vector2 screenSize
	{
		get
		{
			return new Vector2((float)Screen.width, (float)Screen.height);
		}
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x00135A08 File Offset: 0x00133E08
	public static string KeyToCaption(KeyCode key)
	{
		switch (key)
		{
		case KeyCode.Keypad0:
			return "K0";
		case KeyCode.Keypad1:
			return "K1";
		case KeyCode.Keypad2:
			return "K2";
		case KeyCode.Keypad3:
			return "K3";
		case KeyCode.Keypad4:
			return "K4";
		case KeyCode.Keypad5:
			return "K5";
		case KeyCode.Keypad6:
			return "K6";
		case KeyCode.Keypad7:
			return "K7";
		case KeyCode.Keypad8:
			return "K8";
		case KeyCode.Keypad9:
			return "K9";
		case KeyCode.KeypadPeriod:
			return ".";
		case KeyCode.KeypadDivide:
			return "/";
		case KeyCode.KeypadMultiply:
			return "*";
		case KeyCode.KeypadMinus:
			return "-";
		case KeyCode.KeypadPlus:
			return "+";
		case KeyCode.KeypadEnter:
			return "NT";
		case KeyCode.KeypadEquals:
			return "=";
		case KeyCode.UpArrow:
			return "UP";
		case KeyCode.DownArrow:
			return "DN";
		case KeyCode.RightArrow:
			return "LT";
		case KeyCode.LeftArrow:
			return "RT";
		case KeyCode.Insert:
			return "Ins";
		case KeyCode.Home:
			return "Home";
		case KeyCode.End:
			return "End";
		case KeyCode.PageUp:
			return "PU";
		case KeyCode.PageDown:
			return "PD";
		case KeyCode.F1:
			return "F1";
		case KeyCode.F2:
			return "F2";
		case KeyCode.F3:
			return "F3";
		case KeyCode.F4:
			return "F4";
		case KeyCode.F5:
			return "F5";
		case KeyCode.F6:
			return "F6";
		case KeyCode.F7:
			return "F7";
		case KeyCode.F8:
			return "F8";
		case KeyCode.F9:
			return "F9";
		case KeyCode.F10:
			return "F10";
		case KeyCode.F11:
			return "F11";
		case KeyCode.F12:
			return "F12";
		case KeyCode.F13:
			return "F13";
		case KeyCode.F14:
			return "F14";
		case KeyCode.F15:
			return "F15";
		default:
			switch (key)
			{
			case KeyCode.Space:
				return "SP";
			case KeyCode.Exclaim:
				return "!";
			case KeyCode.DoubleQuote:
				return "\"";
			case KeyCode.Hash:
				return "#";
			case KeyCode.Dollar:
				return "$";
			default:
				switch (key)
				{
				case KeyCode.Backspace:
					return "BS";
				case KeyCode.Tab:
					return "Tab";
				default:
					if (key == KeyCode.None)
					{
						return null;
					}
					if (key == KeyCode.Pause)
					{
						return "PS";
					}
					if (key != KeyCode.Escape)
					{
						return null;
					}
					return "Esc";
				case KeyCode.Clear:
					return "Clr";
				case KeyCode.Return:
					return "NT";
				}
				break;
			case KeyCode.Ampersand:
				return "&";
			case KeyCode.Quote:
				return "'";
			case KeyCode.LeftParen:
				return "(";
			case KeyCode.RightParen:
				return ")";
			case KeyCode.Asterisk:
				return "*";
			case KeyCode.Plus:
				return "+";
			case KeyCode.Comma:
				return ",";
			case KeyCode.Minus:
				return "-";
			case KeyCode.Period:
				return ".";
			case KeyCode.Slash:
				return "/";
			case KeyCode.Alpha0:
				return "0";
			case KeyCode.Alpha1:
				return "1";
			case KeyCode.Alpha2:
				return "2";
			case KeyCode.Alpha3:
				return "3";
			case KeyCode.Alpha4:
				return "4";
			case KeyCode.Alpha5:
				return "5";
			case KeyCode.Alpha6:
				return "6";
			case KeyCode.Alpha7:
				return "7";
			case KeyCode.Alpha8:
				return "8";
			case KeyCode.Alpha9:
				return "9";
			case KeyCode.Colon:
				return ":";
			case KeyCode.Semicolon:
				return ";";
			case KeyCode.Less:
				return "<";
			case KeyCode.Equals:
				return "=";
			case KeyCode.Greater:
				return ">";
			case KeyCode.Question:
				return "?";
			case KeyCode.At:
				return "@";
			case KeyCode.LeftBracket:
				return "[";
			case KeyCode.Backslash:
				return "\\";
			case KeyCode.RightBracket:
				return "]";
			case KeyCode.Caret:
				return "^";
			case KeyCode.Underscore:
				return "_";
			case KeyCode.BackQuote:
				return "`";
			case KeyCode.A:
				return "A";
			case KeyCode.B:
				return "B";
			case KeyCode.C:
				return "C";
			case KeyCode.D:
				return "D";
			case KeyCode.E:
				return "E";
			case KeyCode.F:
				return "F";
			case KeyCode.G:
				return "G";
			case KeyCode.H:
				return "H";
			case KeyCode.I:
				return "I";
			case KeyCode.J:
				return "J";
			case KeyCode.K:
				return "K";
			case KeyCode.L:
				return "L";
			case KeyCode.M:
				return "M";
			case KeyCode.N:
				return "N0";
			case KeyCode.O:
				return "O";
			case KeyCode.P:
				return "P";
			case KeyCode.Q:
				return "Q";
			case KeyCode.R:
				return "R";
			case KeyCode.S:
				return "S";
			case KeyCode.T:
				return "T";
			case KeyCode.U:
				return "U";
			case KeyCode.V:
				return "V";
			case KeyCode.W:
				return "W";
			case KeyCode.X:
				return "X";
			case KeyCode.Y:
				return "Y";
			case KeyCode.Z:
				return "Z";
			case KeyCode.Delete:
				return "Del";
			}
			break;
		case KeyCode.Numlock:
			return "Num";
		case KeyCode.CapsLock:
			return "Cap";
		case KeyCode.ScrollLock:
			return "Scr";
		case KeyCode.RightShift:
			return "RS";
		case KeyCode.LeftShift:
			return "LS";
		case KeyCode.RightControl:
			return "RC";
		case KeyCode.LeftControl:
			return "LC";
		case KeyCode.RightAlt:
			return "RA";
		case KeyCode.LeftAlt:
			return "LA";
		case KeyCode.Mouse0:
			return "M0";
		case KeyCode.Mouse1:
			return "M1";
		case KeyCode.Mouse2:
			return "M2";
		case KeyCode.Mouse3:
			return "M3";
		case KeyCode.Mouse4:
			return "M4";
		case KeyCode.Mouse5:
			return "M5";
		case KeyCode.Mouse6:
			return "M6";
		case KeyCode.JoystickButton0:
			return "(A)";
		case KeyCode.JoystickButton1:
			return "(B)";
		case KeyCode.JoystickButton2:
			return "(X)";
		case KeyCode.JoystickButton3:
			return "(Y)";
		case KeyCode.JoystickButton4:
			return "(RB)";
		case KeyCode.JoystickButton5:
			return "(LB)";
		case KeyCode.JoystickButton6:
			return "(Back)";
		case KeyCode.JoystickButton7:
			return "(Start)";
		case KeyCode.JoystickButton8:
			return "(LS)";
		case KeyCode.JoystickButton9:
			return "(RS)";
		case KeyCode.JoystickButton10:
			return "J10";
		case KeyCode.JoystickButton11:
			return "J11";
		case KeyCode.JoystickButton12:
			return "J12";
		case KeyCode.JoystickButton13:
			return "J13";
		case KeyCode.JoystickButton14:
			return "J14";
		case KeyCode.JoystickButton15:
			return "J15";
		case KeyCode.JoystickButton16:
			return "J16";
		case KeyCode.JoystickButton17:
			return "J17";
		case KeyCode.JoystickButton18:
			return "J18";
		case KeyCode.JoystickButton19:
			return "J19";
		}
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x001360D8 File Offset: 0x001344D8
	public static T Draw<T>(string id, NGUITools.OnInitFunc<T> onInit = null) where T : UIWidget
	{
		UIWidget uiwidget;
		if (NGUITools.mWidgets.TryGetValue(id, out uiwidget) && uiwidget)
		{
			return (T)((object)uiwidget);
		}
		if (NGUITools.mRoot == null)
		{
			UICamera x = null;
			UIRoot uiroot = null;
			for (int i = 0; i < UIRoot.list.Count; i++)
			{
				UIRoot uiroot2 = UIRoot.list[i];
				if (uiroot2)
				{
					UICamera uicamera = UICamera.FindCameraForLayer(uiroot2.gameObject.layer);
					if (uicamera && uicamera.cachedCamera.orthographic)
					{
						x = uicamera;
						uiroot = uiroot2;
						break;
					}
				}
			}
			if (x == null)
			{
				NGUITools.mRoot = NGUITools.CreateUI(false, LayerMask.NameToLayer("UI"));
			}
			else
			{
				NGUITools.mRoot = uiroot.gameObject.AddChild<UIPanel>();
			}
			NGUITools.mRoot.depth = 100000;
			NGUITools.mGo = NGUITools.mRoot.gameObject;
			NGUITools.mGo.name = "Immediate Mode GUI";
		}
		uiwidget = NGUITools.mGo.AddWidget(int.MaxValue);
		uiwidget.name = id;
		NGUITools.mWidgets[id] = uiwidget;
		if (onInit != null)
		{
			onInit((T)((object)uiwidget));
		}
		return (T)((object)uiwidget);
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x00136230 File Offset: 0x00134630
	public static Color GammaToLinearSpace(this Color c)
	{
		if (NGUITools.mColorSpace == ColorSpace.Uninitialized)
		{
			NGUITools.mColorSpace = QualitySettings.activeColorSpace;
		}
		if (NGUITools.mColorSpace == ColorSpace.Linear)
		{
			return new Color(Mathf.GammaToLinearSpace(c.r), Mathf.GammaToLinearSpace(c.g), Mathf.GammaToLinearSpace(c.b), Mathf.GammaToLinearSpace(c.a));
		}
		return c;
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x00136294 File Offset: 0x00134694
	public static Color LinearToGammaSpace(this Color c)
	{
		if (NGUITools.mColorSpace == ColorSpace.Uninitialized)
		{
			NGUITools.mColorSpace = QualitySettings.activeColorSpace;
		}
		if (NGUITools.mColorSpace == ColorSpace.Linear)
		{
			return new Color(Mathf.LinearToGammaSpace(c.r), Mathf.LinearToGammaSpace(c.g), Mathf.LinearToGammaSpace(c.b), Mathf.LinearToGammaSpace(c.a));
		}
		return c;
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x001362F8 File Offset: 0x001346F8
	// Note: this type is marked as 'beforefieldinit'.
	static NGUITools()
	{
		KeyCode[] array = new KeyCode[145];
		RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.$field-7FB9790B49277F6151D3EB5D555CCF105904DB43).FieldHandle);
		NGUITools.keys = array;
		NGUITools.mWidgets = new Dictionary<string, UIWidget>();
		NGUITools.mColorSpace = ColorSpace.Uninitialized;
	}

	// Token: 0x040029BF RID: 10687
	[NonSerialized]
	private static AudioListener mListener;

	// Token: 0x040029C0 RID: 10688
	[NonSerialized]
	public static AudioSource audioSource;

	// Token: 0x040029C1 RID: 10689
	private static bool mLoaded = false;

	// Token: 0x040029C2 RID: 10690
	private static float mGlobalVolume = 1f;

	// Token: 0x040029C3 RID: 10691
	private static float mLastTimestamp = 0f;

	// Token: 0x040029C4 RID: 10692
	private static AudioClip mLastClip;

	// Token: 0x040029C5 RID: 10693
	private static Dictionary<Type, string> mTypeNames = new Dictionary<Type, string>();

	// Token: 0x040029C6 RID: 10694
	private static Vector3[] mSides = new Vector3[4];

	// Token: 0x040029C7 RID: 10695
	public static KeyCode[] keys;

	// Token: 0x040029C8 RID: 10696
	private static Dictionary<string, UIWidget> mWidgets;

	// Token: 0x040029C9 RID: 10697
	private static UIPanel mRoot;

	// Token: 0x040029CA RID: 10698
	private static GameObject mGo;

	// Token: 0x040029CB RID: 10699
	private static ColorSpace mColorSpace;

	// Token: 0x040029CC RID: 10700
	[CompilerGenerated]
	private static Comparison<UIWidget> <>f__mg$cache0;

	// Token: 0x040029CD RID: 10701
	[CompilerGenerated]
	private static Comparison<UIPanel> <>f__mg$cache1;

	// Token: 0x020005BC RID: 1468
	// (Invoke) Token: 0x060029B5 RID: 10677
	public delegate void OnInitFunc<T>(T w) where T : UIWidget;
}
