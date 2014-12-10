using System;

namespace DotCraftCore.Entity.Passive
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using IEntityLivingData = DotCraftCore.Entity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIAvoidEntity = DotCraftCore.Entity.AI.EntityAIAvoidEntity;
	using EntityAIFollowOwner = DotCraftCore.Entity.AI.EntityAIFollowOwner;
	using EntityAILeapAtTarget = DotCraftCore.Entity.AI.EntityAILeapAtTarget;
	using EntityAIMate = DotCraftCore.Entity.AI.EntityAIMate;
	using EntityAIOcelotAttack = DotCraftCore.Entity.AI.EntityAIOcelotAttack;
	using EntityAIOcelotSit = DotCraftCore.Entity.AI.EntityAIOcelotSit;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAITargetNonTamed = DotCraftCore.Entity.AI.EntityAITargetNonTamed;
	using EntityAITempt = DotCraftCore.Entity.AI.EntityAITempt;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using StatCollector = DotCraftCore.util.StatCollector;
	using World = DotCraftCore.world.World;

	public class EntityOcelot : EntityTameable
	{
///    
///     <summary> * The tempt AI task for this mob, used to prevent taming while it is fleeing. </summary>
///     
		private EntityAITempt aiTempt;
		private const string __OBFID = "CL_00001646";

		public EntityOcelot(World p_i1688_1_) : base(p_i1688_1_)
		{
			this.setSize(0.6F, 0.8F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, this.aiSit);
			this.tasks.addTask(3, this.aiTempt = new EntityAITempt(this, 0.6D, Items.fish, true));
			this.tasks.addTask(4, new EntityAIAvoidEntity(this, typeof(EntityPlayer), 16.0F, 0.8D, 1.33D));
			this.tasks.addTask(5, new EntityAIFollowOwner(this, 1.0D, 10.0F, 5.0F));
			this.tasks.addTask(6, new EntityAIOcelotSit(this, 1.33D));
			this.tasks.addTask(7, new EntityAILeapAtTarget(this, 0.3F));
			this.tasks.addTask(8, new EntityAIOcelotAttack(this));
			this.tasks.addTask(9, new EntityAIMate(this, 0.8D));
			this.tasks.addTask(10, new EntityAIWander(this, 0.8D));
			this.tasks.addTask(11, new EntityAIWatchClosest(this, typeof(EntityPlayer), 10.0F));
			this.targetTasks.addTask(1, new EntityAITargetNonTamed(this, typeof(EntityChicken), 750, false));
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(18, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * main AI tick function, replaces updateEntityActionState </summary>
///     
		public override void updateAITick()
		{
			if (this.MoveHelper.Updating)
			{
				double var1 = this.MoveHelper.Speed;

				if (var1 == 0.6D)
				{
					this.Sneaking = true;
					this.Sprinting = false;
				}
				else if (var1 == 1.33D)
				{
					this.Sneaking = false;
					this.Sprinting = true;
				}
				else
				{
					this.Sneaking = false;
					this.Sprinting = false;
				}
			}
			else
			{
				this.Sneaking = false;
				this.Sprinting = false;
			}
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal override bool canDespawn()
		{
			return !this.Tamed && this.ticksExisted > 2400;
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		public override bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 10.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.30000001192092896D;
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("CatType", this.TameSkin);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.TameSkin = p_70037_1_.getInteger("CatType");
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return this.Tamed ? (this.InLove ? "mob.cat.purr" : (this.rand.Next(4) == 0 ? "mob.cat.purreow" : "mob.cat.meow")) : "";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.cat.hitt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.cat.hitt";
			}
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal override float SoundVolume
		{
			get
			{
				return 0.4F;
			}
		}

		protected internal override Item func_146068_u()
		{
			return Items.leather;
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			return p_70652_1_.attackEntityFrom(DamageSource.causeMobDamage(this), 3.0F);
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
			else
			{
				this.aiSit.Sitting = false;
				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (this.Tamed)
			{
				if (this.func_152114_e(p_70085_1_) && !this.worldObj.isClient && !this.isBreedingItem(var2))
				{
					this.aiSit.Sitting = !this.Sitting;
				}
			}
			else if (this.aiTempt.Running && var2 != null && var2.Item == Items.fish && p_70085_1_.getDistanceSqToEntity(this) < 9.0D)
			{
				if (!p_70085_1_.capabilities.isCreativeMode)
				{
					--var2.stackSize;
				}

				if (var2.stackSize <= 0)
				{
					p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, (ItemStack)null);
				}

				if (!this.worldObj.isClient)
				{
					if (this.rand.Next(3) == 0)
					{
						this.Tamed = true;
						this.TameSkin = 1 + this.worldObj.rand.Next(3);
						this.func_152115_b(p_70085_1_.UniqueID.ToString());
						this.playTameEffect(true);
						this.aiSit.Sitting = true;
						this.worldObj.setEntityState(this, (sbyte)7);
					}
					else
					{
						this.playTameEffect(false);
						this.worldObj.setEntityState(this, (sbyte)6);
					}
				}

				return true;
			}

			return base.interact(p_70085_1_);
		}

		public override EntityOcelot createChild(EntityAgeable p_90011_1_)
		{
			EntityOcelot var2 = new EntityOcelot(this.worldObj);

			if (this.Tamed)
			{
				var2.func_152115_b(this.func_152113_b());
				var2.Tamed = true;
				var2.TameSkin = this.TameSkin;
			}

			return var2;
		}

///    
///     <summary> * Checks if the parameter is an item which this animal can be fed to breed it (wheat, carrots or seeds depending on
///     * the animal type) </summary>
///     
		public override bool isBreedingItem(ItemStack p_70877_1_)
		{
			return p_70877_1_ != null && p_70877_1_.Item == Items.fish;
		}

///    
///     <summary> * Returns true if the mob is currently able to mate with the specified mob. </summary>
///     
		public override bool canMateWith(EntityAnimal p_70878_1_)
		{
			if (p_70878_1_ == this)
			{
				return false;
			}
			else if (!this.Tamed)
			{
				return false;
			}
			else if (!(p_70878_1_ is EntityOcelot))
			{
				return false;
			}
			else
			{
				EntityOcelot var2 = (EntityOcelot)p_70878_1_;
				return !var2.Tamed ? false : this.InLove && var2.InLove;
			}
		}

		public virtual int TameSkin
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(18);
			}
			set
			{
				this.dataWatcher.updateObject(18, Convert.ToByte((sbyte)value));
			}
		}


///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				if (this.worldObj.rand.Next(3) == 0)
				{
					return false;
				}
				else
				{
					if (this.worldObj.checkNoEntityCollision(this.boundingBox) && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Empty && !this.worldObj.isAnyLiquid(this.boundingBox))
					{
						int var1 = MathHelper.floor_double(this.posX);
						int var2 = MathHelper.floor_double(this.boundingBox.minY);
						int var3 = MathHelper.floor_double(this.posZ);
	
						if (var2 < 63)
						{
							return false;
						}
	
						Block var4 = this.worldObj.getBlock(var1, var2 - 1, var3);
	
						if (var4 == Blocks.grass || var4.Material == Material.leaves)
						{
							return true;
						}
					}
	
					return false;
				}
			}
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public override string CommandSenderName
		{
			get
			{
				return this.hasCustomNameTag() ? this.CustomNameTag : (this.Tamed ? StatCollector.translateToLocal("entity.Cat.name") : base.CommandSenderName);
			}
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			p_110161_1_ = base.onSpawnWithEgg(p_110161_1_);

			if (this.worldObj.rand.Next(7) == 0)
			{
				for (int var2 = 0; var2 < 2; ++var2)
				{
					EntityOcelot var3 = new EntityOcelot(this.worldObj);
					var3.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, 0.0F);
					var3.GrowingAge = -24000;
					this.worldObj.spawnEntityInWorld(var3);
				}
			}

			return p_110161_1_;
		}
	}

}