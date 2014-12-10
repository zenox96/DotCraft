using System;
using System.Collections;

namespace DotCraftCore.Entity.Monster
{

	using Block = DotCraftCore.block.Block;
	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EnumCreatureAttribute = DotCraftCore.Entity.EnumCreatureAttribute;
	using IEntityLivingData = DotCraftCore.Entity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIAttackOnCollide = DotCraftCore.Entity.AI.EntityAIAttackOnCollide;
	using EntityAIBreakDoor = DotCraftCore.Entity.AI.EntityAIBreakDoor;
	using EntityAIHurtByTarget = DotCraftCore.Entity.AI.EntityAIHurtByTarget;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAIMoveThroughVillage = DotCraftCore.Entity.AI.EntityAIMoveThroughVillage;
	using EntityAIMoveTowardsRestriction = DotCraftCore.Entity.AI.EntityAIMoveTowardsRestriction;
	using EntityAINearestAttackableTarget = DotCraftCore.Entity.AI.EntityAINearestAttackableTarget;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using AttributeModifier = DotCraftCore.Entity.AI.Attributes.AttributeModifier;
	using IAttribute = DotCraftCore.Entity.AI.Attributes.IAttribute;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using RangedAttribute = DotCraftCore.Entity.AI.Attributes.RangedAttribute;
	using EntityChicken = DotCraftCore.Entity.Passive.EntityChicken;
	using EntityVillager = DotCraftCore.Entity.Passive.EntityVillager;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using Potion = DotCraftCore.potion.Potion;
	using PotionEffect = DotCraftCore.potion.PotionEffect;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using EnumDifficulty = DotCraftCore.world.EnumDifficulty;
	using World = DotCraftCore.world.World;

	public class EntityZombie : EntityMob
	{
		protected internal static readonly IAttribute field_110186_bp = (new RangedAttribute("zombie.spawnReinforcements", 0.0D, 0.0D, 1.0D)).Description = "Spawn Reinforcements Chance";
		private static readonly UUID babySpeedBoostUUID = UUID.fromString("B9766B59-9566-4402-BC1F-2EE2A276D836");
		private static readonly AttributeModifier babySpeedBoostModifier = new AttributeModifier(babySpeedBoostUUID, "Baby speed boost", 0.5D, 1);
		private readonly EntityAIBreakDoor field_146075_bs = new EntityAIBreakDoor(this);

///    
///     <summary> * Ticker used to determine the time remaining for this zombie to convert into a villager when cured. </summary>
///     
		private int conversionTime;
		private bool field_146076_bu = false;
		private float field_146074_bv = -1.0F;
		private float field_146073_bw;
		private const string __OBFID = "CL_00001702";

