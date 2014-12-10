using System;

namespace DotCraftCore.Entity.Monster
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIAttackOnCollide = DotCraftCore.Entity.AI.EntityAIAttackOnCollide;
	using EntityAIDefendVillage = DotCraftCore.Entity.AI.EntityAIDefendVillage;
	using EntityAIHurtByTarget = DotCraftCore.Entity.AI.EntityAIHurtByTarget;
	using EntityAILookAtVillager = DotCraftCore.Entity.AI.EntityAILookAtVillager;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAIMoveThroughVillage = DotCraftCore.Entity.AI.EntityAIMoveThroughVillage;
	using EntityAIMoveTowardsRestriction = DotCraftCore.Entity.AI.EntityAIMoveTowardsRestriction;
	using EntityAIMoveTowardsTarget = DotCraftCore.Entity.AI.EntityAIMoveTowardsTarget;
	using EntityAINearestAttackableTarget = DotCraftCore.Entity.AI.EntityAINearestAttackableTarget;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using ChunkCoordinates = DotCraftCore.util.ChunkCoordinates;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using Village = DotCraftCore.village.Village;
	using World = DotCraftCore.world.World;

	public class EntityIronGolem : EntityGolem
	{
	/// <summary> deincrements, and a distance-to-home check is done at 0  </summary>
		private int homeCheckTimer;
		internal Village villageObj;
		private int attackTimer;
		private int holdRoseTick;
		private const string __OBFID = "CL_00001652";

		public EntityIronGolem(World p_i1694_1_) : base(p_i1694_1_)
		{
			this.setSize(1.4F, 2.9F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(1, new EntityAIAttackOnCollide(this, 1.0D, true));
			this.tasks.addTask(2, new EntityAIMoveTowardsTarget(this, 0.9D, 32.0F));
			this.tasks.addTask(3, new EntityAIMoveThroughVillage(this, 0.6D, true));
			this.tasks.addTask(4, new EntityAIMoveTowardsRestriction(this, 1.0D));
			this.tasks.addTask(5, new EntityAILookAtVillager(this));
			this.tasks.addTask(6, new EntityAIWander(this, 0.6D));
			this.tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(8, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIDefendVillage(this));
			this.targetTasks.addTask(2, new EntityAIHurtByTarget(this, false));
			this.targetTasks.addTask(3, new EntityAINearestAttackableTarget(this, typeof(EntityLiving), 0, false, true, IMob.mobSelector));
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte)0));
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
///     <summary> * main AI tick function, replaces updateEntityActionState </summary>
///     
		protected internal override void updateAITick()
		{
			if (--this.homeCheckTimer <= 0)
			{
				this.homeCheckTimer = 70 + this.rand.Next(50);
				this.villageObj = this.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ), 32);

				if (this.villageObj == null)
				{
					this.detachHome();
				}
				else
				{
					ChunkCoordinates var1 = this.villageObj.Center;
					this.setHomeArea(var1.posX, var1.posY, var1.posZ, (int)((float)this.villageObj.VillageRadius * 0.6F));
				}
			}

			base.updateAITick();
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 100.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.25D;
		}

///    
///     <summary> * Decrements the entity's air supply when underwater </summary>
///     
		protected internal override int decreaseAirSupply(int p_70682_1_)
		{
			return p_70682_1_;
		}

		protected internal override void collideWithEntity(Entity p_82167_1_)
		{
			if (p_82167_1_ is IMob && this.RNG.Next(20) == 0)
			{
				this.AttackTarget = (EntityLivingBase)p_82167_1_;
			}

			base.collideWithEntity(p_82167_1_);
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();

			if (this.attackTimer > 0)
			{
				--this.attackTimer;
			}

			if (this.holdRoseTick > 0)
			{
				--this.holdRoseTick;
			}

			if (this.motionX * this.motionX + this.motionZ * this.motionZ > 2.500000277905201E-7D && this.rand.Next(5) == 0)
			{
				int var1 = MathHelper.floor_double(this.posX);
				int var2 = MathHelper.floor_double(this.posY - 0.20000000298023224D - (double)this.yOffset);
				int var3 = MathHelper.floor_double(this.posZ);
				Block var4 = this.worldObj.getBlock(var1, var2, var3);

				if (var4.Material != Material.air)
				{
					this.worldObj.spawnParticle("blockcrack_" + Block.getIdFromBlock(var4) + "_" + this.worldObj.getBlockMetadata(var1, var2, var3), this.posX + ((double)this.rand.nextFloat() - 0.5D) * (double)this.width, this.boundingBox.minY + 0.1D, this.posZ + ((double)this.rand.nextFloat() - 0.5D) * (double)this.width, 4.0D * ((double)this.rand.nextFloat() - 0.5D), 0.5D, ((double)this.rand.nextFloat() - 0.5D) * 4.0D);
				}
			}
		}

///    
///     <summary> * Returns true if this entity can attack entities of the specified class. </summary>
///     
		public override bool canAttackClass(Type p_70686_1_)
		{
			return this.PlayerCreated && typeof(EntityPlayer).isAssignableFrom(p_70686_1_) ? false : base.canAttackClass(p_70686_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("PlayerCreated", this.PlayerCreated);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.PlayerCreated = p_70037_1_.getBoolean("PlayerCreated");
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			this.attackTimer = 10;
			this.worldObj.setEntityState(this, (sbyte)4);
			bool var2 = p_70652_1_.attackEntityFrom(DamageSource.causeMobDamage(this), (float)(7 + this.rand.Next(15)));

			if (var2)
			{
				p_70652_1_.motionY += 0.4000000059604645D;
			}

			this.playSound("mob.irongolem.throw", 1.0F, 1.0F);
			return var2;
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 4)
			{
				this.attackTimer = 10;
				this.playSound("mob.irongolem.throw", 1.0F, 1.0F);
			}
			else if (p_70103_1_ == 11)
			{
				this.holdRoseTick = 400;
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

		public virtual Village Village
		{
			get
			{
				return this.villageObj;
			}
		}

		public virtual int AttackTimer
		{
			get
			{
				return this.attackTimer;
			}
		}

		public virtual bool HoldingRose
		{
			set
			{
				this.holdRoseTick = value ? 400 : 0;
				this.worldObj.setEntityState(this, (sbyte)11);
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.irongolem.hit";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.irongolem.death";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.irongolem.walk", 1.0F, 1.0F);
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(3);
			int var4;

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145778_a(Item.getItemFromBlock(Blocks.red_flower), 1, 0.0F);
			}

			var4 = 3 + this.rand.Next(3);

			for (int var5 = 0; var5 < var4; ++var5)
			{
				this.func_145779_a(Items.iron_ingot, 1);
			}
		}

		public virtual int HoldRoseTick
		{
			get
			{
				return this.holdRoseTick;
			}
		}

		public virtual bool isPlayerCreated()
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
			}
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
		}


///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public override void onDeath(DamageSource p_70645_1_)
		{
			if (!this.PlayerCreated && this.attackingPlayer != null && this.villageObj != null)
			{
				this.villageObj.setReputationForPlayer(this.attackingPlayer.CommandSenderName, -5);
			}

			base.onDeath(p_70645_1_);
		}
	}

}