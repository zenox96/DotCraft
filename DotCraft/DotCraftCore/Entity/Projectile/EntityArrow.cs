using System;
using System.Collections;

namespace DotCraftCore.nEntity.nProjectile
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using EnchantmentHelper = DotCraftCore.nEnchantment.EnchantmentHelper;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using IProjectile = DotCraftCore.nEntity.IProjectile;
	using EntityEnderman = DotCraftCore.nEntity.nMonster.EntityEnderman;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using S2BPacketChangeGameState = DotCraftCore.nNetwork.nPlay.nServer.S2BPacketChangeGameState;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using Vec3 = DotCraftCore.nUtil.Vec3;
	using World = DotCraftCore.nWorld.World;

	public class EntityArrow : Entity, IProjectile
	{
		private int field_145791_d = -1;
		private int field_145792_e = -1;
		private int field_145789_f = -1;
		private Block field_145790_g;
		private int inData;
		private bool inGround;

	/// <summary> 1 if the player can pick up the arrow  </summary>
		public int canBePickedUp;

	/// <summary> Seems to be some sort of timer for animating an arrow.  </summary>
		public int arrowShake;

	/// <summary> The owner of this arrow.  </summary>
		public Entity shootingEntity;
		private int ticksInGround;
		private int ticksInAir;
		private double damage = 2.0D;

	/// <summary> The amount of knockback an arrow applies when it hits a mob.  </summary>
		private int knockbackStrength;
		

		public EntityArrow(World p_i1753_1_) : base(p_i1753_1_)
		{
			this.renderDistanceWeight = 10.0D;
			this.setSize(0.5F, 0.5F);
		}

		public EntityArrow(World p_i1754_1_, double p_i1754_2_, double p_i1754_4_, double p_i1754_6_) : base(p_i1754_1_)
		{
			this.renderDistanceWeight = 10.0D;
			this.setSize(0.5F, 0.5F);
			this.setPosition(p_i1754_2_, p_i1754_4_, p_i1754_6_);
			this.yOffset = 0.0F;
		}

		public EntityArrow(World p_i1755_1_, EntityLivingBase p_i1755_2_, EntityLivingBase p_i1755_3_, float p_i1755_4_, float p_i1755_5_) : base(p_i1755_1_)
		{
			this.renderDistanceWeight = 10.0D;
			this.shootingEntity = p_i1755_2_;

			if (p_i1755_2_ is EntityPlayer)
			{
				this.canBePickedUp = 1;
			}

			this.posY = p_i1755_2_.posY + (double)p_i1755_2_.EyeHeight - 0.10000000149011612D;
			double var6 = p_i1755_3_.posX - p_i1755_2_.posX;
			double var8 = p_i1755_3_.boundingBox.minY + (double)(p_i1755_3_.height / 3.0F) - this.posY;
			double var10 = p_i1755_3_.posZ - p_i1755_2_.posZ;
			double var12 = (double)MathHelper.sqrt_double(var6 * var6 + var10 * var10);

			if (var12 >= 1.0E-7D)
			{
				float var14 = (float)(Math.Atan2(var10, var6) * 180.0D / Math.PI) - 90.0F;
				float var15 = (float)(-(Math.Atan2(var8, var12) * 180.0D / Math.PI));
				double var16 = var6 / var12;
				double var18 = var10 / var12;
				this.setLocationAndAngles(p_i1755_2_.posX + var16, this.posY, p_i1755_2_.posZ + var18, var14, var15);
				this.yOffset = 0.0F;
				float var20 = (float)var12 * 0.2F;
				this.setThrowableHeading(var6, var8 + (double)var20, var10, p_i1755_4_, p_i1755_5_);
			}
		}

		public EntityArrow(World p_i1756_1_, EntityLivingBase p_i1756_2_, float p_i1756_3_) : base(p_i1756_1_)
		{
			this.renderDistanceWeight = 10.0D;
			this.shootingEntity = p_i1756_2_;

			if (p_i1756_2_ is EntityPlayer)
			{
				this.canBePickedUp = 1;
			}

			this.setSize(0.5F, 0.5F);
			this.setLocationAndAngles(p_i1756_2_.posX, p_i1756_2_.posY + (double)p_i1756_2_.EyeHeight, p_i1756_2_.posZ, p_i1756_2_.rotationYaw, p_i1756_2_.rotationPitch);
			this.posX -= (double)(MathHelper.cos(this.rotationYaw / 180.0F * (float)Math.PI) * 0.16F);
			this.posY -= 0.10000000149011612D;
			this.posZ -= (double)(MathHelper.sin(this.rotationYaw / 180.0F * (float)Math.PI) * 0.16F);
			this.setPosition(this.posX, this.posY, this.posZ);
			this.yOffset = 0.0F;
			this.motionX = (double)(-MathHelper.sin(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI));
			this.motionZ = (double)(MathHelper.cos(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI));
			this.motionY = (double)(-MathHelper.sin(this.rotationPitch / 180.0F * (float)Math.PI));
			this.setThrowableHeading(this.motionX, this.motionY, this.motionZ, p_i1756_3_ * 1.5F, 1.0F);
		}

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte)0));
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
			p_70186_1_ += this.rand.nextGaussian() * (double)(this.rand.nextBoolean() ? -1 : 1) * 0.007499999832361937D * (double)p_70186_8_;
			p_70186_3_ += this.rand.nextGaussian() * (double)(this.rand.nextBoolean() ? -1 : 1) * 0.007499999832361937D * (double)p_70186_8_;
			p_70186_5_ += this.rand.nextGaussian() * (double)(this.rand.nextBoolean() ? -1 : 1) * 0.007499999832361937D * (double)p_70186_8_;
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
///     <summary> * Sets the position and rotation. Only difference from the other one is no bounding on the rotation. Args: posX,
///     * posY, posZ, yaw, pitch </summary>
///     
		public override void setPositionAndRotation2(double p_70056_1_, double p_70056_3_, double p_70056_5_, float p_70056_7_, float p_70056_8_, int p_70056_9_)
		{
			this.setPosition(p_70056_1_, p_70056_3_, p_70056_5_);
			this.setRotation(p_70056_7_, p_70056_8_);
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
				this.prevRotationPitch = this.rotationPitch;
				this.prevRotationYaw = this.rotationYaw;
				this.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
				this.ticksInGround = 0;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (this.prevRotationPitch == 0.0F && this.prevRotationYaw == 0.0F)
			{
				float var1 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
				this.prevRotationYaw = this.rotationYaw = (float)(Math.Atan2(this.motionX, this.motionZ) * 180.0D / Math.PI);
				this.prevRotationPitch = this.rotationPitch = (float)(Math.Atan2(this.motionY, (double)var1) * 180.0D / Math.PI);
			}

			Block var16 = this.worldObj.getBlock(this.field_145791_d, this.field_145792_e, this.field_145789_f);

			if (var16.Material != Material.air)
			{
				var16.setBlockBoundsBasedOnState(this.worldObj, this.field_145791_d, this.field_145792_e, this.field_145789_f);
				AxisAlignedBB var2 = var16.getCollisionBoundingBoxFromPool(this.worldObj, this.field_145791_d, this.field_145792_e, this.field_145789_f);

				if (var2 != null && var2.isVecInside(Vec3.createVectorHelper(this.posX, this.posY, this.posZ)))
				{
					this.inGround = true;
				}
			}

			if (this.arrowShake > 0)
			{
				--this.arrowShake;
			}

			if (this.inGround)
			{
				int var18 = this.worldObj.getBlockMetadata(this.field_145791_d, this.field_145792_e, this.field_145789_f);

				if (var16 == this.field_145790_g && var18 == this.inData)
				{
					++this.ticksInGround;

					if (this.ticksInGround == 1200)
					{
						this.setDead();
					}
				}
				else
				{
					this.inGround = false;
					this.motionX *= (double)(this.rand.nextFloat() * 0.2F);
					this.motionY *= (double)(this.rand.nextFloat() * 0.2F);
					this.motionZ *= (double)(this.rand.nextFloat() * 0.2F);
					this.ticksInGround = 0;
					this.ticksInAir = 0;
				}
			}
			else
			{
				++this.ticksInAir;
				Vec3 var17 = Vec3.createVectorHelper(this.posX, this.posY, this.posZ);
				Vec3 var3 = Vec3.createVectorHelper(this.posX + this.motionX, this.posY + this.motionY, this.posZ + this.motionZ);
				MovingObjectPosition var4 = this.worldObj.func_147447_a(var17, var3, false, true, false);
				var17 = Vec3.createVectorHelper(this.posX, this.posY, this.posZ);
				var3 = Vec3.createVectorHelper(this.posX + this.motionX, this.posY + this.motionY, this.posZ + this.motionZ);

				if (var4 != null)
				{
					var3 = Vec3.createVectorHelper(var4.hitVec.xCoord, var4.hitVec.yCoord, var4.hitVec.zCoord);
				}

				Entity var5 = null;
				IList var6 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.addCoord(this.motionX, this.motionY, this.motionZ).expand(1.0D, 1.0D, 1.0D));
				double var7 = 0.0D;
				int var9;
				float var11;

				for (var9 = 0; var9 < var6.Count; ++var9)
				{
					Entity var10 = (Entity)var6[var9];

					if (var10.canBeCollidedWith() && (var10 != this.shootingEntity || this.ticksInAir >= 5))
					{
						var11 = 0.3F;
						AxisAlignedBB var12 = var10.boundingBox.expand((double)var11, (double)var11, (double)var11);
						MovingObjectPosition var13 = var12.calculateIntercept(var17, var3);

						if (var13 != null)
						{
							double var14 = var17.distanceTo(var13.hitVec);

							if (var14 < var7 || var7 == 0.0D)
							{
								var5 = var10;
								var7 = var14;
							}
						}
					}
				}

				if (var5 != null)
				{
					var4 = new MovingObjectPosition(var5);
				}

				if (var4 != null && var4.entityHit != null && var4.entityHit is EntityPlayer)
				{
					EntityPlayer var19 = (EntityPlayer)var4.entityHit;

					if (var19.capabilities.disableDamage || this.shootingEntity is EntityPlayer && !((EntityPlayer)this.shootingEntity).canAttackPlayer(var19))
					{
						var4 = null;
					}
				}

				float var20;
				float var26;

				if (var4 != null)
				{
					if (var4.entityHit != null)
					{
						var20 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionY * this.motionY + this.motionZ * this.motionZ);
						int var21 = MathHelper.ceiling_double_int((double)var20 * this.damage);

						if (this.IsCritical)
						{
							var21 += this.rand.Next(var21 / 2 + 2);
						}

						DamageSource var23 = null;

						if (this.shootingEntity == null)
						{
							var23 = DamageSource.causeArrowDamage(this, this);
						}
						else
						{
							var23 = DamageSource.causeArrowDamage(this, this.shootingEntity);
						}

						if (this.Burning && !(var4.entityHit is EntityEnderman))
						{
							var4.entityHit.Fire = 5;
						}

						if (var4.entityHit.attackEntityFrom(var23, (float)var21))
						{
							if (var4.entityHit is EntityLivingBase)
							{
								EntityLivingBase var24 = (EntityLivingBase)var4.entityHit;

								if (!this.worldObj.isClient)
								{
									var24.ArrowCountInEntity = var24.ArrowCountInEntity + 1;
								}

								if (this.knockbackStrength > 0)
								{
									var26 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);

									if (var26 > 0.0F)
									{
										var4.entityHit.addVelocity(this.motionX * (double)this.knockbackStrength * 0.6000000238418579D / (double)var26, 0.1D, this.motionZ * (double)this.knockbackStrength * 0.6000000238418579D / (double)var26);
									}
								}

								if (this.shootingEntity != null && this.shootingEntity is EntityLivingBase)
								{
									EnchantmentHelper.func_151384_a(var24, this.shootingEntity);
									EnchantmentHelper.func_151385_b((EntityLivingBase)this.shootingEntity, var24);
								}

								if (this.shootingEntity != null && var4.entityHit != this.shootingEntity && var4.entityHit is EntityPlayer && this.shootingEntity is EntityPlayerMP)
								{
									((EntityPlayerMP)this.shootingEntity).playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(6, 0.0F));
								}
							}

							this.playSound("random.bowhit", 1.0F, 1.2F / (this.rand.nextFloat() * 0.2F + 0.9F));

							if (!(var4.entityHit is EntityEnderman))
							{
								this.setDead();
							}
						}
						else
						{
							this.motionX *= -0.10000000149011612D;
							this.motionY *= -0.10000000149011612D;
							this.motionZ *= -0.10000000149011612D;
							this.rotationYaw += 180.0F;
							this.prevRotationYaw += 180.0F;
							this.ticksInAir = 0;
						}
					}
					else
					{
						this.field_145791_d = var4.blockX;
						this.field_145792_e = var4.blockY;
						this.field_145789_f = var4.blockZ;
						this.field_145790_g = this.worldObj.getBlock(this.field_145791_d, this.field_145792_e, this.field_145789_f);
						this.inData = this.worldObj.getBlockMetadata(this.field_145791_d, this.field_145792_e, this.field_145789_f);
						this.motionX = (double)((float)(var4.hitVec.xCoord - this.posX));
						this.motionY = (double)((float)(var4.hitVec.yCoord - this.posY));
						this.motionZ = (double)((float)(var4.hitVec.zCoord - this.posZ));
						var20 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionY * this.motionY + this.motionZ * this.motionZ);
						this.posX -= this.motionX / (double)var20 * 0.05000000074505806D;
						this.posY -= this.motionY / (double)var20 * 0.05000000074505806D;
						this.posZ -= this.motionZ / (double)var20 * 0.05000000074505806D;
						this.playSound("random.bowhit", 1.0F, 1.2F / (this.rand.nextFloat() * 0.2F + 0.9F));
						this.inGround = true;
						this.arrowShake = 7;
						this.IsCritical = false;

						if (this.field_145790_g.Material != Material.air)
						{
							this.field_145790_g.onEntityCollidedWithBlock(this.worldObj, this.field_145791_d, this.field_145792_e, this.field_145789_f, this);
						}
					}
				}

				if (this.IsCritical)
				{
					for (var9 = 0; var9 < 4; ++var9)
					{
						this.worldObj.spawnParticle("crit", this.posX + this.motionX * (double)var9 / 4.0D, this.posY + this.motionY * (double)var9 / 4.0D, this.posZ + this.motionZ * (double)var9 / 4.0D, -this.motionX, -this.motionY + 0.2D, -this.motionZ);
					}
				}

				this.posX += this.motionX;
				this.posY += this.motionY;
				this.posZ += this.motionZ;
				var20 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
				this.rotationYaw = (float)(Math.Atan2(this.motionX, this.motionZ) * 180.0D / Math.PI);

				for (this.rotationPitch = (float)(Math.Atan2(this.motionY, (double)var20) * 180.0D / Math.PI); this.rotationPitch - this.prevRotationPitch < -180.0F; this.prevRotationPitch -= 360.0F)
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
				float var22 = 0.99F;
				var11 = 0.05F;

				if (this.InWater)
				{
					for (int var25 = 0; var25 < 4; ++var25)
					{
						var26 = 0.25F;
						this.worldObj.spawnParticle("bubble", this.posX - this.motionX * (double)var26, this.posY - this.motionY * (double)var26, this.posZ - this.motionZ * (double)var26, this.motionX, this.motionY, this.motionZ);
					}

					var22 = 0.8F;
				}

				if (this.Wet)
				{
					this.extinguish();
				}

				this.motionX *= (double)var22;
				this.motionY *= (double)var22;
				this.motionZ *= (double)var22;
				this.motionY -= (double)var11;
				this.setPosition(this.posX, this.posY, this.posZ);
				this.func_145775_I();
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setShort("xTile", (short)this.field_145791_d);
			p_70014_1_.setShort("yTile", (short)this.field_145792_e);
			p_70014_1_.setShort("zTile", (short)this.field_145789_f);
			p_70014_1_.setShort("life", (short)this.ticksInGround);
			p_70014_1_.setByte("inTile", (sbyte)Block.getIdFromBlock(this.field_145790_g));
			p_70014_1_.setByte("inData", (sbyte)this.inData);
			p_70014_1_.setByte("shake", (sbyte)this.arrowShake);
			p_70014_1_.setByte("inGround", (sbyte)(this.inGround ? 1 : 0));
			p_70014_1_.setByte("pickup", (sbyte)this.canBePickedUp);
			p_70014_1_.setDouble("damage", this.damage);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			this.field_145791_d = p_70037_1_.getShort("xTile");
			this.field_145792_e = p_70037_1_.getShort("yTile");
			this.field_145789_f = p_70037_1_.getShort("zTile");
			this.ticksInGround = p_70037_1_.getShort("life");
			this.field_145790_g = Block.getBlockById(p_70037_1_.getByte("inTile") & 255);
			this.inData = p_70037_1_.getByte("inData") & 255;
			this.arrowShake = p_70037_1_.getByte("shake") & 255;
			this.inGround = p_70037_1_.getByte("inGround") == 1;

			if (p_70037_1_.func_150297_b("damage", 99))
			{
				this.damage = p_70037_1_.getDouble("damage");
			}

			if (p_70037_1_.func_150297_b("pickup", 99))
			{
				this.canBePickedUp = p_70037_1_.getByte("pickup");
			}
			else if (p_70037_1_.func_150297_b("player", 99))
			{
				this.canBePickedUp = p_70037_1_.getBoolean("player") ? 1 : 0;
			}
		}