		public EntityZombie(World p_i1745_1_) : base(p_i1745_1_)
		{
			this.Navigator.BreakDoors = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(2, new EntityAIAttackOnCollide(this, typeof(EntityPlayer), 1.0D, false));
			this.tasks.addTask(4, new EntityAIAttackOnCollide(this, typeof(EntityVillager), 1.0D, true));
			this.tasks.addTask(5, new EntityAIMoveTowardsRestriction(this, 1.0D));
			this.tasks.addTask(6, new EntityAIMoveThroughVillage(this, 1.0D, false));
			this.tasks.addTask(7, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(8, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(8, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIHurtByTarget(this, true));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 0, true));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityVillager), 0, false));
			this.setSize(0.6F, 1.8F);
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.followRange).BaseValue = 40.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.23000000417232513D;
			this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 3.0D;
			this.AttributeMap.registerAttribute(field_110186_bp).BaseValue = this.rand.NextDouble() * 0.10000000149011612D;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.DataWatcher.addObject(12, Convert.ToByte((sbyte)0));
			this.DataWatcher.addObject(13, Convert.ToByte((sbyte)0));
			this.DataWatcher.addObject(14, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * Returns the current armor value as determined by a call to InventoryPlayer.getTotalArmorValue </summary>
///     
		public override int TotalArmorValue
		{
			get
			{
				int var1 = base.TotalArmorValue + 2;
	
				if (var1 > 20)
				{
					var1 = 20;
				}
	
				return var1;
			}
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		protected internal override bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

		public virtual bool func_146072_bX()
		{
			return this.field_146076_bu;
		}

		public virtual void func_146070_a(bool p_146070_1_)
		{
			if (this.field_146076_bu != p_146070_1_)
			{
				this.field_146076_bu = p_146070_1_;

				if (p_146070_1_)
				{
					this.tasks.addTask(1, this.field_146075_bs);
				}
				else
				{
					this.tasks.removeTask(this.field_146075_bs);
				}
			}
		}

///    
///     <summary> * If Animal, checks if the age timer is negative </summary>
///     
		public override bool isChild()
		{
			get
			{
				return this.DataWatcher.getWatchableObjectByte(12) == 1;
			}
			set
			{
				this.DataWatcher.updateObject(12, Convert.ToByte((sbyte)(value ? 1 : 0)));
	
				if (this.worldObj != null && !this.worldObj.isClient)
				{
					IAttributeInstance var2 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);
					var2.removeModifier(babySpeedBoostModifier);
	
					if (value)
					{
						var2.applyModifier(babySpeedBoostModifier);
					}
				}
	
				this.func_146071_k(value);
			}
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal override int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			if (this.Child)
			{
				this.experienceValue = (int)((float)this.experienceValue * 2.5F);
			}

			return base.getExperiencePoints(p_70693_1_);
		}

///    
///     <summary> * Set whether this zombie is a child. </summary>
///     

///    
///     <summary> * Return whether this zombie is a villager. </summary>
///     
		public virtual bool isVillager()
		{
			get
			{
				return this.DataWatcher.getWatchableObjectByte(13) == 1;
			}
			set
			{
				this.DataWatcher.updateObject(13, Convert.ToByte((sbyte)(value ? 1 : 0)));
			}
		}

///    
///     <summary> * Set whether this zombie is a villager. </summary>
///     

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (this.worldObj.Daytime && !this.worldObj.isClient && !this.Child)
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

			if (this.Riding && this.AttackTarget != null && this.ridingEntity is EntityChicken)
			{
				((EntityLiving)this.ridingEntity).Navigator.setPath(this.Navigator.Path, 1.5D);
			}

			base.onLivingUpdate();
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (!base.attackEntityFrom(p_70097_1_, p_70097_2_))
			{
				return false;
			}
			else
			{
				EntityLivingBase var3 = this.AttackTarget;

				if (var3 == null && this.EntityToAttack is EntityLivingBase)
				{
					var3 = (EntityLivingBase)this.EntityToAttack;
				}

				if (var3 == null && p_70097_1_.Entity is EntityLivingBase)
				{
					var3 = (EntityLivingBase)p_70097_1_.Entity;
				}

				if (var3 != null && this.worldObj.difficultySetting == EnumDifficulty.HARD && (double)this.rand.nextFloat() < this.getEntityAttribute(field_110186_bp).AttributeValue)
				{
					int var4 = MathHelper.floor_double(this.posX);
					int var5 = MathHelper.floor_double(this.posY);
					int var6 = MathHelper.floor_double(this.posZ);
					EntityZombie var7 = new EntityZombie(this.worldObj);

					for (int var8 = 0; var8 < 50; ++var8)
					{
						int var9 = var4 + MathHelper.getRandomIntegerInRange(this.rand, 7, 40) * MathHelper.getRandomIntegerInRange(this.rand, -1, 1);
						int var10 = var5 + MathHelper.getRandomIntegerInRange(this.rand, 7, 40) * MathHelper.getRandomIntegerInRange(this.rand, -1, 1);
						int var11 = var6 + MathHelper.getRandomIntegerInRange(this.rand, 7, 40) * MathHelper.getRandomIntegerInRange(this.rand, -1, 1);

						if (World.doesBlockHaveSolidTopSurface(this.worldObj, var9, var10 - 1, var11) && this.worldObj.getBlockLightValue(var9, var10, var11) < 10)
						{
							var7.setPosition((double)var9, (double)var10, (double)var11);

							if (this.worldObj.checkNoEntityCollision(var7.boundingBox) && this.worldObj.getCollidingBoundingBoxes(var7, var7.boundingBox).Empty && !this.worldObj.isAnyLiquid(var7.boundingBox))
							{
								this.worldObj.spawnEntityInWorld(var7);
								var7.AttackTarget = var3;
								var7.onSpawnWithEgg((IEntityLivingData)null);
								this.getEntityAttribute(field_110186_bp).applyModifier(new AttributeModifier("Zombie reinforcement caller charge", -0.05000000074505806D, 0));
								var7.getEntityAttribute(field_110186_bp).applyModifier(new AttributeModifier("Zombie reinforcement callee charge", -0.05000000074505806D, 0));
								break;
							}
						}
					}
				}

				return true;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (!this.worldObj.isClient && this.Converting)
			{
				int var1 = this.ConversionTimeBoost;
				this.conversionTime -= var1;

				if (this.conversionTime <= 0)
				{
					this.convertToVillager();
				}
			}

			base.onUpdate();
		}

		public override bool attackEntityAsMob(Entity p_70652_1_)
		{
			bool var2 = base.attackEntityAsMob(p_70652_1_);

			if (var2)
			{
				int var3 = this.worldObj.difficultySetting.DifficultyId;

				if (this.HeldItem == null && this.Burning && this.rand.nextFloat() < (float)var3 * 0.3F)
				{
					p_70652_1_.Fire = 2 * var3;
				}
			}

			return var2;
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.zombie.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.zombie.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.zombie.death";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.zombie.step", 0.15F, 1.0F);
		}

		protected internal override Item func_146068_u()
		{
			return Items.rotten_flesh;
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

		protected internal override void dropRareDrop(int p_70600_1_)
		{
			switch (this.rand.Next(3))
			{
				case 0:
					this.func_145779_a(Items.iron_ingot, 1);
					break;

				case 1:
					this.func_145779_a(Items.carrot, 1);
					break;

				case 2:
					this.func_145779_a(Items.potato, 1);
				break;
			}
		}

///    
///     <summary> * Makes entity wear random armor based on difficulty </summary>
///     
		protected internal override void addRandomArmor()
		{
			base.addRandomArmor();

			if (this.rand.nextFloat() < (this.worldObj.difficultySetting == EnumDifficulty.HARD ? 0.05F : 0.01F))
			{
				int var1 = this.rand.Next(3);

				if (var1 == 0)
				{
					this.setCurrentItemOrArmor(0, new ItemStack(Items.iron_sword));
				}
				else
				{
					this.setCurrentItemOrArmor(0, new ItemStack(Items.iron_shovel));
				}
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);

			if (this.Child)
			{
				p_70014_1_.setBoolean("IsBaby", true);
			}

			if (this.Villager)
			{
				p_70014_1_.setBoolean("IsVillager", true);
			}

			p_70014_1_.setInteger("ConversionTime", this.Converting ? this.conversionTime : -1);
			p_70014_1_.setBoolean("CanBreakDoors", this.func_146072_bX());
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);

			if (p_70037_1_.getBoolean("IsBaby"))
			{
				this.Child = true;
			}

			if (p_70037_1_.getBoolean("IsVillager"))
			{
				this.Villager = true;
			}

			if (p_70037_1_.func_150297_b("ConversionTime", 99) && p_70037_1_.getInteger("ConversionTime") > -1)
			{
				this.startConversion(p_70037_1_.getInteger("ConversionTime"));
			}

			this.func_146070_a(p_70037_1_.getBoolean("CanBreakDoors"));
		}

