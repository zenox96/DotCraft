using System;

namespace DotCraftCore.Entity.Passive
{

	using Block = DotCraftCore.block.Block;
	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIControlledByPlayer = DotCraftCore.Entity.AI.EntityAIControlledByPlayer;
	using EntityAIFollowParent = DotCraftCore.Entity.AI.EntityAIFollowParent;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAIMate = DotCraftCore.Entity.AI.EntityAIMate;
	using EntityAIPanic = DotCraftCore.Entity.AI.EntityAIPanic;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAITempt = DotCraftCore.Entity.AI.EntityAITempt;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityLightningBolt = DotCraftCore.Entity.Effect.EntityLightningBolt;
	using EntityPigZombie = DotCraftCore.Entity.Monster.EntityPigZombie;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using AchievementList = DotCraftCore.Stats.AchievementList;
	using World = DotCraftCore.World.World;

	public class EntityPig : EntityAnimal
	{
	/// <summary> AI task for player control.  </summary>
		private readonly EntityAIControlledByPlayer aiControlledByPlayer;
		

		public EntityPig(World p_i1689_1_) : base(p_i1689_1_)
		{
			this.setSize(0.9F, 0.9F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIPanic(this, 1.25D));
			this.tasks.addTask(2, this.aiControlledByPlayer = new EntityAIControlledByPlayer(this, 0.3F));
			this.tasks.addTask(3, new EntityAIMate(this, 1.0D));
			this.tasks.addTask(4, new EntityAITempt(this, 1.2D, Items.carrot_on_a_stick, false));
			this.tasks.addTask(4, new EntityAITempt(this, 1.2D, Items.carrot, false));
			this.tasks.addTask(5, new EntityAIFollowParent(this, 1.1D));
			this.tasks.addTask(6, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(8, new EntityAILookIdle(this));
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
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.25D;
		}

		protected internal override void updateAITasks()
		{
			base.updateAITasks();
		}

///    
///     <summary> * returns true if all the conditions for steering the entity are met. For pigs, this is true if it is being ridden
///     * by a player and the player is holding a carrot-on-a-stick </summary>
///     
		public override bool canBeSteered()
		{
			ItemStack var1 = ((EntityPlayer)this.riddenByEntity).HeldItem;
			return var1 != null && var1.Item == Items.carrot_on_a_stick;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("Saddle", this.Saddled);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.Saddled = p_70037_1_.getBoolean("Saddle");
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.pig.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.pig.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.pig.death";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.pig.step", 0.15F, 1.0F);
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			if (base.interact(p_70085_1_))
			{
				return true;
			}
			else if (this.Saddled && !this.worldObj.isClient && (this.riddenByEntity == null || this.riddenByEntity == p_70085_1_))
			{
				p_70085_1_.mountEntity(this);
				return true;
			}
			else
			{
				return false;
			}
		}

		protected internal override Item func_146068_u()
		{
			return this.Burning ? Items.cooked_porkchop : Items.porkchop;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(3) + 1 + this.rand.Next(1 + p_70628_2_);

			for (int var4 = 0; var4 < var3; ++var4)
			{
				if (this.Burning)
				{
					this.func_145779_a(Items.cooked_porkchop, 1);
				}
				else
				{
					this.func_145779_a(Items.porkchop, 1);
				}
			}

			if (this.Saddled)
			{
				this.func_145779_a(Items.saddle, 1);
			}
		}

///    
///     <summary> * Returns true if the pig is saddled. </summary>
///     
		public virtual bool Saddled
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)1));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)0));
				}
			}
		}

///    
///     <summary> * Set or remove the saddle of the pig. </summary>
///     

///    
///     <summary> * Called when a lightning bolt hits the entity. </summary>
///     
		public override void onStruckByLightning(EntityLightningBolt p_70077_1_)
		{
			if (!this.worldObj.isClient)
			{
				EntityPigZombie var2 = new EntityPigZombie(this.worldObj);
				var2.setCurrentItemOrArmor(0, new ItemStack(Items.golden_sword));
				var2.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
				this.worldObj.spawnEntityInWorld(var2);
				this.setDead();
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
			base.fall(p_70069_1_);

			if (p_70069_1_ > 5.0F && this.riddenByEntity is EntityPlayer)
			{
				((EntityPlayer)this.riddenByEntity).triggerAchievement(AchievementList.flyPig);
			}
		}

		public override EntityPig createChild(EntityAgeable p_90011_1_)
		{
			return new EntityPig(this.worldObj);
		}

///    
///     <summary> * Checks if the parameter is an item which this animal can be fed to breed it (wheat, carrots or seeds depending on
///     * the animal type) </summary>
///     
		public override bool isBreedingItem(ItemStack p_70877_1_)
		{
			return p_70877_1_ != null && p_70877_1_.Item == Items.carrot;
		}

///    
///     <summary> * Return the AI task for player control. </summary>
///     
		public virtual EntityAIControlledByPlayer AIControlledByPlayer
		{
			get
			{
				return this.aiControlledByPlayer;
			}
		}
	}

}