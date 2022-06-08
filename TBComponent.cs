using System;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public abstract class TBComponent : MonoBehaviour
{
	// Token: 0x1700017D RID: 381
	// (get) Token: 0x0600203C RID: 8252 RVA: 0x000F2186 File Offset: 0x000F0586
	// (set) Token: 0x0600203D RID: 8253 RVA: 0x000F218E File Offset: 0x000F058E
	public int FingerIndex
	{
		get
		{
			return this.fingerIndex;
		}
		protected set
		{
			this.fingerIndex = value;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x0600203E RID: 8254 RVA: 0x000F2197 File Offset: 0x000F0597
	// (set) Token: 0x0600203F RID: 8255 RVA: 0x000F219F File Offset: 0x000F059F
	public Vector2 FingerPos
	{
		get
		{
			return this.fingerPos;
		}
		protected set
		{
			this.fingerPos = value;
		}
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x000F21A8 File Offset: 0x000F05A8
	protected virtual void Start()
	{
		if (!base.GetComponent<Collider>())
		{
			Debug.LogError(base.name + " must have a valid collider.");
			base.enabled = false;
		}
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x000F21D8 File Offset: 0x000F05D8
	protected bool Send(TBComponent.Message msg)
	{
		if (!msg.enabled)
		{
			return false;
		}
		GameObject gameObject = msg.target;
		if (!gameObject)
		{
			gameObject = base.gameObject;
		}
		gameObject.SendMessage(msg.methodName, SendMessageOptions.DontRequireReceiver);
		return true;
	}

	// Token: 0x0400211C RID: 8476
	private int fingerIndex = -1;

	// Token: 0x0400211D RID: 8477
	private Vector2 fingerPos;

	// Token: 0x02000456 RID: 1110
	// (Invoke) Token: 0x06002043 RID: 8259
	public delegate void EventHandler<T>(T sender) where T : TBComponent;

	// Token: 0x02000457 RID: 1111
	[Serializable]
	public class Message
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x000F2219 File Offset: 0x000F0619
		public Message()
		{
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000F2233 File Offset: 0x000F0633
		public Message(string methodName)
		{
			this.methodName = methodName;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000F2254 File Offset: 0x000F0654
		public Message(string methodName, bool enabled)
		{
			this.enabled = enabled;
			this.methodName = methodName;
		}

		// Token: 0x0400211E RID: 8478
		public bool enabled = true;

		// Token: 0x0400211F RID: 8479
		public string methodName = "MethodToCall";

		// Token: 0x04002120 RID: 8480
		public GameObject target;
	}
}
