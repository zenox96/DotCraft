using System;
using System.Collections;

namespace DotCraftCore.nServer.nManagement
{

	using Charsets = com.google.common.base.Charsets;
	using Lists = com.google.common.collect.Lists;
	using Maps = com.google.common.collect.Maps;
	using GameProfile = com.mojang.authlib.GameProfile;
	using Entity = DotCraftCore.entity.Entity;
	using EntityList = DotCraftCore.entity.EntityList;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NetHandlerPlayServer = DotCraftCore.network.NetHandlerPlayServer;
	using NetworkManager = DotCraftCore.network.NetworkManager;
	using Packet = DotCraftCore.network.Packet;
	using S01PacketJoinGame = DotCraftCore.network.play.server.S01PacketJoinGame;
	using S02PacketChat = DotCraftCore.network.play.server.S02PacketChat;
	using S03PacketTimeUpdate = DotCraftCore.network.play.server.S03PacketTimeUpdate;
	using S05PacketSpawnPosition = DotCraftCore.network.play.server.S05PacketSpawnPosition;
	using S07PacketRespawn = DotCraftCore.network.play.server.S07PacketRespawn;
	using S09PacketHeldItemChange = DotCraftCore.network.play.server.S09PacketHeldItemChange;
	using S1DPacketEntityEffect = DotCraftCore.network.play.server.S1DPacketEntityEffect;
	using S1FPacketSetExperience = DotCraftCore.network.play.server.S1FPacketSetExperience;
	using S2BPacketChangeGameState = DotCraftCore.network.play.server.S2BPacketChangeGameState;
	using S38PacketPlayerListItem = DotCraftCore.network.play.server.S38PacketPlayerListItem;
	using S39PacketPlayerAbilities = DotCraftCore.network.play.server.S39PacketPlayerAbilities;
	using S3EPacketTeams = DotCraftCore.network.play.server.S3EPacketTeams;
	using S3FPacketCustomPayload = DotCraftCore.network.play.server.S3FPacketCustomPayload;
	using PotionEffect = DotCraftCore.potion.PotionEffect;
	using Score = DotCraftCore.Scoreboard.Score;
	using ScoreObjective = DotCraftCore.Scoreboard.ScoreObjective;
	using ScorePlayerTeam = DotCraftCore.Scoreboard.ScorePlayerTeam;
	using Scoreboard = DotCraftCore.Scoreboard.Scoreboard;
	using ServerScoreboard = DotCraftCore.Scoreboard.ServerScoreboard;
	using Team = DotCraftCore.Scoreboard.Team;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using StatList = DotCraftCore.Stats.StatList;
	using StatisticsFile = DotCraftCore.Stats.StatisticsFile;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;
	using WorldServer = DotCraftCore.World.WorldServer;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using DemoWorldManager = DotCraftCore.World.Demo.DemoWorldManager;
	using IPlayerFileData = DotCraftCore.World.Storage.IPlayerFileData;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public abstract class ServerConfigurationManager
	{
		public static readonly File field_152613_a = new File("banned-players.json");
		public static readonly File field_152614_b = new File("banned-ips.json");
		public static readonly File field_152615_c = new File("ops.json");
		public static readonly File field_152616_d = new File("whitelist.json");
		private static readonly Logger logger = LogManager.Logger;
		private static readonly SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd \'at\' HH:mm:ss z");

	/// <summary> Reference to the MinecraftServer object.  </summary>
		private readonly MinecraftServer mcServer;

	/// <summary> A list of player entities that exist on this server.  </summary>
		public readonly IList playerEntityList = new ArrayList();
		private readonly UserListBans bannedPlayers;
		private readonly BanList bannedIPs;

	/// <summary> A set containing the OPs.  </summary>
		private readonly UserListOps ops;

	/// <summary> The Set of all whitelisted players.  </summary>
		private readonly UserListWhitelist whiteListedPlayers;
		private readonly IDictionary field_148547_k;

	/// <summary> Reference to the PlayerNBTManager object.  </summary>
		private IPlayerFileData playerNBTManagerObj;

///    
///     <summary> * Server setting to only allow OPs and whitelisted players to join the server. </summary>
///     
		private bool whiteListEnforced;

	/// <summary> The maximum number of players that can be connected at a time.  </summary>
		protected internal int maxPlayers;
		private int viewDistance;
		private WorldSettings.GameType gameType;

	/// <summary> True if all players are allowed to use commands (cheats).  </summary>
		private bool commandsAllowedForAll;

///    
///     <summary> * index into playerEntities of player to ping, updated every tick; currently hardcoded to max at 200 players </summary>
///     
		private int playerPingIndex;
		

		public ServerConfigurationManager(MinecraftServer p_i1500_1_)
		{
			this.bannedPlayers = new UserListBans(field_152613_a);
			this.bannedIPs = new BanList(field_152614_b);
			this.ops = new UserListOps(field_152615_c);
			this.whiteListedPlayers = new UserListWhitelist(field_152616_d);
			this.field_148547_k = Maps.newHashMap();
			this.mcServer = p_i1500_1_;
			this.bannedPlayers.func_152686_a(false);
			this.bannedIPs.func_152686_a(false);
			this.maxPlayers = 8;
		}

		public virtual void initializeConnectionToPlayer(NetworkManager p_72355_1_, EntityPlayerMP p_72355_2_)
		{
			GameProfile var3 = p_72355_2_.GameProfile;
			PlayerProfileCache var4 = this.mcServer.func_152358_ax();
			GameProfile var5 = var4.func_152652_a(var3.Id);
			string var6 = var5 == null ? var3.Name : var5.Name;
			var4.func_152649_a(var3);
			NBTTagCompound var7 = this.readPlayerDataFromFile(p_72355_2_);
			p_72355_2_.World = this.mcServer.worldServerForDimension(p_72355_2_.dimension);
			p_72355_2_.theItemInWorldManager.World = (WorldServer)p_72355_2_.worldObj;
			string var8 = "local";

			if(p_72355_1_.SocketAddress != null)
			{
				var8 = p_72355_1_.SocketAddress.ToString();
			}

			logger.info(p_72355_2_.CommandSenderName + "[" + var8 + "] logged in with entity id " + p_72355_2_.EntityId + " at (" + p_72355_2_.posX + ", " + p_72355_2_.posY + ", " + p_72355_2_.posZ + ")");
			WorldServer var9 = this.mcServer.worldServerForDimension(p_72355_2_.dimension);
			ChunkCoordinates var10 = var9.SpawnPoint;
			this.func_72381_a(p_72355_2_, (EntityPlayerMP)null, var9);
			NetHandlerPlayServer var11 = new NetHandlerPlayServer(this.mcServer, p_72355_1_, p_72355_2_);
			var11.sendPacket(new S01PacketJoinGame(p_72355_2_.EntityId, p_72355_2_.theItemInWorldManager.GameType, var9.WorldInfo.HardcoreModeEnabled, var9.provider.dimensionId, var9.difficultySetting, this.MaxPlayers, var9.WorldInfo.TerrainType));
			var11.sendPacket(new S3FPacketCustomPayload("MC|Brand", this.ServerInstance.ServerModName.getBytes(Charsets.UTF_8)));
			var11.sendPacket(new S05PacketSpawnPosition(var10.posX, var10.posY, var10.posZ));
			var11.sendPacket(new S39PacketPlayerAbilities(p_72355_2_.capabilities));
			var11.sendPacket(new S09PacketHeldItemChange(p_72355_2_.inventory.currentItem));
			p_72355_2_.func_147099_x().func_150877_d();
			p_72355_2_.func_147099_x().func_150884_b(p_72355_2_);
			this.func_96456_a((ServerScoreboard)var9.Scoreboard, p_72355_2_);
			this.mcServer.func_147132_au();
			ChatComponentTranslation var12;

			if(!p_72355_2_.CommandSenderName.equalsIgnoreCase(var6))
			{
				var12 = new ChatComponentTranslation("multiplayer.player.joined.renamed", new object[] {p_72355_2_.func_145748_c_(), var6});
			}
			else
			{
				var12 = new ChatComponentTranslation("multiplayer.player.joined", new object[] {p_72355_2_.func_145748_c_()});
			}

			var12.ChatStyle.Color = EnumChatFormatting.YELLOW;
			this.func_148539_a(var12);
			this.playerLoggedIn(p_72355_2_);
			var11.setPlayerLocation(p_72355_2_.posX, p_72355_2_.posY, p_72355_2_.posZ, p_72355_2_.rotationYaw, p_72355_2_.rotationPitch);
			this.updateTimeAndWeatherForPlayer(p_72355_2_, var9);

			if(this.mcServer.func_147133_T().Length > 0)
			{
				p_72355_2_.func_147095_a(this.mcServer.func_147133_T());
			}

			IEnumerator var13 = p_72355_2_.ActivePotionEffects.GetEnumerator();

			while(var13.MoveNext())
			{
				PotionEffect var14 = (PotionEffect)var13.Current;
				var11.sendPacket(new S1DPacketEntityEffect(p_72355_2_.EntityId, var14));
			}

			p_72355_2_.addSelfToInternalCraftingInventory();

			if(var7 != null && var7.func_150297_b("Riding", 10))
			{
				Entity var15 = EntityList.createEntityFromNBT(var7.getCompoundTag("Riding"), var9);

				if(var15 != null)
				{
					var15.forceSpawn = true;
					var9.spawnEntityInWorld(var15);
					p_72355_2_.mountEntity(var15);
					var15.forceSpawn = false;
				}
			}
		}

		protected internal virtual void func_96456_a(ServerScoreboard p_96456_1_, EntityPlayerMP p_96456_2_)
		{
			HashSet var3 = new HashSet();
			IEnumerator var4 = p_96456_1_.Teams.GetEnumerator();

			while(var4.MoveNext())
			{
				ScorePlayerTeam var5 = (ScorePlayerTeam)var4.Current;
				p_96456_2_.playerNetServerHandler.sendPacket(new S3EPacketTeams(var5, 0));
			}

			for(int var9 = 0; var9 < 3; ++var9)
			{
				ScoreObjective var10 = p_96456_1_.func_96539_a(var9);

				if(var10 != null && !var3.Contains(var10))
				{
					IList var6 = p_96456_1_.func_96550_d(var10);
					IEnumerator var7 = var6.GetEnumerator();

					while(var7.MoveNext())
					{
						Packet var8 = (Packet)var7.Current;
						p_96456_2_.playerNetServerHandler.sendPacket(var8);
					}

					var3.Add(var10);
				}
			}
		}

///    
///     <summary> * Sets the NBT manager to the one for the WorldServer given. </summary>
///     
		public virtual WorldServer[] PlayerManager
		{
			set
			{
				this.playerNBTManagerObj = value[0].SaveHandler.SaveHandler;
			}
		}

		public virtual void func_72375_a(EntityPlayerMP p_72375_1_, WorldServer p_72375_2_)
		{
			WorldServer var3 = p_72375_1_.ServerForPlayer;

			if(p_72375_2_ != null)
			{
				p_72375_2_.PlayerManager.removePlayer(p_72375_1_);
			}

			var3.PlayerManager.addPlayer(p_72375_1_);
			var3.theChunkProviderServer.loadChunk((int)p_72375_1_.posX >> 4, (int)p_72375_1_.posZ >> 4);
		}

		public virtual int EntityViewDistance
		{
			get
			{
				return PlayerManager.getFurthestViewableBlock(this.ViewDistance);
			}
		}

///    
///     <summary> * called during player login. reads the player information from disk. </summary>
///     
		public virtual NBTTagCompound readPlayerDataFromFile(EntityPlayerMP p_72380_1_)
		{
			NBTTagCompound var2 = this.mcServer.worldServers[0].WorldInfo.PlayerNBTTagCompound;
			NBTTagCompound var3;

			if(p_72380_1_.CommandSenderName.Equals(this.mcServer.ServerOwner) && var2 != null)
			{
				p_72380_1_.readFromNBT(var2);
				var3 = var2;
				logger.debug("loading single player");
			}
			else
			{
				var3 = this.playerNBTManagerObj.readPlayerData(p_72380_1_);
			}

			return var3;
		}

///    
///     <summary> * also stores the NBTTags if this is an intergratedPlayerList </summary>
///     
		protected internal virtual void writePlayerData(EntityPlayerMP p_72391_1_)
		{
			this.playerNBTManagerObj.writePlayerData(p_72391_1_);
			StatisticsFile var2 = (StatisticsFile)this.field_148547_k.get(p_72391_1_.UniqueID);

			if(var2 != null)
			{
				var2.func_150883_b();
			}
		}

///    
///     <summary> * Called when a player successfully logs in. Reads player data from disk and inserts the player into the world. </summary>
///     
		public virtual void playerLoggedIn(EntityPlayerMP p_72377_1_)
		{
			this.func_148540_a(new S38PacketPlayerListItem(p_72377_1_.CommandSenderName, true, 1000));
			this.playerEntityList.Add(p_72377_1_);
			WorldServer var2 = this.mcServer.worldServerForDimension(p_72377_1_.dimension);
			var2.spawnEntityInWorld(p_72377_1_);
			this.func_72375_a(p_72377_1_, (WorldServer)null);

			for(int var3 = 0; var3 < this.playerEntityList.Count; ++var3)
			{
				EntityPlayerMP var4 = (EntityPlayerMP)this.playerEntityList.get(var3);
				p_72377_1_.playerNetServerHandler.sendPacket(new S38PacketPlayerListItem(var4.CommandSenderName, true, var4.ping));
			}
		}

///    
///     <summary> * using player's dimension, update their movement when in a vehicle (e.g. cart, boat) </summary>
///     
		public virtual void serverUpdateMountedMovingPlayer(EntityPlayerMP p_72358_1_)
		{
			p_72358_1_.ServerForPlayer.PlayerManager.updateMountedMovingPlayer(p_72358_1_);
		}

///    
///     <summary> * Called when a player disconnects from the game. Writes player data to disk and removes them from the world. </summary>
///     
		public virtual void playerLoggedOut(EntityPlayerMP p_72367_1_)
		{
			p_72367_1_.triggerAchievement(StatList.leaveGameStat);
			this.writePlayerData(p_72367_1_);
			WorldServer var2 = p_72367_1_.ServerForPlayer;

			if(p_72367_1_.ridingEntity != null)
			{
				var2.removePlayerEntityDangerously(p_72367_1_.ridingEntity);
				logger.debug("removing player mount");
			}

			var2.removeEntity(p_72367_1_);
			var2.PlayerManager.removePlayer(p_72367_1_);
			this.playerEntityList.Remove(p_72367_1_);
			this.field_148547_k.Remove(p_72367_1_.UniqueID);
			this.func_148540_a(new S38PacketPlayerListItem(p_72367_1_.CommandSenderName, false, 9999));
		}

		public virtual string func_148542_a(SocketAddress p_148542_1_, GameProfile p_148542_2_)
		{
			string var4;

			if(this.bannedPlayers.func_152702_a(p_148542_2_))
			{
				UserListBansEntry var5 = (UserListBansEntry)this.bannedPlayers.func_152683_b(p_148542_2_);
				var4 = "You are banned from this server!\nReason: " + var5.BanReason;

				if(var5.BanEndDate != null)
				{
					var4 = var4 + "\nYour ban will be removed on " + dateFormat.format(var5.BanEndDate);
				}

				return var4;
			}
			else if(!this.func_152607_e(p_148542_2_))
			{
				return "You are not white-listed on this server!";
			}
			else if(this.bannedIPs.func_152708_a(p_148542_1_))
			{
				IPBanEntry var3 = this.bannedIPs.func_152709_b(p_148542_1_);
				var4 = "Your IP address is banned from this server!\nReason: " + var3.BanReason;

				if(var3.BanEndDate != null)
				{
					var4 = var4 + "\nYour ban will be removed on " + dateFormat.format(var3.BanEndDate);
				}

				return var4;
			}
			else
			{
				return this.playerEntityList.Count >= this.maxPlayers ? "The server is full!" : null;
			}
		}

		public virtual EntityPlayerMP func_148545_a(GameProfile p_148545_1_)
		{
			UUID var2 = EntityPlayer.func_146094_a(p_148545_1_);
			ArrayList var3 = Lists.newArrayList();
			EntityPlayerMP var5;

			for(int var4 = 0; var4 < this.playerEntityList.Count; ++var4)
			{
				var5 = (EntityPlayerMP)this.playerEntityList.get(var4);

				if(var5.UniqueID.Equals(var2))
				{
					var3.Add(var5);
				}
			}

			IEnumerator var6 = var3.GetEnumerator();

			while(var6.MoveNext())
			{
				var5 = (EntityPlayerMP)var6.Current;
				var5.playerNetServerHandler.kickPlayerFromServer("You logged in from another location");
			}

			object var7;

			if(this.mcServer.Demo)
			{
				var7 = new DemoWorldManager(this.mcServer.worldServerForDimension(0));
			}
			else
			{
				var7 = new ItemInWorldManager(this.mcServer.worldServerForDimension(0));
			}

			return new EntityPlayerMP(this.mcServer, this.mcServer.worldServerForDimension(0), p_148545_1_, (ItemInWorldManager)var7);
		}

///    
///     <summary> * creates and returns a respawned player based on the provided PlayerEntity. Args are the PlayerEntityMP to
///     * respawn, an INT for the dimension to respawn into (usually 0), and a boolean value that is true if the player
///     * beat the game rather than dying </summary>
///     
		public virtual EntityPlayerMP respawnPlayer(EntityPlayerMP p_72368_1_, int p_72368_2_, bool p_72368_3_)
		{
			p_72368_1_.ServerForPlayer.EntityTracker.removePlayerFromTrackers(p_72368_1_);
			p_72368_1_.ServerForPlayer.EntityTracker.removeEntityFromAllTrackingPlayers(p_72368_1_);
			p_72368_1_.ServerForPlayer.PlayerManager.removePlayer(p_72368_1_);
			this.playerEntityList.Remove(p_72368_1_);
			this.mcServer.worldServerForDimension(p_72368_1_.dimension).removePlayerEntityDangerously(p_72368_1_);
			ChunkCoordinates var4 = p_72368_1_.BedLocation;
			bool var5 = p_72368_1_.SpawnForced;
			p_72368_1_.dimension = p_72368_2_;
			object var6;

			if(this.mcServer.Demo)
			{
				var6 = new DemoWorldManager(this.mcServer.worldServerForDimension(p_72368_1_.dimension));
			}
			else
			{
				var6 = new ItemInWorldManager(this.mcServer.worldServerForDimension(p_72368_1_.dimension));
			}

			EntityPlayerMP var7 = new EntityPlayerMP(this.mcServer, this.mcServer.worldServerForDimension(p_72368_1_.dimension), p_72368_1_.GameProfile, (ItemInWorldManager)var6);
			var7.playerNetServerHandler = p_72368_1_.playerNetServerHandler;
			var7.clonePlayer(p_72368_1_, p_72368_3_);
			var7.EntityId = p_72368_1_.EntityId;
			WorldServer var8 = this.mcServer.worldServerForDimension(p_72368_1_.dimension);
			this.func_72381_a(var7, p_72368_1_, var8);
			ChunkCoordinates var9;

			if(var4 != null)
			{
				var9 = EntityPlayer.verifyRespawnCoordinates(this.mcServer.worldServerForDimension(p_72368_1_.dimension), var4, var5);

				if(var9 != null)
				{
					var7.setLocationAndAngles((double)((float)var9.posX + 0.5F), (double)((float)var9.posY + 0.1F), (double)((float)var9.posZ + 0.5F), 0.0F, 0.0F);
					var7.setSpawnChunk(var4, var5);
				}
				else
				{
					var7.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(0, 0.0F));
				}
			}

			var8.theChunkProviderServer.loadChunk((int)var7.posX >> 4, (int)var7.posZ >> 4);

			while(!var8.getCollidingBoundingBoxes(var7, var7.boundingBox).Empty)
			{
				var7.setPosition(var7.posX, var7.posY + 1.0D, var7.posZ);
			}

			var7.playerNetServerHandler.sendPacket(new S07PacketRespawn(var7.dimension, var7.worldObj.difficultySetting, var7.worldObj.WorldInfo.TerrainType, var7.theItemInWorldManager.GameType));
			var9 = var8.SpawnPoint;
			var7.playerNetServerHandler.setPlayerLocation(var7.posX, var7.posY, var7.posZ, var7.rotationYaw, var7.rotationPitch);
			var7.playerNetServerHandler.sendPacket(new S05PacketSpawnPosition(var9.posX, var9.posY, var9.posZ));
			var7.playerNetServerHandler.sendPacket(new S1FPacketSetExperience(var7.experience, var7.experienceTotal, var7.experienceLevel));
			this.updateTimeAndWeatherForPlayer(var7, var8);
			var8.PlayerManager.addPlayer(var7);
			var8.spawnEntityInWorld(var7);
			this.playerEntityList.Add(var7);
			var7.addSelfToInternalCraftingInventory();
			var7.Health = var7.Health;
			return var7;
		}

