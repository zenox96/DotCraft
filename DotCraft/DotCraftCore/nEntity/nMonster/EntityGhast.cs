using System;

namespace DotCraftCore.nEntity.nMonster
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityFlying = DotCraftCore.nEntity.EntityFlying;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityLargeFireball = DotCraftCore.nEntity.nProjectile.EntityLargeFireball;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using AchievementList = DotCraftCore.nStats.AchievementList;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using Vec3 = DotCraftCore.nUtil.Vec3;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;
	using World = DotCraftCore.nWorld.World;

	public class EntityGhast : EntityFlying, IMob
	{
		public int courseChangeCooldown;
		public double waypointX;
		public double waypointY;
		public double waypointZ;
		private Entity targetedEntity;

	/// <summary> Cooldown time between target loss and new target aquirement.  </summary>
		private int aggroCooldown;
		public int prevAttackCounter;
		public int attackCounter;

	/// <summary> The explosion radius of spawned fireballs.  </summary>
		private int explosionStrength = 1;
		

		public EntityGhast(World p_i1735_1_) : base(p_i1735_1_)
		{
			this.setSize(4.0F, 4.0F);
			this.isImmuneToFire = true;
			this.experienceValue = 5;
		}

		public virtual bool func_110182_bF()
		{
			return this.dataWatcher.getWatchableObjectByte(16) != 0;
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public virtual bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (this.EntityInvulnerable)
			{
				return false;
			}
			else if ("fireball".Equals(p_70097_1_.DamageType) && p_70097_1_.Entity is EntityPlayer)
			{
				base.attackEntityFrom(p_70097_1_, 1000.0F);
				((EntityPlayer)p_70097_1_.Entity).triggerAchievement(AchievementList.ghast);
				return true;
			}
			else
			{
				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

		protected internal virtual void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte)0));
		}

		protected internal virtual void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 10.0D;
		}

		protected internal virtual void updateEntityActionState()
		{
			if (!this.worldObj.isClient && this.worldObj.difficultySetting == EnumDifficulty.PEACEFUL)
			{
				this.setDead();
			}

			this.despawnEntity();
			this.prevAttackCounter = this.attackCounter;
			double var1 = this.waypointX - this.posX;
			double var3 = this.waypointY - this.posY;
			double var5 = this.waypointZ - this.posZ;
			double var7 = var1 * var1 + var3 * var3 + var5 * var5;

			if (var7 < 1.0D || var7 > 3600.0D)
			{
				this.waypointX = this.posX + (double)((this.rand.nextFloat() * 2.0F - 1.0F) * 16.0F);
				this.waypointY = this.posY + (double)((this.rand.nextFloat() * 2.0F - 1.0F) * 16.0F);
				this.waypointZ = this.posZ + (double)((this.rand.nextFloat() * 2.0F - 1.0F) * 16.0F);
			}

			if (this.courseChangeCooldown-- <= 0)
			{
				this.courseChangeCooldown += this.rand.Next(5) + 2;
				var7 = (double)MathHelper.sqrt_double(var7);

				if (this.isCourseTraversable(this.waypointX, this.waypointY, this.waypointZ, var7))
				{
					this.motionX += var1 / var7 * 0.1D;
					this.motionY += var3 / var7 * 0.1D;
					this.motionZ += var5 / var7 * 0.1D;
				}
				else
				{
					this.waypointX = this.posX;
					this.waypointY = this.posY;
					this.waypointZ = this.posZ;
				}
			}

			if (this.targetedEntity != null && this.targetedEntity.isDead)
			{
				this.targetedEntity = null;
			}

			if (this.targetedEntity == null || this.aggroCooldown-- <= 0)
			{
				this.targetedEntity = this.worldObj.getClosestVulnerablePlayerToEntity(this, 100.0D);

				if (this.targetedEntity != null)
				{
					this.aggroCooldown = 20;
				}
			}

			double var9 = 64.0D;

			if (this.targetedEntity != null && this.targetedEntity.getDistanceSqToEntity(this) < var9 * var9)
			{
				double var11 = this.targetedEntity.posX - this.posX;
				double var13 = this.targetedEntity.boundingBox.minY + (double)(this.targetedEntity.height / 2.0F) - (this.posY + (double)(this.height / 2.0F));
				double var15 = this.targetedEntity.posZ - this.posZ;
				this.renderYawOffset = this.rotationYaw = -((float)Math.Atan2(var11, var15)) * 180.0F / (float)Math.PI;

				if (this.canEntityBeSeen(this.targetedEntity))
				{
					if (this.attackCounter == 10)
					{
						this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1007, (int)this.posX, (int)this.posY, (int)this.posZ, 0);
					}

					++this.attackCounter;

					if (this.attackCounter == 20)
					{
						this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1008, (int)this.posX, (int)this.posY, (int)this.posZ, 0);
						EntityLargeFireball var17 = new EntityLargeFireball(this.worldObj, this, var11, var13, var15);
						var17.field_92057_e = this.explosionStrength;
						double var18 = 4.0D;
						Vec3 var20 = this.getLook(1.0F);
						var17.posX = this.posX + var20.xCoord * var18;
						var17.posY = this.posY + (double)(this.height / 2.0F) + 0.5D;
						var17.posZ = this.posZ + var20.zCoord * var18;
						this.worldObj.spawnEntityInWorld(var17);
						this.attackCounter = -40;
					}
				}
				else if (this.attackCounter > 0)
				{
					--this.attackCounter;
				}
			}
			else
			{
				this.renderYawOffset = this.rotationYaw = -((float)Math.Atan2(this.motionX, this.motionZ)) * 180.0F / (float)Math.PI;

				if (this.attackCounter > 0)
				{
					--this.attackCounter;
				}
			}

			if (!this.worldObj.isClient)
			{
				sbyte var21 = this.dataWatcher.getWatchableObjectByte(16);
				sbyte var12 = (sbyte)(this.attackCounter > 10 ? 1 : 0);

				if (var21 != var12)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte(var12));
				}
			}
		}

