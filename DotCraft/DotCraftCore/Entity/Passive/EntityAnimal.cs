using System;
using System.Collections;

namespace DotCraftCore.nEntity.nPassive
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityAgeable = DotCraftCore.nEntity.EntityAgeable;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using IAttributeInstance = DotCraftCore.nEntity.nAI.nAttributes.IAttributeInstance;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using AchievementList = DotCraftCore.nStats.AchievementList;
	using StatList = DotCraftCore.nStats.StatList;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public abstract class EntityAnimal : EntityAgeable, IAnimals
	{
		private int inLove;

///    
///     <summary> * This is representation of a counter for reproduction progress. (Note that this is different from the inLove which
///     * represent being in Love-Mode) </summary>
///     
		private int breeding;
		private EntityPlayer field_146084_br;
		

		public EntityAnimal(World p_i1681_1_) : base(p_i1681_1_)
		{
		}

///    
///     <summary> * main AI tick function, replaces updateEntityActionState </summary>
///     
		protected internal virtual void updateAITick()
		{
			if (this.GrowingAge != 0)
			{
				this.inLove = 0;
			}

			base.updateAITick();
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();

			if (this.GrowingAge != 0)
			{
				this.inLove = 0;
			}

			if (this.inLove > 0)
			{
				--this.inLove;
				string var1 = "heart";

				if (this.inLove % 10 == 0)
				{
					double var2 = this.rand.nextGaussian() * 0.02D;
					double var4 = this.rand.nextGaussian() * 0.02D;
					double var6 = this.rand.nextGaussian() * 0.02D;
					this.worldObj.spawnParticle(var1, this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var2, var4, var6);
				}
			}
			else
			{
				this.breeding = 0;
			}
		}

///    
///     <summary> * Basic mob attack. Default to touch of death in EntityCreature. Overridden by each mob to define their attack. </summary>
///     
		protected internal virtual void attackEntity(Entity p_70785_1_, float p_70785_2_)
		{
			if (p_70785_1_ is EntityPlayer)
			{
				if (p_70785_2_ < 3.0F)
				{
					double var3 = p_70785_1_.posX - this.posX;
					double var5 = p_70785_1_.posZ - this.posZ;
					this.rotationYaw = (float)(Math.Atan2(var5, var3) * 180.0D / Math.PI) - 90.0F;
					this.hasAttacked = true;
				}

				EntityPlayer var7 = (EntityPlayer)p_70785_1_;

				if (var7.CurrentEquippedItem == null || !this.isBreedingItem(var7.CurrentEquippedItem))
				{
					this.entityToAttack = null;
				}
			}
			else if (p_70785_1_ is EntityAnimal)
			{
				EntityAnimal var8 = (EntityAnimal)p_70785_1_;

				if (this.GrowingAge > 0 && var8.GrowingAge < 0)
				{
					if ((double)p_70785_2_ < 2.5D)
					{
						this.hasAttacked = true;
					}
				}
				else if (this.inLove > 0 && var8.inLove > 0)
				{
					if (var8.entityToAttack == null)
					{
						var8.entityToAttack = this;
					}

					if (var8.entityToAttack == this && (double)p_70785_2_ < 3.5D)
					{
						++var8.inLove;
						++this.inLove;
						++this.breeding;

						if (this.breeding % 4 == 0)
						{
							this.worldObj.spawnParticle("heart", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, 0.0D, 0.0D, 0.0D);
						}

						if (this.breeding == 60)
						{
							this.procreate((EntityAnimal)p_70785_1_);
						}
					}
					else
					{
						this.breeding = 0;
					}
				}
				else
				{
					this.breeding = 0;
					this.entityToAttack = null;
				}
			}
		}

///    
///     <summary> * Creates a baby animal according to the animal type of the target at the actual position and spawns 'love'
///     * particles. </summary>
///     
		private void procreate(EntityAnimal p_70876_1_)
		{
			EntityAgeable var2 = this.createChild(p_70876_1_);

			if (var2 != null)
			{
				if (this.field_146084_br == null && p_70876_1_.func_146083_cb() != null)
				{
					this.field_146084_br = p_70876_1_.func_146083_cb();
				}

				if (this.field_146084_br != null)
				{
					this.field_146084_br.triggerAchievement(StatList.field_151186_x);

					if (this is EntityCow)
					{
						this.field_146084_br.triggerAchievement(AchievementList.field_150962_H);
					}
				}

				this.GrowingAge = 6000;
				p_70876_1_.GrowingAge = 6000;
				this.inLove = 0;
				this.breeding = 0;
				this.entityToAttack = null;
				p_70876_1_.entityToAttack = null;
				p_70876_1_.breeding = 0;
				p_70876_1_.inLove = 0;
				var2.GrowingAge = -24000;
				var2.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);

				for (int var3 = 0; var3 < 7; ++var3)
				{
					double var4 = this.rand.nextGaussian() * 0.02D;
					double var6 = this.rand.nextGaussian() * 0.02D;
					double var8 = this.rand.nextGaussian() * 0.02D;
					this.worldObj.spawnParticle("heart", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var4, var6, var8);
				}

				this.worldObj.spawnEntityInWorld(var2);
			}
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
			else
			{
				this.fleeingTick = 60;

				if (!this.AIEnabled)
				{
					IAttributeInstance var3 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);

					if (var3.getModifier(field_110179_h) == null)
					{
						var3.applyModifier(field_110181_i);
					}
				}

				this.entityToAttack = null;
				this.inLove = 0;
				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

///    
///     <summary> * Takes a coordinate in and returns a weight to determine how likely this creature will try to path to the block.
///     * Args: x, y, z </summary>
///     
		public virtual float getBlockPathWeight(int p_70783_1_, int p_70783_2_, int p_70783_3_)
		{
			return this.worldObj.getBlock(p_70783_1_, p_70783_2_ - 1, p_70783_3_) == Blocks.grass ? 10.0F : this.worldObj.getLightBrightness(p_70783_1_, p_70783_2_, p_70783_3_) - 0.5F;
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("InLove", this.inLove);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.inLove = p_70037_1_.getInteger("InLove");
		}

///    
///     <summary> * Finds the closest player within 16 blocks to attack, or null if this Entity isn't interested in attacking
///     * (Animals, Spiders at day, peaceful PigZombies). </summary>
///     
		protected internal virtual Entity findPlayerToAttack()
		{
			if (this.fleeingTick > 0)
			{
				return null;
			}
			else
			{
				float var1 = 8.0F;
				IList var2;
				int var3;
				EntityAnimal var4;

				if (this.inLove > 0)
				{
					var2 = this.worldObj.getEntitiesWithinAABB(this.GetType(), this.boundingBox.expand((double)var1, (double)var1, (double)var1));

					for (var3 = 0; var3 < var2.Count; ++var3)
					{
						var4 = (EntityAnimal)var2[var3];

						if (var4 != this && var4.inLove > 0)
						{
							return var4;
						}
					}
				}
				else if (this.GrowingAge == 0)
				{
					var2 = this.worldObj.getEntitiesWithinAABB(typeof(EntityPlayer), this.boundingBox.expand((double)var1, (double)var1, (double)var1));

					for (var3 = 0; var3 < var2.Count; ++var3)
					{
						EntityPlayer var5 = (EntityPlayer)var2[var3];

						if (var5.CurrentEquippedItem != null && this.isBreedingItem(var5.CurrentEquippedItem))
						{
							return var5;
						}
					}
				}
				else if (this.GrowingAge > 0)
				{
					var2 = this.worldObj.getEntitiesWithinAABB(this.GetType(), this.boundingBox.expand((double)var1, (double)var1, (double)var1));

					for (var3 = 0; var3 < var2.Count; ++var3)
					{
						var4 = (EntityAnimal)var2[var3];

						if (var4 != this && var4.GrowingAge < 0)
						{
							return var4;
						}
					}
				}

				return null;
			}
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public virtual bool CanSpawnHere
		{
			get
			{
				int var1 = MathHelper.floor_double(this.posX);
				int var2 = MathHelper.floor_double(this.boundingBox.minY);
				int var3 = MathHelper.floor_double(this.posZ);
				return this.worldObj.getBlock(var1, var2 - 1, var3) == Blocks.grass && this.worldObj.getFullBlockLightValue(var1, var2, var3) > 8 && base.CanSpawnHere;
			}
		}

///    
///     <summary> * Get number of ticks, at least during which the living entity will be silent. </summary>
///     
		public virtual int TalkInterval
		{
			get
			{
				return 120;
			}
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal virtual bool canDespawn()
		{
			return false;
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal virtual int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			return 1 + this.worldObj.rand.Next(3);
		}

///    
///     <summary> * Checks if the parameter is an item which this animal can be fed to breed it (wheat, carrots or seeds depending on
///     * the animal type) </summary>
///     
		public virtual bool isBreedingItem(ItemStack p_70877_1_)
		{
			return p_70877_1_.Item == Items.wheat;
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && this.isBreedingItem(var2) && this.GrowingAge == 0 && this.inLove <= 0)
			{
				if (!p_70085_1_.capabilities.isCreativeMode)
				{
					--var2.stackSize;

					if (var2.stackSize <= 0)
					{
						p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, (ItemStack)null);
					}
				}

				this.func_146082_f(p_70085_1_);
				return true;
			}
			else
			{
				return base.interact(p_70085_1_);
			}
		}

		public virtual void func_146082_f(EntityPlayer p_146082_1_)
		{
			this.inLove = 600;
			this.field_146084_br = p_146082_1_;
			this.entityToAttack = null;
			this.worldObj.setEntityState(this, (sbyte)18);
		}

		public virtual EntityPlayer func_146083_cb()
		{
			return this.field_146084_br;
		}

///    
///     <summary> * Returns if the entity is currently in 'love mode'. </summary>
///     
		public virtual bool isInLove()
		{
			get
			{
				return this.inLove > 0;
			}
		}

		public virtual void resetInLove()
		{
			this.inLove = 0;
		}

///    
///     <summary> * Returns true if the mob is currently able to mate with the specified mob. </summary>
///     
		public virtual bool canMateWith(EntityAnimal p_70878_1_)
		{
			return p_70878_1_ == this ? false : (p_70878_1_.GetType() != this.GetType() ? false : this.InLove && p_70878_1_.InLove);
		}

		public virtual void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 18)
			{
				for (int var2 = 0; var2 < 7; ++var2)
				{
					double var3 = this.rand.nextGaussian() * 0.02D;
					double var5 = this.rand.nextGaussian() * 0.02D;
					double var7 = this.rand.nextGaussian() * 0.02D;
					this.worldObj.spawnParticle("heart", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var3, var5, var7);
				}
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}
	}

}