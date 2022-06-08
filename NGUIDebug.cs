using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x17000244 RID: 580
	// (get) Token: 0x060028E7 RID: 10471 RVA: 0x0012C912 File Offset: 0x0012AD12
	// (set) Token: 0x060028E8 RID: 10472 RVA: 0x0012C919 File Offset: 0x0012AD19
	public static bool debugRaycast
	{
		get
		{
			return NGUIDebug.mRayDebug;
		}
		set
		{
			NGUIDebug.mRayDebug = value;
			if (value && Application.isPlaying)
			{
				NGUIDebug.CreateInstance();
			}
		}
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x0012C938 File Offset: 0x0012AD38
	public static void CreateInstance()
	{
		if (NGUIDebug.mInstance == null)
		{
			GameObject gameObject = new GameObject("_NGUI Debug");
			NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x0012C974 File Offset: 0x0012AD74
	private static void LogString(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			NGUIDebug.CreateInstance();
		}
		else
		{
			Debug.Log(text);
		}
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x0012C9C4 File Offset: 0x0012ADC4
	public static void Log(params object[] objs)
	{
		string text = string.Empty;
		for (int i = 0; i < objs.Length; i++)
		{
			if (i == 0)
			{
				text += objs[i].ToString();
			}
			else
			{
				text = text + ", " + objs[i].ToString();
			}
		}
		NGUIDebug.LogString(text);
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x0012CA20 File Offset: 0x0012AE20
	public static void Log(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			string[] array = s.Split(new char[]
			{
				'\n'
			});
			foreach (string text in array)
			{
				NGUIDebug.LogString(text);
			}
		}
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x0012CA6A File Offset: 0x0012AE6A
	public static void Clear()
	{
		NGUIDebug.mLines.Clear();
	}

	// Token: 0x060028EE RID: 10478 RVA: 0x0012CA78 File Offset: 0x0012AE78
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x060028EF RID: 10479 RVA: 0x0012CBB0 File Offset: 0x0012AFB0
	private void OnGUI()
	{
		Rect position = new Rect(5f, 5f, 1000f, 22f);
		if (NGUIDebug.mRayDebug)
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			string text = "Scheme: " + currentScheme;
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Hover: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", string.Empty);
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Selection: " + NGUITools.GetHierarchy(UICamera.selectedObject).Replace("\"", string.Empty);
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Controller: " + NGUITools.GetHierarchy(UICamera.controllerNavigationObject).Replace("\"", string.Empty);
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Active events: " + UICamera.CountInputSources();
			if (UICamera.disableController)
			{
				text += ", disabled controller";
			}
			if (UICamera.ignoreControllerInput)
			{
				text += ", ignore controller";
			}
			if (UICamera.inputHasFocus)
			{
				text += ", input focus";
			}
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
		}
		int i = 0;
		int count = NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUI.color = Color.black;
			GUI.Label(position, NGUIDebug.mLines[i]);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, NGUIDebug.mLines[i]);
			position.y += 18f;
			position.x += 1f;
			i++;
		}
	}

	// Token: 0x04002983 RID: 10627
	private static bool mRayDebug = false;

	// Token: 0x04002984 RID: 10628
	private static List<string> mLines = new List<string>();

	// Token: 0x04002985 RID: 10629
	private static NGUIDebug mInstance = null;
}