		public virtual void transferPlayerToDimension(EntityPlayerMP p_72356_1_, int p_72356_2_)
		{
			int var3 = p_72356_1_.dimension;
			WorldServer var4 = this.mcServer.worldServerForDimension(p_72356_1_.dimension);
			p_72356_1_.dimension = p_72356_2_;
			WorldServer var5 = this.mcServer.worldServerForDimension(p_72356_1_.dimension);
			p_72356_1_.playerNetServerHandler.sendPacket(new S07PacketRespawn(p_72356_1_.dimension, p_72356_1_.worldObj.difficultySetting, p_72356_1_.worldObj.WorldInfo.TerrainType, p_72356_1_.theItemInWorldManager.GameType));
			var4.removePlayerEntityDangerously(p_72356_1_);
			p_72356_1_.isDead = false;
			this.transferEntityToWorld(p_72356_1_, var3, var4, var5);
			this.func_72375_a(p_72356_1_, var4);
			p_72356_1_.playerNetServerHandler.setPlayerLocation(p_72356_1_.posX, p_72356_1_.posY, p_72356_1_.posZ, p_72356_1_.rotationYaw, p_72356_1_.rotationPitch);
			p_72356_1_.theItemInWorldManager.World = var5;
			this.updateTimeAndWeatherForPlayer(p_72356_1_, var5);
			this.syncPlayerInventory(p_72356_1_);
			IEnumerator var6 = p_72356_1_.ActivePotionEffects.GetEnumerator();

			while(var6.MoveNext())
			{
				PotionEffect var7 = (PotionEffect)var6.Current;
				p_72356_1_.playerNetServerHandler.sendPacket(new S1DPacketEntityEffect(p_72356_1_.EntityId, var7));
			}
		}

///    
///     <summary> * Transfers an entity from a world to another world. </summary>
///     
		public virtual void transferEntityToWorld(nEntity p_82448_1_, int p_82448_2_, WorldServer p_82448_3_, WorldServer p_82448_4_)
		{
			double var5 = p_82448_1_.posX;
			double var7 = p_82448_1_.posZ;
			double var9 = 8.0D;
			double var11 = p_82448_1_.posX;
			double var13 = p_82448_1_.posY;
			double var15 = p_82448_1_.posZ;
			float var17 = p_82448_1_.rotationYaw;
			p_82448_3_.theProfiler.startSection("moving");

			if(p_82448_1_.dimension == -1)
			{
				var5 /= var9;
				var7 /= var9;
				p_82448_1_.setLocationAndAngles(var5, p_82448_1_.posY, var7, p_82448_1_.rotationYaw, p_82448_1_.rotationPitch);

				if(p_82448_1_.EntityAlive)
				{
					p_82448_3_.updateEntityWithOptionalForce(p_82448_1_, false);
				}
			}
			else if(p_82448_1_.dimension == 0)
			{
				var5 *= var9;
				var7 *= var9;
				p_82448_1_.setLocationAndAngles(var5, p_82448_1_.posY, var7, p_82448_1_.rotationYaw, p_82448_1_.rotationPitch);

				if(p_82448_1_.EntityAlive)
				{
					p_82448_3_.updateEntityWithOptionalForce(p_82448_1_, false);
				}
			}
			else
			{
				ChunkCoordinates var18;

				if(p_82448_2_ == 1)
				{
					var18 = p_82448_4_.SpawnPoint;
				}
				else
				{
					var18 = p_82448_4_.EntrancePortalLocation;
				}

				var5 = (double)var18.posX;
				p_82448_1_.posY = (double)var18.posY;
				var7 = (double)var18.posZ;
				p_82448_1_.setLocationAndAngles(var5, p_82448_1_.posY, var7, 90.0F, 0.0F);

				if(p_82448_1_.EntityAlive)
				{
					p_82448_3_.updateEntityWithOptionalForce(p_82448_1_, false);
				}
			}

			p_82448_3_.theProfiler.endSection();

			if(p_82448_2_ != 1)
			{
				p_82448_3_.theProfiler.startSection("placing");
				var5 = (double)MathHelper.clamp_int((int)var5, -29999872, 29999872);
				var7 = (double)MathHelper.clamp_int((int)var7, -29999872, 29999872);

				if(p_82448_1_.EntityAlive)
				{
					p_82448_1_.setLocationAndAngles(var5, p_82448_1_.posY, var7, p_82448_1_.rotationYaw, p_82448_1_.rotationPitch);
					p_82448_4_.DefaultTeleporter.placeInPortal(p_82448_1_, var11, var13, var15, var17);
					p_82448_4_.spawnEntityInWorld(p_82448_1_);
					p_82448_4_.updateEntityWithOptionalForce(p_82448_1_, false);
				}

				p_82448_3_.theProfiler.endSection();
			}

			p_82448_1_.World = p_82448_4_;
		}

///    
///     <summary> * sends 1 player per tick, but only sends a player once every 600 ticks </summary>
///     
		public virtual void sendPlayerInfoToAllPlayers()
		{
			if(++this.playerPingIndex > 600)
			{
				this.playerPingIndex = 0;
			}

			if(this.playerPingIndex < this.playerEntityList.Count)
			{
				EntityPlayerMP var1 = (EntityPlayerMP)this.playerEntityList.get(this.playerPingIndex);
				this.func_148540_a(new S38PacketPlayerListItem(var1.CommandSenderName, true, var1.ping));
			}
		}

