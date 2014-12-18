using System;

namespace DotCraftCore.nEntity.nPassive
{

	using Block = DotCraftCore.nBlock.Block;
	using BlockColored = DotCraftCore.nBlock.BlockColored;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityAgeable = DotCraftCore.nEntity.EntityAgeable;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using EntityAIAttackOnCollide = DotCraftCore.nEntity.nAI.EntityAIAttackOnCollide;
	using EntityAIBeg = DotCraftCore.nEntity.nAI.EntityAIBeg;
	using EntityAIFollowOwner = DotCraftCore.nEntity.nAI.EntityAIFollowOwner;
	using EntityAIHurtByTarget = DotCraftCore.nEntity.nAI.EntityAIHurtByTarget;
	using EntityAILeapAtTarget = DotCraftCore.nEntity.nAI.EntityAILeapAtTarget;
	using EntityAILookIdle = DotCraftCore.nEntity.nAI.EntityAILookIdle;
	using EntityAIMate = DotCraftCore.nEntity.nAI.EntityAIMate;
	using EntityAIOwnerHurtByTarget = DotCraftCore.nEntity.nAI.EntityAIOwnerHurtByTarget;
	using EntityAIOwnerHurtTarget = DotCraftCore.nEntity.nAI.EntityAIOwnerHurtTarget;
	using EntityAISwimming = DotCraftCore.nEntity.nAI.EntityAISwimming;
	using EntityAITargetNonTamed = DotCraftCore.nEntity.nAI.EntityAITargetNonTamed;
	using EntityAIWander = DotCraftCore.nEntity.nAI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.nEntity.nAI.EntityAIWatchClosest;
	using EntityCreeper = DotCraftCore.nEntity.nMonster.EntityCreeper;
	using EntityGhast = DotCraftCore.nEntity.nMonster.EntityGhast;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityArrow = DotCraftCore.nEntity.nProjectile.EntityArrow;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemFood = DotCraftCore.nItem.ItemFood;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using PathEntity = DotCraftCore.nPathfinding.PathEntity;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class EntityWolf : EntityTameable
	{
		private float field_70926_e;
		private float field_70924_f;

	/// <summary> true is the wolf is wet else false  </summary>
		private bool isShaking;
		private bool field_70928_h;

///    
///     <summary> * This time increases while wolf is shaking and emitting water particles. </summary>
///     
		private float timeWolfIsShaking;
		private float prevTimeWolfIsShaking;
		

		public EntityWolf(World p_i1696_1_) : base(p_i1696_1_)
		{
			this.setSize(0.6F, 0.8F);
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, this.aiSit);
			this.tasks.addTask(3, new EntityAILeapAtTarget(this, 0.4F));
			this.tasks.addTask(4, new EntityAIAttackOnCollide(this, 1.0D, true));
			this.tasks.addTask(5, new EntityAIFollowOwner(this, 1.0D, 10.0F, 2.0F));
			this.tasks.addTask(6, new EntityAIMate(this, 1.0D));
			this.tasks.addTask(7, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(8, new EntityAIBeg(this, 8.0F));
			this.tasks.addTask(9, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(9, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIOwnerHurtByTarget(this));
			this.targetTasks.addTask(2, new EntityAIOwnerHurtTarget(this));
			this.targetTasks.addTask(3, new EntityAIHurtByTarget(this, true));
			this.targetTasks.addTask(4, new EntityAITargetNonTamed(this, typeof(EntitySheep), 200, false));
			this.Tamed = false;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.30000001192092896D;

			if (this.Tamed)
			{
				this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 20.0D;
			}
			else
			{
				this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 8.0D;
			}
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
///     <summary> * Sets the active target the Task system uses for tracking </summary>
///     
		public override EntityLivingBase AttackTarget
		{
			set
			{
				base.AttackTarget = value;
	
				if (value == null)
				{
					this.Angry = false;
				}
				else if (!this.Tamed)
				{
					this.Angry = true;
				}
			}
		}

///    
///     <summary> * main AI tick function, replaces updateEntityActionState </summary>
///     
		protected internal override void updateAITick()
		{
			this.dataWatcher.updateObject(18, Convert.ToSingle(this.Health));
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(18, new float?(this.Health));
			this.dataWatcher.addObject(19, new sbyte?((sbyte)0));
			this.dataWatcher.addObject(20, new sbyte?((sbyte)BlockColored.func_150032_b(1)));
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.wolf.step", 0.15F, 1.0F);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("Angry", this.Angry);
			p_70014_1_.setByte("CollarColor", (sbyte)this.CollarColor);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.Angry = p_70037_1_.getBoolean("Angry");

			if (p_70037_1_.func_150297_b("CollarColor", 99))
			{
				this.CollarColor = p_70037_1_.getByte("CollarColor");
			}
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return this.Angry ? "mob.wolf.growl" : (this.rand.Next(3) == 0 ? (this.Tamed && this.dataWatcher.getWatchableObjectFloat(18) < 10.0F ? "mob.wolf.whine" : "mob.wolf.panting") : "mob.wolf.bark");
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.wolf.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.wolf.death";
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
			return Item.getItemById(-1);
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();

			if (!this.worldObj.isClient && this.isShaking && !this.field_70928_h && !this.hasPath() && this.onGround)
			{
				this.field_70928_h = true;
				this.timeWolfIsShaking = 0.0F;
				this.prevTimeWolfIsShaking = 0.0F;
				this.worldObj.setEntityState(this, (sbyte)8);
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();
			this.field_70924_f = this.field_70926_e;

			if (this.func_70922_bv())
			{
				this.field_70926_e += (1.0F - this.field_70926_e) * 0.4F;
			}
			else
			{
				this.field_70926_e += (0.0F - this.field_70926_e) * 0.4F;
			}

			if (this.func_70922_bv())
			{
				this.numTicksToChaseTarget = 10;
			}

			if (this.Wet)
			{
				this.isShaking = true;
				this.field_70928_h = false;
				this.timeWolfIsShaking = 0.0F;
				this.prevTimeWolfIsShaking = 0.0F;
			}
			else if ((this.isShaking || this.field_70928_h) && this.field_70928_h)
			{
				if (this.timeWolfIsShaking == 0.0F)
				{
					this.playSound("mob.wolf.shake", this.SoundVolume, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				}

				this.prevTimeWolfIsShaking = this.timeWolfIsShaking;
				this.timeWolfIsShaking += 0.05F;

				if (this.prevTimeWolfIsShaking >= 2.0F)
				{
					this.isShaking = false;
					this.field_70928_h = false;
					this.prevTimeWolfIsShaking = 0.0F;
					this.timeWolfIsShaking = 0.0F;
				}

				if (this.timeWolfIsShaking > 0.4F)
				{
					float var1 = (float)this.boundingBox.minY;
					int var2 = (int)(MathHelper.sin((this.timeWolfIsShaking - 0.4F) * (float)Math.PI) * 7.0F);

					for (int var3 = 0; var3 < var2; ++var3)
					{
						float var4 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width * 0.5F;
						float var5 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width * 0.5F;
						this.worldObj.spawnParticle("splash", this.posX + (double)var4, (double)(var1 + 0.8F), this.posZ + (double)var5, this.motionX, this.motionY, this.motionZ);
					}
				}
			}
		}

		public virtual bool WolfShaking
		{
			get
			{
				return this.isShaking;
			}
		}

///    
///     <summary> * Used when calculating the amount of shading to apply while the wolf is shaking. </summary>
///     
		public virtual float getShadingWhileShaking(float p_70915_1_)
		{
			return 0.75F + (this.prevTimeWolfIsShaking + (this.timeWolfIsShaking - this.prevTimeWolfIsShaking) * p_70915_1_) / 2.0F * 0.25F;
		}

		public virtual float getShakeAngle(float p_70923_1_, float p_70923_2_)
		{
			float var3 = (this.prevTimeWolfIsShaking + (this.timeWolfIsShaking - this.prevTimeWolfIsShaking) * p_70923_1_ + p_70923_2_) / 1.8F;

			if (var3 < 0.0F)
			{
				var3 = 0.0F;
			}
			else if (var3 > 1.0F)
			{
				var3 = 1.0F;
			}

			return MathHelper.sin(var3 * (float)Math.PI) * MathHelper.sin(var3 * (float)Math.PI * 11.0F) * 0.15F * (float)Math.PI;
		}

		public virtual float getInterestedAngle(float p_70917_1_)
		{
			return (this.field_70924_f + (this.field_70926_e - this.field_70924_f) * p_70917_1_) * 0.15F * (float)Math.PI;
		}

		public override float EyeHeight
		{
			get
			{
				return this.height * 0.8F;
			}
		}

///    
///     <summary> * The speed it takes to move the entityliving's rotationPitch through the faceEntity method. This is only currently
///     * use in wolves. </summary>
///     
		public override int VerticalFaceSpeed
		{
			get
			{
				return this.Sitting ? 20 : base.VerticalFaceSpeed;
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
			else
			{
				Entity var3 = p_70097_1_.Entity;
				this.aiSit.Sitting = false;

				if (var3 != null && !(var3 is EntityPlayer) && !(var3 is EntityArrow))
				{
					p_70097_2_ = (p_70097_2_ + 1.0F) / 2.0F;
				}

				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			int var2 = this.Tamed ? 4 : 2;
			return p_70652_1_.attackEntityFrom(DamageSource.causeMobDamage(this), (float)var2);
		}

		public override bool Tamed
		{
			set
			{
				base.Tamed = value;
	
				if (value)
				{
					this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 20.0D;
				}
				else
				{
					this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 8.0D;
				}
			}
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (this.Tamed)
			{
				if (var2 != null)
				{
					if (var2.Item is ItemFood)
					{
						ItemFood var3 = (ItemFood)var2.Item;

						if (var3.WolfsFavoriteMeat && this.dataWatcher.getWatchableObjectFloat(18) < 20.0F)
						{
							if (!p_70085_1_.capabilities.isCreativeMode)
							{
								--var2.stackSize;
							}

							this.heal((float)var3.func_150905_g(var2));

							if (var2.stackSize <= 0)
							{
								p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, (ItemStack)null);
							}

							return true;
						}
					}
					else if (var2.Item == Items.dye)
					{
						int var4 = BlockColored.func_150032_b(var2.ItemDamage);

						if (var4 != this.CollarColor)
						{
							this.CollarColor = var4;

							if (!p_70085_1_.capabilities.isCreativeMode && --var2.stackSize <= 0)
							{
								p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, (ItemStack)null);
							}

							return true;
						}
					}
				}

				if (this.func_152114_e(p_70085_1_) && !this.worldObj.isClient && !this.isBreedingItem(var2))
				{
					this.aiSit.Sitting = !this.Sitting;
					this.isJumping = false;
					this.PathToEntity = (PathEntity)null;
					this.Target = (Entity)null;
					this.AttackTarget = (EntityLivingBase)null;
				}
			}
			else if (var2 != null && var2.Item == Items.bone && !this.Angry)
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
						this.PathToEntity = (PathEntity)null;
						this.AttackTarget = (EntityLivingBase)null;
						this.aiSit.Sitting = true;
						this.Health = 20.0F;
						this.func_152115_b(p_70085_1_.UniqueID.ToString());
						this.playTameEffect(true);
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

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 8)
			{
				this.field_70928_h = true;
				this.timeWolfIsShaking = 0.0F;
				this.prevTimeWolfIsShaking = 0.0F;
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

		public virtual float TailRotation
		{
			get
			{
				return this.Angry ? 1.5393804F : (this.Tamed ? (0.55F - (20.0F - this.dataWatcher.getWatchableObjectFloat(18)) * 0.02F) * (float)Math.PI : ((float)Math.PI / 5F));
			}
		}

///    
///     <summary> * Checks if the parameter is an item which this animal can be fed to breed it (wheat, carrots or seeds depending on
///     * the animal type) </summary>
///     
		public override bool isBreedingItem(ItemStack p_70877_1_)
		{
			return p_70877_1_ == null ? false : (!(p_70877_1_.Item is ItemFood) ? false : ((ItemFood)p_70877_1_.Item).WolfsFavoriteMeat);
		}

///    
///     <summary> * Will return how many at most can spawn in a chunk at once. </summary>
///     
		public override int MaxSpawnedInChunk
		{
			get
			{
				return 8;
			}
		}

///    
///     <summary> * Determines whether this wolf is angry or not. </summary>
///     
		public virtual bool isAngry()
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 2) != 0;
			}
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
	
				if (value)
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 | 2)));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & -3)));
				}
			}
		}

