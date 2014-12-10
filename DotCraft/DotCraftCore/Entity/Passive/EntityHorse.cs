using System;
using System.Collections;

namespace DotCraftCore.Entity.Passive
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityAgeable = DotCraftCore.Entity.EntityAgeable;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IEntityLivingData = DotCraftCore.Entity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIFollowParent = DotCraftCore.Entity.AI.EntityAIFollowParent;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAIMate = DotCraftCore.Entity.AI.EntityAIMate;
	using EntityAIPanic = DotCraftCore.Entity.AI.EntityAIPanic;
	using EntityAIRunAroundLikeCrazy = DotCraftCore.Entity.AI.EntityAIRunAroundLikeCrazy;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using IAttribute = DotCraftCore.Entity.AI.Attributes.IAttribute;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using RangedAttribute = DotCraftCore.Entity.AI.Attributes.RangedAttribute;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using AnimalChest = DotCraftCore.Inventory.AnimalChest;
	using IInvBasic = DotCraftCore.Inventory.IInvBasic;
	using InventoryBasic = DotCraftCore.Inventory.InventoryBasic;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using PathEntity = DotCraftCore.pathfinding.PathEntity;
	using Potion = DotCraftCore.potion.Potion;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using StatCollector = DotCraftCore.util.StatCollector;
	using World = DotCraftCore.world.World;

	public class EntityHorse : EntityAnimal, IInvBasic
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private static final IEntitySelector horseBreedingSelector = new IEntitySelector()
//	{
//		private static final String __OBFID = "CL_00001642";
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			return p_82704_1_ instanceof EntityHorse && ((EntityHorse)p_82704_1_).func_110205_ce();
//		}
//	};
		private static readonly IAttribute horseJumpStrength = (new RangedAttribute("horse.jumpStrength", 0.7D, 0.0D, 2.0D)).setDescription("Jump Strength").setShouldWatch(true);
		private static readonly string[] horseArmorTextures = new string[] {null, "textures/entity/horse/armor/horse_armor_iron.png", "textures/entity/horse/armor/horse_armor_gold.png", "textures/entity/horse/armor/horse_armor_diamond.png"};
		private static readonly string[] field_110273_bx = new string[] {"", "meo", "goo", "dio"};
		private static readonly int[] armorValues = new int[] {0, 5, 7, 11};
		private static readonly string[] horseTextures = new string[] {"textures/entity/horse/horse_white.png", "textures/entity/horse/horse_creamy.png", "textures/entity/horse/horse_chestnut.png", "textures/entity/horse/horse_brown.png", "textures/entity/horse/horse_black.png", "textures/entity/horse/horse_gray.png", "textures/entity/horse/horse_darkbrown.png"};
		private static readonly string[] field_110269_bA = new string[] {"hwh", "hcr", "hch", "hbr", "hbl", "hgr", "hdb"};
		private static readonly string[] horseMarkingTextures = new string[] {null, "textures/entity/horse/horse_markings_white.png", "textures/entity/horse/horse_markings_whitefield.png", "textures/entity/horse/horse_markings_whitedots.png", "textures/entity/horse/horse_markings_blackdots.png"};
		private static readonly string[] field_110292_bC = new string[] {"", "wo_", "wmo", "wdo", "bdo"};
		private int eatingHaystackCounter;
		private int openMouthCounter;
		private int jumpRearingCounter;
		public int field_110278_bp;
		public int field_110279_bq;
		protected internal bool horseJumping;
		private AnimalChest horseChest;
		private bool hasReproduced;

///    
///     <summary> * "The higher this value, the more likely the horse is to be tamed next time a player rides it." </summary>
///     
		protected internal int temper;
		protected internal float jumpPower;
		private bool field_110294_bI;
		private float headLean;
		private float prevHeadLean;
		private float rearingAmount;
		private float prevRearingAmount;
		private float mouthOpenness;
		private float prevMouthOpenness;
		private int field_110285_bP;
		private string field_110286_bQ;
		private string[] field_110280_bR = new string[3];
		private const string __OBFID = "CL_00001641";

		public EntityHorse(World p_i1685_1_) : base(p_i1685_1_)
		{
			this.setSize(1.4F, 1.6F);
			this.isImmuneToFire = false;
			this.Chested = false;
			this.Navigator.AvoidsWater = true;
			this.tasks.addTask(0, new EntityAISwimming(this));
			this.tasks.addTask(1, new EntityAIPanic(this, 1.2D));
			this.tasks.addTask(1, new EntityAIRunAroundLikeCrazy(this, 1.2D));
			this.tasks.addTask(2, new EntityAIMate(this, 1.0D));
			this.tasks.addTask(4, new EntityAIFollowParent(this, 1.0D));
			this.tasks.addTask(6, new EntityAIWander(this, 0.7D));
			this.tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(8, new EntityAILookIdle(this));
			this.func_110226_cD();
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToInt32(0));
			this.dataWatcher.addObject(19, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(20, Convert.ToInt32(0));
			this.dataWatcher.addObject(21, Convert.ToString(""));
			this.dataWatcher.addObject(22, Convert.ToInt32(0));
		}

		public virtual int HorseType
		{
			set
			{
				this.dataWatcher.updateObject(19, Convert.ToByte((sbyte)value));
				this.func_110230_cF();
			}
			get
			{
				return this.dataWatcher.getWatchableObjectByte(19);
			}
		}

///    
///     <summary> * returns the horse type </summary>
///     

		public virtual int HorseVariant
		{
			set
			{
				this.dataWatcher.updateObject(20, Convert.ToInt32(value));
				this.func_110230_cF();
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(20);
			}
		}


