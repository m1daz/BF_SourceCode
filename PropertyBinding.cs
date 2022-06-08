using System;
using UnityEngine;

// Token: 0x020005BD RID: 1469
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Property Binding")]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x060029B9 RID: 10681 RVA: 0x00136374 File Offset: 0x00134774
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x0013638E File Offset: 0x0013478E
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x001363A2 File Offset: 0x001347A2
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x001363B6 File Offset: 0x001347B6
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x001363CA File Offset: 0x001347CA
	private void OnValidate()
	{
		if (this.source != null)
		{
			this.source.Reset();
		}
		if (this.target != null)
		{
			this.target.Reset();
		}
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x001363F8 File Offset: 0x001347F8
	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (this.source != null && this.target != null && this.source.isValid && this.target.isValid)
		{
			if (this.direction == PropertyBinding.Direction.SourceUpdatesTarget)
			{
				this.target.Set(this.source.Get());
			}
			else if (this.direction == PropertyBinding.Direction.TargetUpdatesSource)
			{
				this.source.Set(this.target.Get());
			}
			else if (this.source.GetPropertyType() == this.target.GetPropertyType())
			{
				object obj = this.source.Get();
				if (this.mLastValue == null || !this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.target.Set(obj);
				}
				else
				{
					obj = this.target.Get();
					if (!this.mLastValue.Equals(obj))
					{
						this.mLastValue = obj;
						this.source.Set(obj);
					}
				}
			}
		}
	}

	// Token: 0x040029CE RID: 10702
	public PropertyReference source;

	// Token: 0x040029CF RID: 10703
	public PropertyReference target;

	// Token: 0x040029D0 RID: 10704
	public PropertyBinding.Direction direction;

	// Token: 0x040029D1 RID: 10705
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x040029D2 RID: 10706
	public bool editMode = true;

	// Token: 0x040029D3 RID: 10707
	private object mLastValue;

	// Token: 0x020005BE RID: 1470
	[DoNotObfuscateNGUI]
	public enum UpdateCondition
	{
		// Token: 0x040029D5 RID: 10709
		OnStart,
		// Token: 0x040029D6 RID: 10710
		OnUpdate,
		// Token: 0x040029D7 RID: 10711
		OnLateUpdate,
		// Token: 0x040029D8 RID: 10712
		OnFixedUpdate
	}

	// Token: 0x020005BF RID: 1471
	[DoNotObfuscateNGUI]
	public enum Direction
	{
		// Token: 0x040029DA RID: 10714
		SourceUpdatesTarget,
		// Token: 0x040029DB RID: 10715
		TargetUpdatesSource,
		// Token: 0x040029DC RID: 10716
		BiDirectional
	}
}
