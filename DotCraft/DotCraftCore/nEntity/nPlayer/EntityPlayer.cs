using System;
using System.Collections;

namespace DotCraftCore.nEntity.nPlayer
{

	using DotCraftCore.nBlock;
using DotCraftCore.nStats;
using Charsets = com.google.common.base.Charsets;
	using GameProfile = com.mojang.authlib.GameProfile;
	using Block = DotCraftCore.nBlock.Block;
	using BlockBed = DotCraftCore.nBlock.BlockBed;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using CommandBlockLogic = DotCraftCore.nCommand.nServer.CommandBlockLogic;
	using EnchantmentHelper = DotCraftCore.Enchantment.EnchantmentHelper;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityList = DotCraftCore.Entity.EntityList;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IEntityMultiPart = DotCraftCore.Entity.IEntityMultiPart;
	using IMerchant = DotCraftCore.Entity.IMerchant;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using EntityDragonPart = DotCraftCore.Entity.Boss.EntityDragonPart;
	using EntityBoat = DotCraftCore.Entity.Item.EntityBoat;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using EntityMinecart = DotCraftCore.Entity.Item.EntityMinecart;
	using EntityMinecartHopper = DotCraftCore.Entity.Item.EntityMinecartHopper;
	using EntityMob = DotCraftCore.Entity.Monster.EntityMob;
	using IMob = DotCraftCore.Entity.Monster.IMob;
	using EntityHorse = DotCraftCore.Entity.Passive.EntityHorse;
	using EntityPig = DotCraftCore.Entity.Passive.EntityPig;
	using EntityArrow = DotCraftCore.Entity.Projectile.EntityArrow;
	using EntityFishHook = DotCraftCore.Entity.Projectile.EntityFishHook;
	using ClickEvent = DotCraftCore.Event.ClickEvent;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Container = DotCraftCore.Inventory.Container;
	using ContainerPlayer = DotCraftCore.Inventory.ContainerPlayer;
	using IInventory = DotCraftCore.Inventory.IInventory;
	using InventoryEnderChest = DotCraftCore.Inventory.InventoryEnderChest;
	using EnumAction = DotCraftCore.Item.EnumAction;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using Potion = DotCraftCore.Potion.Potion;
	using IScoreObjectiveCriteria = DotCraftCore.Scoreboard.IScoreObjectiveCriteria;
	using Score = DotCraftCore.Scoreboard.Score;
	using ScoreObjective = DotCraftCore.Scoreboard.ScoreObjective;
	using ScorePlayerTeam = DotCraftCore.Scoreboard.ScorePlayerTeam;
	using Scoreboard = DotCraftCore.Scoreboard.Scoreboard;
	using Team = DotCraftCore.Scoreboard.Team;
	using AchievementList = DotCraftCore.Stats.AchievementList;
	using StatBase = DotCraftCore.Stats.StatBase;
	using StatList = DotCraftCore.Stats.StatList;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using TileEntityBeacon = DotCraftCore.TileEntity.TileEntityBeacon;
	using TileEntityBrewingStand = DotCraftCore.TileEntity.TileEntityBrewingStand;
	using TileEntityDispenser = DotCraftCore.TileEntity.TileEntityDispenser;
	using TileEntityFurnace = DotCraftCore.TileEntity.TileEntityFurnace;
	using TileEntityHopper = DotCraftCore.TileEntity.TileEntityHopper;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using FoodStats = DotCraftCore.Util.FoodStats;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using IIcon = DotCraftCore.Util.IIcon;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using World = DotCraftCore.World.World;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;

	public abstract class EntityPlayer : EntityLivingBase, ICommandSender
	{
	/// <summary> Inventory of the player  </summary>
		public InventoryPlayer inventory = new InventoryPlayer(this);
		private InventoryEnderChest theInventoryEnderChest = new InventoryEnderChest();

///    
///     <summary> * The Container for the player's inventory (which opens when they press E) </summary>
///     
		public Container inventoryContainer;

	/// <summary> The Container the player has open.  </summary>
		public Container openContainer;

	/// <summary> The food object of the player, the general hunger logic.  </summary>
		protected internal FoodStats foodStats = new FoodStats();

///    
///     <summary> * Used to tell if the player pressed jump twice. If this is at 0 and it's pressed (And they are allowed to fly, as
///     * defined in the player's movementInput) it sets this to 7. If it's pressed and it's greater than 0 enable fly. </summary>
///     
		protected internal int flyToggleTimer;
		public float prevCameraYaw;
		public float cameraYaw;

///    
///     <summary> * Used by EntityPlayer to prevent too many xp orbs from getting absorbed at once. </summary>
///     
		public int xpCooldown;
		public double field_71091_bM;
		public double field_71096_bN;
		public double field_71097_bO;
		public double field_71094_bP;
		public double field_71095_bQ;
		public double field_71085_bR;

	/// <summary> Boolean value indicating weather a player is sleeping or not  </summary>
		protected internal bool sleeping;

	/// <summary> the current location of the player  </summary>
		public ChunkCoordinates playerLocation;
		private int sleepTimer;
		public float field_71079_bU;
		public float field_71082_cx;
		public float field_71089_bV;

	/// <summary> holds the spawn chunk of the player  </summary>
		private ChunkCoordinates spawnChunk;

///    
///     <summary> * Whether this player's spawn point is forced, preventing execution of bed checks. </summary>
///     
		private bool spawnForced;

	/// <summary> Holds the coordinate of the player when enter a minecraft to ride.  </summary>
		private ChunkCoordinates startMinecartRidingCoordinate;

	/// <summary> The player's capabilities. (See class PlayerCapabilities)  </summary>
		public PlayerCapabilities capabilities = new PlayerCapabilities();

	/// <summary> The current experience level the player is on.  </summary>
		public int experienceLevel;

///    
///     <summary> * The total amount of experience the player has. This also includes the amount of experience within their
///     * Experience Bar. </summary>
///     
		public int experienceTotal;

///    
///     <summary> * The current amount of experience the player has within their Experience Bar. </summary>
///     
		public float experience;

///    
///     <summary> * This is the item that is in use when the player is holding down the useItemButton (e.g., bow, food, sword) </summary>
///     
		private ItemStack itemInUse;

///    
///     <summary> * This field starts off equal to getMaxItemUseDuration and is decremented on each tick </summary>
///     
		private int itemInUseCount;
		protected internal float speedOnGround = 0.1F;
		protected internal float speedInAir = 0.02F;
		private int field_82249_h;
		private readonly GameProfile field_146106_i;

///    
///     <summary> * An instance of a fishing rod's hook. If this isn't null, the icon image of the fishing rod is slightly different </summary>
///     
		public EntityFishHook fishEntity;
		

		public EntityPlayer(nWorld p_i45324_1_, GameProfile p_i45324_2_) : base(p_i45324_1_)
		{
			this.entityUniqueID = func_146094_a(p_i45324_2_);
			this.field_146106_i = p_i45324_2_;
			this.inventoryContainer = new ContainerPlayer(this.inventory, !p_i45324_1_.isClient, this);
			this.openContainer = this.inventoryContainer;
			this.yOffset = 1.62F;
			ChunkCoordinates var3 = p_i45324_1_.SpawnPoint;
			this.setLocationAndAngles((double)var3.posX + 0.5D, (double)(var3.posY + 1), (double)var3.posZ + 0.5D, 0.0F, 0.0F);
			this.field_70741_aB = 180.0F;
			this.fireResistance = 20;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.AttributeMap.registerAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 1.0D;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(17, Convert.ToSingle(0.0F));
			this.dataWatcher.addObject(18, Convert.ToInt32(0));
		}

///    
///     <summary> * returns the ItemStack containing the itemInUse </summary>
///     
		public virtual ItemStack ItemInUse
		{
			get
			{
				return this.itemInUse;
			}
		}

///    
///     <summary> * Returns the item in use count </summary>
///     
		public virtual int ItemInUseCount
		{
			get
			{
				return this.itemInUseCount;
			}
		}

///    
///     <summary> * Checks if the entity is currently using an item (e.g., bow, food, sword) by holding down the useItemButton </summary>
///     
		public virtual bool isUsingItem()
		{
			get
			{
				return this.itemInUse != null;
			}
		}

///    
///     <summary> * gets the duration for how long the current itemInUse has been in use </summary>
///     
		public virtual int ItemInUseDuration
		{
			get
			{
				return this.UsingItem ? this.itemInUse.MaxItemUseDuration - this.itemInUseCount : 0;
			}
		}

		public virtual void stopUsingItem()
		{
			if (this.itemInUse != null)
			{
				this.itemInUse.onPlayerStoppedUsing(this.worldObj, this, this.itemInUseCount);
			}

			this.clearItemInUse();
		}

		public virtual void clearItemInUse()
		{
			this.itemInUse = null;
			this.itemInUseCount = 0;

			if (!this.worldObj.isClient)
			{
				this.Eating = false;
			}
		}