///    
///     <summary> * Sets whether this wolf is angry or not. </summary>
///     

///    
///     <summary> * Return this wolf's collar color. </summary>
///     
		public virtual int CollarColor
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(20) & 15;
			}
			set
			{
				this.dataWatcher.updateObject(20, Convert.ToByte((sbyte)(value & 15)));
			}
		}

///    
///     <summary> * Set this wolf's collar color. </summary>
///     

		public override EntityWolf createChild(EntityAgeable p_90011_1_)
		{
			EntityWolf var2 = new EntityWolf(this.worldObj);
			string var3 = this.func_152113_b();

			if (var3 != null && var3.Trim().Length > 0)
			{
				var2.func_152115_b(var3);
				var2.Tamed = true;
			}

			return var2;
		}

		public virtual void func_70918_i(bool p_70918_1_)
		{
			if (p_70918_1_)
			{
				this.dataWatcher.updateObject(19, Convert.ToByte((sbyte)1));
			}
			else
			{
				this.dataWatcher.updateObject(19, Convert.ToByte((sbyte)0));
			}
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
			else if (!(p_70878_1_ is EntityWolf))
			{
				return false;
			}
			else
			{
				EntityWolf var2 = (EntityWolf)p_70878_1_;
				return !var2.Tamed ? false : (var2.Sitting ? false : this.InLove && var2.InLove);
			}
		}

		public virtual bool func_70922_bv()
		{
			return this.dataWatcher.getWatchableObjectByte(19) == 1;
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal override bool canDespawn()
		{
			return !this.Tamed && this.ticksExisted > 2400;
		}

		public override bool func_142018_a(EntityLivingBase p_142018_1_, EntityLivingBase p_142018_2_)
		{
			if (!(p_142018_1_ is EntityCreeper) && !(p_142018_1_ is EntityGhast))
			{
				if (p_142018_1_ is EntityWolf)
				{
					EntityWolf var3 = (EntityWolf)p_142018_1_;

					if (var3.Tamed && var3.Owner == p_142018_2_)
					{
						return false;
					}
				}

				return p_142018_1_ is EntityPlayer && p_142018_2_ is EntityPlayer && !((EntityPlayer)p_142018_2_).canAttackPlayer((EntityPlayer)p_142018_1_) ? false : !(p_142018_1_ is EntityHorse) || !((EntityHorse)p_142018_1_).Tame;
			}
			else
			{
				return false;
			}
		}
	}

}