		public virtual void func_148540_a(Packet p_148540_1_)
		{
			for(int var2 = 0; var2 < this.playerEntityList.Count; ++var2)
			{
				((EntityPlayerMP)this.playerEntityList.get(var2)).playerNetServerHandler.sendPacket(p_148540_1_);
			}
		}

		public virtual void func_148537_a(Packet p_148537_1_, int p_148537_2_)
		{
			for(int var3 = 0; var3 < this.playerEntityList.Count; ++var3)
			{
				EntityPlayerMP var4 = (EntityPlayerMP)this.playerEntityList.get(var3);

				if(var4.dimension == p_148537_2_)
				{
					var4.playerNetServerHandler.sendPacket(p_148537_1_);
				}
			}
		}

		public virtual string func_152609_b(bool p_152609_1_)
		{
			string var2 = "";
			ArrayList var3 = Lists.newArrayList(this.playerEntityList);

			for(int var4 = 0; var4 < var3.Count; ++var4)
			{
				if(var4 > 0)
				{
					var2 = var2 + ", ";
				}

				var2 = var2 + ((EntityPlayerMP)var3[var4]).CommandSenderName;

				if(p_152609_1_)
				{
					var2 = var2 + " (" + ((EntityPlayerMP)var3[var4]).UniqueID.ToString() + ")";
				}
			}

			return var2;
		}

///    
///     <summary> * Returns an array of the usernames of all the connected players. </summary>
///     
		public virtual string[] AllUsernames
		{
			get
			{
				string[] var1 = new string[this.playerEntityList.Count];
	
				for(int var2 = 0; var2 < this.playerEntityList.Count; ++var2)
				{
					var1[var2] = ((EntityPlayerMP)this.playerEntityList.get(var2)).CommandSenderName;
				}
	
				return var1;
			}
		}

