using System;

namespace DotCraftCore.Entity.Monster
{

	using Entity = DotCraftCore.Entity.Entity;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIAttackOnCollide = DotCraftCore.Entity.AI.EntityAIAttackOnCollide;
	using EntityAIAvoidEntity = DotCraftCore.Entity.AI.EntityAIAvoidEntity;
	using EntityAICreeperSwell = DotCraftCore.Entity.AI.EntityAICreeperSwell;
	using EntityAIHurtByTarget = DotCraftCore.Entity.AI.EntityAIHurtByTarget;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAINearestAttackableTarget = DotCraftCore.Entity.AI.EntityAINearestAttackableTarget;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityLightningBolt = DotCraftCore.Entity.Effect.EntityLightningBolt;
	using EntityOcelot = DotCraftCore.Entity.Passive.EntityOcelot;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using DamageSource = DotCraftCore.util.DamageSource;
	using World = DotCraftCore.world.World;

	public class EntityCreeper : EntityMob
	{
///    
///     <summary> * Time when this creeper was last in an active state (Messed up code here, probably causes creeper animation to go
///     * weird) </summary>
///     
		private int lastActiveTime;

///    
///     <summary> * The amount of time since the creeper was close enough to the player to ignite </summary>
///     
		private int timeSinceIgnited;
		private int fuseTime = 30;

	/// <summary> Explosion radius for this creeper.  </summary>
		private int explosionRadius = 3;
		private const string __OBFID = "CL_00001684";

		public EntityCreeper(World p_i1733_1_) : base(p_i1733_1_)
		{
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, new EntityAICreeperSwell(this));
			this.tasks.addTask(3, new EntityAIAvoidEntity(this, typeof(EntityOcelot), 6.0F, 1.0D, 1.2D));
			this.tasks.addTask(4, new EntityAIAttackOnCollide(this, 1.0D, false));
			this.tasks.addTask(5, new EntityAIWander(this, 0.8D));
			this.tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(6, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 0, true));
			this.targetTasks.addTask(2, new EntityAIHurtByTarget(this, false));
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.25D;
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

///    
///     <summary> * The number of iterations PathFinder.getSafePoint will execute before giving up. </summary>
///     
		public override int MaxSafePointTries
		{
			get
			{
				return this.AttackTarget == null ? 3 : 3 + (int)(this.Health - 1.0F);
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
			base.fall(p_70069_1_);
			this.timeSinceIgnited = (int)((float)this.timeSinceIgnited + p_70069_1_ * 1.5F);

			if (this.timeSinceIgnited > this.fuseTime - 5)
			{
				this.timeSinceIgnited = this.fuseTime - 5;
			}
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte) - 1));
			this.dataWatcher.addObject(17, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(18, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);

			if (this.dataWatcher.getWatchableObjectByte(17) == 1)
			{
				p_70014_1_.setBoolean("powered", true);
			}

			p_70014_1_.setShort("Fuse", (short)this.fuseTime);
			p_70014_1_.setByte("ExplosionRadius", (sbyte)this.explosionRadius);
			p_70014_1_.setBoolean("ignited", this.func_146078_ca());
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.dataWatcher.updateObject(17, Convert.ToByte((sbyte)(p_70037_1_.getBoolean("powered") ? 1 : 0)));

			if (p_70037_1_.func_150297_b("Fuse", 99))
			{
				this.fuseTime = p_70037_1_.getShort("Fuse");
			}

			if (p_70037_1_.func_150297_b("ExplosionRadius", 99))
			{
				this.explosionRadius = p_70037_1_.getByte("ExplosionRadius");
			}

			if (p_70037_1_.getBoolean("ignited"))
			{
				this.func_146079_cb();
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (this.EntityAlive)
			{
				this.lastActiveTime = this.timeSinceIgnited;

				if (this.func_146078_ca())
				{
					this.CreeperState = 1;
				}

				int var1 = this.CreeperState;

				if (var1 > 0 && this.timeSinceIgnited == 0)
				{
					this.playSound("creeper.primed", 1.0F, 0.5F);
				}

				this.timeSinceIgnited += var1;

				if (this.timeSinceIgnited < 0)
				{
					this.timeSinceIgnited = 0;
				}

				if (this.timeSinceIgnited >= this.fuseTime)
				{
					this.timeSinceIgnited = this.fuseTime;
					this.func_146077_cc();
				}
			}

			base.onUpdate();
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.creeper.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.creeper.death";
			}
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public override void onDeath(DamageSource p_70645_1_)
		{
			base.onDeath(p_70645_1_);

			if (p_70645_1_.Entity is EntitySkeleton)
			{
				int var2 = Item.getIdFromItem(Items.record_13);
				int var3 = Item.getIdFromItem(Items.record_wait);
				int var4 = var2 + this.rand.Next(var3 - var2 + 1);
				this.func_145779_a(Item.getItemById(var4), 1);
			}
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			return true;
		}

///    
///     <summary> * Returns true if the creeper is powered by a lightning bolt. </summary>
///     
		public virtual bool Powered
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(17) == 1;
			}
		}

///    
///     <summary> * Params: (Float)Render tick. Returns the intensity of the creeper's flash when it is ignited. </summary>
///     
		public virtual float getCreeperFlashIntensity(float p_70831_1_)
		{
			return ((float)this.lastActiveTime + (float)(this.timeSinceIgnited - this.lastActiveTime) * p_70831_1_) / (float)(this.fuseTime - 2);
		}

		protected internal override Item func_146068_u()
		{
			return Items.gunpowder;
		}

///    
///     <summary> * Returns the current state of creeper, -1 is idle, 1 is 'in fuse' </summary>
///     
		public virtual int CreeperState
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(16);
			}
			set
			{
				this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)value));
			}
		}

///    
///     <summary> * Sets the state of creeper, -1 to idle and 1 to be 'in fuse' </summary>
///     

///    
///     <summary> * Called when a lightning bolt hits the entity. </summary>
///     
		public override void onStruckByLightning(EntityLightningBolt p_70077_1_)
		{
			base.onStruckByLightning(p_70077_1_);
			this.dataWatcher.updateObject(17, Convert.ToByte((sbyte)1));
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		protected internal override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && var2.Item == Items.flint_and_steel)
			{
				this.worldObj.playSoundEffect(this.posX + 0.5D, this.posY + 0.5D, this.posZ + 0.5D, "fire.ignite", 1.0F, this.rand.nextFloat() * 0.4F + 0.8F);
				p_70085_1_.swingItem();

				if (!this.worldObj.isClient)
				{
					this.func_146079_cb();
					var2.damageItem(1, p_70085_1_);
					return true;
				}
			}

			return base.interact(p_70085_1_);
		}

		private void func_146077_cc()
		{
			if (!this.worldObj.isClient)
			{
				bool var1 = this.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing");

				if (this.Powered)
				{
					this.worldObj.createExplosion(this, this.posX, this.posY, this.posZ, (float)(this.explosionRadius * 2), var1);
				}
				else
				{
					this.worldObj.createExplosion(this, this.posX, this.posY, this.posZ, (float)this.explosionRadius, var1);
				}

				this.setDead();
			}
		}

		public virtual bool func_146078_ca()
		{
			return this.dataWatcher.getWatchableObjectByte(18) != 0;
		}

		public virtual void func_146079_cb()
		{
			this.dataWatcher.updateObject(18, Convert.ToByte((sbyte)1));
		}
	}

}