///    
///     <summary> * This method gets called when the entity kills another one. </summary>
///     
		public override void onKillEntity(EntityLivingBase p_70074_1_)
		{
			base.onKillEntity(p_70074_1_);

			if ((this.worldObj.difficultySetting == EnumDifficulty.NORMAL || this.worldObj.difficultySetting == EnumDifficulty.HARD) && p_70074_1_ is EntityVillager)
			{
				if (this.worldObj.difficultySetting != EnumDifficulty.HARD && this.rand.nextBoolean())
				{
					return;
				}

				EntityZombie var2 = new EntityZombie(this.worldObj);
				var2.copyLocationAndAnglesFrom(p_70074_1_);
				this.worldObj.removeEntity(p_70074_1_);
				var2.onSpawnWithEgg((IEntityLivingData)null);
				var2.Villager = true;

				if (p_70074_1_.Child)
				{
					var2.Child = true;
				}

				this.worldObj.spawnEntityInWorld(var2);
				this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1016, (int)this.posX, (int)this.posY, (int)this.posZ, 0);
			}
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			object p_110161_1_1 = base.onSpawnWithEgg(p_110161_1_);
			float var2 = this.worldObj.func_147462_b(this.posX, this.posY, this.posZ);
			this.CanPickUpLoot = this.rand.nextFloat() < 0.55F * var2;

			if (p_110161_1_1 == null)
			{
				p_110161_1_1 = new EntityZombie.GroupData(this.worldObj.rand.nextFloat() < 0.05F, this.worldObj.rand.nextFloat() < 0.05F, null);
			}

			if (p_110161_1_1 is EntityZombie.GroupData)
			{
				EntityZombie.GroupData var3 = (EntityZombie.GroupData)p_110161_1_1;

				if (var3.field_142046_b)
				{
					this.Villager = true;
				}

				if (var3.field_142048_a)
				{
					this.Child = true;

					if ((double)this.worldObj.rand.nextFloat() < 0.05D)
					{
						IList var4 = this.worldObj.selectEntitiesWithinAABB(typeof(EntityChicken), this.boundingBox.expand(5.0D, 3.0D, 5.0D), IEntitySelector.field_152785_b);

						if (!var4.Count == 0)
						{
							EntityChicken var5 = (EntityChicken)var4[0];
							var5.func_152117_i(true);
							this.mountEntity(var5);
						}
					}
					else if ((double)this.worldObj.rand.nextFloat() < 0.05D)
					{
						EntityChicken var9 = new EntityChicken(this.worldObj);
						var9.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, 0.0F);
						var9.onSpawnWithEgg((IEntityLivingData)null);
						var9.func_152117_i(true);
						this.worldObj.spawnEntityInWorld(var9);
						this.mountEntity(var9);
					}
				}
			}

			this.func_146070_a(this.rand.nextFloat() < var2 * 0.1F);
			this.addRandomArmor();
			this.enchantEquipment();

			if (this.getEquipmentInSlot(4) == null)
			{
				Calendar var7 = this.worldObj.CurrentDate;

				if (var7.get(2) + 1 == 10 && var7.get(5) == 31 && this.rand.nextFloat() < 0.25F)
				{
					this.setCurrentItemOrArmor(4, new ItemStack(this.rand.nextFloat() < 0.1F ? Blocks.lit_pumpkin : Blocks.pumpkin));
					this.equipmentDropChances[4] = 0.0F;
				}
			}

			this.getEntityAttribute(SharedMonsterAttributes.knockbackResistance).applyModifier(new AttributeModifier("Random spawn bonus", this.rand.NextDouble() * 0.05000000074505806D, 0));
			double var8 = this.rand.NextDouble() * 1.5D * (double)this.worldObj.func_147462_b(this.posX, this.posY, this.posZ);

			if (var8 > 1.0D)
			{
				this.getEntityAttribute(SharedMonsterAttributes.followRange).applyModifier(new AttributeModifier("Random zombie-spawn bonus", var8, 2));
			}

			if (this.rand.nextFloat() < var2 * 0.05F)
			{
				this.getEntityAttribute(field_110186_bp).applyModifier(new AttributeModifier("Leader zombie bonus", this.rand.NextDouble() * 0.25D + 0.5D, 0));
				this.getEntityAttribute(SharedMonsterAttributes.maxHealth).applyModifier(new AttributeModifier("Leader zombie bonus", this.rand.NextDouble() * 3.0D + 1.0D, 2));
				this.func_146070_a(true);
			}

			return (IEntityLivingData)p_110161_1_1;
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.CurrentEquippedItem;

			if (var2 != null && var2.Item == Items.golden_apple && var2.ItemDamage == 0 && this.Villager && this.isPotionActive(Potion.weakness))
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
					this.startConversion(this.rand.Next(2401) + 3600);
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Starts converting this zombie into a villager. The zombie converts into a villager after the specified time in
///     * ticks. </summary>
///     
		protected internal virtual void startConversion(int p_82228_1_)
		{
			this.conversionTime = p_82228_1_;
			this.DataWatcher.updateObject(14, Convert.ToByte((sbyte)1));
			this.removePotionEffect(Potion.weakness.id);
			this.addPotionEffect(new PotionEffect(Potion.damageBoost.id, p_82228_1_, Math.Min(this.worldObj.difficultySetting.DifficultyId - 1, 0)));
			this.worldObj.setEntityState(this, (sbyte)16);
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 16)
			{
				this.worldObj.playSound(this.posX + 0.5D, this.posY + 0.5D, this.posZ + 0.5D, "mob.zombie.remedy", 1.0F + this.rand.nextFloat(), this.rand.nextFloat() * 0.7F + 0.3F, false);
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal override bool canDespawn()
		{
			return !this.Converting;
		}

///    
///     <summary> * Returns whether this zombie is in the process of converting to a villager </summary>
///     
		public virtual bool isConverting()
		{
			get
			{
				return this.DataWatcher.getWatchableObjectByte(14) == 1;
			}
		}

///    
///     <summary> * Convert this zombie into a villager. </summary>
///     
		protected internal virtual void convertToVillager()
		{
			EntityVillager var1 = new EntityVillager(this.worldObj);
			var1.copyLocationAndAnglesFrom(this);
			var1.onSpawnWithEgg((IEntityLivingData)null);
			var1.setLookingForHome();

			if (this.Child)
			{
				var1.GrowingAge = -24000;
			}

			this.worldObj.removeEntity(this);
			this.worldObj.spawnEntityInWorld(var1);
			var1.addPotionEffect(new PotionEffect(Potion.confusion.id, 200, 0));
			this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1017, (int)this.posX, (int)this.posY, (int)this.posZ, 0);
		}

