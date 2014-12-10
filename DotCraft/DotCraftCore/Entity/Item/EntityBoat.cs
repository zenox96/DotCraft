using System;
using System.Collections;

namespace DotCraftCore.Entity.Item
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class EntityBoat : Entity
	{
	/// <summary> true if no player in boat  </summary>
		private bool isBoatEmpty;
		private double speedMultiplier;
		private int boatPosRotationIncrements;
		private double boatX;
		private double boatY;
		private double boatZ;
		private double boatYaw;
		private double boatPitch;
		private double velocityX;
		private double velocityY;
		private double velocityZ;
		private const string __OBFID = "CL_00001667";

		public EntityBoat(World p_i1704_1_) : base(p_i1704_1_)
		{
			this.isBoatEmpty = true;
			this.speedMultiplier = 0.07D;
			this.preventEntitySpawning = true;
			this.setSize(1.5F, 0.6F);
			this.yOffset = this.height / 2.0F;
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(17, new int?(0));
			this.dataWatcher.addObject(18, new int?(1));
			this.dataWatcher.addObject(19, new float?(0.0F));
		}

///    
///     <summary> * Returns a boundingBox used to collide the entity with other entities and blocks. This enables the entity to be
///     * pushable on contact, like boats or minecarts. </summary>
///     
		public override AxisAlignedBB getCollisionBox(Entity p_70114_1_)
		{
			return p_70114_1_.boundingBox;
		}

///    
///     <summary> * returns the bounding box for this entity </summary>
///     
		public override AxisAlignedBB BoundingBox
		{
			get
			{
				return this.boundingBox;
			}
		}

///    
///     <summary> * Returns true if this entity should push and be pushed by other entities when colliding. </summary>
///     
		public override bool canBePushed()
		{
			return true;
		}

		public EntityBoat(World p_i1705_1_, double p_i1705_2_, double p_i1705_4_, double p_i1705_6_) : this(p_i1705_1_)
		{
			this.setPosition(p_i1705_2_, p_i1705_4_ + (double)this.yOffset, p_i1705_6_);
			this.motionX = 0.0D;
			this.motionY = 0.0D;
			this.motionZ = 0.0D;
			this.prevPosX = p_i1705_2_;
			this.prevPosY = p_i1705_4_;
			this.prevPosZ = p_i1705_6_;
		}

///    
///     <summary> * Returns the Y offset from the entity's position for any entity riding this one. </summary>
///     
		public override double MountedYOffset
		{
			get
			{
				return (double)this.height * 0.0D - 0.30000001192092896D;
			}
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (this.EntityInvulnerable)
			{
				return false;
			}
			else if (!this.worldObj.isClient && !this.isDead)
			{
				this.ForwardDirection = -this.ForwardDirection;
				this.TimeSinceHit = 10;
				this.DamageTaken = this.DamageTaken + p_70097_2_ * 10.0F;
				this.setBeenAttacked();
				bool var3 = p_70097_1_.Entity is EntityPlayer && ((EntityPlayer)p_70097_1_.Entity).capabilities.isCreativeMode;

				if (var3 || this.DamageTaken > 40.0F)
				{
					if (this.riddenByEntity != null)
					{
						this.riddenByEntity.mountEntity(this);
					}

					if (!var3)
					{
						this.func_145778_a(Items.boat, 1, 0.0F);
					}

					this.setDead();
				}

				return true;
			}
			else
			{
				return true;
			}
		}

///    
///     <summary> * Setups the entity to do the hurt animation. Only used by packets in multiplayer. </summary>
///     
		public override void performHurtAnimation()
		{
			this.ForwardDirection = -this.ForwardDirection;
			this.TimeSinceHit = 10;
			this.DamageTaken = this.DamageTaken * 11.0F;
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

///    
///     <summary> * Sets the position and rotation. Only difference from the other one is no bounding on the rotation. Args: posX,
///     * posY, posZ, yaw, pitch </summary>
///     
		public override void setPositionAndRotation2(double p_70056_1_, double p_70056_3_, double p_70056_5_, float p_70056_7_, float p_70056_8_, int p_70056_9_)
		{
			if (this.isBoatEmpty)
			{
				this.boatPosRotationIncrements = p_70056_9_ + 5;
			}
			else
			{
				double var10 = p_70056_1_ - this.posX;
				double var12 = p_70056_3_ - this.posY;
				double var14 = p_70056_5_ - this.posZ;
				double var16 = var10 * var10 + var12 * var12 + var14 * var14;

				if (var16 <= 1.0D)
				{
					return;
				}

				this.boatPosRotationIncrements = 3;
			}

			this.boatX = p_70056_1_;
			this.boatY = p_70056_3_;
			this.boatZ = p_70056_5_;
			this.boatYaw = (double)p_70056_7_;
			this.boatPitch = (double)p_70056_8_;
			this.motionX = this.velocityX;
			this.motionY = this.velocityY;
			this.motionZ = this.velocityZ;
		}

///    
///     <summary> * Sets the velocity to the args. Args: x, y, z </summary>
///     
		public override void setVelocity(double p_70016_1_, double p_70016_3_, double p_70016_5_)
		{
			this.velocityX = this.motionX = p_70016_1_;
			this.velocityY = this.motionY = p_70016_3_;
			this.velocityZ = this.motionZ = p_70016_5_;
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (this.TimeSinceHit > 0)
			{
				this.TimeSinceHit = this.TimeSinceHit - 1;
			}

			if (this.DamageTaken > 0.0F)
			{
				this.DamageTaken = this.DamageTaken - 1.0F;
			}

			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			sbyte var1 = 5;
			double var2 = 0.0D;

			for (int var4 = 0; var4 < var1; ++var4)
			{
				double var5 = this.boundingBox.minY + (this.boundingBox.maxY - this.boundingBox.minY) * (double)(var4 + 0) / (double)var1 - 0.125D;
				double var7 = this.boundingBox.minY + (this.boundingBox.maxY - this.boundingBox.minY) * (double)(var4 + 1) / (double)var1 - 0.125D;
				AxisAlignedBB var9 = AxisAlignedBB.getBoundingBox(this.boundingBox.minX, var5, this.boundingBox.minZ, this.boundingBox.maxX, var7, this.boundingBox.maxZ);

				if (this.worldObj.isAABBInMaterial(var9, Material.water))
				{
					var2 += 1.0D / (double)var1;
				}
			}

			double var19 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);
			double var6;
			double var8;
			int var10;

			if (var19 > 0.26249999999999996D)
			{
				var6 = Math.Cos((double)this.rotationYaw * Math.PI / 180.0D);
				var8 = Math.Sin((double)this.rotationYaw * Math.PI / 180.0D);

				for (var10 = 0; (double)var10 < 1.0D + var19 * 60.0D; ++var10)
				{
					double var11 = (double)(this.rand.nextFloat() * 2.0F - 1.0F);
					double var13 = (double)(this.rand.Next(2) * 2 - 1) * 0.7D;
					double var15;
					double var17;

					if (this.rand.nextBoolean())
					{
						var15 = this.posX - var6 * var11 * 0.8D + var8 * var13;
						var17 = this.posZ - var8 * var11 * 0.8D - var6 * var13;
						this.worldObj.spawnParticle("splash", var15, this.posY - 0.125D, var17, this.motionX, this.motionY, this.motionZ);
					}
					else
					{
						var15 = this.posX + var6 + var8 * var11 * 0.7D;
						var17 = this.posZ + var8 - var6 * var11 * 0.7D;
						this.worldObj.spawnParticle("splash", var15, this.posY - 0.125D, var17, this.motionX, this.motionY, this.motionZ);
					}
				}
			}

			double var24;
			double var26;

			if (this.worldObj.isClient && this.isBoatEmpty)
			{
				if (this.boatPosRotationIncrements > 0)
				{
					var6 = this.posX + (this.boatX - this.posX) / (double)this.boatPosRotationIncrements;
					var8 = this.posY + (this.boatY - this.posY) / (double)this.boatPosRotationIncrements;
					var24 = this.posZ + (this.boatZ - this.posZ) / (double)this.boatPosRotationIncrements;
					var26 = MathHelper.wrapAngleTo180_double(this.boatYaw - (double)this.rotationYaw);
					this.rotationYaw = (float)((double)this.rotationYaw + var26 / (double)this.boatPosRotationIncrements);
					this.rotationPitch = (float)((double)this.rotationPitch + (this.boatPitch - (double)this.rotationPitch) / (double)this.boatPosRotationIncrements);
					--this.boatPosRotationIncrements;
					this.setPosition(var6, var8, var24);
					this.setRotation(this.rotationYaw, this.rotationPitch);
				}
				else
				{
					var6 = this.posX + this.motionX;
					var8 = this.posY + this.motionY;
					var24 = this.posZ + this.motionZ;
					this.setPosition(var6, var8, var24);

					if (this.onGround)
					{
						this.motionX *= 0.5D;
						this.motionY *= 0.5D;
						this.motionZ *= 0.5D;
					}

					this.motionX *= 0.9900000095367432D;
					this.motionY *= 0.949999988079071D;
					this.motionZ *= 0.9900000095367432D;
				}
			}
			else
			{
				if (var2 < 1.0D)
				{
					var6 = var2 * 2.0D - 1.0D;
					this.motionY += 0.03999999910593033D * var6;
				}
				else
				{
					if (this.motionY < 0.0D)
					{
						this.motionY /= 2.0D;
					}

					this.motionY += 0.007000000216066837D;
				}

				if (this.riddenByEntity != null && this.riddenByEntity is EntityLivingBase)
				{
					EntityLivingBase var20 = (EntityLivingBase)this.riddenByEntity;
					float var21 = this.riddenByEntity.rotationYaw + -var20.moveStrafing * 90.0F;
					this.motionX += -Math.Sin((double)(var21 * (float)Math.PI / 180.0F)) * this.speedMultiplier * (double)var20.moveForward * 0.05000000074505806D;
					this.motionZ += Math.Cos((double)(var21 * (float)Math.PI / 180.0F)) * this.speedMultiplier * (double)var20.moveForward * 0.05000000074505806D;
				}

				var6 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);

				if (var6 > 0.35D)
				{
					var8 = 0.35D / var6;
					this.motionX *= var8;
					this.motionZ *= var8;
					var6 = 0.35D;
				}

				if (var6 > var19 && this.speedMultiplier < 0.35D)
				{
					this.speedMultiplier += (0.35D - this.speedMultiplier) / 35.0D;

					if (this.speedMultiplier > 0.35D)
					{
						this.speedMultiplier = 0.35D;
					}
				}
				else
				{
					this.speedMultiplier -= (this.speedMultiplier - 0.07D) / 35.0D;

					if (this.speedMultiplier < 0.07D)
					{
						this.speedMultiplier = 0.07D;
					}
				}

				int var22;

				for (var22 = 0; var22 < 4; ++var22)
				{
					int var23 = MathHelper.floor_double(this.posX + ((double)(var22 % 2) - 0.5D) * 0.8D);
					var10 = MathHelper.floor_double(this.posZ + ((double)(var22 / 2) - 0.5D) * 0.8D);

					for (int var25 = 0; var25 < 2; ++var25)
					{
						int var12 = MathHelper.floor_double(this.posY) + var25;
						Block var27 = this.worldObj.getBlock(var23, var12, var10);

						if (var27 == Blocks.snow_layer)
						{
							this.worldObj.setBlockToAir(var23, var12, var10);
							this.isCollidedHorizontally = false;
						}
						else if (var27 == Blocks.waterlily)
						{
							this.worldObj.func_147480_a(var23, var12, var10, true);
							this.isCollidedHorizontally = false;
						}
					}
				}

				if (this.onGround)
				{
					this.motionX *= 0.5D;
					this.motionY *= 0.5D;
					this.motionZ *= 0.5D;
				}

				this.moveEntity(this.motionX, this.motionY, this.motionZ);

				if (this.isCollidedHorizontally && var19 > 0.2D)
				{
					if (!this.worldObj.isClient && !this.isDead)
					{
						this.setDead();

						for (var22 = 0; var22 < 3; ++var22)
						{
							this.func_145778_a(Item.getItemFromBlock(Blocks.planks), 1, 0.0F);
						}

						for (var22 = 0; var22 < 2; ++var22)
						{
							this.func_145778_a(Items.stick, 1, 0.0F);
						}
					}
				}
				else
				{
					this.motionX *= 0.9900000095367432D;
					this.motionY *= 0.949999988079071D;
					this.motionZ *= 0.9900000095367432D;
				}

				this.rotationPitch = 0.0F;
				var8 = (double)this.rotationYaw;
				var24 = this.prevPosX - this.posX;
				var26 = this.prevPosZ - this.posZ;

				if (var24 * var24 + var26 * var26 > 0.001D)
				{
					var8 = (double)((float)(Math.Atan2(var26, var24) * 180.0D / Math.PI));
				}

				double var14 = MathHelper.wrapAngleTo180_double(var8 - (double)this.rotationYaw);

				if (var14 > 20.0D)
				{
					var14 = 20.0D;
				}

				if (var14 < -20.0D)
				{
					var14 = -20.0D;
				}

				this.rotationYaw = (float)((double)this.rotationYaw + var14);
				this.setRotation(this.rotationYaw, this.rotationPitch);

				if (!this.worldObj.isClient)
				{
					IList var16 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand(0.20000000298023224D, 0.0D, 0.20000000298023224D));

					if (var16 != null && !var16.Count == 0)
					{
						for (int var28 = 0; var28 < var16.Count; ++var28)
						{
							Entity var18 = (Entity)var16[var28];

							if (var18 != this.riddenByEntity && var18.canBePushed() && var18 is EntityBoat)
							{
								var18.applyEntityCollision(this);
							}
						}
					}

					if (this.riddenByEntity != null && this.riddenByEntity.isDead)
					{
						this.riddenByEntity = null;
					}
				}
			}
		}

		public override void updateRiderPosition()
		{
			if (this.riddenByEntity != null)
			{
				double var1 = Math.Cos((double)this.rotationYaw * Math.PI / 180.0D) * 0.4D;
				double var3 = Math.Sin((double)this.rotationYaw * Math.PI / 180.0D) * 0.4D;
				this.riddenByEntity.setPosition(this.posX + var1, this.posY + this.MountedYOffset + this.riddenByEntity.YOffset, this.posZ + var3);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
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
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (this.riddenByEntity != null && this.riddenByEntity is EntityPlayer && this.riddenByEntity != p_130002_1_)
			{
				return true;
			}
			else
			{
				if (!this.worldObj.isClient)
				{
					p_130002_1_.mountEntity(this);
				}

				return true;
			}
		}

///    
///     <summary> * Takes in the distance the entity has fallen this tick and whether its on the ground to update the fall distance
///     * and deal fall damage if landing on the ground.  Args: distanceFallenThisTick, onGround </summary>
///     
		protected internal override void updateFallState(double p_70064_1_, bool p_70064_3_)
		{
			int var4 = MathHelper.floor_double(this.posX);
			int var5 = MathHelper.floor_double(this.posY);
			int var6 = MathHelper.floor_double(this.posZ);

			if (p_70064_3_)
			{
				if (this.fallDistance > 3.0F)
				{
					this.fall(this.fallDistance);

					if (!this.worldObj.isClient && !this.isDead)
					{
						this.setDead();
						int var7;

						for (var7 = 0; var7 < 3; ++var7)
						{
							this.func_145778_a(Item.getItemFromBlock(Blocks.planks), 1, 0.0F);
						}

						for (var7 = 0; var7 < 2; ++var7)
						{
							this.func_145778_a(Items.stick, 1, 0.0F);
						}
					}

					this.fallDistance = 0.0F;
				}
			}
			else if (this.worldObj.getBlock(var4, var5 - 1, var6).Material != Material.water && p_70064_1_ < 0.0D)
			{
				this.fallDistance = (float)((double)this.fallDistance - p_70064_1_);
			}
		}

///    
///     <summary> * Sets the damage taken from the last hit. </summary>
///     
		public virtual float DamageTaken
		{
			set
			{
				this.dataWatcher.updateObject(19, Convert.ToSingle(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectFloat(19);
			}
		}

///    
///     <summary> * Gets the damage taken from the last hit. </summary>
///     

///    
///     <summary> * Sets the time to count down from since the last time entity was hit. </summary>
///     
		public virtual int TimeSinceHit
		{
			set
			{
				this.dataWatcher.updateObject(17, Convert.ToInt32(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(17);
			}
		}

///    
///     <summary> * Gets the time since the last hit. </summary>
///     

///    
///     <summary> * Sets the forward direction of the entity. </summary>
///     
		public virtual int ForwardDirection
		{
			set
			{
				this.dataWatcher.updateObject(18, Convert.ToInt32(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(18);
			}
		}

///    
///     <summary> * Gets the forward direction of the entity. </summary>
///     

///    
///     <summary> * true if no player in boat </summary>
///     
		public virtual bool IsBoatEmpty
		{
			set
			{
				this.isBoatEmpty = value;
			}
		}
	}

}