		public virtual GameProfile[] func_152600_g()
		{
			GameProfile[] var1 = new GameProfile[this.playerEntityList.Count];

			for(int var2 = 0; var2 < this.playerEntityList.Count; ++var2)
			{
				var1[var2] = ((EntityPlayerMP)this.playerEntityList.get(var2)).GameProfile;
			}

			return var1;
		}

		public virtual UserListBans func_152608_h()
		{
			return this.bannedPlayers;
		}

		public virtual BanList BannedIPs
		{
			get
			{
				return this.bannedIPs;
			}
		}

		public virtual void func_152605_a(GameProfile p_152605_1_)
		{
			this.ops.func_152687_a(new UserListOpsEntry(p_152605_1_, this.mcServer.func_110455_j()));
		}

		public virtual void func_152610_b(GameProfile p_152610_1_)
		{
			this.ops.func_152684_c(p_152610_1_);
		}

		public virtual bool func_152607_e(GameProfile p_152607_1_)
		{
			return !this.whiteListEnforced || this.ops.func_152692_d(p_152607_1_) || this.whiteListedPlayers.func_152692_d(p_152607_1_);
		}

		public virtual bool func_152596_g(GameProfile p_152596_1_)
		{
			return this.ops.func_152692_d(p_152596_1_) || this.mcServer.SinglePlayer && this.mcServer.worldServers[0].WorldInfo.areCommandsAllowed() && this.mcServer.ServerOwner.equalsIgnoreCase(p_152596_1_.Name) || this.commandsAllowedForAll;
		}

