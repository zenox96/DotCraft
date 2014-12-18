using System;

namespace DotCraftCore.nEntity.nItem
{

	using Entity = DotCraftCore.nEntity.Entity;
	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class EntityEnderEye : Entity
	{
	/// <summary> 'x' location the eye should float towards.  </summary>
		private double targetX;

	/// <summary> 'y' location the eye should float towards.  </summary>
		private double targetY;

	/// <summary> 'z' location the eye should float towards.  </summary>
		private double targetZ;
		private int despawnTimer;
		private bool shatterOrDrop;
		

		public EntityEnderEye(World p_i1757_1_) : base(p_i1757_1_)
		{
			this.setSize(0.25F, 0.25F);
		}

		protected internal override void entityInit()
		{
		}

///    
///     <summary> * Checks if the entity is in range to render by using the past in distance and comparing it to its average edge
///     * length * 64 * renderDistanceWeight Args: distance </summary>
///     
		public override bool isInRangeToRenderDist(double p_70112_1_)
		{
			double var3 = this.boundingBox.AverageEdgeLength * 4.0D;
			var3 *= 64.0D;
			return p_70112_1_ < var3 * var3;
		}

		public EntityEnderEye(World p_i1758_1_, double p_i1758_2_, double p_i1758_4_, double p_i1758_6_) : base(p_i1758_1_)
		{
			this.despawnTimer = 0;
			this.setSize(0.25F, 0.25F);
			this.setPosition(p_i1758_2_, p_i1758_4_, p_i1758_6_);
			this.yOffset = 0.0F;
		}

///    
///     <summary> * The location the eye should float/move towards. Currently used for moving towards the nearest stronghold. Args:
///     * strongholdX, strongholdY, strongholdZ </summary>
///     
		public virtual void moveTowards(double p_70220_1_, int p_70220_3_, double p_70220_4_)
		{
			double var6 = p_70220_1_ - this.posX;
			double var8 = p_70220_4_ - this.posZ;
			float var10 = MathHelper.sqrt_double(var6 * var6 + var8 * var8);

			if (var10 > 12.0F)
			{
				this.targetX = this.posX + var6 / (double)var10 * 12.0D;
				this.targetZ = this.posZ + var8 / (double)var10 * 12.0D;
				this.targetY = this.posY + 8.0D;
			}
			else
			{
				this.targetX = p_70220_1_;
				this.targetY = (double)p_70220_3_;
				this.targetZ = p_70220_4_;
			}

			this.despawnTimer = 0;
			this.shatterOrDrop = this.rand.Next(5) > 0;
		}

///    
///     <summary> * Sets the velocity to the args. Args: x, y, z </summary>
///     
		public override void setVelocity(double p_70016_1_, double p_70016_3_, double p_70016_5_)
		{
			this.motionX = p_70016_1_;
			this.motionY = p_70016_3_;
			this.motionZ = p_70016_5_;

			if (this.prevRotationPitch == 0.0F && this.prevRotationYaw == 0.0F)
			{
				float var7 = MathHelper.sqrt_double(p_70016_1_ * p_70016_1_ + p_70016_5_ * p_70016_5_);
				this.prevRotationYaw = this.rotationYaw = (float)(Math.Atan2(p_70016_1_, p_70016_5_) * 180.0D / Math.PI);
				this.prevRotationPitch = this.rotationPitch = (float)(Math.Atan2(p_70016_3_, (double)var7) * 180.0D / Math.PI);
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			this.lastTickPosX = this.posX;
			this.lastTickPosY = this.posY;
			this.lastTickPosZ = this.posZ;
			base.onUpdate();
			this.posX += this.motionX;
			this.posY += this.motionY;
			this.posZ += this.motionZ;
			float var1 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
			this.rotationYaw = (float)(Math.Atan2(this.motionX, this.motionZ) * 180.0D / Math.PI);

			for (this.rotationPitch = (float)(Math.Atan2(this.motionY, (double)var1) * 180.0D / Math.PI); this.rotationPitch - this.prevRotationPitch < -180.0F; this.prevRotationPitch -= 360.0F)
			{
				;
			}

			while (this.rotationPitch - this.prevRotationPitch >= 180.0F)
			{
				this.prevRotationPitch += 360.0F;
			}

			while (this.rotationYaw - this.prevRotationYaw < -180.0F)
			{
				this.prevRotationYaw -= 360.0F;
			}

			while (this.rotationYaw - this.prevRotationYaw >= 180.0F)
			{
				this.prevRotationYaw += 360.0F;
			}

			this.rotationPitch = this.prevRotationPitch + (this.rotationPitch - this.prevRotationPitch) * 0.2F;
			this.rotationYaw = this.prevRotationYaw + (this.rotationYaw - this.prevRotationYaw) * 0.2F;

			if (!this.worldObj.isClient)
			{
				double var2 = this.targetX - this.posX;
				double var4 = this.targetZ - this.posZ;
				float var6 = (float)Math.Sqrt(var2 * var2 + var4 * var4);
				float var7 = (float)Math.Atan2(var4, var2);
				double var8 = (double)var1 + (double)(var6 - var1) * 0.0025D;

				if (var6 < 1.0F)
				{
					var8 *= 0.8D;
					this.motionY *= 0.8D;
				}

				this.motionX = Math.Cos((double)var7) * var8;
				this.motionZ = Math.Sin((double)var7) * var8;

				if (this.posY < this.targetY)
				{
					this.motionY += (1.0D - this.motionY) * 0.014999999664723873D;
				}
				else
				{
					this.motionY += (-1.0D - this.motionY) * 0.014999999664723873D;
				}
			}

			float var10 = 0.25F;

			if (this.InWater)
			{
				for (int var3 = 0; var3 < 4; ++var3)
				{
					this.worldObj.spawnParticle("bubble", this.posX - this.motionX * (double)var10, this.posY - this.motionY * (double)var10, this.posZ - this.motionZ * (double)var10, this.motionX, this.motionY, this.motionZ);
				}
			}
			else
			{
				this.worldObj.spawnParticle("portal", this.posX - this.motionX * (double)var10 + this.rand.NextDouble() * 0.6D - 0.3D, this.posY - this.motionY * (double)var10 - 0.5D, this.posZ - this.motionZ * (double)var10 + this.rand.NextDouble() * 0.6D - 0.3D, this.motionX, this.motionY, this.motionZ);
			}

			if (!this.worldObj.isClient)
			{
				this.setPosition(this.posX, this.posY, this.posZ);
				++this.despawnTimer;

				if (this.despawnTimer > 80 && !this.worldObj.isClient)
				{
					this.setDead();

					if (this.shatterOrDrop)
					{
						this.worldObj.spawnEntityInWorld(new EntityItem(this.worldObj, this.posX, this.posY, this.posZ, new ItemStack(Items.ender_eye)));
					}
					else
					{
						this.worldObj.playAuxSFX(2003, (int)Math.Round(this.posX), (int)Math.Round(this.posY), (int)Math.Round(this.posZ), 0);
					}
				}
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Gets how bright this entity is. </summary>
///     
		public override float getBrightness(float p_70013_1_)
		{
			return 1.0F;
		}

		public override int getBrightnessForRender(float p_70070_1_)
		{
			return 15728880;
		}

///    
///     <summary> * If returns false, the item will not inflict any damage against entities. </summary>
///     
		public override bool canAttackWithItem()
		{
			return false;
		}
	}

}