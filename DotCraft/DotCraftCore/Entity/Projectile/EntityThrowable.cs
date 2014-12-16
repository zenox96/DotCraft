using System;
using System.Collections;

namespace DotCraftCore.Entity.Projectile
{

	using Block = DotCraftCore.block.Block;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IProjectile = DotCraftCore.Entity.IProjectile;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using Vec3 = DotCraftCore.Util.Vec3;
	using World = DotCraftCore.World.World;

	public abstract class EntityThrowable : Entity, IProjectile
	{
		private int field_145788_c = -1;
		private int field_145786_d = -1;
		private int field_145787_e = -1;
		private Block field_145785_f;
		protected internal bool inGround;
		public int throwableShake;

	/// <summary> The entity that threw this throwable item.  </summary>
		private EntityLivingBase thrower;
		private string throwerName;
		private int ticksInGround;
		private int ticksInAir;
		

		public EntityThrowable(World p_i1776_1_) : base(p_i1776_1_)
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

		public EntityThrowable(World p_i1777_1_, EntityLivingBase p_i1777_2_) : base(p_i1777_1_)
		{
			this.thrower = p_i1777_2_;
			this.setSize(0.25F, 0.25F);
			this.setLocationAndAngles(p_i1777_2_.posX, p_i1777_2_.posY + (double)p_i1777_2_.EyeHeight, p_i1777_2_.posZ, p_i1777_2_.rotationYaw, p_i1777_2_.rotationPitch);
			this.posX -= (double)(MathHelper.cos(this.rotationYaw / 180.0F * (float)Math.PI) * 0.16F);
			this.posY -= 0.10000000149011612D;
			this.posZ -= (double)(MathHelper.sin(this.rotationYaw / 180.0F * (float)Math.PI) * 0.16F);
			this.setPosition(this.posX, this.posY, this.posZ);
			this.yOffset = 0.0F;
			float var3 = 0.4F;
			this.motionX = (double)(-MathHelper.sin(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI) * var3);
			this.motionZ = (double)(MathHelper.cos(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI) * var3);
			this.motionY = (double)(-MathHelper.sin((this.rotationPitch + this.func_70183_g()) / 180.0F * (float)Math.PI) * var3);
			this.setThrowableHeading(this.motionX, this.motionY, this.motionZ, this.func_70182_d(), 1.0F);
		}

		public EntityThrowable(World p_i1778_1_, double p_i1778_2_, double p_i1778_4_, double p_i1778_6_) : base(p_i1778_1_)
		{
			this.ticksInGround = 0;
			this.setSize(0.25F, 0.25F);
			this.setPosition(p_i1778_2_, p_i1778_4_, p_i1778_6_);
			this.yOffset = 0.0F;
		}

		protected internal virtual float func_70182_d()
		{
			return 1.5F;
		}

		protected internal virtual float func_70183_g()
		{
			return 0.0F;
		}

///    
///     <summary> * Similar to setArrowHeading, it's point the throwable entity to a x, y, z direction. </summary>
///     
		public virtual void setThrowableHeading(double p_70186_1_, double p_70186_3_, double p_70186_5_, float p_70186_7_, float p_70186_8_)
		{
			float var9 = MathHelper.sqrt_double(p_70186_1_ * p_70186_1_ + p_70186_3_ * p_70186_3_ + p_70186_5_ * p_70186_5_);
			p_70186_1_ /= (double)var9;
			p_70186_3_ /= (double)var9;
			p_70186_5_ /= (double)var9;
			p_70186_1_ += this.rand.nextGaussian() * 0.007499999832361937D * (double)p_70186_8_;
			p_70186_3_ += this.rand.nextGaussian() * 0.007499999832361937D * (double)p_70186_8_;
			p_70186_5_ += this.rand.nextGaussian() * 0.007499999832361937D * (double)p_70186_8_;
			p_70186_1_ *= (double)p_70186_7_;
			p_70186_3_ *= (double)p_70186_7_;
			p_70186_5_ *= (double)p_70186_7_;
			this.motionX = p_70186_1_;
			this.motionY = p_70186_3_;
			this.motionZ = p_70186_5_;
			float var10 = MathHelper.sqrt_double(p_70186_1_ * p_70186_1_ + p_70186_5_ * p_70186_5_);
			this.prevRotationYaw = this.rotationYaw = (float)(Math.Atan2(p_70186_1_, p_70186_5_) * 180.0D / Math.PI);
			this.prevRotationPitch = this.rotationPitch = (float)(Math.Atan2(p_70186_3_, (double)var10) * 180.0D / Math.PI);
			this.ticksInGround = 0;
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

			if (this.throwableShake > 0)
			{
				--this.throwableShake;
			}

			if (this.inGround)
			{
				if (this.worldObj.getBlock(this.field_145788_c, this.field_145786_d, this.field_145787_e) == this.field_145785_f)
				{
					++this.ticksInGround;

					if (this.ticksInGround == 1200)
					{
						this.setDead();
					}

					return;
				}

				this.inGround = false;
				this.motionX *= (double)(this.rand.nextFloat() * 0.2F);
				this.motionY *= (double)(this.rand.nextFloat() * 0.2F);
				this.motionZ *= (double)(this.rand.nextFloat() * 0.2F);
				this.ticksInGround = 0;
				this.ticksInAir = 0;
			}
			else
			{
				++this.ticksInAir;
			}

			Vec3 var1 = Vec3.createVectorHelper(this.posX, this.posY, this.posZ);
			Vec3 var2 = Vec3.createVectorHelper(this.posX + this.motionX, this.posY + this.motionY, this.posZ + this.motionZ);
			MovingObjectPosition var3 = this.worldObj.rayTraceBlocks(var1, var2);
			var1 = Vec3.createVectorHelper(this.posX, this.posY, this.posZ);
			var2 = Vec3.createVectorHelper(this.posX + this.motionX, this.posY + this.motionY, this.posZ + this.motionZ);

			if (var3 != null)
			{
				var2 = Vec3.createVectorHelper(var3.hitVec.xCoord, var3.hitVec.yCoord, var3.hitVec.zCoord);
			}

			if (!this.worldObj.isClient)
			{
				Entity var4 = null;
				IList var5 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.addCoord(this.motionX, this.motionY, this.motionZ).expand(1.0D, 1.0D, 1.0D));
				double var6 = 0.0D;
				EntityLivingBase var8 = this.Thrower;

				for (int var9 = 0; var9 < var5.Count; ++var9)
				{
					Entity var10 = (Entity)var5[var9];

					if (var10.canBeCollidedWith() && (var10 != var8 || this.ticksInAir >= 5))
					{
						float var11 = 0.3F;
						AxisAlignedBB var12 = var10.boundingBox.expand((double)var11, (double)var11, (double)var11);
						MovingObjectPosition var13 = var12.calculateIntercept(var1, var2);

						if (var13 != null)
						{
							double var14 = var1.distanceTo(var13.hitVec);

							if (var14 < var6 || var6 == 0.0D)
							{
								var4 = var10;
								var6 = var14;
							}
						}
					}
				}

				if (var4 != null)
				{
					var3 = new MovingObjectPosition(var4);
				}
			}