		public virtual EntityPlayerMP func_152612_a(string p_152612_1_)
		{
			IEnumerator var2 = this.playerEntityList.GetEnumerator();
			EntityPlayerMP var3;

			do
			{
				if(!var2.MoveNext())
				{
					return null;
				}

				var3 = (EntityPlayerMP)var2.Current;
			}
			while(!var3.CommandSenderName.equalsIgnoreCase(p_152612_1_));

			return var3;
		}

///    
///     <summary> * Find all players in a specified range and narrowing down by other parameters </summary>
///     
		public virtual IList findPlayers(ChunkCoordinates p_82449_1_, int p_82449_2_, int p_82449_3_, int p_82449_4_, int p_82449_5_, int p_82449_6_, int p_82449_7_, IDictionary p_82449_8_, string p_82449_9_, string p_82449_10_, nWorld p_82449_11_)
		{
			if(this.playerEntityList.Count == 0)
			{
				return Collections.emptyList();
			}
			else
			{
				object var12 = new ArrayList();
				bool var13 = p_82449_4_ < 0;
				bool var14 = p_82449_9_ != null && p_82449_9_.StartsWith("!");
				bool var15 = p_82449_10_ != null && p_82449_10_.StartsWith("!");
				int var16 = p_82449_2_ * p_82449_2_;
				int var17 = p_82449_3_ * p_82449_3_;
				p_82449_4_ = MathHelper.abs_int(p_82449_4_);

				if(var14)
				{
					p_82449_9_ = p_82449_9_.Substring(1);
				}

				if(var15)
				{
					p_82449_10_ = p_82449_10_.Substring(1);
				}

				for(int var18 = 0; var18 < this.playerEntityList.Count; ++var18)
				{
					EntityPlayerMP var19 = (EntityPlayerMP)this.playerEntityList.get(var18);

					if((p_82449_11_ == null || var19.worldObj == p_82449_11_) && (p_82449_9_ == null || var14 != p_82449_9_.equalsIgnoreCase(var19.CommandSenderName)))
					{
						if(p_82449_10_ != null)
						{
							Team var20 = var19.Team;
							string var21 = var20 == null ? "" : var20.RegisteredName;

							if(var15 == p_82449_10_.equalsIgnoreCase(var21))
							{
								continue;
							}
						}

						if(p_82449_1_ != null && (p_82449_2_ > 0 || p_82449_3_ > 0))
						{
							float var22 = p_82449_1_.getDistanceSquaredToChunkCoordinates(var19.PlayerCoordinates);

							if(p_82449_2_ > 0 && var22 < (float)var16 || p_82449_3_ > 0 && var22 > (float)var17)
							{
								continue;
							}
						}

						if(this.func_96457_a(var19, p_82449_8_) && (p_82449_5_ == WorldSettings.GameType.NOT_SET.ID || p_82449_5_ == var19.theItemInWorldManager.GameType.ID) && (p_82449_6_ <= 0 || var19.experienceLevel >= p_82449_6_) && var19.experienceLevel <= p_82449_7_)
						{
							((IList)var12).add(var19);
						}
					}
				}

				if(p_82449_1_ != null)
				{
					Collections.sort((IList)var12, new PlayerPositionComparator(p_82449_1_));
				}

				if(var13)
				{
					Collections.reverse((IList)var12);
				}

				if(p_82449_4_ > 0)
				{
					var12 = ((IList)var12).subList(0, Math.Min(p_82449_4_, ((IList)var12).size()));
				}

				return(IList)var12;
			}
		}