///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public override string CommandSenderName
		{
			get
			{
				if (this.hasCustomNameTag())
				{
					return this.CustomNameTag;
				}
				else
				{
					int var1 = this.HorseType;
	
					switch (var1)
					{
						case 0:
						default:
							return StatCollector.translateToLocal("entity.horse.name");
	
						case 1:
							return StatCollector.translateToLocal("entity.donkey.name");
	
						case 2:
							return StatCollector.translateToLocal("entity.mule.name");
	
						case 3:
							return StatCollector.translateToLocal("entity.zombiehorse.name");
	
						case 4:
							return StatCollector.translateToLocal("entity.skeletonhorse.name");
					}
				}
			}
		}

		private bool getHorseWatchableBoolean(int p_110233_1_)
		{
			return (this.dataWatcher.getWatchableObjectInt(16) & p_110233_1_) != 0;
		}

		private void setHorseWatchableBoolean(int p_110208_1_, bool p_110208_2_)
		{
			int var3 = this.dataWatcher.getWatchableObjectInt(16);

			if (p_110208_2_)
			{
				this.dataWatcher.updateObject(16, Convert.ToInt32(var3 | p_110208_1_));
			}
			else
			{
				this.dataWatcher.updateObject(16, Convert.ToInt32(var3 & ~p_110208_1_));
			}
		}

		public virtual bool isAdultHorse()
		{
			get
			{
				return !this.Child;
			}
		}

		public virtual bool isTame()
		{
			get
			{
				return this.getHorseWatchableBoolean(2);
			}
		}

		public virtual bool func_110253_bW()
		{
			return this.AdultHorse;
		}

		public virtual string func_152119_ch()
		{
			return this.dataWatcher.getWatchableObjectString(21);
		}

		public virtual void func_152120_b(string p_152120_1_)
		{
			this.dataWatcher.updateObject(21, p_152120_1_);
		}

		public virtual float HorseSize
		{
			get
			{
				int var1 = this.GrowingAge;
				return var1 >= 0 ? 1.0F : 0.5F + (float)(-24000 - var1) / -24000.0F * 0.5F;
			}
		}

///    
///     <summary> * "Sets the scale for an ageable entity according to the boolean parameter, which says if it's a child." </summary>
///     
		public override bool ScaleForAge
		{
			set
			{
				if (value)
				{
					this.Scale = this.HorseSize;
				}
				else
				{
					this.Scale = 1.0F;
				}
			}
		}

		public virtual bool isHorseJumping()
		{
			get
			{
				return this.horseJumping;
			}
			set
			{
				this.horseJumping = value;
			}
		}

		public virtual bool HorseTamed
		{
			set
			{
				this.setHorseWatchableBoolean(2, value);
			}
		}


		public override bool allowLeashing()
		{
			return !this.func_110256_cu() && base.allowLeashing();
		}

		protected internal override void func_142017_o(float p_142017_1_)
		{
			if (p_142017_1_ > 6.0F && this.EatingHaystack)
			{
				this.EatingHaystack = false;
			}
		}

		public virtual bool isChested()
		{
			get
			{
				return this.getHorseWatchableBoolean(8);
			}
			set
			{
				this.setHorseWatchableBoolean(8, value);
			}
		}

		public virtual int func_110241_cb()
		{
			return this.dataWatcher.getWatchableObjectInt(22);
		}

///    
///     <summary> * 0 = iron, 1 = gold, 2 = diamond </summary>
///     
		private int getHorseArmorIndex(ItemStack p_110260_1_)
		{
			if (p_110260_1_ == null)
			{
				return 0;
			}
			else
			{
				Item var2 = p_110260_1_.Item;
				return var2 == Items.iron_horse_armor ? 1 : (var2 == Items.golden_horse_armor ? 2 : (var2 == Items.diamond_horse_armor ? 3 : 0));
			}
		}

		public virtual bool isEatingHaystack()
		{
			get
			{
				return this.getHorseWatchableBoolean(32);
			}
			set
			{
				this.Eating = value;
			}
		}

		public virtual bool isRearing()
		{
			get
			{
				return this.getHorseWatchableBoolean(64);
			}
			set
			{
				if (value)
				{
					this.EatingHaystack = false;
				}
	
				this.setHorseWatchableBoolean(64, value);
			}
		}

		public virtual bool func_110205_ce()
		{
			return this.getHorseWatchableBoolean(16);
		}

		public virtual bool HasReproduced
		{
			get
			{
				return this.hasReproduced;
			}
			set
			{
				this.hasReproduced = value;
			}
		}

		public virtual void func_146086_d(ItemStack p_146086_1_)
		{
			this.dataWatcher.updateObject(22, Convert.ToInt32(this.getHorseArmorIndex(p_146086_1_)));
			this.func_110230_cF();
		}

		public virtual void func_110242_l(bool p_110242_1_)
		{
			this.setHorseWatchableBoolean(16, p_110242_1_);
		}



		public virtual bool HorseSaddled
		{
			set
			{
				this.setHorseWatchableBoolean(4, value);
			}
			get
			{
				return this.getHorseWatchableBoolean(4);
			}
		}

		public virtual int Temper
		{
			get
			{
				return this.temper;
			}
			set
			{
				this.temper = value;
			}
		}


		public virtual int increaseTemper(int p_110198_1_)
		{
			int var2 = MathHelper.clamp_int(this.Temper + p_110198_1_, 0, this.MaxTemper);
			this.Temper = var2;
			return var2;
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			Entity var3 = p_70097_1_.Entity;
			return this.riddenByEntity != null && this.riddenByEntity.Equals(var3) ? false : base.attackEntityFrom(p_70097_1_, p_70097_2_);
		}

///    
///     <summary> * Returns the current armor value as determined by a call to InventoryPlayer.getTotalArmorValue </summary>
///     
		public override int TotalArmorValue
		{
			get
			{
				return armorValues[this.func_110241_cb()];
			}
		}

