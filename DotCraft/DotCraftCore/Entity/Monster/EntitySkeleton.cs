using System;

namespace DotCraftCore.nEntity.nMonster
{

	using Block = DotCraftCore.nBlock.Block;
	using Enchantment = DotCraftCore.nEnchantment.Enchantment;
	using EnchantmentHelper = DotCraftCore.nEnchantment.EnchantmentHelper;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityCreature = DotCraftCore.nEntity.EntityCreature;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EnumCreatureAttribute = DotCraftCore.nEntity.EnumCreatureAttribute;
	using IEntityLivingData = DotCraftCore.nEntity.IEntityLivingData;
	using IRangedAttackMob = DotCraftCore.nEntity.IRangedAttackMob;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using EntityAIArrowAttack = DotCraftCore.nEntity.nAI.EntityAIArrowAttack;
	using EntityAIAttackOnCollide = DotCraftCore.nEntity.nAI.EntityAIAttackOnCollide;
	using EntityAIFleeSun = DotCraftCore.nEntity.nAI.EntityAIFleeSun;
	using EntityAIHurtByTarget = DotCraftCore.nEntity.nAI.EntityAIHurtByTarget;
	using EntityAILookIdle = DotCraftCore.nEntity.nAI.EntityAILookIdle;
	using EntityAINearestAttackableTarget = DotCraftCore.nEntity.nAI.EntityAINearestAttackableTarget;
	using EntityAIRestrictSun = DotCraftCore.nEntity.nAI.EntityAIRestrictSun;
	using EntityAISwimming = DotCraftCore.nEntity.nAI.EntityAISwimming;
	using EntityAIWander = DotCraftCore.nEntity.nAI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.nEntity.nAI.EntityAIWatchClosest;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityArrow = DotCraftCore.nEntity.nProjectile.EntityArrow;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using Potion = DotCraftCore.nPotion.Potion;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using AchievementList = DotCraftCore.nStats.AchievementList;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;
	using WorldProviderHell = DotCraftCore.nWorld.WorldProviderHell;

	public class EntitySkeleton : EntityMob, IRangedAttackMob
	{
		private EntityAIArrowAttack aiArrowAttack = new EntityAIArrowAttack(this, 1.0D, 20, 60, 15.0F);
		private EntityAIAttackOnCollide aiAttackOnCollide = new EntityAIAttackOnCollide(this, typeof(EntityPlayer), 1.2D, false);
		

		public EntitySkeleton(World p_i1741_1_) : base(p_i1741_1_)
		{
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, new EntityAIRestrictSun(this));
			this.tasks.addTask(3, new EntityAIFleeSun(this, 1.0D));
			this.tasks.addTask(5, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(6, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(6, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIHurtByTarget(this, false));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 0, true));

			if (p_i1741_1_ != null && !p_i1741_1_.isClient)
			{
				this.setCombatTask();
			}
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.25D;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(13, new sbyte?((sbyte)0));
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
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.skeleton.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.skeleton.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.skeleton.death";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.skeleton.step", 0.15F, 1.0F);
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			if (base.attackEntityAsMob(p_70652_1_))
			{
				if (this.SkeletonType == 1 && p_70652_1_ is EntityLivingBase)
				{
					((EntityLivingBase)p_70652_1_).addPotionEffect(new PotionEffect(Potion.wither.id, 200));
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Get this Entity's EnumCreatureAttribute </summary>
///     
		public override EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.UNDEAD;
			}
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (this.worldObj.Daytime && !this.worldObj.isClient)
			{
				float var1 = this.getBrightness(1.0F);

				if (var1 > 0.5F && this.rand.nextFloat() * 30.0F < (var1 - 0.4F) * 2.0F && this.worldObj.canBlockSeeTheSky(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)))
				{
					bool var2 = true;
					ItemStack var3 = this.getEquipmentInSlot(4);

					if (var3 != null)
					{
						if (var3.ItemStackDamageable)
						{
							var3.ItemDamage = var3.ItemDamageForDisplay + this.rand.Next(2);

							if (var3.ItemDamageForDisplay >= var3.MaxDamage)
							{
								this.renderBrokenItemStack(var3);
								this.setCurrentItemOrArmor(4, (ItemStack)null);
							}
						}

						var2 = false;
					}

					if (var2)
					{
						this.Fire = 8;
					}
				}
			}

			if (this.worldObj.isClient && this.SkeletonType == 1)
			{
				this.setSize(0.72F, 2.34F);
			}

			base.onLivingUpdate();
		}