		public virtual bool isBlocking()
		{
			get
			{
				return this.UsingItem && this.itemInUse.Item.getItemUseAction(this.itemInUse) == EnumAction.block;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (this.itemInUse != null)
			{
				ItemStack var1 = this.inventory.CurrentItem;

				if (var1 == this.itemInUse)
				{
					if (this.itemInUseCount <= 25 && this.itemInUseCount % 4 == 0)
					{
						this.updateItemUse(var1, 5);
					}

					if (--this.itemInUseCount == 0 && !this.worldObj.isClient)
					{
						this.onItemUseFinish();
					}
				}
				else
				{
					this.clearItemInUse();
				}
			}

			if (this.xpCooldown > 0)
			{
				--this.xpCooldown;
			}

			if (this.PlayerSleeping)
			{
				++this.sleepTimer;

				if (this.sleepTimer > 100)
				{
					this.sleepTimer = 100;
				}

				if (!this.worldObj.isClient)
				{
					if (!this.InBed)
					{
						this.wakeUpPlayer(true, true, false);
					}
					else if (this.worldObj.Daytime)
					{
						this.wakeUpPlayer(false, true, true);
					}
				}
			}
			else if (this.sleepTimer > 0)
			{
				++this.sleepTimer;

				if (this.sleepTimer >= 110)
				{
					this.sleepTimer = 0;
				}
			}

			base.onUpdate();

			if (!this.worldObj.isClient && this.openContainer != null && !this.openContainer.canInteractWith(this))
			{
				this.closeScreen();
				this.openContainer = this.inventoryContainer;
			}

			if (this.Burning && this.capabilities.disableDamage)
			{
				this.extinguish();
			}

			this.field_71091_bM = this.field_71094_bP;
			this.field_71096_bN = this.field_71095_bQ;
			this.field_71097_bO = this.field_71085_bR;
			double var9 = this.posX - this.field_71094_bP;
			double var3 = this.posY - this.field_71095_bQ;
			double var5 = this.posZ - this.field_71085_bR;
			double var7 = 10.0D;

			if (var9 > var7)
			{
				this.field_71091_bM = this.field_71094_bP = this.posX;
			}

			if (var5 > var7)
			{
				this.field_71097_bO = this.field_71085_bR = this.posZ;
			}

			if (var3 > var7)
			{
				this.field_71096_bN = this.field_71095_bQ = this.posY;
			}

			if (var9 < -var7)
			{
				this.field_71091_bM = this.field_71094_bP = this.posX;
			}

			if (var5 < -var7)
			{
				this.field_71097_bO = this.field_71085_bR = this.posZ;
			}

			if (var3 < -var7)
			{
				this.field_71096_bN = this.field_71095_bQ = this.posY;
			}

			this.field_71094_bP += var9 * 0.25D;
			this.field_71085_bR += var5 * 0.25D;
			this.field_71095_bQ += var3 * 0.25D;

			if (this.ridingEntity == null)
			{
				this.startMinecartRidingCoordinate = null;
			}

			if (!this.worldObj.isClient)
			{
				this.foodStats.onUpdate(this);
				this.addStat(StatList.minutesPlayedStat, 1);
			}
		}

///    
///     <summary> * Return the amount of time this entity should stay in a portal before being transported. </summary>
///     
		public override int MaxInPortalTime
		{
			get
			{
				return this.capabilities.disableDamage ? 0 : 80;
			}
		}

		protected internal override string SwimSound
		{
			get
			{
				return "game.player.swim";
			}
		}

		protected internal override string SplashSound
		{
			get
			{
				return "game.player.swim.splash";
			}
		}

///    
///     <summary> * Return the amount of cooldown before this entity can use a portal again. </summary>
///     
		public override int PortalCooldown
		{
			get
			{
				return 10;
			}
		}

		public override void playSound(string p_85030_1_, float p_85030_2_, float p_85030_3_)
		{
			this.worldObj.playSoundToNearExcept(this, p_85030_1_, p_85030_2_, p_85030_3_);
		}

///    
///     <summary> * Plays sounds and makes particles for item in use state </summary>
///     
		protected internal virtual void updateItemUse(ItemStack p_71010_1_, int p_71010_2_)
		{
			if (p_71010_1_.ItemUseAction == EnumAction.drink)
			{
				this.playSound("random.drink", 0.5F, this.worldObj.rand.nextFloat() * 0.1F + 0.9F);
			}

			if (p_71010_1_.ItemUseAction == EnumAction.eat)
			{
				for (int var3 = 0; var3 < p_71010_2_; ++var3)
				{
					Vec3 var4 = Vec3.createVectorHelper(((double)this.rand.nextFloat() - 0.5D) * 0.1D, new Random(1).NextDouble() * 0.1D + 0.1D, 0.0D);
					var4.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
					var4.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
					Vec3 var5 = Vec3.createVectorHelper(((double)this.rand.nextFloat() - 0.5D) * 0.3D, (double)(-this.rand.nextFloat()) * 0.6D - 0.3D, 0.6D);
					var5.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
					var5.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
					var5 = var5.addVector(this.posX, this.posY + (double)this.EyeHeight, this.posZ);
					string var6 = "iconcrack_" + nItem.getIdFromItem(p_71010_1_.Item);

					if (p_71010_1_.HasSubtypes)
					{
						var6 = var6 + "_" + p_71010_1_.ItemDamage;
					}

					this.worldObj.spawnParticle(var6, var5.xCoord, var5.yCoord, var5.zCoord, var4.xCoord, var4.yCoord + 0.05D, var4.zCoord);
				}

				this.playSound("random.eat", 0.5F + 0.5F * (float)this.rand.Next(2), (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
			}
		}

///    
///     <summary> * Used for when item use count runs out, ie: eating completed </summary>
///     
		protected internal virtual void onItemUseFinish()
		{
			if (this.itemInUse != null)
			{
				this.updateItemUse(this.itemInUse, 16);
				int var1 = this.itemInUse.stackSize;
				ItemStack var2 = this.itemInUse.onFoodEaten(this.worldObj, this);

				if (var2 != this.itemInUse || var2 != null && var2.stackSize != var1)
				{
					this.inventory.mainInventory[this.inventory.currentItem] = var2;

					if (var2.stackSize == 0)
					{
						this.inventory.mainInventory[this.inventory.currentItem] = null;
					}
				}

				this.clearItemInUse();
			}
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 9)
			{
				this.onItemUseFinish();
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

///    
///     <summary> * Dead and sleeping entities cannot move </summary>
///     
		protected internal override bool isMovementBlocked()
		{
			get
			{
				return this.Health <= 0.0F || this.PlayerSleeping;
			}
		}

///    
///     <summary> * set current crafting inventory back to the 2x2 square </summary>
///     
		protected internal virtual void closeScreen()
		{
			this.openContainer = this.inventoryContainer;
		}

///    
///     <summary> * Called when a player mounts an entity. e.g. mounts a pig, mounts a boat. </summary>
///     
		public override void mountEntity(Entity p_70078_1_)
		{
			if (this.ridingEntity != null && p_70078_1_ == null)
			{
				if (!this.worldObj.isClient)
				{
					this.dismountEntity(this.ridingEntity);
				}

				if (this.ridingEntity != null)
				{
					this.ridingEntity.riddenByEntity = null;
				}

				this.ridingEntity = null;
			}
			else
			{
				base.mountEntity(p_70078_1_);
			}
		}

///    
///     <summary> * Handles updating while being ridden by an entity </summary>
///     
		public override void updateRidden()
		{
			if (!this.worldObj.isClient && this.Sneaking)
			{
				this.mountEntity((Entity)null);
				this.Sneaking = false;
			}
			else
			{
				double var1 = this.posX;
				double var3 = this.posY;
				double var5 = this.posZ;
				float var7 = this.rotationYaw;
				float var8 = this.rotationPitch;
				base.updateRidden();
				this.prevCameraYaw = this.cameraYaw;
				this.cameraYaw = 0.0F;
				this.addMountedMovementStat(this.posX - var1, this.posY - var3, this.posZ - var5);

				if (this.ridingEntity is EntityPig)
				{
					this.rotationPitch = var8;
					this.rotationYaw = var7;
					this.renderYawOffset = ((EntityPig)this.ridingEntity).renderYawOffset;
				}
			}
		}

///    
///     <summary> * Keeps moving the entity up so it isn't colliding with blocks and other requirements for this entity to be spawned
///     * (only actually used on players though its also on Entity) </summary>
///     
		public override void preparePlayerToSpawn()
		{
			this.yOffset = 1.62F;
			this.setSize(0.6F, 1.8F);
			base.preparePlayerToSpawn();
			this.Health = this.MaxHealth;
			this.deathTime = 0;
		}

		protected internal override void updateEntityActionState()
		{
			base.updateEntityActionState();
			this.updateArmSwingProgress();
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (this.flyToggleTimer > 0)
			{
				--this.flyToggleTimer;
			}

			if (this.worldObj.difficultySetting == EnumDifficulty.PEACEFUL && this.Health < this.MaxHealth && this.worldObj.GameRules.getGameRuleBooleanValue("naturalRegeneration") && this.ticksExisted % 20 * 12 == 0)
			{
				this.heal(1.0F);
			}

			this.inventory.decrementAnimations();
			this.prevCameraYaw = this.cameraYaw;
			base.onLivingUpdate();
			IAttributeInstance var1 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);

			if (!this.worldObj.isClient)
			{
				var1.BaseValue = (double)this.capabilities.WalkSpeed;
			}

			this.jumpMovementFactor = this.speedInAir;

			if (this.Sprinting)
			{
				this.jumpMovementFactor = (float)((double)this.jumpMovementFactor + (double)this.speedInAir * 0.3D);
			}

			this.AIMoveSpeed = (float)var1.AttributeValue;
			float var2 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
			float var3 = (float)Math.Atan(-this.motionY * 0.20000000298023224D) * 15.0F;

			if (var2 > 0.1F)
			{
				var2 = 0.1F;
			}

			if (!this.onGround || this.Health <= 0.0F)
			{
				var2 = 0.0F;
			}

			if (this.onGround || this.Health <= 0.0F)
			{
				var3 = 0.0F;
			}

			this.cameraYaw += (var2 - this.cameraYaw) * 0.4F;
			this.cameraPitch += (var3 - this.cameraPitch) * 0.8F;

			if (this.Health > 0.0F)
			{
				AxisAlignedBB var4 = null;

				if (this.ridingEntity != null && !this.ridingEntity.isDead)
				{
					var4 = this.boundingBox.func_111270_a(this.ridingEntity.boundingBox).expand(1.0D, 0.0D, 1.0D);
				}
				else
				{
					var4 = this.boundingBox.expand(1.0D, 0.5D, 1.0D);
				}

				IList var5 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, var4);

				if (var5 != null)
				{
					for (int var6 = 0; var6 < var5.Count; ++var6)
					{
						Entity var7 = (Entity)var5[var6];

						if (!var7.isDead)
						{
							this.collideWithPlayer(var7);
						}
					}
				}
			}
		}

		private void collideWithPlayer(Entity p_71044_1_)
		{
			p_71044_1_.onCollideWithPlayer(this);
		}

		public virtual int Score
		{
			get
			{
				return this.dataWatcher.getWatchableObjectInt(18);
			}
			set
			{
				this.dataWatcher.updateObject(18, Convert.ToInt32(value));
			}
		}

///    
///     <summary> * Set player's score </summary>
///     

///    
///     <summary> * Add to player's score </summary>
///     
		public virtual void addScore(int p_85039_1_)
		{
			int var2 = this.Score;
			this.dataWatcher.updateObject(18, Convert.ToInt32(var2 + p_85039_1_));
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public override void onDeath(DamageSource p_70645_1_)
		{
			base.onDeath(p_70645_1_);
			this.setSize(0.2F, 0.2F);
			this.setPosition(this.posX, this.posY, this.posZ);
			this.motionY = 0.10000000149011612D;

			if (this.CommandSenderName.Equals("Notch"))
			{
				this.func_146097_a(new ItemStack(Items.apple, 1), true, false);
			}

			if (!this.worldObj.GameRules.getGameRuleBooleanValue("keepInventory"))
			{
				this.inventory.dropAllItems();
			}

			if (p_70645_1_ != null)
			{
				this.motionX = (double)(-MathHelper.cos((this.attackedAtYaw + this.rotationYaw) * (float)Math.PI / 180.0F) * 0.1F);
				this.motionZ = (double)(-MathHelper.sin((this.attackedAtYaw + this.rotationYaw) * (float)Math.PI / 180.0F) * 0.1F);
			}
			else
			{
				this.motionX = this.motionZ = 0.0D;
			}

			this.yOffset = 0.1F;
			this.addStat(StatList.deathsStat, 1);
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "game.player.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "game.player.die";
			}
		}

///    
///     <summary> * Adds a value to the player score. Currently not actually used and the entity passed in does nothing. Args:
///     * entity, scoreToAdd </summary>
///     
		public override void addToPlayerScore(Entity p_70084_1_, int p_70084_2_)
		{
			this.addScore(p_70084_2_);
			ICollection var3 = this.WorldScoreboard.func_96520_a(IScoreObjectiveCriteria.totalKillCount);

			if (p_70084_1_ is EntityPlayer)
			{
				this.addStat(StatList.playerKillsStat, 1);
				var3.addAll(this.WorldScoreboard.func_96520_a(IScoreObjectiveCriteria.playerKillCount));
			}
			else
			{
				this.addStat(StatList.mobKillsStat, 1);
			}

			IEnumerator var4 = var3.GetEnumerator();

			while (var4.MoveNext())
			{
				ScoreObjective var5 = (ScoreObjective)var4.Current;
				Score var6 = this.WorldScoreboard.func_96529_a(this.CommandSenderName, var5);
				var6.func_96648_a();
			}
		}

///    
///     <summary> * Called when player presses the drop item key </summary>
///     
		public virtual EntityItem dropOneItem(bool p_71040_1_)
		{
			return this.func_146097_a(this.inventory.decrStackSize(this.inventory.currentItem, p_71040_1_ && this.inventory.CurrentItem != null ? this.inventory.CurrentItem.stackSize : 1), false, true);
		}

///    
///     <summary> * Args: itemstack, flag </summary>
///     
		public virtual EntityItem dropPlayerItemWithRandomChoice(ItemStack p_71019_1_, bool p_71019_2_)
		{
			return this.func_146097_a(p_71019_1_, false, false);
		}

		public virtual EntityItem func_146097_a(ItemStack p_146097_1_, bool p_146097_2_, bool p_146097_3_)
		{
			if (p_146097_1_ == null)
			{
				return null;
			}
			else if (p_146097_1_.stackSize == 0)
			{
				return null;
			}
			else
			{
				EntityItem var4 = new EntityItem(this.worldObj, this.posX, this.posY - 0.30000001192092896D + (double)this.EyeHeight, this.posZ, p_146097_1_);
				var4.delayBeforeCanPickup = 40;

				if (p_146097_3_)
				{
					var4.func_145799_b(this.CommandSenderName);
				}

				float var5 = 0.1F;
				float var6;

				if (p_146097_2_)
				{
					var6 = this.rand.nextFloat() * 0.5F;
					float var7 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
					var4.motionX = (double)(-MathHelper.sin(var7) * var6);
					var4.motionZ = (double)(MathHelper.cos(var7) * var6);
					var4.motionY = 0.20000000298023224D;
				}
				else
				{
					var5 = 0.3F;
					var4.motionX = (double)(-MathHelper.sin(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI) * var5);
					var4.motionZ = (double)(MathHelper.cos(this.rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(this.rotationPitch / 180.0F * (float)Math.PI) * var5);
					var4.motionY = (double)(-MathHelper.sin(this.rotationPitch / 180.0F * (float)Math.PI) * var5 + 0.1F);
					var5 = 0.02F;
					var6 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
					var5 *= this.rand.nextFloat();
					var4.motionX += Math.Cos((double)var6) * (double)var5;
					var4.motionY += (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.1F);
					var4.motionZ += Math.Sin((double)var6) * (double)var5;
				}

				this.joinEntityItemWithWorld(var4);
				this.addStat(StatList.dropStat, 1);
				return var4;
			}
		}

///    
///     <summary> * Joins the passed in entity item with the world. Args: entityItem </summary>
///     
		protected internal virtual void joinEntityItemWithWorld(EntityItem p_71012_1_)
		{
			this.worldObj.spawnEntityInWorld(p_71012_1_);
		}

///    
///     <summary> * Returns how strong the player is against the specified block at this moment </summary>
///     
		public virtual float getCurrentPlayerStrVsBlock(Block p_146096_1_, bool p_146096_2_)
		{
			float var3 = this.inventory.func_146023_a(p_146096_1_);

			if (var3 > 1.0F)
			{
				int var4 = EnchantmentHelper.getEfficiencyModifier(this);
				ItemStack var5 = this.inventory.CurrentItem;

				if (var4 > 0 && var5 != null)
				{
					float var6 = (float)(var4 * var4 + 1);

					if (!var5.func_150998_b(p_146096_1_) && var3 <= 1.0F)
					{
						var3 += var6 * 0.08F;
					}
					else
					{
						var3 += var6;
					}
				}
			}

			if (this.isPotionActive(nPotion.digSpeed))
			{
				var3 *= 1.0F + (float)(this.getActivePotionEffect(nPotion.digSpeed).Amplifier + 1) * 0.2F;
			}

			if (this.isPotionActive(nPotion.digSlowdown))
			{
				var3 *= 1.0F - (float)(this.getActivePotionEffect(nPotion.digSlowdown).Amplifier + 1) * 0.2F;
			}

			if (this.isInsideOfMaterial(Material.water) && !EnchantmentHelper.getAquaAffinityModifier(this))
			{
				var3 /= 5.0F;
			}

			if (!this.onGround)
			{
				var3 /= 5.0F;
			}

			return var3;
		}

///    
///     <summary> * Checks if the player has the ability to harvest a block (checks current inventory item for a tool if necessary) </summary>
///     
		public virtual bool canHarvestBlock(Block p_146099_1_)
		{
			return this.inventory.func_146025_b(p_146099_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.entityUniqueID = func_146094_a(this.field_146106_i);
			NBTTagList var2 = p_70037_1_.getTagList("Inventory", 10);
			this.inventory.readFromNBT(var2);
			this.inventory.currentItem = p_70037_1_.getInteger("SelectedItemSlot");
			this.sleeping = p_70037_1_.getBoolean("Sleeping");
			this.sleepTimer = p_70037_1_.getShort("SleepTimer");
			this.experience = p_70037_1_.getFloat("XpP");
			this.experienceLevel = p_70037_1_.getInteger("XpLevel");
			this.experienceTotal = p_70037_1_.getInteger("XpTotal");
			this.Score = p_70037_1_.getInteger("Score");

			if (this.sleeping)
			{
				this.playerLocation = new ChunkCoordinates(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
				this.wakeUpPlayer(true, true, false);
			}

			if (p_70037_1_.func_150297_b("SpawnX", 99) && p_70037_1_.func_150297_b("SpawnY", 99) && p_70037_1_.func_150297_b("SpawnZ", 99))
			{
				this.spawnChunk = new ChunkCoordinates(p_70037_1_.getInteger("SpawnX"), p_70037_1_.getInteger("SpawnY"), p_70037_1_.getInteger("SpawnZ"));
				this.spawnForced = p_70037_1_.getBoolean("SpawnForced");
			}

			this.foodStats.readNBT(p_70037_1_);
			this.capabilities.readCapabilitiesFromNBT(p_70037_1_);

			if (p_70037_1_.func_150297_b("EnderItems", 9))
			{
				NBTTagList var3 = p_70037_1_.getTagList("EnderItems", 10);
				this.theInventoryEnderChest.loadInventoryFromNBT(var3);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setTag("Inventory", this.inventory.writeToNBT(new NBTTagList()));
			p_70014_1_.setInteger("SelectedItemSlot", this.inventory.currentItem);
			p_70014_1_.setBoolean("Sleeping", this.sleeping);
			p_70014_1_.setShort("SleepTimer", (short)this.sleepTimer);
			p_70014_1_.setFloat("XpP", this.experience);
			p_70014_1_.setInteger("XpLevel", this.experienceLevel);
			p_70014_1_.setInteger("XpTotal", this.experienceTotal);
			p_70014_1_.setInteger("Score", this.Score);

			if (this.spawnChunk != null)
			{
				p_70014_1_.setInteger("SpawnX", this.spawnChunk.posX);
				p_70014_1_.setInteger("SpawnY", this.spawnChunk.posY);
				p_70014_1_.setInteger("SpawnZ", this.spawnChunk.posZ);
				p_70014_1_.setBoolean("SpawnForced", this.spawnForced);
			}

			this.foodStats.writeNBT(p_70014_1_);
			this.capabilities.writeCapabilitiesToNBT(p_70014_1_);
			p_70014_1_.setTag("EnderItems", this.theInventoryEnderChest.saveInventoryToNBT());
		}

///    
///     <summary> * Displays the GUI for interacting with a chest inventory. Args: chestInventory </summary>
///     
		public virtual void displayGUIChest(IInventory p_71007_1_)
		{
		}

		public virtual void func_146093_a(TileEntityHopper p_146093_1_)
		{
		}

		public virtual void displayGUIHopperMinecart(EntityMinecartHopper p_96125_1_)
		{
		}

		public virtual void displayGUIHorse(EntityHorse p_110298_1_, IInventory p_110298_2_)
		{
		}

		public virtual void displayGUIEnchantment(int p_71002_1_, int p_71002_2_, int p_71002_3_, string p_71002_4_)
		{
		}

///    
///     <summary> * Displays the GUI for interacting with an anvil. </summary>
///     
		public virtual void displayGUIAnvil(int p_82244_1_, int p_82244_2_, int p_82244_3_)
		{
		}

///    
///     <summary> * Displays the crafting GUI for a workbench. </summary>
///     
		public virtual void displayGUIWorkbench(int p_71058_1_, int p_71058_2_, int p_71058_3_)
		{
		}

		public override float EyeHeight
		{
			get
			{
				return 0.12F;
			}
		}

///    
///     <summary> * sets the players height back to normal after doing things like sleeping and dieing </summary>
///     
		protected internal virtual void resetHeight()
		{
			this.yOffset = 1.62F;
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
			else if (this.capabilities.disableDamage && !p_70097_1_.canHarmInCreative())
			{
				return false;
			}
			else
			{
				this.entityAge = 0;

				if (this.Health <= 0.0F)
				{
					return false;
				}
				else
				{
					if (this.PlayerSleeping && !this.worldObj.isClient)
					{
						this.wakeUpPlayer(true, true, false);
					}

					if (p_70097_1_.DifficultyScaled)
					{
						if (this.worldObj.difficultySetting == EnumDifficulty.PEACEFUL)
						{
							p_70097_2_ = 0.0F;
						}

						if (this.worldObj.difficultySetting == EnumDifficulty.EASY)
						{
							p_70097_2_ = p_70097_2_ / 2.0F + 1.0F;
						}

						if (this.worldObj.difficultySetting == EnumDifficulty.HARD)
						{
							p_70097_2_ = p_70097_2_ * 3.0F / 2.0F;
						}
					}

					if (p_70097_2_ == 0.0F)
					{
						return false;
					}
					else
					{
						Entity var3 = p_70097_1_.Entity;

						if (var3 is EntityArrow && ((EntityArrow)var3).shootingEntity != null)
						{
							var3 = ((EntityArrow)var3).shootingEntity;
						}

						this.addStat(StatList.damageTakenStat, Math.Round(p_70097_2_ * 10.0F));
						return base.attackEntityFrom(p_70097_1_, p_70097_2_);
					}
				}
			}
		}

		public virtual bool canAttackPlayer(EntityPlayer p_96122_1_)
		{
			Team var2 = this.Team;
			Team var3 = p_96122_1_.Team;
			return var2 == null ? true : (!var2.isSameTeam(var3) ? true : var2.AllowFriendlyFire);
		}

		protected internal override void damageArmor(float p_70675_1_)
		{
			this.inventory.damageArmor(p_70675_1_);
		}

///    
///     <summary> * Returns the current armor value as determined by a call to InventoryPlayer.getTotalArmorValue </summary>
///     
		public override int TotalArmorValue
		{
			get
			{
				return this.inventory.TotalArmorValue;
			}
		}

///    
///     <summary> * When searching for vulnerable players, if a player is invisible, the return value of this is the chance of seeing
///     * them anyway. </summary>
///     
		public virtual float ArmorVisibility
		{
			get
			{
				int var1 = 0;
				ItemStack[] var2 = this.inventory.armorInventory;
				int var3 = var2.Length;
	
				for (int var4 = 0; var4 < var3; ++var4)
				{
					ItemStack var5 = var2[var4];
	
					if (var5 != null)
					{
						++var1;
					}
				}
	
				return (float)var1 / (float)this.inventory.armorInventory.Length;
			}
		}

///    
///     <summary> * Deals damage to the entity. If its a EntityPlayer then will take damage from the armor first and then health
///     * second with the reduced value. Args: damageAmount </summary>
///     
		protected internal override void damageEntity(DamageSource p_70665_1_, float p_70665_2_)
		{
			if (!this.EntityInvulnerable)
			{
				if (!p_70665_1_.Unblockable && this.Blocking && p_70665_2_ > 0.0F)
				{
					p_70665_2_ = (1.0F + p_70665_2_) * 0.5F;
				}

				p_70665_2_ = this.applyArmorCalculations(p_70665_1_, p_70665_2_);
				p_70665_2_ = this.applyPotionDamageCalculations(p_70665_1_, p_70665_2_);
				float var3 = p_70665_2_;
				p_70665_2_ = Math.Max(p_70665_2_ - this.AbsorptionAmount, 0.0F);
				this.AbsorptionAmount = this.AbsorptionAmount - (var3 - p_70665_2_);

				if (p_70665_2_ != 0.0F)
				{
					this.addExhaustion(p_70665_1_.HungerDamage);
					float var4 = this.Health;
					this.Health = this.Health - p_70665_2_;
					this.func_110142_aN().func_94547_a(p_70665_1_, var4, p_70665_2_);
				}
			}
		}

		public virtual void func_146101_a(TileEntityFurnace p_146101_1_)
		{
		}

		public virtual void func_146102_a(TileEntityDispenser p_146102_1_)
		{
		}

		public virtual void func_146100_a(nTileEntity p_146100_1_)
		{
		}

		public virtual void func_146095_a(CommandBlockLogic p_146095_1_)
		{
		}

		public virtual void func_146098_a(TileEntityBrewingStand p_146098_1_)
		{
		}

		public virtual void func_146104_a(TileEntityBeacon p_146104_1_)
		{
		}

		public virtual void displayGUIMerchant(IMerchant p_71030_1_, string p_71030_2_)
		{
		}

///    
///     <summary> * Displays the GUI for interacting with a book. </summary>
///     
		public virtual void displayGUIBook(ItemStack p_71048_1_)
		{
		}

		public virtual bool interactWith(Entity p_70998_1_)
		{
			ItemStack var2 = this.CurrentEquippedItem;
			ItemStack var3 = var2 != null ? var2.copy() : null;

			if (!p_70998_1_.interactFirst(this))
			{
				if (var2 != null && p_70998_1_ is EntityLivingBase)
				{
					if (this.capabilities.isCreativeMode)
					{
						var2 = var3;
					}

					if (var2.interactWithEntity(this, (EntityLivingBase)p_70998_1_))
					{
						if (var2.stackSize <= 0 && !this.capabilities.isCreativeMode)
						{
							this.destroyCurrentEquippedItem();
						}

						return true;
					}
				}

				return false;
			}
			else
			{
				if (var2 != null && var2 == this.CurrentEquippedItem)
				{
					if (var2.stackSize <= 0 && !this.capabilities.isCreativeMode)
					{
						this.destroyCurrentEquippedItem();
					}
					else if (var2.stackSize < var3.stackSize && this.capabilities.isCreativeMode)
					{
						var2.stackSize = var3.stackSize;
					}
				}

				return true;
			}
		}

///    
///     <summary> * Returns the currently being used item by the player. </summary>
///     
		public virtual ItemStack CurrentEquippedItem
		{
			get
			{
				return this.inventory.CurrentItem;
			}
		}

///    
///     <summary> * Destroys the currently equipped item from the player's inventory. </summary>
///     
		public virtual void destroyCurrentEquippedItem()
		{
			this.inventory.setInventorySlotContents(this.inventory.currentItem, (ItemStack)null);
		}

///    
///     <summary> * Returns the Y Offset of this entity. </summary>
///     
		public override double YOffset
		{
			get
			{
				return (double)(this.yOffset - 0.5F);
			}
		}

///    
///     <summary> * Attacks for the player the targeted entity with the currently equipped item.  The equipped item has hitEntity
///     * called on it. Args: targetEntity </summary>
///     
		public virtual void attackTargetEntityWithCurrentItem(Entity p_71059_1_)
		{
			if (p_71059_1_.canAttackWithItem())
			{
				if (!p_71059_1_.hitByEntity(this))
				{
					float var2 = (float)this.getEntityAttribute(SharedMonsterAttributes.attackDamage).AttributeValue;
					int var3 = 0;
					float var4 = 0.0F;

					if (p_71059_1_ is EntityLivingBase)
					{
						var4 = EnchantmentHelper.getEnchantmentModifierLiving(this, (EntityLivingBase)p_71059_1_);
						var3 += EnchantmentHelper.getKnockbackModifier(this, (EntityLivingBase)p_71059_1_);
					}

					if (this.Sprinting)
					{
						++var3;
					}

					if (var2 > 0.0F || var4 > 0.0F)
					{
						bool var5 = this.fallDistance > 0.0F && !this.onGround && !this.OnLadder && !this.InWater && !this.isPotionActive(nPotion.blindness) && this.ridingEntity == null && p_71059_1_ is EntityLivingBase;

						if (var5 && var2 > 0.0F)
						{
							var2 *= 1.5F;
						}

						var2 += var4;
						bool var6 = false;
						int var7 = EnchantmentHelper.getFireAspectModifier(this);

						if (p_71059_1_ is EntityLivingBase && var7 > 0 && !p_71059_1_.Burning)
						{
							var6 = true;
							p_71059_1_.Fire = 1;
						}

						bool var8 = p_71059_1_.attackEntityFrom(DamageSource.causePlayerDamage(this), var2);

						if (var8)
						{
							if (var3 > 0)
							{
								p_71059_1_.addVelocity((double)(-MathHelper.sin(this.rotationYaw * (float)Math.PI / 180.0F) * (float)var3 * 0.5F), 0.1D, (double)(MathHelper.cos(this.rotationYaw * (float)Math.PI / 180.0F) * (float)var3 * 0.5F));
								this.motionX *= 0.6D;
								this.motionZ *= 0.6D;
								this.Sprinting = false;
							}

							if (var5)
							{
								this.onCriticalHit(p_71059_1_);
							}

							if (var4 > 0.0F)
							{
								this.onEnchantmentCritical(p_71059_1_);
							}

							if (var2 >= 18.0F)
							{
								this.triggerAchievement(AchievementList.overkill);
							}

							this.LastAttacker = p_71059_1_;

							if (p_71059_1_ is EntityLivingBase)
							{
								EnchantmentHelper.func_151384_a((EntityLivingBase)p_71059_1_, this);
							}

							EnchantmentHelper.func_151385_b(this, p_71059_1_);
							ItemStack var9 = this.CurrentEquippedItem;
							object var10 = p_71059_1_;

							if (p_71059_1_ is EntityDragonPart)
							{
								IEntityMultiPart var11 = ((EntityDragonPart)p_71059_1_).entityDragonObj;

								if (var11 != null && var11 is EntityLivingBase)
								{
									var10 = (EntityLivingBase)var11;
								}
							}

							if (var9 != null && var10 is EntityLivingBase)
							{
								var9.hitEntity((EntityLivingBase)var10, this);

								if (var9.stackSize <= 0)
								{
									this.destroyCurrentEquippedItem();
								}
							}

							if (p_71059_1_ is EntityLivingBase)
							{
								this.addStat(StatList.damageDealtStat, Math.Round(var2 * 10.0F));

								if (var7 > 0)
								{
									p_71059_1_.Fire = var7 * 4;
								}
							}

							this.addExhaustion(0.3F);
						}
						else if (var6)
						{
							p_71059_1_.extinguish();
						}
					}
				}
			}
		}

///    
///     <summary> * Called when the player performs a critical hit on the Entity. Args: entity that was hit critically </summary>
///     
		public virtual void onCriticalHit(Entity p_71009_1_)
		{
		}

		public virtual void onEnchantmentCritical(Entity p_71047_1_)
		{
		}

		public virtual void respawnPlayer()
		{
		}

///    
///     <summary> * Will get destroyed next tick. </summary>
///     
		public override void setDead()
		{
			base.setDead();
			this.inventoryContainer.onContainerClosed(this);

			if (this.openContainer != null)
			{
				this.openContainer.onContainerClosed(this);
			}
		}

///    
///     <summary> * Checks if this entity is inside of an opaque block </summary>
///     
		public override bool isEntityInsideOpaqueBlock()
		{
			get
			{
				return !this.sleeping && base.EntityInsideOpaqueBlock;
			}
		}

///    
///     <summary> * Returns the GameProfile for this player </summary>
///     
		public virtual GameProfile GameProfile
		{
			get
			{
				return this.field_146106_i;
			}
		}

///    
///     <summary> * puts player to sleep on specified bed if possible </summary>
///     
		public virtual EntityPlayer.EnumStatus sleepInBedAt(int p_71018_1_, int p_71018_2_, int p_71018_3_)
		{
			if (!this.worldObj.isClient)
			{
				if (this.PlayerSleeping || !this.EntityAlive)
				{
					return EntityPlayer.EnumStatus.OTHER_PROBLEM;
				}

				if (!this.worldObj.provider.SurfaceWorld)
				{
					return EntityPlayer.EnumStatus.NOT_POSSIBLE_HERE;
				}

				if (this.worldObj.Daytime)
				{
					return EntityPlayer.EnumStatus.NOT_POSSIBLE_NOW;
				}

				if (Math.Abs(this.posX - (double)p_71018_1_) > 3.0D || Math.Abs(this.posY - (double)p_71018_2_) > 2.0D || Math.Abs(this.posZ - (double)p_71018_3_) > 3.0D)
				{
					return EntityPlayer.EnumStatus.TOO_FAR_AWAY;
				}

				double var4 = 8.0D;
				double var6 = 5.0D;
				IList var8 = this.worldObj.getEntitiesWithinAABB(typeof(EntityMob), AxisAlignedBB.getBoundingBox((double)p_71018_1_ - var4, (double)p_71018_2_ - var6, (double)p_71018_3_ - var4, (double)p_71018_1_ + var4, (double)p_71018_2_ + var6, (double)p_71018_3_ + var4));

				if (!var8.Count == 0)
				{
					return EntityPlayer.EnumStatus.NOT_SAFE;
				}
			}

			if (this.Riding)
			{
				this.mountEntity((Entity)null);
			}

			this.setSize(0.2F, 0.2F);
			this.yOffset = 0.2F;

			if (this.worldObj.blockExists(p_71018_1_, p_71018_2_, p_71018_3_))
			{
				int var9 = this.worldObj.getBlockMetadata(p_71018_1_, p_71018_2_, p_71018_3_);
				int var5 = BlockBed.func_149895_l(var9);
				float var10 = 0.5F;
				float var7 = 0.5F;

				switch (var5)
				{
					case 0:
						var7 = 0.9F;
						break;

					case 1:
						var10 = 0.1F;
						break;

					case 2:
						var7 = 0.1F;
						break;

					case 3:
						var10 = 0.9F;
					break;
				}

				this.func_71013_b(var5);
				this.setPosition((double)((float)p_71018_1_ + var10), (double)((float)p_71018_2_ + 0.9375F), (double)((float)p_71018_3_ + var7));
			}
			else
			{
				this.setPosition((double)((float)p_71018_1_ + 0.5F), (double)((float)p_71018_2_ + 0.9375F), (double)((float)p_71018_3_ + 0.5F));
			}

			this.sleeping = true;
			this.sleepTimer = 0;
			this.playerLocation = new ChunkCoordinates(p_71018_1_, p_71018_2_, p_71018_3_);
			this.motionX = this.motionZ = this.motionY = 0.0D;

			if (!this.worldObj.isClient)
			{
				this.worldObj.updateAllPlayersSleepingFlag();
			}

			return EntityPlayer.EnumStatus.OK;
		}

		private void func_71013_b(int p_71013_1_)
		{
			this.field_71079_bU = 0.0F;
			this.field_71089_bV = 0.0F;

			switch (p_71013_1_)
			{
				case 0:
					this.field_71089_bV = -1.8F;
					break;

				case 1:
					this.field_71079_bU = 1.8F;
					break;

				case 2:
					this.field_71089_bV = 1.8F;
					break;

				case 3:
					this.field_71079_bU = -1.8F;
				break;
			}
		}

///    
///     <summary> * Wake up the player if they're sleeping. </summary>
///     
		public virtual void wakeUpPlayer(bool p_70999_1_, bool p_70999_2_, bool p_70999_3_)
		{
			this.setSize(0.6F, 1.8F);
			this.resetHeight();
			ChunkCoordinates var4 = this.playerLocation;
			ChunkCoordinates var5 = this.playerLocation;

			if (var4 != null && this.worldObj.getBlock(var4.posX, var4.posY, var4.posZ) == Blocks.bed)
			{
				BlockBed.func_149979_a(this.worldObj, var4.posX, var4.posY, var4.posZ, false);
				var5 = BlockBed.func_149977_a(this.worldObj, var4.posX, var4.posY, var4.posZ, 0);

				if (var5 == null)
				{
					var5 = new ChunkCoordinates(var4.posX, var4.posY + 1, var4.posZ);
				}

				this.setPosition((double)((float)var5.posX + 0.5F), (double)((float)var5.posY + this.yOffset + 0.1F), (double)((float)var5.posZ + 0.5F));
			}

			this.sleeping = false;

			if (!this.worldObj.isClient && p_70999_2_)
			{
				this.worldObj.updateAllPlayersSleepingFlag();
			}

			if (p_70999_1_)
			{
				this.sleepTimer = 0;
			}
			else
			{
				this.sleepTimer = 100;
			}

			if (p_70999_3_)
			{
				this.setSpawnChunk(this.playerLocation, false);
			}
		}

///    
///     <summary> * Checks if the player is currently in a bed </summary>
///     
		private bool isInBed()
		{
			get
			{
				return this.worldObj.getBlock(this.playerLocation.posX, this.playerLocation.posY, this.playerLocation.posZ) == Blocks.bed;
			}
		}

///    
///     <summary> * Ensure that a block enabling respawning exists at the specified coordinates and find an empty space nearby to
///     * spawn. </summary>
///     
		public static ChunkCoordinates verifyRespawnCoordinates(nWorld p_71056_0_, ChunkCoordinates p_71056_1_, bool p_71056_2_)
		{
			IChunkProvider var3 = p_71056_0_.ChunkProvider;
			var3.loadChunk(p_71056_1_.posX - 3 >> 4, p_71056_1_.posZ - 3 >> 4);
			var3.loadChunk(p_71056_1_.posX + 3 >> 4, p_71056_1_.posZ - 3 >> 4);
			var3.loadChunk(p_71056_1_.posX - 3 >> 4, p_71056_1_.posZ + 3 >> 4);
			var3.loadChunk(p_71056_1_.posX + 3 >> 4, p_71056_1_.posZ + 3 >> 4);

			if (p_71056_0_.getBlock(p_71056_1_.posX, p_71056_1_.posY, p_71056_1_.posZ) == Blocks.bed)
			{
				ChunkCoordinates var8 = BlockBed.func_149977_a(p_71056_0_, p_71056_1_.posX, p_71056_1_.posY, p_71056_1_.posZ, 0);
				return var8;
			}
			else
			{
				Material var4 = p_71056_0_.getBlock(p_71056_1_.posX, p_71056_1_.posY, p_71056_1_.posZ).Material;
				Material var5 = p_71056_0_.getBlock(p_71056_1_.posX, p_71056_1_.posY + 1, p_71056_1_.posZ).Material;
				bool var6 = !var4.Solid && !var4.Liquid;
				bool var7 = !var5.Solid && !var5.Liquid;
				return p_71056_2_ && var6 && var7 ? p_71056_1_ : null;
			}
		}

///    
///     <summary> * Returns the orientation of the bed in degrees. </summary>
///     
		public virtual float BedOrientationInDegrees
		{
			get
			{
				if (this.playerLocation != null)
				{
					int var1 = this.worldObj.getBlockMetadata(this.playerLocation.posX, this.playerLocation.posY, this.playerLocation.posZ);
					int var2 = BlockBed.func_149895_l(var1);
	
					switch (var2)
					{
						case 0:
							return 90.0F;
	
						case 1:
							return 0.0F;
	
						case 2:
							return 270.0F;
	
						case 3:
							return 180.0F;
					}
				}
	
				return 0.0F;
			}
		}

///    
///     <summary> * Returns whether player is sleeping or not </summary>
///     
		public override bool isPlayerSleeping()
		{
			get
			{
				return this.sleeping;
			}
		}

///    
///     <summary> * Returns whether or not the player is asleep and the screen has fully faded. </summary>
///     
		public virtual bool isPlayerFullyAsleep()
		{
			get
			{
				return this.sleeping && this.sleepTimer >= 100;
			}
		}

		public virtual int SleepTimer
		{
			get
			{
				return this.sleepTimer;
			}
		}

		protected internal virtual bool getHideCape(int p_82241_1_)
		{
			return (this.dataWatcher.getWatchableObjectByte(16) & 1 << p_82241_1_) != 0;
		}

		protected internal virtual void setHideCape(int p_82239_1_, bool p_82239_2_)
		{
			sbyte var3 = this.dataWatcher.getWatchableObjectByte(16);

			if (p_82239_2_)
			{
				this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var3 | 1 << p_82239_1_)));
			}
			else
			{
				this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var3 & ~(1 << p_82239_1_))));
			}
		}