		private bool func_96457_a(EntityPlayer p_96457_1_, IDictionary p_96457_2_)
		{
			if(p_96457_2_ != null && p_96457_2_.Count != 0)
			{
				IEnumerator var3 = p_96457_2_.GetEnumerator();
				Entry var4;
				bool var6;
				int var10;

				do
				{
					if(!var3.MoveNext())
					{
						return true;
					}

					var4 = (Entry)var3.Current;
					string var5 = (string)var4.Key;
					var6 = false;

					if(var5.EndsWith("_min") && var5.Length > 4)
					{
						var6 = true;
						var5 = var5.Substring(0, var5.Length - 4);
					}

					Scoreboard var7 = p_96457_1_.WorldScoreboard;
					ScoreObjective var8 = var7.getObjective(var5);

					if(var8 == null)
					{
						return false;
					}

					Score var9 = p_96457_1_.WorldScoreboard.func_96529_a(p_96457_1_.CommandSenderName, var8);
					var10 = var9.ScorePoints;

					if(var10 < (int)((int?)var4.Value) && var6)
					{
						return false;
					}
				}
				while(var10 <= (int)((int?)var4.Value) || var6);

				return false;
			}
			else
			{
				return true;
			}
		}

		public virtual void func_148541_a(double p_148541_1_, double p_148541_3_, double p_148541_5_, double p_148541_7_, int p_148541_9_, Packet p_148541_10_)
		{
			this.func_148543_a((EntityPlayer)null, p_148541_1_, p_148541_3_, p_148541_5_, p_148541_7_, p_148541_9_, p_148541_10_);
		}