///    
///     <summary> * Handles updating while being ridden by an entity </summary>
///     
		public override void updateRidden()
		{
			base.updateRidden();

			if (this.ridingEntity is EntityCreature)
			{
				EntityCreature var1 = (EntityCreature)this.ridingEntity;
				this.renderYawOffset = var1.renderYawOffset;
			}
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public override void onDeath(DamageSource p_70645_1_)
		{
			base.onDeath(p_70645_1_);

			if (p_70645_1_.SourceOfDamage is EntityArrow && p_70645_1_.Entity is EntityPlayer)
			{
				EntityPlayer var2 = (EntityPlayer)p_70645_1_.Entity;
				double var3 = var2.posX - this.posX;
				double var5 = var2.posZ - this.posZ;

				if (var3 * var3 + var5 * var5 >= 2500.0D)
				{
					var2.triggerAchievement(AchievementList.snipeSkeleton);
				}
			}
		}

		protected internal override Item func_146068_u()
		{
			return Items.arrow;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3;
			int var4;

			if (this.SkeletonType == 1)
			{
				var3 = this.rand.Next(3 + p_70628_2_) - 1;

				for (var4 = 0; var4 < var3; ++var4)
				{
					this.func_145779_a(Items.coal, 1);
				}
			}
			else
			{
				var3 = this.rand.Next(3 + p_70628_2_);

				for (var4 = 0; var4 < var3; ++var4)
				{
					this.func_145779_a(Items.arrow, 1);
				}
			}

			var3 = this.rand.Next(3 + p_70628_2_);

			for (var4 = 0; var4 < var3; ++var4)
			{
				this.func_145779_a(Items.bone, 1);
			}
		}

		protected internal override void dropRareDrop(int p_70600_1_)
		{
			if (this.SkeletonType == 1)
			{
				this.entityDropItem(new ItemStack(Items.skull, 1, 1), 0.0F);
			}
		}

///    
///     <summary> * Makes entity wear random armor based on difficulty </summary>
///     
		protected internal override void addRandomArmor()
		{
			base.addRandomArmor();
			this.setCurrentItemOrArmor(0, new ItemStack(Items.bow));
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			p_110161_1_ = base.onSpawnWithEgg(p_110161_1_);

			if (this.worldObj.provider is WorldProviderHell && this.RNG.Next(5) > 0)
			{
				this.tasks.addTask(4, this.aiAttackOnCollide);
				this.SkeletonType = 1;
				this.setCurrentItemOrArmor(0, new ItemStack(Items.stone_sword));
				this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 4.0D;
			}
			else
			{
				this.tasks.addTask(4, this.aiArrowAttack);
				this.addRandomArmor();
				this.enchantEquipment();
			}

			this.CanPickUpLoot = this.rand.nextFloat() < 0.55F * this.worldObj.func_147462_b(this.posX, this.posY, this.posZ);

			if (this.getEquipmentInSlot(4) == null)
			{
				Calendar var2 = this.worldObj.CurrentDate;

				if (var2.get(2) + 1 == 10 && var2.get(5) == 31 && this.rand.nextFloat() < 0.25F)
				{
					this.setCurrentItemOrArmor(4, new ItemStack(this.rand.nextFloat() < 0.1F ? Blocks.lit_pumpkin : Blocks.pumpkin));
					this.equipmentDropChances[4] = 0.0F;
				}
			}

			return p_110161_1_;
		}

///    
///     <summary> * sets this entity's combat AI. </summary>
///     
		public virtual void setCombatTask()
		{
			this.tasks.removeTask(this.aiAttackOnCollide);
			this.tasks.removeTask(this.aiArrowAttack);
			ItemStack var1 = this.HeldItem;

			if (var1 != null && var1.Item == Items.bow)
			{
				this.tasks.addTask(4, this.aiArrowAttack);
			}
			else
			{
				this.tasks.addTask(4, this.aiAttackOnCollide);
			}
		}

///    
///     <summary> * Attack the specified entity using a ranged attack. </summary>
///     
		public virtual void attackEntityWithRangedAttack(EntityLivingBase p_82196_1_, float p_82196_2_)
		{
			EntityArrow var3 = new EntityArrow(this.worldObj, this, p_82196_1_, 1.6F, (float)(14 - this.worldObj.difficultySetting.DifficultyId * 4));
			int var4 = EnchantmentHelper.getEnchantmentLevel(Enchantment.power.effectId, this.HeldItem);
			int var5 = EnchantmentHelper.getEnchantmentLevel(Enchantment.punch.effectId, this.HeldItem);
			var3.Damage = (double)(p_82196_2_ * 2.0F) + this.rand.nextGaussian() * 0.25D + (double)((float)this.worldObj.difficultySetting.DifficultyId * 0.11F);

			if (var4 > 0)
			{
				var3.Damage = var3.Damage + (double)var4 * 0.5D + 0.5D;
			}

			if (var5 > 0)
			{
				var3.KnockbackStrength = var5;
			}

			if (EnchantmentHelper.getEnchantmentLevel(Enchantment.flame.effectId, this.HeldItem) > 0 || this.SkeletonType == 1)
			{
				var3.Fire = 100;
			}

			this.playSound("random.bow", 1.0F, 1.0F / (this.RNG.nextFloat() * 0.4F + 0.8F));
			this.worldObj.spawnEntityInWorld(var3);
		}

///    
///     <summary> * Return this skeleton's type. </summary>
///     
		public virtual int SkeletonType
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(13);
			}
			set
			{
				this.dataWatcher.updateObject(13, Convert.ToByte((sbyte)value));
				this.isImmuneToFire = value == 1;
	
				if (value == 1)
				{
					this.setSize(0.72F, 2.34F);
				}
				else
				{
					this.setSize(0.6F, 1.8F);
				}
			}
		}

///    
///     <summary> * Set this skeleton's type. </summary>
///     

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);

			if (p_70037_1_.func_150297_b("SkeletonType", 99))
			{
				sbyte var2 = p_70037_1_.getByte("SkeletonType");
				this.SkeletonType = var2;
			}

			this.setCombatTask();
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setByte("SkeletonType", (sbyte)this.SkeletonType);
		}

///    
///     <summary> * Sets the held item, or an armor slot. Slot 0 is held item. Slot 1-4 is armor. Params: Item, slot </summary>
///     
		public override void setCurrentItemOrArmor(int p_70062_1_, ItemStack p_70062_2_)
		{
			base.setCurrentItemOrArmor(p_70062_1_, p_70062_2_);

			if (!this.worldObj.isClient && p_70062_1_ == 0)
			{
				this.setCombatTask();
			}
		}

///    
///     <summary> * Returns the Y Offset of this entity. </summary>
///     
		public override double YOffset
		{
			get
			{
				return base.YOffset - 0.5D;
			}
		}
	}

}