			if (var3 != null)
			{
				if (var3.typeOfHit == MovingObjectPosition.MovingObjectType.BLOCK && this.worldObj.getBlock(var3.blockX, var3.blockY, var3.blockZ) == Blocks.portal)
				{
					this.setInPortal();
				}
				else
				{
					this.onImpact(var3);
				}
			}

			this.posX += this.motionX;
			this.posY += this.motionY;
			this.posZ += this.motionZ;
			float var16 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
			this.rotationYaw = (float)(Math.Atan2(this.motionX, this.motionZ) * 180.0D / Math.PI);

			for (this.rotationPitch = (float)(Math.Atan2(this.motionY, (double)var16) * 180.0D / Math.PI); this.rotationPitch - this.prevRotationPitch < -180.0F; this.prevRotationPitch -= 360.0F)
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
			float var17 = 0.99F;
			float var18 = this.GravityVelocity;

			if (this.InWater)
			{
				for (int var7 = 0; var7 < 4; ++var7)
				{
					float var19 = 0.25F;
					this.worldObj.spawnParticle("bubble", this.posX - this.motionX * (double)var19, this.posY - this.motionY * (double)var19, this.posZ - this.motionZ * (double)var19, this.motionX, this.motionY, this.motionZ);
				}

				var17 = 0.8F;
			}

			this.motionX *= (double)var17;
			this.motionY *= (double)var17;
			this.motionZ *= (double)var17;
			this.motionY -= (double)var18;
			this.setPosition(this.posX, this.posY, this.posZ);
		}

///    
///     <summary> * Gets the amount of gravity to apply to the thrown entity with each tick. </summary>
///     
		protected internal virtual float GravityVelocity
		{
			get
			{
				return 0.03F;
			}
		}

///    
///     <summary> * Called when this EntityThrowable hits a block or entity. </summary>
///     
		protected internal abstract void onImpact(MovingObjectPosition p_70184_1_);

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setShort("xTile", (short)this.field_145788_c);
			p_70014_1_.setShort("yTile", (short)this.field_145786_d);
			p_70014_1_.setShort("zTile", (short)this.field_145787_e);
			p_70014_1_.setByte("inTile", (sbyte)Block.getIdFromBlock(this.field_145785_f));
			p_70014_1_.setByte("shake", (sbyte)this.throwableShake);
			p_70014_1_.setByte("inGround", (sbyte)(this.inGround ? 1 : 0));

			if ((this.throwerName == null || this.throwerName.Length == 0) && this.thrower != null && this.thrower is EntityPlayer)
			{
				this.throwerName = this.thrower.CommandSenderName;
			}

			p_70014_1_.setString("ownerName", this.throwerName == null ? "" : this.throwerName);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			this.field_145788_c = p_70037_1_.getShort("xTile");
			this.field_145786_d = p_70037_1_.getShort("yTile");
			this.field_145787_e = p_70037_1_.getShort("zTile");
			this.field_145785_f = Block.getBlockById(p_70037_1_.getByte("inTile") & 255);
			this.throwableShake = p_70037_1_.getByte("shake") & 255;
			this.inGround = p_70037_1_.getByte("inGround") == 1;
			this.throwerName = p_70037_1_.getString("ownerName");

			if (this.throwerName != null && this.throwerName.Length == 0)
			{
				this.throwerName = null;
			}
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

		public virtual EntityLivingBase Thrower
		{
			get
			{
				if (this.thrower == null && this.throwerName != null && this.throwerName.Length > 0)
				{
					this.thrower = this.worldObj.getPlayerEntityByName(this.throwerName);
				}
	
				return this.thrower;
			}
		}
	}

}