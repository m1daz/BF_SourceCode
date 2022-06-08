using System;
using System.IO;
using Pathfinding;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class AstarSerializer3_07 : AstarSerializer
{
	// Token: 0x060002B0 RID: 688 RVA: 0x000134C6 File Offset: 0x000118C6
	public AstarSerializer3_07(AstarPath script) : base(script)
	{
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x000134D0 File Offset: 0x000118D0
	public override void AddUnityReferenceValue(string key, UnityEngine.Object value)
	{
		BinaryWriter writerStream = this.writerStream;
		base.AddVariableAnchor(key);
		if (value == null)
		{
			writerStream.Write(0);
			return;
		}
		writerStream.Write(1);
		if (value == this.active.gameObject)
		{
			writerStream.Write(-128);
		}
		else if (value == this.active.transform)
		{
			writerStream.Write(-129);
		}
		else
		{
			writerStream.Write(value.GetInstanceID());
		}
		writerStream.Write(value.name);
		Component component = value as Component;
		GameObject gameObject = value as GameObject;
		if (component == null && gameObject == null)
		{
			writerStream.Write(string.Empty);
		}
		else
		{
			if (gameObject == null)
			{
				gameObject = component.gameObject;
			}
			string text = gameObject.name;
			while (gameObject.transform.parent != null)
			{
				gameObject = gameObject.transform.parent.gameObject;
				text = gameObject.name + "/" + text;
			}
			writerStream.Write(text);
		}
		if (AstarSerializer.writeUnityReference_Editor != null)
		{
			writerStream.Write(true);
			AstarSerializer.writeUnityReference_Editor(this, value);
		}
		else
		{
			writerStream.Write(false);
		}
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00013628 File Offset: 0x00011A28
	public override UnityEngine.Object GetUnityReferenceValue(string key, Type type, UnityEngine.Object defaultValue = null)
	{
		if (!base.MoveToVariableAnchor(key))
		{
			Debug.Log("Couldn't find key '" + key + "' in the data, returning default");
			return ((!(defaultValue == null)) ? defaultValue : base.GetDefaultValue(type)) as UnityEngine.Object;
		}
		BinaryReader readerStream = this.readerStream;
		int num = (int)readerStream.ReadByte();
		if (num == 0)
		{
			return ((!(defaultValue == null)) ? defaultValue : base.GetDefaultValue(type)) as UnityEngine.Object;
		}
		if (num == 2)
		{
			Debug.Log("The variable '" + key + "' was not serialized correctly and can therefore not be deserialized");
			return ((!(defaultValue == null)) ? defaultValue : base.GetDefaultValue(type)) as UnityEngine.Object;
		}
		int num2 = readerStream.ReadInt32();
		string text = readerStream.ReadString();
		if (num2 == -128)
		{
			return this.active.gameObject;
		}
		if (num2 == -129)
		{
			return this.active.transform;
		}
		string text2 = readerStream.ReadString();
		UnityEngine.Object @object = null;
		if (text2 != string.Empty)
		{
			GameObject gameObject = GameObject.Find(text2);
			if (gameObject != null)
			{
				if (type == typeof(GameObject))
				{
					return gameObject;
				}
				@object = gameObject.GetComponent(type);
				if (@object != null && @object.name == text)
				{
					return @object;
				}
			}
		}
		bool flag = readerStream.ReadBoolean();
		if (AstarSerializer.readUnityReference_Editor != null && flag)
		{
			UnityEngine.Object object2 = AstarSerializer.readUnityReference_Editor(this, text, num2, type);
			if (object2 != null && object2.name == text)
			{
				return object2;
			}
			if (@object != null)
			{
				return @object;
			}
			return object2;
		}
		else
		{
			if (@object != null)
			{
				return @object;
			}
			UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(type);
			UnityEngine.Object object3 = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].GetInstanceID() == num2)
				{
					object3 = array[i];
					break;
				}
			}
			if (object3 != null)
			{
				return object3;
			}
			return Resources.Load(text);
		}
	}
}
