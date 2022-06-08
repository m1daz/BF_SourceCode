using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class GraphCollision : ISerializableObject
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00019680 File Offset: 0x00017A80
		public void Initialize(Matrix4x4 matrix, float scale)
		{
			this.up = matrix.MultiplyVector(Vector3.up);
			this.upheight = this.up * this.height;
			this.finalRadius = this.diameter * scale * 0.5f;
			this.finalRaycastRadius = this.thickRaycastDiameter * scale * 0.5f;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000196E0 File Offset: 0x00017AE0
		public bool Check(Vector3 position)
		{
			if (!this.collisionCheck)
			{
				return true;
			}
			position += this.up * this.collisionOffset;
			ColliderType colliderType = this.type;
			if (colliderType == ColliderType.Capsule)
			{
				return !Physics.CheckCapsule(position, position + this.upheight, this.finalRadius, this.mask);
			}
			if (colliderType == ColliderType.Sphere)
			{
				return !Physics.CheckSphere(position, this.finalRadius, this.mask);
			}
			RayDirection rayDirection = this.rayDirection;
			if (rayDirection == RayDirection.Both)
			{
				return !Physics.Raycast(position, this.up, this.height, this.mask) && !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask);
			}
			if (rayDirection != RayDirection.Up)
			{
				return !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask);
			}
			return !Physics.Raycast(position, this.up, this.height, this.mask);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00019830 File Offset: 0x00017C30
		public Vector3 CheckHeight(Vector3 position)
		{
			RaycastHit raycastHit;
			bool flag;
			return this.CheckHeight(position, out raycastHit, out flag);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00019848 File Offset: 0x00017C48
		public Vector3 CheckHeight(Vector3 position, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck)
			{
				hit = default(RaycastHit);
				return position;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(position + this.up * this.fromHeight, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return Mathfx.NearestPoint(ray.origin, ray.origin + ray.direction, hit.point);
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			else
			{
				if (Physics.Raycast(position + this.up * this.fromHeight, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return hit.point;
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			return position;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001995C File Offset: 0x00017D5C
		public Vector3 Raycast(Vector3 origin, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck)
			{
				hit = default(RaycastHit);
				return origin - this.up * this.fromHeight;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(origin, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return Mathfx.NearestPoint(ray.origin, ray.origin + ray.direction, hit.point);
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			else
			{
				if (Physics.Raycast(origin, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return hit.point;
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			return origin - this.up * this.fromHeight;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00019A70 File Offset: 0x00017E70
		public RaycastHit[] CheckHeightAll(Vector3 position)
		{
			if (!this.heightCheck)
			{
				return new RaycastHit[]
				{
					new RaycastHit
					{
						point = position,
						distance = 0f
					}
				};
			}
			if (this.thickRaycast)
			{
				Debug.LogWarning("Thick raycast cannot be used with CheckHeightAll. Disabling thick raycast...");
				this.thickRaycast = false;
			}
			List<RaycastHit> list = new List<RaycastHit>();
			bool flag = true;
			Vector3 vector = position + this.up * this.fromHeight;
			Vector3 vector2 = Vector3.zero;
			int num = 0;
			for (;;)
			{
				RaycastHit item;
				this.Raycast(vector, out item, out flag);
				if (item.transform == null)
				{
					break;
				}
				if (item.point != vector2 || list.Count == 0)
				{
					vector = item.point - this.up * 0.005f;
					vector2 = item.point;
					num = 0;
					list.Add(item);
				}
				else
				{
					vector -= this.up * 0.001f;
					num++;
					if (num > 10)
					{
						goto Block_5;
					}
				}
			}
			goto IL_15B;
			Block_5:
			Debug.LogError(string.Concat(new object[]
			{
				"Infinite Loop when raycasting. Please report this error (arongranberg.com)\n",
				vector,
				" : ",
				vector2
			}));
			IL_15B:
			return list.ToArray();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00019BE0 File Offset: 0x00017FE0
		public void SerializeSettings(AstarSerializer serializer)
		{
			serializer.AddValue("Mask", this.mask);
			serializer.AddValue("Diameter", this.diameter);
			serializer.AddValue("Height", this.height);
			serializer.AddValue("Type", (int)this.type);
			serializer.AddValue("RayDirection", (int)this.rayDirection);
			serializer.AddValue("heightMask", this.heightMask);
			serializer.AddValue("fromHeight", this.fromHeight);
			serializer.AddValue("thickRaycastDiameter", this.thickRaycastDiameter);
			serializer.AddValue("thickRaycast", this.thickRaycast);
			serializer.AddValue("collisionCheck", this.collisionCheck);
			serializer.AddValue("heightCheck", this.heightCheck);
			serializer.AddValue("unwalkableWhenNoGround", this.unwalkableWhenNoGround);
			serializer.AddValue("collisionOffset", this.collisionOffset);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00019D18 File Offset: 0x00018118
		public void DeSerializeSettings(AstarSerializer serializer)
		{
			this.mask.value = (int)serializer.GetValue("Mask", typeof(int), null);
			this.diameter = (float)serializer.GetValue("Diameter", typeof(float), null);
			this.height = (float)serializer.GetValue("Height", typeof(float), null);
			this.type = (ColliderType)serializer.GetValue("Type", typeof(int), null);
			this.rayDirection = (RayDirection)serializer.GetValue("RayDirection", typeof(int), null);
			this.heightMask.value = (int)serializer.GetValue("heightMask", typeof(int), -1);
			this.fromHeight = (float)serializer.GetValue("fromHeight", typeof(float), 100f);
			this.thickRaycastDiameter = (float)serializer.GetValue("thickRaycastDiameter", typeof(float), null);
			this.thickRaycast = (bool)serializer.GetValue("thickRaycast", typeof(bool), null);
			this.collisionCheck = (bool)serializer.GetValue("collisionCheck", typeof(bool), true);
			this.heightCheck = (bool)serializer.GetValue("heightCheck", typeof(bool), true);
			this.unwalkableWhenNoGround = (bool)serializer.GetValue("unwalkableWhenNoGround", typeof(bool), true);
			this.collisionOffset = (float)serializer.GetValue("collisionOffset", typeof(float), 0f);
			if (this.fromHeight == 0f)
			{
				this.fromHeight = 100f;
			}
		}

		// Token: 0x040002B0 RID: 688
		public ColliderType type = ColliderType.Capsule;

		// Token: 0x040002B1 RID: 689
		public float diameter = 1f;

		// Token: 0x040002B2 RID: 690
		public float height = 2f;

		// Token: 0x040002B3 RID: 691
		public float collisionOffset;

		// Token: 0x040002B4 RID: 692
		public RayDirection rayDirection = RayDirection.Both;

		// Token: 0x040002B5 RID: 693
		public LayerMask mask;

		// Token: 0x040002B6 RID: 694
		public LayerMask heightMask = -1;

		// Token: 0x040002B7 RID: 695
		public float fromHeight = 100f;

		// Token: 0x040002B8 RID: 696
		public bool thickRaycast;

		// Token: 0x040002B9 RID: 697
		public float thickRaycastDiameter = 1f;

		// Token: 0x040002BA RID: 698
		public Vector3 up;

		// Token: 0x040002BB RID: 699
		private Vector3 upheight;

		// Token: 0x040002BC RID: 700
		private float finalRadius;

		// Token: 0x040002BD RID: 701
		private float finalRaycastRadius;

		// Token: 0x040002BE RID: 702
		public const float RaycastErrorMargin = 0.005f;

		// Token: 0x040002BF RID: 703
		public bool collisionCheck = true;

		// Token: 0x040002C0 RID: 704
		public bool heightCheck = true;

		// Token: 0x040002C1 RID: 705
		public bool unwalkableWhenNoGround = true;
	}
}