///    
///     <summary> * Returns true if this entity should push and be pushed by other entities when colliding. </summary>
///     
		public override bool canBePushed()
		{
			return this.riddenByEntity == null;
		}

		public virtual bool prepareChunkForSpawn()
		{
			int var1 = MathHelper.floor_double(this.posX);
			int var2 = MathHelper.floor_double(this.posZ);
			this.worldObj.getBiomeGenForCoords(var1, var2);
			return true;
		}

		public virtual void dropChests()
		{
			if (!this.worldObj.isClient && this.Chested)
			{
				this.func_145779_a(Item.getItemFromBlock(Blocks.chest), 1);
				this.Chested = false;
			}
		}

		private void func_110266_cB()
		{
			this.openHorseMouth();
			this.worldObj.playSoundAtEntity(this, "eating", 1.0F, 1.0F + (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F);
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
			if (p_70069_1_ > 1.0F)
			{
				this.playSound("mob.horse.land", 0.4F, 1.0F);
			}

			int var2 = MathHelper.ceiling_float_int(p_70069_1_ * 0.5F - 3.0F);

			if (var2 > 0)
			{
				this.attackEntityFrom(DamageSource.fall, (float)var2);

				if (this.riddenByEntity != null)
				{
					this.riddenByEntity.attackEntityFrom(DamageSource.fall, (float)var2);
				}

				Block var3 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY - 0.2D - (double)this.prevRotationYaw), MathHelper.floor_double(this.posZ));

				if (var3.Material != Material.air)
				{
					Block.SoundType var4 = var3.stepSound;
					this.worldObj.playSoundAtEntity(this, var4.func_150498_e(), var4.func_150497_c() * 0.5F, var4.func_150494_d() * 0.75F);
				}
			}
		}

		private int func_110225_cC()
		{
			int var1 = this.HorseType;
			return this.Chested && (var1 == 1 || var1 == 2) ? 17 : 2;
		}

		private void func_110226_cD()
		{
			AnimalChest var1 = this.horseChest;
			this.horseChest = new AnimalChest("HorseChest", this.func_110225_cC());
			this.horseChest.func_110133_a(this.CommandSenderName);

			if (var1 != null)
			{
				var1.func_110132_b(this);
				int var2 = Math.Min(var1.SizeInventory, this.horseChest.SizeInventory);

				for (int var3 = 0; var3 < var2; ++var3)
				{
					ItemStack var4 = var1.getStackInSlot(var3);

					if (var4 != null)
					{
						this.horseChest.setInventorySlotContents(var3, var4.copy());
					}
				}

				var1 = null;
			}

			this.horseChest.func_110134_a(this);
			this.func_110232_cE();
		}

		private void func_110232_cE()
		{
			if (!this.worldObj.isClient)
			{
				this.HorseSaddled = this.horseChest.getStackInSlot(0) != null;

				if (this.func_110259_cr())
				{
					this.func_146086_d(this.horseChest.getStackInSlot(1));
				}
			}
		}

///    
///     <summary> * Called by InventoryBasic.onInventoryChanged() on a array that is never filled. </summary>
///     
		public virtual void onInventoryChanged(InventoryBasic p_76316_1_)
		{
			int var2 = this.func_110241_cb();
			bool var3 = this.HorseSaddled;
			this.func_110232_cE();

			if (this.ticksExisted > 20)
			{
				if (var2 == 0 && var2 != this.func_110241_cb())
				{
					this.playSound("mob.horse.armor", 0.5F, 1.0F);
				}
				else if (var2 != this.func_110241_cb())
				{
					this.playSound("mob.horse.armor", 0.5F, 1.0F);
				}

				if (!var3 && this.HorseSaddled)
				{
					this.playSound("mob.horse.leather", 0.5F, 1.0F);
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
				this.prepareChunkForSpawn();
				return base.CanSpawnHere;
			}
		}

		protected internal virtual EntityHorse getClosestHorse(Entity p_110250_1_, double p_110250_2_)
		{
			double var4 = double.MaxValue;
			Entity var6 = null;
			IList var7 = this.worldObj.getEntitiesWithinAABBExcludingEntity(p_110250_1_, p_110250_1_.boundingBox.addCoord(p_110250_2_, p_110250_2_, p_110250_2_), horseBreedingSelector);
			IEnumerator var8 = var7.GetEnumerator();

			while (var8.MoveNext())
			{
				Entity var9 = (Entity)var8.Current;
				double var10 = var9.getDistanceSq(p_110250_1_.posX, p_110250_1_.posY, p_110250_1_.posZ);

				if (var10 < var4)
				{
					var6 = var9;
					var4 = var10;
				}
			}

			return (EntityHorse)var6;
		}

		public virtual double HorseJumpStrength
		{
			get
			{
				return this.getEntityAttribute(horseJumpStrength).AttributeValue;
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				this.openHorseMouth();
				int var1 = this.HorseType;
				return var1 == 3 ? "mob.horse.zombie.death" : (var1 == 4 ? "mob.horse.skeleton.death" : (var1 != 1 && var1 != 2 ? "mob.horse.death" : "mob.horse.donkey.death"));
			}
		}

		protected internal override Item func_146068_u()
		{
			bool var1 = this.rand.Next(4) == 0;
			int var2 = this.HorseType;
			return var2 == 4 ? Items.bone : (var2 == 3 ? (var1 ? Item.getItemById(0) : Items.rotten_flesh) : Items.leather);
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				this.openHorseMouth();
	
				if (this.rand.Next(3) == 0)
				{
					this.makeHorseRear();
				}
	
				int var1 = this.HorseType;
				return var1 == 3 ? "mob.horse.zombie.hit" : (var1 == 4 ? "mob.horse.skeleton.hit" : (var1 != 1 && var1 != 2 ? "mob.horse.hit" : "mob.horse.donkey.hit"));
			}
		}