///    
///     <summary> * True if the ghast has an unobstructed line of travel to the waypoint. </summary>
///     
		private bool isCourseTraversable(double p_70790_1_, double p_70790_3_, double p_70790_5_, double p_70790_7_)
		{
			double var9 = (this.waypointX - this.posX) / p_70790_7_;
			double var11 = (this.waypointY - this.posY) / p_70790_7_;
			double var13 = (this.waypointZ - this.posZ) / p_70790_7_;
			AxisAlignedBB var15 = this.boundingBox.copy();

			for (int var16 = 1; (double)var16 < p_70790_7_; ++var16)
			{
				var15.offset(var9, var11, var13);

				if (!this.worldObj.getCollidingBoundingBoxes(this, var15).Empty)
				{
					return false;
				}
			}

			return true;
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal virtual string LivingSound
		{
			get
			{
				return "mob.ghast.moan";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal virtual string HurtSound
		{
			get
			{
				return "mob.ghast.scream";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal virtual string DeathSound
		{
			get
			{
				return "mob.ghast.death";
			}
		}

		protected internal virtual Item func_146068_u()
		{
			return Items.gunpowder;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal virtual void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(2) + this.rand.Next(1 + p_70628_2_);
			int var4;

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.ghast_tear, 1);
			}

			var3 = this.rand.Next(3) + this.rand.Next(1 + p_70628_2_);

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.gunpowder, 1);
			}
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal virtual float SoundVolume
		{
			get
			{
				return 10.0F;
			}
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public virtual bool CanSpawnHere
		{
			get
			{
				return this.rand.Next(20) == 0 && base.CanSpawnHere && this.worldObj.difficultySetting != EnumDifficulty.PEACEFUL;
			}
		}

///    
///     <summary> * Will return how many at most can spawn in a chunk at once. </summary>
///     
		public virtual int MaxSpawnedInChunk
		{
			get
			{
				return 1;
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public virtual void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("ExplosionPower", this.explosionStrength);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public virtual void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);

			if (p_70037_1_.func_150297_b("ExplosionPower", 99))
			{
				this.explosionStrength = p_70037_1_.getInteger("ExplosionPower");
			}
		}
	}

}