		public virtual void func_148543_a(EntityPlayer p_148543_1_, double p_148543_2_, double p_148543_4_, double p_148543_6_, double p_148543_8_, int p_148543_10_, Packet p_148543_11_)
		{
			for(int var12 = 0; var12 < this.playerEntityList.Count; ++var12)
			{
				EntityPlayerMP var13 = (EntityPlayerMP)this.playerEntityList.get(var12);

				if(var13 != p_148543_1_ && var13.dimension == p_148543_10_)
				{
					double var14 = p_148543_2_ - var13.posX;
					double var16 = p_148543_4_ - var13.posY;
					double var18 = p_148543_6_ - var13.posZ;

					if(var14 * var14 + var16 * var16 + var18 * var18 < p_148543_8_ * p_148543_8_)
					{
						var13.playerNetServerHandler.sendPacket(p_148543_11_);
					}
				}
			}
		}

///    
///     <summary> * Saves all of the players' current states. </summary>
///     
		public virtual void saveAllPlayerData()
		{
			for(int var1 = 0; var1 < this.playerEntityList.Count; ++var1)
			{
				this.writePlayerData((EntityPlayerMP)this.playerEntityList.get(var1));
			}
		}

		public virtual void func_152601_d(GameProfile p_152601_1_)
		{
			this.whiteListedPlayers.func_152687_a(new UserListWhitelistEntry(p_152601_1_));
		}

		public virtual void func_152597_c(GameProfile p_152597_1_)
		{
			this.whiteListedPlayers.func_152684_c(p_152597_1_);
		}

		public virtual UserListWhitelist func_152599_k()
		{
			return this.whiteListedPlayers;
		}

		public virtual string[] func_152598_l()
		{
			return this.whiteListedPlayers.func_152685_a();
		}

		public virtual UserListOps func_152603_m()
		{
			return this.ops;
		}

		public virtual string[] func_152606_n()
		{
			return this.ops.func_152685_a();
		}

///    
///     <summary> * Either does nothing, or calls readWhiteList. </summary>
///     
		public virtual void loadWhiteList()
		{
		}

///    
///     <summary> * Updates the time and weather for the given player to those of the given world </summary>
///     
		public virtual void updateTimeAndWeatherForPlayer(EntityPlayerMP p_72354_1_, WorldServer p_72354_2_)
		{
			p_72354_1_.playerNetServerHandler.sendPacket(new S03PacketTimeUpdate(p_72354_2_.TotalWorldTime, p_72354_2_.WorldTime, p_72354_2_.GameRules.getGameRuleBooleanValue("doDaylightCycle")));

			if(p_72354_2_.Raining)
			{
				p_72354_1_.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(1, 0.0F));
				p_72354_1_.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(7, p_72354_2_.getRainStrength(1.0F)));
				p_72354_1_.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(8, p_72354_2_.getWeightedThunderStrength(1.0F)));
			}
		}