///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				this.openHorseMouth();
	
				if (this.rand.Next(10) == 0 && !this.MovementBlocked)
				{
					this.makeHorseRear();
				}
	
				int var1 = this.HorseType;
				return var1 == 3 ? "mob.horse.zombie.idle" : (var1 == 4 ? "mob.horse.skeleton.idle" : (var1 != 1 && var1 != 2 ? "mob.horse.idle" : "mob.horse.donkey.idle"));
			}
		}

		protected internal virtual string AngrySoundName
		{
			get
			{
				this.openHorseMouth();
				this.makeHorseRear();
				int var1 = this.HorseType;
				return var1 != 3 && var1 != 4 ? (var1 != 1 && var1 != 2 ? "mob.horse.angry" : "mob.horse.donkey.angry") : null;
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			Block.SoundType var5 = p_145780_4_.stepSound;

			if (this.worldObj.getBlock(p_145780_1_, p_145780_2_ + 1, p_145780_3_) == Blocks.snow_layer)
			{
				var5 = Blocks.snow_layer.stepSound;
			}

			if (!p_145780_4_.Material.Liquid)
			{
				int var6 = this.HorseType;

				if (this.riddenByEntity != null && var6 != 1 && var6 != 2)
				{
					++this.field_110285_bP;

					if (this.field_110285_bP > 5 && this.field_110285_bP % 3 == 0)
					{
						this.playSound("mob.horse.gallop", var5.func_150497_c() * 0.15F, var5.func_150494_d());

						if (var6 == 0 && this.rand.Next(10) == 0)
						{
							this.playSound("mob.horse.breathe", var5.func_150497_c() * 0.6F, var5.func_150494_d());
						}
					}
					else if (this.field_110285_bP <= 5)
					{
						this.playSound("mob.horse.wood", var5.func_150497_c() * 0.15F, var5.func_150494_d());
					}
				}
				else if (var5 == Block.soundTypeWood)
				{
					this.playSound("mob.horse.wood", var5.func_150497_c() * 0.15F, var5.func_150494_d());
				}
				else
				{
					this.playSound("mob.horse.soft", var5.func_150497_c() * 0.15F, var5.func_150494_d());
				}
			}
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.AttributeMap.registerAttribute(horseJumpStrength);
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 53.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.22499999403953552D;
		}

///    
///     <summary> * Will return how many at most can spawn in a chunk at once. </summary>
///     
		public override int MaxSpawnedInChunk
		{
			get
			{
				return 6;
			}
		}

		public virtual int MaxTemper
		{
			get
			{
				return 100;
			}
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal override float SoundVolume
		{
			get
			{
				return 0.8F;
			}
		}

///    
///     <summary> * Get number of ticks, at least during which the living entity will be silent. </summary>
///     
		public override int TalkInterval
		{
			get
			{
				return 400;
			}
		}

		public virtual bool func_110239_cn()
		{
			return this.HorseType == 0 || this.func_110241_cb() > 0;
		}

		private void func_110230_cF()
		{
			this.field_110286_bQ = null;
		}

		private void setHorseTexturePaths()
		{
			this.field_110286_bQ = "horse/";
			this.field_110280_bR[0] = null;
			this.field_110280_bR[1] = null;
			this.field_110280_bR[2] = null;
			int var1 = this.HorseType;
			int var2 = this.HorseVariant;
			int var3;

			if (var1 == 0)
			{
				var3 = var2 & 255;
				int var4 = (var2 & 65280) >> 8;
				this.field_110280_bR[0] = horseTextures[var3];
				this.field_110286_bQ = this.field_110286_bQ + field_110269_bA[var3];
				this.field_110280_bR[1] = horseMarkingTextures[var4];
				this.field_110286_bQ = this.field_110286_bQ + field_110292_bC[var4];
			}
			else
			{
				this.field_110280_bR[0] = "";
				this.field_110286_bQ = this.field_110286_bQ + "_" + var1 + "_";
			}

			var3 = this.func_110241_cb();
			this.field_110280_bR[2] = horseArmorTextures[var3];
			this.field_110286_bQ = this.field_110286_bQ + field_110273_bx[var3];
		}

		public virtual string HorseTexture
		{
			get
			{
				if (this.field_110286_bQ == null)
				{
					this.setHorseTexturePaths();
				}
	
				return this.field_110286_bQ;
			}
		}

		public virtual string[] VariantTexturePaths
		{
			get
			{
				if (this.field_110286_bQ == null)
				{
					this.setHorseTexturePaths();
				}
	
				return this.field_110280_bR;
			}
		}

		public virtual void openGUI(EntityPlayer p_110199_1_)
		{
			if (!this.worldObj.isClient && (this.riddenByEntity == null || this.riddenByEntity == p_110199_1_) && this.Tame)
			{
				this.horseChest.func_110133_a(this.CommandSenderName);
				p_110199_1_.displayGUIHorse(this, this.horseChest);
			}
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public override bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && var2.Item == Items.spawn_egg)
			{
				return base.interact(p_70085_1_);
			}
			else if (!this.Tame && this.func_110256_cu())
			{
				return false;
			}
			else if (this.Tame && this.AdultHorse && p_70085_1_.Sneaking)
			{
				this.openGUI(p_70085_1_);
				return true;
			}
			else if (this.func_110253_bW() && this.riddenByEntity != null)
			{
				return base.interact(p_70085_1_);
			}
			else
			{
				if (var2 != null)
				{
					bool var3 = false;

					if (this.func_110259_cr())
					{
						sbyte var4 = -1;

						if (var2.Item == Items.iron_horse_armor)
						{
							var4 = 1;
						}
						else if (var2.Item == Items.golden_horse_armor)
						{
							var4 = 2;
						}
						else if (var2.Item == Items.diamond_horse_armor)
						{
							var4 = 3;
						}

						if (var4 >= 0)
						{
							if (!this.Tame)
							{
								this.makeHorseRearWithSound();
								return true;
							}

							this.openGUI(p_70085_1_);
							return true;
						}
					}

					if (!var3 && !this.func_110256_cu())
					{
						float var7 = 0.0F;
						short var5 = 0;
						sbyte var6 = 0;

						if (var2.Item == Items.wheat)
						{
							var7 = 2.0F;
							var5 = 60;
							var6 = 3;
						}
						else if (var2.Item == Items.sugar)
						{
							var7 = 1.0F;
							var5 = 30;
							var6 = 3;
						}
						else if (var2.Item == Items.bread)
						{
							var7 = 7.0F;
							var5 = 180;
							var6 = 3;
						}
						else if (Block.getBlockFromItem(var2.Item) == Blocks.hay_block)
						{
							var7 = 20.0F;
							var5 = 180;
						}
						else if (var2.Item == Items.apple)
						{
							var7 = 3.0F;
							var5 = 60;
							var6 = 3;
						}
						else if (var2.Item == Items.golden_carrot)
						{
							var7 = 4.0F;
							var5 = 60;
							var6 = 5;

							if (this.Tame && this.GrowingAge == 0)
							{
								var3 = true;
								this.func_146082_f(p_70085_1_);
							}
						}
						else if (var2.Item == Items.golden_apple)
						{
							var7 = 10.0F;
							var5 = 240;
							var6 = 10;

							if (this.Tame && this.GrowingAge == 0)
							{
								var3 = true;
								this.func_146082_f(p_70085_1_);
							}
						}

						if (this.Health < this.MaxHealth && var7 > 0.0F)
						{
							this.heal(var7);
							var3 = true;
						}

						if (!this.AdultHorse && var5 > 0)
						{
							this.addGrowth(var5);
							var3 = true;
						}

						if (var6 > 0 && (var3 || !this.Tame) && var6 < this.MaxTemper)
						{
							var3 = true;
							this.increaseTemper(var6);
						}

						if (var3)
						{
							this.func_110266_cB();
						}
					}

					if (!this.Tame && !var3)
					{
						if (var2 != null && var2.interactWithEntity(p_70085_1_, this))
						{
							return true;
						}

						this.makeHorseRearWithSound();
						return true;
					}

					if (!var3 && this.func_110229_cs() && !this.Chested && var2.Item == Item.getItemFromBlock(Blocks.chest))
					{
						this.Chested = true;
						this.playSound("mob.chickenplop", 1.0F, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
						var3 = true;
						this.func_110226_cD();
					}

					if (!var3 && this.func_110253_bW() && !this.HorseSaddled && var2.Item == Items.saddle)
					{
						this.openGUI(p_70085_1_);
						return true;
					}

					if (var3)
					{
						if (!p_70085_1_.capabilities.isCreativeMode && --var2.stackSize == 0)
						{
							p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, (ItemStack)null);
						}

						return true;
					}
				}

				if (this.func_110253_bW() && this.riddenByEntity == null)
				{
					if (var2 != null && var2.interactWithEntity(p_70085_1_, this))
					{
						return true;
					}
					else
					{
						this.func_110237_h(p_70085_1_);
						return true;
					}
				}
				else
				{
					return base.interact(p_70085_1_);
				}
			}
		}

		private void func_110237_h(EntityPlayer p_110237_1_)
		{
			p_110237_1_.rotationYaw = this.rotationYaw;
			p_110237_1_.rotationPitch = this.rotationPitch;
			this.EatingHaystack = false;
			this.Rearing = false;

			if (!this.worldObj.isClient)
			{
				p_110237_1_.mountEntity(this);
			}
		}

		public virtual bool func_110259_cr()
		{
			return this.HorseType == 0;
		}

		public virtual bool func_110229_cs()
		{
			int var1 = this.HorseType;
			return var1 == 2 || var1 == 1;
		}

///    
///     <summary> * Dead and sleeping entities cannot move </summary>
///     
		protected internal override bool isMovementBlocked()
		{
			get
			{
				return this.riddenByEntity != null && this.HorseSaddled ? true : this.EatingHaystack || this.Rearing;
			}
		}

		public virtual bool func_110256_cu()
		{
			int var1 = this.HorseType;
			return var1 == 3 || var1 == 4;
		}

		public virtual bool func_110222_cv()
		{
			return this.func_110256_cu() || this.HorseType == 2;
		}

///    
///     <summary> * Checks if the parameter is an item which this animal can be fed to breed it (wheat, carrots or seeds depending on
///     * the animal type) </summary>
///     
		public override bool isBreedingItem(ItemStack p_70877_1_)
		{
			return false;
		}

		private void func_110210_cH()
		{
			this.field_110278_bp = 1;
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public override void onDeath(DamageSource p_70645_1_)
		{
			base.onDeath(p_70645_1_);

			if (!this.worldObj.isClient)
			{
				this.dropChestItems();
			}
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (this.rand.Next(200) == 0)
			{
				this.func_110210_cH();
			}

			base.onLivingUpdate();

			if (!this.worldObj.isClient)
			{
				if (this.rand.Next(900) == 0 && this.deathTime == 0)
				{
					this.heal(1.0F);
				}

				if (!this.EatingHaystack && this.riddenByEntity == null && this.rand.Next(300) == 0 && this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY) - 1, MathHelper.floor_double(this.posZ)) == Blocks.grass)
				{
					this.EatingHaystack = true;
				}

				if (this.EatingHaystack && ++this.eatingHaystackCounter > 50)
				{
					this.eatingHaystackCounter = 0;
					this.EatingHaystack = false;
				}

				if (this.func_110205_ce() && !this.AdultHorse && !this.EatingHaystack)
				{
					EntityHorse var1 = this.getClosestHorse(this, 16.0D);

					if (var1 != null && this.getDistanceSqToEntity(var1) > 4.0D)
					{
						PathEntity var2 = this.worldObj.getPathEntityToEntity(this, var1, 16.0F, true, false, false, true);
						this.PathToEntity = var2;
					}
				}
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (this.worldObj.isClient && this.dataWatcher.hasChanges())
			{
				this.dataWatcher.func_111144_e();
				this.func_110230_cF();
			}

			if (this.openMouthCounter > 0 && ++this.openMouthCounter > 30)
			{
				this.openMouthCounter = 0;
				this.setHorseWatchableBoolean(128, false);
			}

			if (!this.worldObj.isClient && this.jumpRearingCounter > 0 && ++this.jumpRearingCounter > 20)
			{
				this.jumpRearingCounter = 0;
				this.Rearing = false;
			}

			if (this.field_110278_bp > 0 && ++this.field_110278_bp > 8)
			{
				this.field_110278_bp = 0;
			}

			if (this.field_110279_bq > 0)
			{
				++this.field_110279_bq;

				if (this.field_110279_bq > 300)
				{
					this.field_110279_bq = 0;
				}
			}

			this.prevHeadLean = this.headLean;

			if (this.EatingHaystack)
			{
				this.headLean += (1.0F - this.headLean) * 0.4F + 0.05F;

				if (this.headLean > 1.0F)
				{
					this.headLean = 1.0F;
				}
			}
			else
			{
				this.headLean += (0.0F - this.headLean) * 0.4F - 0.05F;

				if (this.headLean < 0.0F)
				{
					this.headLean = 0.0F;
				}
			}

			this.prevRearingAmount = this.rearingAmount;

			if (this.Rearing)
			{
				this.prevHeadLean = this.headLean = 0.0F;
				this.rearingAmount += (1.0F - this.rearingAmount) * 0.4F + 0.05F;

				if (this.rearingAmount > 1.0F)
				{
					this.rearingAmount = 1.0F;
				}
			}
			else
			{
				this.field_110294_bI = false;
				this.rearingAmount += (0.8F * this.rearingAmount * this.rearingAmount * this.rearingAmount - this.rearingAmount) * 0.6F - 0.05F;

				if (this.rearingAmount < 0.0F)
				{
					this.rearingAmount = 0.0F;
				}
			}

			this.prevMouthOpenness = this.mouthOpenness;

			if (this.getHorseWatchableBoolean(128))
			{
				this.mouthOpenness += (1.0F - this.mouthOpenness) * 0.7F + 0.05F;

				if (this.mouthOpenness > 1.0F)
				{
					this.mouthOpenness = 1.0F;
				}
			}
			else
			{
				this.mouthOpenness += (0.0F - this.mouthOpenness) * 0.7F - 0.05F;

				if (this.mouthOpenness < 0.0F)
				{
					this.mouthOpenness = 0.0F;
				}
			}
		}

		private void openHorseMouth()
		{
			if (!this.worldObj.isClient)
			{
				this.openMouthCounter = 1;
				this.setHorseWatchableBoolean(128, true);
			}
		}

		private bool func_110200_cJ()
		{
			return this.riddenByEntity == null && this.ridingEntity == null && this.Tame && this.AdultHorse && !this.func_110222_cv() && this.Health >= this.MaxHealth;
		}

		public override bool Eating
		{
			set
			{
				this.setHorseWatchableBoolean(32, value);
			}
		}



		private void makeHorseRear()
		{
			if (!this.worldObj.isClient)
			{
				this.jumpRearingCounter = 1;
				this.Rearing = true;
			}
		}

		public virtual void makeHorseRearWithSound()
		{
			this.makeHorseRear();
			string var1 = this.AngrySoundName;

			if (var1 != null)
			{
				this.playSound(var1, this.SoundVolume, this.SoundPitch);
			}
		}

		public virtual void dropChestItems()
		{
			this.dropItemsInChest(this, this.horseChest);
			this.dropChests();
		}

		private void dropItemsInChest(Entity p_110240_1_, AnimalChest p_110240_2_)
		{
			if (p_110240_2_ != null && !this.worldObj.isClient)
			{
				for (int var3 = 0; var3 < p_110240_2_.SizeInventory; ++var3)
				{
					ItemStack var4 = p_110240_2_.getStackInSlot(var3);

					if (var4 != null)
					{
						this.entityDropItem(var4, 0.0F);
					}
				}
			}
		}

		public virtual bool TamedBy
		{
			set
			{
				this.func_152120_b(value.UniqueID.ToString());
				this.HorseTamed = true;
				return true;
			}
		}

///    
///     <summary> * Moves the entity based on the specified heading.  Args: strafe, forward </summary>
///     
		public override void moveEntityWithHeading(float p_70612_1_, float p_70612_2_)
		{
			if (this.riddenByEntity != null && this.riddenByEntity is EntityLivingBase && this.HorseSaddled)
			{
				this.prevRotationYaw = this.rotationYaw = this.riddenByEntity.rotationYaw;
				this.rotationPitch = this.riddenByEntity.rotationPitch * 0.5F;
				this.setRotation(this.rotationYaw, this.rotationPitch);
				this.rotationYawHead = this.renderYawOffset = this.rotationYaw;
				p_70612_1_ = ((EntityLivingBase)this.riddenByEntity).moveStrafing * 0.5F;
				p_70612_2_ = ((EntityLivingBase)this.riddenByEntity).moveForward;

				if (p_70612_2_ <= 0.0F)
				{
					p_70612_2_ *= 0.25F;
					this.field_110285_bP = 0;
				}

				if (this.onGround && this.jumpPower == 0.0F && this.Rearing && !this.field_110294_bI)
				{
					p_70612_1_ = 0.0F;
					p_70612_2_ = 0.0F;
				}

				if (this.jumpPower > 0.0F && !this.HorseJumping && this.onGround)
				{
					this.motionY = this.HorseJumpStrength * (double)this.jumpPower;

					if (this.isPotionActive(Potion.jump))
					{
						this.motionY += (double)((float)(this.getActivePotionEffect(Potion.jump).Amplifier + 1) * 0.1F);
					}

					this.HorseJumping = true;
					this.isAirBorne = true;

					if (p_70612_2_ > 0.0F)
					{
						float var3 = MathHelper.sin(this.rotationYaw * (float)Math.PI / 180.0F);
						float var4 = MathHelper.cos(this.rotationYaw * (float)Math.PI / 180.0F);
						this.motionX += (double)(-0.4F * var3 * this.jumpPower);
						this.motionZ += (double)(0.4F * var4 * this.jumpPower);
						this.playSound("mob.horse.jump", 0.4F, 1.0F);
					}

					this.jumpPower = 0.0F;
				}

				this.stepHeight = 1.0F;
				this.jumpMovementFactor = this.AIMoveSpeed * 0.1F;

				if (!this.worldObj.isClient)
				{
					this.AIMoveSpeed = (float)this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).AttributeValue;
					base.moveEntityWithHeading(p_70612_1_, p_70612_2_);
				}

				if (this.onGround)
				{
					this.jumpPower = 0.0F;
					this.HorseJumping = false;
				}

				this.prevLimbSwingAmount = this.limbSwingAmount;
				double var8 = this.posX - this.prevPosX;
				double var5 = this.posZ - this.prevPosZ;
				float var7 = MathHelper.sqrt_double(var8 * var8 + var5 * var5) * 4.0F;

				if (var7 > 1.0F)
				{
					var7 = 1.0F;
				}

				this.limbSwingAmount += (var7 - this.limbSwingAmount) * 0.4F;
				this.limbSwing += this.limbSwingAmount;
			}
			else
			{
				this.stepHeight = 0.5F;
				this.jumpMovementFactor = 0.02F;
				base.moveEntityWithHeading(p_70612_1_, p_70612_2_);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("EatingHaystack", this.EatingHaystack);
			p_70014_1_.setBoolean("ChestedHorse", this.Chested);
			p_70014_1_.setBoolean("HasReproduced", this.HasReproduced);
			p_70014_1_.setBoolean("Bred", this.func_110205_ce());
			p_70014_1_.setInteger("Type", this.HorseType);
			p_70014_1_.setInteger("Variant", this.HorseVariant);
			p_70014_1_.setInteger("Temper", this.Temper);
			p_70014_1_.setBoolean("Tame", this.Tame);
			p_70014_1_.setString("OwnerUUID", this.func_152119_ch());

			if (this.Chested)
			{
				NBTTagList var2 = new NBTTagList();

				for (int var3 = 2; var3 < this.horseChest.SizeInventory; ++var3)
				{
					ItemStack var4 = this.horseChest.getStackInSlot(var3);

					if (var4 != null)
					{
						NBTTagCompound var5 = new NBTTagCompound();
						var5.setByte("Slot", (sbyte)var3);
						var4.writeToNBT(var5);
						var2.appendTag(var5);
					}
				}

				p_70014_1_.setTag("Items", var2);
			}

			if (this.horseChest.getStackInSlot(1) != null)
			{
				p_70014_1_.setTag("ArmorItem", this.horseChest.getStackInSlot(1).writeToNBT(new NBTTagCompound()));
			}

			if (this.horseChest.getStackInSlot(0) != null)
			{
				p_70014_1_.setTag("SaddleItem", this.horseChest.getStackInSlot(0).writeToNBT(new NBTTagCompound()));
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.EatingHaystack = p_70037_1_.getBoolean("EatingHaystack");
			this.func_110242_l(p_70037_1_.getBoolean("Bred"));
			this.Chested = p_70037_1_.getBoolean("ChestedHorse");
			this.HasReproduced = p_70037_1_.getBoolean("HasReproduced");
			this.HorseType = p_70037_1_.getInteger("Type");
			this.HorseVariant = p_70037_1_.getInteger("Variant");
			this.Temper = p_70037_1_.getInteger("Temper");
			this.HorseTamed = p_70037_1_.getBoolean("Tame");

			if (p_70037_1_.func_150297_b("OwnerUUID", 8))
			{
				this.func_152120_b(p_70037_1_.getString("OwnerUUID"));
			}

			IAttributeInstance var2 = this.AttributeMap.getAttributeInstanceByName("Speed");

			if (var2 != null)
			{
				this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = var2.BaseValue * 0.25D;
			}

			if (this.Chested)
			{
				NBTTagList var3 = p_70037_1_.getTagList("Items", 10);
				this.func_110226_cD();

				for (int var4 = 0; var4 < var3.tagCount(); ++var4)
				{
					NBTTagCompound var5 = var3.getCompoundTagAt(var4);
					int var6 = var5.getByte("Slot") & 255;

					if (var6 >= 2 && var6 < this.horseChest.SizeInventory)
					{
						this.horseChest.setInventorySlotContents(var6, ItemStack.loadItemStackFromNBT(var5));
					}
				}
			}

			ItemStack var7;

			if (p_70037_1_.func_150297_b("ArmorItem", 10))
			{
				var7 = ItemStack.loadItemStackFromNBT(p_70037_1_.getCompoundTag("ArmorItem"));

				if (var7 != null && func_146085_a(var7.Item))
				{
					this.horseChest.setInventorySlotContents(1, var7);
				}
			}

			if (p_70037_1_.func_150297_b("SaddleItem", 10))
			{
				var7 = ItemStack.loadItemStackFromNBT(p_70037_1_.getCompoundTag("SaddleItem"));

				if (var7 != null && var7.Item == Items.saddle)
				{
					this.horseChest.setInventorySlotContents(0, var7);
				}
			}
			else if (p_70037_1_.getBoolean("Saddle"))
			{
				this.horseChest.setInventorySlotContents(0, new ItemStack(Items.saddle));
			}

			this.func_110232_cE();
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
			else if (p_70878_1_.GetType() != this.GetType())
			{
				return false;
			}
			else
			{
				EntityHorse var2 = (EntityHorse)p_70878_1_;

				if (this.func_110200_cJ() && var2.func_110200_cJ())
				{
					int var3 = this.HorseType;
					int var4 = var2.HorseType;
					return var3 == var4 || var3 == 0 && var4 == 1 || var3 == 1 && var4 == 0;
				}
				else
				{
					return false;
				}
			}
		}

		public override EntityAgeable createChild(EntityAgeable p_90011_1_)
		{
			EntityHorse var2 = (EntityHorse)p_90011_1_;
			EntityHorse var3 = new EntityHorse(this.worldObj);
			int var4 = this.HorseType;
			int var5 = var2.HorseType;
			int var6 = 0;

			if (var4 == var5)
			{
				var6 = var4;
			}
			else if (var4 == 0 && var5 == 1 || var4 == 1 && var5 == 0)
			{
				var6 = 2;
			}

			if (var6 == 0)
			{
				int var8 = this.rand.Next(9);
				int var7;

				if (var8 < 4)
				{
					var7 = this.HorseVariant & 255;
				}
				else if (var8 < 8)
				{
					var7 = var2.HorseVariant & 255;
				}
				else
				{
					var7 = this.rand.Next(7);
				}

				int var9 = this.rand.Next(5);

				if (var9 < 2)
				{
					var7 |= this.HorseVariant & 65280;
				}
				else if (var9 < 4)
				{
					var7 |= var2.HorseVariant & 65280;
				}
				else
				{
					var7 |= this.rand.Next(5) << 8 & 65280;
				}

				var3.HorseVariant = var7;
			}

			var3.HorseType = var6;
			double var13 = this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue + p_90011_1_.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue + (double)this.func_110267_cL();
			var3.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = var13 / 3.0D;
			double var14 = this.getEntityAttribute(horseJumpStrength).BaseValue + p_90011_1_.getEntityAttribute(horseJumpStrength).BaseValue + this.func_110245_cM();
			var3.getEntityAttribute(horseJumpStrength).BaseValue = var14 / 3.0D;
			double var11 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue + p_90011_1_.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue + this.func_110203_cN();
			var3.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = var11 / 3.0D;
			return var3;
		}

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			object p_110161_1_1 = base.onSpawnWithEgg(p_110161_1_);
			bool var2 = false;
			int var3 = 0;
			int var7;

			if (p_110161_1_1 is EntityHorse.GroupData)
			{
				var7 = ((EntityHorse.GroupData)p_110161_1_1).field_111107_a;
				var3 = ((EntityHorse.GroupData)p_110161_1_1).field_111106_b & 255 | this.rand.Next(5) << 8;
			}
			else
			{
				if (this.rand.Next(10) == 0)
				{
					var7 = 1;
				}
				else
				{
					int var4 = this.rand.Next(7);
					int var5 = this.rand.Next(5);
					var7 = 0;
					var3 = var4 | var5 << 8;
				}

				p_110161_1_1 = new EntityHorse.GroupData(var7, var3);
			}

			this.HorseType = var7;
			this.HorseVariant = var3;

			if (this.rand.Next(5) == 0)
			{
				this.GrowingAge = -24000;
			}

			if (var7 != 4 && var7 != 3)
			{
				this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = (double)this.func_110267_cL();

				if (var7 == 0)
				{
					this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = this.func_110203_cN();
				}
				else
				{
					this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.17499999701976776D;
				}
			}
			else
			{
				this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 15.0D;
				this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.20000000298023224D;
			}

			if (var7 != 2 && var7 != 1)
			{
				this.getEntityAttribute(horseJumpStrength).BaseValue = this.func_110245_cM();
			}
			else
			{
				this.getEntityAttribute(horseJumpStrength).BaseValue = 0.5D;
			}

			this.Health = this.MaxHealth;
			return (IEntityLivingData)p_110161_1_1;
		}

		public virtual float getGrassEatingAmount(float p_110258_1_)
		{
			return this.prevHeadLean + (this.headLean - this.prevHeadLean) * p_110258_1_;
		}

		public virtual float getRearingAmount(float p_110223_1_)
		{
			return this.prevRearingAmount + (this.rearingAmount - this.prevRearingAmount) * p_110223_1_;
		}

		public virtual float func_110201_q(float p_110201_1_)
		{
			return this.prevMouthOpenness + (this.mouthOpenness - this.prevMouthOpenness) * p_110201_1_;
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

		public virtual int JumpPower
		{
			set
			{
				if (this.HorseSaddled)
				{
					if (value < 0)
					{
						value = 0;
					}
					else
					{
						this.field_110294_bI = true;
						this.makeHorseRear();
					}
	
					if (value >= 90)
					{
						this.jumpPower = 1.0F;
					}
					else
					{
						this.jumpPower = 0.4F + 0.4F * (float)value / 90.0F;
					}
				}
			}
		}

///    
///     <summary> * "Spawns particles for the horse entity. par1 tells whether to spawn hearts. If it is false, it spawns smoke." </summary>
///     
		protected internal virtual void spawnHorseParticles(bool p_110216_1_)
		{
			string var2 = p_110216_1_ ? "heart" : "smoke";

			for (int var3 = 0; var3 < 7; ++var3)
			{
				double var4 = this.rand.nextGaussian() * 0.02D;
				double var6 = this.rand.nextGaussian() * 0.02D;
				double var8 = this.rand.nextGaussian() * 0.02D;
				this.worldObj.spawnParticle(var2, this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var4, var6, var8);
			}
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 7)
			{
				this.spawnHorseParticles(true);
			}
			else if (p_70103_1_ == 6)
			{
				this.spawnHorseParticles(false);
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

		public override void updateRiderPosition()
		{
			base.updateRiderPosition();

			if (this.prevRearingAmount > 0.0F)
			{
				float var1 = MathHelper.sin(this.renderYawOffset * (float)Math.PI / 180.0F);
				float var2 = MathHelper.cos(this.renderYawOffset * (float)Math.PI / 180.0F);
				float var3 = 0.7F * this.prevRearingAmount;
				float var4 = 0.15F * this.prevRearingAmount;
				this.riddenByEntity.setPosition(this.posX + (double)(var3 * var1), this.posY + this.MountedYOffset + this.riddenByEntity.YOffset + (double)var4, this.posZ - (double)(var3 * var2));

				if (this.riddenByEntity is EntityLivingBase)
				{
					((EntityLivingBase)this.riddenByEntity).renderYawOffset = this.renderYawOffset;
				}
			}
		}

		private float func_110267_cL()
		{
			return 15.0F + (float)this.rand.Next(8) + (float)this.rand.Next(9);
		}

		private double func_110245_cM()
		{
			return 0.4000000059604645D + this.rand.NextDouble() * 0.2D + this.rand.NextDouble() * 0.2D + this.rand.NextDouble() * 0.2D;
		}

		private double func_110203_cN()
		{
			return (0.44999998807907104D + this.rand.NextDouble() * 0.3D + this.rand.NextDouble() * 0.3D + this.rand.NextDouble() * 0.3D) * 0.25D;
		}

		public static bool func_146085_a(Item p_146085_0_)
		{
			return p_146085_0_ == Items.iron_horse_armor || p_146085_0_ == Items.golden_horse_armor || p_146085_0_ == Items.diamond_horse_armor;
		}

///    
///     <summary> * returns true if this entity is by a ladder, false otherwise </summary>
///     
		public override bool isOnLadder()
		{
			get
			{
				return false;
			}
		}

		public class GroupData : IEntityLivingData
		{
			public int field_111107_a;
			public int field_111106_b;
			private const string __OBFID = "CL_00001643";

			public GroupData(int p_i1684_1_, int p_i1684_2_)
			{
				this.field_111107_a = p_i1684_1_;
				this.field_111106_b = p_i1684_2_;
			}
		}
	}

}