///    
///     <summary> * Called by a player entity when they collide with an entity </summary>
///     
		public override void onCollideWithPlayer(EntityPlayer p_70100_1_)
		{
			if (!this.worldObj.isClient && this.inGround && this.arrowShake <= 0)
			{
				bool var2 = this.canBePickedUp == 1 || this.canBePickedUp == 2 && p_70100_1_.capabilities.isCreativeMode;

				if (this.canBePickedUp == 1 && !p_70100_1_.inventory.addItemStackToInventory(new ItemStack(Items.arrow, 1)))
				{
					var2 = false;
				}

				if (var2)
				{
					this.playSound("random.pop", 0.2F, ((this.rand.nextFloat() - this.rand.nextFloat()) * 0.7F + 1.0F) * 2.0F);
					p_70100_1_.onItemPickup(this, 1);
					this.setDead();
				}
			}
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

		public virtual double Damage
		{
			set
			{
				this.damage = value;
			}
			get
			{
				return this.damage;
			}
		}


///    
///     <summary> * Sets the amount of knockback the arrow applies when it hits a mob. </summary>
///     
		public virtual int KnockbackStrength
		{
			set
			{
				this.knockbackStrength = value;
			}
		}

///    
///     <summary> * If returns false, the item will not inflict any damage against entities. </summary>
///     
		public override bool canAttackWithItem()
		{
			return false;
		}

///    
///     <summary> * Whether the arrow has a stream of critical hit particles flying behind it. </summary>
///     
		public virtual bool IsCritical
		{
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
	
				if (value)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 | 1)));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & -2)));
				}
			}
			get
			{
				sbyte var1 = this.dataWatcher.getWatchableObjectByte(16);
				return (var1 & 1) != 0;
			}
		}

///    
///     <summary> * Whether the arrow has a stream of critical hit particles flying behind it. </summary>
///     
	}

}