///    
///     <summary> * sends the players inventory to himself </summary>
///     
		public virtual void syncPlayerInventory(EntityPlayerMP p_72385_1_)
		{
			p_72385_1_.sendContainerToPlayer(p_72385_1_.inventoryContainer);
			p_72385_1_.setPlayerHealthUpdated();
			p_72385_1_.playerNetServerHandler.sendPacket(new S09PacketHeldItemChange(p_72385_1_.inventory.currentItem));
		}

///    
///     <summary> * Returns the number of players currently on the server. </summary>
///     
		public virtual int CurrentPlayerCount
		{
			get
			{
				return this.playerEntityList.Count;
			}
		}

///    
///     <summary> * Returns the maximum number of players allowed on the server. </summary>
///     
		public virtual int MaxPlayers
		{
			get
			{
				return this.maxPlayers;
			}
		}

///    
///     <summary> * Returns an array of usernames for which player.dat exists for. </summary>
///     
		public virtual string[] AvailablePlayerDat
		{
			get
			{
				return this.mcServer.worldServers[0].SaveHandler.SaveHandler.AvailablePlayerDat;
			}
		}

		public virtual bool WhiteListEnabled
		{
			set
			{
				this.whiteListEnforced = value;
			}
		}

		public virtual IList getPlayerList(string p_72382_1_)
		{
			ArrayList var2 = new ArrayList();
			IEnumerator var3 = this.playerEntityList.GetEnumerator();

			while(var3.MoveNext())
			{
				EntityPlayerMP var4 = (EntityPlayerMP)var3.Current;

				if(var4.PlayerIP.Equals(p_72382_1_))
				{
					var2.Add(var4);
				}
			}

			return var2;
		}

///    
///     <summary> * Gets the View Distance. </summary>
///     
		public virtual int ViewDistance
		{
			get
			{
				return this.viewDistance;
			}
		}

		public virtual MinecraftServer ServerInstance
		{
			get
			{
				return this.mcServer;
			}
		}

///    
///     <summary> * On integrated servers, returns the host's player data to be written to level.dat. </summary>
///     
		public virtual NBTTagCompound HostPlayerData
		{
			get
			{
				return null;
			}
		}

		public virtual void func_152604_a(WorldSettings.GameType p_152604_1_)
		{
			this.gameType = p_152604_1_;
		}

		private void func_72381_a(EntityPlayerMP p_72381_1_, EntityPlayerMP p_72381_2_, nWorld p_72381_3_)
		{
			if(p_72381_2_ != null)
			{
				p_72381_1_.theItemInWorldManager.GameType = p_72381_2_.theItemInWorldManager.GameType;
			}
			else if(this.gameType != null)
			{
				p_72381_1_.theItemInWorldManager.GameType = this.gameType;
			}

			p_72381_1_.theItemInWorldManager.initializeGameType(p_72381_3_.WorldInfo.GameType);
		}

///    
///     <summary> * Sets whether all players are allowed to use commands (cheats) on the server. </summary>
///     
		public virtual bool CommandsAllowedForAll
		{
			set
			{
				this.commandsAllowedForAll = value;
			}
		}

///    
///     <summary> * Kicks everyone with "Server closed" as reason. </summary>
///     
		public virtual void removeAllPlayers()
		{
			for(int var1 = 0; var1 < this.playerEntityList.Count; ++var1)
			{
				((EntityPlayerMP)this.playerEntityList.get(var1)).playerNetServerHandler.kickPlayerFromServer("Server closed");
			}
		}

		public virtual void func_148544_a(IChatComponent p_148544_1_, bool p_148544_2_)
		{
			this.mcServer.addChatMessage(p_148544_1_);
			this.func_148540_a(new S02PacketChat(p_148544_1_, p_148544_2_));
		}

		public virtual void func_148539_a(IChatComponent p_148539_1_)
		{
			this.func_148544_a(p_148539_1_, true);
		}

		public virtual StatisticsFile func_152602_a(EntityPlayer p_152602_1_)
		{
			UUID var2 = p_152602_1_.UniqueID;
			StatisticsFile var3 = var2 == null ? null : (StatisticsFile)this.field_148547_k.get(var2);

			if(var3 == null)
			{
				File var4 = new File(this.mcServer.worldServerForDimension(0).SaveHandler.WorldDirectory, "stats");
				File var5 = new File(var4, var2.ToString() + ".json");

				if(!var5.exists())
				{
					File var6 = new File(var4, p_152602_1_.CommandSenderName + ".json");

					if(var6.exists() && var6.File)
					{
						var6.renameTo(var5);
					}
				}

				var3 = new StatisticsFile(this.mcServer, var5);
				var3.func_150882_a();
				this.field_148547_k.Add(var2, var3);
			}

			return var3;
		}

		public virtual void func_152611_a(int p_152611_1_)
		{
			this.viewDistance = p_152611_1_;

			if(this.mcServer.worldServers != null)
			{
				WorldServer[] var2 = this.mcServer.worldServers;
				int var3 = var2.Length;

				for(int var4 = 0; var4 < var3; ++var4)
				{
					WorldServer var5 = var2[var4];

					if(var5 != null)
					{
						var5.PlayerManager.func_152622_a(p_152611_1_);
					}
				}
			}
		}
	}

}