///    
///     <summary> * Return the amount of time decremented from conversionTime every tick. </summary>
///     
		protected internal virtual int ConversionTimeBoost
		{
			get
			{
				int var1 = 1;
	
				if (this.rand.nextFloat() < 0.01F)
				{
					int var2 = 0;
	
					for (int var3 = (int)this.posX - 4; var3 < (int)this.posX + 4 && var2 < 14; ++var3)
					{
						for (int var4 = (int)this.posY - 4; var4 < (int)this.posY + 4 && var2 < 14; ++var4)
						{
							for (int var5 = (int)this.posZ - 4; var5 < (int)this.posZ + 4 && var2 < 14; ++var5)
							{
								Block var6 = this.worldObj.getBlock(var3, var4, var5);
	
								if (var6 == Blocks.iron_bars || var6 == Blocks.bed)
								{
									if (this.rand.nextFloat() < 0.3F)
									{
										++var1;
									}
	
									++var2;
								}
							}
						}
					}
				}
	
				return var1;
			}
		}

		public virtual void func_146071_k(bool p_146071_1_)
		{
			this.func_146069_a(p_146071_1_ ? 0.5F : 1.0F);
		}

///    
///     <summary> * Sets the width and height of the entity. Args: width, height </summary>
///     
		protected internal sealed override void setSize(float p_70105_1_, float p_70105_2_)
		{
			bool var3 = this.field_146074_bv > 0.0F && this.field_146073_bw > 0.0F;
			this.field_146074_bv = p_70105_1_;
			this.field_146073_bw = p_70105_2_;

			if (!var3)
			{
				this.func_146069_a(1.0F);
			}
		}

		protected internal void func_146069_a(float p_146069_1_)
		{
			base.setSize(this.field_146074_bv * p_146069_1_, this.field_146073_bw * p_146069_1_);
		}

		internal class GroupData : IEntityLivingData
		{
			public bool field_142048_a;
			public bool field_142046_b;
			private const string __OBFID = "CL_00001704";

			private GroupData(bool p_i2348_2_, bool p_i2348_3_)
			{
				this.field_142048_a = false;
				this.field_142046_b = false;
				this.field_142048_a = p_i2348_2_;
				this.field_142046_b = p_i2348_3_;
			}

			internal GroupData(bool p_i2349_2_, bool p_i2349_3_, object p_i2349_4_) : this(p_i2349_2_, p_i2349_3_)
			{
			}
		}
	}

}