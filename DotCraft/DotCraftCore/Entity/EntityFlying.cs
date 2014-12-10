namespace DotCraftCore.Entity
{

	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public abstract class EntityFlying : EntityLiving
	{
		private const string __OBFID = "CL_00001545";

		public EntityFlying(World p_i1587_1_) : base(p_i1587_1_)
		{
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal virtual void fall(float p_70069_1_)
		{
		}

///    
///     <summary> * Takes in the distance the entity has fallen this tick and whether its on the ground to update the fall distance
///     * and deal fall damage if landing on the ground.  Args: distanceFallenThisTick, onGround </summary>
///     
		protected internal virtual void updateFallState(double p_70064_1_, bool p_70064_3_)
		{
		}

///    
///     <summary> * Moves the entity based on the specified heading.  Args: strafe, forward </summary>
///     
		public virtual void moveEntityWithHeading(float p_70612_1_, float p_70612_2_)
		{
			if (this.InWater)
			{
				this.moveFlying(p_70612_1_, p_70612_2_, 0.02F);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= 0.800000011920929D;
				this.motionY *= 0.800000011920929D;
				this.motionZ *= 0.800000011920929D;
			}
			else if (this.handleLavaMovement())
			{
				this.moveFlying(p_70612_1_, p_70612_2_, 0.02F);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= 0.5D;
				this.motionY *= 0.5D;
				this.motionZ *= 0.5D;
			}
			else
			{
				float var3 = 0.91F;

				if (this.onGround)
				{
					var3 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ)).slipperiness * 0.91F;
				}

				float var4 = 0.16277136F / (var3 * var3 * var3);
				this.moveFlying(p_70612_1_, p_70612_2_, this.onGround ? 0.1F * var4 : 0.02F);
				var3 = 0.91F;

				if (this.onGround)
				{
					var3 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ)).slipperiness * 0.91F;
				}

				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= (double)var3;
				this.motionY *= (double)var3;
				this.motionZ *= (double)var3;
			}

			this.prevLimbSwingAmount = this.limbSwingAmount;
			double var8 = this.posX - this.prevPosX;
			double var5 = this.posZ - this.prevPosZ;
			float var7 = MathHelper.sqrt_double(var8 * var8 + var5 * var5) * 4.0F;

			if (var7 > 1.0F)
			{
				var7 = 1.0F;
			}

			this.limbSwingAmount += (var7 - this.limbSwingAmount) * 0.4F;
			this.limbSwing += this.limbSwingAmount;
		}

///    
///     <summary> * returns true if this entity is by a ladder, false otherwise </summary>
///     
		public virtual bool isOnLadder()
		{
			get
			{
				return false;
			}
		}
	}

}