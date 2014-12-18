using System;

namespace DotCraftCore.nEntity.nAI
{

	using EntityLiving = DotCraftCore.nEntity.EntityLiving;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using MathHelper = DotCraftCore.nUtil.MathHelper;

	public class EntityMoveHelper
	{
	/// <summary> The EntityLiving that is being moved  </summary>
		private EntityLiving entity;
		private double posX;
		private double posY;
		private double posZ;

	/// <summary> The speed at which the entity should move  </summary>
		private double speed;
		private bool update;
		

		public EntityMoveHelper(EntityLiving p_i1614_1_)
		{
			this.entity = p_i1614_1_;
			this.posX = p_i1614_1_.posX;
			this.posY = p_i1614_1_.posY;
			this.posZ = p_i1614_1_.posZ;
		}

		public virtual bool isUpdating()
		{
			get
			{
				return this.update;
			}
		}

		public virtual double Speed
		{
			get
			{
				return this.speed;
			}
		}

///    
///     <summary> * Sets the speed and location to move to </summary>
///     
		public virtual void setMoveTo(double p_75642_1_, double p_75642_3_, double p_75642_5_, double p_75642_7_)
		{
			this.posX = p_75642_1_;
			this.posY = p_75642_3_;
			this.posZ = p_75642_5_;
			this.speed = p_75642_7_;
			this.update = true;
		}

		public virtual void onUpdateMoveHelper()
		{
			this.entity.MoveForward = 0.0F;

			if (this.update)
			{
				this.update = false;
				int var1 = MathHelper.floor_double(this.entity.boundingBox.minY + 0.5D);
				double var2 = this.posX - this.entity.posX;
				double var4 = this.posZ - this.entity.posZ;
				double var6 = this.posY - (double)var1;
				double var8 = var2 * var2 + var6 * var6 + var4 * var4;

				if (var8 >= 2.500000277905201E-7D)
				{
					float var10 = (float)(Math.Atan2(var4, var2) * 180.0D / Math.PI) - 90.0F;
					this.entity.rotationYaw = this.limitAngle(this.entity.rotationYaw, var10, 30.0F);
					this.entity.AIMoveSpeed = (float)(this.speed * this.entity.getEntityAttribute(SharedMonsterAttributes.movementSpeed).AttributeValue);

					if (var6 > 0.0D && var2 * var2 + var4 * var4 < 1.0D)
					{
						this.entity.JumpHelper.setJumping();
					}
				}
			}
		}

///    
///     <summary> * Limits the given angle to a upper and lower limit. </summary>
///     
		private float limitAngle(float p_75639_1_, float p_75639_2_, float p_75639_3_)
		{
			float var4 = MathHelper.wrapAngleTo180_float(p_75639_2_ - p_75639_1_);

			if (var4 > p_75639_3_)
			{
				var4 = p_75639_3_;
			}

			if (var4 < -p_75639_3_)
			{
				var4 = -p_75639_3_;
			}

			return p_75639_1_ + var4;
		}
	}

}