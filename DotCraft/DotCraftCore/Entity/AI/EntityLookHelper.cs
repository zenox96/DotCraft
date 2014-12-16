using System;

namespace DotCraftCore.Entity.AI
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class EntityLookHelper
	{
		private EntityLiving entity;

///    
///     <summary> * The amount of change that is made each update for an entity facing a direction. </summary>
///     
		private float deltaLookYaw;

///    
///     <summary> * The amount of change that is made each update for an entity facing a direction. </summary>
///     
		private float deltaLookPitch;

	/// <summary> Whether or not the entity is trying to look at something.  </summary>
		private bool isLooking;
		private double posX;
		private double posY;
		private double posZ;
		

		public EntityLookHelper(EntityLiving p_i1613_1_)
		{
			this.entity = p_i1613_1_;
		}

///    
///     <summary> * Sets position to look at using entity </summary>
///     
		public virtual void setLookPositionWithEntity(Entity p_75651_1_, float p_75651_2_, float p_75651_3_)
		{
			this.posX = p_75651_1_.posX;

			if (p_75651_1_ is EntityLivingBase)
			{
				this.posY = p_75651_1_.posY + (double)p_75651_1_.EyeHeight;
			}
			else
			{
				this.posY = (p_75651_1_.boundingBox.minY + p_75651_1_.boundingBox.maxY) / 2.0D;
			}

			this.posZ = p_75651_1_.posZ;
			this.deltaLookYaw = p_75651_2_;
			this.deltaLookPitch = p_75651_3_;
			this.isLooking = true;
		}

///    
///     <summary> * Sets position to look at </summary>
///     
		public virtual void setLookPosition(double p_75650_1_, double p_75650_3_, double p_75650_5_, float p_75650_7_, float p_75650_8_)
		{
			this.posX = p_75650_1_;
			this.posY = p_75650_3_;
			this.posZ = p_75650_5_;
			this.deltaLookYaw = p_75650_7_;
			this.deltaLookPitch = p_75650_8_;
			this.isLooking = true;
		}

///    
///     <summary> * Updates look </summary>
///     
		public virtual void onUpdateLook()
		{
			this.entity.rotationPitch = 0.0F;

			if (this.isLooking)
			{
				this.isLooking = false;
				double var1 = this.posX - this.entity.posX;
				double var3 = this.posY - (this.entity.posY + (double)this.entity.EyeHeight);
				double var5 = this.posZ - this.entity.posZ;
				double var7 = (double)MathHelper.sqrt_double(var1 * var1 + var5 * var5);
				float var9 = (float)(Math.Atan2(var5, var1) * 180.0D / Math.PI) - 90.0F;
				float var10 = (float)(-(Math.Atan2(var3, var7) * 180.0D / Math.PI));
				this.entity.rotationPitch = this.updateRotation(this.entity.rotationPitch, var10, this.deltaLookPitch);
				this.entity.rotationYawHead = this.updateRotation(this.entity.rotationYawHead, var9, this.deltaLookYaw);
			}
			else
			{
				this.entity.rotationYawHead = this.updateRotation(this.entity.rotationYawHead, this.entity.renderYawOffset, 10.0F);
			}

			float var11 = MathHelper.wrapAngleTo180_float(this.entity.rotationYawHead - this.entity.renderYawOffset);

			if (!this.entity.Navigator.noPath())
			{
				if (var11 < -75.0F)
				{
					this.entity.rotationYawHead = this.entity.renderYawOffset - 75.0F;
				}

				if (var11 > 75.0F)
				{
					this.entity.rotationYawHead = this.entity.renderYawOffset + 75.0F;
				}
			}
		}

		private float updateRotation(float p_75652_1_, float p_75652_2_, float p_75652_3_)
		{
			float var4 = MathHelper.wrapAngleTo180_float(p_75652_2_ - p_75652_1_);

			if (var4 > p_75652_3_)
			{
				var4 = p_75652_3_;
			}

			if (var4 < -p_75652_3_)
			{
				var4 = -p_75652_3_;
			}

			return p_75652_1_ + var4;
		}
	}

}