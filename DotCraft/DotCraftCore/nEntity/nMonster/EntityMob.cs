using System;

namespace DotCraftCore.nEntity.nMonster
{

	using EnchantmentHelper = DotCraftCore.nEnchantment.EnchantmentHelper;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityCreature = DotCraftCore.nEntity.EntityCreature;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;
	using EnumSkyBlock = DotCraftCore.nWorld.EnumSkyBlock;
	using World = DotCraftCore.nWorld.World;

	public abstract class EntityMob : EntityCreature, IMob
	{
		

		public EntityMob(World p_i1738_1_) : base(p_i1738_1_)
		{
			this.experienceValue = 5;
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public virtual void onLivingUpdate()
		{
			this.updateArmSwingProgress();
			float var1 = this.getBrightness(1.0F);

			if (var1 > 0.5F)
			{
				this.entityAge += 2;
			}

			base.onLivingUpdate();
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public virtual void onUpdate()
		{
			base.onUpdate();

			if (!this.worldObj.isClient && this.worldObj.difficultySetting == EnumDifficulty.PEACEFUL)
			{
				this.setDead();
			}
		}

		protected internal virtual string SwimSound
		{
			get
			{
				return "game.hostile.swim";
			}
		}

		protected internal virtual string SplashSound
		{
			get
			{
				return "game.hostile.swim.splash";
			}
		}

///    
///     <summary> * Finds the closest player within 16 blocks to attack, or null if this Entity isn't interested in attacking
///     * (Animals, Spiders at day, peaceful PigZombies). </summary>
///     
		protected internal override Entity findPlayerToAttack()
		{
			EntityPlayer var1 = this.worldObj.getClosestVulnerablePlayerToEntity(this, 16.0D);
			return var1 != null && this.canEntityBeSeen(var1) ? var1 : null;
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
			else if (base.attackEntityFrom(p_70097_1_, p_70097_2_))
			{
				Entity var3 = p_70097_1_.Entity;

				if (this.riddenByEntity != var3 && this.ridingEntity != var3)
				{
					if (var3 != this)
					{
						this.entityToAttack = var3;
					}

					return true;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal virtual string HurtSound
		{
			get
			{
				return "game.hostile.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal virtual string DeathSound
		{
			get
			{
				return "game.hostile.die";
			}
		}

		protected internal virtual string func_146067_o(int p_146067_1_)
		{
			return p_146067_1_ > 4 ? "game.hostile.hurt.fall.big" : "game.hostile.hurt.fall.small";
		}

		public virtual bool attackEntityAsMob(Entity p_70652_1_)
		{
			float var2 = (float)this.getEntityAttribute(SharedMonsterAttributes.attackDamage).AttributeValue;
			int var3 = 0;

			if (p_70652_1_ is EntityLivingBase)
			{
				var2 += EnchantmentHelper.getEnchantmentModifierLiving(this, (EntityLivingBase)p_70652_1_);
				var3 += EnchantmentHelper.getKnockbackModifier(this, (EntityLivingBase)p_70652_1_);
			}

			bool var4 = p_70652_1_.attackEntityFrom(DamageSource.causeMobDamage(this), var2);

			if (var4)
			{
				if (var3 > 0)
				{
					p_70652_1_.addVelocity((double)(-MathHelper.sin(this.rotationYaw * (float)Math.PI / 180.0F) * (float)var3 * 0.5F), 0.1D, (double)(MathHelper.cos(this.rotationYaw * (float)Math.PI / 180.0F) * (float)var3 * 0.5F));
					this.motionX *= 0.6D;
					this.motionZ *= 0.6D;
				}

				int var5 = EnchantmentHelper.getFireAspectModifier(this);

				if (var5 > 0)
				{
					p_70652_1_.Fire = var5 * 4;
				}

				if (p_70652_1_ is EntityLivingBase)
				{
					EnchantmentHelper.func_151384_a((EntityLivingBase)p_70652_1_, this);
				}

				EnchantmentHelper.func_151385_b(this, p_70652_1_);
			}

			return var4;
		}

///    
///     <summary> * Basic mob attack. Default to touch of death in EntityCreature. Overridden by each mob to define their attack. </summary>
///     
		protected internal override void attackEntity(Entity p_70785_1_, float p_70785_2_)
		{
			if (this.attackTime <= 0 && p_70785_2_ < 2.0F && p_70785_1_.boundingBox.maxY > this.boundingBox.minY && p_70785_1_.boundingBox.minY < this.boundingBox.maxY)
			{
				this.attackTime = 20;
				this.attackEntityAsMob(p_70785_1_);
			}
		}

///    
///     <summary> * Takes a coordinate in and returns a weight to determine how likely this creature will try to path to the block.
///     * Args: x, y, z </summary>
///     
		public override float getBlockPathWeight(int p_70783_1_, int p_70783_2_, int p_70783_3_)
		{
			return 0.5F - this.worldObj.getLightBrightness(p_70783_1_, p_70783_2_, p_70783_3_);
		}

///    
///     <summary> * Checks to make sure the light is not too bright where the mob is spawning </summary>
///     
		protected internal virtual bool isValidLightLevel()
		{
			get
			{
				int var1 = MathHelper.floor_double(this.posX);
				int var2 = MathHelper.floor_double(this.boundingBox.minY);
				int var3 = MathHelper.floor_double(this.posZ);
	
				if (this.worldObj.getSavedLightValue(EnumSkyBlock.Sky, var1, var2, var3) > this.rand.Next(32))
				{
					return false;
				}
				else
				{
					int var4 = this.worldObj.getBlockLightValue(var1, var2, var3);
	
					if (this.worldObj.Thundering)
					{
						int var5 = this.worldObj.skylightSubtracted;
						this.worldObj.skylightSubtracted = 10;
						var4 = this.worldObj.getBlockLightValue(var1, var2, var3);
						this.worldObj.skylightSubtracted = var5;
					}
	
					return var4 <= this.rand.Next(8);
				}
			}
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				return this.worldObj.difficultySetting != EnumDifficulty.PEACEFUL && this.ValidLightLevel && base.CanSpawnHere;
			}
		}

		protected internal virtual void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.AttributeMap.registerAttribute(SharedMonsterAttributes.attackDamage);
		}

		protected internal virtual bool func_146066_aG()
		{
			return true;
		}
	}

}