		public virtual void addChatComponentMessage(IChatComponent p_146105_1_)
		{
		}

///    
///     <summary> * Returns the location of the bed the player will respawn at, or null if the player has not slept in a bed. </summary>
///     
		public virtual ChunkCoordinates BedLocation
		{
			get
			{
				return this.spawnChunk;
			}
		}

		public virtual bool isSpawnForced()
		{
			get
			{
				return this.spawnForced;
			}
		}

///    
///     <summary> * Defines a spawn coordinate to player spawn. Used by bed after the player sleep on it. </summary>
///     
		public virtual void setSpawnChunk(ChunkCoordinates p_71063_1_, bool p_71063_2_)
		{
			if (p_71063_1_ != null)
			{
				this.spawnChunk = new ChunkCoordinates(p_71063_1_);
				this.spawnForced = p_71063_2_;
			}
			else
			{
				this.spawnChunk = null;
				this.spawnForced = false;
			}
		}

///    
///     <summary> * Will trigger the specified trigger. </summary>
///     
		public virtual void triggerAchievement(StatBase p_71029_1_)
		{
			this.addStat(p_71029_1_, 1);
		}

///    
///     <summary> * Adds a value to a statistic field. </summary>
///     
		public virtual void addStat(StatBase p_71064_1_, int p_71064_2_)
		{
		}

///    
///     <summary> * Causes this entity to do an upwards motion (jumping). </summary>
///     
		public override void jump()
		{
			base.jump();
			this.addStat(StatList.jumpStat, 1);

			if (this.Sprinting)
			{
				this.addExhaustion(0.8F);
			}
			else
			{
				this.addExhaustion(0.2F);
			}
		}

///    
///     <summary> * Moves the entity based on the specified heading.  Args: strafe, forward </summary>
///     
		public override void moveEntityWithHeading(float p_70612_1_, float p_70612_2_)
		{
			double var3 = this.posX;
			double var5 = this.posY;
			double var7 = this.posZ;

			if (this.capabilities.isFlying && this.ridingEntity == null)
			{
				double var9 = this.motionY;
				float var11 = this.jumpMovementFactor;
				this.jumpMovementFactor = this.capabilities.FlySpeed;
				base.moveEntityWithHeading(p_70612_1_, p_70612_2_);
				this.motionY = var9 * 0.6D;
				this.jumpMovementFactor = var11;
			}
			else
			{
				base.moveEntityWithHeading(p_70612_1_, p_70612_2_);
			}

			this.addMovementStat(this.posX - var3, this.posY - var5, this.posZ - var7);
		}

///    
///     <summary> * the movespeed used for the new AI system </summary>
///     
		public override float AIMoveSpeed
		{
			get
			{
				return (float)this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).AttributeValue;
			}
		}

///    
///     <summary> * Adds a value to a movement statistic field - like run, walk, swin or climb. </summary>
///     
		public virtual void addMovementStat(double p_71000_1_, double p_71000_3_, double p_71000_5_)
		{
			if (this.ridingEntity == null)
			{
				int var7;

				if (this.isInsideOfMaterial(Material.water))
				{
					var7 = Math.Round(MathHelper.sqrt_double(p_71000_1_ * p_71000_1_ + p_71000_3_ * p_71000_3_ + p_71000_5_ * p_71000_5_) * 100.0F);

					if (var7 > 0)
					{
						this.addStat(StatList.distanceDoveStat, var7);
						this.addExhaustion(0.015F * (float)var7 * 0.01F);
					}
				}
				else if (this.InWater)
				{
					var7 = Math.Round(MathHelper.sqrt_double(p_71000_1_ * p_71000_1_ + p_71000_5_ * p_71000_5_) * 100.0F);

					if (var7 > 0)
					{
						this.addStat(StatList.distanceSwumStat, var7);
						this.addExhaustion(0.015F * (float)var7 * 0.01F);
					}
				}
				else if (this.OnLadder)
				{
					if (p_71000_3_ > 0.0D)
					{
						this.addStat(StatList.distanceClimbedStat, (int)Math.Round(p_71000_3_ * 100.0D));
					}
				}
				else if (this.onGround)
				{
					var7 = Math.Round(MathHelper.sqrt_double(p_71000_1_ * p_71000_1_ + p_71000_5_ * p_71000_5_) * 100.0F);

					if (var7 > 0)
					{
						this.addStat(StatList.distanceWalkedStat, var7);

						if (this.Sprinting)
						{
							this.addExhaustion(0.099999994F * (float)var7 * 0.01F);
						}
						else
						{
							this.addExhaustion(0.01F * (float)var7 * 0.01F);
						}
					}
				}
				else
				{
					var7 = Math.Round(MathHelper.sqrt_double(p_71000_1_ * p_71000_1_ + p_71000_5_ * p_71000_5_) * 100.0F);

					if (var7 > 25)
					{
						this.addStat(StatList.distanceFlownStat, var7);
					}
				}
			}
		}

///    
///     <summary> * Adds a value to a mounted movement statistic field - by minecart, boat, or pig. </summary>
///     
		private void addMountedMovementStat(double p_71015_1_, double p_71015_3_, double p_71015_5_)
		{
			if (this.ridingEntity != null)
			{
				int var7 = Math.Round(MathHelper.sqrt_double(p_71015_1_ * p_71015_1_ + p_71015_3_ * p_71015_3_ + p_71015_5_ * p_71015_5_) * 100.0F);

				if (var7 > 0)
				{
					if (this.ridingEntity is EntityMinecart)
					{
						this.addStat(StatList.distanceByMinecartStat, var7);

						if (this.startMinecartRidingCoordinate == null)
						{
							this.startMinecartRidingCoordinate = new ChunkCoordinates(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
						}
						else if ((double)this.startMinecartRidingCoordinate.getDistanceSquared(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) >= 1000000.0D)
						{
							this.addStat(AchievementList.onARail, 1);
						}
					}
					else if (this.ridingEntity is EntityBoat)
					{
						this.addStat(StatList.distanceByBoatStat, var7);
					}
					else if (this.ridingEntity is EntityPig)
					{
						this.addStat(StatList.distanceByPigStat, var7);
					}
					else if (this.ridingEntity is EntityHorse)
					{
						this.addStat(StatList.field_151185_q, var7);
					}
				}
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
			if (!this.capabilities.allowFlying)
			{
				if (p_70069_1_ >= 2.0F)
				{
					this.addStat(StatList.distanceFallenStat, (int)Math.Round((double)p_70069_1_ * 100.0D));
				}

				base.fall(p_70069_1_);
			}
		}

		protected internal override string func_146067_o(int p_146067_1_)
		{
			return p_146067_1_ > 4 ? "game.player.hurt.fall.big" : "game.player.hurt.fall.small";
		}

///    
///     <summary> * This method gets called when the entity kills another one. </summary>
///     
		public override void onKillEntity(EntityLivingBase p_70074_1_)
		{
			if (p_70074_1_ is IMob)
			{
				this.triggerAchievement(AchievementList.killEnemy);
			}

			int var2 = EntityList.getEntityID(p_70074_1_);
			EntityList.EntityEggInfo var3 = (EntityList.EntityEggInfo)EntityList.entityEggs[Convert.ToInt32(var2)];

			if (var3 != null)
			{
				this.addStat(var3.field_151512_d, 1);
			}
		}

///    
///     <summary> * Sets the Entity inside a web block. </summary>
///     
		public override void setInWeb()
		{
			if (!this.capabilities.isFlying)
			{
				base.setInWeb();
			}
		}

///    
///     <summary> * Gets the Icon Index of the item currently held </summary>
///     
		public override IIcon getItemIcon(ItemStack p_70620_1_, int p_70620_2_)
		{
			IIcon var3 = base.getItemIcon(p_70620_1_, p_70620_2_);

			if (p_70620_1_.Item == Items.fishing_rod && this.fishEntity != null)
			{
				var3 = Items.fishing_rod.func_94597_g();
			}
			else
			{
				if (p_70620_1_.Item.requiresMultipleRenderPasses())
				{
					return p_70620_1_.Item.getIconFromDamageForRenderPass(p_70620_1_.ItemDamage, p_70620_2_);
				}

				if (this.itemInUse != null && p_70620_1_.Item == Items.bow)
				{
					int var4 = p_70620_1_.MaxItemUseDuration - this.itemInUseCount;

					if (var4 >= 18)
					{
						return Items.bow.getItemIconForUseDuration(2);
					}

					if (var4 > 13)
					{
						return Items.bow.getItemIconForUseDuration(1);
					}

					if (var4 > 0)
					{
						return Items.bow.getItemIconForUseDuration(0);
					}
				}
			}

			return var3;
		}

		public virtual ItemStack getCurrentArmor(int p_82169_1_)
		{
			return this.inventory.armorItemInSlot(p_82169_1_);
		}

///    
///     <summary> * Add experience points to player. </summary>
///     
		public virtual void addExperience(int p_71023_1_)
		{
			this.addScore(p_71023_1_);
			int var2 = int.MaxValue - this.experienceTotal;

			if (p_71023_1_ > var2)
			{
				p_71023_1_ = var2;
			}

			this.experience += (float)p_71023_1_ / (float)this.xpBarCap();

			for (this.experienceTotal += p_71023_1_; this.experience >= 1.0F; this.experience /= (float)this.xpBarCap())
			{
				this.experience = (this.experience - 1.0F) * (float)this.xpBarCap();
				this.addExperienceLevel(1);
			}
		}

///    
///     <summary> * Add experience levels to this player. </summary>
///     
		public virtual void addExperienceLevel(int p_82242_1_)
		{
			this.experienceLevel += p_82242_1_;

			if (this.experienceLevel < 0)
			{
				this.experienceLevel = 0;
				this.experience = 0.0F;
				this.experienceTotal = 0;
			}

			if (p_82242_1_ > 0 && this.experienceLevel % 5 == 0 && (float)this.field_82249_h < (float)this.ticksExisted - 100.0F)
			{
				float var2 = this.experienceLevel > 30 ? 1.0F : (float)this.experienceLevel / 30.0F;
				this.worldObj.playSoundAtEntity(this, "random.levelup", var2 * 0.75F, 1.0F);
				this.field_82249_h = this.ticksExisted;
			}
		}

///    
///     <summary> * This method returns the cap amount of experience that the experience bar can hold. With each level, the
///     * experience cap on the player's experience bar is raised by 10. </summary>
///     
		public virtual int xpBarCap()
		{
			return this.experienceLevel >= 30 ? 62 + (this.experienceLevel - 30) * 7 : (this.experienceLevel >= 15 ? 17 + (this.experienceLevel - 15) * 3 : 17);
		}

///    
///     <summary> * increases exhaustion level by supplied amount </summary>
///     
		public virtual void addExhaustion(float p_71020_1_)
		{
			if (!this.capabilities.disableDamage)
			{
				if (!this.worldObj.isClient)
				{
					this.foodStats.addExhaustion(p_71020_1_);
				}
			}
		}

///    
///     <summary> * Returns the player's FoodStats object. </summary>
///     
		public virtual FoodStats FoodStats
		{
			get
			{
				return this.foodStats;
			}
		}

		public virtual bool canEat(bool p_71043_1_)
		{
			return (p_71043_1_ || this.foodStats.needFood()) && !this.capabilities.disableDamage;
		}

///    
///     <summary> * Checks if the player's health is not full and not zero. </summary>
///     
		public virtual bool shouldHeal()
		{
			return this.Health > 0.0F && this.Health < this.MaxHealth;
		}

///    
///     <summary> * sets the itemInUse when the use item button is clicked. Args: itemstack, int maxItemUseDuration </summary>
///     
		public virtual void setItemInUse(ItemStack p_71008_1_, int p_71008_2_)
		{
			if (p_71008_1_ != this.itemInUse)
			{
				this.itemInUse = p_71008_1_;
				this.itemInUseCount = p_71008_2_;

				if (!this.worldObj.isClient)
				{
					this.Eating = true;
				}
			}
		}

///    
///     <summary> * Returns true if the given block can be mined with the current tool in adventure mode. </summary>
///     
		public virtual bool isCurrentToolAdventureModeExempt(int p_82246_1_, int p_82246_2_, int p_82246_3_)
		{
			if (this.capabilities.allowEdit)
			{
				return true;
			}
			else
			{
				Block var4 = this.worldObj.getBlock(p_82246_1_, p_82246_2_, p_82246_3_);

				if (var4.Material != Material.air)
				{
					if (var4.Material.AdventureModeExempt)
					{
						return true;
					}

					if (this.CurrentEquippedItem != null)
					{
						ItemStack var5 = this.CurrentEquippedItem;

						if (var5.func_150998_b(var4) || var5.func_150997_a(var4) > 1.0F)
						{
							return true;
						}
					}
				}

				return false;
			}
		}

		public virtual bool canPlayerEdit(int p_82247_1_, int p_82247_2_, int p_82247_3_, int p_82247_4_, ItemStack p_82247_5_)
		{
			return this.capabilities.allowEdit ? true : (p_82247_5_ != null ? p_82247_5_.canEditBlocks() : false);
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal override int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			if (this.worldObj.GameRules.getGameRuleBooleanValue("keepInventory"))
			{
				return 0;
			}
			else
			{
				int var2 = this.experienceLevel * 7;
				return var2 > 100 ? 100 : var2;
			}
		}

///    
///     <summary> * Only use is to identify if class is an instance of player for experience dropping </summary>
///     
		protected internal override bool isPlayer()
		{
			get
			{
				return true;
			}
		}

		public override bool AlwaysRenderNameTagForRender
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Copies the values from the given player into this player if boolean par2 is true. Always clones Ender Chest
///     * Inventory. </summary>
///     
		public virtual void clonePlayer(EntityPlayer p_71049_1_, bool p_71049_2_)
		{
			if (p_71049_2_)
			{
				this.inventory.copyInventory(p_71049_1_.inventory);
				this.Health = p_71049_1_.Health;
				this.foodStats = p_71049_1_.foodStats;
				this.experienceLevel = p_71049_1_.experienceLevel;
				this.experienceTotal = p_71049_1_.experienceTotal;
				this.experience = p_71049_1_.experience;
				this.Score = p_71049_1_.Score;
				this.teleportDirection = p_71049_1_.teleportDirection;
			}
			else if (this.worldObj.GameRules.getGameRuleBooleanValue("keepInventory"))
			{
				this.inventory.copyInventory(p_71049_1_.inventory);
				this.experienceLevel = p_71049_1_.experienceLevel;
				this.experienceTotal = p_71049_1_.experienceTotal;
				this.experience = p_71049_1_.experience;
				this.Score = p_71049_1_.Score;
			}

			this.theInventoryEnderChest = p_71049_1_.theInventoryEnderChest;
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return !this.capabilities.isFlying;
		}

///    
///     <summary> * Sends the player's abilities to the server (if there is one). </summary>
///     
		public virtual void sendPlayerAbilities()
		{
		}

///    
///     <summary> * Sets the player's game mode and sends it to them. </summary>
///     
		public virtual WorldSettings.GameType GameType
		{
			set
			{
			}
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public override string CommandSenderName
		{
			get
			{
				return this.field_146106_i.Name;
			}
		}

		public virtual nWorld EntityWorld
		{
			get
			{
				return this.worldObj;
			}
		}

///    
///     <summary> * Returns the InventoryEnderChest of this player. </summary>
///     
		public virtual InventoryEnderChest InventoryEnderChest
		{
			get
			{
				return this.theInventoryEnderChest;
			}
		}

///    
///     <summary> * 0: Tool in Hand; 1-4: Armor </summary>
///     
		public override ItemStack getEquipmentInSlot(int p_71124_1_)
		{
			return p_71124_1_ == 0 ? this.inventory.CurrentItem : this.inventory.armorInventory[p_71124_1_ - 1];
		}

///    
///     <summary> * Returns the item that this EntityLiving is holding, if any. </summary>
///     
		public override ItemStack HeldItem
		{
			get
			{
				return this.inventory.CurrentItem;
			}
		}

///    
///     <summary> * Sets the held item, or an armor slot. Slot 0 is held item. Slot 1-4 is armor. Params: Item, slot </summary>
///     
		public override void setCurrentItemOrArmor(int p_70062_1_, ItemStack p_70062_2_)
		{
			this.inventory.armorInventory[p_70062_1_] = p_70062_2_;
		}

///    
///     <summary> * Only used by renderer in EntityLivingBase subclasses.\nDetermines if an entity is visible or not to a specfic
///     * player, if the entity is normally invisible.\nFor EntityLivingBase subclasses, returning false when invisible
///     * will render the entity semitransparent. </summary>
///     
		public override bool isInvisibleToPlayer(EntityPlayer p_98034_1_)
		{
			if (!this.Invisible)
			{
				return false;
			}
			else
			{
				Team var2 = this.Team;
				return var2 == null || p_98034_1_ == null || p_98034_1_.Team != var2 || !var2.func_98297_h();
			}
		}

		public override ItemStack[] LastActiveItems
		{
			get
			{
				return this.inventory.armorInventory;
			}
		}

		public virtual bool HideCape
		{
			get
			{
				return this.getHideCape(1);
			}
		}

		public override bool isPushedByWater()
		{
			get
			{
				return !this.capabilities.isFlying;
			}
		}

		public virtual nScoreboard WorldScoreboard
		{
			get
			{
				return this.worldObj.Scoreboard;
			}
		}

		public override Team Team
		{
			get
			{
				return this.WorldScoreboard.getPlayersTeam(this.CommandSenderName);
			}
		}

		public override IChatComponent func_145748_c_()
		{
			ChatComponentText var1 = new ChatComponentText(ScorePlayerTeam.formatPlayerName(this.Team, this.CommandSenderName));
			var1.ChatStyle.ChatClickEvent = new ClickEvent(ClickEvent.Action.SUGGEST_COMMAND, "/msg " + this.CommandSenderName + " ");
			return var1;
		}

		public override float AbsorptionAmount
		{
			set
			{
				if (value < 0.0F)
				{
					value = 0.0F;
				}
	
				this.DataWatcher.updateObject(17, Convert.ToSingle(value));
			}
			get
			{
				return this.DataWatcher.getWatchableObjectFloat(17);
			}
		}


		public static UUID func_146094_a(GameProfile p_146094_0_)
		{
			UUID var1 = p_146094_0_.Id;

			if (var1 == null)
			{
				var1 = UUID.nameUUIDFromBytes(("OfflinePlayer:" + p_146094_0_.Name).getBytes(Charsets.UTF_8));
			}

			return var1;
		}

		public enum EnumChatVisibility
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			FULL("FULL", 0, 0, "options.chat.visibility.full"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SYSTEM("SYSTEM", 1, 1, "options.chat.visibility.system"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			HIDDEN("HIDDEN", 2, 2, "options.chat.visibility.hidden");
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final EntityPlayer.EnumChatVisibility[] field_151432_d = new EntityPlayer.EnumChatVisibility[values().length];
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final int chatVisibility;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final String resourceKey;

			@private static final EntityPlayer.EnumChatVisibility[] $VALUES = new EntityPlayer.EnumChatVisibility[]{FULL, SYSTEM, HIDDEN
		}
			

			private EnumChatVisibility(string p_i45323_1_, int p_i45323_2_, int p_i45323_3_, string p_i45323_4_)
			{
				this.chatVisibility = p_i45323_3_;
				this.resourceKey = p_i45323_4_;
			}

			public virtual int ChatVisibility
			{
				get
				{
					return this.chatVisibility;
				}
			}

			public static EntityPlayer.EnumChatVisibility getEnumChatVisibility(int p_151426_0_)
			{
				return field_151432_d[p_151426_0_ % field_151432_d.length];
			}

			public virtual string ResourceKey
			{
				get
				{
					return this.resourceKey;
				}
			}

			static EntityPlayer()
			{
				EntityPlayer.EnumChatVisibility[] var0 = values();
				int var1 = var0.Length;

				for (int var2 = 0; var2 < var1; ++var2)
				{
					EntityPlayer.EnumChatVisibility var3 = var0[var2];
					field_151432_d[var3.chatVisibility] = var3;
				}
			}
		}

		public enum EnumStatus
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OK("OK", 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			NOT_POSSIBLE_HERE("NOT_POSSIBLE_HERE", 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			NOT_POSSIBLE_NOW("NOT_POSSIBLE_NOW", 2),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			TOO_FAR_AWAY("TOO_FAR_AWAY", 3),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OTHER_PROBLEM("OTHER_PROBLEM", 4),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			NOT_SAFE("NOT_SAFE", 5);

			@private static final EntityPlayer.EnumStatus[] $VALUES = new EntityPlayer.EnumStatus[]{OK, NOT_POSSIBLE_HERE, NOT_POSSIBLE_NOW, TOO_FAR_AWAY, OTHER_PROBLEM, NOT_SAFE
		}
//JAVA TO VB & C# CONVERTER TODO TASK: The following line could not be converted:
			

//JAVA TO VB & C# CONVERTER TODO TASK: The following line could not be converted:
			private EnumStatus(String p_i1751_1_, int p_i1751_2_)
			{
			}
		}
	}

}