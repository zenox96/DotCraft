using System;
using System.Collections;

namespace DotCraftCore.Entity.Player
{

	using Sets = com.google.common.collect.Sets;
	using GameProfile = com.mojang.authlib.GameProfile;
	using Unpooled = io.netty.buffer.Unpooled;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityList = DotCraftCore.Entity.EntityList;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IMerchant = DotCraftCore.Entity.IMerchant;
	using EntityMinecartHopper = DotCraftCore.Entity.Item.EntityMinecartHopper;
	using EntityHorse = DotCraftCore.Entity.Passive.EntityHorse;
	using EntityArrow = DotCraftCore.Entity.Projectile.EntityArrow;
	using Container = DotCraftCore.Inventory.Container;
	using ContainerBeacon = DotCraftCore.Inventory.ContainerBeacon;
	using ContainerBrewingStand = DotCraftCore.Inventory.ContainerBrewingStand;
	using ContainerChest = DotCraftCore.Inventory.ContainerChest;
	using ContainerDispenser = DotCraftCore.Inventory.ContainerDispenser;
	using ContainerEnchantment = DotCraftCore.Inventory.ContainerEnchantment;
	using ContainerFurnace = DotCraftCore.Inventory.ContainerFurnace;
	using ContainerHopper = DotCraftCore.Inventory.ContainerHopper;
	using ContainerHorseInventory = DotCraftCore.Inventory.ContainerHorseInventory;
	using ContainerMerchant = DotCraftCore.Inventory.ContainerMerchant;
	using ContainerRepair = DotCraftCore.Inventory.ContainerRepair;
	using ContainerWorkbench = DotCraftCore.Inventory.ContainerWorkbench;
	using ICrafting = DotCraftCore.Inventory.ICrafting;
	using IInventory = DotCraftCore.Inventory.IInventory;
	using InventoryMerchant = DotCraftCore.Inventory.InventoryMerchant;
	using SlotCrafting = DotCraftCore.Inventory.SlotCrafting;
	using EnumAction = DotCraftCore.item.EnumAction;
	using ItemMapBase = DotCraftCore.item.ItemMapBase;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NetHandlerPlayServer = DotCraftCore.network.NetHandlerPlayServer;
	using Packet = DotCraftCore.network.Packet;
	using PacketBuffer = DotCraftCore.network.PacketBuffer;
	using C15PacketClientSettings = DotCraftCore.network.play.client.C15PacketClientSettings;
	using S02PacketChat = DotCraftCore.network.play.server.S02PacketChat;
	using S06PacketUpdateHealth = DotCraftCore.network.play.server.S06PacketUpdateHealth;
	using S0APacketUseBed = DotCraftCore.network.play.server.S0APacketUseBed;
	using S0BPacketAnimation = DotCraftCore.network.play.server.S0BPacketAnimation;
	using S13PacketDestroyEntities = DotCraftCore.network.play.server.S13PacketDestroyEntities;
	using S19PacketEntityStatus = DotCraftCore.network.play.server.S19PacketEntityStatus;
	using S1BPacketEntityAttach = DotCraftCore.network.play.server.S1BPacketEntityAttach;
	using S1DPacketEntityEffect = DotCraftCore.network.play.server.S1DPacketEntityEffect;
	using S1EPacketRemoveEntityEffect = DotCraftCore.network.play.server.S1EPacketRemoveEntityEffect;
	using S1FPacketSetExperience = DotCraftCore.network.play.server.S1FPacketSetExperience;
	using S26PacketMapChunkBulk = DotCraftCore.network.play.server.S26PacketMapChunkBulk;
	using S2BPacketChangeGameState = DotCraftCore.network.play.server.S2BPacketChangeGameState;
	using S2DPacketOpenWindow = DotCraftCore.network.play.server.S2DPacketOpenWindow;
	using S2EPacketCloseWindow = DotCraftCore.network.play.server.S2EPacketCloseWindow;
	using S2FPacketSetSlot = DotCraftCore.network.play.server.S2FPacketSetSlot;
	using S30PacketWindowItems = DotCraftCore.network.play.server.S30PacketWindowItems;
	using S31PacketWindowProperty = DotCraftCore.network.play.server.S31PacketWindowProperty;
	using S36PacketSignEditorOpen = DotCraftCore.network.play.server.S36PacketSignEditorOpen;
	using S39PacketPlayerAbilities = DotCraftCore.network.play.server.S39PacketPlayerAbilities;
	using S3FPacketCustomPayload = DotCraftCore.network.play.server.S3FPacketCustomPayload;
	using PotionEffect = DotCraftCore.potion.PotionEffect;
	using IScoreObjectiveCriteria = DotCraftCore.scoreboard.IScoreObjectiveCriteria;
	using Score = DotCraftCore.scoreboard.Score;
	using ScoreObjective = DotCraftCore.scoreboard.ScoreObjective;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ItemInWorldManager = DotCraftCore.server.management.ItemInWorldManager;
	using UserListOpsEntry = DotCraftCore.server.management.UserListOpsEntry;
	using AchievementList = DotCraftCore.stats.AchievementList;
	using StatBase = DotCraftCore.stats.StatBase;
	using StatList = DotCraftCore.stats.StatList;
	using StatisticsFile = DotCraftCore.stats.StatisticsFile;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityBeacon = DotCraftCore.tileentity.TileEntityBeacon;
	using TileEntityBrewingStand = DotCraftCore.tileentity.TileEntityBrewingStand;
	using TileEntityDispenser = DotCraftCore.tileentity.TileEntityDispenser;
	using TileEntityDropper = DotCraftCore.tileentity.TileEntityDropper;
	using TileEntityFurnace = DotCraftCore.tileentity.TileEntityFurnace;
	using TileEntityHopper = DotCraftCore.tileentity.TileEntityHopper;
	using TileEntitySign = DotCraftCore.tileentity.TileEntitySign;
	using ChunkCoordinates = DotCraftCore.util.ChunkCoordinates;
	using DamageSource = DotCraftCore.util.DamageSource;
	using EntityDamageSource = DotCraftCore.util.EntityDamageSource;
	using IChatComponent = DotCraftCore.util.IChatComponent;
	using JsonSerializableSet = DotCraftCore.util.JsonSerializableSet;
	using MathHelper = DotCraftCore.util.MathHelper;
	using ReportedException = DotCraftCore.util.ReportedException;
	using MerchantRecipeList = DotCraftCore.village.MerchantRecipeList;
	using ChunkCoordIntPair = DotCraftCore.world.ChunkCoordIntPair;
	using WorldServer = DotCraftCore.world.WorldServer;
	using WorldSettings = DotCraftCore.world.WorldSettings;
	using BiomeGenBase = DotCraftCore.world.biome.BiomeGenBase;
	using Chunk = DotCraftCore.world.chunk.Chunk;
	using Charsets = org.apache.commons.io.Charsets;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class EntityPlayerMP : EntityPlayer, ICrafting
	{
		private static readonly Logger logger = LogManager.Logger;
		private string translator = "en_US";

///    
///     <summary> * The NetServerHandler assigned to this player by the ServerConfigurationManager. </summary>
///     
		public NetHandlerPlayServer playerNetServerHandler;

	/// <summary> Reference to the MinecraftServer object.  </summary>
		public readonly MinecraftServer mcServer;

	/// <summary> The ItemInWorldManager belonging to this player  </summary>
		public readonly ItemInWorldManager theItemInWorldManager;

	/// <summary> player X position as seen by PlayerManager  </summary>
		public double managedPosX;

	/// <summary> player Z position as seen by PlayerManager  </summary>
		public double managedPosZ;

	/// <summary> LinkedList that holds the loaded chunks.  </summary>
		public readonly IList loadedChunks = new LinkedList();

	/// <summary> entities added to this list will  be packet29'd to the player  </summary>
		private readonly IList destroyedItemsNetCache = new LinkedList();
		private readonly StatisticsFile field_147103_bO;
		private float field_130068_bO = float.MIN_VALUE;

	/// <summary> amount of health the client was last set to  </summary>
		private float lastHealth = -1.0E8F;

	/// <summary> set to foodStats.GetFoodLevel  </summary>
		private int lastFoodLevel = -99999999;

	/// <summary> set to foodStats.getSaturationLevel() == 0.0F each tick  </summary>
		private bool wasHungry = true;

	/// <summary> Amount of experience the client was last set to  </summary>
		private int lastExperience = -99999999;
		private int field_147101_bU = 60;
		private EntityPlayer.EnumChatVisibility chatVisibility;
		private bool chatColours = true;
		private long field_143005_bX = System.currentTimeMillis();

///    
///     <summary> * The currently in use window ID. Incremented every time a window is opened. </summary>
///     
		private int currentWindowId;

///    
///     <summary> * set to true when player is moving quantity of items from one inventory to another(crafting) but item in either
///     * slot is not changed </summary>
///     
		public bool isChangingQuantityOnly;
		public int ping;

///    
///     <summary> * Set when a player beats the ender dragon, used to respawn the player at the spawn point while retaining inventory
///     * and XP </summary>
///     
		public bool playerConqueredTheEnd;
		private const string __OBFID = "CL_00001440";

		public EntityPlayerMP(MinecraftServer p_i45285_1_, WorldServer p_i45285_2_, GameProfile p_i45285_3_, ItemInWorldManager p_i45285_4_) : base(p_i45285_2_, p_i45285_3_)
		{
			p_i45285_4_.thisPlayerMP = this;
			this.theItemInWorldManager = p_i45285_4_;
			ChunkCoordinates var5 = p_i45285_2_.SpawnPoint;
			int var6 = var5.posX;
			int var7 = var5.posZ;
			int var8 = var5.posY;

			if (!p_i45285_2_.provider.hasNoSky && p_i45285_2_.WorldInfo.GameType != WorldSettings.GameType.ADVENTURE)
			{
				int var9 = Math.Max(5, p_i45285_1_.SpawnProtectionSize - 6);
				var6 += this.rand.Next(var9 * 2) - var9;
				var7 += this.rand.Next(var9 * 2) - var9;
				var8 = p_i45285_2_.getTopSolidOrLiquidBlock(var6, var7);
			}

			this.mcServer = p_i45285_1_;
			this.field_147103_bO = p_i45285_1_.ConfigurationManager.func_152602_a(this);
			this.stepHeight = 0.0F;
			this.yOffset = 0.0F;
			this.setLocationAndAngles((double)var6 + 0.5D, (double)var8, (double)var7 + 0.5D, 0.0F, 0.0F);

			while (!p_i45285_2_.getCollidingBoundingBoxes(this, this.boundingBox).Empty)
			{
				this.setPosition(this.posX, this.posY + 1.0D, this.posZ);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);

			if (p_70037_1_.func_150297_b("playerGameType", 99))
			{
				if (MinecraftServer.Server.ForceGamemode)
				{
					this.theItemInWorldManager.GameType = MinecraftServer.Server.GameType;
				}
				else
				{
					this.theItemInWorldManager.GameType = WorldSettings.GameType.getByID(p_70037_1_.getInteger("playerGameType"));
				}
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("playerGameType", this.theItemInWorldManager.GameType.ID);
		}

///    
///     <summary> * Add experience levels to this player. </summary>
///     
		public override void addExperienceLevel(int p_82242_1_)
		{
			base.addExperienceLevel(p_82242_1_);
			this.lastExperience = -1;
		}

		public virtual void addSelfToInternalCraftingInventory()
		{
			this.openContainer.addCraftingToCrafters(this);
		}

///    
///     <summary> * sets the players height back to normal after doing things like sleeping and dieing </summary>
///     
		protected internal override void resetHeight()
		{
			this.yOffset = 0.0F;
		}

		public override float EyeHeight
		{
			get
			{
				return 1.62F;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			this.theItemInWorldManager.updateBlockRemoving();
			--this.field_147101_bU;

			if (this.hurtResistantTime > 0)
			{
				--this.hurtResistantTime;
			}

			this.openContainer.detectAndSendChanges();

			if (!this.worldObj.isClient && !this.openContainer.canInteractWith(this))
			{
				this.closeScreen();
				this.openContainer = this.inventoryContainer;
			}

			while (!this.destroyedItemsNetCache.Count == 0)
			{
				int var1 = Math.Min(this.destroyedItemsNetCache.Count, 127);
				int[] var2 = new int[var1];
				IEnumerator var3 = this.destroyedItemsNetCache.GetEnumerator();
				int var4 = 0;

				while (var3.MoveNext() && var4 < var1)
				{
					var2[var4++] = (int)((int?)var3.Current);
					var3.remove();
				}

				this.playerNetServerHandler.sendPacket(new S13PacketDestroyEntities(var2));
			}

			if (!this.loadedChunks.Count == 0)
			{
				ArrayList var6 = new ArrayList();
				IEnumerator var7 = this.loadedChunks.GetEnumerator();
				ArrayList var8 = new ArrayList();
				Chunk var5;

				while (var7.MoveNext() && var6.Count < S26PacketMapChunkBulk.func_149258_c())
				{
					ChunkCoordIntPair var9 = (ChunkCoordIntPair)var7.Current;

					if (var9 != null)
					{
						if (this.worldObj.blockExists(var9.chunkXPos << 4, 0, var9.chunkZPos << 4))
						{
							var5 = this.worldObj.getChunkFromChunkCoords(var9.chunkXPos, var9.chunkZPos);

							if (var5.func_150802_k())
							{
								var6.Add(var5);
								var8.AddRange(((WorldServer)this.worldObj).func_147486_a(var9.chunkXPos * 16, 0, var9.chunkZPos * 16, var9.chunkXPos * 16 + 16, 256, var9.chunkZPos * 16 + 16));
								var7.remove();
							}
						}
					}
					else
					{
						var7.remove();
					}
				}

				if (!var6.Count == 0)
				{
					this.playerNetServerHandler.sendPacket(new S26PacketMapChunkBulk(var6));
					IEnumerator var10 = var8.GetEnumerator();

					while (var10.MoveNext())
					{
						TileEntity var11 = (TileEntity)var10.Current;
						this.func_147097_b(var11);
					}

					var10 = var6.GetEnumerator();

					while (var10.MoveNext())
					{
						var5 = (Chunk)var10.Current;
						this.ServerForPlayer.EntityTracker.func_85172_a(this, var5);
					}
				}
			}
		}

		public virtual void onUpdateEntity()
		{
			try
			{
				base.onUpdate();

				for (int var1 = 0; var1 < this.inventory.SizeInventory; ++var1)
				{
					ItemStack var6 = this.inventory.getStackInSlot(var1);

					if (var6 != null && var6.Item.Map)
					{
						Packet var8 = ((ItemMapBase)var6.Item).func_150911_c(var6, this.worldObj, this);

						if (var8 != null)
						{
							this.playerNetServerHandler.sendPacket(var8);
						}
					}
				}

				if (this.Health != this.lastHealth || this.lastFoodLevel != this.foodStats.FoodLevel || this.foodStats.SaturationLevel == 0.0F != this.wasHungry)
				{
					this.playerNetServerHandler.sendPacket(new S06PacketUpdateHealth(this.Health, this.foodStats.FoodLevel, this.foodStats.SaturationLevel));
					this.lastHealth = this.Health;
					this.lastFoodLevel = this.foodStats.FoodLevel;
					this.wasHungry = this.foodStats.SaturationLevel == 0.0F;
				}

				if (this.Health + this.AbsorptionAmount != this.field_130068_bO)
				{
					this.field_130068_bO = this.Health + this.AbsorptionAmount;
					ICollection var5 = this.WorldScoreboard.func_96520_a(IScoreObjectiveCriteria.health);
					IEnumerator var7 = var5.GetEnumerator();

					while (var7.MoveNext())
					{
						ScoreObjective var9 = (ScoreObjective)var7.Current;
						this.WorldScoreboard.func_96529_a(this.CommandSenderName, var9).func_96651_a(new EntityPlayer[] {this});
					}
				}

				if (this.experienceTotal != this.lastExperience)
				{
					this.lastExperience = this.experienceTotal;
					this.playerNetServerHandler.sendPacket(new S1FPacketSetExperience(this.experience, this.experienceTotal, this.experienceLevel));
				}

				if (this.ticksExisted % 20 * 5 == 0 && !this.func_147099_x().hasAchievementUnlocked(AchievementList.field_150961_L))
				{
					this.func_147098_j();
				}
			}
			catch (Exception var4)
			{
				CrashReport var2 = CrashReport.makeCrashReport(var4, "Ticking player");
				CrashReportCategory var3 = var2.makeCategory("Player being ticked");
				this.addEntityCrashInfo(var3);
				throw new ReportedException(var2);
			}
		}

		protected internal virtual void func_147098_j()
		{
			BiomeGenBase var1 = this.worldObj.getBiomeGenForCoords(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posZ));

			if (var1 != null)
			{
				string var2 = var1.biomeName;
				JsonSerializableSet var3 = (JsonSerializableSet)this.func_147099_x().func_150870_b(AchievementList.field_150961_L);

				if (var3 == null)
				{
					var3 = (JsonSerializableSet)this.func_147099_x().func_150872_a(AchievementList.field_150961_L, new JsonSerializableSet());
				}

				var3.add(var2);

				if (this.func_147099_x().canUnlockAchievement(AchievementList.field_150961_L) && var3.size() == BiomeGenBase.field_150597_n.size())
				{
					HashSet var4 = Sets.newHashSet(BiomeGenBase.field_150597_n);
					IEnumerator var5 = var3.GetEnumerator();

					while (var5.MoveNext())
					{
						string var6 = (string)var5.Current;
						IEnumerator var7 = var4.GetEnumerator();

						while (var7.MoveNext())
						{
							BiomeGenBase var8 = (BiomeGenBase)var7.Current;

							if (var8.biomeName.Equals(var6))
							{
								var7.remove();
							}
						}

						if (var4.Count == 0)
						{
							break;
						}
					}

					if (var4.Count == 0)
					{
						this.triggerAchievement(AchievementList.field_150961_L);
					}
				}
			}
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public override void onDeath(DamageSource p_70645_1_)
		{
			this.mcServer.ConfigurationManager.func_148539_a(this.func_110142_aN().func_151521_b());

			if (!this.worldObj.GameRules.getGameRuleBooleanValue("keepInventory"))
			{
				this.inventory.dropAllItems();
			}

			ICollection var2 = this.worldObj.Scoreboard.func_96520_a(IScoreObjectiveCriteria.deathCount);
			IEnumerator var3 = var2.GetEnumerator();

			while (var3.MoveNext())
			{
				ScoreObjective var4 = (ScoreObjective)var3.Current;
				Score var5 = this.WorldScoreboard.func_96529_a(this.CommandSenderName, var4);
				var5.func_96648_a();
			}

			EntityLivingBase var6 = this.func_94060_bK();

			if (var6 != null)
			{
				int var7 = EntityList.getEntityID(var6);
				EntityList.EntityEggInfo var8 = (EntityList.EntityEggInfo)EntityList.entityEggs[Convert.ToInt32(var7)];

				if (var8 != null)
				{
					this.addStat(var8.field_151513_e, 1);
				}

				var6.addToPlayerScore(this, this.scoreValue);
			}

			this.addStat(StatList.deathsStat, 1);
			this.func_110142_aN().func_94549_h();
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
				bool var3 = this.mcServer.DedicatedServer && this.mcServer.PVPEnabled && "fall".Equals(p_70097_1_.damageType);

				if (!var3 && this.field_147101_bU > 0 && p_70097_1_ != DamageSource.outOfWorld)
				{
					return false;
				}
				else
				{
					if (p_70097_1_ is EntityDamageSource)
					{
						Entity var4 = p_70097_1_.Entity;

						if (var4 is EntityPlayer && !this.canAttackPlayer((EntityPlayer)var4))
						{
							return false;
						}

						if (var4 is EntityArrow)
						{
							EntityArrow var5 = (EntityArrow)var4;

							if (var5.shootingEntity is EntityPlayer && !this.canAttackPlayer((EntityPlayer)var5.shootingEntity))
							{
								return false;
							}
						}
					}

					return base.attackEntityFrom(p_70097_1_, p_70097_2_);
				}
			}
		}

		public override bool canAttackPlayer(EntityPlayer p_96122_1_)
		{
			return !this.mcServer.PVPEnabled ? false : base.canAttackPlayer(p_96122_1_);
		}

///    
///     <summary> * Teleports the entity to another dimension. Params: Dimension number to teleport to </summary>
///     
		public override void travelToDimension(int p_71027_1_)
		{
			if (this.dimension == 1 && p_71027_1_ == 1)
			{
				this.triggerAchievement(AchievementList.theEnd2);
				this.worldObj.removeEntity(this);
				this.playerConqueredTheEnd = true;
				this.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(4, 0.0F));
			}
			else
			{
				if (this.dimension == 0 && p_71027_1_ == 1)
				{
					this.triggerAchievement(AchievementList.theEnd);
					ChunkCoordinates var2 = this.mcServer.worldServerForDimension(p_71027_1_).EntrancePortalLocation;

					if (var2 != null)
					{
						this.playerNetServerHandler.setPlayerLocation((double)var2.posX, (double)var2.posY, (double)var2.posZ, 0.0F, 0.0F);
					}

					p_71027_1_ = 1;
				}
				else
				{
					this.triggerAchievement(AchievementList.portal);
				}

				this.mcServer.ConfigurationManager.transferPlayerToDimension(this, p_71027_1_);
				this.lastExperience = -1;
				this.lastHealth = -1.0F;
				this.lastFoodLevel = -1;
			}
		}

		private void func_147097_b(TileEntity p_147097_1_)
		{
			if (p_147097_1_ != null)
			{
				Packet var2 = p_147097_1_.DescriptionPacket;

				if (var2 != null)
				{
					this.playerNetServerHandler.sendPacket(var2);
				}
			}
		}

///    
///     <summary> * Called whenever an item is picked up from walking over it. Args: pickedUpEntity, stackSize </summary>
///     
		public override void onItemPickup(Entity p_71001_1_, int p_71001_2_)
		{
			base.onItemPickup(p_71001_1_, p_71001_2_);
			this.openContainer.detectAndSendChanges();
		}

///    
///     <summary> * puts player to sleep on specified bed if possible </summary>
///     
		public override EntityPlayer.EnumStatus sleepInBedAt(int p_71018_1_, int p_71018_2_, int p_71018_3_)
		{
			EntityPlayer.EnumStatus var4 = base.sleepInBedAt(p_71018_1_, p_71018_2_, p_71018_3_);

			if (var4 == EntityPlayer.EnumStatus.OK)
			{
				S0APacketUseBed var5 = new S0APacketUseBed(this, p_71018_1_, p_71018_2_, p_71018_3_);
				this.ServerForPlayer.EntityTracker.func_151247_a(this, var5);
				this.playerNetServerHandler.setPlayerLocation(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
				this.playerNetServerHandler.sendPacket(var5);
			}

			return var4;
		}

///    
///     <summary> * Wake up the player if they're sleeping. </summary>
///     
		public override void wakeUpPlayer(bool p_70999_1_, bool p_70999_2_, bool p_70999_3_)
		{
			if (this.PlayerSleeping)
			{
				this.ServerForPlayer.EntityTracker.func_151248_b(this, new S0BPacketAnimation(this, 2));
			}

			base.wakeUpPlayer(p_70999_1_, p_70999_2_, p_70999_3_);

			if (this.playerNetServerHandler != null)
			{
				this.playerNetServerHandler.setPlayerLocation(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
			}
		}

///    
///     <summary> * Called when a player mounts an entity. e.g. mounts a pig, mounts a boat. </summary>
///     
		public override void mountEntity(Entity p_70078_1_)
		{
			base.mountEntity(p_70078_1_);
			this.playerNetServerHandler.sendPacket(new S1BPacketEntityAttach(0, this, this.ridingEntity));
			this.playerNetServerHandler.setPlayerLocation(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
		}

///    
///     <summary> * Takes in the distance the entity has fallen this tick and whether its on the ground to update the fall distance
///     * and deal fall damage if landing on the ground.  Args: distanceFallenThisTick, onGround </summary>
///     
		protected internal override void updateFallState(double p_70064_1_, bool p_70064_3_)
		{
		}

///    
///     <summary> * process player falling based on movement packet </summary>
///     
		public virtual void handleFalling(double p_71122_1_, bool p_71122_3_)
		{
			base.updateFallState(p_71122_1_, p_71122_3_);
		}

		public override void func_146100_a(TileEntity p_146100_1_)
		{
			if (p_146100_1_ is TileEntitySign)
			{
				((TileEntitySign)p_146100_1_).func_145912_a(this);
				this.playerNetServerHandler.sendPacket(new S36PacketSignEditorOpen(p_146100_1_.field_145851_c, p_146100_1_.field_145848_d, p_146100_1_.field_145849_e));
			}
		}

///    
///     <summary> * get the next window id to use </summary>
///     
		private void NextWindowId
		{
			get
			{
				this.currentWindowId = this.currentWindowId % 100 + 1;
			}
		}

///    
///     <summary> * Displays the crafting GUI for a workbench. </summary>
///     
		public override void displayGUIWorkbench(int p_71058_1_, int p_71058_2_, int p_71058_3_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 1, "Crafting", 9, true));
			this.openContainer = new ContainerWorkbench(this.inventory, this.worldObj, p_71058_1_, p_71058_2_, p_71058_3_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void displayGUIEnchantment(int p_71002_1_, int p_71002_2_, int p_71002_3_, string p_71002_4_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 4, p_71002_4_ == null ? "" : p_71002_4_, 9, p_71002_4_ != null));
			this.openContainer = new ContainerEnchantment(this.inventory, this.worldObj, p_71002_1_, p_71002_2_, p_71002_3_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

///    
///     <summary> * Displays the GUI for interacting with an anvil. </summary>
///     
		public override void displayGUIAnvil(int p_82244_1_, int p_82244_2_, int p_82244_3_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 8, "Repairing", 9, true));
			this.openContainer = new ContainerRepair(this.inventory, this.worldObj, p_82244_1_, p_82244_2_, p_82244_3_, this);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

///    
///     <summary> * Displays the GUI for interacting with a chest inventory. Args: chestInventory </summary>
///     
		public override void displayGUIChest(IInventory p_71007_1_)
		{
			if (this.openContainer != this.inventoryContainer)
			{
				this.closeScreen();
			}

			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 0, p_71007_1_.InventoryName, p_71007_1_.SizeInventory, p_71007_1_.InventoryNameLocalized));
			this.openContainer = new ContainerChest(this.inventory, p_71007_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void func_146093_a(TileEntityHopper p_146093_1_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 9, p_146093_1_.InventoryName, p_146093_1_.SizeInventory, p_146093_1_.InventoryNameLocalized));
			this.openContainer = new ContainerHopper(this.inventory, p_146093_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void displayGUIHopperMinecart(EntityMinecartHopper p_96125_1_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 9, p_96125_1_.InventoryName, p_96125_1_.SizeInventory, p_96125_1_.InventoryNameLocalized));
			this.openContainer = new ContainerHopper(this.inventory, p_96125_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void func_146101_a(TileEntityFurnace p_146101_1_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 2, p_146101_1_.InventoryName, p_146101_1_.SizeInventory, p_146101_1_.InventoryNameLocalized));
			this.openContainer = new ContainerFurnace(this.inventory, p_146101_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void func_146102_a(TileEntityDispenser p_146102_1_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, p_146102_1_ is TileEntityDropper ? 10 : 3, p_146102_1_.InventoryName, p_146102_1_.SizeInventory, p_146102_1_.InventoryNameLocalized));
			this.openContainer = new ContainerDispenser(this.inventory, p_146102_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void func_146098_a(TileEntityBrewingStand p_146098_1_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 5, p_146098_1_.InventoryName, p_146098_1_.SizeInventory, p_146098_1_.InventoryNameLocalized));
			this.openContainer = new ContainerBrewingStand(this.inventory, p_146098_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void func_146104_a(TileEntityBeacon p_146104_1_)
		{
			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 7, p_146104_1_.InventoryName, p_146104_1_.SizeInventory, p_146104_1_.InventoryNameLocalized));
			this.openContainer = new ContainerBeacon(this.inventory, p_146104_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

		public override void displayGUIMerchant(IMerchant p_71030_1_, string p_71030_2_)
		{
			this.NextWindowId;
			this.openContainer = new ContainerMerchant(this.inventory, p_71030_1_, this.worldObj);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
			InventoryMerchant var3 = ((ContainerMerchant)this.openContainer).MerchantInventory;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 6, p_71030_2_ == null ? "" : p_71030_2_, var3.SizeInventory, p_71030_2_ != null));
			MerchantRecipeList var4 = p_71030_1_.getRecipes(this);

			if (var4 != null)
			{
				PacketBuffer var5 = new PacketBuffer(Unpooled.buffer());

				try
				{
					var5.writeInt(this.currentWindowId);
					var4.func_151391_a(var5);
					this.playerNetServerHandler.sendPacket(new S3FPacketCustomPayload("MC|TrList", var5));
				}
				catch (IOException var10)
				{
					logger.error("Couldn\'t send trade list", var10);
				}
				finally
				{
					var5.release();
				}
			}
		}

		public override void displayGUIHorse(EntityHorse p_110298_1_, IInventory p_110298_2_)
		{
			if (this.openContainer != this.inventoryContainer)
			{
				this.closeScreen();
			}

			this.NextWindowId;
			this.playerNetServerHandler.sendPacket(new S2DPacketOpenWindow(this.currentWindowId, 11, p_110298_2_.InventoryName, p_110298_2_.SizeInventory, p_110298_2_.InventoryNameLocalized, p_110298_1_.EntityId));
			this.openContainer = new ContainerHorseInventory(this.inventory, p_110298_2_, p_110298_1_);
			this.openContainer.windowId = this.currentWindowId;
			this.openContainer.addCraftingToCrafters(this);
		}

///    
///     <summary> * Sends the contents of an inventory slot to the client-side Container. This doesn't have to match the actual
///     * contents of that slot. Args: Container, slot number, slot contents </summary>
///     
		public virtual void sendSlotContents(Container p_71111_1_, int p_71111_2_, ItemStack p_71111_3_)
		{
			if (!(p_71111_1_.getSlot(p_71111_2_) is SlotCrafting))
			{
				if (!this.isChangingQuantityOnly)
				{
					this.playerNetServerHandler.sendPacket(new S2FPacketSetSlot(p_71111_1_.windowId, p_71111_2_, p_71111_3_));
				}
			}
		}

		public virtual void sendContainerToPlayer(Container p_71120_1_)
		{
			this.sendContainerAndContentsToPlayer(p_71120_1_, p_71120_1_.Inventory);
		}

		public virtual void sendContainerAndContentsToPlayer(Container p_71110_1_, IList p_71110_2_)
		{
			this.playerNetServerHandler.sendPacket(new S30PacketWindowItems(p_71110_1_.windowId, p_71110_2_));
			this.playerNetServerHandler.sendPacket(new S2FPacketSetSlot(-1, -1, this.inventory.ItemStack));
		}

///    
///     <summary> * Sends two ints to the client-side Container. Used for furnace burning time, smelting progress, brewing progress,
///     * and enchanting level. Normally the first int identifies which variable to update, and the second contains the new
///     * value. Both are truncated to shorts in non-local SMP. </summary>
///     
		public virtual void sendProgressBarUpdate(Container p_71112_1_, int p_71112_2_, int p_71112_3_)
		{
			this.playerNetServerHandler.sendPacket(new S31PacketWindowProperty(p_71112_1_.windowId, p_71112_2_, p_71112_3_));
		}

///    
///     <summary> * set current crafting inventory back to the 2x2 square </summary>
///     
		public override void closeScreen()
		{
			this.playerNetServerHandler.sendPacket(new S2EPacketCloseWindow(this.openContainer.windowId));
			this.closeContainer();
		}

///    
///     <summary> * updates item held by mouse </summary>
///     
		public virtual void updateHeldItem()
		{
			if (!this.isChangingQuantityOnly)
			{
				this.playerNetServerHandler.sendPacket(new S2FPacketSetSlot(-1, -1, this.inventory.ItemStack));
			}
		}

///    
///     <summary> * Closes the container the player currently has open. </summary>
///     
		public virtual void closeContainer()
		{
			this.openContainer.onContainerClosed(this);
			this.openContainer = this.inventoryContainer;
		}

		public virtual void setEntityActionState(float p_110430_1_, float p_110430_2_, bool p_110430_3_, bool p_110430_4_)
		{
			if (this.ridingEntity != null)
			{
				if (p_110430_1_ >= -1.0F && p_110430_1_ <= 1.0F)
				{
					this.moveStrafing = p_110430_1_;
				}

				if (p_110430_2_ >= -1.0F && p_110430_2_ <= 1.0F)
				{
					this.moveForward = p_110430_2_;
				}

				this.isJumping = p_110430_3_;
				this.Sneaking = p_110430_4_;
			}
		}

///    
///     <summary> * Adds a value to a statistic field. </summary>
///     
		public override void addStat(StatBase p_71064_1_, int p_71064_2_)
		{
			if (p_71064_1_ != null)
			{
				this.field_147103_bO.func_150871_b(this, p_71064_1_, p_71064_2_);
				IEnumerator var3 = this.WorldScoreboard.func_96520_a(p_71064_1_.func_150952_k()).GetEnumerator();

				while (var3.MoveNext())
				{
					ScoreObjective var4 = (ScoreObjective)var3.Current;
					this.WorldScoreboard.func_96529_a(this.CommandSenderName, var4).func_96648_a();
				}

				if (this.field_147103_bO.func_150879_e())
				{
					this.field_147103_bO.func_150876_a(this);
				}
			}
		}

		public virtual void mountEntityAndWakeUp()
		{
			if (this.riddenByEntity != null)
			{
				this.riddenByEntity.mountEntity(this);
			}

			if (this.sleeping)
			{
				this.wakeUpPlayer(true, false, false);
			}
		}

///    
///     <summary> * this function is called when a players inventory is sent to him, lastHealth is updated on any dimension
///     * transitions, then reset. </summary>
///     
		public virtual void setPlayerHealthUpdated()
		{
			this.lastHealth = -1.0E8F;
		}

		public override void addChatComponentMessage(IChatComponent p_146105_1_)
		{
			this.playerNetServerHandler.sendPacket(new S02PacketChat(p_146105_1_));
		}

///    
///     <summary> * Used for when item use count runs out, ie: eating completed </summary>
///     
		protected internal override void onItemUseFinish()
		{
			this.playerNetServerHandler.sendPacket(new S19PacketEntityStatus(this, (sbyte)9));
			base.onItemUseFinish();
		}

///    
///     <summary> * sets the itemInUse when the use item button is clicked. Args: itemstack, int maxItemUseDuration </summary>
///     
		public override void setItemInUse(ItemStack p_71008_1_, int p_71008_2_)
		{
			base.setItemInUse(p_71008_1_, p_71008_2_);

			if (p_71008_1_ != null && p_71008_1_.Item != null && p_71008_1_.Item.getItemUseAction(p_71008_1_) == EnumAction.eat)
			{
				this.ServerForPlayer.EntityTracker.func_151248_b(this, new S0BPacketAnimation(this, 3));
			}
		}

///    
///     <summary> * Copies the values from the given player into this player if boolean par2 is true. Always clones Ender Chest
///     * Inventory. </summary>
///     
		public override void clonePlayer(EntityPlayer p_71049_1_, bool p_71049_2_)
		{
			base.clonePlayer(p_71049_1_, p_71049_2_);
			this.lastExperience = -1;
			this.lastHealth = -1.0F;
			this.lastFoodLevel = -1;
			this.destroyedItemsNetCache.AddRange(((EntityPlayerMP)p_71049_1_).destroyedItemsNetCache);
		}

		protected internal override void onNewPotionEffect(PotionEffect p_70670_1_)
		{
			base.onNewPotionEffect(p_70670_1_);
			this.playerNetServerHandler.sendPacket(new S1DPacketEntityEffect(this.EntityId, p_70670_1_));
		}

		protected internal override void onChangedPotionEffect(PotionEffect p_70695_1_, bool p_70695_2_)
		{
			base.onChangedPotionEffect(p_70695_1_, p_70695_2_);
			this.playerNetServerHandler.sendPacket(new S1DPacketEntityEffect(this.EntityId, p_70695_1_));
		}

		protected internal override void onFinishedPotionEffect(PotionEffect p_70688_1_)
		{
			base.onFinishedPotionEffect(p_70688_1_);
			this.playerNetServerHandler.sendPacket(new S1EPacketRemoveEntityEffect(this.EntityId, p_70688_1_));
		}

///    
///     <summary> * Sets the position of the entity and updates the 'last' variables </summary>
///     
		public override void setPositionAndUpdate(double p_70634_1_, double p_70634_3_, double p_70634_5_)
		{
			this.playerNetServerHandler.setPlayerLocation(p_70634_1_, p_70634_3_, p_70634_5_, this.rotationYaw, this.rotationPitch);
		}

///    
///     <summary> * Called when the player performs a critical hit on the Entity. Args: entity that was hit critically </summary>
///     
		public override void onCriticalHit(Entity p_71009_1_)
		{
			this.ServerForPlayer.EntityTracker.func_151248_b(this, new S0BPacketAnimation(p_71009_1_, 4));
		}

		public override void onEnchantmentCritical(Entity p_71047_1_)
		{
			this.ServerForPlayer.EntityTracker.func_151248_b(this, new S0BPacketAnimation(p_71047_1_, 5));
		}

///    
///     <summary> * Sends the player's abilities to the server (if there is one). </summary>
///     
		public override void sendPlayerAbilities()
		{
			if (this.playerNetServerHandler != null)
			{
				this.playerNetServerHandler.sendPacket(new S39PacketPlayerAbilities(this.capabilities));
			}
		}

		public virtual WorldServer ServerForPlayer
		{
			get
			{
				return (WorldServer)this.worldObj;
			}
		}

///    
///     <summary> * Sets the player's game mode and sends it to them. </summary>
///     
		public override WorldSettings.GameType GameType
		{
			set
			{
				this.theItemInWorldManager.GameType = value;
				this.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(3, (float)value.ID));
			}
		}

///    
///     <summary> * Notifies this sender of some sort of information.  This is for messages intended to display to the user.  Used
///     * for typical output (like "you asked for whether or not this game rule is set, so here's your answer"), warnings
///     * (like "I fetched this block for you by ID, but I'd like you to know that every time you do this, I die a little
///     * inside"), and errors (like "it's not called iron_pixacke, silly"). </summary>
///     
		public virtual void addChatMessage(IChatComponent p_145747_1_)
		{
			this.playerNetServerHandler.sendPacket(new S02PacketChat(p_145747_1_));
		}

///    
///     <summary> * Returns true if the command sender is allowed to use the given command. </summary>
///     
		public virtual bool canCommandSenderUseCommand(int p_70003_1_, string p_70003_2_)
		{
			if ("seed".Equals(p_70003_2_) && !this.mcServer.DedicatedServer)
			{
				return true;
			}
			else if (!"tell".Equals(p_70003_2_) && !"help".Equals(p_70003_2_) && !"me".Equals(p_70003_2_))
			{
				if (this.mcServer.ConfigurationManager.func_152596_g(this.GameProfile))
				{
					UserListOpsEntry var3 = (UserListOpsEntry)this.mcServer.ConfigurationManager.func_152603_m().func_152683_b(this.GameProfile);
					return var3 != null ? var3.func_152644_a() >= p_70003_1_ : this.mcServer.func_110455_j() >= p_70003_1_;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return true;
			}
		}

///    
///     <summary> * Gets the player's IP address. Used in /banip. </summary>
///     
		public virtual string PlayerIP
		{
			get
			{
				string var1 = this.playerNetServerHandler.netManager.SocketAddress.ToString();
				var1 = var1.Substring(var1.IndexOf("/") + 1);
				var1 = var1.Substring(0, var1.IndexOf(":"));
				return var1;
			}
		}

		public virtual void func_147100_a(C15PacketClientSettings p_147100_1_)
		{
			this.translator = p_147100_1_.func_149524_c();
			int var2 = 256 >> p_147100_1_.func_149521_d();

			if (var2 > 3 && var2 < 20)
			{
				;
			}

			this.chatVisibility = p_147100_1_.func_149523_e();
			this.chatColours = p_147100_1_.func_149520_f();

			if (this.mcServer.SinglePlayer && this.mcServer.ServerOwner.Equals(this.CommandSenderName))
			{
				this.mcServer.func_147139_a(p_147100_1_.func_149518_g());
			}

			this.setHideCape(1, !p_147100_1_.func_149519_h());
		}

		public virtual EntityPlayer.EnumChatVisibility func_147096_v()
		{
			return this.chatVisibility;
		}

		public virtual void func_147095_a(string p_147095_1_)
		{
			this.playerNetServerHandler.sendPacket(new S3FPacketCustomPayload("MC|RPack", p_147095_1_.getBytes(Charsets.UTF_8)));
		}

///    
///     <summary> * Return the position for this command sender. </summary>
///     
		public virtual ChunkCoordinates PlayerCoordinates
		{
			get
			{
				return new ChunkCoordinates(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY + 0.5D), MathHelper.floor_double(this.posZ));
			}
		}

		public virtual void func_143004_u()
		{
			this.field_143005_bX = MinecraftServer.SystemTimeMillis;
		}

		public virtual StatisticsFile func_147099_x()
		{
			return this.field_147103_bO;
		}

		public virtual void func_152339_d(Entity p_152339_1_)
		{
			if (p_152339_1_ is EntityPlayer)
			{
				this.playerNetServerHandler.sendPacket(new S13PacketDestroyEntities(new int[] {p_152339_1_.EntityId}));
			}
			else
			{
				this.destroyedItemsNetCache.Add(Convert.ToInt32(p_152339_1_.EntityId));
			}
		}

		public virtual long func_154331_x()
		{
			return this.field_143005_bX;
		}
	}

}